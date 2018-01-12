using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WS_Popups;
using HMConnection;
using System.Data.SqlClient;

namespace AP_Shell
{
	public class frmAP : DevExpress.XtraEditors.XtraForm
	{
		private System.ComponentModel.Container components = null;
		private ucAP.ucAP AP;
		private WS_Popups.frmPopup Popup;
		private HMCon Connection;

		public frmAP( string WebDB, string Server, string CompanyID, string User )
		{
			TUC_HMDevXManager.TUC_HMDevXManager DevXMgr = new TUC_HMDevXManager.TUC_HMDevXManager();
			DevXMgr.AppInit( User );
			Connection = new HMCon( WebDB, Server, Convert.ToInt32( CompanyID ), User );
			Popup = new WS_Popups.frmPopup(DevXMgr);
			InitializeComponent();
			AP = new ucAP.ucAP( WebDB, Server, CompanyID, User, DevXMgr );
			AP.Dock = DockStyle.Fill;
			AP.Parent = this;
		}

		#region Windows Form Designer generated code
		[STAThread]
		static void Main( string[] args ) 
		{			
			if( args.Length == 4 )
			{
				Application.Run(new frmAP( args[0], args[1], args[2], args[3] ));
			}
		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAP));
			// 
			// frmAP
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(992, 774);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmAP";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Accounts Payable";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmAP_Closing);
			this.Load += new System.EventHandler(this.frmAP_Load);

		}
		#endregion

		private void frmAP_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if( Popup.ShowPopup( "Close Accounts Payable Application?", frmPopup.PopupType.Yes_No ) == frmPopup.PopupResult.Yes ) 
				e.Cancel = false;
			else
				e.Cancel = true;
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

		private void frmAP_Load(object sender, System.EventArgs e)
		{
			string sSelect = "select Company_name from companies where autoid="+Connection.CompanyID;
			object oCompanyName = ExecuteScalar( sSelect, new SqlConnection( Connection.WebConnection ) );
			if( oCompanyName != null && oCompanyName != DBNull.Value )
				this.Text += " - "+oCompanyName;
		}
	}
}

