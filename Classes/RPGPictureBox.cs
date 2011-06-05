using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace OpenCharas
{
	public class RPGPictureBox
	{
		public RPGImage MainImage { get; set; }
		public Bitmap MyImage { get; set; }
		public Bitmap PreviewImage { get; set; }
		public Size Size { get; set; }

		public static readonly RPGPictureBox EmptyBox = Create(null);

		public void ClickProcess()
		{
			if (!Program.imagePackerForm.Visible)
			{
				if (MainImage != null)
				{
					if ((Images.CurrentGameFile != MainImage.GameFile) && Canvas.Settings.DontAskGameChange == false)
					{
						using (DontAskDialog dad = new DontAskDialog())
						{
							DialogResult result = dad.ShowDialog("You are about to use an image associated with another game. This may result in problems with exporting and animation.\n\nAre you sure you want to do this?", SystemIcons.Question.ToBitmap());

							if (result == DialogResult.Cancel)
								return;

							Canvas.Settings.DontAskGameChange = dad.CheckboxResult;
						}
					}
				}

				// Are we modifying a layer
				if (Program.layersWindowForm.CurrentNode != null)
				{
					if (MainImage != null)
						Program.layersWindowForm.CurrentNode.Layer.SetImage(this);
					else
						Program.layersWindowForm.CurrentNode.Layer.SetImage(null);

					Program.canvasForm.UpdateDrawing();
				}
			}
		}

		/*protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Program.imagePackerForm.Visible)
			{
				if (ImagePacker.ContainsThisBox(Program.imagePackerForm.FlowLayoutPanel1.Controls, this))
					Program.imagePackerForm.FlowLayoutPanel2.Controls.Add(this);
				else if (ImagePacker.ContainsThisBox(Program.imagePackerForm.FlowLayoutPanel2.Controls, this))
					Program.imagePackerForm.FlowLayoutPanel1.Controls.Add(this);
			}

			base.OnMouseDown(e);
		}

		public RPGPictureBox CreatePictureBoxCopy()
		{
			RPGPictureBox Box = new RPGPictureBox();

			Box.Size = new Size(48, 48);
			Box.MainImage = MainImage;
			Box.MyImage = MyImage;
			Box.BackgroundImage = BackgroundImage;
			Box.BackgroundImageLayout = ImageLayout.Center;
			Program.imagePackerForm.ToolTip1.SetToolTip(Box, MainImage.Name);

			return Box;
		}*/

		public static bool BitmapEmpty(Bitmap bmp, Color emptyColor, bool transparentOnly)
		{
			using (FastPixel fp = new FastPixel(bmp, false))
			{
				for (int y = 0; y < fp.Height; ++y)
				{
					for (int x = 0; x < fp.Width; ++x)
					{
						Color c = fp.GetPixel(x, y);

						if (transparentOnly && (c.A != emptyColor.A) ||
							!transparentOnly && (c != emptyColor))
							return false;
					}
				}
			}

			return true;
		}

		public static Bitmap CreateImagePreview(Bitmap bmp)
		{
			Point mins = new Point(9999, 9999);
			Point maxs = new Point(-9999, -9999);

			using (FastPixel fp = new FastPixel(bmp, true))
			{
				for (int y = 0; y < bmp.Height; ++y)
				{
					for (int x = 0; x < bmp.Width; ++x)
					{
						if (fp.GetPixel(x, y).A != 0)
						{
							if (x < mins.X)
								mins.X = x;
							if (y < mins.Y)
								mins.Y = y;

							if (x > maxs.X)
								maxs.X = x;
							if (y > maxs.Y)
								maxs.Y = y;
						}
					}
				}
			}

			try
			{
				Bitmap b = new Bitmap((maxs.X - mins.X) + 1, (maxs.Y - mins.Y) + 1);
				using (Graphics g = Graphics.FromImage(b))
					g.DrawImage(bmp, new Rectangle(0, 0, b.Width, b.Height), new Rectangle(mins.X, mins.Y, b.Width, b.Height), GraphicsUnit.Pixel);
				return b;
			}
			catch
			{
				return bmp;
			}
		}

		public static RPGPictureBox Create(RPGImage MainImage)
		{
			RPGPictureBox Box = new RPGPictureBox();

			Box.MainImage = MainImage;

			// Create the preview image
			if (MainImage != null)
			{
				if (MainImage.GameFile != null)
				{
					Size FrameSize = new Size(MainImage.RawBitmap.Width / MainImage.GameFile.SheetRows,
						MainImage.RawBitmap.Height / MainImage.GameFile.SheetColumns);
					Box.Size = FrameSize;

					int curFrame = MainImage.GameFile.PreviewFrame;
					bool triedPreviewFrame = false;

					do
					{
						Box.MyImage = new Bitmap(FrameSize.Width, FrameSize.Height,
							System.Drawing.Imaging.PixelFormat.Format32bppArgb);

						using (FastPixel MyImageFP = new FastPixel(Box.MyImage, true))
						{
							using (FastPixel RawImageFP = new FastPixel(Box.MainImage.RawBitmap, false))
							{
								Rectangle Rect = Character.GetRectangleForBitmapFrame(Box.MainImage.RawBitmap,
									curFrame, MainImage.GameFile.SheetRows, MainImage.GameFile.SheetColumns);

								for (int i = 0; i <= FrameSize.Width - 1; i++)
								{
									for (int z = 0; z <= FrameSize.Height - 1; z++)
										MyImageFP.SetPixel(i, z, RawImageFP.GetPixel(Rect.X + i, Rect.Y + z));
								}
							}
						}

						if (!triedPreviewFrame)
						{
							triedPreviewFrame = true;
							curFrame = 0;
						}
						else
							curFrame++;

						if (curFrame >= MainImage.GameFile.SheetRows * MainImage.GameFile.SheetColumns)
							break;
					}
					while (BitmapEmpty(Box.MyImage, Color.FromArgb(0, 0, 0, 0), true));

					Box.PreviewImage = CreateImagePreview(Box.MyImage);
				}
				else
					Box.PreviewImage = Properties.Resources.delete;
			}
			else
				Box.PreviewImage = Properties.Resources.delete;

			return Box;
		}
	}
}
