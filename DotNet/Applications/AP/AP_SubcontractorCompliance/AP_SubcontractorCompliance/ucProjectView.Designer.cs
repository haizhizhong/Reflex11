namespace AP_SubcontractorCompliance
{
    partial class ucProjectView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjectView));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daProjectView = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daType = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daInsertUpdate = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.dsProjectView1 = new AP_SubcontractorCompliance.dsProjectView();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlCommand4 = new System.Data.SqlClient.SqlCommand();
            this.dsSupplier1 = new AP_SubcontractorCompliance.dsSupplier();
            this.dsType1 = new AP_SubcontractorCompliance.dsType();
            this.bsSupplier = new System.Windows.Forms.BindingSource(this.components);
            this.bsType = new System.Windows.Forms.BindingSource(this.components);
            this.gcProjView = new DevExpress.XtraGrid.GridControl();
            this.gvProjView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gbandSupplier = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSupplier = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riSupp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gbandDetails = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colCode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riTypeCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riTypeDesc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colPO = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFrequency = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colactive = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colExpiry = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbandExpiryRules = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colrule_restrict = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colrule_holdback_release = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbandWebRules = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colwebrule_internal_alert = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colwebrule_warn_contractor = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colwebrule_restrict_payment_req = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbandDocumentReference = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riSharepoint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.riDirectAttachment = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProjectView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSupp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSharepoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDirectAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=csmsql2016;Initial Catalog=TR_KELLER_DEV;Persist Security Info=True;U" +
    "ser ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daProjectView
            // 
            this.daProjectView.SelectCommand = this.sqlSelectCommand2;
            this.daProjectView.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SUBCON_COMP_SEARCH", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("supplier_id", "supplier_id"),
                        new System.Data.Common.DataColumnMapping("insur_type_id", "insur_type_id"),
                        new System.Data.Common.DataColumnMapping("expiry_date", "expiry_date"),
                        new System.Data.Common.DataColumnMapping("rule_warning", "rule_warning"),
                        new System.Data.Common.DataColumnMapping("rule_accrual", "rule_accrual"),
                        new System.Data.Common.DataColumnMapping("rule_payment", "rule_payment"),
                        new System.Data.Common.DataColumnMapping("rule_po", "rule_po"),
                        new System.Data.Common.DataColumnMapping("webrule_internal_alert", "webrule_internal_alert"),
                        new System.Data.Common.DataColumnMapping("webrule_warn_contractor", "webrule_warn_contractor"),
                        new System.Data.Common.DataColumnMapping("webrule_restrict_payment_req", "webrule_restrict_payment_req"),
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("active", "active"),
                        new System.Data.Common.DataColumnMapping("project_required", "project_required"),
                        new System.Data.Common.DataColumnMapping("Frequency", "Frequency"),
                        new System.Data.Common.DataColumnMapping("rule_restrict", "rule_restrict"),
                        new System.Data.Common.DataColumnMapping("rule_holdback_release", "rule_holdback_release"),
                        new System.Data.Common.DataColumnMapping("PO", "PO")})});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "dbo.AP_SUBCON_COMP_SEARCH";
            this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            this.sqlSelectCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@supplier_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@supplier", System.Data.SqlDbType.VarChar, 10),
            new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.VarChar, 40),
            new System.Data.SqlClient.SqlParameter("@insur_type_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@insur_type_id2", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@expiry_from", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@expiry_to", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4)});
            // 
            // daType
            // 
            this.daType.DeleteCommand = this.sqlDeleteCommand1;
            this.daType.SelectCommand = this.sqlSelectCommand1;
            this.daType.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_SUBCON_INSUR_TYPE_FREQUENCY", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("code", "code"),
                        new System.Data.Common.DataColumnMapping("description", "description"),
                        new System.Data.Common.DataColumnMapping("frequency_id", "frequency_id"),
                        new System.Data.Common.DataColumnMapping("project_Required", "project_Required"),
                        new System.Data.Common.DataColumnMapping("Frequency", "Frequency")})});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_code", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_description", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_description", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "description", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // daInsertUpdate
            // 
            this.daInsertUpdate.SelectCommand = this.sqlSelectCommand3;
            this.daInsertUpdate.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SUBCON_COMP_IU", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id")})});
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "dbo.AP_SUBCON_COMP_IU";
            this.sqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand3.Connection = this.TR_Conn;
            this.sqlSelectCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@supplier_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@insur_type_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@expiry_date", System.Data.SqlDbType.DateTime, 8),
            new System.Data.SqlClient.SqlParameter("@rule_warning", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_accrual", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_payment", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_po", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_internal_alert", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_warn_contractor", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@webrule_restrict_payment_req", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@active", System.Data.SqlDbType.Bit, 1),
            new System.Data.SqlClient.SqlParameter("@rule_restrict", System.Data.SqlDbType.VarChar, 1),
            new System.Data.SqlClient.SqlParameter("@rule_holdback_release", System.Data.SqlDbType.VarChar, 1)});
            // 
            // dsProjectView1
            // 
            this.dsProjectView1.DataSetName = "dsProjectView";
            this.dsProjectView1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlCommand1;
            this.daSupplier.InsertCommand = this.sqlCommand2;
            this.daSupplier.SelectCommand = this.sqlCommand3;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER_ID", "SUPPLIER_ID"),
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daSupplier.UpdateCommand = this.sqlCommand4;
            // 
            // sqlCommand1
            // 
            this.sqlCommand1.CommandText = resources.GetString("sqlCommand1.CommandText");
            this.sqlCommand1.Connection = this.TR_Conn;
            this.sqlCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlCommand2
            // 
            this.sqlCommand2.CommandText = "INSERT INTO [SUPPLIER_MASTER] ([SUPPLIER_ID], [SUPPLIER], [NAME]) VALUES (@SUPPLI" +
    "ER_ID, @SUPPLIER, @NAME);\r\nSELECT SUPPLIER_ID, SUPPLIER, NAME FROM SUPPLIER_MAST" +
    "ER WHERE (SUPPLIER = @SUPPLIER)";
            this.sqlCommand2.Connection = this.TR_Conn;
            this.sqlCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME")});
            // 
            // sqlCommand3
            // 
            this.sqlCommand3.CommandText = "select SUPPLIER_ID, SUPPLIER, NAME from SUPPLIER_MASTER where ISNULL(subcontracto" +
    "r,\'F\') = \'T\'";
            this.sqlCommand3.Connection = this.TR_Conn;
            // 
            // sqlCommand4
            // 
            this.sqlCommand4.CommandText = resources.GetString("sqlCommand4.CommandText");
            this.sqlCommand4.Connection = this.TR_Conn;
            this.sqlCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // dsSupplier1
            // 
            this.dsSupplier1.DataSetName = "dsSupplier";
            this.dsSupplier1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsType1
            // 
            this.dsType1.DataSetName = "dsType";
            this.dsType1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsSupplier
            // 
            this.bsSupplier.DataMember = "SUPPLIER_MASTER";
            this.bsSupplier.DataSource = this.dsSupplier1;
            // 
            // bsType
            // 
            this.bsType.DataMember = "SUPPLIER_SUBCON_INSUR_TYPE_FREQUENCY";
            this.bsType.DataSource = this.dsType1;
            // 
            // gcProjView
            // 
            this.gcProjView.DataMember = "AP_SUBCON_COMP_SEARCH";
            this.gcProjView.DataSource = this.dsProjectView1;
            this.gcProjView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProjView.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcSearch_EmbeddedNavigator_ButtonClick);
            this.gcProjView.Location = new System.Drawing.Point(0, 0);
            this.gcProjView.MainView = this.gvProjView;
            this.gcProjView.Name = "gcProjView";
            this.gcProjView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riCheck,
            this.riSharepoint,
            this.riSupp,
            this.riName,
            this.riTypeCode,
            this.riTypeDesc,
            this.riDirectAttachment});
            this.gcProjView.Size = new System.Drawing.Size(1764, 765);
            this.gcProjView.TabIndex = 2;
            this.gcProjView.UseEmbeddedNavigator = true;
            this.gcProjView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjView});
            // 
            // gvProjView
            // 
            this.gvProjView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gbandSupplier,
            this.gbandDetails,
            this.gbandExpiryRules,
            this.gbandWebRules,
            this.gbandDocumentReference});
            this.gvProjView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSupplier,
            this.bandedGridColumn2,
            this.colCode,
            this.bandedGridColumn4,
            this.bandedGridColumn5,
            this.bandedGridColumn1,
            this.colExpiry,
            this.bandedGridColumn7,
            this.colrule_restrict,
            this.bandedGridColumn8,
            this.bandedGridColumn9,
            this.bandedGridColumn10,
            this.colwebrule_internal_alert,
            this.colwebrule_warn_contractor,
            this.colwebrule_restrict_payment_req,
            this.colactive,
            this.colFrequency,
            this.colrule_holdback_release,
            this.colPO});
            this.gvProjView.CustomizationFormBounds = new System.Drawing.Rectangle(1455, 721, 225, 235);
            this.gvProjView.GridControl = this.gcProjView;
            this.gvProjView.Name = "gvProjView";
            this.gvProjView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colExpiry, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.bandedGridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvProjView.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvProjView_InitNewRow);
            this.gvProjView.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvProjView_InvalidRowException);
            this.gvProjView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvProjView_ValidateRow);
            this.gvProjView.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvProjView_RowUpdated);
            // 
            // gbandSupplier
            // 
            this.gbandSupplier.Caption = "Supplier";
            this.gbandSupplier.Columns.Add(this.colSupplier);
            this.gbandSupplier.Columns.Add(this.bandedGridColumn2);
            this.gbandSupplier.MinWidth = 20;
            this.gbandSupplier.Name = "gbandSupplier";
            this.gbandSupplier.VisibleIndex = 0;
            this.gbandSupplier.Width = 316;
            // 
            // colSupplier
            // 
            this.colSupplier.Caption = "Supplier";
            this.colSupplier.ColumnEdit = this.riSupp;
            this.colSupplier.FieldName = "supplier_id";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.Visible = true;
            this.colSupplier.Width = 111;
            // 
            // riSupp
            // 
            this.riSupp.AutoHeight = false;
            this.riSupp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSupp.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER_ID", "SUPPLIER_ID", 87, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riSupp.DataSource = this.bsSupplier;
            this.riSupp.DisplayMember = "SUPPLIER";
            this.riSupp.Name = "riSupp";
            this.riSupp.NullText = "";
            this.riSupp.PopupWidth = 250;
            this.riSupp.ValueMember = "SUPPLIER_ID";
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Name";
            this.bandedGridColumn2.ColumnEdit = this.riName;
            this.bandedGridColumn2.FieldName = "supplier_id";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 205;
            // 
            // riName
            // 
            this.riName.AutoHeight = false;
            this.riName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riName.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER_ID", "SUPPLIER_ID", 87, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riName.DataSource = this.bsSupplier;
            this.riName.DisplayMember = "NAME";
            this.riName.Name = "riName";
            this.riName.NullText = "";
            this.riName.PopupWidth = 250;
            this.riName.ValueMember = "SUPPLIER_ID";
            // 
            // gbandDetails
            // 
            this.gbandDetails.Caption = "Details";
            this.gbandDetails.Columns.Add(this.colCode);
            this.gbandDetails.Columns.Add(this.bandedGridColumn4);
            this.gbandDetails.Columns.Add(this.colPO);
            this.gbandDetails.Columns.Add(this.colFrequency);
            this.gbandDetails.Columns.Add(this.colactive);
            this.gbandDetails.Columns.Add(this.colExpiry);
            this.gbandDetails.MinWidth = 20;
            this.gbandDetails.Name = "gbandDetails";
            this.gbandDetails.VisibleIndex = 1;
            this.gbandDetails.Width = 537;
            // 
            // colCode
            // 
            this.colCode.Caption = "Code";
            this.colCode.ColumnEdit = this.riTypeCode;
            this.colCode.FieldName = "insur_type_id";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.Width = 92;
            // 
            // riTypeCode
            // 
            this.riTypeCode.AutoHeight = false;
            this.riTypeCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTypeCode.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riTypeCode.DataSource = this.bsType;
            this.riTypeCode.DisplayMember = "code";
            this.riTypeCode.Name = "riTypeCode";
            this.riTypeCode.NullText = "";
            this.riTypeCode.PopupWidth = 250;
            this.riTypeCode.ValueMember = "id";
            this.riTypeCode.EditValueChanged += new System.EventHandler(this.ComplianceChange_EditValueChanged);
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "Description";
            this.bandedGridColumn4.ColumnEdit = this.riTypeDesc;
            this.bandedGridColumn4.FieldName = "insur_type_id";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 187;
            // 
            // riTypeDesc
            // 
            this.riTypeDesc.AutoHeight = false;
            this.riTypeDesc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTypeDesc.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("code", "Code", 33, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("description", "Description", 62, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riTypeDesc.DataSource = this.bsType;
            this.riTypeDesc.DisplayMember = "description";
            this.riTypeDesc.Name = "riTypeDesc";
            this.riTypeDesc.NullText = "";
            this.riTypeDesc.PopupWidth = 250;
            this.riTypeDesc.ValueMember = "id";
            this.riTypeDesc.EditValueChanged += new System.EventHandler(this.ComplianceChange_EditValueChanged);
            // 
            // colPO
            // 
            this.colPO.FieldName = "PO";
            this.colPO.Name = "colPO";
            this.colPO.OptionsColumn.AllowEdit = false;
            this.colPO.Visible = true;
            this.colPO.Width = 96;
            // 
            // colFrequency
            // 
            this.colFrequency.FieldName = "Frequency";
            this.colFrequency.Name = "colFrequency";
            this.colFrequency.OptionsColumn.AllowEdit = false;
            this.colFrequency.OptionsColumn.ShowInCustomizationForm = false;
            this.colFrequency.Visible = true;
            this.colFrequency.Width = 108;
            // 
            // colactive
            // 
            this.colactive.Caption = "Complete";
            this.colactive.FieldName = "active";
            this.colactive.Name = "colactive";
            this.colactive.Visible = true;
            this.colactive.Width = 54;
            // 
            // colExpiry
            // 
            this.colExpiry.Caption = "Expiry";
            this.colExpiry.FieldName = "expiry_date";
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // gbandExpiryRules
            // 
            this.gbandExpiryRules.Caption = "Expiry Rules";
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn7);
            this.gbandExpiryRules.Columns.Add(this.colrule_restrict);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn8);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn9);
            this.gbandExpiryRules.Columns.Add(this.colrule_holdback_release);
            this.gbandExpiryRules.Columns.Add(this.bandedGridColumn10);
            this.gbandExpiryRules.MinWidth = 20;
            this.gbandExpiryRules.Name = "gbandExpiryRules";
            this.gbandExpiryRules.VisibleIndex = 2;
            this.gbandExpiryRules.Width = 550;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.Caption = "Warning Message";
            this.bandedGridColumn7.ColumnEdit = this.riCheck;
            this.bandedGridColumn7.FieldName = "rule_warning";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 109;
            // 
            // riCheck
            // 
            this.riCheck.AutoHeight = false;
            this.riCheck.Name = "riCheck";
            this.riCheck.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riCheck.ValueChecked = "T";
            this.riCheck.ValueUnchecked = "F";
            // 
            // colrule_restrict
            // 
            this.colrule_restrict.Caption = "Restrict";
            this.colrule_restrict.ColumnEdit = this.riCheck;
            this.colrule_restrict.FieldName = "rule_restrict";
            this.colrule_restrict.Name = "colrule_restrict";
            this.colrule_restrict.Visible = true;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.Caption = "Pre-Accrual";
            this.bandedGridColumn8.ColumnEdit = this.riCheck;
            this.bandedGridColumn8.FieldName = "rule_accrual";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 80;
            // 
            // bandedGridColumn9
            // 
            this.bandedGridColumn9.Caption = "Payment Hold";
            this.bandedGridColumn9.ColumnEdit = this.riCheck;
            this.bandedGridColumn9.FieldName = "rule_payment";
            this.bandedGridColumn9.Name = "bandedGridColumn9";
            this.bandedGridColumn9.Visible = true;
            this.bandedGridColumn9.Width = 96;
            // 
            // colrule_holdback_release
            // 
            this.colrule_holdback_release.Caption = "Holdback Release";
            this.colrule_holdback_release.ColumnEdit = this.riCheck;
            this.colrule_holdback_release.FieldName = "rule_holdback_release";
            this.colrule_holdback_release.Name = "colrule_holdback_release";
            this.colrule_holdback_release.Visible = true;
            this.colrule_holdback_release.Width = 106;
            // 
            // bandedGridColumn10
            // 
            this.bandedGridColumn10.Caption = "PO Approval";
            this.bandedGridColumn10.ColumnEdit = this.riCheck;
            this.bandedGridColumn10.FieldName = "rule_po";
            this.bandedGridColumn10.Name = "bandedGridColumn10";
            this.bandedGridColumn10.Visible = true;
            this.bandedGridColumn10.Width = 84;
            // 
            // gbandWebRules
            // 
            this.gbandWebRules.Caption = "Web Rules";
            this.gbandWebRules.Columns.Add(this.colwebrule_internal_alert);
            this.gbandWebRules.Columns.Add(this.colwebrule_warn_contractor);
            this.gbandWebRules.Columns.Add(this.colwebrule_restrict_payment_req);
            this.gbandWebRules.MinWidth = 20;
            this.gbandWebRules.Name = "gbandWebRules";
            this.gbandWebRules.VisibleIndex = 3;
            this.gbandWebRules.Width = 383;
            // 
            // colwebrule_internal_alert
            // 
            this.colwebrule_internal_alert.Caption = "Internal Alert";
            this.colwebrule_internal_alert.ColumnEdit = this.riCheck;
            this.colwebrule_internal_alert.FieldName = "webrule_internal_alert";
            this.colwebrule_internal_alert.Name = "colwebrule_internal_alert";
            this.colwebrule_internal_alert.Visible = true;
            this.colwebrule_internal_alert.Width = 118;
            // 
            // colwebrule_warn_contractor
            // 
            this.colwebrule_warn_contractor.Caption = "Warn Contractor";
            this.colwebrule_warn_contractor.ColumnEdit = this.riCheck;
            this.colwebrule_warn_contractor.FieldName = "webrule_warn_contractor";
            this.colwebrule_warn_contractor.Name = "colwebrule_warn_contractor";
            this.colwebrule_warn_contractor.Visible = true;
            this.colwebrule_warn_contractor.Width = 118;
            // 
            // colwebrule_restrict_payment_req
            // 
            this.colwebrule_restrict_payment_req.Caption = "Restrict Payment Request";
            this.colwebrule_restrict_payment_req.ColumnEdit = this.riCheck;
            this.colwebrule_restrict_payment_req.FieldName = "webrule_restrict_payment_req";
            this.colwebrule_restrict_payment_req.Name = "colwebrule_restrict_payment_req";
            this.colwebrule_restrict_payment_req.Visible = true;
            this.colwebrule_restrict_payment_req.Width = 147;
            // 
            // gbandDocumentReference
            // 
            this.gbandDocumentReference.Caption = "Document Reference";
            this.gbandDocumentReference.Columns.Add(this.bandedGridColumn5);
            this.gbandDocumentReference.Columns.Add(this.bandedGridColumn1);
            this.gbandDocumentReference.MinWidth = 20;
            this.gbandDocumentReference.Name = "gbandDocumentReference";
            this.gbandDocumentReference.VisibleIndex = 4;
            this.gbandDocumentReference.Width = 300;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.Caption = "Sharepoint";
            this.bandedGridColumn5.ColumnEdit = this.riSharepoint;
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 150;
            // 
            // riSharepoint
            // 
            this.riSharepoint.AutoHeight = false;
            this.riSharepoint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Sharepoint", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, ((System.Drawing.Image)(resources.GetObject("riSharepoint.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, false)});
            this.riSharepoint.Name = "riSharepoint";
            this.riSharepoint.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riSharepoint.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riSharepoint_ButtonClick);
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Direct Attachment";
            this.bandedGridColumn1.ColumnEdit = this.riDirectAttachment;
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 150;
            // 
            // riDirectAttachment
            // 
            this.riDirectAttachment.AutoHeight = false;
            this.riDirectAttachment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Direct Attachment", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, false)});
            this.riDirectAttachment.Name = "riDirectAttachment";
            this.riDirectAttachment.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riDirectAttachment.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riDirectAttachment_ButtonClick);
            // 
            // ucProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcProjView);
            this.Name = "ucProjectView";
            this.Size = new System.Drawing.Size(1764, 765);
            this.Load += new System.EventHandler(this.ucProjectView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsProjectView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSupp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTypeDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSharepoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDirectAttachment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daProjectView;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlDataAdapter daType;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlDataAdapter daInsertUpdate;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
        private dsProjectView dsProjectView1;
        private System.Data.SqlClient.SqlDataAdapter daSupplier;
        private System.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Data.SqlClient.SqlCommand sqlCommand2;
        private System.Data.SqlClient.SqlCommand sqlCommand3;
        private System.Data.SqlClient.SqlCommand sqlCommand4;
        private dsSupplier dsSupplier1;
        private dsType dsType1;
        private System.Windows.Forms.BindingSource bsSupplier;
        private System.Windows.Forms.BindingSource bsType;
        private DevExpress.XtraGrid.GridControl gcProjView;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gvProjView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSupplier;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSupp;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riTypeCode;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riTypeDesc;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colExpiry;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riCheck;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_internal_alert;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_warn_contractor;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colwebrule_restrict_payment_req;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riSharepoint;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riDirectAttachment;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colactive;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFrequency;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colrule_restrict;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colrule_holdback_release;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandSupplier;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPO;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandExpiryRules;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandWebRules;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbandDocumentReference;
    }
}
