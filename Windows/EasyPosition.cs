using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public partial class EasyPosition
	{
		public EasyPosition()
		{
			InitializeComponent();
		}

		public void Form5_Deactivate(System.Object sender, System.EventArgs e)
		{
			if (this.Visible)
				this.Close();
		}

		public void Form5_Load(System.Object sender, System.EventArgs e)
		{
			if (Program.layersWindowForm.CurrentNode == null)
			{
				Label1.Text = "";
				return;
			}

			Label1.SendToBack();
			Label1.Text = Program.layersWindowForm.CurrentNode.Layer.Offset.X.ToString() + "," + Program.layersWindowForm.CurrentNode.Layer.Offset.Y.ToString();
		}

		public void Form5_FormClosed(System.Object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
		}

		public void Button9_Click(System.Object sender, System.EventArgs e)
		{
			if (Program.layersWindowForm.CurrentNode == null)
				return;

			Program.layersWindowForm.CurrentNode.Layer.Offset = new Point(0, 0);
			Program.canvasForm.UpdateDrawing();
			Label1.Text = Program.layersWindowForm.CurrentNode.Layer.Offset.X.ToString() + "," + Program.layersWindowForm.CurrentNode.Layer.Offset.Y.ToString();
		}

		public void OffsetIt(Point Pnt)
		{
			if (Program.layersWindowForm.CurrentNode == null)
				return;

			Program.layersWindowForm.CurrentNode.Layer.Offset = new Point(Program.layersWindowForm.CurrentNode.Layer.Offset.X + Pnt.X, Program.layersWindowForm.CurrentNode.Layer.Offset.Y + Pnt.Y);
			Program.canvasForm.UpdateDrawing();
			Label1.Text = Program.layersWindowForm.CurrentNode.Layer.Offset.X.ToString() + "," + Program.layersWindowForm.CurrentNode.Layer.Offset.Y.ToString();
		}

		public void Button1_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(0, -1));
		}

		public void Button8_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(1, -1));
		}

		public void Button4_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(1, 0));
		}

		public void Button7_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(1, 1));
		}

		public void Button2_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(0, 1));
		}

		public void Button6_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(-1, 1));
		}

		public void Button3_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(-1, 0));
		}

		public void Button5_Click(System.Object sender, System.EventArgs e)
		{
			OffsetIt(new Point(-1, -1));
		}
	}
}
