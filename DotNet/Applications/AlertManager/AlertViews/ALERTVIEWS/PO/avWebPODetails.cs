using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlertViews.PO
{
    public partial class avWebPODetails : AlertManager.AlertView
    {
        private ucWebPODetails _ucWebPODetails;

        public avWebPODetails()
        {
            InitializeComponent();
        }

        public avWebPODetails(HMConnection.HMCon hmCon, TUC_HMDevXManager.TUC_HMDevXManager tuc)
        {
            InitializeComponent();

            this.HMConnection = hmCon;
            this.TUC_DevXMgr = tuc;


            _ucWebPODetails = new ucWebPODetails(hmCon, tuc);
            _ucWebPODetails.IsWorkFlow = false;            
            _ucWebPODetails.Dock = DockStyle.Fill;
            _ucWebPODetails.Parent = this;
            _ucWebPODetails.BringToFront();
        }

        private void avWebPODetails_Load(object sender, EventArgs e)
        {
        }

        private void DoSplitterLayout()
        {
            this.layoutControl1.Dock = DockStyle.Top;
            this.splitterControl.Dock = DockStyle.Top;

            this.layoutControl1.Parent.Controls.SetChildIndex(splitterControl, 1);
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

            _ucWebPODetails.WS_PCPO_ID = DetailID;
        }

    }
}
