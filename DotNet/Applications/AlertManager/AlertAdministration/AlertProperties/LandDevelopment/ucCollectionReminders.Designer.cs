namespace AlertProperties.LandDevelopment
{
    partial class ucCollectionReminders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCollectionReminders));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDays = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcComPur = new DevExpress.XtraGrid.GridControl();
            this.dsComPur1 = new AlertProperties.dsComPur();
            this.gvComPur = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCOMM_PUR_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riComPur = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcDocMerge = new DevExpress.XtraGrid.GridControl();
            this.dsDocMerge1 = new AlertProperties.dsDocMerge();
            this.gvDocMerge = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDocMergeFunction_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDocMerge = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daComPur = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daDocMerge = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcComPur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsComPur1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComPur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riComPur)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDocMerge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDocMerge)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.xtraTabControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Size = new System.Drawing.Size(477, 438);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(24, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(414, 49);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Send Notification if Current Date is X days prior to  Expected Payout Date and ba" +
                "lance on Lot Sale is greater than zero.";
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(148, 59);
            this.txtDays.Name = "txtDays";
            this.txtDays.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDays.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDays.Properties.Mask.EditMask = "g0";
            this.txtDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDays.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDays.Size = new System.Drawing.Size(121, 20);
            this.txtDays.TabIndex = 8;
            this.txtDays.EditValueChanged += new System.EventHandler(this.txtDays_EditValueChanged);
            this.txtDays.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtDays_EditValueChanging);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtDays);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(477, 95);
            this.panelControl2.TabIndex = 9;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 95);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(477, 343);
            this.xtraTabControl1.TabIndex = 10;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcComPur);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(468, 312);
            this.xtraTabPage1.Text = "Communication Purpose";
            // 
            // gcComPur
            // 
            this.gcComPur.DataMember = "alert_land_reminder_com_purpose";
            this.gcComPur.DataSource = this.dsComPur1;
            this.gcComPur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcComPur.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcComPur_EmbeddedNavigator_ButtonClick);
            this.gcComPur.Location = new System.Drawing.Point(0, 0);
            this.gcComPur.MainView = this.gvComPur;
            this.gcComPur.Name = "gcComPur";
            this.gcComPur.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riComPur});
            this.gcComPur.Size = new System.Drawing.Size(468, 312);
            this.gcComPur.TabIndex = 0;
            this.gcComPur.UseEmbeddedNavigator = true;
            this.gcComPur.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComPur});
            // 
            // dsComPur1
            // 
            this.dsComPur1.DataSetName = "dsComPur";
            this.dsComPur1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvComPur
            // 
            this.gvComPur.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCOMM_PUR_ID});
            this.gvComPur.GridControl = this.gcComPur;
            this.gvComPur.Name = "gvComPur";
            this.gvComPur.OptionsView.ShowGroupPanel = false;
            this.gvComPur.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvComPur_InitNewRow);
            this.gvComPur.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvComPur_RowUpdated);
            this.gvComPur.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvComPur_InvalidRowException);
            this.gvComPur.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvComPur_ValidateRow);
            // 
            // colCOMM_PUR_ID
            // 
            this.colCOMM_PUR_ID.Caption = "Communication Purpose";
            this.colCOMM_PUR_ID.ColumnEdit = this.riComPur;
            this.colCOMM_PUR_ID.FieldName = "COMM_PUR_ID";
            this.colCOMM_PUR_ID.Name = "colCOMM_PUR_ID";
            this.colCOMM_PUR_ID.Visible = true;
            this.colCOMM_PUR_ID.VisibleIndex = 0;
            // 
            // riComPur
            // 
            this.riComPur.AutoHeight = false;
            this.riComPur.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riComPur.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description")});
            this.riComPur.DisplayMember = "DESCRIPTION";
            this.riComPur.Name = "riComPur";
            this.riComPur.NullText = "";
            this.riComPur.PopupWidth = 250;
            this.riComPur.ValueMember = "ID";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcDocMerge);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(586, 359);
            this.xtraTabPage2.Text = "Document Merge Library Items";
            // 
            // gcDocMerge
            // 
            this.gcDocMerge.DataMember = "alert_land_reminder_docMerge";
            this.gcDocMerge.DataSource = this.dsDocMerge1;
            this.gcDocMerge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDocMerge.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcDocMerge_EmbeddedNavigator_ButtonClick);
            this.gcDocMerge.Location = new System.Drawing.Point(0, 0);
            this.gcDocMerge.MainView = this.gvDocMerge;
            this.gcDocMerge.Name = "gcDocMerge";
            this.gcDocMerge.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDocMerge});
            this.gcDocMerge.Size = new System.Drawing.Size(586, 359);
            this.gcDocMerge.TabIndex = 0;
            this.gcDocMerge.UseEmbeddedNavigator = true;
            this.gcDocMerge.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDocMerge});
            // 
            // dsDocMerge1
            // 
            this.dsDocMerge1.DataSetName = "dsDocMerge";
            this.dsDocMerge1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDocMerge
            // 
            this.gvDocMerge.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDocMergeFunction_ID});
            this.gvDocMerge.GridControl = this.gcDocMerge;
            this.gvDocMerge.Name = "gvDocMerge";
            this.gvDocMerge.OptionsView.ShowGroupPanel = false;
            this.gvDocMerge.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvDocMerge_InitNewRow);
            this.gvDocMerge.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvDocMerge_RowUpdated);
            this.gvDocMerge.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvDocMerge_InvalidRowException);
            this.gvDocMerge.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvDocMerge_ValidateRow);
            // 
            // colDocMergeFunction_ID
            // 
            this.colDocMergeFunction_ID.Caption = "Library Document Item";
            this.colDocMergeFunction_ID.ColumnEdit = this.riDocMerge;
            this.colDocMergeFunction_ID.FieldName = "DocMergeLibrary_ID";
            this.colDocMergeFunction_ID.Name = "colDocMergeFunction_ID";
            this.colDocMergeFunction_ID.Visible = true;
            this.colDocMergeFunction_ID.VisibleIndex = 0;
            // 
            // riDocMerge
            // 
            this.riDocMerge.AutoHeight = false;
            this.riDocMerge.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDocMerge.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MergeTemplateName", "Document")});
            this.riDocMerge.DisplayMember = "MergeTemplateName";
            this.riDocMerge.Name = "riDocMerge";
            this.riDocMerge.NullText = "";
            this.riDocMerge.PopupWidth = 250;
            this.riDocMerge.ValueMember = "DocMergeLibrary_ID";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        ID, AlertID, COMM_PUR_ID\r\nFROM            alert_land_reminder_com_p" +
                "urpose\r\nWHERE        (AlertID = @AlertID)";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 4, "AlertID")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@COMM_PUR_ID", System.Data.SqlDbType.Int, 0, "COMM_PUR_ID")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@COMM_PUR_ID", System.Data.SqlDbType.Int, 0, "COMM_PUR_ID"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daComPur
            // 
            this.daComPur.DeleteCommand = this.sqlDeleteCommand1;
            this.daComPur.InsertCommand = this.sqlInsertCommand1;
            this.daComPur.SelectCommand = this.sqlSelectCommand1;
            this.daComPur.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_land_reminder_com_purpose", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AlertID", "AlertID"),
                        new System.Data.Common.DataColumnMapping("COMM_PUR_ID", "COMM_PUR_ID")})});
            this.daComPur.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT        ID, AlertID, DocMergeLibrary_ID\r\nFROM            alert_land_reminde" +
                "r_docMerge\r\nWHERE        (AlertID = @AlertID)";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 4, "AlertID")});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, "DocMergeLibrary_ID")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, "DocMergeLibrary_ID"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daDocMerge
            // 
            this.daDocMerge.DeleteCommand = this.sqlDeleteCommand2;
            this.daDocMerge.InsertCommand = this.sqlInsertCommand2;
            this.daDocMerge.SelectCommand = this.sqlSelectCommand2;
            this.daDocMerge.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_land_reminder_docMerge", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AlertID", "AlertID"),
                        new System.Data.Common.DataColumnMapping("DocMergeLibrary_ID", "DocMergeLibrary_ID")})});
            this.daDocMerge.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // ucCollectionReminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucCollectionReminders";
            this.Size = new System.Drawing.Size(481, 460);
            this.Load += new System.EventHandler(this.ucCollectionReminders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcComPur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsComPur1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComPur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riComPur)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDocMerge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDocMerge)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDays;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcComPur;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComPur;
        private DevExpress.XtraGrid.GridControl gcDocMerge;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocMerge;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daComPur;
        private dsComPur dsComPur1;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMM_PUR_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riComPur;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riDocMerge;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daDocMerge;
        private dsDocMerge dsDocMerge1;
        private DevExpress.XtraGrid.Columns.GridColumn colDocMergeFunction_ID;
    }
}
