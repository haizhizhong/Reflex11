using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Data.SqlClient;
using WS_Popups;
using HMConnection;
using HM_Report_Printer;

namespace AP_Invoice_Entry
{
	public class frmQuickCheck2 : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;
		private int _AP_INV_HEADER_ID;
		private int _Year;
		private int _Period;
        private bool ElectronicSave = false;
        private int ContexItem_ID = -1;
        private bool b_ElectronicSaving = false;
        private bool b_Manual = false;

		#region frmQuickCheck2 Component Variables

		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.TextEdit txtCheckNo;
		private DevExpress.XtraEditors.DateEdit deChkDate;
		public DevExpress.XtraEditors.LookUpEdit lueBank;
		private DevExpress.XtraEditors.LabelControl labelControl5;
		private DevExpress.XtraEditors.LabelControl labelControl4;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private System.Data.SqlClient.SqlDataAdapter daBank;
		private AP_Invoice_Entry.dsBank dsBank1;
		private System.Data.SqlClient.SqlConnection TR_Conn;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		private DevExpress.XtraEditors.SimpleButton btnPrint;
		private AccountingPicker.ucAccountingPicker ucAccountingPicker1;
        private CheckEdit chkIncludeHB;
		private System.ComponentModel.Container components = null;

		#endregion

