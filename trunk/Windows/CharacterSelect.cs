using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class CharacterSelect
	{
		public CharacterSelect()
		{
			InitializeComponent();

			CharacterList = new RPGCharacterNodeList(this);
		}

		public void CharacterSelect_Load(System.Object sender, System.EventArgs e)
		{
		}

		public void CharacterSelect_Move(System.Object sender, System.EventArgs e)
		{
			if (this.WindowState != FormWindowState.Normal)
				return;
			if (Program.canvasForm.SkipSizeChanged)
				return;

			// Dockable.CheckDocking()
		}

		public RPGCharacterNodeList CharacterList;

		public void ClearCharacters()
		{
			TreeView1.Nodes.Clear();
			
			var Row = CharacterList.AddRow();
			Row.AddCharacter(true);
		}

		public void UpdateLayersTreeView_Recurse(RPGLayer Layer)
		{
			Program.layersWindowForm.TreeView1.Nodes.Add(Layer.Node);
		
			foreach (var SubLayer in Layer.SubLayers)
				UpdateLayersTreeView_Recurse(SubLayer);
		}

		public void UpdateLayersTreeView()
		{
			Program.layersWindowForm.TreeView1.Nodes.Clear();
			foreach (var Layer in CharacterList.CurrentCharacter.Character.Layers)
				UpdateLayersTreeView_Recurse(Layer);
	
			Program.layersWindowForm.CurrentNode = null;
			Program.layersWindowForm.ChangeStates(false);
		}

		public void TreeView1_NodeMouseClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			TreeNode Node = e.Node;

			if (Node == null)
				return;
			if (!(Node is RPGCharacterNode))
				return;

			RPGCharacterNode MyNode = (RPGCharacterNode)Node;

			if (CharacterList.CurrentCharacter != MyNode)
			{
				CharacterList.CurrentCharacter = MyNode;
				UpdateLayersTreeView();
				Program.canvasForm.UpdateDrawing();
			}
		}

		public RPGCharacterRowNode GetSelectedRow()
		{
			TreeNode Node = TreeView1.SelectedNode;
			RPGCharacterRowNode Row;

			if (!(Node is RPGCharacterRowNode))
				Row = (RPGCharacterRowNode)Node.Parent;
			else
				Row = (RPGCharacterRowNode)Node;

			return Row;
		}

		public void ToolStripButton1_Click(System.Object sender, System.EventArgs e)
		{
			var Row = GetSelectedRow();

			if (Row == null)
				return;

			Row.AddCharacter(true);
		}

		public int RowsWithNodes()
		{
			int NumWithNodes = 0;

			foreach (TreeNode Row in TreeView1.Nodes)
			{
				if (Row.Nodes.Count != 0)
					NumWithNodes++;
			}

			return NumWithNodes;
		}

		public void ToolStripButton2_Click(System.Object sender, System.EventArgs e)
		{
			var Node = CharacterList.CurrentCharacter;
			var Row = GetSelectedRow();

			if (Node == null || Row == null || TreeView1.SelectedNode.GetType() == typeof(RPGCharacterRowNode))
				return;

			if (RowsWithNodes() == 1 && Row.Nodes.Count == 1)
				// special case: if we have only this character, just clear the data.
				Node.Character = new RPGCharacter();
			else if (Row.Nodes.Count == 1)
			{
				Row.Nodes.Remove(Node);

				// Is there a row behind us with nodes
				if (Row.Index > 0)
				{
					if (TreeView1.Nodes[Row.Index - 1].Nodes.Count > 0)
						// Good, select it
						TreeView1.SelectedNode = TreeView1.Nodes[Row.Index - 1].Nodes[TreeView1.Nodes[Row.Index - 1].Nodes.Count - 1];
				}
				else if (Row.Index == 0)
				{
					// Ahead of us
					if (TreeView1.Nodes[Row.Index + 1].Nodes.Count > 0)
						// Good, select it
						TreeView1.SelectedNode = TreeView1.Nodes[Row.Index + 1].Nodes[TreeView1.Nodes[Row.Index + 1].Nodes.Count - 1];
				}
			}
			else
			{
				int NewIndex = Node.Index;
				Row.Nodes.Remove(Node);
				if (NewIndex > Row.Nodes.Count - 1)
					NewIndex = (int)(Row.Nodes.Count - 1);
				TreeView1.SelectedNode = Row.Nodes[NewIndex];
			}

			if (CharacterList.CurrentCharacter != TreeView1.SelectedNode)
			{
				CharacterList.CurrentCharacter = (RPGCharacterNode)TreeView1.SelectedNode;
				UpdateLayersTreeView();
				Program.canvasForm.UpdateDrawing();
			}
		}

		public void TreeView1_NodeMouseDoubleClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			e.Node.BeginEdit();
		}

		public void MoveNode(int IndexOffset)
		{
			var Node = CharacterList.CurrentCharacter;
			if (Node == null || TreeView1.SelectedNode.GetType() == typeof(RPGCharacterRowNode))
				return;

			RPGCharacterRowNode Row = (RPGCharacterRowNode)Node.Parent;

			// Can we even move
			int WantedIndex = Node.Index + IndexOffset;
			if ((WantedIndex > Node.Index) && Node.Index == Row.Nodes.Count - 1)
			{
				// Rows in front of us
				if (Row.Index != TreeView1.Nodes.Count - 1)
				{
					// remove us from this row and push us back to the end
					Row.Nodes.Remove(Node);
					TreeView1.Nodes[Row.Index + 1].Nodes.Insert(0, Node);
					TreeView1.SelectedNode = Node;
					Program.canvasForm.UpdateDrawing();
				}
				return;
			}
			if ((WantedIndex < Node.Index) && Node.Index == 0)
			{
				// We're at the end
				// Are there any rows behind us
				if (Row.Index > 0 && TreeView1.Nodes.Count > (Row.Index))
				{
					// remove us from this row and push us back to the end
					Row.Nodes.Remove(Node);
					TreeView1.Nodes[Row.Index - 1].Nodes.Add(Node);
					TreeView1.SelectedNode = Node;
					Program.canvasForm.UpdateDrawing();
				}
				return;
			}

			Row.Nodes.Remove(Node);
			Row.Nodes.Insert(WantedIndex, Node);
			TreeView1.SelectedNode = Node;
			Program.canvasForm.UpdateDrawing();
		}

		public void ToolStripButton5_Click(System.Object sender, System.EventArgs e)
		{
			MoveNode(1);
		}

		public void ToolStripButton6_Click(System.Object sender, System.EventArgs e)
		{
			MoveNode(-1);
		}

		public void ToolStripButton3_Click(System.Object sender, System.EventArgs e)
		{
			CharacterList.AddRow();
		}

		public void ToolStripButton4_Click(System.Object sender, System.EventArgs e)
		{
			if (TreeView1.SelectedNode.GetType() == typeof(RPGCharacterRowNode))
			{
				RPGCharacterRowNode Row = (RPGCharacterRowNode)TreeView1.SelectedNode;

				if (TreeView1.Nodes.Count == 1)
					return;
				if (Row.Nodes.Count > 0)
				{
					if (MessageBox.Show("Deleting this row will delete all characters associated with it. Are you sure you want to do this?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						return;
				}

				TreeView1.Nodes.Remove(Row);
				Program.canvasForm.UpdateDrawing();
			}
		}

		public void MoveRow(int IndexOffset)
		{
			var Node = TreeView1.SelectedNode;
			if (Node == null || !(Node is RPGCharacterRowNode))
				return;

			if (TreeView1.Nodes.Count == 1)
				return;

			// Can we even move
			int WantedIndex = Node.Index + IndexOffset;

			TreeView1.Nodes.Remove(Node);
			TreeView1.Nodes.Insert(WantedIndex, Node);
			TreeView1.SelectedNode = Node;
			Program.canvasForm.UpdateDrawing();
		}

		public void ToolStripButton8_Click(System.Object sender, System.EventArgs e)
		{
			MoveRow(1);
		}

		public void ToolStripButton7_Click(System.Object sender, System.EventArgs e)
		{
			MoveRow(-1);
		}
	}

	public class RPGCharacterNode : TreeNode
	{
		public RPGCharacter Character = new RPGCharacter();
	}

	public class RPGCharacterRowNode : TreeNode
	{
		public RPGCharacterNodeList Row;
		public CharacterSelect SelectForm;

		public RPGCharacterRowNode(CharacterSelect Form)
		{
			SelectForm = Form;
		}

		public RPGCharacterNode AddCharacter(bool SwitchTo = false)
		{
			RPGCharacterNode NewCharacter = new RPGCharacterNode();
			NewCharacter.Text = (string)("Character " + (Nodes.Count + 1).ToString());

			Nodes.Add(NewCharacter);
			if (SwitchTo)
			{
				SelectForm.TreeView1.SelectedNode = NewCharacter;
				Program.characterSelectForm.CharacterList.CurrentCharacter = NewCharacter;

				SelectForm.UpdateLayersTreeView();
				Program.canvasForm.UpdateDrawing();
			}
			return NewCharacter;
		}
	}

	public class RPGCharacterNodeList
	{
		public CharacterSelect SelectForm;

		public RPGCharacterNodeList(CharacterSelect Form)
		{
			SelectForm = Form;
		}

		private RPGCharacterNode CurrentSelectedNode_;
		public RPGCharacterNode CurrentCharacter
		{
			get { return CurrentSelectedNode_; }
			set { CurrentSelectedNode_ = value; }
		}

		public RPGCharacterRowNode AddRow()
		{
			RPGCharacterRowNode NewRow = new RPGCharacterRowNode(SelectForm);
			NewRow.Text = (string)("Row " + (SelectForm.TreeView1.Nodes.Count + 1).ToString());

			SelectForm.TreeView1.Nodes.Add(NewRow);
			return NewRow;
		}
	}
}
