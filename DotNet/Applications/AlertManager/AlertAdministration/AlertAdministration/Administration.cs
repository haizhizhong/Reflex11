using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using WS_Popups;
using HMConnection;
using System.Data.SqlClient;

namespace AlertAdministration
{
    public partial class Administration : DevExpress.XtraEditors.XtraUserControl
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private HMCon Connection;

        private string sSQL_AlertPointSelect = @"SELECT distinct a.ID, a.APP_MOD_ID, a.ALERT, a.ALERT_POINT, r.ID [ID1], r.REQUIRED, a.TYPE, a.has_properties, r.Email, r.ExtEmail
            FROM ApplicationAlertPoint AS a 
            left outer join AlertPointRequired AS r ON r.AAP_ID = a.ID
            join web_db.dbo.ApplicationModule am on am.ID=a.app_mod_id
            left outer join SecurityOverrideDetail od on od.SecurityFunctionID=am.SecurityFunctionID
            left outer join SecurityOverride o on o.ID=od.SecurityOverrideID
            where isnull(o.Active,1) = 1
            ORDER BY a.ALERT";

        public Administration(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            Popup = new frmPopup(DevXMgr);

            sSQL_AlertPointSelect = sSQL_AlertPointSelect.Replace("web_db", Connection.WebDB);
            InitializeComponent();

            daAlertPoint.SelectCommand.CommandText = sSQL_AlertPointSelect;

            TR_Conn.ConnectionString = this.Connection.TRConnection;
            WEB_Conn.ConnectionString = this.Connection.WebConnection;

            daAlertPoint.Fill(dsAlertPoint1);            
            daModule.Fill(dsModule1);
            gvAlert.CollapseAllGroups();
        }

        private void Administration_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void RefreshGrid()
        {
            int iHandle = gvAlert.FocusedRowHandle;
            dsAlertPoint1.Clear();
            daAlertPoint.Fill(dsAlertPoint1);
            gvAlert.ExpandAllGroups();
            gvAlert.FocusedRowHandle = iHandle;
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            if (dr != null)
            {
                UserExceptions fUserExp = new UserExceptions(Connection, DevXMgr, Convert.ToInt32(dr["ID"]));
                fUserExp.ShowDialog();
                IDisposable dispose = fUserExp;
                dispose.Dispose();
            }
        }

