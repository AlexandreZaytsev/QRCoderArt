using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace RicQRCoderArt
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {


          
/*
            //"TestReflection" искомое пространство
            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "RicQRCoderArt").ToArray();
            foreach (Type type in typelist)
            {
                //создание объекта
                object targetObject = Activator.CreateInstance(Type.GetType(type.FullName));

                //что бы получить public методы без базовых(наследованных от object)
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var methodInfo in methods)
                {
                    //вызов
             //       methodInfo.Invoke(targetObject, new object[] { });
                }
            }
*/







            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

    }
}
