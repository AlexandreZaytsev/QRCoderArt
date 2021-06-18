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

            // This variable holds the amount of indenting that
            // should be used when displaying each line of information.
            Int32 indent = 0;
            // Display information about the EXE assembly.
            Assembly a = typeof(Program).Assembly;
            Display(indent, "Assembly identity={0}", a.FullName);
            Display(indent + 1, "Codebase={0}", a.CodeBase);

            // Display the set of assemblies our assemblies reference.

            Display(indent, "Referenced assemblies:");
            foreach (AssemblyName an in a.GetReferencedAssemblies())
            {
                Display(indent + 1, "Name={0}, Version={1}, Culture={2}, PublicKey token={3}", an.Name, an.Version, an.CultureInfo.Name, (BitConverter.ToString(an.GetPublicKeyToken())));
            }
            Display(indent, "");

            // Display information about each assembly loading into this AppDomain.
            foreach (Assembly b in AppDomain.CurrentDomain.GetAssemblies())
            {
                Display(indent, "Assembly: {0}", b);

                // Display information about each module of this assembly.
                foreach (Module m in b.GetModules(true))
                {
                    Display(indent + 1, "Module: {0}", m.Name);
                }

                // Display information about each type exported from this assembly.

                indent += 1;
                foreach (Type t in b.GetExportedTypes())
                {
                    Display(0, "");
                    Display(indent, "Type: {0}", t);

                    // For each type, show its members & their custom attributes.

                    indent += 1;
                    foreach (MemberInfo mi in t.GetMembers())
                    {
                        Display(indent, "Member: {0}", mi.Name);
                        DisplayAttributes(indent, mi);

                        // If the member is a method, display information about its parameters.

                        if (mi.MemberType == MemberTypes.Method)
                        {
                            foreach (ParameterInfo pi in ((MethodInfo)mi).GetParameters())
                            {
                                Display(indent + 1, "Parameter: Type={0}, Name={1}", pi.ParameterType, pi.Name);
                            }
                        }

                        // If the member is a property, display information about the property's accessor methods.
                        if (mi.MemberType == MemberTypes.Property)
                        {
                            foreach (MethodInfo am in ((PropertyInfo)mi).GetAccessors())
                            {
                                Display(indent + 1, "Accessor method: {0}", am);
                            }
                        }
                    }
                    indent -= 1;
                }
                indent -= 1;
            }
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
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(format, param);
        }

    }

    }
