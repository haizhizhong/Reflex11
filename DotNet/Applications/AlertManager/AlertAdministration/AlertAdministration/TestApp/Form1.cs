using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {

        private HMConnection.HMCon _HMCon;
        private TUC_HMDevXManager.TUC_HMDevXManager _TUC;

        private AlertAdministration.Administration _ucAdministration;

        public Form1()
        {
            InitializeComponent();

            // _HMCon = new HMConnection.HMCon("web_imark", @"dev-sql2014\dev2014", 12, "Adam");
            _HMCon = new HMConnection.HMCon("web_eric", @"dev-sql2014\dev2014", 12, "Eric");
            // _HMCon = new HMConnection.HMCon("web_strike_test", @"dev11", 12, "Adam");
            // _HMCon = new HMConnection.HMCon("web_dmd_qa_rat", @"csmsql2012", 12, "John");

            _TUC = new TUC_HMDevXManager.TUC_HMDevXManager();

            Start_AlertAdministration();
        }

        private void Start_AlertAdministration()
        {
            _ucAdministration = new AlertAdministration.Administration(_HMCon, _TUC);
            _ucAdministration.Parent = this;
            _ucAdministration.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
