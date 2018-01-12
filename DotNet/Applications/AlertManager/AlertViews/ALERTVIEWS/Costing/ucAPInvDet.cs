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
    public partial class ucAPInvDet : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private GL_Account_Lookup_Rep.Repository_GL_Lookup GL_Repository;
        private int _DetailID = -1;
        private string _Origin = "";
        private const int CONST_SUB_PAYMENT_REQUEST = 1;
        private int CONST_WS_INV_HEADER_LINK = -1;
        private object _CLog_FieldLink_ID;
        private bool _ReadOnly = true;
        private bool _Editable = false;
        private bool _WorkFlowApproval = false;

        public ucAPInvDet()
        {
            InitializeComponent();
            ucHistoryView1.TypeID = CONST_SUB_PAYMENT_REQUEST;
            lciSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        #region Properties

        public HMCon HMConnection
        {
            set
            {
                Connection = value;
                if (Connection != null)
                {
                    TR_Conn.ConnectionString = Connection.TRConnection;
                    LoadUnits();
                    ucHistoryView1.HMConnection = value;
                    ucChatLog1.HMConnection = value;
                    ucConsolidatedChatLog1.HMConnection = value;
                    SetupReferencePicker();
                    _CLog_FieldLink_ID = Connection.SQLExecutor.ExecuteScalar("exec dbo.sp_WS_ChatFieldLinkGetID 'WS_INV_HEADER.WS_INV_ID', 'PayApproval'", Connection.TRConnection);
                    CONST_WS_INV_HEADER_LINK = Convert.ToInt32(_CLog_FieldLink_ID);
                    ucHistoryView1.ChatFieldLink = CONST_WS_INV_HEADER_LINK;
                    ucChatLog1.ChatFieldLink = CONST_WS_INV_HEADER_LINK;
                    ucConsolidatedChatLog1.ChatFieldLink = CONST_WS_INV_HEADER_LINK;
                }
            }
            get
            {
                return Connection;
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUC_DevXMgr
        {
            set
            {
                DevXMgr = value;
                if (DevXMgr != null)
                {
                    Popup = new frmPopup(value);
                    ucHistoryView1.TUC_DevXMgr = value;
                    ucChatLog1.TUC_DevXMgr = value;
                    ucConsolidatedChatLog1.TUC_DevXMgr = value;
                }
            }
            get
            {
                return DevXMgr;
            }
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
                    lciSave.Move(lciExAttch, DevExpress.XtraLayout.Utils.InsertType.Right);
                }
            }
            get
            {
                return _WorkFlowApproval;
            }
        }

        #endregion

        public void ResetConnections()
        {
            if (Connection != null)
            {
                HMConnection = Connection;
                SetupReferencePicker();
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
            _Origin = "";

            if (!_WorkFlowApproval)
            {
                string sSelect = "select isnull(ORIGIN,'') from WS_INV_HEADER where WS_INV_ID=" + DetailID;
                object oOrigin = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                if (oOrigin != null && oOrigin != DBNull.Value)
                    _Origin = oOrigin.ToString();

                if (_Origin.Equals("A"))
                {
                    pReDist.Visible = false;
                    txtInvNo.Properties.ReadOnly = true;
                    colGLAccount.OptionsColumn.AllowEdit = false;
                    colReference.OptionsColumn.AllowEdit = false;
                    gcDetail.EmbeddedNavigator.Buttons.Edit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    gcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                }
                else
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

                    sSelect = "select isnull(PO_ID,-1) from WS_INV_HEADER where WS_INV_ID=" + DetailID;
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
            }
            
            dsWarehouse1.Clear();
            daWarehouse.Fill(dsWarehouse1);

            dsGST1.Clear();
            daGST.Fill(dsGST1);

            ucHistoryView1.DetailID = DetailID;
            ucHistoryView1.LoadHistory();

            ucChatLog1.LoadChat(DetailID);
            ucConsolidatedChatLog1.LoadChat(DetailID, 1);

            dsDetail.Clear();
            daDetail.SelectCommand.Parameters["@detail_id"].Value = DetailID;
            daDetail.Fill(dsDetail);

            LoadHeader(DetailID);            
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

        private void LoadHeader(int DetailID)
        {
            ClearFields();

            string sSelect = "select COUNT(*) from WS_ChatAttachment where CLog_FieldLink_ID=" + _CLog_FieldLink_ID + " and CLog_IDValue=" + DetailID;
            object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            btnExAttach.Text = "Attachments (" + obj + ")";

            sSelect = "select w.INV_DATE, w.DUE_DATE, w.SUPPLIER, w.SUPP_NAME, w.CON_INV_NO, g.GST_DESC, s.DESCRIPTION, w.INV_AMOUNT, " +
                "w.PURCH_AMT, w.GST_AMT, p.PO, w.WHSE_ID, w.REFERENCE, w.COMMENT, isnull(w.pri_id,-1) [PriID], w.GST_CODE, dbo.fnPO_GetPOTotal(w.po_id) [PO_AMT]" +
                "from WS_INV_HEADER w "+
                "left outer join GST_CODES g on w.GST_CODE=g.GST_CODE "+
                "left outer join SALES_TAXES s on w.SALES_TAX_ID=s.SALES_TAX_ID "+
                "left outer join PO_HEADER p on w.PO_ID=p.PO_ID "+
                "where w.WS_INV_ID = " + DetailID;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
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
                        lueGST.EditValue = dr["GST_CODE"];
                        txtPOAmt.EditValue = dr["PO_AMT"];
                        object oPriID = dr["PriID"];
                        sSelect = "select pri_holdback_date from proj_header where pri_id="+oPriID;
                        deHoldbackDate.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                    }
                }
            }

            sSelect = "select SUM(ISNULL(hold_amt,0)) from WS_INV_DET where WS_INV_ID=" + DetailID;
            txtHoldbackAmt.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);


            SharePointMgr.cSharePointMgr.UpdateButton(Connection, "Contractor Invoice", ref btnSharepoint, "Header", _DetailID);
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
            
            btnExAttach.Text = "Attachments (0)";
        }

        private void btnSharepoint_Click(object sender, EventArgs e)
        {
            using (SharePointMgr.frmSharePointMgr SharePointManager = new SharePointMgr.frmSharePointMgr(Connection, DevXMgr, "Contractor Invoice", "Contractor Invoice", "", "Header", _DetailID))
            {
                SharePointManager.ReadOnly = _ReadOnly;
                SharePointManager.ShowDialog();
                SharePointManager.Dispose();
                SharePointMgr.cSharePointMgr.UpdateButton(Connection, "Contractor Invoice", ref btnSharepoint, "Header", _DetailID);
            }
        }

        private void ucAPInvDet_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
                DevXMgr.FormInit(this);

            ucHistoryView1.Dock = DockStyle.None;
            ucHistoryView1.Dock = DockStyle.Fill;
        }

        public void ReadOnly(bool IsReadOnly)
        {
            _ReadOnly = IsReadOnly;
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
                lciSave.Move(lciExAttch, DevExpress.XtraLayout.Utils.InsertType.Right);
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

            string sSelect = "";
            if (_Origin.Equals("W"))
            {
                sSelect = @"declare @inv_no varchar(15), @supplier varchar(10)
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
                sMessage = "A "+lueGST.Text+" code is required before this request can be approved.";
                goto Error;
            }

            sSelect = "select COUNT(*) from WS_INV_DET where WS_INV_ID = "+_DetailID+" and GL_ACCOUNT is null";
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

        private void btnExAttach_Click(object sender, EventArgs e)
        {
            SubmissionHistory.frmAttachments fAttach = new SubmissionHistory.frmAttachments(Connection, DevXMgr, _DetailID, Convert.ToInt32(_CLog_FieldLink_ID));
            fAttach.ShowDialog();            
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
            ucConsolidatedChatLog1.LoadChat(_DetailID, 1);
        }

        private void lueGST_EditValueChanged(object sender, EventArgs e)
        {
            double dAMT = 0;
            object oGST = lueGST.EditValue;
            if (oGST != null && oGST != DBNull.Value)
            {
                string sSQL = "select isnull(gst_pct,0) from gst_codes where gst_code='"+oGST+"'";
                object oPCT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oPCT == null || oPCT == DBNull.Value)
                    oPCT = 0;

                sSQL = "select sum(isnull(amount,0)) from ws_inv_det where ws_inv_id="+_DetailID+" and isnull(taxable,'T') = 'T'";
                object oAmt = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oAmt == null || oAmt == DBNull.Value)
                    oAmt = 0;

                dAMT = Math.Round( ( Convert.ToDouble( oAmt ) * Convert.ToDouble( oPCT ) * .01), 2, MidpointRounding.AwayFromZero );
            }
            txtGSTAmt.EditValue = dAMT;

            object oPurAmt = txtPurAmt.EditValue;
            if (oPurAmt == null || oPurAmt == DBNull.Value)
                oPurAmt = 0;

            txtInvAmt.EditValue = Convert.ToDouble(oPurAmt) + dAMT;
        }
    }
}
