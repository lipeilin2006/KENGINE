using KENGINE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTK.WinForms.TestForm
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new EditorFormMain());
            Application.ApplicationExit += Application_ApplicationExit;
		}

        private static void Application_ApplicationExit(object? sender, EventArgs e)
        {
            foreach(Shader shader in KENGINE.KENGINE.shaders.Values)
			{
				shader.Delete();
			}
        }
    }
}
