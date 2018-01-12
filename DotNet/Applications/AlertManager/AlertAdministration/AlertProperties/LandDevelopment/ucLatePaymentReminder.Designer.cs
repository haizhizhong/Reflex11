namespace AlertProperties.LandDevelopment
{
    partial class ucLatePaymentReminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLatePaymentReminder));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtRecurrMonths = new DevExpress.XtraEditors.TextEdit();
            this.dsAlertSetup1 = new AlertProperties.dsAlertSetup();
            this.txtRecurrDays = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDays = new DevExpress.XtraEditors.TextEdit();
            this.pcGrid = new DevExpress.XtraEditors.PanelControl();
            this.sqlConnTR = new System.Data.SqlClient.SqlConnection();
            this.daAlertSetup = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecurrMonths.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertSetup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecurrDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pcGrid);
            this.panelControl1.Controls.Add(this.panelControl2);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtDays);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(595, 261);
            this.panelControl2.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(265, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(156, 221);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtRecurrMonths);
            this.groupControl1.Controls.Add(this.txtRecurrDays);
            this.groupControl1.Location = new System.Drawing.Point(95, 20);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(395, 100);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Notification Recurrence Cycle";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(197, 68);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(35, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Months";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(197, 36);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Days";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(75, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Every";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(75, 35);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Every";
            // 
            // txtRecurrMonths
            // 
            this.txtRecurrMonths.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsAlertSetup1, "alert_land_reminder_setup.Recurrence_Months", true));
            this.txtRecurrMonths.Location = new System.Drawing.Point(109, 67);
            this.txtRecurrMonths.Name = "txtRecurrMonths";
            this.txtRecurrMonths.Properties.Mask.EditMask = "g0";
            this.txtRecurrMonths.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRecurrMonths.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRecurrMonths.Size = new System.Drawing.Size(82, 20);
            this.txtRecurrMonths.TabIndex = 1;
            this.txtRecurrMonths.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtRecurrMonths_EditValueChanging);
            // 
            // dsAlertSetup1
            // 
            this.dsAlertSetup1.DataSetName = "dsAlertSetup";
            this.dsAlertSetup1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtRecurrDays
            // 
            this.txtRecurrDays.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsAlertSetup1, "alert_land_reminder_setup.Recurrence_Days", true));
            this.txtRecurrDays.Location = new System.Drawing.Point(109, 33);
            this.txtRecurrDays.Name = "txtRecurrDays";
            this.txtRecurrDays.Properties.Mask.EditMask = "g0";
            this.txtRecurrDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRecurrDays.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRecurrDays.Size = new System.Drawing.Size(82, 20);
            this.txtRecurrDays.TabIndex = 0;
            this.txtRecurrDays.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtRecurrDays_EditValueChanging);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(95, 126);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(420, 49);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Send Notification if Current Date is X days past Expected Payout Date and balance" +
                " on Lot Sale is greater than zero.";
            // 
            // txtDays
            // 
            this.txtDays.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsAlertSetup1, "alert_land_reminder_setup.X_Days", true));
            this.txtDays.Location = new System.Drawing.Point(204, 181);
            this.txtDays.Name = "txtDays";
            this.txtDays.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDays.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtDays.Properties.Mask.EditMask = "g0";
            this.txtDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDays.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDays.Size = new System.Drawing.Size(82, 20);
            this.txtDays.TabIndex = 8;
            this.txtDays.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtDays_EditValueChanging);
            // 
            // pcGrid
            // 
            this.pcGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcGrid.Location = new System.Drawing.Point(0, 261);
            this.pcGrid.Name = "pcGrid";
            this.pcGrid.Size = new System.Drawing.Size(595, 224);
            this.pcGrid.TabIndex = 13;
            // 
            // sqlConnTR
            // 
            this.sqlConnTR.ConnectionString = "Data Source=DEV12;Initial Catalog=TR_Test;Persist Security Info=True;User ID=hmsq" +
                "lsa;Password=hmsqlsa";
            this.sqlConnTR.FireInfoMessageEventOnUserErrors = false;
            // 
            // daAlertSetup
            // 
            this.daAlertSetup.DeleteCommand = this.sqlDeleteCommand1;
            this.daAlertSetup.InsertCommand = this.sqlInsertCommand1;
            this.daAlertSetup.SelectCommand = this.sqlSelectCommand1;
            this.daAlertSetup.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "alert_land_reminder_setup", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ID", "ID"),
                        new System.Data.Common.DataColumnMapping("AlertID", "AlertID"),
                        new System.Data.Common.DataColumnMapping("X_Days", "X_Days"),
                        new System.Data.Common.DataColumnMapping("Recurrence_Days", "Recurrence_Days"),
                        new System.Data.Common.DataColumnMapping("Recurrence_Months", "Recurrence_Months")})});
            this.daAlertSetup.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.sqlConnTR;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_X_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "X_Days", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_X_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "X_Days", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Recurrence_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Recurrence_Days", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Recurrence_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Recurrence_Days", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Recurrence_Months", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Recurrence_Months", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Recurrence_Months", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Recurrence_Months", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlConnTR;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@X_Days", System.Data.SqlDbType.Int, 0, "X_Days"),
            new System.Data.SqlClient.SqlParameter("@Recurrence_Days", System.Data.SqlDbType.Int, 0, "Recurrence_Days"),
            new System.Data.SqlClient.SqlParameter("@Recurrence_Months", System.Data.SqlDbType.Int, 0, "Recurrence_Months")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        ID, AlertID, X_Days, Recurrence_Days, Recurrence_Months\r\nFROM      " +
                "      alert_land_reminder_setup\r\nWHERE        (AlertID = 43)";
            this.sqlSelectCommand1.Connection = this.sqlConnTR;
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlConnTR;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AlertID", System.Data.SqlDbType.Int, 0, "AlertID"),
            new System.Data.SqlClient.SqlParameter("@X_Days", System.Data.SqlDbType.Int, 0, "X_Days"),
            new System.Data.SqlClient.SqlParameter("@Recurrence_Days", System.Data.SqlDbType.Int, 0, "Recurrence_Days"),
            new System.Data.SqlClient.SqlParameter("@Recurrence_Months", System.Data.SqlDbType.Int, 0, "Recurrence_Months"),
            new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AlertID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AlertID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_X_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "X_Days", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_X_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "X_Days", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Recurrence_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Recurrence_Days", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Recurrence_Days", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Recurrence_Days", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Recurrence_Months", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Recurrence_Months", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Recurrence_Months", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Recurrence_Months", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID")});
            // 
            // ucLatePaymentReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucLatePaymentReminder";
            this.Load += new System.EventHandler(this.ucLatePaymentReminder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecurrMonths.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAlertSetup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecurrDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDays;
        private DevExpress.XtraEditors.PanelControl pcGrid;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtRecurrMonths;
        private DevExpress.XtraEditors.TextEdit txtRecurrDays;
        private System.Data.SqlClient.SqlConnection sqlConnTR;
        private System.Data.SqlClient.SqlDataAdapter daAlertSetup;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private dsAlertSetup dsAlertSetup1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}
