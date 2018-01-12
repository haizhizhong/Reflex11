using System;
using System.Data;
using System.Windows.Forms;

namespace AlertViews.PO
{
    public partial class ucWFPODetail : UserControl
    {
        #region Variables and Declarations

        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;

        private int _WF_Route_ID;
        private int _po_id;

        private DM_CentralizedFSManager.ucFileManager _ucFileManager;
        private WO_CentralizedFSManager.ucFileManager CFS_FileMgr;
        private bool _USE_DM = false;
        private string RelTypePO = "PO";
        private string RelTypePurRec = "PURREC";

        #endregion

        #region Public Properties

        public HMConnection.HMCon HMConnection
        {
            get { return Connection; }
            set
            {
                Connection = value;
                TR_Conn.ConnectionString = Connection.TRConnection;
                InitializeFileManager();
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUCDevXMgr
        {
            get { return DevXMgr; }
            set
            {
                DevXMgr = value;
            }
        }

        #endregion

        #region Constructor

        public ucWFPODetail()
        {
            InitializeComponent();
        }

        public ucWFPODetail(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;

            InitializeComponent();

            TR_Conn.ConnectionString = Connection.TRConnection;

            this.daWF_Route.SelectCommand.CommandText =
                daWF_Route.SelectCommand.CommandText.Replace("web_strike_test", Connection.WebDB);

            InitializeFileManager();
            SetupStatusDT();
        }

        private void SetupStatusDT()
        {
            DataTable dtStatus = new DataTable("dtStatus");
            dtStatus.Columns.Add("ID", typeof(string));
            dtStatus.Columns.Add("Desc", typeof(string));

            dtStatus.Rows.Add(new object[] { "A", "Added" });
            dtStatus.Rows.Add(new object[] { "C", "Changed" });
            dtStatus.Rows.Add(new object[] { "D", "Deleted" });

            riRevType.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", 150, "Approval"));
            riRevType.DataSource = dtStatus;
            riRevType.DisplayMember = "Desc";
            riRevType.ValueMember = "ID";
        }

        private void ucWFPODetail_Load(object sender, EventArgs e)
        {
            if( DevXMgr != null )
                DevXMgr.FormInit(this);
        }

        public void LoadRoutingDetail(int WF_Route_ID)
        {
            _WF_Route_ID = WF_Route_ID;

            string sSQL = @"select Link_ID from WF_Route where WF_Route_ID = " + _WF_Route_ID;
            object oLink_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oLink_ID == null || oLink_ID == DBNull.Value)
                oLink_ID = -1;

            _po_id = Convert.ToInt32(oLink_ID);

            ClearScreenControls();
            RefreshucWFPODetails();
        }

        #endregion 

        #region PO Header

        #region PO Information

        #region Loading PO Header Information

        private void RefreshucWFPODetails()
        {
            // refresh the PO_Hdr datasource
            Refresh_PO_Hdr();

            string sSQL = @"select isnull(notes,'') from WF_Route where WF_Route_ID=" + _WF_Route_ID;
            object oNotes = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oNotes == null || oNotes == DBNull.Value)
                oNotes = "";
            meSubmissionDetails_Notes.Text = oNotes.ToString();
            
            // load PO details
            Refresh_PO_Det();

            // load sbmission details grid
            Refresh_WF_Route();

            // load attachments
            Refresh_FileManager();
        }

