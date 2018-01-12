using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.PO
{
    public partial class ucPODetails : AlertManager.AlertView
    {
        private AppDetails_PO_Details.ucPO_Details ucPO;
        public ucPODetails()
        {
            InitializeComponent();

            //gcDetails
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            if (ucPO == null)
            {
                ucPO = new AppDetails_PO_Details.ucPO_Details(this.TUC_DevXMgr, this.HMConnection);
                ucPO.Size = gcDetails.Size;
                ucPO.Dock = DockStyle.Fill;
                ucPO.Parent = gcDetails;
            }

            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

            string sSQL = @"delete from working_po_selected where username='"+this.HMConnection.MLUser+@"'
                insert into working_po_selected (username, po_id)
                select '"+this.HMConnection.MLUser+@"', "+DetailID;
            this.HMConnection.SQLExecutor.ExecuteNonQuery(sSQL, this.HMConnection.TRConnection);
            ucPO.RefreshMe();
        }

        private void ucPODetails_Load(object sender, EventArgs e)
        {
            if (ucPO == null)
            {
                ucPO = new AppDetails_PO_Details.ucPO_Details(this.TUC_DevXMgr, this.HMConnection);
                ucPO.Size = gcDetails.Size;
                ucPO.Dock = DockStyle.Fill;
                ucPO.Parent = gcDetails;
            }
        }
    }
}
