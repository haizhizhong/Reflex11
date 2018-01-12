namespace AlertViews
{
    partial class avChat
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Size = new System.Drawing.Size(992, 350);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl2.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl2.Size = new System.Drawing.Size(975, 315);
            this.layoutControl2.Controls.SetChildIndex(this.mNotes, 0);
            // 
            // mNotes
            // 
            this.mNotes.Size = new System.Drawing.Size(962, 231);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Size = new System.Drawing.Size(967, 274);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Size = new System.Drawing.Size(969, 329);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 376);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(992, 6);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // avChat
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitterControl1);
            this.Name = "avChat";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterControl1;
    }
}
