namespace AlertProperties.LandDevelopment
{
    partial class ucCommPurDocMerge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCommPurDocMerge));
            this.sqlConnTR = new System.Data.SqlClient.SqlConnection();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpTolerance = new DevExpress.XtraTab.XtraTabPage();
            this.gcTolerance = new DevExpress.XtraGrid.GridControl();
            this.dsPaymentTolerance1 = new AlertProperties.dsPaymentTolerance();
            this.gvTolerance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDaysPrior = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDaysPrior = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcComPur = new DevExpress.XtraGrid.GridControl();
            this.dsCommPurpose1 = new AlertProperties.dsCommPurpose();
            this.gvComPur = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCOMM_PUR_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riComPur = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.tpDocumentMerg = new DevExpress.XtraTab.XtraTabPage();
            this.gcDocMerge = new DevExpress.XtraGrid.GridControl();
            this.dsDocMergeLibraryItems1 = new AlertProperties.dsDocMergeLibraryItems();
            this.gvDocMerge = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDocMergeFunction_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDocMerge = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.daCommPurpose = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daDocMergeLibraryItems = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daPaymentTolerance = new System.Data.SqlClient.SqlDataAdapter();
            this.bsDocMergeLibraryItems = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpTolerance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPaymentTolerance1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDaysPrior)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcComPur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCommPurpose1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComPur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riComPur)).BeginInit();
            this.tpDocumentMerg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDocMergeLibraryItems1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDocMergeLibraryItems)).BeginInit();
            this.SuspendLayout();
            // 
            // sqlConnTR
            // 
            this.sqlConnTR.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.sqlConnTR.FireInfoMessageEventOnUserErrors = false;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpTolerance;
            this.xtraTabControl1.Size = new System.Drawing.Size(579, 368);
            this.xtraTabControl1.TabIndex = 12;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpTolerance,
            this.xtraTabPage1,
            this.tpDocumentMerg});
            // 
            // tpTolerance
            // 
            this.tpTolerance.Controls.Add(this.gcTolerance);
            this.tpTolerance.Name = "tpTolerance";
            this.tpTolerance.Size = new System.Drawing.Size(570, 337);
            this.tpTolerance.Text = "Pre- Expiry Date Tolerance Levels";
            // 
            // gcTolerance
            // 
            this.gcTolerance.DataMember = "alter_late_payment_tolerance";
            this.gcTolerance.DataSource = this.dsPaymentTolerance1;
            this.gcTolerance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTolerance.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcTolerance.Location = new System.Drawing.Point(0, 0);
            this.gcTolerance.MainView = this.gvTolerance;
            this.gcTolerance.Name = "gcTolerance";
            this.gcTolerance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDaysPrior});
            this.gcTolerance.Size = new System.Drawing.Size(570, 337);
            this.gcTolerance.TabIndex = 1;
            this.gcTolerance.UseEmbeddedNavigator = true;
            this.gcTolerance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTolerance});
            // 
            // dsPaymentTolerance1
            // 
            this.dsPaymentTolerance1.DataSetName = "dsPaymentTolerance";
            this.dsPaymentTolerance1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvTolerance
            // 
            this.gvTolerance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDaysPrior});
            this.gvTolerance.GridControl = this.gcTolerance;
            this.gvTolerance.Name = "gvTolerance";
            this.gvTolerance.OptionsView.ColumnAutoWidth = false;
            this.gvTolerance.OptionsView.ShowGroupPanel = false;
            this.gvTolerance.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvTolerance_InvalidRowException);
            this.gvTolerance.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvTolerance_ValidateRow);
            // 
            // colDaysPrior
            // 
            this.colDaysPrior.ColumnEdit = this.riDaysPrior;
            this.colDaysPrior.FieldName = "DaysPrior";
            this.colDaysPrior.Name = "colDaysPrior";
            this.colDaysPrior.Visible = true;
            this.colDaysPrior.VisibleIndex = 0;
            this.colDaysPrior.Width = 163;
            // 
            // riDaysPrior
            // 
            this.riDaysPrior.AutoHeight = false;
            this.riDaysPrior.Mask.EditMask = "g0";
            this.riDaysPrior.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riDaysPrior.Mask.UseMaskAsDisplayFormat = true;
            this.riDaysPrior.Name = "riDaysPrior";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcComPur);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(570, 337);
            this.xtraTabPage1.Text = "Communication Purpose";
            // 
            // gcComPur
            // 
            this.gcComPur.DataMember = "alert_land_reminder_com_purpose";
            this.gcComPur.DataSource = this.dsCommPurpose1;
            this.gcComPur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcComPur.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcComPur.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcComPur_EmbeddedNavigator_ButtonClick);
            this.gcComPur.Location = new System.Drawing.Point(0, 0);
            this.gcComPur.MainView = this.gvComPur;
            this.gcComPur.Name = "gcComPur";
            this.gcComPur.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riComPur});
            this.gcComPur.Size = new System.Drawing.Size(570, 337);
            this.gcComPur.TabIndex = 0;
            this.gcComPur.UseEmbeddedNavigator = true;
            this.gcComPur.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComPur});
            // 
            // dsCommPurpose1
            // 
            this.dsCommPurpose1.DataSetName = "dsCommPurpose";
            this.dsCommPurpose1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvComPur
            // 
            this.gvComPur.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCOMM_PUR_ID});
            this.gvComPur.GridControl = this.gcComPur;
            this.gvComPur.Name = "gvComPur";
            this.gvComPur.OptionsView.ColumnAutoWidth = false;
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
            this.colCOMM_PUR_ID.Width = 328;
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
            // tpDocumentMerg
            // 
            this.tpDocumentMerg.Controls.Add(this.gcDocMerge);
            this.tpDocumentMerg.Name = "tpDocumentMerg";
            this.tpDocumentMerg.Size = new System.Drawing.Size(570, 337);
            this.tpDocumentMerg.Text = "Document Merge Library Items";
            // 
            // gcDocMerge
            // 
            this.gcDocMerge.DataSource = this.bsDocMergeLibraryItems;
            this.gcDocMerge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDocMerge.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcDocMerge.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcDocMerge_EmbeddedNavigator_ButtonClick);
            this.gcDocMerge.Location = new System.Drawing.Point(0, 0);
            this.gcDocMerge.MainView = this.gvDocMerge;
            this.gcDocMerge.Name = "gcDocMerge";
            this.gcDocMerge.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDocMerge});
            this.gcDocMerge.Size = new System.Drawing.Size(570, 337);
            this.gcDocMerge.TabIndex = 0;
            this.gcDocMerge.UseEmbeddedNavigator = true;
            this.gcDocMerge.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDocMerge});
            // 
            // dsDocMergeLibraryItems1
            // 
            this.dsDocMergeLibraryItems1.DataSetName = "dsDocMergeLibraryItems";
            this.dsDocMergeLibraryItems1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDocMerge
            // 
            this.gvDocMerge.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDocMergeFunction_ID});
            this.gvDocMerge.GridControl = this.gcDocMerge;
            this.gvDocMerge.Name = "gvDocMerge";
            this.gvDocMerge.OptionsView.ColumnAutoWidth = false;
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
            this.colDocMergeFunction_ID.Width = 302;
            // 
            // riDocMerge
            // 
            this.riDocMerge.AutoHeight = false;
            this.riDocMerge.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDocMerge.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MergeTemplateName", "Library Item")});
            this.riDocMerge.DisplayMember = "MergeTemplateName";
            this.riDocMerge.Name = "riDocMerge";
            this.riDocMerge.NullText = "";
            this.riDocMerge.PopupWidth = 250;
            this.riDocMerge.ValueMember = "DocMergeLibrary_ID";
            // 
            // daCommPurpose
            // 
            this.daCommPurpose.DeleteCommand = this.sqlDeleteCommand1;
            this.daCommPurpose.InsertCommand = this.sqlInsertCommand1;
            this.daCommPurpose.SelectCommand = this.sqlSelectCommand1;
            this.daCommPurpose.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_land_reminder_com_purpose", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AlertID", "AlertID"),
                        new System.Data.Common.DataColumnMapping("COMM_PUR_ID", "COMM_PUR_ID")})});
            this.daCommPurpose.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.sqlConnTR;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_PUR_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_PUR_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlConnTR;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@COMM_PUR_ID", System.Data.SqlDbType.Int, 0, "COMM_PUR_ID")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        ID, AlertID, COMM_PUR_ID\r\nFROM            alert_land_reminder_com_p" +
                "urpose\r\nWHERE        (AlertID = @AlertID)";
            this.sqlSelectCommand1.Connection = this.sqlConnTR;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 4, "AlertID")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlConnTR;
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
            // daDocMergeLibraryItems
            // 
            this.daDocMergeLibraryItems.DeleteCommand = this.sqlDeleteCommand2;
            this.daDocMergeLibraryItems.InsertCommand = this.sqlInsertCommand2;
            this.daDocMergeLibraryItems.SelectCommand = this.sqlSelectCommand2;
            this.daDocMergeLibraryItems.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_land_reminder_docMerge", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AlertID", "AlertID"),
                        new System.Data.Common.DataColumnMapping("DocMergeLibrary_ID", "DocMergeLibrary_ID"),
                        new System.Data.Common.DataColumnMapping("RepDetails", "RepDetails")})});
            this.daDocMergeLibraryItems.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.sqlConnTR;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.sqlConnTR;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, "DocMergeLibrary_ID"),
            new System.Data.SqlClient.SqlParameter("@RepDetails", System.Data.SqlDbType.VarChar, 0, "RepDetails")});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT        ID, AlertID, DocMergeLibrary_ID, RepDetails\r\nFROM            alert_" +
                "land_reminder_docMerge\r\nWHERE        (AlertID = @AlertID)";
            this.sqlSelectCommand2.Connection = this.sqlConnTR;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 4, "AlertID")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.sqlConnTR;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, "DocMergeLibrary_ID"),
            new System.Data.SqlClient.SqlParameter("@RepDetails", System.Data.SqlDbType.VarChar, 0, "RepDetails"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DocMergeLibrary_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DocMergeLibrary_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "SELECT        id, DaysPrior\r\nFROM            alter_late_payment_tolerance";
            this.sqlSelectCommand3.Connection = this.sqlConnTR;
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = "INSERT INTO [alter_late_payment_tolerance] ([DaysPrior]) VALUES (@DaysPrior);\r\nSE" +
                "LECT id, DaysPrior FROM alter_late_payment_tolerance WHERE (id = SCOPE_IDENTITY(" +
                "))";
            this.sqlInsertCommand3.Connection = this.sqlConnTR;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior")});
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.sqlConnTR;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = "DELETE FROM [alter_late_payment_tolerance] WHERE (([id] = @Original_id) AND ((@Is" +
                "Null_DaysPrior = 1 AND [DaysPrior] IS NULL) OR ([DaysPrior] = @Original_DaysPrio" +
                "r)))";
            this.sqlDeleteCommand3.Connection = this.sqlConnTR;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null)});
            // 
            // daPaymentTolerance
            // 
            this.daPaymentTolerance.DeleteCommand = this.sqlDeleteCommand3;
            this.daPaymentTolerance.InsertCommand = this.sqlInsertCommand3;
            this.daPaymentTolerance.SelectCommand = this.sqlSelectCommand3;
            this.daPaymentTolerance.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alter_late_payment_tolerance", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("DaysPrior", "DaysPrior")})});
            this.daPaymentTolerance.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // bsDocMergeLibraryItems
            // 
            this.bsDocMergeLibraryItems.DataMember = "alert_land_reminder_docMerge";
            this.bsDocMergeLibraryItems.DataSource = this.dsDocMergeLibraryItems1;
            // 
            // ucCommPurDocMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "ucCommPurDocMerge";
            this.Size = new System.Drawing.Size(579, 368);
            this.Load += new System.EventHandler(this.ucCommPurDocMerge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpTolerance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPaymentTolerance1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDaysPrior)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcComPur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCommPurpose1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComPur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riComPur)).EndInit();
            this.tpDocumentMerg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDocMergeLibraryItems1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDocMergeLibraryItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.SqlClient.SqlConnection sqlConnTR;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gcComPur;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComPur;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMM_PUR_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riComPur;
        private DevExpress.XtraGrid.GridControl gcDocMerge;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDocMerge;
        private DevExpress.XtraGrid.Columns.GridColumn colDocMergeFunction_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riDocMerge;
        private System.Data.SqlClient.SqlDataAdapter daCommPurpose;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlDataAdapter daDocMergeLibraryItems;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private dsCommPurpose dsCommPurpose1;
        private dsDocMergeLibraryItems dsDocMergeLibraryItems1;
        private DevExpress.XtraGrid.GridControl gcTolerance;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTolerance;
        private DevExpress.XtraGrid.Columns.GridColumn colDaysPrior;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riDaysPrior;
        public DevExpress.XtraTab.XtraTabPage tpTolerance;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
        private System.Data.SqlClient.SqlDataAdapter daPaymentTolerance;
        private dsPaymentTolerance dsPaymentTolerance1;
        public DevExpress.XtraTab.XtraTabPage tpDocumentMerg;
        private System.Windows.Forms.BindingSource bsDocMergeLibraryItems;
    }
}
