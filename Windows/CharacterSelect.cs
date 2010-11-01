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

		public RPGCharacterRowList CharacterList = new RPGCharacterRowList();

		public void ClearCharacters()
		{
			CharacterList.Clear();
			
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
			foreach (var Layer in GetSelectedCharacter().Character.Layers)
				UpdateLayersTreeView_Recurse(Layer);
	
			Program.layersWindowForm.CurrentNode = null;
			Program.layersWindowForm.ChangeStates(false);
		}

		public void TreeView1_NodeMouseClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			CharacterList.NodeChanged(e.Node);
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

		public RPGCharacterNode GetSelectedCharacter()
		{
			return GetSelectedRow().LastSelectedCharacter;
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
			var Node = GetSelectedCharacter();

			if (Node == null)
				return;

			if (RowsWithNodes() == 1 && Node.Row.Nodes.Count == 1)
				// special case: if we have only this character, just clear the data.
				Node.Character = new RPGCharacter();
			else if (Node.Row.Nodes.Count == 1)
			{
				Node.Row.RemoveCharacter(Node);

				// Is there a row behind us with nodes
				if (Node.Row.Index > 0)
				{
					if (TreeView1.Nodes[Node.Row.Index - 1].Nodes.Count > 0)
						// Good, select it
						TreeView1.SelectedNode = TreeView1.Nodes[Node.Row.Index - 1].Nodes[TreeView1.Nodes[Node.Row.Index - 1].Nodes.Count - 1];
				}
				else if (Node.Row.Index == 0)
				{
					// Ahead of us
					if (TreeView1.Nodes[Node.Row.Index + 1].Nodes.Count > 0)
						// Good, select it
						TreeView1.SelectedNode = TreeView1.Nodes[Node.Row.Index + 1].Nodes[TreeView1.Nodes[Node.Row.Index + 1].Nodes.Count - 1];
				}
			}
			else
			{
				int NewIndex = Node.Index;
				Node.Row.Nodes.Remove(Node);
				if (NewIndex > Node.Row.Nodes.Count - 1)
					NewIndex = (int)(Node.Row.Nodes.Count - 1);
				TreeView1.SelectedNode = Node.Row.Nodes[NewIndex];
			}

			UpdateLayersTreeView();
			Program.canvasForm.UpdateDrawing();
		}

		public void TreeView1_NodeMouseDoubleClick(System.Object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			e.Node.BeginEdit();
		}

		public void MoveNode(int IndexOffset)
		{
			var Node = GetSelectedCharacter();
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

				CharacterList.RemoveRow(Row);
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

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			CharacterList.NodeChanged(e.Node);
		}
	}

	public class RPGCharacterNode : TreeNode
	{
		RPGCharacter _character;

		public RPGCharacterNode(RPGCharacter character)
		{
			_character = character;
		}

		public RPGCharacterRowNode Row
		{
			get { return Parent as RPGCharacterRowNode; }
		}

		public RPGCharacter Character
		{
			get { return _character; }
			set { _character = value; }
		}

		public void Select()
		{
			Row.LastSelected = this;
			Program.characterSelectForm.UpdateLayersTreeView();
			Program.canvasForm.UpdateDrawing();
		}
	}

	public class RPGCharacterRowNode : TreeNode
	{
		List<RPGCharacterNode> _characters = new List<RPGCharacterNode>();

		public List<RPGCharacterNode> Characters
		{
			get { return _characters; }
		}

		RPGCharacterNode _lastSelected;

		public RPGCharacterNode LastSelected
		{
			get { return _lastSelected; }
			set { _lastSelected = value; }
		}

		public RPGCharacterNode LastSelectedCharacter
		{
			get
			{
				if (Program.characterSelectForm.TreeView1.SelectedNode.Parent == this)
					return (Program.characterSelectForm.TreeView1.SelectedNode as RPGCharacterNode);

				return _lastSelected;
			}
		}

		public RPGCharacterNode AddCharacter(bool SwitchTo = false)
		{
			RPGCharacterNode NewCharacter = new RPGCharacterNode(new RPGCharacter());
			NewCharacter.Text = (string)("Character " + (Nodes.Count + 1).ToString());

			Nodes.Add(NewCharacter);
			_characters.Add(NewCharacter);

			if (SwitchTo)
			{
				Program.characterSelectForm.TreeView1.SelectedNode = NewCharacter;

				Program.characterSelectForm.UpdateLayersTreeView();
				Program.canvasForm.UpdateDrawing();
			}

			return NewCharacter;
		}

		public void RemoveCharacter(RPGCharacterNode node)
		{
			_characters.Remove(node);
			node.Parent.Remove();
		}

		public void Clear()
		{
			while (_characters.Count != 0)
				RemoveCharacter(_characters[0]);
		}
	}

	public class RPGCharacterRowList
	{
		public RPGCharacterRowList()
		{
		}

		List<RPGCharacterRowNode> _rows = new List<RPGCharacterRowNode>();
		public List<RPGCharacterRowNode> Rows
		{
			get { return _rows; }
		}

		public RPGCharacterRowNode AddRow()
		{
			RPGCharacterRowNode NewRow = new RPGCharacterRowNode();
			NewRow.Text = (string)("Row " + (Program.characterSelectForm.TreeView1.Nodes.Count + 1).ToString());

			Program.characterSelectForm.TreeView1.Nodes.Add(NewRow);
			_rows.Add(NewRow);
			return NewRow;
		}

		public void RemoveRow(RPGCharacterRowNode node)
		{
			node.Clear();
			_rows.Remove(node);
			node.Remove();
		}

		public void Clear()
		{
			while (_rows.Count != 0)
				RemoveRow(_rows[0]);
		}

		public void NodeChanged(TreeNode Node)
		{
			if (Node == null || !(Node is RPGCharacterNode))
				return;

			RPGCharacterNode MyNode = (RPGCharacterNode)Node;

			MyNode.Select();
		}
	}
}
