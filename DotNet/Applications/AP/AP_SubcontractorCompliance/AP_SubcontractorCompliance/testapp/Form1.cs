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
            Connection = new HMCon("web_strike_test", "dev11", 12, "adam");
            //Connection = new HMConnection.HMCon("web_eric", @"dev-sql2014\dev2014", 12, "Eric");
            //Connection = new HMConnection.HMCon("web_gc_test", @"dotnetsql\dotnetsql", 12, "DevGreg");

            InitializeComponent();
            sqlTRConnection.ConnectionString = Connection.TRConnection;
            ucMasterView_Loading();
            dsSupplier1.Clear();
            daSupplier.Fill(dsSupplier1);
            dsProject1.Clear();
            daProject.Fill(dsProject1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        AP_SubcontractorCompliance.ucMasterView mast;
        private void ucMasterView_Loading()
        {
            mast = new AP_SubcontractorCompliance.ucMasterView(Connection, DevXMgr);
            mast.RefreshDS();
            mast.Dock = DockStyle.Fill;
            mast.Parent = tpMain ;
            mast.BringToFront();
        }

        AP_SubcontractorCompliance.ucSupplierView suppl;
        private void ucSupplierView_Loading()
        {
            suppl = new AP_SubcontractorCompliance.ucSupplierView(Connection, DevXMgr);
            suppl.RefreshDS();
            suppl.Dock = DockStyle.Fill;
            suppl.Parent = tpSupplier;
            suppl.BringToFront();
            
            
        }

        AP_SubcontractorCompliance.ucProjectView proj;
        private void ucProjectView_Loading()
        {
            proj = new AP_SubcontractorCompliance.ucProjectView(Connection, DevXMgr);
            proj.RefreshDS();
            proj.Dock = DockStyle.Fill;
            proj.Parent = tpProject;
            proj.BringToFront();
            
            

        }

        private void btnMainRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnSupplierRefresh_Click(object sender, EventArgs e)
        {
            if (lueSupplier.EditValue == DBNull.Value || lueSupplier == null)
            {
                MessageBox.Show("Please");
                MessageBox.Show("Select");
                MessageBox.Show("A");
                MessageBox.Show("Supplier");
                return;
            }
            if(suppl == null)
                ucSupplierView_Loading();
            
            suppl.RefreshDS();
            suppl.SupplierID = Convert.ToInt32(lueSupplier.EditValue);
        }

        private void btnProjectRefresh_Click(object sender, EventArgs e)
        {
            if (lueProject.EditValue == DBNull.Value || lueProject == null)
            {
                MessageBox.Show("Please");
                MessageBox.Show("Select");
                MessageBox.Show("A");
                MessageBox.Show("Project");
            }
            if(proj == null)
                ucProjectView_Loading();
            proj.RefreshDS();
            proj.PriID = Convert.ToInt32(lueProject.EditValue);
        }

    }
}
