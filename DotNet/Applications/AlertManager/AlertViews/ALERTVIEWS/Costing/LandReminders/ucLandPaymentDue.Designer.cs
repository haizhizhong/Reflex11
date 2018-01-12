namespace AlertViews.Costing.LandReminders
{
    partial class ucLandPaymentDue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLandPaymentDue));
            this.gcPaymentDue = new DevExpress.XtraGrid.GridControl();
            this.ds_LandPaymentDue1 = new AlertViews.ds_LandPaymentDue();
            this.gvPaymentDue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colpri_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpri_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinanced_By = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLender = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOwnerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinancingType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colREPAY_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlTRConnection = new System.Data.SqlClient.SqlConnection();
            this.da_LandPaymentDue = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcPaymentDue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_LandPaymentDue1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaymentDue)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPaymentDue
            // 
            this.gcPaymentDue.DataMember = "LD_Financing";
            this.gcPaymentDue.DataSource = this.ds_LandPaymentDue1;
            this.gcPaymentDue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPaymentDue.Location = new System.Drawing.Point(0, 0);
            this.gcPaymentDue.MainView = this.gvPaymentDue;
            this.gcPaymentDue.Name = "gcPaymentDue";
            this.gcPaymentDue.Size = new System.Drawing.Size(976, 403);
            this.gcPaymentDue.TabIndex = 0;
            this.gcPaymentDue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPaymentDue});
            // 
            // ds_LandPaymentDue1
            // 
            this.ds_LandPaymentDue1.DataSetName = "ds_LandPaymentDue";
            this.ds_LandPaymentDue1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvPaymentDue
            // 
            this.gvPaymentDue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colpri_code,
            this.colpri_name,
            this.colFinanced_By,
            this.colLender,
            this.colOwnerName,
            this.colDESCRIPTION,
            this.colFinancingType,
            this.colREPAY_DATE,
            this.colAMOUNT});
            this.gvPaymentDue.GridControl = this.gcPaymentDue;
            this.gvPaymentDue.Name = "gvPaymentDue";
            this.gvPaymentDue.OptionsView.ColumnAutoWidth = false;
            // 
            // colpri_code
            // 
            this.colpri_code.Caption = "Project #";
            this.colpri_code.FieldName = "pri_code";
            this.colpri_code.Name = "colpri_code";
            this.colpri_code.OptionsColumn.AllowEdit = false;
            this.colpri_code.OptionsColumn.ReadOnly = true;
            this.colpri_code.Visible = true;
            this.colpri_code.VisibleIndex = 0;
            this.colpri_code.Width = 106;
            // 
            // colpri_name
            // 
            this.colpri_name.Caption = "Project Name";
            this.colpri_name.FieldName = "pri_name";
            this.colpri_name.Name = "colpri_name";
            this.colpri_name.OptionsColumn.AllowEdit = false;
            this.colpri_name.OptionsColumn.ReadOnly = true;
            this.colpri_name.Visible = true;
            this.colpri_name.VisibleIndex = 1;
            this.colpri_name.Width = 178;
            // 
            // colFinanced_By
            // 
            this.colFinanced_By.Caption = "Financed By";
            this.colFinanced_By.FieldName = "Financed_By";
            this.colFinanced_By.Name = "colFinanced_By";
            this.colFinanced_By.OptionsColumn.AllowEdit = false;
            this.colFinanced_By.OptionsColumn.ReadOnly = true;
            this.colFinanced_By.Visible = true;
            this.colFinanced_By.VisibleIndex = 2;
            this.colFinanced_By.Width = 122;
            // 
            // colLender
            // 
            this.colLender.Caption = "Lender";
            this.colLender.FieldName = "Lender";
            this.colLender.Name = "colLender";
            this.colLender.OptionsColumn.AllowEdit = false;
            this.colLender.OptionsColumn.ReadOnly = true;
            this.colLender.Visible = true;
            this.colLender.VisibleIndex = 3;
            this.colLender.Width = 176;
            // 
            // colOwnerName
            // 
            this.colOwnerName.Caption = "Share Owner";
            this.colOwnerName.FieldName = "OwnerName";
            this.colOwnerName.Name = "colOwnerName";
            this.colOwnerName.OptionsColumn.AllowEdit = false;
            this.colOwnerName.OptionsColumn.ReadOnly = true;
            this.colOwnerName.Visible = true;
            this.colOwnerName.VisibleIndex = 4;
            this.colOwnerName.Width = 157;
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.Caption = "Description";
            this.colDESCRIPTION.FieldName = "DESCRIPTION";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.colDESCRIPTION.OptionsColumn.ReadOnly = true;
            this.colDESCRIPTION.Visible = true;
            this.colDESCRIPTION.VisibleIndex = 5;
            this.colDESCRIPTION.Width = 172;
            // 
            // colFinancingType
            // 
            this.colFinancingType.Caption = "Financing Type";
            this.colFinancingType.FieldName = "FinancingType";
            this.colFinancingType.Name = "colFinancingType";
            this.colFinancingType.OptionsColumn.AllowEdit = false;
            this.colFinancingType.OptionsColumn.ReadOnly = true;
            this.colFinancingType.Visible = true;
            this.colFinancingType.VisibleIndex = 6;
            this.colFinancingType.Width = 141;
            // 
            // colREPAY_DATE
            // 
            this.colREPAY_DATE.Caption = "Payment Date";
            this.colREPAY_DATE.FieldName = "REPAY_DATE";
            this.colREPAY_DATE.Name = "colREPAY_DATE";
            this.colREPAY_DATE.OptionsColumn.AllowEdit = false;
            this.colREPAY_DATE.OptionsColumn.ReadOnly = true;
            this.colREPAY_DATE.Visible = true;
            this.colREPAY_DATE.VisibleIndex = 7;
            this.colREPAY_DATE.Width = 90;
            // 
            // colAMOUNT
            // 
            this.colAMOUNT.Caption = "Payment Amount";
            this.colAMOUNT.DisplayFormat.FormatString = "n2";
            this.colAMOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAMOUNT.FieldName = "AMOUNT";
            this.colAMOUNT.Name = "colAMOUNT";
            this.colAMOUNT.OptionsColumn.AllowEdit = false;
            this.colAMOUNT.OptionsColumn.ReadOnly = true;
            this.colAMOUNT.Visible = true;
            this.colAMOUNT.VisibleIndex = 8;
            this.colAMOUNT.Width = 126;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.sqlTRConnection;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@MIN_REPAY_ID", System.Data.SqlDbType.Int, 4, "MIN_REPAY_ID")});
            // 
            // sqlTRConnection
            // 
            this.sqlTRConnection.ConnectionString = "Data Source=DOTNETSQL\\DOTNETSQL;Initial Catalog=TR_GC_TEST;Persist Security Info=" +
                "True;User ID=hmsqlsa;Password=hmsqlsa";
            this.sqlTRConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // da_LandPaymentDue
            // 
            this.da_LandPaymentDue.SelectCommand = this.sqlSelectCommand1;
            this.da_LandPaymentDue.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "LD_Financing", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pri_code", "pri_code"),
                        new System.Data.Common.DataColumnMapping("pri_name", "pri_name"),
                        new System.Data.Common.DataColumnMapping("Financed_By", "Financed_By"),
                        new System.Data.Common.DataColumnMapping("Lender", "Lender"),
                        new System.Data.Common.DataColumnMapping("OwnerName", "OwnerName"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"),
                        new System.Data.Common.DataColumnMapping("FinancingType", "FinancingType"),
                        new System.Data.Common.DataColumnMapping("REPAY_DATE", "REPAY_DATE"),
                        new System.Data.Common.DataColumnMapping("AMOUNT", "AMOUNT")})});
            // 
            // ucLandPaymentDue
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPaymentDue);
            this.Name = "ucLandPaymentDue";
            this.Size = new System.Drawing.Size(976, 403);
            ((System.ComponentModel.ISupportInitialize)(this.gcPaymentDue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_LandPaymentDue1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaymentDue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPaymentDue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPaymentDue;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlTRConnection;
        private System.Data.SqlClient.SqlDataAdapter da_LandPaymentDue;
        private ds_LandPaymentDue ds_LandPaymentDue1;
        private DevExpress.XtraGrid.Columns.GridColumn colpri_code;
        private DevExpress.XtraGrid.Columns.GridColumn colpri_name;
        private DevExpress.XtraGrid.Columns.GridColumn colFinanced_By;
        private DevExpress.XtraGrid.Columns.GridColumn colLender;
        private DevExpress.XtraGrid.Columns.GridColumn colOwnerName;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn colFinancingType;
        private DevExpress.XtraGrid.Columns.GridColumn colREPAY_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn colAMOUNT;
    }
}
