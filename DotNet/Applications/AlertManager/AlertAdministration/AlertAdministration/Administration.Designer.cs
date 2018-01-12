namespace AlertAdministration
{
    partial class Administration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administration));
            this.gcAlert = new DevExpress.XtraGrid.GridControl();
            this.dsAlertPoint1 = new AlertAdministration.dsAlertPoint();
            this.gvAlert = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAPP_MOD_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riModule = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.applicationModuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsModule1 = new AlertAdministration.dsModule();
            this.colALERT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colALERT_POINT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colREQUIRED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riceAlertEnabled = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colUserException = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colTYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.colProperties = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPropertiesRO = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEmail = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colExEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riExtEmail = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.riProperties = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daAlertPoint = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.WEB_Conn = new System.Data.SqlClient.SqlConnection();
            this.daModule = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertPoint1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riModule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationModuleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsModule1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceAlertEnabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPropertiesRO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riExtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcAlert
            // 
            this.gcAlert.DataMember = "ApplicationAlertPoint";
            this.gcAlert.DataSource = this.dsAlertPoint1;
            this.gcAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAlert.Location = new System.Drawing.Point(0, 0);
            this.gcAlert.MainView = this.gvAlert;
            this.gcAlert.Name = "gcAlert";
            this.gcAlert.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riceAlertEnabled,
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2,
            this.repositoryItemImageComboBox1,
            this.riPropertiesRO,
            this.riProperties,
            this.riModule,
            this.riEmail,
            this.riExtEmail});
            this.gcAlert.Size = new System.Drawing.Size(1063, 531);
            this.gcAlert.TabIndex = 0;
            this.gcAlert.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAlert});
            // 
            // dsAlertPoint1
            // 
            this.dsAlertPoint1.DataSetName = "dsAlertPoint";
            this.dsAlertPoint1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvAlert
            // 
            this.gvAlert.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAPP_MOD_ID,
            this.colALERT,
            this.colALERT_POINT,
            this.colREQUIRED,
            this.colUserException,
            this.colTYPE,
            this.colProperties,
            this.colEmail,
            this.colExEmail});
            this.gvAlert.GridControl = this.gcAlert;
            this.gvAlert.GroupCount = 2;
            this.gvAlert.Name = "gvAlert";
            this.gvAlert.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAlert.OptionsView.ColumnAutoWidth = false;
            this.gvAlert.OptionsView.ShowHorzLines = false;
            this.gvAlert.OptionsView.ShowIndicator = false;
            this.gvAlert.OptionsView.ShowVertLines = false;
            this.gvAlert.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAPP_MOD_ID, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colALERT_POINT, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colALERT, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvAlert.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvAlert_CustomRowCellEdit);
            // 
            // colAPP_MOD_ID
            // 
            this.colAPP_MOD_ID.Caption = "Module";
            this.colAPP_MOD_ID.ColumnEdit = this.riModule;
            this.colAPP_MOD_ID.FieldName = "APP_MOD_ID";
            this.colAPP_MOD_ID.Name = "colAPP_MOD_ID";
            this.colAPP_MOD_ID.OptionsColumn.AllowEdit = false;
            this.colAPP_MOD_ID.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            this.colAPP_MOD_ID.Width = 170;
            // 
            // riModule
            // 
            this.riModule.AutoHeight = false;
            this.riModule.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riModule.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 31, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("displayname", "Module", 69, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riModule.DataSource = this.applicationModuleBindingSource;
            this.riModule.DisplayMember = "displayname";
            this.riModule.Name = "riModule";
            this.riModule.NullText = "";
            this.riModule.ValueMember = "id";
            // 
            // applicationModuleBindingSource
            // 
            this.applicationModuleBindingSource.DataMember = "ApplicationModule";
            this.applicationModuleBindingSource.DataSource = this.dsModule1;
            // 
            // dsModule1
            // 
            this.dsModule1.DataSetName = "dsModule";
            this.dsModule1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colALERT
            // 
            this.colALERT.Caption = "Alert";
            this.colALERT.FieldName = "ALERT";
            this.colALERT.Name = "colALERT";
            this.colALERT.OptionsColumn.AllowEdit = false;
            this.colALERT.Visible = true;
            this.colALERT.VisibleIndex = 0;
            this.colALERT.Width = 252;
            // 
            // colALERT_POINT
            // 
            this.colALERT_POINT.Caption = "Sub Module";
            this.colALERT_POINT.FieldName = "ALERT_POINT";
            this.colALERT_POINT.Name = "colALERT_POINT";
            this.colALERT_POINT.OptionsColumn.AllowEdit = false;
            this.colALERT_POINT.Width = 198;
            // 
            // colREQUIRED
            // 
            this.colREQUIRED.Caption = "Alert Enabled";
            this.colREQUIRED.ColumnEdit = this.riceAlertEnabled;
            this.colREQUIRED.FieldName = "REQUIRED";
            this.colREQUIRED.Name = "colREQUIRED";
            this.colREQUIRED.Visible = true;
            this.colREQUIRED.VisibleIndex = 3;
            this.colREQUIRED.Width = 88;
            // 
            // riceAlertEnabled
            // 
            this.riceAlertEnabled.AutoHeight = false;
            this.riceAlertEnabled.Name = "riceAlertEnabled";
            this.riceAlertEnabled.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riceAlertEnabled.ValueChecked = "T";
            this.riceAlertEnabled.ValueUnchecked = "F";
            this.riceAlertEnabled.EditValueChanged += new System.EventHandler(this.riceAlertEnabled_EditValueChanged);
            // 
            // colUserException
            // 
            this.colUserException.Caption = "User Exceptions";
            this.colUserException.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colUserException.Name = "colUserException";
            this.colUserException.Visible = true;
            this.colUserException.VisibleIndex = 2;
            this.colUserException.Width = 117;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Change Exceptions", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.Click += new System.EventHandler(this.repositoryItemButtonEdit1_Click);
            // 
            // colTYPE
            // 
            this.colTYPE.Caption = "Type";
            this.colTYPE.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colTYPE.FieldName = "TYPE";
            this.colTYPE.Name = "colTYPE";
            this.colTYPE.OptionsColumn.AllowEdit = false;
            this.colTYPE.Visible = true;
            this.colTYPE.VisibleIndex = 1;
            this.colTYPE.Width = 103;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Demand", "D", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Scheduled", "S", 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Lightning 1 16 h p.png");
            this.imageCollection1.Images.SetKeyName(1, "Stopwatch 16 h p.png");
            // 
            // colProperties
            // 
            this.colProperties.Caption = "Properties";
            this.colProperties.ColumnEdit = this.riPropertiesRO;
            this.colProperties.Name = "colProperties";
            this.colProperties.Visible = true;
            this.colProperties.VisibleIndex = 4;
            // 
            // riPropertiesRO
            // 
            this.riPropertiesRO.AutoHeight = false;
            this.riPropertiesRO.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riPropertiesRO.Name = "riPropertiesRO";
            this.riPropertiesRO.ReadOnly = true;
            this.riPropertiesRO.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "Email?";
            this.colEmail.ColumnEdit = this.riEmail;
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 5;
            // 
            // riEmail
            // 
            this.riEmail.AutoHeight = false;
            this.riEmail.Name = "riEmail";
            this.riEmail.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riEmail.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riEmail_EditValueChanging);
            this.riEmail.EditValueChanged += new System.EventHandler(this.riEmail_EditValueChanged);
            // 
            // colExEmail
            // 
            this.colExEmail.Caption = "External Email?";
            this.colExEmail.ColumnEdit = this.riExtEmail;
            this.colExEmail.FieldName = "ExtEmail";
            this.colExEmail.Name = "colExEmail";
            this.colExEmail.Visible = true;
            this.colExEmail.VisibleIndex = 6;
            this.colExEmail.Width = 94;
            // 
            // riExtEmail
            // 
            this.riExtEmail.AutoHeight = false;
            this.riExtEmail.Name = "riExtEmail";
            this.riExtEmail.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riExtEmail.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riExtEmail_EditValueChanging);
            this.riExtEmail.EditValueChanged += new System.EventHandler(this.riExtEmail_EditValueChanged);
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.DataSource = this.applicationModuleBindingSource;
            this.repositoryItemGridLookUpEdit1.DisplayMember = "displayname";
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.ValueMember = "id";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Change Exception", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // riProperties
            // 
            this.riProperties.AutoHeight = false;
            this.riProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riProperties.Name = "riProperties";
            this.riProperties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riProperties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riProperties_ButtonClick);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=DEV12;Initial Catalog=TR_Test2;Persist Security Info=True;User ID=hms" +
                "qlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daAlertPoint
            // 
            this.daAlertPoint.SelectCommand = this.sqlSelectCommand1;
            this.daAlertPoint.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ApplicationAlertPoint", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("APP_MOD_ID", "APP_MOD_ID"),
                        new System.Data.Common.DataColumnMapping("ALERT", "ALERT"),
                        new System.Data.Common.DataColumnMapping("ALERT_POINT", "ALERT_POINT"),
                        new System.Data.Common.DataColumnMapping("ID1", "ID1"),
                        new System.Data.Common.DataColumnMapping("REQUIRED", "REQUIRED"),
                        new System.Data.Common.DataColumnMapping("TYPE", "TYPE"),
                        new System.Data.Common.DataColumnMapping("has_properties", "has_properties"),
                        new System.Data.Common.DataColumnMapping("Email", "Email"),
                        new System.Data.Common.DataColumnMapping("ExtEmail", "ExtEmail")})});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select id, displayname from ApplicationModule";
            this.sqlSelectCommand2.Connection = this.WEB_Conn;
            // 
            // WEB_Conn
            // 
            this.WEB_Conn.ConnectionString = "Data Source=CSM-2005\\SQL2008;Initial Catalog=WEB_SUMMIT_V10;Persist Security Info" +
                "=True;User ID=hmsqlsa;Password=hmsqlsa";
            this.WEB_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daModule
            // 
            this.daModule.SelectCommand = this.sqlSelectCommand2;
            this.daModule.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "ApplicationModule", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("displayname", "displayname")})});
            // 
            // Administration
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcAlert);
            this.Name = "Administration";
            this.Size = new System.Drawing.Size(1063, 531);
            this.Load += new System.EventHandler(this.Administration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertPoint1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riModule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationModuleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsModule1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riceAlertEnabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPropertiesRO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riExtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcAlert;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAlert;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daAlertPoint;
        private dsAlertPoint dsAlertPoint1;
        private DevExpress.XtraGrid.Columns.GridColumn colAPP_MOD_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colALERT;
        private DevExpress.XtraGrid.Columns.GridColumn colALERT_POINT;
        private DevExpress.XtraGrid.Columns.GridColumn colREQUIRED;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riceAlertEnabled;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlConnection WEB_Conn;
        private System.Data.SqlClient.SqlDataAdapter daModule;
        private System.Windows.Forms.BindingSource applicationModuleBindingSource;
        private dsModule dsModule1;
        private DevExpress.XtraGrid.Columns.GridColumn colUserException;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colTYPE;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn colProperties;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riPropertiesRO;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riProperties;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riModule;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colExEmail;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riExtEmail;
    }
}
