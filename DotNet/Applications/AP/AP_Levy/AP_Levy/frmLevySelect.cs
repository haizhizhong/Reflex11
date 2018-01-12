using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace AP_Levy
{
    public partial class frmLevySelect : DevExpress.XtraEditors.XtraForm
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private ComboSearch.Util CBOUtil;
        private int AP_INV_HEADER_ID;
        private int Supplier_ID;
        private bool MiscSupplier = false;

        public delegate void Delegate_PurchaserRemit(Purchaser purchaser);
        public event Delegate_PurchaserRemit PurchaserRemit;

        public frmLevySelect(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AP_INV_HEADER_ID)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;            
            this.AP_INV_HEADER_ID = AP_INV_HEADER_ID;
            CBOUtil = new ComboSearch.Util(DevXMgr);
            Popup = new WS_Popups.frmPopup(DevXMgr);

            string sSQL = @"select supplier_id 
                from AP_INV_HEADER a 
                join SUPPLIER_MASTER s on s.SUPPLIER=a.SUPPLIER
                where a.AP_INV_HEADER_ID=" + AP_INV_HEADER_ID;
            object oSupplier_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oSupplier_ID == null || oSupplier_ID == DBNull.Value)
                oSupplier_ID = -1;
            Supplier_ID = Convert.ToInt32(oSupplier_ID);

            sSQL = @"select count(*) 
                from supplier_master 
                where isnull(one_check,'F')='T' and supplier_id="+Supplier_ID;
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;

            if (Convert.ToInt32(oCNT) == 1)
                MiscSupplier = true;

            InitializeComponent();
            daLevySearch.SelectCommand.Parameters["@username"].Value = Connection.MLUser;
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;

            daProject.Fill(dsProject1);
            daLevy.Fill(dsLevy1);
            daLevyType.Fill(dsLevyType1);
            daPurchaser.Fill(dsPurchaser1);
        }

        private void frmLevySelect_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            object oActiveOnly = (chkActive.EditValue == null || chkActive.EditValue == DBNull.Value) ? DBNull.Value : chkActive.EditValue;
            object oPri_id = (lueProjNo.EditValue == null || lueProjNo.EditValue == DBNull.Value) ? DBNull.Value : lueProjNo.EditValue;
            object oPri_id2 = (lueProject.EditValue == null || lueProject.EditValue == DBNull.Value) ? DBNull.Value : lueProject.EditValue;
            object oPurchaser_ID = (luePurchaser.EditValue == null || luePurchaser.EditValue == DBNull.Value) ? DBNull.Value : luePurchaser.EditValue;
            object oMasterAgreeNo = (cboMasterAgreement.EditValue == null || cboMasterAgreement.EditValue == DBNull.Value || cboMasterAgreement.EditValue.ToString().Trim().Equals("")) ? DBNull.Value : cboMasterAgreement.EditValue;
            object oAgreeNo = (cboAgreement.EditValue == null || cboAgreement.EditValue == DBNull.Value || cboAgreement.EditValue.ToString().Trim().Equals("")) ? DBNull.Value : cboAgreement.EditValue;
            object oLot_num = (cboLot.EditValue == null || cboLot.EditValue == DBNull.Value || cboLot.EditValue.ToString().Trim().Equals("")) ? DBNull.Value : cboLot.EditValue;
            object oBlock_num = (cboBlock.EditValue == null || cboBlock.EditValue == DBNull.Value || cboBlock.EditValue.ToString().Trim().Equals("")) ? DBNull.Value : cboBlock.EditValue;
            object oPlan_num = (cboPlan.EditValue == null || cboPlan.EditValue == DBNull.Value || cboPlan.EditValue.ToString().Trim().Equals("")) ? DBNull.Value : cboPlan.EditValue;
            object oLevy_ID = (lueLevy.EditValue == null || lueLevy.EditValue == DBNull.Value) ? DBNull.Value : lueLevy.EditValue;
            object oLevyType_ID = (lueLevyType.EditValue == null || lueLevyType.EditValue == DBNull.Value) ? DBNull.Value : lueLevyType.EditValue;

            using (SqlConnection sqlcon = new SqlConnection(Connection.TRConnection))
            {
                string sSQL = @"AP_LevySelect";
                SqlCommand cmd = new SqlCommand(sSQL, sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter pUsername = new SqlParameter("@username", Connection.MLUser);
                SqlParameter pActiveOnly = new SqlParameter("@activeOnly", oActiveOnly);
                SqlParameter pPri_id = new SqlParameter("@pri_id", oPri_id);
                SqlParameter pPri_id2 = new SqlParameter("@pri_id2", oPri_id2);
                SqlParameter pPurchaser_ID = new SqlParameter("@purchaser_id", oPurchaser_ID);
                SqlParameter pMasterAgreeNo = new SqlParameter("@masterAgreeNo", oMasterAgreeNo);
                SqlParameter pAgreeNo = new SqlParameter("@agreeNo", oAgreeNo);
                SqlParameter pLot_num = new SqlParameter("@lot_num", oLot_num);
                SqlParameter pBlock_num = new SqlParameter("@block_num", oBlock_num);
                SqlParameter pPlan_num = new SqlParameter("@plan_num", oPlan_num);
                SqlParameter pLevy_ID = new SqlParameter("@Levy_ID", oLevy_ID);
                SqlParameter pLevyType_ID = new SqlParameter("@LevyType_ID", oLevyType_ID);
                SqlParameter pSupplier_id = new SqlParameter("@supplier_id", Supplier_ID);
                SqlParameter pAP_INV_HEADER_ID = new SqlParameter("@ap_inv_header_id", AP_INV_HEADER_ID);

                cmd.Parameters.Add(pUsername);
                cmd.Parameters.Add(pActiveOnly);
                cmd.Parameters.Add(pPri_id);
                cmd.Parameters.Add(pPri_id2);
                cmd.Parameters.Add(pPurchaser_ID);
                cmd.Parameters.Add(pMasterAgreeNo);
                cmd.Parameters.Add(pAgreeNo);
                cmd.Parameters.Add(pLot_num);
                cmd.Parameters.Add(pBlock_num);
                cmd.Parameters.Add(pPlan_num);
                cmd.Parameters.Add(pLevy_ID);
                cmd.Parameters.Add(pLevyType_ID);
                cmd.Parameters.Add(pSupplier_id);
                cmd.Parameters.Add(pAP_INV_HEADER_ID);

                try
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    dsLevySearch1.Clear();
                    daLevySearch.Fill(dsLevySearch1);
                    gvLevy.BestFitColumns();
                }
                catch (Exception)
                {
                    Popup.ShowPopup("Error running search.");
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chkActive.Checked = false;
            lueProjNo.EditValue = null;
            lueProject.EditValue = null;
            luePurchaser.EditValue = null;
            cboMasterAgreement.EditValue = null;
            cboAgreement.EditValue = null;
            cboLot.EditValue = null;
            cboBlock.EditValue = null;
            cboPlan.EditValue = null;
            lueLevy.EditValue = null;
            lueLevyType.EditValue = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sSQL = @"select count(*) from AP_LevySelect_Working where selected=1 and username='"+Connection.MLUser+@"'";
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;

            if (Convert.ToInt32(oCNT) == 0)
            {
                Popup.ShowPopup("No Levies have been selected for matching.");
                return;
            }
            
            if (MiscSupplier)
            {
                sSQL = @"select count(distinct isnull(Purchaser_ID,-1)) 
                    from AP_LevySelect_Working 
                    where selected=1 and username='" + Connection.MLUser + @"'";
                oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oCNT == null || oCNT == DBNull.Value)
                    oCNT = 0;

                if (Convert.ToInt32(oCNT) > 1)
                {
                    Popup.ShowPopup("When selecting multiple levies, the same purchaser must exist on all of them.");
                    return;
                }


                sSQL = @"select distinct isnull(Purchaser_ID,-1) 
                    from AP_LevySelect_Working 
                    where selected=1 and username='" + Connection.MLUser + @"'";
                object oPurchaser_ID = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
                if (oPurchaser_ID == null || oPurchaser_ID == DBNull.Value)
                    oPurchaser_ID = -1;

                if (PurchaserRemit != null)
                    PurchaserRemit(new Purchaser(Connection, Convert.ToInt32(oPurchaser_ID)));
            }

            // Validate levies selected have valid levels for the project they exist on 


            DialogResult = DialogResult.OK;
        }

        private void gvLevy_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daLevySearch.Update(dsLevySearch1);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            DataRow dr;
            for (int i = 0; i < gvLevy.RowCount; i++)
            {
                dr = gvLevy.GetDataRow(i);
                if (dr != null)
                {
                    object oSelected = dr["selected"];
                    if (oSelected == null || oSelected == DBNull.Value)
                        oSelected = false;

                    if (!Convert.ToBoolean(oSelected))
                    {
                        bool bValid = ValidCostCodes(dr["proj_lot_agreement_levy_id"]);
                        if (bValid)
                        {
                            dr.BeginEdit();
                            dr["selected"] = true;
                            dr.EndEdit();
                        }
                    }
                }
            }
            daLevySearch.Update(dsLevySearch1);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            string sSQL = @"update AP_LevySelect_Working set selected=0 where username='" + Connection.MLUser + @"'";
            Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);

            dsLevySearch1.Clear();
            daLevySearch.Fill(dsLevySearch1);
        }

        private void cboMasterAgreement_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, true,
                    @"select distinct MASTER_AGREEMENT_NUM from PROJ_LOT_MASTER_AGREEMENT where MASTER_AGREEMENT_NUM like % order by MASTER_AGREEMENT_NUM", "TR");
            }
        }

        private void cboAgreement_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, true,
                    @"select distinct agreement_num from PROJ_LOT_AGREEMENT where agreement_num like % order by agreement_num", "TR");
            }
        }

        private void cboLot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, false,
                    @"select distinct lot_num from PROJ_LOT where lot_num like % order by lot_num", "TR");
            }
        }

        private void cboBlock_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, false,
                    @"select distinct block_num from PROJ_LOT where block_num like % order by block_num", "TR");
            }
        }

        private void cboPlan_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                CBOUtil.loadDropDown(sender as DevExpress.XtraEditors.ComboBoxEdit, Connection, false,
                    @"select distinct plan_num from PROJ_LOT where plan_num like % order by plan_num", "TR");
            }
        }

        private void riSelect_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DataRow dr = gvLevy.GetFocusedDataRow();
            if( dr != null)
            {
                object oSelect = e.NewValue;
                if (oSelect == null || oSelect == DBNull.Value)
                    oSelect = false;
                if (Convert.ToBoolean(oSelect))
                {
                    bool bValid = ValidCostCodes(dr["proj_lot_agreement_levy_id"]);

                    if (!bValid)
                    {
                        Popup.ShowPopup("Unable to select the levy. The cost codes setup on the levy currently do not exist within the land project.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private bool ValidCostCodes(object proj_lot_agreement_levy_id)
        {
            string sSQL = @"select COUNT(*)
                from PROJ_LOT_AGREEMENT_LEVY al 
                join PROJ_LOT_AGREEMENT a on a.agreement_id=al.agreement_id
                left outer join costing_budget c on c.PRI_ID=a.pri_id and c.lv1ID=al.lv1id and isnull(c.lv2id,-1)=isnull(al.lv2id,-1)
                    and isnull(c.lv3id,-1)=isnull(al.lv3id,-1) and isnull(c.lv4id,-1)=isnull(al.lv4id,-1) and c.lem_comp='S'
                where al.id=" + proj_lot_agreement_levy_id + @" and c.id is null";
            object oCNT = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oCNT == null || oCNT == DBNull.Value)
                oCNT = 0;
            if (Convert.ToInt32(oCNT) == 1)
            {
                return false;
            }

            return true;
        }
    }
}