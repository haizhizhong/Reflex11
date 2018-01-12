using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Costing.LotExpiry
{
    public partial class avLotHoldExpiry : AlertManager.AlertView
    {
        public avLotHoldExpiry()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if( ucLotHoldDet1.HMConnection == null )
                ucLotHoldDet1.HMConnection = this.HMConnection;
            if (ucLotHoldDet1.TUC_DevXMgr == null)
                ucLotHoldDet1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucLotHoldDet1.LoadDetail(DetailID);
        }

        private void avContractorPayment_Load(object sender, EventArgs e)
        {
            ucLotHoldDet1.HMConnection = this.HMConnection;
            ucLotHoldDet1.TUC_DevXMgr = this.TUC_DevXMgr;
        }
    }
}
