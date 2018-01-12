using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HMConnection;
using WS_Popups;


namespace AlertViews.Costing.LandReminders
{
    public partial class ucLandPaymentDue : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;

       
        #region Properties

        public HMCon HMConnection
        {
            set
            {
                Connection = value;
                if (Connection != null)
                {
                    sqlTRConnection.ConnectionString = Connection.TRConnection;                    
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


        public ucLandPaymentDue()
        {
            InitializeComponent();
        }

        public void LoadDetail(int DetailID)
        {
            ds_LandPaymentDue1.Clear();
            da_LandPaymentDue.SelectCommand.Parameters["@MIN_REPAY_ID"].Value = DetailID;
            da_LandPaymentDue.Fill(ds_LandPaymentDue1);
        }




    }
}