		public frmQuickCheck2( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AP_INV_HEADER_ID, int Year, int Period )
		{
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			_AP_INV_HEADER_ID = AP_INV_HEADER_ID;
			_Year = Year;
			_Period = Period;
			InitializeComponent();

			Popup = new frmPopup( myMgr );

			TR_Conn.ConnectionString = Connection.TRConnection;

            object obj = Connection.SQLExecutor.ExecuteScalar("select currency_id from AP_INV_HEADER where AP_INV_HEADER_ID =" + AP_INV_HEADER_ID, Connection.TRConnection);
            if (obj == null || obj == DBNull.Value)
                obj = -1;

            daBank.SelectCommand.Parameters["@currency_id"].Value = obj;
			daBank.Fill( dsBank1 );
			deChkDate.DateTime = DateTime.Now.Date;

			ucAccountingPicker1.UserName = Connection.MLUser;
			ucAccountingPicker1.ConnectionString = Connection.TRConnection;

            chkIncludeHB.EditValue = "T";

            CheckHBRouting();

            string sSQL = @"if exists(select * from AP_INV_HEADER where isnull(INVOICE_TYPE,'I')='M' and AP_INV_HEADER_ID=" + _AP_INV_HEADER_ID + @")
	                        select 1
                        else
	                        select 0";
            b_Manual = Convert.ToBoolean(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection));
            if (b_Manual)
            {
                sSQL = @"select MANUAL_CHECK from AP_INV_HEADER where AP_INV_HEADER_ID=" + _AP_INV_HEADER_ID;
                txtCheckNo.Enabled = false;
                txtCheckNo.EditValue = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                btnPrint.Text = "Process";
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuickCheck2));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtCheckNo = new DevExpress.XtraEditors.TextEdit();
            this.deChkDate = new DevExpress.XtraEditors.DateEdit();
            this.lueBank = new DevExpress.XtraEditors.LookUpEdit();
            this.dsBank1 = new AP_Invoice_Entry.dsBank();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.daBank = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.ucAccountingPicker1 = new AccountingPicker.ucAccountingPicker();
            this.chkIncludeHB = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deChkDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deChkDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBank1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeHB.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtCheckNo
            // 
            this.txtCheckNo.Location = new System.Drawing.Point(135, 76);
            this.txtCheckNo.Name = "txtCheckNo";
            this.txtCheckNo.Properties.Mask.EditMask = "g0";
            this.txtCheckNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCheckNo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCheckNo.Size = new System.Drawing.Size(120, 20);
            this.txtCheckNo.TabIndex = 19;
            // 
            // deChkDate
            // 
            this.deChkDate.EditValue = null;
            this.deChkDate.Location = new System.Drawing.Point(95, 44);
            this.deChkDate.Name = "deChkDate";
            this.deChkDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deChkDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deChkDate.Size = new System.Drawing.Size(160, 20);
            this.deChkDate.TabIndex = 18;
            // 
            // lueBank
            // 
            this.lueBank.Location = new System.Drawing.Point(71, 12);
            this.lueBank.Name = "lueBank";
            this.lueBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueBank.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BANK_ID", "BANK_ID", 62, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BANK_NAME", "Bank", 66, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueBank.Properties.DataSource = this.dsBank1.BANK_MASTER;
            this.lueBank.Properties.DisplayMember = "BANK_NAME";
            this.lueBank.Properties.NullText = "";
            this.lueBank.Properties.ValueMember = "BANK_ID";
            this.lueBank.Size = new System.Drawing.Size(184, 20);
            this.lueBank.TabIndex = 17;
            this.lueBank.EditValueChanged += new System.EventHandler(this.lueBank_EditValueChanged);
            // 
            // dsBank1
            // 
            this.dsBank1.DataSetName = "dsBank";
            this.dsBank1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsBank1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(23, 76);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(97, 13);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "First Check Number:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(23, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(59, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Check Date:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(23, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Bank:";
            // 
            // daBank
            // 
            this.daBank.DeleteCommand = this.sqlDeleteCommand1;
            this.daBank.InsertCommand = this.sqlInsertCommand1;
            this.daBank.SelectCommand = this.sqlSelectCommand1;
            this.daBank.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "BANK_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("BANK_ID", "BANK_ID"),
                        new System.Data.Common.DataColumnMapping("BANK_NAME", "BANK_NAME")})});
            this.daBank.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM [BANK_MASTER] WHERE (([BANK_ID] = @Original_BANK_ID) AND ([BANK_NAME]" +
                " = @Original_BANK_NAME))";
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_BANK_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BANK_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_BANK_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BANK_NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [BANK_MASTER] ([BANK_ID], [BANK_NAME]) VALUES (@BANK_ID, @BANK_NAME);" +
                "\r\nSELECT BANK_ID, BANK_NAME FROM BANK_MASTER WHERE (BANK_NAME = @BANK_NAME) ORDE" +
                "R BY BANK_NAME";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@BANK_ID", System.Data.SqlDbType.Int, 0, "BANK_ID"),
            new System.Data.SqlClient.SqlParameter("@BANK_NAME", System.Data.SqlDbType.VarChar, 0, "BANK_NAME")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT BANK_ID, BANK_NAME FROM BANK_MASTER where currency_id=@currency_id ORDER B" +
                "Y BANK_NAME";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@currency_id", System.Data.SqlDbType.Int, 4, "CURRENCY_ID")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@BANK_ID", System.Data.SqlDbType.Int, 0, "BANK_ID"),
            new System.Data.SqlClient.SqlParameter("@BANK_NAME", System.Data.SqlDbType.VarChar, 0, "BANK_NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_BANK_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BANK_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_BANK_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BANK_NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(127, 205);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 20;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ucAccountingPicker1
            // 
            this.ucAccountingPicker1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAccountingPicker1.Appearance.Options.UseBackColor = true;
            this.ucAccountingPicker1.HasEntryDate = false;
            this.ucAccountingPicker1.Location = new System.Drawing.Point(7, 100);
            this.ucAccountingPicker1.Name = "ucAccountingPicker1";
            this.ucAccountingPicker1.SelectedPeriod = 0;
            this.ucAccountingPicker1.SelectedYear = 0;
            this.ucAccountingPicker1.Size = new System.Drawing.Size(184, 64);
            this.ucAccountingPicker1.TabIndex = 21;
            this.ucAccountingPicker1.UserName = "";
            // 
            // chkIncludeHB
            // 
            this.chkIncludeHB.Location = new System.Drawing.Point(21, 169);
            this.chkIncludeHB.Name = "chkIncludeHB";
            this.chkIncludeHB.Properties.Caption = "Include Holdback";
            this.chkIncludeHB.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkIncludeHB.Properties.ValueChecked = "T";
            this.chkIncludeHB.Properties.ValueUnchecked = "F";
            this.chkIncludeHB.Size = new System.Drawing.Size(113, 19);
            this.chkIncludeHB.TabIndex = 32;
            // 
            // frmQuickCheck2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(288, 234);
            this.Controls.Add(this.chkIncludeHB);
            this.Controls.Add(this.ucAccountingPicker1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtCheckNo);
            this.Controls.Add(this.deChkDate);
            this.Controls.Add(this.lueBank);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuickCheck2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Check Print";
            this.Load += new System.EventHandler(this.frmQuickCheck2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCheckNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deChkDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deChkDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsBank1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeHB.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void CheckHBRouting()
        {
            string sSQL = @"if exists( select * from Approval_Topic where ID=71 and active=1 ) --AP Holdback Approval
		            select 'T'
	            else 
		            select 'F'";
            object obj = Connection.SQLExecutor.ExecuteScalar( sSQL, Connection.WebConnection );
            if( obj == null ||obj == DBNull.Value )
                obj = "F";

            if (obj.Equals("T"))
            {
                sSQL = @"declare @valid varchar(1), @ap_inv_header_id int
                    select @valid = 'F', @ap_inv_header_id = "+_AP_INV_HEADER_ID+@"
                    if exists ( select * from AP_INV_HEADER where AP_INV_HEADER_ID = @ap_inv_header_id and ISNULL(FROM_WEB,'F') = 'T' )
                    begin
	                    if exists( select * from WS_INV_HB where AP_INV_HEADER_ID = @ap_inv_header_id )
	                    begin
		                    if exists( select * from WS_INV_HB where AP_INV_HEADER_ID = @ap_inv_header_id and ISNULL(HOLDBACK_STATUS,'') = 'A' )
		                    begin
			                    select @valid = 'T'
		                    end
	                    end	
                    end
                    else
                    begin
	                    if exists ( select * from AP_INV_HEADER where AP_INV_HEADER_ID = @ap_inv_header_id and isnull(WFHB_STATUS,'') = 'A' )
	                    begin
		                    select @valid = 'T'
	                    end
                    end
                    select @valid";
                obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (obj == null || obj == DBNull.Value)
                    obj = "F";

                if (obj.Equals("T"))
                {
                    chkIncludeHB.Enabled = true;
                    chkIncludeHB.EditValue = "T";
                }
                else
                {
                    chkIncludeHB.Enabled = false;
                    chkIncludeHB.EditValue = "F";
                }
            }
        }

		private void lueBank_EditValueChanged(object sender, System.EventArgs e)
		{
            if (b_Manual)
                return;
			object oBank = lueBank.EditValue;
			if( oBank != null )
			{
				string sSelect = "select isnull(NEXT_CHECK_NO,1) from bank_master where bank_id="+oBank;
				object oNo = ExecuteScalar( sSelect, TR_Conn );
				if( oNo == null || oNo == DBNull.Value )
				{
					oNo = 1;
				}
				else
				{
					if( Convert.ToInt32( oNo ) == 0 )
						oNo = 1;
				}

				txtCheckNo.EditValue = oNo;
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

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
            string sSelect = "select count(*) from ap_inv_header where isnull(PAYMENT_HOLD,'F') = 'T' and ap_inv_header_id=" + _AP_INV_HEADER_ID;
            if (Convert.ToInt32(ExecuteScalar(sSelect, TR_Conn)) > 0)
            {
                string sMessage = "Unable to process payment, payment hold currently exists on invoice.";
                Popup.ShowPopup(sMessage);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

			object oBank = lueBank.EditValue;
			if( oBank != null )
			{
				if( txtCheckNo.Text.Trim() != "" )
				{
					if( ( ucAccountingPicker1.SelectedYear < _Year ) || ( ucAccountingPicker1.SelectedYear == _Year && ucAccountingPicker1.SelectedPeriod < _Period ) )
					{
						Popup.ShowPopup( "Selected year and period must be equal to or greater than the invoice accrual year and period." );
						return;
					}

					string ReportName = ExecuteScalar( "select isnull(cheque_report,'') from ap_setup", TR_Conn ).ToString();
					if( ReportName == "" )
					{
						Popup.ShowPopup( "No check report has been specified." );
						return;
					}


					string sFirstChkNo = txtCheckNo.EditValue.ToString();
					string sChkDate = deChkDate.DateTime.ToShortDateString();

                    string sExec = "exec sp_APFillQuickChk '" + Connection.MLUser + "', " + _AP_INV_HEADER_ID + ", '" + chkIncludeHB.EditValue + "'";
					string sMessage = ExecuteScalar( sExec, TR_Conn ).ToString();
					if( sMessage == "OK" )
					{
                        sExec = @"declare @message varchar(max)
                        exec sp_APQuickCheckPrint '" + Connection.MLUser + @"', '" + sChkDate + @"', " + oBank + @", " + sFirstChkNo + @", null, @message output 
                        select @message";
                        object oMessage = Connection.SQLExecutor.ExecuteScalar(sExec, Connection.TRConnection);
                        if (oMessage == null || oMessage == DBNull.Value)
                        {
                            Popup.ShowPopup("Error generating check data.");
                            return;
                        }

                        if (!oMessage.Equals("OK"))
                        {
                            Popup.ShowPopup(oMessage.ToString());
                            return;
                        }

                        string sUpdateMessage = "Update and post manual check to the period: " + ucAccountingPicker1.SelectedYear + " / " +
                            ucAccountingPicker1.SelectedPeriod + ", " + PeriodName(ucAccountingPicker1.SelectedPeriod) + "?";
                        if (!b_Manual)
                        {
                            CheckElectronicSaving();

                            string[,] saParams = new string[2, 1];
                            saParams[0, 0] = "@USERNAME";
                            saParams[1, 0] = Connection.MLUser;
                            frmHM_Report_Printer HMRP = new frmHM_Report_Printer(Connection, myMgr, saParams, ReportName, frmHM_Report_Printer.DBFlavor.TR);

                            sSelect = "select isnull(ALLOW_CHK_PREVIEW,'F') from ap_setup";
                            if (ExecuteScalar(sSelect, TR_Conn).ToString().ToUpper() == "T")
                            {
                                HMRP.ShowPreview = true;
                            }
                            else
                            {
                                HMRP.ShowPreview = false;
                            }

                            HMRP.ElectronicSaving(false, b_ElectronicSaving);
                            HMRP.ElectronicSave += new frmHM_Report_Printer.ElectronicSaveDelegate(HMRP_ElectronicSave);
                            ElectronicSave = false;

                            HMRP.ShowDialog();
                            IDisposable dispose = HMRP;
                            dispose.Dispose();

                            sUpdateMessage = "Printed OK? Continue with update, and post to the period: " + ucAccountingPicker1.SelectedYear + " / " +
                            ucAccountingPicker1.SelectedPeriod + ", " + PeriodName(ucAccountingPicker1.SelectedPeriod) + "?";
                        }

                        if (Popup.ShowPopup(this, sUpdateMessage, frmPopup.PopupType.OK_Cancel) 
							== frmPopup.PopupResult.OK )
						{
							sMessage = ProcessCheck();
							if( sMessage == "OK" )
							{
                                if (!b_Manual)
                                {
                                    UpdateChkNo();
                                    if (ElectronicSave)
                                        Check_ElectronicSave();
                                }
								this.DialogResult = DialogResult.OK;
								this.Close();
							}
							else
							{
								Popup.ShowPopup( sMessage );
							}
						}
					}
					else
					{
                        if (chkIncludeHB.Enabled)
                            Popup.ShowPopup(sMessage);
                        else
                            Popup.ShowPopup("A holdback balance only remains on this invoice, but the holdback has not been approved yet.");
					}					
				}
				else
				{
					Popup.ShowPopup( this, "Please enter a starting check number." );
				}
			}
			else
			{
				Popup.ShowPopup( this, "Please select a bank." );
			}
		}

        private void Check_ElectronicSave()
        {
            string sFilePath = myMgr.CustomLayoutBaseDir + "\\ElecAPCheckPrint\\", sCheckPrintName = "", ReportName = "";

            try
            {
                if (!System.IO.Directory.Exists(sFilePath))
                    System.IO.Directory.CreateDirectory(sFilePath);
            }
            catch { }


            try { ReportName = ExecuteScalar("select isnull(cheque_report,'') from ap_setup", TR_Conn).ToString().Trim(); }
            catch { }

            if (!System.IO.File.Exists(Connection.ReportPath + "\\" + ReportName))
            {
                Popup.ShowPopup("Unable to fined report to save electronicly. ");
                return;
            }

            object oBank = lueBank.EditValue;
            string sFirstChkNo = "", sChkDate = "", sSupplier = "";
            try { sFirstChkNo = txtCheckNo.EditValue.ToString(); }
            catch { }
            try { sChkDate = deChkDate.DateTime.ToShortDateString(); }
            catch { }

            int iBank = 0;
            try { iBank = Convert.ToInt32(oBank); }
            catch { }

            try { sSupplier = Connection.SQLExecutor.ExecuteScalar("Select SUPPLIER From AP_INV_HEADER Where AP_INV_HEADER_ID = " + _AP_INV_HEADER_ID.ToString(), Connection.TRConnection).ToString(); }
            catch { }

            sCheckPrintName = "Check #" + sFirstChkNo + " (" + sSupplier + ")" + ".pdf";

            string[,] saParams = new string[2, 1];
            saParams[0, 0] = "@USERNAME";
            saParams[1, 0] = Connection.MLUser;

            HMBaseReporting.HMReport hmRpt = new HMBaseReporting.HMReport(Connection.ReportPath + "\\" + ReportName, Connection.CompanyServer, Connection.TRDB, saParams, Connection.CompanyID);
            hmRpt.ExportReportToPDF(sFilePath + sCheckPrintName);

            if (System.IO.File.Exists(sFilePath + sCheckPrintName))
            {
                byte[] baFileInfo = null;

                try { baFileInfo = System.IO.File.ReadAllBytes(sFilePath + sCheckPrintName); }
                catch { }

                if (baFileInfo != null)
                {
                    string sInsert = @"
Declare @FileID int, @Supplier_ID int, @AP_PAY_HEADER_ID int

Select @Supplier_ID = SUPPLIER_ID From SUPPLIER_MASTER Where SUPPLIER = '" + sSupplier.ToString() + @"'
Select @AP_PAY_HEADER_ID = AP_PAY_HEADER_ID From AP_PAY_HEADER Where SUPPLIER_CODE = '" + sSupplier + @"' and CHECK_NUMBER = " + sFirstChkNo + @" and BANK_ID = " + iBank.ToString() + @"

Insert Into CFS_FileRepository(FileName,FileData,FileTypeDescription,AddedBy,DateAdded,FileType, InternalOnly, Mime_type)
Select '" + sCheckPrintName + @"', @FileData, 'Adobe Acrobat Document', '" + Connection.MLUser + @"', GETDATE(),'F', IsNull((Select Top 1 IsNull(DA_Internal_Only,0) From COMPANY),0), 'application/pdf'

Select @FileID = SCOPE_IDENTITY()

Insert Into CFS_FileReleations(FileRepository_ID,RelType,RelType_ID,ContextItemID,FileOrigin,Supplier_ID, AP_PAY_HEADER_ID, ElectronicSaveSetup_ID)
Select @FileID, 'APCHK', @AP_PAY_HEADER_ID, " + ContexItem_ID.ToString() + ", 'AP Check', @Supplier_ID,  @AP_PAY_HEADER_ID, 13 ";

                    using (System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(Connection.TRConnection))
                    {
                        System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(sInsert, sqlCon);

                        try
                        {
                            sqlCmd.Parameters.Add("FileData", SqlDbType.VarBinary);
                            sqlCmd.Parameters["FileData"].Value = baFileInfo;
                            sqlCon.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlCon.Close();
                        }
                        catch
                        {
                            if (sqlCmd != null)
                                sqlCmd.Dispose();
                            if (sqlCon != null)
                                sqlCon.Dispose();
                        }
                    }

                }//File Info is not null
            }//Does files exist

            try { System.IO.File.Delete(sFilePath + sCheckPrintName); }
            catch { }
        }

        private void HMRP_ElectronicSave()
        {
            ElectronicSave = true;
        }

        private void CheckElectronicSaving()
        {
            int CFS_ElectronicSaveSetup_ID = 13;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter("select AutoSave,ContextItem_ID from CFS_ElectronicSaveSetup where ID = " + CFS_ElectronicSaveSetup_ID.ToString(), Connection.TRConnection);
            if (Connection.SQLExecutor.Exception != null)
                Popup.ShowPopup("Error checking electronic save:" + Connection.SQLExecutor.Exception.Message);
            else if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    try { ContexItem_ID = Convert.ToInt32(dt.Rows[0]["ContextItem_ID"]); }
                    catch { }
                    if (ContexItem_ID != -1)
                    {
                        try { b_ElectronicSaving = Convert.ToBoolean(dt.Rows[0]["AutoSave"]); }
                        catch { }
                    }
                }
            }
        }

		private void UpdateChkNo()
		{
			string sUpdate = "declare @chkno int "+
				"select @chkno=Max(CHECK_NUMBER)+1 from working_apcheckhead where username='"+Connection.MLUser+"' "+
				"update bank_master set NEXT_CHECK_NO=@chkno where bank_id="+lueBank.EditValue;
			ExecuteNonQuery( sUpdate, TR_Conn );
		}

		private string PeriodName( int period )
		{
			string PeriodName = "";
			string sSelect = "select top 1 datename( M, end_date) from gl_periods where period = "+period;
			object oPeriod = ExecuteScalar( sSelect, TR_Conn );
			if( oPeriod != null && oPeriod != DBNull.Value )
			{
				PeriodName = oPeriod.ToString();
			}
			return PeriodName;
		}

		private string ProcessCheck()
		{
			string sExec = "exec sp_APQuickChk '"+Connection.MLUser+"', "+_AP_INV_HEADER_ID+", 'U', "+ucAccountingPicker1.SelectedPeriod+", "+ucAccountingPicker1.SelectedYear+", "+lueBank.EditValue;
			ExecuteNonQuery( sExec, TR_Conn );

			string sMessage = "";
			object oMessage = ExecuteScalar( "select message from working_ap_qk_ck where username='"+Connection.MLUser+"'", TR_Conn );
			if( oMessage != null )
			{
				sMessage = oMessage.ToString();
			}			
			else
			{
				sMessage = "Unable to process check.";
			}

			return sMessage;
		}

		#region SUBLEGERPROCESS
//		private string ProcessChkSelect()
//		{
//			string _Message = "";
//			string sExec = "BEGIN TRAN "+
//				"declare @subledger_number int, @message varchar(500) "+
//				"set @subledger_number = -1 "+
//				"set @message = '' "+
//				"exec sp_APQuickChkUpdate '"+Connection.MLUser+"', "+ucAccountingPicker1.SelectedYear+", "+ucAccountingPicker1.SelectedPeriod+", "+lueBank.EditValue+", @subledger_number output, @message output, 'T' "+
//				"if( @message = 'OK' ) "+
//				"begin "+
//				"COMMIT TRAN  "+
//				"end "+
//				"else "+ 
//				"begin "+
//				"ROLLBACK TRAN "+
//				"end "+
//				"Select @message, cast(@subledger_number as varchar(10)) ";
//			string SubNo = "";
//			string Message = "";
//			SqlCommand cmd = new SqlCommand( sExec, new SqlConnection( TR_Conn.ConnectionString ) );
//			try
//			{
//				cmd.Connection.Open();
//				using( SqlDataReader reader = cmd.ExecuteReader( ) )
//				{
//					if( reader.Read() )
//					{
//						Message = reader.GetString(0);
//						SubNo = reader.GetString(1);	
//					}
//					reader.Close();					
//				}
//			}
//			catch( Exception ex )
//			{
//				_Message = ex.Message+ex.StackTrace;
//			}
//			finally
//			{
//				cmd.Connection.Close();
//			}
//			if( Message.Trim() == "OK" )						
//				_Message = "OK";	
//			else if( Message.Trim() != "" )
//				_Message = Message.Trim();
//						
//			if( SubNo != "0" && SubNo != "-1" )
//				this.SubledgerNo = SubNo;
//			return _Message;
//		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void frmQuickCheck2_Load(object sender, System.EventArgs e)
		{
			myMgr.FormInit( this );

			string sSelect = "select bank_id from bank_master where bank_default = 'T'";
			object oBankID = ExecuteScalar( sSelect, TR_Conn );
			if( oBankID != null && oBankID != DBNull.Value )
				lueBank.EditValue = oBankID;

			if( _Year != -1 )
				ucAccountingPicker1.SelectedYear = _Year;
			if( _Period != -1 )
				ucAccountingPicker1.SelectedPeriod = _Period;
		}
	}
}

