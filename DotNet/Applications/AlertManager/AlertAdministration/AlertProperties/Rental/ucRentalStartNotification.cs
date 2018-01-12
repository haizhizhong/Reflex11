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
    public partial class ucRentalStartNotification : ucPropertyInterface
    {
        public ucRentalStartNotification()
        {
            InitializeComponent();
        }

        private void ucRentalStartNotification_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);

            string sSelect = "select count(*) from alert_rental_setup";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_rental_setup (RentalStartEnabled,RentalStartPreEnabled,RentalEndEnabled,RentalEndPreEnabled,RentalStatus_Entered,RentalStatus_Active,RentalStatus_Complete,RentalStatus_Canceled)" +
                    "values (0,0,0,0,0,0,0,0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(RentalStartEnabled,0) from alert_rental_setup";
            ceRentalStart.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            sSelect = "select isnull(RentalStartPreEnabled,0) from alert_rental_setup";
            ceRentalPreStart.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);


            sqlTRConnection.ConnectionString = Connection.TRConnection;
            daRentalStartPreAlerts.Fill(dsRentalStartPreAlerts1);
        }

        private void gvPreAlerts_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daRentalStartPreAlerts.Update(dsRentalStartPreAlerts1);
        }

        private void gcPreAlerts_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                gvPreAlerts.DeleteRow(gvPreAlerts.FocusedRowHandle);
                daRentalStartPreAlerts.Update(dsRentalStartPreAlerts1);
                e.Handled = true;
            }
        }

        private void gvPreAlerts_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row;

            if (IsLessThanZero(drv[colDaysPrior.FieldName], false))
            {
                Popup.ShowPopup("\"Days Prior\" must be greater than zero.");
                e.Valid = false;
            }
            else if (IsNumberToLarge(drv[colDaysPrior.FieldName]))
            {
                Popup.ShowPopup("\"Days Prior\" must be between 1 and 1000.");
                e.Valid = false;
            }

        }

        private bool IsLessThanZero(object value, bool AllowZero)
        {
            bool returnValue = false;
            decimal dValue = -1;
            try { dValue = Convert.ToDecimal(value); }
            catch { }

            if (dValue == 0 && !AllowZero)
                returnValue = true;
            else if (dValue < 0)
                returnValue = true;
            return returnValue;
        }

        private bool IsNumberToLarge(object value)
        {
            bool returnValue = false;
            decimal dValue = -1;
            try
            {
                dValue = Convert.ToDecimal(value);
            }
            catch
            {

            }

            if (dValue > 1001)
                returnValue = true;

            return returnValue;
        }

        private void gvPreAlerts_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvPreAlerts_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow row = gvPreAlerts.GetDataRow(e.RowHandle);
            row[colDaysPrior.FieldName] = 1;
        }

        private void ceRentalStart_EditValueChanged(object sender, EventArgs e)
        {

            string sUpdate = "update alert_rental_setup set RentalStartEnabled= " + (ceRentalStart.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

        }

        private void ceRentalPreStart_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_rental_setup set RentalStartPreEnabled= " + (ceRentalPreStart.Checked ? "1" : "0");
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }


    }
}
