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
	public partial class DontAskDialog : Form
	{
		public DontAskDialog()
		{
			InitializeComponent();
		}

		private void DontAskDialog_Load(object sender, EventArgs e)
		{

		}

		public DialogResult ShowDialog(string message, Bitmap bmp)
		{
			return ShowDialog();
		}
	}
}
