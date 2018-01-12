namespace AlertManager
{
    partial class AlertView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertView));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiMSeen = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMUnseen = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMarkCompleted = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCompleted = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.mNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblReceived = new DevExpress.XtraEditors.LabelControl();
            this.lblModule = new DevExpress.XtraEditors.LabelControl();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiMUnseen,
            this.bbiDelete,
            this.bbiMSeen,
            this.bbiMarkCompleted,
            this.bbiCompleted});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiMSeen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiMUnseen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiMarkCompleted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCompleted, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bbiMSeen
            // 
            this.bbiMSeen.Caption = "Mark as Seen";
            this.bbiMSeen.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiMSeen.Glyph")));
            this.bbiMSeen.Id = 2;
            this.bbiMSeen.Name = "bbiMSeen";
            this.bbiMSeen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiMSeen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMSeen_ItemClick);
            // 
            // bbiMUnseen
            // 
            this.bbiMUnseen.Caption = "Mark as Unseen";
            this.bbiMUnseen.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiMUnseen.Glyph")));
            this.bbiMUnseen.Id = 0;
            this.bbiMUnseen.Name = "bbiMUnseen";
            this.bbiMUnseen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiMUnseen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMUnseen_ItemClick);
            // 
            // bbiMarkCompleted
            // 
            this.bbiMarkCompleted.Caption = "Mark Completed";
            this.bbiMarkCompleted.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiMarkCompleted.Glyph")));
            this.bbiMarkCompleted.Id = 3;
            this.bbiMarkCompleted.Name = "bbiMarkCompleted";
            this.bbiMarkCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiMarkCompleted.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMarkCompleted_ItemClick);
            // 
            // bbiCompleted
            // 
            this.bbiCompleted.Caption = "Completed";
            this.bbiCompleted.Enabled = false;
            this.bbiCompleted.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiCompleted.Glyph")));
            this.bbiCompleted.Id = 4;
            this.bbiCompleted.Name = "bbiCompleted";
            this.bbiCompleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "Delete Alert";
            this.bbiDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiDelete.Glyph")));
            this.bbiDelete.Id = 1;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 26);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(992, 652);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl2);
            this.groupControl1.Location = new System.Drawing.Point(7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(979, 639);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Alert Information";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutControl2.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText;
            this.layoutControl2.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutControl2.Controls.Add(this.mNotes);
            this.layoutControl2.Controls.Add(this.lblReceived);
            this.layoutControl2.Controls.Add(this.lblModule);
            this.layoutControl2.Controls.Add(this.lblSubject);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 20);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(975, 617);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // mNotes
            // 
            this.mNotes.Location = new System.Drawing.Point(7, 78);
            this.mNotes.MenuManager = this.barManager1;
            this.mNotes.Name = "mNotes";
            this.mNotes.Properties.ReadOnly = true;
            this.mNotes.Size = new System.Drawing.Size(962, 533);
            this.mNotes.StyleController = this.layoutControl2;
            this.mNotes.TabIndex = 8;
            // 
            // lblReceived
            // 
            this.lblReceived.Location = new System.Drawing.Point(876, 36);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(93, 13);
            this.lblReceived.StyleController = this.layoutControl2;
            this.lblReceived.TabIndex = 7;
            this.lblReceived.Text = "1/15/2010 8:23 AM";
            // 
            // lblModule
            // 
            this.lblModule.Location = new System.Drawing.Point(86, 36);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(184, 13);
            this.lblModule.StyleController = this.layoutControl2;
            this.lblModule.TabIndex = 6;
            this.lblModule.Text = "Project Costing - General Construction";
            // 
            // lblSubject
            // 
            this.lblSubject.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubject.Appearance.Options.UseFont = true;
            this.lblSubject.Location = new System.Drawing.Point(7, 7);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(288, 18);
            this.lblSubject.StyleController = this.layoutControl2;
            this.lblSubject.TabIndex = 5;
            this.lblSubject.Text = "Purchase Requisition Item Transfered.";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(975, 617);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblSubject;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(973, 29);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lblModule;
            this.layoutControlItem2.CustomizationFormText = "Module:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(274, 24);
            this.layoutControlItem2.Text = "Module:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(74, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblReceived;
            this.layoutControlItem4.CustomizationFormText = "Alert Received:";
            this.layoutControlItem4.Location = new System.Drawing.Point(790, 29);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(183, 24);
            this.layoutControlItem4.Text = "Alert Received:";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(74, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(274, 29);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(516, 24);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.mNotes;
            this.layoutControlItem5.CustomizationFormText = "Notes:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 53);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(973, 562);
            this.layoutControlItem5.Text = "Notes:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(74, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(992, 652);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(990, 650);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // AlertView
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "AlertView";
            this.Size = new System.Drawing.Size(992, 700);
            this.Load += new System.EventHandler(this.AlertView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiMUnseen;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblModule;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblReceived;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraBars.BarButtonItem bbiMSeen;
        protected DevExpress.XtraLayout.LayoutControl layoutControl1;
        protected DevExpress.XtraLayout.LayoutControl layoutControl2;
        protected DevExpress.XtraEditors.MemoEdit mNotes;
        protected DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraBars.BarButtonItem bbiMarkCompleted;
        private DevExpress.XtraBars.BarButtonItem bbiCompleted;
    }
}
