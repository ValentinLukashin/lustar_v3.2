using nlCsDocumentation;
using System;
using System.Windows.Forms;

namespace naCsDocumentation
{
    internal static class csdBegin
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);

            csdApplication.__fFileNameHelp = "CsDocumentation";
            csdApplication.__fPrefix = "csd";

            if (csdApplication.__oEventsHandler.__mBegin())
            {
                csdFormMain vFormMain = new csdFormMain();
                vFormMain.ShowDialog();
                csdApplication.__oEventsHandler.__mEnd();
            }
        }
    }
}
