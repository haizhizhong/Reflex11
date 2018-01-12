using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using System.Data.SqlClient;
using APGSTException;

namespace AP_Invoice_Entry
{
	public class frmGSTExcept : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;
		public ucAPGSTException ucAPGSTE;
		private SqlConnection TR_Conn;

		#region frmGSTExcept Component Variables

		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraEditors.SimpleButton btnSave;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private System.ComponentModel.Container components = null;

		#endregion

		public frmGSTExcept( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr )
		{
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			Popup = new frmPopup( myMgr );
			InitializeComponent();
			TR_Conn = new SqlConnection( Connection.TRConnection );

			ucAPGSTE = new ucAPGSTException( Connection, myMgr );
			ucAPGSTE.Parent = xtraTabPage1;
			ucAPGSTE.Dock = DockStyle.Fill;
			ucAPGSTE.Readonly = false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmGSTExcept));
			this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
			this.xtraTabControl1.SuspendLayout();
			this.xtraTabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
			this.panelControl2.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControl1
			// 
			this.xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl1.Name = "xtraTabControl1";
			this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
			this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControl1.Size = new System.Drawing.Size(288, 238);
			this.xtraTabControl1.TabIndex = 0;
			this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
																							this.xtraTabPage1});
			this.xtraTabControl1.Text = "xtraTabControl1";
			// 
			// xtraTabPage1
			// 
			this.xtraTabPage1.Controls.Add(this.panelControl1);
			this.xtraTabPage1.Name = "xtraTabPage1";
			this.xtraTabPage1.Size = new System.Drawing.Size(279, 229);
			this.xtraTabPage1.Text = "xtraTabPage1";
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.panelControl2);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelControl1.Location = new System.Drawing.Point(0, 189);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(279, 40);
			this.panelControl1.TabIndex = 0;
			this.panelControl1.Text = "panelControl1";
			// 
			// panelControl2
			// 
			this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl2.Controls.Add(this.btnCancel);
			this.panelControl2.Controls.Add(this.btnSave);
			this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelControl2.Location = new System.Drawing.Point(85, 2);
			this.panelControl2.Name = "panelControl2";
			this.panelControl2.Size = new System.Drawing.Size(192, 36);
			this.panelControl2.TabIndex = 1;
			this.panelControl2.Text = "panelControl2";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(104, 8);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(16, 8);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// frmGSTExcept
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(288, 238);
			this.ControlBox = false;
			this.Controls.Add(this.xtraTabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmGSTExcept";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GST Exception";
			this.Load += new System.EventHandler(this.frmGSTExcept_Load);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
			this.xtraTabControl1.ResumeLayout(false);
			this.xtraTabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
			this.panelControl2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmGSTExcept_Load(object sender, System.EventArgs e)
		{
			myMgr.FormInit( this );
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			bool Closable = true;
			string sSelect = "select isnull(other_gst_exceptions,'F') from ap_setup";
			object obj = ExecuteScalar( sSelect, TR_Conn );
			if( obj != null && obj != DBNull.Value )
			{
				if( obj.ToString().ToUpper() == "T" && ucAPGSTE.radException.SelectedIndex == 4 )
				{
					if( ucAPGSTE.lueOther.EditValue == null || ucAPGSTE.lueOther.EditValue == DBNull.Value )
					{
						Popup.ShowPopup( this, "Other GST Exception required before saving." );
						Closable = false;
					}
				}
			}
			if( Closable )
			{
				ucAPGSTE.Readonly = true;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
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

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			ucAPGSTE.Readonly = true;
			this.DialogResult = DialogResult.Cancel;
		}
	}
}

