using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlertViews.PO
{
    public partial class ucWebPODetails : UserControl
    {
        #region Variables and Declarations

        private HMConnection.HMCon _HMCon;
        private TUC_HMDevXManager.TUC_HMDevXManager _TUC;

        private bool _isSubmissionDetailsTabVisible = true;
        private int _ws_pcpo_id;
        private System.Data.DataTable _dtWS_PCPO_ActiveLevel;

        private ReflexChat.ucChat _ucChat;
        private int _Chat_FieldLink_ID;

        private DM_CentralizedFSManager.ucFileManager _ucFileManager;
        private WO_CentralizedFSManager.ucFileManager CFS_FileMgr;
        private bool _USE_DM = false;
        private string RelType = "WSPO";

        private SubmissionHistory.ucHistoryView _ucEventHistoryView;

        public delegate void del_WS_PCPO_ID_PropertyValueChanged();
        public event del_WS_PCPO_ID_PropertyValueChanged WS_PCPO_ID_PropertyValueChanged;

        #endregion

        #region Public Properties

        public HMConnection.HMCon HMConnection
        {
            get { return _HMCon; }
            set {
                _HMCon = value;
                this.TRCon.ConnectionString = _HMCon.TRConnection;
                InitializeFileManager();
            }
        }

        public bool IsWorkFlow
        {
            get { return _isSubmissionDetailsTabVisible; }
            set { 
                _isSubmissionDetailsTabVisible = value;
                xtabPageSubmissionDetails.PageVisible = _isSubmissionDetailsTabVisible;
            }
        }

        public int WS_PCPO_ID
        {
            get { return _ws_pcpo_id; }
            set {
                _ws_pcpo_id = value;

                if (WS_PCPO_ID_PropertyValueChanged != null)
                {
                    ClearScreenControls();

                    // fires an event each time _ws_pcpo_id changes
                    WS_PCPO_ID_PropertyValueChanged();
                }
            }
        }

        #endregion
        
        #region Constructors and Loading

        public ucWebPODetails()
        {
            InitializeComponent();
        }

        public ucWebPODetails(HMConnection.HMCon hmCon, TUC_HMDevXManager.TUC_HMDevXManager tuc)
        {
            InitializeComponent();
            _HMCon = hmCon;
            _TUC = tuc;


            this.TRCon.ConnectionString = hmCon.TRConnection;

            this.daWF_Route.SelectCommand.CommandText =
                daWF_Route.SelectCommand.CommandText.Replace("web_strike_test", _HMCon.WebDB);
          
            this.WS_PCPO_ID_PropertyValueChanged += new del_WS_PCPO_ID_PropertyValueChanged(ucWebPODetails_WS_PCPO_ID_PropertyValueChanged);
                        
            InitializeChat();
            InitializeFileManager();
            InitializeEventHistoryView();

            _TUC.FormInit(gcDetails);
            _TUC.FormInit(gcSubmissionDetails_Grid);
        }

       
        private void ucWebPODetails_Load(object sender, EventArgs e)
        {
            _TUC.FormInit(this);      
        }

     
        #endregion
                
        #region PO Header

        #region PO Information

        #region Loading PO Header Information

        public void Refresh_WS_PCPO_Hdr(int ws_pcpo_id)
        {
            dsWS_PCPO_Hdr.COUNTRIES.Clear();
            daWS_PCPO_Hdr.SelectCommand.Parameters["@ws_pcpo_id"].Value = ws_pcpo_id;
            daWS_PCPO_Hdr.Fill(dsWS_PCPO_Hdr.COUNTRIES);
        }

        private void ucWebPODetails_WS_PCPO_ID_PropertyValueChanged()
        {            
            // binds the WS_PCP_Hdr to PO_Header fields
            SetDataBinding_POHeader();
            // refresh the WS_PCP_Hdr datasource
            Refresh_WS_PCPO_Hdr(_ws_pcpo_id);            

            int pri_id = -1;
            if(isPOHeaderRetrieved())
                pri_id = Convert.ToInt32(dsWS_PCPO_Hdr.Tables[0].Rows[0]["pri_id"]);
            
            // load PO details
            Refresh_WS_PCPO_Det(_ws_pcpo_id);
            
            // load sbmission details grid
            Refresh_WF_Route(_ws_pcpo_id);
            
            // load chat
            Refresh_ChatThread(_ws_pcpo_id);
            
            // load attachments
            Refresh_FileManager(_ws_pcpo_id);

            // load event history
            Refresh_EventHistoryView(_ws_pcpo_id);
        }

        private void SetActiveLevelDescription(System.Data.DataRow row, string colFieldName, DevExpress.XtraLayout.LayoutControlItem lci)
        {
            object levelDesc = CastDBNullToNull(row[colFieldName]);
            if (levelDesc != null && levelDesc.ToString().Trim() != "")
                lci.Text = Convert.ToString(levelDesc);
            else
                lci.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private object CastDBNullToNull(object o)
        {
            if (Convert.IsDBNull(o))
                return null;
            else
                return o;
        }

        private void SetDataBinding_POHeader()
        {
            // Field_PO
            SetControlDataBinding(ceHeader_FieldPO, "EditValue", dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.field_poColumn.ColumnName);
                        
            // PO_No
            SetTextEditDataBinding_POHeader(teHeader_PONum, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_noColumn.ColumnName);            

            // PO Date
            SetTextEditDataBinding_POHeader(teHeader_PODate, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_dateColumn.ColumnName);

            // PO Required Date
            SetTextEditDataBinding_POHeader(teHeader_PORequiredDate, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.required_dateColumn.ColumnName);

            // POName
            SetTextEditDataBinding_POHeader(teHeader_POName, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.PONameColumn.ColumnName);

            // PO_Addr1
            SetTextEditDataBinding_POHeader(teHeader_POAddressOne, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_addr1Column.ColumnName);

            // PO_Addr2
            SetTextEditDataBinding_POHeader(teHeader_POAddressTwo, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_addr2Column.ColumnName);

            // PO_City
            SetTextEditDataBinding_POHeader(teHeader_POCity, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_cityColumn.ColumnName);

            // PO_State
            SetTextEditDataBinding_POHeader(teHeader_POProvince, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_stateColumn.ColumnName);

            // PO_Zip
            SetTextEditDataBinding_POHeader(teHeader_POPostalCode, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.po_zipColumn.ColumnName);

            // Country
            SetTextEditDataBinding_POHeader(teHeader_POCountry, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.CountryColumn.ColumnName);

            // Supplier
            SetTextEditDataBinding_POHeader(teHeader_Supplier, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.supplierColumn.ColumnName);

            // POType
            SetTextEditDataBinding_POHeader(teHeader_POType, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.POTypeColumn.ColumnName);

            // POSupplyType
            SetTextEditDataBinding_POHeader(teHeader_POSupplyType, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.POSupplyTypeColumn.ColumnName);

            // submission notes
            SetControlDataBinding(meSubmissionDetails_Notes, "Text", dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.submission_commentsColumn.ColumnName);

            // Supplier Name
            SetTextEditDataBinding_POHeader(teHeader_SupplierName, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.SupplierNameColumn.ColumnName);

            // Purchase Amount
            SetTextEditDataBinding_POHeader(teHeader_PurchaseAmt, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.PurchaseAmountColumn.ColumnName);

            // Project
            SetTextEditDataBinding_POHeader(teHeader_Project, dsWS_PCPO_Hdr.COUNTRIES, dsWS_PCPO_Hdr.COUNTRIES.ProjectColumn.ColumnName);
        }

        private void SetTextEditDataBinding_POHeader(DevExpress.XtraEditors.TextEdit te, object dataSource, string dataMember)
        {
            te.DataBindings.Clear();
            te.DataBindings.Add("Text", dataSource, dataMember);
        }

        private void SetControlDataBinding(DevExpress.XtraEditors.BaseControl editorControl, string propertyName, object dataSource, string dataMember)
        {
            editorControl.DataBindings.Clear();
            editorControl.DataBindings.Add(propertyName, dataSource, dataMember);
        }

        #endregion

        #endregion

        #region Submission Details

        public void Refresh_WF_Route(int ws_pcpo_id)
        {
            dsWF_Route1.Clear();
            daWF_Route.SelectCommand.Parameters["@ws_pcpo_id"].Value = ws_pcpo_id;
            daWF_Route.Fill(dsWF_Route1);
        }

        #endregion

        #region Chat
        
        private void InitializeChat()
        {
            //Create field link (Done once from constructor when control is first created)
            string cmdText = @"exec sp_WS_ChatFieldLinkGetID 'WS_PCPO_Hdr.ws_pcpo_id', 'Web PO Submission'";
            // creates a unique id and puts into the chat table
            _Chat_FieldLink_ID = Convert.ToInt32(_HMCon.SQLExecutor.ExecuteScalar(cmdText, _HMCon.TRConnection));

            _ucChat = new ReflexChat.ucChat(_HMCon, _TUC, ReflexChat.ChatType.Default);
            _ucChat.Dock = DockStyle.Fill;
            _ucChat.Parent = xtabPageChat;
        }

        public void Refresh_ChatThread(int ws_pcpo_id)
        {
            if (_ucChat != null)
            {
                _ucChat.Enabled = false;
                
                if(ws_pcpo_id >= 0)
                {
                    if (isPOHeaderRetrieved())
                    {
                        _ucChat.Enabled = true;
                        _ucChat.LoadChatThread(_Chat_FieldLink_ID, ws_pcpo_id);
                    }
                }
            }
        }

        #endregion

        #region Attachments

        private void InitializeFileManager()
        {       
            string sSQL = @"select ISNULL((select isnull(USE_DM_ON_PORTAL,0) from system_ctrl), 0)";
            object oUSE_DM = _HMCon.SQLExecutor.ExecuteScalar(sSQL, _HMCon.WebConnection);
            if (oUSE_DM == null || oUSE_DM == DBNull.Value)
                oUSE_DM = false;
            _USE_DM = Convert.ToBoolean(oUSE_DM);
            if (_USE_DM)
            {
                if (_ucFileManager != null)
                    _ucFileManager = null;
                _ucFileManager = new DM_CentralizedFSManager.ucFileManager(_HMCon, _TUC, DM_CentralizedFSManager.DocumentViewerMode.All, true, "F");
                _ucFileManager.DocumentFileLink = GetFileLinks(-1);
                _ucFileManager.ReadOnly = true;
                _ucFileManager.Dock = DockStyle.Fill;
                _ucFileManager.Parent = xtabAttachments;
            }
            else
            {
                if (CFS_FileMgr != null)
                    CFS_FileMgr = null;
                CFS_FileMgr = new WO_CentralizedFSManager.ucFileManager(_HMCon, _TUC,
                        WO_CentralizedFSManager.DocumentViewerMode.All, RelType, -1, true, "Work Flow Approval");
                CFS_FileMgr.Dock = DockStyle.Fill;
                CFS_FileMgr.Parent = xtabAttachments;
            }
        }

        public void Refresh_FileManager(int ws_pcpo_id)
        {   
            if (ws_pcpo_id >= 0)
            {
                if (isPOHeaderRetrieved())
                {
                    if (_USE_DM)
                    {
                        _ucFileManager.ReadOnly = true;
                        _ucFileManager.DocumentFileLink = GetFileLinks(ws_pcpo_id);
                    }
                    else
                    {
                        CFS_FileMgr.RefreshFileList(RelType, ws_pcpo_id, true);
                    }
                }
            }
        }

        private DM_CentralizedFSManager.FileLink[] GetFileLinks(int ws_pcpo_id)
        {
            DM_CentralizedFSManager.FileLink[] fileLinks = new DM_CentralizedFSManager.FileLink[]{                                                               
                    new DM_CentralizedFSManager.FileLink("WS_PCPO_Hdr.ws_pcpo_id", ws_pcpo_id, _HMCon.CompanyID, DM_CentralizedFSManager.FileLink.Database.TR, 0, true)     
            };

            return fileLinks;
        }                                                                                                      

        #endregion

        #region Event History

        private void InitializeEventHistoryView()
        {
            string cmdText = @"EXEC sp_WS_ChatFieldLinkGetID 'WS_PCPO_Hdr.ws_pcpo_id', 'PO Approval' ";
            
            _ucEventHistoryView = new SubmissionHistory.ucHistoryView();
            _ucEventHistoryView.TypeID = 23;
            _ucEventHistoryView.HMConnection = _HMCon;
            _ucEventHistoryView.ChatFieldLink = Convert.ToInt32(_HMCon.SQLExecutor.ExecuteScalar(cmdText, _HMCon.TRConnection));
            _ucEventHistoryView.TUC_DevXMgr = _TUC;
            _ucEventHistoryView.Dock = DockStyle.Fill;
            _ucEventHistoryView.Parent = xtabPageEventHistory;
        }

        public void Refresh_EventHistoryView(int ws_pcpo_id)
        {
            if (_ucEventHistoryView != null)
            {
                _ucEventHistoryView.DetailID = ws_pcpo_id;
                _ucEventHistoryView.LoadHistory();
            }
        }

        #endregion

        #endregion
        
        #region PO Details

        #region Details Grid
        
        public void Refresh_WS_PCPO_Det(int ws_pcpo_id)
        {
            string sSQL = @"select isnull((select ISNULL(contract_po,'F') from WS_PCPO_Hdr where ws_pcpo_id="+ws_pcpo_id+@"), 'F')";
            object oContractPO = _HMCon.SQLExecutor.ExecuteScalar(sSQL, _HMCon.TRConnection);
            if (oContractPO == null || oContractPO == DBNull.Value)
                oContractPO = "F";
            if (oContractPO.Equals("T"))
            {
                colUnitOfMeasure.Visible = false;
                colUnitOfMeasure.OptionsColumn.ShowInCustomizationForm = false;
                colAmount.Visible = false;
                colAmount.OptionsColumn.ShowInCustomizationForm = false;
                colqty_ordered.Caption = "Amount";
            }
            else
            {
                if (!colUnitOfMeasure.Visible)
                {
                    colUnitOfMeasure.VisibleIndex = coldescription.VisibleIndex++;
                    colUnitOfMeasure.OptionsColumn.ShowInCustomizationForm = true;                
                }
                if (!colAmount.Visible)
                {
                    colAmount.VisibleIndex = colqty_ordered.VisibleIndex++;
                    colAmount.OptionsColumn.ShowInCustomizationForm = true;
                }
                colqty_ordered.Caption = "Quantity";
            }

            dsWS_PCPO_Det.Clear();
            daWS_PCPO_Det.SelectCommand.Parameters["@ws_pcpo_id"].Value = ws_pcpo_id;
            daWS_PCPO_Det.Fill(dsWS_PCPO_Det);
        }

        #endregion

        #endregion

        #region Utility Methods

        private void SetTabVisiblity(DevExpress.XtraTab.XtraTabPage page, bool visiblity)
        {
            page.PageVisible = visiblity;
        }

        private void ClearScreenControls()
        {
            if (_USE_DM)
            {
                _ucFileManager.ReferenceID = -1;
                _ucFileManager.RefreshFileList();
                _ucFileManager.ReadOnly = true;
            }
            else
            {
                CFS_FileMgr.RefreshFileList(RelType, -1, true);
            }
            _ucChat.LoadChatThread(-100);
            _ucChat.Enabled = false;
        }

        private bool isPOHeaderRetrieved()
        {
            bool retval = false;

            if (dsWS_PCPO_Hdr.COUNTRIES.Rows.Count > 0)
            {
                retval = true;
            }

            return retval;
        }

        #endregion 
    }
}
