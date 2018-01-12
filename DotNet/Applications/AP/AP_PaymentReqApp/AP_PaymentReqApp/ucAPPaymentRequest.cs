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

namespace AP_PaymentReqApp
{
    public partial class ucAPPaymentRequest : DevExpress.XtraEditors.XtraUserControl
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private HMCon Connection;
        private frmPopup Popup;
        private KeyControlAccess.Validator KCA_Validator;	
        private AlertViews.Costing.ucAPInvDet APInvDet;
        private AlertViews.Costing.ucAPHBDet APHBDet;
        private AlertViews.Costing.ucWebInvDet WebInvDet;
        private const int CONST_NON_PO_ROUTING = 70;
        private const int CONST_SUPPLIER_EDIT = 21;
        private bool _bForceWFRouting = false;

        public ucAPPaymentRequest(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, KeyControlAccess.Validator KCA_Validator)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            this.KCA_Validator = KCA_Validator;
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
            WEB_Conn.ConnectionString = Connection.WebConnection;

            APInvDet = new AlertViews.Costing.ucAPInvDet();            
            APInvDet.HMConnection = Connection;
            APInvDet.TUC_DevXMgr = DevXMgr;
            APInvDet.SetEditable();
            APInvDet.ReadOnly(false);
            APInvDet.Dock = DockStyle.Fill;
            APInvDet.Parent = dpDetails;

            APHBDet = new AlertViews.Costing.ucAPHBDet();
            APHBDet.HMConnection = Connection;
            APHBDet.TUC_DevXMgr = DevXMgr;
            APHBDet.Dock = DockStyle.Fill;
            APHBDet.Parent = dpDetails;

            WebInvDet = new AlertViews.Costing.ucWebInvDet(Connection, DevXMgr);
            WebInvDet.SetEditable();
            WebInvDet.WorkFlowApproval = false;
            WebInvDet.ReadOnly(false);
            WebInvDet.Dock = DockStyle.Fill;
            WebInvDet.Parent = dpDetails;

            APInvDet.BringToFront();

