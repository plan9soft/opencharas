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
		private System.ComponentModel.Container components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.Load += new System.EventHandler(ItemsWindow_Load);
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ItemsWindow_FormClosing);
			base.Move += new System.EventHandler(ItemsWindow_Move);
			base.Resize += new System.EventHandler(ItemsWindow_Resize);
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
			this.FlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
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
			this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
			//
			//SplitContainer1.Panel2
			//
			this.SplitContainer1.Panel2.Controls.Add(this.FlowLayoutPanel1);
			this.SplitContainer1.Size = new System.Drawing.Size(374, 268);
			this.SplitContainer1.SplitterDistance = 133;
			this.SplitContainer1.TabIndex = 0;
			//
			//TreeView1
			//
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.HideSelection = false;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(129, 264);
			this.TreeView1.TabIndex = 0;
			//
			//FlowLayoutPanel1
			//
			this.FlowLayoutPanel1.AutoScroll = true;
			this.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
			this.FlowLayoutPanel1.Size = new System.Drawing.Size(233, 264);
			this.FlowLayoutPanel1.TabIndex = 0;
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
			//ItemsWindow
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 268);
			this.Controls.Add(this.SplitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(382, 291);
			this.Name = "ItemsWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Items";
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel1;
		internal System.Windows.Forms.ToolTip ToolTip1;
	}

}
