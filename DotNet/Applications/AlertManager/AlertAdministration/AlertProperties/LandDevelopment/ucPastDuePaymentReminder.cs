using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.LandDevelopment
{
    public partial class ucPastDuePaymentReminder : ucPropertyInterface
    {

        private ucCommPurDocMerge CommPurDocMerge;

        public ucPastDuePaymentReminder()
        {
            InitializeComponent();
        }

        private void ucPastDuePaymentReminder_Load(object sender, EventArgs e)
        {
            LoadUserControls();
            DevXMgr.FormInit(this);
            
        }

        private void LoadUserControls()
        {
            CommPurDocMerge = new ucCommPurDocMerge(Connection, DevXMgr, AlertPointID);
            CommPurDocMerge.Dock = DockStyle.Fill;
            CommPurDocMerge.Parent = pcGrid;
           
        }

    }
}
