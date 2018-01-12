namespace AlertProperties.CommonComponents
{
    partial class ucStakeholderType
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
            this.gcStakeholderType = new DevExpress.XtraGrid.GridControl();
            this.bsAlert_PC_Stakeholder = new System.Windows.Forms.BindingSource(this.components);
            this.dsAlert_PC_Stakeholder1 = new AlertProperties.dsAlert_PC_Stakeholder();
            this.gvStakeholderType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPC_Stakeholder_Type_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riLueStakeholderType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.pCStakeholderTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPC_StakeholderType_Lookup1 = new AlertProperties.dsPC_StakeholderType_Lookup();
            this.colAlert_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.daPC_StakeholderType_Lookup = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand_daPCStakeholderType = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daAlert_PC_Stakeholder = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAlert_PC_Stakeholder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlert_PC_Stakeholder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLueStakeholderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCStakeholderTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPC_StakeholderType_Lookup1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcStakeholderType
            // 
            this.gcStakeholderType.DataSource = this.bsAlert_PC_Stakeholder;
            this.gcStakeholderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStakeholderType.Location = new System.Drawing.Point(0, 0);
            this.gcStakeholderType.MainView = this.gvStakeholderType;
            this.gcStakeholderType.Name = "gcStakeholderType";
            this.gcStakeholderType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riLueStakeholderType});
            this.gcStakeholderType.Size = new System.Drawing.Size(510, 465);
            this.gcStakeholderType.TabIndex = 0;
            this.gcStakeholderType.UseEmbeddedNavigator = true;
            this.gcStakeholderType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStakeholderType});
            // 
            // bsAlert_PC_Stakeholder
            // 
            this.bsAlert_PC_Stakeholder.DataMember = "Alert_PC_Stakeholder";
            this.bsAlert_PC_Stakeholder.DataSource = this.dsAlert_PC_Stakeholder1;
            // 
            // dsAlert_PC_Stakeholder1
            // 
            this.dsAlert_PC_Stakeholder1.DataSetName = "dsAlert_PC_Stakeholder";
            this.dsAlert_PC_Stakeholder1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvStakeholderType
            // 
            this.gvStakeholderType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colPC_Stakeholder_Type_ID,
            this.colAlert_ID});
            this.gvStakeholderType.GridControl = this.gcStakeholderType;
            this.gvStakeholderType.Name = "gvStakeholderType";
            this.gvStakeholderType.OptionsView.ShowGroupPanel = false;
            this.gvStakeholderType.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvStakeholderType_InitNewRow);
            this.gvStakeholderType.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvStakeholderType_InvalidRowException);
            this.gvStakeholderType.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvStakeholderType_ValidateRow);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            // 
            // colPC_Stakeholder_Type_ID
            // 
            this.colPC_Stakeholder_Type_ID.Caption = "Selected Stakeholder Type";
            this.colPC_Stakeholder_Type_ID.ColumnEdit = this.riLueStakeholderType;
            this.colPC_Stakeholder_Type_ID.FieldName = "PC_Stakeholder_Type_ID";
            this.colPC_Stakeholder_Type_ID.Name = "colPC_Stakeholder_Type_ID";
            this.colPC_Stakeholder_Type_ID.Visible = true;
            this.colPC_Stakeholder_Type_ID.VisibleIndex = 0;
            // 
            // riLueStakeholderType
            // 
            this.riLueStakeholderType.AutoHeight = false;
            this.riLueStakeholderType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riLueStakeholderType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PC_Stakeholder_Type_ID", "PC_Stakeholder_Type_ID", 146, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Proj_Contact_Fieldname", "Proj_Contact_Fieldname", 127, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DisplayName", "Select Stakeholder Type", 74, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riLueStakeholderType.DataSource = this.pCStakeholderTypeBindingSource;
            this.riLueStakeholderType.DisplayMember = "DisplayName";
            this.riLueStakeholderType.Name = "riLueStakeholderType";
            this.riLueStakeholderType.NullText = "";
            this.riLueStakeholderType.ValueMember = "PC_Stakeholder_Type_ID";
            // 
            // pCStakeholderTypeBindingSource
            // 
            this.pCStakeholderTypeBindingSource.DataMember = "PC_Stakeholder_Type";
            this.pCStakeholderTypeBindingSource.DataSource = this.dsPC_StakeholderType_Lookup1;
            // 
            // dsPC_StakeholderType_Lookup1
            // 
            this.dsPC_StakeholderType_Lookup1.DataSetName = "dsPC_StakeholderType_Lookup";
            this.dsPC_StakeholderType_Lookup1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colAlert_ID
            // 
            this.colAlert_ID.FieldName = "Alert_ID";
            this.colAlert_ID.Name = "colAlert_ID";
            this.colAlert_ID.OptionsColumn.AllowEdit = false;
            // 
            // daPC_StakeholderType_Lookup
            // 
            this.daPC_StakeholderType_Lookup.SelectCommand = this.sqlSelectCommand_daPCStakeholderType;
            this.daPC_StakeholderType_Lookup.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PC_Stakeholder_Type", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PC_Stakeholder_Type_ID", "PC_Stakeholder_Type_ID"),
                        new System.Data.Common.DataColumnMapping("Proj_Contact_Fieldname", "Proj_Contact_Fieldname"),
                        new System.Data.Common.DataColumnMapping("DisplayName", "DisplayName")})});
            // 
            // sqlSelectCommand_daPCStakeholderType
            // 
            this.sqlSelectCommand_daPCStakeholderType.CommandText = "SELECT        PC_Stakeholder_Type_ID, Proj_Contact_Fieldname, DisplayName\r\nFROM  " +
                "          PC_Stakeholder_Type\r\nORDER BY DisplayName";
            this.sqlSelectCommand_daPCStakeholderType.Connection = this.TR_Conn;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=DOTNETSQL\\DOTNETSQL;Initial Catalog=TR_GC_TEST;Persist Security Info=" +
                "True;User ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        ID, PC_Stakeholder_Type_ID, Alert_ID\r\nFROM            Alert_PC_Stak" +
                "eholder\r\nWHERE        (Alert_ID = @AlertID)";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 4, "Alert_ID")});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [Alert_PC_Stakeholder] ([PC_Stakeholder_Type_ID], [Alert_ID]) VALUES " +
                "(@PC_Stakeholder_Type_ID, @Alert_ID)";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PC_Stakeholder_Type_ID", System.Data.SqlDbType.Int, 0, "PC_Stakeholder_Type_ID"),
            new System.Data.SqlClient.SqlParameter("@Alert_ID", System.Data.SqlDbType.Int, 0, "Alert_ID")});
            // 
            // daAlert_PC_Stakeholder
            // 
            this.daAlert_PC_Stakeholder.InsertCommand = this.sqlInsertCommand1;
            this.daAlert_PC_Stakeholder.SelectCommand = this.sqlSelectCommand1;
            this.daAlert_PC_Stakeholder.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Alert_PC_Stakeholder", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("PC_Stakeholder_Type_ID", "PC_Stakeholder_Type_ID"),
                        new System.Data.Common.DataColumnMapping("Alert_ID", "Alert_ID")})});
            // 
            // ucStakeholderType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcStakeholderType);
            this.Name = "ucStakeholderType";
            this.Size = new System.Drawing.Size(510, 465);
            this.Load += new System.EventHandler(this.ucStakeholderType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcStakeholderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAlert_PC_Stakeholder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlert_PC_Stakeholder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStakeholderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLueStakeholderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pCStakeholderTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPC_StakeholderType_Lookup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcStakeholderType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStakeholderType;
        private System.Data.SqlClient.SqlDataAdapter daPC_StakeholderType_Lookup;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand_daPCStakeholderType;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private dsPC_StakeholderType_Lookup dsPC_StakeholderType_Lookup1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlDataAdapter daAlert_PC_Stakeholder;
        private dsAlert_PC_Stakeholder dsAlert_PC_Stakeholder1;
        private System.Windows.Forms.BindingSource bsAlert_PC_Stakeholder;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colPC_Stakeholder_Type_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colAlert_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riLueStakeholderType;
        private System.Windows.Forms.BindingSource pCStakeholderTypeBindingSource;
    }
}
