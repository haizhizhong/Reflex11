using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AP_Invoice_Entry
{
    public partial class frmEH_Launch : DevExpress.XtraEditors.XtraForm
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private SubmissionHistory.ucHistoryView ucHistoryView1;
        private const int CONST_SUB_PAYMENT_REQUEST = 1;

        public frmEH_Launch(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AP_INV_HEADER_ID)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            InitializeComponent();

            ucHistoryView1 = new SubmissionHistory.ucHistoryView();
            ucHistoryView1.Dock = DockStyle.Fill;
            ucHistoryView1.Parent = this;
            ucHistoryView1.TypeID = CONST_SUB_PAYMENT_REQUEST;
            ucHistoryView1.HMConnection = Connection;
            ucHistoryView1.TUC_DevXMgr = DevXMgr;

            object CLog_FieldLink_ID = Connection.SQLExecutor.ExecuteScalar("exec dbo.sp_WS_ChatFieldLinkGetID 'WS_INV_HEADER.WS_INV_ID', 'PayApproval'", Connection.TRConnection);
            ucHistoryView1.ChatFieldLink = Convert.ToInt32(CLog_FieldLink_ID);

            string sSQL = @"select ws_inv_id from ws_inv_header where ap_inv_header_id=" + AP_INV_HEADER_ID;
            object oWS_INV_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oWS_INV_ID == null || oWS_INV_ID == DBNull.Value)
                oWS_INV_ID = -1;
            ucHistoryView1.DetailID = Convert.ToInt32(oWS_INV_ID);
            ucHistoryView1.LoadHistory();
        }

        private void frmEH_Launch_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }
    }
}