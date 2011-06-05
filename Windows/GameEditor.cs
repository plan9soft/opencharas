using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using System.Runtime.InteropServices;

namespace OpenCharas
{
	public partial class GameEditor
	{
		public GameEditor()
		{
			InitializeComponent();
		}

		private RPGGameFile GameFile;
		private int OldNumFrames = 0;
		private string SavedPath = "";
		private Bitmap ReferenceImage;

		public void UpdateFrameBox()
		{
			int WantedValue = (int)(NumericUpDown1.Value * NumericUpDown2.Value);

			ListBox1.Items.Clear();
			for (int i = 1; i <= WantedValue; i++)
				ListBox1.Items.Add(i);

			if (WantedValue < OldNumFrames)
			{
				foreach (var Anim in GameFile.Animations)
				{
					for (int i = WantedValue + 1; i <= OldNumFrames; i++)
					{

						if (GameFile.Animations.IndexOf(Anim) == NumericUpDown4.Value - 1)
						{
							while (true)
							{
								int index = ListBox2.Items.IndexOf(i);

								if (index == -1)
									break;

								ListBox2.Items.RemoveAt(index);
							}
						}
						while (true)
						{
							int index = Anim.Frames.IndexOf(i);

							if (index == -1)
								break;

							Anim.Frames.RemoveAt(index);
						}
					}
				}
			}
			OldNumFrames = WantedValue;
		}

		private bool DontUpdateList = false;
		public void UpdateFramesList()
		{
			if (GameFile == null)
				return;
			if (DontUpdateList)
				return;

			GameFile.SheetRows = (int)(NumericUpDown1.Value);
			GameFile.SheetColumns = (int)(NumericUpDown2.Value);
			NumericUpDown5.Maximum = (decimal)((GameFile.SheetRows * GameFile.SheetColumns) - 1);
	
			if (NumericUpDown5.Value > NumericUpDown5.Maximum)
				NumericUpDown5.Value = NumericUpDown5.Maximum;

			UpdateFrameBox();
		}

		public void NumericUpDown1_ValueChanged(System.Object sender, System.EventArgs e)
		{
			UpdateFramesList();
		}

		public void NumericUpDown2_ValueChanged(System.Object sender, System.EventArgs e)
		{
			UpdateFramesList();
		}

		private void UpdateAnimations()
		{
			NumericUpDown4.Maximum = NumericUpDown3.Value;
			if (NumericUpDown4.Value > NumericUpDown3.Value)
				NumericUpDown4.Value = NumericUpDown3.Value;

			int WantedValue = (int)(NumericUpDown3.Value);

			if (GameFile.Animations.Count == WantedValue - 1)
				GameFile.Animations.Add(new RPGGameFileAnimation());
			else if (GameFile.Animations.Count == WantedValue + 1)
				GameFile.Animations.RemoveAt(WantedValue);
			else
			{
				if (GameFile.Animations.Count < WantedValue)
				{
					for (int i = GameFile.Animations.Count + 1; i <= WantedValue; i++)
						GameFile.Animations.Add(new RPGGameFileAnimation());
				}
				else if (GameFile.Animations.Count > WantedValue)
				{
					int i = (int)(GameFile.Animations.Count - WantedValue);
					while (i > 0)
					{
						i--;
						GameFile.Animations.RemoveAt(WantedValue);
					}
				}
			}
		}

		public void NumericUpDown3_ValueChanged(System.Object sender, System.EventArgs e)
		{
			UpdateAnimations();
		}

		public void GrayOutAnimStuff()
		{
			ListBox2.Enabled = false;
			ListBox2.Items.Clear();
			Button1.Enabled = false;
			Button2.Enabled = false;
		}

		public void EnableAnimStuff()
		{
			ListBox2.Enabled = true;
			Button1.Enabled = true;
			Button2.Enabled = true;

			foreach (var Frame in GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames)
				ListBox2.Items.Add(Frame);
		}

		public void NumericUpDown4_ValueChanged(System.Object sender, System.EventArgs e)
		{
			ListBox2.Items.Clear();

			if (NumericUpDown4.Value == 0)
			{
				GrayOutAnimStuff();
				return;
			}

			EnableAnimStuff();
		}

		public void Form6_Load(System.Object sender, System.EventArgs e)
		{
			// TODO: load current game, if applicable
			SavedPath = "";
			GameFile = new RPGGameFile();
		}

		public void TextBox1_TextChanged(System.Object sender, System.EventArgs e)
		{
			GameFile.GameName = TextBox1.Text;
		}

		public void ListBox2_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			if (ListBox2.SelectedIndex == -1)
			{
				MoveUpToolStripMenuItem.Enabled = false;
				MoveDownToolStripMenuItem.Enabled = false;
				return;
			}

			MoveUpToolStripMenuItem.Enabled = true;
			MoveDownToolStripMenuItem.Enabled = true;
		}

