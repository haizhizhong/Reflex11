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
    public partial class avMasterAgreementReminder : AlertManager.AlertView
    {
        public avMasterAgreementReminder()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucMasterAgreementReminder1.HMConnection == null)
                ucMasterAgreementReminder1.HMConnection = this.HMConnection;
            if (ucMasterAgreementReminder1.TUC_DevXMgr == null)
                ucMasterAgreementReminder1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucMasterAgreementReminder1.LoadDetail(DetailID);
        }

        private void avLandReminder_Load(object sender, EventArgs e)
        {
            ucMasterAgreementReminder1.HMConnection = this.HMConnection;
            ucMasterAgreementReminder1.TUC_DevXMgr = this.TUC_DevXMgr;
        }
    }
}
