namespace AP_SubcontractorCompliance
{
    partial class frmDocHotLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocHotLink));
            this.gcAttachments = new DevExpress.XtraGrid.GridControl();
            this.dsAttachments1 = new AP_SubcontractorCompliance.dsAttachments();
            this.gvAttachments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riLink = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riView = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpCompliance = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daAttachments = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpCompliance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcAttachments
            // 
            this.gcAttachments.DataMember = "Table";
            this.gcAttachments.DataSource = this.dsAttachments1;
            this.gcAttachments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAttachments.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAttachments.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAttachments.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAttachments.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAttachments.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcAttachments.Location = new System.Drawing.Point(0, 25);
            this.gcAttachments.MainView = this.gvAttachments;
            this.gcAttachments.Name = "gcAttachments";
            this.gcAttachments.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riLink,
            this.riView});
            this.gcAttachments.Size = new System.Drawing.Size(1208, 167);
            this.gcAttachments.TabIndex = 0;
            this.gcAttachments.UseEmbeddedNavigator = true;
            this.gcAttachments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAttachments});
            // 
            // dsAttachments1
            // 
            this.dsAttachments1.DataSetName = "dsAttachments";
            this.dsAttachments1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvAttachments
            // 
            this.gvAttachments.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gvAttachments.GridControl = this.gcAttachments;
            this.gvAttachments.Name = "gvAttachments";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Topical Area";
            this.gridColumn1.FieldName = "ContextItem";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "File Name";
            this.gridColumn2.FieldName = "FileName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "File Type";
            this.gridColumn3.FieldName = "FileTypeDescription";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Compliance Code";
            this.gridColumn4.FieldName = "CompCode";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Compliance";
            this.gridColumn5.FieldName = "CompDesc";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Link Document";
            this.gridColumn6.ColumnEdit = this.riLink;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // riLink
            // 
            this.riLink.AutoHeight = false;
            this.riLink.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Link", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riLink.Name = "riLink";
            this.riLink.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riLink.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riLink_ButtonClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Attachment";
            this.gridColumn7.ColumnEdit = this.riView;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // riView
            // 
            this.riView.AutoHeight = false;
            this.riView.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "View", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riView.Name = "riView";
            this.riView.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riView.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riView_ButtonClick);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpCompliance});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpCompliance
            // 
            this.dpCompliance.Controls.Add(this.dockPanel1_Container);
            this.dpCompliance.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpCompliance.ID = new System.Guid("e19e368c-31ac-4d3d-99e7-97d09b82a2b9");
            this.dpCompliance.Location = new System.Drawing.Point(0, 192);
            this.dpCompliance.Name = "dpCompliance";
            this.dpCompliance.Size = new System.Drawing.Size(1208, 500);
            this.dpCompliance.Text = "Subcontractor Compliance";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1202, 472);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1208, 25);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(529, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select an attachment and an associated subcontractor compliance record, then clic" +
                "k the link button on the file.";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RelType", System.Data.SqlDbType.VarChar, 15, "RelType"),
            new System.Data.SqlClient.SqlParameter("@RelType_ID", System.Data.SqlDbType.Int, 4, "RelType_ID")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daAttachments
            // 
            this.daAttachments.SelectCommand = this.sqlSelectCommand1;
            this.daAttachments.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Table", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Rel_ID", "Rel_ID"),
                        new System.Data.Common.DataColumnMapping("FileRepository_ID", "FileRepository_ID"),
                        new System.Data.Common.DataColumnMapping("ContextItem", "ContextItem"),
                        new System.Data.Common.DataColumnMapping("FileName", "FileName"),
                        new System.Data.Common.DataColumnMapping("FileTypeDescription", "FileTypeDescription"),
                        new System.Data.Common.DataColumnMapping("CompCode", "CompCode"),
                        new System.Data.Common.DataColumnMapping("CompDesc", "CompDesc")})});
            // 
            // frmDocHotLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 692);
            this.Controls.Add(this.gcAttachments);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.dpCompliance);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDocHotLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compliance Document Link";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDocHotLink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpCompliance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcAttachments;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAttachments;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpCompliance;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox riLink;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daAttachments;
        private dsAttachments dsAttachments1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riView;
    }
}