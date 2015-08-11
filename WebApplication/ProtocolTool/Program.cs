using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtocolTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm _mainForm = new mainForm();
            if (args != null && args.Length > 0)
                _mainForm.encrypt(args[0]);
            else
            {
                Application.EnableVisualStyles();
                Application.Run(_mainForm);
            }
        }
    }
}
