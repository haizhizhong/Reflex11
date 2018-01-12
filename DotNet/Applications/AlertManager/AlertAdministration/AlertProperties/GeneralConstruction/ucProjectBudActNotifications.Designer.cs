namespace AlertProperties.GeneralConstruction
{
    partial class ucProjectBudActNotifications
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
            this.teThresholdPct = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teThresholdPct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Size = new System.Drawing.Size(478, 541);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.teThresholdPct);
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(478, 49);
            this.panelControl2.TabIndex = 0;
            // 
            // teThresholdPct
            // 
            this.teThresholdPct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.teThresholdPct.Location = new System.Drawing.Point(363, 15);
            this.teThresholdPct.Name = "teThresholdPct";
            this.teThresholdPct.Properties.Mask.EditMask = "n0";
            this.teThresholdPct.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.teThresholdPct.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.teThresholdPct.Size = new System.Drawing.Size(100, 20);
            this.teThresholdPct.TabIndex = 2;
            this.teThresholdPct.EditValueChanged += new System.EventHandler(this.teThresholdPct_EditValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First warning Threshold Percent";
            // 
            // ucProjectBudActNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucProjectBudActNotifications";
            this.Size = new System.Drawing.Size(482, 563);
            this.Load += new System.EventHandler(this.ucProjectBudActNotifications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teThresholdPct.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit teThresholdPct;
    }
}
