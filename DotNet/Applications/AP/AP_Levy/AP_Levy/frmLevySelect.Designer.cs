namespace AP_Levy
{
    partial class frmLevySelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLevySelect));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.luePurchaser = new DevExpress.XtraEditors.LookUpEdit();
            this.bsPurchaser = new System.Windows.Forms.BindingSource(this.components);
            this.dsPurchaser1 = new AP_Levy.dsPurchaser();
            this.lueLevyType = new DevExpress.XtraEditors.LookUpEdit();
            this.bsLevyType = new System.Windows.Forms.BindingSource(this.components);
            this.dsLevyType1 = new AP_Levy.dsLevyType();
            this.lueLevy = new DevExpress.XtraEditors.LookUpEdit();
            this.bsLevy = new System.Windows.Forms.BindingSource(this.components);
            this.dsLevy1 = new AP_Levy.dsLevy();
            this.cboPlan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboBlock = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboLot = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboAgreement = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboMasterAgreement = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lueProject = new DevExpress.XtraEditors.LookUpEdit();
            this.bsProject = new System.Windows.Forms.BindingSource(this.components);
            this.dsProject1 = new AP_Levy.dsProject();
            this.lueProjNo = new DevExpress.XtraEditors.LookUpEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.gcLevy = new DevExpress.XtraGrid.GridControl();
            this.dsLevySearch1 = new AP_Levy.dsLevySearch();
            this.gvLevy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSelectNone = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daLevySearch = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daProject = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daLevy = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daLevyType = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
            this.daPurchaser = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luePurchaser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPurchaser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPurchaser1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevyType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevyType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevyType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBlock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgreement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterAgreement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProject1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProjNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLevy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevySearch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.ID = new System.Guid("01c36cfa-5c99-491d-b13b-85a2228413df");
            this.dockPanel1.Location = new System.Drawing.Point(959, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(300, 562);
            this.dockPanel1.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.layoutControl1);
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(294, 534);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Controls.Add(this.luePurchaser);
            this.layoutControl1.Controls.Add(this.lueLevyType);
            this.layoutControl1.Controls.Add(this.lueLevy);
            this.layoutControl1.Controls.Add(this.cboPlan);
            this.layoutControl1.Controls.Add(this.cboBlock);
            this.layoutControl1.Controls.Add(this.cboLot);
            this.layoutControl1.Controls.Add(this.cboAgreement);
            this.layoutControl1.Controls.Add(this.cboMasterAgreement);
            this.layoutControl1.Controls.Add(this.lueProject);
            this.layoutControl1.Controls.Add(this.lueProjNo);
            this.layoutControl1.Controls.Add(this.chkActive);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(294, 500);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // luePurchaser
            // 
            this.luePurchaser.Location = new System.Drawing.Point(112, 99);
            this.luePurchaser.Name = "luePurchaser";
            this.luePurchaser.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.luePurchaser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePurchaser.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CUSTOMER_ID", "CUSTOMER_ID", 95, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CUSTOMER_CODE", "Purchaser Code", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Purchaser", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PurchaserType", "Purchaser Type", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.luePurchaser.Properties.DataSource = this.bsPurchaser;
            this.luePurchaser.Properties.DisplayMember = "NAME";
            this.luePurchaser.Properties.NullText = "";
            this.luePurchaser.Properties.PopupWidth = 350;
            this.luePurchaser.Properties.ValueMember = "CUSTOMER_ID";
            this.luePurchaser.Size = new System.Drawing.Size(176, 20);
            this.luePurchaser.StyleController = this.layoutControl1;
            this.luePurchaser.TabIndex = 15;
            // 
            // bsPurchaser
            // 
            this.bsPurchaser.DataMember = "CUSTOMERS";
            this.bsPurchaser.DataSource = this.dsPurchaser1;
            // 
            // dsPurchaser1
            // 
            this.dsPurchaser1.DataSetName = "dsPurchaser";
            this.dsPurchaser1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueLevyType
            // 
            this.lueLevyType.Location = new System.Drawing.Point(112, 316);
            this.lueLevyType.Name = "lueLevyType";
            this.lueLevyType.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueLevyType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLevyType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LevyType_ID", "Levy Type_ID", 90, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Levy Type", 63, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PaidTo", "Paid To", 45, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueLevyType.Properties.DataSource = this.bsLevyType;
            this.lueLevyType.Properties.DisplayMember = "Description";
            this.lueLevyType.Properties.NullText = "";
            this.lueLevyType.Properties.PopupWidth = 300;
            this.lueLevyType.Properties.ValueMember = "LevyType_ID";
            this.lueLevyType.Size = new System.Drawing.Size(176, 20);
            this.lueLevyType.StyleController = this.layoutControl1;
            this.lueLevyType.TabIndex = 14;
            // 
            // bsLevyType
            // 
            this.bsLevyType.DataMember = "LD_LevyType";
            this.bsLevyType.DataSource = this.dsLevyType1;
            // 
            // dsLevyType1
            // 
            this.dsLevyType1.DataSetName = "dsLevyType";
            this.dsLevyType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueLevy
            // 
            this.lueLevy.Location = new System.Drawing.Point(112, 285);
            this.lueLevy.Name = "lueLevy";
            this.lueLevy.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueLevy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLevy.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Levy_ID", "Levy_ID", 63, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Levy", 63, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueLevy.Properties.DataSource = this.bsLevy;
            this.lueLevy.Properties.DisplayMember = "Description";
            this.lueLevy.Properties.NullText = "";
            this.lueLevy.Properties.PopupWidth = 300;
            this.lueLevy.Properties.ValueMember = "Levy_ID";
            this.lueLevy.Size = new System.Drawing.Size(176, 20);
            this.lueLevy.StyleController = this.layoutControl1;
            this.lueLevy.TabIndex = 13;
            // 
            // bsLevy
            // 
            this.bsLevy.DataMember = "LD_Levy";
            this.bsLevy.DataSource = this.dsLevy1;
            // 
            // dsLevy1
            // 
            this.dsLevy1.DataSetName = "dsLevy";
            this.dsLevy1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboPlan
            // 
            this.cboPlan.Location = new System.Drawing.Point(112, 254);
            this.cboPlan.Name = "cboPlan";
            this.cboPlan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboPlan.Properties.MaxLength = 12;
            this.cboPlan.Size = new System.Drawing.Size(176, 20);
            this.cboPlan.StyleController = this.layoutControl1;
            this.cboPlan.TabIndex = 11;
            this.cboPlan.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboPlan_ButtonClick);
            // 
            // cboBlock
            // 
            this.cboBlock.Location = new System.Drawing.Point(112, 223);
            this.cboBlock.Name = "cboBlock";
            this.cboBlock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboBlock.Properties.MaxLength = 5;
            this.cboBlock.Size = new System.Drawing.Size(176, 20);
            this.cboBlock.StyleController = this.layoutControl1;
            this.cboBlock.TabIndex = 10;
            this.cboBlock.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboBlock_ButtonClick);
            // 
            // cboLot
            // 
            this.cboLot.Location = new System.Drawing.Point(112, 192);
            this.cboLot.Name = "cboLot";
            this.cboLot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboLot.Properties.MaxLength = 5;
            this.cboLot.Size = new System.Drawing.Size(176, 20);
            this.cboLot.StyleController = this.layoutControl1;
            this.cboLot.TabIndex = 9;
            this.cboLot.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboLot_ButtonClick);
            // 
            // cboAgreement
            // 
            this.cboAgreement.Location = new System.Drawing.Point(112, 161);
            this.cboAgreement.Name = "cboAgreement";
            this.cboAgreement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboAgreement.Size = new System.Drawing.Size(176, 20);
            this.cboAgreement.StyleController = this.layoutControl1;
            this.cboAgreement.TabIndex = 8;
            this.cboAgreement.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboAgreement_ButtonClick);
            // 
            // cboMasterAgreement
            // 
            this.cboMasterAgreement.Location = new System.Drawing.Point(112, 130);
            this.cboMasterAgreement.Name = "cboMasterAgreement";
            this.cboMasterAgreement.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboMasterAgreement.Size = new System.Drawing.Size(176, 20);
            this.cboMasterAgreement.StyleController = this.layoutControl1;
            this.cboMasterAgreement.TabIndex = 7;
            this.cboMasterAgreement.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboMasterAgreement_ButtonClick);
            // 
            // lueProject
            // 
            this.lueProject.Location = new System.Drawing.Point(112, 68);
            this.lueProject.Name = "lueProject";
            this.lueProject.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProject.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_id", "pri_id", 49, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_code", "Project #", 51, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_name", "Project Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Status", "Status", 41, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueProject.Properties.DataSource = this.bsProject;
            this.lueProject.Properties.DisplayMember = "pri_name";
            this.lueProject.Properties.NullText = "";
            this.lueProject.Properties.PopupWidth = 300;
            this.lueProject.Properties.ValueMember = "pri_id";
            this.lueProject.Size = new System.Drawing.Size(176, 20);
            this.lueProject.StyleController = this.layoutControl1;
            this.lueProject.TabIndex = 6;
            // 
            // bsProject
            // 
            this.bsProject.DataMember = "PROJ_HEADER";
            this.bsProject.DataSource = this.dsProject1;
            // 
            // dsProject1
            // 
            this.dsProject1.DataSetName = "dsProject";
            this.dsProject1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueProjNo
            // 
            this.lueProjNo.Location = new System.Drawing.Point(112, 37);
            this.lueProjNo.Name = "lueProjNo";
            this.lueProjNo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueProjNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProjNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_id", "pri_id", 49, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_code", "Project #", 51, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_name", "Project Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Status", "Status", 41, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueProjNo.Properties.DataSource = this.bsProject;
            this.lueProjNo.Properties.DisplayMember = "pri_code";
            this.lueProjNo.Properties.NullText = "";
            this.lueProjNo.Properties.PopupWidth = 300;
            this.lueProjNo.Properties.ValueMember = "pri_id";
            this.lueProjNo.Size = new System.Drawing.Size(176, 20);
            this.lueProjNo.StyleController = this.layoutControl1;
            this.lueProjNo.TabIndex = 5;
            // 
            // chkActive
            // 
            this.chkActive.Location = new System.Drawing.Point(7, 7);
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Caption = "Active Only";
            this.chkActive.Size = new System.Drawing.Size(281, 19);
            this.chkActive.StyleController = this.layoutControl1;
            this.chkActive.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.emptySpaceItem1,
            this.layoutControlItem12});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(294, 500);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkActive;
            this.layoutControlItem1.CustomizationFormText = "Active Only";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(292, 30);
            this.layoutControlItem1.Text = "Active Only";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lueProjNo;
            this.layoutControlItem2.CustomizationFormText = "Project #";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem2.Text = "Project #";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lueProject;
            this.layoutControlItem3.CustomizationFormText = "Project";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 61);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem3.Text = "Project";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cboMasterAgreement;
            this.layoutControlItem4.CustomizationFormText = "Master Agreement #";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 123);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem4.Text = "Master Agreement #";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cboAgreement;
            this.layoutControlItem5.CustomizationFormText = "Agreement #";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 154);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem5.Text = "Agreement #";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cboLot;
            this.layoutControlItem6.CustomizationFormText = "Lot";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 185);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem6.Text = "Lot";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cboBlock;
            this.layoutControlItem7.CustomizationFormText = "Block";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 216);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem7.Text = "Block";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.cboPlan;
            this.layoutControlItem8.CustomizationFormText = "Plan";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 247);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem8.Text = "Plan";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lueLevy;
            this.layoutControlItem10.CustomizationFormText = "Levy";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 278);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem10.Text = "Levy";
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.lueLevyType;
            this.layoutControlItem11.CustomizationFormText = "Levy Type";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 309);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem11.Text = "Levy Type";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(100, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 340);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(292, 158);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.luePurchaser;
            this.layoutControlItem12.CustomizationFormText = "Purchaser";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(292, 31);
            this.layoutControlItem12.Text = "Purchaser";
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(100, 13);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 500);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(294, 34);
            this.panelControl1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(167, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(86, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Filter";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gcLevy
            // 
            this.gcLevy.DataMember = "AP_LevySelect_Working";
            this.gcLevy.DataSource = this.dsLevySearch1;
            this.gcLevy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLevy.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcLevy.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcLevy.Location = new System.Drawing.Point(0, 32);
            this.gcLevy.MainView = this.gvLevy;
            this.gcLevy.Name = "gcLevy";
            this.gcLevy.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riSelect});
            this.gcLevy.Size = new System.Drawing.Size(959, 530);
            this.gcLevy.TabIndex = 1;
            this.gcLevy.UseEmbeddedNavigator = true;
            this.gcLevy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLevy,
            this.gridView2});
            // 
            // dsLevySearch1
            // 
            this.dsLevySearch1.DataSetName = "dsLevySearch";
            this.dsLevySearch1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvLevy
            // 
            this.gvLevy.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvLevy.GridControl = this.gcLevy;
            this.gvLevy.Name = "gvLevy";
            this.gvLevy.OptionsView.ColumnAutoWidth = false;
            this.gvLevy.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvLevy_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Project #";
            this.gridColumn1.FieldName = "ProjectNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Project";
            this.gridColumn2.FieldName = "Project";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Purchaser";
            this.gridColumn3.FieldName = "Purchaser";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Master Agreement #";
            this.gridColumn4.FieldName = "MasterAgreementNo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Agreement #";
            this.gridColumn5.FieldName = "AgreementNo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Lot";
            this.gridColumn6.FieldName = "Lot";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 37;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Block";
            this.gridColumn7.FieldName = "Block";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Plan";
            this.gridColumn8.FieldName = "Plan";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Levy Description";
            this.gridColumn9.FieldName = "LevyDescription";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Levy Type";
            this.gridColumn10.FieldName = "LevyType";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Amount";
            this.gridColumn11.DisplayFormat.FormatString = "n2";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "Amount";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Select";
            this.gridColumn12.ColumnEdit = this.riSelect;
            this.gridColumn12.FieldName = "selected";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            // 
            // riSelect
            // 
            this.riSelect.AutoHeight = false;
            this.riSelect.Name = "riSelect";
            this.riSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riSelect.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riSelect_EditValueChanging);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcLevy;
            this.gridView2.Name = "gridView2";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnSelectNone);
            this.panelControl2.Controls.Add(this.btnSelectAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(959, 32);
            this.panelControl2.TabIndex = 3;
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(86, 5);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 23);
            this.btnSelectNone.TabIndex = 1;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(5, 5);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 50, "username")});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 0, "username"),
            new System.Data.SqlClient.SqlParameter("@selected", System.Data.SqlDbType.Bit, 0, "selected"),
            new System.Data.SqlClient.SqlParameter("@ProjectNo", System.Data.SqlDbType.Int, 0, "ProjectNo"),
            new System.Data.SqlClient.SqlParameter("@Project", System.Data.SqlDbType.VarChar, 0, "Project"),
            new System.Data.SqlClient.SqlParameter("@Purchaser_ID", System.Data.SqlDbType.Int, 0, "Purchaser_ID"),
            new System.Data.SqlClient.SqlParameter("@Purchaser", System.Data.SqlDbType.VarChar, 0, "Purchaser"),
            new System.Data.SqlClient.SqlParameter("@MasterAgreementNo", System.Data.SqlDbType.Int, 0, "MasterAgreementNo"),
            new System.Data.SqlClient.SqlParameter("@AgreementNo", System.Data.SqlDbType.Int, 0, "AgreementNo"),
            new System.Data.SqlClient.SqlParameter("@Lot", System.Data.SqlDbType.VarChar, 0, "Lot"),
            new System.Data.SqlClient.SqlParameter("@Block", System.Data.SqlDbType.VarChar, 0, "Block"),
            new System.Data.SqlClient.SqlParameter("@Plan", System.Data.SqlDbType.VarChar, 0, "Plan"),
            new System.Data.SqlClient.SqlParameter("@LevyDescription", System.Data.SqlDbType.VarChar, 0, "LevyDescription"),
            new System.Data.SqlClient.SqlParameter("@LevyType", System.Data.SqlDbType.VarChar, 0, "LevyType"),
            new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Amount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, "proj_lot_agreement_levy_id")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 0, "username"),
            new System.Data.SqlClient.SqlParameter("@selected", System.Data.SqlDbType.Bit, 0, "selected"),
            new System.Data.SqlClient.SqlParameter("@ProjectNo", System.Data.SqlDbType.Int, 0, "ProjectNo"),
            new System.Data.SqlClient.SqlParameter("@Project", System.Data.SqlDbType.VarChar, 0, "Project"),
            new System.Data.SqlClient.SqlParameter("@Purchaser_ID", System.Data.SqlDbType.Int, 0, "Purchaser_ID"),
            new System.Data.SqlClient.SqlParameter("@Purchaser", System.Data.SqlDbType.VarChar, 0, "Purchaser"),
            new System.Data.SqlClient.SqlParameter("@MasterAgreementNo", System.Data.SqlDbType.Int, 0, "MasterAgreementNo"),
            new System.Data.SqlClient.SqlParameter("@AgreementNo", System.Data.SqlDbType.Int, 0, "AgreementNo"),
            new System.Data.SqlClient.SqlParameter("@Lot", System.Data.SqlDbType.VarChar, 0, "Lot"),
            new System.Data.SqlClient.SqlParameter("@Block", System.Data.SqlDbType.VarChar, 0, "Block"),
            new System.Data.SqlClient.SqlParameter("@Plan", System.Data.SqlDbType.VarChar, 0, "Plan"),
            new System.Data.SqlClient.SqlParameter("@LevyDescription", System.Data.SqlDbType.VarChar, 0, "LevyDescription"),
            new System.Data.SqlClient.SqlParameter("@LevyType", System.Data.SqlDbType.VarChar, 0, "LevyType"),
            new System.Data.SqlClient.SqlParameter("@Amount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Amount", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, "proj_lot_agreement_levy_id"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_username", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_username", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_selected", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "selected", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_selected", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "selected", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ProjectNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProjectNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ProjectNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ProjectNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Project", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Project", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Project", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Project", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Purchaser_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Purchaser_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Purchaser_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Purchaser_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Purchaser", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Purchaser", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Purchaser", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Purchaser", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MasterAgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MasterAgreementNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MasterAgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MasterAgreementNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AgreementNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AgreementNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Lot", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Lot", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Lot", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Lot", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Block", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Block", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Block", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Block", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Plan", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Plan", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Plan", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Plan", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LevyDescription", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LevyDescription", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LevyDescription", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyDescription", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LevyType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LevyType", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LevyType", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyType", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Amount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Amount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Amount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Amount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "proj_lot_agreement_levy_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "proj_lot_agreement_levy_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_username", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_username", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_selected", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "selected", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_selected", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "selected", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ProjectNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProjectNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ProjectNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ProjectNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Project", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Project", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Project", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Project", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Purchaser_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Purchaser_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Purchaser_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Purchaser_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Purchaser", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Purchaser", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Purchaser", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Purchaser", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MasterAgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MasterAgreementNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MasterAgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MasterAgreementNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AgreementNo", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AgreementNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AgreementNo", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Lot", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Lot", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Lot", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Lot", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Block", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Block", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Block", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Block", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Plan", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Plan", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Plan", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Plan", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LevyDescription", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LevyDescription", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LevyDescription", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyDescription", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LevyType", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LevyType", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LevyType", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyType", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Amount", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Amount", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Amount", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(19)), ((byte)(4)), "Amount", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "proj_lot_agreement_levy_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_proj_lot_agreement_levy_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "proj_lot_agreement_levy_id", System.Data.DataRowVersion.Original, null)});
            // 
            // daLevySearch
            // 
            this.daLevySearch.DeleteCommand = this.sqlDeleteCommand1;
            this.daLevySearch.InsertCommand = this.sqlInsertCommand1;
            this.daLevySearch.SelectCommand = this.sqlSelectCommand1;
            this.daLevySearch.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_LevySelect_Working", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("username", "username"),
                        new System.Data.Common.DataColumnMapping("selected", "selected"),
                        new System.Data.Common.DataColumnMapping("ProjectNo", "ProjectNo"),
                        new System.Data.Common.DataColumnMapping("Project", "Project"),
                        new System.Data.Common.DataColumnMapping("Purchaser_ID", "Purchaser_ID"),
                        new System.Data.Common.DataColumnMapping("Purchaser", "Purchaser"),
                        new System.Data.Common.DataColumnMapping("MasterAgreementNo", "MasterAgreementNo"),
                        new System.Data.Common.DataColumnMapping("AgreementNo", "AgreementNo"),
                        new System.Data.Common.DataColumnMapping("Lot", "Lot"),
                        new System.Data.Common.DataColumnMapping("Block", "Block"),
                        new System.Data.Common.DataColumnMapping("Plan", "Plan"),
                        new System.Data.Common.DataColumnMapping("LevyDescription", "LevyDescription"),
                        new System.Data.Common.DataColumnMapping("LevyType", "LevyType"),
                        new System.Data.Common.DataColumnMapping("Amount", "Amount"),
                        new System.Data.Common.DataColumnMapping("proj_lot_agreement_levy_id", "proj_lot_agreement_levy_id")})});
            this.daLevySearch.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = resources.GetString("sqlSelectCommand2.CommandText");
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code"),
            new System.Data.SqlClient.SqlParameter("@pri_name", System.Data.SqlDbType.VarChar, 0, "pri_name")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code"),
            new System.Data.SqlClient.SqlParameter("@pri_name", System.Data.SqlDbType.VarChar, 0, "pri_name"),
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_name", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4, "pri_id")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_name", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, null)});
            // 
            // daProject
            // 
            this.daProject.DeleteCommand = this.sqlDeleteCommand2;
            this.daProject.InsertCommand = this.sqlInsertCommand2;
            this.daProject.SelectCommand = this.sqlSelectCommand2;
            this.daProject.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PROJ_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("pri_code", "pri_code"),
                        new System.Data.Common.DataColumnMapping("pri_name", "pri_name"),
                        new System.Data.Common.DataColumnMapping("Status", "Status")})});
            this.daProject.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "select Levy_ID, Description from LD_Levy order by Description";
            this.sqlSelectCommand3.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = "INSERT INTO [LD_Levy] ([Description]) VALUES (@Description);\r\nSELECT Levy_ID, Des" +
                "cription FROM LD_Levy WHERE (Levy_ID = SCOPE_IDENTITY()) ORDER BY Description";
            this.sqlInsertCommand3.Connection = this.TR_Conn;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 0, "Description")});
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.TR_Conn;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Original_Levy_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Levy_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Levy_ID", System.Data.SqlDbType.Int, 4, "Levy_ID")});
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = "DELETE FROM [LD_Levy] WHERE (([Levy_ID] = @Original_Levy_ID) AND ((@IsNull_Descri" +
                "ption = 1 AND [Description] IS NULL) OR ([Description] = @Original_Description))" +
                ")";
            this.sqlDeleteCommand3.Connection = this.TR_Conn;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Levy_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Levy_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // daLevy
            // 
            this.daLevy.DeleteCommand = this.sqlDeleteCommand3;
            this.daLevy.InsertCommand = this.sqlInsertCommand3;
            this.daLevy.SelectCommand = this.sqlSelectCommand3;
            this.daLevy.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "LD_Levy", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Levy_ID", "Levy_ID"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            this.daLevy.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "select LevyType_ID, Description, cast(case when PaidTo = \'P\' then \'Purchaser\' els" +
                "e \'Other\' end as varchar(25)) [PaidTo] from LD_LevyType order by Description";
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand4
            // 
            this.sqlInsertCommand4.CommandText = resources.GetString("sqlInsertCommand4.CommandText");
            this.sqlInsertCommand4.Connection = this.TR_Conn;
            this.sqlInsertCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 0, "Description")});
            // 
            // sqlUpdateCommand4
            // 
            this.sqlUpdateCommand4.CommandText = resources.GetString("sqlUpdateCommand4.CommandText");
            this.sqlUpdateCommand4.Connection = this.TR_Conn;
            this.sqlUpdateCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.VarChar, 0, "Description"),
            new System.Data.SqlClient.SqlParameter("@Original_LevyType_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyType_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@LevyType_ID", System.Data.SqlDbType.Int, 4, "LevyType_ID")});
            // 
            // sqlDeleteCommand4
            // 
            this.sqlDeleteCommand4.CommandText = "DELETE FROM [LD_LevyType] WHERE (([LevyType_ID] = @Original_LevyType_ID) AND ((@I" +
                "sNull_Description = 1 AND [Description] IS NULL) OR ([Description] = @Original_D" +
                "escription)))";
            this.sqlDeleteCommand4.Connection = this.TR_Conn;
            this.sqlDeleteCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_LevyType_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LevyType_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Description", System.Data.DataRowVersion.Original, null)});
            // 
            // daLevyType
            // 
            this.daLevyType.DeleteCommand = this.sqlDeleteCommand4;
            this.daLevyType.InsertCommand = this.sqlInsertCommand4;
            this.daLevyType.SelectCommand = this.sqlSelectCommand4;
            this.daLevyType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "LD_LevyType", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("LevyType_ID", "LevyType_ID"),
                        new System.Data.Common.DataColumnMapping("Description", "Description"),
                        new System.Data.Common.DataColumnMapping("PaidTo", "PaidTo")})});
            this.daLevyType.UpdateCommand = this.sqlUpdateCommand4;
            // 
            // sqlSelectCommand5
            // 
            this.sqlSelectCommand5.CommandText = resources.GetString("sqlSelectCommand5.CommandText");
            this.sqlSelectCommand5.Connection = this.TR_Conn;
            // 
            // daPurchaser
            // 
            this.daPurchaser.SelectCommand = this.sqlSelectCommand5;
            this.daPurchaser.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "CUSTOMERS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("CUSTOMER_ID", "CUSTOMER_ID"),
                        new System.Data.Common.DataColumnMapping("CUSTOMER_CODE", "CUSTOMER_CODE"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME"),
                        new System.Data.Common.DataColumnMapping("PurchaserType", "PurchaserType")})});
            // 
            // frmLevySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 562);
            this.Controls.Add(this.gcLevy);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLevySelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Levy Select";
            this.Load += new System.EventHandler(this.frmLevySelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luePurchaser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPurchaser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPurchaser1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevyType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevyType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevyType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLevy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBlock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgreement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterAgreement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProject1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueProjNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLevy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevySearch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcLevy;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLevy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSelectNone;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;
        private DevExpress.XtraEditors.LookUpEdit lueLevyType;
        private DevExpress.XtraEditors.LookUpEdit lueLevy;
        private DevExpress.XtraEditors.ComboBoxEdit cboPlan;
        private DevExpress.XtraEditors.ComboBoxEdit cboBlock;
        private DevExpress.XtraEditors.ComboBoxEdit cboLot;
        private DevExpress.XtraEditors.ComboBoxEdit cboAgreement;
        private DevExpress.XtraEditors.ComboBoxEdit cboMasterAgreement;
        private DevExpress.XtraEditors.LookUpEdit lueProject;
        private DevExpress.XtraEditors.LookUpEdit lueProjNo;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riSelect;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daLevySearch;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daProject;
        private dsProject dsProject1;
        private System.Windows.Forms.BindingSource bsProject;
        private dsLevySearch dsLevySearch1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
        private System.Data.SqlClient.SqlDataAdapter daLevy;
        private dsLevy dsLevy1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand4;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand4;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand4;
        private System.Data.SqlClient.SqlDataAdapter daLevyType;
        private dsLevyType dsLevyType1;
        private System.Windows.Forms.BindingSource bsLevy;
        private System.Windows.Forms.BindingSource bsLevyType;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand5;
        private System.Data.SqlClient.SqlDataAdapter daPurchaser;
        private dsPurchaser dsPurchaser1;
        private DevExpress.XtraEditors.LookUpEdit luePurchaser;
        private System.Windows.Forms.BindingSource bsPurchaser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
    }
}