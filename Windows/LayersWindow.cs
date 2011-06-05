using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
using Paril.Windows.Forms.Docking;

namespace OpenCharas
{
	public partial class LayersWindow : DockingWindowForm
	{
		public LayersWindow() :
			base(Program.DockContainer)
		{
			InitializeComponent();
		}

		// Loading/Closing
		public void LayersWindow_Load(System.Object sender, System.EventArgs e)
		{
			ToolStrip1.Renderer = new AwesomeToolStripRenderer();
			Icon = Properties.Resources.layers.ToIcon(16, true);
			
			TreeView1.Nodes.Clear();

			SetSliderColors();
		}

		public void LayersWindow_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			if (Program.canvasForm.FinalClosing)
			{
				return;
			}

			e.Cancel = true;
			this.Hide();
		}

		public RPGNode CurrentNode { get; set; }

		public void ChangeStatesSub(Control Ctrl, bool Enabled)
		{
			foreach (Control SubCtrl in Ctrl.Controls)
			{
				SubCtrl.Enabled = Enabled;
				ChangeStatesSub(SubCtrl, Enabled);
			}
		}

		public void ChangeStates(bool Enabled)
		{
			foreach (Control Ctrl in SplitContainer1.Panel2.Controls)
			{
				Ctrl.Enabled = Enabled;
				ChangeStatesSub(Ctrl, Enabled);
			}

			if (Enabled == false)
			{
				NumericUpDown1.Value = 0;
				NumericUpDown2.Value = 0;

				TrackBar1.Value = 0;
				TrackBar2.Value = 100;
				TrackBar3.Value = 100;
				TrackBar5.Value = 0;
				TrackBar6.Value = 255;
				CheckBox3.Checked = false;
				SetSliderColors();
				PictureBox1.Image = null;
			}
		}

		public void LayersWindow_Move(System.Object sender, System.EventArgs e)
		{
			if (Program.canvasForm.SkipSizeChanged)
			{
				return;
			}
		}

		public void SetSliderColors()
		{
			TextBox2.Text = TrackBar1.Value.ToString();
			TextBox3.Text = (string)((TrackBar2.Value / 100).ToString());
			TextBox4.Text = (string)((TrackBar3.Value / 100).ToString());
			TextBox6.Text = (string)((TrackBar5.Value / 100).ToString());
			TextBox7.Text = TrackBar6.Value.ToString();
		}

		public void SliderColorsToLayerColors()
		{
			TextBox2.Text = CurrentNode.Layer.MatrixValues[0].ToString();
			TextBox3.Text = CurrentNode.Layer.MatrixValues[1].ToString();
			TextBox4.Text = CurrentNode.Layer.MatrixValues[2].ToString();
			TextBox6.Text = CurrentNode.Layer.MatrixValues[3].ToString();
			TextBox7.Text = CurrentNode.Layer.Alpha.ToString();
		}

		public void ToolStripButton6_Click(System.Object sender, System.EventArgs e)
		{
			RPGNode Node = new RPGNode();
			Node.Text = (string)("Layer " + (TreeView1.Nodes.Count+ 1).ToString());
			Node.Layer = Program.characterSelectForm.GetSelectedCharacter().Character.CreateLayer(Node);
			Node.Layer.Node = Node;
			TreeView1.Nodes.Add(Node);
			TreeView1.SelectedNode = Node;
		}

		public void ToolStripButton8_Click(System.Object sender, System.EventArgs e)
		{
			if (TreeView1.SelectedNode == null)
				return;

			RPGNode Node = (RPGNode)TreeView1.SelectedNode;

			if (Node == null)
				return;

			RPGNode NewNode = new RPGNode();
			NewNode.Text = (string)("Sub-Layer " + (Node.Nodes.Count+ 1).ToString());
			NewNode.Layer = Program.characterSelectForm.GetSelectedCharacter().Character.CreateLayer(NewNode);
			NewNode.Layer.Node = NewNode;
			Node.Nodes.Add(NewNode);
			Node.Expand();
		}

		public void ToolStripButton7_Click(System.Object sender, System.EventArgs e)
		{
			RPGNode Node = (RPGNode)TreeView1.SelectedNode;

			if (Node == null)
				return;

			Program.characterSelectForm.GetSelectedCharacter().Character.RemoveLayer(Node.Layer);
			TreeView1.Nodes.Remove(Node);

			if (TreeView1.SelectedNode != null)
				DoSelectNode(TreeView1.SelectedNode);
			else
				ChangeStates(false);

			Program.canvasForm.UpdateDrawing();
		}

