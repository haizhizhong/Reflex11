namespace AlertProperties.GeneralConstruction
{
    partial class ucProjectCostingEmployeeScheduling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjectCostingEmployeeScheduling));
            this.grpcMain = new DevExpress.XtraEditors.GroupControl();
            this.grpcStakeholderTypes = new DevExpress.XtraEditors.GroupControl();
            this.gcStakeholderType = new DevExpress.XtraGrid.GridControl();
            this.dsAlertEmployeeScheduling1 = new AlertProperties.dsAlertEmployeeScheduling();
            this.gvStakeholderType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPC_Stakeholder_Type_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rilue_PC_Stakeholder_Type = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.pCStakeholderTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPCStakeholderType1 = new AlertProperties.dsPCStakeholderType();
            this.riBuyer = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lblTitleNote = new DevExpress.XtraEditors.LabelControl();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand_daPCStakeholderType = new System.Data.SqlClient.SqlCommand();
            this.daPCStakeholderType = new System.Data.SqlClient.SqlDataAdapter();
            this.daAlertEmployeeScheduling = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlInsertCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcMain)).BeginInit();
            this.grpcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpcStakeholderTypes)).BeginInit();
            this.grpcStakeholderTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertEmployeeScheduling1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilue_PC_Stakeholder_Type)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCStakeholderTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPCStakeholderType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riBuyer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grpcStakeholderTypes);
            this.panelControl1.Controls.Add(this.lblTitleNote);
            this.panelControl1.Size = new System.Drawing.Size(617, 627);
            // 
            // grpcMain
            // 
            this.grpcMain.Controls.Add(this.panelControl1);
            this.grpcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpcMain.Location = new System.Drawing.Point(0, 0);
            this.grpcMain.Name = "grpcMain";
            this.grpcMain.Size = new System.Drawing.Size(621, 649);
            this.grpcMain.TabIndex = 0;
            this.grpcMain.Text = "Employee Scheduling Notifications";
            // 
            // grpcStakeholderTypes
            // 
            this.grpcStakeholderTypes.Controls.Add(this.gcStakeholderType);
            this.grpcStakeholderTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpcStakeholderTypes.Location = new System.Drawing.Point(0, 64);
            this.grpcStakeholderTypes.Name = "grpcStakeholderTypes";
            this.grpcStakeholderTypes.Size = new System.Drawing.Size(617, 563);
            this.grpcStakeholderTypes.TabIndex = 12;
            this.grpcStakeholderTypes.Text = "Stakeholder Types";
            // 
            // gcStakeholderType
            // 
            this.gcStakeholderType.DataMember = "alert_employee_scheduling";
            this.gcStakeholderType.DataSource = this.dsAlertEmployeeScheduling1;
            this.gcStakeholderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStakeholderType.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcStakeholderType_EmbeddedNavigator_ButtonClick);
            this.gcStakeholderType.Location = new System.Drawing.Point(2, 20);
            this.gcStakeholderType.MainView = this.gvStakeholderType;
            this.gcStakeholderType.Name = "gcStakeholderType";
            this.gcStakeholderType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riBuyer,
            this.rilue_PC_Stakeholder_Type});
            this.gcStakeholderType.Size = new System.Drawing.Size(613, 541);
            this.gcStakeholderType.TabIndex = 7;
            this.gcStakeholderType.UseEmbeddedNavigator = true;
            this.gcStakeholderType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStakeholderType});
            // 
            // dsAlertEmployeeScheduling1
            // 
            this.dsAlertEmployeeScheduling1.DataSetName = "dsAlertEmployeeScheduling";
            this.dsAlertEmployeeScheduling1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvStakeholderType
            // 
            this.gvStakeholderType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid,
            this.colPC_Stakeholder_Type_ID});
            this.gvStakeholderType.GridControl = this.gcStakeholderType;
            this.gvStakeholderType.Name = "gvStakeholderType";
            this.gvStakeholderType.OptionsView.ShowGroupPanel = false;
            this.gvStakeholderType.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvStakeholderType_RowUpdated);
            this.gvStakeholderType.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvStakeholderType_InvalidRowException);
            this.gvStakeholderType.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvStakeholderType_ValidateRow);
            // 
            // colid
            // 
            this.colid.FieldName = "id";
            this.colid.Name = "colid";
            // 
            // colPC_Stakeholder_Type_ID
            // 
            this.colPC_Stakeholder_Type_ID.Caption = "Selected Stakeholder Type";
            this.colPC_Stakeholder_Type_ID.ColumnEdit = this.rilue_PC_Stakeholder_Type;
            this.colPC_Stakeholder_Type_ID.FieldName = "PC_Stakeholder_Type_ID";
            this.colPC_Stakeholder_Type_ID.Name = "colPC_Stakeholder_Type_ID";
            this.colPC_Stakeholder_Type_ID.Visible = true;
            this.colPC_Stakeholder_Type_ID.VisibleIndex = 0;
            // 
            // rilue_PC_Stakeholder_Type
            // 
            this.rilue_PC_Stakeholder_Type.AutoHeight = false;
            this.rilue_PC_Stakeholder_Type.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rilue_PC_Stakeholder_Type.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayName", "Display Name", 120, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.rilue_PC_Stakeholder_Type.DataSource = this.pCStakeholderTypeBindingSource;
            this.rilue_PC_Stakeholder_Type.DisplayMember = "DisplayName";
            this.rilue_PC_Stakeholder_Type.Name = "rilue_PC_Stakeholder_Type";
            this.rilue_PC_Stakeholder_Type.NullText = "";
            this.rilue_PC_Stakeholder_Type.ValueMember = "PC_Stakeholder_Type_ID";
            // 
            // pCStakeholderTypeBindingSource
            // 
            this.pCStakeholderTypeBindingSource.DataMember = "PC_Stakeholder_Type";
            this.pCStakeholderTypeBindingSource.DataSource = this.dsPCStakeholderType1;
            // 
            // dsPCStakeholderType1
            // 
            this.dsPCStakeholderType1.DataSetName = "dsPCStakeholderType";
            this.dsPCStakeholderType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riBuyer
            // 
            this.riBuyer.AutoHeight = false;
            this.riBuyer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riBuyer.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("buyer", "buyer", 51, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", "Buyer", 36, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riBuyer.DisplayMember = "name";
            this.riBuyer.Name = "riBuyer";
            this.riBuyer.NullText = "";
            this.riBuyer.PopupWidth = 250;
            this.riBuyer.ValueMember = "buyer";
            // 
            // lblTitleNote
            // 
            this.lblTitleNote.Appearance.Options.UseTextOptions = true;
            this.lblTitleNote.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitleNote.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitleNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleNote.Location = new System.Drawing.Point(0, 0);
            this.lblTitleNote.Name = "lblTitleNote";
            this.lblTitleNote.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitleNote.Size = new System.Drawing.Size(617, 64);
            this.lblTitleNote.TabIndex = 6;
            this.lblTitleNote.Text = "Add stakeholder types that will receive this alert when it fires";
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11\\;Initial Catalog=tr_strike_test10;User ID=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand_daPCStakeholderType
            // 
            this.sqlSelectCommand_daPCStakeholderType.CommandText = "SELECT        PC_Stakeholder_Type_ID, Proj_Contact_Fieldname, DisplayName\r\nFROM  " +
                "          PC_Stakeholder_Type\r\nORDER BY DisplayName";
            this.sqlSelectCommand_daPCStakeholderType.Connection = this.TR_Conn;
            // 
            // daPCStakeholderType
            // 
            this.daPCStakeholderType.SelectCommand = this.sqlSelectCommand_daPCStakeholderType;
            this.daPCStakeholderType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PC_Stakeholder_Type", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PC_Stakeholder_Type_ID", "PC_Stakeholder_Type_ID"),
                        new System.Data.Common.DataColumnMapping("Proj_Contact_Fieldname", "Proj_Contact_Fieldname"),
                        new System.Data.Common.DataColumnMapping("DisplayName", "DisplayName")})});
            // 
            // daAlertEmployeeScheduling
            // 
            this.daAlertEmployeeScheduling.DeleteCommand = this.sqlDeleteCommand;
            this.daAlertEmployeeScheduling.InsertCommand = this.sqlInsertCommand;
            this.daAlertEmployeeScheduling.SelectCommand = this.sqlSelectCommand;
            this.daAlertEmployeeScheduling.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_employee_scheduling", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("PC_Stakeholder_Type_ID", "PC_Stakeholder_Type_ID")})});
            this.daAlertEmployeeScheduling.UpdateCommand = this.sqlUpdateCommand;
            // 
            // sqlInsertCommand
            // 
            this.sqlInsertCommand.CommandText = "INSERT INTO [alert_employee_scheduling] ([PC_Stakeholder_Type_ID]) VALUES (@PC_St" +
                "akeholder_Type_ID);\r\nSELECT id, PC_Stakeholder_Type_ID FROM alert_employee_sched" +
                "uling WHERE (id = SCOPE_IDENTITY())";
            this.sqlInsertCommand.Connection = this.TR_Conn;
            this.sqlInsertCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, "PC_Stakeholder_Type_ID")});
            // 
            // sqlSelectCommand
            // 
            this.sqlSelectCommand.CommandText = "SELECT        id, PC_Stakeholder_Type_ID\r\nFROM            alert_employee_scheduli" +
                "ng";
            this.sqlSelectCommand.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand
            // 
            this.sqlUpdateCommand.CommandText = resources.GetString("sqlUpdateCommand.CommandText");
            this.sqlUpdateCommand.Connection = this.TR_Conn;
            this.sqlUpdateCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, "PC_Stakeholder_Type_ID"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PC_Stakeholder_Type_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PC_Stakeholder_Type_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // sqlDeleteCommand
            // 
            this.sqlDeleteCommand.CommandText = resources.GetString("sqlDeleteCommand.CommandText");
            this.sqlDeleteCommand.Connection = this.TR_Conn;
            this.sqlDeleteCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PC_Stakeholder_Type_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PC_Stakeholder_Type_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // ucProjectCostingEmployeeScheduling_backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpcMain);
            this.Name = "ucProjectCostingEmployeeScheduling_backup";
            this.Size = new System.Drawing.Size(621, 649);
            this.Load += new System.EventHandler(this.ucProjectCostingEmployeeScheduling_Load);
            this.Controls.SetChildIndex(this.grpcMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpcMain)).EndInit();
            this.grpcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpcStakeholderTypes)).EndInit();
            this.grpcStakeholderTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertEmployeeScheduling1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rilue_PC_Stakeholder_Type)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCStakeholderTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPCStakeholderType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riBuyer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpcMain;
        private DevExpress.XtraGrid.GridControl gcStakeholderType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStakeholderType;
        private DevExpress.XtraEditors.LabelControl lblTitleNote;
        private DevExpress.XtraEditors.GroupControl grpcStakeholderTypes;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riBuyer;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rilue_PC_Stakeholder_Type;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand_daPCStakeholderType;
        private System.Data.SqlClient.SqlDataAdapter daPCStakeholderType;
        private dsPCStakeholderType dsPCStakeholderType1;
        private System.Windows.Forms.BindingSource pCStakeholderTypeBindingSource;
        private System.Data.SqlClient.SqlDataAdapter daAlertEmployeeScheduling;
        private dsAlertEmployeeScheduling dsAlertEmployeeScheduling1;
        private DevExpress.XtraGrid.Columns.GridColumn colPC_Stakeholder_Type_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand;
    }
}
