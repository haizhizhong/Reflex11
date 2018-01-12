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
    public partial class ucAPHBDet : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private int _DetailID = -1;
        private const int CONST_SUB_HB_REQUEST = 18;
        private int CONST_WS_INV_HB_LINK = -1;
        private object _CLog_FieldLink_ID;
        private bool _WorkFlowApproval = false;

        public ucAPHBDet()
        {
            InitializeComponent();
            ucHistoryView1.TypeID = CONST_SUB_HB_REQUEST;
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
                    _CLog_FieldLink_ID = Connection.SQLExecutor.ExecuteScalar("exec dbo.sp_WS_ChatFieldLinkGetID 'WS_INV_HB.ID', 'HBApproval'", Connection.TRConnection);
                    CONST_WS_INV_HB_LINK = Convert.ToInt32(_CLog_FieldLink_ID);
                    ucHistoryView1.ChatFieldLink = CONST_WS_INV_HB_LINK;
                    ucChatLog1.ChatFieldLink = CONST_WS_INV_HB_LINK;
                    ucConsolidatedChatLog1.ChatFieldLink = CONST_WS_INV_HB_LINK;
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
                    Popup = new frmPopup(DevXMgr);
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
            }
        }

        public void LoadDetail(int DetailID)
        {
            _DetailID = DetailID;

            dsWarehouse1.Clear();
            daWarehouse.Fill(dsWarehouse1);

            ucHistoryView1.DetailID = DetailID;
            ucHistoryView1.LoadHistory();

            ucChatLog1.LoadChat(DetailID);
            ucConsolidatedChatLog1.LoadChat(DetailID, 18);

            dsDetail1.Clear();
            daDetail.SelectCommand.Parameters["@detail_id"].Value = DetailID;
            daDetail.Fill(dsDetail1);

            LoadHeader(DetailID);
        }

        private void LoadHeader(int DetailID)
        {
            ClearFields();

            string sSelect = @"declare @CLog_IDValue int
                select @CLog_IDValue=id from ws_inv_hb where id=" + DetailID + @"
                select COUNT(*) from WS_ChatAttachment where CLog_FieldLink_ID=" + _CLog_FieldLink_ID + @" and CLog_IDValue=@CLog_IDValue";
            object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            btnExAttach.Text = "Attachments (" + obj + ")";

            sSelect = "select a.INV_DATE, a.DUE_DATE, a.SUPPLIER, a.SUPP_NAME, a.INV_NO, g.GST_DESC, s.DESCRIPTION, a.INV_AMOUNT, " +
                "a.PURCH_AMT, a.GST_AMT, p.PO, a.WHSE_ID, a.REFERENCE, a.COMMENT, isnull(w.pri_id,-1) [PriID], a.HOLD_PAY_DATE, isnull(a.HOLD_AMT,0) [HOLD_AMT] "+
                "from WS_INV_HB wb " +
                "join ap_inv_header a on a.ap_inv_header_id=wb.ap_inv_header_id " +
                "join ws_inv_header w on a.ws_inv_id=w.ws_inv_id " +
                "join GST_CODES g on a.GST_CODE=g.GST_CODE " +
                "join SALES_TAXES s on a.SALES_TAX_ID=s.SALES_TAX_ID " +
                "left outer join PO_HEADER p on w.PO_ID=p.PO_ID " +
                "where wb.ID = " + DetailID;

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
                        txtInvNo.EditValue = dr["INV_NO"];
                        txtGST.EditValue = dr["GST_DESC"];
                        txtPST.EditValue = dr["DESCRIPTION"];
                        txtInvAmt.EditValue = dr["INV_AMOUNT"];
                        txtPurAmt.EditValue = dr["PURCH_AMT"];
                        txtGSTAmt.EditValue = dr["GST_AMT"];
                        txtPO.EditValue = dr["PO"];
                        lueWHSE.EditValue = dr["WHSE_ID"];
                        txtReference.EditValue = dr["REFERENCE"];
                        memoComment.EditValue = dr["COMMENT"];
                        deHoldbackDate.EditValue = dr["HOLD_PAY_DATE"];
                        txtHoldbackAmt.EditValue = dr["HOLD_AMT"];
                    }
                }
            }
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
            deHoldbackDate.EditValue = null;

            btnExAttach.Text = "Attachments (0)";
        }

        private void ucAPHBDet_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
                DevXMgr.FormInit(this);

            ucHistoryView1.Dock = DockStyle.None;
            ucHistoryView1.Dock = DockStyle.Fill;
        }

        private void btnExAttach_Click(object sender, EventArgs e)
        {
            SubmissionHistory.frmAttachments fAttach = new SubmissionHistory.frmAttachments(Connection, DevXMgr, _DetailID, Convert.ToInt32( _CLog_FieldLink_ID ));
            fAttach.ShowDialog();
        }
    }
}
