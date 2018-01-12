namespace AlertViews.Rental
{
    partial class ucRentalEnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRentalEnd));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daRentalEnd = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlTRConnection = new System.Data.SqlClient.SqlConnection();
            this.dsRentalEnd1 = new AlertViews.dsRentalEnd();
            this.gcRentalEnd = new DevExpress.XtraGrid.GridControl();
            this.gvRentalEnd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCUSTOMER_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRentalType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coleqi_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coleqi_desc1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsRentalEnd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRentalEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRentalEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Size = new System.Drawing.Size(987, 413);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl2.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl2.Size = new System.Drawing.Size(970, 378);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(957, 294);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(968, 323);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(970, 378);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcRentalEnd);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 439);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(987, 235);
            this.groupControl2.TabIndex = 9;
            this.groupControl2.Text = "Rental End Details";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlTRConnection;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Detail_ID", System.Data.SqlDbType.Int, 4, "RentalAgreement_ID")});
            // 
            // daRentalEnd
            // 
            this.daRentalEnd.SelectCommand = this.sqlSelectCommand1;
            this.daRentalEnd.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "RN_RentalAgreementHeader", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("CUSTOMER_CODE", "CUSTOMER_CODE"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME"),
                        new System.Data.Common.DataColumnMapping("RentalType", "RentalType"),
                        new System.Data.Common.DataColumnMapping("eqi_code", "eqi_code"),
                        new System.Data.Common.DataColumnMapping("eqi_desc1", "eqi_desc1"),
                        new System.Data.Common.DataColumnMapping("StartDate", "StartDate"),
                        new System.Data.Common.DataColumnMapping("EndDate", "EndDate")})});
            // 
            // sqlTRConnection
            // 
            this.sqlTRConnection.ConnectionString = "Data Source=DEVSQL2008R2;Initial Catalog=TR_DEV_GC;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.sqlTRConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // dsRentalEnd1
            // 
            this.dsRentalEnd1.DataSetName = "dsRentalEnd";
            this.dsRentalEnd1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gcRentalEnd
            // 
            this.gcRentalEnd.DataMember = "RN_RentalAgreementHeader";
            this.gcRentalEnd.DataSource = this.dsRentalEnd1;
            this.gcRentalEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRentalEnd.Location = new System.Drawing.Point(2, 20);
            this.gcRentalEnd.MainView = this.gvRentalEnd;
            this.gcRentalEnd.Name = "gcRentalEnd";
            this.gcRentalEnd.Size = new System.Drawing.Size(983, 213);
            this.gcRentalEnd.TabIndex = 0;
            this.gcRentalEnd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRentalEnd});
            // 
            // gvRentalEnd
            // 
            this.gvRentalEnd.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCUSTOMER_CODE,
            this.colNAME,
            this.colRentalType,
            this.coleqi_code,
            this.coleqi_desc1,
            this.colStartDate,
            this.colEndDate});
            this.gvRentalEnd.GridControl = this.gcRentalEnd;
            this.gvRentalEnd.Name = "gvRentalEnd";
            this.gvRentalEnd.OptionsView.ShowGroupPanel = false;
            // 
            // colCUSTOMER_CODE
            // 
            this.colCUSTOMER_CODE.Caption = "Customer Code";
            this.colCUSTOMER_CODE.FieldName = "CUSTOMER_CODE";
            this.colCUSTOMER_CODE.Name = "colCUSTOMER_CODE";
            this.colCUSTOMER_CODE.OptionsColumn.AllowEdit = false;
            this.colCUSTOMER_CODE.Visible = true;
            this.colCUSTOMER_CODE.VisibleIndex = 0;
            // 
            // colNAME
            // 
            this.colNAME.Caption = "Customer Name";
            this.colNAME.FieldName = "NAME";
            this.colNAME.Name = "colNAME";
            this.colNAME.OptionsColumn.AllowEdit = false;
            this.colNAME.Visible = true;
            this.colNAME.VisibleIndex = 1;
            // 
            // colRentalType
            // 
            this.colRentalType.Caption = "Rental Type";
            this.colRentalType.FieldName = "RentalType";
            this.colRentalType.Name = "colRentalType";
            this.colRentalType.OptionsColumn.AllowEdit = false;
            this.colRentalType.Visible = true;
            this.colRentalType.VisibleIndex = 2;
            // 
            // coleqi_code
            // 
            this.coleqi_code.Caption = "Asset Code";
            this.coleqi_code.FieldName = "eqi_code";
            this.coleqi_code.Name = "coleqi_code";
            this.coleqi_code.OptionsColumn.AllowEdit = false;
            this.coleqi_code.Visible = true;
            this.coleqi_code.VisibleIndex = 3;
            // 
            // coleqi_desc1
            // 
            this.coleqi_desc1.Caption = "Asset Description";
            this.coleqi_desc1.FieldName = "eqi_desc1";
            this.coleqi_desc1.Name = "coleqi_desc1";
            this.coleqi_desc1.OptionsColumn.AllowEdit = false;
            this.coleqi_desc1.Visible = true;
            this.coleqi_desc1.VisibleIndex = 4;
            // 
            // colStartDate
            // 
            this.colStartDate.Caption = "Rental Start Date";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.OptionsColumn.AllowEdit = false;
            this.colStartDate.Visible = true;
            this.colStartDate.VisibleIndex = 5;
            // 
            // colEndDate
            // 
            this.colEndDate.Caption = "Rental End Date";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.OptionsColumn.AllowEdit = false;
            this.colEndDate.Visible = true;
            this.colEndDate.VisibleIndex = 6;
            // 
            // ucRentalEnd
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Name = "ucRentalEnd";
            this.Size = new System.Drawing.Size(987, 696);
            this.Load += new System.EventHandler(this.ucRentalEnd_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.dsRentalEnd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRentalEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRentalEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlTRConnection;
        private System.Data.SqlClient.SqlDataAdapter daRentalEnd;
        private dsRentalEnd dsRentalEnd1;
        private DevExpress.XtraGrid.GridControl gcRentalEnd;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRentalEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colCUSTOMER_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn colNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colRentalType;
        private DevExpress.XtraGrid.Columns.GridColumn coleqi_code;
        private DevExpress.XtraGrid.Columns.GridColumn coleqi_desc1;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
    }
}
