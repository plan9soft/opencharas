using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using Paril.Windows.Forms.Docking;

namespace OpenCharas
{
	public static class Program
	{
		public static Canvas canvasForm;
		public static CharacterSelect characterSelectForm;
		public static ItemsWindow itemsWindowForm;
		public static LayersWindow layersWindowForm;
		public static ImagePacker imagePackerForm;

		public static DockingContainer DockContainer = new DockingContainer(true);

		public static void WorkInProgress(string feature)
		{
			MessageBox.Show(feature+" is a work in progress and not included/not enabled in this version. Please check back later.", "Sorry", MessageBoxButtons.OK);
		}

		[STAThread]
		public static void Main()
		{
			if (System.IO.File.Exists("Updater.exe"))
				System.IO.File.Delete("Updater.exe");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
			//Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
#endif

			canvasForm = new Canvas();
			characterSelectForm = new CharacterSelect();
			itemsWindowForm = new ItemsWindow();
			layersWindowForm = new LayersWindow();
			imagePackerForm = new ImagePacker();

			Application.Run(canvasForm);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			//Canvas.ReportCrash(e.Exception);
			Paril.Windows.Dialogs.ExceptionDialog.Show(e.Exception);
		}
	}
}
