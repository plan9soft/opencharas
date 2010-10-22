using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class ImagePacker
	{
		public ImagePacker()
		{
			InitializeComponent();
		}

		public void Form7_Load(System.Object sender, System.EventArgs e)
		{
		}

		static public bool ContainsThisBox(Control.ControlCollection Controls, RPGPictureBox Box)
		{
			if (Controls == null || Box == null)
				return false;

			foreach (var Control in Controls)
			{
				var MyBox = (RPGPictureBox)Control;
		
				if (MyBox.MyImage == Box.MyImage)
					return true;
			}

			return false;
		}

		public void TreeView1_NodeMouseClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			// Clear current box
			FlowLayoutPanel1.Controls.Clear();

			GameNodeNode Node = (GameNodeNode)e.Node;

			if (Node.RPGNode.Images.Count != 0)
			{
				foreach (var Image in Node.RPGNode.Images)
				{
					// Add a box
					if (ContainsThisBox(FlowLayoutPanel2.Controls, Image.Box) == false)
						FlowLayoutPanel1.Controls.Add(Image.Box.CreatePictureBoxCopy());
				}
			}

			TreeView1.SelectedNode = Node;
		}

		public void FlowLayoutPanel2_DragEnter(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(RPGPictureBox)))
			{
				RPGPictureBox Box = (RPGPictureBox)e.Data.GetData(typeof(RPGPictureBox));

				if (ContainsThisBox(FlowLayoutPanel2.Controls, Box))
					e.Effect = DragDropEffects.None;
				else
					e.Effect = DragDropEffects.Copy;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		public void FlowLayoutPanel2_DragDrop(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(RPGPictureBox)))
				FlowLayoutPanel2.Controls.Add((RPGPictureBox)e.Data.GetData(typeof(RPGPictureBox)));
		}

		public void FlowLayoutPanel1_DragEnter(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(RPGPictureBox)))
			{
				RPGPictureBox Box = (RPGPictureBox)e.Data.GetData(typeof(RPGPictureBox));

				if (ContainsThisBox(FlowLayoutPanel1.Controls, Box))
					e.Effect = DragDropEffects.None;
				else
					e.Effect = DragDropEffects.Copy;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		public void FlowLayoutPanel1_DragDrop(System.Object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(RPGPictureBox)))
				FlowLayoutPanel1.Controls.Add((RPGPictureBox)e.Data.GetData(typeof(RPGPictureBox)));
		}

		public void Button2_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		public void Button1_Click(System.Object sender, System.EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		public void PackEverythingToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			FlowLayoutPanel2.Controls.Clear();

			foreach (var Node in Images.RPGNodes)
				Node.RecurseNodeAndAddAll();

			FlowLayoutPanel1.Controls.Clear();
		}

		public void UnpackEverythingToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			FlowLayoutPanel2.Controls.Clear();
		}

		public void PackAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			while (FlowLayoutPanel1.Controls.Count != 0)
				FlowLayoutPanel2.Controls.Add(FlowLayoutPanel1.Controls[0]);
		}

		public void UnpackAllToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
		{
			FlowLayoutPanel2.Controls.Clear();
		}
	}
}
