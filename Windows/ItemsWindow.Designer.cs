using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class ItemsWindow : System.Windows.Forms.Form
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

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.imageViewerContainer1 = new OpenCharas.ImageViewerContainer();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// SplitContainer1
			// 
			this.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer1.Name = "SplitContainer1";
			// 
			// SplitContainer1.Panel1
			// 
			this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
			// 
			// SplitContainer1.Panel2
			// 
			this.SplitContainer1.Panel2.Controls.Add(this.imageViewerContainer1);
			this.SplitContainer1.Panel2.SizeChanged += new System.EventHandler(this.SplitContainer1_Panel2_SizeChanged);
			this.SplitContainer1.Size = new System.Drawing.Size(410, 268);
			this.SplitContainer1.SplitterDistance = 145;
			this.SplitContainer1.TabIndex = 0;
			// 
			// TreeView1
			// 
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.HideSelection = false;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(141, 264);
			this.TreeView1.TabIndex = 0;
			this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
			// 
			// imageViewerContainer1
			// 
			this.imageViewerContainer1.AutoScroll = true;
			this.imageViewerContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageViewerContainer1.Images = null;
			this.imageViewerContainer1.Location = new System.Drawing.Point(0, 0);
			this.imageViewerContainer1.Name = "imageViewerContainer1";
			this.imageViewerContainer1.Size = new System.Drawing.Size(257, 264);
			this.imageViewerContainer1.TabIndex = 0;
			this.imageViewerContainer1.Text = "imageViewerContainer1";
			this.imageViewerContainer1.Click += new System.EventHandler(this.imageViewerContainer1_Click);
			this.imageViewerContainer1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.imageViewerContainer1_MouseDoubleClick);
			// 
			// ToolTip1
			// 
			this.ToolTip1.AutomaticDelay = 5000;
			this.ToolTip1.AutoPopDelay = 3000;
			this.ToolTip1.InitialDelay = 100;
			this.ToolTip1.ReshowDelay = 0;
			this.ToolTip1.ShowAlways = true;
			this.ToolTip1.UseAnimation = false;
			this.ToolTip1.UseFading = false;
			// 
			// ItemsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(410, 268);
			this.Controls.Add(this.SplitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(426, 302);
			this.Name = "ItemsWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Items";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemsWindow_FormClosing);
			this.Load += new System.EventHandler(this.ItemsWindow_Load);
			this.Move += new System.EventHandler(this.ItemsWindow_Move);
			this.Resize += new System.EventHandler(this.ItemsWindow_Resize);
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.ToolTip ToolTip1;
		private System.ComponentModel.IContainer components;
		public ImageViewerContainer imageViewerContainer1;
	}

}
