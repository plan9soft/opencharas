using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class LayersWindow : System.Windows.Forms.Form
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Shoulder Pads");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Chest");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Body", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Sword");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("On Back");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Accessories", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Glasses");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Hair");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Head", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Pants");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Shoes");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Legs", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
			this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.TreeView1 = new System.Windows.Forms.TreeView();
			this.CheckBox2 = new System.Windows.Forms.CheckBox();
			this.CheckBox1 = new System.Windows.Forms.CheckBox();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.CheckBox3 = new System.Windows.Forms.CheckBox();
			this.TextBox6 = new System.Windows.Forms.TextBox();
			this.TextBox4 = new System.Windows.Forms.TextBox();
			this.TextBox3 = new System.Windows.Forms.TextBox();
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.TextBox7 = new System.Windows.Forms.TextBox();
			this.TrackBar6 = new System.Windows.Forms.TrackBar();
			this.Label7 = new System.Windows.Forms.Label();
			this.TrackBar5 = new System.Windows.Forms.TrackBar();
			this.Label8 = new System.Windows.Forms.Label();
			this.TrackBar3 = new System.Windows.Forms.TrackBar();
			this.TrackBar2 = new System.Windows.Forms.TrackBar();
			this.TrackBar1 = new System.Windows.Forms.TrackBar();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.NumericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.Label1 = new System.Windows.Forms.Label();
			this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ToolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton8 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton9 = new System.Windows.Forms.ToolStripButton();
			this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.ToolStripContainer1.ContentPanel.SuspendLayout();
			this.ToolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.ToolStripContainer1.SuspendLayout();
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).BeginInit();
			this.ToolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolStripContainer1
			// 
			// 
			// ToolStripContainer1.ContentPanel
			// 
			this.ToolStripContainer1.ContentPanel.Controls.Add(this.SplitContainer1);
			this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(374, 278);
			this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ToolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.ToolStripContainer1.Name = "ToolStripContainer1";
			this.ToolStripContainer1.Size = new System.Drawing.Size(374, 303);
			this.ToolStripContainer1.TabIndex = 5;
			this.ToolStripContainer1.Text = "ToolStripContainer1";
			// 
			// ToolStripContainer1.TopToolStripPanel
			// 
			this.ToolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip1);
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
			this.SplitContainer1.Panel2.AutoScroll = true;
			this.SplitContainer1.Panel2.Controls.Add(this.CheckBox2);
			this.SplitContainer1.Panel2.Controls.Add(this.CheckBox1);
			this.SplitContainer1.Panel2.Controls.Add(this.GroupBox1);
			this.SplitContainer1.Panel2.Controls.Add(this.Label4);
			this.SplitContainer1.Panel2.Controls.Add(this.PictureBox1);
			this.SplitContainer1.Panel2.Controls.Add(this.NumericUpDown2);
			this.SplitContainer1.Panel2.Controls.Add(this.NumericUpDown1);
			this.SplitContainer1.Panel2.Controls.Add(this.Label1);
			this.SplitContainer1.Size = new System.Drawing.Size(374, 278);
			this.SplitContainer1.SplitterDistance = 132;
			this.SplitContainer1.TabIndex = 1;
			// 
			// TreeView1
			// 
			this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TreeView1.HideSelection = false;
			this.TreeView1.LabelEdit = true;
			this.TreeView1.Location = new System.Drawing.Point(0, 0);
			this.TreeView1.Name = "TreeView1";
			treeNode1.Name = "Node3";
			treeNode1.Text = "Shoulder Pads";
			treeNode2.Name = "Node5";
			treeNode2.Text = "Chest";
			treeNode3.Name = "Node0";
			treeNode3.Text = "Body";
			treeNode4.Name = "Node12";
			treeNode4.Text = "Sword";
			treeNode5.Name = "Node13";
			treeNode5.Text = "On Back";
			treeNode6.Name = "Node1";
			treeNode6.Text = "Accessories";
			treeNode7.Name = "Node10";
			treeNode7.Text = "Glasses";
			treeNode8.Name = "Node11";
			treeNode8.Text = "Hair";
			treeNode9.Name = "Node2";
			treeNode9.Text = "Head";
			treeNode10.Name = "Node8";
			treeNode10.Text = "Pants";
			treeNode11.Name = "Node9";
			treeNode11.Text = "Shoes";
			treeNode12.Name = "Node7";
			treeNode12.Text = "Legs";
			this.TreeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9,
            treeNode12});
			this.TreeView1.Size = new System.Drawing.Size(128, 274);
			this.TreeView1.TabIndex = 0;
			this.ToolTip1.SetToolTip(this.TreeView1, "Layers");
			this.TreeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView1_AfterLabelEdit);
			this.TreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
			this.TreeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_AfterSelect);
			this.TreeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
			// 
			// CheckBox2
			// 
			this.CheckBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.CheckBox2.Enabled = false;
			this.CheckBox2.Image = global::OpenCharas.Properties.Resources.flip_ver;
			this.CheckBox2.Location = new System.Drawing.Point(177, 235);
			this.CheckBox2.Name = "CheckBox2";
			this.CheckBox2.Size = new System.Drawing.Size(30, 30);
			this.CheckBox2.TabIndex = 17;
			this.ToolTip1.SetToolTip(this.CheckBox2, "Flip sheet vertically");
			this.CheckBox2.UseVisualStyleBackColor = true;
			this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
			// 
			// CheckBox1
			// 
			this.CheckBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.CheckBox1.Enabled = false;
			this.CheckBox1.Image = global::OpenCharas.Properties.Resources.flip_hor;
			this.CheckBox1.Location = new System.Drawing.Point(177, 203);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(30, 30);
			this.CheckBox1.TabIndex = 16;
			this.ToolTip1.SetToolTip(this.CheckBox1, "Flip sheet horizontally");
			this.CheckBox1.UseVisualStyleBackColor = true;
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			// 
			// GroupBox1
			// 
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.CheckBox3);
			this.GroupBox1.Controls.Add(this.TextBox6);
			this.GroupBox1.Controls.Add(this.TextBox4);
			this.GroupBox1.Controls.Add(this.TextBox3);
			this.GroupBox1.Controls.Add(this.TextBox2);
			this.GroupBox1.Controls.Add(this.TextBox7);
			this.GroupBox1.Controls.Add(this.TrackBar6);
			this.GroupBox1.Controls.Add(this.Label7);
			this.GroupBox1.Controls.Add(this.TrackBar5);
			this.GroupBox1.Controls.Add(this.Label8);
			this.GroupBox1.Controls.Add(this.TrackBar3);
			this.GroupBox1.Controls.Add(this.TrackBar2);
			this.GroupBox1.Controls.Add(this.TrackBar1);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Enabled = false;
			this.GroupBox1.Location = new System.Drawing.Point(10, 32);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(206, 165);
			this.GroupBox1.TabIndex = 15;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Colors";
			// 
			// Label2
			// 
			this.Label2.AutoSize = true;
			this.Label2.Enabled = false;
			this.Label2.Location = new System.Drawing.Point(6, 138);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(34, 13);
			this.Label2.TabIndex = 23;
			this.Label2.Text = "Invert";
			// 
			// CheckBox3
			// 
			this.CheckBox3.AutoSize = true;
			this.CheckBox3.Location = new System.Drawing.Point(145, 138);
			this.CheckBox3.Name = "CheckBox3";
			this.CheckBox3.Size = new System.Drawing.Size(15, 14);
			this.CheckBox3.TabIndex = 22;
			this.CheckBox3.UseVisualStyleBackColor = true;
			this.CheckBox3.CheckedChanged += new System.EventHandler(this.CheckBox3_CheckedChanged);
			// 
			// TextBox6
			// 
			this.TextBox6.Enabled = false;
			this.TextBox6.Location = new System.Drawing.Point(145, 88);
			this.TextBox6.Name = "TextBox6";
			this.TextBox6.Size = new System.Drawing.Size(46, 20);
			this.TextBox6.TabIndex = 21;
			this.TextBox6.Text = "0";
			this.ToolTip1.SetToolTip(this.TextBox6, "Layer\'s transparency");
			this.TextBox6.TextChanged += new System.EventHandler(this.TextBox6_TextChanged);
			// 
			// TextBox4
			// 
			this.TextBox4.Enabled = false;
			this.TextBox4.Location = new System.Drawing.Point(145, 65);
			this.TextBox4.Name = "TextBox4";
			this.TextBox4.Size = new System.Drawing.Size(46, 20);
			this.TextBox4.TabIndex = 20;
			this.TextBox4.Text = "1";
			this.ToolTip1.SetToolTip(this.TextBox4, "Layer\'s transparency");
			this.TextBox4.TextChanged += new System.EventHandler(this.TextBox4_TextChanged);
			// 
			// TextBox3
			// 
			this.TextBox3.Enabled = false;
			this.TextBox3.Location = new System.Drawing.Point(145, 41);
			this.TextBox3.Name = "TextBox3";
			this.TextBox3.Size = new System.Drawing.Size(46, 20);
			this.TextBox3.TabIndex = 19;
			this.TextBox3.Text = "1";
			this.ToolTip1.SetToolTip(this.TextBox3, "Layer\'s transparency");
			this.TextBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
			// 
			// TextBox2
			// 
			this.TextBox2.Enabled = false;
			this.TextBox2.Location = new System.Drawing.Point(145, 17);
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.Size = new System.Drawing.Size(46, 20);
			this.TextBox2.TabIndex = 18;
			this.TextBox2.Text = "0";
			this.ToolTip1.SetToolTip(this.TextBox2, "Layer\'s transparency");
			this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
			// 
			// TextBox7
			// 
			this.TextBox7.Enabled = false;
			this.TextBox7.Location = new System.Drawing.Point(145, 112);
			this.TextBox7.Name = "TextBox7";
			this.TextBox7.Size = new System.Drawing.Size(46, 20);
			this.TextBox7.TabIndex = 17;
			this.TextBox7.Text = "255";
			this.ToolTip1.SetToolTip(this.TextBox7, "Layer\'s transparency");
			this.TextBox7.TextChanged += new System.EventHandler(this.TextBox7_TextChanged);
			// 
			// TrackBar6
			// 
			this.TrackBar6.AutoSize = false;
			this.TrackBar6.Enabled = false;
			this.TrackBar6.Location = new System.Drawing.Point(64, 114);
			this.TrackBar6.Maximum = 255;
			this.TrackBar6.Name = "TrackBar6";
			this.TrackBar6.Size = new System.Drawing.Size(75, 21);
			this.TrackBar6.TabIndex = 16;
			this.TrackBar6.TickStyle = System.Windows.Forms.TickStyle.None;
			this.ToolTip1.SetToolTip(this.TrackBar6, "Layer\'s transparency");
			this.TrackBar6.Value = 255;
			this.TrackBar6.Scroll += new System.EventHandler(this.TrackBar6_Scroll);
			// 
			// Label7
			// 
			this.Label7.AutoSize = true;
			this.Label7.Enabled = false;
			this.Label7.Location = new System.Drawing.Point(6, 115);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(34, 13);
			this.Label7.TabIndex = 15;
			this.Label7.Text = "Alpha";
			// 
			// TrackBar5
			// 
			this.TrackBar5.AutoSize = false;
			this.TrackBar5.Enabled = false;
			this.TrackBar5.Location = new System.Drawing.Point(64, 90);
			this.TrackBar5.Maximum = 100;
			this.TrackBar5.Minimum = -100;
			this.TrackBar5.Name = "TrackBar5";
			this.TrackBar5.Size = new System.Drawing.Size(75, 21);
			this.TrackBar5.TabIndex = 9;
			this.TrackBar5.TickStyle = System.Windows.Forms.TickStyle.None;
			this.ToolTip1.SetToolTip(this.TrackBar5, "Contrast value");
			this.TrackBar5.Scroll += new System.EventHandler(this.TrackBar5_Scroll);
			// 
			// Label8
			// 
			this.Label8.AutoSize = true;
			this.Label8.Enabled = false;
			this.Label8.Location = new System.Drawing.Point(6, 91);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(46, 13);
			this.Label8.TabIndex = 8;
			this.Label8.Text = "Contrast";
			// 
			// TrackBar3
			// 
			this.TrackBar3.AutoSize = false;
			this.TrackBar3.Enabled = false;
			this.TrackBar3.Location = new System.Drawing.Point(64, 67);
			this.TrackBar3.Maximum = 200;
			this.TrackBar3.Name = "TrackBar3";
			this.TrackBar3.Size = new System.Drawing.Size(75, 21);
			this.TrackBar3.TabIndex = 5;
			this.TrackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
			this.ToolTip1.SetToolTip(this.TrackBar3, "Overall brightness value");
			this.TrackBar3.Value = 100;
			this.TrackBar3.Scroll += new System.EventHandler(this.TrackBar3_Scroll);
			// 
			// TrackBar2
			// 
			this.TrackBar2.AutoSize = false;
			this.TrackBar2.Enabled = false;
			this.TrackBar2.Location = new System.Drawing.Point(64, 43);
			this.TrackBar2.Maximum = 500;
			this.TrackBar2.Name = "TrackBar2";
			this.TrackBar2.Size = new System.Drawing.Size(75, 21);
			this.TrackBar2.TabIndex = 4;
			this.TrackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
			this.ToolTip1.SetToolTip(this.TrackBar2, "Color saturation value");
			this.TrackBar2.Value = 100;
			this.TrackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
			// 
			// TrackBar1
			// 
			this.TrackBar1.AutoSize = false;
			this.TrackBar1.Enabled = false;
			this.TrackBar1.Location = new System.Drawing.Point(64, 19);
			this.TrackBar1.Maximum = 360;
			this.TrackBar1.Name = "TrackBar1";
			this.TrackBar1.Size = new System.Drawing.Size(75, 21);
			this.TrackBar1.TabIndex = 3;
			this.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.ToolTip1.SetToolTip(this.TrackBar1, "Hue rotation value (changes color)");
			this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
			// 
			// Label6
			// 
			this.Label6.AutoSize = true;
			this.Label6.Enabled = false;
			this.Label6.Location = new System.Drawing.Point(6, 68);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(56, 13);
			this.Label6.TabIndex = 2;
			this.Label6.Text = "Brightness";
			// 
			// Label5
			// 
			this.Label5.AutoSize = true;
			this.Label5.Enabled = false;
			this.Label5.Location = new System.Drawing.Point(6, 44);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(55, 13);
			this.Label5.TabIndex = 1;
			this.Label5.Text = "Saturation";
			// 
			// Label3
			// 
			this.Label3.AutoSize = true;
			this.Label3.Enabled = false;
			this.Label3.Location = new System.Drawing.Point(6, 20);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(27, 13);
			this.Label3.TabIndex = 0;
			this.Label3.Text = "Hue";
			// 
			// Label4
			// 
			this.Label4.AutoSize = true;
			this.Label4.Enabled = false;
			this.Label4.Location = new System.Drawing.Point(16, 226);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(39, 13);
			this.Label4.TabIndex = 11;
			this.Label4.Text = "Image:";
			// 
			// PictureBox1
			// 
			this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PictureBox1.Enabled = false;
			this.PictureBox1.Location = new System.Drawing.Point(123, 211);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(48, 48);
			this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PictureBox1.TabIndex = 10;
			this.PictureBox1.TabStop = false;
			this.ToolTip1.SetToolTip(this.PictureBox1, "Image Preview.\nDouble-click to\nopen Items window.");
			this.PictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
			// 
			// NumericUpDown2
			// 
			this.NumericUpDown2.Enabled = false;
			this.NumericUpDown2.Location = new System.Drawing.Point(155, 7);
			this.NumericUpDown2.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.NumericUpDown2.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
			this.NumericUpDown2.Name = "NumericUpDown2";
			this.NumericUpDown2.Size = new System.Drawing.Size(46, 20);
			this.NumericUpDown2.TabIndex = 2;
			this.ToolTip1.SetToolTip(this.NumericUpDown2, "Offset on Y axis.\nRight-click in main canvas\nto use easy offsetting tool.");
			this.NumericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
			// 
			// NumericUpDown1
			// 
			this.NumericUpDown1.Enabled = false;
			this.NumericUpDown1.Location = new System.Drawing.Point(103, 7);
			this.NumericUpDown1.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.NumericUpDown1.Minimum = new decimal(new int[] {
            72,
            0,
            0,
            -2147483648});
			this.NumericUpDown1.Name = "NumericUpDown1";
			this.NumericUpDown1.Size = new System.Drawing.Size(46, 20);
			this.NumericUpDown1.TabIndex = 1;
			this.ToolTip1.SetToolTip(this.NumericUpDown1, "Offset on X axis.\nRight-click in main canvas\nto use easy offsetting tool.");
			this.NumericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Enabled = false;
			this.Label1.Location = new System.Drawing.Point(16, 9);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(38, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Offset:";
			// 
			// ToolStrip1
			// 
			this.ToolStrip1.AllowItemReorder = true;
			this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton6,
            this.ToolStripButton8,
            this.ToolStripButton7,
            this.ToolStripButton9,
            this.ToolStripButton1});
			this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
			this.ToolStrip1.Name = "ToolStrip1";
			this.ToolStrip1.Size = new System.Drawing.Size(374, 25);
			this.ToolStrip1.Stretch = true;
			this.ToolStrip1.TabIndex = 4;
			// 
			// ToolStripButton6
			// 
			this.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton6.Image = global::OpenCharas.Properties.Resources.add_layer;
			this.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton6.Name = "ToolStripButton6";
			this.ToolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton6.Text = "ToolStripButton6";
			this.ToolStripButton6.ToolTipText = "Add Layer";
			this.ToolStripButton6.Click += new System.EventHandler(this.ToolStripButton6_Click);
			// 
			// ToolStripButton8
			// 
			this.ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton8.Image = global::OpenCharas.Properties.Resources.move_layer;
			this.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton8.Name = "ToolStripButton8";
			this.ToolStripButton8.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton8.Text = "ToolStripButton8";
			this.ToolStripButton8.ToolTipText = "Add Child Layer";
			this.ToolStripButton8.Click += new System.EventHandler(this.ToolStripButton8_Click);
			// 
			// ToolStripButton7
			// 
			this.ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton7.Image = global::OpenCharas.Properties.Resources.rem_layer;
			this.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton7.Name = "ToolStripButton7";
			this.ToolStripButton7.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton7.Text = "ToolStripButton7";
			this.ToolStripButton7.ToolTipText = "Remove Layer";
			this.ToolStripButton7.Click += new System.EventHandler(this.ToolStripButton7_Click);
			// 
			// ToolStripButton9
			// 
			this.ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton9.Image = global::OpenCharas.Properties.Resources.reorder_up;
			this.ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton9.Name = "ToolStripButton9";
			this.ToolStripButton9.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton9.Text = "ToolStripButton9";
			this.ToolStripButton9.ToolTipText = "Re-order Layer Up";
			this.ToolStripButton9.Click += new System.EventHandler(this.ToolStripButton9_Click);
			// 
			// ToolStripButton1
			// 
			this.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButton1.Image = global::OpenCharas.Properties.Resources.reorder_down;
			this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButton1.Name = "ToolStripButton1";
			this.ToolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.ToolStripButton1.Text = "ToolStripButton1";
			this.ToolStripButton1.ToolTipText = "Re-order Layer Down";
			this.ToolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
			// 
			// LayersWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(374, 303);
			this.Controls.Add(this.ToolStripContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(382, 326);
			this.Name = "LayersWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Layers";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LayersWindow_FormClosing);
			this.Load += new System.EventHandler(this.LayersWindow_Load);
			this.Move += new System.EventHandler(this.LayersWindow_Move);
			this.Resize += new System.EventHandler(this.LayersWindow_Resize);
			this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.ToolStripContainer1.TopToolStripPanel.PerformLayout();
			this.ToolStripContainer1.ResumeLayout(false);
			this.ToolStripContainer1.PerformLayout();
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.Panel2.PerformLayout();
			this.SplitContainer1.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).EndInit();
			this.ToolStrip1.ResumeLayout(false);
			this.ToolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.ToolStripContainer ToolStripContainer1;
		internal System.Windows.Forms.SplitContainer SplitContainer1;
		internal System.Windows.Forms.TreeView TreeView1;
		internal System.Windows.Forms.NumericUpDown NumericUpDown2;
		internal System.Windows.Forms.NumericUpDown NumericUpDown1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TrackBar TrackBar5;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.TrackBar TrackBar2;
		internal System.Windows.Forms.TrackBar TrackBar1;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox TextBox7;
		internal System.Windows.Forms.TrackBar TrackBar6;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal System.Windows.Forms.CheckBox CheckBox2;
		internal System.Windows.Forms.CheckBox CheckBox1;
		internal System.Windows.Forms.ToolStrip ToolStrip1;
		internal System.Windows.Forms.ToolStripButton ToolStripButton6;
		internal System.Windows.Forms.ToolStripButton ToolStripButton8;
		internal System.Windows.Forms.ToolStripButton ToolStripButton7;
		internal System.Windows.Forms.ToolStripButton ToolStripButton9;
		internal System.Windows.Forms.ToolStripButton ToolStripButton1;
		internal System.Windows.Forms.TextBox TextBox6;
		internal System.Windows.Forms.TextBox TextBox4;
		internal System.Windows.Forms.TextBox TextBox3;
		internal System.Windows.Forms.TextBox TextBox2;
		internal System.Windows.Forms.TrackBar TrackBar3;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.CheckBox CheckBox3;
		private System.ComponentModel.IContainer components;
	}

}
