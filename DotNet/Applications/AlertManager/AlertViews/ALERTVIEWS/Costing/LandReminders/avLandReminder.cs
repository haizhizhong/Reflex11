using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Costing.LandReminders
{
    public partial class avLandReminder : AlertManager.AlertView
    {
        public avLandReminder()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucLandReminder1.HMConnection == null)
                ucLandReminder1.HMConnection = this.HMConnection;
            if (ucLandReminder1.TUC_DevXMgr == null)
                ucLandReminder1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucLandReminder1.LoadDetail(DetailID);
        }

        private void avLandReminder_Load(object sender, EventArgs e)
        {
            ucLandReminder1.HMConnection = this.HMConnection;
            ucLandReminder1.TUC_DevXMgr = this.TUC_DevXMgr;
        }
    }
}
