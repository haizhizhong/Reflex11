using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using HMConnection;
using System.Data.SqlClient;
using AP_Invoice_Entry;
using AP_Unpaid_Invoices;
using SupplierMaster;
using APCheckRePrint;
using APInvoiceAccrual;
using APVoid;
using ReportLauncher;
using CL_Dialog;
using APChkSelect;
using APRecurringInvoices;
using AP1099T5018Print;
using WS_Popups;
using AP_PaymentReqApp;
using APHoldbackRelease;

namespace ucAP
{
	public class ucAP : DevExpress.XtraEditors.XtraUserControl
	{
		private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
		private HMCon Connection;
		private ucAP_InvoiceEntry ucAPIE;
		private ucSupplierMaster ucSuppMast;
		private TUC_HMReportLauncher ucReportLauncher;
		private ucAPCheckRePrint ucAPCRP;
		private ucAPInvoiceAccrual ucAPIA;
		private ucAPVoid ucAPV;
		private ucAP_Unpaid_Invoices ucAPUI;
		private ucAPRecuringInvoices ucAPRI;
		private ucAP1099T5018Print ucAP1099;
		private ChqSelectPA ucAPCS;
		private ucAPVoid ucAPVoidAssistant;
        private AP_SubcontractorCompliance.ucMasterView SubConComp;
		private frmPopup Popup;
		private KeyControlAccess.Validator KCA_Validator;
        private ucAPPaymentRequest PaymentRequest;
        private ucAPHoldbackRelease HoldbackRelease;
		private const int CONST_KCA_ACCOUNTS_PAYABLE = 14;
        private const int CONST_HOLDBACK_APPROVAL = 71;
		private bool SetSecurity = false;
        private AP_DEFTXferFile.ucAP_DEFTXferFile DeftXferFile;
        private AP_ComplianceNotification.ucMain ucComplianceNotify;

		#region ucAP Component Variables

		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraEditors.PanelControl pBack;
		private HMTools.HMTabControl hmTabControl1;
		private HMTools.HMTabPage hmTabPage1;
		private HMTools.HMTabPage hmTabPage2;
		private HMTools.HMTabPage hmTabPage3;
		private HMTools.HMTabPage hmTabPage4;
		private HMTools.HMNavigationBar hmNavigationBar1;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup4;
		private DevExpress.XtraTreeList.TreeList treeList1;
		private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
		private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
		private DevExpress.XtraTreeList.TreeList treeList2;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
		private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer3;
		private DevExpress.XtraTreeList.TreeList treeList3;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
		private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer4;
		private DevExpress.XtraTreeList.TreeList treeList4;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
		private HMTools.HMTabControl hmTabControl3;
		private HMTools.HMTabPage hmTabPage7;
		private HMTools.HMTabPage hmTabPage5;
		private HMTools.HMTabControl hmTabControl2;
		private HMTools.HMTabPage hmTabPage6;
		private HMTools.HMTabControl hmTabControl4;
		private HMTools.HMTabPage hmTabPage8;
		private HMTools.HMTabPage hmTabPage9;
		private HMTools.HMTabPage hmTabPage10;
        private HMTools.HMTabPage hmTabPage11;
		private HMTools.HMTabControl hmTabControl5;
		private HMTools.HMTabPage hmTabPage14;
		private HMTools.HMTabPage hmTabPage15;
		private DevExpress.XtraBars.BarManager barManager1;
		private DevExpress.XtraBars.BarDockControl barDockControlTop;
		private DevExpress.XtraBars.BarDockControl barDockControlBottom;
		private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.Bar bar2;
		private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private HMTools.HMTabPage tpSubConComp;
        private HMTools.HMTabPage tpPaymentSubApp;
        private HMTools.HMTabPage tpHBRelease;
        private HMTools.HMTabPage tpGenerateDEFTFileTransfer;
        private HMTools.HMTabPage tpComplianceNotification;
		private System.ComponentModel.IContainer components;

		#endregion

