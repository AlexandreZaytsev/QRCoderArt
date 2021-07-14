// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : NigelThorne
// Created          : 07-14-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
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
