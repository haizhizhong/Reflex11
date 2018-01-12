using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AP_Levy
{
    public class Purchaser
    {
        #region Properties

        private string _Name = "";
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Addr1 = "";
        public string Addr1
        {
            get { return _Addr1; }
            set { _Addr1 = value; }
        }

        private string _Addr2 = "";
        public string Addr2
        {
            get { return _Addr2; }
            set { _Addr2 = value; }
        }

        private string _Addr3 = "";
        public string Addr3
        {
            get { return _Addr3; }
            set { _Addr3 = value; }
        }

        private string _City = "";
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _Prov = "";
        public string Prov
        {
            get { return _Prov; }
            set { _Prov = value; }
        }

        private string _Postal = "";
        public string Postal
        {
            get { return _Postal; }
            set { _Postal = value; }
        }

        #endregion

        public Purchaser(HMConnection.HMCon Connection, int Purchaser_ID)
        {
            string sSQL = @"select isnull(NAME,''), isnull(BILL_ADDRESS_1,''), isnull(BILL_ADDRESS_2,''), isnull(BILL_ADDRESS_3,''), 
                isnull(BILL_CITY,''), isnull(BILL_STATE,''), isnull(BILL_ZIP ,'') 
                from CUSTOMERS 
                where CUSTOMER_ID=" + Purchaser_ID;
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        _Name = dr[0].ToString();
                        _Addr1 = dr[1].ToString();
                        _Addr2 = dr[2].ToString();
                        _Addr3 = dr[3].ToString();
                        _City = dr[4].ToString();
                        _Prov = dr[5].ToString();
                        _Postal = dr[6].ToString();
                    }
                }
            }
        }        
    }
}
