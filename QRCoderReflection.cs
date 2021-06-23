using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using QRCoder;
using System.Collections;

namespace RicQRCoderArt 
{
    class FieldProperty
    {
        public string fName { get; set; }
        public string fType { get; set; }
        public List<string> fList;

    }
    public class QRCoderReflection : IDisposable
    {
        /*
        https://blog.rc21net.ru/рефлексия-отражение-reflection-в-c-sharp/
        https://johnlnelson.com/tag/assembly-gettypes/
        https://johnlnelson.com/2014/06/17/system-reflection-working-with-the-type-class/
        https://forum.unity.com/threads/c-reflection-refuses-to-give-me-private-fields.587506/
        https://docs.microsoft.com/en-us/dotnet/api/system.type.getfields?view=netframework-4.7.2
        */
        public string nameAsm;
        private Assembly Asm;
        private Type[] tAsm;
        private Type[] etAsm;
        private Module[] mAsm;
        private MethodInfo[] mtd;
        private FieldInfo[] fld;

        public QRCoderReflection(string nameAsm)
        {
            Asm = System.Reflection.Assembly.ReflectionOnlyLoad(nameAsm);
//            tAsm = Asm.GetTypes();
            etAsm = Asm.GetExportedTypes();
            //            mAsm = Asm.GetModules(true);
            //            mtd = type.GetMethods();
        }

        public void Dispose()
        {
   //         throw new NotImplementedException();
        }

        public List<string> GetTypeByBaseName(string baseName)
        {
            List<string> items = new List<string>();
            foreach (Type type in etAsm)                //foreach (MemberInfo minf in tAsm)
            {
                if ((type.BaseType.Name) == baseName && (!type.IsAbstract))                    //работаем только с метаданными родитель = "Payload"  
                {
/*
                    foreach (MemberInfo mi in type.GetMembers())
                    {
                        Console.WriteLine($"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
                    }

                    foreach (MethodInfo method in type.GetMethods())
                    {
                        string modificator = "";
                        if (method.IsStatic)
                            modificator += "static ";
                        if (method.IsVirtual)
                            modificator += "virtual";
                        Console.Write($"{modificator} {method.ReturnType.Name} {method.Name} (");
                        //получаем все параметры
                        ParameterInfo[] parameters = method.GetParameters();// BindingFlags((BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                            if (i + 1 < parameters.Length) Console.Write(", ");
                        }
                        Console.WriteLine(")");
                    }
  */                  
                    items.Add(type.Name);
                }
            }
            return items;
        }

        public MethodInfo GetMethodByBaseName(string baseName, string name)
        {
            MethodInfo method = null;
            foreach (Type type in etAsm)                //foreach (MemberInfo minf in tAsm)
            {
                if ((type.BaseType.Name) == baseName && (!type.IsAbstract) && (type.Name == name))                    //работаем только с метаданными родитель = "Payload"  
                {
                    method = type.GetMethod("ToString");// type.GetMethod("toString", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
/*
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        string modificator = "";
                        if (method.IsStatic)
                            modificator += "static ";
                        if (method.IsVirtual)
                            modificator += "virtual";
                        Console.Write($"{modificator} {method.ReturnType.Name} {method.Name} (");
                        //получаем все параметры
                        ParameterInfo[] parameters = method.GetParameters();// BindingFlags((BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                            if (i + 1 < parameters.Length) Console.Write(", ");
                        }
                        Console.WriteLine(")");
                    }
*/
                }
            }
            return method;
        }
        public IList GetFieldByBaseName(string baseName, string name)
        {
            List<FieldProperty> MyList = new List<FieldProperty>();
            foreach (Type type in etAsm)                //foreach (MemberInfo minf in tAsm)
            {
                if ((type.BaseType.Name) == baseName && (!type.IsAbstract) && (type.Name == name))                    //работаем только с метаданными родитель = "Payload"  
                {
                    FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach(var field in fields)
                    {
                        FieldProperty mProp = new FieldProperty { fName = "", fType = "", fList = new List<string>() };
                        mProp.fName = field.Name;
                        switch (field.FieldType.Name)
                        {
                            case "String":
                            case "Decimal":    
                            case "Nullable`1":
                                mProp.fType = "TextBox";
                                break;
                            case "Boolean":
                                mProp.fType = "CheckBox";
                                break;
                            default:
                                //var extType = type.GetMember(field.FieldType.Name).;
                                var nstTypes = type.GetNestedTypes();
                                foreach (var nstType in nstTypes)
                                {
                                    if(nstType.Name== field.FieldType.Name)
                                    {
                                        var items = nstType.GetFields(BindingFlags.Static | BindingFlags.Public);
 //                                       Dictionary<string,List < Object >> fDict = new Dictionary<string, List<Object>>();
                                        
                                        foreach (var item in items)
                                        {
                                            mProp.fList.Add(item.Name);
                                        }
                                        //list1 = new List<t>(list)
                                    }
                                 //   Console.WriteLine($"{nt.DeclaringType} {nt.MemberType} {nt.Name}");
                                    //      nt.GetFields()[1]
                                }
                                mProp.fType = "ComboBox";
                                break;
                        }

                        MyList.Add(mProp);
                    }
                }
            }
            return MyList;
        }
    }
}


/*
            //https://blog.rc21net.ru/рефлексия-отражение-reflection-в-c-sharp/
            //Assembly asm = System.Reflection.Assembly.Load("QRCoder");
            Assembly asm = System.Reflection.Assembly.ReflectionOnlyLoad("QRCoder");
            //            Module[] mAsm = asm.GetModules(true);
            //            Type[] tAsm = asm.GetTypes();
            Type[] etAsm = asm.GetExportedTypes();
            //https://johnlnelson.com/tag/assembly-gettypes/
            //robust code always checks for null FIRST
            if (etAsm != null && etAsm.Length > 0)
            {
                //                System.Text.StringBuilder sb = new System.Text.StringBuilder(); //we'll create a StringBuilder for our formatted output
                foreach (Type type in etAsm)                //foreach (MemberInfo minf in tAsm)
                {
                    if ((type.BaseType.Name) == "Payload" && (!type.IsAbstract))                    //работаем только с метаданными родитель = "Payload"  
                    {
                        this.cbPayload.Items.Add(type.Name);



                        //Display(0, "Member: {0}", type.Name);
                        //get the methods in the type
                        //https://johnlnelson.com/2014/06/17/system-reflection-working-with-the-type-class/
                        //https://forum.unity.com/threads/c-reflection-refuses-to-give-me-private-fields.587506/
                        //https://docs.microsoft.com/en-us/dotnet/api/system.type.getfields?view=netframework-4.7.2
                        MethodInfo[] methods = type.GetMethods();
                        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        foreach (MethodInfo method in methods)
                        {
                            if (method.DeclaringType.Name == type.Name)     //прочтем все функции объекта метаданных с нужными именами             
                            {

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
                        //                        string output = sb.ToString();
                    }

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

                }
                //                string output = sb.ToString();
            }

*/