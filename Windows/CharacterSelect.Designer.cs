using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class CharacterSelect
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
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton8 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripContainer1.ContentPanel.SuspendLayout();
			this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.ToolStripContainer1.SuspendLayout();
			this.ToolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeView1
			// 
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.HideSelection = false;
			this.TreeView1.LabelEdit = true;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			this.TreeView1.Size = new System.Drawing.Size(202, 249);
			this.TreeView1.TabIndex = 0;
			this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
			this.TreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
			// 
			// ToolStripContainer1
			// 
			// 
			// ToolStripContainer1.ContentPanel
			// 
			this.ToolStripContainer1.ContentPanel.Controls.Add(this.TreeView1);
			this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(202, 249);
			this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.ToolStripContainer1.Name = "ToolStripContainer1";
			this.ToolStripContainer1.Size = new System.Drawing.Size(202, 274);
			this.ToolStripContainer1.TabIndex = 1;
			this.ToolStripContainer1.Text = "ToolStripContainer1";
			// 
			// ToolStripContainer1.TopToolStripPanel
			// 
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip1);
			// 
			// ToolStrip1
			// 
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton1,
            this.ToolStripButton2,
            this.ToolStripButton5,
            this.ToolStripButton6,
            this.ToolStripSeparator1,
            this.ToolStripButton3,
            this.ToolStripButton4,
            this.ToolStripButton8,
            this.ToolStripButton7});
			this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(202, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 0;
			// 
			// ToolStripButton1
			// 
			this.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton1.Image = global::OpenCharas.Properties.Resources.newchar;
			this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton1.Name = "ToolStripButton1";
			this.ToolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton1.Text = "ToolStripButton1";
			this.ToolStripButton1.ToolTipText = "New Character";
			this.ToolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
			// 
			// ToolStripButton2
			// 
			this.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton2.Image = global::OpenCharas.Properties.Resources.delchar;
			this.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton2.Name = "ToolStripButton2";
			this.ToolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton2.Text = "ToolStripButton2";
			this.ToolStripButton2.ToolTipText = "Delete Character";
			this.ToolStripButton2.Click += new System.EventHandler(this.ToolStripButton2_Click);
			// 
			// ToolStripButton5
			// 
			this.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton5.Image = global::OpenCharas.Properties.Resources.chardown;
			this.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton5.Name = "ToolStripButton5";
			this.ToolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton5.Text = "ToolStripButton5";
			this.ToolStripButton5.ToolTipText = "Move Down";
			this.ToolStripButton5.Click += new System.EventHandler(this.ToolStripButton5_Click);
			// 
			// ToolStripButton6
			// 
			this.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton6.Image = global::OpenCharas.Properties.Resources.charup;
			this.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton6.Name = "ToolStripButton6";
			this.ToolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton6.Text = "ToolStripButton6";
			this.ToolStripButton6.ToolTipText = "Move Up";
			this.ToolStripButton6.Click += new System.EventHandler(this.ToolStripButton6_Click);
			// 
			// ToolStripSeparator1
			// 
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// ToolStripButton3
			// 
			this.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton3.Image = global::OpenCharas.Properties.Resources.newrow;
			this.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton3.Name = "ToolStripButton3";
			this.ToolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton3.Text = "ToolStripButton3";
			this.ToolStripButton3.ToolTipText = "Add Row";
			this.ToolStripButton3.Click += new System.EventHandler(this.ToolStripButton3_Click);
			// 
			// ToolStripButton4
			// 
			this.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton4.Image = global::OpenCharas.Properties.Resources.delrow;
			this.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton4.Name = "ToolStripButton4";
			this.ToolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton4.Text = "Delete Row";
			this.ToolStripButton4.Click += new System.EventHandler(this.ToolStripButton4_Click);
			// 
			// ToolStripButton8
			// 
			this.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton8.Image = global::OpenCharas.Properties.Resources.rowdown;
			this.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton8.Name = "ToolStripButton8";
			this.ToolStripButton8.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton8.Text = "ToolStripButton5";
			this.ToolStripButton8.ToolTipText = "Move Row Down";
			this.ToolStripButton8.Click += new System.EventHandler(this.ToolStripButton8_Click);
			// 
			// ToolStripButton7
			// 
			this.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton7.Image = global::OpenCharas.Properties.Resources.rowup;
			this.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton7.Name = "ToolStripButton7";
			this.ToolStripButton7.Size = new System.Drawing.Size(23, 20);
			this.ToolStripButton7.Text = "ToolStripButton6";
			this.ToolStripButton7.ToolTipText = "Move Row Up";
			this.ToolStripButton7.Click += new System.EventHandler(this.ToolStripButton7_Click);
			// 
			// CharacterSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(202, 274);
			this.Controls.Add(this.ToolStripContainer1);
			this.MinimumSize = new System.Drawing.Size(208, 302);
			this.Name = "CharacterSelect";
			this.ShowInTaskbar = false;
			this.Text = "Character Select";
			this.Load += new System.EventHandler(this.CharacterSelect_Load);
			this.Move += new System.EventHandler(this.CharacterSelect_Move);
			this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
			this.ToolStripContainer1.ResumeLayout(false);
			this.ToolStripContainer1.PerformLayout();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.ToolStripContainer ToolStripContainer1;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton2;
		internal System.Windows.Forms.ToolStripButton ToolStripButton3;
		internal System.Windows.Forms.ToolStripButton ToolStripButton4;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton5;
		internal System.Windows.Forms.ToolStripButton ToolStripButton6;
		internal System.Windows.Forms.ToolStripButton ToolStripButton7;
		internal System.Windows.Forms.ToolStripButton ToolStripButton8;
	}

}
