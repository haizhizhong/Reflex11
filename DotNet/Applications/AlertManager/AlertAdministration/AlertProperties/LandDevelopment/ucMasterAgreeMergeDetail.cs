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
    public partial class ucMasterAgreeMergeDetail : DevExpress.XtraEditors.XtraUserControl
    {
        private Point p = Point.Empty;
        private BindingSource bsDocument;
        public event UpdateBS_del UpdateDocumentBindingSource;
        public delegate void UpdateBS_del();

        public ucMasterAgreeMergeDetail(BindingSource bsDocument)
        {
            this.bsDocument = bsDocument;
            InitializeComponent();
            dnCommitCancel.DataSource = this.bsDocument;
            mDetail.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDocument, "RepDetails", true));
        }

        private void lbcLegend_MouseDown(object sender, MouseEventArgs e)
        {
            ListBoxControl c = sender as ListBoxControl;
            p = new Point(e.X, e.Y);
            int selectedIndex = c.IndexFromPoint(p);
            if (selectedIndex == -1)
                p = Point.Empty;
        }

        private void lbcLegend_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if ((p != Point.Empty) && ((Math.Abs(e.X - p.X) > 5) || (Math.Abs(e.Y - p.Y) > 5)))
                    lbcLegend.DoDragDrop(sender, DragDropEffects.Move);
        }

        private void mDetail_DragDrop(object sender, DragEventArgs e)
        {
            TextEdit editor = (TextEdit)sender;
            TextBox textBox = editor.MaskBox;
            string text = (string)e.Data.GetData(typeof(string));
            int caretPos = textBox.GetCharIndexFromPosition(textBox.PointToClient(new Point(e.X, e.Y)));
            int memoLength = textBox.Text.Length;
            if (memoLength - caretPos == 1)
                caretPos++;
            textBox.SelectionStart = caretPos;
            textBox.SelectionLength = 0;
            textBox.Paste(lbcLegend.Items[lbcLegend.IndexFromPoint(p)].ToString());
        }

        private void mDetail_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dnCommitCancel_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.EndEdit)
            {
               bsDocument.EndEdit();
               if(UpdateDocumentBindingSource != null)
                   UpdateDocumentBindingSource();
               // daDocument.Update(dsDocument1);
            }
            else if (e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
               bsDocument.CancelEdit();
            }
        }
    }
}
