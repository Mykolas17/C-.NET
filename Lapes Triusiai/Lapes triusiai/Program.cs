using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lapes_triusiai
{
    internal static class Program
    {
        //static void Main(string[] args)
        //{
        //    var a = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        //    for (int i = 0; i < a.Count; i++)
        //    {
        //        if (a[i] % 2 == 0) 
        //        { 
        //            a.RemoveAt(i);
        //            i--;
        //        }  

        //    }    
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
