using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TUC_HMDevXManager.TUC_HMDevXManager dev = new TUC_HMDevXManager.TUC_HMDevXManager();
            dev.AppInit("Adam");
            ucAP.ucAP ap = new ucAP.ucAP("web_steelcraft_v10", "CSMSQL2012", "13", "Adam", dev);
            ap.Parent = this;
            ap.Visible = true;
            ap.Dock = DockStyle.Fill;
        }
    }
}
