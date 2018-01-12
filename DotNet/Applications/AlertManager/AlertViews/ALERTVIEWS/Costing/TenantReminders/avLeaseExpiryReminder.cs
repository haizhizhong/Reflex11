using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Costing.TenantReminders
{
    public partial class avLeaseExpiryReminder : AlertManager.AlertView
    {

        public avLeaseExpiryReminder()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucLeaseExpiryReminder1.HMConnection == null)
                ucLeaseExpiryReminder1.HMConnection = this.HMConnection;
            if (ucLeaseExpiryReminder1.TUC_DevXMgr == null)
                ucLeaseExpiryReminder1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucLeaseExpiryReminder1.LoadDetail(DetailID);
        }

        private void avLeaseExpiryReminder_Load(object sender, EventArgs e)
        {
              ucLeaseExpiryReminder1.HMConnection = this.HMConnection;
            ucLeaseExpiryReminder1.TUC_DevXMgr = this.TUC_DevXMgr;
        }

      
    }
}
