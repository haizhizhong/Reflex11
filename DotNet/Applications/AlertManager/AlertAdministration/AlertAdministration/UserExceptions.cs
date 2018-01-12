using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.Collections;
using WS_Popups;
using HMConnection;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace AlertAdministration
{
    public partial class UserExceptions : DevExpress.XtraEditors.XtraForm
    {
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private frmPopup Popup;
        private HMCon Connection;
        private int AlertPointID;
        private Rectangle dragBox;

        public UserExceptions(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, int AlertPointID)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            this.AlertPointID = AlertPointID;
            Popup = new frmPopup(DevXMgr);
            InitializeComponent();
            TR_Conn.ConnectionString = this.Connection.TRConnection;
            WEB_Conn.ConnectionString = this.Connection.WebConnection;

            string sSelect = "select isnull(alert,'') from ApplicationAlertPoint where id=" + AlertPointID;
            this.Text = "Alert: "+Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);

            daUser.SelectCommand.CommandText = "SELECT u.NAME, d.DESCRIPTION " +
                "FROM MLUSER u " +
                "LEFT OUTER JOIN DEPARTMENT d ON d.NAME = u.DEPARTMENT " +
                "where u.name not in ( select mluser from AlertPointAccess where AAP_ID=" + AlertPointID + " ) " +
                "ORDER BY d.DESCRIPTION, u.NAME";

            daExceptions.SelectCommand.Parameters["@aap_id"].Value = AlertPointID;
            daExceptions.Fill(dsExceptions1);
            daDepartment.Fill(dsDepartment1);
            daUser.Fill(dsUser1);
            daUserDep.Fill(dsUserDep1);            
        }

        private void RefreshGrids()
        {
            dsExceptions1.Clear();
            dsUser1.Clear();
            daExceptions.Fill(dsExceptions1);
            daUser.Fill(dsUser1);
        }

        private void UserExceptions_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        #region GCException DRAG DROP

        private void gcException_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(object[]))) 
			{
				object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
				if( oDrop != null )
				{
					if( oDrop[0].ToString() == "User" )
					{
						ArrayList alRow = new ArrayList();
						int[] iRows = oDrop[1] as int[];
						DataRow dr = null;
						for( int i = 0; i < iRows.Length; i++ )
						{
							dr = gvUser.GetDataRow( iRows[i] );
							object oUser = dr["NAME"];
                            dsExceptions1.Tables[0].Rows.Add(new object[] { null, AlertPointID, oUser });
							alRow.Add( dr );
						}	
						for( int i = 0; i < alRow.Count; i++ )
						{
							dsUser1.Tables[0].Rows.Remove(alRow[i] as DataRow);							
						}
                        BindingContext[dsExceptions1, "AlertPointAccess"].EndCurrentEdit();
                        daExceptions.Update(dsExceptions1);
					}
					else if( oDrop[0].ToString() == "Department" ) 
					{
						ArrayList alRow = new ArrayList();
						int[] iRows = oDrop[1] as int[];
						DataRow dr = null;
						for( int i = 0; i < iRows.Length; i++ )
						{
							dr = gvDepartment.GetDataRow( iRows[i] );
							object oDepartment = dr["NAME"];
                            string sSQL = "delete from AlertPointAccess where aap_id=" + AlertPointID + " and mluser in (select name from mluser where department='" + oDepartment + "') " +
                                "insert into AlertPointAccess (aap_id, mluser) " +
                                "select " + AlertPointID + ", name from mluser where department='" + oDepartment + "'";
							Connection.SQLExecutor.ExecuteNonQuery( sSQL, Connection.TRConnection);
						}	
						RefreshGrids();
					}
				}
			}
        }

        private void gcException_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void gcException_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvException.CalcHitInfo(e.X, e.Y);
			if(hitInfo.InRow) 
			{
				Size dragSize = SystemInformation.DragSize;
				this.dragBox = new Rectangle(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
			}
        }

        private void gcException_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (dragBox != Rectangle.Empty && !dragBox.Contains(pt))
            {
                DoExceptionDrag();
                dragBox = Rectangle.Empty;
            }
        }

        private void DoExceptionDrag()
        {
            if (gvException.RowCount > 0)
            {
                int[] iRows = gvException.GetSelectedRows();
                if (iRows != null)
                {
                    gvException.GridControl.DoDragDrop(new object[] { "Exception", iRows }, DragDropEffects.All);
                }
            }
        }

        #endregion

        #region USER DRAG DROP

        private void gcUser_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(object[]))) 
			{
				object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
				if( oDrop != null )
				{
					if( oDrop[0].ToString() == "Exception" )
					{
						ArrayList alRow = new ArrayList();
						int[] iRows = oDrop[1] as int[];
						DataRow dr = null;
						for( int i = 0; i < iRows.Length; i++ )
						{
							dr = gvException.GetDataRow( iRows[i] );
							object oUSER = dr["MLUSER"];							
							dsUser1.Tables[0].Rows.Add( new object[] { oUSER } );
							alRow.Add( dr );
						}		
						for( int i = 0; i < alRow.Count; i++ )
						{
							(alRow[i] as DataRow).Delete();						
						}
                        BindingContext[dsExceptions1, "AlertPointAccess"].EndCurrentEdit();
                        daExceptions.Update(dsExceptions1);
					}
				}
			}	
        }

        private void gcUser_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(object[]))) 
			{
				object[] oDrop = (object[])e.Data.GetData(typeof(object[]));
				if( oDrop != null )
				{
                    if (oDrop[0].ToString() == "Exception")
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
			if(hitInfo.InRow) 
			{
				Size dragSize = SystemInformation.DragSize;
				this.dragBox = new Rectangle(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
			}
        }

        private void gcUser_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
			if(dragBox != Rectangle.Empty && !dragBox.Contains(pt)) 
			{
				DoUserDrag();
				dragBox = Rectangle.Empty;
			}
        }

        private void DoUserDrag()
		{
			if( gvUser.RowCount > 0 )
			{
				int[] iRows = gvUser.GetSelectedRows();
				if( iRows != null )
				{					
					gvUser.GridControl.DoDragDrop(new object[] { "User",iRows}, DragDropEffects.All);			
				}
			}
		}

		#endregion

        #region DEPARTMENT DRAG

        private void gcDepartment_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = gvDepartment.CalcHitInfo(e.X, e.Y);
            if (hitInfo.InRow)
            {
                Size dragSize = SystemInformation.DragSize;
                this.dragBox = new Rectangle(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height);
            }
        }

        private void gcDepartment_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (dragBox != Rectangle.Empty && !dragBox.Contains(pt))
            {
                DoDepartmentDrag();
                dragBox = Rectangle.Empty;
            }
        }

        private void DoDepartmentDrag()
        {
            if (gvDepartment.RowCount > 0)
            {
                int[] iRows = gvDepartment.GetSelectedRows();
                if (iRows != null)
                {
                    gvDepartment.GridControl.DoDragDrop(new object[] { "Department", iRows }, DragDropEffects.All);
                }
            }
        }

        #endregion
    }
}