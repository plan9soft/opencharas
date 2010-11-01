using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class ItemsWindow
	{
		public ItemsWindow()
		{
			InitializeComponent();
		}

		// Loading/Closing

		public void ItemsWindow_Load(System.Object sender, System.EventArgs e)
		{
		}

		public void ItemsWindow_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (Program.canvasForm.FinalClosing)
				return;

			e.Cancel = true;
			this.Hide();
		}

		// Move/resize code

		public void ItemsWindow_Move(System.Object sender, System.EventArgs e)
		{
			if (Program.canvasForm.SkipSizeChanged)
				return;
		}

		public void ItemsWindow_Resize(System.Object sender, System.EventArgs e)
		{
		}

		// Box code
		public void TreeView1_NodeMouseClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
		}

		private void imageViewerContainer1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var b = imageViewerContainer1.GetImageAtLocation(e.Location);

			if (b != null)
				b.ClickProcess();
		}

		private void imageViewerContainer1_Click(object sender, EventArgs e)
		{

		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			GameNodeNode Node = (GameNodeNode)e.Node;

			imageViewerContainer1.Images = Node.RPGNode.Images;
			TreeView1.SelectedNode = Node;
		}

		private void SplitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
		{
			imageViewerContainer1.ResetImage();
		}
	}

	public class RPGPictureBox : Control
	{
		public RPGPictureBox()
		{
		}

		private RPGImage MainImage_;
		public RPGImage MainImage
		{
			get { return MainImage_; }
			set { MainImage_ = value; }
		}

		private Bitmap MyImage_;
		public Bitmap MyImage
		{
			get { return MyImage_; }
			set { MyImage_ = value; }
		}

		static RPGPictureBox EmptyBoxVal = Create(null);
		static public RPGPictureBox EmptyBox
		{
			get { return EmptyBoxVal; }
		}

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

		protected override void OnClick(EventArgs e)
		{
			//ClickProcess();
			base.OnClick(e);
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			ClickProcess();
			base.OnDoubleClick(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
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
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, Width, Height), Border3DStyle.Sunken);

			base.OnPaint(e);
		}

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

		static public RPGPictureBox Create(RPGImage MainImage)
		{
			RPGPictureBox Box = new RPGPictureBox();

			Box.Size = new Size(48, 48);
			Box.BackgroundImageLayout = ImageLayout.Center;
			Box.MainImage = MainImage;

			// Create the preview image
			if (MainImage != null)
			{
				if (MainImage.GameFile != null)
				{
					Size FrameSize = new Size(MainImage.RawBitmap.Width / MainImage.GameFile.SheetRows,
						MainImage.RawBitmap.Height / MainImage.GameFile.SheetColumns);

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

					Box.BackgroundImage = Box.MyImage;

					if (FrameSize.Width > Box.Size.Width && FrameSize.Height > Box.Size.Height)
						Box.BackgroundImageLayout = ImageLayout.Stretch;
				}
				else
				{
					Box.MyImage = null;
					Box.BackgroundImage = null;
				}
			}
			else
			{
				Box.MyImage = null;
				Box.BackgroundImage = null;
			}

			return Box;
		}
	}

	public class ImageViewerContainer : ScrollableControl
	{
		Panel _imagePanel;

		public ImageViewerContainer()
		{
			_imagePanel = new Panel();
			Controls.Add(_imagePanel);

			_imagePanel.MouseDoubleClick += new MouseEventHandler(_imagePanel_MouseDoubleClick);
			Images = null;
		}

		void _imagePanel_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			OnMouseDoubleClick(e);
		}

		int _rows;

		public RPGPictureBox GetImageAtLocation(Point loc)
		{
			int row = loc.X / 48;
			int column = (loc.Y + HorizontalScroll.Value) / 48;
			int v = (column * _rows) + row;

			if (v == 0)
				return RPGPictureBox.EmptyBox;
			else if (v > _images.Count)
				return null;

			return _images[v - 1].Box;
		}

		List<RPGImage> _images;
		public List<RPGImage> Images
		{
			get { return _images; }
			set
			{
				_images = value;
				ResetImage();
			}
		}

		public void ResetImage()
		{
			if (_images == null)
			{
				if (_imagePanel.BackgroundImage != null)
					_imagePanel.BackgroundImage.Dispose();
				_imagePanel.BackgroundImage = null;
				_imagePanel.Size = new Size(0, 0);
			}

			_rows = (Size.Width - SystemInformation.VerticalScrollBarWidth) / 48;

			int y = 0;
			int x = 0;

			if (_images != null)
			{
				for (int i = 0; i < _images.Count; ++i)
				{
					x++;
					if (x >= _rows)
					{
						y += 48;
						x = 0;
					}
				}
			}
			else
				_rows = 1;

			if (_imagePanel.BackgroundImage != null)
			{
				_imagePanel.BackgroundImage.Dispose();
				_imagePanel.BackgroundImage = null;
			}

			_imagePanel.Size = new Size(48 * _rows, y + 48);
			_imagePanel.BackgroundImage = new Bitmap(_imagePanel.Size.Width, _imagePanel.Size.Height);
			
			int num = (_images != null) ? _images.Count + 1 : 1;

			using (var g = Graphics.FromImage(_imagePanel.BackgroundImage))
			{
				y = 0;
				x = 0;

				for (int i = 0; i < num; ++i)
				{
					if (i >= 1)
						g.DrawImage(_images[i - 1].Box.MyImage,
							(x * 48) + (48 / 2) - (_images[i - 1].Box.MyImage.Width / 2),
							y + ((48 / 2) - (_images[i - 1].Box.MyImage.Height / 2)));

					ControlPaint.DrawBorder3D(g, new Rectangle((x * 48), y, 48, 48), Border3DStyle.Sunken);

					x++;
					if (x >= _rows)
					{
						y += 48;
						x = 0;
					}
				}
			}
		}
	}
}
