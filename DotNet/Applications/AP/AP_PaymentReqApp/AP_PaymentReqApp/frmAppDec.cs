using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HMConnection;
using WS_Popups;

namespace AP_PaymentReqApp
{
    public partial class frmAppDec : DevExpress.XtraEditors.XtraForm
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private HMCon Connection;
        private frmPopup Popup;

        public frmAppDec(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (Popup.ShowPopup("Are you sure you want to approve this request?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                DialogResult = DialogResult.OK;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            if (Popup.ShowPopup("Are you sure you want to decline this request?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
            {
                if (mNotes.Text.Trim().Equals(""))
                {
                    Popup.ShowPopup("Notes are required if declining payment request.");
                    return;
                }
                DialogResult = DialogResult.Ignore;
            }
        }

        private void frmAppDec_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        public string Notes
        {
            get
            {
                return mNotes.Text.Replace("'", "''");
            }
        }
    }
}