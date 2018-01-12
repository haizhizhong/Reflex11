using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WS_Popups;
using HMConnection;

namespace AlertViews.Costing.TenantReminders
{
    public partial class ucLeaseExpiryReminder : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;

        public ucLeaseExpiryReminder()
        {
            InitializeComponent();
        }

        #region Properties

        public HMCon HMConnection
        {
            set
            {
                Connection = value;
                if (Connection != null)
                {
                    sqlConnTR.ConnectionString = Connection.TRConnection;
                }
            }
            get
            {
                return Connection;
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUC_DevXMgr
        {
            set
            {
                DevXMgr = value;
                if (DevXMgr != null)
                {
                    Popup = new frmPopup(value);
                }
            }
            get
            {
                return DevXMgr;
            }
        }


        #endregion
        public void LoadDetail(int DetailID)
        {
            dsLease1.Clear();
            daLease.SelectCommand.Parameters["@Lease_Head_ID"].Value = DetailID;
            daLease.Fill(dsLease1);

            dsOccupant1.Clear();
            daOccupant.SelectCommand.CommandText = daOccupant.SelectCommand.CommandText.Replace("WEB_Test", Connection.WebDB);
            daOccupant.SelectCommand.Parameters["@Lease_Head_ID"].Value = DetailID;
            daOccupant.Fill(dsOccupant1);

            dsTenantContact1.Clear();
            daTenantContact.SelectCommand.CommandText = daTenantContact.SelectCommand.CommandText.Replace("WEB_Test", Connection.WebDB);
            daTenantContact.SelectCommand.Parameters["@Lease_Head_ID"].Value = DetailID;
            daTenantContact.Fill(dsTenantContact1);
            RunSetup(DetailID);
        }

        private void RunSetup(int Lease_Head_ID)
        {
            string RC = "";
            string sqlStr = "select p.RC from RE_Lease_Head h join RE_Property_Head p on p.PropertyHead_ID  = h.Property_Head_ID where  h.Lease_Head_ID = " + Lease_Head_ID.ToString();
            RC = Connection.SQLExecutor.ExecuteScalar(sqlStr, Connection.TRConnection).ToString();
            if (RC == "R")
            {
                colUnitNumber.Caption = "Primary Unit";
                colSpaceUnitPhone.Caption = "Unit Phone";
            }
            else
            {
                colUnitNumber.Caption = "Primary Space";
                colSpaceUnitPhone.Caption = "Space Phone";
            }

        }

    }
}
