using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class ItemsWindow
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.imageButton1 = new OpenCharas.Controls.ImageButton();
			this.pictureBox1 = new OpenCharas.Controls.PictureBoxEx();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.imageViewerContainer1 = new OpenCharas.ImageViewerContainer();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageButton1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
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
			this.SplitContainer1.Panel2.Controls.Add(this.panel2);
			this.SplitContainer1.Panel2.Controls.Add(this.splitter1);
			this.SplitContainer1.Panel2.Controls.Add(this.panel1);
			this.SplitContainer1.Panel2.SizeChanged += new System.EventHandler(this.SplitContainer1_Panel2_SizeChanged);
			this.SplitContainer1.Size = new System.Drawing.Size(550, 268);
			this.SplitContainer1.SplitterDistance = 168;
			this.SplitContainer1.TabIndex = 0;
			// 
			// TreeView1
			// 
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.HideSelection = false;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(164, 264);
			this.TreeView1.TabIndex = 0;
			this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.imageButton1);
			this.panel2.Controls.Add(this.pictureBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(230, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(144, 264);
			this.panel2.TabIndex = 4;
			this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
			// 
			// imageButton1
			// 
			this.imageButton1.Hovered = global::OpenCharas.Properties.Resources.chav_right_hovered;
			this.imageButton1.Image = global::OpenCharas.Properties.Resources.chev_right_normal;
			this.imageButton1.Location = new System.Drawing.Point(0, 123);
			this.imageButton1.Name = "imageButton1";
			this.imageButton1.Normal = global::OpenCharas.Properties.Resources.chev_right_normal;
			this.imageButton1.Pressed = global::OpenCharas.Properties.Resources.chav_right_pressed;
			this.imageButton1.Size = new System.Drawing.Size(19, 19);
			this.imageButton1.TabIndex = 1;
			this.imageButton1.TabStop = false;
			this.imageButton1.Click += new System.EventHandler(this.imageButton1_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			this.pictureBox1.Location = new System.Drawing.Point(60, 120);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(24, 24);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
			// 
			// splitter1
			// 
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter1.Location = new System.Drawing.Point(225, 0);
			this.splitter1.MinExtra = 8;
			this.splitter1.MinSize = 8;
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 264);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.imageViewerContainer1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(225, 264);
			this.panel1.TabIndex = 2;
			// 
			// imageViewerContainer1
			// 
			this.imageViewerContainer1.AutoScroll = true;
			this.imageViewerContainer1.BaseSize = 20;
			this.imageViewerContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageViewerContainer1.Images = null;
			this.imageViewerContainer1.Location = new System.Drawing.Point(0, 0);
			this.imageViewerContainer1.Mode = OpenCharas.ImageContainerViewMode.Icons;
			this.imageViewerContainer1.Name = "imageViewerContainer1";
			this.imageViewerContainer1.SelectedIndex = -1;
			this.imageViewerContainer1.Size = new System.Drawing.Size(225, 264);
			this.imageViewerContainer1.TabIndex = 0;
			this.imageViewerContainer1.Zoom = 1;
			this.imageViewerContainer1.SelectedBoxChanged += new System.EventHandler(this.imageViewerContainer1_SelectedBoxChanged);
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
			this.ClientSize = new System.Drawing.Size(550, 268);
			this.Controls.Add(this.SplitContainer1);
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
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.imageButton1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.ToolTip ToolTip1;
		private System.ComponentModel.IContainer components;
		public ImageViewerContainer imageViewerContainer1;
		private Panel panel2;
		private Splitter splitter1;
		private Panel panel1;
		private Controls.PictureBoxEx pictureBox1;
		private Controls.ImageButton imageButton1;
	}

}
