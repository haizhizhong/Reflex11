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
    public partial class avWebSubConPayReq : AlertManager.AlertView
    {
        private ucWebInvDet ucWebInvDet1;

        public avWebSubConPayReq()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            LoadDetail();
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucWebInvDet1.LoadDetail(DetailID);            
        }

        private void avContractorPayment_Load(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void LoadDetail()
        {
            if (ucWebInvDet1 == null)
            {
                ucWebInvDet1 = new AlertViews.Costing.ucWebInvDet(this.HMConnection, this.TUC_DevXMgr);
                ucWebInvDet1.AlertView = true;
                ucWebInvDet1.Dock = DockStyle.Fill;
                ucWebInvDet1.Parent = groupControl2;
            }
        }
    }
}
