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
    public partial class ucFinancingPaymentDue : ucPropertyInterface
    {
        bool isLoading = false;
        public ucFinancingPaymentDue()
        {
            InitializeComponent();
        }

        private void ucFinancingPaymentDue_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);

            string sSelect = "select isnull(FinancingPastDueDays,0) from Alert_Properties_Setup";
            isLoading = true;
            txtDays.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            isLoading = false;
            labelControl1.Focus();
        }

        private void txtDays_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            int threshold = -1;
            try
            {
                threshold = Convert.ToInt32(txtDays.EditValue);
            }
            catch
            {
            }
            if (threshold >= 0 && threshold < 366)
            {
                string sUpdate = "update Alert_Properties_Setup set FinancingPastDueDays = " + txtDays.EditValue;
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }
            else
                Popup.ShowPopup("Days must be between 0 and 365.");
        }

      


    }
}
