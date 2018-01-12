namespace AlertViews.WorkOrders.BackFlush
{
    partial class ucBackFlush
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBackFlush));
            this.pcDetail = new DevExpress.XtraEditors.PanelControl();
            this.grpDetails = new DevExpress.XtraEditors.GroupControl();
            this.gcDetails = new DevExpress.XtraGrid.GridControl();
            this.wAREHOUSEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsDetails1 = new AlertViews.WorkOrders.BackFlush.dsDetails();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPart_Number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPart_Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantityAdjusted = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteNumber3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colWAREHOUSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sqlTR = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daDetails = new System.Data.SqlClient.SqlDataAdapter();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcDetail)).BeginInit();
            this.pcDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wAREHOUSEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetails1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteNumber3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Size = new System.Drawing.Size(992, 334);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl2.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl2.Size = new System.Drawing.Size(975, 299);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(962, 215);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(973, 244);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(975, 299);
            // 
            // pcDetail
            // 
            this.pcDetail.Controls.Add(this.grpDetails);
            this.pcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcDetail.Location = new System.Drawing.Point(0, 366);
            this.pcDetail.Name = "pcDetail";
            this.pcDetail.Size = new System.Drawing.Size(992, 312);
            this.pcDetail.TabIndex = 11;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.gcDetails);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(2, 2);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(988, 308);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.Text = "Bill of Materials Back Flushed";
            // 
            // gcDetails
            // 
            this.gcDetails.DataSource = this.wAREHOUSEBindingSource;
            this.gcDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDetails.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcDetails.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcDetails.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcDetails.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcDetails.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcDetails.Location = new System.Drawing.Point(2, 20);
            this.gcDetails.MainView = this.gvDetails;
            this.gcDetails.Name = "gcDetails";
            this.gcDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riteNumber3});
            this.gcDetails.Size = new System.Drawing.Size(984, 286);
            this.gcDetails.TabIndex = 0;
            this.gcDetails.UseEmbeddedNavigator = true;
            this.gcDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails,
            this.gridView2});
            // 
            // wAREHOUSEBindingSource
            // 
            this.wAREHOUSEBindingSource.DataMember = "WAREHOUSE";
            this.wAREHOUSEBindingSource.DataSource = this.dsDetails1;
            // 
            // dsDetails1
            // 
            this.dsDetails1.DataSetName = "dsDetails";
            this.dsDetails1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPart_Number,
            this.colPart_Description,
            this.colQuantityAdjusted,
            this.colWAREHOUSE,
            this.colDESCRIPTION});
            this.gvDetails.GridControl = this.gcDetails;
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.Editable = false;
            // 
            // colPart_Number
            // 
            this.colPart_Number.Caption = "Part Number";
            this.colPart_Number.FieldName = "Part_Number";
            this.colPart_Number.Name = "colPart_Number";
            this.colPart_Number.Visible = true;
            this.colPart_Number.VisibleIndex = 2;
            this.colPart_Number.Width = 175;
            // 
            // colPart_Description
            // 
            this.colPart_Description.Caption = "Part Description";
            this.colPart_Description.FieldName = "Part_Description";
            this.colPart_Description.Name = "colPart_Description";
            this.colPart_Description.Visible = true;
            this.colPart_Description.VisibleIndex = 3;
            this.colPart_Description.Width = 262;
            // 
            // colQuantityAdjusted
            // 
            this.colQuantityAdjusted.Caption = "Quantity Adjusted";
            this.colQuantityAdjusted.ColumnEdit = this.riteNumber3;
            this.colQuantityAdjusted.FieldName = "QuantityAdjusted";
            this.colQuantityAdjusted.Name = "colQuantityAdjusted";
            this.colQuantityAdjusted.Visible = true;
            this.colQuantityAdjusted.VisibleIndex = 4;
            this.colQuantityAdjusted.Width = 114;
            // 
            // riteNumber3
            // 
            this.riteNumber3.AutoHeight = false;
            this.riteNumber3.Mask.EditMask = "n3";
            this.riteNumber3.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riteNumber3.Mask.UseMaskAsDisplayFormat = true;
            this.riteNumber3.Name = "riteNumber3";
            // 
            // colWAREHOUSE
            // 
            this.colWAREHOUSE.Caption = "Warehouse";
            this.colWAREHOUSE.FieldName = "WAREHOUSE";
            this.colWAREHOUSE.Name = "colWAREHOUSE";
            this.colWAREHOUSE.Visible = true;
            this.colWAREHOUSE.VisibleIndex = 0;
            this.colWAREHOUSE.Width = 88;
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.Caption = "Warehouse Description";
            this.colDESCRIPTION.FieldName = "DESCRIPTION";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.Visible = true;
            this.colDESCRIPTION.VisibleIndex = 1;
            this.colDESCRIPTION.Width = 164;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcDetails;
            this.gridView2.Name = "gridView2";
            // 
            // sqlTR
            // 
            this.sqlTR.ConnectionString = "Data Source=SQL2008R2;Initial Catalog=TR_LENMAK_MB;User ID=hmsqlsa;Password=hmsql" +
                "sa";
            this.sqlTR.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlTR;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@BatchID", System.Data.SqlDbType.Int, 4, "BatchID")});
            // 
            // daDetails
            // 
            this.daDetails.SelectCommand = this.sqlSelectCommand1;
            this.daDetails.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "WAREHOUSE", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("WO_Inv_Movement_Alert_Details_ID", "WO_Inv_Movement_Alert_Details_ID"),
                        new System.Data.Common.DataColumnMapping("BatchID", "BatchID"),
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("Whse_ID", "Whse_ID"),
                        new System.Data.Common.DataColumnMapping("Part_Number", "Part_Number"),
                        new System.Data.Common.DataColumnMapping("Part_Description", "Part_Description"),
                        new System.Data.Common.DataColumnMapping("QuantityAdjusted", "QuantityAdjusted"),
                        new System.Data.Common.DataColumnMapping("WAREHOUSE", "WAREHOUSE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 360);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(992, 6);
            this.splitterControl1.TabIndex = 12;
            this.splitterControl1.TabStop = false;
            // 
            // ucBackFlush
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcDetail);
            this.Controls.Add(this.splitterControl1);
            this.Name = "ucBackFlush";
            this.Load += new System.EventHandler(this.ucBackFlush_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.pcDetail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcDetail)).EndInit();
            this.pcDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).EndInit();
            this.grpDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wAREHOUSEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetails1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteNumber3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcDetail;
        private DevExpress.XtraEditors.GroupControl grpDetails;
        private DevExpress.XtraGrid.GridControl gcDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Data.SqlClient.SqlConnection sqlTR;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daDetails;
        private dsDetails dsDetails1;
        private System.Windows.Forms.BindingSource wAREHOUSEBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colPart_Number;
        private DevExpress.XtraGrid.Columns.GridColumn colPart_Description;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantityAdjusted;
        private DevExpress.XtraGrid.Columns.GridColumn colWAREHOUSE;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteNumber3;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
    }
}
