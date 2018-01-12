using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HMConnection;
using WS_Popups;

namespace AlertViews.Costing.POReceiving
{
    public partial class ucPORecNotifyDet : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private int _DetailID = -1;

        public ucPORecNotifyDet()
        {
            InitializeComponent();
        }

        #region Properties

        public HMCon HMConnection
        {
            set
            {
                Connection = value;
                if (Connection != null)
                {
                    TR_Conn.ConnectionString = Connection.TRConnection;
                }
            }
            get
            {
                return Connection;
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUC_DevXMgr
        {
            set
            {
                DevXMgr = value;
                if (DevXMgr != null)
                {
                    Popup = new frmPopup(value);
                }
            }
            get
            {
                return DevXMgr;
            }
        }

        #endregion

        private void ucPORecNotify_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
                DevXMgr.FormInit(this);
        }

        public void ResetConnections()
        {
            if (Connection != null)
            {
                HMConnection = Connection;
            }
        }

        public void LoadDetail(int DetailID)
        {
            _DetailID = DetailID;

            lblPO.Text = "";
            lblReceipt.Text = "";
            lblReceiptDate.Text = "";

            dsPORecDet.Clear();
            daPORecDet.SelectCommand.Parameters["@po_rec_id"].Value = DetailID;
            daPORecDet.Fill(dsPORecDet);

            string sSQL = @"select h.po, rh.receiver_number, rh.receipt_date from po_rec_header rh join po_header h on h.po_id=rh.po_id where rh.po_rec_id="+DetailID;
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        lblPO.Text = dr[0].ToString();
                        lblReceipt.Text = dr[1].ToString();
                        lblReceiptDate.Text = String.Format("{0:D}", dr[2]);
                    }
                }
            }
        }

    }
}
