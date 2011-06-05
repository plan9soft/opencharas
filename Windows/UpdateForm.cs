using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class UpdateForm : Form
	{
		public UpdateForm()
		{
			InitializeComponent();
			System.Media.SystemSounds.Asterisk.Play();
		}

		private void UpdateForm_Load(object sender, EventArgs e)
		{
			pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
			pictureBox1.Image = SystemIcons.Information.ToBitmap();
		}

		public bool AutomaticUpdates { get { return checkBox1.Checked; } set { checkBox1.Checked = value; } }

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Yes;
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.No;
			Close();
		}
	}
}
