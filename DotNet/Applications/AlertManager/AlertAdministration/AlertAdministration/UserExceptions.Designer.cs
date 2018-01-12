namespace AlertAdministration
{
    partial class UserExceptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserExceptions));
            this.gcException = new DevExpress.XtraGrid.GridControl();
            this.dsExceptions1 = new AlertAdministration.dsExceptions();
            this.gvException = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMLUSER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMLUSER1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.dEPARTMENTBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsUserDep1 = new AlertAdministration.dsUserDep();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dEPARTMENTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcUser = new DevExpress.XtraGrid.GridControl();
            this.dsUser1 = new AlertAdministration.dsUser();
            this.gvUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcDepartment = new DevExpress.XtraGrid.GridControl();
            this.dsDepartment1 = new AlertAdministration.dsDepartment();
            this.gvDepartment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daExceptions = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daUser = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daDepartment = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daUserDep = new System.Data.SqlClient.SqlDataAdapter();
            this.WEB_Conn = new System.Data.SqlClient.SqlConnection();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExceptions1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvException)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTMENTBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUserDep1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTMENTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUser1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit2View)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartment1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcException
            // 
            this.gcException.AllowDrop = true;
            this.gcException.DataMember = "AlertPointAccess";
            this.gcException.DataSource = this.dsExceptions1;
            this.gcException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcException.Location = new System.Drawing.Point(2, 20);
            this.gcException.MainView = this.gvException;
            this.gcException.Name = "gcException";
            this.gcException.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1});
            this.gcException.Size = new System.Drawing.Size(493, 544);
            this.gcException.TabIndex = 0;
            this.gcException.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvException});
            this.gcException.DragDrop += new System.Windows.Forms.DragEventHandler(this.gcException_DragDrop);
            this.gcException.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcException_MouseMove);
            this.gcException.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcException_MouseDown);
            this.gcException.DragEnter += new System.Windows.Forms.DragEventHandler(this.gcException_DragEnter);
            // 
            // dsExceptions1
            // 
            this.dsExceptions1.DataSetName = "dsExceptions";
            this.dsExceptions1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvException
            // 
            this.gvException.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMLUSER,
            this.colMLUSER1});
            this.gvException.GridControl = this.gcException;
            this.gvException.Name = "gvException";
            this.gvException.OptionsSelection.MultiSelect = true;
            this.gvException.OptionsView.ShowGroupPanel = false;
            // 
            // colMLUSER
            // 
            this.colMLUSER.Caption = "User";
            this.colMLUSER.FieldName = "MLUSER";
            this.colMLUSER.Name = "colMLUSER";
            this.colMLUSER.OptionsColumn.AllowEdit = false;
            this.colMLUSER.Visible = true;
            this.colMLUSER.VisibleIndex = 0;
            // 
            // colMLUSER1
            // 
            this.colMLUSER1.Caption = "Department";
            this.colMLUSER1.ColumnEdit = this.repositoryItemGridLookUpEdit1;
            this.colMLUSER1.FieldName = "MLUSER";
            this.colMLUSER1.Name = "colMLUSER1";
            this.colMLUSER1.OptionsColumn.AllowEdit = false;
            this.colMLUSER1.Visible = true;
            this.colMLUSER1.VisibleIndex = 1;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.DataSource = this.dEPARTMENTBindingSource1;
            this.repositoryItemGridLookUpEdit1.DisplayMember = "DESCRIPTION";
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.ValueMember = "NAME";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // dEPARTMENTBindingSource1
            // 
            this.dEPARTMENTBindingSource1.DataMember = "DEPARTMENT";
            this.dEPARTMENTBindingSource1.DataSource = this.dsUserDep1;
            // 
            // dsUserDep1
            // 
            this.dsUserDep1.DataSetName = "dsUserDep";
            this.dsUserDep1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dEPARTMENTBindingSource
            // 
            this.dEPARTMENTBindingSource.DataMember = "DEPARTMENT";
            this.dEPARTMENTBindingSource.DataSource = this.dsUserDep1;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Controls.Add(this.dockPanel2);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("e10d3cd7-6951-4d71-b1d9-f0a44d07404f");
            this.panelContainer1.Location = new System.Drawing.Point(497, 0);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.Size = new System.Drawing.Size(351, 566);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("25f94a85-7642-474a-932d-63388242d2f2");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(351, 283);
            this.dockPanel1.Text = "Users";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gcUser);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(345, 255);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gcUser
            // 
            this.gcUser.AllowDrop = true;
            this.gcUser.DataMember = "MLUSER";
            this.gcUser.DataSource = this.dsUser1;
            this.gcUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUser.Location = new System.Drawing.Point(0, 0);
            this.gcUser.MainView = this.gvUser;
            this.gcUser.Name = "gcUser";
            this.gcUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit2});
            this.gcUser.Size = new System.Drawing.Size(345, 255);
            this.gcUser.TabIndex = 0;
            this.gcUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUser});
            this.gcUser.DragDrop += new System.Windows.Forms.DragEventHandler(this.gcUser_DragDrop);
            this.gcUser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcUser_MouseMove);
            this.gcUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcUser_MouseDown);
            this.gcUser.DragEnter += new System.Windows.Forms.DragEventHandler(this.gcUser_DragEnter);
            // 
            // dsUser1
            // 
            this.dsUser1.DataSetName = "dsUser";
            this.dsUser1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvUser
            // 
            this.gvUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNAME,
            this.colNAME1});
            this.gvUser.GridControl = this.gcUser;
            this.gvUser.Name = "gvUser";
            this.gvUser.OptionsSelection.MultiSelect = true;
            this.gvUser.OptionsView.ShowGroupPanel = false;
            // 
            // colNAME
            // 
            this.colNAME.Caption = "User";
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 0;
            // 
            // colNAME1
            // 
            this.colNAME1.Caption = "Department";
            this.colNAME1.ColumnEdit = this.repositoryItemGridLookUpEdit2;
            this.colNAME1.FieldName = "NAME";
            this.colNAME1.Name = "colNAME1";
            this.colNAME1.OptionsColumn.AllowEdit = false;
            this.colNAME1.Visible = true;
            this.colNAME1.VisibleIndex = 1;
            // 
            // repositoryItemGridLookUpEdit2
            // 
            this.repositoryItemGridLookUpEdit2.AutoHeight = false;
            this.repositoryItemGridLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit2.DataSource = this.dEPARTMENTBindingSource;
            this.repositoryItemGridLookUpEdit2.DisplayMember = "DESCRIPTION";
            this.repositoryItemGridLookUpEdit2.Name = "repositoryItemGridLookUpEdit2";
            this.repositoryItemGridLookUpEdit2.NullText = "";
            this.repositoryItemGridLookUpEdit2.ValueMember = "NAME";
            this.repositoryItemGridLookUpEdit2.View = this.repositoryItemGridLookUpEdit2View;
            // 
            // repositoryItemGridLookUpEdit2View
            // 
            this.repositoryItemGridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit2View.Name = "repositoryItemGridLookUpEdit2View";
            this.repositoryItemGridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("a172d05c-052b-4c22-86e7-6ac0fee537eb");
            this.dockPanel2.Location = new System.Drawing.Point(0, 283);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Size = new System.Drawing.Size(351, 283);
            this.dockPanel2.Text = "Departments";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.gcDepartment);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(345, 255);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // gcDepartment
            // 
            this.gcDepartment.DataMember = "DEPARTMENT";
            this.gcDepartment.DataSource = this.dsDepartment1;
            this.gcDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDepartment.Location = new System.Drawing.Point(0, 0);
            this.gcDepartment.MainView = this.gvDepartment;
            this.gcDepartment.Name = "gcDepartment";
            this.gcDepartment.Size = new System.Drawing.Size(345, 255);
            this.gcDepartment.TabIndex = 0;
            this.gcDepartment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDepartment});
            this.gcDepartment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcDepartment_MouseMove);
            this.gcDepartment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcDepartment_MouseDown);
            // 
            // dsDepartment1
            // 
            this.dsDepartment1.DataSetName = "dsDepartment";
            this.dsDepartment1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDepartment
            // 
            this.gvDepartment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDESCRIPTION});
            this.gvDepartment.GridControl = this.gcDepartment;
            this.gvDepartment.Name = "gvDepartment";
            this.gvDepartment.OptionsSelection.MultiSelect = true;
            this.gvDepartment.OptionsView.ShowGroupPanel = false;
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.Caption = "Department";
            this.colDESCRIPTION.FieldName = "DESCRIPTION";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.colDESCRIPTION.Visible = true;
            this.colDESCRIPTION.VisibleIndex = 0;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "select ID, AAP_ID, MLUSER from AlertPointAccess where aap_id=@aap_id";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@aap_id", System.Data.SqlDbType.Int, 4, "AAP_ID")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [AlertPointAccess] ([AAP_ID], [MLUSER]) VALUES (@AAP_ID, @MLUSER);\r\nS" +
                "ELECT ID, AAP_ID, MLUSER FROM AlertPointAccess WHERE (ID = SCOPE_IDENTITY())";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AAP_ID", System.Data.SqlDbType.Int, 0, "AAP_ID"),
            new System.Data.SqlClient.SqlParameter("@MLUSER", System.Data.SqlDbType.VarChar, 0, "MLUSER")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AAP_ID", System.Data.SqlDbType.Int, 0, "AAP_ID"),
            new System.Data.SqlClient.SqlParameter("@MLUSER", System.Data.SqlDbType.VarChar, 0, "MLUSER"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AAP_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AAP_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AAP_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AAP_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MLUSER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MLUSER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MLUSER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MLUSER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AAP_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AAP_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AAP_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AAP_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MLUSER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MLUSER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MLUSER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MLUSER", System.Data.DataRowVersion.Original, null)});
            // 
            // daExceptions
            // 
            this.daExceptions.DeleteCommand = this.sqlDeleteCommand1;
            this.daExceptions.InsertCommand = this.sqlInsertCommand1;
            this.daExceptions.SelectCommand = this.sqlSelectCommand1;
            this.daExceptions.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AlertPointAccess", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AAP_ID", "AAP_ID"),
                        new System.Data.Common.DataColumnMapping("MLUSER", "MLUSER")})});
            this.daExceptions.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT NAME FROM MLUSER u ORDER BY NAME";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = "INSERT INTO [MLUSER] ([NAME]) VALUES (@NAME);\r\nSELECT NAME FROM MLUSER AS u WHERE" +
                " (NAME = @NAME) ORDER BY NAME";
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = "UPDATE [MLUSER] SET [NAME] = @NAME WHERE (([NAME] = @Original_NAME));\r\nSELECT NAM" +
                "E FROM MLUSER AS u WHERE (NAME = @NAME) ORDER BY NAME";
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = "DELETE FROM [MLUSER] WHERE (([NAME] = @Original_NAME))";
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daUser
            // 
            this.daUser.DeleteCommand = this.sqlDeleteCommand2;
            this.daUser.InsertCommand = this.sqlInsertCommand2;
            this.daUser.SelectCommand = this.sqlSelectCommand2;
            this.daUser.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "MLUSER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daUser.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "SELECT NAME, DESCRIPTION FROM DEPARTMENT ORDER BY DESCRIPTION";
            this.sqlSelectCommand3.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = "INSERT INTO [DEPARTMENT] ([NAME], [DESCRIPTION]) VALUES (@NAME, @DESCRIPTION);\r\nS" +
                "ELECT NAME, DESCRIPTION FROM DEPARTMENT WHERE (NAME = @NAME) ORDER BY DESCRIPTIO" +
                "N";
            this.sqlInsertCommand3.Connection = this.TR_Conn;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION")});
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.TR_Conn;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = "DELETE FROM [DEPARTMENT] WHERE (([NAME] = @Original_NAME) AND ((@IsNull_DESCRIPTI" +
                "ON = 1 AND [DESCRIPTION] IS NULL) OR ([DESCRIPTION] = @Original_DESCRIPTION)))";
            this.sqlDeleteCommand3.Connection = this.TR_Conn;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daDepartment
            // 
            this.daDepartment.DeleteCommand = this.sqlDeleteCommand3;
            this.daDepartment.InsertCommand = this.sqlInsertCommand3;
            this.daDepartment.SelectCommand = this.sqlSelectCommand3;
            this.daDepartment.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "DEPARTMENT", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("NAME", "NAME"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daDepartment.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "SELECT u.NAME, d.DESCRIPTION FROM MLUSER u LEFT OUTER JOIN DEPARTMENT d ON d.NAME" +
                " = u.DEPARTMENT ORDER BY d.DESCRIPTION, u.NAME";
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            // 
            // daUserDep
            // 
            this.daUserDep.SelectCommand = this.sqlSelectCommand4;
            this.daUserDep.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "DEPARTMENT", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("NAME", "NAME"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            // 
            // WEB_Conn
            // 
            this.WEB_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcException);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(497, 566);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Drag / Drop additional users to receive this alert";
            // 
            // UserExceptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 566);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserExceptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Exceptions";
            this.Load += new System.EventHandler(this.UserExceptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExceptions1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvException)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTMENTBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUserDep1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEPARTMENTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsUser1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit2View)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartment1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcException;
        private DevExpress.XtraGrid.Views.Grid.GridView gvException;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraGrid.GridControl gcUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUser;
        private DevExpress.XtraGrid.GridControl gcDepartment;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDepartment;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daExceptions;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private DevExpress.XtraGrid.Columns.GridColumn colMLUSER;
        private DevExpress.XtraGrid.Columns.GridColumn colMLUSER1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daUser;
        private dsUser dsUser1;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
        private System.Data.SqlClient.SqlDataAdapter daDepartment;
        private dsDepartment dsDepartment1;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPTION;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
        private System.Data.SqlClient.SqlDataAdapter daUserDep;
        private dsUserDep dsUserDep1;
        private System.Data.SqlClient.SqlConnection WEB_Conn;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit2View;
        private System.Windows.Forms.BindingSource dEPARTMENTBindingSource;
        private dsExceptions dsExceptions1;
        private System.Windows.Forms.BindingSource dEPARTMENTBindingSource1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}