namespace AlertProperties.Rental
{
    partial class ucRentalStartNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRentalStartNotification));
            this.grpPreAlerts = new DevExpress.XtraEditors.GroupControl();
            this.gcPreAlerts = new DevExpress.XtraGrid.GridControl();
            this.gvPreAlerts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaysPrior = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riteNumber0 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ceRentalPreStart = new DevExpress.XtraEditors.CheckEdit();
            this.ceRentalStart = new DevExpress.XtraEditors.CheckEdit();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlTRConnection = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daRentalStartPreAlerts = new System.Data.SqlClient.SqlDataAdapter();
            this.dsRentalStartPreAlerts1 = new AlertProperties.dsRentalStartPreAlerts();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPreAlerts)).BeginInit();
            this.grpPreAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPreAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPreAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteNumber0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRentalPreStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRentalStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRentalStartPreAlerts1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.grpPreAlerts);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.ceRentalPreStart);
            this.panelControl1.Controls.Add(this.ceRentalStart);
            this.panelControl1.Size = new System.Drawing.Size(336, 280);
            // 
            // grpPreAlerts
            // 
            this.grpPreAlerts.Controls.Add(this.gcPreAlerts);
            this.grpPreAlerts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpPreAlerts.Location = new System.Drawing.Point(0, 106);
            this.grpPreAlerts.Name = "grpPreAlerts";
            this.grpPreAlerts.Size = new System.Drawing.Size(336, 174);
            this.grpPreAlerts.TabIndex = 9;
            this.grpPreAlerts.Text = "Rental Start Pre Alerts";
            // 
            // gcPreAlerts
            // 
            this.gcPreAlerts.DataMember = "Alert_rental_start_pre";
            this.gcPreAlerts.DataSource = this.dsRentalStartPreAlerts1;
            this.gcPreAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gcPreAlerts.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcPreAlerts_EmbeddedNavigator_ButtonClick);
            this.gcPreAlerts.Location = new System.Drawing.Point(2, 20);
            this.gcPreAlerts.MainView = this.gvPreAlerts;
            this.gcPreAlerts.Name = "gcPreAlerts";
            this.gcPreAlerts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riteNumber0});
            this.gcPreAlerts.Size = new System.Drawing.Size(332, 152);
            this.gcPreAlerts.TabIndex = 0;
            this.gcPreAlerts.UseEmbeddedNavigator = true;
            this.gcPreAlerts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPreAlerts});
            // 
            // gvPreAlerts
            // 
            this.gvPreAlerts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colDaysPrior});
            this.gvPreAlerts.GridControl = this.gcPreAlerts;
            this.gvPreAlerts.Name = "gvPreAlerts";
            this.gvPreAlerts.OptionsView.ShowGroupPanel = false;
            this.gvPreAlerts.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvPreAlerts_InitNewRow);
            this.gvPreAlerts.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvPreAlerts_RowUpdated);
            this.gvPreAlerts.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvPreAlerts_InvalidRowException);
            this.gvPreAlerts.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvPreAlerts_ValidateRow);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            this.colID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colDaysPrior
            // 
            this.colDaysPrior.ColumnEdit = this.riteNumber0;
            this.colDaysPrior.FieldName = "DaysPrior";
            this.colDaysPrior.Name = "colDaysPrior";
            this.colDaysPrior.Visible = true;
            this.colDaysPrior.VisibleIndex = 0;
            // 
            // riteNumber0
            // 
            this.riteNumber0.AutoHeight = false;
            this.riteNumber0.Mask.EditMask = "n0";
            this.riteNumber0.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riteNumber0.Mask.UseMaskAsDisplayFormat = true;
            this.riteNumber0.Name = "riteNumber0";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(135, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Send Rental Start Pre Alerts";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(111, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Send Rental Start Alert";
            // 
            // ceRentalPreStart
            // 
            this.ceRentalPreStart.Location = new System.Drawing.Point(167, 45);
            this.ceRentalPreStart.Name = "ceRentalPreStart";
            this.ceRentalPreStart.Properties.Caption = "";
            this.ceRentalPreStart.Size = new System.Drawing.Size(20, 19);
            this.ceRentalPreStart.TabIndex = 6;
            this.ceRentalPreStart.EditValueChanged += new System.EventHandler(this.ceRentalPreStart_EditValueChanged);
            // 
            // ceRentalStart
            // 
            this.ceRentalStart.Location = new System.Drawing.Point(167, 20);
            this.ceRentalStart.Name = "ceRentalStart";
            this.ceRentalStart.Properties.Caption = "";
            this.ceRentalStart.Size = new System.Drawing.Size(20, 19);
            this.ceRentalStart.TabIndex = 5;
            this.ceRentalStart.EditValueChanged += new System.EventHandler(this.ceRentalStart_EditValueChanged);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        ID, DaysPrior\r\nFROM            Alert_rental_start_pre";
            this.sqlSelectCommand1.Connection = this.sqlTRConnection;
            // 
            // sqlTRConnection
            // 
            this.sqlTRConnection.ConnectionString = "Data Source=DEVSQL2008R2;Initial Catalog=TR_DEV_GC;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.sqlTRConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [Alert_rental_start_pre] ([DaysPrior]) VALUES (@DaysPrior);\r\nSELECT I" +
                "D, DaysPrior FROM Alert_rental_start_pre WHERE (ID = SCOPE_IDENTITY())";
            this.sqlInsertCommand1.Connection = this.sqlTRConnection;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlTRConnection;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM [Alert_rental_start_pre] WHERE (([ID] = @Original_ID) AND ((@IsNull_D" +
                "aysPrior = 1 AND [DaysPrior] IS NULL) OR ([DaysPrior] = @Original_DaysPrior)))";
            this.sqlDeleteCommand1.Connection = this.sqlTRConnection;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null)});
            // 
            // daRentalStartPreAlerts
            // 
            this.daRentalStartPreAlerts.DeleteCommand = this.sqlDeleteCommand1;
            this.daRentalStartPreAlerts.InsertCommand = this.sqlInsertCommand1;
            this.daRentalStartPreAlerts.SelectCommand = this.sqlSelectCommand1;
            this.daRentalStartPreAlerts.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "Alert_rental_start_pre", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("DaysPrior", "DaysPrior")})});
            this.daRentalStartPreAlerts.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // dsRentalStartPreAlerts1
            // 
            this.dsRentalStartPreAlerts1.DataSetName = "dsRentalStartPreAlerts";
            this.dsRentalStartPreAlerts1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ucRentalStartNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucRentalStartNotification";
            this.Size = new System.Drawing.Size(340, 302);
            this.Load += new System.EventHandler(this.ucRentalStartNotification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpPreAlerts)).EndInit();
            this.grpPreAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPreAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPreAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riteNumber0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRentalPreStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceRentalStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRentalStartPreAlerts1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpPreAlerts;
        private DevExpress.XtraGrid.GridControl gcPreAlerts;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPreAlerts;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colDaysPrior;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riteNumber0;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit ceRentalPreStart;
        private DevExpress.XtraEditors.CheckEdit ceRentalStart;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlTRConnection;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daRentalStartPreAlerts;
        private dsRentalStartPreAlerts dsRentalStartPreAlerts1;

    }
}
