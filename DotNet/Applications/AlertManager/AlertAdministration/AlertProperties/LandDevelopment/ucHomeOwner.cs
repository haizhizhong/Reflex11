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
    public partial class ucHomeOwner : ucPropertyInterface
    {
        public ucHomeOwner()
        {
            InitializeComponent();
        }

        private void ucHomeOwner_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);

            string sSelect = "select count(*) from alert_lot_hold_setup";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_lot_hold_setup (id, OnHold_On, PreExpiry_On, Expiry_On, homeowner_confirmed_days, DownPayment_Due_Days) values (1, 'F', 'F', 'F', 0, 0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(homeowner_confirmed_days,0) from alert_lot_hold_setup";
            txtDays.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            labelControl1.Focus();
        }

        private void txtDays_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_lot_hold_setup set homeowner_confirmed_days=" + txtDays.EditValue;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }
    }
}
