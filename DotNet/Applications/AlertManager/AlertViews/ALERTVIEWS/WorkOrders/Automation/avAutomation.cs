using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertViews.WorkOrders.Automation
{
    public partial class avAutomation : AlertManager.AlertView
    {
        public avAutomation()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            TR_Conn.ConnectionString = base.HMConnection.TRConnection;

            dsAutoDet.Clear();
            daAutoDet.SelectCommand.Parameters["@WO_SO_RequestID"].Value = DetailID;
            daAutoDet.Fill(dsAutoDet);
        }

        private void avAutomation_Load(object sender, EventArgs e)
        {
            TR_Conn.ConnectionString = base.HMConnection.TRConnection;
        }
    }
}
