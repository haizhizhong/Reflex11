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

namespace AlertViews.Human_Resources
{
    public partial class avEmployeeCertificateExpiry : AlertManager.AlertView
    {
        public avEmployeeCertificateExpiry()
        {
            InitializeComponent();
        }

        private void avEmployeeCertificateExpiry_Load(object sender, EventArgs e)
        {
            sqlConnTR.ConnectionString = HMConnection.TRConnection;
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            dsCertificate1.Clear();
            daCertificate.SelectCommand.Parameters["@Alert_ID"].Value = AlertID;
            daCertificate.Fill(dsCertificate1);
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

        }
    }
}
