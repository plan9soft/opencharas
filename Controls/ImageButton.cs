using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OpenCharas.Controls
{
	public class ImageButton : PictureBox
	{
		Image _normal, _hovered, _pressed;

		public ImageButton()
		{
		}

		public Image Normal
		{
			get { return _normal; }
			set
			{
				_normal = value;
				Size = _normal.Size;
				Image = Normal;
				Invalidate();
			}
		}

		public Image Hovered
		{
			get { return _hovered; }
			set
			{
				_hovered = value;
				Invalidate();
			}
		}

		public Image Pressed
		{
			get { return _pressed; }
			set
			{
				_pressed = value;
				Invalidate();
			}
		}

		enum ButtonState
		{
			Normal,
			Hovered,
			Pressed
		}

		ButtonState _state;
		bool _moused = false;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			_moused = true;
			_state = ButtonState.Pressed;
			Image = Pressed;

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			_moused = false;
			_state = ButtonState.Hovered;
			Image = Hovered;

			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_moused)
			{
				_state = ButtonState.Pressed;
				Image = Pressed;
			}
			else
			{
				_state = ButtonState.Hovered;
				Image = Hovered;
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			_state = ButtonState.Normal;
			Image = Normal;
			base.OnMouseLeave(e);
		}
	}
}
