namespace AlertViews.WorkOrders.Automation
{
    partial class avAutomation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(avAutomation));
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcDet = new DevExpress.XtraGrid.GridControl();
            this.dsAutoDet = new AlertViews.WorkOrders.Automation.dsAutoDet();
            this.gvDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daAutoDet = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAutoDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDet)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Size = new System.Drawing.Size(992, 334);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Size = new System.Drawing.Size(964, 288);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(940, 209);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(944, 229);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(964, 288);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 358);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(992, 5);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcDet);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 363);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(992, 312);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "Details";
            // 
            // gcDet
            // 
            this.gcDet.DataMember = "INVENTORY";
            this.gcDet.DataSource = this.dsAutoDet;
            this.gcDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDet.Location = new System.Drawing.Point(2, 20);
            this.gcDet.MainView = this.gvDet;
            this.gcDet.Name = "gcDet";
            this.gcDet.Size = new System.Drawing.Size(988, 290);
            this.gcDet.TabIndex = 0;
            this.gcDet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDet});
            // 
            // dsAutoDet
            // 
            this.dsAutoDet.DataSetName = "dsAutoDet";
            this.dsAutoDet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDet
            // 
            this.gvDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvDet.GridControl = this.gcDet;
            this.gvDet.Name = "gvDet";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Warehouse";
            this.gridColumn1.FieldName = "warehouse";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Warehouse Description";
            this.gridColumn2.FieldName = "description";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Part Number";
            this.gridColumn3.FieldName = "part_number";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Part Description";
            this.gridColumn4.FieldName = "description1";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Quantity";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "qty";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=DEV11\\SQL2014;Initial Catalog=tr_strike_test10;Persist Security Info=" +
    "True;User ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@WO_SO_RequestID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // daAutoDet
            // 
            this.daAutoDet.SelectCommand = this.sqlSelectCommand1;
            this.daAutoDet.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "INVENTORY", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("warehouse", "warehouse"),
                        new System.Data.Common.DataColumnMapping("description", "description"),
                        new System.Data.Common.DataColumnMapping("part_number", "part_number"),
                        new System.Data.Common.DataColumnMapping("description1", "description1"),
                        new System.Data.Common.DataColumnMapping("qty", "qty")})});
            // 
            // avAutomation
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitterControl1);
            this.Name = "avAutomation";
            this.Load += new System.EventHandler(this.avAutomation_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAutoDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcDet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daAutoDet;
        private dsAutoDet dsAutoDet;
    }
}
