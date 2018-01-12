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
    public partial class ucRentalRequestStatusChg : AlertManager.AlertView
    {
        public ucRentalRequestStatusChg()
        {
            InitializeComponent();
        }

        private void ucRentalRequestStatusChg_Load(object sender, EventArgs e)
        {
            sqlTRConnection.ConnectionString = HMConnection.TRConnection;
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            daStatusChange.SelectCommand.Parameters["@Detail_ID"].Value = DetailID;
            dsStatusChange1.Clear();
            daStatusChange.Fill(dsStatusChange1);

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

        }


    }
}
