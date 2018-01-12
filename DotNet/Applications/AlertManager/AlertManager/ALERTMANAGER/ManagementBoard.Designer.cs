namespace AlertManager
{
    partial class ManagementBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagementBoard));
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.colRead = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riRead = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.colCompleted = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riImages = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerRight = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcStakeholder = new DevExpress.XtraGrid.GridControl();
            this.dsStakeholders1 = new AlertManager.dsStakeholders();
            this.gvStakeholder = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riContact = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.contactBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsContacts1 = new AlertManager.dsContacts();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.lueAlertType = new DevExpress.XtraEditors.LookUpEdit();
            this.applicationAlertPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsAlertType1 = new AlertManager.dsAlertType();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.gAlert = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDate = new System.Windows.Forms.ToolStripMenuItem();
            this.miModule = new System.Windows.Forms.ToolStripMenuItem();
            this.miType = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.miCollapse = new System.Windows.Forms.ToolStripMenuItem();
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lblArrange = new DevExpress.XtraEditors.LabelControl();
            this.gcAlerts = new DevExpress.XtraGrid.GridControl();
            this.dsAlert1 = new AlertManager.dsAlert();
            this.gvAlerts = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riDateFormat = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSubject = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.alertView1 = new AlertManager.AlertView();
            this.pBack = new DevExpress.XtraEditors.PanelControl();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daAlert = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daStakeholders = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.WEB_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daContacts = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daAlertType = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.riRead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerRight.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStakeholders1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riContact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsContacts1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAlertType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationAlertPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gAlert)).BeginInit();
            this.gAlert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlert1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).BeginInit();
            this.SuspendLayout();
            // 
            // colRead
            // 
            this.colRead.Caption = " ";
            this.colRead.ColumnEdit = this.riRead;
            this.colRead.FieldName = "VIEWED";
            this.colRead.Name = "colRead";
            this.colRead.Visible = true;
            this.colRead.Width = 30;
            // 
            // riRead
            // 
            this.riRead.AutoHeight = false;
            this.riRead.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riRead.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "F", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "T", 3)});
            this.riRead.Name = "riRead";
            this.riRead.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Lightning 1 16 h p.png");
            this.imageCollection1.Images.SetKeyName(1, "Stopwatch 16 h p.png");
            this.imageCollection1.Images.SetKeyName(2, "Light Orange Round 16 h p.png");
            this.imageCollection1.Images.SetKeyName(3, "Light Grey Round 16 h p.png");
            // 
            // colCompleted
            // 
            this.colCompleted.Caption = "Completed";
            this.colCompleted.FieldName = "COMPLETED_TF";
            this.colCompleted.Name = "colCompleted";
            this.colCompleted.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colType
            // 
            this.colType.Caption = " ";
            this.colType.ColumnEdit = this.riImages;
            this.colType.FieldName = "TYPE";
            this.colType.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
            this.colType.Name = "colType";
            this.colType.RowIndex = 1;
            this.colType.Visible = true;
            this.colType.Width = 40;
            // 
            // riImages
            // 
            this.riImages.AutoHeight = false;
            this.riImages.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riImages.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "D", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "S", 1)});
            this.riImages.Name = "riImages";
            this.riImages.SmallImages = this.imageCollection1;
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerRight});
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerRight
            // 
            this.hideContainerRight.Controls.Add(this.dockPanel1);
            this.hideContainerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.hideContainerRight.Location = new System.Drawing.Point(1151, 0);
            this.hideContainerRight.Name = "hideContainerRight";
            this.hideContainerRight.Size = new System.Drawing.Size(19, 710);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.ID = new System.Guid("5f2ec194-c8f7-4280-ba7a-1bfb3b929164");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(272, 710);
            this.dockPanel1.Text = "Info";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.groupControl1);
            this.dockPanel1_Container.Controls.Add(this.groupControl2);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(266, 682);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcStakeholder);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(266, 488);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Stakeholders";
            // 
            // gcStakeholder
            // 
            this.gcStakeholder.DataMember = "AlertStakeholder";
            this.gcStakeholder.DataSource = this.dsStakeholders1;
            this.gcStakeholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStakeholder.Location = new System.Drawing.Point(2, 20);
            this.gcStakeholder.MainView = this.gvStakeholder;
            this.gcStakeholder.Name = "gcStakeholder";
            this.gcStakeholder.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riContact});
            this.gcStakeholder.Size = new System.Drawing.Size(262, 466);
            this.gcStakeholder.TabIndex = 0;
            this.gcStakeholder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStakeholder});
            // 
            // dsStakeholders1
            // 
            this.dsStakeholders1.DataSetName = "dsStakeholders";
            this.dsStakeholders1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvStakeholder
            // 
            this.gvStakeholder.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gvStakeholder.GridControl = this.gcStakeholder;
            this.gvStakeholder.Name = "gvStakeholder";
            this.gvStakeholder.OptionsBehavior.Editable = false;
            this.gvStakeholder.OptionsView.ShowColumnHeaders = false;
            this.gvStakeholder.OptionsView.ShowGroupPanel = false;
            this.gvStakeholder.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.riContact;
            this.gridColumn1.FieldName = "contact_id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // riContact
            // 
            this.riContact.AutoHeight = false;
            this.riContact.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riContact.DataSource = this.contactBindingSource;
            this.riContact.DisplayMember = "Contact";
            this.riContact.Name = "riContact";
            this.riContact.NullText = "";
            this.riContact.ValueMember = "ID";
            // 
            // contactBindingSource
            // 
            this.contactBindingSource.DataMember = "Contact";
            this.contactBindingSource.DataSource = this.dsContacts1;
            // 
            // dsContacts1
            // 
            this.dsContacts1.DataSetName = "dsContacts";
            this.dsContacts1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.layoutControl1);
            this.groupControl2.Controls.Add(this.panelControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 488);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(266, 194);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Find";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Controls.Add(this.dtTo);
            this.layoutControl1.Controls.Add(this.dtFrom);
            this.layoutControl1.Controls.Add(this.txtSubject);
            this.layoutControl1.Controls.Add(this.lueAlertType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 20);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(262, 130);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(83, 100);
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtTo.Size = new System.Drawing.Size(173, 20);
            this.dtTo.StyleController = this.layoutControl1;
            this.dtTo.TabIndex = 7;
            this.dtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(83, 69);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtFrom.Size = new System.Drawing.Size(173, 20);
            this.dtFrom.StyleController = this.layoutControl1;
            this.dtFrom.TabIndex = 6;
            this.dtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(83, 38);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(173, 20);
            this.txtSubject.StyleController = this.layoutControl1;
            this.txtSubject.TabIndex = 5;
            this.txtSubject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // lueAlertType
            // 
            this.lueAlertType.Location = new System.Drawing.Point(83, 7);
            this.lueAlertType.Name = "lueAlertType";
            this.lueAlertType.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueAlertType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAlertType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 34, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ALERT", "Alert Type", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ALERT_POINT", "Sub Module", 78, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayName", "Module", 74, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueAlertType.Properties.DataSource = this.applicationAlertPointBindingSource;
            this.lueAlertType.Properties.DisplayMember = "ALERT";
            this.lueAlertType.Properties.NullText = "";
            this.lueAlertType.Properties.PopupWidth = 450;
            this.lueAlertType.Properties.ValueMember = "ID";
            this.lueAlertType.Size = new System.Drawing.Size(173, 20);
            this.lueAlertType.StyleController = this.layoutControl1;
            this.lueAlertType.TabIndex = 4;
            this.lueAlertType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_KeyDown);
            // 
            // applicationAlertPointBindingSource
            // 
            this.applicationAlertPointBindingSource.DataMember = "ApplicationAlertPoint";
            this.applicationAlertPointBindingSource.DataSource = this.dsAlertType1;
            // 
            // dsAlertType1
            // 
            this.dsAlertType1.DataSetName = "dsAlertType";
            this.dsAlertType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(262, 130);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lueAlertType;
            this.layoutControlItem1.CustomizationFormText = "Module";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(260, 31);
            this.layoutControlItem1.Text = "Alert Type";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtSubject;
            this.layoutControlItem2.CustomizationFormText = "Subject";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(260, 31);
            this.layoutControlItem2.Text = "Subject";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dtFrom;
            this.layoutControlItem3.CustomizationFormText = "Received From";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 62);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(260, 31);
            this.layoutControlItem3.Text = "Received From";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtTo;
            this.layoutControlItem4.CustomizationFormText = "Received To";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(260, 35);
            this.layoutControlItem4.Text = "Received To";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(71, 13);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl4);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 150);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(262, 42);
            this.panelControl3.TabIndex = 1;
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.btnClear);
            this.panelControl4.Controls.Add(this.btnSearch);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl4.Location = new System.Drawing.Point(2, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(200, 38);
            this.panelControl4.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(84, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(3, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gAlert
            // 
            this.gAlert.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gAlert.AppearanceCaption.Options.UseFont = true;
            this.gAlert.Controls.Add(this.panelControl2);
            this.gAlert.Controls.Add(this.gcAlerts);
            this.gAlert.Dock = System.Windows.Forms.DockStyle.Left;
            this.gAlert.Location = new System.Drawing.Point(0, 0);
            this.gAlert.Name = "gAlert";
            this.gAlert.Size = new System.Drawing.Size(437, 710);
            this.gAlert.TabIndex = 2;
            this.gAlert.Text = "Alerts";
            // 
            // panelControl2
            // 
            this.panelControl2.ContextMenuStrip = this.contextMenuStrip1;
            this.panelControl2.Controls.Add(this.btnRefresh);
            this.panelControl2.Controls.Add(this.lblArrange);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 26);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(433, 21);
            this.panelControl2.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDate,
            this.miModule,
            this.miType,
            this.toolStripSeparator1,
            this.miExpand,
            this.miCollapse,
            this.miRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 142);
            // 
            // miDate
            // 
            this.miDate.Checked = true;
            this.miDate.CheckOnClick = true;
            this.miDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miDate.Name = "miDate";
            this.miDate.Size = new System.Drawing.Size(136, 22);
            this.miDate.Text = "Date";
            this.miDate.Click += new System.EventHandler(this.miDate_Click);
            // 
            // miModule
            // 
            this.miModule.CheckOnClick = true;
            this.miModule.Name = "miModule";
            this.miModule.Size = new System.Drawing.Size(136, 22);
            this.miModule.Text = "Module";
            this.miModule.Click += new System.EventHandler(this.miModule_Click);
            // 
            // miType
            // 
            this.miType.CheckOnClick = true;
            this.miType.Name = "miType";
            this.miType.Size = new System.Drawing.Size(136, 22);
            this.miType.Text = "Type";
            this.miType.Visible = false;
            this.miType.Click += new System.EventHandler(this.miType_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // miExpand
            // 
            this.miExpand.Name = "miExpand";
            this.miExpand.Size = new System.Drawing.Size(136, 22);
            this.miExpand.Text = "Expand All";
            this.miExpand.Click += new System.EventHandler(this.miExpand_Click);
            // 
            // miCollapse
            // 
            this.miCollapse.Name = "miCollapse";
            this.miCollapse.Size = new System.Drawing.Size(136, 22);
            this.miCollapse.Text = "Collapse All";
            this.miCollapse.Click += new System.EventHandler(this.miCollapse_Click);
            // 
            // miRefresh
            // 
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.Size = new System.Drawing.Size(136, 22);
            this.miRefresh.Text = "Refresh";
            this.miRefresh.Click += new System.EventHandler(this.miRefresh_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(368, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(62, 18);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblArrange
            // 
            this.lblArrange.Location = new System.Drawing.Point(3, 3);
            this.lblArrange.Name = "lblArrange";
            this.lblArrange.Size = new System.Drawing.Size(58, 13);
            this.lblArrange.TabIndex = 0;
            this.lblArrange.Text = "Arrange By:";
            // 
            // gcAlerts
            // 
            this.gcAlerts.ContextMenuStrip = this.contextMenuStrip1;
            this.gcAlerts.DataMember = "Alert";
            this.gcAlerts.DataSource = this.dsAlert1;
            this.gcAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAlerts.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAlerts.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAlerts.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAlerts.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAlerts.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcAlerts.Location = new System.Drawing.Point(2, 26);
            this.gcAlerts.MainView = this.gvAlerts;
            this.gcAlerts.Name = "gcAlerts";
            this.gcAlerts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDateFormat,
            this.riImages,
            this.riRead});
            this.gcAlerts.Size = new System.Drawing.Size(433, 682);
            this.gcAlerts.TabIndex = 0;
            this.gcAlerts.UseEmbeddedNavigator = true;
            this.gcAlerts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAlerts});
            // 
            // dsAlert1
            // 
            this.dsAlert1.DataSetName = "dsAlert";
            this.dsAlert1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvAlerts
            // 
            this.gvAlerts.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand});
            this.gvAlerts.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colType,
            this.bandedGridColumn2,
            this.colSubject,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.colRead,
            this.colID,
            this.colCompleted});
            this.gvAlerts.CustomizationFormBounds = new System.Drawing.Rectangle(1055, 518, 225, 209);
            styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            styleFormatCondition1.Appearance.Options.UseFont = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.colRead;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "F";
            styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
            styleFormatCondition2.Appearance.Options.UseFont = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Column = this.colCompleted;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "T";
            this.gvAlerts.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gvAlerts.GridControl = this.gcAlerts;
            this.gvAlerts.GroupCount = 1;
            this.gvAlerts.GroupFormat = "[#image] {1}{2}";
            this.gvAlerts.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, "Received", null, "")});
            this.gvAlerts.Name = "gvAlerts";
            this.gvAlerts.OptionsBehavior.Editable = false;
            this.gvAlerts.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAlerts.OptionsView.ColumnAutoWidth = true;
            this.gvAlerts.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvAlerts.OptionsView.ShowGroupPanel = false;
            this.gvAlerts.OptionsView.ShowIndicator = false;
            this.gvAlerts.OptionsView.ShowVertLines = false;
            this.gvAlerts.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn4, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvAlerts.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvAlerts_FocusedRowChanged);
            this.gvAlerts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvAlerts_KeyDown);
            this.gvAlerts.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.gvAlerts_ShowGridMenu);
            // 
            // gridBand
            // 
            this.gridBand.Caption = "Alert Inbox";
            this.gridBand.Columns.Add(this.colRead);
            this.gridBand.Columns.Add(this.bandedGridColumn2);
            this.gridBand.Columns.Add(this.bandedGridColumn4);
            this.gridBand.Columns.Add(this.bandedGridColumn5);
            this.gridBand.Columns.Add(this.colSubject);
            this.gridBand.Columns.Add(this.colType);
            this.gridBand.MinWidth = 20;
            this.gridBand.Name = "gridBand";
            this.gridBand.OptionsBand.FixedWidth = true;
            this.gridBand.Width = 429;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Module";
            this.bandedGridColumn2.FieldName = "MODULE";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 268;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "Received";
            this.bandedGridColumn4.ColumnEdit = this.riDateFormat;
            this.bandedGridColumn4.FieldName = "RECEIVED";
            this.bandedGridColumn4.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateRange;
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 131;
            // 
            // riDateFormat
            // 
            this.riDateFormat.AutoHeight = false;
            this.riDateFormat.Mask.EditMask = "g";
            this.riDateFormat.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riDateFormat.Mask.UseMaskAsDisplayFormat = true;
            this.riDateFormat.Name = "riDateFormat";
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.RowIndex = 1;
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 30;
            // 
            // colSubject
            // 
            this.colSubject.Caption = "Subject";
            this.colSubject.FieldName = "SUBJECT";
            this.colSubject.Name = "colSubject";
            this.colSubject.RowIndex = 1;
            this.colSubject.Visible = true;
            this.colSubject.Width = 359;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(437, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 710);
            this.splitterControl1.TabIndex = 3;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.alertView1);
            this.panelControl1.Controls.Add(this.pBack);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(443, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(708, 710);
            this.panelControl1.TabIndex = 5;
            // 
            // alertView1
            // 
            this.alertView1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.alertView1.Appearance.Options.UseBackColor = true;
            this.alertView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertView1.HMConnection = null;
            this.alertView1.Location = new System.Drawing.Point(2, 2);
            this.alertView1.Name = "alertView1";
            this.alertView1.Size = new System.Drawing.Size(704, 706);
            this.alertView1.TabIndex = 0;
            this.alertView1.TUC_DevXMgr = null;
            this.alertView1.Load += new System.EventHandler(this.alertView1_Load);
            // 
            // pBack
            // 
            this.pBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBack.Location = new System.Drawing.Point(2, 2);
            this.pBack.Name = "pBack";
            this.pBack.Size = new System.Drawing.Size(704, 706);
            this.pBack.TabIndex = 1;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@contactid", System.Data.SqlDbType.Int, 4, "CONTACT_ID")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daAlert
            // 
            this.daAlert.SelectCommand = this.sqlSelectCommand1;
            this.daAlert.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Alert", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("TYPE", "TYPE"),
                        new System.Data.Common.DataColumnMapping("AAP_ID", "AAP_ID"),
                        new System.Data.Common.DataColumnMapping("APP_MOD_ID", "APP_MOD_ID"),
                        new System.Data.Common.DataColumnMapping("SUBJECT", "SUBJECT"),
                        new System.Data.Common.DataColumnMapping("MODULE", "MODULE"),
                        new System.Data.Common.DataColumnMapping("RECEIVED", "RECEIVED"),
                        new System.Data.Common.DataColumnMapping("NOTES", "NOTES"),
                        new System.Data.Common.DataColumnMapping("VIEWED", "VIEWED"),
                        new System.Data.Common.DataColumnMapping("DETAIL_ID", "DETAIL_ID"),
                        new System.Data.Common.DataColumnMapping("COMPLETED_TF", "COMPLETED_TF")})});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select contact_id from AlertStakeHolder where alert_id=@alertid and contact_id <>" +
                " @contactid\r\n";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@alertid", System.Data.SqlDbType.Int, 4, "ALERT_ID"),
            new System.Data.SqlClient.SqlParameter("@contactid", System.Data.SqlDbType.Int, 4, "CONTACT_ID")});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [AlertStakeHolder] ([contact_id]) VALUES (@contact_id)";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@contact_id", System.Data.SqlDbType.Int, 0, "contact_id")});
            // 
            // daStakeholders
            // 
            this.daStakeholders.InsertCommand = this.sqlInsertCommand1;
            this.daStakeholders.SelectCommand = this.sqlSelectCommand2;
            this.daStakeholders.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AlertStakeholder", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("contact_id", "contact_id")})});
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "select ID, isnull(knownas, isnull(Firstname,\'\')+\' \'+isnull(lastname,\'\')) Contact " +
                "from contact where type=\'USER\'";
            this.sqlSelectCommand3.Connection = this.WEB_Conn;
            // 
            // WEB_Conn
            // 
            this.WEB_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=web_strike_test;Persist Security Info=True;User " +
                "ID=hmsqlsa;Password=hmsqlsa";
            this.WEB_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM [contact] WHERE (([ID] = @Original_ID))";
            this.sqlDeleteCommand1.Connection = this.WEB_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daContacts
            // 
            this.daContacts.DeleteCommand = this.sqlDeleteCommand1;
            this.daContacts.SelectCommand = this.sqlSelectCommand3;
            this.daContacts.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Contact", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("Contact", "Contact")})});
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = resources.GetString("sqlSelectCommand4.CommandText");
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            // 
            // daAlertType
            // 
            this.daAlertType.DeleteCommand = this.sqlDeleteCommand;
            this.daAlertType.SelectCommand = this.sqlSelectCommand4;
            this.daAlertType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ApplicationAlertPoint", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("ALERT", "ALERT"),
                        new System.Data.Common.DataColumnMapping("ALERT_POINT", "ALERT_POINT"),
                        new System.Data.Common.DataColumnMapping("DisplayName", "DisplayName")})});
            this.daAlertType.UpdateCommand = this.sqlUpdateCommand;
            // 
            // sqlDeleteCommand
            // 
            this.sqlDeleteCommand.CommandText = resources.GetString("sqlDeleteCommand.CommandText");
            this.sqlDeleteCommand.Connection = this.TR_Conn;
            this.sqlDeleteCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ALERT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ALERT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ALERT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ALERT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ALERT_POINT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ALERT_POINT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ALERT_POINT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ALERT_POINT", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlUpdateCommand
            // 
            this.sqlUpdateCommand.CommandText = resources.GetString("sqlUpdateCommand.CommandText");
            this.sqlUpdateCommand.Connection = this.TR_Conn;
            this.sqlUpdateCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 0, "ID"),
            new System.Data.SqlClient.SqlParameter("@ALERT", System.Data.SqlDbType.VarChar, 0, "ALERT"),
            new System.Data.SqlClient.SqlParameter("@ALERT_POINT", System.Data.SqlDbType.VarChar, 0, "ALERT_POINT"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ALERT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ALERT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ALERT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ALERT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ALERT_POINT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ALERT_POINT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ALERT_POINT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ALERT_POINT", System.Data.DataRowVersion.Original, null)});
            // 
            // ManagementBoard
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.gAlert);
            this.Controls.Add(this.hideContainerRight);
            this.Name = "ManagementBoard";
            this.Size = new System.Drawing.Size(1170, 710);
            this.Load += new System.EventHandler(this.ManagementBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.riRead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerRight.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStakeholders1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riContact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsContacts1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAlertType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationAlertPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gAlert)).EndInit();
            this.gAlert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlert1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.GroupControl gAlert;
        private DevExpress.XtraGrid.GridControl gcAlerts;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl gcStakeholder;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStakeholder;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gvAlerts;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSubject;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riDateFormat;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.ToolStripMenuItem miDate;
        private DevExpress.XtraEditors.LabelControl lblArrange;
        private System.Windows.Forms.ToolStripMenuItem miModule;
        private System.Windows.Forms.ToolStripMenuItem miType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riImages;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRead;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riRead;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LookUpEdit lueAlertType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.Windows.Forms.ToolStripMenuItem miExpand;
        private System.Windows.Forms.ToolStripMenuItem miCollapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daAlert;
        private dsAlert dsAlert1;
        private DevExpress.XtraEditors.PanelControl pBack;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlDataAdapter daStakeholders;
        private dsStakeholders dsStakeholders1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlConnection WEB_Conn;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daContacts;
        private dsContacts dsContacts1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riContact;
        private System.Windows.Forms.BindingSource contactBindingSource;
        private AlertView alertView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colID;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
        private System.Data.SqlClient.SqlDataAdapter daAlertType;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand;
        private dsAlertType dsAlertType1;
        private System.Windows.Forms.BindingSource applicationAlertPointBindingSource;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerRight;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCompleted;
    }
}
