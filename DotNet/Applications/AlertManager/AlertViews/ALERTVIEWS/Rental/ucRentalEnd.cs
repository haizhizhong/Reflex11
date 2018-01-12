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
    public partial class ucRentalEnd : AlertManager.AlertView
    {
        public ucRentalEnd()
        {
            InitializeComponent();
        }

        private void ucRentalEnd_Load(object sender, EventArgs e)
        {
            sqlTRConnection.ConnectionString = HMConnection.TRConnection;
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            daRentalEnd.SelectCommand.Parameters["@Detail_ID"].Value = DetailID;
            dsRentalEnd1.Clear();
            daRentalEnd.Fill(dsRentalEnd1);

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

        }
    }
}
