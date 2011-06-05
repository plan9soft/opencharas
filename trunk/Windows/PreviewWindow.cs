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
	public partial class PreviewWindow : DockingWindowForm
	{
		public PreviewWindow() :
			base(Program.DockContainer)
		{
			InitializeComponent();
		}

		public Bitmap RenderedImage;

		public void Form4_Load(System.Object sender, System.EventArgs e)
		{
			// Create the render
			PictureBox1.Image = RenderedImage;
		}

		public void Form4_Move(System.Object sender, System.EventArgs e)
		{
			if (Program.canvasForm.easyPositionForm != null && Program.canvasForm.easyPositionForm.Visible == true)
				return;

			//Dockable.CheckDocking()
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
