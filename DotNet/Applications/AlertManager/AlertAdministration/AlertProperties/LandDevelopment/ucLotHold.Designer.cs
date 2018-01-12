namespace AlertProperties.LandDevelopment
{
    partial class ucLotHold
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLotHold));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtTolerance = new DevExpress.XtraEditors.TextEdit();
            this.chkPreExpire = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcTolerance = new DevExpress.XtraGrid.GridControl();
            this.dsTolerance = new AlertProperties.LandDevelopment.dsTolerance();
            this.gvTolerance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDaysPrior = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDaysPrior = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.chkExpiry = new DevExpress.XtraEditors.CheckEdit();
            this.chkOnHold = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daTolerance = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreExpire.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDaysPrior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpiry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Size = new System.Drawing.Size(392, 380);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Controls.Add(this.txtTolerance);
            this.layoutControl1.Controls.Add(this.chkPreExpire);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Controls.Add(this.chkExpiry);
            this.layoutControl1.Controls.Add(this.chkOnHold);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(392, 380);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(180, 97);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Properties.Mask.EditMask = "g0";
            this.txtTolerance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTolerance.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTolerance.Size = new System.Drawing.Size(56, 20);
            this.txtTolerance.StyleController = this.layoutControl1;
            this.txtTolerance.TabIndex = 8;
            this.txtTolerance.EditValueChanged += new System.EventHandler(this.txtTolerance_EditValueChanged);
            this.txtTolerance.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtTolerance_EditValueChanging);
            // 
            // chkPreExpire
            // 
            this.chkPreExpire.Location = new System.Drawing.Point(109, 37);
            this.chkPreExpire.Name = "chkPreExpire";
            this.chkPreExpire.Properties.Caption = "";
            this.chkPreExpire.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkPreExpire.Properties.ValueChecked = "T";
            this.chkPreExpire.Properties.ValueUnchecked = "F";
            this.chkPreExpire.Size = new System.Drawing.Size(277, 19);
            this.chkPreExpire.StyleController = this.layoutControl1;
            this.chkPreExpire.TabIndex = 7;
            this.chkPreExpire.EditValueChanged += new System.EventHandler(this.CHK_Changed);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcTolerance);
            this.groupControl1.Location = new System.Drawing.Point(7, 128);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(379, 246);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Pre-Expiry Date Tolerance Levels";
            // 
            // gcTolerance
            // 
            this.gcTolerance.DataMember = "alert_lot_hold_tolerance";
            this.gcTolerance.DataSource = this.dsTolerance;
            this.gcTolerance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTolerance.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcTolerance_EmbeddedNavigator_ButtonClick);
            this.gcTolerance.Location = new System.Drawing.Point(2, 20);
            this.gcTolerance.MainView = this.gvTolerance;
            this.gcTolerance.Name = "gcTolerance";
            this.gcTolerance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riDaysPrior});
            this.gcTolerance.Size = new System.Drawing.Size(375, 224);
            this.gcTolerance.TabIndex = 0;
            this.gcTolerance.UseEmbeddedNavigator = true;
            this.gcTolerance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTolerance});
            // 
            // dsTolerance
            // 
            this.dsTolerance.DataSetName = "dsTolerance";
            this.dsTolerance.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvTolerance
            // 
            this.gvTolerance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDaysPrior});
            this.gvTolerance.GridControl = this.gcTolerance;
            this.gvTolerance.Name = "gvTolerance";
            this.gvTolerance.OptionsView.ShowGroupPanel = false;
            this.gvTolerance.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvTolerance_RowUpdated);
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
            // 
            // riDaysPrior
            // 
            this.riDaysPrior.AutoHeight = false;
            this.riDaysPrior.Mask.EditMask = "g0";
            this.riDaysPrior.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riDaysPrior.Mask.UseMaskAsDisplayFormat = true;
            this.riDaysPrior.Name = "riDaysPrior";
            this.riDaysPrior.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riDaysPrior_EditValueChanging);
            // 
            // chkExpiry
            // 
            this.chkExpiry.Location = new System.Drawing.Point(89, 67);
            this.chkExpiry.Name = "chkExpiry";
            this.chkExpiry.Properties.Caption = "";
            this.chkExpiry.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkExpiry.Properties.ValueChecked = "T";
            this.chkExpiry.Properties.ValueUnchecked = "F";
            this.chkExpiry.Size = new System.Drawing.Size(297, 19);
            this.chkExpiry.StyleController = this.layoutControl1;
            this.chkExpiry.TabIndex = 5;
            this.chkExpiry.EditValueChanged += new System.EventHandler(this.CHK_Changed);
            // 
            // chkOnHold
            // 
            this.chkOnHold.Location = new System.Drawing.Point(97, 7);
            this.chkOnHold.Name = "chkOnHold";
            this.chkOnHold.Properties.Caption = "";
            this.chkOnHold.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkOnHold.Properties.ValueChecked = "T";
            this.chkOnHold.Properties.ValueUnchecked = "F";
            this.chkOnHold.Size = new System.Drawing.Size(289, 19);
            this.chkOnHold.StyleController = this.layoutControl1;
            this.chkOnHold.TabIndex = 4;
            this.chkOnHold.EditValueChanged += new System.EventHandler(this.CHK_Changed);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(392, 380);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkOnHold;
            this.layoutControlItem1.CustomizationFormText = "Fire On Hold Alert";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(390, 30);
            this.layoutControlItem1.Text = "Fire On Hold Alert";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkExpiry;
            this.layoutControlItem2.CustomizationFormText = "Fire Expiry Alert";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(390, 30);
            this.layoutControlItem2.Text = "Fire Expiry Alert";
            this.layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.groupControl1;
            this.layoutControlItem3.CustomizationFormText = "Tolerance";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 121);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(390, 257);
            this.layoutControlItem3.Text = "Tolerance";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkPreExpire;
            this.layoutControlItem4.CustomizationFormText = "Fire Pre-Expiry Alert";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(390, 30);
            this.layoutControlItem4.Text = "Fire Pre-Expiry Alert";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtTolerance;
            this.layoutControlItem5.CustomizationFormText = "Hold Expiry Reply Tolerance (days)";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(240, 31);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(240, 31);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(390, 31);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Hold Expiry Reply Tolerance (days)";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(168, 13);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        id, DaysPrior\r\nFROM            alert_lot_hold_tolerance\r\nORDER BY D" +
                "aysPrior";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [alert_lot_hold_tolerance] ([DaysPrior]) VALUES (@DaysPrior);\r\nSELECT" +
                " id, DaysPrior FROM alert_lot_hold_tolerance WHERE (id = SCOPE_IDENTITY()) ORDER" +
                " BY DaysPrior";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@DaysPrior", System.Data.SqlDbType.Int, 0, "DaysPrior"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM [alert_lot_hold_tolerance] WHERE (([id] = @Original_id) AND ((@IsNull" +
                "_DaysPrior = 1 AND [DaysPrior] IS NULL) OR ([DaysPrior] = @Original_DaysPrior)))" +
                "";
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DaysPrior", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DaysPrior", System.Data.DataRowVersion.Original, null)});
            // 
            // daTolerance
            // 
            this.daTolerance.DeleteCommand = this.sqlDeleteCommand1;
            this.daTolerance.InsertCommand = this.sqlInsertCommand1;
            this.daTolerance.SelectCommand = this.sqlSelectCommand1;
            this.daTolerance.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_lot_hold_tolerance", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("DaysPrior", "DaysPrior")})});
            this.daTolerance.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // ucLotHold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucLotHold";
            this.Size = new System.Drawing.Size(396, 402);
            this.Load += new System.EventHandler(this.ucLotHold_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreExpire.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDaysPrior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpiry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcTolerance;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTolerance;
        private DevExpress.XtraEditors.CheckEdit chkExpiry;
        private DevExpress.XtraEditors.CheckEdit chkOnHold;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daTolerance;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private dsTolerance dsTolerance;
        private DevExpress.XtraGrid.Columns.GridColumn colDaysPrior;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riDaysPrior;
        private DevExpress.XtraEditors.CheckEdit chkPreExpire;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtTolerance;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;

    }
}
