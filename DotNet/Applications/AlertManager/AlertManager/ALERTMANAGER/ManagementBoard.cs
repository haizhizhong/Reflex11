using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Helpers;
using HMConnection;
using WS_Popups;

namespace AlertManager
{
    public partial class ManagementBoard : DevExpress.XtraEditors.XtraUserControl
    {
        #region ucHeader Class Variables

        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        public HMCon Connection;
        private frmPopup Popup;
        private int _NewAlerts = 0;

        string AlertSelect, AlertSearchSelect;

        private const int CONST_AP_CONTRACTOR_PAYMENT_RANGE_START_ALERT_POINT = 2;
        private const int CONST_AP_CONTRACTOR_PAYMENT_RANGE_END_ALERT_POINT = 13;
        private const int CONST_LOT_HOLD_ALERT_POINT = 14;
        private const int CONST_HOLD_RENEWAL_ALERT_POINT = 23;
        private const int CONST_PO_REC_NOTIFY_RANGE_START_ALERT_POINT = 24;
        private const int CONST_PO_REC_NOTIFY_RANGE_END_ALERT_POINT = 35;
        private const int CONST_WO_WOO_AUTO_BACKFLUSH = 36;
        private const int CONST_WO_WOS_AUTO_BACKFLUSH = 37;
        private const int CONST_PO_BUYER = 38;
        private const int CONST_LAND_COLLECTION_REMINDER = 40;
        private const int CONST_ALERT_Builder_Next_Lot_Draw_Exclusion = 41;
        private const int CONST_ALERT_Past_Due_Payment_Reminder = 42;
        private const int CONST_ALERT_Late_Payment_Reminder = 43;
        private const int CONST_ALERT_Title_Transfer_Confirmation_Reminder = 44;
        private const int CONST_ALERT_Payout_Extension_Reminder = 45;
        private const int CONST_ALERT_Probationary_Builders_Reminder = 46;
        private const int CONST_ALERT_Lease_Expiry_Reminder_COMM = 47;
        private const int CONST_ALERT_Lease_Expiry_Reminder_RES = 48;        
        private const int CONST_ALERT_WEB_PO_Submission = 60;
        private const int CONST_ALERT_WEB_Subcontractor_Payment_Request = 63;
        private const int CONST_ALERT_REFLEX_CHAT = 65;
        private const int CONST_ALERT_PO_SUBTYPE_SIGNATURE_NOTICE = 75;
        private const int CONST_ALERT_AGREEMENT_INTEREST_FREE_PERIOD = 76;
        private const int CONST_ALERT_MASTER_AGREEMENT_INTEREST_FREE_PERIOD = 77;
        private const int CONST_ALERT_LAND_REPAYMENT_DUE_ALERT_POINT = 78;
        private const int CONST_ALERT_RENTAL_STATUS_CHANGE = 79;
        private const int CONST_ALERT_RENTAL_START = 80;
        private const int CONST_ALERT_RENTAL_END = 81;
        private const int CONST_ALERT_AUTO_BFS_CREATE_FAIL = 88;
        private const int CONST_ALERT_Employee_Certificate_Expiry = 89;
        private const int CONST_ALERT_FA_Preventative_Maintenance  = 90;

        private AlertViews.Costing.avContractorPayment avConPayment;
        private AlertViews.Costing.LotExpiry.avLotHoldExpiry avHoldExpiry;
        private AlertViews.Costing.POReceiving.ucPORecNotify avPORecNotify;
        private AlertViews.Costing.LandReminders.avLandReminder avReminder;
        private AlertViews.WorkOrders.BackFlush.ucBackFlush avBackFlush;
        private AlertViews.PO.ucPODetails avPODetails;
        private AlertViews.Costing.TenantReminders.avLeaseExpiryReminder avLeaseReminder;
        private AlertViews.PO.avWebPODetails avWebPODetails;
        private AlertViews.Costing.avWebSubConPayReq avSubConPayReq;
        private AlertViews.avChat avReflexChat;
        private AlertViews.PO.avPODocDetail avPODocDet;
        private AlertViews.Costing.LandReminders.avMasterAgreementReminder avMasterReminder;
        private AlertViews.Costing.LandReminders.avLandPaymentDue avLandPaymentDue;
        private AlertViews.Rental.ucRentalRequestStatusChg avRentalStatusChg;
        private AlertViews.Rental.ucRentalStart avRentalStart;
        private AlertViews.Rental.ucRentalEnd avRentalEnd;
        private AlertViews.WorkOrders.Automation.avAutomation avBFSAutoFail;
        private AlertViews.Human_Resources.avEmployeeCertificateExpiry avEmpCerCertificateExpiry;
        private AlertViews.FixedAssets.avFAPreventativeMaintenance avFAPM;


