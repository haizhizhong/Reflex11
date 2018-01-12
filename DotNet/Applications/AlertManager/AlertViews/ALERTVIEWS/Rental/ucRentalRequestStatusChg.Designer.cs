namespace AlertViews.Rental
{
    partial class ucRentalRequestStatusChg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRentalRequestStatusChg));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcStatusChange = new DevExpress.XtraGrid.GridControl();
            this.dsStatusChange1 = new AlertViews.dsStatusChange();
            this.gvStatusChange = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colService_Add1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colService_Add2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colService_Add3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colService_City = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colService_Prov = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colService_Postal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDriver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colBilled = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDATE_INVOICED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlTRConnection = new System.Data.SqlClient.SqlConnection();
            this.daStatusChange = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStatusChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStatusChange1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatusChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Size = new System.Drawing.Size(992, 410);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl2.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl2.Size = new System.Drawing.Size(975, 375);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(962, 291);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(973, 320);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(975, 375);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcStatusChange);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 436);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(992, 242);
            this.groupControl2.TabIndex = 9;
            this.groupControl2.Text = "Status Change Details";
            // 
            // gcStatusChange
            // 
            this.gcStatusChange.DataMember = "RN_Driver";
            this.gcStatusChange.DataSource = this.dsStatusChange1;
            this.gcStatusChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStatusChange.Location = new System.Drawing.Point(2, 20);
            this.gcStatusChange.MainView = this.gvStatusChange;
            this.gcStatusChange.Name = "gcStatusChange";
            this.gcStatusChange.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1});
            this.gcStatusChange.Size = new System.Drawing.Size(988, 220);
            this.gcStatusChange.TabIndex = 0;
            this.gcStatusChange.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStatusChange});
            // 
            // dsStatusChange1
            // 
            this.dsStatusChange1.DataSetName = "dsStatusChange";
            this.dsStatusChange1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvStatusChange
            // 
            this.gvStatusChange.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colService_Add1,
            this.colService_Add2,
            this.colService_Add3,
            this.colService_City,
            this.colService_Prov,
            this.colService_Postal,
            this.colPriority,
            this.colDriver,
            this.colComments,
            this.colBilled,
            this.colDATE_INVOICED});
            this.gvStatusChange.GridControl = this.gcStatusChange;
            this.gvStatusChange.Name = "gvStatusChange";
            this.gvStatusChange.OptionsView.ColumnAutoWidth = false;
            this.gvStatusChange.OptionsView.ShowGroupPanel = false;
            // 
            // colService_Add1
            // 
            this.colService_Add1.Caption = "Service Address 1";
            this.colService_Add1.FieldName = "Service_Add1";
            this.colService_Add1.Name = "colService_Add1";
            this.colService_Add1.OptionsColumn.AllowEdit = false;
            this.colService_Add1.Visible = true;
            this.colService_Add1.VisibleIndex = 0;
            this.colService_Add1.Width = 108;
            // 
            // colService_Add2
            // 
            this.colService_Add2.Caption = "Service Address 2";
            this.colService_Add2.FieldName = "Service_Add2";
            this.colService_Add2.Name = "colService_Add2";
            this.colService_Add2.OptionsColumn.AllowEdit = false;
            this.colService_Add2.Visible = true;
            this.colService_Add2.VisibleIndex = 1;
            this.colService_Add2.Width = 108;
            // 
            // colService_Add3
            // 
            this.colService_Add3.Caption = "Service Address 3";
            this.colService_Add3.FieldName = "Service_Add3";
            this.colService_Add3.Name = "colService_Add3";
            this.colService_Add3.OptionsColumn.AllowEdit = false;
            this.colService_Add3.Visible = true;
            this.colService_Add3.VisibleIndex = 2;
            this.colService_Add3.Width = 108;
            // 
            // colService_City
            // 
            this.colService_City.Caption = "Service City";
            this.colService_City.FieldName = "Service_City";
            this.colService_City.Name = "colService_City";
            this.colService_City.OptionsColumn.AllowEdit = false;
            this.colService_City.Visible = true;
            this.colService_City.VisibleIndex = 3;
            this.colService_City.Width = 100;
            // 
            // colService_Prov
            // 
            this.colService_Prov.Caption = "Service Province";
            this.colService_Prov.FieldName = "Service_Prov";
            this.colService_Prov.Name = "colService_Prov";
            this.colService_Prov.OptionsColumn.AllowEdit = false;
            this.colService_Prov.Visible = true;
            this.colService_Prov.VisibleIndex = 4;
            this.colService_Prov.Width = 101;
            // 
            // colService_Postal
            // 
            this.colService_Postal.Caption = "Service Postal";
            this.colService_Postal.FieldName = "Service_Postal";
            this.colService_Postal.Name = "colService_Postal";
            this.colService_Postal.OptionsColumn.AllowEdit = false;
            this.colService_Postal.Width = 79;
            // 
            // colPriority
            // 
            this.colPriority.FieldName = "Priority";
            this.colPriority.Name = "colPriority";
            this.colPriority.OptionsColumn.AllowEdit = false;
            this.colPriority.Visible = true;
            this.colPriority.VisibleIndex = 5;
            this.colPriority.Width = 88;
            // 
            // colDriver
            // 
            this.colDriver.FieldName = "Driver";
            this.colDriver.Name = "colDriver";
            this.colDriver.OptionsColumn.AllowEdit = false;
            this.colDriver.Visible = true;
            this.colDriver.VisibleIndex = 6;
            this.colDriver.Width = 100;
            // 
            // colComments
            // 
            this.colComments.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colComments.FieldName = "Comments";
            this.colComments.Name = "colComments";
            this.colComments.OptionsColumn.AllowEdit = false;
            this.colComments.Visible = true;
            this.colComments.VisibleIndex = 7;
            this.colComments.Width = 241;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            this.repositoryItemMemoExEdit1.ShowIcon = false;
            // 
            // colBilled
            // 
            this.colBilled.FieldName = "Billed";
            this.colBilled.Name = "colBilled";
            this.colBilled.OptionsColumn.AllowEdit = false;
            this.colBilled.OptionsColumn.ReadOnly = true;
            this.colBilled.Visible = true;
            this.colBilled.VisibleIndex = 8;
            this.colBilled.Width = 46;
            // 
            // colDATE_INVOICED
            // 
            this.colDATE_INVOICED.Caption = "Billed Date";
            this.colDATE_INVOICED.FieldName = "DATE_INVOICED";
            this.colDATE_INVOICED.Name = "colDATE_INVOICED";
            this.colDATE_INVOICED.OptionsColumn.AllowEdit = false;
            this.colDATE_INVOICED.Visible = true;
            this.colDATE_INVOICED.VisibleIndex = 9;
            this.colDATE_INVOICED.Width = 78;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlTRConnection;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Detail_ID", System.Data.SqlDbType.Int, 4, "RentalRequest_ID")});
            // 
            // sqlTRConnection
            // 
            this.sqlTRConnection.ConnectionString = "Data Source=DEVSQL2008R2;Initial Catalog=TR_DEV_GC;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.sqlTRConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // daStatusChange
            // 
            this.daStatusChange.SelectCommand = this.sqlSelectCommand1;
            this.daStatusChange.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "RN_Driver", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Service_Add1", "Service_Add1"),
                        new System.Data.Common.DataColumnMapping("Service_Add2", "Service_Add2"),
                        new System.Data.Common.DataColumnMapping("Service_Add3", "Service_Add3"),
                        new System.Data.Common.DataColumnMapping("Service_City", "Service_City"),
                        new System.Data.Common.DataColumnMapping("Service_Prov", "Service_Prov"),
                        new System.Data.Common.DataColumnMapping("Service_Postal", "Service_Postal"),
                        new System.Data.Common.DataColumnMapping("Priority", "Priority"),
                        new System.Data.Common.DataColumnMapping("Driver", "Driver"),
                        new System.Data.Common.DataColumnMapping("Comments", "Comments"),
                        new System.Data.Common.DataColumnMapping("Billed", "Billed"),
                        new System.Data.Common.DataColumnMapping("DATE_INVOICED", "DATE_INVOICED")})});
            // 
            // ucRentalRequestStatusChg
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Name = "ucRentalRequestStatusChg";
            this.Load += new System.EventHandler(this.ucRentalRequestStatusChg_Load);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStatusChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsStatusChange1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatusChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcStatusChange;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStatusChange;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlTRConnection;
        private System.Data.SqlClient.SqlDataAdapter daStatusChange;
        private dsStatusChange dsStatusChange1;
        private DevExpress.XtraGrid.Columns.GridColumn colService_Add1;
        private DevExpress.XtraGrid.Columns.GridColumn colService_Add2;
        private DevExpress.XtraGrid.Columns.GridColumn colService_Add3;
        private DevExpress.XtraGrid.Columns.GridColumn colService_City;
        private DevExpress.XtraGrid.Columns.GridColumn colService_Prov;
        private DevExpress.XtraGrid.Columns.GridColumn colService_Postal;
        private DevExpress.XtraGrid.Columns.GridColumn colPriority;
        private DevExpress.XtraGrid.Columns.GridColumn colDriver;
        private DevExpress.XtraGrid.Columns.GridColumn colComments;
        private DevExpress.XtraGrid.Columns.GridColumn colBilled;
        private DevExpress.XtraGrid.Columns.GridColumn colDATE_INVOICED;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
    }
}
