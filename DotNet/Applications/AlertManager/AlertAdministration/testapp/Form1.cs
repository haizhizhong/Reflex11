using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HMConnection;
using AlertAdministration;

namespace testapp
{
    public partial class Form1 : Form
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private AlertAdministration.Administration Admin;
        private HMCon Connection;

        public Form1()
        {

            //Data Source=DEV-2005\DEV2000;Initial Catalog=hmreports;Persist Security Info=True;User ID=hmsqlsa;Password=hmsqlsa
            Connection = new HMCon("hmreports", @"DEV-2005\DEV2000", 12, "Adam");

            //Connection = new HMCon("web_strike_test", "dev11", 12, "adam");
            //Connection = new HMCon("web_resg_demo", "man1", 12, "adam");
            // Connection = new HMCon("web_test", "dev12", 12, "ying");
            InitializeComponent();

            DevXMgr = new TUC_HMDevXManager.TUC_HMDevXManager();
            DevXMgr.AppInit(Connection.MLUser);

            Admin = new Administration(Connection, DevXMgr);
            Admin.Parent = this;
            Admin.Dock = DockStyle.Fill;
        }   
    }
}
