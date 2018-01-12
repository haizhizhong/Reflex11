using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using APGSTException;
using APPOSelect;

namespace AP_Invoice_Entry
{
	public class frmPOSelect : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;	
		private ucMatchPOReceipt ucMPOR;
		private ucUnreleasedContractPO ucUCPO;
        private ucSummaryContractPO ucSummCPO;
		public DevExpress.XtraTab.XtraTabControl xtraTabControl1;

		#region frmPOSelect Component Variables

		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
		private System.ComponentModel.Container components = null;

		#endregion

        public frmPOSelect(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, bool ShowContractPOSummary)
		{
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			InitializeComponent();

			ucMPOR = new ucMatchPOReceipt( Connection, DevXMgr ); 
			ucMPOR.Parent = xtraTabPage1;
			ucMPOR.Dock = DockStyle.Fill;
			ucMPOR.ResetSize();

			ucUCPO = new ucUnreleasedContractPO( Connection, DevXMgr ); 
			ucUCPO.Parent = xtraTabPage2;
			ucUCPO.Dock = DockStyle.Fill;
			ucUCPO.ResetSize();

            ucSummCPO = new ucSummaryContractPO(Connection, DevXMgr);
            ucSummCPO.Parent = xtraTabPage3;
            ucSummCPO.Dock = DockStyle.Fill;

            if( !ShowContractPOSummary )
                xtraTabPage3.PageVisible = false;
            else
                xtraTabPage2.PageVisible = false;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOSelect));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1088, 422);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1079, 391);
            this.xtraTabPage1.Text = "Match PO Receipt";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1079, 391);
            this.xtraTabPage2.Text = "Contract PO";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1079, 391);
            this.xtraTabPage3.Text = "Summary Contract PO";
            // 
            // frmPOSelect
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(1088, 422);
            this.Controls.Add(this.xtraTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPOSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PO Select";
            this.Load += new System.EventHandler(this.frmPOSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		public ucMatchPOReceipt MatchPOReciept
		{
			get{ return ucMPOR; }
//			set
//			{ 
//				ucMPOR = value; 
//				ucMPOR.Parent = xtraTabPage1;
//				ucMPOR.Dock = DockStyle.Fill;
//				ucMPOR.ResetSize();
//			}
		}

		public ucUnreleasedContractPO UnreleasedContractPO
		{
			get{ return ucUCPO; }
//			set
//			{ 
//				ucUCPO = value; 
//				ucUCPO.Parent = xtraTabPage2;
//				ucUCPO.Dock = DockStyle.Fill;
//				ucUCPO.ResetSize();
//			}
		}

        public ucSummaryContractPO SummaryContractPO
        {
            get { return ucSummCPO; }
            //			set
            //			{ 
            //				ucUCPO = value; 
            //				ucUCPO.Parent = xtraTabPage2;
            //				ucUCPO.Dock = DockStyle.Fill;
            //				ucUCPO.ResetSize();
            //			}
        }

		#endregion

		private void frmPOSelect_Load(object sender, System.EventArgs e)
		{
			myMgr.FormInit( this );
		}

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == xtraTabPage2 && ucUCPO != null)
            {
                ucUCPO.RefreshMe();
            }
            else if (e.Page == xtraTabPage3 && ucSummCPO != null)
            {
                ucSummCPO.RefreshMe();
            }
        }
	}
}

