using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlertViews.PO
{
    public partial class avPODocDetail : AlertManager.AlertView
    {
        private WO_CentralizedFSManager.ucFileManager CFS_FileMgr;
        private string RelTypePO = "PO";

        public avPODocDetail()
        {
            InitializeComponent();
        }

        public avPODocDetail(HMConnection.HMCon hmCon, TUC_HMDevXManager.TUC_HMDevXManager tuc)
        {
            InitializeComponent();

            this.HMConnection = hmCon;
            this.TUC_DevXMgr = tuc;

            CFS_FileMgr = new WO_CentralizedFSManager.ucFileManager(hmCon, tuc,
                                    WO_CentralizedFSManager.DocumentViewerMode.All, RelTypePO, -1, true, "PO");
            CFS_FileMgr.Dock = DockStyle.Fill;
            CFS_FileMgr.Parent = this;
            CFS_FileMgr.BringToFront();
        }

        private void DoSplitterLayout()
        {
            this.layoutControl1.Dock = DockStyle.Top;
            this.splitterControl.Dock = DockStyle.Top;

            this.layoutControl1.Parent.Controls.SetChildIndex(splitterControl, 1);
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

            if( CFS_FileMgr != null )
                CFS_FileMgr.RefreshFileList(RelTypePO, DetailID, true);
        }
    }
}
