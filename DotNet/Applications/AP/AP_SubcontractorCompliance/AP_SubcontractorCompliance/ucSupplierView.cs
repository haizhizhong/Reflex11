using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using HMConnection;
using WS_Popups;
using System.Data.SqlClient;

namespace AP_SubcontractorCompliance
{
    public partial class ucSupplierView : DevExpress.XtraEditors.XtraUserControl
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private int _SupplierID = -1;
        private string RelType = "SUPPSUBCON";
        private const int CONST_HOLDBACK_APPROVAL = 71;

        public ucSupplierView(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
            dsSuppView1.AP_SUBCON_COMP_SEARCH.idColumn.ReadOnly = false;
            SetupHoldbackRelease();
        }

        private void SetupHoldbackRelease()
        {
            string sSQL = @"select count(*) from Approval_Topic where Active = 1 and ID=" + CONST_HOLDBACK_APPROVAL;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection)) == 0)
            {
                colrule_holdback_release.Visible = false;
                colrule_holdback_release.OptionsColumn.ShowInCustomizationForm = false;
            }
        }

        public void SetLinkMode()
        {
            gcSuppCon.EmbeddedNavigator.Buttons.Append.Visible = false;
            gcSuppCon.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcSuppCon.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcSuppCon.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gcSuppCon.EmbeddedNavigator.Buttons.Remove.Visible = false;
        }

        public int GetFocusedCompliance()
        {
            int iID = -1;

            DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["id"];
                if (oID != null && oID != DBNull.Value)
                    iID = Convert.ToInt32(oID);
            }

            return iID;
        }

        private void ucSupplierView_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        public int SupplierID
        {
            set
            {
                _SupplierID = value;
                dsSuppView1.Clear();
                daSuppView.SelectCommand.Parameters["@supplier_id"].Value = _SupplierID;
                daSuppView.SelectCommand.Parameters["@supplier"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@name"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@insur_type_id"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@insur_type_id2"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@expiry_from"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@expiry_to"].Value = DBNull.Value;
                daSuppView.SelectCommand.Parameters["@pri_id"].Value = DBNull.Value;
                daSuppView.Fill(dsSuppView1);
            }
        }

        public void RefreshDS()
        {
            dsType.Clear();
            daType.Fill(dsType);

            dsProjects1.Clear();
            daProjects.Fill(dsProjects1);
        }

        private void gvSuppCon_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;	
        }

        private void gvSuppCon_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {          
            DataRow dr = gvSuppCon.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["id"] = 0;
                dr["supplier_id"] = _SupplierID;
                dr["rule_warning"] = "F";
                dr["rule_accrual"] = "F";
                dr["rule_payment"] = "F";
                dr["rule_holdback_release"] = "F";
                dr["rule_po"] = "F";
                dr["webrule_internal_alert"] = "F";
                dr["webrule_warn_contractor"] = "F";
                dr["webrule_restrict_payment_req"] = "F";
                dr["active"] = false;
            }
        }

        private void gvSuppCon_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvSuppCon.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                bool ProjectRequired = false;
                string Frequency = "";
                try{ProjectRequired = Convert.ToBoolean(dr["project_required"]);}catch { }
                try{ Frequency = dr["Frequency"].ToString(); } catch { }


                object oCode = dr["insur_type_id"];
                if (oCode == null || oCode == DBNull.Value)
                {
                    e.ErrorText = "Code is required.";
                    gvSuppCon.FocusedColumn = colCode;
                    e.Valid = false;
                    return;
                }

                if (Frequency == "")
                {
                    object oExpiry = dr["expiry_date"];
                    if (oExpiry == null || oExpiry == DBNull.Value)
                    {
                        e.ErrorText = "Expiry date is required.";
                        gvSuppCon.FocusedColumn = colExpiry;
                        e.Valid = false;
                        return;

                    }
                }

                object oProject = dr["pri_id"];
                if (oProject == null || oProject == DBNull.Value)
                    oProject = -1;
                if (ProjectRequired)
                {
                    if (Convert.ToInt32(oProject) == -1)
                    {
                        e.ErrorText = "Project is required.";
                        gvSuppCon.FocusedColumn = colpri_id_Code;
                        e.Valid = false;
                        return;

                    }
                }
                                
                object oID = dr["ID"];
                if (Convert.ToInt32(oID) == 0)
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + _SupplierID + " and insur_type_id=" + oCode + " and isnull(pri_id, -1) = " + oProject;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "The selected code already exists for the supplier.";
                        gvSuppCon.FocusedColumn = colCode;
                        e.Valid = false;
                        return;
                    }
                }
                else
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + _SupplierID + " and insur_type_id=" + oCode + " and id <> " + oID + " and isnull(pri_id, -1) = " + oProject;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "The selected code already exists for the supplier.";
                        gvSuppCon.FocusedColumn = colCode;
                        e.Valid = false;
                        return;
                    }
                }
            }
        }

        private void gvSuppCon_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRowView drv = e.Row as DataRowView;
            if (drv != null)
            {
                DataRow dr = drv.Row;
                if (dr != null)
                {
                    SqlCommand cmd = daInsertUpdate.SelectCommand;
                    
                    object oID = dr["id"];
                    if (oID == null || oID.Equals(0))
                        oID = DBNull.Value;

                    cmd.Parameters["@id"].Value = oID;
                    cmd.Parameters["@supplier_id"].Value = dr["supplier_id"];
                    cmd.Parameters["@insur_type_id"].Value = dr["insur_type_id"];
                    cmd.Parameters["@expiry_date"].Value = dr["expiry_date"];
                    cmd.Parameters["@rule_warning"].Value = dr["rule_warning"];
                    cmd.Parameters["@rule_restrict"].Value = dr["rule_restrict"];
                    cmd.Parameters["@rule_accrual"].Value = dr["rule_accrual"];
                    cmd.Parameters["@rule_payment"].Value = dr["rule_payment"];
                    cmd.Parameters["@rule_holdback_release"].Value = dr["rule_holdback_release"];
                    cmd.Parameters["@rule_po"].Value = dr["rule_po"];
                    cmd.Parameters["@webrule_internal_alert"].Value = dr["webrule_internal_alert"];
                    cmd.Parameters["@webrule_warn_contractor"].Value = dr["webrule_warn_contractor"];
                    cmd.Parameters["@webrule_restrict_payment_req"].Value = dr["webrule_restrict_payment_req"];
                    cmd.Parameters["@pri_id"].Value = dr["pri_id"];
                    cmd.Parameters["@active"].Value = dr["active"];

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    try
                    {
                        da.Fill(dt);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dtdr = dt.Rows[0];
                                if (dtdr != null)
                                {
                                    dr.BeginEdit();
                                    dr["id"] = dtdr["id"];
                                    dr.AcceptChanges();
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        //Popup.ShowPopup("Error saving record.");
                        Popup.ShowPopup("Error saving record." + ex.Source + "\n" + ex.StackTrace);
                    
                    }
                    finally
                    {
                        if (cmd.Connection.State == System.Data.ConnectionState.Open)
                            cmd.Connection.Close();
                        da.Dispose();
                    }
                }
            }
        }

        private void gcSuppCon_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popup.ShowPopup("Are you sure you want to delete this record?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oID = dr["id"];
                        if (oID != null && oID != DBNull.Value)
                        {
                            string sDelete = "delete from SUPPLIER_SUBCON_COMPLIANCE where id=" + oID;
                            Connection.SQLExecutor.ExecuteNonQuery(sDelete, Connection.TRConnection);
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                if (_SupplierID > 0)
                {
                    string sqlStr = "select COUNT(*) from SUPPLIER_MASTER where SUBCONTRACTOR = 'T' and SUPPLIER_ID = " + _SupplierID.ToString();
                    int count = Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sqlStr, Connection.TRConnection));
                    if (count == 0)
                    {
                        Popup.ShowPopup("Subcontractor compliance is not allowed to be set because this supplier is not a subcontractor." );
                        e.Handled = true;
                    }
                }
            }
            else if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
                if (gvSuppCon.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                    gvSuppCon.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle ||
                    gvSuppCon.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                    return;

                DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
                if (dr != null)
                    dr.RejectChanges();
            }
        }

        private void riSharepoint_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["id"];
                if (oID != null && oID != DBNull.Value && Convert.ToInt32(oID) != 0)
                {
                    using (SharePointMgr.frmSharePointMgr SharePointManager = new SharePointMgr.frmSharePointMgr(Connection, DevXMgr, "Supplier Compliance", "Supplier Compliance", "", "Header", Convert.ToInt32(oID)))
                    {
                        SharePointManager.ReadOnly = false;
                        SharePointManager.ShowDialog();
                        SharePointManager.Dispose();

                        //SharePointMgr.cSharePointMgr.UpdateButton
                        //SharePointMgr.cSharePointMgr.UpdateButton(hmConn, SPType, ref btnSharePoint, "WOHeader", _pri_id);
                    }
                }
            }
        }

        private void riDirectAttach_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["id"];
                if (oID != null && oID != DBNull.Value)
                {
                    int iID = Convert.ToInt32(oID);

                    try
                    {

                        using (WO_CentralizedFSManager.frmFileManager frm = new WO_CentralizedFSManager.frmFileManager(Connection, DevXMgr,
                            WO_CentralizedFSManager.DocumentViewerMode.All, RelType, iID, false))
                        {
                            frm.ShowDialog();

                        }
                    }
                    catch { }
                }
            }
        }

        private void gvSuppCon_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow row = gvSuppCon.GetDataRow(e.RowHandle);
            bool ProjectRequired = false;
            string Frequency = "";
            try
            {
                ProjectRequired = Convert.ToBoolean(row["project_required"]);
                Frequency = row["Frequency"].ToString();
            }
            catch { }


            if (e.Column == colpri_id_Code)
            {
                if (ProjectRequired)
                    e.RepositoryItem = riProjectCode;
                else
                    e.RepositoryItem = riReadOnlyBlank;
            }
            else if (e.Column == colpri_id_Desc)
            {
                if (ProjectRequired)
                    e.RepositoryItem = riProjectDesc;
                else
                    e.RepositoryItem = riReadOnlyBlank;

            }
            else if (e.Column == colactive)
            {
                if (Frequency == "No Expiry")
                    e.RepositoryItem = riCheckBool;
                else
                    e.RepositoryItem = riReadOnlyBlank;
            }
            else if (e.Column == colExpiry)
            {
                if (Frequency != "Expiry Based")
                    e.RepositoryItem = riReadOnlyBlank;
            }

        }

        private void riCode_EditValueChanged(object sender, EventArgs e)
        {
            DataRow row = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
            object sourceRow = riCode.GetDataSourceRowByKeyValue(((LookUpEdit)sender).EditValue);
            if (sourceRow != null)
            {
                row["project_required"] = Convert.ToBoolean( ((DataRowView)sourceRow)["project_required"]);
                row[colFrequency.FieldName] = ((DataRowView)sourceRow)["Frequency"].ToString();
            }
            else
            {
                row["project_required"] = false;
                row[colFrequency.FieldName] = "";
            }
            row[colpri_id_Code.FieldName] = DBNull.Value;
            row[colExpiry.FieldName] = DBNull.Value;

            LookUpEdit lue = sender as LookUpEdit;
            if (lue != null)
            {
                object oID = lue.EditValue;
                if (oID == null || oID == DBNull.Value)
                    oID = -1;
                DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
                if (dr != null)
                {
                    Compliance.LoadRules(Connection, dr, Convert.ToInt32(oID));
                }
            }
        }

        private void riDesc_EditValueChanged(object sender, EventArgs e)
        {
            DataRow row = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
            object sourceRow = riDesc.GetDataSourceRowByKeyValue(((LookUpEdit)sender).EditValue);
            if (sourceRow != null)
            {
                row["project_required"] = Convert.ToBoolean(((DataRowView)sourceRow)["project_required"]);
                row[colFrequency.FieldName] = ((DataRowView)sourceRow)["Frequency"].ToString();
            }
            else
            {
                row["project_required"] = false;
                row[colFrequency.FieldName] = "";
            }
            row[colpri_id_Code.FieldName] = DBNull.Value;
            row[colExpiry.FieldName] = DBNull.Value;

            LookUpEdit lue = sender as LookUpEdit;
            if (lue != null)
            {
                object oID = lue.EditValue;
                if (oID == null || oID == DBNull.Value)
                    oID = -1;
                DataRow dr = gvSuppCon.GetDataRow(gvSuppCon.FocusedRowHandle);
                if (dr != null)
                {
                    Compliance.LoadRules(Connection, dr, Convert.ToInt32(oID));
                }
            }
        }

    }
}