        #endregion

        public ManagementBoard(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();    
        }

        #region Setups

        private void RunSetups()
        {
            pBack.BringToFront();

            dsAlert1.Alert.COMPLETED_TFColumn.ReadOnly = false;

            alertView1.HMConnection = Connection;
            alertView1.TUC_DevXMgr = DevXMgr;
            alertView1.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
            alertView1.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
            alertView1.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
            alertView1.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);

            WEB_Conn.ConnectionString = Connection.WebConnection;
            TR_Conn.ConnectionString = Connection.TRConnection;
            
            daContacts.Fill(dsContacts1);
            daAlertType.SelectCommand.CommandText = daAlertType.SelectCommand.CommandText.Replace("web_strike_test", Connection.WebDB);
            daAlertType.Fill(dsAlertType1);
            
            daAlert.SelectCommand.Parameters["@contactid"].Value = Connection.ContactID;
            daStakeholders.SelectCommand.Parameters["@contactid"].Value = Connection.ContactID;
            
            SetupAlertDT();
            
            SetupSearchSelect();
        }

        private void alertView1_AlertCompleted()
        {
            DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
            if (dr != null)
            {
                dr.BeginEdit();
                dr["COMPLETED_TF"] = "T";
                dr.EndEdit();
            }
        }

        private void SetupSearchSelect()
        {
            AlertSelect = @"select a.ID, a.TYPE, a.AAP_ID, a.APP_MOD_ID, a.SUBJECT, a.MODULE, a.RECEIVED, a.NOTES, s.VIEWED 
                from Alert a 
                join AlertStakeholder s on a.ID=s.Alert_id 
                where contact_id=@contactid and isnull(s.deleted,'F')='F' and isnull(a.Complete_TF,'F') = 'T'";

            AlertSearchSelect = @"select a.ID, a.TYPE, a.AAP_ID, a.APP_MOD_ID, a.SUBJECT, a.MODULE, a.RECEIVED, a.NOTES, s.VIEWED 
                from Alert a 
                join AlertStakeholder s on a.ID=s.Alert_id 
                join working_alert_search w on w.alert_id=a.id and w.username='" + Connection.MLUser + @"' 
                where contact_id=@contactid and isnull(s.deleted,'F')='F' and isnull(a.Complete_TF,'F') = 'T'";
        }
                       
        private void alertView1_AlertUnseen()
        {
            DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
            if (dr != null)
            {
                dr["VIEWED"] = "F";
            }
        }

        private void alertView1_AlertSeen()
        {
            DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
            if( dr != null )
            {
                dr["VIEWED"] = "T";
            }
        }

        private void alertView1_AlertDeleted()
        {
            gvAlerts.DeleteRow(gvAlerts.FocusedRowHandle);
            gvAlerts_FocusedRowChanged(null, null);
            gAlert.Text = "Alerts (" + GetAlertCount() + ")";
        }

