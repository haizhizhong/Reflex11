using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace AlertProperties.LandDevelopment
{
    public partial class ucPayoutExtensionReminder : ucPropertyInterface
    {
        private ucCommPurDocMerge CommPurDocMerge;

        public ucPayoutExtensionReminder()
        {
            InitializeComponent();
        }

        private void LoadUserControls()
        {
            CommPurDocMerge = new ucCommPurDocMerge(Connection, DevXMgr, AlertPointID);
            CommPurDocMerge.Dock = DockStyle.Fill;
            CommPurDocMerge.Parent = pcGrid;
            CommPurDocMerge.tpTolerance.PageVisible = false;
        }

        private void ucPayoutExtensionReminder_Load(object sender, EventArgs e)
        {
            LoadUserControls();
            DevXMgr.FormInit(this);
        }

        

        private void ExecPayoutExtensionReminder()
        {
            int agreeID = 160; //_AgreementID;
            DateTime dtNew_date = System.DateTime.Today;
            string sSQL = "exec PC_PayoutExtensionReminder " + agreeID.ToString() + ", '" + dtNew_date.ToShortDateString()+"'" ;
            DataTable dtAlert = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);

            string startPath = Application.StartupPath + @"\tmp\PayoutExtensionReminder";
            int iAlertID;
            foreach (DataRow dr in dtAlert.Rows)
            {
                iAlertID = Convert.ToInt32(dr["alert_id"]);

                SqlParameter[] sqlparams = new SqlParameter[5];

                sqlparams[0] = new SqlParameter("@Agreement_ID", agreeID);
                sqlparams[1] = new SqlParameter("@SkipTransfer", DBNull.Value);
                sqlparams[2] = new SqlParameter("@MLUser", DBNull.Value);
                sqlparams[3] = new SqlParameter("@Solicitor_ID", DBNull.Value);
                sqlparams[4] = new SqlParameter("@SigName", DBNull.Value);

                bool bSuccess = true;
                try
                {
                    sSQL = @"declare @AAP_ID int select @AAP_ID = 45
                        select DocMergeLibrary_ID from alert_land_reminder_docMerge where AlertID=@AAP_ID";
                    DataTable dtDocLib = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                    if (dtDocLib != null)
                    {
                        foreach (DataRow drLib in dtDocLib.Rows)
                        {
                            object oDocMergeLibrary_ID = drLib["DocMergeLibrary_ID"];
                            if (oDocMergeLibrary_ID != null && oDocMergeLibrary_ID != DBNull.Value)
                            {
                                WordMerge2003.DocMergeElf elf = new WordMerge2003.DocMergeElf(Connection);
                                bSuccess = elf.MergeItem(iAlertID, Convert.ToInt32(oDocMergeLibrary_ID), startPath, sqlparams);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    bSuccess = false;
                    LogException(e, "ALERT NOTIFICATION SERVICE", Connection.WebConnection);
                }

                if (bSuccess)
                {
                    sSQL = @"update Alert set Complete_TF='T' where ID=" + iAlertID;
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                }
                else
                {
                    sSQL = @"exec AlertDelete " + iAlertID;
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                }
            }
        }

        private void LogException(Exception ex, string CmdText, string ConnStr)
        {
            string Username = "SERVER";

            string sInsert = "insert into SYSTEM_EXCEPTION ( USERNAME, COMPANYID, ERROR_DATE, COMMAND_TEXT, CONNECTION_STRING, EX_MESSAGE, EX_STACKTRACE ) " +
                "values ( '" + Username + "', " + Connection.CompanyID + ", getdate(), '" + CmdText.Replace("'", "''") + "', '" + ConnStr.Replace("'", "") + "', '" + ex.Message.Replace("'", "") + "', '" + ex.StackTrace.Replace("'", "") + "' )";
            SqlCommand cmd = new SqlCommand(sInsert, new SqlConnection(Connection.WebConnection));
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                    cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
        }

       
    }
}