		public void Button1_Click(System.Object sender, System.EventArgs e)
		{
			if (ListBox1.SelectedIndex == -1)
				return;

			ListBox2.Items.Add(ListBox1.SelectedItem);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.Add((int)(ListBox1.SelectedItem));

			ListBox1.Focus();
		}

		public void Button2_Click(System.Object sender, System.EventArgs e)
		{
			if (ListBox2.SelectedIndex == -1)
				return;

			int Indx = ListBox2.SelectedIndex;
			ListBox2.Items.RemoveAt(ListBox2.SelectedIndex);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.RemoveAt(Indx);
	
			if (ListBox2.Items.Count > 0)
			{
				if ((Indx - 1) == -1)
					Indx = 1;
		
				ListBox2.SelectedIndex = Indx - 1;
			}

			ListBox1.Focus();
		}

		public void MoveUpToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			if (ListBox2.SelectedIndex == -1 || ListBox2.SelectedIndex == 0)
				return;

			int index = ListBox2.SelectedIndex;
			int val = (int)(ListBox2.Items[index]);
			ListBox2.Items.RemoveAt(index);
			ListBox2.Items.Insert(index - 1, val);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.RemoveAt(index);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.Insert(index - 1, val);
			ListBox2.SelectedIndex = index - 1;
		}

		public void MoveDownToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			if (ListBox2.SelectedIndex == -1 || ListBox2.SelectedIndex == ListBox2.Items.Count - 1)
				return;

			int index = ListBox2.SelectedIndex;
			int val = (int)(ListBox2.Items[index]);
			ListBox2.Items.RemoveAt(index);
			ListBox2.Items.Insert(index + 1, val);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.RemoveAt(index);
			GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.Insert(index + 1, val);
			ListBox2.SelectedIndex = index + 1;
		}

		public void DoASaveAs()
		{
			using (SaveFileDialog SaveDlg = new SaveFileDialog())
			{
				SaveDlg.AddExtension = true;
				SaveDlg.Filter = "GAM Files (.gam)|*.gam|All Files|*";
				SaveDlg.DefaultExt = "gam";
				SaveDlg.RestoreDirectory = true;
				var Result = SaveDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					SavedPath = SaveDlg.FileName;
					using (var outFile = System.IO.File.Open(SavedPath, System.IO.FileMode.Create))
					{
						using (System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(outFile))
						{
							if (Writer != null)
								GameFile.Save(Writer);
						}
					}
				}
			}
		}

		public void ToolStripMenuItem1_Click(System.Object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(GameFile.GameName))
			{
				MessageBox.Show("You might want to give this game mode a name!");
				return;
			}

			if (string.IsNullOrEmpty(GameFile.FolderName))
			{
				MessageBox.Show("Give the game a data folder name!");
				return;
			}

			System.IO.Directory.CreateDirectory("data\\"+GameFile.FolderName);

			using (var outfile = System.IO.File.Open("data\\"+GameFile.FolderName+"\\game.gam", System.IO.FileMode.Create))
			{
				using (System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(outfile))
				{
					if (Writer != null)
						GameFile.Save(Writer);
				}
			}
		}

		public void ToolStripMenuItem3_Click(System.Object sender, System.EventArgs e)
		{
			DoASaveAs();
		}

		public void CreateNew()
		{
			GameFile.Clear();
			TextBox1.Clear();
			NumericUpDown1.Value = 1;
			NumericUpDown2.Value = 1;
			NumericUpDown3.Value = 0;
			NumericUpDown4.Value = 0;
			ListBox1.Items.Clear();
			GrayOutAnimStuff();
			ReferenceImage = null;
			CheckBox1.Checked = false;
			NumericUpDown5.Value = 0;
			AnimMode = false;
			FrameIndex = 0;

			UpdateFramesList();
		}

		public void ToolStripMenuItem2_Click(System.Object sender, System.EventArgs e)
		{
			using (OpenFileDialog OpenDlg = new OpenFileDialog())
			{
				OpenDlg.AddExtension = true;
				OpenDlg.Filter = "GAM Files (.gam)|*.gam|All Files|*";
				OpenDlg.DefaultExt = "gam";
				OpenDlg.FilterIndex = 1;
				OpenDlg.RestoreDirectory = true;
				OpenDlg.InitialDirectory = Environment.CurrentDirectory + "\\data\\";
				var Result = OpenDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					string Path = OpenDlg.FileName;
					using (var inFile = System.IO.File.Open(Path, System.IO.FileMode.Open))
					{
						using (System.IO.BinaryReader Reader = new System.IO.BinaryReader(inFile))
						{
							if (Reader != null)
							{
								CreateNew();
								GameFile.Load(Reader);
								Path = Path.Replace('/', '\\');
								string temp = Path.Substring(0, Path.LastIndexOf('\\'));
								GameFile.FolderName = temp.Substring(temp.LastIndexOf('\\')+1);
								textBox2.Text = GameFile.FolderName;

								TextBox1.Text = GameFile.GameName;
								DontUpdateList = true;
								NumericUpDown1.Value = GameFile.SheetRows;
								NumericUpDown2.Value = GameFile.SheetColumns;
								DontUpdateList = false;
								NumericUpDown3.Value = (decimal)(GameFile.Animations.Count);
								NumericUpDown5.Maximum = (decimal)((GameFile.SheetRows * GameFile.SheetColumns) - 1);
								CheckBox1.Checked = GameFile.Is8Bit;
								NumericUpDown5.Value = GameFile.PreviewFrame;

								UpdateFramesList();
							}
						}
					}
				}
			}
		}

		public void ToolStripMenuItem5_Click(System.Object sender, System.EventArgs e)
		{
			CreateNew();
		}

		public void ToolStripMenuItem4_Click(System.Object sender, System.EventArgs e)
		{
			using (OpenFileDialog OpenDlg = new OpenFileDialog())
			{
				OpenDlg.AddExtension = true;
				OpenDlg.Filter = "Image Files (.png;.jpg;.bmp)|*.png;*.jpg;*.bmp|All Files|*";
				OpenDlg.DefaultExt = "png";
				OpenDlg.FilterIndex = 1;
				OpenDlg.RestoreDirectory = true;
				var Result = OpenDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					string Path = OpenDlg.FileName;

					using (Bitmap TmpImage = new Bitmap(Path))
					{
						ReferenceImage = new Bitmap(TmpImage.Width, TmpImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						using (Graphics Gfx = Graphics.FromImage(ReferenceImage))
							Gfx.DrawImage(TmpImage, new Rectangle(0, 0, TmpImage.Width, TmpImage.Height));
					}

					Button3.Enabled = true;
					Button5.Enabled = true;
				}
			}

		}

		public void CloseEditorToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}

		PreviewWindow previewForm;

		private int FrameIndex = 0;
		public void UpdateMyPictureBox()
		{
			if (ReferenceImage == null)
				return;

			Bitmap MyBMP;

			if (AnimMode == false)
			{
				MyBMP = new Bitmap(ReferenceImage.Width, ReferenceImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				using (var Gfx = Graphics.FromImage(MyBMP))
				{
					Gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					Gfx.DrawImage(ReferenceImage, new Rectangle(0, 0, ReferenceImage.Width, ReferenceImage.Height));

					// Draw lines
					int WidthMagic = ReferenceImage.Width / GameFile.SheetRows;
					int HeightMagic = ReferenceImage.Height / GameFile.SheetColumns;

					int Val = 0;
					for (int i = 0; i <= GameFile.SheetRows; i++)
					{
						Gfx.DrawLine(Pens.Black, new Point(Val, 0), new Point(Val, ReferenceImage.Height));
						Val += WidthMagic;
					}

					Val = 0;
					for (int i = 0; i <= GameFile.SheetRows; i++)
					{
						Gfx.DrawLine(Pens.Black, new Point(0, Val), new Point(ReferenceImage.Width, Val));
						Val += HeightMagic;
					}
				}
			}
			else
			{
				MyBMP = new Bitmap(ReferenceImage.Width / GameFile.SheetRows, ReferenceImage.Height / GameFile.SheetColumns, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				using (var Gfx = Graphics.FromImage(MyBMP))
				{
					Gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					var destRect = new Rectangle(0, 0, ReferenceImage.Width / GameFile.SheetRows, ReferenceImage.Height / GameFile.SheetColumns);
					int Frame;

					if (NumericUpDown4.Value == 0)
						Frame = GameFile.PreviewFrame;
					else
						Frame = (int)(GameFile.Animations[(int)(NumericUpDown4.Value - 1)].GetFrame(FrameIndex));

					var srcRect = Character.GetRectangleForBitmapFrame(ReferenceImage, Frame, GameFile.SheetRows, GameFile.SheetColumns);
					Gfx.DrawImage(ReferenceImage, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel);
				}
			}

			previewForm.PictureBox1.Image = MyBMP;
		}

		public static Size Diff = new Size(88, 162) - new Size(72, 128);
		Timer TheTimer = new Timer();
		public void TickTimer(object sender, EventArgs e)
		{
			if (previewForm != null && previewForm.Visible == false)
			{
				TheTimer.Stop();
				return;
			}

			if (NumericUpDown4.Value == 0)
			{
				FrameIndex = GameFile.PreviewFrame;
				return;
			}

			if (GameFile.Animations.Count == 0)
			{
				FrameIndex = 0;
				return;
			}

			if ((GameFile.Animations.Count - 1 < NumericUpDown4.Value - 1) || GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.Count == 0)
			{
				FrameIndex = 0;
				return;
			}

			FrameIndex++;

			if (FrameIndex > GameFile.Animations[(int)(NumericUpDown4.Value - 1)].Frames.Count - 1)
				FrameIndex = 0;

			UpdateMyPictureBox();
		}

		public void Button3_Click(System.Object sender, System.EventArgs e)
		{
			if (GameFile.SheetRows == 0 && GameFile.SheetColumns == 0)
				return;

			if (previewForm != null && previewForm.Visible)
				previewForm.Close();

			previewForm = new PreviewWindow();
			previewForm.Show(this);

			previewForm.StartPosition = FormStartPosition.Manual;
			previewForm.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);
			previewForm.Size = new Size(ReferenceImage.Width + Diff.Width, ReferenceImage.Height + Diff.Height);
			previewForm.FormClosed += new FormClosedEventHandler(previewForm_FormClosed);
			TheTimer.Interval = 350;
			TheTimer.Start();
			TheTimer.Tick += new System.EventHandler(TickTimer);

			UpdateMyPictureBox();
		}

		void previewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			previewForm = null;
		}

		public bool AnimMode { get; set; }

		public void Button5_Click(System.Object sender, System.EventArgs e)
		{
			AnimMode = !AnimMode;

			if (AnimMode)
				previewForm.Size = new Size((ReferenceImage.Width / GameFile.SheetRows) + Diff.Width, (ReferenceImage.Height / GameFile.SheetColumns) + Diff.Height);
			else
				previewForm.Size = new Size(ReferenceImage.Width + Diff.Width, ReferenceImage.Height + Diff.Height);
		
			UpdateMyPictureBox();
		}

		public void CheckBox1_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			GameFile.Is8Bit = CheckBox1.Checked;
		}

		public void Form6_FormClosed(System.Object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			TheTimer.Stop();
			CreateNew();

			GrayOutAnimStuff();
		}

		public void NumericUpDown5_ValueChanged(System.Object sender, System.EventArgs e)
		{
			GameFile.PreviewFrame = (int)(NumericUpDown5.Value);

			if (ReferenceImage != null)
				UpdateMyPictureBox();
		}

		public void GameEditor_Shown(System.Object sender, System.EventArgs e)
		{
			UpdateFramesList();
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			GameFile.FolderName = textBox2.Text;
		}
	}

	public class RPGGameFileAnimation
	{
		public List<int> Frames { get; set; }

		public RPGGameFileAnimation()
		{
			Frames = new List<int>();
		}

		public void Save(System.IO.BinaryWriter Writer)
		{
			Writer.Write(Frames.Count);

			foreach (var Frame in Frames)
				Writer.Write(Frame);
		}

		public void Load(System.IO.BinaryReader Reader)
		{
			int NumFrames = Reader.ReadInt32();

			for (int i = 0; i <= NumFrames - 1; i++)
				Frames.Add(Reader.ReadInt32());
		}

		public int GetFrame(int Index)
		{
			return Frames[Index] - 1;
		}
	}

	public class RPGGameFile
	{
		public string Path { get; set; }
		public string FilePath { get; set; }
		public string GameName { get; set; }
		public string FolderName { get; set; }
		public int SheetRows { get; set; }
		public int SheetColumns { get; set; }
		public bool Is8Bit { get; set; }
		public int PreviewFrame { get; set; }
		public List<RPGGameFileAnimation> Animations { get; set; }
		public GameModeMenuItem MenuItem { get; set; }

		public RPGGameFile()
		{
			SheetRows = 1;
			SheetColumns = 1;
			Animations = new List<RPGGameFileAnimation>();
		}

		public void Clear()
		{
			GameName = FolderName = "";
			SheetRows = 1;
			SheetColumns = 1;
			Is8Bit = false;
			Animations.Clear();
			PreviewFrame = 0;
		}

		public void Save(System.IO.BinaryWriter Writer)
		{
			Writer.Write(GameName);
			Writer.Write(SheetRows);
			Writer.Write(SheetColumns);
			Writer.Write(Is8Bit);
			Writer.Write(PreviewFrame);
			Writer.Write(Animations.Count);

			foreach (var Anim in Animations)
				Anim.Save(Writer);
		}

		public void Load(System.IO.BinaryReader Reader)
		{
			GameName = Reader.ReadString();
			SheetRows = Reader.ReadInt32();
			SheetColumns = Reader.ReadInt32();
			Is8Bit = Reader.ReadBoolean();
			PreviewFrame = Reader.ReadInt32();
			int AnimCount = Reader.ReadInt32();

			for (int i = 0; i <= AnimCount - 1; i++)
			{
				RPGGameFileAnimation NewAnim = new RPGGameFileAnimation();
				NewAnim.Load(Reader);
				Animations.Add(NewAnim);
			}
		}
	}
}
