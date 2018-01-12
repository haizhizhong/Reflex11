namespace AP_Levy
{
    partial class ucLevyMatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLevyMatch));
            this.gcLevy = new DevExpress.XtraGrid.GridControl();
            this.dsLevyMatch1 = new AP_Levy.dsLevyMatch();
            this.gvLevy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daLevyMatch = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcLevy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevyMatch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevy)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLevy
            // 
            this.gcLevy.DataMember = "LD_Levy";
            this.gcLevy.DataSource = this.dsLevyMatch1;
            this.gcLevy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLevy.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcLevy.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcLevy.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcLevy.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcLevy.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcLevy.Location = new System.Drawing.Point(0, 0);
            this.gcLevy.MainView = this.gvLevy;
            this.gcLevy.Name = "gcLevy";
            this.gcLevy.Size = new System.Drawing.Size(1050, 417);
            this.gcLevy.TabIndex = 0;
            this.gcLevy.UseEmbeddedNavigator = true;
            this.gcLevy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLevy});
            // 
            // dsLevyMatch1
            // 
            this.dsLevyMatch1.DataSetName = "dsLevyMatch";
            this.dsLevyMatch1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvLevy
            // 
            this.gvLevy.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvLevy.GridControl = this.gcLevy;
            this.gvLevy.Name = "gvLevy";
            this.gvLevy.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Master Agreement #";
            this.gridColumn1.FieldName = "MASTER_AGREEMENT_NUM";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Agreement #";
            this.gridColumn2.FieldName = "agreement_num";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Lot";
            this.gridColumn3.FieldName = "lot_num";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Block";
            this.gridColumn4.FieldName = "block_num";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Plan";
            this.gridColumn5.FieldName = "plan_num";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Levy Description";
            this.gridColumn6.FieldName = "LevyDescription";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Levy Type";
            this.gridColumn7.FieldName = "LevyType";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Amount";
            this.gridColumn8.DisplayFormat.FormatString = "n2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "Amount";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ap_inv_header_id", System.Data.SqlDbType.Int, 4, "AP_INV_HEADER_ID")});
            // 
            // daLevyMatch
            // 
            this.daLevyMatch.SelectCommand = this.sqlSelectCommand1;
            this.daLevyMatch.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "LD_Levy", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("MASTER_AGREEMENT_NUM", "MASTER_AGREEMENT_NUM"),
                        new System.Data.Common.DataColumnMapping("agreement_num", "agreement_num"),
                        new System.Data.Common.DataColumnMapping("lot_num", "lot_num"),
                        new System.Data.Common.DataColumnMapping("block_num", "block_num"),
                        new System.Data.Common.DataColumnMapping("plan_num", "plan_num"),
                        new System.Data.Common.DataColumnMapping("LevyDescription", "LevyDescription"),
                        new System.Data.Common.DataColumnMapping("LevyType", "LevyType"),
                        new System.Data.Common.DataColumnMapping("Amount", "Amount")})});
            // 
            // ucLevyMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcLevy);
            this.Name = "ucLevyMatch";
            this.Size = new System.Drawing.Size(1050, 417);
            this.Load += new System.EventHandler(this.ucLevyMatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLevy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsLevyMatch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcLevy;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLevy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daLevyMatch;
        private dsLevyMatch dsLevyMatch1;
    }
}
