namespace AlertProperties
{
    partial class ucPropertyInterface
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
            this.gTitle = new DevExpress.XtraEditors.GroupControl();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gTitle)).BeginInit();
            this.gTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gTitle
            // 
            this.gTitle.Controls.Add(this.panelControl1);
            this.gTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gTitle.Location = new System.Drawing.Point(0, 0);
            this.gTitle.Name = "gTitle";
            this.gTitle.Size = new System.Drawing.Size(599, 507);
            this.gTitle.TabIndex = 0;
            this.gTitle.Text = "Alert Point Name Properties";
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(595, 485);
            this.panelControl1.TabIndex = 0;
            // 
            // ucPropertyInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gTitle);
            this.Name = "ucPropertyInterface";
            this.Size = new System.Drawing.Size(599, 507);
            this.Load += new System.EventHandler(this.ucPropertyInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gTitle)).EndInit();
            this.gTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.SqlClient.SqlConnection TR_Conn;
        protected DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl gTitle;
    }
}
