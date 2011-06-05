using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OpenCharas.Controls
{
	public class PictureBoxEx : PictureBox
	{
		public PictureBoxEx()
		{
		}

		public InterpolationMode InterpolationMode { get; set; }

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = InterpolationMode;

			base.OnPaint(e);
		}
	}
}
