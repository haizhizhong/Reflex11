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
    public partial class ucProjectView : DevExpress.XtraEditors.XtraUserControl
    {

        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private int _PriID = -1;
        private string RelType = "SUPPSUBCON";
        private const int CONST_HOLDBACK_APPROVAL = 71;

        public ucProjectView(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
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
            dsProjectView1.AP_SUBCON_COMP_SEARCH.idColumn.ReadOnly = false; 
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

        public int PriID
        {
            set
            {
                _PriID = value;
                dsProjectView1.Clear();
                daProjectView.SelectCommand.Parameters["@pri_id"].Value = _PriID;
                daProjectView.SelectCommand.Parameters["@supplier_id"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@supplier"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@name"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@insur_type_id"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@insur_type_id2"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@expiry_from"].Value = DBNull.Value;
                daProjectView.SelectCommand.Parameters["@expiry_to"].Value = DBNull.Value;
                daProjectView.Fill(dsProjectView1);
            }
        }

        public void SetReadOnly()
        {
            gvProjView.OptionsBehavior.Editable = false;
        }


        private void ucProjectView_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        } 

        public void RefreshDS()
        {
            dsType1.Clear();
            daType.Fill(dsType1);

            dsSupplier1.Clear();
            daSupplier.Fill(dsSupplier1);           
        }

        private void gvProjView_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvProjView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow dr = gvProjView.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["id"] = 0;
                dr["pri_id"] = _PriID;
                dr["rule_warning"] = "F";
                dr["rule_accrual"] = "F";
                dr["rule_payment"] = "F";
                dr["rule_holdback_release"] = "F";
                dr["rule_po"] = "F";
                dr["webrule_internal_alert"] = "F";
                dr["webrule_warn_contractor"] = "F";
                dr["webrule_restrict_payment_req"] = "F";
                dr[colactive.FieldName] = false;
                dr["project_required"] = true;
            }
        }

        private void gvProjView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvProjView.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                object oSupp = dr["supplier_id"];
                if (oSupp == null || oSupp == DBNull.Value)
                {
                    e.ErrorText = "Supplier is required.";
                    gvProjView.FocusedColumn = colSupplier;
                    e.Valid = false;
                    return;
                }

                object oCode = dr["insur_type_id"];
                if (oCode == null || oCode == DBNull.Value)
                {
                    e.ErrorText = "Code is required.";
                    gvProjView.FocusedColumn = colCode;
                    e.Valid = false;
                    return;
                }

                //object oExpiry = dr["expiry_date"];
                //if (oExpiry == null || oExpiry == DBNull.Value)
                //{
                //    e.ErrorText = "Expiry date is required.";
                //    gvProjView.FocusedColumn = colExpiry;
                //    e.Valid = false;
                //    return;
                //}

                object oID = dr["ID"];
                if (Convert.ToInt32(oID) == 0)
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + oSupp + " and insur_type_id=" + oCode + " and isnull(pri_id, -1) = " + _PriID;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "A supplier already exists with the selected code.";
                        gvProjView.FocusedColumn = colSupplier;
                        e.Valid = false;
                        return;
                    }
                }
                else
                {
                    string sSelect = "select count(*) from SUPPLIER_SUBCON_COMPLIANCE where supplier_id=" + oSupp + " and insur_type_id=" + oCode + " and id <> " + oID + " and isnull(pri_id, -1) = " + _PriID;
                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                    {
                        e.ErrorText = "A supplier already exists with the selected code.";
                        gvProjView.FocusedColumn = colSupplier;
                        e.Valid = false;
                        return;
                    }
                }
            }
        }

        private void gvProjView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
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
                    DataRow dr = gvProjView.GetDataRow(gvProjView.FocusedRowHandle);
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
                if (gvProjView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                    gvProjView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle ||
                    gvProjView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                    return;

                DataRow dr = gvProjView.GetDataRow(gvProjView.FocusedRowHandle);
                if (dr != null)
                    dr.RejectChanges();
            }
        }

       

        private void riSharepoint_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvProjView.GetDataRow(gvProjView.FocusedRowHandle);
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
            DataRow dr = gvProjView.GetDataRow(gvProjView.FocusedRowHandle);
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

        private void ComplianceChange_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lue = sender as LookUpEdit;
            if( lue != null )
            {
                object oID = lue.EditValue;
                if( oID == null || oID == DBNull.Value )
                    oID = -1;
                DataRow dr = gvProjView.GetDataRow(gvProjView.FocusedRowHandle);
                if (dr != null)
                {
                    Compliance.LoadRules(Connection, dr, Convert.ToInt32(oID));
                }
            }
        }
    }
}
