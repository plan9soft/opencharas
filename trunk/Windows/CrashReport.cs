using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class CrashReport
	{
		public CrashReport()
		{
			InitializeComponent();
		}

		public void OK_Button_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		public void CrashReport_Load(System.Object sender, System.EventArgs e)
		{
			PictureBox1.Image = SystemIcons.Error.ToBitmap();
		}

		public string Folder_Path;
		public void LinkLabel1_LinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (string.IsNullOrEmpty(TextBox1.Text))
			{
				MessageBox.Show("You need to enter a name!");
				return;
			}

			//Process.Start("http://www.glennfamily.us/Paril/Uploader/?name=" + System.Web.HttpUtility.UrlEncode(TextBox1.Text) + "&email=" + System.Web.HttpUtility.UrlEncode(TextBox2.Text))
			MessageBox.Show("Your default email program will now open. Don\'t forget to attach the dump!");
			Process.Start("explorer.exe", Folder_Path.Substring(0, Folder_Path.LastIndexOf("\\")));
			System.Diagnostics.Process.Start("mailto:crashreporting@opencharas.alteredsoftworks.com?subject=Crash Report&body=Name: " + TextBox1.Text + "%0AEmail: " + TextBox2.Text + "%0A%0AI have attached the crash dump.");
		}
	}

}
