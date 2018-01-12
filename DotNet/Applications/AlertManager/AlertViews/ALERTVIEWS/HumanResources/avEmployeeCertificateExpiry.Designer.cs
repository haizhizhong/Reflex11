namespace AlertViews.Human_Resources
{
    partial class avEmployeeCertificateExpiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(avEmployeeCertificateExpiry));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.dsCertificate1 = new AlertViews.dsCertificate();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCertificate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colexpire_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sqlConnTR = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daCertificate = new System.Data.SqlClient.SqlDataAdapter();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCertificate1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Size = new System.Drawing.Size(850, 342);
            // 
            // layoutControl2
            // 
            this.layoutControl2.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl2.Size = new System.Drawing.Size(822, 296);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(798, 217);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(802, 237);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(822, 296);
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "emp_certificates";
            this.gridControl1.DataSource = this.dsCertificate1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(842, 201);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // dsCertificate1
            // 
            this.dsCertificate1.DataSetName = "dsCertificate";
            this.dsCertificate1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCertificate,
            this.colEmployee,
            this.colexpire_date});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCertificate, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colCertificate
            // 
            this.colCertificate.FieldName = "Certificate";
            this.colCertificate.Name = "colCertificate";
            this.colCertificate.Visible = true;
            this.colCertificate.VisibleIndex = 0;
            this.colCertificate.Width = 188;
            // 
            // colEmployee
            // 
            this.colEmployee.FieldName = "Employee";
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.Visible = true;
            this.colEmployee.VisibleIndex = 0;
            this.colEmployee.Width = 158;
            // 
            // colexpire_date
            // 
            this.colexpire_date.Caption = "Expiry Date";
            this.colexpire_date.FieldName = "expire_date";
            this.colexpire_date.Name = "colexpire_date";
            this.colexpire_date.Visible = true;
            this.colexpire_date.VisibleIndex = 1;
            this.colexpire_date.Width = 111;
            // 
            // sqlConnTR
            // 
            this.sqlConnTR.ConnectionString = "Data Source=dev12;Initial Catalog=TR_Test_V11;Persist Security Info=True;User ID=" +
    "hmsqlsa;Password=hmsqlsa";
            this.sqlConnTR.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlConnTR;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Alert_ID", System.Data.SqlDbType.Int, 4, "Alert_ID")});
            // 
            // daCertificate
            // 
            this.daCertificate.SelectCommand = this.sqlSelectCommand1;
            this.daCertificate.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "emp_certificates", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Certificate", "Certificate"),
                        new System.Data.Common.DataColumnMapping("Employee", "Employee"),
                        new System.Data.Common.DataColumnMapping("expire_date", "expire_date")})});
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel1.ID = new System.Guid("3c868f37-281b-4dd7-b352-24ab119bc2b2");
            this.dockPanel1.Location = new System.Drawing.Point(0, 366);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 229);
            this.dockPanel1.Size = new System.Drawing.Size(850, 229);
            this.dockPanel1.Text = "Details";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gridControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(842, 201);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // avEmployeeCertificateExpiry
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dockPanel1);
            this.Name = "avEmployeeCertificateExpiry";
            this.Size = new System.Drawing.Size(850, 620);
            this.Load += new System.EventHandler(this.avEmployeeCertificateExpiry_Load);
            this.Controls.SetChildIndex(this.dockPanel1, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCertificate1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Data.SqlClient.SqlConnection sqlConnTR;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private dsCertificate dsCertificate1;
        private DevExpress.XtraGrid.Columns.GridColumn colCertificate;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn colexpire_date;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daCertificate;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    }
}
