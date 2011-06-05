using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;

namespace OpenCharas
{
	public partial class Canvas
	{

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && (components != null))
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		public bool FinalClosing { get; set; }

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			switch (m.Msg)
			{
			case 0x10:
				if (AskToClose())
					return;

				FinalClosing = true;
				break;
			}

			base.WndProc(ref m);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Canvas));
			this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.docToPrint = new System.Drawing.Printing.PrintDocument();
			this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.SingleCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImagePackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CharacterSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.currentCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.entireSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PreviewSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.ViewModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SingleSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FrameAnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SheetModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ResetCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.LayersToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CharactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
			this.SetSheetCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
			this.SetGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
			this.ContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ToolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton13 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton18 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton19 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton20 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton22 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.ToolStrip2 = new System.Windows.Forms.ToolStrip();
			this.ToolStripButton8 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton15 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton14 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton17 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton16 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
			this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
			this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.ToolStripContainer1.SuspendLayout();
			this.MenuStrip1.SuspendLayout();
			this.ToolStrip1.SuspendLayout();
			this.ToolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// BottomToolStripPanel
			// 
			this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.BottomToolStripPanel.Name = "BottomToolStripPanel";
			this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// docToPrint
			// 
			this.docToPrint.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.document_PrintPage);
			// 
			// TopToolStripPanel
			// 
			this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.TopToolStripPanel.Name = "TopToolStripPanel";
			this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// RightToolStripPanel
			// 
			this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.RightToolStripPanel.Name = "RightToolStripPanel";
			this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// LeftToolStripPanel
			// 
			this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftToolStripPanel.Name = "LeftToolStripPanel";
			this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
			// 
			// ContentPanel
			// 
			this.ContentPanel.AutoScroll = true;
			this.ContentPanel.BackColor = System.Drawing.SystemColors.Window;
			this.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ContentPanel.Size = new System.Drawing.Size(584, 562);
			// 
			// ToolStripContainer1
			// 
			// 
			// ToolStripContainer1.ContentPanel
			// 
			this.ToolStripContainer1.ContentPanel.BackColor = System.Drawing.SystemColors.Window;
			this.ToolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(297, 254);
			this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.ToolStripContainer1.Name = "ToolStripContainer1";
			this.ToolStripContainer1.Size = new System.Drawing.Size(297, 328);
			this.ToolStripContainer1.TabIndex = 8;
			this.ToolStripContainer1.Text = "ToolStripContainer1";
			// 
			// ToolStripContainer1.TopToolStripPanel
			// 
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.MenuStrip1);
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip1);
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip2);
			// 
			// MenuStrip1
			// 
			this.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.ToolStripMenuItem7,
            this.ToolStripMenuItem12,
            this.HelpToolStripMenuItem});
			this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Size = new System.Drawing.Size(297, 24);
			this.MenuStrip1.TabIndex = 6;
			this.MenuStrip1.Text = "MenuStrip1";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem4,
            this.ToolStripSeparator2,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3,
            this.ToolStripMenuItem5,
            this.ToolStripSeparator4,
            this.ToolStripMenuItem6,
            this.ToolStripMenuItem8,
            this.ToolStripSeparator10,
            this.ToolStripMenuItem11,
            this.ToolStripMenuItem9,
            this.ToolStripSeparator1,
            this.ExitToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileToolStripMenuItem.Text = "&File";
			// 
			// ToolStripMenuItem1
			// 
			this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SingleCharacterToolStripMenuItem,
            this.ImagePackToolStripMenuItem,
            this.CharacterSetToolStripMenuItem});
			this.ToolStripMenuItem1.Image = global::OpenCharas.Properties.Resources.NewDocumentHS;
			this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
			this.ToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem1.Text = "&New...";
			// 
			// SingleCharacterToolStripMenuItem
			// 
			this.SingleCharacterToolStripMenuItem.Name = "SingleCharacterToolStripMenuItem";
			this.SingleCharacterToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.SingleCharacterToolStripMenuItem.Text = "&Character";
			this.SingleCharacterToolStripMenuItem.Click += new System.EventHandler(this.SingleCharacterToolStripMenuItem_Click);
			// 
			// ImagePackToolStripMenuItem
			// 
			this.ImagePackToolStripMenuItem.Name = "ImagePackToolStripMenuItem";
			this.ImagePackToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.ImagePackToolStripMenuItem.Text = "Image &Pack...";
			this.ImagePackToolStripMenuItem.Click += new System.EventHandler(this.ImagePackToolStripMenuItem_Click);
			// 
			// CharacterSetToolStripMenuItem
			// 
			this.CharacterSetToolStripMenuItem.Name = "CharacterSetToolStripMenuItem";
			this.CharacterSetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.CharacterSetToolStripMenuItem.Text = "Character &Sheet";
			this.CharacterSetToolStripMenuItem.Click += new System.EventHandler(this.CharacterSetToolStripMenuItem_Click);
			// 
			// ToolStripMenuItem4
			// 
			this.ToolStripMenuItem4.Image = global::OpenCharas.Properties.Resources.openHS;
			this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
			this.ToolStripMenuItem4.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem4.Text = "O&pen...";
			this.ToolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem4_Click);
			// 
			// ToolStripSeparator2
			// 
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(151, 6);
			// 
			// ToolStripMenuItem2
			// 
			this.ToolStripMenuItem2.Image = global::OpenCharas.Properties.Resources.saveHS;
			this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			this.ToolStripMenuItem2.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem2.Text = "&Save...";
			this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
			// 
			// ToolStripMenuItem3
			// 
			this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
			this.ToolStripMenuItem3.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem3.Text = "Save &As...";
			this.ToolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
			// 
			// ToolStripMenuItem5
			// 
			this.ToolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentCharacterToolStripMenuItem,
            this.entireSheetToolStripMenuItem});
			this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
			this.ToolStripMenuItem5.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem5.Text = "&Export...";
			// 
			// currentCharacterToolStripMenuItem
			// 
			this.currentCharacterToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.export_char;
			this.currentCharacterToolStripMenuItem.Name = "currentCharacterToolStripMenuItem";
			this.currentCharacterToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.currentCharacterToolStripMenuItem.Text = "Current &Character";
			this.currentCharacterToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
			// 
			// entireSheetToolStripMenuItem
			// 
			this.entireSheetToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.export_sheet;
			this.entireSheetToolStripMenuItem.Name = "entireSheetToolStripMenuItem";
			this.entireSheetToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.entireSheetToolStripMenuItem.Text = "Entire &Sheet";
			this.entireSheetToolStripMenuItem.Click += new System.EventHandler(this.entireSheetToolStripMenuItem_Click_1);
			// 
			// ToolStripSeparator4
			// 
			this.ToolStripSeparator4.Name = "ToolStripSeparator4";
			this.ToolStripSeparator4.Size = new System.Drawing.Size(151, 6);
			// 
			// ToolStripMenuItem6
			// 
			this.ToolStripMenuItem6.Image = global::OpenCharas.Properties.Resources.PrintHS;
			this.ToolStripMenuItem6.Name = "ToolStripMenuItem6";
			this.ToolStripMenuItem6.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem6.Text = "&Print";
			this.ToolStripMenuItem6.Click += new System.EventHandler(this.ToolStripMenuItem6_Click);
			// 
			// ToolStripMenuItem8
			// 
			this.ToolStripMenuItem8.Image = global::OpenCharas.Properties.Resources.PrintPreviewHS;
			this.ToolStripMenuItem8.Name = "ToolStripMenuItem8";
			this.ToolStripMenuItem8.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem8.Text = "P&rint Preview";
			this.ToolStripMenuItem8.Click += new System.EventHandler(this.ToolStripMenuItem8_Click);
			// 
			// ToolStripSeparator10
			// 
			this.ToolStripSeparator10.Name = "ToolStripSeparator10";
			this.ToolStripSeparator10.Size = new System.Drawing.Size(151, 6);
			// 
			// ToolStripMenuItem11
			// 
			this.ToolStripMenuItem11.Image = global::OpenCharas.Properties.Resources._112_RefreshArrow_Green_16x16_72;
			this.ToolStripMenuItem11.Name = "ToolStripMenuItem11";
			this.ToolStripMenuItem11.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem11.Text = "Refresh &Images";
			this.ToolStripMenuItem11.Click += new System.EventHandler(this.ToolStripMenuItem11_Click);
			// 
			// ToolStripMenuItem9
			// 
			this.ToolStripMenuItem9.Image = global::OpenCharas.Properties.Resources._305_Close_16x16_72;
			this.ToolStripMenuItem9.Name = "ToolStripMenuItem9";
			this.ToolStripMenuItem9.Size = new System.Drawing.Size(154, 22);
			this.ToolStripMenuItem9.Text = "&Clear Settings";
			this.ToolStripMenuItem9.Click += new System.EventHandler(this.ToolStripMenuItem9_Click);
			// 
			// ToolStripSeparator1
			// 
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(151, 6);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.ExitToolStripMenuItem.Text = "E&xit";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// ViewToolStripMenuItem
			// 
			this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewSheetToolStripMenuItem,
            this.ToolStripSeparator3,
            this.ViewModeToolStripMenuItem,
            this.ResetCameraToolStripMenuItem});
			this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
			this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.ViewToolStripMenuItem.Text = "&View";
			// 
			// PreviewSheetToolStripMenuItem
			// 
			this.PreviewSheetToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.preview;
			this.PreviewSheetToolStripMenuItem.Name = "PreviewSheetToolStripMenuItem";
			this.PreviewSheetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.PreviewSheetToolStripMenuItem.Text = "&Preview Sheet";
			this.PreviewSheetToolStripMenuItem.Click += new System.EventHandler(this.PreviewSheetToolStripMenuItem_Click);
			// 
			// ToolStripSeparator3
			// 
			this.ToolStripSeparator3.Name = "ToolStripSeparator3";
			this.ToolStripSeparator3.Size = new System.Drawing.Size(149, 6);
			// 
			// ViewModeToolStripMenuItem
			// 
			this.ViewModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SingleSetToolStripMenuItem,
            this.FrameAnimationToolStripMenuItem,
            this.SheetModeToolStripMenuItem});
			this.ViewModeToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.setmode;
			this.ViewModeToolStripMenuItem.Name = "ViewModeToolStripMenuItem";
			this.ViewModeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ViewModeToolStripMenuItem.Text = "&View Mode";
			// 
			// SingleSetToolStripMenuItem
			// 
			this.SingleSetToolStripMenuItem.Checked = true;
			this.SingleSetToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SingleSetToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.setmode;
			this.SingleSetToolStripMenuItem.Name = "SingleSetToolStripMenuItem";
			this.SingleSetToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.SingleSetToolStripMenuItem.Text = "&Single Set";
			this.SingleSetToolStripMenuItem.Click += new System.EventHandler(this.EntireSheetToolStripMenuItem_Click);
			// 
			// FrameAnimationToolStripMenuItem
			// 
			this.FrameAnimationToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.animation;
			this.FrameAnimationToolStripMenuItem.Name = "FrameAnimationToolStripMenuItem";
			this.FrameAnimationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.FrameAnimationToolStripMenuItem.Text = "&Frame/Animation";
			this.FrameAnimationToolStripMenuItem.Click += new System.EventHandler(this.FrameAnimationToolStripMenuItem_Click);
			// 
			// SheetModeToolStripMenuItem
			// 
			this.SheetModeToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.sheetmode;
			this.SheetModeToolStripMenuItem.Name = "SheetModeToolStripMenuItem";
			this.SheetModeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.SheetModeToolStripMenuItem.Text = "Sheet &Mode";
			this.SheetModeToolStripMenuItem.Click += new System.EventHandler(this.SheetModeToolStripMenuItem_Click);
			// 
			// ResetCameraToolStripMenuItem
			// 
			this.ResetCameraToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.smCamera;
			this.ResetCameraToolStripMenuItem.Name = "ResetCameraToolStripMenuItem";
			this.ResetCameraToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ResetCameraToolStripMenuItem.Text = "&Reset Camera";
			this.ResetCameraToolStripMenuItem.Click += new System.EventHandler(this.ResetCameraToolStripMenuItem_Click);
			// 
			// ToolStripMenuItem7
			// 
			this.ToolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LayersToolStripMenuItem2,
            this.ItemsToolStripMenuItem,
            this.CharactersToolStripMenuItem});
			this.ToolStripMenuItem7.Name = "ToolStripMenuItem7";
			this.ToolStripMenuItem7.Size = new System.Drawing.Size(68, 20);
			this.ToolStripMenuItem7.Text = "&Windows";
			// 
			// LayersToolStripMenuItem2
			// 
			this.LayersToolStripMenuItem2.Image = global::OpenCharas.Properties.Resources.layers;
			this.LayersToolStripMenuItem2.Name = "LayersToolStripMenuItem2";
			this.LayersToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
			this.LayersToolStripMenuItem2.Text = "&Layers";
			this.LayersToolStripMenuItem2.Click += new System.EventHandler(this.LayersToolStripMenuItem2_Click);
			// 
			// ItemsToolStripMenuItem
			// 
			this.ItemsToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.items;
			this.ItemsToolStripMenuItem.Name = "ItemsToolStripMenuItem";
			this.ItemsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ItemsToolStripMenuItem.Text = "&Items";
			this.ItemsToolStripMenuItem.Click += new System.EventHandler(this.ItemsToolStripMenuItem_Click);
			// 
			// CharactersToolStripMenuItem
			// 
			this.CharactersToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.character;
			this.CharactersToolStripMenuItem.Name = "CharactersToolStripMenuItem";
			this.CharactersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.CharactersToolStripMenuItem.Text = "&Characters";
			this.CharactersToolStripMenuItem.Click += new System.EventHandler(this.CharactersToolStripMenuItem_Click);
			// 
			// ToolStripMenuItem12
			// 
			this.ToolStripMenuItem12.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetSheetCreatorToolStripMenuItem,
            this.ToolStripSeparator5,
            this.ToolStripMenuItem10,
            this.SetGameMenuItem});
			this.ToolStripMenuItem12.Name = "ToolStripMenuItem12";
			this.ToolStripMenuItem12.Size = new System.Drawing.Size(48, 20);
			this.ToolStripMenuItem12.Text = "&Tools";
			// 
			// SetSheetCreatorToolStripMenuItem
			// 
			this.SetSheetCreatorToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.sheet_maker;
			this.SetSheetCreatorToolStripMenuItem.Name = "SetSheetCreatorToolStripMenuItem";
			this.SetSheetCreatorToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.SetSheetCreatorToolStripMenuItem.Text = "Set Sheet &Creator";
			this.SetSheetCreatorToolStripMenuItem.Click += new System.EventHandler(this.SetSheetCreatorToolStripMenuItem_Click);
			// 
			// ToolStripSeparator5
			// 
			this.ToolStripSeparator5.Name = "ToolStripSeparator5";
			this.ToolStripSeparator5.Size = new System.Drawing.Size(161, 6);
			// 
			// ToolStripMenuItem10
			// 
			this.ToolStripMenuItem10.Name = "ToolStripMenuItem10";
			this.ToolStripMenuItem10.Size = new System.Drawing.Size(164, 22);
			this.ToolStripMenuItem10.Text = "&Game Editor";
			this.ToolStripMenuItem10.Click += new System.EventHandler(this.ToolStripMenuItem10_Click);
			// 
			// SetGameMenuItem
			// 
			this.SetGameMenuItem.Name = "SetGameMenuItem";
			this.SetGameMenuItem.Size = new System.Drawing.Size(164, 22);
			this.SetGameMenuItem.Text = "&Set Game";
			this.SetGameMenuItem.Click += new System.EventHandler(this.SetGameMenuItem_Click);
			// 
			// HelpToolStripMenuItem
			// 
			this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripMenuItem13,
            this.ContentsToolStripMenuItem,
            this.AboutToolStripMenuItem});
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.HelpToolStripMenuItem.Text = "&Help";
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Image = global::OpenCharas.Properties.Resources.updates;
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for &Updates";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			this.toolStripMenuItem13.Size = new System.Drawing.Size(168, 6);
			// 
			// ContentsToolStripMenuItem
			// 
			this.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem";
			this.ContentsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.ContentsToolStripMenuItem.Text = "&Contents";
			this.ContentsToolStripMenuItem.Click += new System.EventHandler(this.ContentsToolStripMenuItem_Click);
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.AboutToolStripMenuItem.Text = "&About";
			this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// ToolStrip1
			// 
			this.ToolStrip1.AllowItemReorder = true;
			this.ToolStrip1.AutoSize = false;
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton9,
            this.ToolStripButton13,
            this.ToolStripButton18,
            this.toolStripButton3,
            this.ToolStripButton19,
            this.ToolStripButton20,
            this.ToolStripSeparator8,
            this.ToolStripButton22,
            this.ToolStripSeparator9,
            this.ToolStripButton6,
            this.ToolStripButton7,
            this.ToolStripTextBox1});
			this.ToolStrip1.Location = new System.Drawing.Point(0, 24);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(297, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 7;
			// 
			// ToolStripButton9
			// 
			this.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton9.Image = global::OpenCharas.Properties.Resources.NewDocumentHS;
			this.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton9.Name = "ToolStripButton9";
			this.ToolStripButton9.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton9.Text = "ToolStripButton1";
			this.ToolStripButton9.ToolTipText = "New Character";
			this.ToolStripButton9.Click += new System.EventHandler(this.ToolStripButton1_Click);
			// 
			// ToolStripButton13
			// 
			this.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton13.Image = global::OpenCharas.Properties.Resources.openHS;
			this.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton13.Name = "ToolStripButton13";
			this.ToolStripButton13.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton13.Text = "ToolStripButton2";
			this.ToolStripButton13.ToolTipText = "Open";
			this.ToolStripButton13.Click += new System.EventHandler(this.ToolStripButton13_Click);
			// 
			// ToolStripButton18
			// 
			this.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton18.Image = global::OpenCharas.Properties.Resources.saveHS;
			this.ToolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton18.Name = "ToolStripButton18";
			this.ToolStripButton18.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton18.Text = "ToolStripButton3";
			this.ToolStripButton18.ToolTipText = "Save";
			this.ToolStripButton18.Click += new System.EventHandler(this.ToolStripButton18_Click);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = global::OpenCharas.Properties.Resources.export_char;
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "Export Current Character";
			this.toolStripButton3.ToolTipText = "Export Current Character";
			this.toolStripButton3.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
			// 
			// ToolStripButton19
			// 
			this.ToolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton19.Image = global::OpenCharas.Properties.Resources.export_sheet;
			this.ToolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton19.Name = "ToolStripButton19";
			this.ToolStripButton19.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton19.Text = "Export Sheet";
			this.ToolStripButton19.ToolTipText = "Export Sheet";
			this.ToolStripButton19.Click += new System.EventHandler(this.ToolStripButton19_Click);
			// 
			// ToolStripButton20
			// 
			this.ToolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton20.Image = global::OpenCharas.Properties.Resources.PrintHS;
			this.ToolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton20.Name = "ToolStripButton20";
			this.ToolStripButton20.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton20.Text = "ToolStripButton5";
			this.ToolStripButton20.ToolTipText = "Print";
			this.ToolStripButton20.Click += new System.EventHandler(this.ToolStripButton20_Click);
			// 
			// ToolStripSeparator8
			// 
			this.ToolStripSeparator8.Name = "ToolStripSeparator8";
			this.ToolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// ToolStripButton22
			// 
			this.ToolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton22.Image = global::OpenCharas.Properties.Resources.preview;
			this.ToolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton22.Name = "ToolStripButton22";
			this.ToolStripButton22.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton22.Text = "ToolStripButton10";
			this.ToolStripButton22.ToolTipText = "Preview Sheet";
			this.ToolStripButton22.Click += new System.EventHandler(this.ToolStripButton10_Click);
			// 
			// ToolStripSeparator9
			// 
			this.ToolStripSeparator9.Name = "ToolStripSeparator9";
			this.ToolStripSeparator9.Size = new System.Drawing.Size(6, 25);
			// 
			// ToolStripButton6
			// 
			this.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton6.Image = global::OpenCharas.Properties.Resources.zoom_in;
			this.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton6.Name = "ToolStripButton6";
			this.ToolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton6.Text = "ToolStripButton6";
			this.ToolStripButton6.ToolTipText = "Zoom In";
			this.ToolStripButton6.Click += new System.EventHandler(this.ToolStripButton6_Click);
			// 
			// ToolStripButton7
			// 
			this.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton7.Image = global::OpenCharas.Properties.Resources.zoom_out;
			this.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton7.Name = "ToolStripButton7";
			this.ToolStripButton7.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton7.Text = "ToolStripButton7";
			this.ToolStripButton7.ToolTipText = "Zoom Out";
			this.ToolStripButton7.Click += new System.EventHandler(this.ToolStripButton7_Click);
			// 
			// ToolStripTextBox1
			// 
			this.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ToolStripTextBox1.MaxLength = 5;
			this.ToolStripTextBox1.Name = "ToolStripTextBox1";
			this.ToolStripTextBox1.Size = new System.Drawing.Size(40, 25);
			this.ToolStripTextBox1.Text = "100%";
			this.ToolStripTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToolStripTextBox1_KeyPress);
			// 
			// ToolStrip2
			// 
			this.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton8,
            this.ToolStripButton15,
            this.ToolStripButton14,
            this.ToolStripSeparator6,
            this.ToolStripButton17,
            this.ToolStripButton16,
            this.ToolStripTextBox2,
            this.ToolStripSeparator11,
            this.ToolStripButton2,
            this.ToolStripButton1,
            this.ToolStripTextBox3});
			this.ToolStrip2.Location = new System.Drawing.Point(0, 49);
			this.ToolStrip2.Name = "ToolStrip2";
			this.ToolStrip2.Size = new System.Drawing.Size(297, 25);
			this.ToolStrip2.Stretch = true;
			this.ToolStrip2.TabIndex = 8;
			// 
			// ToolStripButton8
			// 
			this.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton8.Image = global::OpenCharas.Properties.Resources.playanim;
			this.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton8.Name = "ToolStripButton8";
			this.ToolStripButton8.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton8.Text = "ToolStripButton8";
			this.ToolStripButton8.ToolTipText = "Animate/Pause";
			this.ToolStripButton8.Click += new System.EventHandler(this.ToolStripButton8_Click);
			// 
			// ToolStripButton15
			// 
			this.ToolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton15.Image = global::OpenCharas.Properties.Resources.prev_frame;
			this.ToolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton15.Name = "ToolStripButton15";
			this.ToolStripButton15.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton15.Text = "ToolStripButton15";
			this.ToolStripButton15.ToolTipText = "Step Prev Frame";
			this.ToolStripButton15.Click += new System.EventHandler(this.ToolStripButton15_Click);
			// 
			// ToolStripButton14
			// 
			this.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton14.Image = global::OpenCharas.Properties.Resources.next_frame;
			this.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton14.Name = "ToolStripButton14";
			this.ToolStripButton14.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton14.Text = "ToolStripButton14";
			this.ToolStripButton14.ToolTipText = "Step Next Frame";
			this.ToolStripButton14.Click += new System.EventHandler(this.ToolStripButton14_Click);
			// 
			// ToolStripSeparator6
			// 
			this.ToolStripSeparator6.Name = "ToolStripSeparator6";
			this.ToolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// ToolStripButton17
			// 
			this.ToolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton17.Image = global::OpenCharas.Properties.Resources.inc_speed;
			this.ToolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton17.Name = "ToolStripButton17";
			this.ToolStripButton17.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton17.Text = "ToolStripButton17";
			this.ToolStripButton17.ToolTipText = "Decrease Animation Timer\n(Faster)";
			this.ToolStripButton17.Click += new System.EventHandler(this.ToolStripButton17_Click);
			// 
			// ToolStripButton16
			// 
			this.ToolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton16.Image = global::OpenCharas.Properties.Resources.dec_speed;
			this.ToolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton16.Name = "ToolStripButton16";
			this.ToolStripButton16.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton16.Text = "ToolStripButton16";
			this.ToolStripButton16.ToolTipText = "Increase Animation Timer\n(Slower)";
			this.ToolStripButton16.Click += new System.EventHandler(this.ToolStripButton16_Click);
			// 
			// ToolStripTextBox2
			// 
			this.ToolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ToolStripTextBox2.MaxLength = 6;
			this.ToolStripTextBox2.Name = "ToolStripTextBox2";
			this.ToolStripTextBox2.Size = new System.Drawing.Size(60, 25);
			this.ToolStripTextBox2.ToolTipText = "Animation Speed (in ms).\nLower values are faster.";
			this.ToolStripTextBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToolStripTextBox2_KeyPress);
			// 
			// ToolStripSeparator11
			// 
			this.ToolStripSeparator11.Name = "ToolStripSeparator11";
			this.ToolStripSeparator11.Size = new System.Drawing.Size(6, 25);
			// 
			// ToolStripButton2
			// 
			this.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton2.Image = global::OpenCharas.Properties.Resources.prevanim;
			this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton2.Name = "ToolStripButton2";
			this.ToolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton2.Text = "ToolStripButton2";
			this.ToolStripButton2.ToolTipText = "Previous Animation";
			this.ToolStripButton2.Click += new System.EventHandler(this.ToolStripButton2_Click);
			// 
			// ToolStripButton1
			// 
			this.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton1.Image = global::OpenCharas.Properties.Resources.nextanim;
			this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton1.Name = "ToolStripButton1";
			this.ToolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton1.Text = "ToolStripButton1";
			this.ToolStripButton1.ToolTipText = "Next Animation";
			this.ToolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click_1);
			// 
			// ToolStripTextBox3
			// 
			this.ToolStripTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ToolStripTextBox3.MaxLength = 2;
			this.ToolStripTextBox3.Name = "ToolStripTextBox3";
			this.ToolStripTextBox3.Size = new System.Drawing.Size(30, 25);
			this.ToolStripTextBox3.ToolTipText = "Animation Number";
			this.ToolStripTextBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToolStripTextBox3_KeyPress);
			// 
			// Canvas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 328);
			this.Controls.Add(this.ToolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(305, 355);
			this.Name = "Canvas";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "OpenCharas";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Canvas_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Canvas_FormClosed);
			this.Load += new System.EventHandler(this.Canvas_Load);
			this.SizeChanged += new System.EventHandler(this.Canvas_SizeChanged);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseWheel);
			this.Move += new System.EventHandler(this.Canvas_Move);
			this.Resize += new System.EventHandler(this.Canvas_Resize);
			this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
			this.ToolStripContainer1.ResumeLayout(false);
			this.ToolStripContainer1.PerformLayout();
			this.MenuStrip1.ResumeLayout(false);
			this.MenuStrip1.PerformLayout();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.ToolStrip2.ResumeLayout(false);
			this.ToolStrip2.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
		internal System.Windows.Forms.ToolStripPanel TopToolStripPanel;
		internal System.Windows.Forms.ToolStripPanel RightToolStripPanel;
		internal System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
		internal System.Windows.Forms.ToolStripContentPanel ContentPanel;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton9;
		internal System.Windows.Forms.ToolStripButton ToolStripButton13;
		internal System.Windows.Forms.ToolStripButton ToolStripButton18;
		internal System.Windows.Forms.ToolStripButton ToolStripButton19;
		internal System.Windows.Forms.ToolStripButton ToolStripButton20;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator8;
		internal System.Windows.Forms.ToolStripButton ToolStripButton22;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator9;
		internal System.Windows.Forms.ToolStripButton ToolStripButton6;
		internal System.Windows.Forms.ToolStripTextBox ToolStripTextBox1;
		internal System.Windows.Forms.MenuStrip MenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem6;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem8;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
		internal System.Windows.Forms.ToolStripMenuItem SetGameMenuItem;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
		internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem PreviewSheetToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
		internal System.Windows.Forms.ToolStripMenuItem ViewModeToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem SingleSetToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem FrameAnimationToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ResetCameraToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem7;
		internal System.Windows.Forms.ToolStripMenuItem LayersToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripMenuItem ItemsToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ContentsToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		internal System.Windows.Forms.ToolStripContainer ToolStripContainer1;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem9;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem10;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator10;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem11;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem12;
		internal System.Windows.Forms.ToolStripMenuItem SetSheetCreatorToolStripMenuItem;
		internal System.Windows.Forms.ToolStrip ToolStrip2;
		internal System.Windows.Forms.ToolStripButton ToolStripButton8;
		internal System.Windows.Forms.ToolStripButton ToolStripButton15;
		internal System.Windows.Forms.ToolStripButton ToolStripButton14;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator6;
		internal System.Windows.Forms.ToolStripButton ToolStripButton16;
		internal System.Windows.Forms.ToolStripButton ToolStripButton17;
		internal System.Windows.Forms.ToolStripTextBox ToolStripTextBox2;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
		internal System.Windows.Forms.ToolStripButton ToolStripButton2;
		internal System.Windows.Forms.ToolStripButton ToolStripButton1;
		internal System.Windows.Forms.ToolStripTextBox ToolStripTextBox3;
		internal System.Windows.Forms.ToolStripMenuItem SingleCharacterToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ImagePackToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem CharacterSetToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem CharactersToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem SheetModeToolStripMenuItem;
		private ToolStripMenuItem currentCharacterToolStripMenuItem;
		private ToolStripMenuItem entireSheetToolStripMenuItem;
		internal System.Windows.Forms.ToolStripButton ToolStripButton7;
		internal System.Windows.Forms.ToolStripButton toolStripButton3;
		private ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem13;

	}

}
