using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.Rental
{
    public partial class ucRentalStart : AlertManager.AlertView
    {
        public ucRentalStart()
        {
            InitializeComponent();
        }

        private void ucRentalStart_Load(object sender, EventArgs e)
        {
            sqlTRConnection.ConnectionString = HMConnection.TRConnection;
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            daRentalStart.SelectCommand.Parameters["@Detail_ID"].Value = DetailID;
            dsRentalStart1.Clear();
            daRentalStart.Fill(dsRentalStart1);

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

        }
    }
}
