using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class SetSheetCreator
	{
		public SetSheetCreator()
		{
			InitializeComponent();
		}

		private int SetWidth;
		private int SetHeight;
		private int SheetRows;
		private int SheetColumns;
		private int SheetWidth;
		private int SheetHeight;

		private Bitmap[] SetImages;

		private void CreateNew()
		{
			using (SetValues sv = new SetValues())
			{
				if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					CurrentSavePos = "";
					SetWidth = (int)(sv.NumericUpDown1.Value);
					SetHeight = (int)(sv.NumericUpDown2.Value);
					SheetRows = (int)(sv.NumericUpDown3.Value);
					SheetColumns = (int)(sv.NumericUpDown4.Value);

					SheetWidth = SetWidth * SheetRows;
					SheetHeight = SetHeight * SheetColumns;

					TreeView1.Nodes.Clear();

					for (int i = 1; i <= (SheetRows * SheetColumns); i++)
						TreeView1.Nodes.Add((string)("Set " + i.ToString()));

					SetImages = new Bitmap[SheetRows * SheetColumns + 1];
				}
			}

			UpdateSet();
		}

		public void Form8_Shown(System.Object sender, System.EventArgs e)
		{
			CreateNew();
		}

		private void UpdateSet()
		{
			Bitmap StoredImage = null;

			if (SplitContainer1.Panel1.Width != 0 && SplitContainer1.Panel1.Height != 0)
			{
				StoredImage = new Bitmap(SplitContainer1.Panel1.Width, SplitContainer1.Panel1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				
				using (Graphics TempGfx = Graphics.FromImage(StoredImage))
				{
					int ContentWidth = SplitContainer1.Panel1.Width;
					int ContentHeight = SplitContainer1.Panel1.Height;
					TempGfx.DrawLine(Pens.Black, new PointF(ContentWidth / 2, 0), new PointF(ContentWidth / 2, ContentHeight));
					TempGfx.DrawLine(Pens.Black, new PointF(0, ContentHeight / 2), new PointF(ContentWidth, ContentHeight / 2));

					TempGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

					if (SheetWidth != 0 && SheetHeight != 0)
					{
						using (Bitmap EntireSheet = new Bitmap(SheetWidth + 1, SheetHeight + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
						{
							int TheY = 0;
							using (var SheetGfx = Graphics.FromImage(EntireSheet))
							{
								int What = 0;
								for (int y = 0; y <= SheetColumns - 1; y++)
								{
									int TheX = 0;

									for (int x = 0; x <= SheetRows - 1; x++)
									{
										if (SetImages[What] != null)
											SheetGfx.DrawImage(SetImages[What], new Rectangle(TheX, TheY, SetWidth, SetHeight));

										What++;
										TheX += SetWidth;
									}

									TheY += SetHeight;
								}
							}

							float ZoomVal = ZoomNumber / 100;

							// Draw rectangles separately
							TheY = (int)((ContentHeight / 2) - (SheetHeight * ZoomVal) / 2);
							for (int y = 0; y <= SheetColumns - 1; y++)
							{
								int TheX = (int)((ContentWidth / 2) - (SheetWidth * ZoomVal) / 2);

								for (int x = 0; x <= SheetRows - 1; x++)
								{
									TempGfx.DrawRectangle(Pens.Black, new Rectangle(TheX, TheY, (int)(SetWidth * ZoomVal), (int)(SetHeight * ZoomVal)));
									TheX += (int)(SetWidth * ZoomVal);
								}

								TheY += (int)(SetHeight * ZoomVal);
							}
							TempGfx.DrawImage(EntireSheet, new Rectangle((int)((ContentWidth / 2) - (SheetWidth * ZoomVal) / 2), (int)((ContentHeight / 2) - (SheetHeight * ZoomVal) / 2), (int)(SheetWidth * ZoomVal), (int)(SheetHeight * ZoomVal)));
						}
					}
				}
			}

			PictureBox1.Image = StoredImage;
		}

		public void Form8_SizeChanged(System.Object sender, System.EventArgs e)
		{
			UpdateSet();
		}

		public void DoImportImageToSet()
		{
			if (TreeView1.SelectedNode == null)
			{
				return;
			}

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
					using (var TmpImage = Images.Convert8BPPTo32BPP(new Bitmap(Path)))
					{
						Bitmap ReferenceImage = new Bitmap(TmpImage.Width, TmpImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						using (var Gfx = Graphics.FromImage(ReferenceImage))
							Gfx.DrawImage(TmpImage, new Rectangle(0, 0, TmpImage.Width, TmpImage.Height));

						using (FastPixel ImgFP = new FastPixel(ReferenceImage, true))
						{
							using (FastPixel TempMapFP = new FastPixel(TmpImage, false))
							{
								Color TransColor = TempMapFP.GetPixel(0, 0);

								for (int i = 0; i <= TmpImage.Width - 1; i++)
								{
									for (int z = 0; z <= TmpImage.Height - 1; z++)
									{
										if (TempMapFP.GetPixel(i, z) == TransColor)
											ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0));
										else
											ImgFP.SetPixel(i, z, TempMapFP.GetPixel(i, z));
									}
								}
							}
						}

						SetImages[TreeView1.SelectedNode.Index] = ReferenceImage;
					}

					UpdateSet();
				}
			}
		}

		public void ToolStripMenuItem1_Click(System.Object sender, System.EventArgs e)
		{
			DoImportImageToSet();
		}

		private string CurrentSavePos = "";
		private bool CurrentSaveIs8Bit = false;

		static System.Random RenderToTexture_RandomNumGen = new System.Random();

		public Bitmap RenderToTexture(bool ResetTrans)
		{
			Bitmap RenderedImage;

			RenderedImage = new Bitmap(SheetWidth, SheetHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			using (Graphics TempGfx = Graphics.FromImage(RenderedImage))
			{
				TempGfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

				// Go through each layer drawing the sheet
				int What = 0;
				int TheY = 0;
				for (int y = 0; y <= SheetColumns - 1; y++)
				{
					int TheX = 0;

					for (int x = 0; x <= SheetRows - 1; x++)
					{
						if (SetImages[What] != null)
							TempGfx.DrawImage(SetImages[What], new Rectangle(TheX, TheY, SetWidth, SetHeight));

						What++;
						TheX += SetWidth;
					}

					TheY += SetHeight;
				}
			}

			if (ResetTrans == false)
			{
				var ColorList = Canvas.NumColorsInImage(RenderedImage);
				Color EndedUpColor = Canvas.SpecialColor;

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

		public Bitmap RenderToTexture()
		{
			return RenderToTexture(false);
		}

		public void DoSave()
		{
			Bitmap Image = RenderToTexture(CurrentSaveIs8Bit == false);

			if (CurrentSaveIs8Bit)
			{
				var Colors = Canvas.NumColorsInImage(Image);

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

						using (FastPixel fp = new FastPixel(Image, true))
						{
							for (var y = 0; y <= bmd.Height - 1; y++)
							{
								for (var x = 0; x <= bmd.Width - 1; x++)
									System.Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (int)((bmd.Stride * y) + (x)), (byte)(Colors.IndexOf(fp.GetPixel((int)(x), y))));
							}
						}

						Indexed.UnlockBits(bmd);
						Image = (Bitmap)(Indexed.Clone());
					}
				}
				else
					MessageBox.Show("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.");
			}

			int ExtLoc = CurrentSavePos.LastIndexOf(".");
			if (ExtLoc == -1)
			{
				CurrentSavePos = CurrentSavePos.Insert(CurrentSavePos.Length, ".png");
				Image.Save(CurrentSavePos, System.Drawing.Imaging.ImageFormat.Png);
			}
			else
			{
				string Loc = CurrentSavePos.Substring(ExtLoc + 1);
				if (string.Compare(Loc, "bmp", true) == 0)
					Image.Save(CurrentSavePos, System.Drawing.Imaging.ImageFormat.Bmp);
				else if (string.Compare(Loc, "png", true) == 0)
					Image.Save(CurrentSavePos, System.Drawing.Imaging.ImageFormat.Png);
				else if (string.Compare(Loc, "jpg", true) == 0)
					Image.Save(CurrentSavePos, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			Image.Dispose();
		}

		public void DoSaveAs()
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
					CurrentSavePos = SaveDlg.FileName;
					CurrentSaveIs8Bit = SaveDlg.FilterIndex == 1;
					DoSave();
				}
			}
		}

		public void SaveAsToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoSaveAs();
		}

		public void SaveToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(CurrentSavePos))
				DoSaveAs();
			else
				DoSave();
		}

		public void ExitToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}

		public void ImportImageToThisSetToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoImportImageToSet();
		}

		public void TreeView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				TreeView1.SelectedNode = TreeView1.GetNodeAt(e.X, e.Y);
		}

		public void ClearCurrentSet()
		{
			if (TreeView1.SelectedNode == null)
				return;

			SetImages[TreeView1.SelectedNode.Index] = null;
			UpdateSet();
		}

		public void ClearImageToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			ClearCurrentSet();
		}

		public void ToolStripMenuItem2_Click(System.Object sender, System.EventArgs e)
		{
			ClearCurrentSet();
		}

		private float ZoomNumber = 100;

		private void SetZoom(float Num)
		{
			if (Num < 0)
				Num = 0;
			if (Num > 1000)
				Num = 1000;

			ToolStripTextBox1.Text = (string)(Num.ToString() + "%");
			ZoomNumber = Num;
		}

		public void ZoomByNumber(float Addition)
		{
			SetZoom(ZoomNumber + Addition);
			UpdateSet();
		}

		const int ZoomInAmount = 25;

		public void ToolStripButton6_Click(System.Object sender, System.EventArgs e)
		{
			ZoomByNumber((float)(ZoomInAmount));
		}

		public void ToolStripButton7_Click(System.Object sender, System.EventArgs e)
		{
			ZoomByNumber((float)(-ZoomInAmount));
		}

		public void SetSheetCreator_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
				ZoomByNumber((float)(ZoomInAmount));
			else if (e.Delta < 0)
				ZoomByNumber((float)(-ZoomInAmount));
		}

		public void TreeView1_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
				ZoomByNumber((float)(ZoomInAmount));
			else if (e.Delta < 0)
				ZoomByNumber((float)(-ZoomInAmount));
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
					return;

				SetZoom(EndValue);
			}
		}

		public void ToolStripButton9_Click(System.Object sender, System.EventArgs e)
		{
			CreateNew();
		}

		public void NewToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			CreateNew();
		}

		static System.Random ExportSetToImage_RandomNumGen = new System.Random();
		public void ExportSetToImage(string Path, bool Indexed)
		{
			Bitmap Image = new Bitmap(SetImages[TreeView1.SelectedNode.Index]);

			if (Indexed)
			{
				var ColorList = Canvas.NumColorsInImage(Image);
				Color EndedUpColor = Canvas.SpecialColor;

				while (ColorList.IndexOf(EndedUpColor) != -1)
					EndedUpColor = Color.FromArgb(ExportSetToImage_RandomNumGen.Next(256), ExportSetToImage_RandomNumGen.Next(256), ExportSetToImage_RandomNumGen.Next(256));

				using (FastPixel fp = new FastPixel(Image, true))
				{
					for (int i = 0; i <= Image.Width - 1; i++)
					{
						for (int z = 0; z <= Image.Height - 1; z++)
						{
							if (fp.GetPixel(i, z).A == 0)
								fp.SetPixel(i, z, EndedUpColor);
						}
					}
				}

			}

			if (CurrentSaveIs8Bit)
			{
				var Colors = Canvas.NumColorsInImage(Image);

				if (Colors.Count <= 256)
				{
					using (Bitmap IndexedImage = new Bitmap(Image.Width, Image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
					{
						// Create a palette
						System.Drawing.Imaging.ColorPalette Pal = IndexedImage.Palette;
						for (int i = 0; i <= 255; i++)
						{
							if (i > Colors.Count - 1)
								Pal.Entries[i] = Color.White;
							else
								Pal.Entries[i] = Colors[i];
						}

						IndexedImage.Palette = Pal;

						var bmd = IndexedImage.LockBits(
							new Rectangle(0, 0, Image.Width, Image.Height),
							System.Drawing.Imaging.ImageLockMode.ReadOnly,
							IndexedImage.PixelFormat);

						using (FastPixel fp = new FastPixel(Image, true))
						{
							for (var y = 0; y <= bmd.Height - 1; y++)
							{
								for (var x = 0; x <= bmd.Width - 1; x++)
									System.Runtime.InteropServices.Marshal.WriteByte(bmd.Scan0, (int)((bmd.Stride * y) + (x)), (byte)(Colors.IndexOf(fp.GetPixel((int)(x), y))));
							}
						}

						IndexedImage.UnlockBits(bmd);
						Image = (Bitmap)(IndexedImage.Clone());
					}
				}
				else
					MessageBox.Show("Warning: Your image contains more than 256 colors! All this means is this program will not perform the conversion for you. You must manually convert this image to 8 bits per pixel if you wish to use this image in RPG Maker 2000/3.");
			}

			int ExtLoc = Path.LastIndexOf(".");
			if (ExtLoc == -1)
			{
				Path = Path.Insert(Path.Length, ".png");
				Image.Save(Path, System.Drawing.Imaging.ImageFormat.Png);
			}
			else
			{
				string Loc = Path.Substring(ExtLoc + 1);
				if (string.Compare(Loc, "bmp", true) == 0)
					Image.Save(Path, System.Drawing.Imaging.ImageFormat.Bmp);
				else if (string.Compare(Loc, "png", true) == 0)
					Image.Save(Path, System.Drawing.Imaging.ImageFormat.Png);
				else if (string.Compare(Loc, "jpg", true) == 0)
					Image.Save(Path, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			Image.Dispose();
		}

		public void DoExportSet()
		{
			if (TreeView1.SelectedNode == null)
				return;
			if (SetImages[TreeView1.SelectedNode.Index] == null)
				return;

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
					ExportSetToImage(SaveDlg.FileName, SaveDlg.FilterIndex == 1);
			}
		}

		public void ExportSetToImageToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoExportSet();
		}

		public void DoOpen()
		{
			if (MessageBox.Show("Opening an existing sheet will erase this unsaved sheet. Do you wish to save first?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				DoSaveAs();
				CurrentSavePos = "";
			}

			using (OpenFileDialog OpenDlg = new OpenFileDialog())
			{
				OpenDlg.AddExtension = true;
				OpenDlg.Filter = "Image Files (.png;.jpg;.bmp)|*.png;*.jpg;*.bmp|All Files|*";
				OpenDlg.DefaultExt = "png";
				OpenDlg.RestoreDirectory = true;
				var Result = OpenDlg.ShowDialog();

				if (Result == System.Windows.Forms.DialogResult.OK)
				{
					Bitmap Bmp = Images.Convert8BPPTo32BPP(new Bitmap(OpenDlg.FileName));

					using (FastPixel ImgFP = new FastPixel(Bmp, true))
					{
						Color TransColor = ImgFP.GetPixel(0, 0);

						for (int i = 0; i <= Bmp.Width - 1; i++)
						{
							for (int z = 0; z <= Bmp.Height - 1; z++)
							{
								if (ImgFP.GetPixel(i, z) == TransColor)
									ImgFP.SetPixel(i, z, Color.FromArgb(0, 0, 0, 0));
							}
						}
					}

					using (SetValues sv = new SetValues())
					{
						sv.NumericUpDown1.Enabled = false;
						sv.NumericUpDown2.Enabled = false;
						
						if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							CurrentSavePos = "";
							SheetRows = (int)(sv.NumericUpDown3.Value);
							SheetColumns = (int)(sv.NumericUpDown4.Value);
							SetWidth = Bmp.Width / SheetRows;
							SetHeight = Bmp.Height / SheetColumns;

							SheetWidth = Bmp.Width;
							SheetHeight = Bmp.Height;

							TreeView1.Nodes.Clear();

							for (int i = 1; i <= (SheetRows * SheetColumns); i++)
								TreeView1.Nodes.Add((string)("Set " + i.ToString()));

							SetImages = new Bitmap[SheetRows * SheetColumns + 1];

							int What = 0;
							int TheY = 0;
							for (int y = 0; y <= SheetColumns - 1; y++)
							{
								int TheX = 0;

								for (int x = 0; x <= SheetRows - 1; x++)
								{
									Bitmap NewBmpSheet = new Bitmap(SetWidth, SetHeight);
									
									using (var GfxTmp = Graphics.FromImage(NewBmpSheet))
										GfxTmp.DrawImage(Bmp, new Rectangle(0, 0, SetWidth, SetHeight), new Rectangle(TheX, TheY, SetWidth, SetHeight), GraphicsUnit.Pixel);
								
									SetImages[What] = NewBmpSheet;
									What++;
									TheX += SetWidth;
								}

								TheY += SetHeight;
							}
						}

						UpdateSet();
					}
				}
			}
		}

		public void OpenToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			DoOpen();
		}

		public void ToolStripMenuItem3_Click(System.Object sender, System.EventArgs e)
		{
			DoExportSet();
		}

		public void ToolStripButton18_Click(System.Object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(CurrentSavePos))
				DoSaveAs();
			else
				DoSave();
		}

		public void ToolStripButton13_Click(System.Object sender, System.EventArgs e)
		{
			DoOpen();
		}
	}
}