        public void Refresh_PO_Hdr()
        {
            string sSQL = @"select p.po_id, p.pri_num, p.po, p.order_date, p.org_due_date, p.supplier, t.DESCRIPTION AS POType, st.Description AS POSupplyType, 
	            s.name, p.po_add1, p.po_add2, p.po_city, p.po_state, p.po_zip, c.DESCRIPTION AS Country,
	            cast(ph.pri_code as varchar(15)) + ' - '+ph.pri_name [Project], 
	            isnull((select SUM(ISNULL(d.extension,0)) from PO_DETAIL d where d.po_id=p.po_id),0) [PurchaseAmount],
	            ISNULL(p.HOLD_AMT,0) [HOLD_AMT], p.ContractPO_Start_Date, p.ContractPO_End_Date, cst.SubType_Description
            from PO_HEADER p
            left outer join SUPPLIER_MASTER s on s.SUPPLIER=p.supplier
            left outer join COUNTRIES AS c ON c.COUNTRY_ID = p.po_country_id 
            left outer join PO_TYPE AS t ON t.TYPE = p.po_type 
            left outer join PO_SupplyType AS st ON st.PO_SupplyType_ID = p.po_supplytype_id 
            left outer join PROJ_HEADER ph on ph.pri_id=p.pri_num
            left outer join PO_ContractPO_SubType cst on cst.id=p.ContractPO_Subtype_ID
            where p.po_id=" + _po_id;
             
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        teHeader_PONum.EditValue = dr["po"];
                        teHeader_POType.EditValue = dr["POType"];
                        teHeader_PODate.EditValue = dr["order_date"];
                        teHeader_PORequiredDate.EditValue = dr["org_due_date"];
                        teHeader_Supplier.EditValue = dr["supplier"];
                        teHeader_SupplierName.EditValue = dr["name"];
                        teHeader_PurchaseAmt.EditValue = dr["PurchaseAmount"];
                        teHeader_Project.EditValue = dr["Project"];
                        teHeader_POSupplyType.EditValue = dr["POSupplyType"];
                        teHeader_POAddressOne.EditValue = dr["po_add1"];
                        teHeader_POAddressTwo.EditValue = dr["po_add2"];
                        teHeader_POCity.EditValue = dr["po_city"];
                        teHeader_POProvince.EditValue = dr["po_state"];
                        teHeader_POPostalCode.EditValue = dr["po_zip"];
                        teHeader_POCountry.EditValue = dr["Country"];
                        txtHBAmt.EditValue = dr["HOLD_AMT"];
                        txtSubtype.EditValue = dr["SubType_Description"];
                        deConStart.EditValue = dr["ContractPO_Start_Date"];
                        deConEnd.EditValue = dr["ContractPO_End_Date"];
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Submission Details

        public void Refresh_WF_Route()
        {
            dsWF_Route.Clear();
            daWF_Route.SelectCommand.Parameters["@WF_Route_ID"].Value = _WF_Route_ID;
            daWF_Route.Fill(dsWF_Route);
        }

        #endregion

        #region Attachments

        private void InitializeFileManager()
        {
            string sSQL = @"select ISNULL((select isnull(USE_DM_ON_PORTAL,0) from system_ctrl), 0)";
            object oUSE_DM = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.WebConnection);
            if (oUSE_DM == null || oUSE_DM == DBNull.Value)
                oUSE_DM = false;
            _USE_DM = Convert.ToBoolean(oUSE_DM);
            if (_USE_DM)
            {
                if (_ucFileManager != null)
                {
                    try { _ucFileManager.Dispose(); }
                    catch { }
                    _ucFileManager = null;
                }
                _ucFileManager = new DM_CentralizedFSManager.ucFileManager(Connection, DevXMgr, DM_CentralizedFSManager.DocumentViewerMode.All, true, "F");
                _ucFileManager.DocumentFileLink = GetFileLinks(-1);
                _ucFileManager.ReadOnly = true;
                _ucFileManager.Dock = DockStyle.Fill;
                _ucFileManager.Parent = xtabAttachments;
                _ucFileManager.BringToFront();
            }
            else
            {
                if (CFS_FileMgr != null)
                {
                    try { CFS_FileMgr.Dispose(); }
                    catch { }
                    CFS_FileMgr = null;
                }
                CFS_FileMgr = new WO_CentralizedFSManager.ucFileManager(Connection, DevXMgr,
                        WO_CentralizedFSManager.DocumentViewerMode.All, RelTypePO, -1, false, "PO");
                CFS_FileMgr.Dock = DockStyle.Fill;
                CFS_FileMgr.Parent = xtabAttachments;
                CFS_FileMgr.BringToFront();
            }
        }

        public void Refresh_FileManager()
        {
            if (_po_id >= 0)
            {
                if (_USE_DM)
                {
                    _ucFileManager.ReadOnly = true;
                    _ucFileManager.DocumentFileLink = GetFileLinks(_po_id);
                }
                else
                {
                    string sSQL = @"select WF_ApprovalPoint_ID from WF_Route where WF_Route_ID =" + _WF_Route_ID;
                    object oWF_ApprovalPoint_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                    if (oWF_ApprovalPoint_ID == null || oWF_ApprovalPoint_ID == DBNull.Value)
                        oWF_ApprovalPoint_ID = 7;

                    if( Convert.ToInt32(oWF_ApprovalPoint_ID) == 6 )
                        CFS_FileMgr.RefreshFileList(RelTypePurRec, _po_id, false);
                    else
                        CFS_FileMgr.RefreshFileList(RelTypePO, _po_id, false);
                }
            }
        }

        private DM_CentralizedFSManager.FileLink[] GetFileLinks(int po_id)
        {
            DM_CentralizedFSManager.FileLink[] fileLinks = new DM_CentralizedFSManager.FileLink[]{                                                               
                    new DM_CentralizedFSManager.FileLink("PO_HEADER.po_id", po_id, Connection.CompanyID, DM_CentralizedFSManager.FileLink.Database.TR, 0, true)     
            };

            return fileLinks;
        }

        #endregion
        
        #endregion

        #region PO Details

        #region Details Grid

        public void Refresh_PO_Det()
        {
            LoadPODetails();            
        }

        private void LoadPODetails()
        {
            string sSQL = @"select isnull((select ISNULL(contract_po,'F') from PO_HEADER where po_id=" + _po_id + @"), 'F')";
            object oContractPO = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
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

            dsRevHist1.Clear();
            dsPO_Det1.Clear();
            daPO_Det.SelectCommand.Parameters["@po_id"].Value = _po_id;
            daPO_Det.Fill(dsPO_Det1);
            gvDetails_FocusedRowChanged(null, null);
        }

        private void gvDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int PO_DETAIL_ID = 0;
            DataRow dr = gvDetails.GetDataRow(gvDetails.FocusedRowHandle);
            if (dr != null)
            {
                if (dr["PO_DETAIL_ID"] != null && dr["PO_DETAIL_ID"] != DBNull.Value)
                    PO_DETAIL_ID = Convert.ToInt32(dr["PO_DETAIL_ID"]);
            }
            dsRevHist1.Clear();
            daRevHist.SelectCommand.Parameters["@po_detail_id"].Value = PO_DETAIL_ID;
            daRevHist.Fill(dsRevHist1);
        }



        #endregion

        #endregion

        #region Utility Methods

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
                CFS_FileMgr.RefreshFileList(RelTypePO, -1, false);
            }
        }

        #endregion

        
    }
}
