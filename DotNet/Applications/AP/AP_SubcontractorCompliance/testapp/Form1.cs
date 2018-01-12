using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HMConnection;

namespace testapp
{
    public partial class Form1 : Form
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private HMCon Connection;

        public Form1()
        {
            DevXMgr = new TUC_HMDevXManager.TUC_HMDevXManager();
            DevXMgr.AppInit("adam");
          //  Connection = new HMCon("web_strike_test", "dev1", 12, "adam");
            Connection = new HMCon("web_comco", "dev4", 12, "ying");
            InitializeComponent();

            AP_SubcontractorCompliance.ucMasterView mast = new AP_SubcontractorCompliance.ucMasterView(Connection, DevXMgr);
            mast.RefreshDS();
            mast.Dock = DockStyle.Fill;
            mast.Parent = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }
    }
}
