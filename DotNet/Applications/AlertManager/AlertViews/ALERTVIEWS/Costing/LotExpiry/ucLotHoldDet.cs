using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HMConnection;

namespace AlertViews.Costing.LotExpiry
{
    public partial class ucLotHoldDet : DevExpress.XtraEditors.XtraUserControl
    {
        private HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private int _DetailID = -1;

        public ucLotHoldDet()
        {
            InitializeComponent();
        }

        public HMCon HMConnection
        {
            set
            {
                Connection = value;
            }
            get
            {
                return Connection;
            }
        }

        public TUC_HMDevXManager.TUC_HMDevXManager TUC_DevXMgr
        {
            set
            {
                DevXMgr = value;
            }
            get
            {
                return DevXMgr;
            }
        }

        public void LoadDetail(int DetailID)
        {
            _DetailID = DetailID;
            ClearFields();

            string sSQL = @"if exists (
                select * from proj_lot_hold h
                join proj_lot l on l.proj_lot_id=h.proj_lot_id
                join proj_header p on p.pri_id=l.pri_id
                where h.hold_id="+DetailID +@")
                    select 1
                else 
                    select 0 ";
            object obj = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (Convert.ToInt32(obj) == 1)
                layoutControl1.Visible = true;
            else
            {
                layoutControl1.Visible = false;
                return;
            }

            string sSelect = "select c.Communities, p.pri_name [ProjectName], cu.NAME [Purchaser], l.block_num, l.lot_num, l.plan_num, "+
                "isnull(h.price_quoted,0) [QuotedPrice],  h.end_date, l.long_legal, l.parcel_number, l.title_number, "+
                "isnull(l.frontage,0) [Frontage], ISNULL(l.depth,0) [Depth], ISNULL(l.area_ft,0) [SqFt], ISNULL(l.area_acres,0) [Acres], "+
                "ISNULL(l.area_metre,0) [SqMt], ISNULL(l.area_hectares,0) [Hectares], ISNULL(l.corner,'F') [Corner], z.Zone, s.Shape, "+
                "ISNULL(l.curb_treatment,'F') [Curb], ISNULL(l.rear_side_treatment,'F') [RearSide], g.Description [Grading], "+
                "b.Description [BuildType], o.Description [Orientation], a.Description [Amenity], lph.lot_reference [LotPairRef] " +
                "from PROJ_LOT_HOLD h "+
                "join PROJ_LOT l on l.proj_lot_id=h.proj_lot_id "+
                "join PROJ_HEADER p on p.pri_id=l.pri_id "+
                "left outer join PROJ_DEV_INFO d on d.pri_id=p.pri_id "+
                "left outer join LD_Communities c on c.Communities_ID=d.COMMUNITIES_ID "+
                "left outer join CUSTOMERS cu on cu.CUSTOMER_ID=h.purchaser_id "+
                "left outer join LD_Zoning z on z.Zoning_ID=l.zoning_id "+
                "left outer join LD_Shapes s on s.Shapes_ID=l.shapes_id "+
                "left outer join LD_Grading g on g.Grading_ID=l.grading_id "+
                "left outer join LD_Build_Type b on b.BuildType_ID=l.buildtype_id "+
                "left outer join LD_Orientation o on o.Orientation_ID=l.orientation_id "+
                "left outer join LD_Amenity a on a.Amenity_ID=l.amenity_id "+
                "left outer join LD_Lot_Pair_Det lpd on lpd.proj_lot_id = l.proj_lot_id "+
                "left outer join LD_Lot_Pair_Hdr lph on lph.Lot_Pair_ID=lpd.Lot_Pair_ID "+
                "where h.hold_id=" + DetailID;

            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSelect, Connection.TRConnection);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr != null)
                    {
                        txtCommunity.EditValue = dr["Communities"];
                        txtProject.EditValue = dr["ProjectName"];
                        txtPurchaser.EditValue = dr["Purchaser"];
                        txtBlock.EditValue = dr["block_num"];
                        txtLot.EditValue = dr["lot_num"];
                        txtPlan.EditValue = dr["plan_num"];
                        txtPrice.EditValue = dr["QuotedPrice"];
                        deExpiry.EditValue = dr["end_date"];
                        txtLongLegal.EditValue = dr["long_legal"];
                        txtParcel.EditValue = dr["parcel_number"];
                        txtTitle.EditValue = dr["title_number"];
                        txtFrontage.EditValue = dr["Frontage"];
                        txtDepth.EditValue = dr["Depth"];
                        txtSqFt.EditValue = dr["SqFt"];
                        txtAcres.EditValue = dr["Acres"];
                        txtSqMt.EditValue = dr["SqMt"];
                        txtHectares.EditValue = dr["Hectares"];
                        chkCorner.EditValue = dr["Corner"];
                        txtZoning.EditValue = dr["Zone"];
                        txtShape.EditValue = dr["Shape"];
                        chkCurb.EditValue = dr["Curb"];
                        chkRearSide.EditValue = dr["RearSide"];
                        txtGrading.EditValue = dr["Grading"];
                        txtBuildType.EditValue = dr["BuildType"];
                        txtOrientation.EditValue = dr["Orientation"];
                        txtAmenity.EditValue = dr["Amenity"];
                        txtPairing.EditValue = dr["LotPairRef"];
                    }
                }
            }
        }

        private void ClearFields()
        {
            txtCommunity.EditValue = null;
            txtProject.EditValue = null;
            txtPurchaser.EditValue = null;
            txtBlock.EditValue = null;
            txtLot.EditValue = null;
            txtPlan.EditValue = null;
            txtPrice.EditValue = null;
            deExpiry.EditValue = null;
            txtLongLegal.EditValue = null;
            txtParcel.EditValue = null;
            txtTitle.EditValue = null;
            txtFrontage.EditValue = null;
            txtDepth.EditValue = null;
            txtSqFt.EditValue = null;
            txtAcres.EditValue = null;
            txtSqMt.EditValue = null;
            txtHectares.EditValue = null;
            chkCorner.EditValue = null;
            txtZoning.EditValue = null;
            txtShape.EditValue = null;
            chkCurb.EditValue = null;
            chkRearSide.EditValue = null;
            txtGrading.EditValue = null;
            txtBuildType.EditValue = null;
            txtOrientation.EditValue = null;
            txtAmenity.EditValue = null;
            txtPairing.EditValue = null;
        }

        private void ucLotHoldDet_Load(object sender, EventArgs e)
        {
            if (DevXMgr != null)
                DevXMgr.FormInit(this);
        }

        private void lblMessage_Paint(object sender, PaintEventArgs e)
        {
            Label l = sender as Label;
            Graphics g = e.Graphics;
            g.DrawString("The Selected Lot Hold Has Been Deleted.", new Font("Tahoma", 8, FontStyle.Bold), new SolidBrush(Color.Black), (l.Width / 2) - 115, (l.Height / 2) - 12);
        }
    }
}
