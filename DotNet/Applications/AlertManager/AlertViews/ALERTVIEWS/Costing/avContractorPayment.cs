using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Costing
{
    public partial class avContractorPayment : AlertManager.AlertView
    {
        public avContractorPayment()
        {
            InitializeComponent();
            ucAPInvDet1.WorkFlowApproval = true;
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucAPInvDet1.HMConnection == null)
                ucAPInvDet1.HMConnection = this.HMConnection;
            if (ucAPInvDet1.TUC_DevXMgr == null)
                ucAPInvDet1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucAPInvDet1.LoadDetail(DetailID);            
        }

        private void avContractorPayment_Load(object sender, EventArgs e)
        {
            if (ucAPInvDet1.HMConnection == null)
                ucAPInvDet1.HMConnection = this.HMConnection;
            if (ucAPInvDet1.TUC_DevXMgr == null)
                ucAPInvDet1.TUC_DevXMgr = this.TUC_DevXMgr;
        }
    }
}