		public ucAP( string WebDB, string Server, string CompanyID, string User, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr )
		{
			CL_Dialog.PleaseWait.Show( "Loading Accounts Payable...\r\nPlease Wait", null );
			Connection = new HMCon( WebDB, Server, Convert.ToInt32( CompanyID ), User );
			KCA_Validator = new KeyControlAccess.Validator( Connection, DevXMgr, CONST_KCA_ACCOUNTS_PAYABLE );
			
			InitializeComponent();						
			KCA_Validator.MenuBar = bar2;
            pBack.BringToFront();

			this.DevXMgr = DevXMgr;
			Popup = new frmPopup( DevXMgr );
			
			Connection.SQLExecutor.ExecuteNonQuery( "exec sp_fill_mluser_supervisor '"+Connection.MLUser+"','"+Connection.MLUser+"', 1", Connection.TRConnection );
	
			SetupCountryCode();            

			CL_Dialog.PleaseWait.Hide();
            bar2.Visible = true;

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

//		/// <summary>
//		/// The main entry point for the application.
//		/// </summary>
//		[STAThread]
//		static void Main( string[] args ) 
//		{
//			try
//			{
//				if( args.Length == 4 )
//				{
//					Application.Run(new frmAP( args[0], args[1], args[2], args[3] ));
//				}
//			}
//			catch( Exception ex )
//			{
//				MessageBox.Show( ex.Message + ex.StackTrace );
//			}
//		}

		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.hmNavigationBar1 = new HMTools.HMNavigationBar();
            this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer4 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeList4 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.treeList3 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.hmTabControl1 = new HMTools.HMTabControl();
            this.hmTabPage1 = new HMTools.HMTabPage();
            this.hmTabControl3 = new HMTools.HMTabControl();
            this.hmTabPage7 = new HMTools.HMTabPage();
            this.hmTabPage5 = new HMTools.HMTabPage();
            this.tpPaymentSubApp = new HMTools.HMTabPage();
            this.hmTabPage2 = new HMTools.HMTabPage();
            this.hmTabControl2 = new HMTools.HMTabControl();
            this.hmTabPage6 = new HMTools.HMTabPage();
            this.tpSubConComp = new HMTools.HMTabPage();
            this.hmTabPage3 = new HMTools.HMTabPage();
            this.hmTabControl4 = new HMTools.HMTabControl();
            this.hmTabPage8 = new HMTools.HMTabPage();
            this.hmTabPage9 = new HMTools.HMTabPage();
            this.hmTabPage10 = new HMTools.HMTabPage();
            this.hmTabPage11 = new HMTools.HMTabPage();
            this.hmTabPage15 = new HMTools.HMTabPage();
            this.tpHBRelease = new HMTools.HMTabPage();
            this.tpGenerateDEFTFileTransfer = new HMTools.HMTabPage();
            this.tpComplianceNotification = new HMTools.HMTabPage();
            this.hmTabPage4 = new HMTools.HMTabPage();
            this.hmTabControl5 = new HMTools.HMTabControl();
            this.hmTabPage14 = new HMTools.HMTabPage();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pBack = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerLeft.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hmNavigationBar1)).BeginInit();
            this.hmNavigationBar1.SuspendLayout();
            this.navBarGroupControlContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.navBarGroupControlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl1)).BeginInit();
            this.hmTabControl1.SuspendLayout();
            this.hmTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl3)).BeginInit();
            this.hmTabControl3.SuspendLayout();
            this.hmTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl2)).BeginInit();
            this.hmTabControl2.SuspendLayout();
            this.hmTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl4)).BeginInit();
            this.hmTabControl4.SuspendLayout();
            this.hmTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl5)).BeginInit();
            this.hmTabControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerLeft});
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.hideContainerLeft.Controls.Add(this.dockPanel1);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 20);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(19, 746);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("46a5c5b8-c18d-4571-9414-1f28be4318a4");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(250, 742);
            this.dockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(250, 746);
            this.dockPanel1.Text = "Navigation";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.hmNavigationBar1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(241, 719);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // hmNavigationBar1
            // 
            this.hmNavigationBar1.ActiveGroup = this.navBarGroup3;
            this.hmNavigationBar1.ContentButtonHint = null;
            this.hmNavigationBar1.Controls.Add(this.navBarGroupControlContainer1);
            this.hmNavigationBar1.Controls.Add(this.navBarGroupControlContainer2);
            this.hmNavigationBar1.Controls.Add(this.navBarGroupControlContainer3);
            this.hmNavigationBar1.Controls.Add(this.navBarGroupControlContainer4);
            this.hmNavigationBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmNavigationBar1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3,
            this.navBarGroup4});
            this.hmNavigationBar1.HM_DataRow.AppFlavor = "Main";
            this.hmNavigationBar1.HM_DataRow.AppStyle = "NewStyle";
            this.hmNavigationBar1.HM_DataRow.ID = 10334;
            this.hmNavigationBar1.HM_DataRow.ParentID = 10185;
            this.hmNavigationBar1.HM_Module = "Accounts Payable";
            this.hmNavigationBar1.HMTabControl = this.hmTabControl1;
            this.hmNavigationBar1.Location = new System.Drawing.Point(0, 0);
            this.hmNavigationBar1.Name = "hmNavigationBar1";
            this.hmNavigationBar1.OptionsNavPane.ExpandedWidth = 241;
            this.hmNavigationBar1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.hmNavigationBar1.Size = new System.Drawing.Size(241, 719);
            this.hmNavigationBar1.TabIndex = 3;
            this.hmNavigationBar1.Text = "hmNavigationBar1";
            this.hmNavigationBar1.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            // 
            // navBarGroup4
            // 
            this.navBarGroup4.Caption = "Reporting";
            this.navBarGroup4.ControlContainer = this.navBarGroupControlContainer4;
            this.navBarGroup4.GroupClientHeight = 80;
            this.navBarGroup4.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup4.Name = "navBarGroup4";
            // 
            // navBarGroupControlContainer4
            // 
            this.navBarGroupControlContainer4.Controls.Add(this.treeList4);
            this.navBarGroupControlContainer4.Name = "navBarGroupControlContainer4";
            this.navBarGroupControlContainer4.Size = new System.Drawing.Size(241, 532);
            this.navBarGroupControlContainer4.TabIndex = 3;
            // 
            // treeList4
            // 
            this.treeList4.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn4});
            this.treeList4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList4.Location = new System.Drawing.Point(0, 0);
            this.treeList4.Name = "treeList4";
            this.treeList4.BeginUnboundLoad();
            this.treeList4.AppendNode(new object[] {
            "Accounts Payable Report Launcher"}, -1);
            this.treeList4.EndUnboundLoad();
            this.treeList4.OptionsBehavior.Editable = false;
            this.treeList4.Size = new System.Drawing.Size(241, 532);
            this.treeList4.TabIndex = 0;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.MinWidth = 34;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 0;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.treeList1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(240, 529);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.BeginUnboundLoad();
            this.treeList1.AppendNode(new object[] {
            "Invoice Entry"}, -1);
            this.treeList1.AppendNode(new object[] {
            "Unpaid Invoices"}, -1);
            this.treeList1.AppendNode(new object[] {
            "Payment Submission Approval"}, -1);
            this.treeList1.EndUnboundLoad();
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.Size = new System.Drawing.Size(240, 529);
            this.treeList1.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.treeList2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(241, 530);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // treeList2
            // 
            this.treeList2.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList2.Location = new System.Drawing.Point(0, 0);
            this.treeList2.Name = "treeList2";
            this.treeList2.BeginUnboundLoad();
            this.treeList2.AppendNode(new object[] {
            "Supplier Master"}, -1);
            this.treeList2.AppendNode(new object[] {
            "Subcontractor Compliance"}, -1);
            this.treeList2.EndUnboundLoad();
            this.treeList2.OptionsBehavior.Editable = false;
            this.treeList2.Size = new System.Drawing.Size(241, 530);
            this.treeList2.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.MinWidth = 34;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            // 
            // navBarGroupControlContainer3
            // 
            this.navBarGroupControlContainer3.Controls.Add(this.treeList3);
            this.navBarGroupControlContainer3.Name = "navBarGroupControlContainer3";
            this.navBarGroupControlContainer3.Size = new System.Drawing.Size(241, 532);
            this.navBarGroupControlContainer3.TabIndex = 2;
            // 
            // treeList3
            // 
            this.treeList3.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn3});
            this.treeList3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList3.Location = new System.Drawing.Point(0, 0);
            this.treeList3.Name = "treeList3";
            this.treeList3.BeginUnboundLoad();
            this.treeList3.AppendNode(new object[] {
            "Invoice Accrual Assistant"}, -1);
            this.treeList3.AppendNode(new object[] {
            "Check Processing Assistant"}, -1);
            this.treeList3.AppendNode(new object[] {
            "Recurring Invoice Assistant"}, -1);
            this.treeList3.AppendNode(new object[] {
            "AP Void Assistant"}, -1);
            this.treeList3.AppendNode(new object[] {
            "T5018"}, -1);
            this.treeList3.AppendNode(new object[] {
            "Holdback Release Assistant"}, -1);
            this.treeList3.AppendNode(new object[] {
            "Generate AP Transfer File"}, -1);
            this.treeList3.AppendNode(new object[] {
            "Compliance Notification Assistant"}, -1);
            this.treeList3.EndUnboundLoad();
            this.treeList3.OptionsBehavior.Editable = false;
            this.treeList3.Size = new System.Drawing.Size(241, 532);
            this.treeList3.TabIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.MinWidth = 34;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Processing";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.GroupClientHeight = 80;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Maintenance";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup2.GroupClientHeight = 80;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "Tools";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainer3;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 80;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // hmTabControl1
            // 
            this.hmTabControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hmTabControl1.Appearance.Options.UseBackColor = true;
            this.hmTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmTabControl1.HM_IsRoot = true;
            this.hmTabControl1.HM_NavigationBar = this.hmNavigationBar1;
            this.hmTabControl1.HM_NextID = -24;
            this.hmTabControl1.Location = new System.Drawing.Point(19, 20);
            this.hmTabControl1.Name = "hmTabControl1";
            this.hmTabControl1.SelectedTabPage = this.hmTabPage1;
            this.hmTabControl1.Size = new System.Drawing.Size(1085, 746);
            this.hmTabControl1.TabIndex = 2;
            this.hmTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.hmTabPage1,
            this.hmTabPage2,
            this.hmTabPage3,
            this.hmTabPage4});
            this.hmTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.hmTabControl1_SelectedPageChanged);
            // 
            // hmTabPage1
            // 
            this.hmTabPage1.Controls.Add(this.hmTabControl3);
            this.hmTabPage1.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage1.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage1.HM_DataRow.ID = 10335;
            this.hmTabPage1.HM_DataRow.ParentID = 10334;
            this.hmTabPage1.HM_Tree = this.treeList1;
            this.hmTabPage1.Name = "hmTabPage1";
            this.hmTabPage1.node = null;
            this.hmTabPage1.Size = new System.Drawing.Size(1079, 718);
            this.hmTabPage1.Text = "Processing";
            // 
            // hmTabControl3
            // 
            this.hmTabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmTabControl3.HM_IsRoot = false;
            this.hmTabControl3.HM_NavigationBar = null;
            this.hmTabControl3.HM_NextID = -1;
            this.hmTabControl3.Location = new System.Drawing.Point(0, 0);
            this.hmTabControl3.Name = "hmTabControl3";
            this.hmTabControl3.SelectedTabPage = this.hmTabPage7;
            this.hmTabControl3.Size = new System.Drawing.Size(1079, 718);
            this.hmTabControl3.TabIndex = 1;
            this.hmTabControl3.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.hmTabPage7,
            this.hmTabPage5,
            this.tpPaymentSubApp});
            this.hmTabControl3.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.hmTabControl3_SelectedPageChanged);
            // 
            // hmTabPage7
            // 
            this.hmTabPage7.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage7.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage7.HM_DataRow.ID = 10339;
            this.hmTabPage7.HM_DataRow.ParentID = 10335;
            this.hmTabPage7.HM_Tree = null;
            this.hmTabPage7.Name = "hmTabPage7";
            this.hmTabPage7.nodeID = 0;
            this.hmTabPage7.Size = new System.Drawing.Size(1073, 690);
            this.hmTabPage7.Text = "Invoice Entry";
            // 
            // hmTabPage5
            // 
            this.hmTabPage5.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage5.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage5.HM_DataRow.ID = 10340;
            this.hmTabPage5.HM_DataRow.ParentID = 10335;
            this.hmTabPage5.HM_Tree = null;
            this.hmTabPage5.Name = "hmTabPage5";
            this.hmTabPage5.nodeID = 1;
            this.hmTabPage5.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage5.Text = "Unpaid Invoices";
            // 
            // tpPaymentSubApp
            // 
            this.tpPaymentSubApp.HM_DataRow.AppFlavor = "Main";
            this.tpPaymentSubApp.HM_DataRow.AppStyle = "NewStyle";
            this.tpPaymentSubApp.HM_DataRow.ID = 20806;
            this.tpPaymentSubApp.HM_DataRow.ParentID = 10335;
            this.tpPaymentSubApp.HM_Tree = null;
            this.tpPaymentSubApp.Name = "tpPaymentSubApp";
            this.tpPaymentSubApp.nodeID = 2;
            this.tpPaymentSubApp.Size = new System.Drawing.Size(1073, 688);
            this.tpPaymentSubApp.Text = "Payment Submission Approval";
            // 
            // hmTabPage2
            // 
            this.hmTabPage2.Controls.Add(this.hmTabControl2);
            this.hmTabPage2.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage2.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage2.HM_DataRow.ID = 10336;
            this.hmTabPage2.HM_DataRow.ParentID = 10334;
            this.hmTabPage2.HM_Tree = this.treeList2;
            this.hmTabPage2.Name = "hmTabPage2";
            this.hmTabPage2.node = null;
            this.hmTabPage2.Size = new System.Drawing.Size(1079, 716);
            this.hmTabPage2.Text = "Maintenance";
            // 
            // hmTabControl2
            // 
            this.hmTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmTabControl2.HM_IsRoot = false;
            this.hmTabControl2.HM_NavigationBar = null;
            this.hmTabControl2.HM_NextID = -1;
            this.hmTabControl2.Location = new System.Drawing.Point(0, 0);
            this.hmTabControl2.Name = "hmTabControl2";
            this.hmTabControl2.SelectedTabPage = this.hmTabPage6;
            this.hmTabControl2.Size = new System.Drawing.Size(1079, 716);
            this.hmTabControl2.TabIndex = 0;
            this.hmTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.hmTabPage6,
            this.tpSubConComp});
            this.hmTabControl2.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.hmTabControl2_SelectedPageChanged);
            // 
            // hmTabPage6
            // 
            this.hmTabPage6.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage6.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage6.HM_DataRow.ID = 10341;
            this.hmTabPage6.HM_DataRow.ParentID = 10336;
            this.hmTabPage6.HM_Tree = null;
            this.hmTabPage6.Name = "hmTabPage6";
            this.hmTabPage6.nodeID = 0;
            this.hmTabPage6.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage6.Text = "Supplier Master";
            // 
            // tpSubConComp
            // 
            this.tpSubConComp.HM_DataRow.AppFlavor = "Main";
            this.tpSubConComp.HM_DataRow.AppStyle = "NewStyle";
            this.tpSubConComp.HM_DataRow.ID = 20783;
            this.tpSubConComp.HM_DataRow.ParentID = 10336;
            this.tpSubConComp.HM_Tree = null;
            this.tpSubConComp.Name = "tpSubConComp";
            this.tpSubConComp.nodeID = 1;
            this.tpSubConComp.Size = new System.Drawing.Size(1073, 688);
            this.tpSubConComp.Text = "Subcontractor Compliance";
            // 
            // hmTabPage3
            // 
            this.hmTabPage3.Controls.Add(this.hmTabControl4);
            this.hmTabPage3.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage3.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage3.HM_DataRow.ID = 10337;
            this.hmTabPage3.HM_DataRow.ParentID = 10334;
            this.hmTabPage3.HM_Tree = this.treeList3;
            this.hmTabPage3.Name = "hmTabPage3";
            this.hmTabPage3.node = null;
            this.hmTabPage3.Size = new System.Drawing.Size(1079, 718);
            this.hmTabPage3.Text = "Tools";
            // 
            // hmTabControl4
            // 
            this.hmTabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmTabControl4.HM_IsRoot = false;
            this.hmTabControl4.HM_NavigationBar = null;
            this.hmTabControl4.HM_NextID = -1;
            this.hmTabControl4.Location = new System.Drawing.Point(0, 0);
            this.hmTabControl4.Name = "hmTabControl4";
            this.hmTabControl4.SelectedTabPage = this.hmTabPage8;
            this.hmTabControl4.Size = new System.Drawing.Size(1079, 718);
            this.hmTabControl4.TabIndex = 0;
            this.hmTabControl4.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.hmTabPage8,
            this.hmTabPage9,
            this.hmTabPage10,
            this.hmTabPage11,
            this.hmTabPage15,
            this.tpHBRelease,
            this.tpGenerateDEFTFileTransfer,
            this.tpComplianceNotification});
            this.hmTabControl4.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.hmTabControl4_SelectedPageChanged);
            // 
            // hmTabPage8
            // 
            this.hmTabPage8.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage8.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage8.HM_DataRow.ID = 10342;
            this.hmTabPage8.HM_DataRow.ParentID = 10337;
            this.hmTabPage8.HM_Tree = null;
            this.hmTabPage8.Name = "hmTabPage8";
            this.hmTabPage8.nodeID = 0;
            this.hmTabPage8.Size = new System.Drawing.Size(1073, 690);
            this.hmTabPage8.Text = "Invoice Accrual Assistant";
            // 
            // hmTabPage9
            // 
            this.hmTabPage9.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage9.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage9.HM_DataRow.ID = 10343;
            this.hmTabPage9.HM_DataRow.ParentID = 10337;
            this.hmTabPage9.HM_Tree = null;
            this.hmTabPage9.Name = "hmTabPage9";
            this.hmTabPage9.nodeID = 1;
            this.hmTabPage9.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage9.Text = "Check Processing Assistant";
            // 
            // hmTabPage10
            // 
            this.hmTabPage10.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage10.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage10.HM_DataRow.ID = 10344;
            this.hmTabPage10.HM_DataRow.ParentID = 10337;
            this.hmTabPage10.HM_Tree = null;
            this.hmTabPage10.Name = "hmTabPage10";
            this.hmTabPage10.nodeID = 2;
            this.hmTabPage10.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage10.Text = "Recurring Invoice Assistant";
            // 
            // hmTabPage11
            // 
            this.hmTabPage11.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage11.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage11.HM_DataRow.ID = 10345;
            this.hmTabPage11.HM_DataRow.ParentID = 10337;
            this.hmTabPage11.HM_Tree = null;
            this.hmTabPage11.Name = "hmTabPage11";
            this.hmTabPage11.nodeID = 3;
            this.hmTabPage11.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage11.Text = "AP Void Assistant";
            // 
            // hmTabPage15
            // 
            this.hmTabPage15.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage15.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage15.HM_DataRow.ID = 10449;
            this.hmTabPage15.HM_DataRow.ParentID = 10337;
            this.hmTabPage15.HM_Tree = null;
            this.hmTabPage15.Name = "hmTabPage15";
            this.hmTabPage15.nodeID = 4;
            this.hmTabPage15.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage15.Text = "T5018";
            // 
            // tpHBRelease
            // 
            this.tpHBRelease.HM_DataRow.AppFlavor = "Main";
            this.tpHBRelease.HM_DataRow.AppStyle = "NewStyle";
            this.tpHBRelease.HM_DataRow.ID = 21004;
            this.tpHBRelease.HM_DataRow.ParentID = 10337;
            this.tpHBRelease.HM_Tree = null;
            this.tpHBRelease.Name = "tpHBRelease";
            this.tpHBRelease.nodeID = 5;
            this.tpHBRelease.Size = new System.Drawing.Size(1073, 688);
            this.tpHBRelease.Text = "Holdback Release Assistant";
            // 
            // tpGenerateDEFTFileTransfer
            // 
            this.tpGenerateDEFTFileTransfer.HM_DataRow.AppFlavor = "Main";
            this.tpGenerateDEFTFileTransfer.HM_DataRow.AppStyle = "NewStyle";
            this.tpGenerateDEFTFileTransfer.HM_DataRow.ID = 21487;
            this.tpGenerateDEFTFileTransfer.HM_DataRow.ParentID = 10337;
            this.tpGenerateDEFTFileTransfer.HM_Tree = null;
            this.tpGenerateDEFTFileTransfer.Name = "tpGenerateDEFTFileTransfer";
            this.tpGenerateDEFTFileTransfer.nodeID = 6;
            this.tpGenerateDEFTFileTransfer.Size = new System.Drawing.Size(1073, 688);
            this.tpGenerateDEFTFileTransfer.Text = "Generate AP Transfer File";
            // 
            // tpComplianceNotification
            // 
            this.tpComplianceNotification.HM_DataRow.AppFlavor = "Main";
            this.tpComplianceNotification.HM_DataRow.AppStyle = "NewStyle";
            this.tpComplianceNotification.HM_DataRow.ID = 21595;
            this.tpComplianceNotification.HM_DataRow.ParentID = 10337;
            this.tpComplianceNotification.HM_Tree = null;
            this.tpComplianceNotification.Name = "tpComplianceNotification";
            this.tpComplianceNotification.nodeID = 7;
            this.tpComplianceNotification.Size = new System.Drawing.Size(1073, 688);
            this.tpComplianceNotification.Text = "Compliance Notification Assistant";
            // 
            // hmTabPage4
            // 
            this.hmTabPage4.Controls.Add(this.hmTabControl5);
            this.hmTabPage4.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage4.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage4.HM_DataRow.ID = 10338;
            this.hmTabPage4.HM_DataRow.ParentID = 10334;
            this.hmTabPage4.HM_Tree = this.treeList4;
            this.hmTabPage4.Name = "hmTabPage4";
            this.hmTabPage4.node = null;
            this.hmTabPage4.Size = new System.Drawing.Size(1079, 716);
            this.hmTabPage4.Text = "Reporting";
            // 
            // hmTabControl5
            // 
            this.hmTabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hmTabControl5.HM_IsRoot = false;
            this.hmTabControl5.HM_NavigationBar = null;
            this.hmTabControl5.HM_NextID = -1;
            this.hmTabControl5.Location = new System.Drawing.Point(0, 0);
            this.hmTabControl5.Name = "hmTabControl5";
            this.hmTabControl5.SelectedTabPage = this.hmTabPage14;
            this.hmTabControl5.Size = new System.Drawing.Size(1079, 716);
            this.hmTabControl5.TabIndex = 0;
            this.hmTabControl5.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.hmTabPage14});
            this.hmTabControl5.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.hmTabControl5_SelectedPageChanged);
            // 
            // hmTabPage14
            // 
            this.hmTabPage14.HM_DataRow.AppFlavor = "Main";
            this.hmTabPage14.HM_DataRow.AppStyle = "NewStyle";
            this.hmTabPage14.HM_DataRow.ID = 10348;
            this.hmTabPage14.HM_DataRow.ParentID = 10338;
            this.hmTabPage14.HM_Tree = null;
            this.hmTabPage14.Name = "hmTabPage14";
            this.hmTabPage14.nodeID = 0;
            this.hmTabPage14.Size = new System.Drawing.Size(1073, 688);
            this.hmTabPage14.Text = "Accounts Payable Report Launcher";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 0;
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1104, 20);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 766);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1104, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 20);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 746);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1104, 20);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 746);
            // 
            // pBack
            // 
            this.pBack.Appearance.BackColor = System.Drawing.Color.White;
            this.pBack.Appearance.Options.UseBackColor = true;
            this.pBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBack.Location = new System.Drawing.Point(19, 20);
            this.pBack.Name = "pBack";
            this.pBack.Size = new System.Drawing.Size(1085, 746);
            this.pBack.TabIndex = 1;
            // 
            // ucAP
            // 
            this.Controls.Add(this.hmTabControl1);
            this.Controls.Add(this.pBack);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucAP";
            this.Size = new System.Drawing.Size(1104, 766);
            this.Load += new System.EventHandler(this.frmAP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerLeft.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hmNavigationBar1)).EndInit();
            this.hmNavigationBar1.ResumeLayout(false);
            this.navBarGroupControlContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList4)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.navBarGroupControlContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl1)).EndInit();
            this.hmTabControl1.ResumeLayout(false);
            this.hmTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl3)).EndInit();
            this.hmTabControl3.ResumeLayout(false);
            this.hmTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl2)).EndInit();
            this.hmTabControl2.ResumeLayout(false);
            this.hmTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl4)).EndInit();
            this.hmTabControl4.ResumeLayout(false);
            this.hmTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hmTabControl5)).EndInit();
            this.hmTabControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmAP_Load(object sender, System.EventArgs e)
		{
            CL_Dialog.PleaseWait.Show("Loading Accounts Payable..." + Environment.NewLine + "Please Wait", this.ParentForm);
            
			Hide();
			DevXMgr.FormInit(this);	
			
			SetSecurity = true;
			hmNavigationBar1.SetSecurity(Connection.Department,Connection.TRConnection);
			SetSecurity = false;
	
			FindBottomTabControl( hmTabControl1 );
            SetupHoldbackRelease();

			pBack.SendToBack();			
			dockPanel1.Width = 250;
			Show();
            CL_Dialog.PleaseWait.Hide();
        }

		private void SetupCountryCode()
		{
			if( Connection.CountryCode == "C" )
				hmTabPage15.Text = "T5018";
			else
                hmTabPage15.Text = "1099";
		}

        private void SetupHoldbackRelease()
        {
            string sSQL = @"select count(*) from Approval_Topic where Active = 1 and ID=" + CONST_HOLDBACK_APPROVAL;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection)) == 0)
            {
                tpHBRelease.PageVisible = false;
            }
        }

		private void ExecuteNonQuery( string CmdText, SqlConnection Conn )
		{
			SqlConnection Connection = new SqlConnection( Conn.ConnectionString );
			SqlCommand cmd = new SqlCommand( CmdText, Connection );
			try
			{				
				Connection.Open();
				cmd.ExecuteNonQuery();
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show(  ex.Message + ex.StackTrace );
			}
			finally
			{
				Connection.Close();
			}
		}

		private object ExecuteScalar( string CmdText, SqlConnection Conn )
		{
			SqlConnection Connection = new SqlConnection( Conn.ConnectionString );
			object obj = null;
			SqlCommand cmd = new SqlCommand( CmdText, Connection );
			try
			{		
				Connection.Open();
				obj = cmd.ExecuteScalar();
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show( ex.Message + ex.StackTrace );
			}
			finally
			{
				Connection.Close();
			}
			if( obj != null )
			{
				if( obj.GetType() == typeof( DBNull ) )
					obj = null;
			}
			return obj;
		}		

		private void hmTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
            if (SetSecurity)
                return;

            if (hmNavigationBar1.SettingSecurity) return;

            if (hmTabControl3.SelectedTabPage != null)
            {
                if (hmTabControl3.SelectedTabPage.PageVisible)
                {
                    if (e != null && e.Page != null)
                        CL_Dialog.PleaseWait.Show("Loading " + e.Page.Text + "..." + Environment.NewLine + "Please Wait", this.ParentForm);
                    if (hmTabControl3.SelectedTabPage == hmTabPage7)
                    {
                        if (ucAPIE != null)
                        {
                            ucAPIE.RefreshMe();
                        }
                    }
                    else if (hmTabControl3.SelectedTabPage == hmTabPage5)
                    {
                        if (ucAPUI != null)
                        {
                            ucAPUI.RefreshMe();
                        }
                    }

                    if (hmTabControl3.SelectedTabPage == hmTabPage7 && ucAPIE == null)
                    {                        
                        ucAPIE = new ucAP_InvoiceEntry(Connection, DevXMgr, KCA_Validator);
                        pBack.BringToFront();
                        ucAPIE.Parent = hmTabPage7;
                        ucAPIE.Dock = DockStyle.Fill;
                        pBack.SendToBack();
                    }
                    else if (hmTabControl3.SelectedTabPage == hmTabPage5 && ucAPUI == null)
                    {
                        ucAPUI = new ucAP_Unpaid_Invoices(Connection, DevXMgr, KCA_Validator);
                        pBack.BringToFront();
                        ucAPUI.Parent = hmTabPage5;
                        ucAPUI.Dock = DockStyle.Fill;
                        pBack.SendToBack();
                    }
                    else if (hmTabControl3.SelectedTabPage == tpPaymentSubApp && PaymentRequest == null)
                    {
                        PaymentRequest = new ucAPPaymentRequest(Connection, DevXMgr, KCA_Validator);
                        PaymentRequest.Dock = DockStyle.Fill;
                        PaymentRequest.Parent = tpPaymentSubApp;
                    }

                    if (hmTabControl3.SelectedTabPage == tpPaymentSubApp && PaymentRequest != null)
                    {
                        PaymentRequest.RefreshMe();
                    }

                    CL_Dialog.PleaseWait.Hide();
                }
            }
		}

		private void hmTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if( SetSecurity )
				return;

            if (hmNavigationBar1.SettingSecurity) return;

			if( hmTabControl1.SelectedTabPage != null ) 
			{
				if( hmTabControl1.SelectedTabPage.PageVisible )
                {
                    if(e != null && e.Page != null)
                        CL_Dialog.PleaseWait.Show("Loading " + e.Page.Text + "..." + Environment.NewLine + "Please Wait", this.ParentForm);
                    if ( hmTabControl1.SelectedTabPage == hmTabPage1 )
					{
						if( ucAPIE != null )
						{
							ucAPIE.RefreshMe();
						}
						if( ucAPUI != null )
						{
							ucAPUI.RefreshMe();
						}
					}
					else if( hmTabControl1.SelectedTabPage == hmTabPage2 )
					{
						hmTabControl2_SelectedPageChanged( null, null );
					}
					else if( hmTabControl1.SelectedTabPage == hmTabPage3 )
					{
						hmTabControl4_SelectedPageChanged( null, null );
					}
					else if( hmTabControl1.SelectedTabPage == hmTabPage4 && ucReportLauncher == null )
					{
						ucReportLauncher = new ReportLauncher.TUC_HMReportLauncher();
						ucReportLauncher.ConnectTimeout = 200;
						pBack.BringToFront();
						ucReportLauncher.Parent = hmTabPage14;
						ucReportLauncher.Dock = DockStyle.Fill;

						string sSelect = "select DBID from DBS where DBName='"+Connection.TRDB+"'";
						object oDBID = ExecuteScalar( sSelect, new SqlConnection( Connection.WebConnection ) );
						if( oDBID != null && oDBID != DBNull.Value )
						{
							ucReportLauncher.InitReportLauncher( Connection.WebDB, Connection.WebServer, oDBID.ToString(), "38", "50", "0", Connection.ReportPath, 
								Connection.MLUser, DevXMgr, Connection.CompanyID );
						}
						pBack.SendToBack();	
					}
                    CL_Dialog.PleaseWait.Hide();
                }
			}
		}

		private void frmAP_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( Popup.ShowPopup( "Close AP Application?", frmPopup.PopupType.Yes_No ) == frmPopup.PopupResult.Yes ) 
				e.Cancel = false;
			else
				e.Cancel = true;
		}

		private void hmTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if( SetSecurity )
				return;

            if (hmNavigationBar1.SettingSecurity) return;

			if( hmTabControl2.SelectedTabPage != null ) 
			{
				if( hmTabControl2.SelectedTabPage.PageVisible )
                {
                    if (e != null && e.Page != null)
                        CL_Dialog.PleaseWait.Show("Loading " + e.Page.Text + "..." + Environment.NewLine + "Please Wait", this.ParentForm);
                    if ( hmTabControl2.SelectedTabPage == hmTabPage6 && ucSuppMast == null )
					{
                        ucSuppMast = new ucSupplierMaster(Connection, KCA_Validator);
                        pBack.BringToFront();
                        ucSuppMast.Style = DevXMgr;
                        ucSuppMast.Parent = hmTabPage6;
                        ucSuppMast.Dock = DockStyle.Fill;
                        pBack.SendToBack();
					}
                    else if (hmTabControl2.SelectedTabPage == tpSubConComp && SubConComp == null)
                    {
                        SubConComp = new AP_SubcontractorCompliance.ucMasterView(Connection, DevXMgr);                        
                        SubConComp.Dock = DockStyle.Fill;
                        SubConComp.Parent = tpSubConComp;
                    }

                    if (hmTabControl2.SelectedTabPage == hmTabPage6 && ucSuppMast != null)
                    {
                        ucSuppMast.RefreshSubConComp();
                    }
                    else if (hmTabControl2.SelectedTabPage == tpSubConComp && SubConComp != null)
                    {
                        SubConComp.RefreshDS();
                    }
                    CL_Dialog.PleaseWait.Hide();
                }                
			}            
		}

		private void hmTabControl4_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			if( SetSecurity )
				return;

            if (hmNavigationBar1.SettingSecurity) return;

			if( hmTabControl4.SelectedTabPage != null ) 
			{
				if( hmTabControl4.SelectedTabPage.PageVisible )
				{
                    if (e != null && e.Page != null)
                        CL_Dialog.PleaseWait.Show("Loading " + e.Page.Text + "..." + Environment.NewLine + "Please Wait", this.ParentForm);
                    //if( hmTabControl4.SelectedTabPage == hmTabPage12 && ucAPCRP == null )
                    //{
                    //    ucAPCRP = new ucAPCheckRePrint( Connection, DevXMgr, KCA_Validator );
                    //    pBack.BringToFront();
                    //    ucAPCRP.Parent = hmTabPage12;
                    //    ucAPCRP.Dock = DockStyle.Fill;
                    //    pBack.SendToBack();	
                    //}
                    if ( hmTabControl4.SelectedTabPage == hmTabPage8 && ucAPIA == null )
					{
						ucAPIA = new ucAPInvoiceAccrual( Connection, DevXMgr, KCA_Validator );
						pBack.BringToFront();
						ucAPIA.Parent = hmTabPage8;
						ucAPIA.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
					else if( hmTabControl4.SelectedTabPage == hmTabPage11 && ucAPIA == null )
					{
						ucAPV = new ucAPVoid( Connection, DevXMgr, KCA_Validator );
						pBack.BringToFront();
						ucAPV.Parent = hmTabPage11;
						ucAPV.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
					else if( hmTabControl4.SelectedTabPage == hmTabPage9 && ucAPCS == null )
					{
						ucAPCS = new ChqSelectPA( Connection, DevXMgr, KCA_Validator );
						pBack.BringToFront();
						ucAPCS.Parent = hmTabPage9;
						ucAPCS.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
					else if( hmTabControl4.SelectedTabPage == hmTabPage10 && ucAPRI == null )
					{
						ucAPRI = new ucAPRecuringInvoices( Connection, DevXMgr );
						pBack.BringToFront();
						ucAPRI.Parent = hmTabPage10;
						ucAPRI.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
					else if( hmTabControl4.SelectedTabPage == hmTabPage15 && ucAP1099 == null )
					{
						ucAP1099 = new ucAP1099T5018Print( DevXMgr, Connection );  
						pBack.BringToFront();
						ucAP1099.Parent = hmTabPage15;
						ucAP1099.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
					else if( hmTabControl4.SelectedTabPage == hmTabPage11 && ucAPVoidAssistant == null )
					{
						ucAPVoidAssistant = new ucAPVoid( Connection, DevXMgr, KCA_Validator );
						pBack.BringToFront();
						ucAPVoidAssistant.Parent = hmTabPage11;
						ucAPVoidAssistant.Dock = DockStyle.Fill;
						pBack.SendToBack();
					}
                    else if (hmTabControl4.SelectedTabPage == tpHBRelease && HoldbackRelease == null)
                    {
                        HoldbackRelease = new ucAPHoldbackRelease(Connection, DevXMgr);
                        pBack.BringToFront();
                        HoldbackRelease.Dock = DockStyle.Fill;
                        HoldbackRelease.Parent = tpHBRelease;                        
                        pBack.SendToBack();
                    }
                    else if (hmTabControl4.SelectedTabPage == tpGenerateDEFTFileTransfer)
                    {
                        DeftXferFile = new AP_DEFTXferFile.ucAP_DEFTXferFile(Connection, DevXMgr);
                        pBack.BringToFront();
                        DeftXferFile.Parent = tpGenerateDEFTFileTransfer;
                        DeftXferFile.Dock = DockStyle.Fill;
                        pBack.SendToBack();
                    }
                    else if (hmTabControl4.SelectedTabPage == tpComplianceNotification && ucComplianceNotify == null)
                    {
                        ucComplianceNotify = new AP_ComplianceNotification.ucMain(Connection, DevXMgr);
                        pBack.BringToFront();
                        ucComplianceNotify.Parent = tpComplianceNotification;
                        ucComplianceNotify.Dock = DockStyle.Fill;
                        pBack.SendToBack();
                    }
                    CL_Dialog.PleaseWait.Hide();
                }
			}
		}

		private void FindBottomTabControl( HMTools.HMTabControl TControl )
		{
			if( TControl.TabPages.Count > 0 )
			{
				for( int i = 0; i < TControl.TabPages.Count; i++ )
				{
					bool HasLower = false;
					foreach( Control C in TControl.TabPages[ i ].Controls )
					{
						if( C is HMTools.HMTabControl )
						{
							FindBottomTabControl( C as HMTools.HMTabControl );
							HasLower = true;
							break;
						}
					}
					if( !HasLower )
					{					
						if( TControl.Name == hmTabControl1.Name )
						{
							hmTabControl1_SelectedPageChanged(null,null);
						}
						else if( TControl.Name == hmTabControl2.Name )
						{
							hmTabControl2_SelectedPageChanged(null,null);
						}
						else if( TControl.Name == hmTabControl3.Name )
						{
							hmTabControl3_SelectedPageChanged(null,null);
						}
						else if( TControl.Name == hmTabControl4.Name )
						{
							hmTabControl4_SelectedPageChanged(null,null);
						}
                        else if (TControl.Name == hmTabControl5.Name)
                        {
                            hmTabControl5_SelectedPageChanged(null, null);
                        }
					}
				}
			}			
		}

        private void hmTabControl5_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (hmNavigationBar1.SettingSecurity) return;

            if (hmTabControl5.SelectedTabPage != null)
            {
                if (hmTabControl5.SelectedTabPage.PageVisible)
                {
                    if (e != null && e.Page != null)
                        CL_Dialog.PleaseWait.Show("Loading " + e.Page.Text + "..." + Environment.NewLine + "Please Wait", this.ParentForm);
                    if (hmTabControl5.SelectedTabPage == hmTabPage14 && ucReportLauncher == null)
                    {
                        ucReportLauncher = new ReportLauncher.TUC_HMReportLauncher();
                        ucReportLauncher.ConnectTimeout = 200;
                        pBack.BringToFront();
                        ucReportLauncher.Parent = hmTabPage14;
                        ucReportLauncher.Dock = DockStyle.Fill;

                        string sSelect = "select DBID from DBS where DBName='" + Connection.TRDB + "'";
                        object oDBID = ExecuteScalar(sSelect, new SqlConnection(Connection.WebConnection));
                        if (oDBID != null && oDBID != DBNull.Value)
                        {
                            ucReportLauncher.InitReportLauncher(Connection.WebDB, Connection.WebServer, oDBID.ToString(), "38", "50", "0", Connection.ReportPath,
                                Connection.MLUser, DevXMgr, Connection.CompanyID);                         
                        }
                        pBack.SendToBack();
                    }
                    CL_Dialog.PleaseWait.Hide();
                }
            }
        }


	}
}

