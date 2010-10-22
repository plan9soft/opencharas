using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class SetSheetCreator : System.Windows.Forms.Form
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

		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.Shown += new System.EventHandler(Form8_Shown);
			base.SizeChanged += new System.EventHandler(Form8_SizeChanged);
			base.MouseWheel += new System.Windows.Forms.MouseEventHandler(SetSheetCreator_MouseWheel);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetSheetCreator));
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.TreeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseDown);
			this.TreeView1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseWheel);
			this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ImportImageToThisSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ImportImageToThisSetToolStripMenuItem.Click += new System.EventHandler(this.ImportImageToThisSetToolStripMenuItem_Click);
			this.ClearImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ClearImageToolStripMenuItem.Click += new System.EventHandler(this.ClearImageToolStripMenuItem_Click);
			this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ExportSetToImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExportSetToImageToolStripMenuItem.Click += new System.EventHandler(this.ExportSetToImageToolStripMenuItem_Click);
			this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
			this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
			this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ToolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton9.Click += new System.EventHandler(this.ToolStripButton9_Click);
			this.ToolStripButton13 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton13.Click += new System.EventHandler(this.ToolStripButton13_Click);
			this.ToolStripButton18 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton18.Click += new System.EventHandler(this.ToolStripButton18_Click);
			this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton6.Click += new System.EventHandler(this.ToolStripButton6_Click);
			this.ToolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton7.Click += new System.EventHandler(this.ToolStripButton7_Click);
			this.ToolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.ToolStripTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ToolStripTextBox1_KeyPress);
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.PictureBox1).BeginInit();
			this.ContextMenuStrip1.SuspendLayout();
			this.MenuStrip1.SuspendLayout();
			this.ToolStripContainer1.ContentPanel.SuspendLayout();
			this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.ToolStripContainer1.SuspendLayout();
			this.ToolStrip1.SuspendLayout();
			this.SuspendLayout();
			//
			//SplitContainer1
			//
			this.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer1.Name = "SplitContainer1";
			//
			//SplitContainer1.Panel1
			//
			this.SplitContainer1.Panel1.Controls.Add(this.PictureBox1);
			//
			//SplitContainer1.Panel2
			//
			this.SplitContainer1.Panel2.Controls.Add(this.TreeView1);
			this.SplitContainer1.Size = new System.Drawing.Size(666, 432);
			this.SplitContainer1.SplitterDistance = 491;
			this.SplitContainer1.TabIndex = 0;
			//
			//PictureBox1
			//
			this.PictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PictureBox1.Location = new System.Drawing.Point(0, 0);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(487, 428);
			this.PictureBox1.TabIndex = 0;
			this.PictureBox1.TabStop = false;
			//
			//TreeView1
			//
			this.TreeView1.ContextMenuStrip = this.ContextMenuStrip1;
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(167, 428);
			this.TreeView1.TabIndex = 0;
			//
			//ContextMenuStrip1
			//
			this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ImportImageToThisSetToolStripMenuItem, this.ClearImageToolStripMenuItem, this.ToolStripSeparator5, this.ExportSetToImageToolStripMenuItem });
			this.ContextMenuStrip1.Name = "ContextMenuStrip1";
			this.ContextMenuStrip1.Size = new System.Drawing.Size(204, 76);
			//
			//ImportImageToThisSetToolStripMenuItem
			//
			this.ImportImageToThisSetToolStripMenuItem.Name = "ImportImageToThisSetToolStripMenuItem";
			this.ImportImageToThisSetToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.ImportImageToThisSetToolStripMenuItem.Text = "Import Image to this Set...";
			//
			//ClearImageToolStripMenuItem
			//
			this.ClearImageToolStripMenuItem.Name = "ClearImageToolStripMenuItem";
			this.ClearImageToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.ClearImageToolStripMenuItem.Text = "Clear Image";
			//
			//ToolStripSeparator5
			//
			this.ToolStripSeparator5.Name = "ToolStripSeparator5";
			this.ToolStripSeparator5.Size = new System.Drawing.Size(200, 6);
			//
			//ExportSetToImageToolStripMenuItem
			//
			this.ExportSetToImageToolStripMenuItem.Name = "ExportSetToImageToolStripMenuItem";
			this.ExportSetToImageToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			this.ExportSetToImageToolStripMenuItem.Text = "Export Set to Image...";
			//
			//MenuStrip1
			//
			this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.FileToolStripMenuItem });
			this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Size = new System.Drawing.Size(666, 24);
			this.MenuStrip1.TabIndex = 1;
			this.MenuStrip1.Text = "MenuStrip1";
			//
			//FileToolStripMenuItem
			//
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NewToolStripMenuItem, this.OpenToolStripMenuItem, this.toolStripSeparator, this.SaveToolStripMenuItem, this.SaveAsToolStripMenuItem, this.ToolStripSeparator2, this.ToolStripMenuItem1, this.ToolStripMenuItem2, this.ToolStripMenuItem3, this.toolStripSeparator1, this.ExitToolStripMenuItem });
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.FileToolStripMenuItem.Text = "&File";
			//
			//NewToolStripMenuItem
			//
			this.NewToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("NewToolStripMenuItem.Image"));
			this.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
			this.NewToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N);
			this.NewToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
			this.NewToolStripMenuItem.Text = "&New...";
			//
			//OpenToolStripMenuItem
			//
			this.OpenToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("OpenToolStripMenuItem.Image"));
			this.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			this.OpenToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O);
			this.OpenToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.OpenToolStripMenuItem.Text = "&Open...";
			//
			//toolStripSeparator
			//
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(185, 6);
			//
			//SaveToolStripMenuItem
			//
			this.SaveToolStripMenuItem.Image = (System.Drawing.Image)(resources.GetObject("SaveToolStripMenuItem.Image"));
			this.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
			this.SaveToolStripMenuItem.ShortcutKeys = (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S);
			this.SaveToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.SaveToolStripMenuItem.Text = "&Save";
			//
			//SaveAsToolStripMenuItem
			//
			this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
			this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.SaveAsToolStripMenuItem.Text = "Save &As...";
			//
			//ToolStripSeparator2
			//
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(185, 6);
			//
			//ToolStripMenuItem1
			//
			this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
			this.ToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
			this.ToolStripMenuItem1.Text = "Import Image to Set...";
			//
			//ToolStripMenuItem2
			//
			this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			this.ToolStripMenuItem2.Size = new System.Drawing.Size(188, 22);
			this.ToolStripMenuItem2.Text = "Clear Image on Set";
			//
			//ToolStripMenuItem3
			//
			this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
			this.ToolStripMenuItem3.Size = new System.Drawing.Size(188, 22);
			this.ToolStripMenuItem3.Text = "Export Set to Image...";
			//
			//toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
			//
			//ExitToolStripMenuItem
			//
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.ExitToolStripMenuItem.Text = "Close";
			//
			//ToolStripContainer1
			//
			//
			//ToolStripContainer1.ContentPanel
			//
			this.ToolStripContainer1.ContentPanel.Controls.Add(this.SplitContainer1);
			this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(666, 432);
			this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolStripContainer1.Location = new System.Drawing.Point(0, 24);
			this.ToolStripContainer1.Name = "ToolStripContainer1";
			this.ToolStripContainer1.Size = new System.Drawing.Size(666, 457);
			this.ToolStripContainer1.TabIndex = 2;
			this.ToolStripContainer1.Text = "ToolStripContainer1";
			//
			//ToolStripContainer1.TopToolStripPanel
			//
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip1);
			//
			//ToolStrip1
			//
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.ToolStripButton9, this.ToolStripButton13, this.ToolStripButton18, this.ToolStripSeparator4, this.ToolStripButton6, this.ToolStripButton7, this.ToolStripTextBox1 });
			this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(666, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 0;
			//
			//ToolStripButton9
			//
			this.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton9.Image = Properties.Resources.NewDocumentHS;
			this.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton9.Name = "ToolStripButton9";
			this.ToolStripButton9.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton9.Text = "ToolStripButton1";
			this.ToolStripButton9.ToolTipText = "New";
			//
			//ToolStripButton13
			//
			this.ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton13.Image = Properties.Resources.openHS;
			this.ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton13.Name = "ToolStripButton13";
			this.ToolStripButton13.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton13.Text = "ToolStripButton2";
			this.ToolStripButton13.ToolTipText = "Open";
			//
			//ToolStripButton18
			//
			this.ToolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton18.Image = Properties.Resources.saveHS;
			this.ToolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton18.Name = "ToolStripButton18";
			this.ToolStripButton18.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton18.Text = "ToolStripButton3";
			this.ToolStripButton18.ToolTipText = "Save";
			//
			//ToolStripSeparator4
			//
			this.ToolStripSeparator4.Name = "ToolStripSeparator4";
			this.ToolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			//
			//ToolStripButton6
			//
			this.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton6.Image = Properties.Resources.zoom_in;
			this.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton6.Name = "ToolStripButton6";
			this.ToolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton6.Text = "ToolStripButton6";
			this.ToolStripButton6.ToolTipText = "Zoom In";
			//
			//ToolStripButton7
			//
			this.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton7.Image = Properties.Resources.zoom_out;
			this.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton7.Name = "ToolStripButton7";
			this.ToolStripButton7.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton7.Text = "ToolStripButton7";
			this.ToolStripButton7.ToolTipText = "Zoom Out";
			//
			//ToolStripTextBox1
			//
			this.ToolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ToolStripTextBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.ToolStripTextBox1.MaxLength = 5;
			this.ToolStripTextBox1.Name = "ToolStripTextBox1";
			this.ToolStripTextBox1.Size = new System.Drawing.Size(40, 25);
			this.ToolStripTextBox1.Text = "100%";
			//
			//SetSheetCreator
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 481);
			this.Controls.Add(this.ToolStripContainer1);
			this.Controls.Add(this.MenuStrip1);
			this.MainMenuStrip = this.MenuStrip1;
			this.MinimumSize = new System.Drawing.Size(674, 508);
			this.Name = "SetSheetCreator";
			this.Text = "Set Sheet Editor";
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.PictureBox1).EndInit();
			this.ContextMenuStrip1.ResumeLayout(false);
			this.MenuStrip1.ResumeLayout(false);
			this.MenuStrip1.PerformLayout();
			this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
			this.ToolStripContainer1.ResumeLayout(false);
			this.ToolStripContainer1.PerformLayout();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.MenuStrip MenuStrip1;
		internal System.Windows.Forms.ToolStripContainer ToolStripContainer1;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem ImportImageToThisSetToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ClearImageToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripButton ToolStripButton6;
		internal System.Windows.Forms.ToolStripButton ToolStripButton7;
		internal System.Windows.Forms.ToolStripTextBox ToolStripTextBox1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton18;
		internal System.Windows.Forms.ToolStripButton ToolStripButton13;
		internal System.Windows.Forms.ToolStripButton ToolStripButton9;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
		internal System.Windows.Forms.ToolStripMenuItem ExportSetToImageToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
	}

}
