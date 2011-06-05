using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace OpenCharas
{
	public class AwesomeToolStripRenderer : ToolStripRenderer
	{
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			e.ArrowColor = Color.Black;
			base.OnRenderArrow(e);
		}

		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			int xOffs = 1;

			if (e.Item.Bounds.Contains(e.Item.Owner.PointToClient(Cursor.Position)) || (e.Item is ToolStripButton && (e.Item as ToolStripButton).Checked))
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

				var rect = new RectangleF(xOffs, 1, e.Item.Width - (xOffs * 2), e.Item.Height - 3);
				var radius = new Size(4, 4);

				using (var path = AeroRenderer.GetRoundedRectangle(rect, radius))
				{
					if (e.Item.Pressed)
					{
						e.Graphics.FillPath(new SolidBrush(Color.FromArgb(20, 0, 0, 0)), path);

						AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF((xOffs + 1), 2, e.Item.Width - ((xOffs + 1) * 2), e.Item.Height - 4), radius, 87);
						AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF(xOffs, 1, e.Item.Width - (xOffs * 2), e.Item.Height - 4), radius, 87);
					}

					e.Graphics.DrawPath((e.Item.Pressed ? SystemPens.ControlDarkDark : SystemPens.ButtonShadow), path);
				}
			
				e.Graphics.SmoothingMode = SmoothingMode.Default;
			}
		}

		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderDropDownButtonBackground(e);
		}

		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			base.OnRenderGrip(e);
		}

		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			base.OnRenderImageMargin(e);
		}

		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderItemBackground(e);
		}

		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			base.OnRenderItemCheck(e);

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			var rect = new RectangleF(e.ImageRectangle.X - 2, e.ImageRectangle.Y - 2, e.ImageRectangle.Width + 3, e.ImageRectangle.Height + 3);
			var radius = new Size(4, 4);

			using (var path = AeroRenderer.GetRoundedRectangle(rect, radius))
			{
				e.Graphics.FillPath(new SolidBrush(Color.FromArgb(20, 0, 0, 0)), path);
				AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF(e.ImageRectangle.X - 1, e.ImageRectangle.Y - 1, e.ImageRectangle.Width + 3, e.ImageRectangle.Height + 3), radius, 87);

				e.Graphics.DrawPath((e.Item.Pressed ? SystemPens.ControlDarkDark : SystemPens.ButtonShadow), path);
			}

			e.Graphics.SmoothingMode = SmoothingMode.Default;	
		}

		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			base.OnRenderItemImage(e);
		}

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			e.TextColor = Color.Black;
			base.OnRenderItemText(e);
		}

		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderLabelBackground(e);
		}

		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderMenuItemBackground(e);

			if (e.Item is ToolStripDropDownItem)
			{
				int xOffs = 1;
				if (e.Item.IsOnDropDown)
				{
					e.Graphics.DrawLine(SystemPens.ControlLight, new Point(31, 0), new Point(31, e.Item.Height));
					e.Graphics.DrawLine(SystemPens.ButtonHighlight, new Point(32, 0), new Point(32, e.Item.Height));
					xOffs = 3;
				}
				//else
				//	DrawBackgroundGradient(e.Graphics, e.ToolStrip, e.Item.ContentRectangle);

				if (e.Item.Selected || e.Item.Pressed)
				{
					e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

					var rect = new RectangleF(xOffs, 1, e.Item.Width - (xOffs * 2), e.Item.Height - 3);
					var radius = new Size(4, 4);

					using (var path = AeroRenderer.GetRoundedRectangle(rect, radius))
					{
						if (e.Item.Pressed)
						{
							e.Graphics.FillPath(new SolidBrush(Color.FromArgb(20, 0, 0, 0)), path);

							AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF((xOffs + 1), 2, e.Item.Width - ((xOffs + 1) * 2), e.Item.Height - 4), radius, 87);
							AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF(xOffs, 1, e.Item.Width - (xOffs * 2), e.Item.Height - 4), radius, 87);
						}

						e.Graphics.DrawPath((e.Item.Pressed ? SystemPens.ControlDarkDark : SystemPens.ButtonShadow), path);
					}
					
					e.Graphics.SmoothingMode = SmoothingMode.Default;
				}

				return;
			}
		}

		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderOverflowButtonBackground(e);
		}

		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (e.Item.IsOnDropDown)
			{
				e.Graphics.DrawLine(SystemPens.ControlLight, new Point(29, 0), new Point(29, e.Item.Height));
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, new Point(30, 0), new Point(30, e.Item.Height));

				e.Graphics.DrawLine(SystemPens.ControlLight, new Point(31, (e.Item.Height / 2) - 1), new Point(e.Item.Width, (e.Item.Height / 2) - 1));
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, new Point(31, (e.Item.Height / 2)), new Point(e.Item.Width, (e.Item.Height / 2)));
			}
			else
			{
				e.Graphics.DrawLine(SystemPens.ControlDark, new Point(e.Item.Width / 2, 2), new Point(e.Item.Width / 2, e.Item.Height - 4));
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, new Point((e.Item.Width / 2 + 1), 2), new Point((e.Item.Width / 2) + 1, e.Item.Height - 4));
			}
		}

		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderSplitButtonBackground(e);
		}

		protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
		{
			base.OnRenderStatusStripSizingGrip(e);
		}

		void DrawBackgroundGradient(Graphics g, ToolStrip strip, Rectangle rect)
		{
			var top = strip.Height * 0.3333f;
			var rTop = rect.Height * 0.3333f;
			var gradient = new LinearGradientBrush(new PointF(rect.X, rect.Y), new PointF(rect.X, rTop), Color.White, Color.FromArgb(229, 234, 245));

			g.FillRectangle(gradient, new RectangleF(rect.X, rect.Y, rect.Width, rTop));

			gradient = new LinearGradientBrush(new PointF(rect.X, rTop), new PointF(rect.X, rect.Height), Color.FromArgb(211, 218, 237), Color.FromArgb(225, 230, 246));
			g.FillRectangle(gradient, new RectangleF(rect.X, rect.Height * 0.3333f, rect.Width, (rect.Height - rTop) - 1));

			g.DrawLine(new Pen(Color.FromArgb(182, 188, 204)), new Point(0, strip.Height - 2), new Point(strip.Width, strip.Height - 2));
		}

		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDownMenu)
			{
				e.Graphics.Clear(SystemColors.Control);
				e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
				return;
			}

			DrawBackgroundGradient(e.Graphics, e.ToolStrip, e.AffectedBounds);
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			base.OnRenderToolStripBorder(e);
		}

		protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			base.OnRenderToolStripContentPanelBackground(e);
		}

		protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			base.OnRenderToolStripPanelBackground(e);
		}

		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderToolStripStatusLabelBackground(e);
		}
	}

	public static class AeroRenderer
	{
		public static GraphicsPath GetRoundedRectangle(RectangleF rect, Size radius)
		{
			GraphicsPath p = new GraphicsPath();
			p.StartFigure();
			p.AddArc(new RectangleF(rect.X, rect.Y + rect.Height - radius.Height, radius.Width, radius.Height), 90, 90);
			p.AddArc(new RectangleF(rect.X, rect.Y, radius.Width, radius.Height), 180, 90);
			p.AddArc(new RectangleF(rect.X + rect.Width - radius.Width, rect.Y, radius.Width, radius.Height), 270, 90);
			p.AddArc(new RectangleF(rect.X + rect.Width - radius.Width, rect.Y + rect.Height - radius.Height, radius.Width, radius.Height), 0, 90);
			p.CloseFigure();
			return p;
		}

		public static void DrawShadowPiece(Graphics g, RectangleF rect, Size radius, byte alpha)
		{
			using (GraphicsPath p = new GraphicsPath())
			{
				p.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y + radius.Height);
				p.AddArc(new RectangleF(rect.X, rect.Y, radius.Width, radius.Height), 180, 90);
				p.AddLine(rect.X + radius.Width, rect.Y, rect.X + rect.Width, rect.Y);

				using (var shadowPen = new Pen(Color.FromArgb(alpha, SystemColors.ButtonShadow), 2))
					g.DrawPath(shadowPen, p);
			}
		}

	}
}
