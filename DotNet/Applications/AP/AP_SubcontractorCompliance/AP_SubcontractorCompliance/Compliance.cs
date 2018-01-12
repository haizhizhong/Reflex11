using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AP_SubcontractorCompliance
{
    public class Compliance
    {
        public static void LoadRules(HMConnection.HMCon Connection, DataRow drSrc, int InsurType_ID)
        {
            string sSQL = @"select isnull(rule_warning,'F') [rule_warning], isnull(rule_accrual,'F') [rule_accrual], isnull(rule_payment,'F') [rule_payment], 
                isnull(rule_po,'F') [rule_po], isnull(webrule_internal_alert,'F') [webrule_internal_alert], isnull(webrule_warn_contractor,'F') [webrule_warn_contractor], 
                isnull(webrule_restrict_payment_req,'F') [webrule_restrict_payment_req], isnull(rule_restrict,'F') [rule_restrict], isnull(rule_holdback_release,'F') [rule_holdback_release]
            from SUPPLIER_SUBCON_INSUR_TYPE
            where id=" + InsurType_ID;
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {                        
                        drSrc.BeginEdit();
                        drSrc["rule_warning"] = dr["rule_warning"];
                        drSrc["rule_accrual"] = dr["rule_accrual"];
                        drSrc["rule_payment"] = dr["rule_payment"];
                        drSrc["rule_po"] = dr["rule_po"];
                        drSrc["webrule_internal_alert"] = dr["webrule_internal_alert"];
                        drSrc["webrule_warn_contractor"] = dr["webrule_warn_contractor"];
                        drSrc["webrule_restrict_payment_req"] = dr["webrule_restrict_payment_req"];
                        drSrc["rule_restrict"] = dr["rule_restrict"];
                        drSrc["rule_holdback_release"] = dr["rule_holdback_release"];
                        drSrc.EndEdit();                        
                    }
                }
            }
			
        }
    }
}
