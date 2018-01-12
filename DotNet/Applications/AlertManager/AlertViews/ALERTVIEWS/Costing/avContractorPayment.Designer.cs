namespace AlertViews.Costing
{
    partial class avContractorPayment
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
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.ucAPInvDet1 = new AlertViews.Costing.ucAPInvDet();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Size = new System.Drawing.Size(992, 291);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 317);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(992, 6);
            this.splitterControl1.TabIndex = 12;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.ucAPInvDet1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 323);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(992, 408);
            this.groupControl2.TabIndex = 13;
            this.groupControl2.Text = "Submission Details";
            // 
            // ucAPInvDet1
            // 
            this.ucAPInvDet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAPInvDet1.Location = new System.Drawing.Point(2, 20);
            this.ucAPInvDet1.Name = "ucAPInvDet1";
            this.ucAPInvDet1.Size = new System.Drawing.Size(988, 386);
            this.ucAPInvDet1.TabIndex = 11;
            this.ucAPInvDet1.WorkFlowApproval = false;
            // 
            // avContractorPayment
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitterControl1);
            this.Name = "avContractorPayment";
            this.Size = new System.Drawing.Size(992, 753);
            this.Load += new System.EventHandler(this.avContractorPayment_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucAPInvDet ucAPInvDet1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
    }
}
