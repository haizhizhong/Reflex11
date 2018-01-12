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
			
namespace AlertProperties
{
    public partial class ucPropertyInterface : DevExpress.XtraEditors.XtraUserControl
    {
        protected HMConnection.HMCon Connection;
        protected TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        protected WS_Popups.frmPopup Popup;
        private int _AlertPoint = -1;
        private bool _Initialized = false;
        
        public ucPropertyInterface()
        {   
            InitializeComponent();
        }

        #region Properties

        public HMCon HMConnection
        {
            set
            {
                this.Connection = value;
                TR_Conn.ConnectionString = Connection.TRConnection;
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager HMDevXMgr
        {
            set
            {
                this.DevXMgr = value;
                Popup = new frmPopup(DevXMgr);
            }
        }

        public int AlertPointID
        {
            set
            {
                _AlertPoint = value;
            }
            get
            {
                return _AlertPoint;
            }
        }

        #endregion

        public void InitializeProperty()
        {
            bool bConn = false;
            bool bTuc = false;
            bool bAlert = false;
            
            if (Connection != null)
                bConn = true;
            if (DevXMgr != null)
                bTuc = true;
            if (_AlertPoint != -1)
                bAlert = true;

            if (bConn && bTuc && bAlert)
            {
                _Initialized = true;
                RunInitialization();
            }
            else
            {
                MessageBox.Show("HMConnection, HMDevXMgr and AlertPointID properties all need to be set prior to calling InitializeProperty.");
            }
        }

        private void ucPropertyInterface_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (!_Initialized)
                {
                    MessageBox.Show("The property user control has not been initialized.");
                    this.ParentForm.Close();
                }
                else
                {
                    DevXMgr.FormInit(this);
                }
            }
        }

        private void RunInitialization()
        {
            string sSelect = "select isnull(alert,'') from ApplicationAlertPoint where id=" + _AlertPoint;
            gTitle.Text = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection) + " Alert Properties";
        }

    }
}
