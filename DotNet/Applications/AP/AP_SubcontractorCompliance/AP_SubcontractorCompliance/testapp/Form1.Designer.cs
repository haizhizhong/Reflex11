namespace testapp
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpMain = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnMainRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.tpSupplier = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.sUPPLIERMASTERBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsSupplier1 = new testapp.dsSupplier();
            this.btnSupplierRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.tpProject = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lueProject = new DevExpress.XtraEditors.LookUpEdit();
            this.pROJHEADERBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsProject1 = new testapp.dsProject();
            this.btnProjectRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlTRConnection = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daProject = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.tpSupplier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sUPPLIERMASTERBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).BeginInit();
            this.tpProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJHEADERBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProject1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpMain;
            this.xtraTabControl1.Size = new System.Drawing.Size(1212, 715);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpMain,
            this.tpSupplier,
            this.tpProject});
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.panelControl3);
            this.tpMain.Name = "tpMain";
            this.tpMain.Size = new System.Drawing.Size(1203, 684);
            this.tpMain.Text = "Main";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnMainRefresh);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1203, 33);
            this.panelControl3.TabIndex = 0;
            // 
            // btnMainRefresh
            // 
            this.btnMainRefresh.Location = new System.Drawing.Point(498, 6);
            this.btnMainRefresh.Name = "btnMainRefresh";
            this.btnMainRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnMainRefresh.TabIndex = 0;
            this.btnMainRefresh.Text = "Refresh";
            this.btnMainRefresh.Click += new System.EventHandler(this.btnMainRefresh_Click);
            // 
            // tpSupplier
            // 
            this.tpSupplier.Controls.Add(this.panelControl1);
            this.tpSupplier.Name = "tpSupplier";
            this.tpSupplier.Size = new System.Drawing.Size(1203, 684);
            this.tpSupplier.Text = "Supplier";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lueSupplier);
            this.panelControl1.Controls.Add(this.btnSupplierRefresh);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1203, 38);
            this.panelControl1.TabIndex = 0;
            // 
            // lueSupplier
            // 
            this.lueSupplier.Location = new System.Drawing.Point(9, 8);
            this.lueSupplier.Name = "lueSupplier";
            this.lueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSupplier.Properties.DataSource = this.sUPPLIERMASTERBindingSource;
            this.lueSupplier.Properties.DisplayMember = "SUPPLIER";
            this.lueSupplier.Properties.ValueMember = "SUPPLIER_ID";
            this.lueSupplier.Size = new System.Drawing.Size(609, 20);
            this.lueSupplier.TabIndex = 1;
            // 
            // sUPPLIERMASTERBindingSource
            // 
            this.sUPPLIERMASTERBindingSource.DataMember = "SUPPLIER_MASTER";
            this.sUPPLIERMASTERBindingSource.DataSource = this.dsSupplier1;
            // 
            // dsSupplier1
            // 
            this.dsSupplier1.DataSetName = "dsSupplier";
            this.dsSupplier1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSupplierRefresh
            // 
            this.btnSupplierRefresh.Location = new System.Drawing.Point(624, 5);
            this.btnSupplierRefresh.Name = "btnSupplierRefresh";
            this.btnSupplierRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnSupplierRefresh.TabIndex = 0;
            this.btnSupplierRefresh.Text = "Refresh";
            this.btnSupplierRefresh.Click += new System.EventHandler(this.btnSupplierRefresh_Click);
            // 
            // tpProject
            // 
            this.tpProject.Controls.Add(this.panelControl2);
            this.tpProject.Name = "tpProject";
            this.tpProject.Size = new System.Drawing.Size(1203, 684);
            this.tpProject.Text = "Project";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lueProject);
            this.panelControl2.Controls.Add(this.btnProjectRefresh);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1203, 42);
            this.panelControl2.TabIndex = 0;
            // 
            // lueProject
            // 
            this.lueProject.Location = new System.Drawing.Point(9, 6);
            this.lueProject.Name = "lueProject";
            this.lueProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueProject.Properties.DataSource = this.pROJHEADERBindingSource;
            this.lueProject.Properties.DisplayMember = "pri_name";
            this.lueProject.Properties.ValueMember = "pri_id";
            this.lueProject.Size = new System.Drawing.Size(560, 20);
            this.lueProject.TabIndex = 1;
            // 
            // pROJHEADERBindingSource
            // 
            this.pROJHEADERBindingSource.DataMember = "PROJ_HEADER";
            this.pROJHEADERBindingSource.DataSource = this.dsProject1;
            // 
            // dsProject1
            // 
            this.dsProject1.DataSetName = "dsProject";
            this.dsProject1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnProjectRefresh
            // 
            this.btnProjectRefresh.Location = new System.Drawing.Point(575, 3);
            this.btnProjectRefresh.Name = "btnProjectRefresh";
            this.btnProjectRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnProjectRefresh.TabIndex = 0;
            this.btnProjectRefresh.Text = "Refresh";
            this.btnProjectRefresh.Click += new System.EventHandler(this.btnProjectRefresh_Click);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT        SUPPLIER, NAME, SUPPLIER_ID,ACTIVE\r\nFROM            SUPPLIER_MASTER" +
                "\r\nORDER BY SUPPLIER";
            this.sqlSelectCommand1.Connection = this.sqlTRConnection;
            // 
            // sqlTRConnection
            // 
            this.sqlTRConnection.ConnectionString = "Data Source=DOTNETSQL\\DOTNETSQL;Initial Catalog=TR_GC_TEST;Persist Security Info=" +
                "True;User ID=hmsqlsa;Password=hmsqlsa";
            this.sqlTRConnection.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlTRConnection;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@ACTIVE", System.Data.SqlDbType.VarChar, 0, "ACTIVE")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.sqlTRConnection;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 0, "NAME"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER_ID", System.Data.SqlDbType.Int, 0, "SUPPLIER_ID"),
            new System.Data.SqlClient.SqlParameter("@ACTIVE", System.Data.SqlDbType.VarChar, 0, "ACTIVE"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACTIVE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACTIVE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACTIVE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACTIVE", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.sqlTRConnection;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACTIVE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACTIVE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACTIVE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACTIVE", System.Data.DataRowVersion.Original, null)});
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlDeleteCommand1;
            this.daSupplier.InsertCommand = this.sqlInsertCommand1;
            this.daSupplier.SelectCommand = this.sqlSelectCommand1;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME"),
                        new System.Data.Common.DataColumnMapping("SUPPLIER_ID", "SUPPLIER_ID"),
                        new System.Data.Common.DataColumnMapping("ACTIVE", "ACTIVE")})});
            this.daSupplier.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT        pri_id, pri_code, pri_desc1, pri_name, pri_type\r\nFROM            PR" +
                "OJ_HEADER\r\nWHERE        (pri_type = \'PGC\')\r\nORDER BY pri_code";
            this.sqlSelectCommand2.Connection = this.sqlTRConnection;
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.sqlTRConnection;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code"),
            new System.Data.SqlClient.SqlParameter("@pri_desc1", System.Data.SqlDbType.VarChar, 0, "pri_desc1"),
            new System.Data.SqlClient.SqlParameter("@pri_name", System.Data.SqlDbType.VarChar, 0, "pri_name"),
            new System.Data.SqlClient.SqlParameter("@pri_type", System.Data.SqlDbType.VarChar, 0, "pri_type")});
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.sqlTRConnection;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@pri_code", System.Data.SqlDbType.Int, 0, "pri_code"),
            new System.Data.SqlClient.SqlParameter("@pri_desc1", System.Data.SqlDbType.VarChar, 0, "pri_desc1"),
            new System.Data.SqlClient.SqlParameter("@pri_name", System.Data.SqlDbType.VarChar, 0, "pri_name"),
            new System.Data.SqlClient.SqlParameter("@pri_type", System.Data.SqlDbType.VarChar, 0, "pri_type"),
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_desc1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_desc1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_desc1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_desc1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_name", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_type", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_type", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_type", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 4, "pri_id")});
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.sqlTRConnection;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_desc1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_desc1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_desc1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_desc1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_name", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_name", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_name", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_type", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_type", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_type", System.Data.DataRowVersion.Original, null)});
            // 
            // daProject
            // 
            this.daProject.DeleteCommand = this.sqlDeleteCommand2;
            this.daProject.InsertCommand = this.sqlInsertCommand2;
            this.daProject.SelectCommand = this.sqlSelectCommand2;
            this.daProject.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PROJ_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("pri_code", "pri_code"),
                        new System.Data.Common.DataColumnMapping("pri_desc1", "pri_desc1"),
                        new System.Data.Common.DataColumnMapping("pri_name", "pri_name"),
                        new System.Data.Common.DataColumnMapping("pri_type", "pri_type")})});
            this.daProject.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 715);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "Form1";
            this.Text = "Test App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.tpSupplier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sUPPLIERMASTERBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).EndInit();
            this.tpProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJHEADERBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProject1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpMain;
        private DevExpress.XtraTab.XtraTabPage tpSupplier;
        private DevExpress.XtraTab.XtraTabPage tpProject;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueSupplier;
        private DevExpress.XtraEditors.SimpleButton btnSupplierRefresh;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnMainRefresh;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit lueProject;
        private DevExpress.XtraEditors.SimpleButton btnProjectRefresh;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection sqlTRConnection;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daSupplier;
        private dsSupplier dsSupplier1;
        private System.Windows.Forms.BindingSource sUPPLIERMASTERBindingSource;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
        private System.Data.SqlClient.SqlDataAdapter daProject;
        private dsProject dsProject1;
        private System.Windows.Forms.BindingSource pROJHEADERBindingSource;
    }
}

