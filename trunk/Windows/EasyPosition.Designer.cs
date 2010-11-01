using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class EasyPosition : System.Windows.Forms.Form
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
			this.Button1 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button3 = new System.Windows.Forms.Button();
			this.Button4 = new System.Windows.Forms.Button();
			this.Button5 = new System.Windows.Forms.Button();
			this.Button6 = new System.Windows.Forms.Button();
			this.Button7 = new System.Windows.Forms.Button();
			this.Button8 = new System.Windows.Forms.Button();
			this.Button9 = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Button1
			// 
			this.Button1.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button1.Image = global::OpenCharas.Properties.Resources.up;
			this.Button1.Location = new System.Drawing.Point(45, 42);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(32, 32);
			this.Button1.TabIndex = 0;
			this.Button1.UseVisualStyleBackColor = false;
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// Button2
			// 
			this.Button2.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button2.Image = global::OpenCharas.Properties.Resources.bottom;
			this.Button2.Location = new System.Drawing.Point(45, 114);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(32, 32);
			this.Button2.TabIndex = 1;
			this.Button2.UseVisualStyleBackColor = false;
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// Button3
			// 
			this.Button3.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button3.Image = global::OpenCharas.Properties.Resources.left;
			this.Button3.Location = new System.Drawing.Point(9, 78);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(32, 32);
			this.Button3.TabIndex = 2;
			this.Button3.UseVisualStyleBackColor = false;
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// Button4
			// 
			this.Button4.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button4.Image = global::OpenCharas.Properties.Resources.right;
			this.Button4.Location = new System.Drawing.Point(81, 78);
			this.Button4.Name = "Button4";
			this.Button4.Size = new System.Drawing.Size(32, 32);
			this.Button4.TabIndex = 3;
			this.Button4.UseVisualStyleBackColor = false;
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			// 
			// Button5
			// 
			this.Button5.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button5.Image = global::OpenCharas.Properties.Resources.up_left;
			this.Button5.Location = new System.Drawing.Point(9, 42);
			this.Button5.Name = "Button5";
			this.Button5.Size = new System.Drawing.Size(32, 32);
			this.Button5.TabIndex = 4;
			this.Button5.UseVisualStyleBackColor = false;
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			// 
			// Button6
			// 
			this.Button6.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button6.Image = global::OpenCharas.Properties.Resources.bottom_left;
			this.Button6.Location = new System.Drawing.Point(9, 114);
			this.Button6.Name = "Button6";
			this.Button6.Size = new System.Drawing.Size(32, 32);
			this.Button6.TabIndex = 5;
			this.Button6.UseVisualStyleBackColor = false;
			this.Button6.Click += new System.EventHandler(this.Button6_Click);
			// 
			// Button7
			// 
			this.Button7.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button7.Image = global::OpenCharas.Properties.Resources.bottom_right;
			this.Button7.Location = new System.Drawing.Point(81, 114);
			this.Button7.Name = "Button7";
			this.Button7.Size = new System.Drawing.Size(32, 32);
			this.Button7.TabIndex = 6;
			this.Button7.UseVisualStyleBackColor = false;
			this.Button7.Click += new System.EventHandler(this.Button7_Click);
			// 
			// Button8
			// 
			this.Button8.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button8.Image = global::OpenCharas.Properties.Resources.up_right;
			this.Button8.Location = new System.Drawing.Point(81, 42);
			this.Button8.Name = "Button8";
			this.Button8.Size = new System.Drawing.Size(32, 32);
			this.Button8.TabIndex = 7;
			this.Button8.UseVisualStyleBackColor = false;
			this.Button8.Click += new System.EventHandler(this.Button8_Click);
			// 
			// Button9
			// 
			this.Button9.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Button9.Image = global::OpenCharas.Properties.Resources.center;
			this.Button9.Location = new System.Drawing.Point(45, 78);
			this.Button9.Name = "Button9";
			this.Button9.Size = new System.Drawing.Size(32, 32);
			this.Button9.TabIndex = 8;
			this.Button9.UseVisualStyleBackColor = false;
			this.Button9.Click += new System.EventHandler(this.Button9_Click);
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(1, 9);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(120, 23);
			this.Label1.TabIndex = 9;
			this.Label1.Text = "Label1";
			this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// EasyPosition
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(120, 159);
			this.ControlBox = false;
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.Button9);
			this.Controls.Add(this.Button8);
			this.Controls.Add(this.Button7);
			this.Controls.Add(this.Button6);
			this.Controls.Add(this.Button5);
			this.Controls.Add(this.Button4);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "EasyPosition";
			this.Opacity = 0.75D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Form5";
			this.Deactivate += new System.EventHandler(this.Form5_Deactivate);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form5_FormClosed);
			this.Load += new System.EventHandler(this.Form5_Load);
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button4;
		internal System.Windows.Forms.Button Button5;
		internal System.Windows.Forms.Button Button6;
		internal System.Windows.Forms.Button Button7;
		internal System.Windows.Forms.Button Button8;
		internal System.Windows.Forms.Button Button9;
		internal System.Windows.Forms.Label Label1;
	}

}
