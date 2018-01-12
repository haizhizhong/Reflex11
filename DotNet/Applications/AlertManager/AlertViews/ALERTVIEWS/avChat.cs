using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews
{
    public partial class avChat : AlertManager.AlertView
    {
        private ReflexChat.ucChat _ucChat;

        public avChat()
        {
            InitializeComponent();
        }

        public avChat(HMConnection.HMCon hmCon, TUC_HMDevXManager.TUC_HMDevXManager tuc)
        {
            InitializeComponent();

            this.HMConnection = hmCon;
            this.TUC_DevXMgr = tuc;


            _ucChat = new ReflexChat.ucChat(hmCon, tuc, ReflexChat.ChatType.Default);
            _ucChat.Dock = DockStyle.Fill;
            _ucChat.Parent = this;
            _ucChat.BringToFront();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

            _ucChat.LoadChatThread(DetailID);
        }
    }
}
