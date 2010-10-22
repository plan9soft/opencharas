using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class SetValues : System.Windows.Forms.Form
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
			this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			base.Load += new System.EventHandler(Dialog1_Load);
			this.OK_Button = new System.Windows.Forms.Button();
			this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
			this.Label1 = new System.Windows.Forms.Label();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.NumericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.NumericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.NumericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.NumericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.TableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			//
			//TableLayoutPanel1
			//
			this.TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.TableLayoutPanel1.ColumnCount = 2;
			this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
			this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
			this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
			this.TableLayoutPanel1.Location = new System.Drawing.Point(18, 124);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 1;
			this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
			this.TableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
			this.TableLayoutPanel1.TabIndex = 0;
			//
			//OK_Button
			//
			this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.OK_Button.Location = new System.Drawing.Point(3, 3);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(67, 23);
			this.OK_Button.TabIndex = 0;
			this.OK_Button.Text = "OK";
			//
			//Cancel_Button
			//
			this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new System.Drawing.Point(76, 3);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(67, 23);
			this.Cancel_Button.TabIndex = 1;
			this.Cancel_Button.Text = "Cancel";
			//
			//Label1
			//
			this.Label1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(7, 17);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(57, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Set Width:";
			//
			//Label2
			//
			this.Label2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Label2.AutoSize = true;
			this.Label2.Location = new System.Drawing.Point(7, 43);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(60, 13);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Set Height:";
			//
			//Label3
			//
			this.Label3.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Label3.AutoSize = true;
			this.Label3.Location = new System.Drawing.Point(7, 69);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(68, 13);
			this.Label3.TabIndex = 3;
			this.Label3.Text = "Sheet Rows:";
			//
			//Label4
			//
			this.Label4.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.Label4.AutoSize = true;
			this.Label4.Location = new System.Drawing.Point(7, 95);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(81, 13);
			this.Label4.TabIndex = 4;
			this.Label4.Text = "Sheet Columns:";
			//
			//NumericUpDown1
			//
			this.NumericUpDown1.Location = new System.Drawing.Point(101, 15);
			this.NumericUpDown1.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
			this.NumericUpDown1.Name = "NumericUpDown1";
			this.NumericUpDown1.Size = new System.Drawing.Size(70, 20);
			this.NumericUpDown1.TabIndex = 5;
			//
			//NumericUpDown2
			//
			this.NumericUpDown2.Location = new System.Drawing.Point(101, 41);
			this.NumericUpDown2.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
			this.NumericUpDown2.Name = "NumericUpDown2";
			this.NumericUpDown2.Size = new System.Drawing.Size(70, 20);
			this.NumericUpDown2.TabIndex = 6;
			//
			//NumericUpDown3
			//
			this.NumericUpDown3.Location = new System.Drawing.Point(101, 67);
			this.NumericUpDown3.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			this.NumericUpDown3.Name = "NumericUpDown3";
			this.NumericUpDown3.Size = new System.Drawing.Size(70, 20);
			this.NumericUpDown3.TabIndex = 7;
			//
			//NumericUpDown4
			//
			this.NumericUpDown4.Location = new System.Drawing.Point(101, 93);
			this.NumericUpDown4.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
			this.NumericUpDown4.Name = "NumericUpDown4";
			this.NumericUpDown4.Size = new System.Drawing.Size(70, 20);
			this.NumericUpDown4.TabIndex = 8;
			//
			//SetValues
			//
			this.AcceptButton = this.OK_Button;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel_Button;
			this.ClientSize = new System.Drawing.Size(183, 165);
			this.Controls.Add(this.NumericUpDown4);
			this.Controls.Add(this.NumericUpDown3);
			this.Controls.Add(this.NumericUpDown2);
			this.Controls.Add(this.NumericUpDown1);
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label2);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.TableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetValues";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Set Sheet";
			this.TableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
		internal System.Windows.Forms.Button OK_Button;
		internal System.Windows.Forms.Button Cancel_Button;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.NumericUpDown NumericUpDown1;
		internal System.Windows.Forms.NumericUpDown NumericUpDown2;
		internal System.Windows.Forms.NumericUpDown NumericUpDown3;
		internal System.Windows.Forms.NumericUpDown NumericUpDown4;

	}

}
