namespace AP_PaymentReqApp
{
    partial class ucAPPaymentRequest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAPPaymentRequest));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpDetails = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcRequest = new DevExpress.XtraGrid.GridControl();
            this.dsRequest1 = new AP_PaymentReqApp.dsRequest();
            this.gvRequest = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUPPLIER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUPP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContact = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_AMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAP_ROUTING_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colApprove = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riApproveDecline = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colnon_po_route = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWF_Approval_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riWorkflow = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.wSApprovalWorkFlowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsWorkFlow1 = new AP_PaymentReqApp.dsWorkFlow();
            this.colSUB_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riEmpty = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riRouteStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riApproveDeclineDisable = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.riWorkFlowLocked = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daRequest = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.WEB_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daWorkFlow = new System.Data.SqlClient.SqlDataAdapter();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnLinkCompAttch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRequest1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riApproveDecline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkflow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wSApprovalWorkFlowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWorkFlow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riApproveDeclineDisable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlowLocked)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpDetails});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpDetails
            // 
            this.dpDetails.Controls.Add(this.dockPanel1_Container);
            this.dpDetails.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpDetails.ID = new System.Guid("8bcaec55-5db3-497b-b478-c703d6e97891");
            this.dpDetails.Location = new System.Drawing.Point(0, 239);
            this.dpDetails.Name = "dpDetails";
            this.dpDetails.Size = new System.Drawing.Size(1299, 475);
            this.dpDetails.Text = "Details";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1293, 447);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gcRequest
            // 
            this.gcRequest.DataMember = "AP_PayReqApprover";
            this.gcRequest.DataSource = this.dsRequest1;
            this.gcRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRequest.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcRequest.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcRequest.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcRequest.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcRequest.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcRequest.Location = new System.Drawing.Point(0, 0);
            this.gcRequest.MainView = this.gvRequest;
            this.gcRequest.Name = "gcRequest";
            this.gcRequest.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riApproveDecline,
            this.riStatus,
            this.riEmpty,
            this.riRouteStatus,
            this.riWorkflow,
            this.riApproveDeclineDisable,
            this.riWorkFlowLocked});
            this.gcRequest.Size = new System.Drawing.Size(1299, 239);
            this.gcRequest.TabIndex = 1;
            this.gcRequest.UseEmbeddedNavigator = true;
            this.gcRequest.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRequest});
            // 
            // dsRequest1
            // 
            this.dsRequest1.DataSetName = "dsRequest";
            this.dsRequest1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvRequest
            // 
            this.gvRequest.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDATE,
            this.colSUPPLIER,
            this.colSUPP_NAME,
            this.colContact,
            this.colINV_AMOUNT,
            this.colAP_ROUTING_STATUS,
            this.colApprove,
            this.gridColumn1,
            this.gridColumn2,
            this.colnon_po_route,
            this.colWF_Approval_ID,
            this.colSUB_TYPE});
            this.gvRequest.GridControl = this.gcRequest;
            this.gvRequest.Name = "gvRequest";
            this.gvRequest.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvRequest_FocusedRowChanged);
            this.gvRequest.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvRequest_CustomRowCellEdit);
            this.gvRequest.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvRequest_RowUpdated);
            // 
            // colDATE
            // 
            this.colDATE.Caption = "Date";
            this.colDATE.FieldName = "DATE";
            this.colDATE.Name = "colDATE";
            this.colDATE.OptionsColumn.AllowEdit = false;
            this.colDATE.Visible = true;
            this.colDATE.VisibleIndex = 0;
            this.colDATE.Width = 125;
            // 
            // colSUPPLIER
            // 
            this.colSUPPLIER.Caption = "Supplier";
            this.colSUPPLIER.FieldName = "SUPPLIER";
            this.colSUPPLIER.Name = "colSUPPLIER";
            this.colSUPPLIER.OptionsColumn.AllowEdit = false;
            this.colSUPPLIER.Visible = true;
            this.colSUPPLIER.VisibleIndex = 1;
            this.colSUPPLIER.Width = 125;
            // 
            // colSUPP_NAME
            // 
            this.colSUPP_NAME.Caption = "Name";
            this.colSUPP_NAME.FieldName = "SUPP_NAME";
            this.colSUPP_NAME.Name = "colSUPP_NAME";
            this.colSUPP_NAME.OptionsColumn.AllowEdit = false;
            this.colSUPP_NAME.Visible = true;
            this.colSUPP_NAME.VisibleIndex = 2;
            this.colSUPP_NAME.Width = 125;
            // 
            // colContact
            // 
            this.colContact.Caption = "Contact";
            this.colContact.FieldName = "Contact";
            this.colContact.Name = "colContact";
            this.colContact.OptionsColumn.AllowEdit = false;
            this.colContact.Visible = true;
            this.colContact.VisibleIndex = 3;
            this.colContact.Width = 125;
            // 
            // colINV_AMOUNT
            // 
            this.colINV_AMOUNT.Caption = "Requested Amount";
            this.colINV_AMOUNT.DisplayFormat.FormatString = "n2";
            this.colINV_AMOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colINV_AMOUNT.FieldName = "INV_AMOUNT";
            this.colINV_AMOUNT.Name = "colINV_AMOUNT";
            this.colINV_AMOUNT.OptionsColumn.AllowEdit = false;
            this.colINV_AMOUNT.Visible = true;
            this.colINV_AMOUNT.VisibleIndex = 4;
            this.colINV_AMOUNT.Width = 125;
            // 
            // colAP_ROUTING_STATUS
            // 
            this.colAP_ROUTING_STATUS.Caption = "AP Approval Status";
            this.colAP_ROUTING_STATUS.ColumnEdit = this.riStatus;
            this.colAP_ROUTING_STATUS.FieldName = "AP_ROUTING_STATUS";
            this.colAP_ROUTING_STATUS.Name = "colAP_ROUTING_STATUS";
            this.colAP_ROUTING_STATUS.OptionsColumn.AllowEdit = false;
            this.colAP_ROUTING_STATUS.Visible = true;
            this.colAP_ROUTING_STATUS.VisibleIndex = 5;
            this.colAP_ROUTING_STATUS.Width = 125;
            // 
            // riStatus
            // 
            this.riStatus.AutoHeight = false;
            this.riStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riStatus.Name = "riStatus";
            this.riStatus.NullText = "";
            // 
            // colApprove
            // 
            this.colApprove.Caption = "Approve / Decline";
            this.colApprove.ColumnEdit = this.riApproveDecline;
            this.colApprove.MinWidth = 110;
            this.colApprove.Name = "colApprove";
            this.colApprove.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colApprove.Visible = true;
            this.colApprove.VisibleIndex = 9;
            this.colApprove.Width = 110;
            // 
            // riApproveDecline
            // 
            this.riApproveDecline.AutoHeight = false;
            this.riApproveDecline.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Approve / Decline", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riApproveDecline.Name = "riApproveDecline";
            this.riApproveDecline.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.riApproveDecline.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riApproveDecline_ButtonClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PO Based";
            this.gridColumn1.FieldName = "Submission_Type";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 125;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Origin";
            this.gridColumn2.FieldName = "Origin";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 7;
            this.gridColumn2.Width = 125;
            // 
            // colnon_po_route
            // 
            this.colnon_po_route.Caption = "Workflow Routing Status";
            this.colnon_po_route.FieldName = "non_po_route";
            this.colnon_po_route.Name = "colnon_po_route";
            this.colnon_po_route.OptionsColumn.ShowInCustomizationForm = false;
            this.colnon_po_route.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colnon_po_route.Width = 132;
            // 
            // colWF_Approval_ID
            // 
            this.colWF_Approval_ID.Caption = "Routing Workflow";
            this.colWF_Approval_ID.ColumnEdit = this.riWorkflow;
            this.colWF_Approval_ID.FieldName = "WF_Approval_ID";
            this.colWF_Approval_ID.Name = "colWF_Approval_ID";
            this.colWF_Approval_ID.OptionsColumn.ShowInCustomizationForm = false;
            this.colWF_Approval_ID.Width = 121;
            // 
            // riWorkflow
            // 
            this.riWorkflow.AutoHeight = false;
            this.riWorkflow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.riWorkflow.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Approval_ID", "Approval_ID", 83, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Work_Flow", "Work Flow", 63, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riWorkflow.DataSource = this.wSApprovalWorkFlowBindingSource;
            this.riWorkflow.DisplayMember = "Work_Flow";
            this.riWorkflow.Name = "riWorkflow";
            this.riWorkflow.NullText = "";
            this.riWorkflow.PopupWidth = 250;
            this.riWorkflow.ValueMember = "Approval_ID";
            this.riWorkflow.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riWorkflow_ButtonClick);
            this.riWorkflow.EditValueChanged += new System.EventHandler(this.riWorkflow_EditValueChanged);
            this.riWorkflow.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riWorkflow_EditValueChanging);
            // 
            // wSApprovalWorkFlowBindingSource
            // 
            this.wSApprovalWorkFlowBindingSource.DataMember = "WS_Approval_WorkFlow";
            this.wSApprovalWorkFlowBindingSource.DataSource = this.dsWorkFlow1;
            // 
            // dsWorkFlow1
            // 
            this.dsWorkFlow1.DataSetName = "dsWorkFlow";
            this.dsWorkFlow1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colSUB_TYPE
            // 
            this.colSUB_TYPE.Caption = "Submission Type";
            this.colSUB_TYPE.FieldName = "SUB_TYPE";
            this.colSUB_TYPE.Name = "colSUB_TYPE";
            this.colSUB_TYPE.OptionsColumn.AllowEdit = false;
            this.colSUB_TYPE.Visible = true;
            this.colSUB_TYPE.VisibleIndex = 8;
            this.colSUB_TYPE.Width = 79;
            // 
            // riEmpty
            // 
            this.riEmpty.AutoHeight = false;
            this.riEmpty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riEmpty.Name = "riEmpty";
            this.riEmpty.NullText = "";
            this.riEmpty.ReadOnly = true;
            // 
            // riRouteStatus
            // 
            this.riRouteStatus.AutoHeight = false;
            this.riRouteStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riRouteStatus.Name = "riRouteStatus";
            this.riRouteStatus.NullText = "";
            this.riRouteStatus.ReadOnly = true;
            this.riRouteStatus.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riRouteStatus_ButtonClick);
            // 
            // riApproveDeclineDisable
            // 
            this.riApproveDeclineDisable.AutoHeight = false;
            this.riApproveDeclineDisable.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Approve / Decline", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.riApproveDeclineDisable.Name = "riApproveDeclineDisable";
            this.riApproveDeclineDisable.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // riWorkFlowLocked
            // 
            this.riWorkFlowLocked.AutoHeight = false;
            this.riWorkFlowLocked.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riWorkFlowLocked.DataSource = this.wSApprovalWorkFlowBindingSource;
            this.riWorkFlowLocked.DisplayMember = "Work_Flow";
            this.riWorkFlowLocked.Name = "riWorkFlowLocked";
            this.riWorkFlowLocked.NullText = "";
            this.riWorkFlowLocked.ReadOnly = true;
            this.riWorkFlowLocked.ValueMember = "Approval_ID";
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 50, "username")});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
                "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daRequest
            // 
            this.daRequest.DeleteCommand = this.sqlDeleteCommand;
            this.daRequest.InsertCommand = this.sqlInsertCommand;
            this.daRequest.SelectCommand = this.sqlSelectCommand1;
            this.daRequest.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_PayReqApprover", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("username", "username"),
                        new System.Data.Common.DataColumnMapping("WS_INV_ID", "WS_INV_ID"),
                        new System.Data.Common.DataColumnMapping("WS_HB_ID", "WS_HB_ID"),
                        new System.Data.Common.DataColumnMapping("DATE", "DATE"),
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("SUPP_NAME", "SUPP_NAME"),
                        new System.Data.Common.DataColumnMapping("Contact", "Contact"),
                        new System.Data.Common.DataColumnMapping("INV_AMOUNT", "INV_AMOUNT"),
                        new System.Data.Common.DataColumnMapping("AP_ROUTING_STATUS", "AP_ROUTING_STATUS"),
                        new System.Data.Common.DataColumnMapping("INV_NO", "INV_NO"),
                        new System.Data.Common.DataColumnMapping("Submission_Type", "Submission_Type"),
                        new System.Data.Common.DataColumnMapping("Origin", "Origin"),
                        new System.Data.Common.DataColumnMapping("non_po_route", "non_po_route"),
                        new System.Data.Common.DataColumnMapping("WF_Approval_ID", "WF_Approval_ID"),
                        new System.Data.Common.DataColumnMapping("SUB_TYPE", "SUB_TYPE"),
                        new System.Data.Common.DataColumnMapping("SUB_TYPE_C", "SUB_TYPE_C")})});
            this.daRequest.UpdateCommand = this.sqlUpdateCommand;
            // 
            // sqlDeleteCommand
            // 
            this.sqlDeleteCommand.CommandText = resources.GetString("sqlDeleteCommand.CommandText");
            this.sqlDeleteCommand.Connection = this.TR_Conn;
            this.sqlDeleteCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_username", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_username", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WS_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WS_INV_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WS_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WS_INV_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WS_HB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WS_HB_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WS_HB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WS_HB_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Contact", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Contact", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Contact", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Contact", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_ROUTING_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_ROUTING_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_ROUTING_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_ROUTING_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Submission_Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Submission_Type", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Submission_Type", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Submission_Type", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Origin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Origin", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Origin", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Origin", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_non_po_route", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "non_po_route", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_non_po_route", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "non_po_route", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_TYPE_C", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_TYPE_C", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_TYPE_C", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_TYPE_C", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand
            // 
            this.sqlInsertCommand.CommandText = resources.GetString("sqlInsertCommand.CommandText");
            this.sqlInsertCommand.Connection = this.TR_Conn;
            this.sqlInsertCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 0, "username"),
            new System.Data.SqlClient.SqlParameter("@WS_INV_ID", System.Data.SqlDbType.Int, 0, "WS_INV_ID"),
            new System.Data.SqlClient.SqlParameter("@WS_HB_ID", System.Data.SqlDbType.Int, 0, "WS_HB_ID"),
            new System.Data.SqlClient.SqlParameter("@DATE", System.Data.SqlDbType.DateTime, 0, "DATE"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@Contact", System.Data.SqlDbType.VarChar, 0, "Contact"),
            new System.Data.SqlClient.SqlParameter("@INV_AMOUNT", System.Data.SqlDbType.Money, 0, "INV_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_ROUTING_STATUS", System.Data.SqlDbType.VarChar, 0, "AP_ROUTING_STATUS"),
            new System.Data.SqlClient.SqlParameter("@INV_NO", System.Data.SqlDbType.VarChar, 0, "INV_NO"),
            new System.Data.SqlClient.SqlParameter("@Submission_Type", System.Data.SqlDbType.VarChar, 0, "Submission_Type"),
            new System.Data.SqlClient.SqlParameter("@Origin", System.Data.SqlDbType.VarChar, 0, "Origin"),
            new System.Data.SqlClient.SqlParameter("@non_po_route", System.Data.SqlDbType.VarChar, 0, "non_po_route"),
            new System.Data.SqlClient.SqlParameter("@WF_Approval_ID", System.Data.SqlDbType.Int, 0, "WF_Approval_ID"),
            new System.Data.SqlClient.SqlParameter("@SUB_TYPE", System.Data.SqlDbType.VarChar, 0, "SUB_TYPE"),
            new System.Data.SqlClient.SqlParameter("@SUB_TYPE_C", System.Data.SqlDbType.VarChar, 0, "SUB_TYPE_C")});
            // 
            // sqlUpdateCommand
            // 
            this.sqlUpdateCommand.CommandText = resources.GetString("sqlUpdateCommand.CommandText");
            this.sqlUpdateCommand.Connection = this.TR_Conn;
            this.sqlUpdateCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 0, "username"),
            new System.Data.SqlClient.SqlParameter("@WS_INV_ID", System.Data.SqlDbType.Int, 0, "WS_INV_ID"),
            new System.Data.SqlClient.SqlParameter("@WS_HB_ID", System.Data.SqlDbType.Int, 0, "WS_HB_ID"),
            new System.Data.SqlClient.SqlParameter("@DATE", System.Data.SqlDbType.DateTime, 0, "DATE"),
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@Contact", System.Data.SqlDbType.VarChar, 0, "Contact"),
            new System.Data.SqlClient.SqlParameter("@INV_AMOUNT", System.Data.SqlDbType.Money, 0, "INV_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_ROUTING_STATUS", System.Data.SqlDbType.VarChar, 0, "AP_ROUTING_STATUS"),
            new System.Data.SqlClient.SqlParameter("@INV_NO", System.Data.SqlDbType.VarChar, 0, "INV_NO"),
            new System.Data.SqlClient.SqlParameter("@Submission_Type", System.Data.SqlDbType.VarChar, 0, "Submission_Type"),
            new System.Data.SqlClient.SqlParameter("@Origin", System.Data.SqlDbType.VarChar, 0, "Origin"),
            new System.Data.SqlClient.SqlParameter("@non_po_route", System.Data.SqlDbType.VarChar, 0, "non_po_route"),
            new System.Data.SqlClient.SqlParameter("@WF_Approval_ID", System.Data.SqlDbType.Int, 0, "WF_Approval_ID"),
            new System.Data.SqlClient.SqlParameter("@SUB_TYPE", System.Data.SqlDbType.VarChar, 0, "SUB_TYPE"),
            new System.Data.SqlClient.SqlParameter("@SUB_TYPE_C", System.Data.SqlDbType.VarChar, 0, "SUB_TYPE_C"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_username", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_username", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "username", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WS_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WS_INV_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WS_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WS_INV_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WS_HB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WS_HB_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WS_HB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WS_HB_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPPLIER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Contact", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Contact", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Contact", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Contact", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_ROUTING_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_ROUTING_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_ROUTING_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_ROUTING_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Submission_Type", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Submission_Type", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Submission_Type", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Submission_Type", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Origin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Origin", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Origin", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Origin", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_non_po_route", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "non_po_route", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_non_po_route", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "non_po_route", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_TYPE_C", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_TYPE_C", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_TYPE_C", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_TYPE_C", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "select Approval_ID, Work_Flow from WS_Approval_WorkFlow where ISNULL(Is_Active,\'F" +
                "\')=\'T\' order by Work_Flow";
            this.sqlSelectCommand2.Connection = this.WEB_Conn;
            // 
            // WEB_Conn
            // 
            this.WEB_Conn.ConnectionString = "Data Source=dev1;Initial Catalog=web_strike_test;Persist Security Info=True;User " +
                "ID=hmsqlsa;Password=hmsqlsa";
            this.WEB_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = "INSERT INTO [WS_Approval_WorkFlow] ([Work_Flow]) VALUES (@Work_Flow);\r\nSELECT App" +
                "roval_ID, Work_Flow FROM WS_Approval_WorkFlow WHERE (Approval_ID = SCOPE_IDENTIT" +
                "Y()) ORDER BY Work_Flow";
            this.sqlInsertCommand1.Connection = this.WEB_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Work_Flow", System.Data.SqlDbType.VarChar, 0, "Work_Flow")});
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.WEB_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Work_Flow", System.Data.SqlDbType.VarChar, 0, "Work_Flow"),
            new System.Data.SqlClient.SqlParameter("@Original_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Work_Flow", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Work_Flow", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Approval_ID", System.Data.SqlDbType.Int, 4, "Approval_ID")});
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = "DELETE FROM [WS_Approval_WorkFlow] WHERE (([Approval_ID] = @Original_Approval_ID)" +
                " AND ((@IsNull_Work_Flow = 1 AND [Work_Flow] IS NULL) OR ([Work_Flow] = @Origina" +
                "l_Work_Flow)))";
            this.sqlDeleteCommand1.Connection = this.WEB_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Work_Flow", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Work_Flow", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, null)});
            // 
            // daWorkFlow
            // 
            this.daWorkFlow.DeleteCommand = this.sqlDeleteCommand1;
            this.daWorkFlow.InsertCommand = this.sqlInsertCommand1;
            this.daWorkFlow.SelectCommand = this.sqlSelectCommand2;
            this.daWorkFlow.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "WS_Approval_WorkFlow", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Approval_ID", "Approval_ID"),
                        new System.Data.Common.DataColumnMapping("Work_Flow", "Work_Flow")})});
            this.daWorkFlow.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1194, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLinkCompAttch
            // 
            this.btnLinkCompAttch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLinkCompAttch.Location = new System.Drawing.Point(994, 8);
            this.btnLinkCompAttch.Name = "btnLinkCompAttch";
            this.btnLinkCompAttch.Size = new System.Drawing.Size(194, 22);
            this.btnLinkCompAttch.TabIndex = 25;
            this.btnLinkCompAttch.Text = "Link Compliance Attachments";
            this.btnLinkCompAttch.Click += new System.EventHandler(this.btnLinkCompAttch_Click);
            // 
            // ucAPPaymentRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLinkCompAttch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.gcRequest);
            this.Controls.Add(this.dpDetails);
            this.Name = "ucAPPaymentRequest";
            this.Size = new System.Drawing.Size(1299, 714);
            this.Load += new System.EventHandler(this.ucAPPaymentRequest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRequest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsRequest1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riApproveDecline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkflow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wSApprovalWorkFlowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWorkFlow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riApproveDeclineDisable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlowLocked)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraGrid.GridControl gcRequest;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRequest;
        private DevExpress.XtraBars.Docking.DockPanel dpDetails;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlConnection TR_Conn;
        private System.Data.SqlClient.SqlDataAdapter daRequest;
        private dsRequest dsRequest1;
        private DevExpress.XtraGrid.Columns.GridColumn colDATE;
        private DevExpress.XtraGrid.Columns.GridColumn colSUPPLIER;
        private DevExpress.XtraGrid.Columns.GridColumn colSUPP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn colContact;
        private DevExpress.XtraGrid.Columns.GridColumn colINV_AMOUNT;
        private DevExpress.XtraGrid.Columns.GridColumn colAP_ROUTING_STATUS;
        private DevExpress.XtraGrid.Columns.GridColumn colApprove;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riApproveDecline;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colnon_po_route;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riEmpty;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riRouteStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colWF_Approval_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riWorkflow;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riApproveDeclineDisable;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter daWorkFlow;
        private dsWorkFlow dsWorkFlow1;
        private System.Windows.Forms.BindingSource wSApprovalWorkFlowBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riWorkFlowLocked;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand;
        private DevExpress.XtraGrid.Columns.GridColumn colSUB_TYPE;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private System.Data.SqlClient.SqlConnection WEB_Conn;
        private DevExpress.XtraEditors.SimpleButton btnLinkCompAttch;
    }
}
