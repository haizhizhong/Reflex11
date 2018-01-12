using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.WorkOrders.BackFlush
{
    public partial class ucBackFlush : AlertManager.AlertView
    {
        public ucBackFlush()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);
            sqlTR.ConnectionString = base.HMConnection.TRConnection;

            dsDetails1.Clear();
            daDetails.SelectCommand.Parameters["@BatchID"].Value = DetailID;
            daDetails.Fill(dsDetails1);
        }

        private void ucBackFlush_Load(object sender, EventArgs e)
        {
            sqlTR.ConnectionString = base.HMConnection.TRConnection;
        }
    }
}
