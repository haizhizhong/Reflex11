using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.LandDevelopment
{
    public partial class ucLotHold : ucPropertyInterface
    {
        public ucLotHold()
        {
            InitializeComponent();
        }

        private void riDaysPrior_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && e.NewValue != DBNull.Value)
            {
                if (Convert.ToInt32(e.NewValue) <= 0)
                {                    
                    Popup.ShowPopup("Days prior value must be greater than zero.");
                    e.Cancel = true;
                }
            }
        }

        private void gvTolerance_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daTolerance.Update(dsTolerance);
        }

        private void gcTolerance_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                DataRow dr = gvTolerance.GetDataRow(gvTolerance.FocusedRowHandle);
                if (dr != null)
                {
                    string sDelete = "delete from alert_lot_hold_tolerance where id="+dr["id"];
                    Connection.SQLExecutor.ExecuteNonQuery(sDelete, Connection.TRConnection);
                    int iHandle = gvTolerance.FocusedRowHandle - 1;
                    dsTolerance.Clear();
                    daTolerance.Fill(dsTolerance);
                    gvTolerance.FocusedRowHandle = iHandle;
                    e.Handled = true;
                }                
            }
        }

        private void gvTolerance_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvTolerance.GetDataRow(gvTolerance.FocusedRowHandle);
            if (dr != null)
            {
                object oDaysPrior = dr["DaysPrior"];
                if (oDaysPrior == null || oDaysPrior == DBNull.Value)
                {
                    e.ErrorText = "Days prior value must be greater than zero.";
                    e.Valid = false;
                    return;
                }
                else if( Convert.ToInt32( oDaysPrior ) <= 0 )
                {
                    e.ErrorText = "Days prior value must be greater than zero.";
                    e.Valid = false;
                    return;
                }

                object oID = dr["id"];
                string sSelect = "select count(*) from alert_lot_hold_tolerance where id<>"+oID+" and daysprior="+oDaysPrior;
                if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                {
                    e.ErrorText = "A record with the same days prior already exists.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvTolerance_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void ucLotHold_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
            TR_Conn.ConnectionString = Connection.TRConnection;
            daTolerance.Fill(dsTolerance);

            string sSelect = "select count(*) from alert_lot_hold_setup";
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_lot_hold_setup (id, OnHold_On, PreExpiry_On, Expiry_On, reply_tolerance) values (1, 'F', 'F', 'F', 1)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(OnHold_On,'F'), isnull(PreExpiry_On,'F'), isnull(Expiry_On,'F'), isnull(reply_tolerance,1) from alert_lot_hold_setup";
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        chkOnHold.EditValue = dr[0];
                        chkPreExpire.EditValue = dr[1];
                        chkExpiry.EditValue = dr[2];
                        txtTolerance.EditValue = dr[3];
                    }
                }
            }
			
        }

        private void CHK_Changed(object sender, EventArgs e)
        {
            string sUpdate = "update alert_lot_hold_setup set OnHold_On='" + chkOnHold.EditValue.ToString().Substring(0, 1) + "', PreExpiry_On='" + chkPreExpire.EditValue.ToString().Substring(0, 1) + "', Expiry_On='" + chkExpiry.EditValue.ToString().Substring(0, 1) + "'";
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void txtTolerance_EditValueChanged(object sender, EventArgs e)
        {
            int iTolerance = 1;
            if (txtTolerance.EditValue != null && txtTolerance.EditValue != DBNull.Value)
                iTolerance = Convert.ToInt32(txtTolerance.EditValue);
            string sUpdate = "update alert_lot_hold_setup set reply_tolerance=" + iTolerance;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void txtTolerance_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue != null && e.NewValue != DBNull.Value)
            {
                if (Convert.ToInt32(e.NewValue) <= 0)
                {
                    Popup.ShowPopup("Tolerance value must be greater than zero.");
                    e.Cancel = true;
                }
            }
        }
        
    }
}
