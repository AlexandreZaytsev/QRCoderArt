using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using QRCoder;
using System.Runtime;


//using System.Collections.Generic;
//using System.Reflection;
//using ValidationFramework.Extensions;
//using ValidationFramework.Reflection;

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
            //      var b = HasCurrentTag(asm);
 //           var st = (typeof)asm.GetTypes()[36].BaseType.Name;
            Module[] mAsm = asm.GetModules(true);
            Type[] tAsm = asm.GetTypes();
            Type[] etAsm = asm.GetExportedTypes();
            //            https://johnlnelson.com/tag/assembly-gettypes/
            //robust code always checks for null FIRST
            if (etAsm != null && etAsm.Length > 0)
            {
                //we'll create a StringBuilder for our formatted output
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //iterate through the Type[] array
//                foreach (MemberInfo minf in tAsm)
                foreach (Type type in tAsm)
                {
                    if (type.BaseType.Name == "Payload")
                    {
                        Display(0, "Member: {0}", type.Name);
                  //      IEnumerable<FieldInfo> fld = type.DeclaredFields.Count;

                        //get the methods in the type
                        MethodInfo[] methods = type.GetMethods();
                        // type.DeclaringMethod.

                        //https://johnlnelson.com/2014/06/17/system-reflection-working-with-the-type-class/
                        //iterate the methods and write output
                        //                        foreach (MethodInfo method in methods)

                        //                        foreach (MethodInfo method in type.GetDeclaredMethods())
                        //  foreach (FieldInfo field in type.GetDeclaredFields())
                        //https://stackoverflow.com/questions/16966629/what-is-the-difference-between-getfields-and-getdeclaredfields-in-java-reflectio
                        //  {
                        //  }

                        foreach (MethodInfo method in methods)

                            {
                                if (method.DeclaringType.Name == type.Name)
                            {
                         //       DeclaredFields
                         //       System.Reflection.FieldInfo df= method.GetDeclaredField(string "name");
                                sb.AppendLine("=1===============================================================");
                                sb.AppendLine(String.Format("Method Name: {0}", method.DeclaringType.Name));
                           
                            sb.AppendLine("================================================================");
                            sb.AppendLine(String.Format("Method Name: {0}", method.Name));
                            sb.AppendLine("================================================================");

                            sb.AppendLine(String.Format("Contains Generic Parameters: {0}", method.ContainsGenericParameters.ToString()));
                            sb.AppendLine(String.Format("Is Abstract?: {0}", method.IsAbstract.ToString()));

                            sb.AppendLine(String.Format("Is a Constructor?: {0}", method.IsConstructor.ToString()));
                            sb.AppendLine(String.Format("Is it Generic?: {0}", method.IsGenericMethod.ToString()));

                            sb.AppendLine(String.Format("Is it Private?: {0}", method.IsPrivate.ToString()));
                            sb.AppendLine(String.Format("Is it Public?: {0}", method.IsPublic.ToString()));
                            sb.AppendLine(String.Format("Is it Static?: {0}", method.IsStatic.ToString()));
                            sb.AppendLine(String.Format("Is is Virtual?: {0}", method.IsVirtual.ToString()));

                            //if the method is a void, the Return type will be null
                            //otherwise, it will return a Type
                            if (method.ReturnType != null && !String.IsNullOrEmpty(method.ReturnType.Name))
                            {
                                sb.AppendLine(String.Format("Return Type: {0}", method.ReturnType.Name.ToString()));
                            }

                            }
                            //there are more properties of the MethodInfo you could output...
                        }

                        string output = sb.ToString();


                    }
                    /*
                    sb.AppendLine("===============================================================");
                    sb.AppendLine(String.Format("Type Name: {0}", type.Name));
                    sb.AppendLine("===============================================================");

                    sb.AppendLine(String.Format("Type FullName: {0}", type.FullName));
                    sb.AppendLine(String.Format("Namespace: {0}", type.Namespace));

                    sb.AppendLine(String.Format("Is it a Class?: {0}", type.IsClass.ToString()));
                    sb.AppendLine(String.Format("Is it an Interface?: {0}", type.IsInterface.ToString()));
                    sb.AppendLine(String.Format("Is it Generic?: {0}", type.IsGenericType.ToString()));
                    sb.AppendLine(String.Format("Is it Public?: {0}", type.IsPublic.ToString()));
                    sb.AppendLine(String.Format("Is it Sealed?: {0}", type.IsSealed.ToString()));

                    sb.AppendLine(String.Format("Qualified Name: {0}", type.AssemblyQualifiedName));

                    if (type.BaseType != null && !String.IsNullOrEmpty(type.BaseType.Name))
                    {
                        sb.AppendLine(String.Format("Base Type: {0}", type.BaseType.Name));
                    }

                    //there are many, many more properties that an be shown...
*/
                }
//                string output = sb.ToString();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
