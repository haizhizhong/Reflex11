using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AP_Levy
{
    public partial class ucLevyMatch : DevExpress.XtraEditors.XtraUserControl
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;

        public ucLevyMatch(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new WS_Popups.frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
        }

        private void ucLevyMatch_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        public void LoadInvoice(int ap_inv_header_id)
        {
            dsLevyMatch1.Clear();
            if (ap_inv_header_id != -1)
            {
                daLevyMatch.SelectCommand.Parameters["@ap_inv_header_id"].Value = ap_inv_header_id;
                daLevyMatch.Fill(dsLevyMatch1);
            }
        }
    }
}