		public void ToolStripButton9_Click(System.Object sender, System.EventArgs e)
		{
			RPGNode Node = (RPGNode)TreeView1.SelectedNode;

			if (Node == null)
				return;

			TreeNodeCollection NodeCont;
			if (Node.Parent == null)
				NodeCont = TreeView1.Nodes;
			else
				NodeCont = Node.Parent.Nodes;

			if (NodeCont.Count == 1 || NodeCont.IndexOf(Node) == 0)
				return;

			int OldIndex = NodeCont.IndexOf(Node);
			NodeCont.Remove(Node);
			NodeCont.Insert(OldIndex - 1, Node);
			TreeView1.SelectedNode = Node;

			Program.characterSelectForm.GetSelectedCharacter().Character.MoveLayer(Node.Layer, false);
		}

		public void ToolStripButton1_Click(System.Object sender, System.EventArgs e)
		{
			RPGNode Node = (RPGNode)TreeView1.SelectedNode;

			if (Node == null)
				return;

			TreeNodeCollection NodeCont;
			if (Node.Parent == null)
				NodeCont = TreeView1.Nodes;
			else
				NodeCont = Node.Parent.Nodes;

			if (NodeCont.Count == 1 || NodeCont.IndexOf(Node) == NodeCont.Count - 1)
				return;

			int OldIndex = NodeCont.IndexOf(Node);
			NodeCont.Remove(Node);
			NodeCont.Insert(OldIndex + 1, Node);
			TreeView1.SelectedNode = Node;

			Program.characterSelectForm.GetSelectedCharacter().Character.MoveLayer(Node.Layer, true);
		}

