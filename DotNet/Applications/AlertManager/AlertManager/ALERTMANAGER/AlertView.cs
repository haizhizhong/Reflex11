using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using HMConnection;

namespace AlertManager
{
    public partial class AlertView : DevExpress.XtraEditors.XtraUserControl
    {
        #region AlertView Class Variables 

        public delegate void AlertDeleted_Delegate();
        public event AlertDeleted_Delegate AlertDeleted;

        public delegate void AlertSeen_Delegate();
        public event AlertSeen_Delegate AlertSeen;

        public delegate void AlertUnseen_Delegate();
        public event AlertUnseen_Delegate AlertUnseen;

        public delegate void AlertCompleted_Delegate();
        public event AlertCompleted_Delegate AlertCompleted;

        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private int AlertID = -1;

        #endregion

        public AlertView()
        {            
            InitializeComponent();
        }

        public HMCon HMConnection
        {
            set { this.Connection = value; }
            get { return this.Connection; }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUC_DevXMgr
        {
            set { DevXMgr = value; }
            get { return this.DevXMgr; }
        }

        public virtual void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            this.AlertID = AlertID;
            LoadAlert(AlertID);
            lblSubject.Text = Subject;
            lblModule.Text = Module;
            lblReceived.Text = string.Format( "{0:G}", Received );
            mNotes.Text = Notes;
        }

        private void LoadAlert(int AlertID)
        {
            string sSelect = "select isnull(viewed,'F') from alertstakeholder where alert_id=" + AlertID+" and  contact_id="+Connection.ContactID;
            object oResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            if (oResult != null && oResult != DBNull.Value)
            {
                if (oResult.Equals("T"))
                {
                    bbiMUnseen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    bbiMSeen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    bbiMUnseen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    bbiMSeen.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }

            bbiMarkCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            bbiCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            string sSQL = @"select isnull(ap.COMPLETABLE_TF,'F') [COMPLETABLE_TF], isnull(a.COMPLETED_TF,'F') [COMPLETED_TF]
                from Alert a
                join ApplicationAlertPoint ap on ap.ID=a.AAP_ID
                where a.ID=" + AlertID;
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        object oCompletable_tf = dr["COMPLETABLE_TF"];
                        if (oCompletable_tf == null || oCompletable_tf == DBNull.Value)
                            oCompletable_tf = "F";
                        object oCompleted_tf = dr["COMPLETED_TF"];
                        if (oCompleted_tf == null || oCompleted_tf == DBNull.Value)
                            oCompleted_tf = "F";
                        if (oCompletable_tf.Equals("F"))
                        {
                            bbiMarkCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            bbiCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        else
                        {
                            if (oCompleted_tf.Equals("F"))
                            {
                                bbiMarkCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                bbiCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                bbiMarkCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                bbiCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                        }
                    }
                }
            }
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sUpdate = "update alertstakeholder set deleted='T' where alert_id="+AlertID+" and contact_id="+Connection.ContactID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            
            if (AlertDeleted != null)
                AlertDeleted();
        }

        private void bbiMUnseen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sUpdate = "update alertstakeholder set viewed='F' where alert_id=" + AlertID + " and contact_id=" + Connection.ContactID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

            LoadAlert(AlertID);

            if (AlertUnseen != null)
                AlertUnseen();
        }

        private void bbiMSeen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sUpdate = "update alertstakeholder set viewed='T' where alert_id=" + AlertID + " and contact_id=" + Connection.ContactID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

            LoadAlert(AlertID);

            if (AlertSeen != null)
                AlertSeen();            
        }

        private void bbiMarkCompleted_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sUpdate = @"update Alert set COMPLETED_TF='T' where ID=" + AlertID;
            Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);

            LoadAlert(AlertID);

            if (AlertCompleted != null)
                AlertCompleted();

            //send off email to alert all other stakeholders that the alert has been completed
            string sSQL = @"select ISNULL(pr.email,0) [Email], ISNULL(pr.extemail,0) [ExtEmail] 
                from Alert a
                join ApplicationAlertPoint ap on ap.ID=a.AAP_ID
                join AlertPointRequired pr on pr.AAP_ID=ap.ID
                where a.ID=" + AlertID;
            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        object oEmail = dr["Email"];
                        if (oEmail == null || oEmail == DBNull.Value)
                            oEmail = "F";
                        object oExtEmail = dr["ExtEmail"];
                        if (oExtEmail == null || oExtEmail == DBNull.Value)
                            oExtEmail = "F";

                        if (oEmail.Equals("T"))
                        {
                        }
                        if (oExtEmail.Equals("T"))
                        {
                        }
                    }
                }
            }
        }

        private void AlertView_Load(object sender, EventArgs e)
        {
            if( DevXMgr != null )
                DevXMgr.FormInit(this);
        }
    }
}
