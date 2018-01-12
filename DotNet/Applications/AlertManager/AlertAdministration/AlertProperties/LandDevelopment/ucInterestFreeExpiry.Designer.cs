namespace AlertProperties.LandDevelopment
{
    partial class ucInterestFreeExpiry
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDays = new DevExpress.XtraEditors.TextEdit();
            this.pGrid = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pGrid);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Size = new System.Drawing.Size(541, 460);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.txtDays);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(541, 95);
            this.panelControl2.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(69, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(426, 49);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Send notification if the purchaser is a builder. The current date is X number of " +
                "days prior to the \'Interest Adjustment Date\', and the balance on the lot sale is" +
                " greater than zero.";
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(222, 59);
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
            // pGrid
            // 
            this.pGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGrid.Location = new System.Drawing.Point(0, 95);
            this.pGrid.Name = "pGrid";
            this.pGrid.Size = new System.Drawing.Size(541, 365);
            this.pGrid.TabIndex = 16;
            // 
            // ucIntrestFreeExpiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucIntrestFreeExpiry";
            this.Size = new System.Drawing.Size(545, 482);
            this.Load += new System.EventHandler(this.ucIntrestFreeExpiry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDays;
        private DevExpress.XtraEditors.PanelControl pGrid;
    }
}
