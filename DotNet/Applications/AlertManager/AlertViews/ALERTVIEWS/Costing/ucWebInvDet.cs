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

namespace AlertViews.Costing
{
    public partial class ucWebInvDet : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;        
        private DM_CentralizedFSManager.ucFileManager FileMgr;
        private DM_CentralizedFSManager.FileLink[] FileLinks;
        private WO_CentralizedFSManager.ucFileManager CFS_FileMgr;
        private WS_Popups.frmPopup Popup;
        private GL_Account_Lookup_Rep.Repository_GL_Lookup GL_Repository;
        private ReflexChat.ucChat Chat;
        private int _Chat_FieldLink_ID;
        
        private int _DetailID = -1;
        private const int CONST_SUB_PAYMENT_REQUEST = 1;
        private int CONST_WS_INV_HEADER_LINK = -1;
        private object _CLog_FieldLink_ID;
        private bool _ReadOnly = true;
        private bool _Editable = false;
        private bool _WorkFlowApproval = false;
        private bool _USE_DM = false;
        private string RelType = "WSINV";

        private string SubmissionDetails = @"select c.KnownAs, l.Level_ID, l.Level_Description, a.response, a.response_time, a.response_notes
            from WF_Route AS h 
            JOIN WF_RouteDet AS a ON a.WF_Route_ID = h.WF_Route_ID 
            JOIN web_db.dbo.Contact AS c ON c.ID = a.contact_id 
            JOIN web_db.dbo.WS_Approval_Levels AS l ON l.Approval_Level_ID = a.approval_level_id
            where (h.Link_ID = @ws_inv_id) and h.WF_ApprovalPoint_ID=3
            order by a.WF_Route_ID desc, c.KnownAs";

        public ucWebInvDet()
        {
            InitializeComponent();
        }

