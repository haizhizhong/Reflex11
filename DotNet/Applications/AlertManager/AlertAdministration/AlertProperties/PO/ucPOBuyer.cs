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

namespace AlertProperties.PO
{
    public partial class ucPOBuyer : ucPropertyInterface
    {
        private Rectangle dragBox;

        public ucPOBuyer()
        {
            InitializeComponent();            
        }

        #region GCRec Drag Drop

        private void gcRec_DragDrop(object sender, DragEventArgs e)
        {
            if (gvBuyer.FocusedRowHandle < 0)
                return;

            if (e.Data.GetDataPresent(typeof(object[])))
            {
                object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
                if (oDrop != null)
                {
                    if (oDrop[0].ToString() == "User")
                    {
                        object oAlert_buyer_po_id = gvBuyer.GetFocusedRowCellValue("id");
                        if (oAlert_buyer_po_id == null || oAlert_buyer_po_id == DBNull.Value)
                            return;

                        ArrayList alRow = new ArrayList();
                        int[] iRows = oDrop[1] as int[];
                        DataRow dr = null;
                        for (int i = 0; i < iRows.Length; i++)
                        {
                            dr = gvUser.GetDataRow(iRows[i]);
                            object oUser = dr["name"];
                            dsAlertUsers1.Tables[0].Rows.Add(new object[] { null, oAlert_buyer_po_id, oUser });
                            alRow.Add(dr);
                        }
                        for (int i = 0; i < alRow.Count; i++)
                        {
                            dsUser1.Tables[0].Rows.Remove(alRow[i] as DataRow);
                        }
                        BindingContext[dsAlertUsers1, "alert_buyer_po_users"].EndCurrentEdit();
                        daAlertUsers.Update(dsAlertUsers1);
                    }                    
                }
            }
        }

        private void gcRec_DragEnter(object sender, DragEventArgs e)
        {
            if (gvBuyer.FocusedRowHandle >= 0)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void gcRec_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvRec.CalcHitInfo(e.X, e.Y);
            if (hitInfo.InRow)
            {
                Size dragSize = SystemInformation.DragSize;
                this.dragBox = new Rectangle(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
            }
        }

        private void gcRec_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (dragBox != Rectangle.Empty && !dragBox.Contains(pt))
            {
                DoRecDrag();
                dragBox = Rectangle.Empty;
            }
        }

        private void DoRecDrag()
        {
            if (gvRec.RowCount > 0)
            {
                int[] iRows = gvRec.GetSelectedRows();
                if (iRows != null)
                {
                    gvRec.GridControl.DoDragDrop(new object[] { "Rec", iRows }, DragDropEffects.All);
                }
            }
        }

        #endregion

        #region USER DRAG DROP

        private void gcUser_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(object[])))
            {
                object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
                if (oDrop != null)
                {
                    if (oDrop[0].ToString() == "Rec")
                    {
                        ArrayList alRow = new ArrayList();
                        int[] iRows = oDrop[1] as int[];
                        DataRow dr = null;
                        for (int i = 0; i < iRows.Length; i++)
                        {
                            dr = gvRec.GetDataRow(iRows[i]);
                            object oUSER = dr["mluser"];
                            string sSQL = @"select full_name, department from mluser where name='"+oUSER+@"'";
                            DataTable dt = Connection.SQLExecutor.ExecuteDataAdapter(sSQL, Connection.TRConnection);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow drUser = dt.Rows[0];
                                    if (drUser != null)
                                    {
                                        dsUser1.Tables[0].Rows.Add(new object[] { oUSER, drUser["full_name"], drUser["department"] });
                                        alRow.Add(dr);
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < alRow.Count; i++)
                        {
                            (alRow[i] as DataRow).Delete();
                        }
                        BindingContext[dsAlertUsers1, "alert_buyer_po_users"].EndCurrentEdit();
                        daAlertUsers.Update(dsAlertUsers1);
                    }
                }
            }
        }

        private void gcUser_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(object[])))
            {
                object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
                if (oDrop != null)
                {
                    if (oDrop[0].ToString() == "Rec")
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
        }

        private void gcUser_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvUser.CalcHitInfo(e.X, e.Y);
            if (hitInfo.InRow)
            {
                Size dragSize = SystemInformation.DragSize;
                this.dragBox = new Rectangle(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
            }
        }

        private void gcUser_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (dragBox != Rectangle.Empty && !dragBox.Contains(pt))
            {
                DoUserDrag();
                dragBox = Rectangle.Empty;
            }
        }

        private void DoUserDrag()
        {
            if (gvUser.RowCount > 0)
            {
                int[] iRows = gvUser.GetSelectedRows();
                if (iRows != null)
                {
                    gvUser.GridControl.DoDragDrop(new object[] { "User", iRows }, DragDropEffects.All);
                }
            }
        }

        #endregion

        private void ucPOBuyer_Load(object sender, EventArgs e)
        {
            TR_Conn.ConnectionString = Connection.TRConnection;

            daUser.SelectCommand.Parameters["@id"].Value = -1;
            daUser.Fill(dsUser1);

            daAlertBuyer.Fill(dsAlertBuyer1);
            daBuyer.Fill(dsBuyer1);            
            daAllUsers.Fill(dsAllUsers1);
        }

        private void gvBuyer_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            daAlertBuyer.Update(dsAlertBuyer1);
        }

        private void gcBuyer_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                object oID = gvBuyer.GetDataRow(gvBuyer.FocusedRowHandle)["id"];
                if (oID == null || oID == DBNull.Value)
                {
                    e.Handled = true;
                    return;
                }

                if (Popup.ShowPopup("Are you sure you want to remove the selected buyer?", WS_Popups.frmPopup.PopupType.OK_Cancel) ==
                    frmPopup.PopupResult.OK)
                {
                    string sSQL = @"delete from alert_buyer_po_users where alert_buyer_po_id=" + oID + @"
                    delete from alert_buyer_po where id=" + oID;
                    Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }
        }

        private void gvBuyer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dsAlertUsers1.Clear();
            dsUser1.Clear();

            DataRow dr = gvBuyer.GetDataRow(gvBuyer.FocusedRowHandle);
            if (dr != null)
            {
                daAlertUsers.SelectCommand.Parameters["@id"].Value = dr["id"];
                daAlertUsers.Fill(dsAlertUsers1);
                daUser.SelectCommand.Parameters["@id"].Value = dr["id"];
                daUser.Fill(dsUser1);
            }
            else
            {
                daUser.SelectCommand.Parameters["@id"].Value = -1;
                daUser.Fill(dsUser1);
            }
        }

        private void gvBuyer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvBuyer.GetDataRow(gvBuyer.FocusedRowHandle);
            if (dr != null)
            {
                object oBuyer = dr["buyer"];
                if (oBuyer == null || oBuyer == DBNull.Value)
                {
                    e.ErrorText = "Buyer is required.";
                    e.Valid = false;
                    return;
                }

                object oID = dr["id"];
                if( oID == null || oID == DBNull.Value )
                    oID = -1;

                string sSQL = @"select dbo.fn_Alert_ValidateBuyer("+oID+@", '"+oBuyer+@"')";
                if( !Convert.ToBoolean( Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection) ) )
                {
                    e.ErrorText = "The selected buyer already exists.";
                    e.Valid = false;
                    return;
                }
                
            }
        }

        private void gvBuyer_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            Popup.ShowPopup(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

    }
}
