using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.CommonComponents
{
    public partial class ucStakeholderType : DevExpress.XtraEditors.XtraUserControl
    {

        /*
         #################################################################
         * Use the following procedure to fill in the contacts when sending the alert
         * 
         * PC_Alert_PC_Stakeholder_Insert(@Alert_ID int,@pri_id int,@mluser varchar(10))
         * 
         #################################################################
         */
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private int _AlertPoint = -1;
        private TUC_GridG.GridG gGrid;

        public ucStakeholderType(HMConnection.HMCon con,TUC_HMDevXManager.TUC_HMDevXManager tuc,int AlertPoint)
        {
            InitializeComponent();
            Connection = con;
            DevXMgr = tuc;
            _AlertPoint = AlertPoint;
            TR_Conn.ConnectionString = Connection.TRConnection;
            daAlert_PC_Stakeholder.SelectCommand.Parameters["@AlertID"].Value = _AlertPoint;

        }

        private void ucStakeholderType_Load(object sender, EventArgs e)
        {
            Popup = new WS_Popups.frmPopup(DevXMgr);

            try
            {
                dsPC_StakeholderType_Lookup1.Clear();
                daPC_StakeholderType_Lookup.Fill(dsPC_StakeholderType_Lookup1);
                dsAlert_PC_Stakeholder1.Clear();
                daAlert_PC_Stakeholder.Fill(dsAlert_PC_Stakeholder1);
            }
            catch (Exception ex)
            {
                Popup.ShowPopup("Error loading lookup's : " + ex.Message);
            }
            gGrid = new TUC_GridG.GridG(Connection.CompanyServer, Connection.TRDB, gvStakeholderType, daAlert_PC_Stakeholder, dsAlert_PC_Stakeholder1);
            gGrid.AskBeforeDelete = false;
            gGrid.AllowTabCreateNewRecord = true;
            gGrid.AllowDelete += new TUC_GridG.GridG.AllowDeleteDelegate(gGrid_AllowDelete);
   
            DevXMgr.FormInit(this);
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

                if (this.Popup.ShowPopup(this, "Are you sure you want to delete?", "Confirmation", WS_Popups.frmPopup.PopupType.Yes_No)
                    == WS_Popups.frmPopup.PopupResult.Yes)
                {

                    doDeleting = true;
                }

            }
            catch (Exception ex)
            {
                doDeleting = false;
                Popup.ShowPopup("Error found in deleting process: " + ex.Message.ToString());
            }

            return doDeleting;

        }

        private void gvStakeholderType_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DataRow row = gvStakeholderType.GetDataRow(e.RowHandle);
            if (row != null)
            {
                row[colAlert_ID.FieldName] = _AlertPoint;
            }
        }

        private void gvStakeholderType_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvStakeholderType.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                if (dr[colPC_Stakeholder_Type_ID.FieldName] == null || dr[colPC_Stakeholder_Type_ID.FieldName] == DBNull.Value)
                {
                    e.ErrorText = "Stakeholder type is required.";
                    e.Valid = false;
                    return;
                }

                object oID = dr["id"];
                string sSelect = "select count(*) from Alert_PC_Stakeholder where id <>" + dr[colID.FieldName].ToString() + " and Alert_ID =" + dr[colAlert_ID.FieldName].ToString()+" and PC_Stakeholder_Type_ID = "+ dr[colPC_Stakeholder_Type_ID.FieldName].ToString();
                if (Convert.ToInt32(Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection)) > 0)
                {
                    e.ErrorText = "Stakeholder type has already been selected.";
                    e.Valid = false;
                    return;
                }
            }
        }

        private void gvStakeholderType_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
