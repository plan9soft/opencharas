using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class ImagePacker : System.Windows.Forms.Form
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
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			base.Load += new System.EventHandler(Form7_Load);
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
			this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
			this.PackEverythingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PackEverythingToolStripMenuItem.Click += new System.EventHandler(this.PackEverythingToolStripMenuItem_Click);
			this.UnpackEverythingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.UnpackEverythingToolStripMenuItem.Click += new System.EventHandler(this.UnpackEverythingToolStripMenuItem_Click);
			this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
			this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.FlowLayoutPanel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.FlowLayoutPanel1_DragEnter);
			this.FlowLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.FlowLayoutPanel1_DragDrop);
			this.ContextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip();
			this.PackAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PackAllToolStripMenuItem.Click += new System.EventHandler(this.PackAllToolStripMenuItem_Click);
			this.FlowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.FlowLayoutPanel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.FlowLayoutPanel2_DragEnter);
			this.FlowLayoutPanel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.FlowLayoutPanel2_DragDrop);
			this.ContextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip();
			this.UnpackAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.UnpackAllToolStripMenuItem.Click += new System.EventHandler(this.UnpackAllToolStripMenuItem_Click);
			this.ToolTip1 = new System.Windows.Forms.ToolTip();
			this.SplitContainer3 = new System.Windows.Forms.SplitContainer();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1 = new System.Windows.Forms.Button();
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.ContextMenuStrip1.SuspendLayout();
			this.SplitContainer2.Panel1.SuspendLayout();
			this.SplitContainer2.Panel2.SuspendLayout();
			this.SplitContainer2.SuspendLayout();
			this.ContextMenuStrip2.SuspendLayout();
			this.ContextMenuStrip3.SuspendLayout();
			this.SplitContainer3.Panel1.SuspendLayout();
			this.SplitContainer3.Panel2.SuspendLayout();
			this.SplitContainer3.SuspendLayout();
			this.SuspendLayout();
			//
			//SplitContainer1
			//
			this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer1.Name = "SplitContainer1";
			//
			//SplitContainer1.Panel1
			//
			this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
			//
			//SplitContainer1.Panel2
			//
			this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
			this.SplitContainer1.Size = new System.Drawing.Size(557, 192);
			this.SplitContainer1.SplitterDistance = 128;
			this.SplitContainer1.TabIndex = 0;
			//
			//TreeView1
			//
			this.TreeView1.ContextMenuStrip = this.ContextMenuStrip1;
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(128, 192);
			this.TreeView1.TabIndex = 0;
			//
			//ContextMenuStrip1
			//
			this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.PackEverythingToolStripMenuItem, this.UnpackEverythingToolStripMenuItem });
			this.ContextMenuStrip1.Name = "ContextMenuStrip1";
			this.ContextMenuStrip1.Size = new System.Drawing.Size(174, 48);
			//
			//PackEverythingToolStripMenuItem
			//
			this.PackEverythingToolStripMenuItem.Name = "PackEverythingToolStripMenuItem";
			this.PackEverythingToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.PackEverythingToolStripMenuItem.Text = "Pack Everything";
			//
			//UnpackEverythingToolStripMenuItem
			//
			this.UnpackEverythingToolStripMenuItem.Name = "UnpackEverythingToolStripMenuItem";
			this.UnpackEverythingToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.UnpackEverythingToolStripMenuItem.Text = "Unpack Everything";
			//
			//SplitContainer2
			//
			this.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer2.Name = "SplitContainer2";
			//
			//SplitContainer2.Panel1
			//
			this.SplitContainer2.Panel1.Controls.Add(this.FlowLayoutPanel1);
			//
			//SplitContainer2.Panel2
			//
			this.SplitContainer2.Panel2.Controls.Add(this.FlowLayoutPanel2);
			this.SplitContainer2.Size = new System.Drawing.Size(425, 192);
			this.SplitContainer2.SplitterDistance = 208;
			this.SplitContainer2.TabIndex = 0;
			//
			//FlowLayoutPanel1
			//
			this.FlowLayoutPanel1.AllowDrop = true;
			this.FlowLayoutPanel1.AutoScroll = true;
			this.FlowLayoutPanel1.ContextMenuStrip = this.ContextMenuStrip2;
			this.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
			this.FlowLayoutPanel1.Size = new System.Drawing.Size(204, 188);
			this.FlowLayoutPanel1.TabIndex = 0;
			//
			//ContextMenuStrip2
			//
			this.ContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.PackAllToolStripMenuItem });
			this.ContextMenuStrip2.Name = "ContextMenuStrip2";
			this.ContextMenuStrip2.Size = new System.Drawing.Size(117, 26);
			//
			//PackAllToolStripMenuItem
			//
			this.PackAllToolStripMenuItem.Name = "PackAllToolStripMenuItem";
			this.PackAllToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.PackAllToolStripMenuItem.Text = "Pack All";
			//
			//FlowLayoutPanel2
			//
			this.FlowLayoutPanel2.AllowDrop = true;
			this.FlowLayoutPanel2.AutoScroll = true;
			this.FlowLayoutPanel2.ContextMenuStrip = this.ContextMenuStrip3;
			this.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.FlowLayoutPanel2.Name = "FlowLayoutPanel2";
			this.FlowLayoutPanel2.Size = new System.Drawing.Size(209, 188);
			this.FlowLayoutPanel2.TabIndex = 0;
			//
			//ContextMenuStrip3
			//
			this.ContextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.UnpackAllToolStripMenuItem });
			this.ContextMenuStrip3.Name = "ContextMenuStrip3";
			this.ContextMenuStrip3.Size = new System.Drawing.Size(132, 26);
			//
			//UnpackAllToolStripMenuItem
			//
			this.UnpackAllToolStripMenuItem.Name = "UnpackAllToolStripMenuItem";
			this.UnpackAllToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.UnpackAllToolStripMenuItem.Text = "Unpack All";
			//
			//ToolTip1
			//
			this.ToolTip1.AutomaticDelay = 5000;
			this.ToolTip1.AutoPopDelay = 3000;
			this.ToolTip1.InitialDelay = 100;
			this.ToolTip1.ReshowDelay = 0;
			this.ToolTip1.ShowAlways = true;
			this.ToolTip1.UseAnimation = false;
			this.ToolTip1.UseFading = false;
			//
			//SplitContainer3
			//
			this.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer3.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer3.Name = "SplitContainer3";
			this.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			//SplitContainer3.Panel1
			//
			this.SplitContainer3.Panel1.Controls.Add(this.SplitContainer1);
			//
			//SplitContainer3.Panel2
			//
			this.SplitContainer3.Panel2.Controls.Add(this.Button2);
			this.SplitContainer3.Panel2.Controls.Add(this.Button1);
			this.SplitContainer3.Size = new System.Drawing.Size(559, 282);
			this.SplitContainer3.SplitterDistance = 194;
			this.SplitContainer3.TabIndex = 1;
			//
			//Button2
			//
			this.Button2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Button2.Location = new System.Drawing.Point(462, 44);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(84, 26);
			this.Button2.TabIndex = 1;
			this.Button2.Text = "Cancel";
			this.Button2.UseVisualStyleBackColor = true;
			//
			//Button1
			//
			this.Button1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Button1.Location = new System.Drawing.Point(462, 12);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(84, 26);
			this.Button1.TabIndex = 0;
			this.Button1.Text = "Done";
			this.Button1.UseVisualStyleBackColor = true;
			//
			//ImagePacker
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(559, 282);
			this.ControlBox = false;
			this.Controls.Add(this.SplitContainer3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(416, 263);
			this.Name = "ImagePacker";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Image Packer";
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.ResumeLayout(false);
			this.ContextMenuStrip1.ResumeLayout(false);
			this.SplitContainer2.Panel1.ResumeLayout(false);
			this.SplitContainer2.Panel2.ResumeLayout(false);
			this.SplitContainer2.ResumeLayout(false);
			this.ContextMenuStrip2.ResumeLayout(false);
			this.ContextMenuStrip3.ResumeLayout(false);
			this.SplitContainer3.Panel1.ResumeLayout(false);
			this.SplitContainer3.Panel2.ResumeLayout(false);
			this.SplitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.SplitContainer SplitContainer2;
		internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
		internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel2;
		internal System.Windows.Forms.SplitContainer SplitContainer3;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem PackEverythingToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem UnpackEverythingToolStripMenuItem;
		internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip2;
		internal System.Windows.Forms.ToolStripMenuItem PackAllToolStripMenuItem;
		internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip3;
		internal System.Windows.Forms.ToolStripMenuItem UnpackAllToolStripMenuItem;
	}

}
