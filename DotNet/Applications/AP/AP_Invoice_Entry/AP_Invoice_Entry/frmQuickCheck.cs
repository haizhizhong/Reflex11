using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Data.SqlClient;
using WS_Popups;
using HMConnection;

namespace AP_Invoice_Entry
{
	public class frmQuickCheck : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;
		private SqlConnection TR_Conn;
		private int _AP_INV_HEADER_ID;
		private int _Year;
		private int _Period;

		#region frmQuickCheck Component Variables

		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private AccountingPicker.ucAccountingPicker ucAccountingPicker1;
		private System.ComponentModel.Container components = null;

		#endregion

		public frmQuickCheck( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AP_INV_HEADER_ID, int Year, int Period )
		{
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			_AP_INV_HEADER_ID = AP_INV_HEADER_ID;
			_Year = Year;
			_Period = Period;
			InitializeComponent();

			Popup = new frmPopup( myMgr );

			TR_Conn = new SqlConnection( Connection.TRConnection );

			ucAccountingPicker1.UserName = Connection.MLUser;
			ucAccountingPicker1.ConnectionString = Connection.TRConnection;	

			ucAccountingPicker1.setDefaults();          
		}

		public int Year
		{
			get{ return _Year; }
		}

		public int Period
		{
			get{ return _Period; }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuickCheck));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ucAccountingPicker1 = new AccountingPicker.ucAccountingPicker();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(224, 104);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(304, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(271, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Invoice will be accrued to this year and period. Proceed?";
            // 
            // ucAccountingPicker1
            // 
            this.ucAccountingPicker1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAccountingPicker1.Appearance.Options.UseBackColor = true;
            this.ucAccountingPicker1.HasEntryDate = false;
            this.ucAccountingPicker1.LLayout = AccountingPicker.ucAccountingPicker.enmLayout.Horizontal;
            this.ucAccountingPicker1.Location = new System.Drawing.Point(16, 48);
            this.ucAccountingPicker1.Name = "ucAccountingPicker1";
            this.ucAccountingPicker1.ReadOnly = true;
            this.ucAccountingPicker1.SelectedPeriod = 0;
            this.ucAccountingPicker1.SelectedYear = 0;
            this.ucAccountingPicker1.Size = new System.Drawing.Size(352, 32);
            this.ucAccountingPicker1.TabIndex = 3;
            this.ucAccountingPicker1.UserName = "";
            // 
            // frmQuickCheck
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(384, 134);
            this.Controls.Add(this.ucAccountingPicker1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuickCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Check";
            this.Load += new System.EventHandler(this.frmQuickCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
            string sSelect = "select count(*) from ap_inv_header where isnull(HOLD,'F') = 'T' and ap_inv_header_id=" + _AP_INV_HEADER_ID;
            if (Convert.ToInt32(ExecuteScalar(sSelect, TR_Conn)) > 0)
            {
                string sMessage = "Unable to accrue invoice, invoice is currently on hold.";
                Popup.ShowPopup(sMessage);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

			if( ( ucAccountingPicker1.SelectedYear < _Year ) || ( ucAccountingPicker1.SelectedYear == _Year && ucAccountingPicker1.SelectedPeriod < _Period ) )
			{
				Popup.ShowPopup( "Selected year and period must be equal to or greater than the invoice year and period." );
				return;
			}

			if( Popup.ShowPopup( "Are you sure you want to process this invoice?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
			{
				string sExec = "exec sp_APQuickChk '"+Connection.MLUser+"', "+_AP_INV_HEADER_ID+", 'I', "+ucAccountingPicker1.SelectedPeriod+", "+ucAccountingPicker1.SelectedYear+", -1";
				ExecuteNonQuery( sExec, TR_Conn );

				string sMessage = "";
				object oMessage = ExecuteScalar( "select message from working_ap_qk_ck where username='"+Connection.MLUser+"'", TR_Conn );
				if( oMessage != null )
				{
					sMessage = oMessage.ToString();
				}				

				if( sMessage == "OK" )
				{
					sSelect = "select count(*) from ap_inv_header where isnull(accrual_flag,'U') = 'A' and ap_inv_header_id="+_AP_INV_HEADER_ID;
					if( Convert.ToInt32( ExecuteScalar( sSelect, TR_Conn ) ) == 0 )
						sMessage = "Unable to accrue invoice.";
				}
				
				if( sMessage != "OK" )
				{
					Popup.ShowPopup( sMessage );
					this.DialogResult = DialogResult.Cancel;
				}
				else
				{
					_Year = ucAccountingPicker1.SelectedYear;
					_Period = ucAccountingPicker1.SelectedPeriod;
					this.DialogResult = DialogResult.OK;
				}
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void frmQuickCheck_Load(object sender, System.EventArgs e)
		{
            myMgr.FormInit(this);
            ucAccountingPicker1.SelectedYear = _Year;
            ucAccountingPicker1.SelectedPeriod = _Period;  
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
	}
}