		public void TreeView1_NodeMouseClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
		}

		private bool SkipChanged = false;

		public void PictureBox1_Click(System.Object sender, System.EventArgs e)
		{
			Program.itemsWindowForm.Show();
			Program.itemsWindowForm.BringToFront();
		}

		public void NumericUpDown1_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode != null)
			{
				CurrentNode.Layer.Offset = new Point((int)(NumericUpDown1.Value), CurrentNode.Layer.Offset.Y);
				Program.canvasForm.UpdateDrawing();
			}
		}

		public void NumericUpDown2_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode != null)
			{
				CurrentNode.Layer.Offset = new Point(CurrentNode.Layer.Offset.X, (int)(NumericUpDown2.Value));
				Program.canvasForm.UpdateDrawing();
			}
		}

		public void LayersWindow_Resize(System.Object sender, System.EventArgs e)
		{
		}

		public void SetTextToTrackBar(TextBox Box, float Value, int Index)
		{
			if (SkipChanged)
			{
				return;
			}

			SkipChanged = true;
			Box.Text = Value.ToString();
			SkipChanged = false;
			CurrentNode.Layer.SetColorMatrix();
			CurrentNode.Layer.MatrixValues[Index] = Value;
		}

		public void SetTrackBarToText(TrackBar TrackB, TextBox Box, int LayerIndex, bool IsSingle)
		{
			if (CurrentNode == null)
			{
				return;
			}
			if (SkipChanged)
			{
				return;
			}

			float Val;
			if (IsSingle == false)
			{
				int val;
				if (int.TryParse(Box.Text, out val) == false)
				{
					Box.Text = (TrackB.Value).ToString();
					return;
				}

				Val = val;
			}
			else
			{
				float val;
				if (float.TryParse(Box.Text, out val) == false)
				{
					Box.Text = (TrackB.Value / 100).ToString();
					return;
				}

				Val = val;
			}

			if (IsSingle)
				Val *= 100;

			if (Val > TrackB.Maximum)
				Val = TrackB.Maximum;
			else if (Val < TrackB.Minimum)
				Val = TrackB.Minimum;

			SkipChanged = true;
			TrackB.Value = (int)(Val);
			if (IsSingle)
				Box.Text = (string)((TrackB.Value / 100).ToString());
			else
				Box.Text = (string)((TrackB.Value).ToString());
			SkipChanged = false;

			CurrentNode.Layer.MatrixValues[LayerIndex] = float.Parse(Box.Text);
			CurrentNode.Layer.SetColorMatrix();
		}

		public void SetTrackBarToText(TrackBar TrackB, TextBox Box, int LayerIndex)
		{
			SetTrackBarToText(TrackB, Box, LayerIndex, false);
		}

		public void TrackBar1_Scroll(System.Object sender, System.EventArgs e)
		{
			SetTextToTrackBar(TextBox2, TrackBar1.Value, 0);
		}

		public void TrackBar2_Scroll(System.Object sender, System.EventArgs e)
		{
			SetTextToTrackBar(TextBox3, TrackBar2.Value / 100.0f, 1);
		}

		public void TrackBar3_Scroll(System.Object sender, System.EventArgs e)
		{
			SetTextToTrackBar(TextBox4, TrackBar3.Value / 100.0f, 2);
		}

		public void TrackBar5_Scroll(System.Object sender, System.EventArgs e)
		{
			SetTextToTrackBar(TextBox6, TrackBar5.Value / 100.0f, 3);
		}

		public void TextBox2_TextChanged(System.Object sender, System.EventArgs e)
		{
			SetTrackBarToText(TrackBar1, TextBox2, 0);
		}

		public void TextBox3_TextChanged(System.Object sender, System.EventArgs e)
		{
			SetTrackBarToText(TrackBar2, TextBox3, 1, true);
		}

		public void TextBox4_TextChanged(System.Object sender, System.EventArgs e)
		{
			SetTrackBarToText(TrackBar3, TextBox4, 2, true);
		}

		public void TextBox6_TextChanged(System.Object sender, System.EventArgs e)
		{
			SetTrackBarToText(TrackBar5, TextBox6, 3, true);
		}

		public void TrackBar6_Scroll(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode == null)
				return;

			TextBox7.Text = TrackBar6.Value.ToString();
			CurrentNode.Layer.Alpha = TrackBar6.Value;
			CurrentNode.Layer.SetColorMatrix();
		}

		public void TextBox7_TextChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode == null)
			{
				return;
			}

			int Val;
			if (int.TryParse(TextBox7.Text, out Val) == false)
			{
				TextBox7.Text = TrackBar6.Value.ToString();
				return;
			}

			if (Val > TrackBar6.Maximum)
			{
				Val = TrackBar6.Maximum;
			}
			else if (Val < TrackBar6.Minimum)
			{
				Val = TrackBar6.Minimum;
			}

			TrackBar6.Value = Val;
			TextBox7.Text = Val.ToString();
			CurrentNode.Layer.Alpha = Val;
			CurrentNode.Layer.SetColorMatrix();
		}

		public void TreeView1_AfterLabelEdit(System.Object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			((RPGNode)e.Node).Layer.Name = e.Node.Text;
		}

		public void CheckBox1_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode == null)
				return;

			if (CheckBox1.Checked)
				CurrentNode.Layer.FlipFlags |= EFlipFlag.FlipHorizontal;
			else
				CurrentNode.Layer.FlipFlags &= ~EFlipFlag.FlipHorizontal;

			CurrentNode.Layer.UpdateFlippedImage();
			Program.canvasForm.UpdateDrawing();
		}

		public void CheckBox2_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode == null)
			{
				return;
			}

			if (CheckBox2.Checked)
				CurrentNode.Layer.FlipFlags |= EFlipFlag.FlipVertical;
			else
				CurrentNode.Layer.FlipFlags &= ~EFlipFlag.FlipVertical;

			CurrentNode.Layer.UpdateFlippedImage();
			Program.canvasForm.UpdateDrawing();
		}

		public void DoSelectNode(TreeNode Node)
		{
			CurrentNode = (RPGNode)Node;
			SliderColorsToLayerColors();
			ChangeStates(true);

			//TextBox1.Text = CurrentNode.Layer.Name
			NumericUpDown1.Value = CurrentNode.Layer.Offset.X;
			NumericUpDown2.Value = CurrentNode.Layer.Offset.Y;

			if (CurrentNode.Layer.Image != null)
			{
				PictureBox1.Image = CurrentNode.Layer.Image.Box.MyImage;
			}
			else
			{
				PictureBox1.Image = null;
			}

			CheckBox1.Checked = false;
			CheckBox2.Checked = false;

			if ((CurrentNode.Layer.FlipFlags & (EFlipFlag.FlipHorizontal | EFlipFlag.FlipVertical)) != 0)
			{
				CheckBox1.Checked = true;
				CheckBox2.Checked = true;
			}
			else if ((CurrentNode.Layer.FlipFlags & EFlipFlag.FlipHorizontal) != 0)
			{
				CheckBox1.Checked = true;
			}
			else if ((CurrentNode.Layer.FlipFlags & EFlipFlag.FlipVertical) != 0)
			{
				CheckBox2.Checked = true;
			}

			CheckBox3.Checked = CurrentNode.Layer.Inverted;
			TreeView1.SelectedNode = Node;
		}

		public void TreeView1_AfterSelect(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			RPGNode Node = (RPGNode)e.Node;

			if (Node != null)
			{
				DoSelectNode(Node);
			}
			else
			{
				ChangeStates(false);
			}
		}

		public void TreeView1_NodeMouseDoubleClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			e.Node.BeginEdit();
		}

		public void CheckBox3_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (CurrentNode == null)
			{
				return;
			}

			CurrentNode.Layer.Inverted = CheckBox3.Checked;
			CurrentNode.Layer.SetColorMatrix();
			Program.canvasForm.UpdateDrawing();
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			RPGNode Node = (RPGNode)e.Node;

			if (Node != null)
				DoSelectNode(Node);
			else
				ChangeStates(false);
		}
	}

	public class RPGNode : TreeNode
	{
		public RPGLayer Layer { get; set; }

		public void RecurseAndLoadSubNodes()
		{
			foreach (var SubLayer in Layer.SubLayers)
			{
				RPGNode SubNode = new RPGNode();
				SubNode.Text = SubLayer.Name;
				SubNode.Layer = SubLayer;
				SubNode.Layer.Node = SubNode;

				Nodes.Add(SubNode);
				SubNode.RecurseAndLoadSubNodes();
			}
		}
	}
}
