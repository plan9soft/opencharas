using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class CrashReport : System.Windows.Forms.Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrashReport));
			this.OK_Button = new System.Windows.Forms.Button();
			this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
			base.Load += new System.EventHandler(CrashReport_Load);
			this.Label1 = new System.Windows.Forms.Label();
			this.PictureBox1 = new System.Windows.Forms.PictureBox();
			this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
			this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)this.PictureBox1).BeginInit();
			this.SuspendLayout();
			//
			//OK_Button
			//
			this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.OK_Button.Location = new System.Drawing.Point(190, 306);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(67, 23);
			this.OK_Button.TabIndex = 0;
			this.OK_Button.Text = "OK";
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(73, 18);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(336, 206);
			this.Label1.TabIndex = 1;
			this.Label1.Text = resources.GetString("Label1.Text");
			//
			//PictureBox1
			//
			this.PictureBox1.Location = new System.Drawing.Point(12, 18);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new System.Drawing.Size(40, 40);
			this.PictureBox1.TabIndex = 2;
			this.PictureBox1.TabStop = false;
			//
			//LinkLabel1
			//
			this.LinkLabel1.AutoSize = true;
			this.LinkLabel1.Location = new System.Drawing.Point(73, 279);
			this.LinkLabel1.Name = "LinkLabel1";
			this.LinkLabel1.Size = new System.Drawing.Size(104, 13);
			this.LinkLabel1.TabIndex = 3;
			this.LinkLabel1.TabStop = true;
			this.LinkLabel1.Text = "Submit Crash Report";
			//
			//TextBox1
			//
			this.TextBox1.Location = new System.Drawing.Point(162, 227);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(148, 20);
			this.TextBox1.TabIndex = 4;
			//
			//TextBox2
			//
			this.TextBox2.Location = new System.Drawing.Point(162, 253);
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.Size = new System.Drawing.Size(148, 20);
			this.TextBox2.TabIndex = 5;
			//
			//Label2
			//
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(73, 230);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(38, 13);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "Name:";
			//
			//Label3
			//
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(73, 256);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(83, 13);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "Email (Optional):";
			//
			//CrashReport
			//
			this.AcceptButton = this.OK_Button;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(446, 341);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.TextBox2);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.LinkLabel1);
			this.Controls.Add(this.PictureBox1);
			this.Controls.Add(this.Label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CrashReport";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CrashReport";
			((System.ComponentModel.ISupportInitialize)this.PictureBox1).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.Button OK_Button;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.PictureBox PictureBox1;
		internal System.Windows.Forms.LinkLabel LinkLabel1;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.TextBox TextBox2;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;

	}

}
