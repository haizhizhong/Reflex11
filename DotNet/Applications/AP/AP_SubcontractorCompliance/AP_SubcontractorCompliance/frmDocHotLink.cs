using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AP_SubcontractorCompliance
{
    public partial class frmDocHotLink : DevExpress.XtraEditors.XtraForm
    {
        private HMConnection.HMCon Connection;
        private TUC_HMDevXManager.TUC_HMDevXManager DevXMgr;
        private WS_Popups.frmPopup Popup;
        private ucSupplierView ucSV;
        private int _RelType_ID;
        private string _RelType;
        private int _Supplier_ID;

        public frmDocHotLink(HMConnection.HMCon Connection, TUC_HMDevXManager.TUC_HMDevXManager DevXMgr, string RelType, int RelType_ID, int Supplier_ID)
        {
            this.Connection = Connection;
            this.DevXMgr = DevXMgr;
            _RelType_ID = RelType_ID;
            _RelType = RelType;
            _Supplier_ID = Supplier_ID;
            Popup = new WS_Popups.frmPopup(DevXMgr);
            InitializeComponent();
            RunSetups();
        }

        private void RunSetups()
        {
            TR_Conn.ConnectionString = Connection.TRConnection;
            daAttachments.SelectCommand.CommandText = daAttachments.SelectCommand.CommandText.Replace("web_strike_test", Connection.WebDB);

            daAttachments.SelectCommand.Parameters["@RelType"].Value = _RelType;
            daAttachments.SelectCommand.Parameters["@RelType_ID"].Value = _RelType_ID;
            daAttachments.Fill(dsAttachments1);

            string sSQL = @"select supplier+' - '+name from supplier_master where supplier_id = " + _Supplier_ID;
            object oSupplier = Connection.SQLExecutor.ExecuteScalar(sSQL, Connection.TRConnection);
            if (oSupplier != null && oSupplier != DBNull.Value)
                dpCompliance.Text = "Subcontractor Compliance - " + oSupplier; 

            ucSV = new ucSupplierView(Connection, DevXMgr);            
            ucSV.SetLinkMode();
            ucSV.RefreshDS();
            ucSV.SupplierID = _Supplier_ID; 
            ucSV.Dock = DockStyle.Fill;
            ucSV.Parent = dpCompliance;
        }

        private void frmDocHotLink_Load(object sender, EventArgs e)
        {
            DevXMgr.FormInit(this);
        }

        private void riLink_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                int ComplianceID = ucSV.GetFocusedCompliance();

                if (ComplianceID == -1)
                {
                    Popup.ShowPopup("Please select a subcontractor compliance record to link the document to.");
                }
                else
                {
                    if (Popup.ShowPopup("Are you sure you want to link the selected attachment with the selected subcontractor compliance?", WS_Popups.frmPopup.PopupType.OK_Cancel) == WS_Popups.frmPopup.PopupResult.OK)
                    {
                        DataRow dr = gvAttachments.GetDataRow(gvAttachments.FocusedRowHandle);
                        object oRep_ID = dr["FileRepository_ID"];
                        object oRel_ID = dr["Rel_ID"];
                        string sSQL = @"declare @FileRepository_ID int
		                        insert into CFS_FileRepository ([FileName], FileData, AddedBy, DateAdded, FileType, 
			                        Mime_type, InternalOnly, FileStatus, FileOrigin, OriginLink, OriginLink2, permanent_tf, CurrentTCSE_ID)
		                        select [FileName], FileData, AddedBy, DateAdded, FileType, 
			                        Mime_type, InternalOnly, FileStatus, FileOrigin, OriginLink, OriginLink2, permanent_tf, CurrentTCSE_ID
		                        from CFS_FileRepository 
		                        where ID = " + oRep_ID + @"
                        		
		                        select @FileRepository_ID=SCOPE_IDENTITY()
                        		
		                        insert into CFS_FileReleations (FileRepository_ID, RelType, RelType_ID, ContextItemID, FileOrigin, TargetPrint, Comment, 
			                        LinkOrigin, Supplier_ID, pri_id, AP_INV_HEADER_ID, PO_ID)
		                        select @FileRepository_ID, 'SUPPSUBCON', " + ComplianceID + @", ContextItemID, FileOrigin, TargetPrint, Comment, 
			                        LinkOrigin, Supplier_ID, pri_id, AP_INV_HEADER_ID, PO_ID
		                        from CFS_FileReleations
		                        where ID = " + oRel_ID;
                        Connection.SQLExecutor.ExecuteNonQuery(sSQL, Connection.TRConnection);
                        object oEX = Connection.SQLExecutor.Exception;

                        if (oEX == null)
                            Popup.ShowPopup("Attachment has been linked.");
                        else
                            Popup.ShowPopup("Error linking attachment.");
                    }                    
                }
            }
        }

        public void GetFile(string FileName, int ID)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            SqlDataReader sdr = null;
            SqlCommand cmd = new SqlCommand();
            string FileLocation = "";
            //The bytes returned from the GetBytes
            long blob;
            //Size of the BLOB Buffer
            int bufferSize = 255;
            //The BLOB byte[] buffer to be filled by GetBytes
            byte[] outBuffer = new byte[bufferSize];
            //The starting position of the BLOB output
            int startIndex;
            try
            {
                cmd.Connection = TR_Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"select r.FileData
                    from CFS_FileReleations l
                    join CFS_FileRepository r on r.ID = l.FileRepository_ID
                    where l.RelType = @RelType and l.RelType_ID = @RelType_ID and l.FileRepository_ID=@FileRepository_ID";
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@RelType", _RelType);
                cmd.Parameters.AddWithValue("@RelType_ID", _RelType_ID);
                cmd.Parameters.AddWithValue("@FileRepository_ID", ID);                

                if (TR_Conn.State != ConnectionState.Open)
                    cmd.Connection.Open();

                sdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                while (sdr.Read())
                {
                    //Get saving location in the temporary folder
                    FileLocation = Path.GetTempPath() + FileName;
                    //Create a file to hold the output
                    fs = new FileStream(FileLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    bw = new BinaryWriter(fs);
                    //Reset the starting index to new BLOB
                    startIndex = 0;
                    //Read byte into outBuffer[] and retained the number of bytes returned
                    blob = sdr.GetBytes(0, startIndex, outBuffer, 0, bufferSize);
                    //Continue while there are bytes beyond the size of the buffer
                    while (blob == bufferSize)
                    {
                        bw.Write(outBuffer);
                        bw.Flush();
                        //Reposition start index to the end of the last buffer and fill buffer
                        startIndex += bufferSize;
                        blob = sdr.GetBytes(0, startIndex, outBuffer, 0, bufferSize);
                    }
                    //Write the remaining buffer
                    bw.Write(outBuffer, 0, (int)blob);
                    bw.Flush();
                    //Close output file
                    bw.Close();
                    fs.Close();
                }
                //Close reader and connection
                sdr.Close();
                Process.Start(FileLocation);
            }
            catch (Exception ex)
            {
                Popup.ShowPopup(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        private void riView_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow dr = gvAttachments.GetDataRow(gvAttachments.FocusedRowHandle);
            if (dr != null)
            {
                object oID = dr["FileRepository_ID"];
                if (oID != null && oID != DBNull.Value)
                {
                    GetFile(dr["FileName"].ToString(), Convert.ToInt32(oID));
                }
            }
        }
    }
}