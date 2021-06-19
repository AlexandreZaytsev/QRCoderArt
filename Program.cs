using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using QRCoder;

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
            //https://blog.rc21net.ru/рефлексия-отражение-reflection-в-c-sharp/
            //Assembly asm = System.Reflection.Assembly.Load("QRCoder");
            Assembly asm = System.Reflection.Assembly.ReflectionOnlyLoad("QRCoder");
            Type[] tAsm = asm.GetTypes();
            Module[] mAsm = asm.GetModules(true);
            Type[] etAsm = asm.GetExportedTypes();
      //      MemberInfo[] members = typeof(tAsm).GetMembers();
            foreach (MemberInfo minf in etAsm)
            {
                //((System.RuntimeType)((System.RuntimeType)etAsm[25]).BaseType).Name 		Name	"Payload"	string
                //       if (minf.GetCustomAttributes().BaseType.Name== "Payload")
                //      {

                //    }
//                DisplayAttributes(4, minf);
                var attrs = minf.GetCustomAttributesData();
                Display(0,"Member: {0}", minf.Name);
       //         Display(1,minf);

                // If the member is a method, display information about its parameters.

                if (minf.MemberType == MemberTypes.Method)
                {
                    foreach (ParameterInfo pinf in ((MethodInfo)minf).GetParameters())
                    {
                        Console.Write("Parameter: Type={0}, Name={1}", pinf.ParameterType, pinf.Name);
                    }
                }

                // If the member is a property, display information about the property's accessor methods.
                if (minf.MemberType == MemberTypes.Property)
                {
                    foreach (MethodInfo aminf in ((PropertyInfo)minf).GetAccessors())
                    {
                        Console.Write("Accessor method: {0}", aminf);
                    }
                }
            }


                Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /*
                public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
                {
                    return
                      assembly.GetTypes()
                              .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                              .ToArray();
                }
        */

        // Displays the custom attributes applied to the specified member.
        public static void DisplayAttributes(Int32 indent, MemberInfo mi)
        {
            // Get the set of custom attributes; if none exist, just return.
            object[] attrs = mi.GetCustomAttributes(false);
            if (attrs.Length == 0) { return; }

            // Display the custom attributes applied to this member.
            Display(indent + 1, "Attributes:");
            foreach (object o in attrs)
            {
                Display(indent + 2, "{0}", o.ToString());
            }
        }

        // Display a formatted string indented by the specified amount.
        public static void Display(Int32 indent, string format, params object[] param)

        {
//            System.Diagnostics.Debug.WriteLine(new string(' ', indent * 2));
//            System.Diagnostics.Debug.WriteLine(format, param);
            Console.Write(new string(' ', Math.Abs(indent) * 2));
            Console.WriteLine(format, param);
        }

    }

    }
