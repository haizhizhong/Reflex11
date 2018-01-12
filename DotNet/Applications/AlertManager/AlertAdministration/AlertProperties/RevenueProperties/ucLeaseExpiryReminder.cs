using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.RevenueProperties
{
    public partial class ucLeaseExpiryReminder : ucPropertyInterface
    {
        private LandDevelopment.ucCommPurDocMerge CommPurDocMerge;

        public ucLeaseExpiryReminder()
        {
            InitializeComponent();
        }

        private void LoadUserControls()
        {
            CommPurDocMerge = new AlertProperties.LandDevelopment.ucCommPurDocMerge(Connection, DevXMgr, AlertPointID);
            CommPurDocMerge.Dock = DockStyle.Fill;
            CommPurDocMerge.Parent = pcGrid;
            CommPurDocMerge.tpTolerance.PageVisible = false;
            CommPurDocMerge.tpDocumentMerg.PageVisible = false;
        }

        private void ucLeaseExpiryReminder_Load(object sender, EventArgs e)
        {
            LoadUserControls();

            DevXMgr.FormInit(this);
            string sSelect = "select count(*) from alert_land_reminder_setup where AlertID=" + AlertPointID;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_land_reminder_setup (AlertID, X_Days) values (" + AlertPointID + ", 0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(X_Days,0) from alert_land_reminder_setup where AlertID=" + AlertPointID;
            txtDays.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            labelControl1.Focus();
        }

        private void txtDays_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_land_reminder_setup set X_Days=" + txtDays.EditValue + " where AlertID=" + AlertPointID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

    }
}
