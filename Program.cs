using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

namespace OpenCharas
{
	public static class Program
	{
		public static Canvas canvasForm;
		public static CharacterSelect characterSelectForm;
		public static ItemsWindow itemsWindowForm;
		public static LayersWindow layersWindowForm;
		public static ImagePacker imagePackerForm;

		[STAThread]
		public static void Main()
		{
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
			Canvas.ReportCrash(e.Exception);
		}
	}
}
