using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using System.Data.SqlClient;

namespace AP_Unpaid_Invoices
{
	public class frmManChk : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;	
		private SqlConnection TR_Conn;

		private int _ManChk = -1;
        private string _Supplier = "";
        private int _AP_INV_HEADER_ID = -1;

		#region frmManChk Component Variables

		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.SimpleButton btnSave;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.TextEdit txtManChk;
		private System.ComponentModel.Container components = null;

		#endregion

		public frmManChk( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, string sSupplier, int iAP_INV_HEADER_ID )
		{
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			this.Popup = new frmPopup( myMgr );
            _Supplier = sSupplier;
            _AP_INV_HEADER_ID = iAP_INV_HEADER_ID;
			InitializeComponent();
			TR_Conn = new SqlConnection( Connection.TRConnection );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManChk));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.txtManChk = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtManChk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Size = new System.Drawing.Size(304, 78);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.txtManChk);
            this.xtraTabPage1.Controls.Add(this.labelControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(295, 69);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // txtManChk
            // 
            this.txtManChk.Location = new System.Drawing.Point(120, 24);
            this.txtManChk.Name = "txtManChk";
            this.txtManChk.Properties.Mask.EditMask = "g0";
            this.txtManChk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtManChk.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtManChk.Size = new System.Drawing.Size(136, 20);
            this.txtManChk.TabIndex = 1;
            this.txtManChk.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtManChk_EditValueChanging);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Manual Check #";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 78);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(304, 40);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(126, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(176, 36);
            this.panelControl2.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(96, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmManChk
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(304, 118);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManChk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Check";
            this.Load += new System.EventHandler(this.frmManChk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtManChk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void frmManChk_Load(object sender, System.EventArgs e)
		{
			myMgr.FormInit( this );
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
            string sSelect = "select count(*) from ap_inv_header where isnull(PAYMENT_HOLD,'F') = 'T' and ap_inv_header_id=" + _AP_INV_HEADER_ID;
            if (Convert.ToInt32(ExecuteScalar(sSelect, TR_Conn)) > 0)
            {
                string sMessage = "Unable to process manual check, payment hold currently exists on invoice.";
                Popup.ShowPopup(sMessage);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

			if( txtManChk.EditValue != null )
			{
				_ManChk = Convert.ToInt32( txtManChk.EditValue );
                sSelect = @"exec AP_Validate_ManChk " + _ManChk + @", '" + _Supplier + @"'";
                int iResult = Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection));
                if (iResult == -1)
                {
                    Popup.ShowPopup( this, "Check number has already been used.");
                    return;
                }
                else if (iResult == -2)
                {
                    Popup.ShowPopup( this, "Manual check number has already been used for a different supplier.");
                    return;
                }
                else if (iResult == -3)
                {
                    Popup.ShowPopup( this, "Manual check number must be greater than zero.");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
			}			
			else
			{
				_ManChk = -1;
				Popup.ShowPopup( this, "Please enter a check number." );
			}			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			_ManChk = -1;
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		public int ManualCheck
		{
			get{ return _ManChk; }set{ _ManChk = value; }
		}

		private void ExecuteNonQuery( string CmdText, SqlConnection Conn )
		{
			SqlConnection Connection = new SqlConnection( Conn.ConnectionString );
			SqlCommand cmd = new SqlCommand( CmdText, Connection );
			try
			{				
				Connection.Open();
				cmd.ExecuteNonQuery();
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show(  ex.Message + ex.StackTrace );
			}
			finally
			{
				Connection.Close();
			}
		}

		private object ExecuteScalar( string CmdText, SqlConnection Conn )
		{
			SqlConnection Connection = new SqlConnection( Conn.ConnectionString );
			object obj = null;
			SqlCommand cmd = new SqlCommand( CmdText, Connection );
			try
			{		
				Connection.Open();
				obj = cmd.ExecuteScalar();
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show( ex.Message + ex.StackTrace );
			}
			finally
			{
				Connection.Close();
			}
			if( obj != null )
			{
				if( obj.GetType() == typeof( DBNull ) )
					obj = null;
			}
			return obj;
		}

        private void txtManChk_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            object obj = e.NewValue;
            if (obj != null && obj != DBNull.Value)
            {
                if (Convert.ToInt32(obj) <= 0)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
       
	}
}

