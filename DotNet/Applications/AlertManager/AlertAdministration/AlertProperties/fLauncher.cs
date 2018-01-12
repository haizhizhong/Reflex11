using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using HMConnection;
using WS_Popups;

namespace AlertProperties
{
    public partial class fLauncher : DevExpress.XtraEditors.XtraForm
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private int _AlertID = -1;

        #region Constants

        private const int CONST_ALERT_LOT_HOLD = 14;
        private const int CONST_ALERT_HOME_OWNER = 15;
        private const int CONST_ALERT_DOWNPAYMENT_DUE = 20;
        private const int CONST_ALERT_PO_CREATE = 38;
        private const int CONST_ALERT_COLLECTION_REMINDERS = 40;
        private const int CONST_ALERT_Builder_Next_Lot_Draw_Exclusion = 41;
        private const int CONST_ALERT_Past_Due_Payment_Reminder = 42;
        private const int CONST_ALERT_Late_Payment_Reminder = 43;
        private const int CONST_ALERT_Title_Transfer_Confirmation_Reminder = 44;
        private const int CONST_ALERT_Payout_Extension_Reminder = 45;
        private const int CONST_ALERT_Probationary_Builders_Reminder = 46;
        private const int CONST_ALERT_Lease_Expiry_Reminder_COMM = 47;
        private const int CONST_ALERT_Lease_Expiry_Reminder_RES = 48;
        private const int CONST_ALERT_EMPLOYEE_SCHEDULING = 66;
        private const int CONST_ALERT_PROJECT_BUDACT = 71;
        private const int CONST_ALERT_PO_SUBTYPE_SIGNATURE_APPROVAL = 75;
        private const int CONST_ALERT_AGREEMENT_INTEREST_FREE_PERIOD_EXPIRY = 76;
        private const int CONST_ALERT_MASTER_AGREEMENT_INTEREST_FREE_PERIOD_EXPIRY = 77;
        private const int CONST_ALERT_LAND_PAYMENT_DUE = 78;
        private const int CONST_ALERT_Rental_Request_Status_Change = 79;
        private const int CONST_ALERT_Rental_Agreement_Start = 80;
        private const int CONST_ALERT_Rental_Agreement_End = 81;
        private const int CONST_ALERT_Employee_Certificate_Expiry = 89;

        #endregion

        public fLauncher(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AlertID)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            _AlertID = AlertID;
            InitializeComponent();
            LoadAlertProperty();
        }

        private void LoadAlertProperty()
        {
            ucPropertyInterface ucPI;
            if (_AlertID == CONST_ALERT_LOT_HOLD)
            {
                ucPI = new LandDevelopment.ucLotHold();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;                
            }
            else if (_AlertID == CONST_ALERT_HOME_OWNER)
            {
                ucPI = new LandDevelopment.ucHomeOwner();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;                
            }
            else if (_AlertID == CONST_ALERT_DOWNPAYMENT_DUE)
            {
                ucPI = new LandDevelopment.ucDownPaymentDue();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_PO_CREATE)
            {
                ucPI = new PO.ucPOBuyer();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_COLLECTION_REMINDERS)
            {
                ucPI = new LandDevelopment.ucCollectionReminders();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Builder_Next_Lot_Draw_Exclusion)
            {
                ucPI = new LandDevelopment.ucBuilderNextLotDrawExclusion();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Past_Due_Payment_Reminder)
            {
                ucPI = new LandDevelopment.ucPastDuePaymentReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;

            }
            else if (_AlertID == CONST_ALERT_Late_Payment_Reminder)
            {
                ucPI = new LandDevelopment.ucLatePaymentReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if ( _AlertID == CONST_ALERT_Title_Transfer_Confirmation_Reminder)
            {
                ucPI = new LandDevelopment.ucTitleTransferConfirmationReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Payout_Extension_Reminder)
            {
                ucPI = new LandDevelopment.ucPayoutExtensionReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Probationary_Builders_Reminder)
            {
                ucPI = new LandDevelopment.ucProbationaryBuildersReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Lease_Expiry_Reminder_COMM)
            {
                ucPI = new RevenueProperties.ucLeaseExpiryReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Lease_Expiry_Reminder_RES)
            {
                ucPI = new RevenueProperties.ucLeaseExpiryReminder();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_EMPLOYEE_SCHEDULING)
            {
                ucPI = new GeneralConstruction.ucProjectCostingEmployeeScheduling();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_PROJECT_BUDACT)
            {
                ucPI = new GeneralConstruction.ucProjectBudActNotifications();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_PO_SUBTYPE_SIGNATURE_APPROVAL)
            {
                ucPI = new GeneralConstruction.ucProjectPOSubtypeApproval();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_AGREEMENT_INTEREST_FREE_PERIOD_EXPIRY)
            {
                ucPI = new LandDevelopment.ucInterestFreeExpiry();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_MASTER_AGREEMENT_INTEREST_FREE_PERIOD_EXPIRY)
            {
                ucPI = new LandDevelopment.ucInterestFreeExpiryMaster();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_LAND_PAYMENT_DUE)
            {
                ucPI = new LandDevelopment.ucFinancingPaymentDue();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Rental_Request_Status_Change)
            {
                ucPI = new Rental.ucRentalRequestStatusChange();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Rental_Agreement_Start)
            {
                ucPI = new Rental.ucRentalStartNotification();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Rental_Agreement_End)
            {
                ucPI = new Rental.ucRentalEndNotification();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
            else if (_AlertID == CONST_ALERT_Employee_Certificate_Expiry)
            {
                ucPI = new HumanResources.ucEmpCertificateExpiry();
                ucPI.Dock = DockStyle.Fill;
                ucPI.HMConnection = Connection;
                ucPI.HMDevXMgr = DevXMgr;
                ucPI.AlertPointID = _AlertID;
                ucPI.InitializeProperty();
                this.Size = ucPI.Size;
                ucPI.Parent = this;
            }
        }

        private void fLauncher_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }
    
    }
}