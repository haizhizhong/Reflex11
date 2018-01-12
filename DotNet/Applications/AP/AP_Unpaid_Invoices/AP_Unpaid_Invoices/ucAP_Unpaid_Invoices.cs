using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using System.Data.SqlClient;
using APGSTException;
using APPOSelect;

namespace AP_Unpaid_Invoices
{
	public class ucAP_Unpaid_Invoices : DevExpress.XtraEditors.XtraUserControl
    {
        #region ucAP_Unpaid_Invoices Class Variables

        private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager myMgr;
		private HMCon Connection;	
		private ucAPGSTException ucAPGSTE;
		private ucMatchPOReceipt ucMPOR;
		private ucUnreleasedContractPO ucUCPO;
		private DataTable dtInvType;
		private DataTable dtDetType;

        private ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer ucARHV;

		private bool _Loaded = false;
        private bool _WFRequired = false;

		private KeyControlAccess.Validator KCA_Validator;	
		private const int CONST_QUICK_CHECK = 14;	
		private const int CONST_MANUAL_CHECK = 15;
        private const int CONST_SUBCON_COMP_PAY_HOLD = 60;
        private const int CONST_RELEASE_AP_PAY_HOLD = 279;
        private const int CONST_SUBCON_COMP_PAY_HOLD_WF = 5;

        private const int CONST_PWP_STATUS_OPEN = 1;
        private const int CONST_PWP_STATUS_AVAILABLE = 2;
        private const int CONST_PWP_STATUS_REJECTED = 3;
        private const int CONST_PWP_STATUS_PENDING = 4;
        private const int CONST_OVERRIDE_PWP_STATUS = 358;
        		
        private string _HeaderSelect = "SELECT a.SUPPLIER, a.INV_NO, a.TRANS_TYPE, a.ACCT_PERIOD, a.ACCT_YEAR, a.JOURNAL_NUMBER, a.JOURNAL_LINE_NO, a.TRANS_DATE, a.INV_DATE, a.DUE_DATE, a.DISCOUNT_DATE, a.INV_AMOUNT, a.DISCOUNT_AMOUNT, a.BALANCE, a.REFERENCE, a.CANCEL, a.DISCOUNT_TAKEN, a.CK_SELECT, a.OPERATOR, a.DATE_SAVED, a.HOLD, a.SUPP_NAME, a.REMITADD1, a.REMITADD2, a.REMITADD3, a.REMITCITY, a.REMITSTATE, a.REMITZIP, a.SUPP_ACCOUNT, a.AP_SETUP_GL_ID, a.GST_CODE, a.PURCH_AMT, a.GST_AMT, a.HOLD_PCT, a.HOLD_AMT, a.HOLD_BAL, a.HOLD_PAY_DATE, a.SEG_CHANGE, a.CURRENCY_ID, a.REMITCOUNTRY, a.GST_EXCEPT, a.ACCRUAL_FLAG, a.LOCKED_BY, a.LOCKED, a.AP_INV_HEADER_ID, a.INVOICE_TYPE, a.MANUAL_CHECK, a.SALES_TAX_ID, a.NEW_INVOICE, a.COMMENT, a.EXCH_RATE, a.PO_ID, a.GST_EXCEPT_ID, a.whse_id, a.SOX_ROUTING, a.SOX_APPROVAL, a.id, a.AP_DIV, a.KC_PAYHOLD_STATUS, a.TERMS_ID FROM AP_INV_HEADER a WHERE (ISNULL(a.CANCEL, 'F') = 'F') AND (a.ACCRUAL_FLAG = 'A') AND (a.BALANCE <> 0) ORDER BY a.INV_NO ";

		private string sdaDetPOSelect = "SELECT DISTINCT POHDR.PO_ID, POHDR.PO, POHDR.ORDER_DATE "+
			"FROM PO_HEADER POHDR  "+
			"LEFT OUTER JOIN PO_REC_HEADER HDR ON POHDR.PO_ID = HDR.PO_ID  "+
			"LEFT OUTER JOIN PO_REC_DETAIL dtl ON dtl.PO_REC_ID = HDR.PO_REC_ID  "+
			"WHERE  "+
			"(POHDR.SUPPLIER = @SUPPLIER) AND  "+
			"(POHDR.STATUS <> 'Closed') AND  "+
			"((HDR.PO_REC_ID IS NOT NULL) OR (ISNULL(POHDR.CONTRACT_PO, 'F') <> 'F')) ";

        #endregion

        #region ucAP_Unpaid_Invoices Component Variables

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel4;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel5;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel5_Container;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel6_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel7;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel7_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel8;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel8_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel9;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel9_Container;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer3;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControl layoutControl2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControl layoutControl3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControl layoutControl4;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
		private DevExpress.XtraLayout.LayoutControl layoutControl5;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraGrid.GridControl gcHeader;
		private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
		private System.Data.SqlClient.SqlDataAdapter daHeader;
		private System.Data.SqlClient.SqlConnection TR_Conn;
		private AP_Unpaid_Invoices.dsHeader dsHeader1;
		private DevExpress.XtraGrid.Columns.GridColumn colSUPPLIER;
		private DevExpress.XtraGrid.Columns.GridColumn colINV_NO;
		private DevExpress.XtraGrid.Columns.GridColumn colACCT_PERIOD;
		private DevExpress.XtraGrid.Columns.GridColumn colACCT_YEAR;
		private DevExpress.XtraGrid.Columns.GridColumn colTRANS_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colINV_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colDUE_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colDISCOUNT_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colINV_AMOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colDISCOUNT_AMOUNT;
        private DevExpress.XtraGrid.Columns.GridColumn colREFERENCE;
		private DevExpress.XtraGrid.Columns.GridColumn colSUPP_NAME;
		private DevExpress.XtraGrid.Columns.GridColumn colAP_SETUP_GL_ID;
		private DevExpress.XtraGrid.Columns.GridColumn colGST_CODE;
		private DevExpress.XtraGrid.Columns.GridColumn colPURCH_AMT;
		private DevExpress.XtraGrid.Columns.GridColumn colGST_AMT;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD_PCT;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD_AMT;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD_PAY_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colCURRENCY_ID;
		private DevExpress.XtraGrid.Columns.GridColumn colINVOICE_TYPE;
		private DevExpress.XtraGrid.Columns.GridColumn colMANUAL_CHECK;
		private DevExpress.XtraGrid.Columns.GridColumn colSALES_TAX_ID;
		private DevExpress.XtraGrid.Columns.GridColumn colCOMMENT;
		private DevExpress.XtraGrid.Columns.GridColumn colPO_ID;
		private DevExpress.XtraGrid.Columns.GridColumn colSOX_ROUTING;
		private DevExpress.XtraGrid.Columns.GridColumn colSOX_APPROVAL;
		private DevExpress.XtraGrid.Columns.GridColumn colOPERATOR;
		private DevExpress.XtraBars.Docking.DockPanel dpActions;
		private AccountingPicker.ucAccountingPicker ucAccountingPicker1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraEditors.LookUpEdit lueAPCntl;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraEditors.DateEdit deHoldDue;
        private DevExpress.XtraEditors.TextEdit txtManChk;
		private DevExpress.XtraEditors.LookUpEdit lueInvType;
		private DevExpress.XtraEditors.LookUpEdit lueCurrency;
		private DevExpress.XtraEditors.LookUpEdit lueTerms;
		private DevExpress.XtraEditors.TextEdit txtDiscA;
		private DevExpress.XtraEditors.DateEdit deDiscDate;
		private DevExpress.XtraEditors.TextEdit txtHoldP;
		private DevExpress.XtraEditors.TextEdit txtHoldA;
		private DevExpress.XtraEditors.TextEdit txtRName;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
		private DevExpress.XtraEditors.TextEdit txtRAddr1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
		private DevExpress.XtraEditors.TextEdit txtRAddr2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
		private DevExpress.XtraEditors.TextEdit txtRAddr3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
		private DevExpress.XtraEditors.TextEdit txtRCity;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
		private DevExpress.XtraEditors.TextEdit txtRState;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
		private DevExpress.XtraEditors.TextEdit txtRZip;
		private DevExpress.XtraEditors.TextEdit txtRAcctNo;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
		private DevExpress.XtraEditors.MemoEdit memoEdit1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
		private System.Data.SqlClient.SqlDataAdapter daCurrency;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		private AP_Unpaid_Invoices.dsCurrency dsCurrency1;
		private System.Data.SqlClient.SqlDataAdapter daAPSetupGL;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
		private AP_Unpaid_Invoices.dsAPSetupGL dsAPSetupGL1;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
		private System.Data.SqlClient.SqlDataAdapter daAllPO;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand4;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand4;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand4;
		private AP_Unpaid_Invoices.dsAllPO dsAllPO1;
		private System.Data.SqlClient.SqlDataAdapter daGST;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand5;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand5;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand5;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand5;
		private AP_Unpaid_Invoices.dsGST dsGST1;
		private System.Data.SqlClient.SqlDataAdapter daPST;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand6;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand6;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand6;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand6;
		private AP_Unpaid_Invoices.dsPST dsPST1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
		private System.Data.SqlClient.SqlDataAdapter daSupplier;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand7;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand7;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand7;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand7;
		private AP_Unpaid_Invoices.dsSupplier dsSupplier1;
		private ReflexEditors.RHyperLinkEdit hlRefresh;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
		private System.Data.SqlClient.SqlDataAdapter daDetail;
		private AP_Unpaid_Invoices.dsDetail dsDetail1;
		private DevExpress.XtraGrid.GridControl gcDetail;
		private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
		private DevExpress.XtraGrid.Columns.GridColumn colGL_ACCOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colCOMMENT1;
		private DevExpress.XtraGrid.Columns.GridColumn colAMOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colTRANS_TYPE;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD_AMT1;
		private DevExpress.XtraGrid.Columns.GridColumn colPO_ID1;
		private DevExpress.XtraGrid.Columns.GridColumn colSUB_CODE;
		private DevExpress.XtraGrid.Columns.GridColumn colREFERENCE1;
		private DevExpress.XtraGrid.Columns.GridColumn colGL_ACCOUNT1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
		private System.Data.SqlClient.SqlDataAdapter daGLAccts;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand9;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand9;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand9;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand9;
		private AP_Unpaid_Invoices.dsGLAccts dsGLAccts1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit6;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit7;
		private DevExpress.XtraEditors.LookUpEdit lueSupplier;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
		private DevExpress.XtraEditors.LookUpEdit lueSuppName;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
		private DevExpress.XtraEditors.SimpleButton btnFilter;
		private DevExpress.XtraEditors.SimpleButton btnReset;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
		private System.Data.SqlClient.SqlDataAdapter daInvNo;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand10;
		private AP_Unpaid_Invoices.dsInvNo dsInvNo1;
		private DevExpress.XtraEditors.LookUpEdit lueInvNo;
		private DevExpress.XtraEditors.DateEdit deInvDate;
		private ReflexEditors.RHyperLinkEdit hlQikChk;
		private ReflexEditors.RHyperLinkEdit hlManChk;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraGrid.Columns.GridColumn colAP_DIV;
		private System.Data.SqlClient.SqlDataAdapter daSwapSeg;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand11;
		private AP_Unpaid_Invoices.dsSwapSeg dsSwapSeg1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit8;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit9;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand8;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand12;
		private System.Data.SqlClient.SqlDataAdapter daPOFSelect;
		private AP_Unpaid_Invoices.dsPOFSelect dsPOFSelect1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand13;
		private System.Data.SqlClient.SqlDataAdapter daPOBSelect;
		private AP_Unpaid_Invoices.dsPOBSelect dsPOBSelect1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand14;
		private System.Data.SqlClient.SqlDataAdapter daPOMSelect;
		private AP_Unpaid_Invoices.dsPOMSelect dsPOMSelect1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand15;
		private System.Data.SqlClient.SqlDataAdapter daPODSelect;
		private AP_Unpaid_Invoices.dsPODSelect dsPODSelect1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand16;
		private System.Data.SqlClient.SqlDataAdapter daPOM2Select;
		private AP_Unpaid_Invoices.dsPOM2Select dsPOM2Select1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOFSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOBSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPODSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOMSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOM2Select;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riNoPOSelect;
		private System.Data.SqlClient.SqlDataAdapter daPOSelect;
		private AP_Unpaid_Invoices.dsPOSelect dsPOSelect1;
		private DevExpress.XtraGrid.Columns.GridColumn colPO_ID2;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riDetPOSelect;
		private System.Data.SqlClient.SqlDataAdapter daDetPO;
		private AP_Unpaid_Invoices.dsDetPO dsDetPO1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand18;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand17;
        private DevExpress.XtraGrid.Columns.GridColumn colBALANCE;
        private DevExpress.XtraGrid.Columns.GridColumn colKC_PAYHOLD_STATUS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riRoutePayHold;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riEmpty;
        private SqlCommand sqlSelectCommand19;
        private SqlCommand sqlUpdateCommand8;
        private SqlCommand sqlDeleteCommand8;
        private SqlDataAdapter daTerms;
        private dsTerms dsTerms1;
        private BindingSource tERMSBindingSource;
        private CheckEdit chkPaymentHold;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riHold;
        private DevExpress.XtraGrid.Columns.GridColumn colPAYMENT_HOLD;
        private ReflexEditors.RHyperLinkEdit hlReleasePayHold;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private ReflexEditors.RHyperLinkEdit hlAddPayHold;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private ReflexEditors.RHyperLinkEdit hlOverridePWPStatus;
        private DevExpress.XtraLayout.LayoutControlItem lciOverridePWPStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colAR_PWP_STATUS_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPWPStatus;
        private SqlCommand sqlSelectCommand20;
        private SqlCommand sqlInsertCommand8;
        private SqlCommand sqlUpdateCommand10;
        private SqlCommand sqlDeleteCommand10;
        private SqlDataAdapter daPWP_Status;
        private dsPWP_Status dsPWP_Status1;
        private BindingSource aRPWPSTATUSBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCOST_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn colAFE_NO;
        private DevExpress.XtraGrid.Columns.GridColumn colTIME_TICKET;
        private IContainer components;

        #endregion

