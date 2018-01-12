using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using HMConnection;
using System.Windows.Forms;
using System.Data;

namespace AlertWatcher
{
    public class Watcher : System.Windows.Forms.Control
    {
        #region Watcher Class Variables

        public delegate void SafeAlertPopupCallBack(string Title, string Subject, string Type, int ID);

        public event NewAlert_Delegate NewAlert;
        public delegate void NewAlert_Delegate();

        public event AlertClicked_Delegate AlertClicked;
        public delegate void AlertClicked_Delegate(int ID);

        private Form ParentForm;
        private HMCon Connection;
        private Thread WatchThread;
        private System.Timers.Timer WatchTimer;

        private const double CONST_TICK_INTERVAL_FIVE_SECONDS = 5000;
        private const double CONST_TICK_INTERVAL_THIRTY_SECONDS = 30000;

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.ComponentModel.IContainer components;
       
        #endregion

        public Watcher(HMCon Connection, Form f) 
        {
            this.Connection = Connection;
            this.ParentForm = f;
            this.Parent = ParentForm;

            InitializeComponent();
            
            WatchThread = new Thread(new ThreadStart(StartTimer));
            WatchThread.IsBackground = true;

            WatchTimer = new System.Timers.Timer(CONST_TICK_INTERVAL_FIVE_SECONDS); 
            WatchTimer.Elapsed += new System.Timers.ElapsedEventHandler(WatchTimer_Elapsed);
        }

        #region Component Initialization

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Watcher));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Lightning 1 16 h p.png");
            this.imageCollection1.Images.SetKeyName(1, "Stopwatch 16 h p.png");
            this.imageCollection1.Images.SetKeyName(2, "Light Orange Round 16 h p.png");
            // 
            // alertControl1
            // 
            this.alertControl1.AlertClick += new DevExpress.XtraBars.Alerter.AlertClickEventHandler(this.alertControl1_AlertClick);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        public void Start()
        {
            WatchThread.Start();
        }

        public void Stop()
        {
            WatchTimer.Stop();
            WatchThread.Abort();            
        }

        private void WatchTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                WatchTimer.Stop();
                WatchTimer.Interval = CONST_TICK_INTERVAL_THIRTY_SECONDS;
                LookForAlerts();
            }
            finally
            {
                WatchTimer.Start();
            }
        }

        private void StartTimer()
        {
            WatchTimer_Elapsed(null, null);
            WatchTimer.Start();
        }

        private void LookForAlerts()
        {
            string sSelect = @"select count(*) 
                from Alert a 
                join AlertStakeholder s on a.id=s.alert_id 
                where isnull(s.received,'F') = 'F' and isnull(a.Complete_TF,'F') = 'T' and s.contact_id=" + Connection.ContactID;
            object oResult = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            if( oResult != null )
            {
                int AlertCnt = Convert.ToInt32( oResult );
                if (AlertCnt > 0)
                {
                    if (AlertCnt > 1)
                    {
                        SafePopupAlert(AlertCnt + " NEW ALERTS", "", "", -1);
                    }
                    else
                    {
                        sSelect = @"select Type, Module, Subject, a.ID 
                            from Alert a 
                            join AlertStakeholder s on a.id=s.alert_id 
                            where isnull(s.received,'F') = 'F' and isnull(a.Complete_TF,'F') = 'T' and s.contact_id=" + Connection.ContactID;
                        DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
                        if (dt != null)
                        {
                            DataRow dr = dt.Rows[0];
                            if (dr != null)
                            {
                                object oType = dr[0];
                                object oModule = dr[1];
                                object oSubject = dr[2];
                                object oID = dr[3];
                                string Type = "";
                                string Module = "";
                                string Subject = "";
                                int ID = -1;

                                if (oType != null && oType != DBNull.Value)
                                {
                                    Type = oType.ToString();
                                }

                                if (oModule != null && oModule != DBNull.Value)
                                {
                                    Module = oModule.ToString();
                                }

                                if (oSubject != null && oSubject != DBNull.Value)
                                {
                                    Subject = oSubject.ToString();
                                }

                                if (oID != null && oID != DBNull.Value)
                                {
                                    ID = Convert.ToInt32(oID);
                                }

                                SafePopupAlert(Module, Subject, Type, ID);
                            }
                        }
                    }
                    string sUpdate = @"update s
                        set s.received='T'
                        from alertstakeholder s 
                        join alert a on a.ID=s.ALERT_ID
                        where s.received='F' and isnull(a.Complete_TF,'F') = 'T' and s.contact_id=" + Connection.ContactID;
                    Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
                }
            }            
        }

        private void SafePopupAlert(string Title, string Subject, string Type, int ID)
        {
            if (this.InvokeRequired)
            {
                SafeAlertPopupCallBack del = new SafeAlertPopupCallBack(SafePopupAlert);
                this.Invoke(del, new object[] { Title, Subject, Type, ID });
            }
            else
            {
                try
                {
                    int ImageID = 2;
                    if (Type.Equals("D"))
                        ImageID = 0;
                    else if (Type.Equals("S"))
                        ImageID = 1;
                    alertControl1.Show(ParentForm, Title, Subject, Subject, imageCollection1.Images[ImageID], ID);
                    if (NewAlert != null)
                        NewAlert();
                }
                catch { }
            }
        }

        private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {            
            ParentForm.WindowState = FormWindowState.Maximized;
            ParentForm.BringToFront();
            if (AlertClicked != null)
                AlertClicked(Convert.ToInt32(e.Info.Tag));
        }

    }

}