        public ucWebInvDet(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr) : this()
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);

            LoadConnectionDefaults();

            ucHistoryView1.TypeID = CONST_SUB_PAYMENT_REQUEST;
            lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            DetNonEditable();
        }

        private void LoadConnectionDefaults()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
            LoadUnits();

            ucHistoryView1.HMConnection = Connection;
            ucHistoryView1.TUC_DevXMgr = DevXMgr;

            SetupReferencePicker();
            SetupDMAttachments();
            SetupChat();

            _CLog_FieldLink_ID = Connection.SQLExecutor.ExecuteScalar("exec dbo.sp_WS_ChatFieldLinkGetID 'WS_INV_HEADER.WS_INV_ID', 'PayApproval'", Connection.TRConnection);
            CONST_WS_INV_HEADER_LINK = Convert.ToInt32(_CLog_FieldLink_ID);
            ucHistoryView1.ChatFieldLink = CONST_WS_INV_HEADER_LINK;

            daWF_Route.SelectCommand.CommandText = SubmissionDetails.Replace("web_db", Connection.WebDB);
        }

        #region Properties

        public HMConnection.HMCon ResetHMConnection
        {
            set
            {
                if (!value.TRDB.Equals(Connection.TRDB))
                {
                    Connection = value;
                    LoadConnectionDefaults();
                }
            }
            get
            {
                return Connection;
            }
        }

        public bool AlertView
        {
            set
            {
                if (value)
                {
                    pReDist.Visible = false;
                    txtInvNo.Properties.ReadOnly = true;
                    lueWHSE.Properties.ReadOnly = true;
                    lueGST.Properties.ReadOnly = true;
                    colGLAccount.OptionsColumn.AllowEdit = false;
                    colReference.OptionsColumn.AllowEdit = false;
                    gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;

                    lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciSave.ShowInCustomizationForm = false;
                    tpSubDet.PageVisible = false;
                }
            }
        }

        private void DetNonEditable()
        {
            pReDist.Visible = false;
            colGLAccount.OptionsColumn.AllowEdit = false;
            colReference.OptionsColumn.AllowEdit = false;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        }

        public bool WorkFlowApproval
        {
            set
            {
                _WorkFlowApproval = value;
                if (_WorkFlowApproval)
                {
                    pReDist.Visible = true;
                    txtInvNo.Properties.ReadOnly = true;
                    lueWHSE.Properties.ReadOnly = true;
                    lueGST.Properties.ReadOnly = true;
                    colGLAccount.OptionsColumn.AllowEdit = true;
                    colReference.OptionsColumn.AllowEdit = true;
                    gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = true;
                    gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
                    gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = true;

                    lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lciSave.ShowInCustomizationForm = false;
                    DetNonEditable();
                }
                else
                {
                    pReDist.Visible = false;
                    txtInvNo.Properties.ReadOnly = false;
                    lueWHSE.Properties.ReadOnly = false;
                    lueGST.Properties.ReadOnly = false;
                    colGLAccount.OptionsColumn.AllowEdit = false;
                    colReference.OptionsColumn.AllowEdit = false;
                    gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;

                    lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lciSave.ShowInCustomizationForm = true;
                }
                tpSubDet.PageVisible = value;
            }
            get
            {
                return _WorkFlowApproval;
            }
        }

        #endregion

        #region Setup       

        private void SetupChat()
        {
            string cmdText = @"exec sp_WS_ChatFieldLinkGetID 'WS_INV_HEADER.WS_INV_ID', 'Subcontractor Payment Request'";
            // creates a unique id and puts into the chat table
            _Chat_FieldLink_ID = Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(cmdText, Connection.TRConnection));

            Chat = new ReflexChat.ucChat(Connection, DevXMgr, ReflexChat.ChatType.Default);
            Chat.MessageAdded += new ReflexChat.ucChat.DelegateMessageAdded(Chat_MessageAdded);
            Chat.Dock = DockStyle.Fill;
            Chat.Parent = tpChat;
        }

        private void Chat_MessageAdded(int Chat_Thread_ID)
        {
            string sSQL = @"declare @chat_thread_id int, @ws_inv_id int, @username varchar(50)
	            select @chat_thread_id=" + Chat_Thread_ID + @", @ws_inv_id=" + _DetailID + @", @username='" + Connection.MLUser + @"'
             
	            declare @contact_id int, @ServerUserName varchar(50), @AAP_ID int
	            select @AAP_ID = 65
	            select @contact_id=contact_id from WS_INV_HEADER where ws_inv_id=@ws_inv_id
	            if not exists( select * from chat_thread_assignee 
		            where chat_thread_id = @chat_thread_id and active_tf = 'T' and contact_id = @contact_id )
	            begin
		            insert into chat_thread_assignee (chat_thread_id, contact_id, active_tf, date_added)
		            select @chat_thread_id, @contact_id, 'T', GETDATE()
            	
		            delete from working_alert_contacts where USERNAME = @ServerUserName 
		            insert into working_alert_contacts (USERNAME, AAP_ID, Contact_ID)
		            select @ServerUserName, @AAP_ID, @contact_id

		            exec chat_message_alert @chat_thread_id, @contact_id, @username
	            end";
            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
        }

        private void SetupDMAttachments()
        {
            string sSQL = @"select ISNULL((select isnull(USE_DM_ON_PORTAL,0) from system_ctrl), 0)";
            object oUSE_DM = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oUSE_DM == null || oUSE_DM == DBNull.Value)
                oUSE_DM = false;
            _USE_DM = Convert.ToBoolean(oUSE_DM);
            if (_USE_DM)
            {
                if (FileMgr != null)
                {
                    try { FileMgr.Dispose(); }
                    catch { }
                    FileMgr = null;
                }
                FileMgr = new DM_CentralizedFSManager.ucFileManager(Connection, DevXMgr, DM_CentralizedFSManager.DocumentViewerMode.All, true, "F");
                FileMgr.Dock = DockStyle.Fill;
                FileMgr.Parent = tpAttachments;
                FileMgr.BringToFront();

                FileLinks = new DM_CentralizedFSManager.FileLink[1];
            }
            else
            {
                if (CFS_FileMgr != null)
                {
                    try { CFS_FileMgr.Dispose(); }
                    catch { }
                    CFS_FileMgr = null;
                }
                CFS_FileMgr = new WO_CentralizedFSManager.ucFileManager(Connection, DevXMgr,
                        WO_CentralizedFSManager.DocumentViewerMode.All, RelType, -1, true, "Work Flow Approval");
                CFS_FileMgr.Dock = DockStyle.Fill;
                CFS_FileMgr.Parent = tpAttachments;
                CFS_FileMgr.BringToFront();
            }
        }

        private void SetupReferencePicker()
        {
            PC_CostCodesLU.PopupCostCodeLookupRepository PopupCostCode = new PC_CostCodesLU.PopupCostCodeLookupRepository(Connection);
            PopupCostCode.Lv1ID_name = "lv1id";
            PopupCostCode.Lv2ID_name = "lv2id";
            PopupCostCode.Lv3ID_name = "lv3id";
            PopupCostCode.Lv4ID_name = "lv4id";
            PopupCostCode.PRI_ID_name = "pri_id";
            PopupCostCode.LEMOS_name = "lem_comp";
            PopupCostCode.ExpenseType_name = "EXPENSE_TYPE";
            PopupCostCode.CostCodeReferenceSelected += new PC_CostCodesLU.PopupCostCodeLookupRepository.CostCodeReferenceSelectedDelegate(PopupCostCode_CostCodeReferenceSelected);
            colReference.ColumnEdit = PopupCostCode;
        }

        private void PopupCostCode_CostCodeReferenceSelected(PC_CostCodesLU.CostCodeReference CCR)
        {
            if ((gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle ||
                gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle) && CCR.PriID == -1)
            {
                return;
            }

            object oPriID = gvDetail.GetFocusedRowCellValue(colPRI_ID);
            if ((oPriID == null || oPriID == DBNull.Value) && CCR.PriID == -1)
                return;

            gvDetail.SetFocusedRowCellValue(colReference, CCR.Reference);
            gvDetail.SetFocusedRowCellValue(collv1ID, CCR.Lv1ID);
            gvDetail.SetFocusedRowCellValue(collv2ID, CCR.Lv2ID);
            gvDetail.SetFocusedRowCellValue(collv3ID, CCR.Lv3ID);
            gvDetail.SetFocusedRowCellValue(collv4ID, CCR.Lv4ID);
            gvDetail.SetFocusedRowCellValue(colLEM_COMP, CCR.LEM);
            gvDetail.SetFocusedRowCellValue(colPRI_ID, CCR.PriID);
            gvDetail.SetFocusedRowCellValue(colEXPENSE_TYPE, CCR.ExpenseType);

            if (CCR.PriID != -1)
            {
                gvDetail.SetFocusedRowCellValue(colGLAccount, CCR.GLAccount);

                string sSelect = "select DESCRIPTION from GL_ACCOUNTS where ACCOUNT_NUMBER='" + CCR.GLAccount + "'";
                object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                gvDetail.SetFocusedRowCellValue(colGLAcctDesc, obj);
            }
            else
            {
                gvDetail.SetFocusedRowCellValue(colGLAccount, DBNull.Value);
                gvDetail.SetFocusedRowCellValue(colGLAcctDesc, DBNull.Value);
            }
        }

        public void SetEditable()
        {
            _Editable = true;

            colGLAccount.OptionsColumn.AllowEdit = true;
            colReference.OptionsColumn.AllowEdit = true;
            gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = true;
            gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
            gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = true;

            if (!_WorkFlowApproval)
            {
                txtInvNo.Properties.ReadOnly = false;
                lueWHSE.Properties.ReadOnly = false;
                lueGST.Properties.ReadOnly = false;

                lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciSave.ShowInCustomizationForm = true;
                lciSave.Move(lciInvoiceInfo, DevExpress.XtraLayout.Utils.InsertType.Bottom);
            }

            GL_Repository = new GL_Account_Lookup_Rep.Repository_GL_Lookup();
            GL_Repository.HideVailidation = true; //must be set before the hmconnection is set.
            GL_Repository.HideSubCode = true;
            GL_Repository.HMConnection = Connection;
            GL_Repository.DevXMgr = DevXMgr;
            GL_Repository.ValidateOnEnterKey = true;
            GL_Repository.EditValueChanged += new EventHandler(GL_Repository_EditValueChanged);
            colGLAccount.ColumnEdit = GL_Repository;
        }

        private void LoadUnits()
        {
            if (Connection.CountryCode == "U")
            {
                lciGST.Text = "Tax 1";
                lciPST.Text = "Tax 2";
                lciGSTAmt.Text = "Tax 1 Amount";
                lciGST1.Text = "Tax 1";
            }
        }

        private void GL_Repository_EditValueChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                LookUpEdit lue = sender as LookUpEdit;
                if (lue != null)
                {
                    object obj = lue.EditValue;
                    if (obj == null || obj == DBNull.Value)
                    {
                        gvDetail.SetFocusedRowCellValue(colGLAcctDesc, "");
                    }
                    else
                    {
                        gvDetail.SetFocusedRowCellValue(colGLAcctDesc, lue.GetColumnValue("DESCRIPTION"));
                    }
                }
            }
        }

        #endregion

        public void ResetConnections()
        {
            if (Connection != null)
            {
                LoadConnectionDefaults();
            }
        }

        public void LoadDetail(int DetailID, bool ReDistEditable)
        {
            LoadDetail(DetailID);
            if( !ReDistEditable )
                pReDist.Visible = false;
        }
        
        public void LoadDetail(int DetailID)
        {
            _DetailID = DetailID;            

            if (!_WorkFlowApproval)
            {                
                if (_Editable)
                {
                    txtInvNo.Properties.ReadOnly = false;
                    colGLAccount.OptionsColumn.AllowEdit = true;
                    colReference.OptionsColumn.AllowEdit = true;
                    gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = true;
                    gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
                    gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
                }

                string sSelect = "select isnull(PO_ID,-1) from WS_INV_HEADER where WS_INV_ID=" + DetailID;
                if (DetailID == -1)
                {
                    pReDist.Visible = false;
                }
                else if (Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection).Equals(-1))
                {
                    pReDist.Visible = true;
                }
                else
                {
                    pReDist.Visible = false;
                }
            }
            
            dsWarehouse1.Clear();
            daWarehouse.Fill(dsWarehouse1);

            dsGST1.Clear();
            daGST.Fill(dsGST1);

            ucHistoryView1.DetailID = DetailID;
            ucHistoryView1.LoadHistory();

            dsDetail.Clear();
            daDetail.SelectCommand.Parameters["@detail_id"].Value = DetailID;
            daDetail.Fill(dsDetail);
            DetNonEditable();

            LoadHeader(DetailID);            
        }

        private void LoadHeader(int DetailID)
        {
            ClearFields();

            string sSelect = @"select w.INV_DATE, w.DUE_DATE, w.SUPPLIER, w.SUPP_NAME, w.CON_INV_NO, g.GST_DESC, s.DESCRIPTION, w.INV_AMOUNT, 
                    w.PURCH_AMT, w.GST_AMT, p.PO, w.WHSE_ID, w.REFERENCE, w.COMMENT, isnull(w.pri_id,-1) [PriID], w.GST_CODE, 
                    isnull((select isnull(sum(isnull(pd.EXTENSION,0)),0) from po_detail pd where pd.po_id=w.po_id),0) [PO_AMT],
                    t.DESCRIPTION [TERMS], st.Description [SUPPLYTYPE], 
                    '('+CAST(ph.pri_code as varchar(15))+') - '+ph.pri_name [Project], isnull(w.Submitted,0) [Submitted], 
                    isnull(w.Approved,0) [Approved], isnull(w.PaidToDate,0) [PaidToDate], isnull(w.Remaining,0) [Remaining]
                from WS_INV_HEADER w 
                left outer join GST_CODES g on w.GST_CODE=g.GST_CODE 
                left outer join SALES_TAXES s on w.SALES_TAX_ID=s.SALES_TAX_ID 
                left outer join PO_HEADER p on w.PO_ID=p.PO_ID 
                left outer join TERMS t on t.TERMS_ID=p.TERMS_ID
                left outer join PO_SupplyType st on st.PO_SupplyType_ID=p.PO_SupplyType_ID
                left outer join PROJ_HEADER ph on ph.pri_id=w.pri_id
                where w.WS_INV_ID = " + DetailID;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        lueGST.EditValue = dr["GST_CODE"];
                        deInvDate.EditValue = dr["INV_DATE"];
                        deDueDate.EditValue = dr["DUE_DATE"];
                        txtSupp.EditValue = dr["SUPPLIER"];
                        txtName.EditValue = dr["SUPP_NAME"];
                        txtInvNo.EditValue = dr["CON_INV_NO"];
                        txtGST.EditValue = dr["GST_DESC"];
                        txtPST.EditValue = dr["DESCRIPTION"];
                        txtInvAmt.EditValue = dr["INV_AMOUNT"];
                        txtPurAmt.EditValue = dr["PURCH_AMT"];
                        txtGSTAmt.EditValue = dr["GST_AMT"];
                        txtPO.EditValue = dr["PO"];
                        lueWHSE.EditValue = dr["WHSE_ID"];
                        txtReference.EditValue = dr["REFERENCE"];
                        memoComment.EditValue = dr["COMMENT"];                        
                        txtPOAmt.EditValue = dr["PO_AMT"];
                        object oPriID = dr["PriID"];
                        sSelect = "select pri_holdback_date from proj_header where pri_id="+oPriID;
                        deHoldbackDate.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                        txtTerms.EditValue = dr["TERMS"];
                        txtPOSupplyType.EditValue = dr["SUPPLYTYPE"];
                        txtProject.EditValue = dr["Project"];
                        txtSubmitted.EditValue = Convert.ToDouble(dr["Submitted"]) + Convert.ToDouble(dr["PURCH_AMT"]);
                        txtApproved.EditValue = dr["Approved"];
                        txtPaidToDate.EditValue = dr["PaidToDate"];
                        double dRemaining = Convert.ToDouble(dr["Remaining"]) - Convert.ToDouble(dr["PURCH_AMT"]);
                        txtRemaining.EditValue = dRemaining;

                        object oPurchAmt = dr["PURCH_AMT"];
                        if (oPurchAmt == null || oPurchAmt == DBNull.Value)
                            oPurchAmt = 0;
                        double dPurchAmt = Convert.ToDouble(oPurchAmt);

                        object oPO_AMT = dr["PO_AMT"];
                        if (oPO_AMT == null || oPO_AMT == DBNull.Value)
                            oPO_AMT = 0;
                        double dPO_AMT = Convert.ToDouble(oPO_AMT);

                        if (dRemaining < 0)
                        {
                            txtPaymentStatus.EditValue = "OVER PAYMENT";
                            this.txtPaymentStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                            txtPaymentStatus.ForeColor = Color.Red;
                        }
                        else if (dRemaining > 0)
                        {
                            txtPaymentStatus.EditValue = "UNDER PAYMENT";
                            this.txtPaymentStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
                            txtPaymentStatus.ForeColor = Color.Black;
                        }
                        else
                        {
                            txtPaymentStatus.EditValue = "COMPLETE PAYMENT";
                            this.txtPaymentStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
                            txtPaymentStatus.ForeColor = Color.Black;
                        }
                    }
                }
            }

            sSelect = "select SUM(ISNULL(hold_amt,0)) from WS_INV_DET where WS_INV_ID=" + DetailID;
            txtHoldbackAmt.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            if (_USE_DM)
            {
                FileLinks[0] = new DM_CentralizedFSManager.FileLink("WS_INV_HEADER.WS_INV_ID", _DetailID, Connection.CompanyID, DM_CentralizedFSManager.FileLink.Database.TR, 0, true);
                FileMgr.DocumentFileLink = FileLinks;
            }
            else
            {
                CFS_FileMgr.RefreshFileList(RelType, _DetailID, true);
            }

            RefreshSubmissionDetails(_DetailID);
            RefreshChatThread(_DetailID);
        }

        private void ClearFields()
        {
            deInvDate.EditValue = null;
            deDueDate.EditValue = null;
            txtSupp.EditValue = null;
            txtInvNo.EditValue = null;
            txtName.EditValue = null;
            txtGST.EditValue = null;
            txtPST.EditValue = null;
            txtInvAmt.EditValue = null;
            txtPurAmt.EditValue = null;
            txtGSTAmt.EditValue = null;
            txtPO.EditValue = null;
            lueWHSE.EditValue = null;
            txtHoldbackAmt.EditValue = null;
            deHoldbackDate.EditValue = null;
            lueGST.EditValue = null;
            txtPOAmt.EditValue = null;
            txtReference.EditValue = null;
            memoComment.EditValue = null;
            txtTerms.EditValue = null;
            txtPOSupplyType.EditValue = null;
            txtProject.EditValue = null;
            txtSubmitted.EditValue = null;
            txtApproved.EditValue = null;
            txtPaidToDate.EditValue = null;
            txtRemaining.EditValue = null;
            txtPaymentStatus.EditValue = null;

            if (_USE_DM)
            {
                if (FileMgr != null)
                {
                    FileLinks[0] = new DM_CentralizedFSManager.FileLink("WS_INV_HEADER.WS_INV_ID", -1, Connection.CompanyID, DM_CentralizedFSManager.FileLink.Database.TR, 0, true);
                    FileMgr.DocumentFileLink = FileLinks;
                }
            }
            else
            {
                if (CFS_FileMgr != null)
                {
                    CFS_FileMgr.RefreshFileList(RelType, -1, true);
                }
            }
        }

        private void ucAPInvDet_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
            {
                DevXMgr.FormInit(this);
            }

            ucHistoryView1.Dock = DockStyle.None;
            ucHistoryView1.Dock = DockStyle.Fill;
        }

        public void ReadOnly(bool IsReadOnly)
        {
            _ReadOnly = IsReadOnly;
        }

        #region Header

        private void btnSave_Click(object sender, EventArgs e)
        {
            object oWHSE = lueWHSE.EditValue;
            if (oWHSE == null || oWHSE == DBNull.Value)
                oWHSE = "null";

            object oGST = lueGST.EditValue;
            if (oGST == null || oGST == DBNull.Value)
                oGST = "null";

            string sSelect = "exec WS_PaymentInvChange " + _DetailID + ", '" + txtInvNo.Text.Replace("'", "''") + "', " + oWHSE + ", " + Connection.ContactID + ", '" + oGST + "', " + txtGSTAmt.EditValue;
            Connection.SQLExecutor.ExecuteNonQuery(sSelect, Connection.TRConnection);

            ucHistoryView1.DetailID = _DetailID;
            ucHistoryView1.LoadHistory();
        }

        private void lueGST_EditValueChanged(object sender, EventArgs e)
        {
            double dAMT = 0;
            object oGST = lueGST.EditValue;
            if (oGST != null && oGST != DBNull.Value)
            {
                string sSQL = "select isnull(gst_pct,0) from gst_codes where gst_code='" + oGST + "'";
                object oPCT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oPCT == null || oPCT == DBNull.Value)
                    oPCT = 0;

                sSQL = "select sum(isnull(amount,0)) from ws_inv_det where ws_inv_id=" + _DetailID + " and isnull(taxable,'T') = 'T'";
                object oAmt = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oAmt == null || oAmt == DBNull.Value)
                    oAmt = 0;

                dAMT = Math.Round((Convert.ToDouble(oAmt) * Convert.ToDouble(oPCT) * .01), 2, MidpointRounding.AwayFromZero);
            }
            txtGSTAmt.EditValue = dAMT;

            object oPurAmt = txtPurAmt.EditValue;
            if (oPurAmt == null || oPurAmt == DBNull.Value)
                oPurAmt = 0;

            txtInvAmt.EditValue = Convert.ToDouble(oPurAmt) + dAMT;
        }

        public bool ValidateRequest()
        {
            bool bValid = true;
            string sMessage = "";

            btnSave_Click(null, null);

            if (txtInvNo.Text.Trim().Equals(""))
            {
                bValid = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
                txtInvNo.Focus();
                sMessage = "Invoice number is required.";
                goto Error;
            }

            string sSelect = @"declare @inv_no varchar(15), @supplier varchar(10)
                select @inv_no=con_inv_no, @supplier=supplier from ws_inv_header where ws_inv_id=" + _DetailID + @"
                select count(*) from ap_inv_header where inv_no=@inv_no and supplier=@supplier";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
            {
                bValid = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
                txtInvNo.Focus();
                sMessage = "The selected invoice number already exists, a different number is required before this request can be approved.";
                goto Error;
            }

            if (lueWHSE.EditValue == null || lueWHSE.EditValue == DBNull.Value)
            {
                bValid = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
                lueWHSE.Focus();
                sMessage = "A warehouse is required before this request can be approved.";
                goto Error;
            }

            if (lueGST.EditValue == null || lueGST.EditValue == DBNull.Value)
            {
                bValid = false;
                xtraTabControl1.SelectedTabPageIndex = 0;
                lueGST.Focus();
                sMessage = "A " + lueGST.Text + " code is required before this request can be approved.";
                goto Error;
            }

            sSelect = "select COUNT(*) from WS_INV_DET where WS_INV_ID = " + _DetailID + " and GL_ACCOUNT is null";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
            {
                bValid = false;
                xtraTabControl1.SelectedTabPageIndex = 1;
                sMessage = "All detail records require a GL Account before this request can be approved.";
                goto Error;
            }

            Error:
            if (!bValid)
            {
                Popup.ShowPopup(sMessage);
            }

            return bValid;
        }

        #endregion

        #region Detail

        private void gvDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                object oGLAcct = dr["GL_ACCOUNT"];

                if (oGLAcct == null || oGLAcct == DBNull.Value)
                {
                    e.ErrorText = "GL Account Required.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvDetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                object oPriID = dr["PRI_ID"];
                if (oPriID == null || oPriID == DBNull.Value)
                    oPriID = "null";
                object olv1ID = dr["lv1ID"];
                if (olv1ID == null || olv1ID == DBNull.Value)
                    olv1ID = "null";
                object olv2ID = dr["lv2ID"];
                if (olv2ID == null || olv2ID == DBNull.Value)
                    olv2ID = "null";
                object olv3ID = dr["lv3ID"];
                if (olv3ID == null || olv3ID == DBNull.Value)
                    olv3ID = "null";
                object olv4ID = dr["lv4ID"];
                if (olv4ID == null || olv4ID == DBNull.Value)
                    olv4ID = "null";
                object oLem = dr["LEM_COMP"];
                if (oLem == null || oLem == DBNull.Value)
                    oLem = "null";
                else
                    oLem = "'" + oLem + "'";
                object oExpense = dr["EXPENSE_TYPE"];
                if (oExpense == null || oExpense == DBNull.Value)
                    oExpense = "null";
                else
                    oExpense = "'" + oExpense + "'";
                object oReference = dr["REFERENCE"];
                if (oReference == null || oReference == DBNull.Value)
                    oReference = "null";
                else
                    oReference = "'" + oReference + "'";

                string sUpdate = "update ws_inv_det set "+
                    "gl_account='"+dr["GL_ACCOUNT"]+"', "+
                    "pri_id="+oPriID+", "+
                    "lv1id=" + olv1ID + ", " +
                    "lv2id=" + olv2ID + ", " +
                    "lv3id=" + olv3ID + ", " +
                    "lv4id=" + olv4ID + ", " +
                    "lem_comp=" + oLem + ", " +
                    "expense_type=" + oExpense + ", " +
                    "reference=" + oReference + " " +
                    "where ws_inv_det_id="+dr["WS_INV_DET_ID"];
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }
        }

        private void gvDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void btnReDist_Click(object sender, EventArgs e)
        {
            if (_DetailID != -1 )
            {
                WS_InvReDist.ucInvReDist ucIRD = new WS_InvReDist.ucInvReDist(Connection, DevXMgr, WS_InvReDist.ucInvReDist.Flavor.WS, _DetailID );
                if (ucIRD.ShowDialog() == DialogResult.OK)
                {
                    LoadDetail(_DetailID);
                }
            }
        }

        #endregion        

        #region Submission Details

        public void RefreshSubmissionDetails(int ws_inv_id)
        {
            dsWF_Route1.Clear();
            daWF_Route.SelectCommand.Parameters["@ws_inv_id"].Value = ws_inv_id;
            daWF_Route.Fill(dsWF_Route1);
        }

        #endregion

        #region Chat

        public void RefreshChatThread(int ws_inv_id)
        {
            if (Chat != null)
            {
                Chat.Enabled = false;

                if (ws_inv_id > 0)
                {
                    Chat.Enabled = true;
                }
                Chat.LoadChatThread(_Chat_FieldLink_ID, ws_inv_id);
            }
        }

        #endregion
    }
}
