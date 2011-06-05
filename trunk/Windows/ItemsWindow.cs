using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using Paril.Windows.Forms.Docking;

namespace OpenCharas
{
	public partial class ItemsWindow : DockingWindowForm
	{
		public ItemsWindow() :
			base(Program.DockContainer)
		{
			InitializeComponent();
		}

		// Loading/Closing
		public void ItemsWindow_Load(System.Object sender, System.EventArgs e)
		{
			Icon = Properties.Resources.items.ToIcon(16, true);
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
		}

		private void pictureBox1_SizeChanged(object sender, EventArgs e)
		{
		}

		private void imageViewerContainer1_SelectedBoxChanged(object sender, EventArgs e)
		{
			var box = imageViewerContainer1.SelectedBox;

			if (_collapsed || box == null || box == RPGPictureBox.EmptyBox)
				pictureBox1.Image = null;
			else
			{
				pictureBox1.Image = box.PreviewImage;
				pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox1.Size = new System.Drawing.Size(box.PreviewImage.Width * 3, box.PreviewImage.Height * 3);
				pictureBox1.Location = new Point((pictureBox1.Parent.Width / 2) - (pictureBox1.Width / 2), (pictureBox1.Parent.Height / 2) - (pictureBox1.Height / 2));
			}
		}

		bool _collapsed = false;
		int _lastPos = 0;

		private void panel2_Resize(object sender, EventArgs e)
		{
			if (_collapsed)
				splitter1.SplitPosition = SplitContainer1.Panel2.Width - 28;
		}

		private void imageButton1_Click(object sender, EventArgs e)
		{
			_collapsed = !_collapsed;

			if (_collapsed)
			{
				_lastPos = splitter1.SplitPosition;
				splitter1.SplitPosition = SplitContainer1.Panel2.Width - 28;

				imageButton1.Normal = Properties.Resources.chev_left_normal;
				imageButton1.Hovered = Properties.Resources.chav_left_hovered;
				imageButton1.Pressed = Properties.Resources.chav_left_pressed;
			}
			else
			{
				splitter1.SplitPosition = _lastPos;
	
				imageButton1.Normal = Properties.Resources.chev_right_normal;
				imageButton1.Hovered = Properties.Resources.chav_right_hovered;
				imageButton1.Pressed = Properties.Resources.chav_right_pressed;
			}

			imageViewerContainer1_SelectedBoxChanged(null, null);
		}
	}
}
