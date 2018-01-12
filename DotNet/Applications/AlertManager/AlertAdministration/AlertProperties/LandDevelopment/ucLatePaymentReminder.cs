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
    public partial class ucLatePaymentReminder : ucPropertyInterface
    {
        private ucCommPurDocMerge CommPurDocMerge;

        public ucLatePaymentReminder()
        {
            InitializeComponent();
        }

        private void LoadUserControls()
        {
            CommPurDocMerge = new ucCommPurDocMerge(Connection, DevXMgr, AlertPointID);
            CommPurDocMerge.Dock = DockStyle.Fill;
            CommPurDocMerge.Parent = pcGrid;
            CommPurDocMerge.tpTolerance.PageVisible = false;
            sqlConnTR.ConnectionString = Connection.TRConnection;
        }

        private void ucLatePaymentReminder_Load(object sender, EventArgs e)
        {
            LoadUserControls();

            DevXMgr.FormInit(this);
            string sSelect = "select count(*) from alert_land_reminder_setup where AlertID=" + AlertPointID;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                 string sInsert = "insert into alert_land_reminder_setup (AlertID, X_Days, Recurrence_Days, Recurrence_Months) values (" + AlertPointID + ", 0, 0, 0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            LoadAlertSetup();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BindingContext[dsAlertSetup1, "alert_land_reminder_setup"].EndCurrentEdit();
            daAlertSetup.Update(dsAlertSetup1);
            daAlertSetup.Fill(dsAlertSetup1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BindingContext[dsAlertSetup1, "alert_land_reminder_setup"].EndCurrentEdit();
            dsAlertSetup1.RejectChanges();
        }

        private void LoadAlertSetup()
        {
            dsAlertSetup1.Clear();
            daAlertSetup.Fill(dsAlertSetup1);
        }

        private void txtRecurrDays_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToInt32(e.NewValue) < 0)
            {
                e.Cancel = true;
            }
        }

        private void txtRecurrMonths_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToInt32(e.NewValue) < 0)
            {
                e.Cancel = true;
            }
        }

        private void txtDays_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToInt32(e.NewValue) < 0)
            {
                e.Cancel = true;
            }
        }

       
    }
}
