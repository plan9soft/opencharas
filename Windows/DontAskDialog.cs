using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class DontAskDialog : Form
	{
		public DontAskDialog()
		{
			InitializeComponent();
		}

		private void DontAskDialog_Load(object sender, EventArgs e)
		{
		}

		public bool CheckboxResult
		{
			get { return checkBox1.Checked; }
		}

		public DialogResult ShowDialog(string message, Bitmap bmp)
		{
			label1.Text = message;
			pictureBox1.Image = bmp;
			return ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}
	}
}
