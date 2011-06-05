using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OpenCharas
{
	public enum ImageContainerViewMode
	{
		Icons,
		List,
	}

	public class ImageViewerContainer : UserControl
	{
		VScrollBar _scrollBar;

		ToolStrip toolStrip1;
		ToolStripButton toolStripButton1;
		ToolStripButton toolStripButton2;
		ToolStripSeparator toolStripSeparator1;
		ToolStripButton toolStripButton3;
		ToolStripButton toolStripButton4;

		int _zoom;
		public int Zoom { get { return _zoom; } set { _zoom = value; CheckScrollbar(); } }
		public int BaseSize { get; set; }

		ImageContainerViewMode _mode;
		public ImageContainerViewMode Mode { get { return _mode; } set { _mode = value; CheckScrollbar();  Invalidate(); } }

		int _selectedIndex = -1;
		public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; Invalidate(); } }

		int _hoverIndex = -1;
		public int HoverIndex { get { return _hoverIndex; } }

		List<RPGImage> _images;
		public List<RPGImage> Images
		{
			get { return _images; }

			set
			{
				_images = value;
				_selectedIndex = _hoverIndex = -1;
				CheckScrollbar();
				Invalidate();
			}
		}

		public event EventHandler SelectedBoxChanged;

		public RPGPictureBox SelectedBox
		{
			get
			{
				if (_selectedIndex == -1)
					return null;
				else if (_selectedIndex == 0)
					return RPGPictureBox.EmptyBox;

				return _images[_selectedIndex - 1].Box;
			}
		}

		ToolTip _tip;
		Timer _tipTimer;

		public ImageViewerContainer()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserMouse | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
			DoubleBuffered = true;

			_scrollBar = new VScrollBar();
			_scrollBar.Dock = DockStyle.Right;
			_scrollBar.Minimum = _scrollBar.Maximum = 0;
			_scrollBar.SmallChange = 1;
			_scrollBar.LargeChange = 1;
			_scrollBar.ValueChanged += new EventHandler(_scrollBar_ValueChanged);

			Controls.Add(_scrollBar);
			InitToolbar();

			BaseSize = 20;
			Zoom = 1;

			_tipTimer = new Timer();
			_tipTimer.Interval = 500;
			_tipTimer.Tick += new EventHandler(_tipTimer_Tick);

			_tip = new ToolTip();
		}

		void InitToolbar()
		{
			this.toolStrip1 = new ToolStrip();
			this.toolStripButton2 = new ToolStripButton();
			this.toolStripButton1 = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripButton3 = new ToolStripButton();
			this.toolStripButton4 = new ToolStripButton();

			// 
			// toolStrip1
			// 
			toolStrip1.Dock = DockStyle.Bottom;
			toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new ToolStripItem[] {
			toolStripButton2,
			toolStripButton1,
			toolStripSeparator1,
			toolStripButton3,
			toolStripButton4});
			toolStrip1.Location = new System.Drawing.Point(0, 239);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(225, 25);
			toolStrip1.TabIndex = 1;
			toolStrip1.Renderer = new AwesomeToolStripRenderer();
			toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton2
			// 
			toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton2.Image = global::OpenCharas.Properties.Resources.ZoomOutHS;
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Black;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(23, 22);
			toolStripButton2.Text = "Zoom Out";
			toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
			// 
			// toolStripButton1
			// 
			toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = global::OpenCharas.Properties.Resources.ZoomInHS;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Black;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(23, 22);
			toolStripButton1.Text = "Zoom In";
			toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton3
			// 
			toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton3.Image = global::OpenCharas.Properties.Resources.list;
			toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton3.Name = "toolStripButton3";
			toolStripButton3.Size = new System.Drawing.Size(23, 22);
			toolStripButton3.Text = "List";
			toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
			// 
			// toolStripButton4
			// 
			toolStripButton4.Checked = true;
			toolStripButton4.CheckState = CheckState.Checked;
			toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton4.Image = global::OpenCharas.Properties.Resources.medium_icons;
			toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton4.Name = "toolStripButton4";
			toolStripButton4.Size = new System.Drawing.Size(23, 22);
			toolStripButton4.Text = "Icons";
			toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);

			Controls.Add(toolStrip1);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Zoom++;
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (Zoom > 1)
				Zoom--;
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			toolStripButton3.Checked = true;
			toolStripButton4.Checked = false;

			Mode = ImageContainerViewMode.List;
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			toolStripButton3.Checked = false;
			toolStripButton4.Checked = true;

			Mode = ImageContainerViewMode.Icons;
		}

		void _tipTimer_Tick(object sender, EventArgs e)
		{
			if (!new Rectangle(PointToScreen(Location), Size).Contains(Cursor.Position))
				return;

			if (_hoverIndex >= 1)
			{
				_tip.ShowAlways = true;
				_tip.Show(_images[_hoverIndex - 1].Name, this, PointToClient(Cursor.Position) + new Point2(0, 12));
			}
			else
				_tip.Hide(this);

			_tipTimer.Stop();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			CalculateValues();
		}

		int _usableWidth, _iconsPerColumn, _usableHeight, _iconsPerRow;
		public void CalculateValues()
		{
			_usableWidth = Width - 2 - _scrollBar.Width;
			_iconsPerColumn = _usableWidth / (BoxSize + 2);

			_usableHeight = Height - 2 - toolStrip1.Height;
			_iconsPerRow = _usableHeight / (BoxSize + 2);
		}

		public int GetImageIndexAtLocation(Point p)
		{
			int c;

			switch (Mode)
			{
			case ImageContainerViewMode.Icons:
				{
					int col = (p.X - 2) / (BoxSize + 2);
					int row = (p.Y - 2) / (BoxSize + 2);
					int curImage = _iconsPerColumn * _scrollBar.Value;
					int rows = (_images != null && _images.Count != 0) ? (_images.Count / _iconsPerColumn) : 0;

					if (col >= _iconsPerColumn || row > rows)
						return -1;

					c = col + (_iconsPerColumn * (row)) + curImage;

					break;
				}

			case ImageContainerViewMode.List:
			default:
				c = ((p.Y - 2) / (BoxSize + 2)) + _scrollBar.Value;
				break;
			}

			if (c > _images.Count)
				return -1;

			return c;
		}

		public RPGPictureBox GetImageAtLocation(Point p)
		{
			var index = GetImageIndexAtLocation(p);

			if (index == 0)
				return RPGPictureBox.EmptyBox;
			else if (index == -1)
				return null;

			return _images[index - 1].Box;
		}

		public Rectangle GetRectangle(int index)
		{
			switch (Mode)
			{
			case ImageContainerViewMode.Icons:
				{
					int col = index % _iconsPerColumn;
					int row = index / _iconsPerRow;

					return new Rectangle(2 + (col * (BoxSize + 2)), 2 + (row * (BoxSize + 2)), BoxSize + 1, BoxSize + 1);
				}
			default:
				{
					int row = index;

					return new Rectangle(2, 2 + (row * (BoxSize + 2)), Width - _scrollBar.Width - 3, BoxSize + 1);
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			var ind = GetImageIndexAtLocation(e.Location);

			if (_hoverIndex != ind)
			{
				if (_hoverIndex != -1)
					Invalidate(GetRectangle(_hoverIndex));
				_hoverIndex = ind;
				Invalidate(GetRectangle(_hoverIndex));

				if (Mode == ImageContainerViewMode.Icons)
				{
					_tipTimer.Stop();

					if (_tip != null && _tip.Active)
						_tip.Hide(this);

					if (_hoverIndex >= 1)
						_tipTimer.Start();
				}
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			var ind = GetImageIndexAtLocation(e.Location);

			if (SelectedIndex != ind)
			{
				if (SelectedIndex != -1)
					Invalidate(GetRectangle(SelectedIndex));
				SelectedIndex = ind;
				Invalidate(GetRectangle(SelectedIndex));
			}

			if (SelectedBoxChanged != null)
				SelectedBoxChanged(this, EventArgs.Empty);

			base.OnMouseDown(e);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (e.Delta < 0)
			{
				if (_scrollBar.Value < _scrollBar.Maximum)
					_scrollBar.Value++;
			}
			else if (e.Delta > 0)
			{
				if (_scrollBar.Value > 0)
					_scrollBar.Value--;
			}
		}

		public int BoxSize
		{
			get { return BaseSize + (4 * Zoom); }
		}

		void CheckScrollbar()
		{
			// calculate the number of visible rows
			if (_images != null && _images.Count != 0)
			{
				switch (Mode)
				{
				case ImageContainerViewMode.Icons:
					{
						var totalHeight = (int)Math.Ceiling((float)_images.Count / _iconsPerColumn);
						var heightUsage = totalHeight - _iconsPerRow;

						if (totalHeight > _iconsPerRow)
						{
							_scrollBar.Maximum = heightUsage;
							_scrollBar.Value = 0;
						}
						else
							_scrollBar.Maximum = _scrollBar.Value = 0;
					}
					break;
				case ImageContainerViewMode.List:
					{
						if (_images.Count() < _iconsPerRow)
							_scrollBar.Maximum = _scrollBar.Value = 0;
						else
						{
							_scrollBar.Value = 0;
							_scrollBar.Maximum = _images.Count() + 1 - _iconsPerRow;
						}
					}
					break;
				}
			}

			Invalidate();
		}

		void _scrollBar_ValueChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			CheckScrollbar();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (DesignMode || _images == null)
				return;

			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			if (Mode == ImageContainerViewMode.Icons)
			{
				int rows = (_images != null && _images.Count != 0) ? (_images.Count / _iconsPerColumn) : 0;

				int curImage = (_iconsPerColumn) * _scrollBar.Value;
				int ipR = _iconsPerRow;

				for (int y = 0; y < rows + 1; ++y)
				{
					if (ipR == 0)
						break;
					ipR--;

					for (int x = 0; x < _iconsPerColumn; ++x)
					{
						if (_images.Count < curImage)
							return;

						int v = 2 + (x * (BoxSize + 2));
						int h = 2 + (y * (BoxSize + 2));

						e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

						var rect = new RectangleF(v, h, BoxSize, BoxSize);
						var radius = new Size(4, 4);

						using (var path = AeroRenderer.GetRoundedRectangle(rect, radius))
						{
							if (_selectedIndex == curImage)
							{
								e.Graphics.FillPath(new SolidBrush(Color.FromArgb(20, 0, 0, 0)), path);

								AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF(v + 1, h + 1, BoxSize - 2, BoxSize - 2), radius, 87);
							}

							e.Graphics.DrawPath((_hoverIndex == curImage ? SystemPens.Highlight : SystemPens.InactiveCaption), path);
						}

						int imW, imH;
						var inSize = BoxSize - 1;
						var image = (curImage != 0) ? _images[curImage - 1].Box.PreviewImage : Properties.Resources.delete;

						if (image.Width <= inSize && image.Height <= inSize)
						{
							imW = image.Width;
							imH = image.Height;
						}
						else
						{
							float scW = (float)image.Width / inSize;
							float scH = (float)image.Height / inSize;

							float sc = (scW > scH) ? scW : scH;

							imW = (int)(image.Width / sc);
							imH = (int)(image.Height / sc);
						}

						e.Graphics.DrawImage(image, new RectangleF(v + ((inSize - imW) / 2) + 1, h + ((inSize - imH) / 2) + 1, imW, imH));

						curImage++;
					}
				}
			}
			else
			{
				int curImage = _scrollBar.Value;

				for (int i = 0; i < _iconsPerRow; ++i)
				{
					if (_images.Count < curImage)
						return;

					int v = 2;
					int h = 2 + (i * (BoxSize + 2));

					e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

					var rect = new RectangleF(v, h, Width - _scrollBar.Width - 4, BoxSize);
					var radius = new Size(4, 4);

					using (var path = AeroRenderer.GetRoundedRectangle(rect, radius))
					{
						if (_selectedIndex == curImage)
						{
							e.Graphics.FillPath(new SolidBrush(Color.FromArgb(20, 0, 0, 0)), path);

							AeroRenderer.DrawShadowPiece(e.Graphics, new RectangleF(v + 1, h + 1, BoxSize - 2, BoxSize - 2), radius, 87);
						}

						e.Graphics.DrawPath((_hoverIndex == curImage ? SystemPens.Highlight : SystemPens.InactiveCaption), path);
					}

					int imW, imH;
					var inSize = BoxSize - 1;
					var box = (curImage != 0) ? _images[curImage - 1].Box : null;
					var image = (box == null) ? Properties.Resources.delete : box.PreviewImage;

					if (image.Width <= inSize && image.Height <= inSize)
					{
						imW = image.Width;
						imH = image.Height;
					}
					else
					{
						float scW = (float)image.Width / inSize;
						float scH = (float)image.Height / inSize;

						float sc = (scW > scH) ? scW : scH;

						imW = (int)(image.Width / sc);
						imH = (int)(image.Height / sc);
					}

					e.Graphics.DrawImage(image, new RectangleF(v + ((inSize - imW) / 2) + 1, h + ((inSize - imH) / 2) + 1, imW, imH));

					if (box != null)
					TextRenderer.DrawText(e.Graphics, box.MainImage.Name, Font, new Rectangle(2 + BoxSize + 4, h, Width - _scrollBar.Width - 2 - (2 + BoxSize + 4), BoxSize), Color.Black, TextFormatFlags.VerticalCenter);

					curImage++;
				}
			}
		}
	}
}
