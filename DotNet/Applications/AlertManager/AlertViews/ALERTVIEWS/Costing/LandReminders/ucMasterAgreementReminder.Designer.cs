namespace AlertViews.Costing.LandReminders
{
    partial class ucMasterAgreementReminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMasterAgreementReminder));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcAgreements = new DevExpress.XtraGrid.GridControl();
            this.gvAgreements = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daMastAgree = new System.Data.SqlClient.SqlDataAdapter();
            this.dsMastAgree = new AlertViews.Costing.LandReminders.dsMastAgree();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgreements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgreements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMastAgree)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcAgreements);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1034, 539);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Master Agreement Details";
            // 
            // gcAgreements
            // 
            this.gcAgreements.DataMember = "PROJ_LOT_AGREEMENT";
            this.gcAgreements.DataSource = this.dsMastAgree;
            this.gcAgreements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAgreements.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAgreements.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAgreements.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAgreements.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAgreements.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcAgreements.Location = new System.Drawing.Point(2, 20);
            this.gcAgreements.MainView = this.gvAgreements;
            this.gcAgreements.Name = "gcAgreements";
            this.gcAgreements.Size = new System.Drawing.Size(1030, 517);
            this.gcAgreements.TabIndex = 0;
            this.gcAgreements.UseEmbeddedNavigator = true;
            this.gcAgreements.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAgreements});
            // 
            // gvAgreements
            // 
            this.gvAgreements.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gvAgreements.GridControl = this.gcAgreements;
            this.gvAgreements.Name = "gvAgreements";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Agreement #";
            this.gridColumn1.FieldName = "agreement_num";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Lot #";
            this.gridColumn2.FieldName = "lot_num";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Block #";
            this.gridColumn3.FieldName = "block_num";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Plan #";
            this.gridColumn4.FieldName = "plan_num";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Lot Class";
            this.gridColumn5.FieldName = "LotClass";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Lot Sub Class";
            this.gridColumn6.FieldName = "LotSubClass";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
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
            new System.Data.SqlClient.SqlParameter("@master_agreement_id", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // daMastAgree
            // 
            this.daMastAgree.SelectCommand = this.sqlSelectCommand1;
            this.daMastAgree.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PROJ_LOT_AGREEMENT", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("agreement_num", "agreement_num"),
                        new System.Data.Common.DataColumnMapping("lot_num", "lot_num"),
                        new System.Data.Common.DataColumnMapping("block_num", "block_num"),
                        new System.Data.Common.DataColumnMapping("plan_num", "plan_num"),
                        new System.Data.Common.DataColumnMapping("LotClass", "LotClass"),
                        new System.Data.Common.DataColumnMapping("LotSubClass", "LotSubClass")})});
            // 
            // dsMastAgree
            // 
            this.dsMastAgree.DataSetName = "dsMastAgree";
            this.dsMastAgree.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ucMasterAgreementReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ucMasterAgreementReminder";
            this.Size = new System.Drawing.Size(1034, 539);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAgreements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgreements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMastAgree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcAgreements;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAgreements;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daMastAgree;
        private dsMastAgree dsMastAgree;
    }
}
