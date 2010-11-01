using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using Paril;

namespace OpenCharas
{
	public partial class Canvas
	{
		public EasyPosition easyPositionForm;

		public Canvas()
		{
			SpecialColor = Color.FromArgb(32, 156, 0);

			InitializeComponent();

			WindowGeometry.FromString(Canvas.Settings.WindowGeometry_Canvas, this);
		}

		private bool SkipSizeChanged_ = true;
		public bool SkipSizeChanged
		{
			get { return SkipSizeChanged_; }
			set { SkipSizeChanged_ = value; }
		}

		public struct LastSaveInfo
		{
			public string Path;
			public EHeaderValue Header;
		}

		private LastSaveInfo LastSavePath_ = new LastSaveInfo();
		public LastSaveInfo LastSavePath
		{
			get { return LastSavePath_; }
			set { LastSavePath_ = value; }
		}

		public void SetCurrentSavePosition(string Path, EHeaderValue Header)
		{
			LastSavePath = new LastSaveInfo { Path = Path, Header = Header };
		}

		public static void ReportCrash(Exception ex)
		{
			// Write the report
			System.IO.Directory.CreateDirectory("dumps\\");
			string FilePath = "dumps\\" + DateTime.Now.ToString().Replace('/', '_').Replace(' ', '_').Replace(':', '_') + "_report.txt";

			using (var outFile = System.IO.File.Open(FilePath, System.IO.FileMode.Create))
			{
				using (System.IO.StreamWriter strm = new System.IO.StreamWriter(outFile))
				{
					string output = (string)("==Exception Message==\n" + ex.Message + "\n\n==Inner Exception==\n");

					if (ex.InnerException != null)
						output += ex.InnerException.Message;
					else
						output += "Nothing";

					output += "\n\n==Source==\n" + ex.Source + "\n\n==Stack Trace==\n" + ex.StackTrace;
					output += "\n\n\n==String==\n" + ex.ToString();
					strm.Write(output);
				}
			}

			using (CrashReport crd = new CrashReport())
			{
				// Open dialog
				crd.Label1.Text += "\n\nException Message: " + ex.Message;
				crd.Folder_Path = FilePath;
				crd.ShowDialog();
			}
		}

		// Loading code
		public static class Settings
		{
			static SettingsContainer _settings;

			static bool _firstRun = true;
			public static bool FirstRun
			{
				get { return _firstRun; }
				set { _firstRun = value; }
			}

			static bool _dontAskGameChange = false;
			public static bool DontAskGameChange
			{
				get { return _dontAskGameChange; }
				set { _dontAskGameChange = value; }
			}

			static string _currentGame = "";
			public static string CurrentGame
			{
				get { return _currentGame; }
				set { _currentGame = value; }
			}

			static string _windowGeometryItems = "";
			public static string WindowGeometry_Items
			{
				get { return _windowGeometryItems; }
				set { _windowGeometryItems = value; }
			}

			static string _windowGeometryCanvas = "";
			public static string WindowGeometry_Canvas
			{
				get { return _windowGeometryCanvas; }
				set { _windowGeometryCanvas = value; }
			}

			static string _windowGeometryLayers = "";
			public static string WindowGeometry_Layers
			{
				get { return _windowGeometryLayers; }
				set { _windowGeometryLayers = value; }
			}

			static string _windowGeometryCharSelect = "";
			public static string WindowGeometry_CharSelect
			{
				get { return _windowGeometryCharSelect; }
				set { _windowGeometryCharSelect = value; }
			}

			public static void Init()
			{
				_settings = new SettingsContainer();
				_settings.AddType(typeof(Settings));

				Load();
			}

			static string settingsFile = "settings\\" + Environment.UserName + "\\settings.txt";

			public static void Load ()
			{
				_settings.Load(settingsFile);
			}

			public static void Save()
			{
				_settings.Save(settingsFile);
			}

			public static void Reset()
			{
				_settings.Reset();
			}
		}

		public class DoubleBufferImageControl : Control
		{
			public DoubleBufferImageControl()
			{
				SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
				DoubleBuffered = true;
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				e.Graphics.DrawImage(Program.canvasForm.StoredImage, 0, 0);
			}

			protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
			{
				Program.canvasForm.MovingCamera = true;
				Program.canvasForm.OldMousePos = e.Location;
			}

			protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
			{
				Program.canvasForm.MovingCamera = false;

				if (Program.canvasForm.OldMousePos.X == e.Location.X && Program.canvasForm.OldMousePos.Y == e.Location.Y)
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Right)
					{
						Program.canvasForm.easyPositionForm = new EasyPosition();
						Program.canvasForm.easyPositionForm.Show();
						Program.canvasForm.easyPositionForm.Location = new Point(MousePosition.X, MousePosition.Y);
						Program.canvasForm.easyPositionForm.TopMost = true;
						Program.canvasForm.easyPositionForm.FormClosed += new FormClosedEventHandler(easyPositionForm_FormClosed);
					}
				}
			}

			protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
			{
				if (Program.canvasForm.MovingCamera)
				{
					Program.canvasForm.MouseDelta = new Point(e.X - Program.canvasForm.OldMousePos.X, e.Y - Program.canvasForm.OldMousePos.Y);
					Program.canvasForm.OldMousePos = new Point(e.X, e.Y);
					Program.canvasForm.CanvasCamera = new Point(Program.canvasForm.CanvasCamera.X + Program.canvasForm.MouseDelta.X, Program.canvasForm.CanvasCamera.Y + Program.canvasForm.MouseDelta.Y);
					Program.canvasForm.UpdateDrawing();
				}
			}
		}

		DoubleBufferImageControl imgControl;

		public void Canvas_Load(System.Object sender, System.EventArgs e)
		{
			System.IO.Directory.CreateDirectory("data");
			Paril.Windows.Registry.FileExtension ext = new Paril.Windows.Registry.FileExtension(".txt");

			imgControl = new DoubleBufferImageControl();
			imgControl.Dock = DockStyle.Fill;
			ToolStripContainer1.ContentPanel.Controls.Add(imgControl);

			Settings.Init();

			Program.characterSelectForm.ClearCharacters();
			SkipSizeChanged = false;

			Images.LoadImages();
			AnimTimer = new Timer();
			AnimTimer.Interval = 350;
			AnimTimer.Tick += new System.EventHandler(AnimTimerUp);
			ToolStripTextBox2.Text = AnimTimer.Interval.ToString();

			// Do we have any games
			while (Images.RPGGames.Count == 0)
			{
				MessageBox.Show(Application.ProductName + " notices that you don\'t have any games set up.\n\nTo use this application, you must create at least one game.", "", MessageBoxButtons.OK);

				using (GameEditor ged = new GameEditor())
					ged.ShowDialog();

				// Re-load images and games
				Images.LoadImages();
			}

			UpdateDrawing();

			SkipSizeChanged = true;
			Program.characterSelectForm.Show(this);
			Program.layersWindowForm.Show(this);
			Program.itemsWindowForm.Show(this);
			SkipSizeChanged = false;

			if (string.IsNullOrEmpty(Canvas.Settings.WindowGeometry_Canvas))
				this.Location = new Point((int)((Screen.PrimaryScreen.WorkingArea.X + (Screen.PrimaryScreen.WorkingArea.Width / 2)) - (this.Size.Width / 2) - (Program.layersWindowForm.Size.Width / 2)), (int)((Screen.PrimaryScreen.WorkingArea.Y + (Screen.PrimaryScreen.WorkingArea.Height / 2)) - (this.Size.Height / 2) + (Program.layersWindowForm.Size.Height / 3.5)));
			else
				WindowGeometry.FromString(Canvas.Settings.WindowGeometry_Canvas, this);

			if (string.IsNullOrEmpty(Canvas.Settings.WindowGeometry_Items))
				Program.itemsWindowForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y + this.Size.Height - Program.itemsWindowForm.Size.Height);
			else
				WindowGeometry.FromString(Canvas.Settings.WindowGeometry_Items, Program.itemsWindowForm);

			if (string.IsNullOrEmpty(Canvas.Settings.WindowGeometry_Layers))
				Program.layersWindowForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y - Program.layersWindowForm.Size.Height + (this.Size.Height - Program.layersWindowForm.Size.Height) + (Program.layersWindowForm.Size.Height - Program.itemsWindowForm.Size.Height));
			else
				WindowGeometry.FromString(Canvas.Settings.WindowGeometry_Layers, Program.layersWindowForm);

			if (string.IsNullOrEmpty(Canvas.Settings.WindowGeometry_CharSelect))
				Program.characterSelectForm.Location = new Point(this.Location.X - Program.characterSelectForm.Size.Width, this.Location.Y + this.Size.Height - Program.characterSelectForm.Size.Height);
			else
				WindowGeometry.FromString(Canvas.Settings.WindowGeometry_CharSelect, Program.characterSelectForm);

			if (Canvas.Settings.FirstRun)
				Canvas.Settings.FirstRun = false;

			UpdateAnimNum(1);

			if (Environment.GetCommandLineArgs().Length != 1)
			{
				string file = Environment.GetCommandLineArgs()[1];
				PerformOpen(file);
			}
		}

		public bool AskToClose()
		{
			var Result = MessageBox.Show("You may have unsaved work. Would you like to save your changes before exiting?", "", MessageBoxButtons.YesNoCancel);

			if (Result == DialogResult.Yes)
				DoSaveAs();
			else if (Result == DialogResult.Cancel)
				return true;

			return false;
		}

		// Closing Code

		public void Canvas_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			Canvas.Settings.WindowGeometry_Canvas = WindowGeometry.ToString(this);
			Canvas.Settings.WindowGeometry_Items = WindowGeometry.ToString(Program.itemsWindowForm);
			Canvas.Settings.WindowGeometry_Layers = WindowGeometry.ToString(Program.layersWindowForm);
			Canvas.Settings.WindowGeometry_CharSelect = WindowGeometry.ToString(Program.characterSelectForm);
		}

		public void Canvas_FormClosed(System.Object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			ToolStripManager.SaveSettings(this);
			ToolStripManager.SaveSettings(Program.layersWindowForm);
			ToolStripManager.SaveSettings(Program.characterSelectForm);

			Canvas.Settings.Save();
		}

		public void Canvas_Move(System.Object sender, System.EventArgs e)
		{
			if (this.WindowState != FormWindowState.Normal)
				return;
		}

		bool WasMinimized = false;
		public void Canvas_SizeChanged(System.Object sender, System.EventArgs e)
		{
			if (WasMinimized)
			{
				this.Focus();
				WasMinimized = false;
			}
		}

		public void Canvas_Resize(System.Object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized || SkipSizeChanged)
				return;

			UpdateDrawing();
		}

		// Open Layers
		public void LayersToolStripMenuItem2_Click(System.Object sender, System.EventArgs e)
		{
			if (Program.layersWindowForm.Visible)
				return;

			Program.layersWindowForm.Show(this);
		}

		// Open Items
		public void ItemsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			if (Program.itemsWindowForm.Visible)
				return;

			Program.itemsWindowForm.Show(this);
		}

		public void CharactersToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			if (Program.characterSelectForm.Visible)
				return;

			Program.characterSelectForm.Show(this);
		}

		// Drawing Code
		Bitmap StoredImage;

		public void UpdateDrawing()
		{
			if (ToolStripContainer1.ContentPanel.Width != 0 && ToolStripContainer1.ContentPanel.Height != 0)
			{
				StoredImage = new Bitmap(ToolStripContainer1.ContentPanel.Width, ToolStripContainer1.ContentPanel.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				using (Graphics TempGfx = Graphics.FromImage(StoredImage))
				{
					int ContentWidth = ToolStripContainer1.ContentPanel.Width;
					int ContentHeight = ToolStripContainer1.ContentPanel.Height;
					TempGfx.DrawLine(Pens.Black, new PointF(ContentWidth / 2, 0), new PointF(ContentWidth / 2, ContentHeight));
					TempGfx.DrawLine(Pens.Black, new PointF(0, ContentHeight / 2), new PointF(ContentWidth, ContentHeight / 2));

					System.Drawing.Drawing2D.GraphicsContainer Cont = TempGfx.BeginContainer();

					TempGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

					// Go through each layer drawing the sheet
					if (SheetModeToolStripMenuItem.Checked)
					{
						var bmp = RenderSheetToTexture(true);

						if (bmp != null)
						{
							float Zoom = (float)(ZoomNumber) / 100;
							TempGfx.DrawImage(bmp, new Rectangle(
								(int)((ToolStripContainer1.ContentPanel.Width / 2 - ((bmp.Width * Zoom) / 2)) + CameraPosition.X),
								(int)((ToolStripContainer1.ContentPanel.Height / 2 - ((bmp.Height * Zoom) / 2)) + CameraPosition.Y),
								(int)(bmp.Width * Zoom), (int)(bmp.Height * Zoom)));
						}
					}
					else
					{
						if (Program.characterSelectForm.GetSelectedCharacter().Character != null)
						{
							foreach (var Layer in Program.characterSelectForm.GetSelectedCharacter().Character.Layers)
								Layer.DrawCanvasLayers(TempGfx);
						}
					}

					TempGfx.EndContainer(Cont);
				}
			}

			imgControl.Invalidate();
		}

		// Switch to Frame Mode
		public void SwitchToFrameMode()
		{
			FrameAnimationToolStripMenuItem.Checked = true;
			SingleSetToolStripMenuItem.Checked = false;
			SheetModeToolStripMenuItem.Checked = false;
			UpdateDrawing();
		}

		public void FrameAnimationToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SwitchToFrameMode();
		}

		private void ToolStripButton12_Click(System.Object sender, System.EventArgs e)
		{
			SwitchToFrameMode();
		}

		// Switch to Set Mode
		public void SwitchToSetMode()
		{
			FrameAnimationToolStripMenuItem.Checked = false;
			SingleSetToolStripMenuItem.Checked = true;
			SheetModeToolStripMenuItem.Checked = false;
			UpdateDrawing();
		}

		public void EntireSheetToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SwitchToSetMode();
		}

		private void ToolStripButton11_Click(System.Object sender, System.EventArgs e)
		{
			SwitchToSetMode();
		}

		// Switch to Sheet Mode
		public void SwitchToSheetMode()
		{
			FrameAnimationToolStripMenuItem.Checked = false;
			SingleSetToolStripMenuItem.Checked = false;
			SheetModeToolStripMenuItem.Checked = true;
			UpdateDrawing();
		}

		public void SheetModeToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			SwitchToSheetMode();
		}

		// Zooming code
		private int ZoomNumber_ = 100;
		public int ZoomNumber
		{
			get { return ZoomNumber_; }
			set { ZoomNumber_ = value; }
		}

		private void SetZoom(int Num)
		{
			if (Num < 0)
				Num = 0;

			if (Num > 1000)
				Num = 1000;

			ToolStripTextBox1.Text = (string)(Num.ToString() + "%");
			ZoomNumber = Num;
		}

		public void ZoomByNumber(int Addition)
		{
			SetZoom(ZoomNumber + Addition);
			UpdateDrawing();
		}

		const int ZoomInAmount = 25;

		public void ToolStripButton6_Click(System.Object sender, System.EventArgs e)
		{
			ZoomByNumber((int)(ZoomInAmount));
		}

		public void ToolStripButton7_Click(System.Object sender, System.EventArgs e)
		{
			ZoomByNumber((int)(-ZoomInAmount));
		}

		public void Canvas_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				ZoomByNumber((int)(ZoomInAmount));
			}
			else if (e.Delta < 0)
			{
				ZoomByNumber((int)(-ZoomInAmount));
			}
		}

		public void ToolStripTextBox1_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)(Keys.Return) || e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				bool EndResult;
				int EndValue;
				EndResult = int.TryParse(ToolStripTextBox1.Text, out EndValue);

				if (EndResult == false)
				{
					return;
				}

				SetZoom(EndValue);
			}
		}

		// Animation player
		Timer AnimTimer;
		private int AnimationFrame = 0;
		public int AnimationFrameIndex
		{
			get { return AnimationFrame; }
			set { AnimationFrame = value; }
		}

		private void AnimTimerUp(object sender, EventArgs e)
		{
			if (AnimationIndexVal == 0)
				return;

			AnimationFrame++;
			if (AnimationFrame > Images.CurrentGameFile.Animations[AnimationIndexVal - 1].Frames.Count - 1)
				AnimationFrame = 0;

			UpdateDrawing();
		}

		private bool PlayingAnimation = false;
		public void ToolStripButton8_Click(System.Object sender, System.EventArgs e)
		{
			PlayingAnimation = !PlayingAnimation;

			if (PlayingAnimation)
			{
				ToolStripButton8.Image = Properties.Resources.stop_anim;
				AnimTimer.Start();
			}
			else
			{
				ToolStripButton8.Image = Properties.Resources.playanim;
				AnimTimer.Stop();
			}

			UpdateDrawing();
		}

		public void ToolStripButton14_Click(System.Object sender, System.EventArgs e)
		{
			AnimationFrame++;

			if (AnimationFrame > 3)
				AnimationFrame = 0;

			UpdateDrawing();
		}

		public void ToolStripButton15_Click(System.Object sender, System.EventArgs e)
		{
			AnimationFrame--;

			if (AnimationFrame < 0)
				AnimationFrame = 3;

			UpdateDrawing();
		}

		// Animation speed setting
		public void SetSpeed(int Speed)
		{
			if (Speed < 1)
				Speed = 1;

			AnimTimer.Interval = Speed;
			ToolStripTextBox2.Text = Speed.ToString();
		}

		public void ToolStripTextBox2_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)(Keys.Return) || e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				bool EndResult;
				int EndValue;
				EndResult = int.TryParse(ToolStripTextBox2.Text, out EndValue);

				if (EndResult == false)
					return;

				SetSpeed(EndValue);
			}

		}

		public void ToolStripButton16_Click(System.Object sender, System.EventArgs e)
		{
			SetSpeed(AnimTimer.Interval + 25);
		}

		public void ToolStripButton17_Click(System.Object sender, System.EventArgs e)
		{
			SetSpeed(AnimTimer.Interval - 25);
		}

		// Preview sheet
		public PreviewWindow previewForm;
		private void DoPreviewSheet()
		{
			if (Program.characterSelectForm.GetSelectedCharacter().Character.Layers.Count == 0)
			{
				MessageBox.Show("You need some layers first!");
				return;
			}

			if (previewForm != null && previewForm.Visible)
			{
				previewForm.Close();
				return;
			}

			previewForm = new PreviewWindow();
			previewForm.RenderedImage = Program.canvasForm.RenderSheetToTexture();
			previewForm.StartPosition = FormStartPosition.Manual;
			var ImgWidth = previewForm.RenderedImage.Size;
			previewForm.Size = new Size(ImgWidth.Width + GameEditor.Diff.Width, ImgWidth.Height + GameEditor.Diff.Height);
			previewForm.Location = this.Location + new Size(this.Size.Width - previewForm.Size.Width, -previewForm.Size.Height);
			previewForm.FormClosed += new FormClosedEventHandler(previewForm_FormClosed);
			previewForm.Show();
		}

		void previewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			previewForm = null;
		}

		public void ToolStripButton10_Click(System.Object sender, System.EventArgs e)
		{
			DoPreviewSheet();
		}

		public void PreviewSheetToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoPreviewSheet();
		}

		// Rendering: for getting the finalized image for saving
		public static Color SpecialColor;

		static System.Random RenderToTexture_RandomNumGen = new System.Random();

		public Bitmap RenderToTexture(RPGCharacter node, bool ResetTrans = false)
		{
			Size ImgSize = node.SizeOfOutput;

			if (ImgSize.Width == 0 || ImgSize.Height == 0)
				return null;

			var RenderedImage = new Bitmap(ImgSize.Width, ImgSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (Graphics TempGfx = Graphics.FromImage(RenderedImage))
			{
				TempGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

				// Go through each layer drawing the sheet
				foreach (var Layer in node.Layers)
					Layer.RenderLayerToGraphicsRecursion(TempGfx);
			}

			if (ResetTrans == false)
			{
				var ColorList = NumColorsInImage(RenderedImage);
				Color EndedUpColor = SpecialColor;

				while (ColorList.IndexOf(EndedUpColor) != -1)
					EndedUpColor = Color.FromArgb(RenderToTexture_RandomNumGen.Next(256), RenderToTexture_RandomNumGen.Next(256), RenderToTexture_RandomNumGen.Next(256));

				using (FastPixel fp = new FastPixel(RenderedImage, true))
				{
					for (int i = 0; i <= RenderedImage.Width - 1; i++)
					{
						for (int z = 0; z <= RenderedImage.Height - 1; z++)
						{
							if (fp.GetPixel(i, z).A == 0)
								fp.SetPixel(i, z, EndedUpColor);
						}
					}
				}
			}

			return RenderedImage;
		}

		public Bitmap RenderSheetToTexture(bool ResetTrans = false)
		{
			List<List<Size>> highestSize = new List<List<Size>>();

			for (int i = 0; i < Program.characterSelectForm.TreeView1.Nodes.Count; ++i)
			{
				TreeNode n = Program.characterSelectForm.TreeView1.Nodes[i];

				highestSize.Add(new List<Size>());

				for (int z = 0; z < n.Nodes.Count; ++z)
					highestSize[i].Add((n.Nodes[z] as RPGCharacterNode).Character.SizeOfOutput);
			}

			Size largestSize = Size.Empty;

			foreach (var n in highestSize)
			{
				int lW = 0;
				int lH = 0;

				foreach (var x in n)
				{
					lW += x.Width;

					if (lH < x.Height)
						lH = x.Height;
				}

				if (largestSize.Width < lW)
					largestSize.Width = lW;
				largestSize.Height += lH;
			}

			Size ImgSize = largestSize;

			if (ImgSize.Width == 0 || ImgSize.Height == 0)
				return null;

			var RenderedImage = new Bitmap(ImgSize.Width, ImgSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (Graphics TempGfx = Graphics.FromImage(RenderedImage))
			{
				TempGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

				int curY = 0;
				for (int i = 0; i < Program.characterSelectForm.TreeView1.Nodes.Count; ++i)
				{
					TreeNode n = Program.characterSelectForm.TreeView1.Nodes[i];

					int curX = 0;
					int highY = 0;
					for (int z = 0; z < n.Nodes.Count; ++z)
					{
						RPGCharacterNode node = n.Nodes[z] as RPGCharacterNode;

						var yNode = RenderToTexture(node.Character, true);

						if (yNode != null)
						{
							TempGfx.DrawImage(yNode, new Point(curX, curY));

							curX += yNode.Width;
							if (highY < yNode.Height)
								highY = yNode.Height;
						}
					}

					curY += highY;
				}
			}

			if (ResetTrans == false)
			{
				var ColorList = NumColorsInImage(RenderedImage);
				Color EndedUpColor = SpecialColor;

				while (ColorList.IndexOf(EndedUpColor) != -1)
					EndedUpColor = Color.FromArgb(RenderToTexture_RandomNumGen.Next(256), RenderToTexture_RandomNumGen.Next(256), RenderToTexture_RandomNumGen.Next(256));

				using (FastPixel fp = new FastPixel(RenderedImage, true))
				{
					for (int i = 0; i <= RenderedImage.Width - 1; i++)
					{
						for (int z = 0; z <= RenderedImage.Height - 1; z++)
						{
							if (fp.GetPixel(i, z).A == 0)
								fp.SetPixel(i, z, EndedUpColor);
						}
					}
				}
			}

			return RenderedImage;
		}

		// Saving/Loading
		// Bitmap utility
		static public void WriteBitmapToFile(System.IO.BinaryWriter File, Bitmap Bmp)
		{
			if (File == null || Bmp == null)
				return;

			using (FastPixel fp = new FastPixel(Bmp, false))
			{
				for (int x = 0; x <= Bmp.Width - 1; x++)
				{
					for (int y = 0; y <= Bmp.Height - 1; y++)
					{
						Color Col = fp.GetPixel(x, y);
						// Write it
						File.Write(Col.A);
						File.Write(Col.R);
						File.Write(Col.G);
						File.Write(Col.B);
					}
				}
			}

		}

		static public void ReadBitmapFromFile(System.IO.BinaryReader File, int Width, int Height, Bitmap Bmp)
		{
			if (File == null)
				return;

			FastPixel fp = null;

			if (Bmp != null)
				fp = new FastPixel(Bmp, true);

			for (int x = 0; x <= Width - 1; x++)
			{
				for (int y = 0; y <= Height - 1; y++)
				{
					Color clr = Color.FromArgb(File.ReadByte(), File.ReadByte(), File.ReadByte(), File.ReadByte());
					
					// Set this pixel
					if (Bmp != null)
						fp.SetPixel(x, y, clr);
				}
			}

			if (Bmp != null)
				fp.Dispose();
		}

		static public void WriteHeader(System.IO.BinaryWriter Writer, EHeaderValue Header)
		{
			if (Writer == null)
			{
				return;
			}

			if (Header == EHeaderValue.HeaderImagePack)
			{
				Writer.Write("chz2");
			}
			else if (Header == EHeaderValue.HeaderSheetNoImages)
			{
				Writer.Write("chz3");
			}
			else if (Header == EHeaderValue.HeaderSheetImages)
			{
				Writer.Write("chz4");
			}
		}

		public static int Percent(long CurrVal, long MaxVal)
		{
			return (int)((CurrVal / MaxVal * 100 + 0.5));
		}

		static public void PerformImagePackCreate(string Path)
		{
			Program.imagePackerForm.TreeView1.Nodes.Clear();
			Program.imagePackerForm.FlowLayoutPanel1.Controls.Clear();
			Program.imagePackerForm.FlowLayoutPanel2.Controls.Clear();
			foreach (var Node in Images.RPGNodes)
			{
				Images.AddAndRecurseNodes(Program.imagePackerForm.TreeView1.Nodes, Node);
			}

			var Result = Program.imagePackerForm.ShowDialog();
			if (Result == System.Windows.Forms.DialogResult.OK && Program.imagePackerForm.FlowLayoutPanel2.Controls.Count != 0)
			{
				// Regular compression
				using (System.IO.MemoryStream MemStrm = new System.IO.MemoryStream())
				{
					using (System.IO.BinaryWriter File = new System.IO.BinaryWriter(MemStrm))
					{
						List<RPGGameFile> GameFilesInUse = new List<RPGGameFile>();

						WriteHeader(File, EHeaderValue.HeaderImagePack);

						File.Write(Program.imagePackerForm.FlowLayoutPanel2.Controls.Count);
						foreach (RPGPictureBox Box in Program.imagePackerForm.FlowLayoutPanel2.Controls)
						{
							RPGImage Image = Box.MainImage;

							File.Write(Image != null);
							if (Image != null)
							{
								File.Write(Image.Path);

								File.Write(Image.OriginalBitmap.Width);
								File.Write(Image.OriginalBitmap.Height);

								WriteBitmapToFile(File, Image.OriginalBitmap);

								if (GameFilesInUse.Contains(Image.GameFile) == false)
								{
									GameFilesInUse.Add(Image.GameFile);
								}
							}
						}

						File.Write(GameFilesInUse.Count);
						foreach (var Game in GameFilesInUse)
						{
							Game.Save(File);
							File.Write(Game.FilePath);
						}

						// Create the compressed file.
						using (System.IO.FileStream outFile = System.IO.File.Create(Path))
						{
							using (System.IO.Compression.GZipStream Compress = new System.IO.Compression.GZipStream(outFile, System.IO.Compression.CompressionMode.Compress))
							{

								// Copy the source file into the compression stream.
								MemStrm.Seek(0, System.IO.SeekOrigin.Begin);
								Compress.Write(MemStrm.ToArray(), 0, (int)MemStrm.Length);
							}

						}


					}

				}

			}
		}

		public void PerformSheetSave(string Path, EHeaderValue Header)
		{
			using (System.IO.MemoryStream MemStrm = new System.IO.MemoryStream())
			{
				using (System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(MemStrm))
				{

					if (Writer != null)
					{
						WriteHeader(Writer, Header);

						// Write the game file (this is to ensure that receiving ends have the game)
						Images.CurrentGameFile.Save(Writer);
						// Write the path
						Writer.Write(Images.CurrentGameFile.FilePath);

						// Write every character
						Writer.Write(Program.characterSelectForm.TreeView1.Nodes.Count);
						foreach (RPGCharacterRowNode Row in Program.characterSelectForm.TreeView1.Nodes)
						{
							Writer.Write(Row.Text);

							Writer.Write(Row.Nodes.Count);
							foreach (RPGCharacterNode Col in Row.Nodes)
							{
								Col.Character.Save(Writer, (Header == EHeaderValue.HeaderSheetImages));

								Writer.Write(Col.Text);
							}
						}
					}

					SetCurrentSavePosition(Path, Header);

					using (System.IO.FileStream outFile = System.IO.File.Create(Path))
					{
						using (System.IO.Compression.GZipStream Compress = new System.IO.Compression.GZipStream(outFile, System.IO.Compression.CompressionMode.Compress))
						{

							// Copy the source file into the compression stream.
							MemStrm.Seek(0, System.IO.SeekOrigin.Begin);
							Compress.Write(MemStrm.ToArray(), 0, (int)MemStrm.Length);
						}

					}

				}

			}

		}

		public void DoSaveAs()
		{
			using (SaveFileDialog SaveDlg = new SaveFileDialog())
			{
				SaveDlg.AddExtension = true;
				SaveDlg.Filter = "Character Sheet (No Images) (.chs)|*.chs|Character Sheet (Images) (.chs)|*.chs|All Files|*";
				SaveDlg.DefaultExt = "chz";
				SaveDlg.RestoreDirectory = true;
				SaveDlg.FilterIndex = 1;
				var Result = SaveDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					if (SaveDlg.FilterIndex == 1)
					{
						PerformSheetSave(SaveDlg.FileName, EHeaderValue.HeaderSheetNoImages);
					}
					else if (SaveDlg.FilterIndex == 2)
					{
						PerformSheetSave(SaveDlg.FileName, EHeaderValue.HeaderSheetImages);
					}
				}
			}

		}

		public void DoSave()
		{
			if (string.IsNullOrEmpty(LastSavePath.Path))
			{
				DoSaveAs();
			}
			else
			{
				PerformSheetSave(LastSavePath.Path, LastSavePath.Header);
			}
		}

		public void ToolStripMenuItem2_Click(System.Object sender, System.EventArgs e)
		{
			DoSave();
		}

		public void ToolStripMenuItem3_Click(System.Object sender, System.EventArgs e)
		{
			DoSaveAs();
		}

		public void ToolStripButton18_Click(System.Object sender, System.EventArgs e)
		{
			DoSave();
		}

		// Loading portion
		static public EHeaderValue ReadHeader(System.IO.BinaryReader Reader)
		{
			if (Reader == null)
			{
				return EHeaderValue.HeaderUnknown;
			}

			string Header = Reader.ReadString();

			if (Header == "chz0" || Header == "chz1")
			{
				MessageBox.Show("Single character files are no longer supported.");
				return EHeaderValue.HeaderUnknown;
			}
			else if (Header == "chz2")
			{
				return EHeaderValue.HeaderImagePack;
			}
			else if (Header == "chz3")
			{
				return EHeaderValue.HeaderSheetNoImages;
			}
			else if (Header == "chz4")
			{
				return EHeaderValue.HeaderSheetImages;
			}
			return EHeaderValue.HeaderUnknown;
		}

		private List<string> ImagesNotFound = new List<string>();
		public List<string> PackImagesNotFound
		{
			get { return ImagesNotFound; }
		}

		private List<string> ImagesAcquired_ = new List<string>();
		public List<string> ImagesAcquired
		{
			get { return ImagesAcquired_; }
		}

		public void PerformOpen(string Path)
		{
			try
			{
				using (System.IO.MemoryStream DecompressedFile = new System.IO.MemoryStream())
				{
					using (var InFile = System.IO.File.Open(Path, System.IO.FileMode.Open))
					{
						using (System.IO.Compression.GZipStream FileToBeDcmp = new System.IO.Compression.GZipStream(InFile, System.IO.Compression.CompressionMode.Decompress))
						{
							byte[] buffer = new byte[4096];
							int numRead;
							numRead = FileToBeDcmp.Read(buffer, 0, buffer.Length);
							while (numRead != 0)
							{
								DecompressedFile.Write(buffer, 0, numRead);
								numRead = FileToBeDcmp.Read(buffer, 0, buffer.Length);
							}
						}


						DecompressedFile.Position = 0;
						System.IO.BinaryReader Reader = new System.IO.BinaryReader(DecompressedFile);

						if (Reader != null)
						{
							EHeaderValue Header = ReadHeader(Reader);
							if (Header == EHeaderValue.HeaderUnknown)
							{
								MessageBox.Show("Error opening " + Path + ": invalid header.", "", MessageBoxButtons.OK);
								Reader.Close();
								return;
							}
							else if (Header == EHeaderValue.HeaderImagePack)
							{
								int Count = Reader.ReadInt32();
								List<string> StrList = new List<string>();

								for (int i = 0; i <= Count - 1; i++)
								{
									var DoRead = Reader.ReadBoolean();

									if (DoRead)
									{
										string Img = Reader.ReadString();
										var Image = Images.FindImage(Img);
										int Width = Reader.ReadInt32();
										int Height = Reader.ReadInt32();
										using (Bitmap BMP = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
										{
											ReadBitmapFromFile(Reader, BMP.Width, BMP.Height, BMP);

											if (System.IO.File.Exists(Img))
											{
												if (Images.BitmapCompare(BMP, Images.FindImage(Img).OriginalBitmap) == false)
												{
													// Message the user
													if (MessageBox.Show("The image " + Img + " already exists in your image folder, but yours differs from the one in the pack. Would you like to overwrite with this one?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
													{
														BMP.Save(Img);
														StrList.Add(Img);
													}
												}
											}
											else if (System.IO.File.Exists(Img) == false)
											{
												System.IO.Directory.CreateDirectory(Img.Substring(0, Img.LastIndexOf("\\")));

												// Save the bitmap
												BMP.Save(Img);

												// Set our image to a temporary image for later re-loading
												Image = new RPGImage();
												Image.Path = Img;
												StrList.Add(Img);
											}
										}

									}
								}

								Count = Reader.ReadInt32();
								for (int i = 0; i <= Count - 1; i++)
								{
									RPGGameFile CheckGameFile = new RPGGameFile();

									CheckGameFile.Load(Reader);
									CheckGameFile.FilePath = Reader.ReadString();

									RPGGameFile FoundGameFile = Images.FindGame(CheckGameFile);

									if (FoundGameFile != null)
									{
										Images.CurrentGameFile.MenuItem.Checked = false;
										Images.CurrentGameFile = FoundGameFile;
										FoundGameFile.MenuItem.Checked = true;
									}
									else
									{
										System.IO.Directory.CreateDirectory(CheckGameFile.FilePath.Substring(0, CheckGameFile.FilePath.LastIndexOf("\\")));
										using (System.IO.BinaryWriter File = new System.IO.BinaryWriter(System.IO.File.Open(CheckGameFile.FilePath, System.IO.FileMode.Create)))
											CheckGameFile.Save(File);

										Images.RPGGames.Add(CheckGameFile);
										Images.CurrentGameFile = CheckGameFile;

										GameModeMenuItem MenuItem = new GameModeMenuItem();
										MenuItem.GameFile = CheckGameFile;
										CheckGameFile.MenuItem = MenuItem;
										SetGameMenuItem.DropDownItems.Add(MenuItem);
									}
								}

								string ResultStr = (StrList.Count.ToString() + " images were added to your list:\n\n");

								foreach (var Stri in StrList)
									ResultStr += (Stri + '\n');

								MessageBox.Show(ResultStr);
							}
							else if (Header == EHeaderValue.HeaderSheetNoImages || Header == EHeaderValue.HeaderSheetImages)
							{
								// Read the game
								RPGGameFile CheckGameFile = new RPGGameFile();
								CheckGameFile.Load(Reader);
								CheckGameFile.FilePath = Reader.ReadString();

								RPGGameFile FoundGameFile = Images.FindGame(CheckGameFile);

								if (FoundGameFile != null)
								{
									Images.CurrentGameFile.MenuItem.Checked = false;
									Images.CurrentGameFile = FoundGameFile;
									FoundGameFile.MenuItem.Checked = true;
								}
								else
								{
									System.IO.Directory.CreateDirectory(CheckGameFile.FilePath.Substring(0, CheckGameFile.FilePath.LastIndexOf("\\")));
									using (System.IO.BinaryWriter File = new System.IO.BinaryWriter(System.IO.File.Open(CheckGameFile.FilePath, System.IO.FileMode.Create)))
										CheckGameFile.Save(File);

									Images.RPGGames.Add(CheckGameFile);
									Images.CurrentGameFile = CheckGameFile;

									GameModeMenuItem MenuItem = new GameModeMenuItem();
									MenuItem.GameFile = CheckGameFile;
									CheckGameFile.MenuItem = MenuItem;
									SetGameMenuItem.DropDownItems.Add(MenuItem);
								}

								Program.characterSelectForm.TreeView1.Nodes.Clear();
								int RowCount = Reader.ReadInt32();
								for (int i = 0; i <= RowCount - 1; i++)
								{
									RPGCharacterRowNode Row = Program.characterSelectForm.CharacterList.AddRow();
									Row.Text = Reader.ReadString();

									int CharCount = Reader.ReadInt32();
									for (int x = 0; x <= CharCount - 1; x++)
									{
										RPGCharacterNode Character = Row.AddCharacter();
										Character.Character.Load(Reader, (Header == EHeaderValue.HeaderSheetImages));
										Character.Text = Reader.ReadString();
									}
								}
								Program.characterSelectForm.TreeView1.ExpandAll();
								Program.characterSelectForm.TreeView1.SelectedNode = Program.characterSelectForm.TreeView1.Nodes[0].Nodes[0];
								UpdateDrawing();

								if (Header == EHeaderValue.HeaderSheetImages)
									Images.ReloadImages();

								if (ImagesNotFound.Count != 0)
								{
									string Prompt = (string)("The character file loaded with the following errors:\n\n");
									foreach (var MissingImg in ImagesNotFound)
										Prompt += (string)("The image " + MissingImg + " was not found.\n");

									MessageBox.Show(Prompt);
									ImagesNotFound.Clear();
								}
								if (ImagesAcquired.Count != 0)
								{
									string ResultStr = (string)(ImagesAcquired.Count.ToString() + " images were added to your list:\n\n");
									foreach (var Stri in ImagesAcquired)
										ResultStr += Stri + '\n';

									MessageBox.Show(ResultStr);
									ImagesAcquired.Clear();
								}
								SetCurrentSavePosition(Path, Header);
							}

							Reader.Close();
						}
					}

				}
			}
			catch
			{
				MessageBox.Show("Unable to load file!\n");
			}

			Images.ReloadImages();
			UpdateDrawing();
		}

		public void HitOpen()
		{
			if (MessageBox.Show("Opening a new character file will erase this unsaved character/sheet. Do you wish to save first?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				DoSaveAs();
				SetCurrentSavePosition("", EHeaderValue.HeaderUnknown);
			}

			ResetEverything();

			using (OpenFileDialog OpenDlg = new OpenFileDialog())
			{
				OpenDlg.AddExtension = true;
				OpenDlg.Filter = "Supported Files (.chz;.chx;.chs)|*.chz;*.chx;*.chs|All Files|*";
				OpenDlg.DefaultExt = "chz";
				OpenDlg.RestoreDirectory = true;
				var Result = OpenDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
					PerformOpen(OpenDlg.FileName);
			}
		}

		public void ToolStripMenuItem4_Click(System.Object sender, System.EventArgs e)
		{
			HitOpen();
		}

		public void ToolStripButton13_Click(System.Object sender, System.EventArgs e)
		{
			HitOpen();
		}

		// Export sprite to image
		static public List<Color> NumColorsInImage(Bitmap img)
		{
			if (img == null)
				return null;

			List<Color> ColorsInImage = new List<Color>();

			using (FastPixel fp = new FastPixel(img, false))
			{
				for (int i = 0; i <= img.Width - 1; i++)
				{
					for (int z = 0; z <= img.Height - 1; z++)
					{
						if (ColorsInImage.Contains(fp.GetPixel(i, z)))
							continue;
						else
							ColorsInImage.Add(fp.GetPixel(i, z));
					}
				}
			}

			return ColorsInImage;
		}

		public void ExportSprite()
		{
			using (SaveFileDialog SaveDlg = new SaveFileDialog())
			{
				SaveDlg.AddExtension = true;
				SaveDlg.Filter = "8-Bit PNG Files (RPG2000/3) (.png)|*.png|32-Bit PNG Files (.png)|*.png|JPG Files (.jpg)|*.jpg|BMP Files (.bmp)|*.bmp|All Files|*";
				SaveDlg.DefaultExt = "png";
				SaveDlg.RestoreDirectory = true;
				if (Images.CurrentGameFile.Is8Bit)
					SaveDlg.FilterIndex = 1;
				else
					SaveDlg.FilterIndex = 2;
				var Result = SaveDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					Bitmap Image = RenderToTexture(Program.characterSelectForm.GetSelectedCharacter().Character, SaveDlg.FilterIndex == 2);

					if (SaveDlg.FilterIndex == 1)
					{
						var Colors = NumColorsInImage(Image);

						if (Colors.Count <= 256)
						{
							using (Bitmap Indexed = new Bitmap(Image.Width, Image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
							{
								// Create a palette
								System.Drawing.Imaging.ColorPalette Pal = Indexed.Palette;
								for (int i = 0; i <= 255; i++)
								{
									if (i > Colors.Count - 1)
										Pal.Entries[i] = Color.White;
									else
										Pal.Entries[i] = Colors[i];
								}
								Indexed.Palette = Pal;

								System.Drawing.Imaging.BitmapData bmd = Indexed.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, Indexed.PixelFormat);

								using (FastPixel fp = new FastPixel(Image, false))
								{
									for (var y = 0; y <= bmd.Height - 1; y++)
									{
										for (var x = 0; x <= bmd.Width - 1; x++)
											System.Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (int)((bmd.Stride * y) + (x)), (byte)(Colors.IndexOf(fp.GetPixel((int)(x), (int)(y)))));
									}
								}


								Indexed.UnlockBits(bmd);
								Image = Indexed.Clone(new Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
							}
						}
						else
						{
							MessageBox.Show("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.");
						}
					}

					int ExtLoc = SaveDlg.FileName.LastIndexOf(".");
					if (ExtLoc == -1)
					{
						SaveDlg.FileName = SaveDlg.FileName.Insert(SaveDlg.FileName.Length, ".png");
						Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
					}
					else
					{
						string Loc = SaveDlg.FileName.Substring(ExtLoc + 1);
						if (string.Compare(Loc, "bmp", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
						else if (string.Compare(Loc, "png", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
						else if (string.Compare(Loc, "jpg", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
					}
				}
			}

		}

		public void ExportSheet()
		{
			using (SaveFileDialog SaveDlg = new SaveFileDialog())
			{
				SaveDlg.AddExtension = true;
				SaveDlg.Filter = "8-Bit PNG Files (RPG2000/3) (.png)|*.png|32-Bit PNG Files (.png)|*.png|JPG Files (.jpg)|*.jpg|BMP Files (.bmp)|*.bmp|All Files|*";
				SaveDlg.DefaultExt = "png";
				SaveDlg.RestoreDirectory = true;
				if (Images.CurrentGameFile.Is8Bit)
					SaveDlg.FilterIndex = 1;
				else
					SaveDlg.FilterIndex = 2;
				var Result = SaveDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					Bitmap Image = RenderSheetToTexture(SaveDlg.FilterIndex == 2);

					if (SaveDlg.FilterIndex == 1)
					{
						var Colors = NumColorsInImage(Image);

						if (Colors.Count <= 256)
						{
							using (Bitmap Indexed = new Bitmap(Image.Width, Image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
							{
								// Create a palette
								System.Drawing.Imaging.ColorPalette Pal = Indexed.Palette;
								for (int i = 0; i <= 255; i++)
								{
									if (i > Colors.Count - 1)
										Pal.Entries[i] = Color.White;
									else
										Pal.Entries[i] = Colors[i];
								}
								Indexed.Palette = Pal;

								System.Drawing.Imaging.BitmapData bmd = Indexed.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, Indexed.PixelFormat);

								using (FastPixel fp = new FastPixel(Image, false))
								{
									for (var y = 0; y <= bmd.Height - 1; y++)
									{
										for (var x = 0; x <= bmd.Width - 1; x++)
											System.Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (int)((bmd.Stride * y) + (x)), (byte)(Colors.IndexOf(fp.GetPixel((int)(x), (int)(y)))));
									}
								}


								Indexed.UnlockBits(bmd);
								Image = Indexed.Clone(new Rectangle(0, 0, Image.Width, Image.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
							}
						}
						else
						{
							MessageBox.Show("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.");
						}
					}

					int ExtLoc = SaveDlg.FileName.LastIndexOf(".");
					if (ExtLoc == -1)
					{
						SaveDlg.FileName = SaveDlg.FileName.Insert(SaveDlg.FileName.Length, ".png");
						Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
					}
					else
					{
						string Loc = SaveDlg.FileName.Substring(ExtLoc + 1);
						if (string.Compare(Loc, "bmp", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
						else if (string.Compare(Loc, "png", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
						else if (string.Compare(Loc, "jpg", true) == 0)
							Image.Save(SaveDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
					}
				}
			}
		}

		public void ToolStripMenuItem5_Click(System.Object sender, System.EventArgs e)
		{
			ExportSprite();
		}

		public void ResetCharacter()
		{
			Program.characterSelectForm.GetSelectedCharacter().Character.Layers.Clear();
			Program.layersWindowForm.TreeView1.Nodes.Clear();
			Program.layersWindowForm.CurrentNode = null;
			Program.layersWindowForm.ChangeStates(false);

			UpdateDrawing();
		}

		public void NewCharacter()
		{
			var Result = MessageBox.Show("You may have unsaved work. Would you like to save your changes before creating a new character?", "", MessageBoxButtons.YesNoCancel);
			if (Result == DialogResult.Yes)
				DoSaveAs();
			else if (Result == DialogResult.Cancel)
				return;

			ResetCharacter();
		}

		public void SingleCharacterToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			NewCharacter();
		}

		public void ResetEverything()
		{
			// Clear the layers list
			Program.characterSelectForm.ClearCharacters();
			Program.layersWindowForm.TreeView1.Nodes.Clear();
			Program.layersWindowForm.CurrentNode = null;
			Program.layersWindowForm.ChangeStates(false);

			UpdateDrawing();
		}

		public bool NewSheet()
		{
			var Result = MessageBox.Show("You may have unsaved work. Creating a new sheet will make you lose all of your characters in this sheet. Would you like to save your sheet first?", "", MessageBoxButtons.YesNoCancel);
			if (Result == DialogResult.Yes)
				DoSaveAs();
			else if (Result == DialogResult.Cancel)
				return false;

			ResetEverything();
			return true;
		}

		public void CharacterSetToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			NewSheet();
		}

		public void ImagePackToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			using (SaveFileDialog SaveDlg = new SaveFileDialog())
			{
				SaveDlg.AddExtension = true;
				SaveDlg.Filter = "Image Pack (.chx)|*.chx|All Files|*";
				SaveDlg.DefaultExt = "chx";
				SaveDlg.RestoreDirectory = true;
				var Result = SaveDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
					PerformImagePackCreate(SaveDlg.FileName);
			}

		}

		public void ToolStripButton1_Click(System.Object sender, System.EventArgs e)
		{
			if ((Program.characterSelectForm.TreeView1.SelectedNode) == null)
				return;

			if (Program.characterSelectForm.TreeView1.SelectedNode.GetType() == typeof(RPGCharacterRowNode))
				NewSheet();
			else
				NewCharacter();
		}

		public void DoClose()
		{
			FinalClosing = true;
			this.Close();
		}

		public void ExitToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoClose();
		}

		// Camera code
		public bool MovingCamera = false;
		private Point _mouseDelta = new Point(0, 0);
		public Point MouseDelta
		{
			get { return _mouseDelta; }
			set { _mouseDelta = value; }
		}
		
		private Point _oldMousePos = new Point(0, 0);
		public Point OldMousePos
		{
			get { return _oldMousePos; }
			set { _oldMousePos = value; }
		}

		private Point CameraPosition = new Point(0, 0);
		public Point CanvasCamera
		{
			get { return CameraPosition; }
			set { CameraPosition = value; }
		}

		// Reset camera
		public void ResetCameraToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			CameraPosition = Point.Empty;
			UpdateDrawing();
		}

		// Printing code
		private System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();

		// The PrintDialog will print the document
		// by handling the document's PrintPage event.
		public void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			// Insert code to render the page here.
			// This code will be called when the control is drawn.
			e.Graphics.DrawImage(RenderSheetToTexture(), 0, 0);
		}

		public void DoPrint()
		{
			using (PrintDialog PrintDialog1 = new PrintDialog())
			{
				// Allow the user to choose the page range he or she would
				// like to print.
				PrintDialog1.AllowSomePages = true;

				// Show the help button.
				PrintDialog1.ShowHelp = true;

				// Set the Document property to the PrintDocument for
				// which the PrintPage Event has been handled. To display the
				// dialog, either this property or the PrinterSettings property
				// must be set
				PrintDialog1.Document = docToPrint;

				DialogResult result = PrintDialog1.ShowDialog();

				// If the result is OK then print the document.
				if (result == DialogResult.OK)
					docToPrint.Print();
			}
		}

		public void ToolStripMenuItem6_Click(System.Object sender, System.EventArgs e)
		{
			DoPrint();
		}

		// Print Preview
		public void ToolStripMenuItem8_Click(System.Object sender, System.EventArgs e)
		{
			using (PrintPreviewDialog Prev = new PrintPreviewDialog())
			{
				//Set the size, location, and name.
				Prev.ClientSize = new System.Drawing.Size(400, 300);
				Prev.Location = new System.Drawing.Point(29, 29);
				Prev.Name = "Print Preview";

				// Set the minimum size the dialog can be resized to.
				Prev.MinimumSize = new System.Drawing.Size(375, 250);
				Prev.Document = docToPrint;

				Prev.ShowDialog();
			}
		}

		// About
		public void AboutToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			using (About abt = new About())
				abt.ShowDialog();
		}

		static void easyPositionForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Program.canvasForm.easyPositionForm = null;
		}

		// Clear Settings
		public void ToolStripMenuItem9_Click(System.Object sender, System.EventArgs e)
		{
			Canvas.Settings.Reset();

			if (MessageBox.Show("In order for the settings to be fully cleared, the application must be closed. You can restart the app after this. Proceed?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				FinalClosing = true;
				this.Close();
			}
		}

		// Game Editor
		public void ToolStripMenuItem10_Click(System.Object sender, System.EventArgs e)
		{
			using (GameEditor ged = new GameEditor())
				ged.ShowDialog();
		}

		// Animation number
		private int AnimationIndexVal = 1;
		public int AnimationIndex
		{
			get { return AnimationIndexVal; }
			set { AnimationIndexVal = value; }
		}

		public void UpdateAnimNum(int Wanted)
		{
			if (Images.CurrentGameFile == null)
			{
				Wanted = 0;
				ToolStripTextBox3.Text = "0";
				return;
			}

			if (Wanted > Images.CurrentGameFile.Animations.Count)
				Wanted = (int)(Images.CurrentGameFile.Animations.Count);
			else if (Wanted < 0)
				Wanted = 0;

			if (Wanted == 0)
			{
				AnimationIndexVal = 0;
				ToolStripTextBox3.Text = AnimationIndexVal.ToString();
				return;
			}

			AnimationIndexVal = Wanted;
			ToolStripTextBox3.Text = AnimationIndexVal.ToString();
			AnimationFrame = 0;

			if (AnimTimer.Enabled)
			{
				AnimTimer.Stop();
				AnimTimer.Start();
			}
		}

		public void ToolStripButton1_Click_1(System.Object sender, System.EventArgs e)
		{
			UpdateAnimNum(AnimationIndexVal + 1);
		}

		public void ToolStripButton2_Click(System.Object sender, System.EventArgs e)
		{
			UpdateAnimNum(AnimationIndexVal - 1);
		}

		public void ToolStripTextBox3_KeyPress(System.Object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)(Keys.Return) || e.KeyChar == (char)(Keys.Enter))
			{
				e.Handled = true;
				bool EndResult;
				int EndValue;
				EndResult = int.TryParse(ToolStripTextBox3.Text, out EndValue);

				if (EndResult == false)
					return;

				UpdateAnimNum(EndValue);
			}
		}

		// Reload Images
		public void ToolStripMenuItem11_Click(System.Object sender, System.EventArgs e)
		{
			Images.ReloadImages();
		}

		// Sheet Creator
		public void SetSheetCreatorToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			using (SetSheetCreator ssc = new SetSheetCreator())
				ssc.ShowDialog();
		}

		private void ContentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(".\\Documentation\\OpenCharas Documentation.htm"))
				Process.Start(".\\Documentation\\OpenCharas Documentation.htm");
			else
				Process.Start("http://opencharas.alteredsoftworks.com/documentation/OpenCharas Documentation.htm");
		}

		private void ToolStripButton19_Click(object sender, EventArgs e)
		{
			ExportSheet();
		}

		private void entireSheetToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			ExportSheet();
		}

		private void SetGameMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void ToolStripButton20_Click(object sender, EventArgs e)
		{
			DoPrint();
		}
	}

	public enum EHeaderValue
	{
		// Unsupported
		HeaderCharacterNoImages,
		HeaderCharacterImages,

		// Supported
		HeaderImagePack,
		HeaderSheetNoImages,
		HeaderSheetImages,
		HeaderUnknown
	}

	// Position/size saving code
	public sealed class WindowGeometry
	{
		static public void FromString(string thisWindowGeometry, Form formIn)
		{
			if (string.IsNullOrEmpty(thisWindowGeometry))
				return;

			if (formIn == null)
				return;

			string[] numbers = thisWindowGeometry.Split('|'.ToString().ToCharArray());
			string windowString = numbers[4];

			if (windowString == "Normal")
			{
				Point windowPoint = new Point(int.Parse(numbers[0]), int.Parse(numbers[1]));
				Size windowSize = new Size(int.Parse(numbers[2]), int.Parse(numbers[3]));

				bool locOkay = InBizarreLocation(windowPoint, windowSize);
				bool sizeOkay = IsBizarreSize(windowSize);

				if (locOkay == true && sizeOkay == true)
				{
					formIn.Location = windowPoint;
					formIn.Size = windowSize;
					formIn.StartPosition = FormStartPosition.Manual;
					formIn.WindowState = FormWindowState.Normal;
				}
				else if (sizeOkay == true)
					formIn.Size = windowSize;
			}
			else if (windowString == "Maximized")
			{
				formIn.Location = new Point(100, 100);
				formIn.StartPosition = FormStartPosition.Manual;
				formIn.WindowState = FormWindowState.Maximized;
			}
		}

		static public bool InBizarreLocation(Point Loc, Size Size)
		{
			bool locOkay;

			if (Loc.X < 0 || Loc.Y < 0)
				locOkay = false;
			else if (Loc.X + Size.Width > Screen.PrimaryScreen.WorkingArea.Width)
				locOkay = false;
			else if (Loc.Y + Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
				locOkay = false;
			else
				locOkay = true;

			return locOkay;
		}

		static public bool IsBizarreSize(Size Size)
		{
			return (Size.Height <= Screen.PrimaryScreen.WorkingArea.Height && Size.Width <= Screen.PrimaryScreen.WorkingArea.Width);
		}

		static public string ToString(Form mainForm)
		{
			if (mainForm == null)
				return "";
			return mainForm.Location.X.ToString() + "|" + mainForm.Location.Y.ToString() + "|" + mainForm.Size.Width.ToString() + "|" + mainForm.Size.Height.ToString() + "|" + mainForm.WindowState.ToString();
		}
	}
}
