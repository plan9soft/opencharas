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

		private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://opencharas.alteredsoftworks.com/forums/viewforum.php?id=4");
			Process.Start("explorer.exe", Folder_Path.Substring(0, Folder_Path.LastIndexOf("\\")));
		}
	}

}
