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
    public partial class avLandPaymentDue : AlertManager.AlertView
    {
        public avLandPaymentDue()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucLandPaymentDue1.HMConnection == null)
                ucLandPaymentDue1.HMConnection = this.HMConnection;
            if (ucLandPaymentDue1.TUC_DevXMgr == null)
                ucLandPaymentDue1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucLandPaymentDue1.LoadDetail(DetailID);
        }

        private void avLandPaymentDue_Load(object sender, EventArgs e)
        {
            ucLandPaymentDue1.HMConnection = this.HMConnection;
            ucLandPaymentDue1.TUC_DevXMgr = this.TUC_DevXMgr;
        }


    }
}
