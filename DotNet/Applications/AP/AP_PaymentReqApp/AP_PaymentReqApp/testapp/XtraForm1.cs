using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Windows.Forms;
using HMConnection;

namespace testapp
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private HMCon Connection;

        public XtraForm1()
        {   
            Connection = new HMCon("web_strike_test", "dev11", 12, "adam");
            DevXMgr = new TUC_HMDevXManager.TUC_HMDevXManager();
            InitializeComponent();
            
            AP_PaymentReqApp.ucAPPaymentRequest uc = new AP_PaymentReqApp.ucAPPaymentRequest(Connection, DevXMgr);
            uc.Dock = DockStyle.Fill;
            uc.Parent = this;
        }

    }
}