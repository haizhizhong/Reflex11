namespace AlertViews.Costing.POReceiving
{
    partial class ucPORecNotifyDet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPORecNotifyDet));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcPORecDet = new DevExpress.XtraGrid.GridControl();
            this.dsPORecDet = new AlertViews.Costing.POReceiving.dsPORecDet();
            this.gvPORecDet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLINE_NUMBER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colONETIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riOneTime = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colPART_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPART_NO_DESC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUNIT_PRICE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUNIT_OF_MEASURE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQTY_ORDERED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQTY_RECEIVED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTOTAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPO = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daPORecDet = new System.Data.SqlClient.SqlDataAdapter();
            this.lblReceiptDate = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPORecDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPORecDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPORecDet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riOneTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcPORecDet);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1011, 482);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "PO Receiving Details";
            // 
            // gcPORecDet
            // 
            this.gcPORecDet.DataMember = "PO_DETAIL";
            this.gcPORecDet.DataSource = this.dsPORecDet;
            this.gcPORecDet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPORecDet.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPORecDet.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcPORecDet.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcPORecDet.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcPORecDet.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcPORecDet.Location = new System.Drawing.Point(2, 60);
            this.gcPORecDet.MainView = this.gvPORecDet;
            this.gcPORecDet.Name = "gcPORecDet";
            this.gcPORecDet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riOneTime});
            this.gcPORecDet.Size = new System.Drawing.Size(1007, 420);
            this.gcPORecDet.TabIndex = 0;
            this.gcPORecDet.UseEmbeddedNavigator = true;
            this.gcPORecDet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPORecDet});
            // 
            // dsPORecDet
            // 
            this.dsPORecDet.DataSetName = "dsPORecDet";
            this.dsPORecDet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvPORecDet
            // 
            this.gvPORecDet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLINE_NUMBER,
            this.colONETIME,
            this.colPART_NO,
            this.colPART_NO_DESC,
            this.colUNIT_PRICE,
            this.colUNIT_OF_MEASURE,
            this.colQTY_ORDERED,
            this.colQTY_RECEIVED,
            this.colTOTAL});
            this.gvPORecDet.GridControl = this.gcPORecDet;
            this.gvPORecDet.Name = "gvPORecDet";
            this.gvPORecDet.OptionsView.ShowFooter = true;
            this.gvPORecDet.OptionsView.ShowGroupPanel = false;
            // 
            // colLINE_NUMBER
            // 
            this.colLINE_NUMBER.Caption = "Line Number";
            this.colLINE_NUMBER.FieldName = "LINE_NUMBER";
            this.colLINE_NUMBER.Name = "colLINE_NUMBER";
            this.colLINE_NUMBER.OptionsColumn.AllowEdit = false;
            this.colLINE_NUMBER.Visible = true;
            this.colLINE_NUMBER.VisibleIndex = 0;
            // 
            // colONETIME
            // 
            this.colONETIME.Caption = "One Time Part";
            this.colONETIME.ColumnEdit = this.riOneTime;
            this.colONETIME.FieldName = "ONETIME";
            this.colONETIME.Name = "colONETIME";
            this.colONETIME.OptionsColumn.AllowEdit = false;
            this.colONETIME.Visible = true;
            this.colONETIME.VisibleIndex = 1;
            // 
            // riOneTime
            // 
            this.riOneTime.AutoHeight = false;
            this.riOneTime.Name = "riOneTime";
            this.riOneTime.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riOneTime.ValueChecked = "T";
            this.riOneTime.ValueUnchecked = "F";
            // 
            // colPART_NO
            // 
            this.colPART_NO.Caption = "Part Number";
            this.colPART_NO.FieldName = "PART_NO";
            this.colPART_NO.Name = "colPART_NO";
            this.colPART_NO.OptionsColumn.AllowEdit = false;
            this.colPART_NO.OptionsColumn.ReadOnly = true;
            this.colPART_NO.Visible = true;
            this.colPART_NO.VisibleIndex = 2;
            // 
            // colPART_NO_DESC
            // 
            this.colPART_NO_DESC.Caption = "Part Description";
            this.colPART_NO_DESC.FieldName = "PART_NO_DESC";
            this.colPART_NO_DESC.Name = "colPART_NO_DESC";
            this.colPART_NO_DESC.OptionsColumn.AllowEdit = false;
            this.colPART_NO_DESC.Visible = true;
            this.colPART_NO_DESC.VisibleIndex = 3;
            // 
            // colUNIT_PRICE
            // 
            this.colUNIT_PRICE.Caption = "Unit Price";
            this.colUNIT_PRICE.DisplayFormat.FormatString = "n2";
            this.colUNIT_PRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUNIT_PRICE.FieldName = "UNIT_PRICE";
            this.colUNIT_PRICE.Name = "colUNIT_PRICE";
            this.colUNIT_PRICE.OptionsColumn.AllowEdit = false;
            this.colUNIT_PRICE.SummaryItem.DisplayFormat = "{0:n2}";
            this.colUNIT_PRICE.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colUNIT_PRICE.Visible = true;
            this.colUNIT_PRICE.VisibleIndex = 4;
            // 
            // colUNIT_OF_MEASURE
            // 
            this.colUNIT_OF_MEASURE.Caption = "UOM";
            this.colUNIT_OF_MEASURE.FieldName = "UNIT_OF_MEASURE";
            this.colUNIT_OF_MEASURE.Name = "colUNIT_OF_MEASURE";
            this.colUNIT_OF_MEASURE.OptionsColumn.AllowEdit = false;
            this.colUNIT_OF_MEASURE.Visible = true;
            this.colUNIT_OF_MEASURE.VisibleIndex = 5;
            // 
            // colQTY_ORDERED
            // 
            this.colQTY_ORDERED.Caption = "Ordered Qty";
            this.colQTY_ORDERED.DisplayFormat.FormatString = "n3";
            this.colQTY_ORDERED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQTY_ORDERED.FieldName = "QTY_ORDERED";
            this.colQTY_ORDERED.Name = "colQTY_ORDERED";
            this.colQTY_ORDERED.OptionsColumn.AllowEdit = false;
            this.colQTY_ORDERED.SummaryItem.DisplayFormat = "{0:n3}";
            this.colQTY_ORDERED.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQTY_ORDERED.Visible = true;
            this.colQTY_ORDERED.VisibleIndex = 6;
            // 
            // colQTY_RECEIVED
            // 
            this.colQTY_RECEIVED.Caption = "Received Qty";
            this.colQTY_RECEIVED.DisplayFormat.FormatString = "n3";
            this.colQTY_RECEIVED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQTY_RECEIVED.FieldName = "QTY_RECEIVED";
            this.colQTY_RECEIVED.Name = "colQTY_RECEIVED";
            this.colQTY_RECEIVED.OptionsColumn.AllowEdit = false;
            this.colQTY_RECEIVED.SummaryItem.DisplayFormat = "{0:n3}";
            this.colQTY_RECEIVED.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQTY_RECEIVED.Visible = true;
            this.colQTY_RECEIVED.VisibleIndex = 7;
            // 
            // colTOTAL
            // 
            this.colTOTAL.Caption = "Total";
            this.colTOTAL.DisplayFormat.FormatString = "n2";
            this.colTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTOTAL.FieldName = "TOTAL";
            this.colTOTAL.Name = "colTOTAL";
            this.colTOTAL.OptionsColumn.AllowEdit = false;
            this.colTOTAL.SummaryItem.DisplayFormat = "{0:n2}";
            this.colTOTAL.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colTOTAL.Visible = true;
            this.colTOTAL.VisibleIndex = 8;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblReceiptDate);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.lblReceipt);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.lblPO);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 20);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1007, 40);
            this.panelControl1.TabIndex = 1;
            // 
            // lblReceipt
            // 
            this.lblReceipt.AutoSize = true;
            this.lblReceipt.Location = new System.Drawing.Point(265, 15);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(0, 13);
            this.lblReceipt.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PO Receipt #:";
            // 
            // lblPO
            // 
            this.lblPO.AutoSize = true;
            this.lblPO.Location = new System.Drawing.Point(57, 15);
            this.lblPO.Name = "lblPO";
            this.lblPO.Size = new System.Drawing.Size(0, 13);
            this.lblPO.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PO #:";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@po_rec_id", System.Data.SqlDbType.Int, 4, "PO_REC_ID")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=tr_strike_test10;Persist Security Info=True;User" +
                " ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daPORecDet
            // 
            this.daPORecDet.SelectCommand = this.sqlSelectCommand1;
            this.daPORecDet.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_DETAIL", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("LINE_NUMBER", "LINE_NUMBER"),
                        new System.Data.Common.DataColumnMapping("ONETIME", "ONETIME"),
                        new System.Data.Common.DataColumnMapping("PART_NO", "PART_NO"),
                        new System.Data.Common.DataColumnMapping("PART_NO_DESC", "PART_NO_DESC"),
                        new System.Data.Common.DataColumnMapping("UNIT_PRICE", "UNIT_PRICE"),
                        new System.Data.Common.DataColumnMapping("UNIT_OF_MEASURE", "UNIT_OF_MEASURE"),
                        new System.Data.Common.DataColumnMapping("QTY_ORDERED", "QTY_ORDERED"),
                        new System.Data.Common.DataColumnMapping("QTY_RECEIVED", "QTY_RECEIVED"),
                        new System.Data.Common.DataColumnMapping("TOTAL", "TOTAL")})});
            // 
            // lblReceiptDate
            // 
            this.lblReceiptDate.AutoSize = true;
            this.lblReceiptDate.Location = new System.Drawing.Point(461, 15);
            this.lblReceiptDate.Name = "lblReceiptDate";
            this.lblReceiptDate.Size = new System.Drawing.Size(0, 13);
            this.lblReceiptDate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Receipt Date:";
            // 
            // ucPORecNotifyDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ucPORecNotifyDet";
            this.Size = new System.Drawing.Size(1011, 482);
            this.Load += new System.EventHandler(this.ucPORecNotify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPORecDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPORecDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPORecDet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riOneTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcPORecDet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPORecDet;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daPORecDet;
        private dsPORecDet dsPORecDet;
        private DevExpress.XtraGrid.Columns.GridColumn colLINE_NUMBER;
        private DevExpress.XtraGrid.Columns.GridColumn colONETIME;
        private DevExpress.XtraGrid.Columns.GridColumn colPART_NO;
        private DevExpress.XtraGrid.Columns.GridColumn colPART_NO_DESC;
        private DevExpress.XtraGrid.Columns.GridColumn colUNIT_PRICE;
        private DevExpress.XtraGrid.Columns.GridColumn colUNIT_OF_MEASURE;
        private DevExpress.XtraGrid.Columns.GridColumn colQTY_ORDERED;
        private DevExpress.XtraGrid.Columns.GridColumn colQTY_RECEIVED;
        private DevExpress.XtraGrid.Columns.GridColumn colTOTAL;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riOneTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.Label lblReceiptDate;
        private System.Windows.Forms.Label label4;
    }
}
