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
    public partial class ucLandReminder : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;

        public ucLandReminder()
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
                    TR_Conn.ConnectionString = Connection.TRConnection;                    
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
            dsAgreeDet.Clear();
            dsExtDet.Clear();
            daAgreeDet.SelectCommand.Parameters["@agreement_id"].Value = DetailID;
            daExtDet.SelectCommand.Parameters["@agreement_id"].Value = DetailID;
            daAgreeDet.Fill(dsAgreeDet);
            daExtDet.Fill(dsExtDet);
        }

    }
}
