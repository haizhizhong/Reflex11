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

namespace AlertViews.AP
{
    public partial class ucWFAPDetail : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private GL_Account_Lookup_Rep.Repository_GL_Lookup GL_Repository;

        private int _WF_Route_ID;
        private int _ap_inv_header_id;

        private DM_CentralizedFSManager.ucFileManager _ucFileManager;
        private WO_CentralizedFSManager.ucFileManager CFS_FileMgr;
        private bool _USE_DM = false;
        private string RelType = "APINV";

        public ucWFAPDetail()
        {
            InitializeComponent();           
        }

        public ucWFAPDetail(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;

            InitializeComponent();

            TR_Conn.ConnectionString = Connection.TRConnection;

            this.daWF_Route.SelectCommand.CommandText =
                daWF_Route.SelectCommand.CommandText.Replace("web_strike_test", Connection.WebDB);

            InitializeFileManager();
            SetupGLPicker();
            SetupReferencePicker();
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
                    SetupReferencePicker();
                    GL_Repository.HMConnection = Connection;
                    InitializeFileManager();
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
                }
            }
            get
            {
                return DevXMgr;
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

        private void SetupGLPicker()
        {
            GL_Repository = new GL_Account_Lookup_Rep.Repository_GL_Lookup();
            GL_Repository.HideVailidation = true; //must be set before the hmconnection is set.
            GL_Repository.HideSubCode = true;
            GL_Repository.HMConnection = Connection;
            GL_Repository.DevXMgr = DevXMgr;
            GL_Repository.ValidateOnEnterKey = true;
            GL_Repository.EditValueChanged += new EventHandler(GL_Repository_EditValueChanged);
            colGLAccount.ColumnEdit = GL_Repository;
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

        private void ucWFAPDetail_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
                DevXMgr.FormInit(this);
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

        public void LoadRoutingDetail(int WF_Route_ID)
        {
            _WF_Route_ID = WF_Route_ID;

            string sSQL = @"select Link_ID from WF_Route where WF_Route_ID = " + _WF_Route_ID;
            object oLink_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oLink_ID == null || oLink_ID == DBNull.Value)
                oLink_ID = -1;

            _ap_inv_header_id = Convert.ToInt32(oLink_ID);

            ClearScreenControls();
            RefreshucWFPODetails();
        }

        #region Header

        private void RefreshucWFPODetails()
        {            
            Refresh_AP_Hdr();

            string sSQL = @"select isnull(notes,'') from WF_Route where WF_Route_ID=" + _WF_Route_ID;
            object oNotes = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oNotes == null || oNotes == DBNull.Value)
                oNotes = "";
            meSubmissionDetails_Notes.Text = oNotes.ToString();

            // load AP details
            Refresh_AP_Det();

            // load sbmission details grid
            Refresh_WF_Route();

            // load attachments
            Refresh_FileManager();
        }

        private void Refresh_AP_Hdr()
        {
            ClearFields();

            string sSQL = @"select a.INV_DATE, a.DUE_DATE, a.SUPPLIER, a.SUPP_NAME, a.INV_NO, g.GST_DESC, s.DESCRIPTION, a.INV_AMOUNT, 
                a.PURCH_AMT, a.GST_AMT, p.PO, a.WHSE_ID, a.REFERENCE, a.COMMENT, dbo.fnPO_GetPOTotal(a.po_id) [PO_AMT]
                from AP_INV_HEADER a
                left outer join GST_CODES g on a.GST_CODE=g.GST_CODE 
                left outer join SALES_TAXES s on a.SALES_TAX_ID=s.SALES_TAX_ID 
                left outer join PO_HEADER p on a.PO_ID=p.PO_ID 
                where a.ap_inv_header_id = " + _ap_inv_header_id;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
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
                        txtInvNo.EditValue = dr["INV_NO"];
                        txtGST.EditValue = dr["GST_DESC"];
                        txtPST.EditValue = dr["DESCRIPTION"];
                        txtInvAmt.EditValue = dr["INV_AMOUNT"];
                        txtPurAmt.EditValue = dr["PURCH_AMT"];
                        txtGSTAmt.EditValue = dr["GST_AMT"];
                        txtPO.EditValue = dr["PO"];
                        lueWHSE.EditValue = dr["WHSE_ID"];
                        txtReference.EditValue = dr["REFERENCE"];
                        txtPOAmt.EditValue = dr["PO_AMT"];
                    }
                }
            }

            sSQL = @"select SUM(ISNULL(hold_amt,0)) from ap_gl_alloc where ap_inv_header_id=" + _ap_inv_header_id;
            txtHoldbackAmt.EditValue = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
        }

        public void Refresh_WF_Route()
        {
            dsWF_Route.Clear();
            daWF_Route.SelectCommand.Parameters["@WF_Route_ID"].Value = _WF_Route_ID;
            daWF_Route.Fill(dsWF_Route);
        }

        private void LoadUnits()
        {
            if (Connection.CountryCode == "U")
            {
                lciGST.Text = "Tax 1";
                lciPST.Text = "Tax 2";
                lciGSTAmt.Text = "Tax 1 Amount";
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
            txtPOAmt.EditValue = null;
        }

        #endregion

        #region Utility Methods

        private void ClearScreenControls()
        {
            if (_USE_DM)
            {
                _ucFileManager.ReferenceID = -1;
                _ucFileManager.RefreshFileList();
                _ucFileManager.ReadOnly = true;
            }
            else
            {
                CFS_FileMgr.RefreshFileList(RelType, -1, true);
            }
        }

        #endregion 

        #region Attachments

        private void InitializeFileManager()
        {
            string sSQL = @"select ISNULL((select isnull(USE_DM_ON_PORTAL,0) from system_ctrl), 0)";
            object oUSE_DM = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oUSE_DM == null || oUSE_DM == DBNull.Value)
                oUSE_DM = false;
            _USE_DM = Convert.ToBoolean(oUSE_DM);
            if (_USE_DM)
            {
                if (_ucFileManager != null)
                {
                    try { _ucFileManager.Dispose(); }
                    catch { }
                    _ucFileManager = null;
                }
                _ucFileManager = new DM_CentralizedFSManager.ucFileManager(Connection, DevXMgr, DM_CentralizedFSManager.DocumentViewerMode.All, true, "F");
                _ucFileManager.DocumentFileLink = GetFileLinks(-1);
                _ucFileManager.ReadOnly = true;
                _ucFileManager.Dock = DockStyle.Fill;
                _ucFileManager.Parent = xtabAttachments;
                _ucFileManager.BringToFront();
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
                        WO_CentralizedFSManager.DocumentViewerMode.All, RelType, -1, true, "PO");
                CFS_FileMgr.Dock = DockStyle.Fill;
                CFS_FileMgr.Parent = xtabAttachments;
                CFS_FileMgr.BringToFront();
            }
        }

        public void Refresh_FileManager()
        {
            if (_ap_inv_header_id >= 0)
            {
                if (_USE_DM)
                {
                    _ucFileManager.ReadOnly = true;
                    _ucFileManager.DocumentFileLink = GetFileLinks(_ap_inv_header_id);
                }
                else
                {
                    CFS_FileMgr.RefreshFileList(RelType, _ap_inv_header_id, true);
                }
            }
        }

        private DM_CentralizedFSManager.FileLink[] GetFileLinks(int po_id)
        {
            DM_CentralizedFSManager.FileLink[] fileLinks = new DM_CentralizedFSManager.FileLink[]{                                                               
                    new DM_CentralizedFSManager.FileLink("PO_HEADER.po_id", po_id, Connection.CompanyID, DM_CentralizedFSManager.FileLink.Database.TR, 0, true)     
            };

            return fileLinks;
        }

        #endregion

        #region Detail

        public void Refresh_AP_Det()
        {
            dsAPDetail.Clear();
            daAPDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = _ap_inv_header_id;
            daAPDetail.Fill(dsAPDetail);
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

                string sUpdate = "update ap_gl_alloc set " +
                    "gl_account='" + dr["GL_ACCOUNT"] + "', " +
                    "pri_id=" + oPriID + ", " +
                    "lv1id=" + olv1ID + ", " +
                    "lv2id=" + olv2ID + ", " +
                    "lv3id=" + olv3ID + ", " +
                    "lv4id=" + olv4ID + ", " +
                    "lem_comp=" + oLem + ", " +
                    "expense_type=" + oExpense + ", " +
                    "reference=" + oReference + " " +
                    "where ap_gl_alloc_id=" + dr["AP_GL_ALLOC_ID"];
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }
        }

        private void gvDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        #endregion
    }
}
