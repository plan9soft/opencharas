using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public enum EFlipFlag
	{
		FlipNone,
		FlipHorizontal,
		FlipVertical
	}

	public class RPGLayer
	{
		private Point _offset = new Point();
		public Point Offset
		{
			get { return _offset; }
			set { _offset = value; }
		}

		private RPGImage _image;
		public RPGImage Image
		{
			get { return _image; }
			set { _image = value; }
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private QColorMatrix _matrix = new QColorMatrix();
		public QColorMatrix Matrix
		{
			get { return _matrix; }
			set { _matrix = value; }
		}

		private float[] _matrixValues = new float[4];
		public float[] MatrixValues
		{
			get { return _matrixValues; }
		}

		private int _alpha;
		public int Alpha
		{
			get { return _alpha; }
			set { _alpha = value; }
		}

		private List<RPGLayer> _subLayers = new List<RPGLayer>();
		public List<RPGLayer> SubLayers
		{
			get { return _subLayers; }
		}

		private RPGLayer _parent;
		public RPGLayer Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		private RPGCharacter _ownedCharacter;
		public RPGCharacter OwnedCharacter
		{
			get { return _ownedCharacter; }
			set { _ownedCharacter = value; }
		}

		private EFlipFlag _flipFlags;
		public EFlipFlag FlipFlags
		{
			get { return _flipFlags; }
			set { _flipFlags = value; }
		}

		private bool _inverted;
		public bool Inverted
		{
			get { return _inverted; }
			set { _inverted = value; }
		}

		private Bitmap _flippedImage;
		public Bitmap FlippedImage
		{
			get { return _flippedImage; }
			set { _flippedImage = value; }
		}

		private RPGNode _node;
		public RPGNode Node
		{
			get { return _node; }
			set { _node = value; }
		}

		public RPGLayer()
		{
			Offset = Point.Empty;
			Image = null;
			Name = "";
			Parent = null;
			Matrix.Reset();
			Alpha = 255;
			FlipFlags = EFlipFlag.FlipNone;
			Inverted = false;
			FlippedImage = null;

			_matrixValues[0] = 0;
			_matrixValues[1] = 1;
			_matrixValues[2] = 1;
			_matrixValues[3] = 0;
		}

		public void SetImage(RPGPictureBox Img)
		{
			if (Img == null)
			{
				Image = null;
				Program.layersWindowForm.PictureBox1.Image = null;
				FlippedImage = null;
				return;
			}

			Image = Img.MainImage;
			Program.layersWindowForm.PictureBox1.Image = Img.MyImage;
			UpdateFlippedImage();
		}

		public void UpdateFlippedImage()
		{
			if (Image == null)
				return;

			FlippedImage = Image.RawBitmap.Clone(new Rectangle(0, 0, Image.RawBitmap.Width, Image.RawBitmap.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			if ((FlipFlags & (EFlipFlag.FlipHorizontal | EFlipFlag.FlipVertical)) != 0)
				FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);
			else if ((FlipFlags & EFlipFlag.FlipHorizontal) != 0)
				FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
			else if ((FlipFlags & EFlipFlag.FlipVertical) != 0)
				FlippedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
		}

		public void Save(System.IO.BinaryWriter File, bool ImagesToo)
		{
			File.Write(Offset.X);
			File.Write(Offset.Y);
			File.Write(Name);
			File.Write(Image != null);

			if (Image != null)
			{
				File.Write(Image.Path);

				if (ImagesToo)
				{
					File.Write(Image.OriginalBitmap.Width);
					File.Write(Image.OriginalBitmap.Height);

					Canvas.WriteBitmapToFile(File, Image.OriginalBitmap);
				}
			}

			for (int x = 0; x <= 4; x++)
			{
				for (int y = 0; y <= 4; y++)
					File.Write(Matrix.MyMatrix[x, y]);
			}

			for (int i = 0; i <= 3; i++)
				File.Write(_matrixValues[i]);

			File.Write(Alpha);
			File.Write((int)FlipFlags);
			File.Write(Inverted);

			// Parent is not saved/loaded, it is re-created on run-time
			File.Write(SubLayers.Count);

			foreach (var SubLayer in SubLayers)
				SubLayer.Save(File, ImagesToo);
		}

		public void Load(System.IO.BinaryReader File, bool ImagesToo)
		{
			Offset = new Point(File.ReadInt32(), File.ReadInt32());
			Name = File.ReadString();
			if (File.ReadBoolean() == true)
			{
				string Img = File.ReadString();
				Image = Images.FindImage(Img);

				if (Image != null)
					FlippedImage = Image.RawBitmap;

				if (ImagesToo)
				{
					int Width = File.ReadInt32();
					int Height = File.ReadInt32();

					using (Bitmap BMP = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
					{
						Canvas.ReadBitmapFromFile(File, Width, Height, BMP);
						// Do we have this image
						if (System.IO.File.Exists(Img))
						{
							if (Images.BitmapCompare(BMP, Images.FindImage(Img).OriginalBitmap) == false)
							{
								// Message the user
								if (MessageBox.Show("The image " + Img + " already exists in your image folder, but yours differs from the one in the file. Would you like to overwrite with this one?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
								{
									BMP.Save(Img);
									Program.canvasForm.ImagesAcquired.Add(Img);
								}
							}
						}
						else if (System.IO.File.Exists(Img) == false)
						{
							System.IO.Directory.CreateDirectory(Img.Substring(0, Img.LastIndexOf("\\")));

							// Save the bitmap
							BMP.Save(Img);
							Program.canvasForm.ImagesAcquired.Add(Img);

							// Set our image to a temporary image for later re-loading
							Image = new RPGImage();
							Image.Path = Img;
						}
					}

				}
				else
				{
					if (Image == null)
						Program.canvasForm.PackImagesNotFound.Add(Img);
				}
			}

			for (int x = 0; x <= 4; x++)
			{
				for (int y = 0; y <= 4; y++)
					Matrix.MyMatrix[x, y] = File.ReadSingle();
			}

			for (int i = 0; i <= 3; i++)
				_matrixValues[i] = File.ReadSingle();

			Alpha = File.ReadInt32();
			FlipFlags = (EFlipFlag)File.ReadInt32();
			Inverted = File.ReadBoolean();

			int SubLayerNum = File.ReadInt32();
			for (int i = 0; i <= SubLayerNum - 1; i++)
			{
				RPGLayer SubLayer = new RPGLayer();
				SubLayer.Load(File, ImagesToo);
				SubLayer.Parent = this;
				SubLayers.Add(SubLayer);
			}
		}

		public System.Drawing.Imaging.ImageAttributes GenerateColorMatrix()
		{
			System.Drawing.Imaging.ImageAttributes Attr = new System.Drawing.Imaging.ImageAttributes();
			Attr.SetColorMatrix(Matrix.MyMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
			return Attr;
		}

		public void ReloadImagesSubLayers(bool Restore)
		{
			if (Restore == false)
			{
				foreach (var SubLayer in SubLayers)
				{
					if (SubLayer.Image != null)
					{
						RPGImage Img = SubLayer.Image;

						SubLayer.Image = new RPGImage();
						SubLayer.Image.Path = Img.Path;
					}
					SubLayer.ReloadImagesSubLayers(Restore);
				}
			}
			else
			{
				foreach (var SubLayer in SubLayers)
				{
					if (SubLayer.Image != null)
					{
						SubLayer.Image = Images.FindImage(SubLayer.Image.Path);
						SubLayer.UpdateFlippedImage();
					}
					SubLayer.ReloadImagesSubLayers(Restore);
				}
			}
		}

		public void SetColorMatrix()
		{
			Matrix.Reset();
			Matrix.ScaleColors(float.Parse(Program.layersWindowForm.TextBox4.Text));
			Matrix.SetSaturation(float.Parse(Program.layersWindowForm.TextBox3.Text));
			Matrix.RotateHue(int.Parse(Program.layersWindowForm.TextBox2.Text));
			Matrix.ScaleOpacity((float)(Alpha / 255.0f));

			if (Inverted)
				Matrix.Invert();

			Matrix.TranslateColors(float.Parse(Program.layersWindowForm.TextBox6.Text));
			Program.canvasForm.UpdateDrawing();
		}

		// CANVAS RENDERING
		public void RenderCanvasLayer(Graphics gfx)
		{
			float Zoom = (float)(Program.canvasForm.ZoomNumber) / 100.0f;
			int ContentWidth = Program.canvasForm.ToolStripContainer1.ContentPanel.Width / 2;
			int ContentHeight = Program.canvasForm.ToolStripContainer1.ContentPanel.Height / 2;
			Bitmap bmp = FlippedImage;
			Rectangle destRect;
			Rectangle srcRect;
			System.Drawing.Imaging.ImageAttributes Attr = GenerateColorMatrix();

			// Frame mode
			if (Program.canvasForm.FrameAnimationToolStripMenuItem.Checked)
			{
				int BmpWidth = (int)(((bmp.Width / Image.GameFile.SheetRows) * Zoom) / 2);
				int BmpHeight = (int)(((bmp.Height / Image.GameFile.SheetColumns) * Zoom) / 2);
				int DrawX = (int)((ContentWidth - BmpWidth) + (Offset.X * Zoom));
				int DrawY = (int)((ContentHeight - BmpHeight) + (Offset.Y * Zoom));
				int Frame;

				if (Program.canvasForm.AnimationIndex == 0)
					Frame = 0;
				else
					Frame = (int)(Images.CurrentGameFile.Animations[Program.canvasForm.AnimationIndex - 1].GetFrame(Program.canvasForm.AnimationFrameIndex));

				destRect = new Rectangle(DrawX, DrawY, (int)((bmp.Width / Image.GameFile.SheetRows) * Zoom), (int)((bmp.Height / Image.GameFile.SheetColumns) * Zoom));
				srcRect = Character.GetRectangleForBitmapFrame(bmp, Frame, Image.GameFile.SheetRows, Image.GameFile.SheetColumns);
				gfx.DrawImage(bmp, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, Attr);
			}
			else if (Program.canvasForm.SingleSetToolStripMenuItem.Checked) // Sheet mode
			{
				int BmpWidth = (int)((bmp.Width * Zoom) / 2);
				int BmpHeight = (int)((bmp.Height * Zoom) / 2);
				int DrawX = (int)((ContentWidth - BmpWidth) + (Offset.X * Zoom) + Program.canvasForm.CanvasCamera.X);
				int DrawY = (int)((ContentHeight - BmpHeight) + (Offset.Y * Zoom) + Program.canvasForm.CanvasCamera.Y);

				destRect = new Rectangle(new Point(DrawX, DrawY), new Size((int)(bmp.Width * Zoom), (int)(bmp.Height * Zoom)));
				srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);

				gfx.DrawImage(bmp, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, Attr);
			}
			else
			{
				int BmpWidth = (int)(bmp.Width / 2);
				int BmpHeight = (int)(bmp.Height / 2);
				ContentWidth = (int)(gfx.VisibleClipBounds.Width / 2);
				ContentHeight = (int)(gfx.VisibleClipBounds.Height / 2);
				int DrawX = (int)((ContentWidth - BmpWidth) + (Offset.X));
				int DrawY = (int)((ContentHeight - BmpHeight) + (Offset.Y));

				destRect = new Rectangle(new Point(DrawX, DrawY), new Size(bmp.Width, bmp.Height));
				srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);

				gfx.DrawImage(bmp, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, Attr);
			}
		}

		public void DrawCanvasLayers(Graphics e)
		{
			if (Image != null)
				RenderCanvasLayer(e);

			foreach (var SubLayer in SubLayers)
				SubLayer.DrawCanvasLayers(e);
		}

		public void RenderLayerToGraphics(Graphics gfx)
		{
			var WImg = OwnedCharacter.SizeOfOutput;
			Size ContentSize = new Size(WImg.Width / 2, WImg.Height / 2);
			Size BmpSize = new Size(FlippedImage.Width / 2, FlippedImage.Height / 2);
			Point DrawLocation = new Point((ContentSize.Width - BmpSize.Width) + Offset.X, (ContentSize.Height - BmpSize.Height) + Offset.Y);

			gfx.DrawImage(FlippedImage, new Rectangle(DrawLocation, FlippedImage.Size), 0, 0, FlippedImage.Width, FlippedImage.Height, GraphicsUnit.Pixel, GenerateColorMatrix());
		}

		public void RenderLayerToGraphicsRecursion(Graphics e)
		{
			if (Image != null)
				RenderLayerToGraphics(e);

			foreach (var SubLayer in SubLayers)
				SubLayer.RenderLayerToGraphicsRecursion(e);
		}
	}

	public class RPGCharacter
	{
		private string _name; // Unique ID for character file (for multi-sheet)
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private List<RPGLayer> Layers_ = new List<RPGLayer>();
		public List<RPGLayer> Layers
		{
			get { return Layers_; }
		}

		public void Save(System.IO.BinaryWriter Writer, bool ImagesToo)
		{
			Writer.Write(Layers.Count);

			foreach (var Layer in Layers)
				Layer.Save(Writer, ImagesToo);
		}

		public void Load(System.IO.BinaryReader Reader, bool ImagesToo)
		{
			int Count = Reader.ReadInt32();

			for (int i = 0; i <= Count - 1; i++)
			{
				RPGLayer NewLayer = new RPGLayer();
				NewLayer.Load(Reader, ImagesToo);
				Layers.Add(NewLayer);
			}

			foreach (var Layer in Layers)
			{
				RPGNode Node = new RPGNode();
				Node.Layer = Layer;
				Node.Text = Layer.Name;
				Layer.Node = Node;

				Program.layersWindowForm.TreeView1.Nodes.Add(Node);

				Node.RecurseAndLoadSubNodes();
			}
		}

		private Size SizeRecursion(List<RPGLayer> LayerCollection, Size CurrentSize)
		{
			foreach (var Layer in LayerCollection)
			{
				CurrentSize = SizeRecursion(Layer.SubLayers, CurrentSize);

				if (Layer.FlippedImage == null)
					continue;

				if (Layer.FlippedImage.Size.Width > CurrentSize.Width && Layer.FlippedImage.Size.Height > CurrentSize.Height)
					CurrentSize = Layer.FlippedImage.Size;
			}

			return CurrentSize;
		}

		public Size SizeOfOutput
		{
			get
			{
				if (Layers.Count == 0)
					return Size.Empty;

				Size LargestSize = new Size(0, 0);
				return SizeRecursion(Layers, LargestSize);
			}
		}

		public RPGLayer CreateLayer(TreeNode Node)
		{
			RPGLayer Layer = new RPGLayer();

			if ((Node != null) && (Node.Parent != null))
			{
				// Use our parent layer
				Layer.Parent = ((RPGNode)Node.Parent).Layer;
				Layer.OwnedCharacter = Layer.Parent.OwnedCharacter;
				Layer.Parent.SubLayers.Add(Layer);
			}
			else
			{
				Layer.Parent = null;
				Layer.OwnedCharacter = this;
				Layers.Add(Layer);
			}

			Layer.Name = Node.Text;
			return Layer;
		}

		public void RemoveLayer(RPGLayer Layer)
		{
			// Do we have a parent
			if (Layer.Parent != null)
				Layer.Parent.SubLayers.Remove(Layer); // Remove us from sublayers
			else
				Layers.Remove(Layer);
		}

		public void MoveLayer(RPGLayer Layer, bool Down)
		{
			if (Layer.Parent != null)
			{
				int OldIndex = (int)(Layer.Parent.SubLayers.IndexOf(Layer));
				Layer.Parent.SubLayers.Remove(Layer);

				if (Down)
					Layer.Parent.SubLayers.Insert(OldIndex + 1, Layer);
				else
					Layer.Parent.SubLayers.Insert(OldIndex - 1, Layer);
			}
			else
			{
				int OldIndex = (int)(Program.characterSelectForm.CharacterList.CurrentCharacter.Character.Layers.IndexOf(Layer));
				Layers.Remove(Layer);

				if (Down)
					Layers.Insert(OldIndex + 1, Layer);
				else
					Layers.Insert(OldIndex - 1, Layer);
			}
			Program.canvasForm.UpdateDrawing();
		}
	}

	public class Character
	{
		public static Rectangle GetRectangleForBitmapFrame(Image bmp, int FrameNumber, int NumRows, int NumColumns)
		{
			if (bmp == null)
				return new Rectangle(0, 0, 0, 0);

			int SpriteWidth = bmp.Width / NumRows;
			int SpriteHeight = bmp.Height / NumColumns;
			int FramesPerRow = bmp.Width / SpriteWidth;

			return new Rectangle(((FrameNumber % FramesPerRow) * SpriteWidth), ((FrameNumber / FramesPerRow) * SpriteHeight), SpriteWidth, SpriteHeight);
		}
	}
}
