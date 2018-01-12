namespace AP_SubcontractorCompliance
{
    partial class ucMasterView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMasterView));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lueDesc = new DevExpress.XtraEditors.LookUpEdit();
            this.bsType = new System.Windows.Forms.BindingSource(this.components);
            this.dsType1 = new AP_SubcontractorCompliance.dsType();
            this.lueCode = new DevExpress.XtraEditors.LookUpEdit();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.cboName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboSupp = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.gcSearch = new DevExpress.XtraGrid.GridControl();
            this.dsSearch1 = new AP_SubcontractorCompliance.dsSearch();
            this.gvSearch = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.colSupplier = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riSupp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsSupplier = new System.Windows.Forms.BindingSource(this.components);
            this.dsSupplier1 = new AP_SubcontractorCompliance.dsSupplier();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riTypeCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riTypeDesc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colFrequency = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colExpiry = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colactive = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colpri_id_Code = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riProjectCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsProjects1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsProjects1 = new AP_SubcontractorCompliance.dsProjects();
            this.colpri_id_Desc = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riProjectDesc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsProjects = new System.Windows.Forms.BindingSource(this.components);
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colrule_restrict = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colrule_holdback_release = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colwebrule_internal_alert = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colwebrule_warn_contractor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colwebrule_restrict_payment_req = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riSharepoint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riDirectAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.riReadOnlyBlank = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riCheckBool = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daType = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daSearch = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daInsertUpdate = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daProjects = new System.Data.SqlClient.SqlDataAdapter();
            this.colPO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbandSupplier = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gbandDetails = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gbandProject = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gbandExpiryRules = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gbandWebRules = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gbandDocumentReference = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSearch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSupp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProjectCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProjects1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProjects1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProjectDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSharepoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDirectAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReadOnlyBlank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheckBool)).BeginInit();
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
            this.dockPanel1.ID = new System.Guid("e66a0de3-e00d-4b16-aaa4-b21a98c68dda");
            this.dockPanel1.Location = new System.Drawing.Point(1605, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(283, 626);
            this.dockPanel1.Size = new System.Drawing.Size(283, 626);
            this.dockPanel1.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.layoutControl1);
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(274, 599);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lueDesc);
            this.layoutControl1.Controls.Add(this.lueCode);
            this.layoutControl1.Controls.Add(this.deTo);
            this.layoutControl1.Controls.Add(this.deFrom);
            this.layoutControl1.Controls.Add(this.cboName);
            this.layoutControl1.Controls.Add(this.cboSupp);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(274, 565);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lueDesc
            // 
            this.lueDesc.Location = new System.Drawing.Point(72, 84);
            this.lueDesc.Name = "lueDesc";
            this.lueDesc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDesc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueDesc.Properties.DataSource = this.bsType;
            this.lueDesc.Properties.DisplayMember = "description";
            this.lueDesc.Properties.NullText = "";
            this.lueDesc.Properties.PopupWidth = 250;
            this.lueDesc.Properties.ValueMember = "id";
            this.lueDesc.Size = new System.Drawing.Size(190, 20);
            this.lueDesc.StyleController = this.layoutControl1;
            this.lueDesc.TabIndex = 12;
            this.lueDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // bsType
            // 
            this.bsType.DataMember = "SUPPLIER_SUBCON_INSUR_TYPE_FREQUENCY";
            this.bsType.DataSource = this.dsType1;
            // 
            // dsType1
            // 
            this.dsType1.DataSetName = "dsType";
            this.dsType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueCode
            // 
            this.lueCode.Location = new System.Drawing.Point(72, 60);
            this.lueCode.Name = "lueCode";
            this.lueCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueCode.Properties.DataSource = this.bsType;
            this.lueCode.Properties.DisplayMember = "code";
            this.lueCode.Properties.NullText = "";
            this.lueCode.Properties.PopupWidth = 250;
            this.lueCode.Properties.ValueMember = "id";
            this.lueCode.Size = new System.Drawing.Size(190, 20);
            this.lueCode.StyleController = this.layoutControl1;
            this.lueCode.TabIndex = 11;
            this.lueCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // deTo
            // 
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(84, 162);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deTo.Size = new System.Drawing.Size(166, 20);
            this.deTo.StyleController = this.layoutControl1;
            this.deTo.TabIndex = 8;
            this.deTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // deFrom
            // 
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(84, 138);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFrom.Size = new System.Drawing.Size(166, 20);
            this.deFrom.StyleController = this.layoutControl1;
            this.deFrom.TabIndex = 7;
            this.deFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // cboName
            // 
            this.cboName.Location = new System.Drawing.Point(72, 36);
            this.cboName.Name = "cboName";
            this.cboName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboName.Size = new System.Drawing.Size(190, 20);
            this.cboName.StyleController = this.layoutControl1;
            this.cboName.TabIndex = 5;
            this.cboName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboName_ButtonClick);
            this.cboName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // cboSupp
            // 
            this.cboSupp.Location = new System.Drawing.Point(72, 12);
            this.cboSupp.Name = "cboSupp";
            this.cboSupp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.cboSupp.Size = new System.Drawing.Size(190, 20);
            this.cboSupp.StyleController = this.layoutControl1;
            this.cboSupp.TabIndex = 4;
            this.cboSupp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboSupp_ButtonClick);
            this.cboSupp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlGroup2,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(274, 565);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cboSupp;
            this.layoutControlItem1.CustomizationFormText = "Supplier";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem1.Text = "Supplier";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(57, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cboName;
            this.layoutControlItem2.CustomizationFormText = "Name";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem2.Text = "Name";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(57, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 186);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(254, 359);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Expiry Range";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(254, 90);
            this.layoutControlGroup2.Text = "Expiry Range";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.deTo;
            this.layoutControlItem5.CustomizationFormText = "Expiry To";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItem5.Text = "Expiry To";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(57, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.deFrom;
            this.layoutControlItem4.CustomizationFormText = "Expiry From";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItem4.Text = "Expiry From";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(57, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lueCode;
            this.layoutControlItem8.CustomizationFormText = "Code";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem8.Text = "Code";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(57, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lueDesc;
            this.layoutControlItem9.CustomizationFormText = "Description";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem9.Text = "Description";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(57, 13);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 565);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(274, 34);
            this.panelControl1.TabIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnSearch);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(166, 30);
            this.panelControl2.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(84, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gcSearch
            // 
            this.gcSearch.DataMember = "AP_SUBCON_COMP_SEARCH";
            this.gcSearch.DataSource = this.dsSearch1;
            this.gcSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSearch.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcSearch_EmbeddedNavigator_ButtonClick);
            this.gcSearch.Location = new System.Drawing.Point(0, 0);
            this.gcSearch.MainView = this.gvSearch;
            this.gcSearch.Name = "gcSearch";
            this.gcSearch.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riCheck,
            this.riSharepoint,
            this.riSupp,
            this.riName,
            this.riTypeCode,
            this.riTypeDesc,
            this.riDirectAttachment,
            this.riProjectCode,
            this.riProjectDesc,
            this.riReadOnlyBlank,
            this.riCheckBool});
            this.gcSearch.Size = new System.Drawing.Size(1605, 626);
            this.gcSearch.TabIndex = 1;
            this.gcSearch.UseEmbeddedNavigator = true;
            this.gcSearch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSearch});
            // 
            // dsSearch1
            // 
            this.dsSearch1.DataSetName = "dsSearch";
            this.dsSearch1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvSearch
            // 
            this.gvSearch.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gbandSupplier,
            this.gbandDetails,
            this.gbandProject,
            this.gbandExpiryRules,
            this.gbandWebRules,
            this.gbandDocumentReference});
            this.gvSearch.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSupplier,
            this.bandedGridColumn2,
            this.colCode,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn1,
            this.colExpiry,
            this.bandedGridColumn7,
            this.colrule_restrict,
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.colwebrule_internal_alert,
            this.colwebrule_warn_contractor,
            this.colwebrule_restrict_payment_req,
            this.colpri_id_Code,
            this.colpri_id_Desc,
            this.colactive,
            this.colFrequency,
            this.colrule_holdback_release,
            this.colPO});
            this.gvSearch.GridControl = this.gcSearch;
            this.gvSearch.Name = "gvSearch";
            this.gvSearch.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvSearch.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvSearch_CustomRowCellEdit);
            this.gvSearch.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvSearch_InitNewRow);
            this.gvSearch.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvSearch_InvalidRowException);
            this.gvSearch.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvSearch_ValidateRow);
            this.gvSearch.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvSearch_RowUpdated);
            // 
            // colSupplier
            // 
            this.colSupplier.Caption = "Supplier";
            this.colSupplier.ColumnEdit = this.riSupp;
            this.colSupplier.FieldName = "supplier_id";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.Visible = true;
            this.colSupplier.Width = 111;
            // 
            // riSupp
            // 
            this.riSupp.AutoHeight = false;
            this.riSupp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSupp.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER_ID", "SUPPLIER_ID", 87, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riSupp.DataSource = this.bsSupplier;
            this.riSupp.DisplayMember = "SUPPLIER";
            this.riSupp.Name = "riSupp";
            this.riSupp.NullText = "";
            this.riSupp.PopupWidth = 250;
            this.riSupp.ValueMember = "SUPPLIER_ID";
            // 
            // bsSupplier
            // 
            this.bsSupplier.DataMember = "SUPPLIER_MASTER";
            this.bsSupplier.DataSource = this.dsSupplier1;
            // 
            // dsSupplier1
            // 
            this.dsSupplier1.DataSetName = "dsSupplier";
            this.dsSupplier1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Name";
            this.bandedGridColumn2.ColumnEdit = this.riName;
            this.bandedGridColumn2.FieldName = "supplier_id";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 205;
            // 
            // riName
            // 
            this.riName.AutoHeight = false;
            this.riName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riName.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER_ID", "SUPPLIER_ID", 87, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riName.DataSource = this.bsSupplier;
            this.riName.DisplayMember = "NAME";
            this.riName.Name = "riName";
            this.riName.NullText = "";
            this.riName.PopupWidth = 250;
            this.riName.ValueMember = "SUPPLIER_ID";
            // 
            // colCode
            // 
            this.colCode.Caption = "Code";
            this.colCode.ColumnEdit = this.riTypeCode;
            this.colCode.FieldName = "insur_type_id";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.Width = 92;
            // 
            // riTypeCode
            // 
            this.riTypeCode.AutoHeight = false;
            this.riTypeCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTypeCode.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riTypeCode.DataSource = this.bsType;
            this.riTypeCode.DisplayMember = "code";
            this.riTypeCode.Name = "riTypeCode";
            this.riTypeCode.NullText = "";
            this.riTypeCode.PopupWidth = 250;
            this.riTypeCode.ValueMember = "id";
            this.riTypeCode.EditValueChanged += new System.EventHandler(this.riTypeCode_EditValueChanged);
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "Description";
            this.bandedGridColumn4.ColumnEdit = this.riTypeDesc;
            this.bandedGridColumn4.FieldName = "insur_type_id";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 187;
            // 
            // riTypeDesc
            // 
            this.riTypeDesc.AutoHeight = false;
            this.riTypeDesc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTypeDesc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riTypeDesc.DataSource = this.bsType;
            this.riTypeDesc.DisplayMember = "description";
            this.riTypeDesc.Name = "riTypeDesc";
            this.riTypeDesc.NullText = "";
            this.riTypeDesc.PopupWidth = 250;
            this.riTypeDesc.ValueMember = "id";
            this.riTypeDesc.EditValueChanged += new System.EventHandler(this.riTypeDesc_EditValueChanged);
            // 
            // colFrequency
            // 
            this.colFrequency.FieldName = "Frequency";
            this.colFrequency.Name = "colFrequency";
            this.colFrequency.OptionsColumn.AllowEdit = false;
            this.colFrequency.OptionsColumn.ReadOnly = true;
            this.colFrequency.Visible = true;
            this.colFrequency.Width = 110;
            // 
            // colExpiry
            // 
            this.colExpiry.Caption = "Expiry";
            this.colExpiry.FieldName = "expiry_date";
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.Visible = true;
            this.colExpiry.Width = 112;
            // 
            // colactive
            // 
            this.colactive.Caption = "Complete";
            this.colactive.FieldName = "active";
            this.colactive.Name = "colactive";
            this.colactive.Visible = true;
            this.colactive.Width = 67;
            // 
            // colpri_id_Code
            // 
            this.colpri_id_Code.Caption = "Code";
            this.colpri_id_Code.ColumnEdit = this.riProjectCode;
            this.colpri_id_Code.FieldName = "pri_id";
            this.colpri_id_Code.Name = "colpri_id_Code";
            this.colpri_id_Code.Visible = true;
            this.colpri_id_Code.Width = 123;
            // 
            // riProjectCode
            // 
            this.riProjectCode.AutoHeight = false;
            this.riProjectCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riProjectCode.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_code", "Code", 75, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riProjectCode.DataSource = this.bsProjects1;
            this.riProjectCode.DisplayMember = "pri_code";
            this.riProjectCode.Name = "riProjectCode";
            this.riProjectCode.NullText = "";
            this.riProjectCode.PopupWidth = 250;
            this.riProjectCode.ValueMember = "pri_id";
            // 
            // bsProjects1
            // 
            this.bsProjects1.DataMember = "PROJ_HEADER";
            this.bsProjects1.DataSource = this.dsProjects1;
            // 
            // dsProjects1
            // 
            this.dsProjects1.DataSetName = "dsProjects";
            this.dsProjects1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colpri_id_Desc
            // 
            this.colpri_id_Desc.Caption = "Description";
            this.colpri_id_Desc.ColumnEdit = this.riProjectDesc;
            this.colpri_id_Desc.FieldName = "pri_id";
            this.colpri_id_Desc.Name = "colpri_id_Desc";
            this.colpri_id_Desc.Visible = true;
            this.colpri_id_Desc.Width = 123;
            // 
            // riProjectDesc
            // 
            this.riProjectDesc.AutoHeight = false;
            this.riProjectDesc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riProjectDesc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pri_code", "Code", 75, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riProjectDesc.DataSource = this.bsProjects;
            this.riProjectDesc.DisplayMember = "Desc";
            this.riProjectDesc.Name = "riProjectDesc";
            this.riProjectDesc.NullText = "";
            this.riProjectDesc.PopupWidth = 250;
            this.riProjectDesc.ValueMember = "pri_id";
            // 
            // bsProjects
            // 
            this.bsProjects.DataMember = "PROJ_HEADER";
            this.bsProjects.DataSource = this.dsProjects1;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.Caption = "Warning Message";
            this.bandedGridColumn7.ColumnEdit = this.riCheck;
            this.bandedGridColumn7.FieldName = "rule_warning";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 109;
            // 
            // riCheck
            // 
            this.riCheck.AutoHeight = false;
            this.riCheck.Name = "riCheck";
            this.riCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riCheck.ValueChecked = "T";
            this.riCheck.ValueUnchecked = "F";
            // 
            // colrule_restrict
            // 
            this.colrule_restrict.Caption = "Restrict";
            this.colrule_restrict.ColumnEdit = this.riCheck;
            this.colrule_restrict.FieldName = "rule_restrict";
            this.colrule_restrict.Name = "colrule_restrict";
            this.colrule_restrict.Visible = true;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.Caption = "Pre-Accrual";
            this.bandedGridColumn8.ColumnEdit = this.riCheck;
            this.bandedGridColumn8.FieldName = "rule_accrual";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 80;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.Caption = "Payment Hold";
            this.bandedGridColumn9.ColumnEdit = this.riCheck;
            this.bandedGridColumn9.FieldName = "rule_payment";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 96;
            // 
            // colrule_holdback_release
            // 
            this.colrule_holdback_release.Caption = "Holdback Release";
            this.colrule_holdback_release.ColumnEdit = this.riCheck;
            this.colrule_holdback_release.FieldName = "rule_holdback_release";
            this.colrule_holdback_release.Name = "colrule_holdback_release";
            this.colrule_holdback_release.Visible = true;
            this.colrule_holdback_release.Width = 106;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.Caption = "PO Approval";
            this.bandedGridColumn10.ColumnEdit = this.riCheck;
            this.bandedGridColumn10.FieldName = "rule_po";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 84;
            // 
            // colwebrule_internal_alert
            // 
            this.colwebrule_internal_alert.Caption = "Internal Alert";
            this.colwebrule_internal_alert.ColumnEdit = this.riCheck;
            this.colwebrule_internal_alert.FieldName = "webrule_internal_alert";
            this.colwebrule_internal_alert.Name = "colwebrule_internal_alert";
            this.colwebrule_internal_alert.Visible = true;
            this.colwebrule_internal_alert.Width = 118;
            // 
            // colwebrule_warn_contractor
            // 
            this.colwebrule_warn_contractor.Caption = "Warn Contractor";
            this.colwebrule_warn_contractor.ColumnEdit = this.riCheck;
            this.colwebrule_warn_contractor.FieldName = "webrule_warn_contractor";
            this.colwebrule_warn_contractor.Name = "colwebrule_warn_contractor";
            this.colwebrule_warn_contractor.Visible = true;
            this.colwebrule_warn_contractor.Width = 118;
            // 
            // colwebrule_restrict_payment_req
            // 
            this.colwebrule_restrict_payment_req.Caption = "Restrict Payment Request";
            this.colwebrule_restrict_payment_req.ColumnEdit = this.riCheck;
            this.colwebrule_restrict_payment_req.FieldName = "webrule_restrict_payment_req";
            this.colwebrule_restrict_payment_req.Name = "colwebrule_restrict_payment_req";
            this.colwebrule_restrict_payment_req.Visible = true;
            this.colwebrule_restrict_payment_req.Width = 147;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.Caption = "Sharepoint";
            this.bandedGridColumn5.ColumnEdit = this.riSharepoint;
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 150;
            // 
            // riSharepoint
            // 
            this.riSharepoint.AutoHeight = false;
            this.riSharepoint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Sharepoint", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, ((System.Drawing.Image)(resources.GetObject("riSharepoint.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, false)});
            this.riSharepoint.Name = "riSharepoint";
            this.riSharepoint.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riSharepoint.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riSharepoint_ButtonClick);
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Direct Attachment";
            this.bandedGridColumn1.ColumnEdit = this.riDirectAttachment;
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 150;
            // 
            // riDirectAttachment
            // 
            this.riDirectAttachment.AutoHeight = false;
            this.riDirectAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Direct Attachment", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, false)});
            this.riDirectAttachment.Name = "riDirectAttachment";
            this.riDirectAttachment.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riDirectAttachment.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riDirectAttachment_ButtonClick);
            // 
            // riReadOnlyBlank
            // 
            this.riReadOnlyBlank.AutoHeight = false;
            this.riReadOnlyBlank.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, false)});
            this.riReadOnlyBlank.Name = "riReadOnlyBlank";
            this.riReadOnlyBlank.NullText = "";
            this.riReadOnlyBlank.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            // 
            // riCheckBool
            // 
            this.riCheckBool.AutoHeight = false;
            this.riCheckBool.Name = "riCheckBool";
            this.riCheckBool.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select SUPPLIER_ID, SUPPLIER, NAME from SUPPLIER_MASTER where ISNULL(subcontracto" +
    "r,\'F\') = \'T\'";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=csmsql2016;Initial Catalog=TR_KELLER_DEV;Persist Security Info=True;U" +
    "ser ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [SUPPLIER_MASTER] ([SUPPLIER_ID], [SUPPLIER], [NAME]) VALUES (@SUPPLI" +
    "ER_ID, @SUPPLIER, @NAME);\r\nSELECT SUPPLIER_ID, SUPPLIER, NAME FROM SUPPLIER_MAST" +
    "ER WHERE (SUPPLIER = @SUPPLIER)";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlDeleteCommand1;
            this.daSupplier.InsertCommand = this.sqlInsertCommand1;
            this.daSupplier.SelectCommand = this.sqlSelectCommand1;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER_ID", "SUPPLIER_ID"),
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daSupplier.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = resources.GetString("sqlSelectCommand2.CommandText");
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@code", System.Data.SqlDbType.VarChar, 0, "code"),
            new System.Data.SqlClient.SqlParameter("@description", System.Data.SqlDbType.VarChar, 0, "description"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_code", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_code", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, null)});
            // 
            // daType
            // 
            this.daType.DeleteCommand = this.sqlDeleteCommand2;
            this.daType.SelectCommand = this.sqlSelectCommand2;
            this.daType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_SUBCON_INSUR_TYPE_FREQUENCY", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("code", "code"),
                        new System.Data.Common.DataColumnMapping("description", "description"),
                        new System.Data.Common.DataColumnMapping("frequency_id", "frequency_id"),
                        new System.Data.Common.DataColumnMapping("project_Required", "project_Required"),
                        new System.Data.Common.DataColumnMapping("Frequency", "Frequency")})});
            this.daType.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "dbo.AP_SUBCON_COMP_SEARCH";
            this.sqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand3.Connection = this.TR_Conn;
            this.sqlSelectCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@supplier_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@supplier", System.Data.SqlDbType.VarChar, 10),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.VarChar, 40),
            new System.Data.SqlClient.SqlParameter("@insur_type_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@insur_type_id2", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@expiry_from", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@expiry_to", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4)});
            // 
            // daSearch
            // 
            this.daSearch.SelectCommand = this.sqlSelectCommand3;
            this.daSearch.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SUBCON_COMP_SEARCH", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("supplier_id", "supplier_id"),
                        new System.Data.Common.DataColumnMapping("insur_type_id", "insur_type_id"),
                        new System.Data.Common.DataColumnMapping("expiry_date", "expiry_date"),
                        new System.Data.Common.DataColumnMapping("rule_warning", "rule_warning"),
                        new System.Data.Common.DataColumnMapping("rule_accrual", "rule_accrual"),
                        new System.Data.Common.DataColumnMapping("rule_payment", "rule_payment"),
                        new System.Data.Common.DataColumnMapping("rule_po", "rule_po"),
                        new System.Data.Common.DataColumnMapping("webrule_internal_alert", "webrule_internal_alert"),
                        new System.Data.Common.DataColumnMapping("webrule_warn_contractor", "webrule_warn_contractor"),
                        new System.Data.Common.DataColumnMapping("webrule_restrict_payment_req", "webrule_restrict_payment_req"),
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("active", "active"),
                        new System.Data.Common.DataColumnMapping("project_required", "project_required"),
                        new System.Data.Common.DataColumnMapping("Frequency", "Frequency"),
                        new System.Data.Common.DataColumnMapping("rule_restrict", "rule_restrict"),
                        new System.Data.Common.DataColumnMapping("rule_holdback_release", "rule_holdback_release"),
                        new System.Data.Common.DataColumnMapping("PO", "PO")})});
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "dbo.AP_SUBCON_COMP_IU";
            this.sqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            this.sqlSelectCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@supplier_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@insur_type_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@expiry_date", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@rule_warning", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_accrual", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_payment", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_po", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_internal_alert", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_warn_contractor", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_restrict_payment_req", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@active", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@rule_restrict", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_holdback_release", System.Data.SqlDbType.VarChar, 1)});
            // 
            // daInsertUpdate
            // 
            this.daInsertUpdate.SelectCommand = this.sqlSelectCommand4;
            this.daInsertUpdate.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SUBCON_COMP_IU", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id")})});
            // 
            // sqlSelectCommand5
            // 
            this.sqlSelectCommand5.CommandText = "SELECT        pri_id, pri_code, ISNULL(pri_desc1, pri_name) AS [Desc]\r\nFROM      " +
    "      PROJ_HEADER where pri_type = \'PGC\'\r\norder by pri_code";
            this.sqlSelectCommand5.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = "INSERT INTO [PROJ_HEADER] ([pri_code]) VALUES (@pri_code);\r\nSELECT pri_id, pri_co" +
    "de, ISNULL(pri_desc1, pri_name) AS [Desc] FROM PROJ_HEADER WHERE (pri_id = SCOPE" +
    "_IDENTITY()) ORDER BY pri_code";
            this.sqlInsertCommand3.Connection = this.TR_Conn;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code")});
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.TR_Conn;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code"),
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4, "pri_id")});
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = "DELETE FROM [PROJ_HEADER] WHERE (([pri_id] = @Original_pri_id) AND ((@IsNull_pri_" +
    "code = 1 AND [pri_code] IS NULL) OR ([pri_code] = @Original_pri_code)))";
            this.sqlDeleteCommand3.Connection = this.TR_Conn;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null)});
            // 
            // daProjects
            // 
            this.daProjects.DeleteCommand = this.sqlDeleteCommand3;
            this.daProjects.InsertCommand = this.sqlInsertCommand3;
            this.daProjects.SelectCommand = this.sqlSelectCommand5;
            this.daProjects.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PROJ_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("pri_code", "pri_code"),
                        new System.Data.Common.DataColumnMapping("Desc", "Desc")})});
            this.daProjects.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // colPO
            // 
            this.colPO.FieldName = "PO";
            this.colPO.Name = "colPO";
            this.colPO.OptionsColumn.AllowEdit = false;
            this.colPO.Visible = true;
            // 
            // gbandSupplier
            // 
            this.gbandSupplier.Caption = "Supplier";
            this.gbandSupplier.Columns.Add(this.colSupplier);
            this.gbandSupplier.Columns.Add(this.bandedGridColumn2);
            this.gbandSupplier.MinWidth = 20;
            this.gbandSupplier.Name = "gbandSupplier";
            this.gbandSupplier.VisibleIndex = 0;
            this.gbandSupplier.Width = 316;
            // 
            // gbandDetails
            // 
            this.gbandDetails.Caption = "Details";
            this.gbandDetails.Columns.Add(this.colCode);
            this.gbandDetails.Columns.Add(this.bandedGridColumn4);
            this.gbandDetails.Columns.Add(this.colPO);
            this.gbandDetails.Columns.Add(this.colFrequency);
            this.gbandDetails.Columns.Add(this.colExpiry);
            this.gbandDetails.Columns.Add(this.colactive);
            this.gbandDetails.MinWidth = 20;
            this.gbandDetails.Name = "gbandDetails";
            this.gbandDetails.VisibleIndex = 1;
            this.gbandDetails.Width = 643;
            // 
            // gbandProject
            // 
            this.gbandProject.Caption = "Project";
            this.gbandProject.Columns.Add(this.colpri_id_Code);
            this.gbandProject.Columns.Add(this.colpri_id_Desc);
            this.gbandProject.MinWidth = 20;
            this.gbandProject.Name = "gbandProject";
            this.gbandProject.VisibleIndex = 2;
            this.gbandProject.Width = 246;
            // 
            // gbandExpiryRules
            // 
            this.gbandExpiryRules.Caption = "Expiry Rules";
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn7);
            this.gbandExpiryRules.Columns.Add(this.colrule_restrict);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn8);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn9);
            this.gbandExpiryRules.Columns.Add(this.colrule_holdback_release);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn10);
            this.gbandExpiryRules.MinWidth = 20;
            this.gbandExpiryRules.Name = "gbandExpiryRules";
            this.gbandExpiryRules.VisibleIndex = 3;
            this.gbandExpiryRules.Width = 550;
            // 
            // gbandWebRules
            // 
            this.gbandWebRules.Caption = "Web Rules";
            this.gbandWebRules.Columns.Add(this.colwebrule_internal_alert);
            this.gbandWebRules.Columns.Add(this.colwebrule_warn_contractor);
            this.gbandWebRules.Columns.Add(this.colwebrule_restrict_payment_req);
            this.gbandWebRules.MinWidth = 20;
            this.gbandWebRules.Name = "gbandWebRules";
            this.gbandWebRules.VisibleIndex = 4;
            this.gbandWebRules.Width = 383;
            // 
            // gbandDocumentReference
            // 
            this.gbandDocumentReference.Caption = "Document Reference";
            this.gbandDocumentReference.Columns.Add(this.bandedGridColumn5);
            this.gbandDocumentReference.Columns.Add(this.bandedGridColumn1);
            this.gbandDocumentReference.MinWidth = 20;
            this.gbandDocumentReference.Name = "gbandDocumentReference";
            this.gbandDocumentReference.VisibleIndex = 5;
            this.gbandDocumentReference.Width = 300;
            // 
            // ucMasterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcSearch);
            this.Controls.Add(this.dockPanel1);
            this.Name = "ucMasterView";
            this.Size = new System.Drawing.Size(1888, 626);
            this.Load += new System.EventHandler(this.ucMasterView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSupp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSearch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSupp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProjectCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProjects1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProjects1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProjectDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSharepoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDirectAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReadOnlyBlank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheckBool)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraGrid.GridControl gcSearch;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gvSearch;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSupplier;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colExpiry;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riCheck;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit deTo;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private DevExpress.XtraEditors.ComboBoxEdit cboName;
        private DevExpress.XtraEditors.ComboBoxEdit cboSupp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riSharepoint;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daSupplier;
        private dsSupplier dsSupplier1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daType;
        private dsType dsType1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSupp;
        private System.Windows.Forms.BindingSource bsSupplier;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riTypeCode;
        private System.Windows.Forms.BindingSource bsType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riTypeDesc;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlDataAdapter daSearch;
        private DevExpress.XtraEditors.LookUpEdit lueDesc;
        private DevExpress.XtraEditors.LookUpEdit lueCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
        private System.Data.SqlClient.SqlDataAdapter daInsertUpdate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riDirectAttachment;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_internal_alert;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_warn_contractor;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_restrict_payment_req;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand5;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
        private System.Data.SqlClient.SqlDataAdapter daProjects;
        private dsProjects dsProjects1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colpri_id_Code;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colpri_id_Desc;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colactive;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riProjectCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riProjectDesc;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riReadOnlyBlank;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFrequency;
        private System.Windows.Forms.BindingSource bsProjects;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riCheckBool;
        private System.Windows.Forms.BindingSource bsProjects1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colrule_restrict;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colrule_holdback_release;
        private dsSearch dsSearch1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandSupplier;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPO;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandProject;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandExpiryRules;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandWebRules;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandDocumentReference;
    }
}
