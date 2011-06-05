using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class GameEditor : System.Windows.Forms.Form
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
			this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
			this.GameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CloseEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.NumericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.NumericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.CheckBox1 = new System.Windows.Forms.CheckBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.NumericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button1 = new System.Windows.Forms.Button();
			this.ListBox1 = new System.Windows.Forms.ListBox();
			this.ListBox2 = new System.Windows.Forms.ListBox();
			this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MoveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MoveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Label4 = new System.Windows.Forms.Label();
			this.NumericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.Button3 = new System.Windows.Forms.Button();
			this.Button5 = new System.Windows.Forms.Button();
			this.Label5 = new System.Windows.Forms.Label();
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.MenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).BeginInit();
			this.GroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown3)).BeginInit();
			this.GroupBox2.SuspendLayout();
			this.ContextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown4)).BeginInit();
			this.SuspendLayout();
			// 
			// MenuStrip1
			// 
			this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameToolStripMenuItem});
			this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Size = new System.Drawing.Size(509, 24);
			this.MenuStrip1.TabIndex = 0;
			this.MenuStrip1.Text = "MenuStrip1";
			// 
			// GameToolStripMenuItem
			// 
			this.GameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem5,
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2,
            this.ToolStripSeparator2,
            this.ToolStripMenuItem4,
            this.ToolStripSeparator1,
            this.CloseEditorToolStripMenuItem});
			this.GameToolStripMenuItem.Name = "GameToolStripMenuItem";
			this.GameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.GameToolStripMenuItem.Text = "Game";
			// 
			// ToolStripMenuItem5
			// 
			this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
			this.ToolStripMenuItem5.Size = new System.Drawing.Size(210, 22);
			this.ToolStripMenuItem5.Text = "New";
			this.ToolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
			// 
			// ToolStripMenuItem1
			// 
			this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
			this.ToolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
			this.ToolStripMenuItem1.Text = "Save";
			this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
			// 
			// ToolStripMenuItem2
			// 
			this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
			this.ToolStripMenuItem2.Size = new System.Drawing.Size(210, 22);
			this.ToolStripMenuItem2.Text = "Open...";
			this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
			// 
			// ToolStripSeparator2
			// 
			this.ToolStripSeparator2.Name = "ToolStripSeparator2";
			this.ToolStripSeparator2.Size = new System.Drawing.Size(207, 6);
			// 
			// ToolStripMenuItem4
			// 
			this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
			this.ToolStripMenuItem4.Size = new System.Drawing.Size(210, 22);
			this.ToolStripMenuItem4.Text = "Import Reference Image...";
			this.ToolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem4_Click);
			// 
			// ToolStripSeparator1
			// 
			this.ToolStripSeparator1.Name = "ToolStripSeparator1";
			this.ToolStripSeparator1.Size = new System.Drawing.Size(207, 6);
			// 
			// CloseEditorToolStripMenuItem
			// 
			this.CloseEditorToolStripMenuItem.Name = "CloseEditorToolStripMenuItem";
			this.CloseEditorToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.CloseEditorToolStripMenuItem.Text = "Close Editor";
			this.CloseEditorToolStripMenuItem.Click += new System.EventHandler(this.CloseEditorToolStripMenuItem_Click);
			// 
			// NumericUpDown1
			// 
			this.NumericUpDown1.Location = new System.Drawing.Point(94, 19);
			this.NumericUpDown1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.NumericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericUpDown1.Name = "NumericUpDown1";
			this.NumericUpDown1.Size = new System.Drawing.Size(57, 20);
			this.NumericUpDown1.TabIndex = 1;
			this.ToolTip1.SetToolTip(this.NumericUpDown1, "Number of frames per row");
			this.NumericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(7, 21);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(68, 13);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Sheet Rows:";
			// 
			// Label2
			// 
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(7, 47);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(81, 13);
			this.Label2.TabIndex = 4;
			this.Label2.Text = "Sheet Columns:";
			// 
			// NumericUpDown2
			// 
			this.NumericUpDown2.Location = new System.Drawing.Point(94, 45);
			this.NumericUpDown2.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.NumericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericUpDown2.Name = "NumericUpDown2";
			this.NumericUpDown2.Size = new System.Drawing.Size(57, 20);
			this.NumericUpDown2.TabIndex = 3;
			this.ToolTip1.SetToolTip(this.NumericUpDown2, "Number of frames per column");
			this.NumericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NumericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
			// 
			// GroupBox1
			// 
			this.GroupBox1.Controls.Add(this.Label7);
			this.GroupBox1.Controls.Add(this.NumericUpDown5);
			this.GroupBox1.Controls.Add(this.CheckBox1);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.Label3);
			this.GroupBox1.Controls.Add(this.NumericUpDown3);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.NumericUpDown2);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.NumericUpDown1);
			this.GroupBox1.Location = new System.Drawing.Point(12, 102);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(164, 158);
			this.GroupBox1.TabIndex = 5;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Sheet Properties";
			// 
			// Label7
			// 
			this.Label7.AutoSize = true;
			this.Label7.Location = new System.Drawing.Point(7, 121);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(80, 13);
			this.Label7.TabIndex = 10;
			this.Label7.Text = "Preview Frame:";
			// 
			// NumericUpDown5
			// 
			this.NumericUpDown5.Location = new System.Drawing.Point(94, 119);
			this.NumericUpDown5.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.NumericUpDown5.Name = "NumericUpDown5";
			this.NumericUpDown5.Size = new System.Drawing.Size(57, 20);
			this.NumericUpDown5.TabIndex = 9;
			this.ToolTip1.SetToolTip(this.NumericUpDown5, "Frame used in preview windows.");
			this.NumericUpDown5.ValueChanged += new System.EventHandler(this.NumericUpDown5_ValueChanged);
			// 
			// CheckBox1
			// 
			this.CheckBox1.AutoSize = true;
			this.CheckBox1.Location = new System.Drawing.Point(94, 99);
			this.CheckBox1.Name = "CheckBox1";
			this.CheckBox1.Size = new System.Drawing.Size(15, 14);
			this.CheckBox1.TabIndex = 8;
			this.ToolTip1.SetToolTip(this.CheckBox1, "If unchecked, default PNG save method\nwill use transparency, otherwise will use\na" +
        "n 8-bit color palette to determine\ntransparent chroma key.");
			this.CheckBox1.UseVisualStyleBackColor = true;
			this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			// 
			// Label6
			// 
			this.Label6.AutoSize = true;
			this.Label6.Location = new System.Drawing.Point(7, 99);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(31, 13);
			this.Label6.TabIndex = 7;
			this.Label6.Text = "8-Bit:";
			// 
			// Label3
			// 
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(7, 73);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(61, 13);
			this.Label3.TabIndex = 6;
			this.Label3.Text = "Animations:";
			// 
			// NumericUpDown3
			// 
			this.NumericUpDown3.Location = new System.Drawing.Point(94, 71);
			this.NumericUpDown3.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
			this.NumericUpDown3.Name = "NumericUpDown3";
			this.NumericUpDown3.Size = new System.Drawing.Size(57, 20);
			this.NumericUpDown3.TabIndex = 5;
			this.ToolTip1.SetToolTip(this.NumericUpDown3, "Number of animations");
			this.NumericUpDown3.ValueChanged += new System.EventHandler(this.NumericUpDown3_ValueChanged);
			// 
			// GroupBox2
			// 
			this.GroupBox2.Controls.Add(this.Button2);
			this.GroupBox2.Controls.Add(this.Button1);
			this.GroupBox2.Controls.Add(this.ListBox1);
			this.GroupBox2.Controls.Add(this.ListBox2);
			this.GroupBox2.Controls.Add(this.Label4);
			this.GroupBox2.Controls.Add(this.NumericUpDown4);
			this.GroupBox2.Location = new System.Drawing.Point(207, 102);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(290, 221);
			this.GroupBox2.TabIndex = 6;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Animations";
			// 
			// Button2
			// 
			this.Button2.Enabled = false;
			this.Button2.Image = global::OpenCharas.Properties.Resources.g_X;
			this.Button2.Location = new System.Drawing.Point(129, 127);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(32, 32);
			this.Button2.TabIndex = 12;
			this.ToolTip1.SetToolTip(this.Button2, "Removes frame from right.");
			this.Button2.UseVisualStyleBackColor = true;
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// Button1
			// 
			this.Button1.Enabled = false;
			this.Button1.Image = global::OpenCharas.Properties.Resources.g_up;
			this.Button1.Location = new System.Drawing.Point(129, 89);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(32, 32);
			this.Button1.TabIndex = 11;
			this.ToolTip1.SetToolTip(this.Button1, "Adds frame on left to list on right.");
			this.Button1.UseVisualStyleBackColor = true;
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// ListBox1
			// 
			this.ListBox1.FormattingEnabled = true;
			this.ListBox1.Location = new System.Drawing.Point(9, 47);
			this.ListBox1.Name = "ListBox1";
			this.ListBox1.Size = new System.Drawing.Size(112, 160);
			this.ListBox1.TabIndex = 10;
			this.ToolTip1.SetToolTip(this.ListBox1, "List of frames that you can use.\nSelect frames here, then click\nthe green arrow t" +
        "o add them.");
			// 
			// ListBox2
			// 
			this.ListBox2.ContextMenuStrip = this.ContextMenuStrip1;
			this.ListBox2.FormattingEnabled = true;
			this.ListBox2.Location = new System.Drawing.Point(169, 21);
			this.ListBox2.Name = "ListBox2";
			this.ListBox2.Size = new System.Drawing.Size(112, 186);
			this.ListBox2.TabIndex = 9;
			this.ToolTip1.SetToolTip(this.ListBox2, "Frames that this animation will go through.\nDuplicate frames can appear here.\nCli" +
        "ck on a frame here, then the red X to\nremove it.");
			this.ListBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2_SelectedIndexChanged);
			// 
			// ContextMenuStrip1
			// 
			this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveUpToolStripMenuItem,
            this.MoveDownToolStripMenuItem});
			this.ContextMenuStrip1.Name = "ContextMenuStrip1";
			this.ContextMenuStrip1.Size = new System.Drawing.Size(139, 48);
			// 
			// MoveUpToolStripMenuItem
			// 
			this.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem";
			this.MoveUpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.MoveUpToolStripMenuItem.Text = "Move Up";
			this.MoveUpToolStripMenuItem.Click += new System.EventHandler(this.MoveUpToolStripMenuItem_Click);
			// 
			// MoveDownToolStripMenuItem
			// 
			this.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
			this.MoveDownToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.MoveDownToolStripMenuItem.Text = "Move Down";
			this.MoveDownToolStripMenuItem.Click += new System.EventHandler(this.MoveDownToolStripMenuItem_Click);
			// 
			// Label4
			// 
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(3, 21);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(89, 13);
			this.Label4.TabIndex = 7;
			this.Label4.Text = "Select Animation:";
			// 
			// NumericUpDown4
			// 
			this.NumericUpDown4.Location = new System.Drawing.Point(98, 19);
			this.NumericUpDown4.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.NumericUpDown4.Name = "NumericUpDown4";
			this.NumericUpDown4.Size = new System.Drawing.Size(57, 20);
			this.NumericUpDown4.TabIndex = 6;
			this.ToolTip1.SetToolTip(this.NumericUpDown4, "Animation to modify.\nNote that 0 means none.");
			this.NumericUpDown4.ValueChanged += new System.EventHandler(this.NumericUpDown4_ValueChanged);
			// 
			// Button3
			// 
			this.Button3.Enabled = false;
			this.Button3.Location = new System.Drawing.Point(13, 265);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(164, 26);
			this.Button3.TabIndex = 7;
			this.Button3.Text = "Open Preview Window";
			this.ToolTip1.SetToolTip(this.Button3, "Opens preview window");
			this.Button3.UseVisualStyleBackColor = true;
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// Button5
			// 
			this.Button5.Enabled = false;
			this.Button5.Location = new System.Drawing.Point(12, 297);
			this.Button5.Name = "Button5";
			this.Button5.Size = new System.Drawing.Size(87, 26);
			this.Button5.TabIndex = 9;
			this.Button5.Text = "Test Animation";
			this.ToolTip1.SetToolTip(this.Button5, "Switch between animation and sheet mode");
			this.Button5.UseVisualStyleBackColor = true;
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			// 
			// Label5
			// 
			this.Label5.AutoSize = true;
			this.Label5.Location = new System.Drawing.Point(10, 45);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(38, 13);
			this.Label5.TabIndex = 10;
			this.Label5.Text = "Name:";
			// 
			// TextBox1
			// 
			this.TextBox1.Location = new System.Drawing.Point(54, 42);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(140, 20);
			this.TextBox1.TabIndex = 11;
			this.ToolTip1.SetToolTip(this.TextBox1, "Name of this game.\nFor ID purposes in save and\nSet Game menu.");
			this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(54, 68);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(140, 20);
			this.textBox2.TabIndex = 13;
			this.ToolTip1.SetToolTip(this.textBox2, "Folder to save game file in.");
			this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(10, 71);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(39, 13);
			this.label8.TabIndex = 12;
			this.label8.Text = "Folder:";
			// 
			// GameEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(509, 338);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.Button5);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.GroupBox2);
			this.Controls.Add(this.MenuStrip1);
			this.MainMenuStrip = this.MenuStrip1;
			this.Name = "GameEditor";
			this.Text = "Game Editor";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form6_FormClosed);
			this.Load += new System.EventHandler(this.Form6_Load);
			this.Shown += new System.EventHandler(this.GameEditor_Shown);
			this.MenuStrip1.ResumeLayout(false);
			this.MenuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown2)).EndInit();
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown3)).EndInit();
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox2.PerformLayout();
			this.ContextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NumericUpDown4)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.MenuStrip MenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem GameToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
		internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
		internal System.Windows.Forms.ToolStripMenuItem CloseEditorToolStripMenuItem;
		internal System.Windows.Forms.NumericUpDown NumericUpDown1;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.NumericUpDown NumericUpDown2;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.NumericUpDown NumericUpDown3;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.ListBox ListBox1;
		internal System.Windows.Forms.ListBox ListBox2;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.NumericUpDown NumericUpDown4;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button5;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
		internal System.Windows.Forms.ToolStripMenuItem MoveUpToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem MoveDownToolStripMenuItem;
		internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
		internal System.Windows.Forms.CheckBox CheckBox1;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.NumericUpDown NumericUpDown5;
		internal System.Windows.Forms.ToolTip ToolTip1;
		internal TextBox textBox2;
		internal Label label8;
		private System.ComponentModel.IContainer components;
	}

}