        public ucAP_Unpaid_Invoices( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, KeyControlAccess.Validator KCA_Validator )
		{
			this.KCA_Validator = KCA_Validator;
			this.Connection = Connection;
			this.myMgr = DevXMgr;
			InitializeComponent();
			TR_Conn.ConnectionString = Connection.TRConnection;
			daDetPO.SelectCommand.Parameters.Clear();

			ExecuteNonQuery( "exec sp_fill_mluser_supervisor '"+Connection.MLUser+"','"+Connection.MLUser+"', 1", TR_Conn );

			SetupCustomComps();
			SetupInvoiceType();			
			SetupHdrSwapSeg();
            SetupApprovalDT();
            CheckWorkFlowApproval();
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAP_Unpaid_Invoices));
            this.gcHeader = new DevExpress.XtraGrid.GridControl();
            this.dsHeader1 = new AP_Unpaid_Invoices.dsHeader();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSUPPLIER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colACCT_PERIOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colACCT_YEAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTRANS_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDUE_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDISCOUNT_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_AMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDISCOUNT_AMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colREFERENCE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOPERATOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSUPP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGST_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsGST1 = new AP_Unpaid_Invoices.dsGST();
            this.colPURCH_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGST_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHOLD_PCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHOLD_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHOLD_PAY_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCURRENCY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsCurrency1 = new AP_Unpaid_Invoices.dsCurrency();
            this.colINVOICE_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMANUAL_CHECK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSALES_TAX_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPST1 = new AP_Unpaid_Invoices.dsPST();
            this.colCOMMENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPO_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsAllPO1 = new AP_Unpaid_Invoices.dsAllPO();
            this.colSOX_ROUTING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSOX_APPROVAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAP_SETUP_GL_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAP_DIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsSwapSeg1 = new AP_Unpaid_Invoices.dsSwapSeg();
            this.colBALANCE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKC_PAYHOLD_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRoutePayHold = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colPAYMENT_HOLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riHold = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riEmpty = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.chkPaymentHold = new DevExpress.XtraEditors.CheckEdit();
            this.txtManChk = new DevExpress.XtraEditors.TextEdit();
            this.deHoldDue = new DevExpress.XtraEditors.DateEdit();
            this.txtHoldA = new DevExpress.XtraEditors.TextEdit();
            this.txtHoldP = new DevExpress.XtraEditors.TextEdit();
            this.deDiscDate = new DevExpress.XtraEditors.DateEdit();
            this.txtDiscA = new DevExpress.XtraEditors.TextEdit();
            this.lueTerms = new DevExpress.XtraEditors.LookUpEdit();
            this.tERMSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsTerms1 = new AP_Unpaid_Invoices.dsTerms();
            this.lueCurrency = new DevExpress.XtraEditors.LookUpEdit();
            this.lueInvType = new DevExpress.XtraEditors.LookUpEdit();
            this.lueAPCntl = new DevExpress.XtraEditors.LookUpEdit();
            this.dsAPSetupGL1 = new AP_Unpaid_Invoices.dsAPSetupGL();
            this.ucAccountingPicker1 = new AccountingPicker.ucAccountingPicker();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanel5 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.txtRAcctNo = new DevExpress.XtraEditors.TextEdit();
            this.txtRZip = new DevExpress.XtraEditors.TextEdit();
            this.txtRState = new DevExpress.XtraEditors.TextEdit();
            this.txtRCity = new DevExpress.XtraEditors.TextEdit();
            this.txtRAddr3 = new DevExpress.XtraEditors.TextEdit();
            this.txtRAddr2 = new DevExpress.XtraEditors.TextEdit();
            this.txtRAddr1 = new DevExpress.XtraEditors.TextEdit();
            this.txtRName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanel4 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.deInvDate = new DevExpress.XtraEditors.DateEdit();
            this.lueInvNo = new DevExpress.XtraEditors.LookUpEdit();
            this.dsInvNo1 = new AP_Unpaid_Invoices.dsInvNo();
            this.lueSuppName = new DevExpress.XtraEditors.LookUpEdit();
            this.dsSupplier1 = new AP_Unpaid_Invoices.dsSupplier();
            this.lueSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.dpActions = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel6_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.hlOverridePWPStatus = new ReflexEditors.RHyperLinkEdit();
            this.hlAddPayHold = new ReflexEditors.RHyperLinkEdit();
            this.hlReleasePayHold = new ReflexEditors.RHyperLinkEdit();
            this.hlRefresh = new ReflexEditors.RHyperLinkEdit();
            this.hlQikChk = new ReflexEditors.RHyperLinkEdit();
            this.hlManChk = new ReflexEditors.RHyperLinkEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOverridePWPStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel8 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel8_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcDetail = new DevExpress.XtraGrid.GridControl();
            this.dsDetail1 = new AP_Unpaid_Invoices.dsDetail();
            this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGL_ACCOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMMENT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTRANS_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colHOLD_AMT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPO_ID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOSelect1 = new AP_Unpaid_Invoices.dsPOSelect();
            this.colSUB_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGL_ACCOUNT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsGLAccts1 = new AP_Unpaid_Invoices.dsGLAccts();
            this.colREFERENCE1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPO_ID2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDetPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsDetPO1 = new AP_Unpaid_Invoices.dsDetPO();
            this.colAR_PWP_STATUS_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPWPStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.aRPWPSTATUSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsPWP_Status1 = new AP_Unpaid_Invoices.dsPWP_Status();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOST_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAFE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTIME_TICKET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riPOFSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOFSelect1 = new AP_Unpaid_Invoices.dsPOFSelect();
            this.riPOBSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOBSelect1 = new AP_Unpaid_Invoices.dsPOBSelect();
            this.riPODSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPODSelect1 = new AP_Unpaid_Invoices.dsPODSelect();
            this.riPOMSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOMSelect1 = new AP_Unpaid_Invoices.dsPOMSelect();
            this.riPOM2Select = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOM2Select1 = new AP_Unpaid_Invoices.dsPOM2Select();
            this.riNoPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dockPanel7 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel7_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel9 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel9_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.daHeader = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daCurrency = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daAPSetupGL = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daAllPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daGST = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand5 = new System.Data.SqlClient.SqlCommand();
            this.daPST = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand6 = new System.Data.SqlClient.SqlCommand();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand7 = new System.Data.SqlClient.SqlCommand();
            this.daDetail = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand8 = new System.Data.SqlClient.SqlCommand();
            this.daGLAccts = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand9 = new System.Data.SqlClient.SqlCommand();
            this.daInvNo = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand10 = new System.Data.SqlClient.SqlCommand();
            this.daSwapSeg = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand11 = new System.Data.SqlClient.SqlCommand();
            this.daPOFSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand12 = new System.Data.SqlClient.SqlCommand();
            this.daPOBSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand13 = new System.Data.SqlClient.SqlCommand();
            this.daPOMSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand14 = new System.Data.SqlClient.SqlCommand();
            this.daPODSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand15 = new System.Data.SqlClient.SqlCommand();
            this.daPOM2Select = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand16 = new System.Data.SqlClient.SqlCommand();
            this.daPOSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand17 = new System.Data.SqlClient.SqlCommand();
            this.daDetPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand18 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand19 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand8 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand8 = new System.Data.SqlClient.SqlCommand();
            this.daTerms = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand20 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand8 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand10 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand10 = new System.Data.SqlClient.SqlCommand();
            this.daPWP_Status = new System.Data.SqlClient.SqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeader1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGST1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCurrency1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPST1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllPO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSwapSeg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRoutePayHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer2.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManChk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTerms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tERMSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerms1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAPCntl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAPSetupGL1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.dockPanel5.SuspendLayout();
            this.dockPanel5_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAcctNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRCity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            this.dockPanel4.SuspendLayout();
            this.dockPanel3.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deInvDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInvDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSuppName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.dpActions.SuspendLayout();
            this.dockPanel6_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hlOverridePWPStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlAddPayHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlReleasePayHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlRefresh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlQikChk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlManChk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverridePWPStatus)).BeginInit();
            this.panelContainer3.SuspendLayout();
            this.dockPanel8.SuspendLayout();
            this.dockPanel8_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGLAccts1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetPOSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetPO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aRPWPSTATUSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPWP_Status1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOFSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOFSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOBSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOBSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPODSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPODSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOMSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOMSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOM2Select)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOM2Select1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPOSelect)).BeginInit();
            this.dockPanel7.SuspendLayout();
            this.dockPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcHeader
            // 
            this.gcHeader.DataMember = "AP_INV_HEADER";
            this.gcHeader.DataSource = this.dsHeader1;
            this.gcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHeader.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcHeader.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcHeader.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcHeader.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcHeader.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcHeader.Location = new System.Drawing.Point(0, 0);
            this.gcHeader.MainView = this.gvHeader;
            this.gcHeader.Name = "gcHeader";
            this.gcHeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemLookUpEdit3,
            this.repositoryItemLookUpEdit4,
            this.repositoryItemLookUpEdit8,
            this.repositoryItemLookUpEdit9,
            this.riRoutePayHold,
            this.riEmpty,
            this.riHold});
            this.gcHeader.Size = new System.Drawing.Size(997, 441);
            this.gcHeader.TabIndex = 0;
            this.gcHeader.UseEmbeddedNavigator = true;
            this.gcHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // dsHeader1
            // 
            this.dsHeader1.DataSetName = "dsHeader";
            this.dsHeader1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsHeader1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSUPPLIER,
            this.colINV_NO,
            this.colACCT_PERIOD,
            this.colACCT_YEAR,
            this.colTRANS_DATE,
            this.colINV_DATE,
            this.colDUE_DATE,
            this.colDISCOUNT_DATE,
            this.colINV_AMOUNT,
            this.colDISCOUNT_AMOUNT,
            this.colREFERENCE,
            this.colOPERATOR,
            this.colSUPP_NAME,
            this.colGST_CODE,
            this.colPURCH_AMT,
            this.colGST_AMT,
            this.colHOLD_PCT,
            this.colHOLD_AMT,
            this.colHOLD_PAY_DATE,
            this.colCURRENCY_ID,
            this.colINVOICE_TYPE,
            this.colMANUAL_CHECK,
            this.colSALES_TAX_ID,
            this.colCOMMENT,
            this.colPO_ID,
            this.colSOX_ROUTING,
            this.colSOX_APPROVAL,
            this.colAP_SETUP_GL_ID,
            this.colAP_DIV,
            this.colBALANCE,
            this.colKC_PAYHOLD_STATUS,
            this.colPAYMENT_HOLD});
            this.gvHeader.GridControl = this.gcHeader;
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.ShowFooter = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colINV_NO, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvHeader.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvHeader_CustomRowCellEdit);
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvHeader_FocusedRowChanged);
            // 
            // colSUPPLIER
            // 
            this.colSUPPLIER.Caption = "Supplier";
            this.colSUPPLIER.FieldName = "SUPPLIER";
            this.colSUPPLIER.Name = "colSUPPLIER";
            this.colSUPPLIER.OptionsColumn.AllowEdit = false;
            this.colSUPPLIER.Visible = true;
            this.colSUPPLIER.VisibleIndex = 2;
            // 
            // colINV_NO
            // 
            this.colINV_NO.Caption = "Invoice Number";
            this.colINV_NO.FieldName = "INV_NO";
            this.colINV_NO.Name = "colINV_NO";
            this.colINV_NO.OptionsColumn.AllowEdit = false;
            this.colINV_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colINV_NO.Visible = true;
            this.colINV_NO.VisibleIndex = 5;
            this.colINV_NO.Width = 110;
            // 
            // colACCT_PERIOD
            // 
            this.colACCT_PERIOD.Caption = "Period";
            this.colACCT_PERIOD.FieldName = "ACCT_PERIOD";
            this.colACCT_PERIOD.Name = "colACCT_PERIOD";
            this.colACCT_PERIOD.OptionsColumn.AllowEdit = false;
            // 
            // colACCT_YEAR
            // 
            this.colACCT_YEAR.Caption = "Year";
            this.colACCT_YEAR.FieldName = "ACCT_YEAR";
            this.colACCT_YEAR.Name = "colACCT_YEAR";
            this.colACCT_YEAR.OptionsColumn.AllowEdit = false;
            // 
            // colTRANS_DATE
            // 
            this.colTRANS_DATE.Caption = "Entry Date";
            this.colTRANS_DATE.FieldName = "TRANS_DATE";
            this.colTRANS_DATE.Name = "colTRANS_DATE";
            this.colTRANS_DATE.OptionsColumn.AllowEdit = false;
            this.colTRANS_DATE.Visible = true;
            this.colTRANS_DATE.VisibleIndex = 0;
            // 
            // colINV_DATE
            // 
            this.colINV_DATE.Caption = "Invoice Date";
            this.colINV_DATE.FieldName = "INV_DATE";
            this.colINV_DATE.Name = "colINV_DATE";
            this.colINV_DATE.OptionsColumn.AllowEdit = false;
            this.colINV_DATE.Visible = true;
            this.colINV_DATE.VisibleIndex = 7;
            this.colINV_DATE.Width = 83;
            // 
            // colDUE_DATE
            // 
            this.colDUE_DATE.Caption = "Due Date";
            this.colDUE_DATE.FieldName = "DUE_DATE";
            this.colDUE_DATE.Name = "colDUE_DATE";
            this.colDUE_DATE.OptionsColumn.AllowEdit = false;
            this.colDUE_DATE.Visible = true;
            this.colDUE_DATE.VisibleIndex = 8;
            // 
            // colDISCOUNT_DATE
            // 
            this.colDISCOUNT_DATE.Caption = "Discount Date";
            this.colDISCOUNT_DATE.FieldName = "DISCOUNT_DATE";
            this.colDISCOUNT_DATE.Name = "colDISCOUNT_DATE";
            this.colDISCOUNT_DATE.OptionsColumn.AllowEdit = false;
            // 
            // colINV_AMOUNT
            // 
            this.colINV_AMOUNT.Caption = "Invoice Amount";
            this.colINV_AMOUNT.DisplayFormat.FormatString = "n2";
            this.colINV_AMOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colINV_AMOUNT.FieldName = "INV_AMOUNT";
            this.colINV_AMOUNT.Name = "colINV_AMOUNT";
            this.colINV_AMOUNT.OptionsColumn.AllowEdit = false;
            this.colINV_AMOUNT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "INV_AMOUNT", "{0:n}")});
            this.colINV_AMOUNT.Visible = true;
            this.colINV_AMOUNT.VisibleIndex = 11;
            this.colINV_AMOUNT.Width = 97;
            // 
            // colDISCOUNT_AMOUNT
            // 
            this.colDISCOUNT_AMOUNT.Caption = "Discount";
            this.colDISCOUNT_AMOUNT.DisplayFormat.FormatString = "n2";
            this.colDISCOUNT_AMOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDISCOUNT_AMOUNT.FieldName = "DISCOUNT_AMOUNT";
            this.colDISCOUNT_AMOUNT.Name = "colDISCOUNT_AMOUNT";
            this.colDISCOUNT_AMOUNT.OptionsColumn.AllowEdit = false;
            // 
            // colREFERENCE
            // 
            this.colREFERENCE.Caption = "Reference";
            this.colREFERENCE.FieldName = "REFERENCE";
            this.colREFERENCE.Name = "colREFERENCE";
            this.colREFERENCE.OptionsColumn.AllowEdit = false;
            // 
            // colOPERATOR
            // 
            this.colOPERATOR.Caption = "Entered By";
            this.colOPERATOR.FieldName = "OPERATOR";
            this.colOPERATOR.Name = "colOPERATOR";
            this.colOPERATOR.OptionsColumn.AllowEdit = false;
            // 
            // colSUPP_NAME
            // 
            this.colSUPP_NAME.Caption = "Name";
            this.colSUPP_NAME.FieldName = "SUPP_NAME";
            this.colSUPP_NAME.Name = "colSUPP_NAME";
            this.colSUPP_NAME.OptionsColumn.AllowEdit = false;
            this.colSUPP_NAME.Visible = true;
            this.colSUPP_NAME.VisibleIndex = 3;
            this.colSUPP_NAME.Width = 205;
            // 
            // colGST_CODE
            // 
            this.colGST_CODE.Caption = "GST";
            this.colGST_CODE.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colGST_CODE.FieldName = "GST_CODE";
            this.colGST_CODE.Name = "colGST_CODE";
            this.colGST_CODE.OptionsColumn.AllowEdit = false;
            this.colGST_CODE.Visible = true;
            this.colGST_CODE.VisibleIndex = 9;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit3.DataSource = this.dsGST1.GST_CODES;
            this.repositoryItemLookUpEdit3.DisplayMember = "GST_DESC";
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.NullText = "";
            this.repositoryItemLookUpEdit3.ValueMember = "GST_CODE";
            // 
            // dsGST1
            // 
            this.dsGST1.DataSetName = "dsGST";
            this.dsGST1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsGST1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colPURCH_AMT
            // 
            this.colPURCH_AMT.Caption = "Purchase Amount";
            this.colPURCH_AMT.DisplayFormat.FormatString = "n2";
            this.colPURCH_AMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPURCH_AMT.FieldName = "PURCH_AMT";
            this.colPURCH_AMT.Name = "colPURCH_AMT";
            this.colPURCH_AMT.OptionsColumn.AllowEdit = false;
            this.colPURCH_AMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PURCH_AMT", "{0:n}")});
            this.colPURCH_AMT.Visible = true;
            this.colPURCH_AMT.VisibleIndex = 12;
            this.colPURCH_AMT.Width = 106;
            // 
            // colGST_AMT
            // 
            this.colGST_AMT.Caption = "GST Amount";
            this.colGST_AMT.DisplayFormat.FormatString = "n2";
            this.colGST_AMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGST_AMT.FieldName = "GST_AMT";
            this.colGST_AMT.Name = "colGST_AMT";
            this.colGST_AMT.OptionsColumn.AllowEdit = false;
            this.colGST_AMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GST_AMT", "{0:n}")});
            this.colGST_AMT.Visible = true;
            this.colGST_AMT.VisibleIndex = 13;
            this.colGST_AMT.Width = 81;
            // 
            // colHOLD_PCT
            // 
            this.colHOLD_PCT.Caption = "Holdback %";
            this.colHOLD_PCT.DisplayFormat.FormatString = "n2";
            this.colHOLD_PCT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHOLD_PCT.FieldName = "HOLD_PCT";
            this.colHOLD_PCT.Name = "colHOLD_PCT";
            this.colHOLD_PCT.OptionsColumn.AllowEdit = false;
            // 
            // colHOLD_AMT
            // 
            this.colHOLD_AMT.Caption = "Holdback $";
            this.colHOLD_AMT.DisplayFormat.FormatString = "n2";
            this.colHOLD_AMT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHOLD_AMT.FieldName = "HOLD_AMT";
            this.colHOLD_AMT.Name = "colHOLD_AMT";
            this.colHOLD_AMT.OptionsColumn.AllowEdit = false;
            // 
            // colHOLD_PAY_DATE
            // 
            this.colHOLD_PAY_DATE.Caption = "Holdback Due";
            this.colHOLD_PAY_DATE.FieldName = "HOLD_PAY_DATE";
            this.colHOLD_PAY_DATE.Name = "colHOLD_PAY_DATE";
            this.colHOLD_PAY_DATE.OptionsColumn.AllowEdit = false;
            // 
            // colCURRENCY_ID
            // 
            this.colCURRENCY_ID.Caption = "Currency";
            this.colCURRENCY_ID.ColumnEdit = this.repositoryItemLookUpEdit9;
            this.colCURRENCY_ID.FieldName = "CURRENCY_ID";
            this.colCURRENCY_ID.Name = "colCURRENCY_ID";
            this.colCURRENCY_ID.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemLookUpEdit9
            // 
            this.repositoryItemLookUpEdit9.AutoHeight = false;
            this.repositoryItemLookUpEdit9.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit9.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_ID", "CURRENCY_ID", 90, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Currency", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit9.DataSource = this.dsCurrency1.CURRENCY;
            this.repositoryItemLookUpEdit9.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit9.Name = "repositoryItemLookUpEdit9";
            this.repositoryItemLookUpEdit9.NullText = "";
            this.repositoryItemLookUpEdit9.ValueMember = "CURRENCY_ID";
            // 
            // dsCurrency1
            // 
            this.dsCurrency1.DataSetName = "dsCurrency";
            this.dsCurrency1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsCurrency1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colINVOICE_TYPE
            // 
            this.colINVOICE_TYPE.Caption = "Invoice Type";
            this.colINVOICE_TYPE.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colINVOICE_TYPE.FieldName = "INVOICE_TYPE";
            this.colINVOICE_TYPE.Name = "colINVOICE_TYPE";
            this.colINVOICE_TYPE.OptionsColumn.AllowEdit = false;
            this.colINVOICE_TYPE.Visible = true;
            this.colINVOICE_TYPE.VisibleIndex = 1;
            this.colINVOICE_TYPE.Width = 84;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            // 
            // colMANUAL_CHECK
            // 
            this.colMANUAL_CHECK.Caption = "Manual Check #";
            this.colMANUAL_CHECK.FieldName = "MANUAL_CHECK";
            this.colMANUAL_CHECK.Name = "colMANUAL_CHECK";
            this.colMANUAL_CHECK.OptionsColumn.AllowEdit = false;
            // 
            // colSALES_TAX_ID
            // 
            this.colSALES_TAX_ID.Caption = "PST";
            this.colSALES_TAX_ID.ColumnEdit = this.repositoryItemLookUpEdit4;
            this.colSALES_TAX_ID.FieldName = "SALES_TAX_ID";
            this.colSALES_TAX_ID.Name = "colSALES_TAX_ID";
            this.colSALES_TAX_ID.OptionsColumn.AllowEdit = false;
            this.colSALES_TAX_ID.Visible = true;
            this.colSALES_TAX_ID.VisibleIndex = 10;
            // 
            // repositoryItemLookUpEdit4
            // 
            this.repositoryItemLookUpEdit4.AutoHeight = false;
            this.repositoryItemLookUpEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit4.DataSource = this.dsPST1.SALES_TAXES;
            this.repositoryItemLookUpEdit4.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit4.Name = "repositoryItemLookUpEdit4";
            this.repositoryItemLookUpEdit4.NullText = "";
            this.repositoryItemLookUpEdit4.ValueMember = "SALES_TAX_ID";
            // 
            // dsPST1
            // 
            this.dsPST1.DataSetName = "dsPST";
            this.dsPST1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPST1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colCOMMENT
            // 
            this.colCOMMENT.Caption = "Comment";
            this.colCOMMENT.FieldName = "COMMENT";
            this.colCOMMENT.Name = "colCOMMENT";
            this.colCOMMENT.OptionsColumn.AllowEdit = false;
            // 
            // colPO_ID
            // 
            this.colPO_ID.Caption = "PO #";
            this.colPO_ID.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.colPO_ID.FieldName = "PO_ID";
            this.colPO_ID.Name = "colPO_ID";
            this.colPO_ID.OptionsColumn.AllowEdit = false;
            this.colPO_ID.Visible = true;
            this.colPO_ID.VisibleIndex = 6;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_ID", "PO_ID", 50, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO", "PO #", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ORDER_DATE", "PO Date", 125, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit2.DataSource = this.dsAllPO1.PO_HEADER;
            this.repositoryItemLookUpEdit2.DisplayMember = "PO";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.ValueMember = "PO_ID";
            // 
            // dsAllPO1
            // 
            this.dsAllPO1.DataSetName = "dsAllPO";
            this.dsAllPO1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsAllPO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colSOX_ROUTING
            // 
            this.colSOX_ROUTING.Caption = "Key Control Routing";
            this.colSOX_ROUTING.FieldName = "SOX_ROUTING";
            this.colSOX_ROUTING.Name = "colSOX_ROUTING";
            this.colSOX_ROUTING.OptionsColumn.AllowEdit = false;
            // 
            // colSOX_APPROVAL
            // 
            this.colSOX_APPROVAL.Caption = "Key Control Approval";
            this.colSOX_APPROVAL.FieldName = "SOX_APPROVAL";
            this.colSOX_APPROVAL.Name = "colSOX_APPROVAL";
            this.colSOX_APPROVAL.OptionsColumn.AllowEdit = false;
            // 
            // colAP_SETUP_GL_ID
            // 
            this.colAP_SETUP_GL_ID.Caption = "AP Control";
            this.colAP_SETUP_GL_ID.FieldName = "AP_SETUP_GL_ID";
            this.colAP_SETUP_GL_ID.Name = "colAP_SETUP_GL_ID";
            this.colAP_SETUP_GL_ID.OptionsColumn.AllowEdit = false;
            // 
            // colAP_DIV
            // 
            this.colAP_DIV.Caption = "AP_DIV";
            this.colAP_DIV.ColumnEdit = this.repositoryItemLookUpEdit8;
            this.colAP_DIV.FieldName = "AP_DIV";
            this.colAP_DIV.Name = "colAP_DIV";
            this.colAP_DIV.OptionsColumn.AllowEdit = false;
            this.colAP_DIV.Visible = true;
            this.colAP_DIV.VisibleIndex = 4;
            this.colAP_DIV.Width = 85;
            // 
            // repositoryItemLookUpEdit8
            // 
            this.repositoryItemLookUpEdit8.AutoHeight = false;
            this.repositoryItemLookUpEdit8.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit8.DataSource = this.dsSwapSeg1.GL_ACCOUNTS;
            this.repositoryItemLookUpEdit8.DisplayMember = "Description";
            this.repositoryItemLookUpEdit8.Name = "repositoryItemLookUpEdit8";
            this.repositoryItemLookUpEdit8.NullText = "";
            this.repositoryItemLookUpEdit8.ValueMember = "Code";
            // 
            // dsSwapSeg1
            // 
            this.dsSwapSeg1.DataSetName = "dsSwapSeg";
            this.dsSwapSeg1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsSwapSeg1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colBALANCE
            // 
            this.colBALANCE.Caption = "Balance";
            this.colBALANCE.DisplayFormat.FormatString = "N2";
            this.colBALANCE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBALANCE.FieldName = "BALANCE";
            this.colBALANCE.Name = "colBALANCE";
            this.colBALANCE.OptionsColumn.AllowEdit = false;
            this.colBALANCE.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BALANCE", "{0:n2}")});
            this.colBALANCE.Visible = true;
            this.colBALANCE.VisibleIndex = 14;
            // 
            // colKC_PAYHOLD_STATUS
            // 
            this.colKC_PAYHOLD_STATUS.Caption = "Subcontractor Payment Hold Status";
            this.colKC_PAYHOLD_STATUS.ColumnEdit = this.riRoutePayHold;
            this.colKC_PAYHOLD_STATUS.FieldName = "KC_PAYHOLD_STATUS";
            this.colKC_PAYHOLD_STATUS.Name = "colKC_PAYHOLD_STATUS";
            this.colKC_PAYHOLD_STATUS.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colKC_PAYHOLD_STATUS.Visible = true;
            this.colKC_PAYHOLD_STATUS.VisibleIndex = 15;
            this.colKC_PAYHOLD_STATUS.Width = 162;
            // 
            // riRoutePayHold
            // 
            this.riRoutePayHold.AutoHeight = false;
            this.riRoutePayHold.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riRoutePayHold.Name = "riRoutePayHold";
            this.riRoutePayHold.NullText = "";
            this.riRoutePayHold.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.riRouting_QueryPopUp);
            this.riRoutePayHold.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riRoutePayHold_ButtonClick);
            // 
            // colPAYMENT_HOLD
            // 
            this.colPAYMENT_HOLD.Caption = "Payment Hold";
            this.colPAYMENT_HOLD.ColumnEdit = this.riHold;
            this.colPAYMENT_HOLD.FieldName = "PAYMENT_HOLD";
            this.colPAYMENT_HOLD.Name = "colPAYMENT_HOLD";
            this.colPAYMENT_HOLD.OptionsColumn.AllowEdit = false;
            // 
            // riHold
            // 
            this.riHold.AutoHeight = false;
            this.riHold.Name = "riHold";
            this.riHold.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riHold.ValueChecked = "T";
            this.riHold.ValueUnchecked = "F";
            // 
            // riEmpty
            // 
            this.riEmpty.AutoHeight = false;
            this.riEmpty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, false)});
            this.riEmpty.Name = "riEmpty";
            this.riEmpty.NullText = "";
            this.riEmpty.ReadOnly = true;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer2,
            this.panelContainer3});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panelContainer2
            // 
            this.panelContainer2.Controls.Add(this.panelContainer1);
            this.panelContainer2.Controls.Add(this.dpActions);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer2.ID = new System.Guid("36481668-3de6-4542-a471-034639686bec");
            this.panelContainer2.Location = new System.Drawing.Point(997, 0);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.OriginalSize = new System.Drawing.Size(355, 736);
            this.panelContainer2.Size = new System.Drawing.Size(355, 736);
            this.panelContainer2.Text = "panelContainer2";
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanel2;
            this.panelContainer1.Controls.Add(this.dockPanel2);
            this.panelContainer1.Controls.Add(this.dockPanel5);
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Controls.Add(this.dockPanel4);
            this.panelContainer1.Controls.Add(this.dockPanel3);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer1.ID = new System.Guid("7f5a29d6-8d8e-42a2-8550-f6999eab6498");
            this.panelContainer1.Location = new System.Drawing.Point(0, 0);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(0, 0);
            this.panelContainer1.Size = new System.Drawing.Size(355, 489);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Right;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("5b0586f6-e3ca-458a-9c4d-121313635810");
            this.dockPanel2.Location = new System.Drawing.Point(5, 23);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(327, 461);
            this.dockPanel2.Size = new System.Drawing.Size(319, 461);
            this.dockPanel2.Text = "Invoice Defaults";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.layoutControl2);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(319, 461);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.chkPaymentHold);
            this.layoutControl2.Controls.Add(this.txtManChk);
            this.layoutControl2.Controls.Add(this.deHoldDue);
            this.layoutControl2.Controls.Add(this.txtHoldA);
            this.layoutControl2.Controls.Add(this.txtHoldP);
            this.layoutControl2.Controls.Add(this.deDiscDate);
            this.layoutControl2.Controls.Add(this.txtDiscA);
            this.layoutControl2.Controls.Add(this.lueTerms);
            this.layoutControl2.Controls.Add(this.lueCurrency);
            this.layoutControl2.Controls.Add(this.lueInvType);
            this.layoutControl2.Controls.Add(this.lueAPCntl);
            this.layoutControl2.Controls.Add(this.ucAccountingPicker1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(319, 461);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // chkPaymentHold
            // 
            this.chkPaymentHold.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.PAYMENT_HOLD", true));
            this.chkPaymentHold.Location = new System.Drawing.Point(12, 322);
            this.chkPaymentHold.Name = "chkPaymentHold";
            this.chkPaymentHold.Properties.Caption = "Payment Hold";
            this.chkPaymentHold.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkPaymentHold.Properties.ReadOnly = true;
            this.chkPaymentHold.Properties.ValueChecked = "T";
            this.chkPaymentHold.Properties.ValueUnchecked = "F";
            this.chkPaymentHold.Size = new System.Drawing.Size(295, 19);
            this.chkPaymentHold.StyleController = this.layoutControl2;
            this.chkPaymentHold.TabIndex = 19;
            // 
            // txtManChk
            // 
            this.txtManChk.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.MANUAL_CHECK", true));
            this.txtManChk.Location = new System.Drawing.Point(82, 298);
            this.txtManChk.Name = "txtManChk";
            this.txtManChk.Properties.Mask.EditMask = "g0";
            this.txtManChk.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtManChk.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtManChk.Properties.ReadOnly = true;
            this.txtManChk.Size = new System.Drawing.Size(225, 20);
            this.txtManChk.StyleController = this.layoutControl2;
            this.txtManChk.TabIndex = 15;
            // 
            // deHoldDue
            // 
            this.deHoldDue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.HOLD_PAY_DATE", true));
            this.deHoldDue.EditValue = null;
            this.deHoldDue.Location = new System.Drawing.Point(82, 274);
            this.deHoldDue.Name = "deHoldDue";
            this.deHoldDue.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deHoldDue.Properties.ReadOnly = true;
            this.deHoldDue.Size = new System.Drawing.Size(225, 20);
            this.deHoldDue.StyleController = this.layoutControl2;
            this.deHoldDue.TabIndex = 14;
            // 
            // txtHoldA
            // 
            this.txtHoldA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.HOLD_AMT", true));
            this.txtHoldA.Location = new System.Drawing.Point(82, 250);
            this.txtHoldA.Name = "txtHoldA";
            this.txtHoldA.Properties.Mask.EditMask = "n2";
            this.txtHoldA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHoldA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHoldA.Properties.ReadOnly = true;
            this.txtHoldA.Size = new System.Drawing.Size(225, 20);
            this.txtHoldA.StyleController = this.layoutControl2;
            this.txtHoldA.TabIndex = 13;
            // 
            // txtHoldP
            // 
            this.txtHoldP.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.HOLD_PCT", true));
            this.txtHoldP.Location = new System.Drawing.Point(82, 226);
            this.txtHoldP.Name = "txtHoldP";
            this.txtHoldP.Properties.Mask.EditMask = "n2";
            this.txtHoldP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHoldP.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHoldP.Properties.ReadOnly = true;
            this.txtHoldP.Size = new System.Drawing.Size(225, 20);
            this.txtHoldP.StyleController = this.layoutControl2;
            this.txtHoldP.TabIndex = 12;
            // 
            // deDiscDate
            // 
            this.deDiscDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.DISCOUNT_DATE", true));
            this.deDiscDate.EditValue = null;
            this.deDiscDate.Location = new System.Drawing.Point(82, 202);
            this.deDiscDate.Name = "deDiscDate";
            this.deDiscDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDiscDate.Properties.ReadOnly = true;
            this.deDiscDate.Size = new System.Drawing.Size(225, 20);
            this.deDiscDate.StyleController = this.layoutControl2;
            this.deDiscDate.TabIndex = 11;
            // 
            // txtDiscA
            // 
            this.txtDiscA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.DISCOUNT_AMOUNT", true));
            this.txtDiscA.Location = new System.Drawing.Point(82, 178);
            this.txtDiscA.Name = "txtDiscA";
            this.txtDiscA.Properties.Mask.EditMask = "n2";
            this.txtDiscA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDiscA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDiscA.Properties.ReadOnly = true;
            this.txtDiscA.Size = new System.Drawing.Size(225, 20);
            this.txtDiscA.StyleController = this.layoutControl2;
            this.txtDiscA.TabIndex = 10;
            // 
            // lueTerms
            // 
            this.lueTerms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.TERMS_ID", true));
            this.lueTerms.Location = new System.Drawing.Point(82, 154);
            this.lueTerms.Name = "lueTerms";
            this.lueTerms.Properties.DataSource = this.tERMSBindingSource;
            this.lueTerms.Properties.DisplayMember = "DESCRIPTION";
            this.lueTerms.Properties.NullText = "";
            this.lueTerms.Properties.ReadOnly = true;
            this.lueTerms.Properties.ValueMember = "TERMS_ID";
            this.lueTerms.Size = new System.Drawing.Size(225, 20);
            this.lueTerms.StyleController = this.layoutControl2;
            this.lueTerms.TabIndex = 8;
            // 
            // tERMSBindingSource
            // 
            this.tERMSBindingSource.DataMember = "TERMS";
            this.tERMSBindingSource.DataSource = this.dsTerms1;
            // 
            // dsTerms1
            // 
            this.dsTerms1.DataSetName = "dsTerms";
            this.dsTerms1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueCurrency
            // 
            this.lueCurrency.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.CURRENCY_ID", true));
            this.lueCurrency.Location = new System.Drawing.Point(82, 130);
            this.lueCurrency.Name = "lueCurrency";
            this.lueCurrency.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_ID", "CURRENCY_ID", 90, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueCurrency.Properties.DataSource = this.dsCurrency1.CURRENCY;
            this.lueCurrency.Properties.DisplayMember = "DESCRIPTION";
            this.lueCurrency.Properties.NullText = "";
            this.lueCurrency.Properties.ReadOnly = true;
            this.lueCurrency.Properties.ValueMember = "CURRENCY_ID";
            this.lueCurrency.Size = new System.Drawing.Size(225, 20);
            this.lueCurrency.StyleController = this.layoutControl2;
            this.lueCurrency.TabIndex = 7;
            // 
            // lueInvType
            // 
            this.lueInvType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.INVOICE_TYPE", true));
            this.lueInvType.Location = new System.Drawing.Point(82, 106);
            this.lueInvType.Name = "lueInvType";
            this.lueInvType.Properties.NullText = "";
            this.lueInvType.Properties.ReadOnly = true;
            this.lueInvType.Size = new System.Drawing.Size(225, 20);
            this.lueInvType.StyleController = this.layoutControl2;
            this.lueInvType.TabIndex = 6;
            // 
            // lueAPCntl
            // 
            this.lueAPCntl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.AP_SETUP_GL_ID", true));
            this.lueAPCntl.Location = new System.Drawing.Point(82, 82);
            this.lueAPCntl.Name = "lueAPCntl";
            this.lueAPCntl.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AP_SETUP_GL_ID", "AP_SETUP_GL_ID", 104, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "AP Control", 74, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ACCOUNT", "GL Account", 73, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueAPCntl.Properties.DataSource = this.dsAPSetupGL1.AP_SETUP_GL;
            this.lueAPCntl.Properties.DisplayMember = "DESCRIPTION";
            this.lueAPCntl.Properties.NullText = "";
            this.lueAPCntl.Properties.ReadOnly = true;
            this.lueAPCntl.Properties.ValueMember = "AP_SETUP_GL_ID";
            this.lueAPCntl.Size = new System.Drawing.Size(225, 20);
            this.lueAPCntl.StyleController = this.layoutControl2;
            this.lueAPCntl.TabIndex = 5;
            // 
            // dsAPSetupGL1
            // 
            this.dsAPSetupGL1.DataSetName = "dsAPSetupGL";
            this.dsAPSetupGL1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsAPSetupGL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ucAccountingPicker1
            // 
            this.ucAccountingPicker1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAccountingPicker1.Appearance.Options.UseBackColor = true;
            this.ucAccountingPicker1.HasEntryDate = false;
            this.ucAccountingPicker1.Location = new System.Drawing.Point(12, 12);
            this.ucAccountingPicker1.Name = "ucAccountingPicker1";
            this.ucAccountingPicker1.ReadOnly = true;
            this.ucAccountingPicker1.SelectedPeriod = 0;
            this.ucAccountingPicker1.SelectedYear = 0;
            this.ucAccountingPicker1.Size = new System.Drawing.Size(295, 66);
            this.ucAccountingPicker1.TabIndex = 4;
            this.ucAccountingPicker1.UserName = "";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(319, 461);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ucAccountingPicker1;
            this.layoutControlItem4.CustomizationFormText = "Period";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 80);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(111, 70);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(299, 70);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Period";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lueAPCntl;
            this.layoutControlItem5.CustomizationFormText = "AP Control";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 70);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem5.Text = "AP Control";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lueInvType;
            this.layoutControlItem6.CustomizationFormText = "Invoice Type";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem6.Text = "Invoice Type";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lueCurrency;
            this.layoutControlItem7.CustomizationFormText = "Currency";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 118);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem7.Text = "Currency";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lueTerms;
            this.layoutControlItem8.CustomizationFormText = "Terms";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem8.Text = "Terms";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtDiscA;
            this.layoutControlItem10.CustomizationFormText = "Discount $";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem10.Text = "Discount $";
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.deDiscDate;
            this.layoutControlItem11.CustomizationFormText = "Discount Date";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem11.Text = "Discount Date";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtHoldP;
            this.layoutControlItem12.CustomizationFormText = "Holdback %";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem12.Text = "Holdback %";
            this.layoutControlItem12.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtHoldA;
            this.layoutControlItem13.CustomizationFormText = "Holdback $";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem13.Text = "Holdback $";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.deHoldDue;
            this.layoutControlItem14.CustomizationFormText = "Holdback Due";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem14.Text = "Holdback Due";
            this.layoutControlItem14.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.txtManChk;
            this.layoutControlItem15.CustomizationFormText = "Manual Ck #";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 286);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem15.Text = "Manual Ck #";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(67, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 333);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(299, 108);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkPaymentHold;
            this.layoutControlItem3.CustomizationFormText = "Payment Hold";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 310);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(299, 23);
            this.layoutControlItem3.Text = "Payment Hold";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // dockPanel5
            // 
            this.dockPanel5.Controls.Add(this.dockPanel5_Container);
            this.dockPanel5.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel5.ID = new System.Guid("2a2b8f2f-93a3-4fe5-a3ca-fe356004c0fd");
            this.dockPanel5.Location = new System.Drawing.Point(5, 23);
            this.dockPanel5.Name = "dockPanel5";
            this.dockPanel5.OriginalSize = new System.Drawing.Size(327, 461);
            this.dockPanel5.Size = new System.Drawing.Size(319, 461);
            this.dockPanel5.Text = "Remit To";
            // 
            // dockPanel5_Container
            // 
            this.dockPanel5_Container.Controls.Add(this.layoutControl3);
            this.dockPanel5_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel5_Container.Name = "dockPanel5_Container";
            this.dockPanel5_Container.Size = new System.Drawing.Size(319, 461);
            this.dockPanel5_Container.TabIndex = 0;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.txtRAcctNo);
            this.layoutControl3.Controls.Add(this.txtRZip);
            this.layoutControl3.Controls.Add(this.txtRState);
            this.layoutControl3.Controls.Add(this.txtRCity);
            this.layoutControl3.Controls.Add(this.txtRAddr3);
            this.layoutControl3.Controls.Add(this.txtRAddr2);
            this.layoutControl3.Controls.Add(this.txtRAddr1);
            this.layoutControl3.Controls.Add(this.txtRName);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(319, 461);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // txtRAcctNo
            // 
            this.txtRAcctNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.SUPP_ACCOUNT", true));
            this.txtRAcctNo.Location = new System.Drawing.Point(65, 156);
            this.txtRAcctNo.Name = "txtRAcctNo";
            this.txtRAcctNo.Properties.ReadOnly = true;
            this.txtRAcctNo.Size = new System.Drawing.Size(242, 20);
            this.txtRAcctNo.StyleController = this.layoutControl3;
            this.txtRAcctNo.TabIndex = 11;
            // 
            // txtRZip
            // 
            this.txtRZip.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITZIP", true));
            this.txtRZip.Location = new System.Drawing.Point(214, 132);
            this.txtRZip.Name = "txtRZip";
            this.txtRZip.Properties.ReadOnly = true;
            this.txtRZip.Size = new System.Drawing.Size(93, 20);
            this.txtRZip.StyleController = this.layoutControl3;
            this.txtRZip.TabIndex = 10;
            // 
            // txtRState
            // 
            this.txtRState.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITSTATE", true));
            this.txtRState.Location = new System.Drawing.Point(65, 132);
            this.txtRState.Name = "txtRState";
            this.txtRState.Properties.ReadOnly = true;
            this.txtRState.Size = new System.Drawing.Size(92, 20);
            this.txtRState.StyleController = this.layoutControl3;
            this.txtRState.TabIndex = 9;
            // 
            // txtRCity
            // 
            this.txtRCity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITCITY", true));
            this.txtRCity.Location = new System.Drawing.Point(65, 108);
            this.txtRCity.Name = "txtRCity";
            this.txtRCity.Properties.ReadOnly = true;
            this.txtRCity.Size = new System.Drawing.Size(242, 20);
            this.txtRCity.StyleController = this.layoutControl3;
            this.txtRCity.TabIndex = 8;
            // 
            // txtRAddr3
            // 
            this.txtRAddr3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITADD3", true));
            this.txtRAddr3.Location = new System.Drawing.Point(65, 84);
            this.txtRAddr3.Name = "txtRAddr3";
            this.txtRAddr3.Properties.ReadOnly = true;
            this.txtRAddr3.Size = new System.Drawing.Size(242, 20);
            this.txtRAddr3.StyleController = this.layoutControl3;
            this.txtRAddr3.TabIndex = 7;
            // 
            // txtRAddr2
            // 
            this.txtRAddr2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITADD2", true));
            this.txtRAddr2.Location = new System.Drawing.Point(65, 60);
            this.txtRAddr2.Name = "txtRAddr2";
            this.txtRAddr2.Properties.ReadOnly = true;
            this.txtRAddr2.Size = new System.Drawing.Size(242, 20);
            this.txtRAddr2.StyleController = this.layoutControl3;
            this.txtRAddr2.TabIndex = 6;
            // 
            // txtRAddr1
            // 
            this.txtRAddr1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.REMITADD1", true));
            this.txtRAddr1.Location = new System.Drawing.Point(65, 36);
            this.txtRAddr1.Name = "txtRAddr1";
            this.txtRAddr1.Properties.ReadOnly = true;
            this.txtRAddr1.Size = new System.Drawing.Size(242, 20);
            this.txtRAddr1.StyleController = this.layoutControl3;
            this.txtRAddr1.TabIndex = 5;
            // 
            // txtRName
            // 
            this.txtRName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeader1, "AP_INV_HEADER.SUPP_NAME", true));
            this.txtRName.Location = new System.Drawing.Point(65, 12);
            this.txtRName.Name = "txtRName";
            this.txtRName.Properties.ReadOnly = true;
            this.txtRName.Size = new System.Drawing.Size(242, 20);
            this.txtRName.StyleController = this.layoutControl3;
            this.txtRName.TabIndex = 4;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem26,
            this.emptySpaceItem2,
            this.layoutControlItem25});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(319, 461);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.txtRName;
            this.layoutControlItem19.CustomizationFormText = "Name";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem19.Text = "Name";
            this.layoutControlItem19.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.txtRAddr1;
            this.layoutControlItem20.CustomizationFormText = "Address 1";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem20.Text = "Address 1";
            this.layoutControlItem20.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem20.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.txtRAddr2;
            this.layoutControlItem21.CustomizationFormText = "Address 2";
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem21.Text = "Address 2";
            this.layoutControlItem21.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem21.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.txtRAddr3;
            this.layoutControlItem22.CustomizationFormText = "Address 3";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem22.Text = "Address 3";
            this.layoutControlItem22.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem22.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.txtRCity;
            this.layoutControlItem23.CustomizationFormText = "City";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem23.Text = "City";
            this.layoutControlItem23.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.txtRState;
            this.layoutControlItem24.CustomizationFormText = "State";
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(149, 24);
            this.layoutControlItem24.Text = "State";
            this.layoutControlItem24.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem24.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.txtRAcctNo;
            this.layoutControlItem26.CustomizationFormText = "Account #";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem26.Text = "Account #";
            this.layoutControlItem26.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem26.TextSize = new System.Drawing.Size(50, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 168);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(299, 273);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.txtRZip;
            this.layoutControlItem25.CustomizationFormText = "Zip";
            this.layoutControlItem25.Location = new System.Drawing.Point(149, 120);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(150, 24);
            this.layoutControlItem25.Text = "Zip";
            this.layoutControlItem25.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem25.TextSize = new System.Drawing.Size(50, 13);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("768163d2-ccc1-4edc-ae88-1ede0391fcab");
            this.dockPanel1.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(327, 461);
            this.dockPanel1.Size = new System.Drawing.Size(319, 461);
            this.dockPanel1.Text = "Comment";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.layoutControl4);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(319, 461);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.memoEdit1);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(319, 461);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(12, 28);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(295, 421);
            this.memoEdit1.StyleController = this.layoutControl4;
            this.memoEdit1.TabIndex = 4;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(319, 461);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.memoEdit1;
            this.layoutControlItem27.CustomizationFormText = "Comment";
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(299, 441);
            this.layoutControlItem27.Text = "Comment";
            this.layoutControlItem27.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem27.TextSize = new System.Drawing.Size(45, 13);
            // 
            // dockPanel4
            // 
            this.dockPanel4.Controls.Add(this.dockPanel4_Container);
            this.dockPanel4.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel4.ID = new System.Guid("32884800-4884-424f-8232-2f5018480462");
            this.dockPanel4.Location = new System.Drawing.Point(5, 23);
            this.dockPanel4.Name = "dockPanel4";
            this.dockPanel4.OriginalSize = new System.Drawing.Size(327, 461);
            this.dockPanel4.Size = new System.Drawing.Size(319, 461);
            this.dockPanel4.Text = "GST Exception";
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(319, 461);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.ID = new System.Guid("045c06f1-9789-4e03-9e11-beed5c1deef1");
            this.dockPanel3.Location = new System.Drawing.Point(5, 23);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(327, 461);
            this.dockPanel3.Size = new System.Drawing.Size(319, 461);
            this.dockPanel3.Text = "Search";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.layoutControl5);
            this.dockPanel3_Container.Controls.Add(this.panelControl1);
            this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(319, 461);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.deInvDate);
            this.layoutControl5.Controls.Add(this.lueInvNo);
            this.layoutControl5.Controls.Add(this.lueSuppName);
            this.layoutControl5.Controls.Add(this.lueSupplier);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(319, 417);
            this.layoutControl5.TabIndex = 0;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // deInvDate
            // 
            this.deInvDate.EditValue = null;
            this.deInvDate.Location = new System.Drawing.Point(83, 84);
            this.deInvDate.Name = "deInvDate";
            this.deInvDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInvDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deInvDate.Size = new System.Drawing.Size(224, 20);
            this.deInvDate.StyleController = this.layoutControl5;
            this.deInvDate.TabIndex = 7;
            // 
            // lueInvNo
            // 
            this.lueInvNo.Location = new System.Drawing.Point(83, 60);
            this.lueInvNo.Name = "lueInvNo";
            this.lueInvNo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueInvNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueInvNo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("INV_NO", "Invoice #", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueInvNo.Properties.DataSource = this.dsInvNo1.AP_INV_HEADER;
            this.lueInvNo.Properties.DisplayMember = "INV_NO";
            this.lueInvNo.Properties.NullText = "";
            this.lueInvNo.Properties.ValueMember = "INV_NO";
            this.lueInvNo.Size = new System.Drawing.Size(224, 20);
            this.lueInvNo.StyleController = this.layoutControl5;
            this.lueInvNo.TabIndex = 6;
            // 
            // dsInvNo1
            // 
            this.dsInvNo1.DataSetName = "dsInvNo";
            this.dsInvNo1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsInvNo1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueSuppName
            // 
            this.lueSuppName.Location = new System.Drawing.Point(83, 36);
            this.lueSuppName.Name = "lueSuppName";
            this.lueSuppName.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueSuppName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSuppName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueSuppName.Properties.DataSource = this.dsSupplier1.SUPPLIER_MASTER;
            this.lueSuppName.Properties.DisplayMember = "NAME";
            this.lueSuppName.Properties.NullText = "";
            this.lueSuppName.Properties.ValueMember = "SUPPLIER";
            this.lueSuppName.Size = new System.Drawing.Size(224, 20);
            this.lueSuppName.StyleController = this.layoutControl5;
            this.lueSuppName.TabIndex = 5;
            // 
            // dsSupplier1
            // 
            this.dsSupplier1.DataSetName = "dsSupplier";
            this.dsSupplier1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsSupplier1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueSupplier
            // 
            this.lueSupplier.Location = new System.Drawing.Point(83, 12);
            this.lueSupplier.Name = "lueSupplier";
            this.lueSupplier.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.lueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueSupplier.Properties.DataSource = this.dsSupplier1.SUPPLIER_MASTER;
            this.lueSupplier.Properties.DisplayMember = "SUPPLIER";
            this.lueSupplier.Properties.NullText = "";
            this.lueSupplier.Properties.ValueMember = "SUPPLIER";
            this.lueSupplier.Size = new System.Drawing.Size(224, 20);
            this.lueSupplier.StyleController = this.layoutControl5;
            this.lueSupplier.TabIndex = 4;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem32,
            this.emptySpaceItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(319, 417);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.lueSupplier;
            this.layoutControlItem29.CustomizationFormText = "Supplier Code";
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem29.Text = "Supplier Code";
            this.layoutControlItem29.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem29.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.lueSuppName;
            this.layoutControlItem30.CustomizationFormText = "Supplier Name";
            this.layoutControlItem30.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem30.Text = "Supplier Name";
            this.layoutControlItem30.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem30.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.lueInvNo;
            this.layoutControlItem31.CustomizationFormText = "Invoice #";
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem31.Text = "Invoice #";
            this.layoutControlItem31.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem31.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.deInvDate;
            this.layoutControlItem32.CustomizationFormText = "Invoice Date";
            this.layoutControlItem32.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(299, 24);
            this.layoutControlItem32.Text = "Invoice Date";
            this.layoutControlItem32.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem32.TextSize = new System.Drawing.Size(68, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 96);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(299, 301);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 417);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(319, 44);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnFilter);
            this.panelControl2.Controls.Add(this.btnReset);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(315, 40);
            this.panelControl2.TabIndex = 0;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(8, 8);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 8;
            this.btnFilter.Text = "Search";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(88, 8);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Clear";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // dpActions
            // 
            this.dpActions.Controls.Add(this.dockPanel6_Container);
            this.dpActions.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpActions.ID = new System.Guid("bd7aabbf-23c0-40c6-aae1-f369987b7cc8");
            this.dpActions.Location = new System.Drawing.Point(0, 489);
            this.dpActions.Name = "dpActions";
            this.dpActions.OriginalSize = new System.Drawing.Size(0, 0);
            this.dpActions.Size = new System.Drawing.Size(355, 247);
            this.dpActions.Text = "Actions";
            // 
            // dockPanel6_Container
            // 
            this.dockPanel6_Container.Controls.Add(this.layoutControl1);
            this.dockPanel6_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel6_Container.Name = "dockPanel6_Container";
            this.dockPanel6_Container.Size = new System.Drawing.Size(346, 220);
            this.dockPanel6_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.hlOverridePWPStatus);
            this.layoutControl1.Controls.Add(this.hlAddPayHold);
            this.layoutControl1.Controls.Add(this.hlReleasePayHold);
            this.layoutControl1.Controls.Add(this.hlRefresh);
            this.layoutControl1.Controls.Add(this.hlQikChk);
            this.layoutControl1.Controls.Add(this.hlManChk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(346, 220);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // hlOverridePWPStatus
            // 
            this.hlOverridePWPStatus.EditValue = "Override PWP Status";
            this.hlOverridePWPStatus.Location = new System.Drawing.Point(175, 80);
            this.hlOverridePWPStatus.Name = "hlOverridePWPStatus";
            this.hlOverridePWPStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlOverridePWPStatus.Properties.Appearance.Options.UseBackColor = true;
            this.hlOverridePWPStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlOverridePWPStatus.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlOverridePWPStatus.Properties.Image")));
            this.hlOverridePWPStatus.RESG_ImageType = ReflexImgSrc.Image.ImageType.Cancel2;
            this.hlOverridePWPStatus.Size = new System.Drawing.Size(159, 30);
            this.hlOverridePWPStatus.StyleController = this.layoutControl1;
            this.hlOverridePWPStatus.TabIndex = 19;
            this.hlOverridePWPStatus.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlOverridePWPStatus_OpenLink);
            // 
            // hlAddPayHold
            // 
            this.hlAddPayHold.EditValue = "Add Payment Hold";
            this.hlAddPayHold.Location = new System.Drawing.Point(175, 12);
            this.hlAddPayHold.Name = "hlAddPayHold";
            this.hlAddPayHold.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlAddPayHold.Properties.Appearance.Options.UseBackColor = true;
            this.hlAddPayHold.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlAddPayHold.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlAddPayHold.Properties.Image")));
            this.hlAddPayHold.RESG_ImageType = ReflexImgSrc.Image.ImageType.AddFile;
            this.hlAddPayHold.Size = new System.Drawing.Size(159, 30);
            this.hlAddPayHold.StyleController = this.layoutControl1;
            this.hlAddPayHold.TabIndex = 9;
            this.hlAddPayHold.Click += new System.EventHandler(this.hlAddPayHold_Click);
            // 
            // hlReleasePayHold
            // 
            this.hlReleasePayHold.EditValue = "Release Payment Hold";
            this.hlReleasePayHold.Location = new System.Drawing.Point(175, 46);
            this.hlReleasePayHold.Name = "hlReleasePayHold";
            this.hlReleasePayHold.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlReleasePayHold.Properties.Appearance.Options.UseBackColor = true;
            this.hlReleasePayHold.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlReleasePayHold.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlReleasePayHold.Properties.Image")));
            this.hlReleasePayHold.RESG_ImageType = ReflexImgSrc.Image.ImageType.Up2;
            this.hlReleasePayHold.Size = new System.Drawing.Size(159, 30);
            this.hlReleasePayHold.StyleController = this.layoutControl1;
            this.hlReleasePayHold.TabIndex = 8;
            this.hlReleasePayHold.Click += new System.EventHandler(this.hlReleasePayHold_Click);
            // 
            // hlRefresh
            // 
            this.hlRefresh.EditValue = "Refresh";
            this.hlRefresh.Location = new System.Drawing.Point(12, 80);
            this.hlRefresh.Name = "hlRefresh";
            this.hlRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlRefresh.Properties.Appearance.Options.UseBackColor = true;
            this.hlRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlRefresh.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlRefresh.Properties.Image")));
            this.hlRefresh.RESG_ImageType = ReflexImgSrc.Image.ImageType.Refresh2;
            this.hlRefresh.Size = new System.Drawing.Size(159, 27);
            this.hlRefresh.StyleController = this.layoutControl1;
            this.hlRefresh.TabIndex = 7;
            this.hlRefresh.Click += new System.EventHandler(this.hlRefresh_Click);
            // 
            // hlQikChk
            // 
            this.hlQikChk.EditValue = "Quick Check";
            this.hlQikChk.Location = new System.Drawing.Point(12, 46);
            this.hlQikChk.Name = "hlQikChk";
            this.hlQikChk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlQikChk.Properties.Appearance.Options.UseBackColor = true;
            this.hlQikChk.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlQikChk.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlQikChk.Properties.Image")));
            this.hlQikChk.RESG_ImageType = ReflexImgSrc.Image.ImageType.BOSale;
            this.hlQikChk.Size = new System.Drawing.Size(159, 27);
            this.hlQikChk.StyleController = this.layoutControl1;
            this.hlQikChk.TabIndex = 5;
            this.hlQikChk.Click += new System.EventHandler(this.hlQikChk_Click);
            // 
            // hlManChk
            // 
            this.hlManChk.EditValue = "Change to Manual Check";
            this.hlManChk.Location = new System.Drawing.Point(12, 12);
            this.hlManChk.Name = "hlManChk";
            this.hlManChk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlManChk.Properties.Appearance.Options.UseBackColor = true;
            this.hlManChk.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlManChk.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlManChk.Properties.Image")));
            this.hlManChk.RESG_ImageType = ReflexImgSrc.Image.ImageType.Edit;
            this.hlManChk.Size = new System.Drawing.Size(159, 30);
            this.hlManChk.StyleController = this.layoutControl1;
            this.hlManChk.TabIndex = 4;
            this.hlManChk.Click += new System.EventHandler(this.hlManChk_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem28,
            this.emptySpaceItem3,
            this.layoutControlItem18,
            this.layoutControlItem9,
            this.lciOverridePWPStatus});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(346, 220);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.hlManChk;
            this.layoutControlItem1.CustomizationFormText = "Change to Manual Check";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem1.Text = "Change to Manual Check";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.hlQikChk;
            this.layoutControlItem2.CustomizationFormText = "Quick Check";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem2.Text = "Quick Check";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.hlRefresh;
            this.layoutControlItem28.CustomizationFormText = "Refresh";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem28.Text = "Refresh";
            this.layoutControlItem28.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 102);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(326, 98);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.hlAddPayHold;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(163, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem18.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.hlReleasePayHold;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(163, 34);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // lciOverridePWPStatus
            // 
            this.lciOverridePWPStatus.Control = this.hlOverridePWPStatus;
            this.lciOverridePWPStatus.CustomizationFormText = "Override PWP Status";
            this.lciOverridePWPStatus.Location = new System.Drawing.Point(163, 68);
            this.lciOverridePWPStatus.Name = "lciOverridePWPStatus";
            this.lciOverridePWPStatus.Size = new System.Drawing.Size(163, 34);
            this.lciOverridePWPStatus.Text = "Override PWP Status";
            this.lciOverridePWPStatus.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciOverridePWPStatus.TextSize = new System.Drawing.Size(0, 0);
            this.lciOverridePWPStatus.TextVisible = false;
            // 
            // panelContainer3
            // 
            this.panelContainer3.ActiveChild = this.dockPanel8;
            this.panelContainer3.Controls.Add(this.dockPanel8);
            this.panelContainer3.Controls.Add(this.dockPanel7);
            this.panelContainer3.Controls.Add(this.dockPanel9);
            this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer3.ID = new System.Guid("a1553727-a53e-433c-b82f-6f297d851ba9");
            this.panelContainer3.Location = new System.Drawing.Point(0, 441);
            this.panelContainer3.Name = "panelContainer3";
            this.panelContainer3.OriginalSize = new System.Drawing.Size(997, 295);
            this.panelContainer3.Size = new System.Drawing.Size(997, 295);
            this.panelContainer3.Tabbed = true;
            this.panelContainer3.Text = "panelContainer3";
            this.panelContainer3.Resize += new System.EventHandler(this.panelContainer3_Resize);
            // 
            // dockPanel8
            // 
            this.dockPanel8.Controls.Add(this.dockPanel8_Container);
            this.dockPanel8.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel8.ID = new System.Guid("40be137c-942f-447d-b239-fc17c3c7b35d");
            this.dockPanel8.Location = new System.Drawing.Point(4, 24);
            this.dockPanel8.Name = "dockPanel8";
            this.dockPanel8.OriginalSize = new System.Drawing.Size(991, 245);
            this.dockPanel8.Size = new System.Drawing.Size(989, 240);
            this.dockPanel8.Text = "Details";
            // 
            // dockPanel8_Container
            // 
            this.dockPanel8_Container.Controls.Add(this.gcDetail);
            this.dockPanel8_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel8_Container.Name = "dockPanel8_Container";
            this.dockPanel8_Container.Size = new System.Drawing.Size(989, 240);
            this.dockPanel8_Container.TabIndex = 0;
            // 
            // gcDetail
            // 
            this.gcDetail.DataMember = "AP_GL_ALLOC";
            this.gcDetail.DataSource = this.dsDetail1;
            this.gcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDetail.Location = new System.Drawing.Point(0, 0);
            this.gcDetail.MainView = this.gvDetail;
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit5,
            this.repositoryItemLookUpEdit6,
            this.repositoryItemLookUpEdit7,
            this.riPOFSelect,
            this.riPOBSelect,
            this.riPODSelect,
            this.riPOMSelect,
            this.riPOM2Select,
            this.riPOSelect,
            this.riNoPOSelect,
            this.riDetPOSelect,
            this.riPWPStatus});
            this.gcDetail.Size = new System.Drawing.Size(989, 240);
            this.gcDetail.TabIndex = 0;
            this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
            // 
            // dsDetail1
            // 
            this.dsDetail1.DataSetName = "dsDetail";
            this.dsDetail1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsDetail1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDetail
            // 
            this.gvDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGL_ACCOUNT,
            this.colCOMMENT1,
            this.colAMOUNT,
            this.colTRANS_TYPE,
            this.colHOLD_AMT1,
            this.colPO_ID1,
            this.colSUB_CODE,
            this.colGL_ACCOUNT1,
            this.colREFERENCE1,
            this.colPO_ID2,
            this.colAR_PWP_STATUS_ID,
            this.colCode,
            this.colDescription,
            this.colCOST_CODE,
            this.colAFE_NO,
            this.colTIME_TICKET});
            this.gvDetail.GridControl = this.gcDetail;
            this.gvDetail.Name = "gvDetail";
            this.gvDetail.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDetail.OptionsView.ColumnAutoWidth = false;
            // 
            // colGL_ACCOUNT
            // 
            this.colGL_ACCOUNT.Caption = "GL Account";
            this.colGL_ACCOUNT.FieldName = "GL_ACCOUNT";
            this.colGL_ACCOUNT.Name = "colGL_ACCOUNT";
            this.colGL_ACCOUNT.OptionsColumn.AllowEdit = false;
            this.colGL_ACCOUNT.Visible = true;
            this.colGL_ACCOUNT.VisibleIndex = 0;
            this.colGL_ACCOUNT.Width = 183;
            // 
            // colCOMMENT1
            // 
            this.colCOMMENT1.Caption = "Comment";
            this.colCOMMENT1.FieldName = "COMMENT";
            this.colCOMMENT1.Name = "colCOMMENT1";
            this.colCOMMENT1.OptionsColumn.AllowEdit = false;
            this.colCOMMENT1.Visible = true;
            this.colCOMMENT1.VisibleIndex = 6;
            // 
            // colAMOUNT
            // 
            this.colAMOUNT.Caption = "Amount";
            this.colAMOUNT.DisplayFormat.FormatString = "n2";
            this.colAMOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAMOUNT.FieldName = "AMOUNT";
            this.colAMOUNT.Name = "colAMOUNT";
            this.colAMOUNT.OptionsColumn.AllowEdit = false;
            this.colAMOUNT.Visible = true;
            this.colAMOUNT.VisibleIndex = 4;
            // 
            // colTRANS_TYPE
            // 
            this.colTRANS_TYPE.Caption = "Type";
            this.colTRANS_TYPE.ColumnEdit = this.repositoryItemLookUpEdit5;
            this.colTRANS_TYPE.FieldName = "PO_TYPE";
            this.colTRANS_TYPE.Name = "colTRANS_TYPE";
            this.colTRANS_TYPE.OptionsColumn.AllowEdit = false;
            this.colTRANS_TYPE.Visible = true;
            this.colTRANS_TYPE.VisibleIndex = 7;
            // 
            // repositoryItemLookUpEdit5
            // 
            this.repositoryItemLookUpEdit5.AutoHeight = false;
            this.repositoryItemLookUpEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit5.Name = "repositoryItemLookUpEdit5";
            this.repositoryItemLookUpEdit5.NullText = "";
            // 
            // colHOLD_AMT1
            // 
            this.colHOLD_AMT1.Caption = "Holdback $";
            this.colHOLD_AMT1.DisplayFormat.FormatString = "n2";
            this.colHOLD_AMT1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHOLD_AMT1.FieldName = "HOLD_AMT";
            this.colHOLD_AMT1.Name = "colHOLD_AMT1";
            this.colHOLD_AMT1.OptionsColumn.AllowEdit = false;
            this.colHOLD_AMT1.Visible = true;
            this.colHOLD_AMT1.VisibleIndex = 5;
            // 
            // colPO_ID1
            // 
            this.colPO_ID1.Caption = "PO #";
            this.colPO_ID1.ColumnEdit = this.riPOSelect;
            this.colPO_ID1.FieldName = "PO_REC_ID";
            this.colPO_ID1.Name = "colPO_ID1";
            this.colPO_ID1.OptionsColumn.AllowEdit = false;
            this.colPO_ID1.Visible = true;
            this.colPO_ID1.VisibleIndex = 8;
            // 
            // riPOSelect
            // 
            this.riPOSelect.AutoHeight = false;
            this.riPOSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOSelect.DataSource = this.dsPOSelect1.PO_REC_HEADER;
            this.riPOSelect.DisplayMember = "PO #";
            this.riPOSelect.Name = "riPOSelect";
            this.riPOSelect.NullText = "";
            this.riPOSelect.ValueMember = "PO_REC_ID";
            // 
            // dsPOSelect1
            // 
            this.dsPOSelect1.DataSetName = "dsPOSelect";
            this.dsPOSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colSUB_CODE
            // 
            this.colSUB_CODE.Caption = "Sub Code";
            this.colSUB_CODE.FieldName = "SUB_CODE";
            this.colSUB_CODE.Name = "colSUB_CODE";
            this.colSUB_CODE.OptionsColumn.AllowEdit = false;
            this.colSUB_CODE.Visible = true;
            this.colSUB_CODE.VisibleIndex = 3;
            this.colSUB_CODE.Width = 89;
            // 
            // colGL_ACCOUNT1
            // 
            this.colGL_ACCOUNT1.Caption = "Descrption";
            this.colGL_ACCOUNT1.ColumnEdit = this.repositoryItemLookUpEdit6;
            this.colGL_ACCOUNT1.FieldName = "GL_ACCOUNT";
            this.colGL_ACCOUNT1.Name = "colGL_ACCOUNT1";
            this.colGL_ACCOUNT1.OptionsColumn.AllowEdit = false;
            this.colGL_ACCOUNT1.Visible = true;
            this.colGL_ACCOUNT1.VisibleIndex = 1;
            this.colGL_ACCOUNT1.Width = 205;
            // 
            // repositoryItemLookUpEdit6
            // 
            this.repositoryItemLookUpEdit6.AutoHeight = false;
            this.repositoryItemLookUpEdit6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit6.DataSource = this.dsGLAccts1.GL_ACCOUNTS;
            this.repositoryItemLookUpEdit6.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit6.Name = "repositoryItemLookUpEdit6";
            this.repositoryItemLookUpEdit6.NullText = "";
            this.repositoryItemLookUpEdit6.ValueMember = "ACCOUNT_NUMBER";
            // 
            // dsGLAccts1
            // 
            this.dsGLAccts1.DataSetName = "dsGLAccts";
            this.dsGLAccts1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsGLAccts1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colREFERENCE1
            // 
            this.colREFERENCE1.Caption = "Reference";
            this.colREFERENCE1.FieldName = "REFERENCE";
            this.colREFERENCE1.Name = "colREFERENCE1";
            this.colREFERENCE1.OptionsColumn.AllowEdit = false;
            this.colREFERENCE1.Visible = true;
            this.colREFERENCE1.VisibleIndex = 2;
            this.colREFERENCE1.Width = 166;
            // 
            // colPO_ID2
            // 
            this.colPO_ID2.Caption = "PO Matching";
            this.colPO_ID2.ColumnEdit = this.riDetPOSelect;
            this.colPO_ID2.FieldName = "PO_ID";
            this.colPO_ID2.Name = "colPO_ID2";
            this.colPO_ID2.OptionsColumn.AllowEdit = false;
            this.colPO_ID2.Visible = true;
            this.colPO_ID2.VisibleIndex = 9;
            this.colPO_ID2.Width = 84;
            // 
            // riDetPOSelect
            // 
            this.riDetPOSelect.AutoHeight = false;
            this.riDetPOSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDetPOSelect.DataSource = this.dsDetPO1.PO_HEADER;
            this.riDetPOSelect.DisplayMember = "PO";
            this.riDetPOSelect.Name = "riDetPOSelect";
            this.riDetPOSelect.NullText = "";
            this.riDetPOSelect.ValueMember = "PO_ID";
            // 
            // dsDetPO1
            // 
            this.dsDetPO1.DataSetName = "dsDetPO";
            this.dsDetPO1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsDetPO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colAR_PWP_STATUS_ID
            // 
            this.colAR_PWP_STATUS_ID.Caption = "PWP Status";
            this.colAR_PWP_STATUS_ID.ColumnEdit = this.riPWPStatus;
            this.colAR_PWP_STATUS_ID.FieldName = "AR_PWP_STATUS_ID";
            this.colAR_PWP_STATUS_ID.Name = "colAR_PWP_STATUS_ID";
            this.colAR_PWP_STATUS_ID.OptionsColumn.AllowEdit = false;
            this.colAR_PWP_STATUS_ID.Visible = true;
            this.colAR_PWP_STATUS_ID.VisibleIndex = 10;
            this.colAR_PWP_STATUS_ID.Width = 85;
            // 
            // riPWPStatus
            // 
            this.riPWPStatus.AutoHeight = false;
            this.riPWPStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPWPStatus.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID", 126, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Status", 78, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riPWPStatus.DataSource = this.aRPWPSTATUSBindingSource;
            this.riPWPStatus.DisplayMember = "DESCRIPTION";
            this.riPWPStatus.Name = "riPWPStatus";
            this.riPWPStatus.NullText = "";
            this.riPWPStatus.PopupWidth = 300;
            this.riPWPStatus.ReadOnly = true;
            this.riPWPStatus.ValueMember = "AR_PWP_STATUS_ID";
            // 
            // aRPWPSTATUSBindingSource
            // 
            this.aRPWPSTATUSBindingSource.DataMember = "AR_PWP_STATUS";
            this.aRPWPSTATUSBindingSource.DataSource = this.dsPWP_Status1;
            // 
            // dsPWP_Status1
            // 
            this.dsPWP_Status1.DataSetName = "dsPWP_Status";
            this.dsPWP_Status1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colCode
            // 
            this.colCode.Caption = "PWP Rejection Reason Code";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Width = 159;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "PWP Rejection Reason";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.Width = 131;
            // 
            // colCOST_CODE
            // 
            this.colCOST_CODE.Caption = "Cust Cost Code";
            this.colCOST_CODE.FieldName = "COST_CODE";
            this.colCOST_CODE.Name = "colCOST_CODE";
            this.colCOST_CODE.OptionsColumn.AllowEdit = false;
            // 
            // colAFE_NO
            // 
            this.colAFE_NO.Caption = "AFE";
            this.colAFE_NO.FieldName = "AFE_NO";
            this.colAFE_NO.Name = "colAFE_NO";
            this.colAFE_NO.OptionsColumn.AllowEdit = false;
            // 
            // colTIME_TICKET
            // 
            this.colTIME_TICKET.Caption = "Time Ticket";
            this.colTIME_TICKET.FieldName = "TIME_TICKET";
            this.colTIME_TICKET.Name = "colTIME_TICKET";
            this.colTIME_TICKET.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemLookUpEdit7
            // 
            this.repositoryItemLookUpEdit7.AutoHeight = false;
            this.repositoryItemLookUpEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit7.DisplayMember = "GL_ALLOC_CODE";
            this.repositoryItemLookUpEdit7.Name = "repositoryItemLookUpEdit7";
            this.repositoryItemLookUpEdit7.NullText = "";
            this.repositoryItemLookUpEdit7.ValueMember = "GL_ALLOC_ID";
            // 
            // riPOFSelect
            // 
            this.riPOFSelect.AutoHeight = false;
            this.riPOFSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOFSelect.DataSource = this.dsPOFSelect1.PO_REC_HEADER;
            this.riPOFSelect.DisplayMember = "PO #";
            this.riPOFSelect.Name = "riPOFSelect";
            this.riPOFSelect.NullText = "";
            this.riPOFSelect.ValueMember = "PO_REC_ID";
            // 
            // dsPOFSelect1
            // 
            this.dsPOFSelect1.DataSetName = "dsPOFSelect";
            this.dsPOFSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOFSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOBSelect
            // 
            this.riPOBSelect.AutoHeight = false;
            this.riPOBSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOBSelect.DataSource = this.dsPOBSelect1.PO_REC_HEADER;
            this.riPOBSelect.DisplayMember = "PO #";
            this.riPOBSelect.Name = "riPOBSelect";
            this.riPOBSelect.NullText = "";
            this.riPOBSelect.ValueMember = "PO_REC_ID";
            // 
            // dsPOBSelect1
            // 
            this.dsPOBSelect1.DataSetName = "dsPOBSelect";
            this.dsPOBSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOBSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPODSelect
            // 
            this.riPODSelect.AutoHeight = false;
            this.riPODSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPODSelect.DataSource = this.dsPODSelect1.PO_REC_HEADER;
            this.riPODSelect.DisplayMember = "PO #";
            this.riPODSelect.Name = "riPODSelect";
            this.riPODSelect.NullText = "";
            this.riPODSelect.ValueMember = "PO_REC_ID";
            // 
            // dsPODSelect1
            // 
            this.dsPODSelect1.DataSetName = "dsPODSelect";
            this.dsPODSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPODSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOMSelect
            // 
            this.riPOMSelect.AutoHeight = false;
            this.riPOMSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOMSelect.DataSource = this.dsPOMSelect1.PO_REC_HEADER;
            this.riPOMSelect.DisplayMember = "PO #";
            this.riPOMSelect.Name = "riPOMSelect";
            this.riPOMSelect.NullText = "";
            this.riPOMSelect.ValueMember = "PO_REC_ID";
            // 
            // dsPOMSelect1
            // 
            this.dsPOMSelect1.DataSetName = "dsPOMSelect";
            this.dsPOMSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOMSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOM2Select
            // 
            this.riPOM2Select.AutoHeight = false;
            this.riPOM2Select.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOM2Select.DataSource = this.dsPOM2Select1.PO_REC_HEADER;
            this.riPOM2Select.DisplayMember = "PO #";
            this.riPOM2Select.Name = "riPOM2Select";
            this.riPOM2Select.NullText = "";
            this.riPOM2Select.ValueMember = "PO_REC_ID";
            // 
            // dsPOM2Select1
            // 
            this.dsPOM2Select1.DataSetName = "dsPOM2Select";
            this.dsPOM2Select1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOM2Select1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riNoPOSelect
            // 
            this.riNoPOSelect.AutoHeight = false;
            this.riNoPOSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riNoPOSelect.Name = "riNoPOSelect";
            this.riNoPOSelect.NullText = "";
            // 
            // dockPanel7
            // 
            this.dockPanel7.Controls.Add(this.dockPanel7_Container);
            this.dockPanel7.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel7.FloatVertical = true;
            this.dockPanel7.ID = new System.Guid("c2e69ebc-6da8-4e0b-8f52-81deb2c1a85c");
            this.dockPanel7.Location = new System.Drawing.Point(4, 24);
            this.dockPanel7.Name = "dockPanel7";
            this.dockPanel7.OriginalSize = new System.Drawing.Size(991, 245);
            this.dockPanel7.Size = new System.Drawing.Size(989, 240);
            this.dockPanel7.Text = "Match PO Receipt";
            // 
            // dockPanel7_Container
            // 
            this.dockPanel7_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel7_Container.Name = "dockPanel7_Container";
            this.dockPanel7_Container.Size = new System.Drawing.Size(989, 240);
            this.dockPanel7_Container.TabIndex = 0;
            // 
            // dockPanel9
            // 
            this.dockPanel9.Controls.Add(this.dockPanel9_Container);
            this.dockPanel9.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel9.ID = new System.Guid("a65504a4-17ee-49f2-8f73-b62382bbafb4");
            this.dockPanel9.Location = new System.Drawing.Point(4, 24);
            this.dockPanel9.Name = "dockPanel9";
            this.dockPanel9.OriginalSize = new System.Drawing.Size(991, 245);
            this.dockPanel9.Size = new System.Drawing.Size(989, 240);
            this.dockPanel9.Text = "Unapproved Contract PO";
            // 
            // dockPanel9_Container
            // 
            this.dockPanel9_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel9_Container.Name = "dockPanel9_Container";
            this.dockPanel9_Container.Size = new System.Drawing.Size(989, 240);
            this.dockPanel9_Container.TabIndex = 0;
            // 
            // daHeader
            // 
            this.daHeader.DeleteCommand = this.sqlDeleteCommand1;
            this.daHeader.InsertCommand = this.sqlInsertCommand1;
            this.daHeader.SelectCommand = this.sqlSelectCommand1;
            this.daHeader.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_INV_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("INV_NO", "INV_NO"),
                        new System.Data.Common.DataColumnMapping("TRANS_TYPE", "TRANS_TYPE"),
                        new System.Data.Common.DataColumnMapping("ACCT_PERIOD", "ACCT_PERIOD"),
                        new System.Data.Common.DataColumnMapping("ACCT_YEAR", "ACCT_YEAR"),
                        new System.Data.Common.DataColumnMapping("JOURNAL_NUMBER", "JOURNAL_NUMBER"),
                        new System.Data.Common.DataColumnMapping("JOURNAL_LINE_NO", "JOURNAL_LINE_NO"),
                        new System.Data.Common.DataColumnMapping("TRANS_DATE", "TRANS_DATE"),
                        new System.Data.Common.DataColumnMapping("INV_DATE", "INV_DATE"),
                        new System.Data.Common.DataColumnMapping("DUE_DATE", "DUE_DATE"),
                        new System.Data.Common.DataColumnMapping("DISCOUNT_DATE", "DISCOUNT_DATE"),
                        new System.Data.Common.DataColumnMapping("INV_AMOUNT", "INV_AMOUNT"),
                        new System.Data.Common.DataColumnMapping("DISCOUNT_AMOUNT", "DISCOUNT_AMOUNT"),
                        new System.Data.Common.DataColumnMapping("BALANCE", "BALANCE"),
                        new System.Data.Common.DataColumnMapping("REFERENCE", "REFERENCE"),
                        new System.Data.Common.DataColumnMapping("CANCEL", "CANCEL"),
                        new System.Data.Common.DataColumnMapping("DISCOUNT_TAKEN", "DISCOUNT_TAKEN"),
                        new System.Data.Common.DataColumnMapping("CK_SELECT", "CK_SELECT"),
                        new System.Data.Common.DataColumnMapping("OPERATOR", "OPERATOR"),
                        new System.Data.Common.DataColumnMapping("DATE_SAVED", "DATE_SAVED"),
                        new System.Data.Common.DataColumnMapping("HOLD", "HOLD"),
                        new System.Data.Common.DataColumnMapping("SUPP_NAME", "SUPP_NAME"),
                        new System.Data.Common.DataColumnMapping("REMITADD1", "REMITADD1"),
                        new System.Data.Common.DataColumnMapping("REMITADD2", "REMITADD2"),
                        new System.Data.Common.DataColumnMapping("REMITADD3", "REMITADD3"),
                        new System.Data.Common.DataColumnMapping("REMITCITY", "REMITCITY"),
                        new System.Data.Common.DataColumnMapping("REMITSTATE", "REMITSTATE"),
                        new System.Data.Common.DataColumnMapping("REMITZIP", "REMITZIP"),
                        new System.Data.Common.DataColumnMapping("SUPP_ACCOUNT", "SUPP_ACCOUNT"),
                        new System.Data.Common.DataColumnMapping("AP_SETUP_GL_ID", "AP_SETUP_GL_ID"),
                        new System.Data.Common.DataColumnMapping("GST_CODE", "GST_CODE"),
                        new System.Data.Common.DataColumnMapping("PURCH_AMT", "PURCH_AMT"),
                        new System.Data.Common.DataColumnMapping("GST_AMT", "GST_AMT"),
                        new System.Data.Common.DataColumnMapping("HOLD_PCT", "HOLD_PCT"),
                        new System.Data.Common.DataColumnMapping("HOLD_AMT", "HOLD_AMT"),
                        new System.Data.Common.DataColumnMapping("HOLD_BAL", "HOLD_BAL"),
                        new System.Data.Common.DataColumnMapping("HOLD_PAY_DATE", "HOLD_PAY_DATE"),
                        new System.Data.Common.DataColumnMapping("SEG_CHANGE", "SEG_CHANGE"),
                        new System.Data.Common.DataColumnMapping("CURRENCY_ID", "CURRENCY_ID"),
                        new System.Data.Common.DataColumnMapping("REMITCOUNTRY", "REMITCOUNTRY"),
                        new System.Data.Common.DataColumnMapping("GST_EXCEPT", "GST_EXCEPT"),
                        new System.Data.Common.DataColumnMapping("ACCRUAL_FLAG", "ACCRUAL_FLAG"),
                        new System.Data.Common.DataColumnMapping("LOCKED_BY", "LOCKED_BY"),
                        new System.Data.Common.DataColumnMapping("LOCKED", "LOCKED"),
                        new System.Data.Common.DataColumnMapping("AP_INV_HEADER_ID", "AP_INV_HEADER_ID"),
                        new System.Data.Common.DataColumnMapping("INVOICE_TYPE", "INVOICE_TYPE"),
                        new System.Data.Common.DataColumnMapping("MANUAL_CHECK", "MANUAL_CHECK"),
                        new System.Data.Common.DataColumnMapping("SALES_TAX_ID", "SALES_TAX_ID"),
                        new System.Data.Common.DataColumnMapping("NEW_INVOICE", "NEW_INVOICE"),
                        new System.Data.Common.DataColumnMapping("COMMENT", "COMMENT"),
                        new System.Data.Common.DataColumnMapping("EXCH_RATE", "EXCH_RATE"),
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("GST_EXCEPT_ID", "GST_EXCEPT_ID"),
                        new System.Data.Common.DataColumnMapping("whse_id", "whse_id"),
                        new System.Data.Common.DataColumnMapping("SOX_ROUTING", "SOX_ROUTING"),
                        new System.Data.Common.DataColumnMapping("SOX_APPROVAL", "SOX_APPROVAL"),
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("AP_DIV", "AP_DIV"),
                        new System.Data.Common.DataColumnMapping("KC_PAYHOLD_STATUS", "KC_PAYHOLD_STATUS"),
                        new System.Data.Common.DataColumnMapping("TERMS_ID", "TERMS_ID"),
                        new System.Data.Common.DataColumnMapping("PAYMENT_HOLD", "PAYMENT_HOLD")})});
            this.daHeader.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_INV_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_PERIOD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_YEAR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_NUMBER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DUE_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DUE_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DUE_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DUE_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BALANCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BALANCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BALANCE", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BALANCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REFERENCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REFERENCE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CANCEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CANCEL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CANCEL", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CANCEL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_TAKEN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_TAKEN", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_TAKEN", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_TAKEN", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CK_SELECT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CK_SELECT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CK_SELECT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CK_SELECT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_OPERATOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OPERATOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_OPERATOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "OPERATOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DATE_SAVED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DATE_SAVED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DATE_SAVED", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DATE_SAVED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD3", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITCITY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITCITY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITCITY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITCITY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITSTATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITSTATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITSTATE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITSTATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITZIP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITZIP", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITZIP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITZIP", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_ACCOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_ACCOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_ACCOUNT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_ACCOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PCT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PAY_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_CHANGE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITCOUNTRY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITCOUNTRY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITCOUNTRY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITCOUNTRY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_EXCEPT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_EXCEPT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_EXCEPT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_EXCEPT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCRUAL_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCRUAL_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCRUAL_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCRUAL_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LOCKED_BY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LOCKED_BY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LOCKED_BY", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LOCKED_BY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LOCKED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LOCKED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LOCKED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LOCKED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INVOICE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INVOICE_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MANUAL_CHECK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MANUAL_CHECK", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MANUAL_CHECK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MANUAL_CHECK", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SALES_TAX_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NEW_INVOICE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NEW_INVOICE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NEW_INVOICE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NEW_INVOICE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_RATE", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_EXCEPT_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_EXCEPT_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_whse_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "whse_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_whse_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "whse_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SOX_ROUTING", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SOX_ROUTING", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SOX_ROUTING", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SOX_ROUTING", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SOX_APPROVAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SOX_APPROVAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SOX_APPROVAL", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SOX_APPROVAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_DIV", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_DIV", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_PAYHOLD_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_PAYHOLD_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_PAYHOLD_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_PAYHOLD_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null)});
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
    "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.TR_Conn;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@INV_NO", System.Data.SqlDbType.VarChar, 0, "INV_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, "TRANS_TYPE"),
            new System.Data.SqlClient.SqlParameter("@ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, "ACCT_PERIOD"),
            new System.Data.SqlClient.SqlParameter("@ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, "ACCT_YEAR"),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, "JOURNAL_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_DATE", System.Data.SqlDbType.DateTime, 0, "TRANS_DATE"),
            new System.Data.SqlClient.SqlParameter("@INV_DATE", System.Data.SqlDbType.DateTime, 0, "INV_DATE"),
            new System.Data.SqlClient.SqlParameter("@DUE_DATE", System.Data.SqlDbType.DateTime, 0, "DUE_DATE"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, "DISCOUNT_DATE"),
            new System.Data.SqlClient.SqlParameter("@INV_AMOUNT", System.Data.SqlDbType.Money, 0, "INV_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, "DISCOUNT_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@BALANCE", System.Data.SqlDbType.Money, 0, "BALANCE"),
            new System.Data.SqlClient.SqlParameter("@REFERENCE", System.Data.SqlDbType.VarChar, 0, "REFERENCE"),
            new System.Data.SqlClient.SqlParameter("@CANCEL", System.Data.SqlDbType.Char, 0, "CANCEL"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_TAKEN", System.Data.SqlDbType.Money, 0, "DISCOUNT_TAKEN"),
            new System.Data.SqlClient.SqlParameter("@CK_SELECT", System.Data.SqlDbType.Char, 0, "CK_SELECT"),
            new System.Data.SqlClient.SqlParameter("@OPERATOR", System.Data.SqlDbType.VarChar, 0, "OPERATOR"),
            new System.Data.SqlClient.SqlParameter("@DATE_SAVED", System.Data.SqlDbType.DateTime, 0, "DATE_SAVED"),
            new System.Data.SqlClient.SqlParameter("@HOLD", System.Data.SqlDbType.VarChar, 0, "HOLD"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@REMITADD1", System.Data.SqlDbType.VarChar, 0, "REMITADD1"),
            new System.Data.SqlClient.SqlParameter("@REMITADD2", System.Data.SqlDbType.VarChar, 0, "REMITADD2"),
            new System.Data.SqlClient.SqlParameter("@REMITADD3", System.Data.SqlDbType.VarChar, 0, "REMITADD3"),
            new System.Data.SqlClient.SqlParameter("@REMITCITY", System.Data.SqlDbType.VarChar, 0, "REMITCITY"),
            new System.Data.SqlClient.SqlParameter("@REMITSTATE", System.Data.SqlDbType.VarChar, 0, "REMITSTATE"),
            new System.Data.SqlClient.SqlParameter("@REMITZIP", System.Data.SqlDbType.VarChar, 0, "REMITZIP"),
            new System.Data.SqlClient.SqlParameter("@SUPP_ACCOUNT", System.Data.SqlDbType.VarChar, 0, "SUPP_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.Char, 0, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@PURCH_AMT", System.Data.SqlDbType.Money, 0, "PURCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@GST_AMT", System.Data.SqlDbType.Money, 0, "GST_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PCT", System.Data.SqlDbType.Money, 0, "HOLD_PCT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_AMT", System.Data.SqlDbType.Money, 0, "HOLD_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_BAL", System.Data.SqlDbType.Money, 0, "HOLD_BAL"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, "HOLD_PAY_DATE"),
            new System.Data.SqlClient.SqlParameter("@SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, "SEG_CHANGE"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 0, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@REMITCOUNTRY", System.Data.SqlDbType.Int, 0, "REMITCOUNTRY"),
            new System.Data.SqlClient.SqlParameter("@GST_EXCEPT", System.Data.SqlDbType.Int, 0, "GST_EXCEPT"),
            new System.Data.SqlClient.SqlParameter("@ACCRUAL_FLAG", System.Data.SqlDbType.Char, 0, "ACCRUAL_FLAG"),
            new System.Data.SqlClient.SqlParameter("@LOCKED_BY", System.Data.SqlDbType.Char, 0, "LOCKED_BY"),
            new System.Data.SqlClient.SqlParameter("@LOCKED", System.Data.SqlDbType.Char, 0, "LOCKED"),
            new System.Data.SqlClient.SqlParameter("@AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, "AP_INV_HEADER_ID"),
            new System.Data.SqlClient.SqlParameter("@INVOICE_TYPE", System.Data.SqlDbType.Char, 0, "INVOICE_TYPE"),
            new System.Data.SqlClient.SqlParameter("@MANUAL_CHECK", System.Data.SqlDbType.Int, 0, "MANUAL_CHECK"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 0, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@NEW_INVOICE", System.Data.SqlDbType.Char, 0, "NEW_INVOICE"),
            new System.Data.SqlClient.SqlParameter("@COMMENT", System.Data.SqlDbType.Text, 0, "COMMENT"),
            new System.Data.SqlClient.SqlParameter("@EXCH_RATE", System.Data.SqlDbType.Float, 0, "EXCH_RATE"),
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, "GST_EXCEPT_ID"),
            new System.Data.SqlClient.SqlParameter("@whse_id", System.Data.SqlDbType.Int, 0, "whse_id"),
            new System.Data.SqlClient.SqlParameter("@SOX_ROUTING", System.Data.SqlDbType.Bit, 0, "SOX_ROUTING"),
            new System.Data.SqlClient.SqlParameter("@SOX_APPROVAL", System.Data.SqlDbType.Bit, 0, "SOX_APPROVAL"),
            new System.Data.SqlClient.SqlParameter("@AP_DIV", System.Data.SqlDbType.VarChar, 0, "AP_DIV"),
            new System.Data.SqlClient.SqlParameter("@KC_PAYHOLD_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_PAYHOLD_STATUS"),
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, "PAYMENT_HOLD")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@INV_NO", System.Data.SqlDbType.VarChar, 0, "INV_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, "TRANS_TYPE"),
            new System.Data.SqlClient.SqlParameter("@ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, "ACCT_PERIOD"),
            new System.Data.SqlClient.SqlParameter("@ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, "ACCT_YEAR"),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, "JOURNAL_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_DATE", System.Data.SqlDbType.DateTime, 0, "TRANS_DATE"),
            new System.Data.SqlClient.SqlParameter("@INV_DATE", System.Data.SqlDbType.DateTime, 0, "INV_DATE"),
            new System.Data.SqlClient.SqlParameter("@DUE_DATE", System.Data.SqlDbType.DateTime, 0, "DUE_DATE"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, "DISCOUNT_DATE"),
            new System.Data.SqlClient.SqlParameter("@INV_AMOUNT", System.Data.SqlDbType.Money, 0, "INV_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, "DISCOUNT_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@BALANCE", System.Data.SqlDbType.Money, 0, "BALANCE"),
            new System.Data.SqlClient.SqlParameter("@REFERENCE", System.Data.SqlDbType.VarChar, 0, "REFERENCE"),
            new System.Data.SqlClient.SqlParameter("@CANCEL", System.Data.SqlDbType.Char, 0, "CANCEL"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_TAKEN", System.Data.SqlDbType.Money, 0, "DISCOUNT_TAKEN"),
            new System.Data.SqlClient.SqlParameter("@CK_SELECT", System.Data.SqlDbType.Char, 0, "CK_SELECT"),
            new System.Data.SqlClient.SqlParameter("@OPERATOR", System.Data.SqlDbType.VarChar, 0, "OPERATOR"),
            new System.Data.SqlClient.SqlParameter("@DATE_SAVED", System.Data.SqlDbType.DateTime, 0, "DATE_SAVED"),
            new System.Data.SqlClient.SqlParameter("@HOLD", System.Data.SqlDbType.VarChar, 0, "HOLD"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@REMITADD1", System.Data.SqlDbType.VarChar, 0, "REMITADD1"),
            new System.Data.SqlClient.SqlParameter("@REMITADD2", System.Data.SqlDbType.VarChar, 0, "REMITADD2"),
            new System.Data.SqlClient.SqlParameter("@REMITADD3", System.Data.SqlDbType.VarChar, 0, "REMITADD3"),
            new System.Data.SqlClient.SqlParameter("@REMITCITY", System.Data.SqlDbType.VarChar, 0, "REMITCITY"),
            new System.Data.SqlClient.SqlParameter("@REMITSTATE", System.Data.SqlDbType.VarChar, 0, "REMITSTATE"),
            new System.Data.SqlClient.SqlParameter("@REMITZIP", System.Data.SqlDbType.VarChar, 0, "REMITZIP"),
            new System.Data.SqlClient.SqlParameter("@SUPP_ACCOUNT", System.Data.SqlDbType.VarChar, 0, "SUPP_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.Char, 0, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@PURCH_AMT", System.Data.SqlDbType.Money, 0, "PURCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@GST_AMT", System.Data.SqlDbType.Money, 0, "GST_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PCT", System.Data.SqlDbType.Money, 0, "HOLD_PCT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_AMT", System.Data.SqlDbType.Money, 0, "HOLD_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_BAL", System.Data.SqlDbType.Money, 0, "HOLD_BAL"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, "HOLD_PAY_DATE"),
            new System.Data.SqlClient.SqlParameter("@SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, "SEG_CHANGE"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 0, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@REMITCOUNTRY", System.Data.SqlDbType.Int, 0, "REMITCOUNTRY"),
            new System.Data.SqlClient.SqlParameter("@GST_EXCEPT", System.Data.SqlDbType.Int, 0, "GST_EXCEPT"),
            new System.Data.SqlClient.SqlParameter("@ACCRUAL_FLAG", System.Data.SqlDbType.Char, 0, "ACCRUAL_FLAG"),
            new System.Data.SqlClient.SqlParameter("@LOCKED_BY", System.Data.SqlDbType.Char, 0, "LOCKED_BY"),
            new System.Data.SqlClient.SqlParameter("@LOCKED", System.Data.SqlDbType.Char, 0, "LOCKED"),
            new System.Data.SqlClient.SqlParameter("@AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, "AP_INV_HEADER_ID"),
            new System.Data.SqlClient.SqlParameter("@INVOICE_TYPE", System.Data.SqlDbType.Char, 0, "INVOICE_TYPE"),
            new System.Data.SqlClient.SqlParameter("@MANUAL_CHECK", System.Data.SqlDbType.Int, 0, "MANUAL_CHECK"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 0, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@NEW_INVOICE", System.Data.SqlDbType.Char, 0, "NEW_INVOICE"),
            new System.Data.SqlClient.SqlParameter("@COMMENT", System.Data.SqlDbType.Text, 0, "COMMENT"),
            new System.Data.SqlClient.SqlParameter("@EXCH_RATE", System.Data.SqlDbType.Float, 0, "EXCH_RATE"),
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, "GST_EXCEPT_ID"),
            new System.Data.SqlClient.SqlParameter("@whse_id", System.Data.SqlDbType.Int, 0, "whse_id"),
            new System.Data.SqlClient.SqlParameter("@SOX_ROUTING", System.Data.SqlDbType.Bit, 0, "SOX_ROUTING"),
            new System.Data.SqlClient.SqlParameter("@SOX_APPROVAL", System.Data.SqlDbType.Bit, 0, "SOX_APPROVAL"),
            new System.Data.SqlClient.SqlParameter("@AP_DIV", System.Data.SqlDbType.VarChar, 0, "AP_DIV"),
            new System.Data.SqlClient.SqlParameter("@KC_PAYHOLD_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_PAYHOLD_STATUS"),
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, "PAYMENT_HOLD"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_INV_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_PERIOD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_YEAR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_NUMBER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DUE_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DUE_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DUE_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DUE_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_BALANCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "BALANCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_BALANCE", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "BALANCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REFERENCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REFERENCE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CANCEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CANCEL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CANCEL", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CANCEL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_TAKEN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_TAKEN", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_TAKEN", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_TAKEN", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CK_SELECT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CK_SELECT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CK_SELECT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CK_SELECT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_OPERATOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OPERATOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_OPERATOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "OPERATOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DATE_SAVED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DATE_SAVED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DATE_SAVED", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DATE_SAVED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_NAME", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITADD3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITADD3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITADD3", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITADD3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITCITY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITCITY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITCITY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITCITY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITSTATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITSTATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITSTATE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITSTATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITZIP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITZIP", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITZIP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITZIP", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUPP_ACCOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUPP_ACCOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUPP_ACCOUNT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPP_ACCOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PCT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PAY_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_CHANGE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REMITCOUNTRY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REMITCOUNTRY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REMITCOUNTRY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REMITCOUNTRY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_EXCEPT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_EXCEPT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_EXCEPT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_EXCEPT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCRUAL_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCRUAL_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCRUAL_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCRUAL_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LOCKED_BY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LOCKED_BY", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LOCKED_BY", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LOCKED_BY", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_LOCKED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "LOCKED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_LOCKED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "LOCKED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INVOICE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INVOICE_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_MANUAL_CHECK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "MANUAL_CHECK", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_MANUAL_CHECK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "MANUAL_CHECK", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SALES_TAX_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_NEW_INVOICE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "NEW_INVOICE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_NEW_INVOICE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NEW_INVOICE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_RATE", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_EXCEPT_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_EXCEPT_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_EXCEPT_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_whse_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "whse_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_whse_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "whse_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SOX_ROUTING", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SOX_ROUTING", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SOX_ROUTING", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SOX_ROUTING", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SOX_APPROVAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SOX_APPROVAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SOX_APPROVAL", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SOX_APPROVAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_DIV", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_DIV", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_PAYHOLD_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_PAYHOLD_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_PAYHOLD_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_PAYHOLD_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // daCurrency
            // 
            this.daCurrency.DeleteCommand = this.sqlDeleteCommand2;
            this.daCurrency.InsertCommand = this.sqlInsertCommand2;
            this.daCurrency.SelectCommand = this.sqlSelectCommand2;
            this.daCurrency.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "CURRENCY", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("CURRENCY_ID", "CURRENCY_ID"),
                        new System.Data.Common.DataColumnMapping("CURRENCY_CODE", "CURRENCY_CODE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daCurrency.UpdateCommand = this.sqlUpdateCommand2;
            // 
            // sqlDeleteCommand2
            // 
            this.sqlDeleteCommand2.CommandText = resources.GetString("sqlDeleteCommand2.CommandText");
            this.sqlDeleteCommand2.Connection = this.TR_Conn;
            this.sqlDeleteCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_CODE", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 4, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_CODE", System.Data.SqlDbType.VarChar, 10, "CURRENCY_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 40, "DESCRIPTION")});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT CURRENCY_ID, CURRENCY_CODE, DESCRIPTION FROM CURRENCY ORDER BY DESCRIPTION" +
    "";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand2
            // 
            this.sqlUpdateCommand2.CommandText = resources.GetString("sqlUpdateCommand2.CommandText");
            this.sqlUpdateCommand2.Connection = this.TR_Conn;
            this.sqlUpdateCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 4, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_CODE", System.Data.SqlDbType.VarChar, 10, "CURRENCY_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 40, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_CODE", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daAPSetupGL
            // 
            this.daAPSetupGL.DeleteCommand = this.sqlDeleteCommand3;
            this.daAPSetupGL.InsertCommand = this.sqlInsertCommand3;
            this.daAPSetupGL.SelectCommand = this.sqlSelectCommand3;
            this.daAPSetupGL.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SETUP_GL", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("AP_SETUP_GL_ID", "AP_SETUP_GL_ID"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"),
                        new System.Data.Common.DataColumnMapping("GL_ACCOUNT", "GL_ACCOUNT")})});
            this.daAPSetupGL.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = resources.GetString("sqlDeleteCommand3.CommandText");
            this.sqlDeleteCommand3.Connection = this.TR_Conn;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = resources.GetString("sqlInsertCommand3.CommandText");
            this.sqlInsertCommand3.Connection = this.TR_Conn;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 20, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, "GL_ACCOUNT")});
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "SELECT AP_SETUP_GL_ID, DESCRIPTION, GL_ACCOUNT FROM AP_SETUP_GL ORDER BY DESCRIPT" +
    "ION";
            this.sqlSelectCommand3.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.TR_Conn;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 20, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, "GL_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null)});
            // 
            // daAllPO
            // 
            this.daAllPO.DeleteCommand = this.sqlDeleteCommand4;
            this.daAllPO.InsertCommand = this.sqlInsertCommand4;
            this.daAllPO.SelectCommand = this.sqlSelectCommand4;
            this.daAllPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            this.daAllPO.UpdateCommand = this.sqlUpdateCommand4;
            // 
            // sqlDeleteCommand4
            // 
            this.sqlDeleteCommand4.CommandText = "DELETE FROM PO_HEADER WHERE (PO = @Original_PO) AND (ORDER_DATE = @Original_ORDER" +
    "_DATE OR @Original_ORDER_DATE IS NULL AND ORDER_DATE IS NULL) AND (PO_ID = @Orig" +
    "inal_PO_ID)";
            this.sqlDeleteCommand4.Connection = this.TR_Conn;
            this.sqlDeleteCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand4
            // 
            this.sqlInsertCommand4.CommandText = "INSERT INTO PO_HEADER(PO_ID, PO, ORDER_DATE) VALUES (@PO_ID, @PO, @ORDER_DATE); S" +
    "ELECT PO_ID, PO, ORDER_DATE FROM PO_HEADER WHERE (PO = @PO) ORDER BY PO";
            this.sqlInsertCommand4.Connection = this.TR_Conn;
            this.sqlInsertCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 4, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 20, "PO"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 8, "ORDER_DATE")});
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "SELECT PO_ID, PO, ORDER_DATE FROM PO_HEADER ORDER BY PO";
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand4
            // 
            this.sqlUpdateCommand4.CommandText = resources.GetString("sqlUpdateCommand4.CommandText");
            this.sqlUpdateCommand4.Connection = this.TR_Conn;
            this.sqlUpdateCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 4, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 20, "PO"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 8, "ORDER_DATE"),
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daGST
            // 
            this.daGST.DeleteCommand = this.sqlDeleteCommand5;
            this.daGST.InsertCommand = this.sqlInsertCommand5;
            this.daGST.SelectCommand = this.sqlSelectCommand5;
            this.daGST.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GST_CODES", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("GST_CODE", "GST_CODE"),
                        new System.Data.Common.DataColumnMapping("GST_DESC", "GST_DESC"),
                        new System.Data.Common.DataColumnMapping("GST_PCT", "GST_PCT")})});
            this.daGST.UpdateCommand = this.sqlUpdateCommand5;
            // 
            // sqlDeleteCommand5
            // 
            this.sqlDeleteCommand5.CommandText = "DELETE FROM GST_CODES WHERE (GST_CODE = @Original_GST_CODE) AND (GST_DESC = @Orig" +
    "inal_GST_DESC) AND (GST_PCT = @Original_GST_PCT)";
            this.sqlDeleteCommand5.Connection = this.TR_Conn;
            this.sqlDeleteCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_DESC", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_DESC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_PCT", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_PCT", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand5
            // 
            this.sqlInsertCommand5.CommandText = "INSERT INTO GST_CODES(GST_CODE, GST_DESC, GST_PCT) VALUES (@GST_CODE, @GST_DESC, " +
    "@GST_PCT); SELECT GST_CODE, GST_DESC, GST_PCT FROM GST_CODES WHERE (GST_CODE = @" +
    "GST_CODE) ORDER BY GST_DESC";
            this.sqlInsertCommand5.Connection = this.TR_Conn;
            this.sqlInsertCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.VarChar, 1, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@GST_DESC", System.Data.SqlDbType.VarChar, 30, "GST_DESC"),
            new System.Data.SqlClient.SqlParameter("@GST_PCT", System.Data.SqlDbType.Money, 8, "GST_PCT")});
            // 
            // sqlSelectCommand5
            // 
            this.sqlSelectCommand5.CommandText = "SELECT GST_CODE, GST_DESC, GST_PCT FROM GST_CODES ORDER BY GST_DESC";
            this.sqlSelectCommand5.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand5
            // 
            this.sqlUpdateCommand5.CommandText = resources.GetString("sqlUpdateCommand5.CommandText");
            this.sqlUpdateCommand5.Connection = this.TR_Conn;
            this.sqlUpdateCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.VarChar, 1, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@GST_DESC", System.Data.SqlDbType.VarChar, 30, "GST_DESC"),
            new System.Data.SqlClient.SqlParameter("@GST_PCT", System.Data.SqlDbType.Money, 8, "GST_PCT"),
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_DESC", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_DESC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_PCT", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_PCT", System.Data.DataRowVersion.Original, null)});
            // 
            // daPST
            // 
            this.daPST.DeleteCommand = this.sqlDeleteCommand6;
            this.daPST.InsertCommand = this.sqlInsertCommand6;
            this.daPST.SelectCommand = this.sqlSelectCommand6;
            this.daPST.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SALES_TAXES", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SALES_TAX_ID", "SALES_TAX_ID"),
                        new System.Data.Common.DataColumnMapping("SALES_TAX_CODE", "SALES_TAX_CODE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"),
                        new System.Data.Common.DataColumnMapping("SALES_TAX", "SALES_TAX")})});
            this.daPST.UpdateCommand = this.sqlUpdateCommand6;
            // 
            // sqlDeleteCommand6
            // 
            this.sqlDeleteCommand6.CommandText = resources.GetString("sqlDeleteCommand6.CommandText");
            this.sqlDeleteCommand6.Connection = this.TR_Conn;
            this.sqlDeleteCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand6
            // 
            this.sqlInsertCommand6.CommandText = resources.GetString("sqlInsertCommand6.CommandText");
            this.sqlInsertCommand6.Connection = this.TR_Conn;
            this.sqlInsertCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 4, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, "SALES_TAX_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 30, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX", System.Data.SqlDbType.Float, 8, "SALES_TAX")});
            // 
            // sqlSelectCommand6
            // 
            this.sqlSelectCommand6.CommandText = "SELECT SALES_TAX_ID, SALES_TAX_CODE, DESCRIPTION, SALES_TAX FROM SALES_TAXES ORDE" +
    "R BY DESCRIPTION";
            this.sqlSelectCommand6.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand6
            // 
            this.sqlUpdateCommand6.CommandText = resources.GetString("sqlUpdateCommand6.CommandText");
            this.sqlUpdateCommand6.Connection = this.TR_Conn;
            this.sqlUpdateCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 4, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, "SALES_TAX_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 30, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX", System.Data.SqlDbType.Float, 8, "SALES_TAX"),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlDeleteCommand7;
            this.daSupplier.InsertCommand = this.sqlInsertCommand7;
            this.daSupplier.SelectCommand = this.sqlSelectCommand7;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daSupplier.UpdateCommand = this.sqlUpdateCommand7;
            // 
            // sqlDeleteCommand7
            // 
            this.sqlDeleteCommand7.CommandText = "DELETE FROM SUPPLIER_MASTER WHERE (SUPPLIER = @Original_SUPPLIER) AND (NAME = @Or" +
    "iginal_NAME OR @Original_NAME IS NULL AND NAME IS NULL)";
            this.sqlDeleteCommand7.Connection = this.TR_Conn;
            this.sqlDeleteCommand7.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand7
            // 
            this.sqlInsertCommand7.CommandText = "INSERT INTO SUPPLIER_MASTER(SUPPLIER, NAME) VALUES (@SUPPLIER, @NAME); SELECT SUP" +
    "PLIER, NAME FROM SUPPLIER_MASTER WHERE (SUPPLIER = @SUPPLIER) ORDER BY SUPPLIER";
            this.sqlInsertCommand7.Connection = this.TR_Conn;
            this.sqlInsertCommand7.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 40, "NAME")});
            // 
            // sqlSelectCommand7
            // 
            this.sqlSelectCommand7.CommandText = "SELECT SUPPLIER, NAME FROM SUPPLIER_MASTER WHERE (ACTIVE = \'T\') ORDER BY SUPPLIER" +
    "";
            this.sqlSelectCommand7.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand7
            // 
            this.sqlUpdateCommand7.CommandText = resources.GetString("sqlUpdateCommand7.CommandText");
            this.sqlUpdateCommand7.Connection = this.TR_Conn;
            this.sqlUpdateCommand7.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 40, "NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daDetail
            // 
            this.daDetail.SelectCommand = this.sqlSelectCommand8;
            this.daDetail.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_GL_ALLOC", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("DOC_NO", "DOC_NO"),
                        new System.Data.Common.DataColumnMapping("JOURNAL_NUMBER", "JOURNAL_NUMBER"),
                        new System.Data.Common.DataColumnMapping("JOURNAL_LINE_NO", "JOURNAL_LINE_NO"),
                        new System.Data.Common.DataColumnMapping("GL_ENTRIES_LINE_NO", "GL_ENTRIES_LINE_NO"),
                        new System.Data.Common.DataColumnMapping("GL_ACCOUNT", "GL_ACCOUNT"),
                        new System.Data.Common.DataColumnMapping("COMMENT", "COMMENT"),
                        new System.Data.Common.DataColumnMapping("AMOUNT", "AMOUNT"),
                        new System.Data.Common.DataColumnMapping("AP_REC_ENTRY_NO", "AP_REC_ENTRY_NO"),
                        new System.Data.Common.DataColumnMapping("TRANS_TYPE", "TRANS_TYPE"),
                        new System.Data.Common.DataColumnMapping("AP_GL_ALLOC_CODE", "AP_GL_ALLOC_CODE"),
                        new System.Data.Common.DataColumnMapping("AP_GL_ENTRIES_ID", "AP_GL_ENTRIES_ID"),
                        new System.Data.Common.DataColumnMapping("HOLD_AMT", "HOLD_AMT"),
                        new System.Data.Common.DataColumnMapping("HOLD_BAL", "HOLD_BAL"),
                        new System.Data.Common.DataColumnMapping("INV_BAL", "INV_BAL"),
                        new System.Data.Common.DataColumnMapping("SEQ", "SEQ"),
                        new System.Data.Common.DataColumnMapping("AGA_PROJ_CD", "AGA_PROJ_CD"),
                        new System.Data.Common.DataColumnMapping("AGA_RLH_CD", "AGA_RLH_CD"),
                        new System.Data.Common.DataColumnMapping("AGA_LOT", "AGA_LOT"),
                        new System.Data.Common.DataColumnMapping("AGA_BLOCK", "AGA_BLOCK"),
                        new System.Data.Common.DataColumnMapping("AGA_PLAN", "AGA_PLAN"),
                        new System.Data.Common.DataColumnMapping("AGA_JOB_COST_CD", "AGA_JOB_COST_CD"),
                        new System.Data.Common.DataColumnMapping("AGA_AGREE_NUM", "AGA_AGREE_NUM"),
                        new System.Data.Common.DataColumnMapping("AGA_INV_ID", "AGA_INV_ID"),
                        new System.Data.Common.DataColumnMapping("AGA_C_PROF_CNTR", "AGA_C_PROF_CNTR"),
                        new System.Data.Common.DataColumnMapping("AGA_C_LEASE_NUM", "AGA_C_LEASE_NUM"),
                        new System.Data.Common.DataColumnMapping("AGA_C_PROP_CD", "AGA_C_PROP_CD"),
                        new System.Data.Common.DataColumnMapping("AGA_R_PROF_CNTR", "AGA_R_PROF_CNTR"),
                        new System.Data.Common.DataColumnMapping("AGA_R_LEASE_NUM", "AGA_R_LEASE_NUM"),
                        new System.Data.Common.DataColumnMapping("AGA_R_PROP_CD", "AGA_R_PROP_CD"),
                        new System.Data.Common.DataColumnMapping("TRANSFER_FLAG", "TRANSFER_FLAG"),
                        new System.Data.Common.DataColumnMapping("pri_num", "pri_num"),
                        new System.Data.Common.DataColumnMapping("phs_code", "phs_code"),
                        new System.Data.Common.DataColumnMapping("subp_code", "subp_code"),
                        new System.Data.Common.DataColumnMapping("prp_component", "prp_component"),
                        new System.Data.Common.DataColumnMapping("prp_subcomp", "prp_subcomp"),
                        new System.Data.Common.DataColumnMapping("GST_AMT", "GST_AMT"),
                        new System.Data.Common.DataColumnMapping("PURCH_AMT", "PURCH_AMT"),
                        new System.Data.Common.DataColumnMapping("ITC", "ITC"),
                        new System.Data.Common.DataColumnMapping("GST_BAL", "GST_BAL"),
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO_TYPE", "PO_TYPE"),
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("CLEAR_REQUIRED", "CLEAR_REQUIRED"),
                        new System.Data.Common.DataColumnMapping("AP_INV_HEADER_ID", "AP_INV_HEADER_ID"),
                        new System.Data.Common.DataColumnMapping("AP_GL_ALLOC_ID", "AP_GL_ALLOC_ID"),
                        new System.Data.Common.DataColumnMapping("SEG_1", "SEG_1"),
                        new System.Data.Common.DataColumnMapping("SEG_2", "SEG_2"),
                        new System.Data.Common.DataColumnMapping("SEG_3", "SEG_3"),
                        new System.Data.Common.DataColumnMapping("SEG_4", "SEG_4"),
                        new System.Data.Common.DataColumnMapping("SEG_5", "SEG_5"),
                        new System.Data.Common.DataColumnMapping("SEG_6", "SEG_6"),
                        new System.Data.Common.DataColumnMapping("WCB_ASSESSABLE", "WCB_ASSESSABLE"),
                        new System.Data.Common.DataColumnMapping("SLS_ID", "SLS_ID"),
                        new System.Data.Common.DataColumnMapping("COMM_STAT_FLAG", "COMM_STAT_FLAG"),
                        new System.Data.Common.DataColumnMapping("COMPANY_ALIAS", "COMPANY_ALIAS"),
                        new System.Data.Common.DataColumnMapping("INTER_CO_ACCT", "INTER_CO_ACCT"),
                        new System.Data.Common.DataColumnMapping("SUBCONTRACTOR_TYPE_ID", "SUBCONTRACTOR_TYPE_ID"),
                        new System.Data.Common.DataColumnMapping("COMM_STAT_APPLICABLE", "COMM_STAT_APPLICABLE"),
                        new System.Data.Common.DataColumnMapping("USE_COMM_PCT", "USE_COMM_PCT"),
                        new System.Data.Common.DataColumnMapping("COMMISSION_PCT", "COMMISSION_PCT"),
                        new System.Data.Common.DataColumnMapping("EXCH_RATE", "EXCH_RATE"),
                        new System.Data.Common.DataColumnMapping("EXCH_AMT", "EXCH_AMT"),
                        new System.Data.Common.DataColumnMapping("PO_REC_DETAIL_ID", "PO_REC_DETAIL_ID"),
                        new System.Data.Common.DataColumnMapping("PROJ_BUD_EXCEEDED", "PROJ_BUD_EXCEEDED"),
                        new System.Data.Common.DataColumnMapping("unit_inv_id", "unit_inv_id"),
                        new System.Data.Common.DataColumnMapping("TIME_TICKET", "TIME_TICKET"),
                        new System.Data.Common.DataColumnMapping("AFE_NO", "AFE_NO"),
                        new System.Data.Common.DataColumnMapping("COST_CODE", "COST_CODE"),
                        new System.Data.Common.DataColumnMapping("SUB_CODE", "SUB_CODE"),
                        new System.Data.Common.DataColumnMapping("REFERENCE", "REFERENCE"),
                        new System.Data.Common.DataColumnMapping("SEG_CHANGE", "SEG_CHANGE"),
                        new System.Data.Common.DataColumnMapping("AGA_C_FLOOR", "AGA_C_FLOOR"),
                        new System.Data.Common.DataColumnMapping("AGA_C_SPACE", "AGA_C_SPACE"),
                        new System.Data.Common.DataColumnMapping("AGA_R_FLOOR", "AGA_R_FLOOR"),
                        new System.Data.Common.DataColumnMapping("AGA_R_UNIT", "AGA_R_UNIT"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID"),
                        new System.Data.Common.DataColumnMapping("Code", "Code"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            // 
            // sqlSelectCommand8
            // 
            this.sqlSelectCommand8.CommandText = resources.GetString("sqlSelectCommand8.CommandText");
            this.sqlSelectCommand8.Connection = this.TR_Conn;
            this.sqlSelectCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ap_inv_header_id", System.Data.SqlDbType.Int, 4, "AP_INV_HEADER_ID")});
            // 
            // daGLAccts
            // 
            this.daGLAccts.DeleteCommand = this.sqlDeleteCommand9;
            this.daGLAccts.InsertCommand = this.sqlInsertCommand9;
            this.daGLAccts.SelectCommand = this.sqlSelectCommand9;
            this.daGLAccts.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GL_ACCOUNTS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ACCOUNT_NUMBER", "ACCOUNT_NUMBER"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daGLAccts.UpdateCommand = this.sqlUpdateCommand9;
            // 
            // sqlDeleteCommand9
            // 
            this.sqlDeleteCommand9.CommandText = "DELETE FROM GL_ACCOUNTS WHERE (ACCOUNT_NUMBER = @Original_ACCOUNT_NUMBER) AND (DE" +
    "SCRIPTION = @Original_DESCRIPTION)";
            this.sqlDeleteCommand9.Connection = this.TR_Conn;
            this.sqlDeleteCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCOUNT_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand9
            // 
            this.sqlInsertCommand9.CommandText = "INSERT INTO GL_ACCOUNTS(ACCOUNT_NUMBER, DESCRIPTION) VALUES (@ACCOUNT_NUMBER, @DE" +
    "SCRIPTION); SELECT ACCOUNT_NUMBER, DESCRIPTION FROM GL_ACCOUNTS WHERE (ACCOUNT_N" +
    "UMBER = @ACCOUNT_NUMBER)";
            this.sqlInsertCommand9.Connection = this.TR_Conn;
            this.sqlInsertCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, "ACCOUNT_NUMBER"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 50, "DESCRIPTION")});
            // 
            // sqlSelectCommand9
            // 
            this.sqlSelectCommand9.CommandText = "SELECT ACCOUNT_NUMBER, DESCRIPTION FROM GL_ACCOUNTS";
            this.sqlSelectCommand9.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand9
            // 
            this.sqlUpdateCommand9.CommandText = resources.GetString("sqlUpdateCommand9.CommandText");
            this.sqlUpdateCommand9.Connection = this.TR_Conn;
            this.sqlUpdateCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, "ACCOUNT_NUMBER"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 50, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCOUNT_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daInvNo
            // 
            this.daInvNo.SelectCommand = this.sqlSelectCommand10;
            this.daInvNo.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_INV_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("INV_NO", "INV_NO")})});
            // 
            // sqlSelectCommand10
            // 
            this.sqlSelectCommand10.CommandText = "SELECT DISTINCT INV_NO FROM AP_INV_HEADER WHERE (INV_NO <> \'\') AND (INV_NO IS NOT" +
    " NULL) ORDER BY INV_NO";
            this.sqlSelectCommand10.Connection = this.TR_Conn;
            // 
            // daSwapSeg
            // 
            this.daSwapSeg.SelectCommand = this.sqlSelectCommand11;
            this.daSwapSeg.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GL_ACCOUNTS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Code", "Code"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            // 
            // sqlSelectCommand11
            // 
            this.sqlSelectCommand11.CommandText = "SELECT DISTINCT a.SEG_2 AS Code, m.SEGMENT_DESC AS Description FROM GL_ACCOUNTS a" +
    " LEFT OUTER JOIN GL_SEGMENT_SETUP m ON m.SEGMENT_VALUE = a.SEG_2 AND m.SEGMENT_N" +
    "UMBER = 2 ORDER BY a.SEG_2";
            this.sqlSelectCommand11.Connection = this.TR_Conn;
            // 
            // daPOFSelect
            // 
            this.daPOFSelect.SelectCommand = this.sqlSelectCommand12;
            this.daPOFSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("RECEIVER_NUMBER", "RECEIVER_NUMBER"),
                        new System.Data.Common.DataColumnMapping("RECEIPT_DATE", "RECEIPT_DATE"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount"),
                        new System.Data.Common.DataColumnMapping("EST", "EST"),
                        new System.Data.Common.DataColumnMapping("ACT", "ACT"),
                        new System.Data.Common.DataColumnMapping("OS", "OS")})});
            // 
            // sqlSelectCommand12
            // 
            this.sqlSelectCommand12.CommandText = resources.GetString("sqlSelectCommand12.CommandText");
            this.sqlSelectCommand12.Connection = this.TR_Conn;
            // 
            // daPOBSelect
            // 
            this.daPOBSelect.SelectCommand = this.sqlSelectCommand13;
            this.daPOBSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("RECEIVER_NUMBER", "RECEIVER_NUMBER"),
                        new System.Data.Common.DataColumnMapping("RECEIPT_DATE", "RECEIPT_DATE"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount"),
                        new System.Data.Common.DataColumnMapping("EST", "EST"),
                        new System.Data.Common.DataColumnMapping("ACT", "ACT"),
                        new System.Data.Common.DataColumnMapping("OS", "OS")})});
            // 
            // sqlSelectCommand13
            // 
            this.sqlSelectCommand13.CommandText = resources.GetString("sqlSelectCommand13.CommandText");
            this.sqlSelectCommand13.Connection = this.TR_Conn;
            // 
            // daPOMSelect
            // 
            this.daPOMSelect.SelectCommand = this.sqlSelectCommand14;
            this.daPOMSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("RECEIVER_NUMBER", "RECEIVER_NUMBER"),
                        new System.Data.Common.DataColumnMapping("RECEIPT_DATE", "RECEIPT_DATE"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount"),
                        new System.Data.Common.DataColumnMapping("EST", "EST"),
                        new System.Data.Common.DataColumnMapping("ACT", "ACT"),
                        new System.Data.Common.DataColumnMapping("OS", "OS")})});
            // 
            // sqlSelectCommand14
            // 
            this.sqlSelectCommand14.CommandText = resources.GetString("sqlSelectCommand14.CommandText");
            this.sqlSelectCommand14.Connection = this.TR_Conn;
            // 
            // daPODSelect
            // 
            this.daPODSelect.SelectCommand = this.sqlSelectCommand15;
            this.daPODSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("RECEIVER_NUMBER", "RECEIVER_NUMBER"),
                        new System.Data.Common.DataColumnMapping("RECEIPT_DATE", "RECEIPT_DATE"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount"),
                        new System.Data.Common.DataColumnMapping("EST", "EST"),
                        new System.Data.Common.DataColumnMapping("ACT", "ACT"),
                        new System.Data.Common.DataColumnMapping("OS", "OS")})});
            // 
            // sqlSelectCommand15
            // 
            this.sqlSelectCommand15.CommandText = resources.GetString("sqlSelectCommand15.CommandText");
            this.sqlSelectCommand15.Connection = this.TR_Conn;
            // 
            // daPOM2Select
            // 
            this.daPOM2Select.SelectCommand = this.sqlSelectCommand16;
            this.daPOM2Select.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("RECEIVER_NUMBER", "RECEIVER_NUMBER"),
                        new System.Data.Common.DataColumnMapping("RECEIPT_DATE", "RECEIPT_DATE"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount"),
                        new System.Data.Common.DataColumnMapping("EST", "EST"),
                        new System.Data.Common.DataColumnMapping("ACT", "ACT"),
                        new System.Data.Common.DataColumnMapping("OS", "OS")})});
            // 
            // sqlSelectCommand16
            // 
            this.sqlSelectCommand16.CommandText = resources.GetString("sqlSelectCommand16.CommandText");
            this.sqlSelectCommand16.Connection = this.TR_Conn;
            // 
            // daPOSelect
            // 
            this.daPOSelect.SelectCommand = this.sqlSelectCommand17;
            this.daPOSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_REC_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #")})});
            // 
            // sqlSelectCommand17
            // 
            this.sqlSelectCommand17.CommandText = "SELECT r.PO_REC_ID, p.PO AS [PO #] FROM PO_REC_HEADER r INNER JOIN PO_HEADER p ON" +
    " p.PO_ID = r.PO_ID INNER JOIN PO_REC_DETAIL rd ON rd.PO_REC_ID = r.PO_REC_ID";
            this.sqlSelectCommand17.Connection = this.TR_Conn;
            // 
            // daDetPO
            // 
            this.daDetPO.SelectCommand = this.sqlSelectCommand18;
            this.daDetPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            // 
            // sqlSelectCommand18
            // 
            this.sqlSelectCommand18.CommandText = resources.GetString("sqlSelectCommand18.CommandText");
            this.sqlSelectCommand18.Connection = this.TR_Conn;
            this.sqlSelectCommand18.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER")});
            // 
            // sqlSelectCommand19
            // 
            this.sqlSelectCommand19.CommandText = "SELECT TERMS_ID, TERM_CODE, DESCRIPTION FROM TERMS ORDER BY DESCRIPTION";
            this.sqlSelectCommand19.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand8
            // 
            this.sqlUpdateCommand8.CommandText = resources.GetString("sqlUpdateCommand8.CommandText");
            this.sqlUpdateCommand8.Connection = this.TR_Conn;
            this.sqlUpdateCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@TERM_CODE", System.Data.SqlDbType.VarChar, 0, "TERM_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_TERM_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERM_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand8
            // 
            this.sqlDeleteCommand8.CommandText = resources.GetString("sqlDeleteCommand8.CommandText");
            this.sqlDeleteCommand8.Connection = this.TR_Conn;
            this.sqlDeleteCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_TERM_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERM_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daTerms
            // 
            this.daTerms.DeleteCommand = this.sqlDeleteCommand8;
            this.daTerms.SelectCommand = this.sqlSelectCommand19;
            this.daTerms.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "TERMS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("TERMS_ID", "TERMS_ID"),
                        new System.Data.Common.DataColumnMapping("TERM_CODE", "TERM_CODE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daTerms.UpdateCommand = this.sqlUpdateCommand8;
            // 
            // sqlSelectCommand20
            // 
            this.sqlSelectCommand20.CommandText = "SELECT        AR_PWP_STATUS_ID, DESCRIPTION\r\nFROM            AR_PWP_STATUS";
            this.sqlSelectCommand20.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand8
            // 
            this.sqlInsertCommand8.CommandText = resources.GetString("sqlInsertCommand8.CommandText");
            this.sqlInsertCommand8.Connection = this.TR_Conn;
            this.sqlInsertCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION")});
            // 
            // sqlUpdateCommand10
            // 
            this.sqlUpdateCommand10.CommandText = resources.GetString("sqlUpdateCommand10.CommandText");
            this.sqlUpdateCommand10.Connection = this.TR_Conn;
            this.sqlUpdateCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand10
            // 
            this.sqlDeleteCommand10.CommandText = "DELETE FROM [AR_PWP_STATUS] WHERE (([AR_PWP_STATUS_ID] = @Original_AR_PWP_STATUS_" +
    "ID) AND ((@IsNull_DESCRIPTION = 1 AND [DESCRIPTION] IS NULL) OR ([DESCRIPTION] =" +
    " @Original_DESCRIPTION)))";
            this.sqlDeleteCommand10.Connection = this.TR_Conn;
            this.sqlDeleteCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daPWP_Status
            // 
            this.daPWP_Status.DeleteCommand = this.sqlDeleteCommand10;
            this.daPWP_Status.InsertCommand = this.sqlInsertCommand8;
            this.daPWP_Status.SelectCommand = this.sqlSelectCommand20;
            this.daPWP_Status.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AR_PWP_STATUS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daPWP_Status.UpdateCommand = this.sqlUpdateCommand10;
            // 
            // ucAP_Unpaid_Invoices
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.gcHeader);
            this.Controls.Add(this.panelContainer3);
            this.Controls.Add(this.panelContainer2);
            this.Name = "ucAP_Unpaid_Invoices";
            this.Size = new System.Drawing.Size(1352, 736);
            this.Load += new System.EventHandler(this.ucAP_Unpaid_Invoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeader1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGST1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCurrency1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPST1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllPO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSwapSeg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRoutePayHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer2.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtManChk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTerms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tERMSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerms1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAPCntl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAPSetupGL1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.dockPanel5.ResumeLayout(false);
            this.dockPanel5_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRAcctNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRCity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRAddr1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            this.dockPanel4.ResumeLayout(false);
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deInvDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInvDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSuppName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.dpActions.ResumeLayout(false);
            this.dockPanel6_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hlOverridePWPStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlAddPayHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlReleasePayHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlRefresh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlQikChk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlManChk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverridePWPStatus)).EndInit();
            this.panelContainer3.ResumeLayout(false);
            this.dockPanel8.ResumeLayout(false);
            this.dockPanel8_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGLAccts1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetPOSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetPO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aRPWPSTATUSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPWP_Status1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOFSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOFSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOBSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOBSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPODSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPODSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOMSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOMSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOM2Select)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOM2Select1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPOSelect)).EndInit();
            this.dockPanel7.ResumeLayout(false);
            this.dockPanel9.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ucAP_Unpaid_Invoices_Load(object sender, System.EventArgs e)
		{
			myMgr.FormInit( this );
			Popup = new frmPopup( myMgr );
			dpActions.Width = 295;
			
			FillDataSets( false );
			_Loaded = true;
		}

		public bool Loaded
		{
			get{ return _Loaded; }
		}

        private void SetupApprovalDT()
        {
            DataTable dtApproval = new DataTable("dtApproval");
            dtApproval.Columns.Add("ID", typeof(string));
            dtApproval.Columns.Add("Desc", typeof(string));

            dtApproval.Rows.Add(new object[] { "A", "Approved" });
            dtApproval.Rows.Add(new object[] { "D", "Declined" });
            dtApproval.Rows.Add(new object[] { "P", "Pending" });
            dtApproval.Rows.Add(new object[] { "Q", "Routing Required" });
            dtApproval.Rows.Add(new object[] { "R", "Recalled" });

            riRoutePayHold.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRoutePayHold.DataSource = dtApproval;
            riRoutePayHold.DisplayMember = "Desc";
            riRoutePayHold.ValueMember = "ID";
        }

        private void CheckWorkFlowApproval()
        {
            _WFRequired = false;

            string sSQL = @"select COUNT(*) from WF_ApprovalPoint where ISNULL(active,0) = 1 and WF_ID = 5";
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;
            if (Convert.ToInt32(oCNT) == 0)
            {
                colKC_PAYHOLD_STATUS.Visible = false;
                colKC_PAYHOLD_STATUS.OptionsColumn.ShowInCustomizationForm = false;
            }
            else
                _WFRequired = true;

            //string sSelect = "select count(*) from approval_topic where active=1 and id=" + CONST_SUBCON_COMP_PAY_HOLD;
            //object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.WebConnection);
            //if (obj != null)
            //{
            //    if (Convert.ToInt32(obj) == 0)
            //    {
            //        colKC_PAYHOLD_STATUS.Visible = false;
            //        colKC_PAYHOLD_STATUS.OptionsColumn.ShowInCustomizationForm = false;
            //    }
            //    else
            //        _WFRequired = true;
            //}
        }

		private void SetupCustomComps()
		{
			ucAccountingPicker1.UserName = Connection.MLUser;
			ucAccountingPicker1.ConnectionString = Connection.TRConnection;
			ucAccountingPicker1.DataBound = true;

			ucMPOR = new ucMatchPOReceipt( Connection, myMgr );
			ucMPOR.IsUnpaid = true;
			ucMPOR.Parent = dockPanel7;
			ucMPOR.Dock = DockStyle.Fill;

			ucUCPO = new ucUnreleasedContractPO( Connection, myMgr );
			ucUCPO.IsUnpaid = true;
			ucUCPO.Parent = dockPanel9;
			ucUCPO.Dock = DockStyle.Fill;

			ucAPGSTE = new ucAPGSTException( Connection, myMgr );
			ucAPGSTE.Parent = dockPanel4;
			ucAPGSTE.Dock = DockStyle.Fill;
			ucAPGSTE.DataBindings.Add( "Exception", dsHeader1, "AP_INV_HEADER.GST_EXCEPT" );
			ucAPGSTE.DataBindings.Add( "OtherException", dsHeader1, "AP_INV_HEADER.GST_EXCEPT_ID" );
			ucAPGSTE.Readonly = true;
		}

		private void SetupHdrSwapSeg()
		{
			string sSelect = "select isnull(clear_req,'F') from gl_setup";
			if( ExecuteScalar( sSelect, TR_Conn ).ToString().ToUpper().Trim() == "T" )
			{
				sSelect = "select clear_seg from gl_setup";
				object obj = ExecuteScalar( sSelect, TR_Conn );
				if( obj != null && obj.GetType() != typeof( DBNull ) )
				{
					int Column = Convert.ToInt32( obj );
					string ColumnName = "SEG_"+Column+"_DESC";

					sSelect = "select "+ColumnName+" from gl_setup";
					daSwapSeg.SelectCommand.CommandText = "select distinct a.seg_"+Column+" [Code], g.SEGMENT_DESC [Description] "+
						"from gl_accounts a "+	
						"left outer join gl_segment_setup g on g.SEGMENT_VALUE = a.seg_"+Column+" "+
						"and SEGMENT_NUMBER = "+Column+" "+
						"order by a.seg_"+Column;
					dsSwapSeg1.Clear();
					daSwapSeg.Fill( dsSwapSeg1 );

					obj = ExecuteScalar( sSelect, TR_Conn );
					if( obj != null && obj.GetType() != typeof( DBNull ) )
					{
						colAP_DIV.Caption = obj.ToString();
						colAP_DIV.Visible = true;
						colAP_DIV.OptionsColumn.ShowInCustomizationForm = true;
					}
				}
			}
			else
			{
				colAP_DIV.Visible = false;
				colAP_DIV.OptionsColumn.ShowInCustomizationForm = false;
			}			
		}	

		private void FillDataSets( bool Refresh )
		{
			int iHeaderHandle = 0;
			if( Refresh )
			{
				if( gvHeader != null )
				{
					iHeaderHandle = gvHeader.FocusedRowHandle;
				}

				dsHeader1.Clear();
				dsDetail1.Clear();
				dsAPSetupGL1.Clear();
				dsCurrency1.Clear();
				dsGST1.Clear();
				dsPST1.Clear();
				dsSupplier1.Clear();
				dsGLAccts1.Clear();
				dsAllPO1.Clear();
				dsInvNo1.Clear();
                dsTerms1.Clear();
                dsPWP_Status1.Clear();

				dsPOSelect1.Clear();
				dsPOFSelect1.Clear();
				dsPOBSelect1.Clear();
				dsPODSelect1.Clear();
				dsPOMSelect1.Clear();
				dsPOM2Select1.Clear();
			}

			daHeader.Fill( dsHeader1 );		
			daAPSetupGL.Fill( dsAPSetupGL1 );			
			daCurrency.Fill( dsCurrency1 );
			daGST.Fill( dsGST1 );
			daPST.Fill( dsPST1 );
			daSupplier.Fill( dsSupplier1 );
			daGLAccts.Fill( dsGLAccts1 );
			daAllPO.Fill( dsAllPO1 );
			daInvNo.Fill( dsInvNo1 );
            daTerms.Fill(dsTerms1);
            daPWP_Status.Fill(dsPWP_Status1);

			daPOSelect.Fill( dsPOSelect1 );

			daPOFSelect.Fill( dsPOFSelect1 );
			daPOBSelect.Fill( dsPOBSelect1 );
			daPODSelect.Fill( dsPODSelect1 );
			daPOMSelect.Fill( dsPOMSelect1 );
			daPOM2Select.Fill( dsPOM2Select1 );

			if( ucAPGSTE != null )
				ucAPGSTE.RefreshMe();

			if( Refresh )
			{
				if( gvHeader != null )
				{
					gvHeader.FocusedRowHandle = iHeaderHandle;
					gvHeader_FocusedRowChanged(null,null);
				}
			}			
		}

		private void SetupInvoiceType()
		{
			dtInvType = new DataTable( "INVTYPE" );
			dtInvType.Columns.Add( "Code", typeof( String ) );
			dtInvType.Columns.Add( "Type", typeof( String ) );

			dtInvType.Rows.Add( new object[] { "I", "Invoice" } );
			dtInvType.Rows.Add( new object[] { "A", "Adjustment" } );
			dtInvType.Rows.Add( new object[] { "M", "Manual Check" } );

			lueInvType.Properties.DataSource = dtInvType;
			lueInvType.Properties.DisplayMember = "Type";
			lueInvType.Properties.ValueMember = "Code";

			lueInvType.Properties.Columns.Add( new DevExpress.XtraEditors.Controls.LookUpColumnInfo( "Type", "Type", 150 ) );

			repositoryItemLookUpEdit1.DataSource = dtInvType;
			repositoryItemLookUpEdit1.DisplayMember = "Type";
			repositoryItemLookUpEdit1.ValueMember = "Code";

			repositoryItemLookUpEdit1.Columns.Add( new DevExpress.XtraEditors.Controls.LookUpColumnInfo( "Type", "Type", 150 ) );

			dtDetType = new DataTable( "DETTYPE" );
			dtDetType.Columns.Add( "Code", typeof( String ) );
			dtDetType.Columns.Add( "Type", typeof( String ) );

			dtDetType.Rows.Add( new object[] { "I", "Invoice" } );
			dtDetType.Rows.Add( new object[] { "F", "Freight" } );
			dtDetType.Rows.Add( new object[] { "D", "Duty" } );
			dtDetType.Rows.Add( new object[] { "B", "Brokerage" } );
			dtDetType.Rows.Add( new object[] { "M", "Miscellaneous" } );
			dtDetType.Rows.Add( new object[] { "2", "Miscellaneous 2" } );

			repositoryItemLookUpEdit5.DataSource = dtDetType;
			repositoryItemLookUpEdit5.DisplayMember = "Type";
			repositoryItemLookUpEdit5.ValueMember = "Code";

			repositoryItemLookUpEdit5.Columns.Add( new DevExpress.XtraEditors.Controls.LookUpColumnInfo( "Type", "Type", 150 ) );
		}

		private void panelContainer3_Resize(object sender, System.EventArgs e)
		{
			try
			{
				dpActions.Height = panelContainer3.Height;
			}
			catch{}
		}

		private void gvHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			dsDetail1.Clear();
			dsDetPO1.Clear();
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oPeriod = dr["ACCT_PERIOD"];
				object oYear = dr["ACCT_YEAR"];
				if( oPeriod != null && oPeriod != DBNull.Value && oYear != null && oYear != DBNull.Value )
				{
					ucAccountingPicker1.SelectedPeriod = Convert.ToInt32( dr["ACCT_PERIOD"] );
					ucAccountingPicker1.SelectedYear = Convert.ToInt32( dr["ACCT_YEAR"] );	
				}
			
				object oID = dr["AP_INV_HEADER_ID"];
				if( oID != null && oID != DBNull.Value )
				{
					daDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oID;
                    if (daDetail.SelectCommand.Connection.State == ConnectionState.Closed)
                    {
                        daDetail.Fill(dsDetail1);
                        ucMPOR.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                        ucUCPO.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                    }
				}
				else
				{
					ucMPOR.AP_INV_HEADER_ID = -1;
					ucUCPO.AP_INV_HEADER_ID = -1;
				}

				object oSupplier = dr["SUPPLIER"];
				if( oSupplier != null && oSupplier != DBNull.Value )
				{                    
                    if (daDetPO.SelectCommand.Connection.State == ConnectionState.Closed)
                    {
                        daDetPO.SelectCommand.CommandText = sdaDetPOSelect.Replace("@SUPPLIER", "'" + oSupplier.ToString() + "'");
                        daDetPO.Fill(dsDetPO1);
                    }
				}

				object oPO_ID = dr["PO_ID"];
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
					ucMPOR.PO_ID = Convert.ToInt32( oPO_ID );
					ucUCPO.PO_ID = Convert.ToInt32( oPO_ID );
				}
				else
				{
					ucMPOR.PO_ID = -1;
					ucUCPO.PO_ID = -1;
				}
                RoutingLock();
			}
		}

		public void RefreshMe()
		{
			CL_Dialog.PleaseWait.Show( "Refreshing Unpaid Invoices", null );
			FillDataSets( true );
			CL_Dialog.PleaseWait.Hide();
		}

		private void hlRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshMe();
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

		private object[] ExecuteDataRow( string CmdText, SqlConnection Conn )
		{
			SqlConnection Connection = new SqlConnection( Conn.ConnectionString );
			SqlCommand cmd = new SqlCommand( CmdText, Connection );
			object []oArr = null;
			try
			{		
				Connection.Open();
				
				using( SqlDataReader reader = cmd.ExecuteReader() )
				{					
					if( reader.Read() )
					{
						oArr = new object[reader.FieldCount];
						for( int i = 0; i < reader.FieldCount; i++ )
						{
							oArr[i] = reader[i];
						}
					}					
					reader.Close();
				}				
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show( ex.Message + ex.StackTrace );
			}
			finally
			{
				Connection.Close();
			}
			return oArr;
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			lueSupplier.EditValue = null;
			lueSuppName.EditValue = null;
			lueInvNo.EditValue = null;
			deInvDate.EditValue = null;
			
			daHeader.SelectCommand.CommandText = _HeaderSelect;
			dsHeader1.Clear();
			daHeader.Fill( dsHeader1 );
			gvHeader_FocusedRowChanged(null,null);
		}

		private void btnFilter_Click(object sender, System.EventArgs e)
		{
			string sHeadSelect = _HeaderSelect;
			if( lueSupplier.EditValue != null )
				sHeadSelect = sHeadSelect.Replace( "ORDER BY", " and supplier='"+lueSupplier.EditValue+"' ORDER BY " );
			if( lueSuppName.EditValue != null )
				sHeadSelect = sHeadSelect.Replace( "ORDER BY", " and supplier='"+lueSuppName.EditValue+"' ORDER BY " );
			if( lueInvNo.EditValue != null )
				sHeadSelect = sHeadSelect.Replace( "ORDER BY", " and inv_no='"+lueInvNo.EditValue+"' ORDER BY " );
			if( deInvDate.EditValue != null )
                sHeadSelect = sHeadSelect.Replace( "ORDER BY", " and inv_date='"+deInvDate.DateTime.ToShortDateString()+"' ORDER BY " );				
			daHeader.SelectCommand.CommandText = sHeadSelect;
			dsHeader1.Clear();
			daHeader.Fill( dsHeader1 );
			gvHeader_FocusedRowChanged(null,null);
		}

		private void hlManChk_Click(object sender, System.EventArgs e)
		{
			if( !KCA_Validator.ValidatePassword( CONST_MANUAL_CHECK ) )
				return;
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oINVOICE_TYPE = dr["INVOICE_TYPE"];
				if( oINVOICE_TYPE != null && oINVOICE_TYPE != DBNull.Value )
				{
					if( oINVOICE_TYPE.ToString() == "I" )
					{
                        frmManChk fMC = new frmManChk(Connection, myMgr, dr["SUPPLIER"].ToString(), Convert.ToInt32( dr["AP_INV_HEADER_ID"] ));
						if( fMC.ShowDialog() == DialogResult.OK )
						{
							if( fMC.ManualCheck != -1 )
							{
								string sUpdate = "update ap_inv_header set manual_check="+fMC.ManualCheck+", INVOICE_TYPE='M' where ap_inv_header_id="+dr["AP_INV_HEADER_ID"];
                                ExecuteNonQuery( sUpdate, TR_Conn );
								FillDataSets( true );
							}
						}
					}
				}
			}
		}

		private bool ProcessingLocked()
		{
			string sSelect = "select CHECK_PROCESS_LOCK from ap_setup";
			object oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
			if( oResult == null || oResult == DBNull.Value )
				return false;
			else
			{
				if( oResult.ToString().Trim().Equals("") )
				{
					return false;
				}
				else if( oResult.ToString().Trim().ToLower().Equals( Connection.MLUser.ToLower() ) )
				{
					return false;
				}
				else
				{
					Popup.ShowPopup( "Unable to print checks -- User: "+oResult+" is currently locking this process. The lock will be released when the process finishes. "+
						"Please try again later or contact your administrator." );
					return true;
				}
			}
		}

		private void hlQikChk_Click(object sender, System.EventArgs e)
		{
			if( !KCA_Validator.ValidatePassword( CONST_QUICK_CHECK ) )
				return;
			
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oID = dr["AP_INV_HEADER_ID"];
				object oP = dr["ACCT_PERIOD"];
				object oY = dr["ACCT_YEAR"];
				if( oID != null && oID != DBNull.Value )
				{
                    // VALIDATE THAT NO PWP STATUS OF OPEN, PENDING, REJECTED EXISTS
                    string sSQL = @"select COUNT(*) from AP_GL_ALLOC where AP_INV_HEADER_ID = " + oID + @" and AR_PWP_STATUS_ID in (1, 3, 4)";
                    object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oCNT == null || oCNT == DBNull.Value)
                        oCNT = 0;

                    if (Convert.ToInt32(oCNT) > 0)
                    {
                        Popup.ShowPopup("Unable to process payment, detail records exist with a paid when paid status of either open, pending or rejected.");
                        return;
                    }

                    object oSTATUS = Connection.SQLExecutor.ExecuteScalar("select isnull(KC_PAYHOLD_STATUS,'') from ap_inv_header where ap_inv_header_id=" + oID, Connection.TRConnection);
                    if (oSTATUS == null || oSTATUS == DBNull.Value)
                        oSTATUS = "";
                    if (oSTATUS.Equals("Q") || oSTATUS.Equals("P") || oSTATUS.Equals("R") || oSTATUS.Equals("D"))
                    {
                        Popup.ShowPopup("A payment cannot be processed because of a payment hold routing status on the invoice.");
                        return;
                    }

					string sSelect = "select count(*) from ap_ck_select where ap_inv_header_id="+oID;
					object oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( Convert.ToInt32( oResult ) > 0 )
					{
						Popup.ShowPopup( "This invoice is currently being processed, unable to process a quick check." );
						return;
					}

					sSelect = "select count(*) from AP_CK_SELECT_BATCH_DET where ap_inv_header_id="+oID;
					oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( Convert.ToInt32( oResult ) > 0 )
					{
						Popup.ShowPopup( "This invoice is part of a batch that is being processed, unable to process a quick check." );
						return;
					}

					sSelect = "select isnull(balance,0) from ap_inv_header where ap_inv_header_id="+oID;
					oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( oResult != null && oResult != DBNull.Value )
					{
						if( Convert.ToDouble( oResult ) == 0 )
						{
							Popup.ShowPopup( "A check has been made for this invoice. Refreshing unpaid invoices." );
							RefreshMe();
							return;
						}
						else if( Convert.ToDouble( oResult ) < 0 )
						{
							Popup.ShowPopup( "Unable to process check, balance must be a positive amount. Refreshing unpaid invoices." );
							RefreshMe();
							return;
						}
					}

					if( ProcessingLocked() )
						return;
					Connection.SQLExecutor.ExecuteNonQuery( "update ap_setup set CHECK_PROCESS_LOCK = '"+Connection.MLUser+"'", Connection.TRConnection );

                    using (frmQuickCheck fQuickCheck = new frmQuickCheck(Connection, myMgr, Convert.ToInt32(oID), Convert.ToInt32(oY), Convert.ToInt32(oP)))
                    {
                        if (fQuickCheck.ShowDialog() == DialogResult.OK)
                        {
                            RefreshMe();
                        }
                    }

					Connection.SQLExecutor.ExecuteNonQuery( "update ap_setup set CHECK_PROCESS_LOCK = null", Connection.TRConnection );
				}
			}
		}

        private void riRouting_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void gvHeader_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(e.RowHandle);
            if (e.Column == colKC_PAYHOLD_STATUS)
            {             
                if (dr != null)
                {
                    object oStatus = dr["KC_PAYHOLD_STATUS"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals(""))
                        e.RepositoryItem = riEmpty;
                    else
                        e.RepositoryItem = riRoutePayHold;
                }
            }
        }

        private void riRoutePayHold_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            #region New Workflow Routing

            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                string sSQL = @"select WS_Approval_WorkFlow_ID from WF_ApprovalPoint where WF_ID = 5";
                object oWorkFlow_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
                if (oWorkFlow_ID == null || oWorkFlow_ID == DBNull.Value)
                {
                    Popup.ShowPopup("The 'Subcontractor Compliance Payment Hold' work flow routing point is incorrectly configured.");
                    return;
                }

                object oSTATUS = dr["KC_PAYHOLD_STATUS"];
                if (oSTATUS.Equals("Q") || oSTATUS.Equals("D") || oSTATUS.Equals("P") || oSTATUS.Equals("A"))
                {
                    WorkFlowRouting.frmApprovalViewer fViewer = new WorkFlowRouting.frmApprovalViewer(Connection, myMgr);
                    fViewer.LoadStatus(CONST_SUBCON_COMP_PAY_HOLD_WF, oSTATUS.ToString(), Convert.ToInt32(dr["ap_inv_header_id"]));
                    if (fViewer.ShowDialog() == DialogResult.OK)
                    {
                        RefreshMe();
                    }
                }
            }
            #endregion

            #region Old Standard Routing

            //DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            //if (dr != null)
            //{
            //    object oSTATUS = dr["KC_PAYHOLD_STATUS"];
            //    if (oSTATUS.Equals("Q") || oSTATUS.Equals("R") || oSTATUS.Equals("D") || oSTATUS.Equals("P"))
            //    {
            //        bool bNoRecall = true;
            //        if (oSTATUS.Equals("P"))
            //            bNoRecall = false;
            //        DevExpress.XtraEditors.XtraForm f = new XtraForm();
            //        f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //        f.ShowInTaskbar = false;

            //        ucARHV = new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer(Connection, myMgr, false);
            //        ucARHV.Dock = DockStyle.Fill;
            //        ucARHV.RequestRecalled += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestRecalled(ucARHV_RequestRecalled);
            //        ucARHV.RequestSubmitted += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestSubmitted(ucARHV_RequestSubmitted);
            //        ucARHV.RefreshMe(dr["AP_INV_HEADER_ID"].ToString(), "AP", null, CONST_SUBCON_COMP_PAY_HOLD, false, bNoRecall);
            //        ucARHV.Parent = f;
            //        f.StartPosition = FormStartPosition.CenterParent;
            //        f.Size = new Size(650, 650);
            //        f.ShowDialog();
            //        ucARHV.Dispose();
            //        ucARHV = null;
            //    }
            //}

            #endregion
        }

        private void ucARHV_RequestRecalled(int ApprovalRequestID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["KC_PAYHOLD_STATUS"] = "R";
                dr.AcceptChanges();
                gvHeader.RefreshData();
                object oID = dr["AP_INV_HEADER_ID"];
                string sUpdate = "update ap_inv_header set KC_PAYHOLD_STATUS='R' where AP_INV_HEADER_ID = "+oID;
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + oID + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Recalled invoice subcontractor compliance payment hold routing.', '', 1 ";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);

                RoutingLock();
            }
        }

        private void ucARHV_RequestSubmitted(int ApprovalRequestID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["KC_PAYHOLD_STATUS"] = "P";
                dr.AcceptChanges();
                gvHeader.RefreshData();
                object oID = dr["AP_INV_HEADER_ID"];
                string sUpdate = "update ap_inv_header set KC_PAYHOLD_STATUS='P' where AP_INV_HEADER_ID = " + oID;
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + dr["ap_inv_header_id"] + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Invoice submitted for subcontractor compliance payment hold routing.', '', 1 ";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);

                RoutingLock();
                ucARHV.ParentForm.Close();
            }
        }

        private void RoutingLock()
        {
            hlManChk.Enabled = true;
            hlQikChk.Enabled = true;

            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object obj = dr["KC_PAYHOLD_STATUS"];
                if (obj == null || obj == DBNull.Value)
                    obj = "";

                if (obj.Equals("Q") || obj.Equals("R") || obj.Equals("D") || obj.Equals("P"))
                {
                    hlManChk.Enabled = false;
                    hlQikChk.Enabled = false;
                }

            }
        }

        private void hlReleasePayHold_Click(object sender, EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oPAYMENT_HOLD = dr["PAYMENT_HOLD"];
                if (oPAYMENT_HOLD == null || oPAYMENT_HOLD == DBNull.Value)
                    oPAYMENT_HOLD = "F";

                if (oPAYMENT_HOLD.Equals("F"))
                {
                    Popup.ShowPopup("No payment hold exists on the current invoice.");
                    return;
                }
                
                if (!KCA_Validator.ValidatePassword(CONST_RELEASE_AP_PAY_HOLD))
                    return;

                if (Popup.ShowPopup("Are you sure you want to release the payment hold on the selected invoice?", WS_Popups.frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                {
                    string sSQL = @"update ap_inv_header set KC_PAYHOLD_STATUS=null, PAYMENT_HOLD = 'F' where ap_inv_header_id=" + dr["AP_INV_HEADER_ID"];
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

                    dr.BeginEdit();
                    dr["PAYMENT_HOLD"] = "F";
                    dr["KC_PAYHOLD_STATUS"] = DBNull.Value;
                    dr.EndEdit();
                }
            }
        }

        private void hlAddPayHold_Click(object sender, EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                if (Popup.ShowPopup("Are you sure you want to put a payment hold on the selected invoice?", WS_Popups.frmPopup.PopupType.OK_Cancel)
                    == frmPopup.PopupResult.OK)
                {
                    object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                    if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
                    {
                        string sSQL = "update ap_inv_header set PAYMENT_HOLD='T' where AP_INV_HEADER_ID=" + oAP_INV_HEADER_ID;
                        Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

                        dr.BeginEdit();
                        dr["PAYMENT_HOLD"] = "T";
                        dr.EndEdit();
                    }
                }
            }
        }

        private void hlOverridePWPStatus_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                if (!KCA_Validator.ValidatePassword(CONST_OVERRIDE_PWP_STATUS))
                    return;

                object oPWP_Status = dr["AR_PWP_STATUS_ID"];
                if (oPWP_Status != null && oPWP_Status != DBNull.Value)
                {
                    int iPWP_Status = Convert.ToInt32(oPWP_Status);
                    if (iPWP_Status == CONST_PWP_STATUS_OPEN || iPWP_Status == CONST_PWP_STATUS_REJECTED || iPWP_Status == CONST_PWP_STATUS_PENDING)
                    {
                        dr.BeginEdit();
                        dr["AR_PWP_STATUS_ID"] = CONST_PWP_STATUS_AVAILABLE;
                        dr.EndEdit();
                        dr.AcceptChanges();
                        string sSQL = @"update ap_gl_alloc set AR_PWP_STATUS_ID=" + CONST_PWP_STATUS_AVAILABLE + @" where ap_gl_alloc_id=" + dr["AP_GL_ALLOC_ID"];
                        Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                    }
                    else
                    {
                        Popup.ShowPopup("Paid when paid status does not exist for override on the selected detail record.");
                    }
                }
                else
                {
                    Popup.ShowPopup("Paid when paid status does not exist for override on the selected detail record.");
                }
            }
        }
	}
}