            SetupDT();
            CheckRouting();
            SetupApprovalDT();
            RefreshMe(false);
        }

        private void SetupApprovalDT()
        {
            DataTable dtApproval = new DataTable("dtApproval");
            dtApproval.Columns.Add("ID", typeof(string));
            dtApproval.Columns.Add("Desc", typeof(string));

            dtApproval.Rows.Add(new object[] { "A", "Approved" });
            dtApproval.Rows.Add(new object[] { "D", "Declined" });
            dtApproval.Rows.Add(new object[] { "P", "Pending" });
            dtApproval.Rows.Add(new object[] { "Q", "Routing Required" });
            dtApproval.Rows.Add(new object[] { "R", "Recalled" });

            riRouteStatus.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRouteStatus.DataSource = dtApproval;
            riRouteStatus.DisplayMember = "Desc";
            riRouteStatus.ValueMember = "ID";
        }

        private void CheckRouting()
        { 
            string sSelect = "select ISNULL(WORK_FLOW_ROUTING,'F') from AP_SETUP";
            object obj = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
            if( obj != null && obj != DBNull.Value )
            {
                //if (obj.Equals("T"))
                //{
                //    colWF_Approval_ID.Visible = true;
                //    colWF_Approval_ID.OptionsColumn.ShowInCustomizationForm = true;
                //    colWF_Approval_ID.VisibleIndex = 9;

                //    colnon_po_route.Visible = true;
                //    colnon_po_route.OptionsColumn.ShowInCustomizationForm = true;
                //    colnon_po_route.VisibleIndex = 9;
                    
                //    gcRequest.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
                //    gcRequest.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
                //}
            }

            sSelect = "select ISNULL(FORCE_WF_ROUTING,'F') from AP_SETUP";
            obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            if (obj != null && obj != DBNull.Value)
            {
                if (obj.Equals("T"))
                {
                    _bForceWFRouting = true;
                }
            }
        }

        public void RefreshMe()
        {
            RefreshMe(false);
        }

        public void RefreshMe(bool AppOrDec)
        {
            int iHandle = gvRequest.FocusedRowHandle;
            dsWorkFlow1.Clear();
            daWorkFlow.Fill(dsWorkFlow1);

            Connection.SQLExecutor.ExecuteNonQuery("exec AP_Fill_PayReqApprover '"+Connection.MLUser+"'", Connection.TRConnection);

            dsRequest1.Clear();
            daRequest.SelectCommand.Parameters["@username"].Value = Connection.MLUser;
            daRequest.Fill(dsRequest1);
            if (AppOrDec)
                gvRequest.FocusedRowHandle = iHandle - 1;
            else
                gvRequest.FocusedRowHandle = iHandle;
        }

        private void SetupDT()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("DESC");

            dt.Rows.Add(new object[] { "A", "Approved" });
            dt.Rows.Add(new object[] { "D", "Declined" });
            dt.Rows.Add(new object[] { "P", "Pending" });
            dt.Rows.Add(new object[] { "R", "Recalled" });
            dt.Rows.Add(new object[] { "N", "Not Submitted" });

            riStatus.DataSource = dt;
            riStatus.DisplayMember = "DESC";
            riStatus.ValueMember = "ID";
        }

        private void ucAPPaymentRequest_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void gvRequest_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                object oSUB_TYPE_C = dr["SUB_TYPE_C"];
                if (oSUB_TYPE_C.Equals("I"))
                {
                    object oID = dr["WS_INV_ID"];
                    if (oID == null || oID == DBNull.Value)
                        oID = -1;


                    string sSQL = @"select isnull(IsSubCon,0) from ws_inv_header where ws_inv_id = " + oID;
                    object oIsSubCon = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oIsSubCon == null || oIsSubCon == DBNull.Value)
                        oIsSubCon = 0;

                    if (Convert.ToBoolean(oIsSubCon))
                    {
                        WebInvDet.BringToFront();
                        WebInvDet.LoadDetail(Convert.ToInt32(oID));
                    }
                    else
                    {
                        APInvDet.BringToFront();
                        APInvDet.LoadDetail(Convert.ToInt32(oID));
                    }
                }
                if (oSUB_TYPE_C.Equals("H"))
                {
                    APHBDet.BringToFront();
                    object oWS_HB_ID = dr["WS_HB_ID"];
                    if (oWS_HB_ID != null && oWS_HB_ID != DBNull.Value)
                    {
                        APHBDet.LoadDetail(Convert.ToInt32(oWS_HB_ID));
                    }
                    else
                    {
                        APHBDet.LoadDetail(-1);
                    }
                }
            }
            else
            {
                WebInvDet.LoadDetail(-1);
                APInvDet.LoadDetail(-1);
                APHBDet.LoadDetail(-1);
            }
        }

        private void riApproveDecline_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                object oSUB_TYPE_C = dr["SUB_TYPE_C"];
                if (oSUB_TYPE_C != null && oSUB_TYPE_C != DBNull.Value)
                {
                    if (oSUB_TYPE_C.Equals("I"))
                    {
                        object oWS_INV_ID = dr["WS_INV_ID"];
                        object oSTATUS = dr["AP_ROUTING_STATUS"];
                        if (oWS_INV_ID != null && oWS_INV_ID != DBNull.Value)
                        {
                            if (oSTATUS.Equals("P"))
                            {
                                frmAppDec fAD = new frmAppDec(Connection, DevXMgr);
                                if (fAD.ShowDialog() == DialogResult.OK)
                                {
                                    string sSQL = @"select isnull(IsSubCon,0) from ws_inv_header where ws_inv_id = " + oWS_INV_ID;
                                    object oIsSubCon = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                                    if (oIsSubCon == null || oIsSubCon == DBNull.Value)
                                        oIsSubCon = 0;

                                    bool bValid = false;

                                    if (Convert.ToBoolean(oIsSubCon))
                                    {
                                        if (WebInvDet.ValidateRequest())
                                            bValid = true;
                                    }
                                    else
                                    {
                                        if (APInvDet.ValidateRequest())
                                            bValid = true;
                                    }

                                    if (bValid)
                                    {
                                        //approved
                                        sSQL = "declare @message varchar(max) " +
                                            "begin tran " +
                                            "exec AP_ApprovePaymentReq '" + Connection.MLUser + "', " + oWS_INV_ID + ", '" + fAD.Notes + "', @message output " +
                                            "if( @message = 'OK') " +
                                            "begin " +
                                            "   commit tran " +
                                            "end " +
                                            "else " +
                                            "begin " +
                                            "   rollback tran " +
                                            "end " +
                                            "select @message";
                                        string sMessage = "Error approving request.";
                                        object oResult = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                                        if (oResult != null && oResult != DBNull.Value)
                                        {
                                            sMessage = oResult.ToString();
                                        }

                                        if (sMessage.Equals("OK"))
                                        {
                                            RefreshMe(true);
                                        }
                                        else
                                        {
                                            Popup.ShowPopup(sMessage);
                                        }
                                    }
                                }
                                else if (fAD.DialogResult == DialogResult.Ignore)
                                {
                                    //declined
                                    string sSQL = "exec AP_DeclinePaymentReq '" + Connection.MLUser + "', " + oWS_INV_ID + ", '" + fAD.Notes + "'";
                                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                                    RefreshMe(true);
                                }
                            }
                            else
                            {
                                Popup.ShowPopup("Payment request has already been delt with.");
                            }
                        }
                    }
                    else if (oSUB_TYPE_C.Equals("H"))
                    {
                        object oWS_HB_ID = dr["WS_HB_ID"];
                        object oSTATUS = dr["AP_ROUTING_STATUS"];
                        if (oWS_HB_ID != null && oWS_HB_ID != DBNull.Value)
                        {
                            if (oSTATUS.Equals("P"))
                            {
                                string sApprovalStatus = "";
                                frmAppDec fAD = new frmAppDec(Connection, DevXMgr);
                                if (fAD.ShowDialog() == DialogResult.OK)
                                {
                                    sApprovalStatus = "A";//approved
                                }
                                else if (fAD.DialogResult == DialogResult.Ignore)
                                {
                                    sApprovalStatus = "D";//declined
                                }

                                if (!sApprovalStatus.Equals(""))
                                {
                                    string sSQL = "exec AP_HBPayReqAppDec '" + Connection.MLUser + "', " + oWS_HB_ID + ", '" + fAD.Notes + "', '" + sApprovalStatus+"'";
                                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                                    RefreshMe(true);
                                }
                            }
                            else
                            {
                                Popup.ShowPopup("Holdback payment request has already been delt with.");
                            }
                        }
                    }
                }
            }
        }

        private void riRouteStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                object oWF = dr["WF_Approval_ID"];
                if (oWF == null || oWF == DBNull.Value)
                {
                    gvRequest.FocusedColumn = colWF_Approval_ID;
                    Popup.ShowPopup("A work flow routing point is required prior to routing.");
                    return;
                }

                object oSTATUS = dr["NON_PO_ROUTE"];
                if (oSTATUS.Equals("Q") || oSTATUS.Equals("D") || oSTATUS.Equals("P") || oSTATUS.Equals("A"))
                {   
                    WorkFlowRouting.frmApprovalViewer fViewer = new WorkFlowRouting.frmApprovalViewer(Connection, DevXMgr);
                    fViewer.LoadStatus(WorkFlowRouting.frmApprovalViewer.Type.WS, Convert.ToInt32(dr["ws_inv_id"]));
                    if (fViewer.ShowDialog() == DialogResult.OK)
                    {
                        RefreshMe(false);
                    }
                }
            }
        }

        private void gvRequest_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(e.RowHandle);
            if (e.Column == colnon_po_route)
            {
                if (dr != null)
                {
                    object oStatus = dr["NON_PO_ROUTE"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals(""))
                        e.RepositoryItem = riEmpty;
                    else
                        e.RepositoryItem = riRouteStatus;
                }
            }
            else if (e.Column == colApprove)
            {
                if (dr != null)
                {
                    object oStatus = dr["NON_PO_ROUTE"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals("") || oStatus.Equals("A"))
                        e.RepositoryItem = riApproveDecline;
                    else
                        e.RepositoryItem = riApproveDeclineDisable;
                }
            }
            else if (e.Column == colWF_Approval_ID)
            {
                if (dr != null)
                {
                    object oStatus = dr["NON_PO_ROUTE"];
                    object oOrigin = dr["Origin"];
                    object oSubType = dr["Submission_Type"];
                    if (oOrigin != null && oOrigin != DBNull.Value && oSubType != null && oSubType != DBNull.Value)
                    {
                        if (oOrigin.Equals("Web") && oSubType.Equals("Non-PO"))
                            if (oStatus.Equals("A") || oStatus.Equals("P"))
                                e.RepositoryItem = riWorkFlowLocked;
                            else
                                e.RepositoryItem = riWorkflow;
                        else if (oOrigin.Equals("Web") && oSubType.Equals("PO"))
                        {
                            if (oStatus.Equals("A") || oStatus.Equals("P"))
                            {
                                e.RepositoryItem = riWorkFlowLocked;
                            }
                            else
                            {
                                object oSUB_TYPE_C = dr["SUB_TYPE_C"];
                                if (oSUB_TYPE_C.Equals("I"))
                                {
                                    int iWF_ID = -1;
                                    int iPRI_ID = -1;
                                    string sSQL = @"select isnull(p.wf_approval_id,-1), isnull(w.PRI_ID,-1) 
                                        from AP_PayReqApprover a 
                                        join WS_INV_HEADER w on w.WS_INV_ID=a.WS_INV_ID
                                        left outer join po_header p on p.PO_ID=w.po_id
                                        where a.id=" + dr["id"];
                                    DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            DataRow drE = dt.Rows[0];
                                            if (drE != null)
                                            {
                                                object obj = drE[0];
                                                if (obj != null && obj != DBNull.Value)
                                                    iWF_ID = Convert.ToInt32(obj);
                                                obj = drE[1];
                                                if (obj != null && obj != DBNull.Value)
                                                    iPRI_ID = Convert.ToInt32(obj);

                                                if (iWF_ID == -1 && iPRI_ID == -1)
                                                    e.RepositoryItem = riWorkflow;
                                                else
                                                    e.RepositoryItem = riEmpty;
                                            }
                                            else
                                                e.RepositoryItem = riEmpty;
                                        }
                                        else
                                            e.RepositoryItem = riEmpty;
                                    }
                                    else
                                        e.RepositoryItem = riEmpty;
                                }
                                else if (oSUB_TYPE_C.Equals("H"))
                                {
                                    e.RepositoryItem = riWorkFlowLocked;
                                }
                                else
                                    e.RepositoryItem = riEmpty;
                            }
                        }
                        else
                            e.RepositoryItem = riEmpty;
                    }
                    else
                    {
                        e.RepositoryItem = riEmpty;
                    }
                }
            }
        }

        private void gvRequest_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                string sNon_PO_Route = "null";
                object oNON_PO_ROUTE = dr["NON_PO_ROUTE"];
                if (oNON_PO_ROUTE != null && oNON_PO_ROUTE != DBNull.Value)
                    sNon_PO_Route = "'" + oNON_PO_ROUTE + "'";

                string sWF_Approval_ID = "null";
                object oWF_Approval_ID = dr["WF_Approval_ID"];
                if (oWF_Approval_ID != null && oWF_Approval_ID != DBNull.Value)
                    sWF_Approval_ID = oWF_Approval_ID.ToString();

                string sUpdate = "update ws_inv_header set wf_approval_id=" + sWF_Approval_ID + ", NON_PO_ROUTE=" + sNon_PO_Route + " where ws_inv_id=" + dr["WS_INV_ID"];
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }            
        }

        private void riWorkflow_EditValueChanged(object sender, EventArgs e)
        {
            object oWF_Approval_ID = DBNull.Value;
            if (sender != null)
            {
                LookUpEdit lue = sender as LookUpEdit;
                if (lue != null)
                {
                    oWF_Approval_ID = lue.EditValue;
                }
            }
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                if (oWF_Approval_ID != null && oWF_Approval_ID != DBNull.Value)
                {
                    object oNON_PO_ROUTE = dr["NON_PO_ROUTE"];
                    if (oNON_PO_ROUTE == null || oNON_PO_ROUTE == DBNull.Value)
                    {
                        dr.BeginEdit();
                        dr["NON_PO_ROUTE"] = "Q";
                    }
                }
                else
                {
                    dr.BeginEdit();
                    if( _bForceWFRouting )
                        dr["NON_PO_ROUTE"] = "Q";
                    else
                        dr["NON_PO_ROUTE"] = DBNull.Value;
                }

                dr["WF_Approval_ID"] = oWF_Approval_ID;

                dr.EndEdit();              
                daRequest.Update(dsRequest1);
                gvRequest_RowUpdated(null, null);
            }
        }

        private void riWorkflow_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                object oNON_PO_ROUTE = dr["NON_PO_ROUTE"];
                if (oNON_PO_ROUTE != null && oNON_PO_ROUTE != DBNull.Value)
                {
                    if (oNON_PO_ROUTE.Equals("P") || oNON_PO_ROUTE.Equals("A"))
                        e.Cancel = true;
                }                
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshMe(false);
        }

        private void riWorkflow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                LookUpEdit lue = (LookUpEdit)sender;
                if (lue != null)
                {
                    lue.EditValue = DBNull.Value;
                }
            }
        }

        private void btnLinkCompAttch_Click(object sender, EventArgs e)
        {
            DataRow dr = gvRequest.GetDataRow(gvRequest.FocusedRowHandle);
            if (dr != null)
            {
                if (!KCA_Validator.ValidatePassword(CONST_SUPPLIER_EDIT))
                {
                    return;
                }

                object oWS_INV_ID = dr["WS_INV_ID"];
                if (oWS_INV_ID != null && oWS_INV_ID != DBNull.Value)
                {
                    string sSQL = @"select supplier_id from supplier_master where supplier='"+ dr["SUPPLIER"]+@"'";
                    object oSupplierID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oSupplierID == null || oSupplierID == DBNull.Value)
                        oSupplierID = -1;

                    AP_SubcontractorCompliance.frmDocHotLink fDHL = new AP_SubcontractorCompliance.frmDocHotLink(Connection, DevXMgr, "WSINV", Convert.ToInt32(oWS_INV_ID), Convert.ToInt32(oSupplierID));
                    fDHL.ShowDialog();
                }
            }
        }

    }
}
