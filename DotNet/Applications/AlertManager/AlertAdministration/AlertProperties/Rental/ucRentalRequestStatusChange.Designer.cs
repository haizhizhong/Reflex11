namespace AlertProperties.Rental
{
    partial class ucRentalRequestStatusChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ceEntered = new DevExpress.XtraEditors.CheckEdit();
            this.ceActive = new DevExpress.XtraEditors.CheckEdit();
            this.ceComplete = new DevExpress.XtraEditors.CheckEdit();
            this.ceCanceled = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceEntered.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceComplete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCanceled.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ceCanceled);
            this.panelControl1.Controls.Add(this.ceComplete);
            this.panelControl1.Controls.Add(this.ceActive);
            this.panelControl1.Controls.Add(this.ceEntered);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Size = new System.Drawing.Size(295, 204);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alert to fire when status changes to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Entered";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Active";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Complete";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Canceled";
            // 
            // ceEntered
            // 
            this.ceEntered.Location = new System.Drawing.Point(101, 50);
            this.ceEntered.Name = "ceEntered";
            this.ceEntered.Properties.Caption = "";
            this.ceEntered.Size = new System.Drawing.Size(19, 19);
            this.ceEntered.TabIndex = 5;
            this.ceEntered.EditValueChanged += new System.EventHandler(this.ceEntered_EditValueChanged);
            // 
            // ceActive
            // 
            this.ceActive.Location = new System.Drawing.Point(101, 74);
            this.ceActive.Name = "ceActive";
            this.ceActive.Properties.Caption = "";
            this.ceActive.Size = new System.Drawing.Size(19, 19);
            this.ceActive.TabIndex = 6;
            this.ceActive.EditValueChanged += new System.EventHandler(this.ceActive_EditValueChanged);
            // 
            // ceComplete
            // 
            this.ceComplete.Location = new System.Drawing.Point(101, 97);
            this.ceComplete.Name = "ceComplete";
            this.ceComplete.Properties.Caption = "";
            this.ceComplete.Size = new System.Drawing.Size(19, 19);
            this.ceComplete.TabIndex = 7;
            this.ceComplete.EditValueChanged += new System.EventHandler(this.ceComplete_EditValueChanged);
            // 
            // ceCanceled
            // 
            this.ceCanceled.Location = new System.Drawing.Point(101, 121);
            this.ceCanceled.Name = "ceCanceled";
            this.ceCanceled.Properties.Caption = "";
            this.ceCanceled.Size = new System.Drawing.Size(19, 19);
            this.ceCanceled.TabIndex = 8;
            this.ceCanceled.EditValueChanged += new System.EventHandler(this.ceCanceled_EditValueChanged);
            // 
            // ucRentalRequestStatusChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucRentalRequestStatusChange";
            this.Size = new System.Drawing.Size(299, 226);
            this.Load += new System.EventHandler(this.ucRentalRequestStatusChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceEntered.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceComplete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceCanceled.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit ceCanceled;
        private DevExpress.XtraEditors.CheckEdit ceComplete;
        private DevExpress.XtraEditors.CheckEdit ceActive;
        private DevExpress.XtraEditors.CheckEdit ceEntered;
    }
}
