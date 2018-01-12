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
    public partial class ucCollectionReminders : ucPropertyInterface
    {
        public ucCollectionReminders()
        {
            InitializeComponent();
        }

        private void ucCollectionReminders_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
            TR_Conn.ConnectionString = Connection.TRConnection;

            string sSelect = "select count(*) from alert_land_reminder_setup where AlertID="+AlertPointID;
            if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) == 0)
            {
                string sInsert = "insert into alert_land_reminder_setup (AlertID, X_Days) values ("+AlertPointID+", 0)";
                Connection.SQLExecutor.ExecuteNonQuery(sInsert, Connection.TRConnection);
            }

            sSelect = "select isnull(X_Days,0) from alert_land_reminder_setup where AlertID=" + AlertPointID;
            txtDays.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            LoadCommPurpose();
            LoadDocMergeFunc();

            daComPur.SelectCommand.Parameters["@AlertID"].Value = AlertPointID;
            daComPur.Fill(dsComPur1);

            daDocMerge.SelectCommand.Parameters["@AlertID"].Value = AlertPointID;
            daDocMerge.Fill(dsDocMerge1);

            labelControl1.Focus();
        }

        private void txtDays_EditValueChanged(object sender, EventArgs e)
        {
            string sUpdate = "update alert_land_reminder_setup set X_Days=" + txtDays.EditValue+" where AlertID="+AlertPointID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
        }

        private void LoadCommPurpose()
        {
            string sSQL = @"select ID, DESCRIPTION 
                from COMMUNICATION_PURPOSE
                order by DESCRIPTION";
            DataTable dtComPur = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.WebConnection);
            riComPur.DataSource = dtComPur;
        }

        private void gvComPur_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvComPur.GetDataRow(gvComPur.FocusedRowHandle);
            if (dr != null)
            {
                object oCOMM_PUR_ID = dr["COMM_PUR_ID"];
                if (oCOMM_PUR_ID == null || oCOMM_PUR_ID == DBNull.Value)
                {
                    e.ErrorText = "Communication purpose is required.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvComPur_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daComPur.Update(dsComPur1);
        }

        private void gvComPur_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow dr = gvComPur.GetDataRow(gvComPur.FocusedRowHandle);
            if (dr != null)
            {
                dr["AlertID"] = AlertPointID;
            }
        }

        private void gvComPur_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gcComPur_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popup.ShowPopup("Are you sure you want to delete the selected communication purpose record?", WS_Popups.frmPopup.PopupType.OK_Cancel) == WS_Popups.frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvComPur.GetDataRow(gvComPur.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oID = dr["ID"];
                        if (oID != null && oID != DBNull.Value)
                        {
                            string sSQL = @"delete from alert_land_reminder_com_purpose where ID=" + oID;
                            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }


        private void LoadDocMergeFunc()
        {
            string sSQL = @"select l.DocMergeLibrary_ID, l.MergeTemplateName
                from DocMergeLibrary l
                where l.DocMergeFunction_ID=1 and (l.Company_ID=-1 or l.Company_ID="+Connection.CompanyID+@")
                order by l.MergeTemplateName";
            DataTable dtDocMerge = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.WebConnection);
            riDocMerge.DataSource = dtDocMerge;
        }

        private void gvDocMerge_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvDocMerge.GetDataRow(gvDocMerge.FocusedRowHandle);
            if (dr != null)
            {
                object oDocMergeLibrary_ID = dr["DocMergeLibrary_ID"];
                if (oDocMergeLibrary_ID == null || oDocMergeLibrary_ID == DBNull.Value)
                {
                    e.ErrorText = "Library document item is required.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvDocMerge_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daDocMerge.Update(dsDocMerge1);
        }

        private void gvDocMerge_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gcDocMerge_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popup.ShowPopup("Are you sure you want to delete the selected library item record?", WS_Popups.frmPopup.PopupType.OK_Cancel) == WS_Popups.frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvDocMerge.GetDataRow(gvDocMerge.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oID = dr["ID"];
                        if (oID != null && oID != DBNull.Value)
                        {
                            string sSQL = @"delete from alert_land_reminder_docMerge where ID=" + oID;
                            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void gvDocMerge_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow dr = gvDocMerge.GetDataRow(gvDocMerge.FocusedRowHandle);
            if (dr != null)
            {
                dr["AlertID"] = AlertPointID;
            }
        }

        private void txtDays_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Convert.ToInt32(e.NewValue) < 0)
            {
                e.Cancel = true;
            }
        }


        //
    }
}
