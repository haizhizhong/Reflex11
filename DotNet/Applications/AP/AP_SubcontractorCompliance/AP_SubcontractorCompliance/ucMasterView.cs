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
    public partial class ucMasterView : DevExpress.XtraEditors.XtraUserControl
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private ComboSearch.Util CBOUtil;
        private string RelType = "SUPPSUBCON";
        private const int CONST_HOLDBACK_APPROVAL = 71;

        public ucMasterView(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            CBOUtil = new ComboSearch.Util(DevXMgr);
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
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

        private void ucMasterView_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        public void RefreshDS()
        {
            dsSupplier1.Clear();
            dsType1.Clear();

            daSupplier.Fill(dsSupplier1);
            daType.Fill(dsType1);

            dsProjects1.Clear();
            daProjects.Fill(dsProjects1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboSupp.EditValue = null;
            cboName.EditValue = null;
            lueCode.EditValue = null;
            lueDesc.EditValue = null;
            deFrom.EditValue = null;
            deTo.EditValue = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool bFieldsEmpty = true;
            daSearch.SelectCommand.Parameters["@supplier_id"].Value = DBNull.Value;

            if (!cboSupp.Text.Trim().Equals(""))
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@supplier"].Value = cboSupp.Text.Replace("'", "''");
            }
            else
            {
                daSearch.SelectCommand.Parameters["@supplier"].Value = DBNull.Value;
            }

            if (!cboName.Text.Trim().Equals(""))
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@name"].Value = cboName.Text.Replace("'", "''");
            }
            else
            {
                daSearch.SelectCommand.Parameters["@name"].Value = DBNull.Value;
            }

            if (lueCode.EditValue != null && lueCode.EditValue != DBNull.Value)
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@insur_type_id"].Value = lueCode.EditValue;
            }
            else
            {
                daSearch.SelectCommand.Parameters["@insur_type_id"].Value = DBNull.Value;
            }

            if (lueDesc.EditValue != null && lueDesc.EditValue != DBNull.Value)
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@insur_type_id2"].Value = lueDesc.EditValue;
            }
            else
            {
                daSearch.SelectCommand.Parameters["@insur_type_id2"].Value = DBNull.Value;
            }

            if (deFrom.EditValue != null && deFrom.EditValue != DBNull.Value)
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@expiry_from"].Value = deFrom.EditValue;
            }
            else
            {
                daSearch.SelectCommand.Parameters["@expiry_from"].Value = DBNull.Value;
            }

            if (deTo.EditValue != null && deTo.EditValue != DBNull.Value)
            {
                bFieldsEmpty = false;
                daSearch.SelectCommand.Parameters["@expiry_to"].Value = deTo.EditValue;
            }
            else
            {
                daSearch.SelectCommand.Parameters["@expiry_to"].Value = DBNull.Value;
            }

            //no search for project ATB
            daSearch.SelectCommand.Parameters["@pri_id"].Value = DBNull.Value;

            if (bFieldsEmpty)
            {
                if (Popup.ShowPopup("Search criteria is empty, are you sure you would like to perform search?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                {
                    CL_Dialog.PleaseWait.Show("Loading Supplier's...", null);
                    dsSearch1.Clear();
                    daSearch.Fill(dsSearch1);
                    CL_Dialog.PleaseWait.Hide();
                }
            }
            else
            {
                CL_Dialog.PleaseWait.Show("Loading Supplier's...", null);
                dsSearch1.Clear();
                daSearch.Fill(dsSearch1);
                CL_Dialog.PleaseWait.Hide();
            }
        }

        private void gvSearch_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;		
        }

        private void gvSearch_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow dr = gvSearch.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["id"] = 0;
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

        private void gvSearch_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvSearch.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                bool ProjectRequired = false;
                string Frequency = "";
                try { ProjectRequired = Convert.ToBoolean(dr["project_required"]); }
                catch { }
                try { Frequency = dr["Frequency"].ToString(); }
                catch { }


                object oSupp = dr["supplier_id"];
                if (oSupp == null || oSupp == DBNull.Value)
                {
                    e.ErrorText = "Supplier is required.";
                    gvSearch.FocusedColumn = colSupplier;
                    e.Valid = false;
                    return;
                }

                object oCode = dr["insur_type_id"];
                if (oCode == null || oCode == DBNull.Value)
                {
                    e.ErrorText = "Code is required.";
                    gvSearch.FocusedColumn = colCode;
                    e.Valid = false;
                    return;
                }

                if (Frequency == "")
                {
                    object oExpiry = dr["expiry_date"];
                    if (oExpiry == null || oExpiry == DBNull.Value)
                    {
                        e.ErrorText = "Expiry date is required.";
                        gvSearch.FocusedColumn = colExpiry;
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
                        gvSearch.FocusedColumn = colpri_id_Code;
                        e.Valid = false;
                        return;

                    }
                }

                object oID = dr["ID"];
                if (Convert.ToInt32(oID) == 0)
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + oSupp + " and insur_type_id=" + oCode + " and isnull(pri_id, -1) = " + oProject;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "A supplier already exists with the selected code.";
                        gvSearch.FocusedColumn = colSupplier;
                        e.Valid = false;
                        return;
                    }
                }
                else
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + oSupp + " and insur_type_id=" + oCode + " and id <> " + oID + " and isnull(pri_id, -1) = " + oProject;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "A supplier already exists with the selected code.";
                        gvSearch.FocusedColumn = colSupplier;
                        e.Valid = false;
                        return;
                    }
                }                
            }
        }

        private void gvSearch_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
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
                    catch (SqlException)
                    {
                        Popup.ShowPopup("Error saving record.");
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

        private void gcSearch_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popup.ShowPopup("Are you sure you want to delete this record?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
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
            else if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
                if (gvSearch.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                    gvSearch.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle ||
                    gvSearch.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                    return;

                DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
                if (dr != null)
                    dr.RejectChanges();
            }
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void cboSupp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, false,
                    "select distinct supplier from supplier_master where isnull(SUBCONTRACTOR,'F') = 'T' order by supplier");
            }
        }

        private void cboName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, false,
                    "select distinct name from supplier_master where isnull(SUBCONTRACTOR,'F') = 'T' order by name");
            }
        }

        private void riSharepoint_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
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
                    }
                }
            }
        }

        private void riDirectAttachment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
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

        private void gvSearch_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow row = gvSearch.GetDataRow(e.RowHandle);
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
		


        private void riTypeCode_EditValueChanged(object sender, EventArgs e)
        {
            DataRow row = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
            object sourceRow = riTypeCode.GetDataSourceRowByKeyValue(((LookUpEdit)sender).EditValue);
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
                DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
                if (dr != null)
                {
                    Compliance.LoadRules(Connection, dr, Convert.ToInt32(oID));
                }
            }
        }

        private void riTypeDesc_EditValueChanged(object sender, EventArgs e)
        {
            DataRow row = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
            object sourceRow = riTypeDesc.GetDataSourceRowByKeyValue(((LookUpEdit)sender).EditValue);
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
                DataRow dr = gvSearch.GetDataRow(gvSearch.FocusedRowHandle);
                if (dr != null)
                {
                    Compliance.LoadRules(Connection, dr, Convert.ToInt32(oID));
                }
            }
        }
    }
}
