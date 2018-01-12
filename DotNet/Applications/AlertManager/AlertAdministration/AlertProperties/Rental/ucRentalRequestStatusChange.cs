using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.Rental
{
    public partial class ucRentalRequestStatusChange : ucPropertyInterface
    {
        public ucRentalRequestStatusChange()
        {
            InitializeComponent();
        }

        private void ucRentalRequestStatusChange_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);

            string sSelect = "select count(*) from alert_rental_setup";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_rental_setup (RentalStartEnabled,RentalStartPreEnabled,RentalEndEnabled,RentalEndPreEnabled,RentalStatus_Entered,RentalStatus_Active,RentalStatus_Complete,RentalStatus_Canceled)" +
                    "values (0,0,0,0,0,0,0,0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(RentalStatus_Entered,0) from alert_rental_setup";
            ceEntered.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            sSelect = "select isnull(RentalStatus_Active,0) from alert_rental_setup";
            ceActive.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            sSelect = "select isnull(RentalStatus_Complete,0) from alert_rental_setup";
            ceComplete.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            sSelect = "select isnull(RentalStatus_Canceled,0) from alert_rental_setup";
            ceCanceled.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);


        }

        private void ceEntered_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_rental_setup set RentalStatus_Entered= " + (ceEntered.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void ceActive_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_rental_setup set RentalStatus_Active= " + (ceActive.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void ceComplete_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_rental_setup set RentalStatus_Complete= " + (ceComplete.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void ceCanceled_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_rental_setup set RentalStatus_Canceled= " + (ceCanceled.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }
    }
}
