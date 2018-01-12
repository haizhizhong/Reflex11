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
    public partial class ucCommPurDocMerge : DevExpress.XtraEditors.XtraUserControl
    {

        #region Variables declared here
        private HMConnection.HMCon hmConn;
        private WS_Popups.frmPopup Popups;
        private TUC_HMDevXManager.TUC_HMDevXManager hmDevMgr;
        private TUC_GridG.GridG gGrid;

        private int AlertPointID;
        private int _DocMergeFunction_ID = 1;

        public int DocMergeFunction_ID
        {
            get { return _DocMergeFunction_ID; }
            set { _DocMergeFunction_ID = value; }
        }

        #endregion

        public ucCommPurDocMerge(HMConnection.HMCon pConn, TUC_HMDevXManager.TUC_HMDevXManager pDevMgr, int pAlertPointID)
        {
            InitializeComponent();
            hmConn = pConn;
            hmDevMgr = pDevMgr;
            AlertPointID = pAlertPointID;
            sqlConnTR.ConnectionString = hmConn.TRConnection;
            Popups = new WS_Popups.frmPopup(this.hmDevMgr);

        }

        private void SetupGridG()
        {
            gGrid = new TUC_GridG.GridG(hmConn.CompanyServer, hmConn.TRDB, gvTolerance, daPaymentTolerance, dsPaymentTolerance1);
            gGrid.AskBeforeDelete = false;
            gGrid.AllowTabCreateNewRecord = true;
            gGrid.AllowDelete += new TUC_GridG.GridG.AllowDeleteDelegate(gGrid_AllowDelete);
        }

        bool gGrid_AllowDelete(object sender, DataRow row)
        {
            return AllowDelete();
        }

        private bool AllowDelete()
        {
            bool doDeleting = false;
            try
            {

                if (this.Popups.ShowPopup(this, "Are you sure you want to delete?", "Confirmation", WS_Popups.frmPopup.PopupType.Yes_No)
                    == WS_Popups.frmPopup.PopupResult.Yes)
                {

                    doDeleting = true;
                }

            }
            catch (Exception ex)
            {
                doDeleting = false;
                Popups.ShowPopup("Error found in deleting process: " + ex.Message.ToString());
            }

            return doDeleting;

        }

        private void LoadommPurDocMerger()
        {
            LoadCommPurpose();
            LoadDocMergeFunc();

            daCommPurpose.SelectCommand.Parameters["@AlertID"].Value = AlertPointID;
            daCommPurpose.Fill(dsCommPurpose1);

            daDocMergeLibraryItems.SelectCommand.Parameters["@AlertID"].Value = AlertPointID;
            daDocMergeLibraryItems.Fill(dsDocMergeLibraryItems1);

            dsPaymentTolerance1.Clear();
            daPaymentTolerance.Fill(dsPaymentTolerance1);

        }
        private void LoadCommPurpose()
        {
            string sSQL = @"select ID, DESCRIPTION 
                from COMMUNICATION_PURPOSE
                order by DESCRIPTION";
            DataTable dtComPur = hmConn.SQLExecutor.ExecuteDataAdapter(sSQL, hmConn.WebConnection);
            riComPur.DataSource = dtComPur;
        }

        private void LoadDocMergeFunc()
        {
            string sSQL = @"select l.DocMergeLibrary_ID, l.MergeTemplateName
                from DocMergeLibrary l
                where l.DocMergeFunction_ID=" + _DocMergeFunction_ID + @" and (l.Company_ID=-1 or l.Company_ID=" + hmConn.CompanyID + @")
                order by l.MergeTemplateName";
            DataTable dtDocMerge = hmConn.SQLExecutor.ExecuteDataAdapter(sSQL, hmConn.WebConnection);
            riDocMerge.DataSource = dtDocMerge;
        }

        private void ucCommPurDocMerge_Load(object sender, EventArgs e)
        {
            hmDevMgr.FormInit(this);
            LoadommPurDocMerger();
            SetupGridG();
            SetupEmbeddedMergeComponent();
        }

        private ucMasterAgreeMergeDetail MasterMergeDet;
        private void SetupEmbeddedMergeComponent()
        {
            if (_DocMergeFunction_ID == 4) // Master Agreement Merge
            {
                MasterMergeDet = new ucMasterAgreeMergeDetail(bsDocMergeLibraryItems);
                MasterMergeDet.UpdateDocumentBindingSource += new ucMasterAgreeMergeDetail.UpdateBS_del(MasterMergeDet_UpdateDocumentBindingSource);
                MasterMergeDet.Dock = DockStyle.Fill;
                MasterMergeDet.Parent = tpDocumentMerg;

                gcDocMerge.Parent = MasterMergeDet;
            }
        }

        private void MasterMergeDet_UpdateDocumentBindingSource()
        {
            daDocMergeLibraryItems.Update(dsDocMergeLibraryItems1);
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
            Popups.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvComPur_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daCommPurpose.Update(dsCommPurpose1);
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

        private void gvDocMerge_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow dr = gvDocMerge.GetDataRow(gvDocMerge.FocusedRowHandle);
            if (dr != null)
            {
                dr["AlertID"] = AlertPointID;
            }
        }

        private void gvDocMerge_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popups.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvDocMerge_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daDocMergeLibraryItems.Update(dsDocMergeLibraryItems1);
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

        private void gcComPur_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popups.ShowPopup("Are you sure you want to delete the selected communication purpose record?", WS_Popups.frmPopup.PopupType.OK_Cancel) == WS_Popups.frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvComPur.GetDataRow(gvComPur.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oID = dr["ID"];
                        if (oID != null && oID != DBNull.Value)
                        {
                            string sSQL = @"delete from alert_land_reminder_com_purpose where ID=" + oID;
                            hmConn.SQLExecutor.ExecuteNonQuery(sSQL, hmConn.TRConnection);
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void gcDocMerge_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (Popups.ShowPopup("Are you sure you want to delete the selected library item record?", WS_Popups.frmPopup.PopupType.OK_Cancel) == WS_Popups.frmPopup.PopupResult.OK)
                {
                    DataRow dr = gvDocMerge.GetDataRow(gvDocMerge.FocusedRowHandle);
                    if (dr != null)
                    {
                        object oID = dr["ID"];
                        if (oID != null && oID != DBNull.Value)
                        {
                            string sSQL = @"delete from alert_land_reminder_docMerge where ID=" + oID;
                            hmConn.SQLExecutor.ExecuteNonQuery(sSQL, hmConn.TRConnection);
                        }
                    }
                }
                else
                {
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
                else if (Convert.ToInt32(oDaysPrior) <= 0)
                {
                    e.ErrorText = "Days prior value must be greater than zero.";
                    e.Valid = false;
                    return;
                }

                object oID = dr["id"];
                string sSelect = "select count(*) from alter_late_payment_tolerance where id<>" + oID + " and daysprior=" + oDaysPrior;
                if (Convert.ToInt32(hmConn.SQLExecutor.ExecuteScalar(sSelect, hmConn.TRConnection)) > 0)
                {
                    e.ErrorText = "A record with the same days prior already exists.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvTolerance_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popups.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
