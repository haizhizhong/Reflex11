using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using TUC_GridG;
using System.Data.SqlClient;
using CustomerInvoiceSearch;
using APGSTException;
using APPOSelect;
using CL_Dialog;
using ApprovalRequestHistoryViewer;
using HM_Report_Printer;

namespace AP_Invoice_Entry
{
	public class ucAP_InvoiceEntry : DevExpress.XtraEditors.XtraUserControl
    {
        #region ucAP_InvoiceEntry Class Variables
        
        private WS_Popups.frmPopup Popup;
		private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
		private GL_Account_Lookup_Rep.Repository_GL_Lookup GL_Repository;
		private Supplier_Lookup_Rep.Repository_Supplier_Lookup Supp_Repository;
        private ChargeBackPicker.PopupChargeBackLookupRepository PopupChgBk;
		private HMCon Connection;	
		private ucAPGSTException ucAPGSTE;
		private ucMatchPOReceipt ucMPOR;
		private ucUnreleasedContractPO ucUCPO;
        private ucSummaryContractPO ucSCPO;
		private DataTable dtInvType;
		private DataTable dtDetType;
        private ucApprovalRequestHistoryViewer ucARHV;
        private AP_SubcontractorCompliance.SupplierSubConValidator SubCon;
        private int Current_AP_INV_ID = -1;
        private int _AP_INV_HEADER_ID = -1;
        private int _PO_ID = -1;
		private GridG ggHeader;
		private GridG ggDetail;
        private AP_Levy.ucLevyMatch LevyMatch;

        private bool _DefaultsEnabled = true;
		private bool bNewRow = false;
		private bool _HeaderValid = true;
		private bool AutoUpdate = false;
		private InvoiceSearch ucIS;
		private bool _SwapSegEnabled = false;
		private bool _SegEnabled = false;
		private bool CodeChanging = false;
		private int UserWHSEID;
        private bool _WFRequired = false;
        private bool _PO_LOCKDOWN = false;
        private bool _MatchPOLoaded = false;
        private bool _UnreleasedLoaded = false;
        private bool _SummUnreleasedLoaded = false;
        private bool _AP_WFRequired = false;
        private bool _AP_ForceWF = false;
        private bool _HoldbackEdit = true;
        private bool _ShowContractPOSummary = false;
        private string RelType = "APINV";
        private string PORelType = "PO";        
        private string HeaderError = "";

		private KeyControlAccess.Validator KCA_Validator;	
		private const int CONST_QUICK_CHECK = 14;	
		private const int CONST_OVERRIDE_AP_CONTROL = 76;
		private const int CONST_OVERRIDE_SUPPLIER_ON_PO = 140;
        private const int CONST_UNAPPROVED_CONTRACT_PO_APPROVAL_TOPIC_ID = 58;
        private const int CONST_SUBCON_COMP_PRE_ACCRUAL = 59;
        private const int CONST_HOLDBACK_EDIT = 250;
        private const int CONST_PO_RESTOCKING_AMT_EDIT = 265;
        private const int CONST_CHARGE_BACK_DELETE = 270;
        private const int CONST_CHANGE_CREDIT_TERMS = 272;
        private const int CONST_SUBCONTRACTOR_COMPLIANCE_OVERRIDE = 354;
        private const string CONST_SUPRESS_ERROR = "~SuppressError~";
        private const int CONST_SUBCON_COMP_PRE_ACCRUAL_WF = 4;
        private const int CONST_SUPPLIER_EDIT = 21;
        private const int CONST_ACCOUNTS_PAYABLE_WF = 8;

        private const int CONST_PWP_STATUS_OPEN = 1;
        private const int CONST_PWP_STATUS_AVAILABLE = 2;
        private const int CONST_PWP_STATUS_REJECTED = 3;
        private const int CONST_PWP_STATUS_PENDING = 4;
        private const int CONST_OVERRIDE_PWP_STATUS = 358;

		private string sdaDetPOSelect = "SELECT DISTINCT POHDR.PO_ID, POHDR.PO, POHDR.ORDER_DATE "+
			"FROM PO_HEADER POHDR  "+
			"LEFT OUTER JOIN PO_REC_HEADER HDR ON POHDR.PO_ID = HDR.PO_ID  "+
			"LEFT OUTER JOIN PO_REC_DETAIL dtl ON dtl.PO_REC_ID = HDR.PO_REC_ID  "+
			"WHERE  "+
			"(ISNULL(dtl.RECEIVER_MATCH_STATUS, 'F') = 'F') AND  "+
			"(POHDR.SUPPLIER = @SUPPLIER) AND  "+
			"(POHDR.STATUS <> 'Closed') AND  "+
			"((HDR.PO_REC_ID IS NOT NULL) OR (ISNULL(POHDR.CONTRACT_PO, 'F') <> 'F')) ";

        #endregion

        #region ucAP_InvoiceEntry Component Variables

        private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraEditors.TextEdit txtRemain;
		private DevExpress.XtraEditors.TextEdit txtUndist;
		private DevExpress.XtraEditors.TextEdit txtInvAmt;
		private DevExpress.XtraGrid.GridControl gcHeader;
		private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraGrid.GridControl gcDetail;
		private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
		private DevExpress.XtraBars.Docking.DockPanel dpRemit;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel5;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel5_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel6;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel6_Container;
		private DevExpress.XtraBars.Docking.DockPanel dpInvDefaults;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel7_Container;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
		private System.Data.SqlClient.SqlConnection TR_Conn;
		private System.Data.SqlClient.SqlDataAdapter daInvHeader;
		private DevExpress.XtraGrid.Columns.GridColumn colSUPPLIER;
        private DevExpress.XtraGrid.Columns.GridColumn colINV_NO;
		private DevExpress.XtraGrid.Columns.GridColumn colTRANS_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colINV_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colDUE_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colDISCOUNT_DATE;
		private DevExpress.XtraGrid.Columns.GridColumn colINV_AMOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colDISCOUNT_AMOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colREFERENCE;
		private DevExpress.XtraGrid.Columns.GridColumn colOPERATOR;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD;
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
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit5;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit6;
		private DevExpress.XtraLayout.LayoutControl lcDefaults;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
		private DevExpress.XtraLayout.LayoutControlItem lciInvoiceType;
		private DevExpress.XtraLayout.LayoutControlItem lciCurrency;
		private DevExpress.XtraLayout.LayoutControlItem lciTerms;
		private DevExpress.XtraLayout.LayoutControlItem lciDiscountPct;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
		private DevExpress.XtraGrid.Columns.GridColumn colAP_SETUP_GL_ID;
		private System.Data.SqlClient.SqlDataAdapter daAPSetupGL;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		private AP_Invoice_Entry.dsAPSetupGL dsAPSetupGL1;
		private DevExpress.XtraEditors.LookUpEdit lueAPCntl;
		private DevExpress.XtraEditors.LookUpEdit lueCurrency;
		private DevExpress.XtraEditors.LookUpEdit lueInvType;
		private System.Data.SqlClient.SqlDataAdapter daCurrency;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand3;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand3;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		private AP_Invoice_Entry.dsCurrency dsCurrency1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
		private DevExpress.XtraEditors.LookUpEdit lueTerms;
		private DevExpress.XtraEditors.CheckEdit chkHold;
		private DevExpress.XtraEditors.CheckEdit chkKCApproval;
		private DevExpress.XtraEditors.CheckEdit chkKCRouting;
		private DevExpress.XtraEditors.DateEdit deHoldDue;
		private DevExpress.XtraEditors.TextEdit txtHoldA;
		private DevExpress.XtraEditors.TextEdit txtHoldP;
		private DevExpress.XtraEditors.TextEdit txtDiscA;
		private DevExpress.XtraEditors.TextEdit txtDiscP;
		private DevExpress.XtraEditors.DateEdit deDiscDate;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
		private System.Data.SqlClient.SqlDataAdapter daGST;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand4;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand4;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand3;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand3;
		private AP_Invoice_Entry.dsGST dsGST1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
		private System.Data.SqlClient.SqlDataAdapter daPST;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand5;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand5;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand4;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand4;
		private AP_Invoice_Entry.dsPST dsPST1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riPaymentHold;
		private System.Data.SqlClient.SqlDataAdapter daTerms;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand6;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand6;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand5;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand5;
		private AP_Invoice_Entry.dsTerms dsTerms1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
		private System.Data.SqlClient.SqlDataAdapter daSupplier;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand7;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand7;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand6;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand6;
		private AP_Invoice_Entry.dsSupplier dsSupplier1;
		private DevExpress.XtraEditors.DataNavigator dnHeaderNav;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riManualChkNo;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riInvoiceType;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit8;
		private DevExpress.XtraLayout.LayoutControl layoutControl3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraEditors.TextEdit txtRName;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
		private DevExpress.XtraEditors.TextEdit txtRAddr1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
		private DevExpress.XtraEditors.TextEdit txtRAddr2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
		private DevExpress.XtraEditors.TextEdit txtRAddr3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
		private DevExpress.XtraEditors.TextEdit txtRCity;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
		private DevExpress.XtraEditors.TextEdit txtRState;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
		private DevExpress.XtraEditors.TextEdit txtRZip;
		private DevExpress.XtraEditors.TextEdit txtRAcctNo;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
		private DevExpress.XtraLayout.LayoutControl layoutControl4;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
		private DevExpress.XtraEditors.MemoEdit memoComment;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
		private System.Data.SqlClient.SqlDataAdapter daSwapSeg;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand8;
		private AP_Invoice_Entry.dsSwapSeg dsSwapSeg1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit7;
		private System.Data.SqlClient.SqlDataAdapter daAllocSeg;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand9;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand8;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand8;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand8;
		private AP_Invoice_Entry.dsAllocSeg dsAllocSeg1;
		private System.Data.SqlClient.SqlDataAdapter daPO;
		private AP_Invoice_Entry.dsPO dsPO1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit8;
		private System.Data.SqlClient.SqlDataAdapter daInvDetail;
		private DevExpress.XtraGrid.Columns.GridColumn colGL_ACCOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colCOMMENT1;
		private DevExpress.XtraGrid.Columns.GridColumn colAMOUNT;
		private DevExpress.XtraGrid.Columns.GridColumn colTRANS_TYPE;
		private DevExpress.XtraGrid.Columns.GridColumn colHOLD_AMT1;
		private DevExpress.XtraGrid.Columns.GridColumn colPO_ID1;
		private DevExpress.XtraGrid.Columns.GridColumn colSUB_CODE;
		private DevExpress.XtraGrid.Columns.GridColumn colGL_ACCOUNT1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit9;
		private DevExpress.XtraGrid.Columns.GridColumn colREFERENCE1;
		private System.Data.SqlClient.SqlDataAdapter daGLAccts;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand12;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand11;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand11;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand11;
		private AP_Invoice_Entry.dsGLAccts dsGLAccts1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riGLDesc;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit9;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit10;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit2;
		private ReflexEditors.RHyperLinkEdit hlRefresh;
		private ReflexEditors.RHyperLinkEdit hlQuickChk;
		private ReflexEditors.RHyperLinkEdit hlPrint;
		private ReflexEditors.RHyperLinkEdit hlDeleteInv;
		private ReflexEditors.RHyperLinkEdit hlNewInv;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit11;
		private System.Data.SqlClient.SqlDataAdapter daAllPO;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand13;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand12;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand12;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand12;
		private AP_Invoice_Entry.dsAllPO dsAllPO1;
		private AP_Invoice_Entry.dsInvDetail dsInvDetail1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit12;
		private DevExpress.XtraGrid.Columns.GridColumn colSEG_CHANGE;
		private DevExpress.XtraEditors.TextEdit txtHoldback;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand11;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand10;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand10;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand10;
		private AccountingPicker.ucAccountingPicker ucAccountingPicker1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
		private AP_Invoice_Entry.dsInvHeader dsInvHeader1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
		private DevExpress.XtraGrid.Columns.GridColumn colIS_BALANCED;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand10;
		private System.Data.SqlClient.SqlDataAdapter daHeaderSide;
		private AP_Invoice_Entry.dsHeaderSide dsHeaderSide1;
		private ReflexEditors.RHyperLinkEdit hlBalance;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer3;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel8_Container;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraBars.Docking.DockPanel dpActions;
		private DevExpress.XtraGrid.Columns.GridColumn colAP_INV_HEADER_ID;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOSelect;
		private System.Data.SqlClient.SqlDataAdapter daPOSelect;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand15;
		private AP_Invoice_Entry.dsPOSelect dsPOSelect1;
		private DevExpress.XtraGrid.Columns.GridColumn colAP_DIV;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit13;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand7;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand7;
		private System.Data.SqlClient.SqlDataAdapter daPOFSelect;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand16;
		private System.Data.SqlClient.SqlDataAdapter daPOBSelect;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand17;
		private System.Data.SqlClient.SqlDataAdapter daPOMSelect;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand18;
		private System.Data.SqlClient.SqlDataAdapter daPODSelect;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand19;
		private System.Data.SqlClient.SqlDataAdapter daPOM2Select;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand20;
		private AP_Invoice_Entry.dsPOFSelect dsPOFSelect1;
		private AP_Invoice_Entry.dsPOBSelect dsPOBSelect1;
		private AP_Invoice_Entry.dsPOMSelect dsPOMSelect1;
		private AP_Invoice_Entry.dsPODSelect dsPODSelect1;
		private AP_Invoice_Entry.dsPOM2Select dsPOM2Select1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOBSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPODSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOFSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOMSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOM2Select;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riNoPO;
		private System.Data.SqlClient.SqlDataAdapter daPOInvSelect;
		private AP_Invoice_Entry.dsPOInvSelect dsPOInvSelect1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPOInvSelect;
		private DevExpress.XtraLayout.LayoutControlItem lciHold;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand21;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand13;
		private System.Data.SqlClient.SqlDataAdapter daDetPO;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand22;
		private AP_Invoice_Entry.dsDetPO dsDetPO1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riDetPOSelect;
		private DevExpress.XtraGrid.Columns.GridColumn colPO_ID2;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riNoPOSelect;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
		private ReflexEditors.RHyperLinkEdit hlChangeSupp;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand14;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand9;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand9;
		private DevExpress.XtraGrid.Columns.GridColumn colITC;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riITC;
        private DevExpress.XtraGrid.Columns.GridColumn colpri_id;
        private DevExpress.XtraGrid.Columns.GridColumn collv1id;
        private DevExpress.XtraGrid.Columns.GridColumn collv2id;
        private DevExpress.XtraGrid.Columns.GridColumn collv3id;
        private DevExpress.XtraGrid.Columns.GridColumn collv4id;
        private DevExpress.XtraGrid.Columns.GridColumn collem_comp;
        private DevExpress.XtraGrid.Columns.GridColumn colEXPENSE_TYPE;
        private ReflexEditors.RHyperLinkEdit hlOverrideCompliance;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riRouteStatus;
        private SqlConnection TR_Conn2;
        private DevExpress.XtraGrid.Columns.GridColumn colAFE_NO;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riAFE;
        private DevExpress.XtraGrid.Columns.GridColumn colCOST_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn colTIME_TICKET;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riCustCostCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riTimeTicket;
        private DevExpress.XtraTab.XtraTabControl tcDetails;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage tpMatchPO;
        private DevExpress.XtraTab.XtraTabPage tpContractPO;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riGLDescReadOnly;
        private BindingSource bsGLAccts;
        private DevExpress.XtraGrid.Columns.GridColumn colKC_ACCRUAL_STATUS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riEmpty;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riRouteStatusPreAccrual;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riReference;
        private DevExpress.XtraGrid.Columns.GridColumn colfrom_web;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riWEB;
        private SimpleButton btnSharepoint;
        private SqlCommand sqlSelectCommand23;
        private SqlCommand sqlInsertCommand9;
        private SqlCommand sqlUpdateCommand13;
        private SqlCommand sqlDeleteCommand13;
        private SqlDataAdapter daWorkFlow;
        private dsWorkFlow dsWorkFlow1;
        private BindingSource bsWorkFlow;
        private DevExpress.XtraGrid.Columns.GridColumn colWF_STATUS;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riRouteStatusWorkFlow;
        private DevExpress.XtraGrid.Columns.GridColumn colWF_Approval_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riWorkFlow;
        private SqlConnection WEB_Conn;
        private DevExpress.XtraGrid.Columns.GridColumn colHOURS;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riHours;
        private DevExpress.XtraGrid.Columns.GridColumn colRATE;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riRate;
        private DevExpress.XtraTab.XtraTabPage tpSummContractPO;
        private DevExpress.XtraGrid.Columns.GridColumn colCOMPANY_ALIAS;
        private SqlCommand sqlSelectCommand24;
        private SqlDataAdapter daCompanies;
        private dsCompanies dsCompanies1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riCompanies;
        private BindingSource bsCompanies;
        private DevExpress.XtraGrid.Columns.GridColumn colHAS_CB;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riHAS_CB;
        private DevExpress.XtraGrid.Columns.GridColumn colIS_CB;
        private DevExpress.XtraGrid.Columns.GridColumn colCB_ID;
        private DevExpress.XtraGrid.Columns.GridColumn colCB_REF;
        private ReflexEditors.RHyperLinkEdit hlChargeBack;
        private SimpleButton btnDirectAttachemnts;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private TextEdit txtROAcctNo;
        private TextEdit txtROZip;
        private TextEdit txtROState;
        private TextEdit txtROCity;
        private TextEdit txtROAddr3;
        private TextEdit txtROAddr2;
        private TextEdit txtROAddr1;
        private TextEdit txtROName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraLayout.LayoutControlItem lciROState;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciROZip;
        private DevExpress.XtraTab.XtraTabControl xtcRemit;
        private DevExpress.XtraTab.XtraTabPage tpRemit;
        private DevExpress.XtraTab.XtraTabPage tpROSupp;
        private CheckEdit chkPaymentHold;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraGrid.Columns.GridColumn colPAYMENT_HOLD;
        private DevExpress.XtraGrid.Columns.GridColumn colACCT_PERIOD;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riYearPeriodRO;
        private DevExpress.XtraGrid.Columns.GridColumn colACCT_YEAR;
        private DevExpress.XtraGrid.Columns.GridColumn colbillable;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ceBillabled;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem lciNewInv;
        private DevExpress.XtraLayout.LayoutControlItem lciDeleteInv;
        private DevExpress.XtraLayout.LayoutControlItem lciQuickChk;
        private DevExpress.XtraLayout.LayoutControlItem lciChangeSupp;
        private DevExpress.XtraLayout.LayoutControlItem lciChargeBack;
        private DevExpress.XtraLayout.LayoutControlItem lciPrint;
        private ReflexEditors.RHyperLinkEdit hlEventHistory;
        private DevExpress.XtraLayout.LayoutControlItem lciBalance;
        private DevExpress.XtraLayout.LayoutControlItem lciRefresh;
        private DevExpress.XtraLayout.LayoutControlItem lciEventHistory;
        private DevExpress.XtraLayout.LayoutControlItem lciOverrideCompliance;
        private DevExpress.XtraLayout.EmptySpaceItem esiBottom;
        private SimpleButton btnLinkCompAttch;
        private DevExpress.XtraLayout.LayoutControlItem lciDocLink;
        private DevExpress.XtraGrid.GridControl gcPWP;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPWP;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private SqlCommand sqlSelectCommand25;
        private SqlCommand sqlInsertCommand14;
        private SqlCommand sqlUpdateCommand14;
        private SqlCommand sqlDeleteCommand14;
        private SqlDataAdapter daPWP_Status;
        private dsPWP_Status dsPWP_Status1;
        private DevExpress.XtraGrid.Columns.GridColumn colAR_PWP_STATUS_ID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riPWPStatus;
        private BindingSource bsPWP_Status;
        private DevExpress.XtraTab.XtraTabPage tpPWPLink;
        private ReflexEditors.RHyperLinkEdit hlOverridePWPStatus;
        private DevExpress.XtraLayout.LayoutControlItem lciOverridePWPStatus;
        private SqlCommand sqlSelectCommand26;
        private SqlDataAdapter daAP_PWP_GetLinks;
        private dsAP_PWP_GetLinks dsAP_PWP_GetLinks1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riPWPSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riWorkFlowRO;
        private DevExpress.XtraGrid.Columns.GridColumn colLevy;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riLevy;
        private DevExpress.XtraTab.XtraTabPage tpLevy;
        private DevExpress.XtraGrid.Columns.GridColumn colSUPP_NAME;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riManualChkNoRO;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private ReflexEditors.RHyperLinkEdit hlMultiCBEntry;
        private DevExpress.XtraLayout.LayoutControlItem lciMultiCBEntry;
        private SimpleButton btnPOAttachments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private IContainer components;
        #endregion

        public ucAP_InvoiceEntry( HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, KeyControlAccess.Validator KCA_Validator )
		{
			this.KCA_Validator = KCA_Validator;
			this.Connection = Connection;
			this.DevXMgr = DevXMgr;
			InitializeComponent();
            RunSetups();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAP_InvoiceEntry));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject17 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject18 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject19 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject20 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject21 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject22 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject23 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtInvAmt = new DevExpress.XtraEditors.TextEdit();
            this.dsInvHeader1 = new AP_Invoice_Entry.dsInvHeader();
            this.txtUndist = new DevExpress.XtraEditors.TextEdit();
            this.txtHoldback = new DevExpress.XtraEditors.TextEdit();
            this.txtRemain = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSharepoint = new DevExpress.XtraEditors.SimpleButton();
            this.lcDefaults = new DevExpress.XtraLayout.LayoutControl();
            this.btnLinkCompAttch = new DevExpress.XtraEditors.SimpleButton();
            this.chkPaymentHold = new DevExpress.XtraEditors.CheckEdit();
            this.dsHeaderSide1 = new AP_Invoice_Entry.dsHeaderSide();
            this.btnDirectAttachemnts = new DevExpress.XtraEditors.SimpleButton();
            this.ucAccountingPicker1 = new AccountingPicker.ucAccountingPicker();
            this.chkHold = new DevExpress.XtraEditors.CheckEdit();
            this.chkKCApproval = new DevExpress.XtraEditors.CheckEdit();
            this.chkKCRouting = new DevExpress.XtraEditors.CheckEdit();
            this.deHoldDue = new DevExpress.XtraEditors.DateEdit();
            this.txtHoldA = new DevExpress.XtraEditors.TextEdit();
            this.txtHoldP = new DevExpress.XtraEditors.TextEdit();
            this.txtDiscA = new DevExpress.XtraEditors.TextEdit();
            this.txtDiscP = new DevExpress.XtraEditors.TextEdit();
            this.lueTerms = new DevExpress.XtraEditors.LookUpEdit();
            this.dsTerms1 = new AP_Invoice_Entry.dsTerms();
            this.lueCurrency = new DevExpress.XtraEditors.LookUpEdit();
            this.dsCurrency1 = new AP_Invoice_Entry.dsCurrency();
            this.lueInvType = new DevExpress.XtraEditors.LookUpEdit();
            this.lueAPCntl = new DevExpress.XtraEditors.LookUpEdit();
            this.dsAPSetupGL1 = new AP_Invoice_Entry.dsAPSetupGL();
            this.deDiscDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciInvoiceType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTerms = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDiscountPct = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciHold = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDocLink = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSUPPLIER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsSupplier1 = new AP_Invoice_Entry.dsSupplier();
            this.colINV_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colTRANS_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colDUE_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDISCOUNT_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINV_AMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDISCOUNT_AMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colREFERENCE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riReference = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colOPERATOR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHOLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPaymentHold = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colGST_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsGST1 = new AP_Invoice_Entry.dsGST();
            this.colPURCH_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colGST_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colHOLD_PCT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colHOLD_AMT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colHOLD_PAY_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCURRENCY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colINVOICE_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riInvoiceType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colMANUAL_CHECK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riManualChkNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colSALES_TAX_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPST1 = new AP_Invoice_Entry.dsPST();
            this.colCOMMENT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colPO_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit11 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsAllPO1 = new AP_Invoice_Entry.dsAllPO();
            this.colSOX_ROUTING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSOX_APPROVAL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAP_SETUP_GL_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colIS_BALANCED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAP_INV_HEADER_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAP_DIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit13 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsSwapSeg1 = new AP_Invoice_Entry.dsSwapSeg();
            this.colStatus1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRouteStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colKC_ACCRUAL_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRouteStatusPreAccrual = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colfrom_web = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riWEB = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colWF_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRouteStatusWorkFlow = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colWF_Approval_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riWorkFlow = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsWorkFlow = new System.Windows.Forms.BindingSource(this.components);
            this.dsWorkFlow1 = new AP_Invoice_Entry.dsWorkFlow();
            this.colHAS_CB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riHAS_CB = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIS_CB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPAYMENT_HOLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colACCT_PERIOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riYearPeriodRO = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colACCT_YEAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLevy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riLevy = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colSUPP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsAllocSeg1 = new AP_Invoice_Entry.dsAllocSeg();
            this.repositoryItemLookUpEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPO1 = new AP_Invoice_Entry.dsPO();
            this.riEmpty = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riWorkFlowRO = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riManualChkNoRO = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpInvDefaults = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel7_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpRemit = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.xtcRemit = new DevExpress.XtraTab.XtraTabControl();
            this.tpRemit = new DevExpress.XtraTab.XtraTabPage();
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
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tpROSupp = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.txtROAcctNo = new DevExpress.XtraEditors.TextEdit();
            this.txtROZip = new DevExpress.XtraEditors.TextEdit();
            this.txtROState = new DevExpress.XtraEditors.TextEdit();
            this.txtROCity = new DevExpress.XtraEditors.TextEdit();
            this.txtROAddr3 = new DevExpress.XtraEditors.TextEdit();
            this.txtROAddr2 = new DevExpress.XtraEditors.TextEdit();
            this.txtROAddr1 = new DevExpress.XtraEditors.TextEdit();
            this.txtROName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciROState = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciROZip = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanel6 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel6_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.memoComment = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dockPanel5 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dpActions = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel8_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.hlMultiCBEntry = new ReflexEditors.RHyperLinkEdit();
            this.hlOverridePWPStatus = new ReflexEditors.RHyperLinkEdit();
            this.hlOverrideCompliance = new ReflexEditors.RHyperLinkEdit();
            this.hlEventHistory = new ReflexEditors.RHyperLinkEdit();
            this.hlNewInv = new ReflexEditors.RHyperLinkEdit();
            this.hlRefresh = new ReflexEditors.RHyperLinkEdit();
            this.hlPrint = new ReflexEditors.RHyperLinkEdit();
            this.hlBalance = new ReflexEditors.RHyperLinkEdit();
            this.hlChargeBack = new ReflexEditors.RHyperLinkEdit();
            this.hlDeleteInv = new ReflexEditors.RHyperLinkEdit();
            this.hlQuickChk = new ReflexEditors.RHyperLinkEdit();
            this.hlChangeSupp = new ReflexEditors.RHyperLinkEdit();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciNewInv = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDeleteInv = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciQuickChk = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciChangeSupp = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciChargeBack = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPrint = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBalance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciEventHistory = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOverrideCompliance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOverridePWPStatus = new DevExpress.XtraLayout.LayoutControlItem();
            this.esiBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciMultiCBEntry = new DevExpress.XtraLayout.LayoutControlItem();
            this.dnHeaderNav = new DevExpress.XtraEditors.DataNavigator();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tcDetails = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcDetail = new DevExpress.XtraGrid.GridControl();
            this.dsInvDetail1 = new AP_Invoice_Entry.dsInvDetail();
            this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGL_ACCOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCOMMENT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colAMOUNT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colTRANS_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colHOLD_AMT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit10 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPO_ID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOSelect1 = new AP_Invoice_Entry.dsPOSelect();
            this.colSUB_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGL_ACCOUNT1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riGLDesc = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsGLAccts1 = new AP_Invoice_Entry.dsGLAccts();
            this.colREFERENCE1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSEG_CHANGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit12 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colPO_ID2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riDetPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsDetPO1 = new AP_Invoice_Entry.dsDetPO();
            this.colITC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riITC = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colpri_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collv1id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collv2id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collv3id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collv4id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collem_comp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEXPENSE_TYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAFE_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riAFE = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCOST_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riCustCostCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colTIME_TICKET = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riTimeTicket = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colHOURS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riHours = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colRATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCOMPANY_ALIAS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riCompanies = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsCompanies = new System.Windows.Forms.BindingSource(this.components);
            this.dsCompanies1 = new AP_Invoice_Entry.dsCompanies();
            this.colCB_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCB_REF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbillable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ceBillabled = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAR_PWP_STATUS_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPWPStatus = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsPWP_Status = new System.Windows.Forms.BindingSource(this.components);
            this.dsPWP_Status1 = new AP_Invoice_Entry.dsPWP_Status();
            this.riPOBSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOBSelect1 = new AP_Invoice_Entry.dsPOBSelect();
            this.riPODSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPODSelect1 = new AP_Invoice_Entry.dsPODSelect();
            this.riPOFSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOFSelect1 = new AP_Invoice_Entry.dsPOFSelect();
            this.riPOMSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOMSelect1 = new AP_Invoice_Entry.dsPOMSelect();
            this.riPOM2Select = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOM2Select1 = new AP_Invoice_Entry.dsPOM2Select();
            this.riNoPO = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.riPOInvSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dsPOInvSelect1 = new AP_Invoice_Entry.dsPOInvSelect();
            this.riNoPOSelect = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riGLDescReadOnly = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsGLAccts = new System.Windows.Forms.BindingSource(this.components);
            this.tpMatchPO = new DevExpress.XtraTab.XtraTabPage();
            this.tpContractPO = new DevExpress.XtraTab.XtraTabPage();
            this.tpSummContractPO = new DevExpress.XtraTab.XtraTabPage();
            this.tpPWPLink = new DevExpress.XtraTab.XtraTabPage();
            this.gcPWP = new DevExpress.XtraGrid.GridControl();
            this.dsAP_PWP_GetLinks1 = new AP_Invoice_Entry.dsAP_PWP_GetLinks();
            this.gvPWP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riPWPSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.tpLevy = new DevExpress.XtraTab.XtraTabPage();
            this.TR_Conn = new System.Data.SqlClient.SqlConnection();
            this.daInvHeader = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand7 = new System.Data.SqlClient.SqlCommand();
            this.daAPSetupGL = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.daCurrency = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daGST = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand3 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand3 = new System.Data.SqlClient.SqlCommand();
            this.daPST = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand4 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand4 = new System.Data.SqlClient.SqlCommand();
            this.daTerms = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand5 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand5 = new System.Data.SqlClient.SqlCommand();
            this.daSupplier = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand6 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand7 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand6 = new System.Data.SqlClient.SqlCommand();
            this.daSwapSeg = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand8 = new System.Data.SqlClient.SqlCommand();
            this.daAllocSeg = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand8 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand8 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand8 = new System.Data.SqlClient.SqlCommand();
            this.daPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand10 = new System.Data.SqlClient.SqlCommand();
            this.TR_Conn2 = new System.Data.SqlClient.SqlConnection();
            this.daInvDetail = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand10 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand10 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand11 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand10 = new System.Data.SqlClient.SqlCommand();
            this.daGLAccts = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand11 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand11 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand12 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand11 = new System.Data.SqlClient.SqlCommand();
            this.daAllPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand12 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand12 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand13 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand12 = new System.Data.SqlClient.SqlCommand();
            this.daHeaderSide = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlDeleteCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand14 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand9 = new System.Data.SqlClient.SqlCommand();
            this.daPOSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand15 = new System.Data.SqlClient.SqlCommand();
            this.daPOFSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand16 = new System.Data.SqlClient.SqlCommand();
            this.daPOBSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand17 = new System.Data.SqlClient.SqlCommand();
            this.daPOMSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand18 = new System.Data.SqlClient.SqlCommand();
            this.daPODSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand19 = new System.Data.SqlClient.SqlCommand();
            this.daPOM2Select = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand20 = new System.Data.SqlClient.SqlCommand();
            this.daPOInvSelect = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlInsertCommand13 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand21 = new System.Data.SqlClient.SqlCommand();
            this.daDetPO = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand22 = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand23 = new System.Data.SqlClient.SqlCommand();
            this.WEB_Conn = new System.Data.SqlClient.SqlConnection();
            this.sqlInsertCommand9 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand13 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand13 = new System.Data.SqlClient.SqlCommand();
            this.daWorkFlow = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand24 = new System.Data.SqlClient.SqlCommand();
            this.daCompanies = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand25 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand14 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand14 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand14 = new System.Data.SqlClient.SqlCommand();
            this.daPWP_Status = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand26 = new System.Data.SqlClient.SqlCommand();
            this.daAP_PWP_GetLinks = new System.Data.SqlClient.SqlDataAdapter();
            this.btnPOAttachments = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvHeader1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUndist.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldback.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDefaults)).BeginInit();
            this.lcDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeaderSide1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKCApproval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKCRouting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTerms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerms1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCurrency1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAPCntl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAPSetupGL1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInvoiceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTerms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDiscountPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDocLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPaymentHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGST1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riInvoiceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riManualChkNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPST1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllPO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSwapSeg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatusPreAccrual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWEB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatusWorkFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsWorkFlow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWorkFlow1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHAS_CB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riYearPeriodRO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLevy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllocSeg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlowRO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riManualChkNoRO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer3.SuspendLayout();
            this.panelContainer2.SuspendLayout();
            this.dpInvDefaults.SuspendLayout();
            this.dockPanel7_Container.SuspendLayout();
            this.dpRemit.SuspendLayout();
            this.dockPanel4_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcRemit)).BeginInit();
            this.xtcRemit.SuspendLayout();
            this.tpRemit.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            this.tpROSupp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAcctNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROCity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciROState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciROZip)).BeginInit();
            this.dockPanel6.SuspendLayout();
            this.dockPanel6_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            this.dockPanel5.SuspendLayout();
            this.dpActions.SuspendLayout();
            this.dockPanel8_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hlMultiCBEntry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlOverridePWPStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlOverrideCompliance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlEventHistory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlNewInv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlRefresh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlPrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlChargeBack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlDeleteInv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlQuickChk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlChangeSupp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciQuickChk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChangeSupp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChargeBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEventHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverrideCompliance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverridePWPStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultiCBEntry)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcDetails)).BeginInit();
            this.tcDetails.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvDetail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGLDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGLAccts1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetPOSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetPO1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riITC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAFE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCustCostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTimeTicket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCompanies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompanies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCompanies1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceBillabled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPWP_Status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPWP_Status1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOBSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOBSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPODSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPODSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOFSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOFSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOMSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOMSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOM2Select)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOM2Select1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOInvSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOInvSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPOSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGLDescReadOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGLAccts)).BeginInit();
            this.tpPWPLink.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcPWP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAP_PWP_GetLinks1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPWP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.layoutControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 435);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1231, 48);
            this.panelControl2.TabIndex = 3;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtInvAmt);
            this.layoutControl1.Controls.Add(this.txtUndist);
            this.layoutControl1.Controls.Add(this.txtHoldback);
            this.layoutControl1.Controls.Add(this.txtRemain);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1227, 44);
            this.layoutControl1.TabIndex = 14;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtInvAmt
            // 
            this.txtInvAmt.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsInvHeader1, "AP_INV_HEADER.INV_AMOUNT", true));
            this.txtInvAmt.Location = new System.Drawing.Point(110, 12);
            this.txtInvAmt.Name = "txtInvAmt";
            this.txtInvAmt.Properties.Mask.EditMask = "n2";
            this.txtInvAmt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtInvAmt.Properties.Mask.PlaceHolder = '2';
            this.txtInvAmt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtInvAmt.Properties.ReadOnly = true;
            this.txtInvAmt.Size = new System.Drawing.Size(98, 20);
            this.txtInvAmt.StyleController = this.layoutControl1;
            this.txtInvAmt.TabIndex = 0;
            // 
            // dsInvHeader1
            // 
            this.dsInvHeader1.DataSetName = "dsInvHeader";
            this.dsInvHeader1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsInvHeader1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtUndist
            // 
            this.txtUndist.Location = new System.Drawing.Point(310, 12);
            this.txtUndist.Name = "txtUndist";
            this.txtUndist.Properties.Mask.EditMask = "n2";
            this.txtUndist.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtUndist.Properties.Mask.PlaceHolder = '2';
            this.txtUndist.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtUndist.Properties.ReadOnly = true;
            this.txtUndist.Size = new System.Drawing.Size(98, 20);
            this.txtUndist.StyleController = this.layoutControl1;
            this.txtUndist.TabIndex = 1;
            // 
            // txtHoldback
            // 
            this.txtHoldback.Location = new System.Drawing.Point(510, 12);
            this.txtHoldback.Name = "txtHoldback";
            this.txtHoldback.Properties.Mask.EditMask = "n2";
            this.txtHoldback.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHoldback.Properties.Mask.PlaceHolder = '2';
            this.txtHoldback.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHoldback.Properties.ReadOnly = true;
            this.txtHoldback.Size = new System.Drawing.Size(98, 20);
            this.txtHoldback.StyleController = this.layoutControl1;
            this.txtHoldback.TabIndex = 12;
            // 
            // txtRemain
            // 
            this.txtRemain.Location = new System.Drawing.Point(710, 12);
            this.txtRemain.Name = "txtRemain";
            this.txtRemain.Properties.Mask.EditMask = "n2";
            this.txtRemain.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRemain.Properties.Mask.PlaceHolder = '2';
            this.txtRemain.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRemain.Properties.ReadOnly = true;
            this.txtRemain.Size = new System.Drawing.Size(98, 20);
            this.txtRemain.StyleController = this.layoutControl1;
            this.txtRemain.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1210, 51);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtUndist;
            this.layoutControlItem2.CustomizationFormText = "Undistributed";
            this.layoutControlItem2.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 31);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Undistributed";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtInvAmt;
            this.layoutControlItem1.CustomizationFormText = "Invoice Amount";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(200, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Invoice Amount";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtHoldback;
            this.layoutControlItem3.CustomizationFormText = "Holdback";
            this.layoutControlItem3.Location = new System.Drawing.Point(400, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(200, 31);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Holdback";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(95, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtRemain;
            this.layoutControlItem4.CustomizationFormText = "Holdback Remaining";
            this.layoutControlItem4.Location = new System.Drawing.Point(600, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(200, 31);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(590, 31);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Holdback Remaining";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(95, 13);
            // 
            // btnSharepoint
            // 
            this.btnSharepoint.Image = ((System.Drawing.Image)(resources.GetObject("btnSharepoint.Image")));
            this.btnSharepoint.Location = new System.Drawing.Point(12, 378);
            this.btnSharepoint.Name = "btnSharepoint";
            this.btnSharepoint.Size = new System.Drawing.Size(158, 22);
            this.btnSharepoint.StyleController = this.lcDefaults;
            this.btnSharepoint.TabIndex = 21;
            this.btnSharepoint.Text = "SharePoint (0)";
            this.btnSharepoint.Click += new System.EventHandler(this.btnSharepoint_Click);
            // 
            // lcDefaults
            // 
            this.lcDefaults.Controls.Add(this.btnPOAttachments);
            this.lcDefaults.Controls.Add(this.btnLinkCompAttch);
            this.lcDefaults.Controls.Add(this.chkPaymentHold);
            this.lcDefaults.Controls.Add(this.btnDirectAttachemnts);
            this.lcDefaults.Controls.Add(this.ucAccountingPicker1);
            this.lcDefaults.Controls.Add(this.chkHold);
            this.lcDefaults.Controls.Add(this.chkKCApproval);
            this.lcDefaults.Controls.Add(this.chkKCRouting);
            this.lcDefaults.Controls.Add(this.btnSharepoint);
            this.lcDefaults.Controls.Add(this.deHoldDue);
            this.lcDefaults.Controls.Add(this.txtHoldA);
            this.lcDefaults.Controls.Add(this.txtHoldP);
            this.lcDefaults.Controls.Add(this.txtDiscA);
            this.lcDefaults.Controls.Add(this.txtDiscP);
            this.lcDefaults.Controls.Add(this.lueTerms);
            this.lcDefaults.Controls.Add(this.lueCurrency);
            this.lcDefaults.Controls.Add(this.lueInvType);
            this.lcDefaults.Controls.Add(this.lueAPCntl);
            this.lcDefaults.Controls.Add(this.deDiscDate);
            this.lcDefaults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcDefaults.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem19,
            this.layoutControlItem18});
            this.lcDefaults.Location = new System.Drawing.Point(0, 0);
            this.lcDefaults.Name = "lcDefaults";
            this.lcDefaults.Root = this.layoutControlGroup2;
            this.lcDefaults.Size = new System.Drawing.Size(309, 450);
            this.lcDefaults.TabIndex = 0;
            this.lcDefaults.Text = "layoutControl2";
            // 
            // btnLinkCompAttch
            // 
            this.btnLinkCompAttch.Location = new System.Drawing.Point(12, 404);
            this.btnLinkCompAttch.Name = "btnLinkCompAttch";
            this.btnLinkCompAttch.Size = new System.Drawing.Size(158, 22);
            this.btnLinkCompAttch.StyleController = this.lcDefaults;
            this.btnLinkCompAttch.TabIndex = 24;
            this.btnLinkCompAttch.Text = "Link Compliance Attachments";
            this.btnLinkCompAttch.Click += new System.EventHandler(this.btnLinkCompAttch_Click);
            // 
            // chkPaymentHold
            // 
            this.chkPaymentHold.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.PAYMENT_HOLD", true));
            this.chkPaymentHold.Location = new System.Drawing.Point(12, 355);
            this.chkPaymentHold.Name = "chkPaymentHold";
            this.chkPaymentHold.Properties.Caption = "Payment Hold";
            this.chkPaymentHold.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkPaymentHold.Properties.ValueChecked = "T";
            this.chkPaymentHold.Properties.ValueUnchecked = "F";
            this.chkPaymentHold.Size = new System.Drawing.Size(285, 19);
            this.chkPaymentHold.StyleController = this.lcDefaults;
            this.chkPaymentHold.TabIndex = 23;
            // 
            // dsHeaderSide1
            // 
            this.dsHeaderSide1.DataSetName = "dsHeaderSide";
            this.dsHeaderSide1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsHeaderSide1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnDirectAttachemnts
            // 
            this.btnDirectAttachemnts.Location = new System.Drawing.Point(174, 378);
            this.btnDirectAttachemnts.Name = "btnDirectAttachemnts";
            this.btnDirectAttachemnts.Size = new System.Drawing.Size(123, 22);
            this.btnDirectAttachemnts.StyleController = this.lcDefaults;
            this.btnDirectAttachemnts.TabIndex = 22;
            this.btnDirectAttachemnts.Text = "Direct Attachments";
            this.btnDirectAttachemnts.Click += new System.EventHandler(this.btnDirectAttachemnts_Click);
            // 
            // ucAccountingPicker1
            // 
            this.ucAccountingPicker1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ucAccountingPicker1.Appearance.Options.UseBackColor = true;
            this.ucAccountingPicker1.HasEntryDate = false;
            this.ucAccountingPicker1.Location = new System.Drawing.Point(12, 12);
            this.ucAccountingPicker1.Name = "ucAccountingPicker1";
            this.ucAccountingPicker1.SelectedPeriod = 0;
            this.ucAccountingPicker1.SelectedYear = 0;
            this.ucAccountingPicker1.Size = new System.Drawing.Size(285, 76);
            this.ucAccountingPicker1.TabIndex = 19;
            this.ucAccountingPicker1.UserName = "";
            this.ucAccountingPicker1.SelectedYearChanged += new AccountingPicker.ucAccountingPicker.SelectedYearChangedDelegate(this.ucAccountingPicker1_SelectedYearChanged);
            this.ucAccountingPicker1.SelectedPeriodChanged += new AccountingPicker.ucAccountingPicker.SelectedPeriodChangedDelegate(this.ucAccountingPicker1_SelectedPeriodChanged);
            // 
            // chkHold
            // 
            this.chkHold.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.HOLD", true));
            this.chkHold.Location = new System.Drawing.Point(12, 332);
            this.chkHold.Name = "chkHold";
            this.chkHold.Properties.Caption = "Invoice Hold";
            this.chkHold.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkHold.Properties.ValueChecked = "T";
            this.chkHold.Properties.ValueUnchecked = "F";
            this.chkHold.Size = new System.Drawing.Size(285, 19);
            this.chkHold.StyleController = this.lcDefaults;
            this.chkHold.TabIndex = 17;
            // 
            // chkKCApproval
            // 
            this.chkKCApproval.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.SOX_APPROVAL", true));
            this.chkKCApproval.Location = new System.Drawing.Point(7, 457);
            this.chkKCApproval.Name = "chkKCApproval";
            this.chkKCApproval.Properties.Caption = "Key Control Approval";
            this.chkKCApproval.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkKCApproval.Size = new System.Drawing.Size(304, 19);
            this.chkKCApproval.StyleController = this.lcDefaults;
            this.chkKCApproval.TabIndex = 16;
            this.chkKCApproval.Visible = false;
            // 
            // chkKCRouting
            // 
            this.chkKCRouting.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.SOX_ROUTING", true));
            this.chkKCRouting.Location = new System.Drawing.Point(7, 427);
            this.chkKCRouting.Name = "chkKCRouting";
            this.chkKCRouting.Properties.Caption = "Key Control Routing";
            this.chkKCRouting.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkKCRouting.Size = new System.Drawing.Size(304, 19);
            this.chkKCRouting.StyleController = this.lcDefaults;
            this.chkKCRouting.TabIndex = 15;
            this.chkKCRouting.Visible = false;
            // 
            // deHoldDue
            // 
            this.deHoldDue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.HOLD_PAY_DATE", true));
            this.deHoldDue.EditValue = null;
            this.deHoldDue.Location = new System.Drawing.Point(82, 308);
            this.deHoldDue.Name = "deHoldDue";
            this.deHoldDue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deHoldDue.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deHoldDue.Size = new System.Drawing.Size(215, 20);
            this.deHoldDue.StyleController = this.lcDefaults;
            this.deHoldDue.TabIndex = 14;
            // 
            // txtHoldA
            // 
            this.txtHoldA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.HOLD_AMT", true));
            this.txtHoldA.Location = new System.Drawing.Point(82, 284);
            this.txtHoldA.Name = "txtHoldA";
            this.txtHoldA.Properties.Mask.EditMask = "n2";
            this.txtHoldA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHoldA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHoldA.Size = new System.Drawing.Size(215, 20);
            this.txtHoldA.StyleController = this.lcDefaults;
            this.txtHoldA.TabIndex = 13;
            this.txtHoldA.EditValueChanged += new System.EventHandler(this.txtHoldA_EditValueChanged);
            // 
            // txtHoldP
            // 
            this.txtHoldP.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.HOLD_PCT", true));
            this.txtHoldP.Location = new System.Drawing.Point(82, 260);
            this.txtHoldP.Name = "txtHoldP";
            this.txtHoldP.Properties.Mask.EditMask = "n2";
            this.txtHoldP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHoldP.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtHoldP.Size = new System.Drawing.Size(215, 20);
            this.txtHoldP.StyleController = this.lcDefaults;
            this.txtHoldP.TabIndex = 12;
            this.txtHoldP.EditValueChanged += new System.EventHandler(this.txtHoldP_EditValueChanged);
            // 
            // txtDiscA
            // 
            this.txtDiscA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.DISCOUNT_AMOUNT", true));
            this.txtDiscA.Location = new System.Drawing.Point(82, 212);
            this.txtDiscA.Name = "txtDiscA";
            this.txtDiscA.Properties.Mask.EditMask = "n2";
            this.txtDiscA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDiscA.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDiscA.Size = new System.Drawing.Size(215, 20);
            this.txtDiscA.StyleController = this.lcDefaults;
            this.txtDiscA.TabIndex = 11;
            // 
            // txtDiscP
            // 
            this.txtDiscP.Location = new System.Drawing.Point(82, 188);
            this.txtDiscP.Name = "txtDiscP";
            this.txtDiscP.Properties.Mask.EditMask = "n2";
            this.txtDiscP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDiscP.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDiscP.Properties.ReadOnly = true;
            this.txtDiscP.Size = new System.Drawing.Size(215, 20);
            this.txtDiscP.StyleController = this.lcDefaults;
            this.txtDiscP.TabIndex = 10;
            // 
            // lueTerms
            // 
            this.lueTerms.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.TERMS_ID", true));
            this.lueTerms.Location = new System.Drawing.Point(82, 164);
            this.lueTerms.Name = "lueTerms";
            this.lueTerms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject14, "", null, null, false)});
            this.lueTerms.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TERMS_ID", "TERMS_ID", 73, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TERM_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueTerms.Properties.DataSource = this.dsTerms1.TERMS;
            this.lueTerms.Properties.DisplayMember = "DESCRIPTION";
            this.lueTerms.Properties.NullText = "";
            this.lueTerms.Properties.PopupWidth = 250;
            this.lueTerms.Properties.ReadOnly = true;
            this.lueTerms.Properties.ValueMember = "TERMS_ID";
            this.lueTerms.Size = new System.Drawing.Size(215, 20);
            this.lueTerms.StyleController = this.lcDefaults;
            this.lueTerms.TabIndex = 8;
            // 
            // dsTerms1
            // 
            this.dsTerms1.DataSetName = "dsTerms";
            this.dsTerms1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsTerms1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueCurrency
            // 
            this.lueCurrency.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.CURRENCY_ID", true));
            this.lueCurrency.Location = new System.Drawing.Point(82, 140);
            this.lueCurrency.Name = "lueCurrency";
            this.lueCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, false)});
            this.lueCurrency.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_ID", "CURRENCY_ID", 90, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueCurrency.Properties.DataSource = this.dsCurrency1.CURRENCY;
            this.lueCurrency.Properties.DisplayMember = "DESCRIPTION";
            this.lueCurrency.Properties.NullText = "";
            this.lueCurrency.Properties.ReadOnly = true;
            this.lueCurrency.Properties.ValueMember = "CURRENCY_ID";
            this.lueCurrency.Size = new System.Drawing.Size(215, 20);
            this.lueCurrency.StyleController = this.lcDefaults;
            this.lueCurrency.TabIndex = 7;
            // 
            // dsCurrency1
            // 
            this.dsCurrency1.DataSetName = "dsCurrency";
            this.dsCurrency1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsCurrency1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lueInvType
            // 
            this.lueInvType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.INVOICE_TYPE", true));
            this.lueInvType.Location = new System.Drawing.Point(82, 116);
            this.lueInvType.Name = "lueInvType";
            this.lueInvType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, false)});
            this.lueInvType.Properties.NullText = "";
            this.lueInvType.Properties.ReadOnly = true;
            this.lueInvType.Size = new System.Drawing.Size(215, 20);
            this.lueInvType.StyleController = this.lcDefaults;
            this.lueInvType.TabIndex = 6;
            this.lueInvType.EditValueChanged += new System.EventHandler(this.lueInvType_EditValueChanged);
            // 
            // lueAPCntl
            // 
            this.lueAPCntl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.AP_SETUP_GL_ID", true));
            this.lueAPCntl.Location = new System.Drawing.Point(82, 92);
            this.lueAPCntl.Name = "lueAPCntl";
            this.lueAPCntl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAPCntl.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AP_SETUP_GL_ID", "AP_SETUP_GL_ID", 104, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "AP Control", 74, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ACCOUNT", "GL Account", 73, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.lueAPCntl.Properties.DataSource = this.dsAPSetupGL1.AP_SETUP_GL;
            this.lueAPCntl.Properties.DisplayMember = "DESCRIPTION";
            this.lueAPCntl.Properties.NullText = "";
            this.lueAPCntl.Properties.ValueMember = "AP_SETUP_GL_ID";
            this.lueAPCntl.Size = new System.Drawing.Size(215, 20);
            this.lueAPCntl.StyleController = this.lcDefaults;
            this.lueAPCntl.TabIndex = 5;
            this.lueAPCntl.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.lueAPCntl_EditValueChanging);
            // 
            // dsAPSetupGL1
            // 
            this.dsAPSetupGL1.DataSetName = "dsAPSetupGL";
            this.dsAPSetupGL1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsAPSetupGL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // deDiscDate
            // 
            this.deDiscDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.DISCOUNT_DATE", true));
            this.deDiscDate.EditValue = null;
            this.deDiscDate.Location = new System.Drawing.Point(82, 236);
            this.deDiscDate.Name = "deDiscDate";
            this.deDiscDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDiscDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDiscDate.Size = new System.Drawing.Size(215, 20);
            this.deDiscDate.StyleController = this.lcDefaults;
            this.deDiscDate.TabIndex = 7;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.chkKCApproval;
            this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 450);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.ShowInCustomizationForm = false;
            this.layoutControlItem19.Size = new System.Drawing.Size(315, 141);
            this.layoutControlItem19.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.chkKCRouting;
            this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 420);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.ShowInCustomizationForm = false;
            this.layoutControlItem18.Size = new System.Drawing.Size(315, 171);
            this.layoutControlItem18.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.lciInvoiceType,
            this.lciCurrency,
            this.lciTerms,
            this.lciDiscountPct,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17,
            this.layoutControlItem31,
            this.lciHold,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem34,
            this.lciDocLink,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(309, 450);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lueAPCntl;
            this.layoutControlItem7.CustomizationFormText = "AP Control";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem7.Text = "AP Control";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciInvoiceType
            // 
            this.lciInvoiceType.Control = this.lueInvType;
            this.lciInvoiceType.CustomizationFormText = "Invoice Type";
            this.lciInvoiceType.Location = new System.Drawing.Point(0, 104);
            this.lciInvoiceType.Name = "lciInvoiceType";
            this.lciInvoiceType.Size = new System.Drawing.Size(289, 24);
            this.lciInvoiceType.Text = "Invoice Type";
            this.lciInvoiceType.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciInvoiceType.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciCurrency
            // 
            this.lciCurrency.Control = this.lueCurrency;
            this.lciCurrency.CustomizationFormText = "Currency";
            this.lciCurrency.Location = new System.Drawing.Point(0, 128);
            this.lciCurrency.Name = "lciCurrency";
            this.lciCurrency.Size = new System.Drawing.Size(289, 24);
            this.lciCurrency.Text = "Currency";
            this.lciCurrency.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciCurrency.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciTerms
            // 
            this.lciTerms.Control = this.lueTerms;
            this.lciTerms.CustomizationFormText = "Terms";
            this.lciTerms.Location = new System.Drawing.Point(0, 152);
            this.lciTerms.Name = "lciTerms";
            this.lciTerms.Size = new System.Drawing.Size(289, 24);
            this.lciTerms.Text = "Terms";
            this.lciTerms.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciTerms.TextSize = new System.Drawing.Size(67, 13);
            // 
            // lciDiscountPct
            // 
            this.lciDiscountPct.Control = this.txtDiscP;
            this.lciDiscountPct.CustomizationFormText = "Discount %";
            this.lciDiscountPct.Location = new System.Drawing.Point(0, 176);
            this.lciDiscountPct.Name = "lciDiscountPct";
            this.lciDiscountPct.Size = new System.Drawing.Size(289, 24);
            this.lciDiscountPct.Text = "Discount %";
            this.lciDiscountPct.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciDiscountPct.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtDiscA;
            this.layoutControlItem13.CustomizationFormText = "Discount $";
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 200);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem13.Text = "Discount $";
            this.layoutControlItem13.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.deDiscDate;
            this.layoutControlItem14.CustomizationFormText = "Discount Date";
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 224);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem14.Text = "Discount Date";
            this.layoutControlItem14.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.txtHoldP;
            this.layoutControlItem15.CustomizationFormText = "Holdback %";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 248);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem15.Text = "Holdback %";
            this.layoutControlItem15.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtHoldA;
            this.layoutControlItem16.CustomizationFormText = "Holdback $";
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 272);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem16.Text = "Holdback $";
            this.layoutControlItem16.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.deHoldDue;
            this.layoutControlItem17.CustomizationFormText = "Holdback Due";
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 296);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(289, 24);
            this.layoutControlItem17.Text = "Holdback Due";
            this.layoutControlItem17.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem17.TextSize = new System.Drawing.Size(67, 13);
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.ucAccountingPicker1;
            this.layoutControlItem31.CustomizationFormText = "layoutControlItem31";
            this.layoutControlItem31.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem31.MaxSize = new System.Drawing.Size(0, 80);
            this.layoutControlItem31.MinSize = new System.Drawing.Size(111, 70);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(289, 80);
            this.layoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem31.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem31.TextVisible = false;
            // 
            // lciHold
            // 
            this.lciHold.Control = this.chkHold;
            this.lciHold.CustomizationFormText = "Invoice Hold";
            this.lciHold.Location = new System.Drawing.Point(0, 320);
            this.lciHold.Name = "lciHold";
            this.lciHold.Size = new System.Drawing.Size(289, 23);
            this.lciHold.Text = "lciInvoiceHold";
            this.lciHold.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciHold.TextSize = new System.Drawing.Size(0, 0);
            this.lciHold.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSharepoint;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 366);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(162, 26);
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnDirectAttachemnts;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(162, 366);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.chkPaymentHold;
            this.layoutControlItem34.CustomizationFormText = "Payment Hold";
            this.layoutControlItem34.Location = new System.Drawing.Point(0, 343);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(289, 23);
            this.layoutControlItem34.Text = "Payment Hold";
            this.layoutControlItem34.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem34.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem34.TextVisible = false;
            // 
            // lciDocLink
            // 
            this.lciDocLink.Control = this.btnLinkCompAttch;
            this.lciDocLink.CustomizationFormText = "layoutControlItem8";
            this.lciDocLink.Location = new System.Drawing.Point(0, 392);
            this.lciDocLink.Name = "lciDocLink";
            this.lciDocLink.Size = new System.Drawing.Size(162, 38);
            this.lciDocLink.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciDocLink.TextSize = new System.Drawing.Size(0, 0);
            this.lciDocLink.TextVisible = false;
            // 
            // gcHeader
            // 
            this.gcHeader.DataMember = "AP_INV_HEADER";
            this.gcHeader.DataSource = this.dsInvHeader1;
            this.gcHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHeader.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcHeader_EmbeddedNavigator_ButtonClick);
            this.gcHeader.Location = new System.Drawing.Point(0, 0);
            this.gcHeader.MainView = this.gvHeader;
            this.gcHeader.Name = "gcHeader";
            this.gcHeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this.repositoryItemTextEdit4,
            this.repositoryItemTextEdit5,
            this.repositoryItemTextEdit6,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemLookUpEdit3,
            this.repositoryItemLookUpEdit4,
            this.riPaymentHold,
            this.repositoryItemLookUpEdit5,
            this.riManualChkNo,
            this.riInvoiceType,
            this.repositoryItemTextEdit8,
            this.repositoryItemLookUpEdit7,
            this.repositoryItemLookUpEdit8,
            this.repositoryItemLookUpEdit11,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemLookUpEdit13,
            this.repositoryItemDateEdit1,
            this.riRouteStatus,
            this.riEmpty,
            this.riRouteStatusPreAccrual,
            this.riReference,
            this.riWEB,
            this.riRouteStatusWorkFlow,
            this.riWorkFlow,
            this.riHAS_CB,
            this.riYearPeriodRO,
            this.riWorkFlowRO,
            this.riLevy,
            this.riManualChkNoRO});
            this.gcHeader.Size = new System.Drawing.Size(1231, 435);
            this.gcHeader.TabIndex = 4;
            this.gcHeader.UseEmbeddedNavigator = true;
            this.gcHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSUPPLIER,
            this.colINV_NO,
            this.colTRANS_DATE,
            this.colINV_DATE,
            this.colDUE_DATE,
            this.colDISCOUNT_DATE,
            this.colINV_AMOUNT,
            this.colDISCOUNT_AMOUNT,
            this.colREFERENCE,
            this.colOPERATOR,
            this.colHOLD,
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
            this.colIS_BALANCED,
            this.colAP_INV_HEADER_ID,
            this.colAP_DIV,
            this.colStatus1,
            this.colKC_ACCRUAL_STATUS,
            this.colfrom_web,
            this.colWF_STATUS,
            this.colWF_Approval_ID,
            this.colHAS_CB,
            this.colIS_CB,
            this.colPAYMENT_HOLD,
            this.colACCT_PERIOD,
            this.colACCT_YEAR,
            this.colLevy,
            this.colSUPP_NAME,
            this.colSupplierName});
            this.gvHeader.CustomizationFormBounds = new System.Drawing.Rectangle(1072, 58, 208, 413);
            this.gvHeader.GridControl = this.gcHeader;
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.ShowFooter = true;
            this.gvHeader.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvHeader_CustomRowCellEditForEditing);
            this.gvHeader.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvHeader_InitNewRow);
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvHeader_FocusedRowChanged);
            this.gvHeader.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvHeader_FocusedColumnChanged);
            this.gvHeader.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvHeader_CellValueChanged);
            this.gvHeader.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvHeader_InvalidRowException);
            this.gvHeader.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvHeader_ValidateRow);
            this.gvHeader.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvHeader_RowUpdated);
            this.gvHeader.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvHeader_CustomUnboundColumnData);
            // 
            // colSUPPLIER
            // 
            this.colSUPPLIER.Caption = "Supplier";
            this.colSUPPLIER.ColumnEdit = this.repositoryItemLookUpEdit5;
            this.colSUPPLIER.FieldName = "SUPPLIER";
            this.colSUPPLIER.Name = "colSUPPLIER";
            this.colSUPPLIER.Visible = true;
            this.colSUPPLIER.VisibleIndex = 2;
            this.colSUPPLIER.Width = 79;
            // 
            // repositoryItemLookUpEdit5
            // 
            this.repositoryItemLookUpEdit5.AutoHeight = false;
            this.repositoryItemLookUpEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit5.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIER", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit5.DataSource = this.dsSupplier1.SUPPLIER_MASTER;
            this.repositoryItemLookUpEdit5.DisplayMember = "SUPPLIER";
            this.repositoryItemLookUpEdit5.Name = "repositoryItemLookUpEdit5";
            this.repositoryItemLookUpEdit5.NullText = "";
            this.repositoryItemLookUpEdit5.PopupWidth = 250;
            this.repositoryItemLookUpEdit5.ValueMember = "SUPPLIER";
            this.repositoryItemLookUpEdit5.EditValueChanged += new System.EventHandler(this.repositoryItemLookUpEdit5_EditValueChanged);
            this.repositoryItemLookUpEdit5.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemLookUpEdit5_EditValueChanging);
            // 
            // dsSupplier1
            // 
            this.dsSupplier1.DataSetName = "dsSupplier";
            this.dsSupplier1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsSupplier1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colINV_NO
            // 
            this.colINV_NO.Caption = "Invoice Number";
            this.colINV_NO.ColumnEdit = this.repositoryItemTextEdit8;
            this.colINV_NO.FieldName = "INV_NO";
            this.colINV_NO.Name = "colINV_NO";
            this.colINV_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colINV_NO.Visible = true;
            this.colINV_NO.VisibleIndex = 5;
            this.colINV_NO.Width = 97;
            // 
            // repositoryItemTextEdit8
            // 
            this.repositoryItemTextEdit8.AutoHeight = false;
            this.repositoryItemTextEdit8.MaxLength = 15;
            this.repositoryItemTextEdit8.Name = "repositoryItemTextEdit8";
            // 
            // colTRANS_DATE
            // 
            this.colTRANS_DATE.Caption = "Entry Date";
            this.colTRANS_DATE.FieldName = "TRANS_DATE";
            this.colTRANS_DATE.Name = "colTRANS_DATE";
            this.colTRANS_DATE.OptionsColumn.AllowEdit = false;
            this.colTRANS_DATE.Visible = true;
            this.colTRANS_DATE.VisibleIndex = 0;
            this.colTRANS_DATE.Width = 74;
            // 
            // colINV_DATE
            // 
            this.colINV_DATE.Caption = "Invoice Date";
            this.colINV_DATE.ColumnEdit = this.repositoryItemDateEdit1;
            this.colINV_DATE.FieldName = "INV_DATE";
            this.colINV_DATE.Name = "colINV_DATE";
            this.colINV_DATE.Visible = true;
            this.colINV_DATE.VisibleIndex = 7;
            this.colINV_DATE.Width = 83;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // colDUE_DATE
            // 
            this.colDUE_DATE.Caption = "Due Date";
            this.colDUE_DATE.FieldName = "DUE_DATE";
            this.colDUE_DATE.Name = "colDUE_DATE";
            this.colDUE_DATE.Visible = true;
            this.colDUE_DATE.VisibleIndex = 8;
            this.colDUE_DATE.Width = 67;
            // 
            // colDISCOUNT_DATE
            // 
            this.colDISCOUNT_DATE.Caption = "Discount Date";
            this.colDISCOUNT_DATE.FieldName = "DISCOUNT_DATE";
            this.colDISCOUNT_DATE.Name = "colDISCOUNT_DATE";
            this.colDISCOUNT_DATE.Width = 79;
            // 
            // colINV_AMOUNT
            // 
            this.colINV_AMOUNT.Caption = "Invoice Amount";
            this.colINV_AMOUNT.ColumnEdit = this.repositoryItemTextEdit1;
            this.colINV_AMOUNT.FieldName = "INV_AMOUNT";
            this.colINV_AMOUNT.Name = "colINV_AMOUNT";
            this.colINV_AMOUNT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "INV_AMOUNT", "{0:n}")});
            this.colINV_AMOUNT.Visible = true;
            this.colINV_AMOUNT.VisibleIndex = 11;
            this.colINV_AMOUNT.Width = 97;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "n2";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit1_EditValueChanged);
            this.repositoryItemTextEdit1.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemTextEdit_EditValueChanging);
            // 
            // colDISCOUNT_AMOUNT
            // 
            this.colDISCOUNT_AMOUNT.Caption = "Discount";
            this.colDISCOUNT_AMOUNT.ColumnEdit = this.repositoryItemTextEdit2;
            this.colDISCOUNT_AMOUNT.FieldName = "DISCOUNT_AMOUNT";
            this.colDISCOUNT_AMOUNT.Name = "colDISCOUNT_AMOUNT";
            this.colDISCOUNT_AMOUNT.Width = 53;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Mask.EditMask = "n2";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit2.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // colREFERENCE
            // 
            this.colREFERENCE.Caption = "Reference";
            this.colREFERENCE.ColumnEdit = this.riReference;
            this.colREFERENCE.FieldName = "REFERENCE";
            this.colREFERENCE.Name = "colREFERENCE";
            this.colREFERENCE.Width = 62;
            // 
            // riReference
            // 
            this.riReference.AutoHeight = false;
            this.riReference.MaxLength = 25;
            this.riReference.Name = "riReference";
            // 
            // colOPERATOR
            // 
            this.colOPERATOR.Caption = "Entered By";
            this.colOPERATOR.FieldName = "OPERATOR";
            this.colOPERATOR.Name = "colOPERATOR";
            this.colOPERATOR.Width = 65;
            // 
            // colHOLD
            // 
            this.colHOLD.Caption = "Invoice Hold";
            this.colHOLD.ColumnEdit = this.riPaymentHold;
            this.colHOLD.FieldName = "HOLD";
            this.colHOLD.Name = "colHOLD";
            this.colHOLD.Width = 71;
            // 
            // riPaymentHold
            // 
            this.riPaymentHold.AutoHeight = false;
            this.riPaymentHold.Name = "riPaymentHold";
            this.riPaymentHold.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riPaymentHold.ValueChecked = "T";
            this.riPaymentHold.ValueUnchecked = "F";
            // 
            // colGST_CODE
            // 
            this.colGST_CODE.Caption = "GST";
            this.colGST_CODE.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colGST_CODE.FieldName = "GST_CODE";
            this.colGST_CODE.Name = "colGST_CODE";
            this.colGST_CODE.Visible = true;
            this.colGST_CODE.VisibleIndex = 9;
            this.colGST_CODE.Width = 41;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit3.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GST_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GST_DESC", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GST_PCT", "Percent", 50, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.repositoryItemLookUpEdit3.DataSource = this.dsGST1.GST_CODES;
            this.repositoryItemLookUpEdit3.DisplayMember = "GST_DESC";
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.NullText = "";
            this.repositoryItemLookUpEdit3.PopupWidth = 250;
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
            this.colPURCH_AMT.ColumnEdit = this.repositoryItemTextEdit3;
            this.colPURCH_AMT.FieldName = "PURCH_AMT";
            this.colPURCH_AMT.Name = "colPURCH_AMT";
            this.colPURCH_AMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PURCH_AMT", "{0:n}")});
            this.colPURCH_AMT.Visible = true;
            this.colPURCH_AMT.VisibleIndex = 12;
            this.colPURCH_AMT.Width = 106;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Mask.EditMask = "n2";
            this.repositoryItemTextEdit3.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit3.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            this.repositoryItemTextEdit3.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit3_EditValueChanged);
            this.repositoryItemTextEdit3.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemTextEdit_EditValueChanging);
            // 
            // colGST_AMT
            // 
            this.colGST_AMT.Caption = "GST Amount";
            this.colGST_AMT.ColumnEdit = this.repositoryItemTextEdit4;
            this.colGST_AMT.FieldName = "GST_AMT";
            this.colGST_AMT.Name = "colGST_AMT";
            this.colGST_AMT.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GST_AMT", "{0:n}")});
            this.colGST_AMT.Visible = true;
            this.colGST_AMT.VisibleIndex = 13;
            this.colGST_AMT.Width = 81;
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.Mask.EditMask = "n2";
            this.repositoryItemTextEdit4.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit4.Mask.PlaceHolder = '-';
            this.repositoryItemTextEdit4.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            this.repositoryItemTextEdit4.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit4_EditValueChanged);
            // 
            // colHOLD_PCT
            // 
            this.colHOLD_PCT.Caption = "Holdback %";
            this.colHOLD_PCT.ColumnEdit = this.repositoryItemTextEdit5;
            this.colHOLD_PCT.FieldName = "HOLD_PCT";
            this.colHOLD_PCT.Name = "colHOLD_PCT";
            this.colHOLD_PCT.Width = 69;
            // 
            // repositoryItemTextEdit5
            // 
            this.repositoryItemTextEdit5.AutoHeight = false;
            this.repositoryItemTextEdit5.Mask.EditMask = "n2";
            this.repositoryItemTextEdit5.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit5.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit5.Name = "repositoryItemTextEdit5";
            this.repositoryItemTextEdit5.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit5_EditValueChanged);
            this.repositoryItemTextEdit5.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemTextEdit5_EditValueChanging);
            // 
            // colHOLD_AMT
            // 
            this.colHOLD_AMT.Caption = "Holdback $";
            this.colHOLD_AMT.ColumnEdit = this.repositoryItemTextEdit6;
            this.colHOLD_AMT.FieldName = "HOLD_AMT";
            this.colHOLD_AMT.Name = "colHOLD_AMT";
            this.colHOLD_AMT.Width = 64;
            // 
            // repositoryItemTextEdit6
            // 
            this.repositoryItemTextEdit6.AutoHeight = false;
            this.repositoryItemTextEdit6.Mask.EditMask = "n2";
            this.repositoryItemTextEdit6.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit6.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit6.Name = "repositoryItemTextEdit6";
            this.repositoryItemTextEdit6.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit6_EditValueChanged);
            // 
            // colHOLD_PAY_DATE
            // 
            this.colHOLD_PAY_DATE.Caption = "Holdback Due";
            this.colHOLD_PAY_DATE.FieldName = "HOLD_PAY_DATE";
            this.colHOLD_PAY_DATE.Name = "colHOLD_PAY_DATE";
            this.colHOLD_PAY_DATE.Width = 77;
            // 
            // colCURRENCY_ID
            // 
            this.colCURRENCY_ID.Caption = "Currency";
            this.colCURRENCY_ID.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.colCURRENCY_ID.FieldName = "CURRENCY_ID";
            this.colCURRENCY_ID.Name = "colCURRENCY_ID";
            this.colCURRENCY_ID.Width = 56;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_ID", "CURRENCY_ID", 90, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CURRENCY_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit2.DataSource = this.dsCurrency1.CURRENCY;
            this.repositoryItemLookUpEdit2.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.ValueMember = "CURRENCY_ID";
            // 
            // colINVOICE_TYPE
            // 
            this.colINVOICE_TYPE.Caption = "Invoice Type";
            this.colINVOICE_TYPE.ColumnEdit = this.riInvoiceType;
            this.colINVOICE_TYPE.FieldName = "INVOICE_TYPE";
            this.colINVOICE_TYPE.Name = "colINVOICE_TYPE";
            this.colINVOICE_TYPE.Visible = true;
            this.colINVOICE_TYPE.VisibleIndex = 1;
            this.colINVOICE_TYPE.Width = 84;
            // 
            // riInvoiceType
            // 
            this.riInvoiceType.AutoHeight = false;
            this.riInvoiceType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riInvoiceType.Name = "riInvoiceType";
            this.riInvoiceType.NullText = "";
            this.riInvoiceType.EditValueChanged += new System.EventHandler(this.repositoryItemLookUpEdit6_EditValueChanged);
            this.riInvoiceType.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemLookUpEdit6_EditValueChanging);
            // 
            // colMANUAL_CHECK
            // 
            this.colMANUAL_CHECK.Caption = "Manual Check #";
            this.colMANUAL_CHECK.ColumnEdit = this.riManualChkNo;
            this.colMANUAL_CHECK.FieldName = "MANUAL_CHECK";
            this.colMANUAL_CHECK.Name = "colMANUAL_CHECK";
            this.colMANUAL_CHECK.Width = 89;
            // 
            // riManualChkNo
            // 
            this.riManualChkNo.AutoHeight = false;
            this.riManualChkNo.Mask.EditMask = "g0";
            this.riManualChkNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riManualChkNo.Mask.UseMaskAsDisplayFormat = true;
            this.riManualChkNo.Name = "riManualChkNo";
            this.riManualChkNo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.riManualChkNo_EditValueChanging);
            // 
            // colSALES_TAX_ID
            // 
            this.colSALES_TAX_ID.Caption = "PST";
            this.colSALES_TAX_ID.ColumnEdit = this.repositoryItemLookUpEdit4;
            this.colSALES_TAX_ID.FieldName = "SALES_TAX_ID";
            this.colSALES_TAX_ID.Name = "colSALES_TAX_ID";
            this.colSALES_TAX_ID.Visible = true;
            this.colSALES_TAX_ID.VisibleIndex = 10;
            this.colSALES_TAX_ID.Width = 40;
            // 
            // repositoryItemLookUpEdit4
            // 
            this.repositoryItemLookUpEdit4.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemLookUpEdit4.AutoHeight = false;
            this.repositoryItemLookUpEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit4.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SALES_TAX_ID", "SALES_TAX_ID", 91, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SALES_TAX_CODE", "Code", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Description", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SALES_TAX", "Percent", 50, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.repositoryItemLookUpEdit4.DataSource = this.dsPST1.SALES_TAXES;
            this.repositoryItemLookUpEdit4.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit4.Name = "repositoryItemLookUpEdit4";
            this.repositoryItemLookUpEdit4.NullText = "";
            this.repositoryItemLookUpEdit4.PopupWidth = 250;
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
            this.colCOMMENT.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colCOMMENT.FieldName = "COMMENT";
            this.colCOMMENT.Name = "colCOMMENT";
            this.colCOMMENT.Width = 57;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // colPO_ID
            // 
            this.colPO_ID.Caption = "PO #";
            this.colPO_ID.ColumnEdit = this.repositoryItemLookUpEdit11;
            this.colPO_ID.FieldName = "PO_ID";
            this.colPO_ID.Name = "colPO_ID";
            this.colPO_ID.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.colPO_ID.Visible = true;
            this.colPO_ID.VisibleIndex = 6;
            this.colPO_ID.Width = 47;
            // 
            // repositoryItemLookUpEdit11
            // 
            this.repositoryItemLookUpEdit11.AutoHeight = false;
            this.repositoryItemLookUpEdit11.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "x", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, false)});
            this.repositoryItemLookUpEdit11.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_ID", "PO_ID", 50, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO", "PO #", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ORDER_DATE", "PO Date", 125, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit11.DataSource = this.dsAllPO1.PO_HEADER;
            this.repositoryItemLookUpEdit11.DisplayMember = "PO";
            this.repositoryItemLookUpEdit11.Name = "repositoryItemLookUpEdit11";
            this.repositoryItemLookUpEdit11.NullText = "";
            this.repositoryItemLookUpEdit11.PopupWidth = 200;
            this.repositoryItemLookUpEdit11.ValueMember = "PO_ID";
            this.repositoryItemLookUpEdit11.EditValueChanged += new System.EventHandler(this.repositoryItemLookUpEdit11_EditValueChanged);
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
            this.colSOX_ROUTING.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colSOX_ROUTING.FieldName = "SOX_ROUTING";
            this.colSOX_ROUTING.Name = "colSOX_ROUTING";
            this.colSOX_ROUTING.Width = 108;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = "T";
            this.repositoryItemCheckEdit2.ValueUnchecked = "F";
            // 
            // colSOX_APPROVAL
            // 
            this.colSOX_APPROVAL.Caption = "Key Control Approval";
            this.colSOX_APPROVAL.FieldName = "SOX_APPROVAL";
            this.colSOX_APPROVAL.Name = "colSOX_APPROVAL";
            this.colSOX_APPROVAL.Width = 114;
            // 
            // colAP_SETUP_GL_ID
            // 
            this.colAP_SETUP_GL_ID.Caption = "AP Control";
            this.colAP_SETUP_GL_ID.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colAP_SETUP_GL_ID.FieldName = "AP_SETUP_GL_ID";
            this.colAP_SETUP_GL_ID.Name = "colAP_SETUP_GL_ID";
            this.colAP_SETUP_GL_ID.Width = 63;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AP_SETUP_GL_ID", "AP_SETUP_GL_ID", 104, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "AP Control", 74, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ACCOUNT", "GL Account", 73, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit1.DataSource = this.dsAPSetupGL1.AP_SETUP_GL;
            this.repositoryItemLookUpEdit1.DisplayMember = "DESCRIPTION";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.ValueMember = "AP_SETUP_GL_ID";
            // 
            // colIS_BALANCED
            // 
            this.colIS_BALANCED.Caption = "Balanced";
            this.colIS_BALANCED.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colIS_BALANCED.FieldName = "IS_BALANCED";
            this.colIS_BALANCED.Name = "colIS_BALANCED";
            this.colIS_BALANCED.OptionsColumn.AllowEdit = false;
            this.colIS_BALANCED.Visible = true;
            this.colIS_BALANCED.VisibleIndex = 15;
            this.colIS_BALANCED.Width = 65;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            this.repositoryItemCheckEdit3.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit3.ValueChecked = "T";
            this.repositoryItemCheckEdit3.ValueUnchecked = "F";
            // 
            // colAP_INV_HEADER_ID
            // 
            this.colAP_INV_HEADER_ID.Caption = "AP_INV_HEADER_ID";
            this.colAP_INV_HEADER_ID.FieldName = "AP_INV_HEADER_ID";
            this.colAP_INV_HEADER_ID.Name = "colAP_INV_HEADER_ID";
            this.colAP_INV_HEADER_ID.OptionsColumn.ShowInCustomizationForm = false;
            this.colAP_INV_HEADER_ID.Width = 111;
            // 
            // colAP_DIV
            // 
            this.colAP_DIV.Caption = "AP_DIV";
            this.colAP_DIV.ColumnEdit = this.repositoryItemLookUpEdit13;
            this.colAP_DIV.FieldName = "AP_DIV";
            this.colAP_DIV.Name = "colAP_DIV";
            this.colAP_DIV.Visible = true;
            this.colAP_DIV.VisibleIndex = 4;
            this.colAP_DIV.Width = 58;
            // 
            // repositoryItemLookUpEdit13
            // 
            this.repositoryItemLookUpEdit13.AutoHeight = false;
            this.repositoryItemLookUpEdit13.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit13.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Code", "Code", 44, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description", 59, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit13.DataSource = this.dsSwapSeg1.GL_ACCOUNTS;
            this.repositoryItemLookUpEdit13.DisplayMember = "Description";
            this.repositoryItemLookUpEdit13.Name = "repositoryItemLookUpEdit13";
            this.repositoryItemLookUpEdit13.NullText = "";
            this.repositoryItemLookUpEdit13.PopupWidth = 250;
            this.repositoryItemLookUpEdit13.ValueMember = "Code";
            // 
            // dsSwapSeg1
            // 
            this.dsSwapSeg1.DataSetName = "dsSwapSeg";
            this.dsSwapSeg1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsSwapSeg1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colStatus1
            // 
            this.colStatus1.Caption = "Contract PO Routing Status";
            this.colStatus1.ColumnEdit = this.riRouteStatus;
            this.colStatus1.FieldName = "KC_CONTRACTPO_STATUS";
            this.colStatus1.Name = "colStatus1";
            this.colStatus1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colStatus1.Visible = true;
            this.colStatus1.VisibleIndex = 16;
            this.colStatus1.Width = 155;
            // 
            // riRouteStatus
            // 
            this.riRouteStatus.AutoHeight = false;
            this.riRouteStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject15, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riRouteStatus.Name = "riRouteStatus";
            this.riRouteStatus.NullText = "";
            this.riRouteStatus.ReadOnly = true;
            this.riRouteStatus.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.riRouteStatus_QueryPopUp);
            this.riRouteStatus.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riRouteStatus_ButtonClick);
            // 
            // colKC_ACCRUAL_STATUS
            // 
            this.colKC_ACCRUAL_STATUS.Caption = "Subcontractor Pre-Accrual Status";
            this.colKC_ACCRUAL_STATUS.ColumnEdit = this.riRouteStatusPreAccrual;
            this.colKC_ACCRUAL_STATUS.FieldName = "KC_ACCRUAL_STATUS";
            this.colKC_ACCRUAL_STATUS.Name = "colKC_ACCRUAL_STATUS";
            this.colKC_ACCRUAL_STATUS.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colKC_ACCRUAL_STATUS.Visible = true;
            this.colKC_ACCRUAL_STATUS.VisibleIndex = 17;
            this.colKC_ACCRUAL_STATUS.Width = 182;
            // 
            // riRouteStatusPreAccrual
            // 
            this.riRouteStatusPreAccrual.AutoHeight = false;
            this.riRouteStatusPreAccrual.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject16, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riRouteStatusPreAccrual.Name = "riRouteStatusPreAccrual";
            this.riRouteStatusPreAccrual.NullText = "";
            this.riRouteStatusPreAccrual.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.riRouteStatus2_QueryPopUp);
            this.riRouteStatusPreAccrual.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riRouteStatusPreAccrual_ButtonClick);
            // 
            // colfrom_web
            // 
            this.colfrom_web.Caption = "From Web";
            this.colfrom_web.ColumnEdit = this.riWEB;
            this.colfrom_web.FieldName = "from_web";
            this.colfrom_web.Name = "colfrom_web";
            this.colfrom_web.OptionsColumn.AllowEdit = false;
            this.colfrom_web.Visible = true;
            this.colfrom_web.VisibleIndex = 20;
            this.colfrom_web.Width = 71;
            // 
            // riWEB
            // 
            this.riWEB.AutoHeight = false;
            this.riWEB.Name = "riWEB";
            this.riWEB.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riWEB.ValueChecked = "T";
            this.riWEB.ValueUnchecked = "F";
            // 
            // colWF_STATUS
            // 
            this.colWF_STATUS.Caption = "Work Flow Status";
            this.colWF_STATUS.ColumnEdit = this.riRouteStatusWorkFlow;
            this.colWF_STATUS.FieldName = "WF_STATUS";
            this.colWF_STATUS.Name = "colWF_STATUS";
            this.colWF_STATUS.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colWF_STATUS.Visible = true;
            this.colWF_STATUS.VisibleIndex = 19;
            this.colWF_STATUS.Width = 106;
            // 
            // riRouteStatusWorkFlow
            // 
            this.riRouteStatusWorkFlow.AutoHeight = false;
            this.riRouteStatusWorkFlow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject17, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riRouteStatusWorkFlow.Name = "riRouteStatusWorkFlow";
            this.riRouteStatusWorkFlow.NullText = "";
            this.riRouteStatusWorkFlow.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.riRouteStatusWorkFlow_QueryPopUp);
            this.riRouteStatusWorkFlow.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riRouteStatusWorkFlow_ButtonClick);
            // 
            // colWF_Approval_ID
            // 
            this.colWF_Approval_ID.Caption = "Work Flow Routing";
            this.colWF_Approval_ID.ColumnEdit = this.riWorkFlow;
            this.colWF_Approval_ID.FieldName = "WF_Approval_ID";
            this.colWF_Approval_ID.Name = "colWF_Approval_ID";
            this.colWF_Approval_ID.Visible = true;
            this.colWF_Approval_ID.VisibleIndex = 18;
            this.colWF_Approval_ID.Width = 112;
            // 
            // riWorkFlow
            // 
            this.riWorkFlow.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.riWorkFlow.AutoHeight = false;
            this.riWorkFlow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.riWorkFlow.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Approval_ID", "Approval_ID", 83, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Work_Flow", "Work Flow", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ApprovalType", "Approval Type", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riWorkFlow.DataSource = this.bsWorkFlow;
            this.riWorkFlow.DisplayMember = "Work_Flow";
            this.riWorkFlow.Name = "riWorkFlow";
            this.riWorkFlow.NullText = "";
            this.riWorkFlow.PopupWidth = 250;
            this.riWorkFlow.ValueMember = "Approval_ID";
            this.riWorkFlow.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riWorkFlow_ButtonClick);
            this.riWorkFlow.EditValueChanged += new System.EventHandler(this.riWorkFlow_EditValueChanged);
            // 
            // bsWorkFlow
            // 
            this.bsWorkFlow.DataMember = "WS_Approval_WorkFlow";
            this.bsWorkFlow.DataSource = this.dsWorkFlow1;
            // 
            // dsWorkFlow1
            // 
            this.dsWorkFlow1.DataSetName = "dsWorkFlow";
            this.dsWorkFlow1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colHAS_CB
            // 
            this.colHAS_CB.Caption = "Has CB";
            this.colHAS_CB.ColumnEdit = this.riHAS_CB;
            this.colHAS_CB.FieldName = "HAS_CB";
            this.colHAS_CB.Name = "colHAS_CB";
            this.colHAS_CB.OptionsColumn.AllowEdit = false;
            this.colHAS_CB.OptionsColumn.ShowInCustomizationForm = false;
            this.colHAS_CB.Width = 46;
            // 
            // riHAS_CB
            // 
            this.riHAS_CB.AutoHeight = false;
            this.riHAS_CB.Name = "riHAS_CB";
            this.riHAS_CB.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riHAS_CB.ReadOnly = true;
            this.riHAS_CB.ValueChecked = "T";
            this.riHAS_CB.ValueUnchecked = "F";
            // 
            // colIS_CB
            // 
            this.colIS_CB.Caption = "Is CB";
            this.colIS_CB.ColumnEdit = this.riHAS_CB;
            this.colIS_CB.FieldName = "IS_CB";
            this.colIS_CB.Name = "colIS_CB";
            this.colIS_CB.OptionsColumn.AllowEdit = false;
            this.colIS_CB.OptionsColumn.ShowInCustomizationForm = false;
            this.colIS_CB.Width = 37;
            // 
            // colPAYMENT_HOLD
            // 
            this.colPAYMENT_HOLD.Caption = "Payment Hold";
            this.colPAYMENT_HOLD.ColumnEdit = this.riPaymentHold;
            this.colPAYMENT_HOLD.FieldName = "PAYMENT_HOLD";
            this.colPAYMENT_HOLD.Name = "colPAYMENT_HOLD";
            this.colPAYMENT_HOLD.Width = 78;
            // 
            // colACCT_PERIOD
            // 
            this.colACCT_PERIOD.Caption = "Period";
            this.colACCT_PERIOD.ColumnEdit = this.riYearPeriodRO;
            this.colACCT_PERIOD.FieldName = "ACCT_PERIOD";
            this.colACCT_PERIOD.Name = "colACCT_PERIOD";
            this.colACCT_PERIOD.OptionsColumn.AllowEdit = false;
            this.colACCT_PERIOD.Width = 42;
            // 
            // riYearPeriodRO
            // 
            this.riYearPeriodRO.AutoHeight = false;
            this.riYearPeriodRO.Name = "riYearPeriodRO";
            this.riYearPeriodRO.ReadOnly = true;
            // 
            // colACCT_YEAR
            // 
            this.colACCT_YEAR.Caption = "Year";
            this.colACCT_YEAR.ColumnEdit = this.riYearPeriodRO;
            this.colACCT_YEAR.FieldName = "ACCT_YEAR";
            this.colACCT_YEAR.Name = "colACCT_YEAR";
            this.colACCT_YEAR.OptionsColumn.AllowEdit = false;
            this.colACCT_YEAR.Width = 34;
            // 
            // colLevy
            // 
            this.colLevy.ColumnEdit = this.riLevy;
            this.colLevy.FieldName = "Levy";
            this.colLevy.Name = "colLevy";
            this.colLevy.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colLevy.Visible = true;
            this.colLevy.VisibleIndex = 14;
            this.colLevy.Width = 98;
            // 
            // riLevy
            // 
            this.riLevy.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.riLevy.AutoHeight = false;
            this.riLevy.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.riLevy.Name = "riLevy";
            this.riLevy.NullText = "";
            this.riLevy.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.riLevy_QueryPopUp);
            this.riLevy.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riLevy_ButtonClick);
            // 
            // colSUPP_NAME
            // 
            this.colSUPP_NAME.Caption = "Remit To";
            this.colSUPP_NAME.FieldName = "SUPP_NAME";
            this.colSUPP_NAME.Name = "colSUPP_NAME";
            this.colSUPP_NAME.OptionsColumn.AllowEdit = false;
            // 
            // colSupplierName
            // 
            this.colSupplierName.Caption = "Name";
            this.colSupplierName.FieldName = "SupplierName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colSupplierName.UnboundExpression = "[SUPPLIER]";
            this.colSupplierName.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colSupplierName.Visible = true;
            this.colSupplierName.VisibleIndex = 3;
            this.colSupplierName.Width = 210;
            // 
            // repositoryItemLookUpEdit7
            // 
            this.repositoryItemLookUpEdit7.AutoHeight = false;
            this.repositoryItemLookUpEdit7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit7.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ALLOC_ID", "GL_ALLOC_ID", 86, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ALLOC_CODE", "Code", 90, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SEGMENT_NAME", "Description", 86, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit7.DataSource = this.dsAllocSeg1.GL_ALLOC;
            this.repositoryItemLookUpEdit7.DisplayMember = "GL_ALLOC_CODE";
            this.repositoryItemLookUpEdit7.Name = "repositoryItemLookUpEdit7";
            this.repositoryItemLookUpEdit7.NullText = "";
            this.repositoryItemLookUpEdit7.ValueMember = "GL_ALLOC_ID";
            // 
            // dsAllocSeg1
            // 
            this.dsAllocSeg1.DataSetName = "dsAllocSeg";
            this.dsAllocSeg1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsAllocSeg1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // repositoryItemLookUpEdit8
            // 
            this.repositoryItemLookUpEdit8.AutoHeight = false;
            this.repositoryItemLookUpEdit8.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "x", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject18, "", null, null, false)});
            this.repositoryItemLookUpEdit8.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_ID", "PO_ID", 50, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO", "PO #", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ORDER_DATE", "PO Date", 125, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit8.DataSource = this.dsPO1.PO_HEADER;
            this.repositoryItemLookUpEdit8.DisplayMember = "PO";
            this.repositoryItemLookUpEdit8.Name = "repositoryItemLookUpEdit8";
            this.repositoryItemLookUpEdit8.NullText = "";
            this.repositoryItemLookUpEdit8.PopupWidth = 200;
            this.repositoryItemLookUpEdit8.ValueMember = "PO_ID";
            this.repositoryItemLookUpEdit8.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemLookUpEdit8_Closed);
            this.repositoryItemLookUpEdit8.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemLookUpEdit8_ButtonClick);
            this.repositoryItemLookUpEdit8.EditValueChanged += new System.EventHandler(this.repositoryItemLookUpEdit8_EditValueChanged);
            this.repositoryItemLookUpEdit8.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.repositoryItemLookUpEdit8_EditValueChanging);
            // 
            // dsPO1
            // 
            this.dsPO1.DataSetName = "dsPO";
            this.dsPO1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riEmpty
            // 
            this.riEmpty.AutoHeight = false;
            this.riEmpty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject19, "", null, null, false)});
            this.riEmpty.Name = "riEmpty";
            this.riEmpty.NullText = "";
            this.riEmpty.ReadOnly = true;
            // 
            // riWorkFlowRO
            // 
            this.riWorkFlowRO.AutoHeight = false;
            this.riWorkFlowRO.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riWorkFlowRO.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Approval_ID", "Approval_ID", 83, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Work_Flow", "Work Flow", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ApprovalType", "Approval Type", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riWorkFlowRO.DataSource = this.bsWorkFlow;
            this.riWorkFlowRO.DisplayMember = "Work_Flow";
            this.riWorkFlowRO.Name = "riWorkFlowRO";
            this.riWorkFlowRO.NullText = "";
            this.riWorkFlowRO.PopupWidth = 250;
            this.riWorkFlowRO.ReadOnly = true;
            this.riWorkFlowRO.ValueMember = "Approval_ID";
            // 
            // riManualChkNoRO
            // 
            this.riManualChkNoRO.AutoHeight = false;
            this.riManualChkNoRO.Mask.EditMask = "g0";
            this.riManualChkNoRO.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riManualChkNoRO.Mask.UseMaskAsDisplayFormat = true;
            this.riManualChkNoRO.Name = "riManualChkNoRO";
            this.riManualChkNoRO.ReadOnly = true;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer3,
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager1.Load += new System.EventHandler(this.dockManager1_Load);
            // 
            // panelContainer3
            // 
            this.panelContainer3.Controls.Add(this.panelContainer2);
            this.panelContainer3.Controls.Add(this.dpActions);
            this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer3.ID = new System.Guid("f0917187-cc49-4b8d-bb4e-44130321fe68");
            this.panelContainer3.Location = new System.Drawing.Point(1231, 0);
            this.panelContainer3.Name = "panelContainer3";
            this.panelContainer3.OriginalSize = new System.Drawing.Size(345, 779);
            this.panelContainer3.Size = new System.Drawing.Size(345, 779);
            this.panelContainer3.Text = "panelContainer3";
            // 
            // panelContainer2
            // 
            this.panelContainer2.ActiveChild = this.dpInvDefaults;
            this.panelContainer2.Controls.Add(this.dpInvDefaults);
            this.panelContainer2.Controls.Add(this.dpRemit);
            this.panelContainer2.Controls.Add(this.dockPanel6);
            this.panelContainer2.Controls.Add(this.dockPanel5);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer2.ID = new System.Guid("53353d73-9698-436a-aa71-b5de746b83de");
            this.panelContainer2.Location = new System.Drawing.Point(0, 0);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.OriginalSize = new System.Drawing.Size(0, 0);
            this.panelContainer2.Size = new System.Drawing.Size(345, 478);
            this.panelContainer2.Tabbed = true;
            this.panelContainer2.TabsPosition = DevExpress.XtraBars.Docking.TabsPosition.Right;
            this.panelContainer2.Text = "panelContainer2";
            // 
            // dpInvDefaults
            // 
            this.dpInvDefaults.Controls.Add(this.dockPanel7_Container);
            this.dpInvDefaults.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpInvDefaults.FloatVertical = true;
            this.dpInvDefaults.ID = new System.Guid("0a04e654-4555-4fd1-bf36-3353049f8cd6");
            this.dpInvDefaults.Location = new System.Drawing.Point(5, 23);
            this.dpInvDefaults.Name = "dpInvDefaults";
            this.dpInvDefaults.OriginalSize = new System.Drawing.Size(317, 450);
            this.dpInvDefaults.Size = new System.Drawing.Size(309, 450);
            this.dpInvDefaults.Text = "Invoice Defaults";
            // 
            // dockPanel7_Container
            // 
            this.dockPanel7_Container.Controls.Add(this.lcDefaults);
            this.dockPanel7_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel7_Container.Name = "dockPanel7_Container";
            this.dockPanel7_Container.Size = new System.Drawing.Size(309, 450);
            this.dockPanel7_Container.TabIndex = 0;
            // 
            // dpRemit
            // 
            this.dpRemit.Controls.Add(this.dockPanel4_Container);
            this.dpRemit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpRemit.FloatVertical = true;
            this.dpRemit.ID = new System.Guid("e99558ce-e361-4108-ac7b-290fb52a35ef");
            this.dpRemit.Location = new System.Drawing.Point(5, 23);
            this.dpRemit.Name = "dpRemit";
            this.dpRemit.OriginalSize = new System.Drawing.Size(317, 450);
            this.dpRemit.Size = new System.Drawing.Size(309, 450);
            this.dpRemit.Text = "Remit To";
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Controls.Add(this.xtcRemit);
            this.dockPanel4_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(309, 450);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // xtcRemit
            // 
            this.xtcRemit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcRemit.Location = new System.Drawing.Point(0, 0);
            this.xtcRemit.Name = "xtcRemit";
            this.xtcRemit.SelectedTabPage = this.tpRemit;
            this.xtcRemit.Size = new System.Drawing.Size(309, 450);
            this.xtcRemit.TabIndex = 1;
            this.xtcRemit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpRemit,
            this.tpROSupp});
            // 
            // tpRemit
            // 
            this.tpRemit.Controls.Add(this.layoutControl3);
            this.tpRemit.Name = "tpRemit";
            this.tpRemit.Size = new System.Drawing.Size(303, 422);
            this.tpRemit.Text = "Editable Remit";
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
            this.layoutControl3.Size = new System.Drawing.Size(303, 422);
            this.layoutControl3.TabIndex = 0;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // txtRAcctNo
            // 
            this.txtRAcctNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.SUPP_ACCOUNT", true));
            this.txtRAcctNo.Location = new System.Drawing.Point(65, 156);
            this.txtRAcctNo.Name = "txtRAcctNo";
            this.txtRAcctNo.Size = new System.Drawing.Size(226, 20);
            this.txtRAcctNo.StyleController = this.layoutControl3;
            this.txtRAcctNo.TabIndex = 11;
            // 
            // txtRZip
            // 
            this.txtRZip.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITZIP", true));
            this.txtRZip.Location = new System.Drawing.Point(207, 132);
            this.txtRZip.Name = "txtRZip";
            this.txtRZip.Size = new System.Drawing.Size(84, 20);
            this.txtRZip.StyleController = this.layoutControl3;
            this.txtRZip.TabIndex = 10;
            // 
            // txtRState
            // 
            this.txtRState.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITSTATE", true));
            this.txtRState.Location = new System.Drawing.Point(65, 132);
            this.txtRState.Name = "txtRState";
            this.txtRState.Size = new System.Drawing.Size(85, 20);
            this.txtRState.StyleController = this.layoutControl3;
            this.txtRState.TabIndex = 9;
            // 
            // txtRCity
            // 
            this.txtRCity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITCITY", true));
            this.txtRCity.Location = new System.Drawing.Point(65, 108);
            this.txtRCity.Name = "txtRCity";
            this.txtRCity.Size = new System.Drawing.Size(226, 20);
            this.txtRCity.StyleController = this.layoutControl3;
            this.txtRCity.TabIndex = 8;
            // 
            // txtRAddr3
            // 
            this.txtRAddr3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITADD3", true));
            this.txtRAddr3.Location = new System.Drawing.Point(65, 84);
            this.txtRAddr3.Name = "txtRAddr3";
            this.txtRAddr3.Size = new System.Drawing.Size(226, 20);
            this.txtRAddr3.StyleController = this.layoutControl3;
            this.txtRAddr3.TabIndex = 7;
            // 
            // txtRAddr2
            // 
            this.txtRAddr2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITADD2", true));
            this.txtRAddr2.Location = new System.Drawing.Point(65, 60);
            this.txtRAddr2.Name = "txtRAddr2";
            this.txtRAddr2.Size = new System.Drawing.Size(226, 20);
            this.txtRAddr2.StyleController = this.layoutControl3;
            this.txtRAddr2.TabIndex = 6;
            // 
            // txtRAddr1
            // 
            this.txtRAddr1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.REMITADD1", true));
            this.txtRAddr1.Location = new System.Drawing.Point(65, 36);
            this.txtRAddr1.Name = "txtRAddr1";
            this.txtRAddr1.Size = new System.Drawing.Size(226, 20);
            this.txtRAddr1.StyleController = this.layoutControl3;
            this.txtRAddr1.TabIndex = 5;
            // 
            // txtRName
            // 
            this.txtRName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.SUPP_NAME", true));
            this.txtRName.Location = new System.Drawing.Point(65, 12);
            this.txtRName.Name = "txtRName";
            this.txtRName.Size = new System.Drawing.Size(226, 20);
            this.txtRName.StyleController = this.layoutControl3;
            this.txtRName.TabIndex = 4;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem25,
            this.layoutControlItem26,
            this.layoutControlItem28,
            this.emptySpaceItem2,
            this.layoutControlItem27});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(303, 422);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.txtRName;
            this.layoutControlItem11.CustomizationFormText = "Name";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem11.Text = "Name";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.txtRAddr1;
            this.layoutControlItem22.CustomizationFormText = "Address 1";
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem22.Text = "Address 1";
            this.layoutControlItem22.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem22.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.txtRAddr2;
            this.layoutControlItem23.CustomizationFormText = "Address 2";
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem23.Text = "Address 2";
            this.layoutControlItem23.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.txtRAddr3;
            this.layoutControlItem24.CustomizationFormText = "Address 3";
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem24.Text = "Address 3";
            this.layoutControlItem24.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem24.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.txtRCity;
            this.layoutControlItem25.CustomizationFormText = "City";
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem25.Text = "City";
            this.layoutControlItem25.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem25.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.txtRState;
            this.layoutControlItem26.CustomizationFormText = "State";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(142, 24);
            this.layoutControlItem26.Text = "State";
            this.layoutControlItem26.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem26.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.txtRAcctNo;
            this.layoutControlItem28.CustomizationFormText = "Account #";
            this.layoutControlItem28.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem28.Text = "Account #";
            this.layoutControlItem28.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem28.TextSize = new System.Drawing.Size(50, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 168);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(283, 234);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.txtRZip;
            this.layoutControlItem27.CustomizationFormText = "Zip";
            this.layoutControlItem27.Location = new System.Drawing.Point(142, 120);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(141, 24);
            this.layoutControlItem27.Text = "Zip";
            this.layoutControlItem27.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem27.TextSize = new System.Drawing.Size(50, 13);
            // 
            // tpROSupp
            // 
            this.tpROSupp.Controls.Add(this.layoutControl5);
            this.tpROSupp.Name = "tpROSupp";
            this.tpROSupp.Size = new System.Drawing.Size(303, 422);
            this.tpROSupp.Text = "RO Supplier Info";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.txtROAcctNo);
            this.layoutControl5.Controls.Add(this.txtROZip);
            this.layoutControl5.Controls.Add(this.txtROState);
            this.layoutControl5.Controls.Add(this.txtROCity);
            this.layoutControl5.Controls.Add(this.txtROAddr3);
            this.layoutControl5.Controls.Add(this.txtROAddr2);
            this.layoutControl5.Controls.Add(this.txtROAddr1);
            this.layoutControl5.Controls.Add(this.txtROName);
            this.layoutControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl5.Enabled = false;
            this.layoutControl5.Location = new System.Drawing.Point(0, 0);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(303, 422);
            this.layoutControl5.TabIndex = 7;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // txtROAcctNo
            // 
            this.txtROAcctNo.Location = new System.Drawing.Point(65, 156);
            this.txtROAcctNo.Name = "txtROAcctNo";
            this.txtROAcctNo.Properties.ReadOnly = true;
            this.txtROAcctNo.Size = new System.Drawing.Size(226, 20);
            this.txtROAcctNo.StyleController = this.layoutControl5;
            this.txtROAcctNo.TabIndex = 11;
            // 
            // txtROZip
            // 
            this.txtROZip.Location = new System.Drawing.Point(207, 132);
            this.txtROZip.Name = "txtROZip";
            this.txtROZip.Properties.ReadOnly = true;
            this.txtROZip.Size = new System.Drawing.Size(84, 20);
            this.txtROZip.StyleController = this.layoutControl5;
            this.txtROZip.TabIndex = 10;
            // 
            // txtROState
            // 
            this.txtROState.Location = new System.Drawing.Point(65, 132);
            this.txtROState.Name = "txtROState";
            this.txtROState.Properties.ReadOnly = true;
            this.txtROState.Size = new System.Drawing.Size(85, 20);
            this.txtROState.StyleController = this.layoutControl5;
            this.txtROState.TabIndex = 9;
            // 
            // txtROCity
            // 
            this.txtROCity.Location = new System.Drawing.Point(65, 108);
            this.txtROCity.Name = "txtROCity";
            this.txtROCity.Properties.ReadOnly = true;
            this.txtROCity.Size = new System.Drawing.Size(226, 20);
            this.txtROCity.StyleController = this.layoutControl5;
            this.txtROCity.TabIndex = 8;
            // 
            // txtROAddr3
            // 
            this.txtROAddr3.Location = new System.Drawing.Point(65, 84);
            this.txtROAddr3.Name = "txtROAddr3";
            this.txtROAddr3.Properties.ReadOnly = true;
            this.txtROAddr3.Size = new System.Drawing.Size(226, 20);
            this.txtROAddr3.StyleController = this.layoutControl5;
            this.txtROAddr3.TabIndex = 7;
            // 
            // txtROAddr2
            // 
            this.txtROAddr2.Location = new System.Drawing.Point(65, 60);
            this.txtROAddr2.Name = "txtROAddr2";
            this.txtROAddr2.Properties.ReadOnly = true;
            this.txtROAddr2.Size = new System.Drawing.Size(226, 20);
            this.txtROAddr2.StyleController = this.layoutControl5;
            this.txtROAddr2.TabIndex = 6;
            // 
            // txtROAddr1
            // 
            this.txtROAddr1.Location = new System.Drawing.Point(65, 36);
            this.txtROAddr1.Name = "txtROAddr1";
            this.txtROAddr1.Properties.ReadOnly = true;
            this.txtROAddr1.Size = new System.Drawing.Size(226, 20);
            this.txtROAddr1.StyleController = this.layoutControl5;
            this.txtROAddr1.TabIndex = 5;
            // 
            // txtROName
            // 
            this.txtROName.Location = new System.Drawing.Point(65, 12);
            this.txtROName.Name = "txtROName";
            this.txtROName.Properties.ReadOnly = true;
            this.txtROName.Size = new System.Drawing.Size(226, 20);
            this.txtROName.StyleController = this.layoutControl5;
            this.txtROName.TabIndex = 4;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup3";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem20,
            this.layoutControlItem21,
            this.layoutControlItem30,
            this.layoutControlItem32,
            this.layoutControlItem33,
            this.lciROState,
            this.layoutControlItem35,
            this.emptySpaceItem1,
            this.lciROZip});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup3";
            this.layoutControlGroup5.Size = new System.Drawing.Size(303, 422);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.txtROName;
            this.layoutControlItem20.CustomizationFormText = "Name";
            this.layoutControlItem20.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem20.Name = "layoutControlItem11";
            this.layoutControlItem20.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem20.Text = "Name";
            this.layoutControlItem20.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem20.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.txtROAddr1;
            this.layoutControlItem21.CustomizationFormText = "Address 1";
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem21.Name = "layoutControlItem22";
            this.layoutControlItem21.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem21.Text = "Address 1";
            this.layoutControlItem21.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem21.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.txtROAddr2;
            this.layoutControlItem30.CustomizationFormText = "Address 2";
            this.layoutControlItem30.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem30.Name = "layoutControlItem23";
            this.layoutControlItem30.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem30.Text = "Address 2";
            this.layoutControlItem30.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem30.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.txtROAddr3;
            this.layoutControlItem32.CustomizationFormText = "Address 3";
            this.layoutControlItem32.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem32.Name = "layoutControlItem24";
            this.layoutControlItem32.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem32.Text = "Address 3";
            this.layoutControlItem32.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem32.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.txtROCity;
            this.layoutControlItem33.CustomizationFormText = "City";
            this.layoutControlItem33.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem33.Name = "layoutControlItem25";
            this.layoutControlItem33.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem33.Text = "City";
            this.layoutControlItem33.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem33.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lciROState
            // 
            this.lciROState.AllowHide = false;
            this.lciROState.Control = this.txtROState;
            this.lciROState.CustomizationFormText = "State";
            this.lciROState.Location = new System.Drawing.Point(0, 120);
            this.lciROState.Name = "lciROState";
            this.lciROState.Size = new System.Drawing.Size(142, 24);
            this.lciROState.Text = "State";
            this.lciROState.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciROState.TextSize = new System.Drawing.Size(50, 13);
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.txtROAcctNo;
            this.layoutControlItem35.CustomizationFormText = "Account #";
            this.layoutControlItem35.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItem35.Name = "layoutControlItem28";
            this.layoutControlItem35.Size = new System.Drawing.Size(283, 24);
            this.layoutControlItem35.Text = "Account #";
            this.layoutControlItem35.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem35.TextSize = new System.Drawing.Size(50, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 168);
            this.emptySpaceItem1.Name = "emptySpaceItem2";
            this.emptySpaceItem1.Size = new System.Drawing.Size(283, 234);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciROZip
            // 
            this.lciROZip.Control = this.txtROZip;
            this.lciROZip.CustomizationFormText = "Zip";
            this.lciROZip.Location = new System.Drawing.Point(142, 120);
            this.lciROZip.Name = "lciROZip";
            this.lciROZip.Size = new System.Drawing.Size(141, 24);
            this.lciROZip.Text = "Zip";
            this.lciROZip.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciROZip.TextSize = new System.Drawing.Size(50, 13);
            // 
            // dockPanel6
            // 
            this.dockPanel6.Controls.Add(this.dockPanel6_Container);
            this.dockPanel6.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel6.ID = new System.Guid("42c141c4-ce25-4f58-a101-0f8cdbd85922");
            this.dockPanel6.Location = new System.Drawing.Point(5, 23);
            this.dockPanel6.Name = "dockPanel6";
            this.dockPanel6.OriginalSize = new System.Drawing.Size(317, 450);
            this.dockPanel6.Size = new System.Drawing.Size(309, 450);
            this.dockPanel6.Text = "Comment";
            // 
            // dockPanel6_Container
            // 
            this.dockPanel6_Container.Controls.Add(this.layoutControl4);
            this.dockPanel6_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel6_Container.Name = "dockPanel6_Container";
            this.dockPanel6_Container.Size = new System.Drawing.Size(309, 450);
            this.dockPanel6_Container.TabIndex = 0;
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.memoComment);
            this.layoutControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl4.Location = new System.Drawing.Point(0, 0);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(309, 450);
            this.layoutControl4.TabIndex = 0;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // memoComment
            // 
            this.memoComment.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsHeaderSide1, "AP_INV_HEADER.COMMENT", true));
            this.memoComment.Location = new System.Drawing.Point(12, 28);
            this.memoComment.Name = "memoComment";
            this.memoComment.Size = new System.Drawing.Size(285, 410);
            this.memoComment.StyleController = this.layoutControl4;
            this.memoComment.TabIndex = 4;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "layoutControlGroup4";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem29});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(309, 450);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.memoComment;
            this.layoutControlItem29.CustomizationFormText = "Comment";
            this.layoutControlItem29.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(289, 430);
            this.layoutControlItem29.Text = "Comment";
            this.layoutControlItem29.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem29.TextSize = new System.Drawing.Size(45, 13);
            // 
            // dockPanel5
            // 
            this.dockPanel5.Controls.Add(this.dockPanel5_Container);
            this.dockPanel5.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel5.ID = new System.Guid("37bef9ed-67f4-40b8-bd3c-5718ceab570f");
            this.dockPanel5.Location = new System.Drawing.Point(5, 23);
            this.dockPanel5.Name = "dockPanel5";
            this.dockPanel5.OriginalSize = new System.Drawing.Size(317, 450);
            this.dockPanel5.Size = new System.Drawing.Size(309, 450);
            this.dockPanel5.Text = "GST Exception";
            // 
            // dockPanel5_Container
            // 
            this.dockPanel5_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel5_Container.Name = "dockPanel5_Container";
            this.dockPanel5_Container.Size = new System.Drawing.Size(309, 450);
            this.dockPanel5_Container.TabIndex = 0;
            // 
            // dpActions
            // 
            this.dpActions.Controls.Add(this.dockPanel8_Container);
            this.dpActions.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpActions.ID = new System.Guid("10cea9d7-824b-4700-a8be-c96b8916cfac");
            this.dpActions.Location = new System.Drawing.Point(0, 478);
            this.dpActions.Name = "dpActions";
            this.dpActions.OriginalSize = new System.Drawing.Size(0, 0);
            this.dpActions.Size = new System.Drawing.Size(345, 301);
            this.dpActions.Text = "Actions";
            // 
            // dockPanel8_Container
            // 
            this.dockPanel8_Container.Controls.Add(this.layoutControl6);
            this.dockPanel8_Container.Controls.Add(this.dnHeaderNav);
            this.dockPanel8_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel8_Container.Name = "dockPanel8_Container";
            this.dockPanel8_Container.Size = new System.Drawing.Size(336, 274);
            this.dockPanel8_Container.TabIndex = 0;
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.hlMultiCBEntry);
            this.layoutControl6.Controls.Add(this.hlOverridePWPStatus);
            this.layoutControl6.Controls.Add(this.hlOverrideCompliance);
            this.layoutControl6.Controls.Add(this.hlEventHistory);
            this.layoutControl6.Controls.Add(this.hlNewInv);
            this.layoutControl6.Controls.Add(this.hlRefresh);
            this.layoutControl6.Controls.Add(this.hlPrint);
            this.layoutControl6.Controls.Add(this.hlBalance);
            this.layoutControl6.Controls.Add(this.hlChargeBack);
            this.layoutControl6.Controls.Add(this.hlDeleteInv);
            this.layoutControl6.Controls.Add(this.hlQuickChk);
            this.layoutControl6.Controls.Add(this.hlChangeSupp);
            this.layoutControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl6.Location = new System.Drawing.Point(0, 0);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.Root = this.layoutControlGroup6;
            this.layoutControl6.Size = new System.Drawing.Size(336, 255);
            this.layoutControl6.TabIndex = 5;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // hlMultiCBEntry
            // 
            this.hlMultiCBEntry.EditValue = "Multi Chargeback Entry";
            this.hlMultiCBEntry.Location = new System.Drawing.Point(12, 179);
            this.hlMultiCBEntry.Name = "hlMultiCBEntry";
            this.hlMultiCBEntry.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlMultiCBEntry.Properties.Appearance.Options.UseBackColor = true;
            this.hlMultiCBEntry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlMultiCBEntry.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlMultiCBEntry.Properties.Image")));
            this.hlMultiCBEntry.RESG_ImageType = ReflexImgSrc.Image.ImageType.MultiplePages;
            this.hlMultiCBEntry.Size = new System.Drawing.Size(154, 30);
            this.hlMultiCBEntry.StyleController = this.layoutControl6;
            this.hlMultiCBEntry.TabIndex = 19;
            this.hlMultiCBEntry.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlMultiCBEntry_OpenLink);
            // 
            // hlOverridePWPStatus
            // 
            this.hlOverridePWPStatus.EditValue = "Override PWP Status";
            this.hlOverridePWPStatus.Location = new System.Drawing.Point(170, 179);
            this.hlOverridePWPStatus.Name = "hlOverridePWPStatus";
            this.hlOverridePWPStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlOverridePWPStatus.Properties.Appearance.Options.UseBackColor = true;
            this.hlOverridePWPStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlOverridePWPStatus.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlOverridePWPStatus.Properties.Image")));
            this.hlOverridePWPStatus.RESG_ImageType = ReflexImgSrc.Image.ImageType.Cancel2;
            this.hlOverridePWPStatus.Size = new System.Drawing.Size(154, 30);
            this.hlOverridePWPStatus.StyleController = this.layoutControl6;
            this.hlOverridePWPStatus.TabIndex = 18;
            this.hlOverridePWPStatus.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlOverridePWPStatus_OpenLink);
            // 
            // hlOverrideCompliance
            // 
            this.hlOverrideCompliance.EditValue = "Override Compliance";
            this.hlOverrideCompliance.Location = new System.Drawing.Point(170, 145);
            this.hlOverrideCompliance.Name = "hlOverrideCompliance";
            this.hlOverrideCompliance.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlOverrideCompliance.Properties.Appearance.Options.UseBackColor = true;
            this.hlOverrideCompliance.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlOverrideCompliance.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlOverrideCompliance.Properties.Image")));
            this.hlOverrideCompliance.RESG_ImageType = ReflexImgSrc.Image.ImageType.Cancel2;
            this.hlOverrideCompliance.Size = new System.Drawing.Size(154, 30);
            this.hlOverrideCompliance.StyleController = this.layoutControl6;
            this.hlOverrideCompliance.TabIndex = 9;
            this.hlOverrideCompliance.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlOverrideCompliance_OpenLink);
            // 
            // hlEventHistory
            // 
            this.hlEventHistory.EditValue = "View Event History";
            this.hlEventHistory.Enabled = false;
            this.hlEventHistory.Location = new System.Drawing.Point(170, 111);
            this.hlEventHistory.Name = "hlEventHistory";
            this.hlEventHistory.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlEventHistory.Properties.Appearance.Options.UseBackColor = true;
            this.hlEventHistory.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlEventHistory.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlEventHistory.Properties.Image")));
            this.hlEventHistory.RESG_ImageType = ReflexImgSrc.Image.ImageType.Show;
            this.hlEventHistory.Size = new System.Drawing.Size(154, 22);
            this.hlEventHistory.StyleController = this.layoutControl6;
            this.hlEventHistory.TabIndex = 17;
            this.hlEventHistory.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hlEventHistory_OpenLink);
            // 
            // hlNewInv
            // 
            this.hlNewInv.EditValue = "New Invoice";
            this.hlNewInv.Location = new System.Drawing.Point(12, 12);
            this.hlNewInv.Name = "hlNewInv";
            this.hlNewInv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlNewInv.Properties.Appearance.Options.UseBackColor = true;
            this.hlNewInv.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlNewInv.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlNewInv.Properties.Image")));
            this.hlNewInv.RESG_ImageType = ReflexImgSrc.Image.ImageType.AddFile;
            this.hlNewInv.Size = new System.Drawing.Size(154, 30);
            this.hlNewInv.StyleController = this.layoutControl6;
            this.hlNewInv.TabIndex = 5;
            this.hlNewInv.Click += new System.EventHandler(this.hlNewInv_Click);
            // 
            // hlRefresh
            // 
            this.hlRefresh.EditValue = "Refresh";
            this.hlRefresh.Location = new System.Drawing.Point(170, 80);
            this.hlRefresh.Name = "hlRefresh";
            this.hlRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlRefresh.Properties.Appearance.Options.UseBackColor = true;
            this.hlRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlRefresh.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlRefresh.Properties.Image")));
            this.hlRefresh.RESG_ImageType = ReflexImgSrc.Image.ImageType.Refresh2;
            this.hlRefresh.Size = new System.Drawing.Size(154, 27);
            this.hlRefresh.StyleController = this.layoutControl6;
            this.hlRefresh.TabIndex = 10;
            this.hlRefresh.Click += new System.EventHandler(this.hlRefresh_Click);
            // 
            // hlPrint
            // 
            this.hlPrint.EditValue = "Print Invoice List";
            this.hlPrint.Location = new System.Drawing.Point(170, 12);
            this.hlPrint.Name = "hlPrint";
            this.hlPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlPrint.Properties.Appearance.Options.UseBackColor = true;
            this.hlPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlPrint.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlPrint.Properties.Image")));
            this.hlPrint.RESG_ImageType = ReflexImgSrc.Image.ImageType.Print;
            this.hlPrint.Size = new System.Drawing.Size(154, 30);
            this.hlPrint.StyleController = this.layoutControl6;
            this.hlPrint.TabIndex = 8;
            this.hlPrint.Click += new System.EventHandler(this.hlPrint_Click);
            // 
            // hlBalance
            // 
            this.hlBalance.EditValue = "Balance Header";
            this.hlBalance.Location = new System.Drawing.Point(170, 46);
            this.hlBalance.Name = "hlBalance";
            this.hlBalance.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlBalance.Properties.Appearance.Options.UseBackColor = true;
            this.hlBalance.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlBalance.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlBalance.Properties.Image")));
            this.hlBalance.RESG_ImageType = ReflexImgSrc.Image.ImageType.AlignHorizontalTop;
            this.hlBalance.Size = new System.Drawing.Size(154, 30);
            this.hlBalance.StyleController = this.layoutControl6;
            this.hlBalance.TabIndex = 11;
            this.hlBalance.Click += new System.EventHandler(this.hlBalance_Click);
            // 
            // hlChargeBack
            // 
            this.hlChargeBack.EditValue = "Create Chargeback";
            this.hlChargeBack.Location = new System.Drawing.Point(12, 145);
            this.hlChargeBack.Name = "hlChargeBack";
            this.hlChargeBack.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlChargeBack.Properties.Appearance.Options.UseBackColor = true;
            this.hlChargeBack.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlChargeBack.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlChargeBack.Properties.Image")));
            this.hlChargeBack.RESG_ImageType = ReflexImgSrc.Image.ImageType.AddItem;
            this.hlChargeBack.Size = new System.Drawing.Size(154, 29);
            this.hlChargeBack.StyleController = this.layoutControl6;
            this.hlChargeBack.TabIndex = 16;
            this.hlChargeBack.Click += new System.EventHandler(this.hlChargeBack_Click);
            // 
            // hlDeleteInv
            // 
            this.hlDeleteInv.EditValue = "Delete Invoice";
            this.hlDeleteInv.Location = new System.Drawing.Point(12, 46);
            this.hlDeleteInv.Name = "hlDeleteInv";
            this.hlDeleteInv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlDeleteInv.Properties.Appearance.Options.UseBackColor = true;
            this.hlDeleteInv.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlDeleteInv.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlDeleteInv.Properties.Image")));
            this.hlDeleteInv.RESG_ImageType = ReflexImgSrc.Image.ImageType.Delete;
            this.hlDeleteInv.Size = new System.Drawing.Size(154, 29);
            this.hlDeleteInv.StyleController = this.layoutControl6;
            this.hlDeleteInv.TabIndex = 7;
            this.hlDeleteInv.Click += new System.EventHandler(this.hlDeleteInv_Click);
            // 
            // hlQuickChk
            // 
            this.hlQuickChk.EditValue = "Quick Check";
            this.hlQuickChk.Location = new System.Drawing.Point(12, 80);
            this.hlQuickChk.Name = "hlQuickChk";
            this.hlQuickChk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlQuickChk.Properties.Appearance.Options.UseBackColor = true;
            this.hlQuickChk.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlQuickChk.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlQuickChk.Properties.Image")));
            this.hlQuickChk.RESG_ImageType = ReflexImgSrc.Image.ImageType.BOSale;
            this.hlQuickChk.Size = new System.Drawing.Size(154, 27);
            this.hlQuickChk.StyleController = this.layoutControl6;
            this.hlQuickChk.TabIndex = 9;
            this.hlQuickChk.Click += new System.EventHandler(this.hlQuickChk_Click);
            // 
            // hlChangeSupp
            // 
            this.hlChangeSupp.EditValue = "Change Supplier on PO";
            this.hlChangeSupp.Location = new System.Drawing.Point(12, 111);
            this.hlChangeSupp.Name = "hlChangeSupp";
            this.hlChangeSupp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlChangeSupp.Properties.Appearance.Options.UseBackColor = true;
            this.hlChangeSupp.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlChangeSupp.Properties.Image = ((System.Drawing.Image)(resources.GetObject("hlChangeSupp.Properties.Image")));
            this.hlChangeSupp.RESG_ImageType = ReflexImgSrc.Image.ImageType.Edit;
            this.hlChangeSupp.Size = new System.Drawing.Size(154, 30);
            this.hlChangeSupp.StyleController = this.layoutControl6;
            this.hlChangeSupp.TabIndex = 10;
            this.hlChangeSupp.Click += new System.EventHandler(this.hlChangeSupp_Click);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "layoutControlGroup6";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciNewInv,
            this.lciDeleteInv,
            this.lciQuickChk,
            this.lciChangeSupp,
            this.lciChargeBack,
            this.lciPrint,
            this.lciBalance,
            this.lciRefresh,
            this.lciEventHistory,
            this.lciOverrideCompliance,
            this.lciOverridePWPStatus,
            this.esiBottom,
            this.lciMultiCBEntry});
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(336, 255);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // lciNewInv
            // 
            this.lciNewInv.Control = this.hlNewInv;
            this.lciNewInv.CustomizationFormText = "New Invoice";
            this.lciNewInv.Location = new System.Drawing.Point(0, 0);
            this.lciNewInv.Name = "lciNewInv";
            this.lciNewInv.Size = new System.Drawing.Size(158, 34);
            this.lciNewInv.Text = "New Invoice";
            this.lciNewInv.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciNewInv.TextSize = new System.Drawing.Size(0, 0);
            this.lciNewInv.TextVisible = false;
            // 
            // lciDeleteInv
            // 
            this.lciDeleteInv.Control = this.hlDeleteInv;
            this.lciDeleteInv.CustomizationFormText = "Delete Invoice";
            this.lciDeleteInv.Location = new System.Drawing.Point(0, 34);
            this.lciDeleteInv.Name = "lciDeleteInv";
            this.lciDeleteInv.Size = new System.Drawing.Size(158, 34);
            this.lciDeleteInv.Text = "Delete Invoice";
            this.lciDeleteInv.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciDeleteInv.TextSize = new System.Drawing.Size(0, 0);
            this.lciDeleteInv.TextVisible = false;
            // 
            // lciQuickChk
            // 
            this.lciQuickChk.Control = this.hlQuickChk;
            this.lciQuickChk.CustomizationFormText = "Quick Check";
            this.lciQuickChk.Location = new System.Drawing.Point(0, 68);
            this.lciQuickChk.Name = "lciQuickChk";
            this.lciQuickChk.Size = new System.Drawing.Size(158, 31);
            this.lciQuickChk.Text = "Quick Check";
            this.lciQuickChk.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciQuickChk.TextSize = new System.Drawing.Size(0, 0);
            this.lciQuickChk.TextVisible = false;
            // 
            // lciChangeSupp
            // 
            this.lciChangeSupp.Control = this.hlChangeSupp;
            this.lciChangeSupp.CustomizationFormText = "Change Supplier on PO";
            this.lciChangeSupp.Location = new System.Drawing.Point(0, 99);
            this.lciChangeSupp.Name = "lciChangeSupp";
            this.lciChangeSupp.Size = new System.Drawing.Size(158, 34);
            this.lciChangeSupp.Text = "Change Supplier on PO";
            this.lciChangeSupp.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciChangeSupp.TextSize = new System.Drawing.Size(0, 0);
            this.lciChangeSupp.TextVisible = false;
            // 
            // lciChargeBack
            // 
            this.lciChargeBack.Control = this.hlChargeBack;
            this.lciChargeBack.CustomizationFormText = "Create Chargeback";
            this.lciChargeBack.Location = new System.Drawing.Point(0, 133);
            this.lciChargeBack.Name = "lciChargeBack";
            this.lciChargeBack.Size = new System.Drawing.Size(158, 34);
            this.lciChargeBack.Text = "Create Chargeback";
            this.lciChargeBack.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciChargeBack.TextSize = new System.Drawing.Size(0, 0);
            this.lciChargeBack.TextVisible = false;
            // 
            // lciPrint
            // 
            this.lciPrint.Control = this.hlPrint;
            this.lciPrint.CustomizationFormText = "Print Invoice List";
            this.lciPrint.Location = new System.Drawing.Point(158, 0);
            this.lciPrint.Name = "lciPrint";
            this.lciPrint.Size = new System.Drawing.Size(158, 34);
            this.lciPrint.Text = "Print Invoice List";
            this.lciPrint.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciPrint.TextSize = new System.Drawing.Size(0, 0);
            this.lciPrint.TextVisible = false;
            // 
            // lciBalance
            // 
            this.lciBalance.Control = this.hlBalance;
            this.lciBalance.CustomizationFormText = "Balance Header";
            this.lciBalance.Location = new System.Drawing.Point(158, 34);
            this.lciBalance.Name = "lciBalance";
            this.lciBalance.Size = new System.Drawing.Size(158, 34);
            this.lciBalance.Text = "Balance Header";
            this.lciBalance.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciBalance.TextSize = new System.Drawing.Size(0, 0);
            this.lciBalance.TextVisible = false;
            // 
            // lciRefresh
            // 
            this.lciRefresh.Control = this.hlRefresh;
            this.lciRefresh.CustomizationFormText = "Refresh";
            this.lciRefresh.Location = new System.Drawing.Point(158, 68);
            this.lciRefresh.Name = "lciRefresh";
            this.lciRefresh.Size = new System.Drawing.Size(158, 31);
            this.lciRefresh.Text = "Refresh";
            this.lciRefresh.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.lciRefresh.TextVisible = false;
            // 
            // lciEventHistory
            // 
            this.lciEventHistory.Control = this.hlEventHistory;
            this.lciEventHistory.CustomizationFormText = "View Event History";
            this.lciEventHistory.Location = new System.Drawing.Point(158, 99);
            this.lciEventHistory.Name = "lciEventHistory";
            this.lciEventHistory.Size = new System.Drawing.Size(158, 34);
            this.lciEventHistory.Text = "View Event History";
            this.lciEventHistory.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciEventHistory.TextSize = new System.Drawing.Size(0, 0);
            this.lciEventHistory.TextVisible = false;
            // 
            // lciOverrideCompliance
            // 
            this.lciOverrideCompliance.Control = this.hlOverrideCompliance;
            this.lciOverrideCompliance.CustomizationFormText = "Send For Routing";
            this.lciOverrideCompliance.Location = new System.Drawing.Point(158, 133);
            this.lciOverrideCompliance.Name = "lciOverrideCompliance";
            this.lciOverrideCompliance.Size = new System.Drawing.Size(158, 34);
            this.lciOverrideCompliance.Text = "Send For Routing";
            this.lciOverrideCompliance.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciOverrideCompliance.TextSize = new System.Drawing.Size(0, 0);
            this.lciOverrideCompliance.TextVisible = false;
            // 
            // lciOverridePWPStatus
            // 
            this.lciOverridePWPStatus.Control = this.hlOverridePWPStatus;
            this.lciOverridePWPStatus.CustomizationFormText = "Override PWP Status";
            this.lciOverridePWPStatus.Location = new System.Drawing.Point(158, 167);
            this.lciOverridePWPStatus.Name = "lciOverridePWPStatus";
            this.lciOverridePWPStatus.Size = new System.Drawing.Size(158, 68);
            this.lciOverridePWPStatus.Text = "Override PWP Status";
            this.lciOverridePWPStatus.TextLocation = DevExpress.Utils.Locations.Left;
            this.lciOverridePWPStatus.TextSize = new System.Drawing.Size(0, 0);
            this.lciOverridePWPStatus.TextVisible = false;
            // 
            // esiBottom
            // 
            this.esiBottom.AllowHotTrack = false;
            this.esiBottom.CustomizationFormText = "emptySpaceItem3";
            this.esiBottom.Location = new System.Drawing.Point(0, 201);
            this.esiBottom.Name = "esiBottom";
            this.esiBottom.Size = new System.Drawing.Size(158, 34);
            this.esiBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciMultiCBEntry
            // 
            this.lciMultiCBEntry.Control = this.hlMultiCBEntry;
            this.lciMultiCBEntry.Location = new System.Drawing.Point(0, 167);
            this.lciMultiCBEntry.Name = "lciMultiCBEntry";
            this.lciMultiCBEntry.Size = new System.Drawing.Size(158, 34);
            this.lciMultiCBEntry.Text = "Multi Chargeback Entry";
            this.lciMultiCBEntry.TextSize = new System.Drawing.Size(0, 0);
            this.lciMultiCBEntry.TextVisible = false;
            // 
            // dnHeaderNav
            // 
            this.dnHeaderNav.Buttons.Append.Visible = false;
            this.dnHeaderNav.Buttons.Remove.Visible = false;
            this.dnHeaderNav.DataMember = "AP_INV_HEADER";
            this.dnHeaderNav.DataSource = this.dsHeaderSide1;
            this.dnHeaderNav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dnHeaderNav.Location = new System.Drawing.Point(0, 255);
            this.dnHeaderNav.Name = "dnHeaderNav";
            this.dnHeaderNav.Size = new System.Drawing.Size(336, 19);
            this.dnHeaderNav.TabIndex = 4;
            this.dnHeaderNav.Text = "dataNavigator1";
            this.dnHeaderNav.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.dnHeaderNav_ButtonClick);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("3b86f324-d4aa-4304-a6cc-ed2a2c19b1ea");
            this.dockPanel1.Location = new System.Drawing.Point(0, 483);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(1231, 296);
            this.dockPanel1.Size = new System.Drawing.Size(1231, 296);
            this.dockPanel1.Text = "Details";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.tcDetails);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1223, 268);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // tcDetails
            // 
            this.tcDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDetails.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.tcDetails.Location = new System.Drawing.Point(0, 0);
            this.tcDetails.Name = "tcDetails";
            this.tcDetails.SelectedTabPage = this.xtraTabPage1;
            this.tcDetails.Size = new System.Drawing.Size(1223, 268);
            this.tcDetails.TabIndex = 1;
            this.tcDetails.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.tpMatchPO,
            this.tpContractPO,
            this.tpSummContractPO,
            this.tpPWPLink,
            this.tpLevy});
            this.tcDetails.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tcDetails_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcDetail);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1217, 240);
            this.xtraTabPage1.Text = "Details";
            // 
            // gcDetail
            // 
            this.gcDetail.DataMember = "AP_GL_ALLOC";
            this.gcDetail.DataSource = this.dsInvDetail1;
            this.gcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDetail.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gcDetail_EmbeddedNavigator_ButtonClick);
            this.gcDetail.Location = new System.Drawing.Point(0, 0);
            this.gcDetail.MainView = this.gvDetail;
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit9,
            this.riGLDesc,
            this.repositoryItemTextEdit9,
            this.repositoryItemTextEdit10,
            this.repositoryItemMemoExEdit2,
            this.repositoryItemLookUpEdit12,
            this.riPOSelect,
            this.riPOBSelect,
            this.riPODSelect,
            this.riPOFSelect,
            this.riPOMSelect,
            this.riPOM2Select,
            this.riNoPO,
            this.riPOInvSelect,
            this.riDetPOSelect,
            this.riNoPOSelect,
            this.riITC,
            this.riAFE,
            this.riCustCostCode,
            this.riTimeTicket,
            this.riGLDescReadOnly,
            this.riHours,
            this.riRate,
            this.riCompanies,
            this.ceBillabled,
            this.riPWPStatus});
            this.gcDetail.Size = new System.Drawing.Size(1217, 240);
            this.gcDetail.TabIndex = 0;
            this.gcDetail.UseEmbeddedNavigator = true;
            this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
            this.gcDetail.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcDetail_ProcessGridKey);
            // 
            // dsInvDetail1
            // 
            this.dsInvDetail1.DataSetName = "dsInvDetail";
            this.dsInvDetail1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsInvDetail1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.colSEG_CHANGE,
            this.colPO_ID2,
            this.colITC,
            this.colpri_id,
            this.collv1id,
            this.collv2id,
            this.collv3id,
            this.collv4id,
            this.collem_comp,
            this.colEXPENSE_TYPE,
            this.colAFE_NO,
            this.colCOST_CODE,
            this.colTIME_TICKET,
            this.colHOURS,
            this.colRATE,
            this.colCOMPANY_ALIAS,
            this.colCB_ID,
            this.colCB_REF,
            this.colbillable,
            this.colAR_PWP_STATUS_ID});
            this.gvDetail.GridControl = this.gcDetail;
            this.gvDetail.Name = "gvDetail";
            this.gvDetail.OptionsView.ColumnAutoWidth = false;
            this.gvDetail.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvDetail_CustomRowCellEdit);
            this.gvDetail.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvDetail_InitNewRow);
            this.gvDetail.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDetail_FocusedRowChanged);
            this.gvDetail.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvDetail_InvalidRowException);
            this.gvDetail.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvDetail_ValidateRow);
            this.gvDetail.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvDetail_RowUpdated);
            // 
            // colGL_ACCOUNT
            // 
            this.colGL_ACCOUNT.Caption = "GL Account";
            this.colGL_ACCOUNT.FieldName = "GL_ACCOUNT";
            this.colGL_ACCOUNT.Name = "colGL_ACCOUNT";
            this.colGL_ACCOUNT.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colGL_ACCOUNT.Visible = true;
            this.colGL_ACCOUNT.VisibleIndex = 0;
            this.colGL_ACCOUNT.Width = 183;
            // 
            // colCOMMENT1
            // 
            this.colCOMMENT1.Caption = "Comment";
            this.colCOMMENT1.ColumnEdit = this.repositoryItemMemoExEdit2;
            this.colCOMMENT1.FieldName = "COMMENT";
            this.colCOMMENT1.Name = "colCOMMENT1";
            this.colCOMMENT1.Visible = true;
            this.colCOMMENT1.VisibleIndex = 6;
            // 
            // repositoryItemMemoExEdit2
            // 
            this.repositoryItemMemoExEdit2.AutoHeight = false;
            this.repositoryItemMemoExEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit2.MaxLength = 40;
            this.repositoryItemMemoExEdit2.Name = "repositoryItemMemoExEdit2";
            // 
            // colAMOUNT
            // 
            this.colAMOUNT.Caption = "Amount";
            this.colAMOUNT.ColumnEdit = this.repositoryItemTextEdit9;
            this.colAMOUNT.FieldName = "AMOUNT";
            this.colAMOUNT.Name = "colAMOUNT";
            this.colAMOUNT.Visible = true;
            this.colAMOUNT.VisibleIndex = 4;
            // 
            // repositoryItemTextEdit9
            // 
            this.repositoryItemTextEdit9.AutoHeight = false;
            this.repositoryItemTextEdit9.Mask.EditMask = "n2";
            this.repositoryItemTextEdit9.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit9.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit9.Name = "repositoryItemTextEdit9";
            this.repositoryItemTextEdit9.EditValueChanged += new System.EventHandler(this.repositoryItemTextEdit9_EditValueChanged);
            // 
            // colTRANS_TYPE
            // 
            this.colTRANS_TYPE.Caption = "Type";
            this.colTRANS_TYPE.ColumnEdit = this.repositoryItemLookUpEdit9;
            this.colTRANS_TYPE.FieldName = "PO_TYPE";
            this.colTRANS_TYPE.Name = "colTRANS_TYPE";
            this.colTRANS_TYPE.Visible = true;
            this.colTRANS_TYPE.VisibleIndex = 7;
            // 
            // repositoryItemLookUpEdit9
            // 
            this.repositoryItemLookUpEdit9.AutoHeight = false;
            this.repositoryItemLookUpEdit9.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit9.Name = "repositoryItemLookUpEdit9";
            this.repositoryItemLookUpEdit9.NullText = "";
            // 
            // colHOLD_AMT1
            // 
            this.colHOLD_AMT1.Caption = "Holdback $";
            this.colHOLD_AMT1.ColumnEdit = this.repositoryItemTextEdit10;
            this.colHOLD_AMT1.FieldName = "HOLD_AMT";
            this.colHOLD_AMT1.Name = "colHOLD_AMT1";
            this.colHOLD_AMT1.Visible = true;
            this.colHOLD_AMT1.VisibleIndex = 5;
            // 
            // repositoryItemTextEdit10
            // 
            this.repositoryItemTextEdit10.AutoHeight = false;
            this.repositoryItemTextEdit10.Mask.EditMask = "n2";
            this.repositoryItemTextEdit10.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit10.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit10.Name = "repositoryItemTextEdit10";
            // 
            // colPO_ID1
            // 
            this.colPO_ID1.Caption = "PO #";
            this.colPO_ID1.ColumnEdit = this.riPOSelect;
            this.colPO_ID1.FieldName = "PO_REC_ID";
            this.colPO_ID1.Name = "colPO_ID1";
            this.colPO_ID1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colPO_ID1.Visible = true;
            this.colPO_ID1.VisibleIndex = 8;
            // 
            // riPOSelect
            // 
            this.riPOSelect.AutoHeight = false;
            this.riPOSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject20, "", null, null, false),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject21, "", null, null, false)});
            this.riPOSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 150, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 75, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPOSelect.DataSource = this.dsPOSelect1.PO_REC_HEADER;
            this.riPOSelect.DisplayMember = "PO #";
            this.riPOSelect.Name = "riPOSelect";
            this.riPOSelect.NullText = "";
            this.riPOSelect.PopupWidth = 300;
            this.riPOSelect.ReadOnly = true;
            this.riPOSelect.ValueMember = "PO_REC_ID";
            this.riPOSelect.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPOSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
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
            this.colGL_ACCOUNT1.Caption = "Description";
            this.colGL_ACCOUNT1.ColumnEdit = this.riGLDesc;
            this.colGL_ACCOUNT1.FieldName = "GL_ACCOUNT";
            this.colGL_ACCOUNT1.Name = "colGL_ACCOUNT1";
            this.colGL_ACCOUNT1.OptionsColumn.AllowEdit = false;
            this.colGL_ACCOUNT1.Visible = true;
            this.colGL_ACCOUNT1.VisibleIndex = 1;
            this.colGL_ACCOUNT1.Width = 205;
            // 
            // riGLDesc
            // 
            this.riGLDesc.AutoHeight = false;
            this.riGLDesc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject22, "", null, null, false)});
            this.riGLDesc.DataSource = this.dsGLAccts1.GL_ACCOUNTS;
            this.riGLDesc.DisplayMember = "DESCRIPTION";
            this.riGLDesc.Name = "riGLDesc";
            this.riGLDesc.NullText = "";
            this.riGLDesc.ReadOnly = true;
            this.riGLDesc.ValueMember = "ACCOUNT_NUMBER";
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
            this.colREFERENCE1.Visible = true;
            this.colREFERENCE1.VisibleIndex = 2;
            this.colREFERENCE1.Width = 166;
            // 
            // colSEG_CHANGE
            // 
            this.colSEG_CHANGE.Caption = "Alloc Seg";
            this.colSEG_CHANGE.ColumnEdit = this.repositoryItemLookUpEdit12;
            this.colSEG_CHANGE.FieldName = "SEG_CHANGE";
            this.colSEG_CHANGE.Name = "colSEG_CHANGE";
            this.colSEG_CHANGE.Visible = true;
            this.colSEG_CHANGE.VisibleIndex = 10;
            // 
            // repositoryItemLookUpEdit12
            // 
            this.repositoryItemLookUpEdit12.AutoHeight = false;
            this.repositoryItemLookUpEdit12.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit12.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ALLOC_ID", "GL_ALLOC_ID", 86, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GL_ALLOC_CODE", "Code", 90, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit12.DataSource = this.dsAllocSeg1.GL_ALLOC;
            this.repositoryItemLookUpEdit12.DisplayMember = "GL_ALLOC_CODE";
            this.repositoryItemLookUpEdit12.Name = "repositoryItemLookUpEdit12";
            this.repositoryItemLookUpEdit12.NullText = "";
            this.repositoryItemLookUpEdit12.ValueMember = "GL_ALLOC_ID";
            // 
            // colPO_ID2
            // 
            this.colPO_ID2.Caption = "PO Matching";
            this.colPO_ID2.ColumnEdit = this.riDetPOSelect;
            this.colPO_ID2.FieldName = "PO_ID";
            this.colPO_ID2.Name = "colPO_ID2";
            this.colPO_ID2.Visible = true;
            this.colPO_ID2.VisibleIndex = 9;
            this.colPO_ID2.Width = 83;
            // 
            // riDetPOSelect
            // 
            this.riDetPOSelect.AutoHeight = false;
            this.riDetPOSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDetPOSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_ID", "PO_ID", 50, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO", "PO #", 75, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ORDER_DATE", "PO Date", 125, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riDetPOSelect.DataSource = this.dsDetPO1.PO_HEADER;
            this.riDetPOSelect.DisplayMember = "PO";
            this.riDetPOSelect.Name = "riDetPOSelect";
            this.riDetPOSelect.NullText = "";
            this.riDetPOSelect.ValueMember = "PO_ID";
            this.riDetPOSelect.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.riDetPOSelect_Closed);
            // 
            // dsDetPO1
            // 
            this.dsDetPO1.DataSetName = "dsDetPO";
            this.dsDetPO1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsDetPO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colITC
            // 
            this.colITC.Caption = "ITC";
            this.colITC.ColumnEdit = this.riITC;
            this.colITC.FieldName = "ITC";
            this.colITC.Name = "colITC";
            this.colITC.Visible = true;
            this.colITC.VisibleIndex = 11;
            // 
            // riITC
            // 
            this.riITC.AutoHeight = false;
            this.riITC.Name = "riITC";
            this.riITC.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.riITC.ValueChecked = "Y";
            this.riITC.ValueUnchecked = "N";
            // 
            // colpri_id
            // 
            this.colpri_id.FieldName = "pri_id";
            this.colpri_id.Name = "colpri_id";
            this.colpri_id.OptionsColumn.AllowEdit = false;
            this.colpri_id.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // collv1id
            // 
            this.collv1id.FieldName = "lv1id";
            this.collv1id.Name = "collv1id";
            this.collv1id.OptionsColumn.AllowEdit = false;
            this.collv1id.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // collv2id
            // 
            this.collv2id.FieldName = "lv2id";
            this.collv2id.Name = "collv2id";
            this.collv2id.OptionsColumn.AllowEdit = false;
            this.collv2id.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // collv3id
            // 
            this.collv3id.FieldName = "lv3id";
            this.collv3id.Name = "collv3id";
            this.collv3id.OptionsColumn.AllowEdit = false;
            this.collv3id.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // collv4id
            // 
            this.collv4id.FieldName = "lv4id";
            this.collv4id.Name = "collv4id";
            this.collv4id.OptionsColumn.AllowEdit = false;
            this.collv4id.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // collem_comp
            // 
            this.collem_comp.FieldName = "lem_comp";
            this.collem_comp.Name = "collem_comp";
            this.collem_comp.OptionsColumn.AllowEdit = false;
            this.collem_comp.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colEXPENSE_TYPE
            // 
            this.colEXPENSE_TYPE.FieldName = "EXPENSE_TYPE";
            this.colEXPENSE_TYPE.Name = "colEXPENSE_TYPE";
            this.colEXPENSE_TYPE.OptionsColumn.AllowEdit = false;
            this.colEXPENSE_TYPE.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colAFE_NO
            // 
            this.colAFE_NO.Caption = "AFE";
            this.colAFE_NO.ColumnEdit = this.riAFE;
            this.colAFE_NO.FieldName = "AFE_NO";
            this.colAFE_NO.Name = "colAFE_NO";
            // 
            // riAFE
            // 
            this.riAFE.AutoHeight = false;
            this.riAFE.MaxLength = 15;
            this.riAFE.Name = "riAFE";
            // 
            // colCOST_CODE
            // 
            this.colCOST_CODE.Caption = "Cust Cost Code";
            this.colCOST_CODE.ColumnEdit = this.riCustCostCode;
            this.colCOST_CODE.FieldName = "COST_CODE";
            this.colCOST_CODE.Name = "colCOST_CODE";
            // 
            // riCustCostCode
            // 
            this.riCustCostCode.AutoHeight = false;
            this.riCustCostCode.MaxLength = 15;
            this.riCustCostCode.Name = "riCustCostCode";
            // 
            // colTIME_TICKET
            // 
            this.colTIME_TICKET.Caption = "Time Ticket";
            this.colTIME_TICKET.ColumnEdit = this.riTimeTicket;
            this.colTIME_TICKET.FieldName = "TIME_TICKET";
            this.colTIME_TICKET.Name = "colTIME_TICKET";
            // 
            // riTimeTicket
            // 
            this.riTimeTicket.AutoHeight = false;
            this.riTimeTicket.MaxLength = 15;
            this.riTimeTicket.Name = "riTimeTicket";
            // 
            // colHOURS
            // 
            this.colHOURS.Caption = "Hours";
            this.colHOURS.ColumnEdit = this.riHours;
            this.colHOURS.FieldName = "HOURS";
            this.colHOURS.Name = "colHOURS";
            // 
            // riHours
            // 
            this.riHours.AutoHeight = false;
            this.riHours.Mask.EditMask = "n2";
            this.riHours.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riHours.Mask.UseMaskAsDisplayFormat = true;
            this.riHours.Name = "riHours";
            this.riHours.EditValueChanged += new System.EventHandler(this.riHours_EditValueChanged);
            // 
            // colRATE
            // 
            this.colRATE.Caption = "Rate";
            this.colRATE.ColumnEdit = this.riRate;
            this.colRATE.DisplayFormat.FormatString = "n2";
            this.colRATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRATE.FieldName = "RATE";
            this.colRATE.Name = "colRATE";
            this.colRATE.OptionsColumn.AllowEdit = false;
            // 
            // riRate
            // 
            this.riRate.AutoHeight = false;
            this.riRate.Mask.EditMask = "n2";
            this.riRate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.riRate.Mask.UseMaskAsDisplayFormat = true;
            this.riRate.Name = "riRate";
            this.riRate.EditValueChanged += new System.EventHandler(this.riRate_EditValueChanged);
            // 
            // colCOMPANY_ALIAS
            // 
            this.colCOMPANY_ALIAS.Caption = "Company";
            this.colCOMPANY_ALIAS.ColumnEdit = this.riCompanies;
            this.colCOMPANY_ALIAS.FieldName = "COMPANY_ALIAS";
            this.colCOMPANY_ALIAS.Name = "colCOMPANY_ALIAS";
            this.colCOMPANY_ALIAS.Visible = true;
            this.colCOMPANY_ALIAS.VisibleIndex = 12;
            // 
            // riCompanies
            // 
            this.riCompanies.AutoHeight = false;
            this.riCompanies.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riCompanies.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("COMPANY_ALIAS", "COMPANY_ALIAS", 107, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("company_name", "Company", 85, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riCompanies.DataSource = this.bsCompanies;
            this.riCompanies.DisplayMember = "company_name";
            this.riCompanies.Name = "riCompanies";
            this.riCompanies.NullText = "";
            this.riCompanies.PopupWidth = 250;
            this.riCompanies.ValueMember = "COMPANY_ALIAS";
            // 
            // bsCompanies
            // 
            this.bsCompanies.DataMember = "COMPANIES";
            this.bsCompanies.DataSource = this.dsCompanies1;
            // 
            // dsCompanies1
            // 
            this.dsCompanies1.DataSetName = "dsCompanies";
            this.dsCompanies1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // colCB_ID
            // 
            this.colCB_ID.FieldName = "CB_ID";
            this.colCB_ID.Name = "colCB_ID";
            this.colCB_ID.OptionsColumn.AllowEdit = false;
            this.colCB_ID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colCB_REF
            // 
            this.colCB_REF.Caption = "Chargeback";
            this.colCB_REF.FieldName = "CB_REF";
            this.colCB_REF.Name = "colCB_REF";
            this.colCB_REF.OptionsColumn.ShowInCustomizationForm = false;
            this.colCB_REF.Width = 95;
            // 
            // colbillable
            // 
            this.colbillable.Caption = "Billable";
            this.colbillable.ColumnEdit = this.ceBillabled;
            this.colbillable.FieldName = "billable";
            this.colbillable.Name = "colbillable";
            this.colbillable.OptionsColumn.AllowEdit = false;
            this.colbillable.Visible = true;
            this.colbillable.VisibleIndex = 13;
            this.colbillable.Width = 54;
            // 
            // ceBillabled
            // 
            this.ceBillabled.AutoHeight = false;
            this.ceBillabled.Name = "ceBillabled";
            this.ceBillabled.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.ceBillabled.ValueChecked = "T";
            this.ceBillabled.ValueUnchecked = "F";
            // 
            // colAR_PWP_STATUS_ID
            // 
            this.colAR_PWP_STATUS_ID.Caption = "PWP Status";
            this.colAR_PWP_STATUS_ID.ColumnEdit = this.riPWPStatus;
            this.colAR_PWP_STATUS_ID.FieldName = "AR_PWP_STATUS_ID";
            this.colAR_PWP_STATUS_ID.Name = "colAR_PWP_STATUS_ID";
            this.colAR_PWP_STATUS_ID.Visible = true;
            this.colAR_PWP_STATUS_ID.VisibleIndex = 14;
            this.colAR_PWP_STATUS_ID.Width = 91;
            // 
            // riPWPStatus
            // 
            this.riPWPStatus.AutoHeight = false;
            this.riPWPStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPWPStatus.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID", 126, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DESCRIPTION", "Status", 78, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.riPWPStatus.DataSource = this.bsPWP_Status;
            this.riPWPStatus.DisplayMember = "DESCRIPTION";
            this.riPWPStatus.Name = "riPWPStatus";
            this.riPWPStatus.NullText = "";
            this.riPWPStatus.PopupWidth = 300;
            this.riPWPStatus.ReadOnly = true;
            this.riPWPStatus.ValueMember = "AR_PWP_STATUS_ID";
            // 
            // bsPWP_Status
            // 
            this.bsPWP_Status.DataMember = "AR_PWP_STATUS";
            this.bsPWP_Status.DataSource = this.dsPWP_Status1;
            // 
            // dsPWP_Status1
            // 
            this.dsPWP_Status1.DataSetName = "dsPWP_Status";
            this.dsPWP_Status1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOBSelect
            // 
            this.riPOBSelect.AutoHeight = false;
            this.riPOBSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riPOBSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIVER_NUMBER", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIPT_DATE", "Receipt Date", 70, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EST", "EST", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ACT", "ACT", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OS", "O/S", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPOBSelect.DataSource = this.dsPOBSelect1.PO_HEADER;
            this.riPOBSelect.DisplayMember = "PO #";
            this.riPOBSelect.Name = "riPOBSelect";
            this.riPOBSelect.NullText = "";
            this.riPOBSelect.PopupWidth = 600;
            this.riPOBSelect.ValueMember = "PO_REC_ID";
            this.riPOBSelect.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPOBSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riPODSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIVER_NUMBER", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIPT_DATE", "Receipt Date", 70, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EST", "EST", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ACT", "ACT", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OS", "O/S", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPODSelect.DataSource = this.dsPODSelect1.PO_HEADER;
            this.riPODSelect.DisplayMember = "PO #";
            this.riPODSelect.Name = "riPODSelect";
            this.riPODSelect.NullText = "";
            this.riPODSelect.PopupWidth = 600;
            this.riPODSelect.ValueMember = "PO_REC_ID";
            this.riPODSelect.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPODSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
            // 
            // dsPODSelect1
            // 
            this.dsPODSelect1.DataSetName = "dsPODSelect";
            this.dsPODSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPODSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOFSelect
            // 
            this.riPOFSelect.AutoHeight = false;
            this.riPOFSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riPOFSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIVER_NUMBER", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIPT_DATE", "Receipt Date", 70, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EST", "EST", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ACT", "ACT", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OS", "O/S", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPOFSelect.DataSource = this.dsPOFSelect1.PO_HEADER;
            this.riPOFSelect.DisplayMember = "PO #";
            this.riPOFSelect.Name = "riPOFSelect";
            this.riPOFSelect.NullText = "";
            this.riPOFSelect.PopupWidth = 650;
            this.riPOFSelect.ValueMember = "PO_REC_ID";
            this.riPOFSelect.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPOFSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
            // 
            // dsPOFSelect1
            // 
            this.dsPOFSelect1.DataSetName = "dsPOFSelect";
            this.dsPOFSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOFSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riPOMSelect
            // 
            this.riPOMSelect.AutoHeight = false;
            this.riPOMSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riPOMSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIVER_NUMBER", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIPT_DATE", "Receipt Date", 70, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EST", "EST", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ACT", "ACT", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OS", "O/S", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPOMSelect.DataSource = this.dsPOMSelect1.PO_HEADER;
            this.riPOMSelect.DisplayMember = "PO #";
            this.riPOMSelect.Name = "riPOMSelect";
            this.riPOMSelect.NullText = "";
            this.riPOMSelect.PopupWidth = 600;
            this.riPOMSelect.ValueMember = "PO_REC_ID";
            this.riPOMSelect.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPOMSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riPOM2Select.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO_REC_ID", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO #", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Supplier Name", "Supplier Name", 125, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIVER_NUMBER", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RECEIPT_DATE", "Receipt Date", 70, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PO Amount", "PO Amount", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EST", "EST", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ACT", "ACT", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OS", "O/S", 60, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far)});
            this.riPOM2Select.DataSource = this.dsPOM2Select1.PO_HEADER;
            this.riPOM2Select.DisplayMember = "PO #";
            this.riPOM2Select.Name = "riPOM2Select";
            this.riPOM2Select.NullText = "";
            this.riPOM2Select.PopupWidth = 600;
            this.riPOM2Select.ValueMember = "PO_REC_ID";
            this.riPOM2Select.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.riPOSelect_ButtonClick);
            this.riPOM2Select.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
            // 
            // dsPOM2Select1
            // 
            this.dsPOM2Select1.DataSetName = "dsPOM2Select";
            this.dsPOM2Select1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOM2Select1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riNoPO
            // 
            this.riNoPO.AutoHeight = false;
            this.riNoPO.Name = "riNoPO";
            this.riNoPO.ReadOnly = true;
            // 
            // riPOInvSelect
            // 
            this.riPOInvSelect.AutoHeight = false;
            this.riPOInvSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPOInvSelect.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("po_rec_id", "PO_REC_ID", 76, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("po", "PO #", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("receiver_number", "Receiver #", 65, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("receipt_date", "Receipt Date", 80, DevExpress.Utils.FormatType.DateTime, "M/d/yyyy", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("receive_amt", "Receive Amount", 85, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("outstanding_amt", "Outstanding Amount", 100, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Default)});
            this.riPOInvSelect.DataSource = this.dsPOInvSelect1.working_PO_Inv_Select;
            this.riPOInvSelect.DisplayMember = "po";
            this.riPOInvSelect.Name = "riPOInvSelect";
            this.riPOInvSelect.NullText = "";
            this.riPOInvSelect.PopupWidth = 475;
            this.riPOInvSelect.ValueMember = "po_rec_id";
            this.riPOInvSelect.EditValueChanged += new System.EventHandler(this.riPOSelect_EditValueChanged);
            // 
            // dsPOInvSelect1
            // 
            this.dsPOInvSelect1.DataSetName = "dsPOInvSelect";
            this.dsPOInvSelect1.Locale = new System.Globalization.CultureInfo("en-US");
            this.dsPOInvSelect1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // riNoPOSelect
            // 
            this.riNoPOSelect.AutoHeight = false;
            this.riNoPOSelect.Name = "riNoPOSelect";
            this.riNoPOSelect.NullText = "";
            // 
            // riGLDescReadOnly
            // 
            this.riGLDescReadOnly.AutoHeight = false;
            this.riGLDescReadOnly.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject23, "", null, null, false)});
            this.riGLDescReadOnly.DataSource = this.bsGLAccts;
            this.riGLDescReadOnly.DisplayMember = "DESCRIPTION";
            this.riGLDescReadOnly.Name = "riGLDescReadOnly";
            this.riGLDescReadOnly.NullText = "";
            this.riGLDescReadOnly.PopupWidth = 250;
            this.riGLDescReadOnly.ReadOnly = true;
            this.riGLDescReadOnly.ValueMember = "ACCOUNT_NUMBER";
            // 
            // bsGLAccts
            // 
            this.bsGLAccts.DataMember = "GL_ACCOUNTS";
            this.bsGLAccts.DataSource = this.dsGLAccts1;
            // 
            // tpMatchPO
            // 
            this.tpMatchPO.Name = "tpMatchPO";
            this.tpMatchPO.Size = new System.Drawing.Size(1217, 240);
            this.tpMatchPO.Text = "Match PO Receipt";
            // 
            // tpContractPO
            // 
            this.tpContractPO.Name = "tpContractPO";
            this.tpContractPO.Size = new System.Drawing.Size(1217, 240);
            this.tpContractPO.Text = "Contract PO";
            // 
            // tpSummContractPO
            // 
            this.tpSummContractPO.Name = "tpSummContractPO";
            this.tpSummContractPO.Size = new System.Drawing.Size(1217, 240);
            this.tpSummContractPO.Text = "Summary Contract PO";
            // 
            // tpPWPLink
            // 
            this.tpPWPLink.Controls.Add(this.gcPWP);
            this.tpPWPLink.Name = "tpPWPLink";
            this.tpPWPLink.Size = new System.Drawing.Size(1217, 240);
            this.tpPWPLink.Text = "Paid When Paid Linking";
            // 
            // gcPWP
            // 
            this.gcPWP.DataMember = "AP_PWP_GetLinks";
            this.gcPWP.DataSource = this.dsAP_PWP_GetLinks1;
            this.gcPWP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPWP.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPWP.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcPWP.Location = new System.Drawing.Point(0, 0);
            this.gcPWP.MainView = this.gvPWP;
            this.gcPWP.Name = "gcPWP";
            this.gcPWP.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riPWPSelected});
            this.gcPWP.Size = new System.Drawing.Size(1217, 240);
            this.gcPWP.TabIndex = 0;
            this.gcPWP.UseEmbeddedNavigator = true;
            this.gcPWP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPWP});
            // 
            // dsAP_PWP_GetLinks1
            // 
            this.dsAP_PWP_GetLinks1.DataSetName = "dsAP_PWP_GetLinks";
            this.dsAP_PWP_GetLinks1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvPWP
            // 
            this.gvPWP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gvPWP.GridControl = this.gcPWP;
            this.gvPWP.Name = "gvPWP";
            this.gvPWP.OptionsView.ShowGroupPanel = false;
            this.gvPWP.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvPWP_InvalidRowException);
            this.gvPWP.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvPWP_ValidateRow);
            this.gvPWP.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvPWP_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "AR Invoice";
            this.gridColumn1.FieldName = "invoiceno";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Invoice Date";
            this.gridColumn2.FieldName = "inv_date";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Year";
            this.gridColumn3.FieldName = "acct_year";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Period";
            this.gridColumn4.FieldName = "acct_period";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Invoice Amount";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "inv_amt";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Allocated Amount";
            this.gridColumn6.DisplayFormat.FormatString = "n2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "allocated_amt";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Amount Available";
            this.gridColumn7.DisplayFormat.FormatString = "n2";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "amt_available";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Amount To Allocate";
            this.gridColumn8.DisplayFormat.FormatString = "n2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "amt_to_allocate";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Selected";
            this.gridColumn9.ColumnEdit = this.riPWPSelected;
            this.gridColumn9.FieldName = "selected";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // riPWPSelected
            // 
            this.riPWPSelected.AutoHeight = false;
            this.riPWPSelected.Name = "riPWPSelected";
            this.riPWPSelected.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // tpLevy
            // 
            this.tpLevy.Name = "tpLevy";
            this.tpLevy.Size = new System.Drawing.Size(1217, 240);
            this.tpLevy.Text = "Levy";
            // 
            // TR_Conn
            // 
            this.TR_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
    "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // daInvHeader
            // 
            this.daInvHeader.ContinueUpdateOnError = true;
            this.daInvHeader.DeleteCommand = this.sqlDeleteCommand7;
            this.daInvHeader.InsertCommand = this.sqlInsertCommand1;
            this.daInvHeader.SelectCommand = this.sqlSelectCommand1;
            this.daInvHeader.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
                        new System.Data.Common.DataColumnMapping("IS_BALANCED", "IS_BALANCED"),
                        new System.Data.Common.DataColumnMapping("AP_DIV", "AP_DIV"),
                        new System.Data.Common.DataColumnMapping("KC_CONTRACTPO_STATUS", "KC_CONTRACTPO_STATUS"),
                        new System.Data.Common.DataColumnMapping("KC_ACCRUAL_STATUS", "KC_ACCRUAL_STATUS"),
                        new System.Data.Common.DataColumnMapping("from_web", "from_web"),
                        new System.Data.Common.DataColumnMapping("WF_STATUS", "WF_STATUS"),
                        new System.Data.Common.DataColumnMapping("WF_Approval_ID", "WF_Approval_ID"),
                        new System.Data.Common.DataColumnMapping("IS_CB", "IS_CB"),
                        new System.Data.Common.DataColumnMapping("HAS_CB", "HAS_CB"),
                        new System.Data.Common.DataColumnMapping("TERMS_ID", "TERMS_ID"),
                        new System.Data.Common.DataColumnMapping("PAYMENT_HOLD", "PAYMENT_HOLD"),
                        new System.Data.Common.DataColumnMapping("Levy", "Levy")})});
            this.daInvHeader.UpdateCommand = this.sqlUpdateCommand7;
            // 
            // sqlDeleteCommand7
            // 
            this.sqlDeleteCommand7.CommandText = resources.GetString("sqlDeleteCommand7.CommandText");
            this.sqlDeleteCommand7.Connection = this.TR_Conn;
            this.sqlDeleteCommand7.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            new System.Data.SqlClient.SqlParameter("@IsNull_IS_BALANCED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IS_BALANCED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IS_BALANCED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IS_BALANCED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_DIV", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_DIV", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_CONTRACTPO_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_CONTRACTPO_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_CONTRACTPO_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_CONTRACTPO_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_ACCRUAL_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_ACCRUAL_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_ACCRUAL_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_ACCRUAL_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_from_web", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "from_web", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_from_web", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "from_web", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IS_CB", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IS_CB", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IS_CB", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IS_CB", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HAS_CB", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HAS_CB", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HAS_CB", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HAS_CB", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Levy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Levy", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Levy", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Levy", System.Data.DataRowVersion.Original, null)});
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
            new System.Data.SqlClient.SqlParameter("@IS_BALANCED", System.Data.SqlDbType.Char, 0, "IS_BALANCED"),
            new System.Data.SqlClient.SqlParameter("@AP_DIV", System.Data.SqlDbType.VarChar, 0, "AP_DIV"),
            new System.Data.SqlClient.SqlParameter("@KC_CONTRACTPO_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_CONTRACTPO_STATUS"),
            new System.Data.SqlClient.SqlParameter("@KC_ACCRUAL_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_ACCRUAL_STATUS"),
            new System.Data.SqlClient.SqlParameter("@from_web", System.Data.SqlDbType.VarChar, 0, "from_web"),
            new System.Data.SqlClient.SqlParameter("@WF_STATUS", System.Data.SqlDbType.VarChar, 0, "WF_STATUS"),
            new System.Data.SqlClient.SqlParameter("@WF_Approval_ID", System.Data.SqlDbType.Int, 0, "WF_Approval_ID"),
            new System.Data.SqlClient.SqlParameter("@IS_CB", System.Data.SqlDbType.VarChar, 0, "IS_CB"),
            new System.Data.SqlClient.SqlParameter("@HAS_CB", System.Data.SqlDbType.VarChar, 0, "HAS_CB"),
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, "PAYMENT_HOLD"),
            new System.Data.SqlClient.SqlParameter("@Levy", System.Data.SqlDbType.Bit, 0, "Levy")});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = resources.GetString("sqlSelectCommand1.CommandText");
            this.sqlSelectCommand1.Connection = this.TR_Conn;
            this.sqlSelectCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 500, "username")});
            // 
            // sqlUpdateCommand7
            // 
            this.sqlUpdateCommand7.CommandText = resources.GetString("sqlUpdateCommand7.CommandText");
            this.sqlUpdateCommand7.Connection = this.TR_Conn;
            this.sqlUpdateCommand7.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            new System.Data.SqlClient.SqlParameter("@IS_BALANCED", System.Data.SqlDbType.Char, 0, "IS_BALANCED"),
            new System.Data.SqlClient.SqlParameter("@AP_DIV", System.Data.SqlDbType.VarChar, 0, "AP_DIV"),
            new System.Data.SqlClient.SqlParameter("@KC_CONTRACTPO_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_CONTRACTPO_STATUS"),
            new System.Data.SqlClient.SqlParameter("@KC_ACCRUAL_STATUS", System.Data.SqlDbType.VarChar, 0, "KC_ACCRUAL_STATUS"),
            new System.Data.SqlClient.SqlParameter("@from_web", System.Data.SqlDbType.VarChar, 0, "from_web"),
            new System.Data.SqlClient.SqlParameter("@WF_STATUS", System.Data.SqlDbType.VarChar, 0, "WF_STATUS"),
            new System.Data.SqlClient.SqlParameter("@WF_Approval_ID", System.Data.SqlDbType.Int, 0, "WF_Approval_ID"),
            new System.Data.SqlClient.SqlParameter("@IS_CB", System.Data.SqlDbType.VarChar, 0, "IS_CB"),
            new System.Data.SqlClient.SqlParameter("@HAS_CB", System.Data.SqlDbType.VarChar, 0, "HAS_CB"),
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, "PAYMENT_HOLD"),
            new System.Data.SqlClient.SqlParameter("@Levy", System.Data.SqlDbType.Bit, 0, "Levy"),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_IS_BALANCED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IS_BALANCED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IS_BALANCED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IS_BALANCED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_DIV", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_DIV", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_DIV", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_CONTRACTPO_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_CONTRACTPO_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_CONTRACTPO_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_CONTRACTPO_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_KC_ACCRUAL_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "KC_ACCRUAL_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_KC_ACCRUAL_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "KC_ACCRUAL_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_from_web", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "from_web", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_from_web", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "from_web", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_STATUS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_STATUS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_STATUS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_STATUS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_IS_CB", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "IS_CB", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_IS_CB", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "IS_CB", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HAS_CB", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HAS_CB", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HAS_CB", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HAS_CB", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Levy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Levy", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Levy", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Levy", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // daAPSetupGL
            // 
            this.daAPSetupGL.DeleteCommand = this.sqlDeleteCommand1;
            this.daAPSetupGL.InsertCommand = this.sqlInsertCommand2;
            this.daAPSetupGL.SelectCommand = this.sqlSelectCommand2;
            this.daAPSetupGL.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_SETUP_GL", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("AP_SETUP_GL_ID", "AP_SETUP_GL_ID"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"),
                        new System.Data.Common.DataColumnMapping("GL_ACCOUNT", "GL_ACCOUNT")})});
            this.daAPSetupGL.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // sqlDeleteCommand1
            // 
            this.sqlDeleteCommand1.CommandText = resources.GetString("sqlDeleteCommand1.CommandText");
            this.sqlDeleteCommand1.Connection = this.TR_Conn;
            this.sqlDeleteCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand2
            // 
            this.sqlInsertCommand2.CommandText = resources.GetString("sqlInsertCommand2.CommandText");
            this.sqlInsertCommand2.Connection = this.TR_Conn;
            this.sqlInsertCommand2.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 20, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, "GL_ACCOUNT")});
            // 
            // sqlSelectCommand2
            // 
            this.sqlSelectCommand2.CommandText = "SELECT AP_SETUP_GL_ID, DESCRIPTION, GL_ACCOUNT FROM AP_SETUP_GL ORDER BY DESCRIPT" +
    "ION";
            this.sqlSelectCommand2.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand1
            // 
            this.sqlUpdateCommand1.CommandText = resources.GetString("sqlUpdateCommand1.CommandText");
            this.sqlUpdateCommand1.Connection = this.TR_Conn;
            this.sqlUpdateCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 20, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, "GL_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null)});
            // 
            // daCurrency
            // 
            this.daCurrency.DeleteCommand = this.sqlDeleteCommand2;
            this.daCurrency.InsertCommand = this.sqlInsertCommand3;
            this.daCurrency.SelectCommand = this.sqlSelectCommand3;
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
            // sqlInsertCommand3
            // 
            this.sqlInsertCommand3.CommandText = resources.GetString("sqlInsertCommand3.CommandText");
            this.sqlInsertCommand3.Connection = this.TR_Conn;
            this.sqlInsertCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 4, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_CODE", System.Data.SqlDbType.VarChar, 10, "CURRENCY_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 40, "DESCRIPTION")});
            // 
            // sqlSelectCommand3
            // 
            this.sqlSelectCommand3.CommandText = "SELECT CURRENCY_ID, CURRENCY_CODE, DESCRIPTION FROM CURRENCY ORDER BY DESCRIPTION" +
    "";
            this.sqlSelectCommand3.Connection = this.TR_Conn;
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
            // daGST
            // 
            this.daGST.DeleteCommand = this.sqlDeleteCommand3;
            this.daGST.InsertCommand = this.sqlInsertCommand4;
            this.daGST.SelectCommand = this.sqlSelectCommand4;
            this.daGST.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GST_CODES", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("GST_CODE", "GST_CODE"),
                        new System.Data.Common.DataColumnMapping("GST_DESC", "GST_DESC"),
                        new System.Data.Common.DataColumnMapping("GST_PCT", "GST_PCT")})});
            this.daGST.UpdateCommand = this.sqlUpdateCommand3;
            // 
            // sqlDeleteCommand3
            // 
            this.sqlDeleteCommand3.CommandText = "DELETE FROM GST_CODES WHERE (GST_CODE = @Original_GST_CODE) AND (GST_DESC = @Orig" +
    "inal_GST_DESC) AND (GST_PCT = @Original_GST_PCT)";
            this.sqlDeleteCommand3.Connection = this.TR_Conn;
            this.sqlDeleteCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_DESC", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_DESC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_PCT", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_PCT", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand4
            // 
            this.sqlInsertCommand4.CommandText = "INSERT INTO GST_CODES(GST_CODE, GST_DESC, GST_PCT) VALUES (@GST_CODE, @GST_DESC, " +
    "@GST_PCT); SELECT GST_CODE, GST_DESC, GST_PCT FROM GST_CODES WHERE (GST_CODE = @" +
    "GST_CODE) ORDER BY GST_DESC";
            this.sqlInsertCommand4.Connection = this.TR_Conn;
            this.sqlInsertCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.VarChar, 1, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@GST_DESC", System.Data.SqlDbType.VarChar, 30, "GST_DESC"),
            new System.Data.SqlClient.SqlParameter("@GST_PCT", System.Data.SqlDbType.Money, 8, "GST_PCT")});
            // 
            // sqlSelectCommand4
            // 
            this.sqlSelectCommand4.CommandText = "SELECT GST_CODE, GST_DESC, GST_PCT FROM GST_CODES ORDER BY GST_DESC";
            this.sqlSelectCommand4.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand3
            // 
            this.sqlUpdateCommand3.CommandText = resources.GetString("sqlUpdateCommand3.CommandText");
            this.sqlUpdateCommand3.Connection = this.TR_Conn;
            this.sqlUpdateCommand3.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GST_CODE", System.Data.SqlDbType.VarChar, 1, "GST_CODE"),
            new System.Data.SqlClient.SqlParameter("@GST_DESC", System.Data.SqlDbType.VarChar, 30, "GST_DESC"),
            new System.Data.SqlClient.SqlParameter("@GST_PCT", System.Data.SqlDbType.Money, 8, "GST_PCT"),
            new System.Data.SqlClient.SqlParameter("@Original_GST_CODE", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_DESC", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_DESC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_GST_PCT", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_PCT", System.Data.DataRowVersion.Original, null)});
            // 
            // daPST
            // 
            this.daPST.DeleteCommand = this.sqlDeleteCommand4;
            this.daPST.InsertCommand = this.sqlInsertCommand5;
            this.daPST.SelectCommand = this.sqlSelectCommand5;
            this.daPST.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SALES_TAXES", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SALES_TAX_ID", "SALES_TAX_ID"),
                        new System.Data.Common.DataColumnMapping("SALES_TAX_CODE", "SALES_TAX_CODE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"),
                        new System.Data.Common.DataColumnMapping("SALES_TAX", "SALES_TAX")})});
            this.daPST.UpdateCommand = this.sqlUpdateCommand4;
            // 
            // sqlDeleteCommand4
            // 
            this.sqlDeleteCommand4.CommandText = resources.GetString("sqlDeleteCommand4.CommandText");
            this.sqlDeleteCommand4.Connection = this.TR_Conn;
            this.sqlDeleteCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand5
            // 
            this.sqlInsertCommand5.CommandText = resources.GetString("sqlInsertCommand5.CommandText");
            this.sqlInsertCommand5.Connection = this.TR_Conn;
            this.sqlInsertCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 4, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, "SALES_TAX_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 30, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX", System.Data.SqlDbType.Float, 8, "SALES_TAX")});
            // 
            // sqlSelectCommand5
            // 
            this.sqlSelectCommand5.CommandText = "SELECT SALES_TAX_ID, SALES_TAX_CODE, DESCRIPTION, SALES_TAX FROM SALES_TAXES ORDE" +
    "R BY DESCRIPTION";
            this.sqlSelectCommand5.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand4
            // 
            this.sqlUpdateCommand4.CommandText = resources.GetString("sqlUpdateCommand4.CommandText");
            this.sqlUpdateCommand4.Connection = this.TR_Conn;
            this.sqlUpdateCommand4.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_ID", System.Data.SqlDbType.Int, 4, "SALES_TAX_ID"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, "SALES_TAX_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 30, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@SALES_TAX", System.Data.SqlDbType.Float, 8, "SALES_TAX"),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_CODE", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX", System.Data.SqlDbType.Float, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SALES_TAX_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SALES_TAX_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daTerms
            // 
            this.daTerms.DeleteCommand = this.sqlDeleteCommand5;
            this.daTerms.InsertCommand = this.sqlInsertCommand6;
            this.daTerms.SelectCommand = this.sqlSelectCommand6;
            this.daTerms.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "TERMS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("TERMS_ID", "TERMS_ID"),
                        new System.Data.Common.DataColumnMapping("TERM_CODE", "TERM_CODE"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daTerms.UpdateCommand = this.sqlUpdateCommand5;
            // 
            // sqlDeleteCommand5
            // 
            this.sqlDeleteCommand5.CommandText = "DELETE FROM TERMS WHERE (TERM_CODE = @Original_TERM_CODE) AND (DESCRIPTION = @Ori" +
    "ginal_DESCRIPTION OR @Original_DESCRIPTION IS NULL AND DESCRIPTION IS NULL) AND " +
    "(TERMS_ID = @Original_TERMS_ID)";
            this.sqlDeleteCommand5.Connection = this.TR_Conn;
            this.sqlDeleteCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_TERM_CODE", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERM_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand6
            // 
            this.sqlInsertCommand6.CommandText = "INSERT INTO TERMS(TERMS_ID, TERM_CODE, DESCRIPTION) VALUES (@TERMS_ID, @TERM_CODE" +
    ", @DESCRIPTION); SELECT TERMS_ID, TERM_CODE, DESCRIPTION FROM TERMS WHERE (TERM_" +
    "CODE = @TERM_CODE) ORDER BY DESCRIPTION";
            this.sqlInsertCommand6.Connection = this.TR_Conn;
            this.sqlInsertCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 4, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@TERM_CODE", System.Data.SqlDbType.VarChar, 10, "TERM_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 40, "DESCRIPTION")});
            // 
            // sqlSelectCommand6
            // 
            this.sqlSelectCommand6.CommandText = "SELECT TERMS_ID, TERM_CODE, DESCRIPTION FROM TERMS ORDER BY DESCRIPTION";
            this.sqlSelectCommand6.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand5
            // 
            this.sqlUpdateCommand5.CommandText = resources.GetString("sqlUpdateCommand5.CommandText");
            this.sqlUpdateCommand5.Connection = this.TR_Conn;
            this.sqlUpdateCommand5.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 4, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@TERM_CODE", System.Data.SqlDbType.VarChar, 10, "TERM_CODE"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 40, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_TERM_CODE", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERM_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daSupplier
            // 
            this.daSupplier.DeleteCommand = this.sqlDeleteCommand6;
            this.daSupplier.InsertCommand = this.sqlInsertCommand7;
            this.daSupplier.SelectCommand = this.sqlSelectCommand7;
            this.daSupplier.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "SUPPLIER_MASTER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("SUPPLIER", "SUPPLIER"),
                        new System.Data.Common.DataColumnMapping("NAME", "NAME")})});
            this.daSupplier.UpdateCommand = this.sqlUpdateCommand6;
            // 
            // sqlDeleteCommand6
            // 
            this.sqlDeleteCommand6.CommandText = "DELETE FROM SUPPLIER_MASTER WHERE (SUPPLIER = @Original_SUPPLIER) AND (NAME = @Or" +
    "iginal_NAME OR @Original_NAME IS NULL AND NAME IS NULL)";
            this.sqlDeleteCommand6.Connection = this.TR_Conn;
            this.sqlDeleteCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
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
            // sqlUpdateCommand6
            // 
            this.sqlUpdateCommand6.CommandText = resources.GetString("sqlUpdateCommand6.CommandText");
            this.sqlUpdateCommand6.Connection = this.TR_Conn;
            this.sqlUpdateCommand6.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@NAME", System.Data.SqlDbType.VarChar, 40, "NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_NAME", System.Data.SqlDbType.VarChar, 40, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daSwapSeg
            // 
            this.daSwapSeg.SelectCommand = this.sqlSelectCommand8;
            this.daSwapSeg.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GL_ACCOUNTS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Code", "Code"),
                        new System.Data.Common.DataColumnMapping("Description", "Description")})});
            // 
            // sqlSelectCommand8
            // 
            this.sqlSelectCommand8.CommandText = "SELECT DISTINCT a.SEG_2 AS Code, m.SEGMENT_DESC AS Description FROM GL_ACCOUNTS a" +
    " LEFT OUTER JOIN GL_SEGMENT_SETUP m ON m.SEGMENT_VALUE = a.SEG_2 AND m.SEGMENT_N" +
    "UMBER = 2 ORDER BY a.SEG_2";
            this.sqlSelectCommand8.Connection = this.TR_Conn;
            // 
            // daAllocSeg
            // 
            this.daAllocSeg.DeleteCommand = this.sqlDeleteCommand8;
            this.daAllocSeg.InsertCommand = this.sqlInsertCommand8;
            this.daAllocSeg.SelectCommand = this.sqlSelectCommand9;
            this.daAllocSeg.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GL_ALLOC", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("GL_ALLOC_ID", "GL_ALLOC_ID"),
                        new System.Data.Common.DataColumnMapping("GL_ALLOC_CODE", "GL_ALLOC_CODE"),
                        new System.Data.Common.DataColumnMapping("SEGMENT_NAME", "SEGMENT_NAME")})});
            this.daAllocSeg.UpdateCommand = this.sqlUpdateCommand8;
            // 
            // sqlDeleteCommand8
            // 
            this.sqlDeleteCommand8.CommandText = "DELETE FROM GL_ALLOC WHERE (GL_ALLOC_ID = @Original_GL_ALLOC_ID) AND (SEGMENT_NAM" +
    "E = @Original_SEGMENT_NAME OR @Original_SEGMENT_NAME IS NULL AND SEGMENT_NAME IS" +
    " NULL)";
            this.sqlDeleteCommand8.Connection = this.TR_Conn;
            this.sqlDeleteCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_GL_ALLOC_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ALLOC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SEGMENT_NAME", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEGMENT_NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand8
            // 
            this.sqlInsertCommand8.CommandText = resources.GetString("sqlInsertCommand8.CommandText");
            this.sqlInsertCommand8.Connection = this.TR_Conn;
            this.sqlInsertCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GL_ALLOC_ID", System.Data.SqlDbType.Int, 4, "GL_ALLOC_ID"),
            new System.Data.SqlClient.SqlParameter("@SEGMENT_NAME", System.Data.SqlDbType.VarChar, 6, "SEGMENT_NAME")});
            // 
            // sqlSelectCommand9
            // 
            this.sqlSelectCommand9.CommandText = "SELECT GL_ALLOC_ID, \'*\' + GL_ALLOC_CODE AS GL_ALLOC_CODE, SEGMENT_NAME FROM GL_AL" +
    "LOC ORDER BY GL_ALLOC_CODE";
            this.sqlSelectCommand9.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand8
            // 
            this.sqlUpdateCommand8.CommandText = resources.GetString("sqlUpdateCommand8.CommandText");
            this.sqlUpdateCommand8.Connection = this.TR_Conn;
            this.sqlUpdateCommand8.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@GL_ALLOC_ID", System.Data.SqlDbType.Int, 4, "GL_ALLOC_ID"),
            new System.Data.SqlClient.SqlParameter("@SEGMENT_NAME", System.Data.SqlDbType.VarChar, 6, "SEGMENT_NAME"),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ALLOC_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ALLOC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SEGMENT_NAME", System.Data.SqlDbType.VarChar, 6, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEGMENT_NAME", System.Data.DataRowVersion.Original, null)});
            // 
            // daPO
            // 
            this.daPO.SelectCommand = this.sqlSelectCommand10;
            this.daPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            // 
            // sqlSelectCommand10
            // 
            this.sqlSelectCommand10.CommandText = resources.GetString("sqlSelectCommand10.CommandText");
            this.sqlSelectCommand10.Connection = this.TR_Conn2;
            this.sqlSelectCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@supplier", System.Data.SqlDbType.VarChar, 10, "SUPPLIER")});
            // 
            // TR_Conn2
            // 
            this.TR_Conn2.ConnectionString = "Data Source=dev11;Initial Catalog=tr_strike_test10;Persist Security Info=True;Use" +
    "r ID=hmsqlsa;Password=hmsqlsa";
            this.TR_Conn2.FireInfoMessageEventOnUserErrors = false;
            // 
            // daInvDetail
            // 
            this.daInvDetail.DeleteCommand = this.sqlDeleteCommand10;
            this.daInvDetail.InsertCommand = this.sqlInsertCommand10;
            this.daInvDetail.SelectCommand = this.sqlSelectCommand11;
            this.daInvDetail.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
                        new System.Data.Common.DataColumnMapping("lv1id", "lv1id"),
                        new System.Data.Common.DataColumnMapping("lv2id", "lv2id"),
                        new System.Data.Common.DataColumnMapping("lv3id", "lv3id"),
                        new System.Data.Common.DataColumnMapping("lv4id", "lv4id"),
                        new System.Data.Common.DataColumnMapping("lem_comp", "lem_comp"),
                        new System.Data.Common.DataColumnMapping("pri_id", "pri_id"),
                        new System.Data.Common.DataColumnMapping("EXPENSE_TYPE", "EXPENSE_TYPE"),
                        new System.Data.Common.DataColumnMapping("HOURS", "HOURS"),
                        new System.Data.Common.DataColumnMapping("RATE", "RATE"),
                        new System.Data.Common.DataColumnMapping("CB_ID", "CB_ID"),
                        new System.Data.Common.DataColumnMapping("CB_REF", "CB_REF"),
                        new System.Data.Common.DataColumnMapping("billable", "billable"),
                        new System.Data.Common.DataColumnMapping("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID")})});
            this.daInvDetail.UpdateCommand = this.sqlUpdateCommand10;
            // 
            // sqlDeleteCommand10
            // 
            this.sqlDeleteCommand10.CommandText = resources.GetString("sqlDeleteCommand10.CommandText");
            this.sqlDeleteCommand10.Connection = this.TR_Conn2;
            this.sqlDeleteCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DOC_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DOC_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_NUMBER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_LINE_NO", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GL_ENTRIES_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GL_ENTRIES_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ENTRIES_LINE_NO", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ENTRIES_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GL_ACCOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMMENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMMENT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMMENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMMENT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_REC_ENTRY_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_REC_ENTRY_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ALLOC_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ALLOC_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ENTRIES_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ENTRIES_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SEQ", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEQ", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_PROJ_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_PROJ_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_RLH_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_RLH_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_RLH_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_RLH_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_LOT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_LOT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_LOT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_LOT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_BLOCK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_BLOCK", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_BLOCK", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_BLOCK", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_PLAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_PLAN", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_PLAN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_PLAN", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_JOB_COST_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_JOB_COST_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_AGREE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_AGREE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_INV_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_INV_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_PROF_CNTR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_PROF_CNTR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_PROF_CNTR", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_PROF_CNTR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_LEASE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_LEASE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_LEASE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_PROP_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_PROP_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_PROP_CD", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_PROP_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_PROF_CNTR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_PROF_CNTR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_PROF_CNTR", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_PROF_CNTR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_LEASE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_LEASE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_LEASE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_PROP_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_PROP_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_PROP_CD", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_PROP_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANSFER_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANSFER_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANSFER_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANSFER_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_num", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_num", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_num", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_num", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_phs_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "phs_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_phs_code", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "phs_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_subp_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "subp_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_subp_code", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "subp_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prp_component", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prp_component", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prp_component", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "prp_component", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prp_subcomp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prp_subcomp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prp_subcomp", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "prp_subcomp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ITC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ITC", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ITC", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ITC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_REC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_REC_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_REC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_REC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CLEAR_REQUIRED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CLEAR_REQUIRED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CLEAR_REQUIRED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CLEAR_REQUIRED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_3", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_4", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_5", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_6", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WCB_ASSESSABLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WCB_ASSESSABLE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WCB_ASSESSABLE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WCB_ASSESSABLE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SLS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SLS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SLS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SLS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_STAT_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_STAT_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_STAT_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_STAT_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMPANY_ALIAS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMPANY_ALIAS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMPANY_ALIAS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMPANY_ALIAS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INTER_CO_ACCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INTER_CO_ACCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INTER_CO_ACCT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INTER_CO_ACCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUBCONTRACTOR_TYPE_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUBCONTRACTOR_TYPE_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_STAT_APPLICABLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_STAT_APPLICABLE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_STAT_APPLICABLE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_STAT_APPLICABLE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_USE_COMM_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "USE_COMM_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_USE_COMM_PCT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "USE_COMM_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMMISSION_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMMISSION_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMMISSION_PCT", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMMISSION_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_RATE", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_REC_DETAIL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_REC_DETAIL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PROJ_BUD_EXCEEDED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PROJ_BUD_EXCEEDED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_unit_inv_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "unit_inv_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_unit_inv_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "unit_inv_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TIME_TICKET", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TIME_TICKET", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TIME_TICKET", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TIME_TICKET", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AFE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AFE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AFE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AFE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COST_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COST_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COST_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REFERENCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REFERENCE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_CHANGE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_FLOOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_FLOOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_FLOOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_SPACE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_SPACE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_SPACE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_SPACE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_FLOOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_FLOOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_UNIT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_UNIT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_UNIT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_UNIT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv1id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv1id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv1id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv1id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv2id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv2id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv2id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv2id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv3id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv3id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv3id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv3id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv4id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv4id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv4id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv4id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lem_comp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lem_comp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lem_comp", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lem_comp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXPENSE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXPENSE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXPENSE_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXPENSE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOURS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOURS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOURS", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOURS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RATE", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CB_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CB_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CB_REF", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CB_REF", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CB_REF", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CB_REF", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_billable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "billable", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_billable", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "billable", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand10
            // 
            this.sqlInsertCommand10.CommandText = resources.GetString("sqlInsertCommand10.CommandText");
            this.sqlInsertCommand10.Connection = this.TR_Conn2;
            this.sqlInsertCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@DOC_NO", System.Data.SqlDbType.VarChar, 0, "DOC_NO"),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_LINE_NO", System.Data.SqlDbType.SmallInt, 0, "JOURNAL_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@GL_ENTRIES_LINE_NO", System.Data.SqlDbType.SmallInt, 0, "GL_ENTRIES_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 0, "GL_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@COMMENT", System.Data.SqlDbType.VarChar, 0, "COMMENT"),
            new System.Data.SqlClient.SqlParameter("@AMOUNT", System.Data.SqlDbType.Money, 0, "AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, "AP_REC_ENTRY_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, "TRANS_TYPE"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ALLOC_CODE", System.Data.SqlDbType.VarChar, 0, "AP_GL_ALLOC_CODE"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, "AP_GL_ENTRIES_ID"),
            new System.Data.SqlClient.SqlParameter("@HOLD_AMT", System.Data.SqlDbType.Money, 0, "HOLD_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_BAL", System.Data.SqlDbType.Money, 0, "HOLD_BAL"),
            new System.Data.SqlClient.SqlParameter("@INV_BAL", System.Data.SqlDbType.Money, 0, "INV_BAL"),
            new System.Data.SqlClient.SqlParameter("@SEQ", System.Data.SqlDbType.Int, 0, "SEQ"),
            new System.Data.SqlClient.SqlParameter("@AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, "AGA_PROJ_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_RLH_CD", System.Data.SqlDbType.Int, 0, "AGA_RLH_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_LOT", System.Data.SqlDbType.Char, 0, "AGA_LOT"),
            new System.Data.SqlClient.SqlParameter("@AGA_BLOCK", System.Data.SqlDbType.Char, 0, "AGA_BLOCK"),
            new System.Data.SqlClient.SqlParameter("@AGA_PLAN", System.Data.SqlDbType.Char, 0, "AGA_PLAN"),
            new System.Data.SqlClient.SqlParameter("@AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, "AGA_JOB_COST_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, "AGA_AGREE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_INV_ID", System.Data.SqlDbType.Int, 0, "AGA_INV_ID"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_PROF_CNTR", System.Data.SqlDbType.Char, 0, "AGA_C_PROF_CNTR"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, "AGA_C_LEASE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_PROP_CD", System.Data.SqlDbType.Char, 0, "AGA_C_PROP_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_PROF_CNTR", System.Data.SqlDbType.Char, 0, "AGA_R_PROF_CNTR"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, "AGA_R_LEASE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_PROP_CD", System.Data.SqlDbType.Char, 0, "AGA_R_PROP_CD"),
            new System.Data.SqlClient.SqlParameter("@TRANSFER_FLAG", System.Data.SqlDbType.Char, 0, "TRANSFER_FLAG"),
            new System.Data.SqlClient.SqlParameter("@pri_num", System.Data.SqlDbType.Int, 0, "pri_num"),
            new System.Data.SqlClient.SqlParameter("@phs_code", System.Data.SqlDbType.Char, 0, "phs_code"),
            new System.Data.SqlClient.SqlParameter("@subp_code", System.Data.SqlDbType.Char, 0, "subp_code"),
            new System.Data.SqlClient.SqlParameter("@prp_component", System.Data.SqlDbType.Char, 0, "prp_component"),
            new System.Data.SqlClient.SqlParameter("@prp_subcomp", System.Data.SqlDbType.Char, 0, "prp_subcomp"),
            new System.Data.SqlClient.SqlParameter("@GST_AMT", System.Data.SqlDbType.Money, 0, "GST_AMT"),
            new System.Data.SqlClient.SqlParameter("@PURCH_AMT", System.Data.SqlDbType.Money, 0, "PURCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@ITC", System.Data.SqlDbType.Char, 0, "ITC"),
            new System.Data.SqlClient.SqlParameter("@GST_BAL", System.Data.SqlDbType.Money, 0, "GST_BAL"),
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO_TYPE", System.Data.SqlDbType.Char, 0, "PO_TYPE"),
            new System.Data.SqlClient.SqlParameter("@PO_REC_ID", System.Data.SqlDbType.Int, 0, "PO_REC_ID"),
            new System.Data.SqlClient.SqlParameter("@CLEAR_REQUIRED", System.Data.SqlDbType.Char, 0, "CLEAR_REQUIRED"),
            new System.Data.SqlClient.SqlParameter("@AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, "AP_INV_HEADER_ID"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, "AP_GL_ALLOC_ID"),
            new System.Data.SqlClient.SqlParameter("@SEG_1", System.Data.SqlDbType.VarChar, 0, "SEG_1"),
            new System.Data.SqlClient.SqlParameter("@SEG_2", System.Data.SqlDbType.VarChar, 0, "SEG_2"),
            new System.Data.SqlClient.SqlParameter("@SEG_3", System.Data.SqlDbType.VarChar, 0, "SEG_3"),
            new System.Data.SqlClient.SqlParameter("@SEG_4", System.Data.SqlDbType.VarChar, 0, "SEG_4"),
            new System.Data.SqlClient.SqlParameter("@SEG_5", System.Data.SqlDbType.VarChar, 0, "SEG_5"),
            new System.Data.SqlClient.SqlParameter("@SEG_6", System.Data.SqlDbType.VarChar, 0, "SEG_6"),
            new System.Data.SqlClient.SqlParameter("@WCB_ASSESSABLE", System.Data.SqlDbType.Char, 0, "WCB_ASSESSABLE"),
            new System.Data.SqlClient.SqlParameter("@SLS_ID", System.Data.SqlDbType.Int, 0, "SLS_ID"),
            new System.Data.SqlClient.SqlParameter("@COMM_STAT_FLAG", System.Data.SqlDbType.Char, 0, "COMM_STAT_FLAG"),
            new System.Data.SqlClient.SqlParameter("@COMPANY_ALIAS", System.Data.SqlDbType.VarChar, 0, "COMPANY_ALIAS"),
            new System.Data.SqlClient.SqlParameter("@INTER_CO_ACCT", System.Data.SqlDbType.VarChar, 0, "INTER_CO_ACCT"),
            new System.Data.SqlClient.SqlParameter("@SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, "SUBCONTRACTOR_TYPE_ID"),
            new System.Data.SqlClient.SqlParameter("@COMM_STAT_APPLICABLE", System.Data.SqlDbType.Char, 0, "COMM_STAT_APPLICABLE"),
            new System.Data.SqlClient.SqlParameter("@USE_COMM_PCT", System.Data.SqlDbType.Char, 0, "USE_COMM_PCT"),
            new System.Data.SqlClient.SqlParameter("@COMMISSION_PCT", System.Data.SqlDbType.Float, 0, "COMMISSION_PCT"),
            new System.Data.SqlClient.SqlParameter("@EXCH_RATE", System.Data.SqlDbType.Float, 0, "EXCH_RATE"),
            new System.Data.SqlClient.SqlParameter("@EXCH_AMT", System.Data.SqlDbType.Money, 0, "EXCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, "PO_REC_DETAIL_ID"),
            new System.Data.SqlClient.SqlParameter("@PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Char, 0, "PROJ_BUD_EXCEEDED"),
            new System.Data.SqlClient.SqlParameter("@unit_inv_id", System.Data.SqlDbType.Int, 0, "unit_inv_id"),
            new System.Data.SqlClient.SqlParameter("@TIME_TICKET", System.Data.SqlDbType.VarChar, 0, "TIME_TICKET"),
            new System.Data.SqlClient.SqlParameter("@AFE_NO", System.Data.SqlDbType.VarChar, 0, "AFE_NO"),
            new System.Data.SqlClient.SqlParameter("@COST_CODE", System.Data.SqlDbType.VarChar, 0, "COST_CODE"),
            new System.Data.SqlClient.SqlParameter("@SUB_CODE", System.Data.SqlDbType.VarChar, 0, "SUB_CODE"),
            new System.Data.SqlClient.SqlParameter("@REFERENCE", System.Data.SqlDbType.VarChar, 0, "REFERENCE"),
            new System.Data.SqlClient.SqlParameter("@SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, "SEG_CHANGE"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_FLOOR", System.Data.SqlDbType.VarChar, 0, "AGA_C_FLOOR"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_SPACE", System.Data.SqlDbType.VarChar, 0, "AGA_C_SPACE"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, "AGA_R_FLOOR"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_UNIT", System.Data.SqlDbType.VarChar, 0, "AGA_R_UNIT"),
            new System.Data.SqlClient.SqlParameter("@lv1id", System.Data.SqlDbType.Int, 0, "lv1id"),
            new System.Data.SqlClient.SqlParameter("@lv2id", System.Data.SqlDbType.Int, 0, "lv2id"),
            new System.Data.SqlClient.SqlParameter("@lv3id", System.Data.SqlDbType.Int, 0, "lv3id"),
            new System.Data.SqlClient.SqlParameter("@lv4id", System.Data.SqlDbType.Int, 0, "lv4id"),
            new System.Data.SqlClient.SqlParameter("@lem_comp", System.Data.SqlDbType.VarChar, 0, "lem_comp"),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 0, "pri_id"),
            new System.Data.SqlClient.SqlParameter("@EXPENSE_TYPE", System.Data.SqlDbType.VarChar, 0, "EXPENSE_TYPE"),
            new System.Data.SqlClient.SqlParameter("@HOURS", System.Data.SqlDbType.Money, 0, "HOURS"),
            new System.Data.SqlClient.SqlParameter("@RATE", System.Data.SqlDbType.Money, 0, "RATE"),
            new System.Data.SqlClient.SqlParameter("@CB_ID", System.Data.SqlDbType.Int, 0, "CB_ID"),
            new System.Data.SqlClient.SqlParameter("@CB_REF", System.Data.SqlDbType.VarChar, 0, "CB_REF"),
            new System.Data.SqlClient.SqlParameter("@billable", System.Data.SqlDbType.VarChar, 0, "billable"),
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID")});
            // 
            // sqlSelectCommand11
            // 
            this.sqlSelectCommand11.CommandText = resources.GetString("sqlSelectCommand11.CommandText");
            this.sqlSelectCommand11.Connection = this.TR_Conn2;
            this.sqlSelectCommand11.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ap_inv_header_id", System.Data.SqlDbType.Int, 4, "AP_INV_HEADER_ID")});
            // 
            // sqlUpdateCommand10
            // 
            this.sqlUpdateCommand10.CommandText = resources.GetString("sqlUpdateCommand10.CommandText");
            this.sqlUpdateCommand10.Connection = this.TR_Conn2;
            this.sqlUpdateCommand10.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 0, "SUPPLIER"),
            new System.Data.SqlClient.SqlParameter("@DOC_NO", System.Data.SqlDbType.VarChar, 0, "DOC_NO"),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@JOURNAL_LINE_NO", System.Data.SqlDbType.SmallInt, 0, "JOURNAL_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@GL_ENTRIES_LINE_NO", System.Data.SqlDbType.SmallInt, 0, "GL_ENTRIES_LINE_NO"),
            new System.Data.SqlClient.SqlParameter("@GL_ACCOUNT", System.Data.SqlDbType.VarChar, 0, "GL_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@COMMENT", System.Data.SqlDbType.VarChar, 0, "COMMENT"),
            new System.Data.SqlClient.SqlParameter("@AMOUNT", System.Data.SqlDbType.Money, 0, "AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, "AP_REC_ENTRY_NO"),
            new System.Data.SqlClient.SqlParameter("@TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, "TRANS_TYPE"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ALLOC_CODE", System.Data.SqlDbType.VarChar, 0, "AP_GL_ALLOC_CODE"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, "AP_GL_ENTRIES_ID"),
            new System.Data.SqlClient.SqlParameter("@HOLD_AMT", System.Data.SqlDbType.Money, 0, "HOLD_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_BAL", System.Data.SqlDbType.Money, 0, "HOLD_BAL"),
            new System.Data.SqlClient.SqlParameter("@INV_BAL", System.Data.SqlDbType.Money, 0, "INV_BAL"),
            new System.Data.SqlClient.SqlParameter("@SEQ", System.Data.SqlDbType.Int, 0, "SEQ"),
            new System.Data.SqlClient.SqlParameter("@AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, "AGA_PROJ_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_RLH_CD", System.Data.SqlDbType.Int, 0, "AGA_RLH_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_LOT", System.Data.SqlDbType.Char, 0, "AGA_LOT"),
            new System.Data.SqlClient.SqlParameter("@AGA_BLOCK", System.Data.SqlDbType.Char, 0, "AGA_BLOCK"),
            new System.Data.SqlClient.SqlParameter("@AGA_PLAN", System.Data.SqlDbType.Char, 0, "AGA_PLAN"),
            new System.Data.SqlClient.SqlParameter("@AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, "AGA_JOB_COST_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, "AGA_AGREE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_INV_ID", System.Data.SqlDbType.Int, 0, "AGA_INV_ID"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_PROF_CNTR", System.Data.SqlDbType.Char, 0, "AGA_C_PROF_CNTR"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, "AGA_C_LEASE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_PROP_CD", System.Data.SqlDbType.Char, 0, "AGA_C_PROP_CD"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_PROF_CNTR", System.Data.SqlDbType.Char, 0, "AGA_R_PROF_CNTR"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, "AGA_R_LEASE_NUM"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_PROP_CD", System.Data.SqlDbType.Char, 0, "AGA_R_PROP_CD"),
            new System.Data.SqlClient.SqlParameter("@TRANSFER_FLAG", System.Data.SqlDbType.Char, 0, "TRANSFER_FLAG"),
            new System.Data.SqlClient.SqlParameter("@pri_num", System.Data.SqlDbType.Int, 0, "pri_num"),
            new System.Data.SqlClient.SqlParameter("@phs_code", System.Data.SqlDbType.Char, 0, "phs_code"),
            new System.Data.SqlClient.SqlParameter("@subp_code", System.Data.SqlDbType.Char, 0, "subp_code"),
            new System.Data.SqlClient.SqlParameter("@prp_component", System.Data.SqlDbType.Char, 0, "prp_component"),
            new System.Data.SqlClient.SqlParameter("@prp_subcomp", System.Data.SqlDbType.Char, 0, "prp_subcomp"),
            new System.Data.SqlClient.SqlParameter("@GST_AMT", System.Data.SqlDbType.Money, 0, "GST_AMT"),
            new System.Data.SqlClient.SqlParameter("@PURCH_AMT", System.Data.SqlDbType.Money, 0, "PURCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@ITC", System.Data.SqlDbType.Char, 0, "ITC"),
            new System.Data.SqlClient.SqlParameter("@GST_BAL", System.Data.SqlDbType.Money, 0, "GST_BAL"),
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 0, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO_TYPE", System.Data.SqlDbType.Char, 0, "PO_TYPE"),
            new System.Data.SqlClient.SqlParameter("@PO_REC_ID", System.Data.SqlDbType.Int, 0, "PO_REC_ID"),
            new System.Data.SqlClient.SqlParameter("@CLEAR_REQUIRED", System.Data.SqlDbType.Char, 0, "CLEAR_REQUIRED"),
            new System.Data.SqlClient.SqlParameter("@AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, "AP_INV_HEADER_ID"),
            new System.Data.SqlClient.SqlParameter("@AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, "AP_GL_ALLOC_ID"),
            new System.Data.SqlClient.SqlParameter("@SEG_1", System.Data.SqlDbType.VarChar, 0, "SEG_1"),
            new System.Data.SqlClient.SqlParameter("@SEG_2", System.Data.SqlDbType.VarChar, 0, "SEG_2"),
            new System.Data.SqlClient.SqlParameter("@SEG_3", System.Data.SqlDbType.VarChar, 0, "SEG_3"),
            new System.Data.SqlClient.SqlParameter("@SEG_4", System.Data.SqlDbType.VarChar, 0, "SEG_4"),
            new System.Data.SqlClient.SqlParameter("@SEG_5", System.Data.SqlDbType.VarChar, 0, "SEG_5"),
            new System.Data.SqlClient.SqlParameter("@SEG_6", System.Data.SqlDbType.VarChar, 0, "SEG_6"),
            new System.Data.SqlClient.SqlParameter("@WCB_ASSESSABLE", System.Data.SqlDbType.Char, 0, "WCB_ASSESSABLE"),
            new System.Data.SqlClient.SqlParameter("@SLS_ID", System.Data.SqlDbType.Int, 0, "SLS_ID"),
            new System.Data.SqlClient.SqlParameter("@COMM_STAT_FLAG", System.Data.SqlDbType.Char, 0, "COMM_STAT_FLAG"),
            new System.Data.SqlClient.SqlParameter("@COMPANY_ALIAS", System.Data.SqlDbType.VarChar, 0, "COMPANY_ALIAS"),
            new System.Data.SqlClient.SqlParameter("@INTER_CO_ACCT", System.Data.SqlDbType.VarChar, 0, "INTER_CO_ACCT"),
            new System.Data.SqlClient.SqlParameter("@SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, "SUBCONTRACTOR_TYPE_ID"),
            new System.Data.SqlClient.SqlParameter("@COMM_STAT_APPLICABLE", System.Data.SqlDbType.Char, 0, "COMM_STAT_APPLICABLE"),
            new System.Data.SqlClient.SqlParameter("@USE_COMM_PCT", System.Data.SqlDbType.Char, 0, "USE_COMM_PCT"),
            new System.Data.SqlClient.SqlParameter("@COMMISSION_PCT", System.Data.SqlDbType.Float, 0, "COMMISSION_PCT"),
            new System.Data.SqlClient.SqlParameter("@EXCH_RATE", System.Data.SqlDbType.Float, 0, "EXCH_RATE"),
            new System.Data.SqlClient.SqlParameter("@EXCH_AMT", System.Data.SqlDbType.Money, 0, "EXCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, "PO_REC_DETAIL_ID"),
            new System.Data.SqlClient.SqlParameter("@PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Char, 0, "PROJ_BUD_EXCEEDED"),
            new System.Data.SqlClient.SqlParameter("@unit_inv_id", System.Data.SqlDbType.Int, 0, "unit_inv_id"),
            new System.Data.SqlClient.SqlParameter("@TIME_TICKET", System.Data.SqlDbType.VarChar, 0, "TIME_TICKET"),
            new System.Data.SqlClient.SqlParameter("@AFE_NO", System.Data.SqlDbType.VarChar, 0, "AFE_NO"),
            new System.Data.SqlClient.SqlParameter("@COST_CODE", System.Data.SqlDbType.VarChar, 0, "COST_CODE"),
            new System.Data.SqlClient.SqlParameter("@SUB_CODE", System.Data.SqlDbType.VarChar, 0, "SUB_CODE"),
            new System.Data.SqlClient.SqlParameter("@REFERENCE", System.Data.SqlDbType.VarChar, 0, "REFERENCE"),
            new System.Data.SqlClient.SqlParameter("@SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, "SEG_CHANGE"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_FLOOR", System.Data.SqlDbType.VarChar, 0, "AGA_C_FLOOR"),
            new System.Data.SqlClient.SqlParameter("@AGA_C_SPACE", System.Data.SqlDbType.VarChar, 0, "AGA_C_SPACE"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, "AGA_R_FLOOR"),
            new System.Data.SqlClient.SqlParameter("@AGA_R_UNIT", System.Data.SqlDbType.VarChar, 0, "AGA_R_UNIT"),
            new System.Data.SqlClient.SqlParameter("@lv1id", System.Data.SqlDbType.Int, 0, "lv1id"),
            new System.Data.SqlClient.SqlParameter("@lv2id", System.Data.SqlDbType.Int, 0, "lv2id"),
            new System.Data.SqlClient.SqlParameter("@lv3id", System.Data.SqlDbType.Int, 0, "lv3id"),
            new System.Data.SqlClient.SqlParameter("@lv4id", System.Data.SqlDbType.Int, 0, "lv4id"),
            new System.Data.SqlClient.SqlParameter("@lem_comp", System.Data.SqlDbType.VarChar, 0, "lem_comp"),
            new System.Data.SqlClient.SqlParameter("@pri_id", System.Data.SqlDbType.Int, 0, "pri_id"),
            new System.Data.SqlClient.SqlParameter("@EXPENSE_TYPE", System.Data.SqlDbType.VarChar, 0, "EXPENSE_TYPE"),
            new System.Data.SqlClient.SqlParameter("@HOURS", System.Data.SqlDbType.Money, 0, "HOURS"),
            new System.Data.SqlClient.SqlParameter("@RATE", System.Data.SqlDbType.Money, 0, "RATE"),
            new System.Data.SqlClient.SqlParameter("@CB_ID", System.Data.SqlDbType.Int, 0, "CB_ID"),
            new System.Data.SqlClient.SqlParameter("@CB_REF", System.Data.SqlDbType.VarChar, 0, "CB_REF"),
            new System.Data.SqlClient.SqlParameter("@billable", System.Data.SqlDbType.VarChar, 0, "billable"),
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID"),
            new System.Data.SqlClient.SqlParameter("@Original_SUPPLIER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUPPLIER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DOC_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DOC_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_NUMBER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_NUMBER", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, false, ((byte)(15)), ((byte)(4)), "JOURNAL_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_JOURNAL_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_JOURNAL_LINE_NO", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "JOURNAL_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GL_ENTRIES_LINE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GL_ENTRIES_LINE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ENTRIES_LINE_NO", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ENTRIES_LINE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GL_ACCOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GL_ACCOUNT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GL_ACCOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMMENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMMENT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMMENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMMENT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_REC_ENTRY_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_REC_ENTRY_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_REC_ENTRY_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANS_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANS_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANS_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ALLOC_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ALLOC_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ENTRIES_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ENTRIES_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ENTRIES_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_SEQ", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEQ", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_PROJ_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_PROJ_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_PROJ_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_RLH_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_RLH_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_RLH_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_RLH_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_LOT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_LOT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_LOT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_LOT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_BLOCK", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_BLOCK", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_BLOCK", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_BLOCK", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_PLAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_PLAN", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_PLAN", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_PLAN", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_JOB_COST_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_JOB_COST_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_JOB_COST_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_AGREE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_AGREE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_AGREE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_INV_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_INV_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_INV_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_PROF_CNTR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_PROF_CNTR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_PROF_CNTR", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_PROF_CNTR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_LEASE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_LEASE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_LEASE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_PROP_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_PROP_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_PROP_CD", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_PROP_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_PROF_CNTR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_PROF_CNTR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_PROF_CNTR", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_PROF_CNTR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_LEASE_NUM", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_LEASE_NUM", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_LEASE_NUM", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_LEASE_NUM", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_PROP_CD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_PROP_CD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_PROP_CD", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_PROP_CD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TRANSFER_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TRANSFER_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TRANSFER_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TRANSFER_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_num", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_num", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_num", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_num", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_phs_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "phs_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_phs_code", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "phs_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_subp_code", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "subp_code", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_subp_code", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "subp_code", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prp_component", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prp_component", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prp_component", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "prp_component", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_prp_subcomp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "prp_subcomp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_prp_subcomp", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "prp_subcomp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ITC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ITC", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ITC", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ITC", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_GST_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "GST_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_GST_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "GST_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_REC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_REC_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_REC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_REC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CLEAR_REQUIRED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CLEAR_REQUIRED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CLEAR_REQUIRED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CLEAR_REQUIRED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_GL_ALLOC_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_GL_ALLOC_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_1", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_1", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_1", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_1", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_2", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_2", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_2", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_2", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_3", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_3", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_3", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_3", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_4", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_4", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_4", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_4", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_5", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_5", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_5", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_5", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_6", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_6", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_6", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_6", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WCB_ASSESSABLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WCB_ASSESSABLE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WCB_ASSESSABLE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WCB_ASSESSABLE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SLS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SLS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SLS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SLS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_STAT_FLAG", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_STAT_FLAG", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_STAT_FLAG", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_STAT_FLAG", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMPANY_ALIAS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMPANY_ALIAS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMPANY_ALIAS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMPANY_ALIAS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INTER_CO_ACCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INTER_CO_ACCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INTER_CO_ACCT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INTER_CO_ACCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUBCONTRACTOR_TYPE_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUBCONTRACTOR_TYPE_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUBCONTRACTOR_TYPE_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMM_STAT_APPLICABLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMM_STAT_APPLICABLE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMM_STAT_APPLICABLE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMM_STAT_APPLICABLE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_USE_COMM_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "USE_COMM_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_USE_COMM_PCT", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "USE_COMM_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COMMISSION_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COMMISSION_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COMMISSION_PCT", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COMMISSION_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_RATE", System.Data.SqlDbType.Float, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PO_REC_DETAIL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PO_REC_DETAIL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_REC_DETAIL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PROJ_BUD_EXCEEDED", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PROJ_BUD_EXCEEDED", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PROJ_BUD_EXCEEDED", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_unit_inv_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "unit_inv_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_unit_inv_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "unit_inv_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TIME_TICKET", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TIME_TICKET", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TIME_TICKET", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TIME_TICKET", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AFE_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AFE_NO", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AFE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AFE_NO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_COST_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "COST_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_COST_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "COST_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SUB_CODE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SUB_CODE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SUB_CODE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SUB_CODE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_REFERENCE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_REFERENCE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "REFERENCE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_SEG_CHANGE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_SEG_CHANGE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "SEG_CHANGE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_FLOOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_FLOOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_FLOOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_C_SPACE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_C_SPACE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_C_SPACE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_C_SPACE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_FLOOR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_FLOOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_FLOOR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AGA_R_UNIT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AGA_R_UNIT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AGA_R_UNIT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AGA_R_UNIT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv1id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv1id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv1id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv1id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv2id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv2id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv2id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv2id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv3id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv3id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv3id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv3id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lv4id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lv4id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lv4id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lv4id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_lem_comp", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "lem_comp", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_lem_comp", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lem_comp", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_pri_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "pri_id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_EXPENSE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EXPENSE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_EXPENSE_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "EXPENSE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOURS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOURS", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOURS", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOURS", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_RATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_RATE", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "RATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CB_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CB_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CB_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CB_REF", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CB_REF", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CB_REF", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CB_REF", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_billable", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "billable", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_billable", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "billable", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daGLAccts
            // 
            this.daGLAccts.DeleteCommand = this.sqlDeleteCommand11;
            this.daGLAccts.InsertCommand = this.sqlInsertCommand11;
            this.daGLAccts.SelectCommand = this.sqlSelectCommand12;
            this.daGLAccts.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "GL_ACCOUNTS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("ACCOUNT_NUMBER", "ACCOUNT_NUMBER"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daGLAccts.UpdateCommand = this.sqlUpdateCommand11;
            // 
            // sqlDeleteCommand11
            // 
            this.sqlDeleteCommand11.CommandText = "DELETE FROM GL_ACCOUNTS WHERE (ACCOUNT_NUMBER = @Original_ACCOUNT_NUMBER) AND (DE" +
    "SCRIPTION = @Original_DESCRIPTION)";
            this.sqlDeleteCommand11.Connection = this.TR_Conn;
            this.sqlDeleteCommand11.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCOUNT_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand11
            // 
            this.sqlInsertCommand11.CommandText = "INSERT INTO GL_ACCOUNTS(ACCOUNT_NUMBER, DESCRIPTION) VALUES (@ACCOUNT_NUMBER, @DE" +
    "SCRIPTION); SELECT ACCOUNT_NUMBER, DESCRIPTION FROM GL_ACCOUNTS WHERE (ACCOUNT_N" +
    "UMBER = @ACCOUNT_NUMBER)";
            this.sqlInsertCommand11.Connection = this.TR_Conn;
            this.sqlInsertCommand11.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, "ACCOUNT_NUMBER"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 50, "DESCRIPTION")});
            // 
            // sqlSelectCommand12
            // 
            this.sqlSelectCommand12.CommandText = "SELECT ACCOUNT_NUMBER, DESCRIPTION FROM GL_ACCOUNTS";
            this.sqlSelectCommand12.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand11
            // 
            this.sqlUpdateCommand11.CommandText = resources.GetString("sqlUpdateCommand11.CommandText");
            this.sqlUpdateCommand11.Connection = this.TR_Conn;
            this.sqlUpdateCommand11.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, "ACCOUNT_NUMBER"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 50, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_ACCOUNT_NUMBER", System.Data.SqlDbType.VarChar, 21, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCOUNT_NUMBER", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daAllPO
            // 
            this.daAllPO.DeleteCommand = this.sqlDeleteCommand12;
            this.daAllPO.InsertCommand = this.sqlInsertCommand12;
            this.daAllPO.SelectCommand = this.sqlSelectCommand13;
            this.daAllPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            this.daAllPO.UpdateCommand = this.sqlUpdateCommand12;
            // 
            // sqlDeleteCommand12
            // 
            this.sqlDeleteCommand12.CommandText = "DELETE FROM PO_HEADER WHERE (PO = @Original_PO) AND (ORDER_DATE = @Original_ORDER" +
    "_DATE OR @Original_ORDER_DATE IS NULL AND ORDER_DATE IS NULL) AND (PO_ID = @Orig" +
    "inal_PO_ID)";
            this.sqlDeleteCommand12.Connection = this.TR_Conn;
            this.sqlDeleteCommand12.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlInsertCommand12
            // 
            this.sqlInsertCommand12.CommandText = "INSERT INTO PO_HEADER(PO_ID, PO, ORDER_DATE) VALUES (@PO_ID, @PO, @ORDER_DATE); S" +
    "ELECT PO_ID, PO, ORDER_DATE FROM PO_HEADER WHERE (PO = @PO) ORDER BY PO";
            this.sqlInsertCommand12.Connection = this.TR_Conn;
            this.sqlInsertCommand12.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 4, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 20, "PO"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 8, "ORDER_DATE")});
            // 
            // sqlSelectCommand13
            // 
            this.sqlSelectCommand13.CommandText = "SELECT PO_ID, PO, ORDER_DATE FROM PO_HEADER ORDER BY PO";
            this.sqlSelectCommand13.Connection = this.TR_Conn;
            // 
            // sqlUpdateCommand12
            // 
            this.sqlUpdateCommand12.CommandText = resources.GetString("sqlUpdateCommand12.CommandText");
            this.sqlUpdateCommand12.Connection = this.TR_Conn;
            this.sqlUpdateCommand12.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@PO_ID", System.Data.SqlDbType.Int, 4, "PO_ID"),
            new System.Data.SqlClient.SqlParameter("@PO", System.Data.SqlDbType.VarChar, 20, "PO"),
            new System.Data.SqlClient.SqlParameter("@ORDER_DATE", System.Data.SqlDbType.DateTime, 8, "ORDER_DATE"),
            new System.Data.SqlClient.SqlParameter("@Original_PO", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_ORDER_DATE", System.Data.SqlDbType.DateTime, 8, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ORDER_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Original_PO_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PO_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // daHeaderSide
            // 
            this.daHeaderSide.DeleteCommand = this.sqlDeleteCommand9;
            this.daHeaderSide.SelectCommand = this.sqlSelectCommand14;
            this.daHeaderSide.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_INV_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("id", "id"),
                        new System.Data.Common.DataColumnMapping("AP_INV_HEADER_ID", "AP_INV_HEADER_ID"),
                        new System.Data.Common.DataColumnMapping("AP_SETUP_GL_ID", "AP_SETUP_GL_ID"),
                        new System.Data.Common.DataColumnMapping("INVOICE_TYPE", "INVOICE_TYPE"),
                        new System.Data.Common.DataColumnMapping("CURRENCY_ID", "CURRENCY_ID"),
                        new System.Data.Common.DataColumnMapping("DISCOUNT_AMOUNT", "DISCOUNT_AMOUNT"),
                        new System.Data.Common.DataColumnMapping("DISCOUNT_DATE", "DISCOUNT_DATE"),
                        new System.Data.Common.DataColumnMapping("HOLD_PCT", "HOLD_PCT"),
                        new System.Data.Common.DataColumnMapping("HOLD_AMT", "HOLD_AMT"),
                        new System.Data.Common.DataColumnMapping("HOLD_PAY_DATE", "HOLD_PAY_DATE"),
                        new System.Data.Common.DataColumnMapping("HOLD", "HOLD"),
                        new System.Data.Common.DataColumnMapping("PAYMENT_HOLD", "PAYMENT_HOLD"),
                        new System.Data.Common.DataColumnMapping("SUPP_NAME", "SUPP_NAME"),
                        new System.Data.Common.DataColumnMapping("REMITADD1", "REMITADD1"),
                        new System.Data.Common.DataColumnMapping("REMITADD2", "REMITADD2"),
                        new System.Data.Common.DataColumnMapping("REMITADD3", "REMITADD3"),
                        new System.Data.Common.DataColumnMapping("REMITCITY", "REMITCITY"),
                        new System.Data.Common.DataColumnMapping("REMITSTATE", "REMITSTATE"),
                        new System.Data.Common.DataColumnMapping("REMITZIP", "REMITZIP"),
                        new System.Data.Common.DataColumnMapping("SUPP_ACCOUNT", "SUPP_ACCOUNT"),
                        new System.Data.Common.DataColumnMapping("COMMENT", "COMMENT"),
                        new System.Data.Common.DataColumnMapping("ACCT_YEAR", "ACCT_YEAR"),
                        new System.Data.Common.DataColumnMapping("ACCT_PERIOD", "ACCT_PERIOD"),
                        new System.Data.Common.DataColumnMapping("PURCH_AMT", "PURCH_AMT"),
                        new System.Data.Common.DataColumnMapping("INV_AMOUNT", "INV_AMOUNT"),
                        new System.Data.Common.DataColumnMapping("HOLD_BAL", "HOLD_BAL"),
                        new System.Data.Common.DataColumnMapping("WF_Approval_ID", "WF_Approval_ID"),
                        new System.Data.Common.DataColumnMapping("TERMS_ID", "TERMS_ID")})});
            this.daHeaderSide.UpdateCommand = this.sqlUpdateCommand9;
            // 
            // sqlDeleteCommand9
            // 
            this.sqlDeleteCommand9.CommandText = resources.GetString("sqlDeleteCommand9.CommandText");
            this.sqlDeleteCommand9.Connection = this.TR_Conn2;
            this.sqlDeleteCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INVOICE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INVOICE_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PCT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PAY_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_YEAR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_PERIOD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlSelectCommand14
            // 
            this.sqlSelectCommand14.CommandText = resources.GetString("sqlSelectCommand14.CommandText");
            this.sqlSelectCommand14.Connection = this.TR_Conn2;
            this.sqlSelectCommand14.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@ap_inv_header_id", System.Data.SqlDbType.Int, 4, "AP_INV_HEADER_ID")});
            // 
            // sqlUpdateCommand9
            // 
            this.sqlUpdateCommand9.CommandText = resources.GetString("sqlUpdateCommand9.CommandText");
            this.sqlUpdateCommand9.Connection = this.TR_Conn2;
            this.sqlUpdateCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, "AP_INV_HEADER_ID"),
            new System.Data.SqlClient.SqlParameter("@AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, "AP_SETUP_GL_ID"),
            new System.Data.SqlClient.SqlParameter("@INVOICE_TYPE", System.Data.SqlDbType.Char, 0, "INVOICE_TYPE"),
            new System.Data.SqlClient.SqlParameter("@CURRENCY_ID", System.Data.SqlDbType.Int, 0, "CURRENCY_ID"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, "DISCOUNT_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, "DISCOUNT_DATE"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PCT", System.Data.SqlDbType.Money, 0, "HOLD_PCT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_AMT", System.Data.SqlDbType.Money, 0, "HOLD_AMT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, "HOLD_PAY_DATE"),
            new System.Data.SqlClient.SqlParameter("@HOLD", System.Data.SqlDbType.VarChar, 0, "HOLD"),
            new System.Data.SqlClient.SqlParameter("@PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, "PAYMENT_HOLD"),
            new System.Data.SqlClient.SqlParameter("@SUPP_NAME", System.Data.SqlDbType.VarChar, 0, "SUPP_NAME"),
            new System.Data.SqlClient.SqlParameter("@REMITADD1", System.Data.SqlDbType.VarChar, 0, "REMITADD1"),
            new System.Data.SqlClient.SqlParameter("@REMITADD2", System.Data.SqlDbType.VarChar, 0, "REMITADD2"),
            new System.Data.SqlClient.SqlParameter("@REMITADD3", System.Data.SqlDbType.VarChar, 0, "REMITADD3"),
            new System.Data.SqlClient.SqlParameter("@REMITCITY", System.Data.SqlDbType.VarChar, 0, "REMITCITY"),
            new System.Data.SqlClient.SqlParameter("@REMITSTATE", System.Data.SqlDbType.VarChar, 0, "REMITSTATE"),
            new System.Data.SqlClient.SqlParameter("@REMITZIP", System.Data.SqlDbType.VarChar, 0, "REMITZIP"),
            new System.Data.SqlClient.SqlParameter("@SUPP_ACCOUNT", System.Data.SqlDbType.VarChar, 0, "SUPP_ACCOUNT"),
            new System.Data.SqlClient.SqlParameter("@COMMENT", System.Data.SqlDbType.Text, 0, "COMMENT"),
            new System.Data.SqlClient.SqlParameter("@ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, "ACCT_YEAR"),
            new System.Data.SqlClient.SqlParameter("@ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, "ACCT_PERIOD"),
            new System.Data.SqlClient.SqlParameter("@PURCH_AMT", System.Data.SqlDbType.Money, 0, "PURCH_AMT"),
            new System.Data.SqlClient.SqlParameter("@INV_AMOUNT", System.Data.SqlDbType.Money, 0, "INV_AMOUNT"),
            new System.Data.SqlClient.SqlParameter("@HOLD_BAL", System.Data.SqlDbType.Money, 0, "HOLD_BAL"),
            new System.Data.SqlClient.SqlParameter("@WF_Approval_ID", System.Data.SqlDbType.Int, 0, "WF_Approval_ID"),
            new System.Data.SqlClient.SqlParameter("@TERMS_ID", System.Data.SqlDbType.Int, 0, "TERMS_ID"),
            new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "id", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_INV_HEADER_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_INV_HEADER_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_AP_SETUP_GL_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AP_SETUP_GL_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INVOICE_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INVOICE_TYPE", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INVOICE_TYPE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_CURRENCY_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "CURRENCY_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DISCOUNT_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DISCOUNT_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DISCOUNT_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PCT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PCT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PCT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_PAY_DATE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_PAY_DATE", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_PAY_DATE", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PAYMENT_HOLD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PAYMENT_HOLD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PAYMENT_HOLD", System.Data.DataRowVersion.Original, null),
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
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_YEAR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_YEAR", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_YEAR", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_ACCT_PERIOD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_ACCT_PERIOD", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "ACCT_PERIOD", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_PURCH_AMT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_PURCH_AMT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "PURCH_AMT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_INV_AMOUNT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_INV_AMOUNT", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "INV_AMOUNT", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_HOLD_BAL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_HOLD_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "HOLD_BAL", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_WF_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "WF_Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_TERMS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "TERMS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id")});
            // 
            // daPOSelect
            // 
            this.daPOSelect.SelectCommand = this.sqlSelectCommand15;
            this.daPOSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_REC_ID", "PO_REC_ID"),
                        new System.Data.Common.DataColumnMapping("PO #", "PO #"),
                        new System.Data.Common.DataColumnMapping("Supplier Name", "Supplier Name"),
                        new System.Data.Common.DataColumnMapping("PO Amount", "PO Amount")})});
            // 
            // sqlSelectCommand15
            // 
            this.sqlSelectCommand15.CommandText = resources.GetString("sqlSelectCommand15.CommandText");
            this.sqlSelectCommand15.Connection = this.TR_Conn;
            // 
            // daPOFSelect
            // 
            this.daPOFSelect.SelectCommand = this.sqlSelectCommand16;
            this.daPOFSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
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
            // daPOBSelect
            // 
            this.daPOBSelect.SelectCommand = this.sqlSelectCommand17;
            this.daPOBSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
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
            // sqlSelectCommand17
            // 
            this.sqlSelectCommand17.CommandText = resources.GetString("sqlSelectCommand17.CommandText");
            this.sqlSelectCommand17.Connection = this.TR_Conn;
            // 
            // daPOMSelect
            // 
            this.daPOMSelect.SelectCommand = this.sqlSelectCommand18;
            this.daPOMSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
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
            // sqlSelectCommand18
            // 
            this.sqlSelectCommand18.CommandText = resources.GetString("sqlSelectCommand18.CommandText");
            this.sqlSelectCommand18.Connection = this.TR_Conn;
            // 
            // daPODSelect
            // 
            this.daPODSelect.SelectCommand = this.sqlSelectCommand19;
            this.daPODSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
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
            // sqlSelectCommand19
            // 
            this.sqlSelectCommand19.CommandText = resources.GetString("sqlSelectCommand19.CommandText");
            this.sqlSelectCommand19.Connection = this.TR_Conn;
            // 
            // daPOM2Select
            // 
            this.daPOM2Select.SelectCommand = this.sqlSelectCommand20;
            this.daPOM2Select.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
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
            // sqlSelectCommand20
            // 
            this.sqlSelectCommand20.CommandText = resources.GetString("sqlSelectCommand20.CommandText");
            this.sqlSelectCommand20.Connection = this.TR_Conn;
            // 
            // daPOInvSelect
            // 
            this.daPOInvSelect.InsertCommand = this.sqlInsertCommand13;
            this.daPOInvSelect.SelectCommand = this.sqlSelectCommand21;
            this.daPOInvSelect.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "working_PO_Inv_Select", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("username", "username"),
                        new System.Data.Common.DataColumnMapping("po_rec_id", "po_rec_id"),
                        new System.Data.Common.DataColumnMapping("po", "po"),
                        new System.Data.Common.DataColumnMapping("receiver_number", "receiver_number"),
                        new System.Data.Common.DataColumnMapping("receipt_date", "receipt_date"),
                        new System.Data.Common.DataColumnMapping("receive_amt", "receive_amt"),
                        new System.Data.Common.DataColumnMapping("outstanding_amt", "outstanding_amt")})});
            // 
            // sqlInsertCommand13
            // 
            this.sqlInsertCommand13.CommandText = resources.GetString("sqlInsertCommand13.CommandText");
            this.sqlInsertCommand13.Connection = this.TR_Conn;
            this.sqlInsertCommand13.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 50, "username"),
            new System.Data.SqlClient.SqlParameter("@po_rec_id", System.Data.SqlDbType.Int, 4, "po_rec_id"),
            new System.Data.SqlClient.SqlParameter("@po", System.Data.SqlDbType.VarChar, 20, "po"),
            new System.Data.SqlClient.SqlParameter("@receiver_number", System.Data.SqlDbType.VarChar, 10, "receiver_number"),
            new System.Data.SqlClient.SqlParameter("@receipt_date", System.Data.SqlDbType.DateTime, 8, "receipt_date"),
            new System.Data.SqlClient.SqlParameter("@receive_amt", System.Data.SqlDbType.Money, 8, "receive_amt"),
            new System.Data.SqlClient.SqlParameter("@outstanding_amt", System.Data.SqlDbType.Money, 8, "outstanding_amt")});
            // 
            // sqlSelectCommand21
            // 
            this.sqlSelectCommand21.CommandText = "SELECT username, po_rec_id, po, receiver_number, receipt_date, receive_amt, outst" +
    "anding_amt FROM working_PO_Inv_Select WHERE (username = @username)";
            this.sqlSelectCommand21.Connection = this.TR_Conn;
            this.sqlSelectCommand21.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@username", System.Data.SqlDbType.VarChar, 50, "username")});
            // 
            // daDetPO
            // 
            this.daDetPO.SelectCommand = this.sqlSelectCommand22;
            this.daDetPO.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "PO_HEADER", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PO_ID", "PO_ID"),
                        new System.Data.Common.DataColumnMapping("PO", "PO"),
                        new System.Data.Common.DataColumnMapping("ORDER_DATE", "ORDER_DATE")})});
            // 
            // sqlSelectCommand22
            // 
            this.sqlSelectCommand22.CommandText = resources.GetString("sqlSelectCommand22.CommandText");
            this.sqlSelectCommand22.Connection = this.TR_Conn2;
            this.sqlSelectCommand22.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@SUPPLIER", System.Data.SqlDbType.VarChar, 10, "SUPPLIER")});
            // 
            // sqlSelectCommand23
            // 
            this.sqlSelectCommand23.CommandText = resources.GetString("sqlSelectCommand23.CommandText");
            this.sqlSelectCommand23.Connection = this.WEB_Conn;
            // 
            // WEB_Conn
            // 
            this.WEB_Conn.ConnectionString = "Data Source=dev11;Initial Catalog=web_strike_test;Persist Security Info=True;User" +
    " ID=hmsqlsa;Password=hmsqlsa";
            this.WEB_Conn.FireInfoMessageEventOnUserErrors = false;
            // 
            // sqlInsertCommand9
            // 
            this.sqlInsertCommand9.CommandText = resources.GetString("sqlInsertCommand9.CommandText");
            this.sqlInsertCommand9.Connection = this.WEB_Conn;
            this.sqlInsertCommand9.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Work_Flow", System.Data.SqlDbType.VarChar, 0, "Work_Flow")});
            // 
            // sqlUpdateCommand13
            // 
            this.sqlUpdateCommand13.CommandText = resources.GetString("sqlUpdateCommand13.CommandText");
            this.sqlUpdateCommand13.Connection = this.WEB_Conn;
            this.sqlUpdateCommand13.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Work_Flow", System.Data.SqlDbType.VarChar, 0, "Work_Flow"),
            new System.Data.SqlClient.SqlParameter("@Original_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Work_Flow", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Work_Flow", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@Approval_ID", System.Data.SqlDbType.Int, 4, "Approval_ID")});
            // 
            // sqlDeleteCommand13
            // 
            this.sqlDeleteCommand13.CommandText = "DELETE FROM [WS_Approval_WorkFlow] WHERE (([Approval_ID] = @Original_Approval_ID)" +
    " AND ((@IsNull_Work_Flow = 1 AND [Work_Flow] IS NULL) OR ([Work_Flow] = @Origina" +
    "l_Work_Flow)))";
            this.sqlDeleteCommand13.Connection = this.WEB_Conn;
            this.sqlDeleteCommand13.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_Approval_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Approval_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_Work_Flow", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_Work_Flow", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "Work_Flow", System.Data.DataRowVersion.Original, null)});
            // 
            // daWorkFlow
            // 
            this.daWorkFlow.DeleteCommand = this.sqlDeleteCommand13;
            this.daWorkFlow.InsertCommand = this.sqlInsertCommand9;
            this.daWorkFlow.SelectCommand = this.sqlSelectCommand23;
            this.daWorkFlow.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "WS_Approval_WorkFlow", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("Approval_ID", "Approval_ID"),
                        new System.Data.Common.DataColumnMapping("Work_Flow", "Work_Flow"),
                        new System.Data.Common.DataColumnMapping("ApprovalType", "ApprovalType")})});
            this.daWorkFlow.UpdateCommand = this.sqlUpdateCommand13;
            // 
            // sqlSelectCommand24
            // 
            this.sqlSelectCommand24.CommandText = "select TreasuryDBName [COMPANY_ALIAS], company_name from COMPANIES where Treasury" +
    "DBName <> @tr_db and isnull(active,0) = 1";
            this.sqlSelectCommand24.Connection = this.WEB_Conn;
            this.sqlSelectCommand24.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@tr_db", System.Data.SqlDbType.VarChar, 500, "COMPANY_ALIAS")});
            // 
            // daCompanies
            // 
            this.daCompanies.SelectCommand = this.sqlSelectCommand24;
            this.daCompanies.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "COMPANIES", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("COMPANY_ALIAS", "COMPANY_ALIAS"),
                        new System.Data.Common.DataColumnMapping("company_name", "company_name")})});
            // 
            // sqlSelectCommand25
            // 
            this.sqlSelectCommand25.CommandText = "SELECT        AR_PWP_STATUS_ID, DESCRIPTION\r\nFROM            AR_PWP_STATUS";
            this.sqlSelectCommand25.Connection = this.TR_Conn;
            // 
            // sqlInsertCommand14
            // 
            this.sqlInsertCommand14.CommandText = resources.GetString("sqlInsertCommand14.CommandText");
            this.sqlInsertCommand14.Connection = this.TR_Conn;
            this.sqlInsertCommand14.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION")});
            // 
            // sqlUpdateCommand14
            // 
            this.sqlUpdateCommand14.CommandText = resources.GetString("sqlUpdateCommand14.CommandText");
            this.sqlUpdateCommand14.Connection = this.TR_Conn;
            this.sqlUpdateCommand14.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, "AR_PWP_STATUS_ID"),
            new System.Data.SqlClient.SqlParameter("@DESCRIPTION", System.Data.SqlDbType.VarChar, 0, "DESCRIPTION"),
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // sqlDeleteCommand14
            // 
            this.sqlDeleteCommand14.CommandText = "DELETE FROM [AR_PWP_STATUS] WHERE (([AR_PWP_STATUS_ID] = @Original_AR_PWP_STATUS_" +
    "ID) AND ((@IsNull_DESCRIPTION = 1 AND [DESCRIPTION] IS NULL) OR ([DESCRIPTION] =" +
    " @Original_DESCRIPTION)))";
            this.sqlDeleteCommand14.Connection = this.TR_Conn;
            this.sqlDeleteCommand14.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@Original_AR_PWP_STATUS_ID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "AR_PWP_STATUS_ID", System.Data.DataRowVersion.Original, null),
            new System.Data.SqlClient.SqlParameter("@IsNull_DESCRIPTION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, true, null, "", "", ""),
            new System.Data.SqlClient.SqlParameter("@Original_DESCRIPTION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "DESCRIPTION", System.Data.DataRowVersion.Original, null)});
            // 
            // daPWP_Status
            // 
            this.daPWP_Status.DeleteCommand = this.sqlDeleteCommand14;
            this.daPWP_Status.InsertCommand = this.sqlInsertCommand14;
            this.daPWP_Status.SelectCommand = this.sqlSelectCommand25;
            this.daPWP_Status.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AR_PWP_STATUS", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("AR_PWP_STATUS_ID", "AR_PWP_STATUS_ID"),
                        new System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})});
            this.daPWP_Status.UpdateCommand = this.sqlUpdateCommand14;
            // 
            // sqlSelectCommand26
            // 
            this.sqlSelectCommand26.CommandText = "dbo.AP_PWP_GetLinks";
            this.sqlSelectCommand26.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand26.Connection = this.TR_Conn;
            this.sqlSelectCommand26.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((byte)(0)), ((byte)(0)), "", System.Data.DataRowVersion.Current, null),
            new System.Data.SqlClient.SqlParameter("@po_id", System.Data.SqlDbType.Int, 4),
            new System.Data.SqlClient.SqlParameter("@ap_gl_alloc_id", System.Data.SqlDbType.Int, 4)});
            // 
            // daAP_PWP_GetLinks
            // 
            this.daAP_PWP_GetLinks.SelectCommand = this.sqlSelectCommand26;
            this.daAP_PWP_GetLinks.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "AP_PWP_GetLinks", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("so_trn_id", "so_trn_id"),
                        new System.Data.Common.DataColumnMapping("invoiceno", "invoiceno"),
                        new System.Data.Common.DataColumnMapping("inv_date", "inv_date"),
                        new System.Data.Common.DataColumnMapping("acct_year", "acct_year"),
                        new System.Data.Common.DataColumnMapping("acct_period", "acct_period"),
                        new System.Data.Common.DataColumnMapping("inv_amt", "inv_amt"),
                        new System.Data.Common.DataColumnMapping("allocated_amt", "allocated_amt"),
                        new System.Data.Common.DataColumnMapping("amt_available", "amt_available"),
                        new System.Data.Common.DataColumnMapping("amt_to_allocate", "amt_to_allocate"),
                        new System.Data.Common.DataColumnMapping("selected", "selected")})});
            // 
            // btnPOAttachments
            // 
            this.btnPOAttachments.Enabled = false;
            this.btnPOAttachments.Location = new System.Drawing.Point(174, 404);
            this.btnPOAttachments.Name = "btnPOAttachments";
            this.btnPOAttachments.Size = new System.Drawing.Size(123, 22);
            this.btnPOAttachments.StyleController = this.lcDefaults;
            this.btnPOAttachments.TabIndex = 25;
            this.btnPOAttachments.Text = "PO Attachments";
            this.btnPOAttachments.Click += new System.EventHandler(this.btnPOAttachments_Click);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnPOAttachments;
            this.layoutControlItem8.Location = new System.Drawing.Point(162, 392);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(127, 38);
            this.layoutControlItem8.Text = "PO Attachments";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // ucAP_InvoiceEntry
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.Controls.Add(this.gcHeader);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.panelContainer3);
            this.Name = "ucAP_InvoiceEntry";
            this.Size = new System.Drawing.Size(1576, 779);
            this.Load += new System.EventHandler(this.ucAP_InvoiceEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInvAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvHeader1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUndist.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldback.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcDefaults)).EndInit();
            this.lcDefaults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHeaderSide1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKCApproval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKCRouting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHoldDue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoldP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueTerms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTerms1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCurrency1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInvType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueAPCntl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAPSetupGL1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDiscDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciInvoiceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTerms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDiscountPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDocLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSupplier1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPaymentHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGST1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riInvoiceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riManualChkNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPST1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllPO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSwapSeg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatusPreAccrual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWEB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRouteStatusWorkFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsWorkFlow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWorkFlow1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHAS_CB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riYearPeriodRO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLevy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAllocSeg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEmpty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riWorkFlowRO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riManualChkNoRO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer3.ResumeLayout(false);
            this.panelContainer2.ResumeLayout(false);
            this.dpInvDefaults.ResumeLayout(false);
            this.dockPanel7_Container.ResumeLayout(false);
            this.dpRemit.ResumeLayout(false);
            this.dockPanel4_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcRemit)).EndInit();
            this.xtcRemit.ResumeLayout(false);
            this.tpRemit.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            this.tpROSupp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtROAcctNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROCity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROAddr1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtROName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciROState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciROZip)).EndInit();
            this.dockPanel6.ResumeLayout(false);
            this.dockPanel6_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            this.dockPanel5.ResumeLayout(false);
            this.dpActions.ResumeLayout(false);
            this.dockPanel8_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hlMultiCBEntry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlOverridePWPStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlOverrideCompliance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlEventHistory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlNewInv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlRefresh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlPrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlChargeBack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlDeleteInv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlQuickChk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlChangeSupp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNewInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDeleteInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciQuickChk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChangeSupp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChargeBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciEventHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverrideCompliance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOverridePWPStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.esiBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultiCBEntry)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcDetails)).EndInit();
            this.tcDetails.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsInvDetail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGLDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsGLAccts1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetPOSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDetPO1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riITC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAFE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCustCostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTimeTicket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCompanies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompanies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCompanies1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceBillabled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPWP_Status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPWP_Status1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOBSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOBSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPODSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPODSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOFSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOFSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOMSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOMSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOM2Select)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOM2Select1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPOInvSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPOInvSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riNoPOSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGLDescReadOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGLAccts)).EndInit();
            this.tpPWPLink.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcPWP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAP_PWP_GetLinks1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPWP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPWPSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	
		#region Setups

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
            TR_Conn2.ConnectionString = Connection.TRConnection;
            WEB_Conn.ConnectionString = Connection.WebConnection;

            daCompanies.SelectCommand.Parameters["@tr_db"].Value = Connection.TRDB;
            daCompanies.Fill(dsCompanies1);

            daPOInvSelect.SelectCommand.Parameters["@username"].Value = Connection.MLUser;
            daDetPO.SelectCommand.Parameters.Clear();

            dsInvDetail1.Tables[0].RowDeleted += new DataRowChangeEventHandler(ucAP_InvoiceEntry_RowDeleted);
            dsInvHeader1.AP_INV_HEADER.IS_BALANCEDColumn.ReadOnly = false;

            ExecuteNonQuery("exec sp_fill_mluser_supervisor '" + Connection.MLUser + "','" + Connection.MLUser + "', 1", TR_Conn);
            daInvHeader.SelectCommand.Parameters["@username"].Value = Connection.MLUser;

            SetupGridG();

            SetupCustomComps();
            SetupInvoiceType();
            SetupCountry();
            SetupHdrSwapSeg();
            SetupApprovalDT();
            SetupPreAccrualWorkFlowRouting();
            SetupAPWorkFlowRouting();
            SetupContractPOMatching();
            SetupMultiCo();
            SetupSortColumn();
            SetupCompliance();
            SetupLevy();
            
            xtcRemit.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
        }
                
        private void SetupLevy()
        {
            string sSQL = @"select ISNULL(use_land,'F') from proj_cntl";
            object oEnabled = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oEnabled == null || oEnabled == DBNull.Value)
                oEnabled = "F";
            if (oEnabled.Equals("F"))
            {
                colLevy.Visible = false;
                colLevy.OptionsColumn.ShowInCustomizationForm = false;
                tpLevy.PageVisible = false;
            }
            else
            {
                DataTable dtLevy = new DataTable("dtLevy");
                dtLevy.Columns.Add("ID", typeof(bool));
                dtLevy.Columns.Add("Desc", typeof(string));
                dtLevy.Rows.Add(new object[] { true, "Levy" });

                riLevy.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Levy"));
                riLevy.DataSource = dtLevy;
                riLevy.DisplayMember = "Desc";
                riLevy.ValueMember = "ID";

                LevyMatch = new AP_Levy.ucLevyMatch(Connection, DevXMgr);
                LevyMatch.Dock = DockStyle.Fill;
                LevyMatch.Parent = tpLevy;
            }
        }

        private void SetupCompliance()
        {
            string sSQL = @"select COUNT(*) from WF_ApprovalPoint where ISNULL(active,0) = 1 and WF_ID = 4";
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;
            if (Convert.ToInt32(oCNT) == 0)
            {
                lciOverrideCompliance.HideToCustomization();
                lciOverrideCompliance.ShowInCustomizationForm = false;
            }

            if (!KCA_Validator.ModulePointRequired(CONST_SUBCONTRACTOR_COMPLIANCE_OVERRIDE))
            {
                lciOverrideCompliance.HideToCustomization();
                lciOverrideCompliance.ShowInCustomizationForm = false;
            }
        }

        private void SetupSortColumn()
        {
            string sSQL = @"select isnull(ENTRY_SORT_COLUMN,'C') from ap_setup";
            object oSort = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oSort == null || oSort == DBNull.Value)
                oSort = "C";

            if (oSort.Equals("C"))
            {
                colSupplierName.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            }
            else
            {
                colSupplierName.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            }
        }

        private void SetupMultiCo()
        {
            string sSQL = @"select isnull(allow_multi_company,'F') from ap_setup";
            object obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (obj == null || obj == DBNull.Value)
                obj = "F";

            if (obj.Equals("F"))
            {
                colCOMPANY_ALIAS.OptionsColumn.ShowInCustomizationForm = false;
                colCOMPANY_ALIAS.Visible = false;
            }
        }

        private void SetupContractPOMatching()
        {
            string sSQL = @"select isnull(AP_ContractPO_Matching,'R') from po_setup";
            object obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (obj == null || obj == DBNull.Value)
                obj = "D";

            if (obj.Equals("D"))
            {
                tpSummContractPO.PageVisible = false;
                _ShowContractPOSummary = false;
            }
            else
            {
                tpContractPO.PageVisible = false;
                _ShowContractPOSummary = true;
            }
        }

        private void SetupKCAccess()
        {
            KCA_Validator.ModulePointLocked_Event += new KeyControlAccess.Validator.ModulePointLocked_Delegate(KCA_Validator_ModulePointLocked_Event);
            KCA_Validator.ModulePointUnlocked_Event += new KeyControlAccess.Validator.ModulePointUnlocked_Delegate(KCA_Validator_ModulePointUnlocked_Event);

            if (KCA_Validator.ModulePointRequired(CONST_HOLDBACK_EDIT))
            {
                if (KCA_Validator.ModulePointLocked(CONST_HOLDBACK_EDIT))
                    KCA_Validator_ModulePointLocked_Event(CONST_HOLDBACK_EDIT);
            }

            if (KCA_Validator.ModulePointRequired(CONST_PO_RESTOCKING_AMT_EDIT))
            {
                if (KCA_Validator.ModulePointLocked(CONST_PO_RESTOCKING_AMT_EDIT))
                    KCA_Validator_ModulePointLocked_Event(CONST_PO_RESTOCKING_AMT_EDIT);
            }

            if (KCA_Validator.ModulePointRequired(CONST_CHANGE_CREDIT_TERMS))
            {
                if (KCA_Validator.ModulePointLocked(CONST_CHANGE_CREDIT_TERMS))
                    KCA_Validator_ModulePointLocked_Event(CONST_CHANGE_CREDIT_TERMS);
            }
        }

        private void KCA_Validator_ModulePointLocked_Event(int ModulePointID)
        {
            if (ModulePointID == CONST_HOLDBACK_EDIT)
            {
                _HoldbackEdit = false;
                colHOLD_AMT.OptionsColumn.AllowEdit = false;
                colHOLD_AMT1.OptionsColumn.AllowEdit = false;
                colHOLD_PCT.OptionsColumn.AllowEdit = false;
                txtHoldA.Properties.ReadOnly = true;
                txtHoldP.Properties.ReadOnly = true;
            }
            else if (ModulePointID == CONST_PO_RESTOCKING_AMT_EDIT)
            {
                ucMPOR.RestockingAmtKCA(true);
            }
            else if (ModulePointID == CONST_CHANGE_CREDIT_TERMS)
            {
                lueTerms.Properties.ReadOnly = true;
                lueTerms.Properties.Buttons[0].Visible = false;
            }
        }

        private void KCA_Validator_ModulePointUnlocked_Event(int ModulePointID)
        {
            if (ModulePointID == CONST_HOLDBACK_EDIT)
            {
                _HoldbackEdit = true;
                if (_DefaultsEnabled)
                {
                    colHOLD_AMT.OptionsColumn.AllowEdit = true;
                    colHOLD_AMT1.OptionsColumn.AllowEdit = true;
                    colHOLD_PCT.OptionsColumn.AllowEdit = true;
                    txtHoldA.Properties.ReadOnly = false;
                    txtHoldP.Properties.ReadOnly = false;
                }
            }
            else if (ModulePointID == CONST_PO_RESTOCKING_AMT_EDIT)
            {
                ucMPOR.RestockingAmtKCA(false);
            }
            else if (ModulePointID == CONST_CHANGE_CREDIT_TERMS)
            {
                lueTerms.Properties.ReadOnly = false;
                lueTerms.Properties.Buttons[0].Visible = true;
            }
        }

        private void SetupAPWorkFlowRouting()
        {
            _AP_WFRequired = false;

            string sSQL = @"select COUNT(*) from WF_ApprovalPoint where ISNULL(active,0) = 1 and WF_ID = " + CONST_ACCOUNTS_PAYABLE_WF;
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;
            if (Convert.ToInt32(oCNT) == 0)
            {
                colWF_STATUS.Visible = false;
                colWF_STATUS.OptionsColumn.ShowInCustomizationForm = false;

                colWF_Approval_ID.Visible = false;
                colWF_Approval_ID.OptionsColumn.ShowInCustomizationForm = false;
            }
            else
            {
                _AP_WFRequired = true;
                dsWorkFlow1.Clear();
                daWorkFlow.Fill(dsWorkFlow1);

                sSQL = @"select isnull((select Force_WF_Routing from ap_setup), 'F')";
                object oForce = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oForce == null || oForce == DBNull.Value)
                    oForce = "F";
                if (oForce.ToString().Equals("T"))
                    _AP_ForceWF = true;
            }
        }

        private void SetupPreAccrualWorkFlowRouting()
        {
            _WFRequired = false;

            string sSelect = "select count(*) from approval_topic where active=1 and id=" + CONST_UNAPPROVED_CONTRACT_PO_APPROVAL_TOPIC_ID;
            object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.WebConnection);
            if (obj != null)
            {
                if (Convert.ToInt32(obj) == 0)
                {                    
                    colStatus1.Visible = false;
                    colStatus1.OptionsColumn.ShowInCustomizationForm = false;
                }
                else
                    _WFRequired = true;
            }

            string sSQL = @"select COUNT(*) from WF_ApprovalPoint where ISNULL(active,0) = 1 and WF_ID = 4";
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;
            if (Convert.ToInt32(oCNT) == 0)
            {
                colKC_ACCRUAL_STATUS.Visible = false;
                colKC_ACCRUAL_STATUS.OptionsColumn.ShowInCustomizationForm = false;
            }
            else
                _WFRequired = true;

            #region Old regular routing replaced by work flow routing
            //sSelect = "select count(*) from approval_topic where active=1 and id=" + CONST_SUBCON_COMP_PRE_ACCRUAL;
            //obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.WebConnection);
            //if (obj != null)
            //{
            //    if (Convert.ToInt32(obj) == 0)
            //    {
            //        colKC_ACCRUAL_STATUS.Visible = false;
            //        colKC_ACCRUAL_STATUS.OptionsColumn.ShowInCustomizationForm = false;
            //    }
            //    else
            //        _WFRequired = true;
            //}
            #endregion
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

            riRouteStatus.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRouteStatus.DataSource = dtApproval;
            riRouteStatus.DisplayMember = "Desc";
            riRouteStatus.ValueMember = "ID";

            riRouteStatusPreAccrual.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRouteStatusPreAccrual.DataSource = dtApproval;
            riRouteStatusPreAccrual.DisplayMember = "Desc";
            riRouteStatusPreAccrual.ValueMember = "ID";

            riRouteStatusWorkFlow.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRouteStatusWorkFlow.DataSource = dtApproval;
            riRouteStatusWorkFlow.DisplayMember = "Desc";
            riRouteStatusWorkFlow.ValueMember = "ID";  
        }

		private void SetupCustomComps()
		{
			ucAccountingPicker1.UserName = Connection.MLUser;
			ucAccountingPicker1.ConnectionString = Connection.TRConnection;

			Supp_Repository = new Supplier_Lookup_Rep.Repository_Supplier_Lookup();
			Supp_Repository.HMConnection = Connection;						
			Supp_Repository.DevXMgr = DevXMgr;
			Supp_Repository.ActiveSuppliers = true;
			//colSUPPLIER1.ColumnEdit = Supp_Repository;
            colSupplierName.ColumnEdit = Supp_Repository;
            Supp_Repository.EditValueChanged += new EventHandler(repositoryItemLookUpEdit5_EditValueChanged);
			Supp_Repository.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(repositoryItemLookUpEdit5_EditValueChanging);

			GL_Repository = new GL_Account_Lookup_Rep.Repository_GL_Lookup();
			GL_Repository.HideVailidation = false; //must be set before the hmconnection is set.
			GL_Repository.HMConnection = Connection;
			GL_Repository.DevXMgr = DevXMgr;	
			GL_Repository.ValidateOnEnterKey = true;
			GL_Repository.SubCodeUpdated += new GL_Account_Lookup_Rep.Repository_GL_Lookup.Delegate_SubCodeUpdated(GL_Repository_SubCodeUpdated);
			colGL_ACCOUNT.ColumnEdit = GL_Repository;			

			dsInvHeader1.AP_INV_HEADER.AP_INV_HEADERRowDeleted += new AP_Invoice_Entry.dsInvHeader.AP_INV_HEADERRowChangeEventHandler(AP_INV_HEADER_AP_INV_HEADERRowDeleted);

            PC_CostCodesLU.PopupCostCodeLookupRepository PopupCostCode = new PC_CostCodesLU.PopupCostCodeLookupRepository(Connection);
            PopupCostCode.Lv1ID_name = "lv1id";
            PopupCostCode.Lv2ID_name = "lv2id";
            PopupCostCode.Lv3ID_name = "lv3id";
            PopupCostCode.Lv4ID_name = "lv4id";
            PopupCostCode.PRI_ID_name = "pri_id";
            PopupCostCode.LEMOS_name = "lem_comp";
            PopupCostCode.ExpenseType_name = "EXPENSE_TYPE";            
            PopupCostCode.CostCodeReferenceSelected += new PC_CostCodesLU.PopupCostCodeLookupRepository.CostCodeReferenceSelectedDelegate(PopupCostCode_CostCodeReferenceSelected);

            colREFERENCE1.ColumnEdit = PopupCostCode;

            SubCon = new AP_SubcontractorCompliance.SupplierSubConValidator(Connection, DevXMgr);

            SetupChargeBackPicker();
		}

        private void PopupCostCode_CostCodeReferenceSelected(PC_CostCodesLU.CostCodeReference CCR)
        {
            if ((gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.NewItemRowHandle ||
                gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle) && CCR.PriID == -1)
            {
                return;
            }

            gvDetail.SetFocusedRowCellValue(colREFERENCE1, CCR.Reference);
            gvDetail.SetFocusedRowCellValue(collv1id, CCR.Lv1ID);
            gvDetail.SetFocusedRowCellValue(collv2id, CCR.Lv2ID);
            gvDetail.SetFocusedRowCellValue(collv3id, CCR.Lv3ID);
            gvDetail.SetFocusedRowCellValue(collv4id, CCR.Lv4ID);
            gvDetail.SetFocusedRowCellValue(collem_comp, CCR.LEM);
            gvDetail.SetFocusedRowCellValue(colpri_id, CCR.PriID);
            gvDetail.SetFocusedRowCellValue(colEXPENSE_TYPE, CCR.ExpenseType);
            gvDetail.SetFocusedRowCellValue(colbillable, CCR.Billable_TF);
           
            if (CCR.PriID != -1)
            {
                CCR.LoadGLAccount(Connection);
                gvDetail.SetFocusedRowCellValue(colGL_ACCOUNT, CCR.GLAccount);
            }
            else
            {
                gvDetail.SetFocusedRowCellValue(colGL_ACCOUNT, DBNull.Value);
            }

            if (CCR.Reference != "")
                colbillable.OptionsColumn.AllowEdit = true;

            else
            {
                
                colbillable.OptionsColumn.AllowEdit = false;
            }
        }

        #region Charge Backs

        private void SetupChargeBackPicker()
        {
            string sSQL = @"select isnull(allow_charge_backs,'F') from ap_setup";
            object oAllow = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oAllow == null || oAllow == DBNull.Value)
                oAllow = "F";

            if (oAllow.Equals("T"))
            {
                colHAS_CB.Visible = true;
                colHAS_CB.OptionsColumn.ShowInCustomizationForm = true;
                colHAS_CB.VisibleIndex = colIS_BALANCED.VisibleIndex + 1;

                colIS_CB.Visible = true;
                colIS_CB.OptionsColumn.ShowInCustomizationForm = true;
                colIS_CB.VisibleIndex = colIS_BALANCED.VisibleIndex + 2;

                colCB_REF.Visible = true;
                colCB_REF.OptionsColumn.ShowInCustomizationForm = true;
                colCB_REF.VisibleIndex = colITC.VisibleIndex + 1;

                PopupChgBk = new ChargeBackPicker.PopupChargeBackLookupRepository(Connection, DevXMgr, ChargeBackPicker.InitFlavor.AP);
                PopupChgBk.CB_ID_name = "CB_ID";
                PopupChgBk.ChargeBackSelected += new ChargeBackPicker.PopupChargeBackLookupRepository.ChargeBackSelectedDelegate(PopupChgBk_ChargeBackSelected);
                colCB_REF.ColumnEdit = PopupChgBk;
            }
            else
            {
                lciChargeBack.HideToCustomization();
                lciChargeBack.ShowInCustomizationForm = false;
                lciMultiCBEntry.HideToCustomization();
                lciMultiCBEntry.ShowInCustomizationForm = false;
            }
        }

        private void PopupChgBk_ChargeBackSelected(string Reference, int CB_ID)
        {
            if ((gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle ||
                   gvDetail.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle))
            {
                return;
            }

            gvDetail.SetFocusedRowCellValue(colCB_REF, Reference);
            gvDetail.SetFocusedRowCellValue(colCB_ID, CB_ID);
        }

        private void gcDetail_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (gvDetail.State == DevExpress.XtraGrid.Views.Grid.GridState.Normal)
                {
                    DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oCB_ID = dr["CB_ID"];
                        if (oCB_ID != null && oCB_ID != DBNull.Value)
                        {
                            if (gvDetail.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                            {
                                try
                                {
                                    object oCur_CB_ID = dr["CB_ID", DataRowVersion.Current];

                                    if (Convert.ToInt32(oCur_CB_ID) == Convert.ToInt32(oCB_ID))
                                        return;
                                }
                                catch { }
                            }

                            if (Convert.ToInt32(oCB_ID) != -1)
                            {
                                string sSQL = @"exec CB_DeleteChargeBack '" + Connection.MLUser + @"', " + oCB_ID;
                                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                            }
                        }
                    }
                }
            }
        }

        private void CB_OnHdr()
        {
            if (PopupChgBk != null) // ChargeBack is enabled.
            {
                DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                if (dr != null)
                {
                    object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                    if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
                    {
                        string sSQL = @"select count(*) from ap_gl_alloc where ap_inv_header_id=" + oAP_INV_HEADER_ID + @" and isnull(cb_id,-1) <> -1";
                        if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection)) > 0)
                        {
                            // update
                            dr.BeginEdit();
                            dr["HAS_CB"] = "T";
                            dr.EndEdit();
                            dr.AcceptChanges();

                            sSQL = @"update ap_inv_header set has_cb='T' where ap_inv_header_id=" + oAP_INV_HEADER_ID;
                            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        }
                        else
                        {
                            // remove
                            dr.BeginEdit();
                            dr["HAS_CB"] = "F";
                            dr.EndEdit();
                            dr.AcceptChanges();

                            sSQL = @"update ap_inv_header set has_cb='F' where ap_inv_header_id=" + oAP_INV_HEADER_ID;
                            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        }
                    }
                }
            }
        }

        private void hlChargeBack_Click(object sender, EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
                {
                    string sSQL = @"begin tran
                        declare @message varchar(max)
                        exec CB_CreateChargeBack '" + Connection.MLUser + @"', 'AP', " + oAP_INV_HEADER_ID + @", @message output
                        if( @message = 'OK' )
                            commit tran
                        else
                            rollback tran
                        select @message";
                    object obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (obj == null || obj == DBNull.Value)
                        obj = "Error creating chargeback.";

                    if (obj.Equals("OK"))
                        Popup.ShowPopup("Chargeback created successfully.");
                    else
                        Popup.ShowPopup(obj.ToString());
                }
            }
        }

        #endregion

        private void SetupUserWHSE()
		{
			string sSelect = "select whse_id from mluser where name ='"+Connection.MLUser+"'";
			object oID = ExecuteScalar( sSelect, TR_Conn );
			if( oID != null && oID != DBNull.Value )
			{
				UserWHSEID = Convert.ToInt32( oID );
			}
			else 
			{ 
				UserWHSEID = -1;
			}
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

					string sSecurity = "";
					sSelect = "select count(*) from working_mluser_supervisor wms "+
						"left outer join warehouse_security ws on wms.mluser=ws.mluser "+
						"where wms.username='"+Connection.MLUser+"' and ws_id is null";
					object oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );

					if( Convert.ToInt32( oResult ) == 0 )
					{
						sSecurity = " join warehouse w on w.whse_div = a.seg_"+Column+" "+
							"join warehouse_security ws on ws.whse_id=w.whse_id "+
							"join mluser m on m.name = ws.mluser "+
							"join working_mluser_supervisor s ON s.mluser = m.name "+
							"where s.username = '"+Connection.MLUser+"' ";
					}

					sSelect = "select "+ColumnName+" from gl_setup";
					daSwapSeg.SelectCommand.CommandText = "select distinct a.seg_"+Column+" [Code], g.SEGMENT_DESC [Description] "+
						"from gl_accounts a "+	
						"left outer join gl_segment_setup g on g.SEGMENT_VALUE = a.seg_"+Column+" "+
						"and SEGMENT_NUMBER = "+Column+" "+
						sSecurity+
						"order by a.seg_"+Column;
					dsSwapSeg1.Clear();
					daSwapSeg.Fill( dsSwapSeg1 );

					obj = ExecuteScalar( sSelect, TR_Conn );
					if( obj != null && obj.GetType() != typeof( DBNull ) )
					{
						colAP_DIV.Caption = obj.ToString();
						colAP_DIV.Visible = true;
						colAP_DIV.OptionsColumn.ShowInCustomizationForm = true;
						_SwapSegEnabled = true;
					}
				}
			}
			else
			{
				colAP_DIV.Visible = false;
				colAP_DIV.OptionsColumn.ShowInCustomizationForm = false;
				_SwapSegEnabled = false;
			}			
		}	

		private void SetupSeg()
		{
			string sSelect = "select isnull(clear_req,'F') from gl_setup";
			if( ExecuteScalar( sSelect, TR_Conn ).ToString().ToUpper().Trim() == "T" )
			{
				sSelect = "select count(*) from gl_setup where ALLOC_SEG is not null and ltrim(rtrim(ALLOC_SEG)) <> ''";
				object obj = ExecuteScalar( sSelect, TR_Conn );
				if( Convert.ToInt32( obj ) > 0 )
				{
					daAllocSeg.Fill( dsAllocSeg1 );

					obj = ExecuteScalar( sSelect, TR_Conn );
					colSEG_CHANGE.Visible = true;
					colSEG_CHANGE.OptionsColumn.ShowInCustomizationForm = true;
					_SegEnabled = true;
				}
				else
				{
					colSEG_CHANGE.Visible = false;
					colSEG_CHANGE.OptionsColumn.ShowInCustomizationForm = false;
					_SegEnabled = false;
				}
			}
			else
			{
				colSEG_CHANGE.Visible = false;
				colSEG_CHANGE.OptionsColumn.ShowInCustomizationForm = false;
				_SegEnabled = false;
			}			
		}

		private void SetupSwapSeg()
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
					daSwapSeg.SelectCommand.CommandText = "select distinct a.seg_"+Column+" [Code], m.SEGMENT_DESC [Description] "+
						"from gl_accounts a "+	
						"left outer join gl_segment_setup m on m.SEGMENT_VALUE = a.seg_"+Column+" "+
						"and SEGMENT_NUMBER = "+Column+" "+
						"order by a.seg_"+Column;
					daSwapSeg.Fill( dsSwapSeg1 );

					obj = ExecuteScalar( sSelect, TR_Conn );
					if( obj != null && obj.GetType() != typeof( DBNull ) )
					{
						colSEG_CHANGE.Caption = obj.ToString();
						colSEG_CHANGE.Visible = true;
						colSEG_CHANGE.OptionsColumn.ShowInCustomizationForm = true;
						_SwapSegEnabled = true;
					}
				}
			}
			else
			{
				colSEG_CHANGE.Visible = false;
				colSEG_CHANGE.OptionsColumn.ShowInCustomizationForm = false;
				_SwapSegEnabled = false;
			}			
		}

		private void SetupCountry()
		{
			if( Connection.CountryCode.ToUpper() == "U" )
			{
				colGST_CODE.Caption = "Tax 1";
				colSALES_TAX_ID.Caption = "Tax 2";
				colGST_AMT.Caption = "Tax 1 Amount";
				layoutControlItem26.Text = "State";
				layoutControlItem27.Text = "Zip";
                lciROState.Text = "State";
                lciROZip.Text = "Zip";
				layoutControlItem15.Text = "Retainage %";
				layoutControlItem16.Text = "Retainage $";
				layoutControlItem17.Text = "Retainage Due";
				layoutControlItem3.Text = "Retainage";
				layoutControlItem4.Text = "Retainage Remaining";
				colHOLD_AMT.Caption = "Retainage $";
				colHOLD_AMT1.Caption = "Retainage $";
				colHOLD_PCT.Caption = "Retainage %";
				colHOLD_PAY_DATE.Caption = "Retainage Due";
			}
			else
			{
				colGST_CODE.Caption = "GST";
				colSALES_TAX_ID.Caption = "PST";
				colGST_AMT.Caption = "GST Amount";
				layoutControlItem26.Text = "Province";
				layoutControlItem27.Text = "Postal Code";
                lciROState.Text = "Province";
                lciROZip.Text = "Postal Code";
				layoutControlItem15.Text = "Holdback %";
				layoutControlItem16.Text = "Holdback $";
				layoutControlItem17.Text = "Holdback Due";
				layoutControlItem3.Text = "Holdback";
				layoutControlItem4.Text = "Holdback Remaining";
				colHOLD_AMT.Caption = "Holdback $";
				colHOLD_AMT1.Caption = "Holdback $";
				colHOLD_PCT.Caption = "Holdback %";
				colHOLD_PAY_DATE.Caption = "Holdback Due";
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

			riInvoiceType.DataSource = dtInvType;
			riInvoiceType.DisplayMember = "Type";
			riInvoiceType.ValueMember = "Code";

			riInvoiceType.Columns.Add( new DevExpress.XtraEditors.Controls.LookUpColumnInfo( "Type", "Type", 150 ) );

			ucIS = new CustomerInvoiceSearch.InvoiceSearch( CustomerInvoiceSearch.InvoiceSearch.Flavour.AP );
			ucIS.HMConnection = Connection;
			ucIS.DevXMgr = DevXMgr;	
			colINV_NO.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;


			dtDetType = new DataTable( "DETTYPE" );
			dtDetType.Columns.Add( "Code", typeof( String ) );
			dtDetType.Columns.Add( "Type", typeof( String ) );

			dtDetType.Rows.Add( new object[] { "I", "Invoice" } );
			dtDetType.Rows.Add( new object[] { "F", "Freight" } );
			dtDetType.Rows.Add( new object[] { "D", "Duty" } );
			dtDetType.Rows.Add( new object[] { "B", "Brokerage" } );
			dtDetType.Rows.Add( new object[] { "M", "Miscellaneous" } );
			dtDetType.Rows.Add( new object[] { "2", "Miscellaneous 2" } );

			repositoryItemLookUpEdit9.DataSource = dtDetType;
			repositoryItemLookUpEdit9.DisplayMember = "Type";
			repositoryItemLookUpEdit9.ValueMember = "Code";

			repositoryItemLookUpEdit9.Columns.Add( new DevExpress.XtraEditors.Controls.LookUpColumnInfo( "Type", "Type", 150 ) );
		}

		private void SetupGridG()
		{
            ggHeader = new GridG(Connection.WebServer, Connection.TRDB, gvHeader, daInvHeader, dsInvHeader1);
            ggHeader.AskBeforeDelete = false;
            ggHeader.AllowTabCreateNewRecord = true;
            ggHeader.Event_BeforeLeaveRow_Enabled = false;
            ggHeader.AllowDelete += new TUC_GridG.GridG.AllowDeleteDelegate(ggHeader_AllowDelete);

			ggDetail = new GridG( Connection.WebServer, Connection.TRDB, gvDetail, daInvDetail, dsInvDetail1 );
			ggDetail.AskBeforeDelete = false;
			ggDetail.AllowTabCreateNewRecord = true;
			ggDetail.Event_BeforeLeaveRow_Enabled = false;
			ggDetail.AllowDelete += new TUC_GridG.GridG.AllowDeleteDelegate(ggDetail_AllowDelete);
		}

		#endregion		

		#region Header Events

		private void gvHeader_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
		{
			if( e.PrevFocusedColumn == colINV_NO )
			{
				if( !bNewRow )
				{
					DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
					if( dr != null )
					{
						object oInvNo = dr["INV_NO"];
						if( oInvNo == null || oInvNo == DBNull.Value )
						{
							gvHeader.FocusedColumn = colINV_NO;
							gvHeader.SetColumnError( colINV_NO, "Invoice number required." );
						}
						else
						{
							if( oInvNo.ToString().Trim() == "" )
							{
								gvHeader.FocusedColumn = colINV_NO;
								gvHeader.SetColumnError( colINV_NO, "Invoice number required." );
							}
						}
					}
				}
			}
		}

        private void ClearRORemit()
        {
            xtcRemit.SelectedTabPage = tpRemit;
            txtROName.EditValue = null;
            txtROAddr1.EditValue = null;
            txtROAddr2.EditValue = null;
            txtROAddr3.EditValue = null;
            txtROCity.EditValue = null;
            txtROState.EditValue = null;
            txtROZip.EditValue = null;
            txtROAcctNo.EditValue = null;
        }

        private void LoadRORemit()
        {
            ClearRORemit();

            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oSUPPLIER = dr["SUPPLIER"];
                if (oSUPPLIER != null && oSUPPLIER != DBNull.Value)
                {
                    string sSQL = @"select NAME, REMIT_ADD1, REMIT_ADD2, REMIT_ADD3, REMIT_CITY, REMIT_STATE, REMIT_ZIP, SUPP_ACCOUNT, isnull(ONE_CHECK,'F') [ONE_CHECK] 
                        from SUPPLIER_MASTER 
                        where SUPPLIER='"+oSUPPLIER.ToString().Replace("'", "''")+@"'";
                    DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow drS = dt.Rows[0];
                            if (drS != null)
                            {
                                txtROName.EditValue = drS["NAME"];
                                txtROAddr1.EditValue = drS["REMIT_ADD1"];
                                txtROAddr2.EditValue = drS["REMIT_ADD2"];
                                txtROAddr3.EditValue = drS["REMIT_ADD3"];
                                txtROCity.EditValue = drS["REMIT_CITY"];
                                txtROState.EditValue = drS["REMIT_STATE"];
                                txtROZip.EditValue = drS["REMIT_ZIP"];
                                txtROAcctNo.EditValue = drS["SUPP_ACCOUNT"];
                                if (drS["ONE_CHECK"].Equals("F"))
                                    xtcRemit.SelectedTabPage = tpROSupp;
                            }
                        }
                    }
                }
            }
        }

		private void gvHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
            Current_AP_INV_ID = -1;
            _MatchPOLoaded = false;
            _UnreleasedLoaded = false;
            LoadRORemit();
            dsAP_PWP_GetLinks1.Clear();
            if( LevyMatch != null )
                LevyMatch.LoadInvoice(-1);
			if( !Loaded )
				return;
            
			if( !b_DontFire )
			{
				CodeChanging = true;
                hlEventHistory.Enabled = false;
				
				if( gvHeader.FocusedColumn == colPO_ID )
				{
					gvHeader.FocusedColumn = colINV_NO;
					gvHeader.FocusedColumn = colPO_ID;
				}
                
				LoadHeaderSide();
				dsPO1.Clear();
				dsDetPO1.Clear();
				dsInvDetail1.Clear();
				POLevyLockDown();

				txtDiscP.EditValue = null;

				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr == null && gvHeader.RowCount > 0 )
				{
					try
					{
						dr = dsInvHeader1.Tables[0].Rows[ e.FocusedRowHandle ];
					}
					catch{}
				}

                if (dr != null)
                {
                    int iAP_Inv_Header_ID = -1;
                    object oID = dr["AP_INV_HEADER_ID"];
                    if (oID != null && oID != DBNull.Value)
                    {
                        iAP_Inv_Header_ID = Convert.ToInt32(oID);
                        Current_AP_INV_ID = iAP_Inv_Header_ID;
                        daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oID;
                        daInvDetail.Fill(dsInvDetail1);
                        if (tcDetails.SelectedTabPage == tpMatchPO)
                            ucMPOR.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                        else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
                        {
                            ucUCPO.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                            ucSCPO.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                        }

                        if (LevyMatch != null)
                            LevyMatch.LoadInvoice(iAP_Inv_Header_ID);
                    }
                    else
                    {
                        ucMPOR.AP_INV_HEADER_ID = -1;
                        ucUCPO.AP_INV_HEADER_ID = -1;
                        ucSCPO.AP_INV_HEADER_ID = -1;                        
                    }

                    string sSQL = @"select COUNT(*) 
                        from AP_INV_HEADER h
                        join TERMS t on t.TERMS_ID=h.TERMS_ID
                        where h.AP_INV_HEADER_ID=" + iAP_Inv_Header_ID + @" and ISNULL(t.PaidWhenPaid,'F') = 'T'";
                    object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oCNT == null || oCNT == DBNull.Value)
                        oCNT = 0;
                    if (Convert.ToInt32(oCNT)>0)
                    {
                        tpPWPLink.PageVisible = true;
                        colAR_PWP_STATUS_ID.Visible = true;
                        colAR_PWP_STATUS_ID.OptionsColumn.ShowInCustomizationForm = true;
                        hlOverridePWPStatus.Enabled = true;
                    }
                    else
                    {
                        tpPWPLink.PageVisible = false;
                        colAR_PWP_STATUS_ID.Visible = false;
                        colAR_PWP_STATUS_ID.OptionsColumn.ShowInCustomizationForm = false;
                        hlOverridePWPStatus.Enabled = false;
                    }

                    object oPeriod = dr["ACCT_PERIOD"];
                    object oYear = dr["ACCT_YEAR"];
                    if (oPeriod != null && oPeriod != DBNull.Value && oYear != null && oYear != DBNull.Value)
                    {
                        ucAccountingPicker1.SelectedPeriod = Convert.ToInt32(dr["ACCT_PERIOD"]);
                        ucAccountingPicker1.SelectedYear = Convert.ToInt32(dr["ACCT_YEAR"]);
                    }

                    ucMPOR.Year = ucAccountingPicker1.SelectedYear;
                    ucMPOR.Period = ucAccountingPicker1.SelectedPeriod;

                    object oSupplier = dr["SUPPLIER"];
                    if (oSupplier != null && oSupplier != DBNull.Value)
                    {
                        ucIS.SupplierCode = oSupplier.ToString();

                        string sSelect = "select Terms_id from supplier_master where supplier = '" + oSupplier + "'";

                        if (lueTerms.EditValue != null && lueTerms.EditValue != DBNull.Value)
                        {
                            sSelect = "select isnull(cash_disc_days,0) from terms where terms_id = " + lueTerms.EditValue;
                            txtDiscP.EditValue = ExecuteScalar(sSelect, TR_Conn);
                        }

                        daPO.SelectCommand.Parameters["@supplier"].Value = oSupplier.ToString();
                        daPO.Fill(dsPO1);
                    }
                    else
                    {
                        ucIS.SupplierCode = "";
                    }
                    CheckMiscSupp(dr["SUPPLIER"]);

                    if (oSupplier != null && oSupplier != DBNull.Value)
                    {
                        daDetPO.SelectCommand.CommandText = sdaDetPOSelect.Replace("@SUPPLIER", "'" + oSupplier.ToString() + "'");
                        daDetPO.Fill(dsDetPO1);
                    }

                    object oPO_ID = dr["PO_ID"];
                    if (oPO_ID != null && oPO_ID != DBNull.Value)
                    {
                        if (tcDetails.SelectedTabPage == tpMatchPO)
                            ucMPOR.RefreshPO_ID(Convert.ToInt32(oPO_ID), Convert.ToInt32(oID));
                        else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
                        {
                            ucUCPO.PO_ID = Convert.ToInt32(oPO_ID);
                            ucSCPO.PO_ID = Convert.ToInt32(oPO_ID);
                        }
                        btnPOAttachments.Enabled = true;
                    }
                    else
                    {
                        ucMPOR.PO_ID = -1;
                        ucUCPO.PO_ID = -1;
                        ucSCPO.PO_ID = -1;
                        btnPOAttachments.Enabled = false;
                    }
                    btnSharepoint.Enabled = true;
                    SharePointMgr.cSharePointMgr.UpdateButton(Connection, "AP Invoice", ref btnSharepoint, "Header", iAP_Inv_Header_ID);


                    chkPaymentHold.Enabled = true;
                    riPaymentHold.ReadOnly = false;
                    object oINV_TYPE = dr["INVOICE_TYPE"];
                    if (oINV_TYPE != null && oINV_TYPE != DBNull.Value)
                    {
                        if (oINV_TYPE.Equals("A"))
                        {
                            chkPaymentHold.Enabled = false;
                            riPaymentHold.ReadOnly = true;
                        }
                    }

                    object oFromWEB = dr["FROM_WEB"];
                    if (oFromWEB == null || oFromWEB == DBNull.Value)
                        oFromWEB = "F";
                    if (oFromWEB.Equals("T"))
                    {
                        hlEventHistory.Enabled = true;
                        btnDirectAttachemnts.Enabled = true;
                        btnLinkCompAttch.Enabled = true;
                    }
                    else
                        hlEventHistory.Enabled = false;
                }
                else
                {
                    btnSharepoint.Enabled = false;
                    SharePointMgr.cSharePointMgr.UpdateButton(Connection, "AP Invoice", ref btnSharepoint, "Header", -1);
                }
                UpdateAdditionaAttachmentsCount();
                UpdatePOAttachmentsCount();
                lueInvType_EditValueChanged(null,null);
				CalculateRemaining();
                IsSubContractor();
				CodeChanging = false;
			}
        }

        private void IsSubContractor()
        {
            bool bIsSubCon = false;
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oSupplier = dr["SUPPLIER"];
                if (oSupplier != null && oSupplier != DBNull.Value)
                {
                    string sSQL = @"select isnull(SUBCONTRACTOR,'F') from SUPPLIER_MASTER where supplier='"+oSupplier+@"'";
                    object oResult = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oResult != null && oResult != DBNull.Value)
                    {
                        if (oResult.Equals("T"))
                        {
                            bIsSubCon = true;
                        }
                    }
                }
            }

            if (bIsSubCon)
            {
                colHOURS.Visible = true;
                colHOURS.OptionsColumn.ShowInCustomizationForm = true;
                colRATE.Visible = true;
                colRATE.OptionsColumn.ShowInCustomizationForm = true;
            }
            else
            {
                colHOURS.Visible = false;
                colHOURS.OptionsColumn.ShowInCustomizationForm = false;
                colRATE.Visible = false;
                colRATE.OptionsColumn.ShowInCustomizationForm = false;
            }
        }

        private void SubContractChange()
        {
            bool bIsSubCon = false;
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oSupplier = dr["SUPPLIER"];
                if (oSupplier != null && oSupplier != DBNull.Value)
                {
                    string sSQL = @"select isnull(SUBCONTRACTOR,'F') from SUPPLIER_MASTER where supplier='"+oSupplier+@"'";
                    object oResult = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oResult != null && oResult != DBNull.Value)
                    {
                        if (oResult.Equals("T"))
                        {
                            bIsSubCon = true;
                        }
                    }
                }
                if (!bIsSubCon)
                {
                    object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                    if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
                    {
                        string sSQL = @"select count(*) from ap_gl_alloc where ap_inv_header_id=" + oAP_INV_HEADER_ID + @" and isnull(hours,0) <> 0 ";
                        if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection)) > 0)
                        {
                            sSQL = @"update ap_gl_alloc set [Hours]=0, rate=0 where ap_inv_header_id="+oAP_INV_HEADER_ID;
                            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

                            dsInvDetail1.Clear();
                            daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oAP_INV_HEADER_ID;
                            daInvDetail.Fill(dsInvDetail1);
                        }
                    }
                }
            }
        }

		private void CheckMiscSupp( object Supplier )
		{
			txtRName.Properties.ReadOnly = true;

			if( Supplier != null && Supplier != DBNull.Value )
			{
				if( Convert.ToInt32( ExecuteScalar( "select count(*) from supplier_master where isnull(one_check,'F') = 'T' and supplier='"+Supplier+"'", TR_Conn ) ) > 0 )
				{
					txtRName.Properties.ReadOnly = false;
				}
			}
		}

		private void gvHeader_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				_HeaderValid = true;
				e.Valid = true;
				object obj = e.Row;
				if( obj != null )
				{
					DataRowView DRV = obj as DataRowView;
					object oSupp = DRV.Row["SUPPLIER"];
					object oType = DRV.Row["INVOICE_TYPE"];
					object oInvNo = DRV.Row["INV_NO"];
					object oInvDate = DRV.Row["INV_DATE"];
					object oDueDate = DRV.Row["DUE_DATE"];
					object oInvAmt = DRV.Row["INV_AMOUNT"];
					object oPurAmt = DRV.Row["PURCH_AMT"];
					object oGSTAmt = DRV.Row["GST_AMT"];
					object oManChk = DRV.Row["MANUAL_CHECK"];
					object oInvType = DRV.Row["INVOICE_TYPE"];
					object oAP_SETUP_GL_ID = DRV.Row["AP_SETUP_GL_ID"];
					object oID = DRV.Row["AP_INV_HEADER_ID"];
					object oSwapSeg = DRV.Row["AP_DIV"];
                    object oPO_ID = DRV.Row["PO_ID"];

					if( oSupp == null || oSupp.GetType() == typeof( DBNull ) )
					{
						_HeaderValid = false;
						e.ErrorText = "Supplier Required.";
						HeaderError = e.ErrorText;
						e.Valid = false;
						return;
					}
					if( oSupp != null && oSupp.GetType() != typeof( DBNull ) )
					{
						string sSelect = "select Count(*) from supplier_master where active='T' and supplier='"+oSupp+"'";
						if( Convert.ToInt32( ExecuteScalar( sSelect, TR_Conn ) ) == 0 )
						{
							_HeaderValid = false;
							e.ErrorText = "Selected Supplier is Inactive.";
							HeaderError = e.ErrorText;
							e.Valid = false;
							return;
						}
					}
					if( (oSwapSeg == null || oSwapSeg.GetType() == typeof( DBNull )) && _SwapSegEnabled )
					{
						_HeaderValid = false;
						e.ErrorText = colAP_DIV.Caption+" Required.";
						e.Valid = false;
						return;
					}
					if( oType == null || oType == DBNull.Value )
					{
						_HeaderValid = false;
						e.ErrorText = "Invoice Type Required.";
						HeaderError = e.ErrorText;
						e.Valid = false;
						return;
					}
					if( oInvNo == null || oInvNo == DBNull.Value )
					{
						_HeaderValid = false;
						e.ErrorText = "Invoice Number Required.";
						HeaderError = e.ErrorText;
						e.Valid = false;
						return;
					}
					else
					{
						if( oInvNo.ToString().Trim() == "" )
						{
							_HeaderValid = false;
							e.ErrorText = "Invoice Number Required.";
							HeaderError = e.ErrorText;
							e.Valid = false;
							return;
						}
					}
					
					if( oID != null && oID != DBNull.Value && !oType.Equals( "A" ) )
					{
						string sSelect = "Select count(*) from ap_inv_header where invoice_type <> 'A' and supplier='"+oSupp+"' and inv_no='"+oInvNo+"' and ap_inv_header_id <> "+oID;
						if( Convert.ToInt32( ExecuteScalar( sSelect, TR_Conn ) ) > 0 )
						{
							_HeaderValid = false;
							e.ErrorText = "Invoice number already exists for supplier.";
							HeaderError = e.ErrorText;
							e.Valid = false;
							return;
						}						
					}
					else if( !oType.Equals( "A" ) )
					{
						string sSelect = "Select count(*) from ap_inv_header where invoice_type <> 'A' and supplier='"+oSupp+"' and inv_no='"+oInvNo+"'";
						if( Convert.ToInt32( ExecuteScalar( sSelect, TR_Conn ) ) > 0 )
						{
							_HeaderValid = false;
							e.ErrorText = "Invoice number already exists for supplier.";
							HeaderError = e.ErrorText;
							e.Valid = false;
							return;
						}
					}
					if( oInvDate == null || oInvDate == DBNull.Value )
					{
						_HeaderValid = false;
						e.ErrorText = "Invoice Date Required.";
						HeaderError = e.ErrorText;
						e.Valid = false;
						return;
					}
					if( oDueDate == null || oDueDate == DBNull.Value )
					{
						_HeaderValid = false;
						e.ErrorText = "Invoice Due Date Required.";
						HeaderError = e.ErrorText;
						e.Valid = false;
						return;
					}
					if( oInvAmt == null || oInvAmt == DBNull.Value )
					{
						DRV.Row["INV_AMOUNT"] = 0.0;
					}
					if( oPurAmt == null || oPurAmt == DBNull.Value )
					{
						DRV.Row["PURCH_AMT"] = 0.0;
					}
					if( oGSTAmt == null || oGSTAmt == DBNull.Value )
					{
						DRV.Row["GST_AMT"] = 0.0;
					}
					if( oInvType != null && oInvType != DBNull.Value )
					{
						if( oInvType.ToString() == "M" )
						{
							if( oManChk != null && oManChk != DBNull.Value )
							{
                                string sSelect = @"exec AP_Validate_ManChk " + oManChk + @", '" + oSupp + @"'";
								int iResult = Convert.ToInt32( Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection ) );
                                if (iResult == -1)
								{
									_HeaderValid = false;
									e.ErrorText = "Check number has already been used.";
									HeaderError = e.ErrorText;
                                    gvHeader.FocusedColumn = colMANUAL_CHECK;
									e.Valid = false;
								}
                                else if (iResult == -2)
                                {
                                    _HeaderValid = false;
                                    e.ErrorText = "Manual check number has already been used for a different supplier.";
                                    HeaderError = e.ErrorText;
                                    gvHeader.FocusedColumn = colMANUAL_CHECK;
                                    e.Valid = false;
                                }
                                else if (iResult == -3)
                                {
                                    _HeaderValid = false;
                                    e.ErrorText = "Manual check number must be greater than zero.";
                                    HeaderError = e.ErrorText;
                                    gvHeader.FocusedColumn = colMANUAL_CHECK;
                                    e.Valid = false;
                                }
							}
							else
							{
								if( !colMANUAL_CHECK.Visible )
									colMANUAL_CHECK.Visible = true;
								_HeaderValid = false;
								e.ErrorText = "Manual Check Number Requred.";
								HeaderError = e.ErrorText;
								e.Valid = false;
								return;
							}
						}
					}

                    if (!PeriodValid())
                    {
                        BindingContext[dsHeaderSide1, "AP_INV_HEADER"].CancelCurrentEdit();                        					

                        e.ErrorText = "Unable to update; the selected invoice period cannot be less than the received date of a matched PO that has been selected for the invoice to be paid against.";
                        HeaderError = e.ErrorText;
                        e.Valid = false;
                        return;
                    }
					
					if( GSTChanged() )
					{
                        using (frmGSTExcept GSTExcept = new frmGSTExcept(Connection, DevXMgr))
                        {
                            GSTExcept.ShowDialog();

                            if (GSTExcept.DialogResult == DialogResult.Cancel)
                            {
                                e.ErrorText = "Invalid GST Exception.";
                                HeaderError = e.ErrorText;
                                e.Valid = false;
                                _HeaderValid = false;
                            }
                            else if (GSTExcept.DialogResult == DialogResult.OK)
                            {
                                object oException = GSTExcept.ucAPGSTE.radException.EditValue;
                                object oOther = GSTExcept.ucAPGSTE.lueOther.EditValue;
                                if (oException == null)
                                    oException = DBNull.Value;
                                if (oOther == null || Convert.ToInt32(oException) != 4)
                                    oOther = DBNull.Value;
                                DRV.Row["GST_EXCEPT"] = oException;
                                DRV.Row["GST_EXCEPT_ID"] = oOther;
                                ucAPGSTE.radException.EditValue = oException;
                                ucAPGSTE.lueOther.EditValue = oOther;
                            }
                        }
					}

                    //Validate adjustment original invoice acct period
                    if (oType.Equals("A"))
                    {
                        string sSQL = @"select acct_period, acct_year from ap_inv_header where INV_NO='" + oInvNo + @"'";
                        DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                if (dr != null)
                                {
                                    int iAdjYear = Convert.ToInt32(ucAccountingPicker1.SelectedYear);
                                    int iAdjPeriod = Convert.ToInt32(ucAccountingPicker1.SelectedPeriod);
                                    int iInvYear = Convert.ToInt32(dr["acct_year"]);
                                    int iInvPeriod = Convert.ToInt32(dr["acct_period"]);

                                    if (iInvYear > iAdjYear || (iInvYear == iAdjYear && iInvPeriod > iAdjPeriod))
                                    {
                                        _HeaderValid = false;

                                        string sInvPeriod = Connection.SQLExecutor.ExecuteScalar(@"select dbo.fn_GetPeriodNameNumericPrefix(" + iInvPeriod + @")", Connection.TRConnection).ToString();
                                        string sAdjPeriod = Connection.SQLExecutor.ExecuteScalar(@"select dbo.fn_GetPeriodNameNumericPrefix(" + iAdjPeriod + @")", Connection.TRConnection).ToString();
                                        e.ErrorText = "The selected invoice cannot exist in a period (" + sInvPeriod + " " + iInvYear + ") which is greater than the adjustment invoices period (" + sAdjPeriod + " " + iAdjYear + ").";
                                        HeaderError = e.ErrorText;
                                        gvHeader.FocusedColumn = colINV_NO;
                                        e.Valid = false;
                                    }
                                }
                            }
                        }			
                    }


					if( _HeaderValid && e.Valid )
					{
						HeaderError = "";
						
						object objHP = DRV.Row["HOLD_PCT"];
						if( objHP != null && objHP != DBNull.Value )
						{
							double dHoldP = Convert.ToDouble( objHP );
							if( dHoldP != 0 )
							{
								string sSelect = "select isnull(CALC_GST_ON_HOLDBACK,'F') from ap_setup";
								object oGSTonHB = ExecuteScalar( sSelect, TR_Conn );
								if( oGSTonHB != null && oGSTonHB != DBNull.Value )
								{
									if( oGSTonHB.ToString() == "F" )
									{
										object oPurchAmt = DRV.Row["PURCH_AMT"];
										if( oPurchAmt != null && oPurchAmt != DBNull.Value )
										{
											DRV.Row["HOLD_AMT"] = Math.Round( Convert.ToDouble( oPurchAmt ) * dHoldP * .01, 2, MidpointRounding.AwayFromZero );
                                            DRV.Row["HOLD_BAL"] = DRV.Row["HOLD_AMT"];
										}
										else
										{
											DRV.Row["HOLD_AMT"] = 0;
                                            DRV.Row["HOLD_BAL"] = 0;
										}
									}
									else if( oGSTonHB.ToString() == "T" )
									{
										object oInv_Amt = DRV.Row["INV_AMOUNT"];
										if( oInv_Amt != null && oInv_Amt != DBNull.Value )
										{
											DRV.Row["HOLD_AMT"] = Math.Round( Convert.ToDouble( oInv_Amt ) * dHoldP * .01, 2, MidpointRounding.AwayFromZero );
                                            DRV.Row["HOLD_BAL"] = DRV.Row["HOLD_AMT"];
										}
										else
										{
											DRV.Row["HOLD_AMT"] = 0;
                                            DRV.Row["HOLD_BAL"] = 0;
										}
									}
								}
							}
						}
						else
						{
							DRV.Row["HOLD_AMT"] = 0;
                            DRV.Row["HOLD_BAL"] = 0;
						}

						DRV.Row["ACCT_PERIOD"] = ucAccountingPicker1.SelectedPeriod;
						DRV.Row["ACCT_YEAR"] = ucAccountingPicker1.SelectedYear;	
						DRV.Row["BALANCE"] = DRV.Row["INV_AMOUNT"];
						DRV.Row["DATE_SAVED"] = DateTime.Now;	

						if( oID == null || oID == DBNull.Value )
						{
							oID = ExecuteScalar( "exec sp_getnextsystemid 'NEXT_AP_INV_HEADER_ID'", TR_Conn );
							DRV.Row["AP_INV_HEADER_ID"] = oID;
                            if (tcDetails.SelectedTabPage == tpMatchPO)
                                ucMPOR.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                            else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
                            {
                                ucUCPO.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                                ucSCPO.AP_INV_HEADER_ID = Convert.ToInt32(oID);
                            }
							daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oID;
							daInvDetail.Fill( dsInvDetail1 );
						}

						object oJNL_NO = DRV.Row["JOURNAL_NUMBER"];
						if( oJNL_NO == null || oJNL_NO == DBNull.Value )
						{
							string sSelect = "select isnull(next_ap_jnl_no,1) from ap_setup";
							oJNL_NO = ExecuteScalar( sSelect, TR_Conn );
							if( oJNL_NO != null && oJNL_NO != DBNull.Value )
							{
								DRV.Row["JOURNAL_NUMBER"] = oJNL_NO;
								DRV.Row["JOURNAL_LINE_NO"] = 1;
								string sUpdate = "update ap_setup set next_ap_jnl_no="+(Convert.ToInt32( oJNL_NO ) + 1);
								ExecuteNonQuery( sUpdate, TR_Conn );
							}
						}
						object oJNL_LN_NO = DRV.Row["JOURNAL_LINE_NO"];
						if( oJNL_LN_NO == null || oJNL_LN_NO == DBNull.Value )
						{
							string sSelect = "select max(JOURNAL_LINE_NO)+1 from ap_inv_header where JOURNAL_NUMBER="+DRV.Row["JOURNAL_NUMBER"];
							oJNL_LN_NO = ExecuteScalar( sSelect, TR_Conn );
							if( oJNL_LN_NO != null && oJNL_LN_NO != DBNull.Value )
							{
								DRV.Row["JOURNAL_LINE_NO"] = oJNL_LN_NO;
							}
						}

                        if (oPO_ID != null && oPO_ID != DBNull.Value)
                        {
                            string sSQL = @"select count(*) from po_header where isnull(pri_num,-1) <> -1 and po_id="+oPO_ID;
                            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                            if (oCNT == null || oCNT == DBNull.Value)
                                oCNT = 0;
                            if (Convert.ToInt32(oCNT) > 0)
                            {
                                int iPO_ID = Convert.ToInt32(oPO_ID);
                                if (!SubCon.SupplierValid(oSupp.ToString(), AP_SubcontractorCompliance.SupplierSubConValidator.Rule.Restrict, Convert.ToDateTime(DRV.Row["DUE_DATE"]), iPO_ID))
                                {
                                    _HeaderValid = false;
                                    e.ErrorText = CONST_SUPRESS_ERROR;
                                    HeaderError = e.ErrorText;
                                    e.Valid = false;
                                    return;
                                }

                                if (oID == null || oID == DBNull.Value)
                                    oID = -1;

                                SubCon.SupplierValid(oSupp.ToString(), AP_SubcontractorCompliance.SupplierSubConValidator.Rule.Warning, DateTime.Today, iPO_ID, Convert.ToInt32(oID));
                                if (!SubCon.SupplierValid(oSupp.ToString(), AP_SubcontractorCompliance.SupplierSubConValidator.Rule.Pre_Accrual, DateTime.Today, iPO_ID, Convert.ToInt32(oID)))
                                {
                                    DRV.Row["KC_ACCRUAL_STATUS"] = "Q";
                                }
                                else
                                {
                                    DRV.Row["KC_ACCRUAL_STATUS"] = DBNull.Value;
                                }
                            }
                        }

                        if (_AP_WFRequired && DRV.Row["WF_Approval_ID"] != null && DRV.Row["WF_Approval_ID"] != DBNull.Value)
                        {
                            object oStatus = DRV.Row["WF_STATUS"];
                            if (oStatus == null || oStatus == DBNull.Value)
                                oStatus = "";
                            if( oStatus.ToString().Equals(""))
                                DRV.Row["WF_STATUS"] = "Q";
                        }
                        else
                        {
                            DRV.Row["WF_STATUS"] = DBNull.Value;
                        }

                        if (_AP_ForceWF)
                        {
                            object oStatus = DRV.Row["WF_STATUS"];
                            if (oStatus == null || oStatus == DBNull.Value)
                                oStatus = "";
                            if (oStatus.ToString().Equals(""))
                                DRV.Row["WF_STATUS"] = "Q";
                        }                        
					}
				}
			}
			catch( DBConcurrencyException )
			{
				Popup.ShowPopup( this, "This record has been modified by another user and will be refreshed." );
				RefreshMe();
			}
		}

        private void gvHeader_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(e.RowHandle);
            if (e.Column == colINV_NO)
            {
                if (dr != null)
                {
                    object oType = dr["INVOICE_TYPE"];
                    if (oType == null || oType == DBNull.Value)
                        oType = "";
                    
                    if (oType.ToString().Equals("I")) // Invoice
                    {
                        e.RepositoryItem = repositoryItemTextEdit8;
                    }
                    else if (oType.ToString().Equals("M")) // Manual
                    {
                        e.RepositoryItem = repositoryItemTextEdit8;
                    }
                    else if (oType.ToString().Equals("A")) // Adjustment
                    {
                        e.RepositoryItem = ucIS;
                    }
                    else
                    {
                        e.RepositoryItem = repositoryItemTextEdit8;
                    }
                }
            }
            else if (e.Column == colPO_ID)
            {
                if (e.RowHandle == gvHeader.FocusedRowHandle)
                {
                    e.RepositoryItem = repositoryItemLookUpEdit8;
                }
                else
                {
                    e.RepositoryItem = repositoryItemLookUpEdit11;
                }
            }
            else if (e.Column == colStatus1)
            {
                if (dr != null)
                {
                    object oStatus = dr["KC_CONTRACTPO_STATUS"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals(""))
                        e.RepositoryItem = riEmpty;
                    else
                        e.RepositoryItem = riRouteStatus;
                }
            }
            else if (e.Column == colKC_ACCRUAL_STATUS)
            {
                if (dr != null)
                {
                    object oStatus = dr["KC_ACCRUAL_STATUS"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals(""))
                        e.RepositoryItem = riEmpty;
                    else
                        e.RepositoryItem = riRouteStatusPreAccrual;
                }
            }            
            else if (e.Column == colWF_STATUS)
            {
                if (dr != null)
                {
                    object oStatus = dr["WF_STATUS"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals(""))
                        e.RepositoryItem = riEmpty;
                    else
                        e.RepositoryItem = riRouteStatusWorkFlow;
                }
            }
            else if (e.Column == colWF_Approval_ID)
            {
                if (dr != null)
                {
                    object oStatus = dr["WF_STATUS"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "";
                    if (oStatus.Equals("Q") || oStatus.Equals("D") || oStatus.Equals("R"))
                        e.RepositoryItem = riWorkFlow;
                    else if (!_AP_ForceWF && oStatus.Equals(""))
                        e.RepositoryItem = riWorkFlow;
                    else
                        e.RepositoryItem = riWorkFlowRO;
                }
            }
            else if (e.Column == colMANUAL_CHECK)
            {
                if (dr != null)
                {
                    object oStatus = dr["INVOICE_TYPE"];
                    if (oStatus == null || oStatus == DBNull.Value)
                        oStatus = "I";
                    if (oStatus.Equals("M"))
                        e.RepositoryItem = riManualChkNo;
                    else
                        e.RepositoryItem = riManualChkNoRO;
                }
            }
        }

        private void riWorkFlow_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lue = sender as LookUpEdit;
            if( lue != null )
            {
                DataRow dr = gvHeader.GetFocusedDataRow();
                if (dr != null)
                {
                    object oWF_Approval_ID = lue.EditValue;
                    if (oWF_Approval_ID != null && oWF_Approval_ID != DBNull.Value)
                    {
                        object oWF_STATUS = dr["WF_STATUS"];
                        if (oWF_STATUS == null || oWF_STATUS == DBNull.Value)
                        {
                            dr.BeginEdit();
                            oWF_STATUS = "";
                            dr.EndEdit();
                        }

                        if (oWF_STATUS.ToString().Trim().Equals(""))
                        {
                            dr.BeginEdit();
                            dr["WF_STATUS"] = "Q";
                            dr.EndEdit();
                        }
                    }
                    else
                    {
                        if (!_AP_ForceWF)
                        {
                            dr.BeginEdit();
                            dr["WF_STATUS"] = DBNull.Value;
                            dr.EndEdit();
                        }
                    }
                }
            }
        }

        private void riWorkFlow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                DataRow dr = gvHeader.GetFocusedDataRow();
                if (dr != null)
                {
                    dr.BeginEdit();
                    gvHeader.SetFocusedRowCellValue("WF_Approval_ID", DBNull.Value);
                    dr["WF_Approval_ID"] = DBNull.Value;
                    dr.EndEdit();

                    if (!_AP_ForceWF)
                    {
                        dr.BeginEdit();
                        dr["WF_STATUS"] = DBNull.Value;
                        dr.EndEdit();
                    }
                }
            }
        }

		private void ClearDetail()
		{
			LoadHeaderSide();
			dsPO1.Clear();
			dsInvDetail1.Clear();
			POLevyLockDown();
		}

		private void gvHeader_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
		{
			bNewRow = true;
			ClearDetail();
			DataRow dr = gvHeader.GetDataRow( e.RowHandle );
			dr["NEW_INVOICE"] = "Y";
			dr["TRANS_DATE"] = DateTime.Now.ToString();
			dr["OPERATOR"] = Connection.MLUser;
			dr["CANCEL"] = "F";
			dr["ACCRUAL_FLAG"] = "U";
			dr["SOX_ROUTING"] = 0;
			dr["SOX_APPROVAL"] = 0;
			dr["HOLD"] = "F";
			dr["INV_DATE"] = DateTime.Now.Date;
			dr["DISCOUNT_DATE"] = DateTime.Now.Date;
			dr["DISCOUNT_AMOUNT"] = 0.0;
			dr["DISCOUNT_TAKEN"] = 0.0;
			dr["HOLD_PCT"] = 0.0;
			dr["HOLD_AMT"] = 0.0;
			dr["HOLD_BAL"] = 0.0;
			dr["GST_EXCEPT"] = 0;
			dr["INVOICE_TYPE"] = "I"; 
			dr["TRANS_TYPE"] = "INV";
			dr["INV_AMOUNT"] = 0.0;
			dr["BALANCE"] = 0.0;
			dr["PURCH_AMT"] = 0.0;
			dr["GST_AMT"] = 0.0;
			dr["REFERENCE"] = "";			
			dr["ACCRUAL_FLAG"] = "X";
			dr["CK_SELECT"] = "F";
			dr["COMMENT"] = "";
			if( UserWHSEID != -1 )
				dr["whse_id"] = UserWHSEID;
            if (_AP_ForceWF)
                dr["WF_STATUS"] = "Q";


			string sSelect = "select isnull(DEF_SUPP_FROM_PREV,'F') from ap_setup";
			object oDef = ExecuteScalar( sSelect, TR_Conn );
			if( oDef != null && oDef != DBNull.Value )
			{
				if( oDef.ToString() == "T" )
				{
					sSelect = "select top 1 supplier "+
						"from ap_inv_header a "+
						"LEFT OUTER JOIN working_mluser_supervisor w ON w.mluser = a.OPERATOR "+
						"WHERE (ISNULL(a.CANCEL, 'F') = 'F') AND (a.ACCRUAL_FLAG <> 'A') AND (w.username = '"+Connection.MLUser+"') "+
						"order by trans_date desc";
					object oSupp = ExecuteScalar( sSelect, TR_Conn );
					if( oSupp != null && oSupp != DBNull.Value )
					{
						dr["SUPPLIER"] = oSupp;
						gvHeader.FocusedColumn = colSUPPLIER;
						gvHeader.ShowEditor();
						object oEditor = gvHeader.ActiveEditor;
						if( oEditor != null )
						{					
							LookUpEdit lue = oEditor as LookUpEdit;
							lue.EditValue = oSupp;
							repositoryItemLookUpEdit5_EditValueChanged(lue,new System.EventArgs());
						}
					}
				}
			}

            bool bDefaultSet = false;
            sSelect = "select isnull(DEF_YEAR_PERIOD_FROM_PREV,'F') from ap_setup";
            oDef = ExecuteScalar(sSelect, TR_Conn);
            if (oDef != null && oDef != DBNull.Value)
            {
                if (oDef.ToString() == "T")
                {
                    sSelect = "select top 1 a.acct_period, a.acct_year " +
                        "from ap_inv_header a " +
                        "LEFT OUTER JOIN working_mluser_supervisor w ON w.mluser = a.OPERATOR " +
                        "WHERE (ISNULL(a.CANCEL, 'F') = 'F') AND (a.ACCRUAL_FLAG <> 'A') AND (w.username = '" + Connection.MLUser + "') " +
                        "order by trans_date desc";
                    object[] oResults = ExecuteDataRow(sSelect, TR_Conn);
                    if (oResults != null)
                    {
                        dr["ACCT_PERIOD"] = oResults[0];
                        dr["ACCT_YEAR"] = oResults[1];
                        ucAccountingPicker1.editPeriod.EditValue = oResults[0];
                        ucAccountingPicker1.editYear.EditValue = oResults[1];
                        bDefaultSet = true;
                    }
                }
            }

            if (!bDefaultSet)
            {
                ucAccountingPicker1.setDefaults();
            }

			gvHeader.RefreshRow( e.RowHandle );

			gvHeader.FocusedColumn = colINVOICE_TYPE;
			bNewRow = false;
		}

		private void gvHeader_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
		{
			CalculateRemaining();
			SaveHeader();			
			POLevyLockDown();
			LoadHeaderSide();
            IsSubContractor();
            SubContractChange();
		}

		private void AP_INV_HEADER_AP_INV_HEADERRowDeleted(object sender, AP_Invoice_Entry.dsInvHeader.AP_INV_HEADERRowChangeEvent e)
		{
			gvHeader_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs( gvHeader.FocusedRowHandle, gvHeader.FocusedRowHandle ) );
		}

		#endregion

		#region Detail Events

		private void gvDetail_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
		{
			if( gvHeader.RowCount > 0 )
			{
				DataRow head = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
                if (head != null)
                {
                    DataRow dr = gvDetail.GetDataRow(e.RowHandle);
                    dr["AP_INV_HEADER_ID"] = head["AP_INV_HEADER_ID"];
                    dr["SUPPLIER"] = head["SUPPLIER"];
                    dr["DOC_NO"] = head["INV_NO"];
                    dr["COMMENT"] = "";
                    dr["AMOUNT"] = txtUndist.EditValue;
                    dr["TRANS_TYPE"] = "INV";
                    dr["HOLD_AMT"] = txtRemain.EditValue;
                    dr["HOLD_BAL"] = txtRemain.EditValue;
                    dr["INV_BAL"] = 0.0;
                    object oSEQ = ExecuteScalar("select isnull(max(seq),0)+1 from ap_gl_alloc where supplier='" + head["SUPPLIER"] + "' and DOC_NO='" + head["INV_NO"] + "'", TR_Conn);
                    if (oSEQ != null && oSEQ != DBNull.Value)
                        dr["SEQ"] = oSEQ;
                    else
                        dr["SEQ"] = 1;
                    dr["AGA_LOT"] = "";
                    dr["AGA_BLOCK"] = "";
                    dr["AGA_PLAN"] = "";
                    dr["AGA_C_PROF_CNTR"] = "";
                    dr["AGA_C_LEASE_NUM"] = "";
                    dr["AGA_C_PROP_CD"] = "";
                    dr["AGA_R_PROF_CNTR"] = "";
                    dr["AGA_R_LEASE_NUM"] = "";
                    dr["AGA_R_PROP_CD"] = "";
                    dr["TRANSFER_FLAG"] = "N";
                    dr["prp_component"] = "";
                    dr["GST_AMT"] = 0.0;
                    dr["PURCH_AMT"] = 0.0;
                    dr["ITC"] = "N";
                    dr["GST_BAL"] = 0.0;
                    dr["CLEAR_REQUIRED"] = "F";
                    dr["COMM_STAT_APPLICABLE"] = "F";
                    dr["USE_COMM_PCT"] = "F";
                    dr["COMMISSION_PCT"] = 0;
                    dr["EXCH_RATE"] = head["EXCH_RATE"];
                    dr["PROJ_BUD_EXCEEDED"] = "F";
                    dr["HOURS"] = 0;
                    dr["RATE"] = 0;
                    dr["REFERENCE"] = "";

                    string sSelect = "select gl_account from supplier_master where supplier='" + head["SUPPLIER"] + "'";
                    dr["GL_ACCOUNT"] = ExecuteScalar(sSelect, TR_Conn);

                    sSelect = "select sub_code from supplier_master where supplier='" + head["SUPPLIER"] + "'";
                    dr["SUB_CODE"] = ExecuteScalar(sSelect, TR_Conn);

                    string sSQL = @"select COUNT(*) 
                        from AP_INV_HEADER h
                        join TERMS t on t.TERMS_ID=h.TERMS_ID
                        where h.AP_INV_HEADER_ID=" + head["AP_INV_HEADER_ID"] + @" and ISNULL(t.PaidWhenPaid,'F') = 'T' and isnull(h.po_id,-1) <> -1";
                    object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oCNT == null || oCNT == DBNull.Value)
                        oCNT = 0;
                    if (Convert.ToInt32(oCNT) > 0)
                    {
                        dr["AR_PWP_STATUS_ID"] = CONST_PWP_STATUS_OPEN;
                    }

                    if (gvHeader.FocusedRowHandle == 0)
                    {
                        dsDetPO1.Clear();
                        daDetPO.SelectCommand.CommandText = sdaDetPOSelect.Replace("@SUPPLIER", "'" + head["SUPPLIER"].ToString() + "'");
                        daDetPO.Fill(dsDetPO1);
                    }

                    gvDetail.RefreshRow(e.RowHandle);
                }
			}
			else
			{
				gvDetail.DeleteRow( e.RowHandle );
			}
            gvDetail.Focus();
			gvDetail.FocusedColumn = colGL_ACCOUNT;
		}

		private void gcDetail_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
		{
			try
			{
				gvDetail.CloseEditor();				
			
				if( e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.EndEdit )
				{
					ggDetail.SaveRecord( gvDetail.FocusedRowHandle );								
					e.Handled = true;
				}
                else if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
                {
                    DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oCB_ID = dr["CB_ID"];
                        if (oCB_ID != null && oCB_ID != DBNull.Value)
                        {
                            if (gvDetail.FocusedRowHandle != DevExpress.XtraGrid.GridControl.NewItemRowHandle)
                            {
                                try
                                {
                                    object oCur_CB_ID = dr["CB_ID", DataRowVersion.Current];

                                    if (Convert.ToInt32(oCur_CB_ID) == Convert.ToInt32(oCB_ID))
                                        return;
                                }
                                catch { }
                            }

                            if (Convert.ToInt32(oCB_ID) != -1)
                            {
                                string sSQL = @"exec CB_DeleteChargeBack '"+Connection.MLUser+@"', " + oCB_ID;
                                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                            }
                        }
                    }
                }
			}
			catch( DBConcurrencyException )
			{
				Popup.ShowPopup( this, "This record has been modified by another user and will be refreshed." );
				dsInvDetail1.Clear();
				daInvDetail.Fill( dsInvDetail1 );
			}
			CalculateRemaining();
		}

		private void gvDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			if( e.Valid )
			{                
				object obj = e.Row;
				if( obj != null )
				{
					DataRowView DRV = obj as DataRowView;

                    object oAmount = DRV.Row["AMOUNT"];
                    if (oAmount == null || oAmount == DBNull.Value)
                    {
                        e.ErrorText = "Amount must be set.";
                        e.Valid = false;
                        return;
                    }
                    if (Convert.ToDouble(oAmount) == 0)
                    {
                        e.ErrorText = "Amount cannot be zero.";
                        e.Valid = false;
                        return;
                    }

                    object oCB_ID = DRV.Row["CB_ID"];
                    if (oCB_ID == null || oCB_ID == DBNull.Value)
                        oCB_ID = -1;

                    if (Convert.ToInt32(oCB_ID) != -1 && Convert.ToDouble(oAmount) < 0)
                    {
                        e.ErrorText = "Amount must be positive if adding a chargeback.";
                        e.Valid = false;
                        return;                    
                    }
                    
                    if (Convert.ToInt32(oCB_ID) != -1)
                    {
                        string sSQL = @"select src_po_id from charge_back where cb_id = " + oCB_ID;
                        object oSRC_PO_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                        if (oSRC_PO_ID == null || oSRC_PO_ID == DBNull.Value)
                            oSRC_PO_ID = -1;
                        if( Convert.ToInt32(oSRC_PO_ID) != -1 )
                        {
                            sSQL = @"select isnull(pri_num,-1) from po_header where po_id = " + oSRC_PO_ID;
                            object oPO_PriID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                            if (oPO_PriID == null || oPO_PriID == DBNull.Value)
                                oPO_PriID = -1;

                            object oREFERENCE = DRV.Row["REFERENCE"];
                            if (oREFERENCE == null || oREFERENCE == DBNull.Value)
                                oREFERENCE = "";

                            if (Convert.ToInt32(oPO_PriID) == -1 && !oREFERENCE.Equals(""))
                            {
                                gvDetail.FocusedColumn = colREFERENCE1;
                                e.ErrorText = "The selected chargeback PO was not generated from a project. Reference is not allowed.";
                                e.Valid = false;
                                return;
                            }

                            object oPRI_ID = DRV.Row["pri_id"];
                            if (oPRI_ID == null || oPRI_ID == DBNull.Value)
                                oPRI_ID = -1;
                            if (Convert.ToInt32(oPO_PriID) != -1 && Convert.ToInt32(oPO_PriID) != Convert.ToInt32(oPRI_ID))
                            {
                                sSQL = @"select pri_code from proj_header where pri_id = "+ oPO_PriID;
                                object oPri_Code = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                                if (oPri_Code == null || oPri_Code == DBNull.Value)
                                    oPri_Code = "";
                                gvDetail.FocusedColumn = colREFERENCE1;
                                e.ErrorText = "Project reference must be set to the same project ("+ oPri_Code+@") the selected chargeback PO was generated from.";
                                e.Valid = false;
                                return;
                            }
                        }
                    }


                    if (!gcDetail.EmbeddedNavigator.Buttons.Remove.Enabled)
                    {
                        e.Valid = true;
                        return;
                    }

					object oGLAcct = DRV.Row["GL_ACCOUNT"];
					if( oGLAcct == null || oGLAcct.GetType() == typeof( DBNull ) )
					{
						e.ErrorText = "GL Account Required.";
						e.Valid = false;
						return;
					}

                    if (Connection.GL.RefReq(oGLAcct.ToString(), Connection.TRConnection) && (DRV.Row["REFERENCE"] == null || DRV.Row["REFERENCE"] == DBNull.Value || DRV.Row["REFERENCE"].Equals("")))
                    {
                        e.Valid = false;
                        gvDetail.FocusedColumn = colREFERENCE1;
                        e.ErrorText = "The selected GL account requires a reference.";
                        return;
                    }

                    object oHours = DRV.Row["HOURS"];
                    if (oHours == null || oHours == DBNull.Value)
                        oHours = 0;
                    if (Convert.ToDouble(oHours) < 0)
                    {
                        e.ErrorText = "Hours cannot be less than zero.";
                        e.Valid = false;
                        return;
                    }

					object oPO_TYPE = DRV.Row["PO_TYPE"];
					object oPO_REC_ID = DRV.Row["PO_REC_ID"];
					if( ( oPO_TYPE == null || oPO_TYPE == DBNull.Value ) && ( oPO_REC_ID != null && oPO_REC_ID != DBNull.Value ) )
					{
						e.ErrorText = "Type is required if PO # is selected.";
						e.Valid = false;
						return;					
					}
					if( ( oPO_TYPE != null && oPO_TYPE != DBNull.Value ) && ( oPO_REC_ID == null || oPO_REC_ID == DBNull.Value ) )
					{
						if( !oPO_TYPE.Equals( "I" ) && !oPO_TYPE.ToString().Trim().Equals( "" ) )
						{
							e.ErrorText = "PO # is required if Type is selected.";
							e.Valid = false;
							return;
						}
					}

					object oPO_ID = DRV.Row["PO_ID"];
					if( oPO_TYPE.Equals( "I" ) && ( oPO_ID == null || oPO_ID == DBNull.Value ) )
					{
						e.ErrorText = "PO Matching is required if 'Invoice' Type is selected.";
						e.Valid = false;
						return;
					}

                    object oCOMPANY_ALIAS = DRV.Row["COMPANY_ALIAS"];
                    if (oCOMPANY_ALIAS != null && oCOMPANY_ALIAS != DBNull.Value)
                    {
                        string sSQL = @"exec usp_IN_JournalDetailsCheckAccountByDatabase '" + gvDetail.GetFocusedRowCellValue("GL_ACCOUNT").ToString() + @"', '" + gvDetail.GetFocusedRowCellValue("COMPANY_ALIAS").ToString().Replace("'", "''") + @"'";
                        DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                        if( dt == null || dt.Rows.Count == 0 )
                        {
                            gvDetail.FocusedColumn = colGL_ACCOUNT;
                            e.ErrorText = "Invalid GL account for the company selected.";
                            e.Valid = false;
                            return;

                        }
                    }

					object oSEG = DRV.Row["SEG_CHANGE"];
					if( oSEG != null && oSEG != DBNull.Value )
					{
						oAmount = 0;
						if( DRV.Row["AMOUNT"] != null && DRV.Row["AMOUNT"] != DBNull.Value )
							oAmount = DRV.Row["AMOUNT"];

						object oID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
						if( oID == null || oID == DBNull.Value )
						{
							e.ErrorText = "Error creating segment allocation.";
							e.Valid = false;
							return;
						}

						string sExec = "declare @Message varchar(250) set @message = '' "+
							"exec sp_AP_SEG_ALLOC '"+oGLAcct+"', "+oSEG+", "+oAmount+", "+oID+", @message output "+
							"select @message";
						object objExec = ExecuteScalar( sExec, TR_Conn );
						if( objExec != null && objExec != DBNull.Value )
						{
							if( objExec.ToString() != "OK" )
							{
								e.ErrorText = " Error creating segment allocation.\r\n " +objExec.ToString()+"\r\n";
								e.Valid = false;
							}
							else
							{
								gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
								dsInvDetail1.Clear();
								daInvDetail.Fill( dsInvDetail1 );								
							}
						}
						else
						{
							e.ErrorText = "Error creating segment allocation.";
							e.Valid = false;
							return;
						}
					}
					else
					{
						object oID = DRV.Row["AP_GL_ALLOC_ID"];
						if( oID == null || oID == DBNull.Value )
						{
							oID = ExecuteScalar( "exec sp_getnextsystemid 'NEXT_AP_GL_ALLOC_ID'", TR_Conn );
							DRV.Row["AP_GL_ALLOC_ID"] = oID;
						}

						DRV.Row["HOLD_BAL"] = DRV.Row["HOLD_AMT"];
						DRV.Row["INV_BAL"] = DRV.Row["AMOUNT"];
					}										

					CalculateRemaining();
				}
			}
		}

		private void gvDetail_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
		{
			ggDetail.SaveRecord( gvDetail.FocusedRowHandle );

            //updates the row reference back to the chargeback.
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                object oCB_ID = dr["CB_ID"];
                if (oCB_ID == null || oCB_ID == DBNull.Value)
                    oCB_ID = -1;

                object oAP_GL_ALLOC_ID = dr["AP_GL_ALLOC_ID"];
                if (oAP_GL_ALLOC_ID == null || oAP_GL_ALLOC_ID == DBNull.Value)
                    oAP_GL_ALLOC_ID = -1;

                string sSQL = @"exec CB_UpdateChargeBack " + oCB_ID + @", 'AP', " + oAP_GL_ALLOC_ID;
                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

                CB_OnHdr();
            }

			CalculateRemaining();
		}

		private void ucAP_InvoiceEntry_RowDeleted(object sender, DataRowChangeEventArgs e)
		{
			CalculateRemaining();
		}

		#endregion

		#region Component Events

		private void dnHeaderNav_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
		{
			try
			{
				if( e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.EndEdit )
				{
					object oRowView = BindingContext[ dsHeaderSide1, "AP_INV_HEADER" ].Current;
					if( oRowView != null )
					{
						DataRowView DRV = oRowView as DataRowView;
                        object oApprovalID = DRV["WF_Approval_ID"];
                        if (oApprovalID != null && oApprovalID != DBNull.Value)
                        {
                            string sSelect = @"declare @message varchar(150)
                                exec WS_NonPORouteValidation " + oApprovalID + @", @message output select @message";
                            string sResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.WebConnection).ToString();
                            if (!sResult.Equals("OK"))
                            {
                                Popup.ShowPopup(sResult);
                                e.Handled = true;
                                return;
                            }
                        }

						object oID = DRV["AP_INV_HEADER_ID"];
						if( oID != null && oID != DBNull.Value )
						{								
							if( PeriodValid() )
							{
								DRV["ACCT_YEAR"] = ucAccountingPicker1.SelectedYear;
								DRV["ACCT_PERIOD"] = ucAccountingPicker1.SelectedPeriod;
								BindingContext[ dsHeaderSide1, "AP_INV_HEADER" ].EndCurrentEdit();	
								daHeaderSide.Update( dsHeaderSide1 );
                                CreateGSTRecord(oID);
                                UpdateWorkFlowStatus(oID);
								int iHandle = gvHeader.FocusedRowHandle;
								dsInvHeader1.Clear();
								daInvHeader.Fill( dsInvHeader1 );
                                SizeHeaderColumns();
								gvHeader.FocusedRowHandle = iHandle;
								gvHeader_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs( iHandle, iHandle ) );
							}
							else
							{
                                Popup.ShowPopup("Unable to update; the selected invoice period cannot be less than the received date of a matched PO that has been selected for the invoice to be paid against." );
								BindingContext[ dsHeaderSide1, "AP_INV_HEADER" ].CancelCurrentEdit();
								LoadHeaderSide();
							}
							e.Handled = true;
						}
					}
				}	
				else if( e.Button.ButtonType == DevExpress.XtraEditors.NavigatorButtonType.CancelEdit )
				{
					LoadHeaderSide();
				}
			}
			catch( DBConcurrencyException )
			{
				Popup.ShowPopup( this, "This record has been modified by another user and will be refreshed." );
				RefreshMe();
			}		
		}

        private void UpdateDetailHoldback(object oID)
        {
            string sExec = "exec AP_ApplyDetailHoldback " + oID;
            Connection.SQLExecutor.ExecuteNonQuery(sExec, Connection.TRConnection);
        }

        private void UpdateWorkFlowStatus(object oID)
        {
            string sExec = "exec AP_WorkflowStatusUpdate "+oID;
            Connection.SQLExecutor.ExecuteNonQuery(sExec, Connection.TRConnection);
        }

		private bool PeriodValid()
		{
			bool Valid = true;
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oPO_ID = dr["PO_ID"];
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
					int Year = Convert.ToInt32( ucAccountingPicker1.SelectedYear );
					int Period = Convert.ToInt32( ucAccountingPicker1.SelectedPeriod );
					string sSelect = "select max(r.ACCOUNTING_YEAR) "+
						"from po_rec_header r "+
						"join po_header p on p.po_id=r.po_id "+
						"join po_rec_detail d on d.po_rec_id=r.po_rec_id and isnull(d.receiver_match_status,'F')='F' "+
						"join ap_receiver a on a.receiver=r.receiver_number "+
						"join ap_receiver_det rd on rd.ap_rec_entry_no=a.ap_rec_entry_no and rd.po_rec_detail_id=d.po_rec_detail_id "+
						"where r.po_id="+oPO_ID+" and isnull(rd.amount,0) <> 0";
					object oYear = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
		
					if( oYear == null || oYear == DBNull.Value )
					{
						return true;
					}
					sSelect = "select max(r.ACCOUNTING_PERIOD) "+
						"from po_rec_header r "+
						"join po_header p on p.po_id=r.po_id "+
						"join po_rec_detail d on d.po_rec_id=r.po_rec_id and isnull(d.receiver_match_status,'F')='F' "+
						"join ap_receiver a on a.receiver=r.receiver_number "+
						"join ap_receiver_det rd on rd.ap_rec_entry_no=a.ap_rec_entry_no and rd.po_rec_detail_id=d.po_rec_detail_id "+
						"where r.po_id="+oPO_ID+" and isnull(rd.amount,0) <> 0 and r.ACCOUNTING_YEAR="+oYear;
					object oPeriod = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );

					if( Period < Convert.ToInt32( oPeriod ) && Year == Convert.ToInt32( oYear ) || Year < Convert.ToInt32( oYear ) )
					{
						Valid = false;
					}
				}
			}
			return Valid;

		}

		private void GL_Repository_SubCodeUpdated()
		{
			DataRow dr = gvDetail.GetDataRow( gvDetail.FocusedRowHandle);
			if( dr != null )
			{
				gvDetail.SetFocusedRowCellValue( colSUB_CODE, DBNull.Value );

				if( GL_Repository.GL_SUB_CODE != null && GL_Repository.GL_SUB_CODE != "" )
				{
					dr["SUB_CODE"] = GL_Repository.GL_SUB_CODE;
					gvDetail.SetFocusedRowCellValue( colSUB_CODE, GL_Repository.GL_SUB_CODE );
				}
			}			
		}
        
		private void hlBalance_Click(object sender, System.EventArgs e)
		{
			if( gvHeader.RowCount > 0 )
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oID = dr["AP_INV_HEADER_ID"];
					if( oID != null && oID != DBNull.Value )
					{
						if( Popup.ShowPopup( this, "Balance Invoice Header Amounts From Detail?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
						{
							double dAmt = 0;
							string sSelect = "select sum(isnull(AMOUNT,0)) from ap_gl_alloc where AP_INV_HEADER_ID="+oID;
							object oAmt = ExecuteScalar( sSelect, TR_Conn );
							if( oAmt != null && oAmt != DBNull.Value )
							{
								dAmt = Convert.ToDouble( oAmt );
							}
							dr["INV_AMOUNT"] = dAmt;
							object oDiscPCT = 0;
							if( txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value )
								oDiscPCT = txtDiscP.EditValue;
							dr["DISCOUNT_AMOUNT"] = Math.Round( dAmt * Convert.ToDouble( oDiscPCT ) * .01, 2 );
                            
                            using (TextEdit te = new TextEdit())
                            {
                                te.EditValue = dAmt;
                                repositoryItemTextEdit1_EditValueChanged(te, null);
                            }
							
							CalculateRemaining();
                            LoadHeaderSide();
						}
					}
				}
			}
		}		

		private void hlNewInv_Click(object sender, System.EventArgs e)
		{
			gcHeader.EmbeddedNavigator.Buttons.DoClick(gcHeader.EmbeddedNavigator.Buttons.Append);
		}

		private void hlDeleteInv_Click(object sender, System.EventArgs e)
		{
			ggHeader_AllowDelete(null,gvHeader.GetDataRow(gvHeader.FocusedRowHandle));
		}

		private void hlRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshMe();
		}

		private void txtHoldA_EditValueChanged(object sender, System.EventArgs e)
		{
			if( txtHoldA.EditorContainsFocus )
			{
				object oHoldA = txtHoldA.EditValue;
				if( oHoldA != null && oHoldA != DBNull.Value )
				{
					object oRowView = BindingContext[ dsHeaderSide1, "AP_INV_HEADER"].Current;
					if( oRowView != null )
					{
                        string sSelect = "select isnull(CALC_GST_ON_HOLDBACK,'F') from ap_setup";
                        object oCalcGSTHold = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                        if (oCalcGSTHold == null || oCalcGSTHold == DBNull.Value)
                            oCalcGSTHold = "F";

                        if (oCalcGSTHold.Equals("F"))
                        {
                            DataRowView DRV = oRowView as DataRowView;

                            object oPurchAmt = DRV["PURCH_AMT"];
                            if (oPurchAmt != null && oPurchAmt != DBNull.Value)
                            {
                                if (Convert.ToDouble(oPurchAmt) != 0)
                                    DRV["HOLD_PCT"] = Math.Round(Convert.ToDouble(oHoldA) / Convert.ToDouble(oPurchAmt) * 100, 2, MidpointRounding.AwayFromZero);
                                else
                                    DRV["HOLD_PCT"] = 0;
                                DRV["HOLD_AMT"] = oHoldA;
                                DRV["HOLD_BAL"] = oHoldA;
                                txtHoldA.EditValue = DRV["HOLD_AMT"];
                            }
                            else
                            {
                                DRV["HOLD_PCT"] = 0;
                                DRV["HOLD_AMT"] = 0;
                                DRV["HOLD_BAL"] = 0;
                                txtHoldA.EditValue = DRV["HOLD_AMT"];
                            }
                        }
                        else
                        {
                            DataRowView DRV = oRowView as DataRowView;

                            object oInvAmt = DRV["INV_AMOUNT"];
                            if (oInvAmt != null && oInvAmt != DBNull.Value)
                            {
                                if (Convert.ToDouble(oInvAmt) != 0)
                                    DRV["HOLD_PCT"] = Math.Round(Convert.ToDouble(oHoldA) / Convert.ToDouble(oInvAmt) * 100, 2, MidpointRounding.AwayFromZero);
                                else
                                    DRV["HOLD_PCT"] = 0;
                                DRV["HOLD_AMT"] = oHoldA;
                                DRV["HOLD_BAL"] = oHoldA;
                                txtHoldA.EditValue = DRV["HOLD_AMT"];
                            }
                            else
                            {
                                DRV["HOLD_PCT"] = 0;
                                DRV["HOLD_AMT"] = 0;
                                DRV["HOLD_BAL"] = 0;
                                txtHoldA.EditValue = DRV["HOLD_AMT"];
                            }
                        }
					}
				}
				deHoldDue.DateTime = DateTime.Now.Date;			
			}
		}

		private void txtHoldP_EditValueChanged(object sender, System.EventArgs e)
		{
			if( txtHoldP.EditorContainsFocus )
			{
				deHoldDue.DateTime = DateTime.Now.Date;

				object oHoldP = txtHoldP.EditValue;
				if( oHoldP != null && oHoldP != DBNull.Value )
				{
					double dHoldP = Convert.ToDouble( oHoldP );
								
					DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
					if( dr != null )
					{

						object oRowView = BindingContext[ dsHeaderSide1, "AP_INV_HEADER"].Current;
						if( oRowView != null )
						{
							DataRowView DRV = oRowView as DataRowView;

							string sSelect = "select isnull(CALC_GST_ON_HOLDBACK,'F') from ap_setup";
							object oGSTonHB = ExecuteScalar( sSelect, TR_Conn );
							if( oGSTonHB != null && oGSTonHB != DBNull.Value )
							{
								if( oGSTonHB.ToString() == "F" )
								{
									object oPurchAmt = DRV["PURCH_AMT"];
									if( oPurchAmt != null && oPurchAmt != DBNull.Value )
									{
										DRV["HOLD_AMT"] = Math.Round( Convert.ToDouble( oPurchAmt ) * dHoldP * .01, 2, MidpointRounding.AwayFromZero );
                                        DRV["HOLD_BAL"] = Math.Round(Convert.ToDouble(oPurchAmt) * dHoldP * .01, 2, MidpointRounding.AwayFromZero);
										txtHoldA.EditValue = DRV["HOLD_AMT"];
									}
									else
									{
										DRV["HOLD_AMT"] = 0;
                                        DRV["HOLD_BAL"] = 0;
										txtHoldA.EditValue = DRV["HOLD_AMT"];
									}
								}
								else if( oGSTonHB.ToString() == "T" )
								{
									object oInvAmt = DRV["INV_AMOUNT"];
									if( oInvAmt != null && oInvAmt != DBNull.Value )
									{
										DRV["HOLD_AMT"] = Math.Round( Convert.ToDouble( oInvAmt ) * dHoldP * .01, 2, MidpointRounding.AwayFromZero );
                                        DRV["HOLD_BAL"] = Math.Round(Convert.ToDouble(oInvAmt) * dHoldP * .01, 2, MidpointRounding.AwayFromZero);
										txtHoldA.EditValue = DRV["HOLD_AMT"];
									}
									else
									{
										DRV["HOLD_AMT"] = 0;
                                        DRV["HOLD_BAL"] = 0;
										txtHoldA.EditValue = DRV["HOLD_AMT"];
									}
								}
							}
						
						}
					}
				}
			}
		}

		private void ucMPOR_AmountUpdated(double Amount)
		{
			b_DontFire = true;
			double PST = 0;
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
				object oPO_ID = dr["PO_ID"];
				AutoUpdate = true;
				gvHeader.FocusedColumn = colPURCH_AMT;
				gvHeader.ShowEditor();
				object oEditor = gvHeader.ActiveEditor;
				if( oEditor != null )
				{					
					string sExec = "exec sp_APCreateGLAlloc "+oAP_INV_HEADER_ID+", "+oPO_ID+", 'T', 'F',  '"+Connection.MLUser+"', 'M'";
					object oPST = ExecuteScalar( sExec, TR_Conn );					
					if( oPST != null && oPST != DBNull.Value )
						PST = Convert.ToDouble( oPST );

                    dr["PURCH_AMT"] = Math.Round(Amount + PST, 2, MidpointRounding.AwayFromZero);								
					SetGSTInvAmount( Amount, PST );
					dr["BALANCE"] = dr["INV_AMOUNT"];

                    gcHeader.EmbeddedNavigator.Buttons.DoClick(gcHeader.EmbeddedNavigator.Buttons.EndEdit);
					SaveHeader();
                    CalculateRemaining();
				}
				AutoUpdate = false;
			}
			b_DontFire = false;
		}

		private void ucUCPO_AmountUpdated(double Amount)
		{
			b_DontFire = true;
			double PST = 0;
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
				object oPO_ID = dr["PO_ID"];
				AutoUpdate = true;
				gvHeader.FocusedColumn = colPURCH_AMT;
				gvHeader.ShowEditor();
				object oEditor = gvHeader.ActiveEditor;
				if( oEditor != null )
				{
					string sExec = "exec sp_APCreateGLAlloc "+oAP_INV_HEADER_ID+", "+oPO_ID+", 'F', 'F', '"+Connection.MLUser+"', 'C'";
					object oPST = ExecuteScalar( sExec, TR_Conn );					
					if( oPST != null && oPST != DBNull.Value )
						PST = Convert.ToDouble( oPST );

                    double newPURCH_AMT = Math.Round(Amount + PST, 2, MidpointRounding.AwayFromZero);

                    dr.BeginEdit();
                    dr["PURCH_AMT"] = newPURCH_AMT;
					SetGSTInvAmount( Amount, PST );
					dr["BALANCE"] = dr["INV_AMOUNT"];

                    CheckRouting(dr);

                    gcHeader.EmbeddedNavigator.Buttons.DoClick(gcHeader.EmbeddedNavigator.Buttons.EndEdit);
					SaveHeader();
                    RoutingLock();
                    CalculateRemaining();
				}
				AutoUpdate = false;
			}
			b_DontFire = false;
		}

        private void CheckRouting(DataRow dr)
        {
            if (_WFRequired)
            {
                bool bAutoApproval = false;

                string sSelect = "select isnull(CONTRACT_PO_AUTO_APPROVAL,'F') from po_setup";
                object obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.Equals("T"))
                    {
                        bAutoApproval = true;
                    }
                }

                string sSQL = @"select COUNT(*) from approval_topic where ID = 58 and ISNULL(active,0) = 1";
                object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
                if (oCNT == null || oCNT == DBNull.Value)
                    oCNT = 0;
                if (Convert.ToInt32(oCNT) == 1)
                {
                    if (bAutoApproval)
                    {
                        sSelect = "select isnull(CONTRACT_PO_FORCED_ROUTING,'F') from supplier_master where supplier='" + dr["SUPPLIER"] + "'";
                        obj = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                        if (obj != null && obj != DBNull.Value)
                        {
                            if (obj.Equals("T"))
                            {
                                dr["KC_CONTRACTPO_STATUS"] = "Q";
                                RecallRequest();
                            }
                        }
                    }
                    else
                    {
                        dr["KC_CONTRACTPO_STATUS"] = "Q";
                        RecallRequest();
                    }
                }
            }
        }

        private void RoutingLock()
        {
            hlQuickChk.Enabled = true;
            hlOverrideCompliance.Enabled = false;

            DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
            if (dr != null)
            {
                bool bLockDown = false;
                bool bRoutingLinkLock = false;
                object obj = dr["KC_CONTRACTPO_STATUS"];
                object obj2 = dr["KC_ACCRUAL_STATUS"];
                object oFROM_WEB = dr["FROM_WEB"];
                object oWF_Status = dr["WF_STATUS"];
                object oIS_CB = dr["IS_CB"];
                object oLevy = dr["Levy"];
                if (obj == null || obj == DBNull.Value)
                    obj = "";
                if (obj2 == null || obj2 == DBNull.Value)
                    obj2 = "";
                if (oFROM_WEB == null || oFROM_WEB == DBNull.Value)
                    oFROM_WEB = "F";
                if (oWF_Status == null || oWF_Status == DBNull.Value)
                    oWF_Status = "";
                if (oIS_CB == null || oIS_CB == DBNull.Value)
                    oIS_CB = "F";
                if (oLevy == null || oLevy == DBNull.Value)
                    oLevy = false;

                if (obj.Equals("Q") || obj2.Equals("Q") || oWF_Status.Equals("Q"))
                {
                    hlOverrideCompliance.Enabled = true;
                    hlQuickChk.Enabled = false;
                }
                else if (obj.Equals("R") || obj.Equals("D") || obj2.Equals("R") || obj2.Equals("D") || oWF_Status.Equals("R") || oWF_Status.Equals("D"))
                {
                    hlOverrideCompliance.Enabled = true;
                    bRoutingLinkLock = true;
                    hlQuickChk.Enabled = false;
                }
                else if (obj.Equals("P") || obj2.Equals("P") || oWF_Status.Equals("P"))
                {
                    bLockDown = true;
                    hlOverrideCompliance.Enabled = true;
                    hlQuickChk.Enabled = false;
                }

                if (oFROM_WEB.Equals("T"))
                {
                    bLockDown = true;
                    hlQuickChk.Enabled = true;
                }

                if (oIS_CB.Equals("T"))
                {
                    bLockDown = true;
                    hlQuickChk.Enabled = false;
                }
                if (Convert.ToBoolean(oLevy))
                {
                    bLockDown = true;
                }

                if (bLockDown)
                {
                    gvDetail.OptionsBehavior.Editable = false;
                    gcDetail.EmbeddedNavigator.Enabled = false;
                    for (int i = 0; i < gvDetail.Columns.Count; i++)
                    {
                        if (gvDetail.Columns[i] != colCOMMENT1)
                            gvDetail.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    ucMPOR.ReadOnly = true;
                    ucUCPO.ReadOnly = true;
                    ucSCPO.ReadOnly = true;
                    dpRemit.Enabled = false;
                    dockPanel5.Enabled = false;
                    dockPanel6.Enabled = false;
                    
                    _DefaultsEnabled = false;
                    LockDefaults();
                    for (int i = 0; i < gvHeader.Columns.Count; i++)
                    {
                        if (gvHeader.Columns[i].Name != "colStatus1" && gvHeader.Columns[i].Name != "colKC_ACCRUAL_STATUS" && gvHeader.Columns[i].Name != "colfrom_web" && gvHeader.Columns[i].Name != "colWF_STATUS")
                            gvHeader.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    
                    gcHeader.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
                    gcHeader.EmbeddedNavigator.Buttons.Edit.Enabled = false;
                    gcHeader.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
                    gcHeader.EmbeddedNavigator.Buttons.Remove.Enabled = false;
                    
                    hlDeleteInv.Enabled = false;
                    hlBalance.Enabled = false;
                    hlQuickChk.Enabled = false;
                    hlChangeSupp.Enabled = false;
                    hlChargeBack.Enabled = false;
                    hlMultiCBEntry.Enabled = false;

                    if (oIS_CB.Equals("T"))
                    {
                        colGST_AMT.OptionsColumn.AllowEdit = true;
                        gcHeader.EmbeddedNavigator.Buttons.CancelEdit.Enabled = true;
                        gcHeader.EmbeddedNavigator.Buttons.Edit.Enabled = true;
                        gcHeader.EmbeddedNavigator.Buttons.EndEdit.Enabled = true;
                    }
                    if (oFROM_WEB.Equals("T"))
                    {
                        hlQuickChk.Enabled = true;
                    }
                }
                else
                {
                    if (!_PO_LOCKDOWN)
                    {
                        hlBalance.Enabled = true;
                        gvDetail.OptionsBehavior.Editable = true;
                        gcDetail.EmbeddedNavigator.Enabled = true;
                        for (int i = 0; i < gvDetail.Columns.Count; i++)
                        {
                            if (gvDetail.Columns[i] != colCOMMENT1 && gvDetail.Columns[i] != colSUB_CODE && gvDetail.Columns[i] != colRATE)
                                gvDetail.Columns[i].OptionsColumn.AllowEdit = true;
                        }
                        hlQuickChk.Enabled = true;
                    }
                    ucMPOR.ReadOnly = false;
                    ucUCPO.ReadOnly = false;
                    ucSCPO.ReadOnly = false;
                    dpRemit.Enabled = true;
                    dockPanel5.Enabled = true;
                    dockPanel6.Enabled = true;
                    _DefaultsEnabled = true;
                    LockDefaults();
                    for (int i = 0; i < gvHeader.Columns.Count; i++)
                    {
                        if (gvHeader.Columns[i].Name != "colStatus1" && gvHeader.Columns[i].Name != "colKC_ACCRUAL_STATUS" && gvHeader.Columns[i].Name != "colfrom_web" && gvHeader.Columns[i].Name != "colWF_STATUS" )
                            gvHeader.Columns[i].OptionsColumn.AllowEdit = true;
                    }
                    
                    gcHeader.EmbeddedNavigator.Buttons.CancelEdit.Enabled = true;
                    gcHeader.EmbeddedNavigator.Buttons.Edit.Enabled = true;
                    gcHeader.EmbeddedNavigator.Buttons.EndEdit.Enabled = true;
                    gcHeader.EmbeddedNavigator.Buttons.Remove.Enabled = true;

                    hlDeleteInv.Enabled = true;                    
                    hlChangeSupp.Enabled = true;
                    hlChargeBack.Enabled = true;
                    hlMultiCBEntry.Enabled = true;

                    if (!_HoldbackEdit)
                    {
                        colHOLD_PCT.OptionsColumn.AllowEdit = false;
                        colHOLD_AMT.OptionsColumn.AllowEdit = false;
                        colHOLD_AMT1.OptionsColumn.AllowEdit = false;
                        txtHoldP.Properties.ReadOnly = true;
                        txtHoldA.Properties.ReadOnly = true;
                    }
                    else
                    {
                        if (!_HoldbackEdit)
                        {
                            colHOLD_PCT.OptionsColumn.AllowEdit = true;
                            colHOLD_AMT.OptionsColumn.AllowEdit = true;
                            colHOLD_AMT1.OptionsColumn.AllowEdit = true;
                            txtHoldP.Properties.ReadOnly = false;
                            txtHoldA.Properties.ReadOnly = false;
                        }
                    }
                }

                if (bRoutingLinkLock)
                {
                    hlQuickChk.Enabled = false;
                    hlChangeSupp.Enabled = false;
                }

                if (oIS_CB.Equals("T"))
                {
                    hlDeleteInv.Enabled = true;
                    gcHeader.EmbeddedNavigator.Buttons.Remove.Enabled = true;
                }
                if (Convert.ToBoolean(oLevy))
                {
                    colLevy.OptionsColumn.AllowEdit = true;
                }
            }
        }

        private void LockDefaults()
        {
            DevExpress.XtraLayout.LayoutControlItem lci;
            for (int i = 0; i < lcDefaults.Items.Count; i++)
            {
                if (lcDefaults.Items[i].GetType() == typeof(DevExpress.XtraLayout.LayoutControlItem))
                {
                    lci = ((DevExpress.XtraLayout.LayoutControlItem)lcDefaults.Items[i]);
                    if (_DefaultsEnabled && (lci.Name.Equals(lciInvoiceType.Name) || lci.Name.Equals(lciCurrency.Name) ||
                        lci.Name.Equals(lciTerms.Name) || lci.Name.Equals(lciDiscountPct.Name)))
                        lci.Control.Enabled = false;
                    else
                        lci.Control.Enabled = _DefaultsEnabled;
                }
            }
        }

		#endregion

		#region Repository Events

		private void repositoryItemLookUpEdit5_EditValueChanged(object sender, System.EventArgs e)
		{
			CodeChanging = true;
			dsPO1.Clear();
            ClearRORemit();		
	
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				dr["PO_ID"] = DBNull.Value;
			

				LookUpEdit lue = sender as LookUpEdit;
				if( lue.EditValue != null && lue.EditValue != DBNull.Value )
				{
					string Supplier = lue.EditValue.ToString();
					ucIS.SupplierCode = Supplier;

					string sSelect = "select Terms_id from supplier_master where supplier = '"+Supplier+"'";                    
                    dr["TERMS_ID"] = ExecuteScalar(sSelect, TR_Conn);

					LoadDefaultSupplierInfo( Supplier );
                    LoadRORemit();
					CalculateTerms( Supplier, null );
				
					daPO.SelectCommand.Parameters["@supplier"].Value = Supplier;
					daPO.Fill( dsPO1 );

					ClearDetailInvType( dr["AP_INV_HEADER_ID"] );
				}
				else
				{
					ucIS.SupplierCode = "";
				}
				CheckMiscSupp( lue.EditValue );

			}
			CodeChanging = false;
		}

		private void ClearDetailInvType( object AP_INV_HEADER_ID )
		{
			if( AP_INV_HEADER_ID != null && AP_INV_HEADER_ID != DBNull.Value )
			{
				string sUpdate = "update ap_gl_alloc set po_rec_id=null, po_type=null where ap_inv_header_id="+AP_INV_HEADER_ID;
				Connection.SQLExecutor.ExecuteNonQuery( sUpdate, Connection.TRConnection );

				dsInvDetail1.Clear();
				daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = AP_INV_HEADER_ID;
				daInvDetail.Fill( dsInvDetail1 );
			}
		}	

		private bool b_DontFire = false;

		private void repositoryItemTextEdit1_EditValueChanged(object sender, System.EventArgs e)
		{
			TextEdit te = sender as TextEdit;
			if( te != null )
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oAmt = te.EditValue;
					if( oAmt != null && oAmt != DBNull.Value )
					{
						double dAmt = Convert.ToDouble( oAmt );
						double dPA = 0;
						double dGST = 0;						

						object oPO_ID = dr["PO_ID"];
						if( oPO_ID != null && oPO_ID != DBNull.Value )
						{
							object oPA = dr["PURCH_AMT"];
							if( oPA != null && oPA != DBNull.Value )
							{
								dPA = Convert.ToDouble( oPA );
							}

                            dr["GST_AMT"] = Math.Round(dAmt - dPA, 2, MidpointRounding.AwayFromZero);
						}
						else
						{
							object oPCT;
							double dGSTPCT = 0;
							double dPSTPCT = 0;
							object oGST = dr["GST_CODE"];
							if( oGST != null && oGST != DBNull.Value )
							{
								string sSelect = "select ( gst_pct * .01 ) from GST_CODES where gst_code='"+oGST+"'";
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dGSTPCT = Convert.ToDouble( oPCT );
							}
							object oPST = dr["SALES_TAX_ID"];
							if( oPST != null && oPST != DBNull.Value )
							{
								string sSelect = "select ( sales_tax * .01 ) from sales_taxes where sales_tax_id="+oPST;
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dPSTPCT = Convert.ToDouble( oPCT );
							}
							double dPAminusPST = dAmt / (1+dPSTPCT+dGSTPCT);
							
							dPA = dPAminusPST * (1+dPSTPCT);
							dGST = dPAminusPST * dGSTPCT; 

							dr["PURCH_AMT"] = Math.Round(dPA, 2, MidpointRounding.AwayFromZero);
                            dr["GST_AMT"] = Math.Round(dGST, 2, MidpointRounding.AwayFromZero);
						}			
			
						object oDiscPCT = 0;
						if( txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value )
							oDiscPCT = txtDiscP.EditValue;
                        dr["DISCOUNT_AMOUNT"] = Math.Round(Math.Round(dPA, 2, MidpointRounding.AwayFromZero) * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);
							
						object objHP = dr["HOLD_PCT"];
						if( objHP != null && objHP != DBNull.Value )
						{
							dr["HOLD_AMT"] = Math.Round( Convert.ToDouble( objHP ) * dPA * .01, 2, MidpointRounding.AwayFromZero );
                            dr["HOLD_BAL"] = dr["HOLD_AMT"];
						}
						else
						{
							dr["HOLD_AMT"] = 0;
                            dr["HOLD_BAL"] = 0;
						}
					}
				}
			}
		}

		private void repositoryItemTextEdit3_EditValueChanged(object sender, System.EventArgs e)
		{
			TextEdit te = sender as TextEdit;
			if( te != null )
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oInvAmt = dr["INV_AMOUNT"];
					if( oInvAmt != null && oInvAmt != DBNull.Value )
					{
						double dInvAmt = Convert.ToDouble( oInvAmt );
						
						object oAmt = te.EditValue;
						if( oAmt != null && oAmt != DBNull.Value )
						{
							double dPA = Convert.ToDouble( oAmt );
							double dGST = 0;
							object oPCT;
							double dGSTPCT = 0;
							double dPSTPCT = 0;
							object oGST = dr["GST_CODE"];
							if( oGST != null && oGST != DBNull.Value )
							{
								string sSelect = "select ( gst_pct * .01 ) from GST_CODES where gst_code='"+oGST+"'";
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dGSTPCT = Convert.ToDouble( oPCT );
							}
							object oPST = dr["SALES_TAX_ID"];
							if( oPST != null && oPST != DBNull.Value )
							{
								string sSelect = "select ( sales_tax * .01 ) from sales_taxes where sales_tax_id="+oPST;
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dPSTPCT = Convert.ToDouble( oPCT );
							}


                            double dPAminusPST = Math.Round(dPA / (dPSTPCT + 1), 2, MidpointRounding.AwayFromZero);
                            dGST = Math.Round(dPAminusPST * dGSTPCT, 2, MidpointRounding.AwayFromZero);

                            dr["INV_AMOUNT"] = Math.Round(dGST + dPA, 2, MidpointRounding.AwayFromZero);
                            dr["GST_AMT"] = Math.Round(dGST, 2, MidpointRounding.AwayFromZero);

							object oDiscPCT = 0;
							if( txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value )
								oDiscPCT = txtDiscP.EditValue;
							dr["DISCOUNT_AMOUNT"] = Math.Round( dPA * Convert.ToDouble( oDiscPCT ) * .01, 2, MidpointRounding.AwayFromZero );

							object objHP = dr["HOLD_PCT"];
							if( objHP != null && objHP != DBNull.Value )
							{
                                dr["HOLD_AMT"] = Math.Round(Convert.ToDouble(objHP) * dPA * .01, 2, MidpointRounding.AwayFromZero);
                                dr["HOLD_BAL"] = dr["HOLD_AMT"];
							}
							else
							{
								dr["HOLD_AMT"] = 0;
                                dr["HOLD_BAL"] = 0;
							}
						}
						
					}
				}
			}
		}

		private void SetGSTInvAmount( double Purchase, double PST )
		{
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{	
				double dGST = 0;
				object oPCT;
				double dGSTPCT = 0;
				object oGST = dr["GST_CODE"];
				if( oGST != null && oGST != DBNull.Value )
				{
					string sSelect = "select ( gst_pct * .01 ) from GST_CODES where gst_code='"+oGST+"'";
					oPCT = ExecuteScalar( sSelect, TR_Conn );
					if( oPCT != null && oPCT != DBNull.Value )
						dGSTPCT = Convert.ToDouble( oPCT );
				}

                dGST = Math.Round(Purchase * dGSTPCT, 2, MidpointRounding.AwayFromZero);

                double dInvAmount = Math.Round(dGST + Purchase + PST, 2, MidpointRounding.AwayFromZero);
                dr["INV_AMOUNT"] = dInvAmount;
                dr["GST_AMT"] = Math.Round(dGST, 2, MidpointRounding.AwayFromZero);

				object oDiscPCT = 0;
				if( txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value )
					oDiscPCT = txtDiscP.EditValue;
                dr["DISCOUNT_AMOUNT"] = Math.Round(Purchase * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);

				object objHP = dr["HOLD_PCT"];
				if( objHP != null && objHP != DBNull.Value )
				{
                    string sSelect = "select isnull(CALC_GST_ON_HOLDBACK,'F') from ap_setup";
                    object oCalcGSTHold = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                    if (oCalcGSTHold == null || oCalcGSTHold == DBNull.Value)
                        oCalcGSTHold = "F";

                    if (oCalcGSTHold.Equals("F"))
                    {
                        dr["HOLD_AMT"] = Math.Round(Convert.ToDouble(objHP) * Purchase * .01, 2, MidpointRounding.AwayFromZero);
                        dr["HOLD_BAL"] = dr["HOLD_AMT"];
                    }
                    else
                    {
                        dr["HOLD_AMT"] = Math.Round(Convert.ToDouble(objHP) * dInvAmount * .01, 2, MidpointRounding.AwayFromZero);
                        dr["HOLD_BAL"] = dr["HOLD_AMT"];
                    }
				}
				else
				{
					dr["HOLD_AMT"] = 0;
                    dr["HOLD_BAL"] = 0;
				}				
			}
		}

		private void repositoryItemTextEdit4_EditValueChanged(object sender, System.EventArgs e)
		{
			TextEdit te = sender as TextEdit;
			if( te != null )
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oPO_ID = dr["PO_ID"];
                    object oIS_CB = dr["IS_CB"];
                    if (oIS_CB == null || oIS_CB == DBNull.Value)
                        oIS_CB = "F";

					object oInvAmt = dr["INV_AMOUNT"];
                    if (oInvAmt != null && oInvAmt != DBNull.Value)
                    {
                        double dInvAmt = Convert.ToDouble(oInvAmt);
                        object oAmt = te.EditValue;
                        if (oAmt != null && oAmt != DBNull.Value)
                        {
                            double dAmt = Convert.ToDouble(oInvAmt);
                            double dPA = 0;
                            double dGST = Convert.ToDouble(oAmt);

                            if (oPO_ID != null && oPO_ID != DBNull.Value || oIS_CB.Equals("T"))
                            {
                                object oPA = dr["PURCH_AMT"];
                                if (oPA != null && oPA != DBNull.Value)
                                {
                                    dPA = Convert.ToDouble(oPA);
                                }

                                dAmt = dPA + dGST;
                                dr["INV_AMOUNT"] = Math.Round(dAmt, 2);

                                object oDiscPCT = 0;
                                if (txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value)
                                    oDiscPCT = txtDiscP.EditValue;
                                dr["DISCOUNT_AMOUNT"] = Math.Round(dPA * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                dPA = dAmt - dGST;
                                dr["PURCH_AMT"] = Math.Round(dPA, 2, MidpointRounding.AwayFromZero);
                                object oDiscPCT = 0;
                                if (txtDiscP.EditValue != null && txtDiscP.EditValue != DBNull.Value)
                                    oDiscPCT = txtDiscP.EditValue;
                                dr["DISCOUNT_AMOUNT"] = Math.Round(dPA * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
				}
			}
		}

		private void repositoryItemLookUpEdit11_EditValueChanged(object sender, System.EventArgs e)
		{
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oPO_ID = dr["PO_ID"];
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
                    if (tcDetails.SelectedTabPage == tpMatchPO)
                        ucMPOR.PO_ID = Convert.ToInt32(oPO_ID);
                    else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
                    {
                        ucUCPO.PO_ID = Convert.ToInt32(oPO_ID);
                        ucSCPO.PO_ID = Convert.ToInt32(oPO_ID);
                    }
				}
				else
				{
					ucMPOR.PO_ID = -1;
					ucUCPO.PO_ID = -1;
                    ucSCPO.PO_ID = -1;
				}
			}
		}

        private void repositoryItemTextEdit5_EditValueChanged(object sender, System.EventArgs e)
        {
            TextEdit te = sender as TextEdit;
            object obj = te.EditValue;
            if (obj != null && obj != DBNull.Value)
            {
                HoldbackPctChg(obj);
            }
        }

        private void HoldbackPctChg(object obj)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                string sSelect = "select isnull(CALC_GST_ON_HOLDBACK,'F') from ap_setup";
                object oGSTonHB = ExecuteScalar(sSelect, TR_Conn);
                if (oGSTonHB.ToString() == "F")
                {
                    object objAmt = dr["PURCH_AMT"];
                    if (objAmt != null && objAmt != DBNull.Value)
                    {
                        double dHoldAmt = Math.Round(Convert.ToDouble(objAmt) * Convert.ToDouble(obj) * .01, 2);
                        dr["HOLD_AMT"] = dHoldAmt;
                        dr["HOLD_BAL"] = dHoldAmt;
                        object oDATE = dr["HOLD_PAY_DATE"];
                        if (oDATE == null || oDATE == DBNull.Value)
                            dr["HOLD_PAY_DATE"] = DateTime.Now.Date;
                    }
                    else
                    {
                        dr["HOLD_AMT"] = 0;
                        dr["HOLD_BAL"] = 0;
                        dr["HOLD_PAY_DATE"] = DBNull.Value;
                    }
                }
                else if (oGSTonHB.ToString() == "T")
                {
                    object objAmt = dr["INV_AMOUNT"];
                    if (objAmt != null && objAmt != DBNull.Value)
                    {
                        double dHoldAmt = Math.Round(Convert.ToDouble(objAmt) * Convert.ToDouble(obj) * .01, 2);
                        dr["HOLD_AMT"] = dHoldAmt;
                        dr["HOLD_BAL"] = dHoldAmt;
                        object oDATE = dr["HOLD_PAY_DATE"];
                        if (oDATE == null || oDATE == DBNull.Value)
                            dr["HOLD_PAY_DATE"] = DateTime.Now.Date;
                    }
                    else
                    {
                        dr["HOLD_AMT"] = 0;
                        dr["HOLD_BAL"] = 0;
                        dr["HOLD_PAY_DATE"] = DBNull.Value;
                    }
                }
            }
        }

        private void repositoryItemTextEdit6_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit te = sender as TextEdit;
            object oHoldA = te.EditValue;
            if (oHoldA != null && oHoldA != DBNull.Value)
            {
                DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                if (dr != null)
                {
                    dr.BeginEdit();
                    dr["HOLD_PCT"] = 0;
                    dr["HOLD_BAL"] = oHoldA;
                    object oDATE = dr["HOLD_PAY_DATE"];
                    if (oDATE == null || oDATE == DBNull.Value)
                        dr["HOLD_PAY_DATE"] = DateTime.Now.Date;
                }
            }
        }

		private void repositoryItemTextEdit5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			object obj = e.NewValue;
			if( obj != null && obj != DBNull.Value )
			{
				if( Convert.ToDouble( obj ) > 100 )
					e.Cancel = true;
			}
		}

		private void repositoryItemLookUpEdit5_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
            object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
            if (oAP_INV_HEADER_ID == null || oAP_INV_HEADER_ID == DBNull.Value)
                oAP_INV_HEADER_ID = -1;

            string sSQL = @"select count(*) from ap_gl_alloc where isnull(cb_id,-1) <> -1 and ap_inv_header_id=" + oAP_INV_HEADER_ID;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection)) > 0)
            {
                Popup.ShowPopup("Chargebacks exist on the detail and must be removed before changing supplier.");
                e.Cancel = true;
                return;
            }

			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oPO_ID = dr["PO_ID"];
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
					object oHdr_ID = dr["AP_INV_HEADER_ID"];
					if( oHdr_ID != null && oHdr_ID != DBNull.Value )
					{
						string sSelect = "select count(*) from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id="+oHdr_ID+") ";
						object oCNT = ExecuteScalar( sSelect, TR_Conn );
						if( Convert.ToInt32( oCNT ) > 0 )
						{
							if( Popup.ShowPopup( this, "Invoice has PO Matching, do you wish to delete?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
							{
                                dr.BeginEdit();
								dr["PO_ID"] = DBNull.Value;
                                dr["KC_CONTRACTPO_STATUS"] = DBNull.Value;
								string sDelete = @"delete from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id="+oHdr_ID+@") 
									delete from ap_receiver where ap_inv_header_id="+oHdr_ID+@"
                                    delete l
                                    from AP_PWP_LINK l 
                                    join AP_GL_ALLOC a on a.AP_GL_ALLOC_ID=l.AP_GL_ALLOC_ID
                                    where a.AP_INV_HEADER_ID="+oHdr_ID;
								ExecuteNonQuery( sDelete, TR_Conn );
                                RecallRequest();
                                RoutingLock();
                                dr.EndEdit();
                                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);
							}
							else
							{
								e.Cancel = true;
							}
						}
					}
				}
			}
		}

		private void repositoryItemLookUpEdit8_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				gvHeader_ValidateRow( null, new DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs( gvHeader.FocusedRowHandle, gvHeader.GetRow(gvHeader.FocusedRowHandle) ) );
				if( _HeaderValid )
				{
					object oPO_ID = dr["PO_ID"];
					if( oPO_ID != null && oPO_ID != DBNull.Value )
					{
						object oHdr_ID = dr["AP_INV_HEADER_ID"];
						if( oHdr_ID != null && oHdr_ID != DBNull.Value )
						{
							string sDelete = "";
							string sSelect = "select count(*) from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id="+oHdr_ID+") ";
							object oCNT = ExecuteScalar( sSelect, TR_Conn );
							if( Convert.ToInt32( oCNT ) > 0 )
							{
								if( Popup.ShowPopup( this, "Invoice has PO Matching, do you wish to delete?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
								{
									sDelete = @"delete from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id="+oHdr_ID+@") 
										delete from ap_receiver where ap_inv_header_id="+oHdr_ID+@"
                                        
                                        delete l
                                        from AP_PWP_LINK l 
                                        join AP_GL_ALLOC a on a.AP_GL_ALLOC_ID=l.AP_GL_ALLOC_ID
                                        where a.AP_INV_HEADER_ID="+oHdr_ID+@"
                                    
                                        delete from AP_GL_ALLOC where AP_INV_HEADER_ID=" + oHdr_ID;
									ExecuteNonQuery( sDelete, TR_Conn );
									
                                    dr.BeginEdit();
									dr["INV_AMOUNT"] = 0;
									dr["DISCOUNT_AMOUNT"] = 0;
									dr["PURCH_AMT"] = 0;
									dr["GST_AMT"] = 0;
									if( e.NewValue == null || e.NewValue == DBNull.Value )
										dr["PO_ID"] = DBNull.Value;
                                    dr["KC_CONTRACTPO_STATUS"] = DBNull.Value;
									dsInvDetail1.Clear();
									daInvDetail.Fill( dsInvDetail1 );
                                    RecallRequest();
                                    RoutingLock();
                                    dr.EndEdit();
                                    ggHeader.SaveRecord(gvHeader.FocusedRowHandle);
								}
								else
								{
									e.Cancel = true;
									return;
								}
							}		
						}
					}
				}
				else
				{
					e.Cancel = true;
					gvHeader.SetColumnError( colINV_NO, "Invoice number required." );
				}
			}
		}


        private void LoadTermsByPO(object PO_ID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                string sSelect = "select terms_id from po_header where po_id = " + PO_ID;
                object obj = ExecuteScalar(sSelect, TR_Conn);
                if (obj != null && obj != DBNull.Value)
                    dr["TERMS_ID"] = obj;
                else
                    LoadTermsBySupplier(dr["SUPPLIER"]);
                dr.EndEdit();
            }
        }

        private void LoadTermsBySupplier(object SUPPLIER)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                if (SUPPLIER != null && SUPPLIER != DBNull.Value)
                {
                    dr.BeginEdit();
                    string sSelect = "select terms_id from supplier_master where supplier = '" + SUPPLIER + "'";
                    dr["TERMS_ID"] = ExecuteScalar(sSelect, TR_Conn);
                    dr.EndEdit();
                }
            }
        }

        private void repositoryItemLookUpEdit8_EditValueChanged(object sender, System.EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oPO_ID = dr["PO_ID"];
                if (oPO_ID != null && oPO_ID != DBNull.Value)
                {
                    LoadTermsByPO(oPO_ID);
                }
                else
                {
                    LoadTermsBySupplier(dr["SUPPLIER"]);
                }
            }
        }

		private bool PO_DateValid( object PO_ID )
		{
			bool Valid = true;

			if( PO_ID != null && PO_ID != DBNull.Value )
			{
				string sSelect = "select accounting_year, accounting_period from po_rec_header where po_id="+PO_ID;
				DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter( sSelect, Connection.TRConnection );
				if( dt != null )
				{
					DataRow dr = dt.Rows[0];
					if( dr != null )
					{
						int acct_period = Convert.ToInt32( dr["accounting_period"] );
						int acct_year = Convert.ToInt32( dr["accounting_year"] );
						if( ucAccountingPicker1.SelectedPeriod < acct_period && ucAccountingPicker1.SelectedYear == acct_year || ucAccountingPicker1.SelectedYear < acct_year )
						{
							Valid = false;
						}
					}
				}
			}
			return Valid;
		}

		private void repositoryItemLookUpEdit6_EditValueChanged(object sender, System.EventArgs e)
		{
            LookUpEdit lue = sender as LookUpEdit;

            object oType = lue.EditValue;
            if (oType == null || oType != DBNull.Value)
                oType = "I";

            //lock
            if (oType.Equals("A"))
            {
                gvHeader.SetFocusedRowCellValue(colINV_NO, DBNull.Value);
                gvHeader.SetFocusedRowCellValue(colPAYMENT_HOLD, DBNull.Value);
                chkPaymentHold.Enabled = false;
                colPAYMENT_HOLD.OptionsColumn.AllowEdit = false;
            }
            else
            {
                //unlock 
                chkPaymentHold.Enabled = true;
                colPAYMENT_HOLD.OptionsColumn.AllowEdit = true;
            }

            if (oType.ToString() != "M")
            {
                gvHeader.SetFocusedRowCellValue("MANUAL_CHECK", DBNull.Value);
            }
		}

        private void lueInvType_EditValueChanged(object sender, System.EventArgs e)
        {
            //DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            //if (dr != null)
            //{
            //    object oType = lueInvType.EditValue;
            //    if (oType == null || oType != DBNull.Value)
            //        oType = "I";

            //    if (oType.ToString() != "M")
            //    {
            //        gvHeader.SetFocusedRowCellValue("MANUAL_CHECK", DBNull.Value);
            //    }
            //}
        }

		private void repositoryItemLookUpEdit8_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if( e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete )
			{			
				LookUpEdit lue = sender as LookUpEdit;
				if( lue != null )
					lue.EditValue = null;
			}
		}

		private void repositoryItemTextEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			if( !AutoUpdate )
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oPO_ID = dr["PO_ID"];
					if( oPO_ID != null && oPO_ID != DBNull.Value )
					{
						object oHdr_ID = dr["AP_INV_HEADER_ID"];
						if( oHdr_ID != null && oHdr_ID != DBNull.Value )
						{
							if( Popup.ShowPopup( this, "Invoice has PO Matching, do you wish to delete?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
							{
								string sDelete = @"delete from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id="+oHdr_ID+@") 
									delete from ap_receiver where ap_inv_header_id="+oHdr_ID+@" 
                                    
                                    delete l
                                    from AP_PWP_LINK l 
                                    join AP_GL_ALLOC a on a.AP_GL_ALLOC_ID=l.AP_GL_ALLOC_ID
                                    where a.AP_INV_HEADER_ID="+oHdr_ID+@"
									
                                    delete from AP_GL_ALLOC where AP_INV_HEADER_ID="+oHdr_ID;
								ExecuteNonQuery( sDelete, TR_Conn );

                                dr.BeginEdit();
								dr["INV_AMOUNT"] = 0;
								dr["DISCOUNT_AMOUNT"] = 0;
								dr["PURCH_AMT"] = 0;
								dr["GST_AMT"] = 0;
								dr["PO_ID"] = DBNull.Value;
                                dr["KC_CONTRACTPO_STATUS"] = DBNull.Value;
								dsInvDetail1.Clear();
								daInvDetail.Fill( dsInvDetail1 );
                                RecallRequest();
                                RoutingLock();
                                dr.EndEdit();
                                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);
							}
							else
							{
								e.Cancel = true;
							}
						}
					}
				}
			}
		}

		#endregion

		#region SQL Methods

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

		#endregion

		private bool Loaded = false;

		private void ucAP_InvoiceEntry_Load(object sender, System.EventArgs e)
		{
			CL_Dialog.PleaseWait.Show( "Initializing Invoice Entry", null );
			DevXMgr.FormInit( this );
            SetupMultiCo();
			Popup = new frmPopup( DevXMgr );			
			dpActions.Height = 300;	
			lcDefaults.UseLocalBindingContext = true;			
			FillDataSets( false );			
			CL_Dialog.PleaseWait.Hide();
			InitializeCustomerUserControls();
            SetupKCAccess();
			Loaded = true;
			gvHeader_FocusedRowChanged(null,null);
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
        }

		private void InitializeCustomerUserControls()
		{
			ucMPOR = new ucMatchPOReceipt( Connection, DevXMgr );
			ucMPOR.Parent = tpMatchPO;
			ucMPOR.Dock = DockStyle.Fill;
			ucMPOR.AmountUpdated += new APPOSelect.ucMatchPOReceipt.Delegate_AmountUpdated(ucMPOR_AmountUpdated);

			ucUCPO = new ucUnreleasedContractPO( Connection, DevXMgr );
			ucUCPO.Parent = tpContractPO;
			ucUCPO.Dock = DockStyle.Fill;
			ucUCPO.AmountUpdated += new APPOSelect.ucUnreleasedContractPO.Delegate_AmountUpdated(ucUCPO_AmountUpdated);

            ucSCPO = new ucSummaryContractPO(Connection, DevXMgr);
            ucSCPO.Parent = tpSummContractPO;
            ucSCPO.Dock = DockStyle.Fill;
            ucSCPO.AmountUpdated += new APPOSelect.ucSummaryContractPO.Delegate_AmountUpdated(ucMPOR_AmountUpdated);

			ucAPGSTE = new ucAPGSTException( Connection, DevXMgr );
			ucAPGSTE.Parent = dockPanel5;
			ucAPGSTE.Dock = DockStyle.Fill;
			ucAPGSTE.DataBindings.Add( "Exception", dsInvHeader1, "AP_INV_HEADER.GST_EXCEPT" );
			ucAPGSTE.DataBindings.Add( "OtherException", dsInvHeader1, "AP_INV_HEADER.GST_EXCEPT_ID" );
			ucAPGSTE.Readonly = true;
		}

		public void RefreshMe()
		{
			CL_Dialog.PleaseWait.Show( "Refreshing Invoice Entry", null );            
            FillDataSets( true );            
			CL_Dialog.PleaseWait.Hide();			
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

				dsInvHeader1.Clear();
				dsInvDetail1.Clear();
				dsAPSetupGL1.Clear();
				dsCurrency1.Clear();
				dsGST1.Clear();
				dsPST1.Clear();
				dsTerms1.Clear();
				dsSupplier1.Clear();
				dsPO1.Clear();
				dsAllocSeg1.Clear();
				dsGLAccts1.Clear();
				dsAllPO1.Clear();
				dsPOSelect1.Clear();
                dsPWP_Status1.Clear();

				dsPOFSelect1.Clear();
				dsPOBSelect1.Clear();
				dsPODSelect1.Clear();
				dsPOMSelect1.Clear();
				dsPOM2Select1.Clear();

				Supp_Repository.RefreshMe();
				SetupHdrSwapSeg();
			}

			daInvHeader.Fill( dsInvHeader1 );
            SizeHeaderColumns();
			daAPSetupGL.Fill( dsAPSetupGL1 );			
			daCurrency.Fill( dsCurrency1 );
			daGST.Fill( dsGST1 );
			daPST.Fill( dsPST1 );
			daTerms.Fill( dsTerms1 );
			daSupplier.Fill( dsSupplier1 );
			daGLAccts.Fill( dsGLAccts1 );
			daAllPO.Fill( dsAllPO1 );
			SetupSeg();
			SetupUserWHSE();
			daPOSelect.Fill( dsPOSelect1 );
            daPWP_Status.Fill(dsPWP_Status1);

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
					gvHeader_FocusedRowChanged( null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs( 0, 0 ) );
				}
			}	
		}

		private void LoadHeaderSide()
		{
			CodeChanging = true;
			dsHeaderSide1.Clear();
			dsInvDetail1.Clear();
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oID = dr["AP_INV_HEADER_ID"];
				if( oID != null && oID != DBNull.Value )
				{
					daHeaderSide.SelectCommand.Parameters[ "@ap_inv_header_id" ].Value = oID;
					daHeaderSide.Fill( dsHeaderSide1 );
					daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oID;
					daInvDetail.Fill( dsInvDetail1 );
					object oYear = dr["ACCT_YEAR"];
					object oPeriod = dr["ACCT_PERIOD"];
					if( oYear != DBNull.Value && oYear != null )
						ucAccountingPicker1.SelectedYear = Convert.ToInt32( oYear );
					if( oPeriod != DBNull.Value && oPeriod != null )
						ucAccountingPicker1.SelectedPeriod = Convert.ToInt32( oPeriod );
				}
			}
			CodeChanging = false;
		}

		private void UpdateIsBalance( DataRow dr )
		{
			if( dr != null )
			{
				object oBAL = dr["IS_BALANCED"];
                double dAmt = Math.Round(Convert.ToDouble(txtUndist.EditValue), 2, MidpointRounding.AwayFromZero);
                double dHB = Math.Round(Convert.ToDouble(txtRemain.EditValue), 2, MidpointRounding.AwayFromZero);
				string sBal = "F";
				if( oBAL != null && oBAL != DBNull.Value )
				{
					sBal = oBAL.ToString();
				}

				if( dAmt == 0 && dHB == 0)
				{
					object oID = dr["AP_INV_HEADER_ID"];
					if( oID != null && oID != DBNull.Value )
					{
						dr.BeginEdit();
						dr["IS_BALANCED"] = "T";
                        dr.EndEdit();
                        if (dr.RowState != DataRowState.Added)
                            ggHeader.SaveRecord( gvHeader.FocusedRowHandle );
					}
					else
					{
						gvHeader.SetFocusedRowCellValue( colIS_BALANCED, "T" );									
					}
				}	
				else
				{
					object oID = dr["AP_INV_HEADER_ID"];
					if( oID != null && oID != DBNull.Value )
					{
						dr.BeginEdit();
						dr["IS_BALANCED"] = "F";
                        dr.EndEdit();
                        if (dr.RowState != DataRowState.Added)
                            ggHeader.SaveRecord( gvHeader.FocusedRowHandle );					
					}
					else
					{
						gvHeader.SetFocusedRowCellValue( colIS_BALANCED, "F" );
					}
				}	
			}
		}

		private void CalculateTerms( string Supplier, object Inv_Date )
		{
			string sSelect = "";
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null ) 
			{
				dr["DUE_DATE"] = DBNull.Value;

                double dPURCH_AMT = 0;
                object oPURCH_AMT = dr["PURCH_AMT"];
                if (oPURCH_AMT != null && oPURCH_AMT != DBNull.Value)
				{
                    dPURCH_AMT = Convert.ToDouble(oPURCH_AMT);
				}

				object oSupp = null;
				if( Supplier != "" )
					oSupp = Supplier;
				else
					oSupp = dr["SUPPLIER"];				
				if( oSupp != null && oSupp != DBNull.Value )
				{
					sSelect = "select terms_id from supplier_master where supplier='"+oSupp+"'";
					object oTerms = ExecuteScalar( sSelect, TR_Conn );
					if( oTerms != null )
					{
						sSelect = "select isnull(due_day,0), method, isnull(no_month,0), isnull(cash_discount,0), isnull(cash_disc_days,0) from terms where terms_id="+oTerms;
						object[] oResult = ExecuteDataRow( sSelect, TR_Conn );
						if( oResult != null )
						{
							object oDay = oResult[0];
							object oMethod = oResult[1];
							object oMonth = oResult[2];
							object oDate = dr["INV_DATE"];
							object oDiscountDay = oResult[3];
							object oDiscPCT = oResult[4];

							if( Inv_Date != null )
								oDate = Inv_Date;
							DateTime dtDate = DateTime.Now.Date;

							txtDiscP.EditValue = oDiscPCT;
                            dr["DISCOUNT_AMOUNT"] = Math.Round(dPURCH_AMT * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);

							if( oDate != null && oDate != DBNull.Value )
							{
								dtDate = Convert.ToDateTime( oDate );
							}

                            dr["DUE_DATE"] = Connection.SQLExecutor.ExecuteScalar("select dbo.GetDueDate( '"+dtDate.ToShortDateString()+"', "+oTerms+" )", Connection.TRConnection);

							if( oMethod != null )
							{
								if( oMethod.ToString().ToUpper() == "D" )
								{
									dr["DISCOUNT_DATE"] = dtDate.AddDays( Convert.ToDouble( oDiscountDay ) );
								}
								else if( oMethod.ToString().ToUpper() == "F" )
								{
                                    dtDate = dtDate.AddMonths(Convert.ToInt32(oMonth));
                                    dtDate = dtDate.AddDays(dtDate.Day * -1);
                                    dtDate = dtDate.AddDays(Convert.ToDouble(oDay));

                                    dtDate = dtDate.AddDays(Convert.ToDouble(oDay) * -1);
                                    dtDate = dtDate.AddDays(Convert.ToDouble(oDiscountDay));
                                    dr["DISCOUNT_DATE"] = dtDate;
								}
							}
						}
					}
				}
			}
		}

        private void LoadTerms(int Terms_ID)
        {
            DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
            if (dr != null) 
            {
                dr.BeginEdit();
                dr["DUE_DATE"] = DBNull.Value;

                double dPURCH_AMT = 0;
                object oPURCH_AMT = dr["PURCH_AMT"];
                if (oPURCH_AMT != null && oPURCH_AMT != DBNull.Value)
				{
                    dPURCH_AMT = Convert.ToDouble(oPURCH_AMT);
				}
                
                string sSelect = "select isnull(due_day,0), method, isnull(no_month,0), isnull(cash_discount,0), isnull(cash_disc_days,0) from terms where terms_id=" + Terms_ID;
                object[] oResult = ExecuteDataRow(sSelect, TR_Conn);
                if (oResult != null)
                {
                    object oDay = oResult[0];
                    object oMethod = oResult[1];
                    object oMonth = oResult[2];
                    object oDate = dr["INV_DATE"];
                    object oDiscountDay = oResult[3];
                    object oDiscPCT = oResult[4];

                    DateTime dtDate = DateTime.Now.Date;

                    txtDiscP.EditValue = oDiscPCT;
                    dr["DISCOUNT_AMOUNT"] = Math.Round(dPURCH_AMT * Convert.ToDouble(oDiscPCT) * .01, 2, MidpointRounding.AwayFromZero);

                    if (oDate != null && oDate != DBNull.Value)
                    {
                        dtDate = Convert.ToDateTime(oDate);
                    }

                    if (oMethod != null)
                    {
                        if (oMethod.ToString().ToUpper() == "D")
                        {
                            dr["DUE_DATE"] = dtDate.AddDays(Convert.ToDouble(oDay));
                            dr["DISCOUNT_DATE"] = dtDate.AddDays(Convert.ToDouble(oDiscountDay));
                        }
                        else if (oMethod.ToString().ToUpper() == "F")
                        {
                            dtDate = dtDate.AddMonths(Convert.ToInt32(oMonth));
                            dtDate = dtDate.AddDays(dtDate.Day * -1);
                            dtDate = dtDate.AddDays(Convert.ToDouble(oDay));
                            dr["DUE_DATE"] = dtDate;
                            
                            dtDate = dtDate.AddDays(Convert.ToDouble(oDay) * -1);
                            dtDate = dtDate.AddDays(Convert.ToDouble(oDiscountDay));
                            dr["DISCOUNT_DATE"] = dtDate;
                        }
                    }
                }
                dr.EndEdit();
            }
        }

		private void LoadDefaultSupplierInfo( string Supplier )
		{
			string sSelect = "select AP_SETUP_GL_ID, CURRENCY_ID, GST_CODE, SALES_TAX_ID, NAME, isnull(hold_pct,0) from supplier_master where supplier = '"+Supplier+"'";
			SqlCommand cmd = new SqlCommand( sSelect, new SqlConnection( Connection.TRConnection ) );
			try
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					cmd.Connection.Open();
					using( SqlDataReader reader = cmd.ExecuteReader() )
					{
						if( reader.Read() )
						{
							dr["AP_SETUP_GL_ID"] = reader.GetValue(0);
							dr["CURRENCY_ID"] = reader.GetValue(1);
							dr["GST_CODE"] = reader.GetValue(2);
							dr["SALES_TAX_ID"] = reader.GetValue(3);
							dr["SUPP_NAME"] = reader.GetValue(4);
                            dr["HOLD_PCT"] = reader.GetValue(5);
                            HoldbackPctChg(dr["HOLD_PCT"]);
						}
						reader.Close();
					}
					object oCurr = dr["CURRENCY_ID"];
					if( oCurr != null && oCurr != DBNull.Value )
					{
						sSelect = "select exchange_rate from currency where currency_id="+oCurr;
						object oExch = ExecuteScalar( sSelect, TR_Conn );
						if( oExch != null && oExch != DBNull.Value )
						{
							dr["EXCH_RATE"] = oExch;
						}
					}

					if( dr["AP_SETUP_GL_ID"] == null || dr["AP_SETUP_GL_ID"] == DBNull.Value )
					{
						sSelect = "select ap_setup_gl_id from ap_setup_gl where default_ap_gl = 'T'";
						object oResult = ExecuteScalar( sSelect, TR_Conn );
						if( oResult == null  )
							oResult = DBNull.Value;
						dr["AP_SETUP_GL_ID"] = oResult;
					}
				}
			}
			catch( Exception ex )
			{
				Popup.ShowPopup( this, ex.Message+ ex.StackTrace );
			}
			finally
			{
				cmd.Connection.Close();
			}
		}

		private void SaveHeader()
		{							
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
				object oinvno = dr["INV_NO"];

				ggHeader.SaveRecord( gvHeader.FocusedRowHandle );

                UpdateDetailSupplier(oAP_INV_HEADER_ID);
                UpdateDetailDocNo(oAP_INV_HEADER_ID);
		
				CreateGSTRecord( oAP_INV_HEADER_ID );
				dsInvHeader1.Clear();
				daInvHeader.Fill( dsInvHeader1 );
                SizeHeaderColumns();
				SetFocusRow( oAP_INV_HEADER_ID );	

				daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = oAP_INV_HEADER_ID;
				dsInvDetail1.Clear();
				daInvDetail.Fill( dsInvDetail1 );
			}
		}

        private void SizeHeaderColumns()
        {
            // gvHeader.BestFitColumns(); // Cannot save the layout with 'Sized' Columns Dec 2016
            colLevy.Width = 82;
        }

		private void UpdateDetailSupplier( object AP_INV_HEADER_ID )
		{
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oSUPPLIER = dr["SUPPLIER"];
				if( oSUPPLIER != null && oSUPPLIER != DBNull.Value )
				{
					string sUpdate = "update ap_gl_alloc set supplier='"+oSUPPLIER+"' where ap_inv_header_id="+AP_INV_HEADER_ID;
					Connection.SQLExecutor.ExecuteNonQuery( sUpdate, Connection.TRConnection );
				}
			}
		}

		private void UpdateDetailDocNo( object AP_INV_HEADER_ID )
		{
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oINV_NO = dr["INV_NO"];
				if( oINV_NO != null && oINV_NO != DBNull.Value )
				{
                    string sUpdate = @"update ap_gl_alloc set doc_no='" + oINV_NO + @"' where ap_inv_header_id=" + AP_INV_HEADER_ID + @"
                        update ap_receiver set doc_no='" + oINV_NO + @"' where ap_inv_header_id=" + AP_INV_HEADER_ID;
                    Connection.SQLExecutor.ExecuteNonQuery( sUpdate, Connection.TRConnection );
				}
			}
		}

		private void CreateGSTRecord( object AP_INV_HEADER_ID )
		{
			object oMessage = ExecuteScalar( "sp_APCreateGSTRecord "+AP_INV_HEADER_ID, TR_Conn );
			if( oMessage != null && oMessage != DBNull.Value )
			{
				if( oMessage.ToString() != "OK" )
					Popup.ShowPopup( this, oMessage.ToString() );
			}
		}

		private void SetFocusRow( object AP_INV_HEADER_ID )
		{
			if( AP_INV_HEADER_ID != null && AP_INV_HEADER_ID != DBNull.Value )
			{
				object oAP_INV_HEADER_ID;
				DataRow dr;
				for( int i = gvHeader.RowCount -1; i >= 0; i-- )
				{
					dr = gvHeader.GetDataRow( i );
					if( dr != null )
					{
						oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
						if( oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value )
						{
							if( AP_INV_HEADER_ID.Equals( oAP_INV_HEADER_ID ) )
							{
                                gvHeader.FocusedRowHandle = i;
								break;									
							}
						}
					}
				}
			}
		}

		private void CalculateRemaining()
		{
			double InvoiceAmt = 0;
			double DetAmt = 0;
			double UndistAmt = 0;
			double Holdback = 0;
			double RemainAmt = 0;
			double HoldbackAmt = 0;

			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oID = dr["AP_INV_HEADER_ID"];
				if( oID != null && oID != DBNull.Value )
				{
					string sSelect = "select sum(isnull(AMOUNT,0)) from ap_gl_alloc where AP_INV_HEADER_ID="+oID;
					object oAmt = ExecuteScalar( sSelect, TR_Conn );
					if( oAmt != null && oAmt != DBNull.Value )
					{
						DetAmt = Convert.ToDouble( oAmt );
					}
					sSelect = "select sum(isnull(HOLD_AMT,0)) from ap_gl_alloc where AP_INV_HEADER_ID="+oID;
					oAmt = ExecuteScalar( sSelect, TR_Conn );
					if( oAmt != null && oAmt != DBNull.Value )
					{
						Holdback = Convert.ToDouble( oAmt );
					}
				}

				object oHOLD_AMT = dr["HOLD_AMT"];
				if( oHOLD_AMT != null && oHOLD_AMT != DBNull.Value )
				{
					HoldbackAmt = Convert.ToDouble( oHOLD_AMT );
				}

                object oInvAmt = dr["INV_AMOUNT"];
                if( oInvAmt != null && oInvAmt != DBNull.Value )
                    InvoiceAmt = Convert.ToDouble(oInvAmt);
			}

			txtHoldback.EditValue = Holdback;

			UndistAmt = InvoiceAmt - DetAmt;
			txtUndist.EditValue = UndistAmt;

			txtRemain.EditValue = HoldbackAmt - Holdback;
			
			if( dr != null )
			{
				if( dr != null && RemainAmt == 0.0 )
				{
					dr["ACCRUAL_FLAG"] = "U";
				}
				else if( dr != null && RemainAmt != 0.0 )
				{
					dr["ACCRUAL_FLAG"] = "X";
				}
				UpdateIsBalance( dr );
			}
			
		}

		private bool GSTChanged()
		{
			bool Changed = false;
			string sSelect = "select isnull(disable_gst_except,'F') from ap_setup";
			object oDisable = ExecuteScalar( sSelect, TR_Conn );
			if( oDisable != null && oDisable != DBNull.Value )
			{
				if( oDisable.ToString().ToUpper() == "F" )
				{
					DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
					if( dr != null )
					{
						object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
						if( oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value )
						{
							sSelect = "select isnull(GST_AMT,0) from ap_inv_header where ap_inv_header_id="+oAP_INV_HEADER_ID;
							object oGST_AMT = ExecuteScalar( sSelect, TR_Conn );
							if( oGST_AMT == null || oGST_AMT == DBNull.Value )
								oGST_AMT = 0;
							object oCurrGST_AMT = dr["GST_AMT"];
							if( oCurrGST_AMT == null || oCurrGST_AMT == DBNull.Value )
								oCurrGST_AMT = 0;

							if( Convert.ToDouble( oGST_AMT ) == Convert.ToDouble( dr["GST_AMT"] ) )
								return false;
						}
						object oAmt = dr["INV_AMOUNT"];
						if( oAmt != null && oAmt != DBNull.Value )
						{
							double dAmt = Convert.ToDouble( oAmt );
							double dGST = 0;
							double dSavedGST = 0;

							object oPCT;
							double dGSTPCT = 0;
							double dPSTPCT = 0;
							object oGST = dr["GST_CODE"];
							if( oGST != null && oGST != DBNull.Value )
							{
								sSelect = "select ( gst_pct * .01 ) from GST_CODES where gst_code='"+oGST+"'";
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dGSTPCT = Convert.ToDouble( oPCT );
							}
							object oPST = dr["SALES_TAX_ID"];
							if( oPST != null && oPST != DBNull.Value )
							{
								sSelect = "select ( sales_tax * .01 ) from sales_taxes where sales_tax_id="+oPST;
								oPCT = ExecuteScalar( sSelect, TR_Conn );
								if( oPCT != null && oPCT != DBNull.Value )
									dPSTPCT = Convert.ToDouble( oPCT );
							}
							double dPAminusPST = dAmt / (1+dPSTPCT+dGSTPCT);
							
							dGST = dPAminusPST * dGSTPCT;					
					
							if( dr["GST_AMT"] != null && dr["GST_AMT"] != DBNull.Value )
							{
								dSavedGST = Convert.ToDouble( dr["GST_AMT"] );
							}

                            if (Math.Round(dSavedGST, 2, MidpointRounding.AwayFromZero) != Math.Round(dGST, 2, MidpointRounding.AwayFromZero))
								Changed = true;


						}
					}
				}
			}
			return Changed;
		}

		private void POLevyLockDown()
		{
            _PO_LOCKDOWN = false;
			ggDetail.Event_ProcessGridKey_Enabled = true;
			ggDetail.Event_BeforeLeaveRow_Enabled = true;

			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
                bool Lock = false;

                object oLevy = dr["Levy"];
                if( oLevy == null || oLevy == DBNull.Value ) 
                    oLevy = false;

                if( Convert.ToBoolean(oLevy))
				{
                    Lock = true;
                }

				object oPO_ID = dr["PO_ID"];
				if( oPO_ID != null && oPO_ID != DBNull.Value ) 
				{
                    Lock = true;
                }

                if( Lock ) //LOCK
                {
                    _PO_LOCKDOWN = true;
					for( int i = 0; i < gvDetail.Columns.Count; i++ )
					{
                        if (gvDetail.Columns[i] != colCOMMENT1 && gvDetail.Columns[i] != colCB_REF)
						    gvDetail.Columns[i].OptionsColumn.AllowEdit = false;
					}
					hlBalance.Enabled = false;
                    gcDetail.EmbeddedNavigator.Buttons.Append.Enabled = false;
                    gcDetail.EmbeddedNavigator.Buttons.Remove.Enabled = false;
					ggDetail.Event_ProcessGridKey_Enabled = false;
					ggDetail.Event_BeforeLeaveRow_Enabled = false;

					object oSOX = dr["SOX_ROUTING"];
					if( oSOX != null && oSOX != DBNull.Value )
					{
						if( Convert.ToBoolean( oSOX ) == true )
						{
							chkKCRouting.Enabled = false;
							repositoryItemCheckEdit2.ReadOnly = true;
						}
						else
						{
							chkKCRouting.Enabled = true;
							repositoryItemCheckEdit2.ReadOnly = false;
						}
					}
					else
					{
						chkKCRouting.Enabled = true;
						repositoryItemCheckEdit2.ReadOnly = false;
					}
				}
				else //UNLOCK
				{
                    _PO_LOCKDOWN = false;
					for( int i = 0; i < gvDetail.Columns.Count; i++ )
					{
                        if (gvDetail.Columns[i] != colSUB_CODE && gvDetail.Columns[i] != colRATE)
							gvDetail.Columns[i].OptionsColumn.AllowEdit = true;
					}
					hlBalance.Enabled = true;
					chkKCRouting.Enabled = true;
					repositoryItemCheckEdit2.ReadOnly = false;
                    gcDetail.EmbeddedNavigator.Buttons.Append.Enabled = true;
                    gcDetail.EmbeddedNavigator.Buttons.Remove.Enabled = true;

                    if (!_HoldbackEdit)
                    {
                        colHOLD_PCT.OptionsColumn.AllowEdit = false;
                        colHOLD_AMT.OptionsColumn.AllowEdit = false;
                        colHOLD_AMT1.OptionsColumn.AllowEdit = false;
                        txtHoldP.Properties.ReadOnly = true;
                        txtHoldA.Properties.ReadOnly = true;
                    }
                    else
                    {
                        if (!_HoldbackEdit)
                        {
                            colHOLD_PCT.OptionsColumn.AllowEdit = true;
                            colHOLD_AMT.OptionsColumn.AllowEdit = true;
                            colHOLD_AMT1.OptionsColumn.AllowEdit = true;
                            txtHoldP.Properties.ReadOnly = false;
                            txtHoldA.Properties.ReadOnly = false;
                        }
                    }
				}
			}
            RoutingLock();
		}

		private void hlQuickChk_Click(object sender, System.EventArgs e)
		{
			if( !KCA_Validator.ValidatePassword( CONST_QUICK_CHECK ) )
				return;
			
			if( txtUndist.EditValue != null )
			{
				if( Convert.ToDouble( txtUndist.EditValue ) != 0 )
				{
					Popup.ShowPopup( "Invoice is not balanced, undistributed amount must equal 0." );
					return;
				}
			}
			if( txtRemain.EditValue != null )
			{
                if (Convert.ToDouble(txtRemain.EditValue) != 0)
				{
					if( Connection.CountryCode != "U" )
						Popup.ShowPopup( "Invoice is not balanced, holdback remaining amount must equal 0." );
					else
						Popup.ShowPopup( "Invoice is not balanced, retainage remaining amount must equal 0." );
					return;
				}
			}

			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oID = dr["AP_INV_HEADER_ID"];
				object oP = dr["ACCT_PERIOD"];
				object oY = dr["ACCT_YEAR"];
				if( oID != null && oID != DBNull.Value )
				{
                    // VALIDATE THAT NO PWP STATUS OF PENDING OR REJECTED EXISTS
                    string sSQL = @"select COUNT(*) from AP_GL_ALLOC where AP_INV_HEADER_ID = "+oID+@" and AR_PWP_STATUS_ID in (3, 4)";
                    object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oCNT == null || oCNT == DBNull.Value)
                        oCNT = 0;

                    if (Convert.ToInt32(oCNT) > 0)
                    {
                        Popup.ShowPopup("Unable to process accrual, detail records exist with a paid when paid status of either pending or rejected.");
                        return;
                    }

                    object oSTATUS1 = dr["KC_CONTRACTPO_STATUS"];
                    if (oSTATUS1 == null || oSTATUS1 == DBNull.Value)
                        oSTATUS1 = "";
                    object oSTATUS2 = dr["KC_ACCRUAL_STATUS"];
                    if (oSTATUS2 == null || oSTATUS2 == DBNull.Value)
                        oSTATUS2 = "";
                    object oWF_STATUS = dr["WF_STATUS"];
                    if (oWF_STATUS == null || oWF_STATUS == DBNull.Value)
                        oWF_STATUS = "";

                    if (oSTATUS1.Equals("Q") || oSTATUS1.Equals("P") || oSTATUS1.Equals("R") || oSTATUS1.Equals("D"))
                    {
                        Popup.ShowPopup("This invoice currently cannot be processed because of its contract PO routing status.");
                        return;
                    }

                    if (oSTATUS2.Equals("Q") || oSTATUS2.Equals("P") || oSTATUS2.Equals("R") || oSTATUS2.Equals("D"))
                    {
                        Popup.ShowPopup("This invoice currently cannot be processed because of its pre-accrual routing status.");
                        return;
                    }

                    if (oWF_STATUS.Equals("Q") || oWF_STATUS.Equals("P") || oWF_STATUS.Equals("R") || oWF_STATUS.Equals("D"))
                    {
                        Popup.ShowPopup("This invoice currently cannot be processed because of its invoice routing status.");
                        return;
                    }

					string sSelect = "select count(*) from working_ap_invoices where ap_inv_header_id="+oID;
					object oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( Convert.ToInt32( oResult ) > 0 )
					{
						Popup.ShowPopup( "This invoice is currently being processed in an accrual run. Unable to process quick check." );
						return;
					}

					bool bAccrued = false;
					sSelect = "select isnull(accrual_flag,'U') from ap_inv_header where ap_inv_header_id="+oID;
					oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( oResult != null && oResult != DBNull.Value )
					{
						if( oResult.Equals( "A" ) )
						{
							Popup.ShowPopup( "This invoice has been accrued. Refreshing invoice entry." );
							RefreshMe();
							bAccrued = true;
						}
					}
		
					if( !bAccrued )
					{
						string sInsert = "insert into working_ap_invoices (username, ap_inv_header_id) values ('"+Connection.MLUser+"', "+oID+")";
						Connection.SQLExecutor.ExecuteScalar( sInsert, Connection.TRConnection );

                        using (frmQuickCheck fQuickCheck = new frmQuickCheck(Connection, DevXMgr, Convert.ToInt32(oID), Convert.ToInt32(oY), Convert.ToInt32(oP)))
                        {
                            if (fQuickCheck.ShowDialog() == DialogResult.OK)
                            {
                                RefreshMe();
                                bAccrued = true;
                                oY = fQuickCheck.Year;
                                oP = fQuickCheck.Period;
                            }
                        }

						string sDelete = "delete from working_ap_invoices where username='"+Connection.MLUser+"'";
						Connection.SQLExecutor.ExecuteScalar( sDelete, Connection.TRConnection );
					}

					if( bAccrued )
					{
                        // VALIDATE THAT NO PWP STATUS OF OPEN, PENDING, REJECTED EXISTS
                        sSQL = @"select COUNT(*) from AP_GL_ALLOC where AP_INV_HEADER_ID = " + oID + @" and AR_PWP_STATUS_ID in (1, 3, 4)";
                        oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                        if (oCNT == null || oCNT == DBNull.Value)
                            oCNT = 0;

                        if (Convert.ToInt32(oCNT) > 0)
                        {
                            Popup.ShowPopup("Unable to process payment, detail records exist with a paid when paid status of either open, pending or rejected.");
                            return;
                        }

                        object oSTATUS3 = Connection.SQLExecutor.ExecuteScalar("select isnull(KC_PAYHOLD_STATUS,'') from ap_inv_header where ap_inv_header_id=" + oID, Connection.TRConnection);
                        if (oSTATUS3 == null || oSTATUS3 == DBNull.Value)
                            oSTATUS3 = "";
                        if (oSTATUS3.Equals("Q") || oSTATUS3.Equals("P") || oSTATUS3.Equals("R") || oSTATUS3.Equals("D"))
                        {
                            Popup.ShowPopup("This invoice has been accrued, but a payment cannot be processed because of a payment hold routing status on the invoice.");
                            return;
                        }

						sSelect = "select isnull(balance,0) from ap_inv_header where ap_inv_header_id="+oID;
						oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
						if( oResult != null && oResult != DBNull.Value )
						{
							if( Convert.ToDouble( oResult ) == 0 )
							{
								Popup.ShowPopup( "A check has been made for this invoice. Refreshing invoice entry." );
								RefreshMe();
								return;
							}
							else if( Convert.ToDouble( oResult ) < 0 )
							{
								Popup.ShowPopup( "Unable to process check, balance must be a positive amount. Refreshing invoice entry." );
								RefreshMe();
								return;
							}
						}

						if( ProcessingLocked() )
							return;
						Connection.SQLExecutor.ExecuteNonQuery( "update ap_setup set CHECK_PROCESS_LOCK = '"+Connection.MLUser+"'", Connection.TRConnection );

                        using (frmQuickCheck2 fQuickCheck2 = new frmQuickCheck2(Connection, DevXMgr, Convert.ToInt32(oID), Convert.ToInt32(oY), Convert.ToInt32(oP)))
                        {
                            fQuickCheck2.ShowDialog();
                        }

						Connection.SQLExecutor.ExecuteNonQuery( "update ap_setup set CHECK_PROCESS_LOCK = null", Connection.TRConnection );
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
		
		private void lueAPCntl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			if( !CodeChanging )
			{
				if( !KCA_Validator.ValidatePassword( CONST_OVERRIDE_AP_CONTROL ) )
				{
					e.Cancel = true;
				}
			}
		}

		private void repositoryItemLookUpEdit6_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			if( e.OldValue.Equals( "A" ) )
			{
				object oRowView = BindingContext[ dsInvHeader1, "AP_INV_HEADER" ].Current;
				if( oRowView != null )
				{
					DataRowView DRV = oRowView as DataRowView;						
					DRV["AP_INV_HEADER_ID"] = DBNull.Value;
				}
			}
		}

		private void gvDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
            dsAP_PWP_GetLinks1.Clear();
            if (!_PO_LOCKDOWN)
            {
                for (int i = 0; i < gvDetail.Columns.Count; i++)
                {
                    if (gvDetail.Columns[i] != colSUB_CODE && gvDetail.Columns[i] != colRATE)

                        gvDetail.Columns[i].OptionsColumn.AllowEdit = true;
                }
            }

			try
			{
                colbillable.OptionsColumn.AllowEdit = false;
				DataRow dr = gvDetail.GetDataRow( gvDetail.FocusedRowHandle );
				if( dr != null )
				{
					object oPO_Type = dr["PO_TYPE"];
					if( oPO_Type != null && oPO_Type != DBNull.Value )
					{
						if( oPO_Type.Equals( "I" )  )
						{
                            for (int i = 0; i < gvDetail.Columns.Count; i++)
                            {
                                if( gvDetail.Columns[i] != colCB_REF )
                                    gvDetail.Columns[i].OptionsColumn.AllowEdit = false;
                            }
						}
					}

					GL_Repository.GL_SUB_CODE = null;
					GL_Repository.PROJ_PRP_COMPONENT = null;
					GL_Repository.PROJ_UNIT_INV_ID = null;
					GL_Repository.PROJ_PRI_NUM = null;
					GL_Repository.PROJ_PHS_CODE = null;
					GL_Repository.PROJ_SUBP_CODE = null;

					GL_Repository.PROJ_TIME_TICKET = null;
					GL_Repository.PROJ_AFE_NO = null;
					GL_Repository.PROJ_COST_CODE = null;

					GL_Repository.LAND_PROJ_CD = null;
					GL_Repository.LAND_RLH_CD = null;
					GL_Repository.LAND_JOB_COST_CD = null;
					GL_Repository.LAND_INV_ID = null;
					GL_Repository.LAND_AGREE_NUM = null;

					GL_Repository.COM_PROP_CD = null;
					GL_Repository.COM_PROF_CNTR = null;
					GL_Repository.COM_LEASE_NUM = null;
					GL_Repository.COM_FLOOR = null;
					GL_Repository.COM_SPACE = null;

					GL_Repository.RES_PROP_CD = null;
					GL_Repository.RES_PROF_CNTR = null;
					GL_Repository.RES_LEASE_NUM = null;
					GL_Repository.RES_FLOOR = null;
					GL_Repository.RES_UNIT = null;

					GL_Repository.REFERENCE = null;

					if( dr["SUB_CODE"] != DBNull.Value )
						GL_Repository.GL_SUB_CODE = dr["SUB_CODE"].ToString();

					if( dr["prp_component"] != DBNull.Value )
						GL_Repository.PROJ_PRP_COMPONENT = dr["prp_component"].ToString();
					if( dr["unit_inv_id"] != DBNull.Value )
						GL_Repository.PROJ_UNIT_INV_ID = dr["unit_inv_id"].ToString();
					if( dr["pri_num"] != DBNull.Value )
						GL_Repository.PROJ_PRI_NUM = dr["pri_num"].ToString();
					if( dr["phs_code"] != DBNull.Value )
						GL_Repository.PROJ_PHS_CODE = dr["phs_code"].ToString();
					if( dr["subp_code"] != DBNull.Value )
						GL_Repository.PROJ_SUBP_CODE = dr["subp_code"].ToString();

					if( dr["TIME_TICKET"] != DBNull.Value )
						GL_Repository.PROJ_TIME_TICKET = dr["TIME_TICKET"].ToString();
					if( dr["AFE_NO"] != DBNull.Value )
						GL_Repository.PROJ_AFE_NO = dr["AFE_NO"].ToString();
					if( dr["COST_CODE"] != DBNull.Value )
						GL_Repository.PROJ_COST_CODE = dr["COST_CODE"].ToString();

					if( dr["AGA_PROJ_CD"] != DBNull.Value )
						GL_Repository.LAND_PROJ_CD = dr["AGA_PROJ_CD"].ToString();
					if( dr["AGA_RLH_CD"] != DBNull.Value )
						GL_Repository.LAND_RLH_CD = dr["AGA_RLH_CD"].ToString();
					if( dr["AGA_JOB_COST_CD"] != DBNull.Value )
						GL_Repository.LAND_JOB_COST_CD = dr["AGA_JOB_COST_CD"].ToString();
					if( dr["AGA_INV_ID"] != DBNull.Value )
						GL_Repository.LAND_INV_ID = dr["AGA_INV_ID"].ToString();
					if( dr["AGA_AGREE_NUM"] != DBNull.Value )
						GL_Repository.LAND_AGREE_NUM = dr["AGA_AGREE_NUM"].ToString();

					if( dr["AGA_C_PROP_CD"] != DBNull.Value )
						GL_Repository.COM_PROP_CD = dr["AGA_C_PROP_CD"].ToString();
					if( dr["AGA_C_PROF_CNTR"] != DBNull.Value )
						GL_Repository.COM_PROF_CNTR = dr["AGA_C_PROF_CNTR"].ToString();
					if( dr["AGA_C_LEASE_NUM"] != DBNull.Value )
						GL_Repository.COM_LEASE_NUM = dr["AGA_C_LEASE_NUM"].ToString();
					if( dr["AGA_C_FLOOR"] != DBNull.Value )
						GL_Repository.COM_FLOOR = dr["AGA_C_FLOOR"].ToString();
					if( dr["AGA_C_SPACE"] != DBNull.Value )
						GL_Repository.COM_SPACE = dr["AGA_C_SPACE"].ToString();

					if( dr["AGA_R_PROP_CD"] != DBNull.Value )
						GL_Repository.RES_PROP_CD = dr["AGA_R_PROP_CD"].ToString();
					if( dr["AGA_R_PROF_CNTR"] != DBNull.Value )
						GL_Repository.RES_PROF_CNTR = dr["AGA_R_PROF_CNTR"].ToString();
					if( dr["AGA_R_LEASE_NUM"] != DBNull.Value )
						GL_Repository.RES_LEASE_NUM = dr["AGA_R_LEASE_NUM"].ToString();
					if( dr["AGA_R_FLOOR"] != DBNull.Value )
						GL_Repository.RES_FLOOR = dr["AGA_R_FLOOR"].ToString();
					if( dr["AGA_R_UNIT"] != DBNull.Value )
						GL_Repository.RES_UNIT = dr["AGA_R_UNIT"].ToString();


                    if (dr["REFERENCE"] != DBNull.Value)
                    {
                        GL_Repository.REFERENCE = dr["REFERENCE"].ToString();
                    }
                   
                    string reference = "";
                    if (dr["REFERENCE"] != DBNull.Value && dr["REFERENCE"] != null)
                    {
                        reference = dr["REFERENCE"].ToString();
                        
                    }
                    if (reference != "")
                        colbillable.OptionsColumn.AllowEdit = true;

                    object oAR_PWP_STATUS_ID = dr["AR_PWP_STATUS_ID"];
                    if (oAR_PWP_STATUS_ID != null && oAR_PWP_STATUS_ID != DBNull.Value)
                    {
                        object oAP_GL_ALLOC_ID = gvDetail.GetFocusedRowCellValue("AP_GL_ALLOC_ID");
                        if (oAP_GL_ALLOC_ID == null || oAP_GL_ALLOC_ID == DBNull.Value)
                            oAP_GL_ALLOC_ID = -1;
                        if (Convert.ToInt32(oAP_GL_ALLOC_ID) != -1)
                        {
                            daAP_PWP_GetLinks.SelectCommand.Parameters["@po_id"].Value = gvHeader.GetFocusedRowCellValue("PO_ID");
                            daAP_PWP_GetLinks.SelectCommand.Parameters["@ap_gl_alloc_id"].Value = oAP_GL_ALLOC_ID;
                            daAP_PWP_GetLinks.Fill(dsAP_PWP_GetLinks1);
                        }
                    }
				}
			}
			catch( DBConcurrencyException )
			{
				Popup.ShowPopup( this, "This record has been modified by another user and will be refreshed." );
				RefreshMe();
			}
		}

		private void ucAccountingPicker1_SelectedPeriodChanged(object sender, System.EventArgs e)
		{
			if( ucMPOR != null )
			{
				ucMPOR.Year = ucAccountingPicker1.SelectedYear;
				ucMPOR.Period = ucAccountingPicker1.SelectedPeriod;
			}
		}

		private void ucAccountingPicker1_SelectedYearChanged(object sender, System.EventArgs e)
		{
			if( ucMPOR != null )
			{
				ucMPOR.Year = ucAccountingPicker1.SelectedYear;
				ucMPOR.Period = ucAccountingPicker1.SelectedPeriod;
			}
		}

		private void riPOSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if( e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis )
			{
                using (POSelect.frmPOSelect fPOSelect = new POSelect.frmPOSelect(Connection, DevXMgr))
                {
                    if (fPOSelect.ShowDialog() == DialogResult.OK)
                    {
                        gvDetail.SetFocusedRowCellValue(colPO_ID1, fPOSelect.SelectedPO);

                        string sSelect = "select po_id from po_rec_header where po_rec_id=" + fPOSelect.SelectedPO;
                        object oPO_ID = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

                        sSelect = "select isnull(stock_po,'F') from po_header where po_id=" + oPO_ID;
                        object oResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                        if (oResult.Equals("F"))
                        {
                            sSelect = "select stock_clear_acct from po_header p join warehouse w on w.whse_id=p.whse_id where p.po_id =" + oPO_ID;
                            oResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                            gvDetail.SetFocusedRowCellValue(colGL_ACCOUNT, oResult);
                        }
                        else
                        {
                            sSelect = "select buyin_clear_acct from po_header p join warehouse w on w.whse_id=p.whse_id where p.po_id =" + oPO_ID;
                            oResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                            gvDetail.SetFocusedRowCellValue(colGL_ACCOUNT, oResult);
                        }
                    }
                }
			}			
		}

		private void repositoryItemLookUpEdit8_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
		{
            int iPO_ID = -1;
            int iAP_INV_HEADER_ID = -1;
			LookUpEdit lue = sender as LookUpEdit;
			if( lue != null )
			{
				object oPO_ID = lue.EditValue;
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
                    iPO_ID = Convert.ToInt32(oPO_ID);
                    iAP_INV_HEADER_ID = Convert.ToInt32(gvHeader.GetDataRow(gvHeader.FocusedRowHandle)["AP_INV_HEADER_ID"]);

                    string sSelect = "select isnull(taxable,'F'), isnull(HOLD_PCT,0), sales_tax_id, wf_approval_id from po_header where po_id=" + oPO_ID;
                    DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            if (dr != null)
                            {
                                object oTaxable = dr[0];
                                if (oTaxable != null && oTaxable != DBNull.Value)
                                {
                                    if (oTaxable.ToString() == "T")
                                    {
                                        object oTax = dr[2];
                                        if (oTax != null && oTax != DBNull.Value)
                                        {
                                            gvHeader.GetDataRow(gvHeader.FocusedRowHandle)["SALES_TAX_ID"] = oTax;
                                        }
                                    }
                                }
                                object oHoldPct = dr[1];
                                if (oHoldPct != null && oHoldPct != DBNull.Value)
                                {
                                    gvHeader.GetDataRow(gvHeader.FocusedRowHandle)["HOLD_PCT"] = oHoldPct;
                                }
                                object oApproval_ID = dr[3];
                                if (oApproval_ID != null && oApproval_ID != DBNull.Value)
                                {
                                    gvHeader.GetDataRow(gvHeader.FocusedRowHandle)["WF_Approval_ID"] = oApproval_ID;
                                }
                            }
                        }
                    }
				}
				else
				{
					ucMPOR.PO_ID = -1;
					ucUCPO.PO_ID = -1;
                    ucSCPO.PO_ID = -1;
				}

			}
            if (iPO_ID != -1)
			{
                SaveHeader();

                using (frmPOSelect POSelect = new frmPOSelect(Connection, DevXMgr, _ShowContractPOSummary))
                {
                    POSelect.MatchPOReciept.AmountUpdated += new APPOSelect.ucMatchPOReceipt.Delegate_AmountUpdated(ucMPOR_AmountUpdated);
                    POSelect.UnreleasedContractPO.AmountUpdated += new APPOSelect.ucUnreleasedContractPO.Delegate_AmountUpdated(ucUCPO_AmountUpdated);
                    POSelect.SummaryContractPO.AmountUpdated += new ucSummaryContractPO.Delegate_AmountUpdated(ucMPOR_AmountUpdated);
                    POSelect.MatchPOReciept.RefreshPO_ID(iPO_ID, iAP_INV_HEADER_ID);
                    POSelect.UnreleasedContractPO.RefreshPO_ID(iPO_ID, iAP_INV_HEADER_ID);
                    POSelect.SummaryContractPO.RefreshPO_ID(iPO_ID, iAP_INV_HEADER_ID);

                    POSelect.MatchPOReciept.AP_INV_HEADER_ID = iAP_INV_HEADER_ID;
                    POSelect.UnreleasedContractPO.AP_INV_HEADER_ID = iAP_INV_HEADER_ID;
                    POSelect.SummaryContractPO.AP_INV_HEADER_ID = iAP_INV_HEADER_ID;
                    POSelect.MatchPOReciept.Year = ucAccountingPicker1.SelectedYear;
                    POSelect.MatchPOReciept.Period = ucAccountingPicker1.SelectedPeriod;

                    if (KCA_Validator.ModulePointRequired(CONST_PO_RESTOCKING_AMT_EDIT))
                    {
                        if (KCA_Validator.ModulePointLocked(CONST_PO_RESTOCKING_AMT_EDIT))
                            POSelect.MatchPOReciept.RestockingAmtKCA(true);
                        else
                            POSelect.MatchPOReciept.RestockingAmtKCA(false);
                    }
                    else
                        POSelect.MatchPOReciept.RestockingAmtKCA(false);

                    if (POSelect.UnreleasedContractPO.HasRows)
                        POSelect.xtraTabControl1.SelectedTabPageIndex = 1;
                    if (_ShowContractPOSummary && POSelect.SummaryContractPO.HasRows)
                        POSelect.xtraTabControl1.SelectedTabPageIndex = 2;
                    POSelect.ShowDialog();
                }

                if (tcDetails.SelectedTabPage == tpMatchPO)
                    ucMPOR.RefreshMe();
                else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
                {
                    ucUCPO.RefreshMe();
                    ucSCPO.RefreshMe();
                }
				gvHeader_FocusedRowChanged(null,null);				
			}
        }

		private void riPOSelect_EditValueChanged(object sender, System.EventArgs e)
		{
			LookUpEdit lue = sender as LookUpEdit;
			if( lue != null )
			{
				object oValue = lue.EditValue;
				if( oValue != null && oValue != DBNull.Value )
				{
					string sSelect = "select po_id from po_rec_header where po_rec_id="+oValue;
					object oPO_ID = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );

					sSelect = "select isnull(stock_po,'F') from po_header where po_id="+oPO_ID;
					object oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
					if( oResult.Equals( "F" ) )
					{
						sSelect = "select stock_clear_acct from po_header p join warehouse w on w.whse_id=p.whse_id where p.po_id ="+oPO_ID;
						oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
						gvDetail.SetFocusedRowCellValue( colGL_ACCOUNT, oResult );
					}
					else
					{
						sSelect = "select buyin_clear_acct from po_header p join warehouse w on w.whse_id=p.whse_id where p.po_id ="+oPO_ID;
						oResult = Connection.SQLExecutor.ExecuteScalar( sSelect, Connection.TRConnection );
						gvDetail.SetFocusedRowCellValue( colGL_ACCOUNT, oResult );
					}
				}
			}
		}

		private void gvDetail_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
		{
			if( e.Column == colPO_ID1 )
			{
				DataRow dr = gvDetail.GetDataRow( e.RowHandle );
				if( dr != null )
				{
					object oType = dr["PO_TYPE"];
					if( oType != null && oType != DBNull.Value )
					{
						if( oType.Equals( "F" ) )
						{
							e.RepositoryItem = riPOFSelect;							
						}
						else if( oType.Equals( "B" ) )
						{
							e.RepositoryItem = riPOBSelect;							
						}
						else if( oType.Equals( "D" ) )
						{
							e.RepositoryItem = riPODSelect;							
						}
						else if( oType.Equals( "M" ) )
						{
							e.RepositoryItem = riPOMSelect;							
						}
						else if( oType.Equals( "2" ) )
						{
							e.RepositoryItem = riPOM2Select;							
						}
						else if( oType.Equals( "I" ) )
						{
							e.RepositoryItem = riPOSelect;
						}
					}
					else
					{
						e.RepositoryItem = riPOSelect;
					}
				}
				else
				{
					e.RepositoryItem = riPOSelect;
				}
			}
			if( e.Column == colPO_ID2 )
			{
				e.RepositoryItem = riNoPOSelect;
				
				DataRow dr = gvDetail.GetDataRow( e.RowHandle );
				if( dr != null )
				{
					object oType = dr["PO_TYPE"];
					if( oType != null && oType != DBNull.Value )
					{
						if( oType.Equals( "I" ) )
						{
							e.RepositoryItem = riDetPOSelect;
						}
					}
				}
			}
            else if (e.Column == colGL_ACCOUNT)
            {
                GL_Repository.ReadOnly = false;
                e.RepositoryItem = GL_Repository;

                bool bLock = false;
                DataRow dr = gvDetail.GetDataRow(e.RowHandle);
                if (dr != null)
                {
                    object oPO_ID = dr["PO_ID"];
                    if (oPO_ID != null && oPO_ID != DBNull.Value)
                    {
                        bLock = true;
                    }

                    object oPO_REC_ID = dr["PO_REC_ID"];
                    if (oPO_REC_ID != null && oPO_REC_ID != DBNull.Value)
                    {
                        bLock = true;
                    }

                    object oPRI_ID = dr["pri_id"];
                    if (oPRI_ID == null || oPRI_ID == DBNull.Value)
                        oPRI_ID = -1;
                    if (Convert.ToInt32(oPRI_ID) > 0)
                    {
                        bLock = true;
                    }
                }

                dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                if (dr != null)
                {
                    object oPO_ID = dr["PO_ID"];
                    if (oPO_ID != null && oPO_ID != DBNull.Value)
                    {
                        bLock = true;
                    }
                }
                
                if (bLock)
                {
                    GL_Repository.ReadOnly = true;
                    e.RepositoryItem = GL_Repository;
                }

                //handles loading of gl accounts when a company is selected in the detail
                if (gvDetail.GetDataRow(e.RowHandle) != null)
                {
                    //Does standard test for blank string but example doesn't check for invalid database name
                    if (gvDetail.GetDataRow(e.RowHandle)["COMPANY_ALIAS"].ToString() != "")
                    {
                        string sSelect = "select autoid from companies where treasurydbname='" + gvDetail.GetDataRow(e.RowHandle)["COMPANY_ALIAS"] + "'";
                        object obj = ExecuteScalar(sSelect, new SqlConnection(Connection.WebConnection));
                        if (obj != null)
                        {
                            GL_Repository.HMConnection = new HMCon(Connection.WebDB, Connection.WebServer, Convert.ToInt32(obj), Connection.MLUser);
                        }
                    }
                    else
                    {
                        if (GL_Repository.HMConnection != Connection)
                            GL_Repository.HMConnection = Connection;
                    }

                }
                else
                {
                    if (GL_Repository.HMConnection != Connection)
                        GL_Repository.HMConnection = Connection;
                }
            }
            else if (e.Column == colGL_ACCOUNT1)
            {
                e.RepositoryItem = riGLDesc;

                bool bLock = false;
                DataRow dr = gvDetail.GetDataRow(e.RowHandle);
                if (dr != null)
                {
                    object oPO_ID = dr["PO_ID"];
                    if (oPO_ID != null && oPO_ID != DBNull.Value)
                    {
                        bLock = true;
                    }

                    object oPO_REC_ID = dr["PO_REC_ID"];
                    if (oPO_REC_ID != null && oPO_REC_ID != DBNull.Value)
                    {
                        bLock = true;
                    }
                }

                dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                if (dr != null)
                {
                    object oPO_ID = dr["PO_ID"];
                    if (oPO_ID != null && oPO_ID != DBNull.Value)
                    {
                        bLock = true;
                    }
                }

                if (bLock)
                {
                    e.RepositoryItem = riGLDescReadOnly;
                }

                //handles loading of gl accounts when a company is selected in the detail
                if (gvDetail.GetDataRow(e.RowHandle) != null)
                {
                    dsGLAccts1.Clear();
                    //Does standard test for blank string but example doesn't check for invalid database name
                    if (gvDetail.GetDataRow(e.RowHandle)["COMPANY_ALIAS"].ToString() != "")
                    {
                        string sSelect = "select autoid from companies where treasurydbname='" + gvDetail.GetDataRow(e.RowHandle)["COMPANY_ALIAS"] + "'";
                        object obj = ExecuteScalar(sSelect, new SqlConnection(Connection.WebConnection));
                        if (obj != null)
                        {                            
                            daGLAccts.SelectCommand.Connection = new SqlConnection(GL_Repository.HMConnection.TRConnection);
                            daGLAccts.Fill(dsGLAccts1);
                        }
                    }
                    else
                    {
                        daGLAccts.SelectCommand.Connection = new SqlConnection(GL_Repository.HMConnection.TRConnection);
                        daGLAccts.Fill(dsGLAccts1);
                    }

                }
                else
                {
                    daGLAccts.SelectCommand.Connection = new SqlConnection(GL_Repository.HMConnection.TRConnection);
                    daGLAccts.Fill(dsGLAccts1);
                }
            }	
		}

		private void FillPOInvSelect()
		{
			dsPOInvSelect1.Clear();

			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oSupplier = dr["SUPPLIER"];
				if( oSupplier != null && oSupplier != DBNull.Value )
				{
					string sExec = "exec sp_AP_Fill_PO_Inv_Select '"+Connection.MLUser+"', '"+oSupplier+"'";
					Connection.SQLExecutor.ExecuteNonQuery( sExec, Connection.TRConnection );
				}			
				daPOInvSelect.Fill( dsPOInvSelect1 );
			}
		}

		private void riDetPOSelect_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
		{
			LookUpEdit lue = sender as LookUpEdit;
			
			if( lue != null )
			{
				object oPO_ID = lue.EditValue;
				if( oPO_ID != null && oPO_ID != DBNull.Value )
				{
					DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
					if( dr != null )
					{
						object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
						if( oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value )
						{
							_PO_ID = Convert.ToInt32( oPO_ID );
							_AP_INV_HEADER_ID = Convert.ToInt32( oAP_INV_HEADER_ID );
                            using (frmPOSelect POSelect = new frmPOSelect(Connection, DevXMgr, _ShowContractPOSummary))
                            {
                                POSelect.MatchPOReciept.AmountUpdated += new APPOSelect.ucMatchPOReceipt.Delegate_AmountUpdated(DetMatching_AmountUpdated);
                                POSelect.UnreleasedContractPO.AmountUpdated += new APPOSelect.ucUnreleasedContractPO.Delegate_AmountUpdated(DetMatchingCon_AmountUpdated);
                                POSelect.SummaryContractPO.AmountUpdated += new ucSummaryContractPO.Delegate_AmountUpdated(DetMatching_AmountUpdated);
                                POSelect.MatchPOReciept.RefreshPO_ID(_PO_ID, _AP_INV_HEADER_ID);
                                POSelect.UnreleasedContractPO.RefreshPO_ID(_PO_ID, _AP_INV_HEADER_ID);
                                POSelect.SummaryContractPO.RefreshPO_ID(_PO_ID, _AP_INV_HEADER_ID);

                                POSelect.MatchPOReciept.AP_INV_HEADER_ID = _AP_INV_HEADER_ID;
                                POSelect.UnreleasedContractPO.AP_INV_HEADER_ID = _AP_INV_HEADER_ID;
                                POSelect.SummaryContractPO.AP_INV_HEADER_ID = _AP_INV_HEADER_ID;
                                POSelect.MatchPOReciept.Year = ucAccountingPicker1.SelectedYear;
                                POSelect.MatchPOReciept.Period = ucAccountingPicker1.SelectedPeriod;

                                if (POSelect.UnreleasedContractPO.HasRows)
                                    POSelect.xtraTabControl1.SelectedTabPageIndex = 1;
                                if (_ShowContractPOSummary && POSelect.SummaryContractPO.HasRows)
                                    POSelect.xtraTabControl1.SelectedTabPageIndex = 2;
                                POSelect.ShowDialog();
                            }
						}
					}
				}
			}
		}

		private void DetMatching_AmountUpdated(double Amount)
		{
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["AP_GL_ALLOC_ID"];
                if (oID == null || oID == DBNull.Value)
                {
                    gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
                }
            }
            int iHandle = gvDetail.FocusedRowHandle;
            if (iHandle < 0)
                iHandle = gvDetail.RowCount - 1;

			string sExec = "exec sp_APCreateGLAlloc "+_AP_INV_HEADER_ID+", "+_PO_ID+", 'T', 'T', '"+Connection.MLUser+"', 'M'";
			ExecuteNonQuery( sExec, TR_Conn );

            dsInvDetail1.Clear();
            daInvDetail.Fill(dsInvDetail1);

            gvDetail.FocusedRowHandle = iHandle;
            CalculateRemaining();
		}

		private void DetMatchingCon_AmountUpdated(double Amount)
		{
            DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["AP_GL_ALLOC_ID"];
                if (oID == null || oID == DBNull.Value)
                {
                    gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
                }
            }
            int iHandle = gvDetail.FocusedRowHandle;
            if (iHandle < 0)
                iHandle = gvDetail.RowCount - 1;

			string sExec = "exec sp_APCreateGLAlloc "+_AP_INV_HEADER_ID+", "+_PO_ID+", 'T', 'T', '"+Connection.MLUser+"', 'C'";
			ExecuteNonQuery( sExec, TR_Conn );

            dsInvDetail1.Clear();
            daInvDetail.Fill(dsInvDetail1);

            gvDetail.FocusedRowHandle = iHandle;
            CalculateRemaining();
		}

		private bool ggDetail_AllowDelete(object sender, DataRow row)
		{
            if (!gvDetail.OptionsBehavior.Editable)
                return false;

            string sSQL;
            object oAP_GL_ALLOC_ID = -1;
			DataRow dr = gvDetail.GetDataRow( gvDetail.FocusedRowHandle );
			if( dr != null )
			{
                oAP_GL_ALLOC_ID = dr["AP_GL_ALLOC_ID"];
                if (oAP_GL_ALLOC_ID == null || oAP_GL_ALLOC_ID == DBNull.Value)
                    oAP_GL_ALLOC_ID = -1;

                object oCB_ID = dr["CB_ID"];
                if (oCB_ID == null || oCB_ID == DBNull.Value)
                    oCB_ID = -1;

				object oPO_Type = dr["PO_TYPE"];
				if( oPO_Type != null && oPO_Type != DBNull.Value )
				{
					if( oPO_Type.Equals( "I" ) )
					{						
						object oAP_REC_ENTRY_NO = dr["AP_REC_ENTRY_NO"];
						if( oAP_REC_ENTRY_NO != null && oAP_REC_ENTRY_NO != DBNull.Value )
						{
							if( Popup.ShowPopup( "Deleting this record will also remove any other rows that were matched to the same PO receiving point, do you wish to continue?", frmPopup.PopupType.OK_Cancel ) == frmPopup.PopupResult.OK )
							{
                                string sDelete = @"delete from ap_gl_alloc where ap_rec_entry_no=" + oAP_REC_ENTRY_NO + @"
                                    delete from ap_receiver where AP_REC_ENTRY_NO=" + oAP_REC_ENTRY_NO + @"
                                    delete from ap_receiver_det where AP_REC_ENTRY_NO=" + oAP_REC_ENTRY_NO;
								Connection.SQLExecutor.ExecuteNonQuery( sDelete, Connection.TRConnection );

                                if (Convert.ToInt32(oCB_ID) != -1)
                                {
                                    sSQL = @"exec CB_DeleteChargeBack '" + Connection.MLUser + @"', " + oCB_ID;
                                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                                    CB_OnHdr();
                                }
                                                                
								gvHeader_FocusedRowChanged(null, null);
							}
							return false;
						}
						else
						{
                            if (Convert.ToInt32(oCB_ID) != -1)
                            {
                                if (Popup.ShowPopup("A chargeback exists on this line. Deleteing it will remove the chargeback, are you sure you want to continue?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                                {
                                    sSQL = @"exec CB_DeleteChargeBack '" + Connection.MLUser + @"', " + oCB_ID;
                                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                                    CB_OnHdr();
                                }
                                else
                                    return false;
                            }
							return true;
						}
					}
				}

                if (Convert.ToInt32(oCB_ID) != -1)
                {
                    if (Popup.ShowPopup("A chargeback exists on this line. Deleteing it will remove the chargeback, are you sure you want to continue?", frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                    {
                        sSQL = @"exec CB_DeleteChargeBack '" + Connection.MLUser + @"', " + oCB_ID;
                        Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        CB_OnHdr();
                    }
                    else
                        return false;
                }
			}
            
            sSQL = @"delete l
                from AP_PWP_LINK l 
                where l.AP_GL_ALLOC_ID=" + oAP_GL_ALLOC_ID;
            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

			return true;
		}

		private bool ggHeader_AllowDelete(object sender, DataRow row)
		{
			try
			{
				gvHeader.CloseEditor();						
				if( gvHeader.RowCount > 0 )
				{
                    string sMessage = "Are you sure you want to delete this invoice?";

                    object oHAS_CB = row["HAS_CB"];
                    if (oHAS_CB == null || oHAS_CB == DBNull.Value)
                        oHAS_CB = "F";

                    if (oHAS_CB.Equals("T"))
                        sMessage = "Chargeback(s) exist for this invoice, and will be deleted. Are you sure you want to delete this invoice?";


                    object oIS_CB = row["IS_CB"];
                    if (oIS_CB == null || oIS_CB == DBNull.Value)
                        oIS_CB = "F";

                    if (oIS_CB.Equals("T"))
                        sMessage = "This invoice was created as a chargeback. Deleting it will remove the chargeback references from the source that created it. Are you sure you want to delete this invoice?";

                    object oLevy = row["Levy"];
                    if (oLevy == null || oLevy == DBNull.Value)
                        oLevy = false;

                    if (Convert.ToBoolean(oLevy))
                        sMessage = "Levies have been matched on this invoice, and will be reset. Are you sure you want to delete this invoice?";
                    
                    if (Popup.ShowPopup(this, sMessage, frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
					{
                        //Validates delete if invoice was generated by chargeback
                        if (oIS_CB.Equals("T"))
                        {
                            if (!KCA_Validator.ValidatePassword(CONST_CHARGE_BACK_DELETE))
                                return false;
                        }

                        object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
                        if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
						{
                            if (oIS_CB.Equals("T"))
                            {
                                string sSQL = @"exec CB_DeleteInvoice '" + Connection.MLUser + @"', 'AP', " + oAP_INV_HEADER_ID+@", 'D'";
                                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                            }
                            else if (oHAS_CB.Equals("T"))
                            {
                                string sSQL = @"exec CB_DeleteInvoice '" + Connection.MLUser + @"', 'AP', " + oAP_INV_HEADER_ID + @", 'S'";
                                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                            }

                            string sDelete = @"
                                update l
                                set l.Matched=0
                                from PROJ_LOT_AGREEMENT_LEVY l
                                join ap_gl_alloc a on a.proj_lot_agreement_levy_id=l.id
                                where a.ap_inv_header_id=" + oAP_INV_HEADER_ID;
                            Connection.SQLExecutor.ExecuteNonQuery(sDelete, Connection.TRConnection);

                            sDelete = @"delete l
                                    from AP_PWP_LINK l 
                                    join AP_GL_ALLOC a on a.AP_GL_ALLOC_ID=l.AP_GL_ALLOC_ID
                                    where a.AP_INV_HEADER_ID="+oAP_INV_HEADER_ID+@"
                                delete from AP_GL_ALLOC where AP_INV_HEADER_ID=" + oAP_INV_HEADER_ID + @" 
                                delete from ap_receiver_det where ap_rec_entry_no in (select ap_rec_entry_no from ap_receiver where ap_inv_header_id=" + oAP_INV_HEADER_ID + @") 
                                delete from ap_receiver where ap_inv_header_id=" + oAP_INV_HEADER_ID + @" 
                                delete from ap_inv_header where ap_inv_header_id=" + oAP_INV_HEADER_ID;
                            Connection.SQLExecutor.ExecuteNonQuery(sDelete, Connection.TRConnection);
                         
					
							int iHandle = gvHeader.FocusedRowHandle;
							dsInvHeader1.Clear();
							daInvHeader.Fill( dsInvHeader1 );
                            SizeHeaderColumns();
							if( iHandle > 0 )
							{
								gvHeader.FocusedRowHandle = iHandle - 1;
								gvHeader_FocusedRowChanged( null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs( 0, gvHeader.FocusedRowHandle ) );
							}
							else
							{
								gvHeader_FocusedRowChanged( null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs( 0, 0 ) );
							}

							return false;
						}
						else
						{
							gvHeader.DeleteRow( gvHeader.FocusedRowHandle );
							return true;
						}
					}
					return false;
				}				
			}
			catch( DBConcurrencyException )
			{
				Popup.ShowPopup( this, "This record has been modified by another user and will be refreshed." );
				RefreshMe();
				return false;
			}	
			return true;
		}

		private void gvHeader_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if(e.Column == colINV_DATE)
			{
				DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
				if( dr != null )
				{
					object oSupplier = dr["SUPPLIER"];
					if( oSupplier != null && oSupplier != DBNull.Value )
					{
						CalculateTerms( oSupplier.ToString(), null );
					}
				}
			}
            else if(e.Column == colPO_ID)
            {
                object oPO_ID = gvHeader.GetFocusedRowCellValue("PO_ID");
                if (oPO_ID == null || oPO_ID == DBNull.Value)
                    oPO_ID = -1;
                if ((int)oPO_ID == -1)
                    btnPOAttachments.Enabled = false;
                else
                    btnPOAttachments.Enabled = true;

                UpdatePOAttachmentsCount();
            }
		}

		private void hlChangeSupp_Click(object sender, System.EventArgs e)
		{
			if( KCA_Validator.ValidatePassword( CONST_OVERRIDE_SUPPLIER_ON_PO ) )
			{
				ucSuppChange ucSC = new ucSuppChange( Connection, DevXMgr );
				if( ucSC.ShowDialog() == DialogResult.OK )
				{
					dsPO1.Clear();
					DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
					if( dr != null )
					{
						object oSupp = dr["SUPPLIER"];
						if( oSupp != null && oSupp != DBNull.Value )
						{
							daPO.SelectCommand.Parameters["@supplier"].Value = oSupp;
							daPO.Fill( dsPO1 );
						}
					}				
				}
			}
		}

        private void dockManager1_Load(object sender, EventArgs e)
        {
            dpActions.Height = 300;	
        }

        private void RecallRequest()
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["AP_INV_HEADER_ID"];
                if (oID != null && oID != DBNull.Value)
                {
                    string sSQL = "sp_ApprovalRequestRecallByCallerID '"+oID+"', "+Connection.ContactID+", null, "+Connection.CompanyID+", 58";
                    object obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
                }
            }
        }

        private void tcDetails_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            bool bNoPO = false;
            if (tcDetails.SelectedTabPage == tpMatchPO)
            {
                if (!_MatchPOLoaded)
                {
                    DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                    if (dr != null)
                    {
                        _MatchPOLoaded = true;
                        object oPO_ID = dr["PO_ID"];
                        if (oPO_ID != null && oPO_ID != DBNull.Value)
                        {
                            object oAP_INV_ID = dr["AP_INV_HEADER_ID"];
                            if (oAP_INV_ID == null || oPO_ID == DBNull.Value)
                                oAP_INV_ID = -1;
                            ucMPOR.RefreshPO_ID(Convert.ToInt32(oPO_ID), Convert.ToInt32(oAP_INV_ID));
                        }
                        else
                        {
                            bNoPO = true;
                        }
                    }
                    else
                    {
                        bNoPO = true;
                    }
                }
                if (bNoPO)
                {
                    ucMPOR.RefreshPO_ID(-1, -1);
                }
            }
            else if (tcDetails.SelectedTabPage == tpContractPO || tcDetails.SelectedTabPage == tpSummContractPO)
            {
                ucUCPO.RefreshMe();
                ucSCPO.RefreshMe();
                if (!_UnreleasedLoaded)
                {
                    DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
                    if (dr != null)
                    {
                        _UnreleasedLoaded = true;
                        object oPO_ID = dr["PO_ID"];
                        if (oPO_ID != null && oPO_ID != DBNull.Value)
                        {
                            object oAP_INV_ID = dr["AP_INV_HEADER_ID"];
                            if (oAP_INV_ID == null || oPO_ID == DBNull.Value)
                                oAP_INV_ID = -1;
                            ucUCPO.RefreshPO_ID(Convert.ToInt32(oPO_ID), Convert.ToInt32(oAP_INV_ID));
                            ucSCPO.RefreshPO_ID(Convert.ToInt32(oPO_ID), Convert.ToInt32(oAP_INV_ID));
                        }
                        else
                        {
                            bNoPO = true;
                        }
                    }
                    else
                    {
                        bNoPO = true;
                    }
                }
                if (bNoPO)
                {
                    ucUCPO.RefreshPO_ID(-1, -1);
                    ucSCPO.RefreshPO_ID(-1, -1);
                }
            }            
        }

        private void gvDetail_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            gvDetail.Focus();
        }

        private void gvHeader_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            if( !e.ErrorText.Equals(CONST_SUPRESS_ERROR) )
                Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            gvHeader.Focus();
        }

        private void riRouteStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                if (txtUndist.EditValue != null)
                {
                    if (Convert.ToDouble(txtUndist.EditValue) != 0)
                    {
                        Popup.ShowPopup("Invoice is not balanced, undistributed amount must equal 0.");
                        return;
                    }
                }
                if (txtRemain.EditValue != null)
                {
                    if (Convert.ToDouble(txtRemain.EditValue) != 0)
                    {
                        if (Connection.CountryCode != "U")
                            Popup.ShowPopup("Invoice is not balanced, holdback remaining amount must equal 0.");
                        else
                            Popup.ShowPopup("Invoice is not balanced, retainage remaining amount must equal 0.");
                        return;
                    }
                }

                object oSTATUS = dr["KC_CONTRACTPO_STATUS"];
                if (oSTATUS.Equals("Q") || oSTATUS.Equals("R") || oSTATUS.Equals("D") || oSTATUS.Equals("P"))
                {
                    bool bNoRecall = true;
                    if (oSTATUS.Equals("P"))
                        bNoRecall = false;
                    DevExpress.XtraEditors.XtraForm f = new XtraForm();
                    f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    f.ShowInTaskbar = false;

                    ucARHV = new ucApprovalRequestHistoryViewer(Connection, DevXMgr, false);
                    ucARHV.Dock = DockStyle.Fill;
                    ucARHV.RequestRecalled += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestRecalled(ucARHV_RequestRecalled);
                    ucARHV.RequestSubmitted += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestSubmitted(ucARHV_RequestSubmitted);
                    ucARHV.RefreshMe(dr["AP_INV_HEADER_ID"].ToString(), "AP PO", null, CONST_UNAPPROVED_CONTRACT_PO_APPROVAL_TOPIC_ID, false, bNoRecall);
                    ucARHV.Parent = f;
                    f.StartPosition = FormStartPosition.CenterParent;
                    f.Size = new Size(650, 650);
                    f.ShowDialog();
                    ucARHV.Dispose();
                    ucARHV = null;
                }
            }
        }

        private void riRouteStatusPreAccrual_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            #region New Workflow Routing

            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                if (txtUndist.EditValue != null)
                {
                    if (Convert.ToDouble(txtUndist.EditValue) != 0)
                    {
                        Popup.ShowPopup("Invoice is not balanced, undistributed amount must equal 0.");
                        return;
                    }
                }
                if (txtRemain.EditValue != null)
                {
                    if (Convert.ToDouble(txtRemain.EditValue) != 0)
                    {
                        if (Connection.CountryCode != "U")
                            Popup.ShowPopup("Invoice is not balanced, holdback remaining amount must equal 0.");
                        else
                            Popup.ShowPopup("Invoice is not balanced, retainage remaining amount must equal 0.");
                        return;
                    }
                }

                string sSQL = @"select WS_Approval_WorkFlow_ID from WF_ApprovalPoint where WF_ID = 4";
                object oWorkFlow_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
                if (oWorkFlow_ID == null || oWorkFlow_ID == DBNull.Value)
                {
                    Popup.ShowPopup("The 'Subcontractor Compliance Pre-Accrual' work flow routing point is incorrectly configured.");
                    return;
                }

                object oSTATUS = dr["KC_ACCRUAL_STATUS"];
                if (oSTATUS.Equals("Q") || oSTATUS.Equals("D") || oSTATUS.Equals("P") || oSTATUS.Equals("A"))
                {
                    WorkFlowRouting.frmApprovalViewer fViewer = new WorkFlowRouting.frmApprovalViewer(Connection, DevXMgr);
                    fViewer.LoadStatus(CONST_SUBCON_COMP_PRE_ACCRUAL_WF, oSTATUS.ToString(), Convert.ToInt32(dr["ap_inv_header_id"]));
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
            //    if (txtUndist.EditValue != null)
            //    {
            //        if (Convert.ToDouble(txtUndist.EditValue) != 0)
            //        {
            //            Popup.ShowPopup("Invoice is not balanced, undistributed amount must equal 0.");
            //            return;
            //        }
            //    }
            //    if (txtRemain.EditValue != null)
            //    {
            //        if (Convert.ToDouble(txtRemain.EditValue) != 0)
            //        {
            //            if (Connection.CountryCode != "U")
            //                Popup.ShowPopup("Invoice is not balanced, holdback remaining amount must equal 0.");
            //            else
            //                Popup.ShowPopup("Invoice is not balanced, retainage remaining amount must equal 0.");
            //            return;
            //        }
            //    }

            //    object oSTATUS = dr["KC_ACCRUAL_STATUS"];
            //    if (oSTATUS.Equals("Q") || oSTATUS.Equals("R") || oSTATUS.Equals("D") || oSTATUS.Equals("P"))
            //    {
            //        bool bNoRecall = true;
            //        if (oSTATUS.Equals("P"))
            //            bNoRecall = false;
            //        DevExpress.XtraEditors.XtraForm f = new XtraForm();
            //        f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //        f.ShowInTaskbar = false;

            //        ucARHV = new ucApprovalRequestHistoryViewer(Connection, DevXMgr, false);
            //        ucARHV.Dock = DockStyle.Fill;
            //        ucARHV.RequestRecalled += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestRecalled(ucARHV2_RequestRecalled);
            //        ucARHV.RequestSubmitted += new ApprovalRequestHistoryViewer.ucApprovalRequestHistoryViewer.Delegate_RequestSubmitted(ucARHV2_RequestSubmitted);
            //        ucARHV.RefreshMe(dr["AP_INV_HEADER_ID"].ToString(), "AP", null, CONST_SUBCON_COMP_PRE_ACCRUAL, false, bNoRecall);
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

        private void riRouteStatusWorkFlow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                // Validate the invoice amount does not equal zero.
                object oAmt = dr["INV_AMOUNT"];
                if (oAmt == null && oAmt == DBNull.Value)
                    oAmt = 0;

                if (Convert.ToDouble(oAmt) == 0)
                {
                    Popup.ShowPopup("The total invoice amount cannot be zero.");
                    return;
                }

                if (txtUndist.EditValue != null)
                {
                    if (Convert.ToDouble(txtUndist.EditValue) != 0)
                    {
                        Popup.ShowPopup("Invoice is not balanced, undistributed amount must equal 0.");
                        return;
                    }
                }
                if (txtRemain.EditValue != null)
                {
                    if (Convert.ToDouble(txtRemain.EditValue) != 0)
                    {
                        if (Connection.CountryCode != "U")
                            Popup.ShowPopup("Invoice is not balanced, holdback remaining amount must equal 0.");
                        else
                            Popup.ShowPopup("Invoice is not balanced, retainage remaining amount must equal 0.");
                        return;
                    }
                }

                object oWF_Approval_ID = dr["WF_Approval_ID"];
                if (_AP_ForceWF)
                {
                    if (oWF_Approval_ID == null || oWF_Approval_ID == DBNull.Value)
                    {
                        gvHeader.FocusedColumn = colWF_Approval_ID;                        
                        Popup.ShowPopup("A work flow routing point is required prior to routing.");
                        return;
                    }                 
                }

                // Validate that the WF_Approval_ID is for a 'S' (Stakeholder) Approval Type Work Flow when PO Matching occured at header and PO is proj generated
                // or detail matched all is referenced to same project
                string sSQL = @"select COUNT(*) from WS_Approval_WorkFlow where Approval_ID=" + oWF_Approval_ID + @" and Approval_Type='S'";
                object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
                if (oCNT == null || oCNT == DBNull.Value)
                    oCNT = 0;
                if (Convert.ToInt32(oCNT) > 0)
                {
                    object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
                    if (oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value)
                    {
                        int iTotalProjects = 0;
                        int iNonProjects = 0;
                        sSQL = @"declare @total_projects int, @non_projects int
                            exec AP_WF_ValidateDetail "+oAP_INV_HEADER_ID+@", @total_projects output, @non_projects output
                            select @total_projects, @non_projects";
                        DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow drVal = dt.Rows[0];
                                if (drVal != null)
                                {
                                    object oTotal = drVal[0];
                                    object oNon = drVal[1];

                                    if (oTotal == null || oTotal == DBNull.Value)
                                        oTotal = 0;
                                    if (oNon == null || oNon == DBNull.Value)
                                        oNon = 0;

                                    iTotalProjects = Convert.ToInt32(oTotal);
                                    iNonProjects = Convert.ToInt32(oNon);
                                }
                            }
                        }

                        if (iTotalProjects == 1 && iNonProjects == 0)
                        {
                            goto ProjectStakeholderValid;
                        }

                        // Change message
                        Popup.ShowPopup("The 'Accounts Payable Invoice' work flow routing point cannot be a 'Stakeholder' based approval type because not all of the detail has been reference to the same project.");
                        return;
                    }
                }

                ProjectStakeholderValid:

                object oSTATUS = dr["WF_STATUS"];
                if (oSTATUS.Equals("Q") || oSTATUS.Equals("D") || oSTATUS.Equals("P") || oSTATUS.Equals("A") || oSTATUS.Equals("R"))
                {                    
                    if (HeaderValid())
                    {
                        SaveHeader();
                        object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
                        WorkFlowRouting.frmApprovalViewer fViewer = new WorkFlowRouting.frmApprovalViewer(Connection, DevXMgr);
                        fViewer.LoadStatus(CONST_ACCOUNTS_PAYABLE_WF, oSTATUS.ToString(), Convert.ToInt32(oAP_INV_HEADER_ID), Convert.ToInt32(oWF_Approval_ID));
                        if (fViewer.ShowDialog() == DialogResult.OK)
                        {
                            RefreshMe();
                        }
                    }
                }

                #region Old HRC Work Flow Routing

//                object oPO_ID = dr["PO_ID"];
//                if (oPO_ID != null && oPO_ID != DBNull.Value && (oWF_Approval_ID == null || oWF_Approval_ID == DBNull.Value))
//                {
//                    string sSelect = "select isnull(pri_num,-1) from po_header where po_id="+oPO_ID;
//                    if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) != -1)
//                    {
//                        object oSTATUS = dr["WF_STATUS"];
//                        if (oSTATUS.Equals("Q") || oSTATUS.Equals("D"))
//                        {
//                            if (Popup.ShowPopup("A work flow routing point has not been selected. This invoice will be routed through Consultant/Project Manager/PM Manager. Are you sure you want proceed?", WS_Popups.frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
//                            {
//                                string sSQL = @"declare @message varchar(250)
//                                exec AP_SubmitPORoute " + dr["ap_inv_header_id"] + @", " + Connection.ContactID + @", @message output
//                                select @message";
//                                object oResult = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
//                                if (oResult != null && oResult != DBNull.Value)
//                                {
//                                    if (!oResult.Equals("OK"))
//                                    {
//                                        Popup.ShowPopup(oResult.ToString());
//                                    }
//                                    else
//                                    {
//                                        RefreshMe();
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {
//                            Popup.ShowPopup("This invoice is currently out for approval.");
//                        }

//                        return;
//                    }
//                }

//                if (oWF_Approval_ID != null && oWF_Approval_ID != DBNull.Value )
//                {
//                    object oSTATUS = dr["WF_STATUS"];
//                    if (oSTATUS.Equals("Q") || oSTATUS.Equals("D") || oSTATUS.Equals("P") || oSTATUS.Equals("A"))
//                    {
//                        WorkFlowRouting.frmApprovalViewer fViewer = new WorkFlowRouting.frmApprovalViewer(Connection, DevXMgr);
//                        fViewer.LoadStatus(WorkFlowRouting.frmApprovalViewer.Type.AP, Convert.ToInt32(dr["ap_inv_header_id"]));
//                        if (fViewer.ShowDialog() == DialogResult.OK)
//                        {
//                            RefreshMe();
//                        }
//                    }
//                }
//                else
//                {
//                    Popup.ShowPopup("A work flow routing point is required prior to routing.");
//                }

                #endregion
            }
        }

        private bool HeaderValid()
        {
            DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs eArgs = new DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs(gvHeader.FocusedRowHandle, gvHeader.GetFocusedRow());
            gvHeader_ValidateRow(gvHeader, eArgs);
            if (!eArgs.Valid)
                Popup.ShowPopup(eArgs.ErrorText);
            return eArgs.Valid;
        }

        private void riRouteStatusWorkFlow_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void ucARHV_RequestRecalled(int ApprovalRequestID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["KC_CONTRACTPO_STATUS"] = "R";
                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);

                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + dr["ap_inv_header_id"] + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Recalled invoice AP unapproved contract PO matching routing.', '', 1 ";
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
                dr["KC_CONTRACTPO_STATUS"] = "P";
                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);

                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + dr["ap_inv_header_id"] + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Invoice submitted for AP unapproved contract PO matching routing.', '', 1 ";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);

                RoutingLock();
                ucARHV.ParentForm.Close();
            }
        }

        private void ucARHV2_RequestRecalled(int ApprovalRequestID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["KC_ACCRUAL_STATUS"] = "R";
                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);
                
                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + dr["ap_inv_header_id"] + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Recalled invoice subcontractor compliance pre-accrual routing.', '', 1 ";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);

                RoutingLock();
            }
        }

        private void ucARHV2_RequestSubmitted(int ApprovalRequestID)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["KC_ACCRUAL_STATUS"] = "P";                
                ggHeader.SaveRecord(gvHeader.FocusedRowHandle);
                
                string sInsert = "declare @ws_inv_id int " +
                    "select @ws_inv_id=ws_inv_id from WS_INV_HEADER where ap_inv_header_id=" + dr["ap_inv_header_id"] + " " +
                    " " +
                    "insert into WS_EVENT_HISTORY (DETAIL_ID, EVENT_DATE, CONTACT_ID, EVENT, NOTES, ST_TYPE_ID) " +
                    "select @ws_inv_id, GETDATE(), " + Connection.ContactID + ", 'Invoice submitted for subcontractor compliance pre-accrual routing.', '', 1 ";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);

                RoutingLock();
                ucARHV.ParentForm.Close();
            }
        }

        private void riRouteStatus2_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void riRouteStatus_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnSharepoint_Click(object sender, EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["ap_inv_header_id"];
                if (oID != null && oID != DBNull.Value)
                {
                    int iAP_Inv_Header_ID = Convert.ToInt32(oID);
                    using (SharePointMgr.frmSharePointMgr SharePointManager = new SharePointMgr.frmSharePointMgr(Connection, DevXMgr, "AP Invoice", "AP Invoice", "", "Header", iAP_Inv_Header_ID))
                    {
                        SharePointManager.ReadOnly = false;
                        SharePointManager.ShowDialog();
                        SharePointManager.Dispose();
                        SharePointMgr.cSharePointMgr.UpdateButton(Connection, "AP Invoice", ref btnSharepoint, "Header", iAP_Inv_Header_ID);
                    }
                }
            }
        }

        private void hlPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists((Connection.ReportPath.EndsWith("\\") ? Connection.ReportPath : Connection.ReportPath + "\\") + "AP_InvoiceEntryListingX.rpt"))
                {
                    string[,] saParams = new string[2, 1];
                    saParams[0, 0] = "@username";
                    saParams[1, 0] = Connection.MLUser;
                    using (frmHM_Report_Printer HMRP = new frmHM_Report_Printer(Connection, DevXMgr, saParams, "AP_InvoiceEntryListingX.rpt", frmHM_Report_Printer.DBFlavor.TR))
                    {
                        HMRP.ShowDialog();
                    }                    
                }
                else
                {
                    Popup.ShowPopup("The report AP_InvoiceEntryListingX.rpt cannot be found.");
                }
            }
            catch (Exception ex)
            {
                Popup.ShowPopup(this, ex.Message + ex.StackTrace);
            }
        }

        private void riHours_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit te = sender as TextEdit;
            if (te != null)
            {
                DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                if (dr != null)
                {
                    object oHrs = te.EditValue;
                    if (oHrs == null || oHrs == DBNull.Value)
                        oHrs = 0;
                    double dHrs = Convert.ToDouble(oHrs);

                    object oAmt = dr["AMOUNT"];
                    if (oAmt == null || oAmt == DBNull.Value)
                        oAmt = 0;
                    double dAmt = Convert.ToDouble(oAmt);

                    double dRate = 0;
                    if( dHrs != 0 )
                    {
                        dRate = Math.Round( (dAmt / dHrs), 2, MidpointRounding.AwayFromZero);
                    }

                    dr.BeginEdit();
                    dr["RATE"] = dRate;
                    dr.EndEdit();
                }
            }
        }

        private void repositoryItemTextEdit9_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit te = sender as TextEdit;
            if (te != null)
            {
                DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                if (dr != null)
                {
                    object oAmt = te.EditValue;
                    if (oAmt == null || oAmt == DBNull.Value)
                        oAmt = 0;
                    double dAmt = Convert.ToDouble(oAmt);

                    object oHrs = dr["HOURS"];
                    if (oHrs == null || oHrs == DBNull.Value)
                        oHrs = 0;
                    double dHrs = Convert.ToDouble(oHrs);

                    double dRate = 0;
                    if (dHrs != 0)
                    {
                        dRate = Math.Round((dAmt / dHrs), 2, MidpointRounding.AwayFromZero);
                    }

                    dr.BeginEdit();
                    dr["RATE"] = dRate;
                    dr.EndEdit();
                }
            }
        }

        private void riRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit te = sender as TextEdit;
            if (te != null)
            {
                DataRow dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                if (dr != null)
                {
                    object oRate = te.EditValue;
                    if (oRate == null || oRate == DBNull.Value)
                        oRate = 0;
                    double dRate = Convert.ToDouble(oRate);

                    object oAmt = dr["AMOUNT"];
                    if (oAmt == null || oAmt == DBNull.Value)
                        oAmt = 0;
                    double dAmt = Convert.ToDouble(oAmt);

                    double dHrs = 0;
                    if (dRate != 0)
                    {
                        dHrs = Math.Round((dAmt / dRate), 2, MidpointRounding.AwayFromZero);
                    }

                    dr.BeginEdit();
                    dr["HOURS"] = dHrs;
                    dr.EndEdit();
                }
            }
        }

        private void btnDirectAttachemnts_Click(object sender, EventArgs e)
        {
            if (Current_AP_INV_ID != -1)
            {
                try
                {
                    using (WO_CentralizedFSManager.frmFileManager frm = new WO_CentralizedFSManager.frmFileManager(Connection, DevXMgr,
                        WO_CentralizedFSManager.DocumentViewerMode.All, RelType, Current_AP_INV_ID, false))
                    {
                        frm.ShowDialog();

                    }
                    UpdateAdditionaAttachmentsCount();
                }
                catch { }
            }
        }

        private void btnPOAttachments_Click(object sender, EventArgs e)
        {            
            try
            {
                bool bReadOnly = false;
                string sSQL = @"select isnull((select AttachEditForCompPO from po_setup), 0)";
                object oEditable = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oEditable == null || oEditable == DBNull.Value)
                    oEditable = false;
                bool bEditable = Convert.ToBoolean(oEditable);

                object oStatus = gvHeader.GetFocusedRowCellValue("STATUS");
                if (oStatus == null || oStatus == DBNull.Value)
                    oStatus = "";
                string sStatus = oStatus.ToString();

                if ((sStatus.Equals("Cancel") || sStatus.Equals("Complete") || sStatus.Equals("Closed")) && !bEditable)
                    bReadOnly = true;

                object oPO_ID = gvHeader.GetFocusedRowCellValue("PO_ID");
                if (oPO_ID == null || oPO_ID == DBNull.Value)
                    oPO_ID = -1;

                using (WO_CentralizedFSManager.frmFileManager frm = new WO_CentralizedFSManager.frmFileManager(Connection, DevXMgr,
                    WO_CentralizedFSManager.DocumentViewerMode.All, PORelType, (int)oPO_ID, bReadOnly))
                {
                    frm.ShowDialog();
                }
                UpdatePOAttachmentsCount();
            }
            catch { }
        }

        private void UpdatePOAttachmentsCount()
        {
            object oPO_ID = gvHeader.GetFocusedRowCellValue("PO_ID");
            if (oPO_ID == null || oPO_ID == DBNull.Value)
                oPO_ID = -1;

            int iPO_ID = (int)oPO_ID;
            
            WO_CentralizedFSManager.cCentralizedFSUtils.UpdateButton(Connection, PORelType, iPO_ID, ref btnPOAttachments, "PO Attachments");            
        }

        private void UpdateAdditionaAttachmentsCount()
        {
            WO_CentralizedFSManager.cCentralizedFSUtils.UpdateButton(Connection, RelType, Current_AP_INV_ID, ref btnDirectAttachemnts);
            if (Current_AP_INV_ID == -1)
            {
                btnDirectAttachemnts.Enabled = false;
            }
            else
            {
                btnDirectAttachemnts.Enabled = true;
            }
        }

        private void gcHeader_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
                LoadHeaderSide();
            }
        }

        private void hlEventHistory_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle );
            if (dr != null)
            {
                object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                if (oAP_INV_HEADER_ID == null || oAP_INV_HEADER_ID == DBNull.Value)
                    oAP_INV_HEADER_ID = -1;
                frmEH_Launch fEH_Launch = new frmEH_Launch(Connection, DevXMgr, Convert.ToInt32(oAP_INV_HEADER_ID));
                fEH_Launch.ShowDialog();
            }
        }

        private void hlOverrideCompliance_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            if (!KCA_Validator.ValidatePassword(CONST_SUBCONTRACTOR_COMPLIANCE_OVERRIDE))
                return;

            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                object oStatus = dr["KC_ACCRUAL_STATUS"];

                if (oStatus.Equals("P"))
                {
                    string sSQL = @"update d
                        set d.Handled = 'T'
                        from WF_RouteDet d 
                        join WF_Route h on h.WF_Route_ID=d.WF_Route_ID
                        where h.Link_ID="+dr["ap_inv_header_id"]+@" and WF_ApprovalPoint_ID=3";
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                }

                dr.BeginEdit();
                dr["KC_ACCRUAL_STATUS"] = DBNull.Value;
                dr.EndEdit();
                daInvHeader.Update(dsInvHeader1);
            }
        }

        private void btnLinkCompAttch_Click(object sender, EventArgs e)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if( dr != null )
            {
                if (!KCA_Validator.ValidatePassword(CONST_SUPPLIER_EDIT))
                {
                    return;
                }

                object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
                if( oAP_INV_HEADER_ID != null && oAP_INV_HEADER_ID != DBNull.Value )
                {
                    string sSQL = @"select supplier_id from supplier_master where supplier='"+ dr["SUPPLIER"]+@"'";
                    object oSupplierID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oSupplierID == null || oSupplierID == DBNull.Value)
                        oSupplierID = -1;
                    AP_SubcontractorCompliance.frmDocHotLink fDHL = new AP_SubcontractorCompliance.frmDocHotLink(Connection, DevXMgr, "APINV", Convert.ToInt32(oAP_INV_HEADER_ID), Convert.ToInt32(oSupplierID));
                    fDHL.ShowDialog();
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
                        daInvDetail.Update(dsInvDetail1);
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

        private void gvPWP_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvPWP.GetDataRow(gvPWP.FocusedRowHandle);
            if (dr != null)
            {
                object oSELECTED = dr["selected"];
                if (oSELECTED == null || oSELECTED == DBNull.Value)
                    oSELECTED = false;
                if (Convert.ToBoolean(oSELECTED))
                {
                    object oAP_GL_ALLOC_ID = gvDetail.GetFocusedRowCellValue("AP_GL_ALLOC_ID");
                    if (oAP_GL_ALLOC_ID != null && oAP_GL_ALLOC_ID != DBNull.Value)
                    {
                        string sSQL = @"select COUNT(*) from AP_PWP_LINK where AP_GL_ALLOC_ID=" + oAP_GL_ALLOC_ID;
                        object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                        if (oCNT == null || oCNT == DBNull.Value)
                            oCNT = 0;
                        if (Convert.ToInt32(oCNT) > 0)
                        {
                            e.ErrorText = "Only one AR invoice can be selected.";
                            e.Valid = false;
                            return;
                        }
                    }

                    object oAmt_Available = dr["amt_available"];
                    if (oAmt_Available == null || oAmt_Available == DBNull.Value)
                        oAmt_Available = 0;
                    double dAmt_Available = Convert.ToDouble(oAmt_Available);

                    object oDetAmt = gvDetail.GetFocusedRowCellValue("AMOUNT");
                    if (oDetAmt == null || oDetAmt == DBNull.Value)
                        oDetAmt = 0;
                    double dDetAmt = Convert.ToDouble(oDetAmt);

                    string sSelect = @"select isnull((select AllowPWPOverAllocation from ap_setup),0)";
                    object oAllow = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
                    if (oAllow == null || oAllow == DBNull.Value)
                        oAllow = false;
                    if (dDetAmt > dAmt_Available && !Convert.ToBoolean(oAllow))
                    {
                        e.ErrorText = "Paid when paid over allocation is not allowed.";
                        e.Valid = false;
                        return;
                    }
                }
            }
        }

        private void gvPWP_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = gvPWP.GetDataRow(gvPWP.FocusedRowHandle);
            if (dr != null)
            {
                object oAP_GL_ALLOC_ID = gvDetail.GetFocusedRowCellValue("AP_GL_ALLOC_ID");
                object oSO_TRN_ID = gvPWP.GetFocusedRowCellValue("so_trn_id");
                object oSELECTED = gvPWP.GetFocusedRowCellValue("selected");
                string sSQL = @"exec AP_PWP_CreateLink " + oAP_GL_ALLOC_ID + @", " + oSO_TRN_ID+@", '"+oSELECTED+@"'";
                DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection );
                if( dt != null )
                {
                    if( dt.Rows.Count > 0 )
                    {
                        DataRow drLink = dt.Rows[0];
                        if( drLink != null )
                        {
                            dr.BeginEdit();
                            dr["allocated_amt"] = drLink["allocated_amt"];
                            dr["amt_available"] = drLink["amt_available"];
                            dr["amt_to_allocate"] = drLink["amt_to_allocate"];
                            dr.EndEdit();
                            dr.AcceptChanges();

                            dr = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                            if (dr != null)
                            {
                                dr.BeginEdit();
                                dr["AR_PWP_STATUS_ID"] = drLink["ar_pwp_status_id"];
                                dr.EndEdit();
                                dr.AcceptChanges();
                            }
                        }
                    }
                }
            }
        }

        private void gvPWP_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void riLevy_QueryPopUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /*
         
         private void ucMPOR_AmountUpdated(double Amount)
		{
			b_DontFire = true;
			double PST = 0;
			DataRow dr = gvHeader.GetDataRow( gvHeader.FocusedRowHandle );
			if( dr != null )
			{
				object oAP_INV_HEADER_ID = dr["AP_INV_HEADER_ID"];
				object oPO_ID = dr["PO_ID"];
				AutoUpdate = true;
				gvHeader.FocusedColumn = colPURCH_AMT;
				gvHeader.ShowEditor();
				object oEditor = gvHeader.ActiveEditor;
				if( oEditor != null )
				{					
					string sExec = "exec sp_APCreateGLAlloc "+oAP_INV_HEADER_ID+", "+oPO_ID+", 'T', 'F',  '"+Connection.MLUser+"', 'M'";
					object oPST = ExecuteScalar( sExec, TR_Conn );					
					if( oPST != null && oPST != DBNull.Value )
						PST = Convert.ToDouble( oPST );

                    dr["PURCH_AMT"] = Math.Round(Amount + PST, 2, MidpointRounding.AwayFromZero);								
					SetGSTInvAmount( Amount, PST );
					dr["BALANCE"] = dr["INV_AMOUNT"];

                    gcHeader.EmbeddedNavigator.Buttons.DoClick(gcHeader.EmbeddedNavigator.Buttons.EndEdit);
					SaveHeader();
                    CalculateRemaining();
				}
				AutoUpdate = false;
			}
			b_DontFire = false;
		}
         
         */

        private void riLevy_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
                if (oAP_INV_HEADER_ID == null || oAP_INV_HEADER_ID == DBNull.Value)
                    oAP_INV_HEADER_ID = -1;

                int iAP_INV_HEADER_ID = Convert.ToInt32(oAP_INV_HEADER_ID);
                if (iAP_INV_HEADER_ID != -1)
                {
                    AP_Levy.frmLevySelect fLevy = new AP_Levy.frmLevySelect(Connection, DevXMgr, iAP_INV_HEADER_ID);
                    fLevy.PurchaserRemit += new AP_Levy.frmLevySelect.Delegate_PurchaserRemit(fLevy_PurchaserRemit);
                    if (fLevy.ShowDialog() == DialogResult.OK)
                    {
                        SetHdrLevy(true);

                        DeleteDetailWithLevies(iAP_INV_HEADER_ID);

                        string sSQL = @"exec AP_LevyDetailCreate '" + Connection.MLUser + @"', " + iAP_INV_HEADER_ID;
                        object oTotal = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                        if (oTotal == null || oTotal == DBNull.Value)
                            oTotal = 0;

                        SetHdrLevyTotal(Convert.ToDouble(oTotal));

                        LevyMatch.LoadInvoice(iAP_INV_HEADER_ID);

                        gvHeader.FocusedColumn = colGST_AMT;
                        gvHeader.FocusedColumn = colLevy;

                        RefreshDetail(iAP_INV_HEADER_ID);
                    }
                }
                else
                {
                    Popup.ShowPopup("Please save the invoice header first.");
                }
            }
            else if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                object oAP_INV_HEADER_ID = gvHeader.GetFocusedRowCellValue("AP_INV_HEADER_ID");
                if (oAP_INV_HEADER_ID == null || oAP_INV_HEADER_ID == DBNull.Value)
                    oAP_INV_HEADER_ID = -1;

                int iAP_INV_HEADER_ID = Convert.ToInt32(oAP_INV_HEADER_ID);
                if (iAP_INV_HEADER_ID != -1)
                {
                    if (Popup.ShowPopup("Are you sure you want to remove the matched levies?", WS_Popups.frmPopup.PopupType.OK_Cancel)
                        == frmPopup.PopupResult.OK)
                    {
                        DeleteDetailWithLevies(iAP_INV_HEADER_ID);

                        // Reset Hdr Values
                        ClearHdrTotals();
                        SetHdrLevy(false);
                        LoadRORemit();

                        gvHeader.FocusedColumn = colGST_AMT;
                        gvHeader.FocusedColumn = colLevy;

                        // Refresh Detail
                        RefreshDetail(iAP_INV_HEADER_ID);

                        // Refresh Levy Match
                        LevyMatch.LoadInvoice(iAP_INV_HEADER_ID);

                        gvHeader_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gvHeader.FocusedRowHandle, gvHeader.FocusedRowHandle));
                    }
                }
            }
        }

        private void DeleteDetailWithLevies(int AP_INV_HEADER_ID)
        {
            string sSQL = @"
                update l
                set l.Matched=0
                from PROJ_LOT_AGREEMENT_LEVY l
                join ap_gl_alloc a on a.proj_lot_agreement_levy_id=l.id
                where a.ap_inv_header_id=" + AP_INV_HEADER_ID + @"

                delete from ap_gl_alloc where ap_inv_header_id=" + AP_INV_HEADER_ID;
            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
        }

        private void RefreshDetail(int AP_INV_HEADER_ID)
        {
            dsInvDetail1.Clear();
            daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = AP_INV_HEADER_ID;
            daInvDetail.Fill(dsInvDetail1);
        }

        private void ClearHdrTotals()
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["INV_AMOUNT"] = 0;
                dr["BALANCE"] = 0;
                dr["DISCOUNT_AMOUNT"] = 0;
                dr["PURCH_AMT"] = 0;
                dr["GST_AMT"] = 0;
                dr.EndEdit();
                daInvHeader.Update(dsInvHeader1);
            }
        }

        private void SetHdrLevyTotal(double Total)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["INV_AMOUNT"] = Total;
                dr["BALANCE"] = Total;
                dr["GST_AMT"] = 0;
                dr["PURCH_AMT"] = Total;
                dr["IS_BALANCED"] = "T";
                dr.EndEdit();
                daInvHeader.Update(dsInvHeader1);
            }
        }

        private void SetHdrLevy(bool Set)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["Levy"] = Set;
                dr.EndEdit();
                daInvHeader.Update(dsInvHeader1);
            }
        }

        private void fLevy_PurchaserRemit(AP_Levy.Purchaser purchaser)
        {
            DataRow dr = gvHeader.GetDataRow(gvHeader.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                txtRName.EditValue = purchaser.Name;
                dr["SUPP_NAME"] = purchaser.Name;
                txtRAddr1.EditValue = purchaser.Addr1;
                dr["REMITADD1"] = purchaser.Addr1;
                txtRAddr2.EditValue = purchaser.Addr2;
                dr["REMITADD2"] = purchaser.Addr2;                
                txtRAddr3.EditValue = purchaser.Addr3;
                dr["REMITADD3"] = purchaser.Addr3;
                txtRCity.EditValue = purchaser.City;
                dr["REMITCITY"] = purchaser.City;
                txtRState.EditValue = purchaser.Prov;
                dr["REMITSTATE"] = purchaser.Prov;
                txtRZip.EditValue = purchaser.Postal;
                dr["REMITZIP"] = purchaser.Postal;
                dr.EndEdit();
                daInvHeader.Update(dsInvHeader1);
            }
        }

        private void riManualChkNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            object oVal = e.NewValue;
            if (oVal == null || oVal == DBNull.Value)
                oVal = 0;
            int iVal = Convert.ToInt32(oVal);
            if (iVal < 0)
                e.Cancel = true;
            else if (iVal > 999999999)
                e.Cancel = true;
        }

        private void gvHeader_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if( e.IsSetData )
            {
                gvHeader.SetFocusedRowCellValue("SUPPLIER", e.Value);
            }
        }

        private void hlMultiCBEntry_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            DataRow dr = gvHeader.GetFocusedDataRow();
            if (dr != null)
            {
                // Check that PO is not set on AP header
                object oPO_ID = dr["PO_ID"];
                if (oPO_ID == null || oPO_ID == DBNull.Value)
                    oPO_ID = -1;
                if( Convert.ToInt32(oPO_ID) != -1)
                {
                    Popup.ShowPopup("PO matching currently exists on the invoice header, unable to create multi chargeback for the invoice.");
                    return;
                }

                object oPurchaseAmt = dr["PURCH_AMT"];
                if (oPurchaseAmt == null || oPurchaseAmt == DBNull.Value)
                    oPurchaseAmt = 0;
                ChargeBackPicker.frmMultiChargeback fMCB = new ChargeBackPicker.frmMultiChargeback(Connection, DevXMgr, Current_AP_INV_ID, Convert.ToDecimal(oPurchaseAmt));
                if( fMCB.ShowDialog() == DialogResult.OK )
                {
                    dsInvDetail1.Clear();                    
                    daInvDetail.SelectCommand.Parameters["@ap_inv_header_id"].Value = Current_AP_INV_ID;
                    daInvDetail.Fill(dsInvDetail1);
                    CalculateRemaining();
                }
            }
        }
    }
}

