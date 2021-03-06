using System;
using System.Windows.Forms;

namespace QRCoderArt
{
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //            GUITree ReflectionData = new GUITree(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName);
            GUITree ReflectionData = new GUITree();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(ReflectionData));
        }


        // Display a formatted string indented by the specified amount.
        /// <summary>
        /// Displays the specified indent.
        /// </summary>
        /// <param name="indent">The indent.</param>
        /// <param name="format">The format.</param>
        /// <param name="param">The parameter.</param>
        public static void Display(Int32 indent, string format, params object[] param)
        {
            //            System.Diagnostics.Debug.WriteLine(new string(' ', indent * 2));
            //            System.Diagnostics.Debug.WriteLine(format, param);
            Console.Write(new string(' ', Math.Abs(indent) * 2));
            Console.WriteLine(format, param);
        }
    }
}