        private int GetAlertCount()
        {
            int Count = 0;
            string sSelect = @"select count(*) 
                from Alert a 
                join AlertStakeholder s on a.ID=s.Alert_id 
                where isnull(s.deleted,'F')='F' and isnull(a.Complete_TF,'F') = 'T' and s.contact_id=" + Connection.ContactID;

            Count = Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection));
            return Count;
        }

        private void SetupAlertDT()
        {
            daAlert.Fill(dsAlert1);

            gvAlerts.ExpandAllGroups();
            //gvAlerts.SetRowExpanded(-1, true);
            //gvAlerts.SetRowExpanded(-2, true);

            gAlert.Text = "Alerts (" + GetAlertCount() + ")";
        }

        #endregion

        #region Alert Context-Menu Events

        private void miDate_Click(object sender, EventArgs e)
        {
            if (miDate.Checked)
            {
                miModule.Checked = false;
                miType.Checked = false;
                lblArrange.Text = "Arrange By:";
                GroupByColumn("Received");              
            }
            else
            {
                miDate.Checked = true;
            }
        }

        private void miModule_Click(object sender, EventArgs e)
        {
            if (miModule.Checked)
            {
                miDate.Checked = false;
                miType.Checked = false;
                lblArrange.Text = "Arrange By: Module";
                GroupByColumn("Module");
            }
            else
            {
                miDate.Checked = true;
                lblArrange.Text = "Arrange By:";
                GroupByColumn("Received");
            }
        }

        private void miType_Click(object sender, EventArgs e)
        {
            if (miType.Checked)
            {
                miDate.Checked = false;
                miModule.Checked = false;
                lblArrange.Text = "Arrange By: Type";
                GroupByColumn("Type");
            }
            else
            {
                miDate.Checked = true;
                lblArrange.Text = "Arrange By:";
                GroupByColumn("Received");
            }
        }


        private void GroupByColumn( string ColumnName )
        {
            gvAlerts.ClearGrouping();
            gvAlerts.Columns[ColumnName].GroupIndex = 0;
            if( ColumnName.EndsWith("Received") )
                gvAlerts.Columns[ColumnName].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gvAlerts.CollapseAllGroups();
            gvAlerts.ExpandAllGroups();
            //gvAlerts.SetRowExpanded(-1, true);
            //gvAlerts.SetRowExpanded(-2, true);
        }

        private void miExpand_Click(object sender, EventArgs e)
        {
            gvAlerts.ExpandAllGroups();
        }

        private void miCollapse_Click(object sender, EventArgs e)
        {
            gvAlerts.CollapseAllGroups();
        }

        private void gvAlerts_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            e.Allow = false;
        }

        #endregion
                
        #region Load Alert

        private void gvAlerts_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvAlerts.FocusedRowHandle >= 0)
            {
                pBack.SendToBack();
                int AlertID = -1;
                int AAP_ID = -1;
                int DETAIL_ID = -1;
                DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
                if (dr != null)
                {
                    object oAlertID = dr["ID"];
                    if (oAlertID != null && oAlertID != DBNull.Value)
                    {
                        AlertID = Convert.ToInt32(oAlertID);
                    }
                    object oAAP_ID = dr["AAP_ID"];
                    if (oAAP_ID != null && oAAP_ID != DBNull.Value)
                    {
                        AAP_ID = Convert.ToInt32(oAAP_ID);
                    }
                    object oDETAIL_ID = dr["DETAIL_ID"];
                    if (oDETAIL_ID != null && oDETAIL_ID != DBNull.Value)
                    {
                        DETAIL_ID = Convert.ToInt32(oDETAIL_ID);
                    }
                }
                LoadAlertDetails(AlertID, AAP_ID, DETAIL_ID);
            }
            else
            {               
                pBack.BringToFront();
                ClearAlertDetails();
            }
        }

        private void LoadAlertDetails( int AlertID, int APP_ID, int DETAIL_ID )
        {
        dsStakeholders1.Clear();
            daStakeholders.SelectCommand.Parameters["@alertid"].Value = AlertID;
            daStakeholders.Fill(dsStakeholders1);

            string Subject = "";
            string Module = "";
            DateTime Received = DateTime.Now;
            string Notes = "";

            DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
            if (dr != null)
            {
                object oSubject = dr["SUBJECT"];
                if (oSubject != null && oSubject != DBNull.Value)
                {
                    Subject = oSubject.ToString();
                }

                object oModule = dr["MODULE"];
                if (oModule != null && oModule != DBNull.Value)
                {
                    Module = oModule.ToString();
                }

                object oReceived = dr["RECEIVED"];
                if (oReceived != null && oReceived != DBNull.Value)
                {
                    Received = Convert.ToDateTime( oReceived );
                }

                object oNotes = dr["NOTES"];
                if (oNotes != null && oNotes != DBNull.Value)
                {
                    Notes = oNotes.ToString();
                }
            }

            AlertView AlertViewMain;
            if (APP_ID >= CONST_AP_CONTRACTOR_PAYMENT_RANGE_START_ALERT_POINT && APP_ID <= CONST_AP_CONTRACTOR_PAYMENT_RANGE_END_ALERT_POINT)
            {
                if (avConPayment == null)
                {
                    avConPayment = new AlertViews.Costing.avContractorPayment();
                    avConPayment.HMConnection = Connection;
                    avConPayment.TUC_DevXMgr = DevXMgr;
                    avConPayment.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avConPayment.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avConPayment.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avConPayment.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avConPayment.Dock = DockStyle.Fill;
                    avConPayment.Parent = panelControl1;
                }
                AlertViewMain = avConPayment;
            }
            else if (APP_ID == CONST_LOT_HOLD_ALERT_POINT || APP_ID == CONST_HOLD_RENEWAL_ALERT_POINT)
            {
                if (avHoldExpiry == null)
                {
                    avHoldExpiry = new AlertViews.Costing.LotExpiry.avLotHoldExpiry();
                    avHoldExpiry.HMConnection = Connection;
                    avHoldExpiry.TUC_DevXMgr = DevXMgr;
                    avHoldExpiry.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avHoldExpiry.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avHoldExpiry.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avHoldExpiry.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avHoldExpiry.Dock = DockStyle.Fill;
                    avHoldExpiry.Parent = panelControl1;
                }
                AlertViewMain = avHoldExpiry;                
            }
            else if (APP_ID >= CONST_PO_REC_NOTIFY_RANGE_START_ALERT_POINT && APP_ID <= CONST_PO_REC_NOTIFY_RANGE_END_ALERT_POINT)
            {
                if (avPORecNotify == null)
                {
                    avPORecNotify = new AlertViews.Costing.POReceiving.ucPORecNotify();
                    avPORecNotify.HMConnection = Connection;
                    avPORecNotify.TUC_DevXMgr = DevXMgr;
                    avPORecNotify.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avPORecNotify.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avPORecNotify.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avPORecNotify.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avPORecNotify.Dock = DockStyle.Fill;
                    avPORecNotify.Parent = panelControl1;
                    
                }
                AlertViewMain = avPORecNotify; 
            }
            else if (CONST_WO_WOO_AUTO_BACKFLUSH == APP_ID || CONST_WO_WOS_AUTO_BACKFLUSH == APP_ID)
            {
                if (avBackFlush == null)
                {
                    avBackFlush = new AlertViews.WorkOrders.BackFlush.ucBackFlush();
                    avBackFlush.HMConnection = Connection;
                    avBackFlush.TUC_DevXMgr = DevXMgr;
                    avBackFlush.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avBackFlush.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avBackFlush.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avBackFlush.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avBackFlush.Dock = DockStyle.Fill;
                    avBackFlush.Parent = panelControl1;
                }
                AlertViewMain = avBackFlush;
            }
            else if (APP_ID == CONST_PO_BUYER)
            {
                if (avPODetails == null)
                {
                    avPODetails = new AlertViews.PO.ucPODetails();
                    avPODetails.HMConnection = Connection;
                    avPODetails.TUC_DevXMgr = DevXMgr;
                    avPODetails.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avPODetails.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avPODetails.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avPODetails.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avPODetails.Dock = DockStyle.Fill;
                    avPODetails.Parent = panelControl1;
                }
                AlertViewMain = avPODetails;
            }
            else if (APP_ID == CONST_LAND_COLLECTION_REMINDER || APP_ID == CONST_ALERT_Builder_Next_Lot_Draw_Exclusion
                || APP_ID == CONST_ALERT_Late_Payment_Reminder || APP_ID == CONST_ALERT_Past_Due_Payment_Reminder
                || APP_ID == CONST_ALERT_Payout_Extension_Reminder || APP_ID == CONST_ALERT_Probationary_Builders_Reminder
                || APP_ID == CONST_ALERT_Title_Transfer_Confirmation_Reminder || APP_ID == CONST_ALERT_AGREEMENT_INTEREST_FREE_PERIOD)
            {
                if (avReminder == null)
                {
                    avReminder = new AlertViews.Costing.LandReminders.avLandReminder();
                    avReminder.HMConnection = Connection;
                    avReminder.TUC_DevXMgr = DevXMgr;
                    avReminder.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avReminder.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avReminder.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avReminder.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avReminder.Dock = DockStyle.Fill;
                    avReminder.Parent = panelControl1;
                }
                AlertViewMain = avReminder;
            }
            else if (APP_ID == CONST_ALERT_MASTER_AGREEMENT_INTEREST_FREE_PERIOD)
            {
                if (avMasterReminder == null)
                {
                    avMasterReminder = new AlertViews.Costing.LandReminders.avMasterAgreementReminder();
                    avMasterReminder.HMConnection = Connection;
                    avMasterReminder.TUC_DevXMgr = DevXMgr;
                    avMasterReminder.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avMasterReminder.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avMasterReminder.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avMasterReminder.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avMasterReminder.Dock = DockStyle.Fill;
                    avMasterReminder.Parent = panelControl1;
                }
                AlertViewMain = avMasterReminder;
            }
            else if (APP_ID == CONST_ALERT_LAND_REPAYMENT_DUE_ALERT_POINT)
            {
                if (avLandPaymentDue == null)
                {
                    avLandPaymentDue = new AlertViews.Costing.LandReminders.avLandPaymentDue();
                    avLandPaymentDue.HMConnection = Connection;
                    avLandPaymentDue.TUC_DevXMgr = DevXMgr;
                    avLandPaymentDue.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avLandPaymentDue.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avLandPaymentDue.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avLandPaymentDue.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avLandPaymentDue.Dock = DockStyle.Fill;
                    avLandPaymentDue.Parent = panelControl1;
                }
                AlertViewMain = avLandPaymentDue;
            }
            else if (APP_ID == CONST_ALERT_Lease_Expiry_Reminder_COMM || APP_ID == CONST_ALERT_Lease_Expiry_Reminder_RES)                
            {
                if (avLeaseReminder == null)
                {
                    avLeaseReminder = new AlertViews.Costing.TenantReminders.avLeaseExpiryReminder();
                    avLeaseReminder.HMConnection = Connection;
                    avLeaseReminder.TUC_DevXMgr = DevXMgr;

                    avLeaseReminder.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avLeaseReminder.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avLeaseReminder.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avLeaseReminder.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avLeaseReminder.Dock = DockStyle.Fill;
                    avLeaseReminder.Parent = panelControl1;
                }
                AlertViewMain = avLeaseReminder;
            }
            else if (APP_ID == CONST_ALERT_WEB_PO_Submission)
            {
                if (avWebPODetails == null)
                {
                    avWebPODetails = new AlertViews.PO.avWebPODetails(this.Connection, this.DevXMgr);
                    avWebPODetails.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avWebPODetails.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avWebPODetails.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avWebPODetails.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avWebPODetails.Dock = DockStyle.Fill;
                    avWebPODetails.Parent = panelControl1;
                }
                AlertViewMain = avWebPODetails;
            }
            else if (APP_ID == CONST_ALERT_WEB_Subcontractor_Payment_Request)
            {
                if (avSubConPayReq == null)
                {
                    avSubConPayReq = new AlertViews.Costing.avWebSubConPayReq();
                    avSubConPayReq.HMConnection = Connection;
                    avSubConPayReq.TUC_DevXMgr = DevXMgr;
                    avSubConPayReq.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avSubConPayReq.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avSubConPayReq.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avSubConPayReq.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avSubConPayReq.Dock = DockStyle.Fill;
                    avSubConPayReq.Parent = panelControl1;
                }
                AlertViewMain = avSubConPayReq;
            }
            else if (APP_ID == CONST_ALERT_REFLEX_CHAT)
            {
                if (avReflexChat == null)
                {
                    avReflexChat = new AlertViews.avChat(Connection, DevXMgr);
                    avReflexChat.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avReflexChat.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avReflexChat.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avReflexChat.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avReflexChat.Dock = DockStyle.Fill;
                    avReflexChat.Parent = panelControl1;
                }
                AlertViewMain = avReflexChat;
            }
            else if (APP_ID == CONST_ALERT_PO_SUBTYPE_SIGNATURE_NOTICE)
            {
                if (avPODocDet == null)
                {
                    avPODocDet = new AlertViews.PO.avPODocDetail(Connection, DevXMgr);
                    avPODocDet.HMConnection = Connection;
                    avPODocDet.TUC_DevXMgr = DevXMgr;
                    avPODocDet.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
                    avPODocDet.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
                    avPODocDet.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
                    avPODocDet.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
                    avPODocDet.Dock = DockStyle.Fill;
                    avPODocDet.Parent = panelControl1;
                }
                AlertViewMain = avPODocDet;
            }
            else if (APP_ID == CONST_ALERT_RENTAL_STATUS_CHANGE)
            {
                if (avRentalStatusChg == null)
                {
                    avRentalStatusChg = new AlertViews.Rental.ucRentalRequestStatusChg();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avRentalStatusChg);
                }
                AlertViewMain = avRentalStatusChg;
            }
            else if (APP_ID == CONST_ALERT_RENTAL_START)
            {
                if (avRentalStart == null)
                {
                    avRentalStart = new AlertViews.Rental.ucRentalStart();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avRentalStart);
                }
                AlertViewMain = avRentalStart;
            }
            else if (APP_ID == CONST_ALERT_RENTAL_END)
            {
                if (avRentalEnd == null)
                {
                    avRentalEnd = new AlertViews.Rental.ucRentalEnd();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avRentalEnd);
                }
                AlertViewMain = avRentalEnd;
            }
            else if (APP_ID == CONST_ALERT_AUTO_BFS_CREATE_FAIL)
            {
                if (avBFSAutoFail == null)
                {
                    avBFSAutoFail = new AlertViews.WorkOrders.Automation.avAutomation();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avBFSAutoFail);
                }
                AlertViewMain = avBFSAutoFail;
            }
            else if (APP_ID == CONST_ALERT_Employee_Certificate_Expiry)
            {
                if (avEmpCerCertificateExpiry==null)
                {
                    avEmpCerCertificateExpiry = new AlertViews.Human_Resources.avEmployeeCertificateExpiry();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avEmpCerCertificateExpiry);
                }
                AlertViewMain = avEmpCerCertificateExpiry;
            }
            else if (APP_ID == CONST_ALERT_FA_Preventative_Maintenance)
            {
                if (avFAPM == null)
                {
                    avFAPM = new AlertViews.FixedAssets.avFAPreventativeMaintenance();
                    //sets base variables,dock,parent and adds events
                    SetAlertViewBase(avFAPM);
                }
                AlertViewMain = alertView1;
            }
            else
            {
                AlertViewMain = alertView1;
            }


            AlertViewMain.BringToFront();
            AlertViewMain.LoadAlert(AlertID, Subject, Module, Received, Notes, DETAIL_ID);  
           
        }

        private void SetAlertViewBase(AlertView av)
        {
            av.HMConnection = Connection;
            av.TUC_DevXMgr = DevXMgr;
            av.AlertDeleted += new AlertView.AlertDeleted_Delegate(alertView1_AlertDeleted);
            av.AlertSeen += new AlertView.AlertSeen_Delegate(alertView1_AlertSeen);
            av.AlertUnseen += new AlertView.AlertUnseen_Delegate(alertView1_AlertUnseen);
            av.AlertCompleted += new AlertView.AlertCompleted_Delegate(alertView1_AlertCompleted);
            av.Dock = DockStyle.Fill;
            av.Parent = panelControl1;
        }

        private void ClearAlertDetails()
        {
            dsStakeholders1.Clear();
        }

        #endregion

        private void ManagementBoard_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void gvAlerts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                gvAlerts.MovePrev();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                gvAlerts.MoveNext();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (gvAlerts.FocusedRowHandle < 0)
                {
                    if (gvAlerts.GetRowExpanded(gvAlerts.FocusedRowHandle))
                        gvAlerts.MoveNext();
                    else
                        gvAlerts.ExpandGroupRow(gvAlerts.FocusedRowHandle);
                    e.Handled = true;
                }
                else
                {
                    gvAlerts.MoveNext();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (gvAlerts.FocusedRowHandle < 0)
                {
                    if (gvAlerts.GetRowExpanded(gvAlerts.FocusedRowHandle))
                        gvAlerts.CollapseGroupRow(gvAlerts.FocusedRowHandle);
                    else
                        gvAlerts.MovePrev();
                    e.Handled = true;
                }
                else
                {
                    int GroupHandle = gvAlerts.GetParentRowHandle(gvAlerts.FocusedRowHandle);
                    gvAlerts.CollapseGroupRow(GroupHandle);
                    gvAlerts.FocusedRowHandle = GroupHandle;
                    e.Handled = true;
                }
            }
        }

        private void alertView1_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            RefreshAlerts();          
        }

        public void AlertClicked(int ID)
        {
            if (ID != -1)
            {
                gvAlerts.FocusedRowHandle = gvAlerts.LocateByValue(0, colID, ID);
            }
        }

        public void RefreshAlerts()
        {
            if (gvAlerts != null)
            {
                object oAlertID = null;
                DataRow dr = gvAlerts.GetDataRow(gvAlerts.FocusedRowHandle);
                if (dr != null)
                {
                    oAlertID = dr["ID"];
                    if (oAlertID == null || oAlertID == DBNull.Value)
                    {
                        oAlertID = null;
                    }
                }

                dsAlert1.Clear();
                daAlert.Fill(dsAlert1);
                gAlert.Text = "Alerts (" + GetAlertCount() + ")";
                gvAlerts.ExpandAllGroups();
                int i = gvAlerts.LocateByValue(0, colID, oAlertID);
                gvAlerts.FocusedRowHandle = i;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lueAlertType.EditValue = null;
            txtSubject.EditValue = null;
            dtFrom.EditValue = null;
            dtTo.EditValue = null;
                      
            daAlert.SelectCommand.CommandText = AlertSelect;
            dsAlert1.Clear();
            daAlert.Fill(dsAlert1);
            gvAlerts.ExpandAllGroups();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtFrom.EditValue != DBNull.Value && dtFrom.EditValue != null &&
                dtTo.EditValue != DBNull.Value && dtTo.EditValue != null)
            {
                if (dtFrom.DateTime.CompareTo(dtTo.DateTime) == 1)
                {
                    Popup.ShowPopup("Received from date cannot be greater than the received to date.");
                    return;
                }
            }

            dsAlert1.Clear();

            if (CriteriaSelected())
            {
                string sAAP_ID = "-1";
                if (lueAlertType.EditValue != null)
                    sAAP_ID = lueAlertType.EditValue.ToString();

                string sFrom = "null";
                if( dtFrom.EditValue != null )
                    sFrom = "'" + dtFrom.DateTime.ToShortDateString() + " 11:59:59 PM'";

                string sTo = "null";
                if (dtTo.EditValue != null)
                    sTo = "'" + dtTo.DateTime.ToShortDateString() + " 11:59:59 PM'";

                string sSQL = "exec sp_FillAlertSearch '"+Connection.MLUser+"'," + sAAP_ID + ",'" + txtSubject.Text.Trim().Replace("'", "''") + "', " + sFrom + ", " + sTo + " ";
                Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                daAlert.SelectCommand.CommandText = AlertSearchSelect;
            }
            else
                daAlert.SelectCommand.CommandText = AlertSelect;

            daAlert.Fill(dsAlert1);
            gvAlerts.ExpandAllGroups();
        }

        private bool CriteriaSelected()
        {
            if (lueAlertType.EditValue != null)
                return true;
            if (!txtSubject.Text.Trim().Equals(""))
                return true;
            if (dtFrom.EditValue != null)
                return true;
            if (dtTo.EditValue != null)
                return true;
            return false;
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAlerts();
        }

    }
}
