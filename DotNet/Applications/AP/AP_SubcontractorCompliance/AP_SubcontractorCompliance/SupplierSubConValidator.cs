using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using HMConnection;
using WS_Popups;
using System.Data.SqlClient;
			
namespace AP_SubcontractorCompliance
{
    public class SupplierSubConValidator
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private SqlConnection TR_Conn;

        public enum Rule { Warning, Pre_Accrual, Payment_Hold, PO_Approval, Restrict };

        public SupplierSubConValidator(HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn = new SqlConnection(Connection.TRConnection);
        }

        public bool SupplierValid(string Supplier, Rule SupplierRule, DateTime Date)
        {
            return SupplierValid(Supplier, SupplierRule, Date, -1);
        }

        public bool SupplierValid(string Supplier, Rule SupplierRule, DateTime Date, int PO_ID)
        {
            bool valid = true;

            string sSQL = @"exec AP_SuppSubCon_Valid null, '" + Supplier + @"', '" + Date.ToShortDateString() + @"', " + GetRuleNo(SupplierRule) + @", " + PO_ID;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        if (Convert.ToInt32(dr[0]) > 0)
                        {
                            valid = false;
                            Popup.ShowPopup(dr[1].ToString());
                        }
                    }
                }
            }

            return valid;
        }

        public bool SupplierValid(string Supplier, Rule SupplierRule, DateTime Date, int PO_ID, int AP_INV_HEADER_ID)
        {
            bool valid = true;

            string sSQL = @"exec AP_SuppSubCon_Valid null, '" + Supplier + @"', '" + Date.ToShortDateString() + @"', " + GetRuleNo(SupplierRule) + @", " + PO_ID + @", " + AP_INV_HEADER_ID;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        if (Convert.ToInt32(dr[0]) > 0)
                        {
                            valid = false;
                            Popup.ShowPopup(dr[1].ToString());
                        }
                    }
                }
            }

            return valid;
        }

        public bool SupplierValid(int SupplierID, Rule SupplierRule, DateTime Date)
        {
            return SupplierValid(SupplierID, SupplierRule, Date, -1);
        }

        public bool SupplierValid(int SupplierID, Rule SupplierRule, DateTime Date, int PO_ID)
        {
            bool valid = true;

            string sSQL = @"exec AP_SuppSubCon_Valid " + SupplierID + @", null, '" + Date.ToShortDateString() + @"', " + GetRuleNo(SupplierRule) + @", " + PO_ID;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection)) > 0)
                valid = false;

            if (!valid)
            {
                if (SupplierRule == Rule.Warning)
                    Popup.ShowPopup("The selected supplier has a warning flaged on a subcontractor compliance rule.");
                else if (SupplierRule == Rule.Pre_Accrual)
                    Popup.ShowPopup("The selected supplier requires pre accrual routing for an expired subcontractor compliance rule.");
                else if (SupplierRule == Rule.Payment_Hold)
                    Popup.ShowPopup("The selected supplier requires payment hold routing for an expired subcontractor compliance rule.");
                else if (SupplierRule == Rule.PO_Approval)
                    Popup.ShowPopup("The selected supplier requires PO routing for an expired subcontractor compliance rule.");
            }

            return valid;
        }

        private int GetRuleNo(Rule SupplierRule)
        {
            int RuleNo = 1;

            if (SupplierRule == Rule.Warning)
                RuleNo = 1;
            else if (SupplierRule == Rule.Pre_Accrual)
                RuleNo = 2;
            else if (SupplierRule == Rule.Payment_Hold)
                RuleNo = 3;
            else if (SupplierRule == Rule.PO_Approval)
                RuleNo = 4;
            else if (SupplierRule == Rule.Restrict)
                RuleNo = 5;

            return RuleNo;
        }
    
    }
}
