namespace AlertViews.Costing.LandReminders
{
    partial class ucLandReminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLandReminder));
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcAgreeDet = new DevExpress.XtraGrid.GridControl();
            this.dsAgreeDet = new AlertViews.Costing.LandReminders.dsAgreeDet();
            this.gvAgreeDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colblock_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collot_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colplan_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcExtDet = new DevExpress.XtraGrid.GridControl();
            this.dsExtDet = new AlertViews.Costing.LandReminders.dsExtDet();
            this.gvExtDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daAgreeDet = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daExtDet = new System.Data.SqlClient.SqlDataAdapter();
            this.colDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgreeDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAgreeDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgreeDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcExtDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExtDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtDet)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            this.repositoryItemMemoExEdit1.ReadOnly = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcAgreeDet);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1132, 161);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Agreement Details";
            // 
            // gcAgreeDet
            // 
            this.gcAgreeDet.DataMember = "PROJ_LOT_AGREEMENT";
            this.gcAgreeDet.DataSource = this.dsAgreeDet;
            this.gcAgreeDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAgreeDet.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAgreeDet.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAgreeDet.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAgreeDet.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAgreeDet.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcAgreeDet.Location = new System.Drawing.Point(2, 20);
            this.gcAgreeDet.MainView = this.gvAgreeDet;
            this.gcAgreeDet.Name = "gcAgreeDet";
            this.gcAgreeDet.Size = new System.Drawing.Size(1128, 139);
            this.gcAgreeDet.TabIndex = 2;
            this.gcAgreeDet.UseEmbeddedNavigator = true;
            this.gcAgreeDet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAgreeDet});
            // 
            // dsAgreeDet
            // 
            this.dsAgreeDet.DataSetName = "dsAgreeDet";
            this.dsAgreeDet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvAgreeDet
            // 
            this.gvAgreeDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.colblock_num,
            this.collot_num,
            this.colplan_num,
            this.colDESCRIPTION});
            this.gvAgreeDet.GridControl = this.gcAgreeDet;
            this.gvAgreeDet.Name = "gvAgreeDet";
            this.gvAgreeDet.OptionsBehavior.Editable = false;
            this.gvAgreeDet.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Master Agreement #";
            this.gridColumn1.FieldName = "Master_Agreement_Number";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 122;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Agreement #";
            this.gridColumn2.FieldName = "agreement_num";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 88;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Primary Purchaser";
            this.gridColumn3.FieldName = "PrimaryPurchaser";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 188;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Secondary Purchaser";
            this.gridColumn4.FieldName = "SecondaryPurchaser";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 196;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Extension Count";
            this.gridColumn5.FieldName = "ExtensionCount";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 101;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Payout Date";
            this.gridColumn6.FieldName = "payout_date";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 82;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Balance";
            this.gridColumn7.DisplayFormat.FormatString = "n2";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "Balance";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 148;
            // 
            // colblock_num
            // 
            this.colblock_num.Caption = "Block";
            this.colblock_num.FieldName = "block_num";
            this.colblock_num.Name = "colblock_num";
            this.colblock_num.Visible = true;
            this.colblock_num.VisibleIndex = 8;
            this.colblock_num.Width = 79;
            // 
            // collot_num
            // 
            this.collot_num.Caption = "Lot";
            this.collot_num.FieldName = "lot_num";
            this.collot_num.Name = "collot_num";
            this.collot_num.Visible = true;
            this.collot_num.VisibleIndex = 7;
            this.collot_num.Width = 83;
            // 
            // colplan_num
            // 
            this.colplan_num.Caption = "Plan";
            this.colplan_num.FieldName = "plan_num";
            this.colplan_num.Name = "colplan_num";
            this.colplan_num.Visible = true;
            this.colplan_num.VisibleIndex = 9;
            this.colplan_num.Width = 65;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 161);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(1132, 6);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcExtDet);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 167);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1132, 432);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "Extension Details";
            // 
            // gcExtDet
            // 
            this.gcExtDet.DataMember = "Proj_Lot_Agreement_Extension_Hist";
            this.gcExtDet.DataSource = this.dsExtDet;
            this.gcExtDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExtDet.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcExtDet.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcExtDet.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcExtDet.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcExtDet.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcExtDet.Location = new System.Drawing.Point(2, 20);
            this.gcExtDet.MainView = this.gvExtDet;
            this.gcExtDet.Name = "gcExtDet";
            this.gcExtDet.Size = new System.Drawing.Size(1128, 410);
            this.gcExtDet.TabIndex = 1;
            this.gcExtDet.UseEmbeddedNavigator = true;
            this.gcExtDet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExtDet});
            // 
            // dsExtDet
            // 
            this.dsExtDet.DataSetName = "dsExtDet";
            this.dsExtDet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvExtDet
            // 
            this.gvExtDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvExtDet.GridControl = this.gcExtDet;
            this.gvExtDet.Name = "gvExtDet";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Previous Date";
            this.gridColumn8.FieldName = "PreviousDate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "New Date";
            this.gridColumn9.FieldName = "NewDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Comment";
            this.gridColumn10.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn10.FieldName = "Comment";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Entered By";
            this.gridColumn11.FieldName = "ExtendedBy";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 3;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Date Modified";
            this.gridColumn12.FieldName = "DateModified";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@agreement_id", System.Data.SqlDbType.Int, 4, "agreement_id")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=DOTNETSQL\\DOTNETSQL;Initial Catalog=TR_GC_TEST;Persist Security Info=" +
                "True;User ID=sa;Password=sa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daAgreeDet
            // 
            this.daAgreeDet.SelectCommand = this.sqlSelectCommand1;
            this.daAgreeDet.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PROJ_LOT_AGREEMENT", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Master_Agreement_Number", "Master_Agreement_Number"),
                        new System.Data.Common.DataColumnMapping("agreement_num", "agreement_num"),
                        new System.Data.Common.DataColumnMapping("PrimaryPurchaser", "PrimaryPurchaser"),
                        new System.Data.Common.DataColumnMapping("SecondaryPurchaser", "SecondaryPurchaser"),
                        new System.Data.Common.DataColumnMapping("payout_date", "payout_date"),
                        new System.Data.Common.DataColumnMapping("Balance", "Balance"),
                        new System.Data.Common.DataColumnMapping("ExtensionCount", "ExtensionCount"),
                        new System.Data.Common.DataColumnMapping("lot_num", "lot_num"),
                        new System.Data.Common.DataColumnMapping("block_num", "block_num"),
                        new System.Data.Common.DataColumnMapping("plan_num", "plan_num"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select PreviousDate, NewDate, Comment, ExtendedBy, DateModified from Proj_Lot_Agr" +
                "eement_Extension_Hist where agreement_id=@agreement_id \r\norder by dateModified";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@agreement_id", System.Data.SqlDbType.Int, 4, "Agreement_ID")});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [Proj_Lot_Agreement_Extension_Hist] ([PreviousDate], [NewDate], [Comm" +
                "ent], [ExtendedBy], [DateModified]) VALUES (@PreviousDate, @NewDate, @Comment, @" +
                "ExtendedBy, @DateModified)";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PreviousDate", System.Data.SqlDbType.DateTime, 0, "PreviousDate"),
            new System.Data.SqlClient.SqlParameter("@NewDate", System.Data.SqlDbType.DateTime, 0, "NewDate"),
            new System.Data.SqlClient.SqlParameter("@Comment", System.Data.SqlDbType.VarChar, 0, "Comment"),
            new System.Data.SqlClient.SqlParameter("@ExtendedBy", System.Data.SqlDbType.VarChar, 0, "ExtendedBy"),
            new System.Data.SqlClient.SqlParameter("@DateModified", System.Data.SqlDbType.DateTime, 0, "DateModified")});
            // 
            // daExtDet
            // 
            this.daExtDet.InsertCommand = this.sqlInsertCommand1;
            this.daExtDet.SelectCommand = this.sqlSelectCommand2;
            this.daExtDet.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Proj_Lot_Agreement_Extension_Hist", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PreviousDate", "PreviousDate"),
                        new System.Data.Common.DataColumnMapping("NewDate", "NewDate"),
                        new System.Data.Common.DataColumnMapping("Comment", "Comment"),
                        new System.Data.Common.DataColumnMapping("ExtendedBy", "ExtendedBy"),
                        new System.Data.Common.DataColumnMapping("DateModified", "DateModified")})});
            // 
            // colDESCRIPTION
            // 
            this.colDESCRIPTION.Caption = "Type";
            this.colDESCRIPTION.FieldName = "DESCRIPTION";
            this.colDESCRIPTION.Name = "colDESCRIPTION";
            this.colDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.colDESCRIPTION.Visible = true;
            this.colDESCRIPTION.VisibleIndex = 10;
            // 
            // ucLandReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "ucLandReminder";
            this.Size = new System.Drawing.Size(1132, 599);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAgreeDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAgreeDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgreeDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcExtDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsExtDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtDet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcAgreeDet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAgreeDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcExtDet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExtDet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daAgreeDet;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlDataAdapter daExtDet;
        private dsAgreeDet dsAgreeDet;
        private dsExtDet dsExtDet;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colblock_num;
        private DevExpress.XtraGrid.Columns.GridColumn collot_num;
        private DevExpress.XtraGrid.Columns.GridColumn colplan_num;
        private DevExpress.XtraGrid.Columns.GridColumn colDESCRIPTION;
    }
}
