using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;

namespace AP_Invoice_Entry
{
	public class ucSuppChange : DevExpress.XtraEditors.XtraForm
	{
		private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
		private HMCon Connection;	

		#region ucSuppChange Component Variables

		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraEditors.SimpleButton btnUpdate;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.LookUpEdit luePO;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LookUpEdit lueSupplier;
		private System.Data.SqlClient.SqlDataAdapter daSupplier;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		private System.Data.SqlClient.SqlConnection TR_Conn;
		private AP_Invoice_Entry.dsSupplier1 dsSupplier11;
		private System.Data.SqlClient.SqlDataAdapter daPO;
		private AP_Invoice_Entry.dsPO1 dsPO11;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		private System.ComponentModel.Container components = null;

		#endregion

		public ucSuppChange(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
		{
			this.Connection = Connection;
			this.DevXMgr = DevXMgr;
			InitializeComponent();
			RunSetups();
		}

		private void RunSetups()
		{
			TR_Conn.ConnectionString = Connection.TRConnection;
			Popup = new frmPopup( DevXMgr );	
			daSupplier.Fill( dsSupplier11 );
			daPO.Fill( dsPO11 );
		}


		#region Component Designer generated code
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSuppChange));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lueSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.dsSupplier11 = new AP_Invoice_Entry.dsSupplier1();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.luePO = new DevExpress.XtraEditors.LookUpEdit();
            this.dsPO11 = new AP_Invoice_Entry.dsPO1();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPO11)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(360, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(280, 8);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 144);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(440, 40);
            this.panelControl1.TabIndex = 4;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lueSupplier);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.luePO);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(440, 144);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Tag = "";
            this.groupControl1.Text = "Change Supplier on PO";
            // 
            // lueSupplier
            // 
            this.lueSupplier.Location = new System.Drawing.Point(16, 112);
            this.lueSupplier.Name = "lueSupplier";
            this.lueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 66, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueSupplier.Properties.DataSource = this.dsSupplier11.SUPPLIER_MASTER;
            this.lueSupplier.Properties.DisplayMember = "SUPPLIER";
            this.lueSupplier.Properties.NullText = "";
            this.lueSupplier.Properties.PopupWidth = 300;
            this.lueSupplier.Properties.ValueMember = "SUPPLIER";
            this.lueSupplier.Size = new System.Drawing.Size(232, 20);
            this.lueSupplier.TabIndex = 3;
            // 
            // dsSupplier11
            // 
            this.dsSupplier11.DataSetName = "dsSupplier1";
            this.dsSupplier11.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsSupplier11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(107, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Replacement Supplier:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(156, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Select PO to change supplier on:";
            // 
            // luePO
            // 
            this.luePO.Location = new System.Drawing.Point(16, 56);
            this.luePO.Name = "luePO";
            this.luePO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePO.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_ID", "PO_ID", 50, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO", "PO #", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ORDER_DATE", "PO Date", 73, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Supplier", 53, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPP_NAME", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.luePO.Properties.DataSource = this.dsPO11.PO_HEADER;
            this.luePO.Properties.DisplayMember = "PO";
            this.luePO.Properties.NullText = "";
            this.luePO.Properties.PopupWidth = 300;
            this.luePO.Properties.ValueMember = "PO_ID";
            this.luePO.Size = new System.Drawing.Size(416, 20);
            this.luePO.TabIndex = 0;
            // 
            // dsPO11
            // 
            this.dsPO11.DataSetName = "dsPO1";
            this.dsPO11.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPO11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlDeleteCommand1;
            this.daSupplier.InsertCommand = this.sqlInsertCommand1;
            this.daSupplier.SelectCommand = this.sqlSelectCommand1;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daSupplier.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM SUPPLIER_MASTER WHERE (SUPPLIER = @Original_SUPPLIER) AND (NAME = @Or" +
                "iginal_NAME OR @Original_NAME IS NULL AND NAME IS NULL)";
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO SUPPLIER_MASTER(SUPPLIER, NAME) VALUES (@SUPPLIER, @NAME); SELECT SUP" +
                "PLIER, NAME FROM SUPPLIER_MASTER WHERE (SUPPLIER = @SUPPLIER) ORDER BY SUPPLIER";
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 40, "NAME")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT SUPPLIER, NAME FROM SUPPLIER_MASTER WHERE (ISNULL(ACTIVE, \'F\') = \'T\') ORDE" +
                "R BY SUPPLIER";
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 40, "NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daPO
            // 
            this.daPO.DeleteCommand = this.sqlDeleteCommand2;
            this.daPO.InsertCommand = this.sqlInsertCommand2;
            this.daPO.SelectCommand = this.sqlSelectCommand2;
            this.daPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("SUPP_NAME", "SUPP_NAME"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            this.daPO.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ORDER_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 0, "PO"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 0, "ORDER_DATE")});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = resources.GetString("sqlSelectCommand2.CommandText");
            this.sqlSelectCommand2.CommandTimeout = 400;
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 0, "PO"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 0, "ORDER_DATE"),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ORDER_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null)});
            // 
            // ucSuppChange
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(440, 184);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ucSuppChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ucSuppChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPO11)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void ucSuppChange_Load(object sender, System.EventArgs e)
		{
			DevXMgr.FormInit( this );
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if( luePO.EditValue == null )
			{
				Popup.ShowPopup( "No PO has been selected." );
				return;
			}
			if( lueSupplier.EditValue == null )
			{
				Popup.ShowPopup( "No replacement Supplier has been selected." );
				return;
			}

			if( Popup.ShowPopup( "Are you sure you want to change the supplier on this PO?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
			{
                string sUpdate = "declare @supplier_id int " +
                    "select @supplier_id=supplier_id from SUPPLIER_MASTER where SUPPLIER='" + lueSupplier.EditValue + "' " + 
                    "update po_header set supplier='" + lueSupplier.EditValue + "', supp_name='" + lueSupplier.GetColumnValue("NAME").ToString().Replace("'", "''") + "' where po_id=" + luePO.EditValue + " " +
					"update so_tasks set vendor=@supplier_id, vendor_description='"+lueSupplier.GetColumnValue("NAME").ToString().Replace("'", "''")+"' where id in (select so_task_id from po_detail where so_task_id is not null and po_id="+luePO.EditValue+")";
				Connection.SQLExecutor.ExecuteNonQuery( sUpdate, Connection.TRConnection );
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}

