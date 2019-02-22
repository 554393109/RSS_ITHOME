using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        private static extern bool FlashWindowEx(int pfwi);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] arr_param)
        {
#if DEBUG
            bool noExists;
            System.Threading.Mutex run = new System.Threading.Mutex(true, "single", out noExists);

            if (noExists)
            {
                run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MainForm frm = new MainForm(arr_param);
                int hdc = frm.Handle.ToInt32();
                Application.Run(frm);
                IntPtr a = new IntPtr(hdc);
            }
            else
            {
                Application.Exit();
            }
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(arr_param));
#endif
        }
    }
}
