using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace InstallerFury.Controls
{
	public class LabelProgressBar : ProgressBar
	{
		public LabelProgressBar()
		{
		}

		string _reportMessage = null;
		public string ReportMessage
		{
			get { return _reportMessage; }
			set { _reportMessage = value; Invalidate(); }
		}

		const int WmPaint = 15;

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			switch (m.Msg)
			{
			case WmPaint:
				using (var graphics = Graphics.FromHwnd(Handle))
				{
					if (string.IsNullOrEmpty(_reportMessage))
						return;

					TextRenderer.DrawText(
						graphics,
						_reportMessage,
						DefaultFont,
						ClientRectangle,
						Color.Black,
						TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				}
				break;
			}
		}
	}
}
