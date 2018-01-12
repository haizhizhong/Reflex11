using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Costing.POReceiving
{
    public partial class ucPORecNotify : AlertManager.AlertView
    {
        public ucPORecNotify()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucPORecNotifyDet1.HMConnection == null)
                ucPORecNotifyDet1.HMConnection = this.HMConnection;
            if (ucPORecNotifyDet1.TUC_DevXMgr == null)
                ucPORecNotifyDet1.TUC_DevXMgr = this.TUC_DevXMgr;

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            ucPORecNotifyDet1.LoadDetail(DetailID);
        }

        private void ucPORecNotify_Load(object sender, EventArgs e)
        {
            ucPORecNotifyDet1.HMConnection = this.HMConnection;
            ucPORecNotifyDet1.TUC_DevXMgr = this.TUC_DevXMgr;
        }
    }
}
