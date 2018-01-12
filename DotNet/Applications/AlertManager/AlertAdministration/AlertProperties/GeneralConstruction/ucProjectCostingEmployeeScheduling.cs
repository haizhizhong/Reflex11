using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Collections;
using WS_Popups;
using HMConnection;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AlertProperties.GeneralConstruction
{
    public partial class ucProjectCostingEmployeeScheduling : ucPropertyInterface
    {
        private Rectangle dragBox;
        
        
        public ucProjectCostingEmployeeScheduling()
        {
            InitializeComponent();   
        }

        private void ucProjectCostingEmployeeScheduling_Load(object sender, EventArgs e)
        {
            TR_Conn.ConnectionString = Connection.TRConnection;

            daPCStakeholderType.Fill(dsPCStakeholderType1.PC_Stakeholder_Type);
            daAlertEmployeeScheduling.Fill(dsAlertEmployeeScheduling1.alert_employee_scheduling);
            this.Disposed += new EventHandler(ucProjectCostingEmployeeScheduling_Disposed);
        }

        void ucProjectCostingEmployeeScheduling_Disposed(object sender, EventArgs e)
        {
            //string sSQL = @"delete from alert_employee_scheduling where pc_stakeholder_type_id = -1";
            //Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
        }

        private void gvStakeholderType_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daAlertEmployeeScheduling.Update(dsAlertEmployeeScheduling1);
            
        }

        private void gcStakeholderType_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                object oID = gvStakeholderType.GetDataRow(gvStakeholderType.FocusedRowHandle)["id"];
                if (oID == null || oID == DBNull.Value)
                {
                    e.Handled = true;
                    return;
                }

                if (Popup.ShowPopup("Are you sure you want to remove the selected stakeholder type ?", WS_Popups.frmPopup.PopupType.OK_Cancel) == frmPopup.PopupResult.OK)
                {
                    string sSQL = @"delete from alert_employee_scheduling where id = " + oID;
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
                       
        }


        private void gvStakeholderType_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        private object CastDBNullToNull(object o)
        {
            if (Convert.IsDBNull(o))
                return null;
            else
                return o;
        }

        private void gvStakeholderType_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string cmdText = "select * from alert_employee_scheduling where pc_stakeholder_type_id = {0}";
            object stakeholder_type_id = gvStakeholderType.GetFocusedRowCellValue(colPC_Stakeholder_Type_ID);
            cmdText = string.Format(cmdText, stakeholder_type_id);
            System.Data.DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(cmdText, Connection.TRConnection);

            if (dt != null && dt.Rows.Count > 0)
            {
                // only 1 stakeholder type is allow at any given time
                e.Valid = false;
                e.ErrorText = "The selected stakeholder type already exists.";
            }       
            else
            {
                e.Valid = true;
            }
        }

       

    }
}
