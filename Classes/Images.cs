using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace OpenCharas
{
	public class RPGImage
	{
		public Bitmap RawBitmap { get; set; }
		public Bitmap OriginalBitmap { get; set; }
		public RPGPictureBox Box { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }
		public RPGGameFile GameFile { get; set; }
	}

	public class GameNodeEntry
	{
		public List<GameNodeEntry> Subdirs { get; private set; }
		public List<RPGImage> Images { get; private set; }
		public string NodeName { get; set; }
		public string Path { get; set; }
		public RPGGameFile GameFile { get; set; }

		public GameNodeEntry()
		{
			Subdirs = new List<GameNodeEntry>();
			Images = new List<RPGImage>();
		}

		public void RecurseNodeAndAddAll()
		{
			//foreach (var Image in Images)
				//Program.imagePackerForm.FlowLayoutPanel2.Controls.Add(Image.Box.CreatePictureBoxCopy());

			foreach (var Subdir in Subdirs)
				Subdir.RecurseNodeAndAddAll();
		}
	}

	public class GameNodeNode : TreeNode
	{
		public GameNodeEntry RPGNode { get; set; }
	}

	public class GameModeMenuItem : ToolStripMenuItem
	{
		public RPGGameFile GameFile { get; set; }
	}

	public static class Images
	{
		public static List<GameNodeEntry> RPGNodes = new List<GameNodeEntry>();
		public static List<RPGGameFile> RPGGames = new List<RPGGameFile>();

		static public bool CompareGameFiles(RPGGameFile Left, RPGGameFile Right)
		{
			if (Left == null || Right == null)
				return false;

			if (Left.SheetRows != Right.SheetRows)
				return false;
			if (Left.SheetColumns != Right.SheetColumns)
				return false;
			if (Left.PreviewFrame != Right.PreviewFrame)
				return false;
			if (Left.Is8Bit != Right.Is8Bit)
				return false;
			if (Left.GameName != Right.GameName)
				return false;
			if (Left.Animations.Count != Right.Animations.Count)
				return false;

			for (int i = 0; i <= Left.Animations.Count - 1; i++)
			{
				if (Left.Animations[i].Frames.Count != Right.Animations[i].Frames.Count)
					return false;

				for (int z = 0; z <= Left.Animations[i].Frames.Count - 1; z++)
				{
					if (Left.Animations[i].Frames[z] != Right.Animations[i].Frames[z])
						return false;
				}
			}

			return true;
		}

		static public RPGGameFile FindGame(RPGGameFile CheckGame)
		{
			if (CheckGame == null)
			{
				return null;
			}

			foreach (var GameFile in RPGGames)
			{
				if (string.Compare(GameFile.FilePath, CheckGame.FilePath, true) == 0)
				{
					// Check game file
					if (CompareGameFiles(CheckGame, GameFile) == true)
						return GameFile; // Gotcha
					else
					{
						if (MessageBox.Show("The required game file (" + CheckGame.FilePath + ") varies from your game file version. In order for this to work properly, you must replace your game file with this one.\n\nA backup will be made at " + CheckGame.FilePath + ".bak. Continue?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
						{
							// FIXME: do it
							System.IO.File.Copy(GameFile.FilePath, GameFile.FilePath + ".bak");

							using (System.IO.BinaryWriter File = new System.IO.BinaryWriter(System.IO.File.Open(GameFile.FilePath, System.IO.FileMode.Truncate)))
								CheckGame.Save(File);

							return GameFile;
						}
						else
							return null;
					}
				}
			}
			return null;
		}

		static public RPGGameFile FindGameByPath(string Path)
		{
			foreach (var GameFile in RPGGames)
			{
				if (string.Compare(GameFile.Path, Path, true) == 0)
					return GameFile;
			}
			return null;
		}

		static public RPGImage SearchImage(List<GameNodeEntry> List, string[] Paths, int CurIndex)
		{
			foreach (var Node in List)
			{
				if (string.Compare((string)Node.NodeName, Paths[CurIndex], true) == 0)
				{
					CurIndex++;

					// Are we at the end of the list now
					if (CurIndex == Paths.Length - 1)
					{
						// See if we have said image
						foreach (var Img in Node.Images)
						{
							if (string.Compare((string)Img.Name, Paths[CurIndex], true) == 0)
								return Img;
						}
						return null;
					}
					else
					{
						// Keep searching from here
						return SearchImage(Node.Subdirs, Paths, CurIndex);
					}
				}
			}

			return null;
		}

		static public RPGImage FindImage(string Path)
		{
			string Main = Path.Substring(5);
			string[] Paths = Main.Split('\\'.ToString().ToCharArray());

			return SearchImage(RPGNodes, Paths, 0);
		}

		static public bool BitmapCompare(Bitmap Bmp1, Bitmap Bmp2)
		{
			if (Bmp1.Size != Bmp2.Size)
			{
				return false;
			}

			using (FastPixel fpBmp1 = new FastPixel(Bmp1, true))
			{
				using (FastPixel fpBmp2 = new FastPixel(Bmp2, true))
				{
					for (int y = 0; y <= fpBmp1.Height - 1; y++)
					{
						for (int x = 0; x <= fpBmp1.Width - 1; x++)
						{
							if (fpBmp1.GetPixel(x, y) != fpBmp2.GetPixel(x, y))
							{
								return false;
							}
						}
					}
				}
			}


			return true; // Same
		}

		static public Bitmap Convert8BPPTo32BPP(Image Convert)
		{
			if (Convert == null)
			{
				return null;
			}

			var TempMap = new Bitmap(Convert.Width, Convert.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics Gfx = Graphics.FromImage(TempMap);
			Gfx.DrawImage(Convert, new Rectangle(0, 0, Convert.Width, Convert.Height));
			Gfx.Dispose();

			return TempMap;
		}

		static public void LoadRPGImage(GameNodeEntry Node, string Path)
		{
			if (Node == null || Path == null)
			{
				return;
			}

			RPGImage Img = new RPGImage();
			using (Bitmap TmpImage = new Bitmap(Path))
			{
				Bitmap TempMap = Convert8BPPTo32BPP(TmpImage);

				Img.RawBitmap = new Bitmap(TempMap.Width, TempMap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Img.OriginalBitmap = new Bitmap(TmpImage);
				Img.GameFile = Node.GameFile;

				using (FastPixel ImgFP = new FastPixel(Img.RawBitmap, true))
				{
					using (FastPixel TempMapFP = new FastPixel(TempMap, true))
					{
						Color TransColor = TempMapFP.GetPixel(0, 0);

						for (int i = 0; i <= TempMap.Width - 1; i++)
						{
							for (int z = 0; z <= TempMap.Height - 1; z++)
							{
								if (TempMapFP.GetPixel(i, z) == TransColor)
								{
									ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0));
								}
								else
								{
									ImgFP.SetPixel(i, z, TempMapFP.GetPixel(i, z));
								}
							}
						}
					}

				}

			}


			Img.Path = Path;
			Img.Name = Path.Substring((int)(Path.LastIndexOf("\\") + 1));
			Img.Box = RPGPictureBox.Create(Img);

			Node.Images.Add(Img);
		}

		static public void AddAndRecurseNodes(TreeNodeCollection Nodes, GameNodeEntry GameNode)
		{
			if (Nodes == null || GameNode == null)
			{
				return;
			}

			// Insert me
			GameNodeNode Node = new GameNodeNode();
			Node.RPGNode = GameNode;
			Node.Text = GameNode.NodeName;

			Nodes.Add(Node);

			foreach (var SubNode in GameNode.Subdirs)
			{
				AddAndRecurseNodes(Node.Nodes, SubNode);
			}
		}

		static public void RecurseSubdirectories(GameNodeEntry Node)
		{
			if (Node == null)
			{
				return;
			}

			string[] entries = System.IO.Directory.GetDirectories(Node.Path);

			foreach (var directory in entries)
			{
				if (directory.Substring(directory.LastIndexOf("\\") + 1).StartsWith("."))
				{
					continue;
				}

				GameNodeEntry Entry = new GameNodeEntry();
				Entry.NodeName = (string)(directory.Substring(directory.LastIndexOf("\\") + 1));
				Entry.Path = directory.ToString();
				Entry.GameFile = CurrentGameFile;

				List<string> Files = new List<string>();
				Files.AddRange(System.IO.Directory.GetFiles(directory.ToString(), "*.png"));
				Files.AddRange(System.IO.Directory.GetFiles(directory.ToString(), "*.bmp"));
				Files.AddRange(System.IO.Directory.GetFiles(directory.ToString(), "*.jpg"));

				// See if path has any images
				foreach (var Img in Files)
				{
					LoadRPGImage(Entry, Img.ToString());
				}

				RecurseSubdirectories(Entry);

				Node.Subdirs.Add(Entry);
			}
		}

		public static RPGGameFile CurrentGameFile;

		static public void GameMenuItemClick(object sender, EventArgs e)
		{
			GameModeMenuItem Item = (GameModeMenuItem)sender;

			if (Item.GameFile == CurrentGameFile)
				return;

			if (!Program.canvasForm.NewSheet())
				return;

			// Set game
			CurrentGameFile.MenuItem.Checked = false;
			CurrentGameFile = Item.GameFile;
			Item.Checked = true;

			Canvas.Settings.CurrentGame = CurrentGameFile.Path;
		}

		static public void ClearImagesSubDirs(GameNodeEntry Node)
		{
			if (Node == null)
				return;

			foreach (var SubNode in Node.Subdirs)
			{
				SubNode.Images.Clear();
				ClearImagesSubDirs(SubNode);
			}
		}

		static public void ClearImages()
		{
			RPGNodes.Clear();
			Program.itemsWindowForm.imageViewerContainer1.Images = null;
			Program.itemsWindowForm.TreeView1.Nodes.Clear();
		}

		// Loads images and game files
		static public void LoadImages()
		{
			string[] subdirectoryEntries = System.IO.Directory.GetDirectories("data\\");

			RPGGameFile WillBeCurrentGame = null;
			foreach (var Game in subdirectoryEntries)
			{
				if (Game.Substring(Game.LastIndexOf("\\") + 1).StartsWith("."))
				{
					continue;
				}
				string[] fileEntries = System.IO.Directory.GetFiles(Game.ToString());

				GameNodeEntry Entry = new GameNodeEntry();
				Entry.NodeName = (string)(Game.Substring(Game.LastIndexOf("\\") + 1));
				Entry.Path = Game.ToString();

				bool hasGame = false;

				// Is there a .gam file in here
				foreach (var File in fileEntries)
				{
					if (File.EndsWith(".gam"))
					{
						Entry.GameFile = new RPGGameFile();
						using (var infile = System.IO.File.Open(File.ToString(), System.IO.FileMode.Open))
						{
							using (System.IO.BinaryReader Reader = new System.IO.BinaryReader(infile))
							{
								Entry.GameFile.FilePath = File.ToString();

								if (Reader != null)
								{
									Entry.GameFile.Load(Reader);
									Entry.GameFile.Path = Entry.Path;
								}

								RPGGames.Add(Entry.GameFile);

								CurrentGameFile = Entry.GameFile;
								if (Canvas.Settings.CurrentGame == CurrentGameFile.Path)
									WillBeCurrentGame = CurrentGameFile;

								hasGame = true;
								break;
							}
						}
					}
				}

				if (hasGame)
				{
					RecurseSubdirectories(Entry);
					RPGNodes.Add(Entry);
				}
			}

			if (WillBeCurrentGame != null)
			{
				CurrentGameFile = WillBeCurrentGame;
			}
			else
			{
				CurrentGameFile = null;
			}

			// Fill in the menu
			foreach (var Game in RPGGames)
			{
				GameModeMenuItem MenuItem = new GameModeMenuItem();
				MenuItem.GameFile = Game;
				Game.MenuItem = MenuItem;
				MenuItem.Text = (string)Game.GameName;
				MenuItem.Click += new System.EventHandler(GameMenuItemClick);

				if (CurrentGameFile != null)
				{
					if (Game == CurrentGameFile)
					{
						MenuItem.Checked = true;
					}
				}
				else if (Canvas.Settings.FirstRun || Canvas.Settings.CurrentGame == "" || CurrentGameFile == null)
				{
					CurrentGameFile = Game;
					Canvas.Settings.CurrentGame = CurrentGameFile.Path;
					MenuItem.Checked = true;
				}

				Program.canvasForm.SetGameMenuItem.DropDownItems.Add(MenuItem);
			}

			// Fill the tree view in ItemsWindow
			foreach (var Node in RPGNodes)
			{
				AddAndRecurseNodes(Program.itemsWindowForm.TreeView1.Nodes, Node);
			}
		}

		// Call this when you want to refresh the image list.
		static public void ReloadImages()
		{
			string Backup = CurrentGameFile.Path;

			// Back up all image data in the layers
			// FIXME: do all characters
			foreach (RPGCharacterRowNode Row in Program.characterSelectForm.TreeView1.Nodes)
			{
				foreach (RPGCharacterNode Col in Row.Nodes)
				{
					foreach (var Layer in Col.Character.Layers)
					{
						if (Layer.Image != null)
						{
							RPGImage Img = Layer.Image;

							Layer.Image = new RPGImage();
							Layer.Image.Path = Img.Path;
						}
						Layer.ReloadImagesSubLayers(false);
					}
				}
			}

			// Clear nodes
			ClearImages();

			// Clear games
			Program.canvasForm.SetGameMenuItem.DropDownItems.Clear();
			RPGGames.Clear();

			LoadImages();

			// Reset game
			CurrentGameFile = FindGameByPath(Backup);

			// Fix layer data
			// FIXME2: above
			foreach (RPGCharacterRowNode Row in Program.characterSelectForm.TreeView1.Nodes)
			{
				foreach (RPGCharacterNode Col in Row.Nodes)
				{
					foreach (var Layer in Col.Character.Layers)
					{
						if (Layer.Image != null)
						{
							Layer.Image = FindImage(Layer.Image.Path);
							Layer.UpdateFlippedImage();
						}
						Layer.ReloadImagesSubLayers(true);
					}
				}
			}

			// Re-draw incase we changed
			Program.canvasForm.UpdateDrawing();
		}
	}

	public static class Extensions
	{
		public static void CopyTo(this Stream from, Stream to, int bufferSize = 4096)
		{
			long fromPos = 0, toPos = 0;

			if (from.CanSeek)
				fromPos = from.Position;
			if (to.CanSeek)
				toPos = to.Position;

			byte[] buffer = new byte[bufferSize];
			while (true)
			{
				int bytes = from.Read(buffer, 0, bufferSize);

				if (bytes == 0)
					break;

				to.Write(buffer, 0, bytes);
			}

			if (from.CanSeek)
				from.Position = fromPos;
			if (to.CanSeek)
				to.Position = toPos;
		}
		/// <summary>
		/// Converts an image into an icon.
		/// </summary>
		/// <param name="img">The image that shall become an icon</param>
		/// <param name="size">The width and height of the icon. Standard
		/// sizes are 16x16, 32x32, 48x48, 64x64.</param>
		/// <param name="keepAspectRatio">Whether the image should be squashed into a
		/// square or whether whitespace should be put around it.</param>
		/// <returns>An icon!!</returns>
		public static Icon ToIcon(this Image img, int size, bool keepAspectRatio)
		{
			Bitmap square = new Bitmap(size, size); // create new bitmap
			Graphics g = Graphics.FromImage(square); // allow drawing to it

			int x, y, w, h; // dimensions for new image

			if (!keepAspectRatio || img.Height == img.Width)
			{
				// just fill the square
				x = y = 0; // set x and y to 0
				w = h = size; // set width and height to size
			}
			else
			{
				// work out the aspect ratio
				float r = (float)img.Width / (float)img.Height;

				// set dimensions accordingly to fit inside size^2 square
				if (r > 1)
				{ // w is bigger, so divide h by r
					w = size;
					h = (int)((float)size / r);
					x = 0; y = (size - h) / 2; // center the image
				}
				else
				{ // h is bigger, so multiply w by r
					w = (int)((float)size * r);
					h = size;
					y = 0; x = (size - w) / 2; // center the image
				}
			}

			// make the image shrink nicely by using HighQualityBicubic mode
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			g.DrawImage(img, x, y, w, h); // draw image with specified dimensions
			g.Flush(); // make sure all drawing operations complete before we get the icon

			// following line would work directly on any image, but then
			// it wouldn't look as nice.
			return Icon.FromHandle(square.GetHicon());
		}
	}
}
