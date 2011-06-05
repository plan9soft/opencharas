using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenCharas;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace OpenCharasUpdater
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Updater.Version myVer = new Updater.Version(Application.ProductVersion);

		void LoadedUpdates(List<Updater.Update> updates)
		{
			if (updates == null)
			{
				MessageBox.Show("There was a problem retrieving updates. Please try again later.");
				Close();
				return;
			}

			Invoke((Action)delegate()
				{
					foreach (var u in updates)
					{
						if (u.Version <= myVer)
							break;

						listBox1.Items.Add(u);
					}
				});
		}

		void UpdatePercent(Updater.PercentClass perc)
		{
			Invoke((Action)delegate()
			{
				labelProgressBar1.Maximum = (int)(perc.dlTotal);
				labelProgressBar1.Value = (int)(perc.dlNow);
			});
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (!File.Exists("version.tmp"))
				Updater.CheckUpdates(LoadedUpdates, null);
			else
			{
				using (var r = new StreamReader("version.tmp"))
					LoadedUpdates(Updater.GetUpdates(r, false));
			}
		}

		private void listBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedItem == null)
				richTextBox1.Text = "";
			else
			{
				richTextBox1.Text = "";
				var upd = (Updater.Update)listBox1.SelectedItem;

				if (upd.Version < myVer)
					richTextBox1.Text += "You already have this update.\n\n";
				else if (upd.Version == myVer)
					richTextBox1.Text += "This is your current version.\n\n";

				richTextBox1.Text +=
					"Version: " + upd.Version.ToString() + "\n" +
					"Date Released: " + upd.Date + "\n" + 
					"Major Released?: " + (upd.Major ? "Yes" : "No") + "\n\n" +
					upd.Description.Replace("\\n", "\n");
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Updater.EndThreads();
			File.Delete("version.tmp");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int i = 0;
			bool found = false;

			for (; i < listBox1.Items.Count; ++i)
			{
				var upd = (Updater.Update)listBox1.Items[i];

				if (upd.Version <= myVer)
				{
					found = true;
					i--;
					break;
				}
				else if (upd.Major)
				{
					found = true;
					break;
				}
			}

			if (i < 0)
				return;

			if (!found)
				i--;

			button2.Enabled = false;

			_beginUpdate = i;
			DownUpdate(i);
		}

		int _currentUpdate, _beginUpdate;
		public void DownUpdate(int i)
		{
			_currentUpdate = i;
			listBox1.SelectedIndex = i;
			var upd = (Updater.Update)listBox1.Items[i];

			labelProgressBar1.ReportMessage = "Downloading v"+upd.Version.ToString()+"...";

			Updater.DownloadFileAsync("http://opencharas.alteredsoftworks.com/updater/" + upd.Version.ToString() + "/update.zip",
				
				stream =>
				{
					using (var fs = File.Create(upd.Version.ToString()+".zip"))
						stream.CopyTo(fs);

					if (_currentUpdate != 0)
						Invoke((Action)delegate() { DownUpdate(_currentUpdate - 1); });
					else
						Invoke((Action)delegate() { InstallUpdate(_beginUpdate); });
				},

				UpdatePercent
			);
		}

		public void InstallUpdate(int i)
		{
			_currentUpdate = i;
			listBox1.SelectedIndex = i;
			var upd = (Updater.Update)listBox1.Items[i];

			labelProgressBar1.ReportMessage = "Extracting v"+upd.Version.ToString()+"...";

			FastZip fz = new FastZip();
			fz.ExtractZip(upd.Version.ToString()+".zip", ".", "");

			File.Delete(upd.Version.ToString()+".zip");

			if (_currentUpdate != 0)
				InstallUpdate(_currentUpdate - 1);
			else
				labelProgressBar1.ReportMessage = "Done";

			myVer = upd.Version;
		}
	}
}
