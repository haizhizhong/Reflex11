using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.HumanResources
{
    public partial class ucEmpCertificateExpiry : ucPropertyInterface
    {
        public ucEmpCertificateExpiry()
        {
            InitializeComponent();
        }

        private void ucEmpCertificateExpiry_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);

            string sSelect = "select isnull(DaysPrior,0) from hr_cntl";
            txtDays.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            labelControl1.Focus();
        }

        private void txtDays_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update hr_cntl set DaysPrior=" + txtDays.EditValue;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }
    }
}
