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
			FlowLayoutPanel1.Controls.Add(RPGPictureBox.EmptyBox);

			//If String.IsNullOrEmpty(Properties.Settings.DockString) Then Dockable.ForceDock(Canvas)
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
			// Clear current box
			FlowLayoutPanel1.Controls.Clear();

			GameNodeNode Node = (GameNodeNode)e.Node;

			FlowLayoutPanel1.Controls.Add (RPGPictureBox.EmptyBox);

			if (Node.RPGNode.Images.Count != 0)
			{
				foreach (var Image in Node.RPGNode.Images)
					// Add a box
					FlowLayoutPanel1.Controls.Add(Image.Box);
			}

			TreeView1.SelectedNode = Node;
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

		void ClickProcess()
		{
			if (!Program.imagePackerForm.Visible)
			{
				if (MainImage != null)
				{
					if ((Images.CurrentGameFile != MainImage.GameFile) && Canvas.Settings.DontAskGameChange == false)
					{
						using (DontAskDialog dad = new DontAskDialog())
						{
							DialogResult result = dad.ShowDialog("You are about to use an image associated with\nanother game. This may result in problems with\nexporting and animation.\n\nAre you sure you want to do this?", SystemIcons.Question.ToBitmap());
							Canvas.Settings.DontAskGameChange = dad.checkBox1.Checked;

							if (result == DialogResult.Cancel)
								return;
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
			ClickProcess();
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
					Size FrameSize = new Size(MainImage.RawBitmap.Width / MainImage.GameFile.SheetRows, MainImage.RawBitmap.Height / MainImage.GameFile.SheetColumns);
					Box.MyImage = new Bitmap(FrameSize.Width, FrameSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

					using (FastPixel MyImageFP = new FastPixel(Box.MyImage, true))
					{
						using (FastPixel RawImageFP = new FastPixel(Box.MainImage.RawBitmap, false))
						{
							Rectangle Rect = Character.GetRectangleForBitmapFrame(Box.MainImage.RawBitmap, MainImage.GameFile.PreviewFrame, MainImage.GameFile.SheetRows, MainImage.GameFile.SheetColumns);
							for (int i = 0; i <= FrameSize.Width - 1; i++)
							{
								for (int z = 0; z <= FrameSize.Height - 1; z++)
									MyImageFP.SetPixel(i, z, RawImageFP.GetPixel(Rect.X + i, Rect.Y + z));
							}
						}
					}

					Program.itemsWindowForm.ToolTip1.SetToolTip(Box, MainImage.Name + "\nDouble-click to set current layer image to this.");
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

}