        private void gvAlert_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow dr = gvAlert.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                if (e.Column == colUserException)
                {

                    object oRequired = dr["REQUIRED"];
                    if (oRequired != null && oRequired != DBNull.Value)
                    {
                        if (oRequired.ToString() == "T")
                        {
                            e.RepositoryItem = repositoryItemButtonEdit1;
                        }
                        else
                        {
                            e.RepositoryItem = repositoryItemButtonEdit2;
                        }
                    }
                    else
                    {
                        e.RepositoryItem = repositoryItemButtonEdit2;
                    }

                }
                else if (e.Column.Name.Equals(colProperties.Name))
                {
                    object oPROPERTIES = dr["has_properties"];
                    if (oPROPERTIES != null && oPROPERTIES != DBNull.Value)
                    {
                        if (oPROPERTIES.Equals("T"))
                            e.RepositoryItem = riProperties;
                        else
                            e.RepositoryItem = riPropertiesRO;
                    }
                    else
                    {
                        e.RepositoryItem = riPropertiesRO;
                    }
                }
            }
        }

       

        private void riceAlertEnabled_EditValueChanged(object sender, EventArgs e)
        {
            DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            if (dr != null)
            {
                CheckEdit CE = sender as CheckEdit;
                object oAlert_ID = dr["ID"];
                if (oAlert_ID != null && oAlert_ID != DBNull.Value)
                {
                    if (Convert.ToInt32(oAlert_ID) == 71 && CE.Checked)
                    {
                        Popup.ShowPopup("Only newly breached notifications will be sent.");
                        Connection.SQLExecutor.ExecuteNonQuery("exec PC_BudgetNotificationsTurnedOn", Connection.TRConnection);

                    }
                }
                
                string sUpdate = "update AlertPointRequired set required='" + CE.EditValue + "' where id=" + dr["ID1"];
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }
        }


        private void riProperties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            if (dr != null)
            {
                object oAlert_ID = dr["ID"];
                if (oAlert_ID != null && oAlert_ID != DBNull.Value)
                {
                    AlertProperties.fLauncher PL = new AlertProperties.fLauncher(Connection, DevXMgr, Convert.ToInt32(oAlert_ID)); 
                    PL.ShowDialog();
                }
            }
        }
        string sWarning = "Note* only new alerts will be emailed (previous alerts will not be sent).";
        string sWarningQues = "If you turn emailing back on for this alert at some point in the future, only new alerts will be emailed (previous alerts will not be sent). Are you sure you want to disable email for this alert?";
        
        private void riEmail_EditValueChanged(object sender, EventArgs e)
        {
            //DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            //if (dr != null)
            //{
            //    CheckEdit CE = sender as CheckEdit;
            //    string sUpdate = "update AlertPointRequired set Email='" + CE.EditValue + "' where id=" + dr["ID1"];
            //    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
                
            //}
        }

        private void riExtEmail_EditValueChanged(object sender, EventArgs e)
        {
            //DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            //if (dr != null)
            //{
            //    CheckEdit CE = sender as CheckEdit;
            //    string sUpdate = "update AlertPointRequired set ExtEmail='" + CE.EditValue + "' where id=" + dr["ID1"];
            //    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            //}
        }

        
        private void riEmail_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string sUpdate = "";
            bool bChange = false;
            DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            if (dr != null)
            {
               if ( Convert.ToBoolean(e.NewValue))
                {
                    Popup.ShowPopup(sWarning);
                    sUpdate = "update AlertStakeholder set Emailed = 'T' where ALERT_ID = " + dr["ID"];
                    sUpdate = @"update st
                        set st.Emailed = 'T' 
                        from AlertStakeholder st
                        join Alert a on a.ID=st.ALERT_ID
                        where a.AAP_ID = " + dr["ID"];
                    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);   
                    bChange = true;
                }
                else
                {
                    if (Popup.ShowPopup(sWarningQues, frmPopup.PopupType.Yes_No) == frmPopup.PopupResult.Yes)
                    {
                        bChange = true;
                    }
                    else
                    {
                        bChange = false;
                        e.Cancel = true;
                    }

                }
                if (bChange)
                {
                    sUpdate = "update AlertPointRequired set Email='" + e.NewValue + "' where id=" + dr["ID1"];
                    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
                }
            }
        }

        private void riExtEmail_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string sUpdate = "";
            bool bChange = false;
            DataRow dr = gvAlert.GetDataRow(gvAlert.FocusedRowHandle);
            if (dr != null)
            {
                if (Convert.ToBoolean(e.NewValue))
                {
                    Popup.ShowPopup(sWarning);
                    sUpdate = "update AlertStakeholder set EXT_EMAILED  = 'T' where ALERT_ID = " + dr["ID"];
                    sUpdate = @"update st
                        set st.EXT_EMAILED = 'T' 
                        from AlertStakeholder st
                        join Alert a on a.ID=st.ALERT_ID
                        where a.AAP_ID = " + dr["ID"];
                    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
                    bChange = true;
                }
                else
                {
                    if (Popup.ShowPopup(sWarningQues, frmPopup.PopupType.Yes_No) == frmPopup.PopupResult.Yes)
                    {
                        bChange = true;
                    }
                    else
                    {
                        bChange = false;
                        e.Cancel = true;
                    }

                }
                if (bChange)
                {
                    sUpdate = "update AlertPointRequired set ExtEmail='" + e.NewValue + "' where id=" + dr["ID1"];
                    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
                }
            }
        }
    }
}
