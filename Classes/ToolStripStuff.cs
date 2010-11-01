using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace OpenCharas.Classes
{
	public class ToolStripButton : System.Windows.Forms.ToolStripButton
	{
		bool _down = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			_down = true;
			Invalidate();
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			_down = false;
			Invalidate();
			base.OnMouseUp(e);
		}

		bool _hovering = false;

		protected override void OnMouseEnter(EventArgs e)
		{
			_hovering = true;
			Invalidate();
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			_hovering = false;
			_down = false;
			Invalidate();
			base.OnMouseLeave(e);
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			PushButtonState pbs = (_hovering) ? PushButtonState.Hot : 0;

			if (_down && _hovering)
				pbs = PushButtonState.Pressed;
			if (!Enabled)
				pbs = PushButtonState.Disabled;

			if (pbs != 0)
				ButtonRenderer.DrawButton(e.Graphics, new Rectangle(-1, -1, Width + 2, Height + 2), pbs);

			Point pt = new Point((Width / 2) - (Image.Width / 2), (Height / 2) - (Image.Height / 2));

			if (pbs == PushButtonState.Pressed)
			{
				pt.X += 1;
				pt.Y += 1;
			}

			e.Graphics.DrawImage(Image, pt);
		}
	}
}
