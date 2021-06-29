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
        private Type tRef;

        public QRCoderReflection(string name)
        {
            tRef = Type.GetType(name);
        }

        public void Dispose()
        {
            //         throw new NotImplementedException();
        }

        //получить словарь конструкторов для выпадающего списка
        public Dictionary<string, ConstructorInfo> GetConstructor(MemberInfo mi)
        {
            //https://metanit.com/sharp/tutorial/14.2.php
            return (from ctor in ((Type)mi).GetConstructors()
                    select new { V = string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)), ctor }).ToDictionary(k => k.V, v => v.ctor);
        }

        //получить member по имени
        public MemberInfo GetMemberByName(string baseName)
        {

            MemberInfo mmbers = tRef.GetMember(baseName).First();
            //         var c = GetConstructor(mmbers);

            /*
                        //            https://www.nookery.ru/understand-with-reflection/
                        Type mi = Asm.GetType("QRCoder.PayloadGenerator");
                        MemberInfo[] members = mi.GetMember(baseName);
                        MethodInfo method = mi.GetMethod(baseName);
                        ParameterInfo[] parameters = method.GetParameters();
                        // Выводим некоторые характеристики каждого из параметров. 
                        foreach (ParameterInfo parameter in parameters)
                        {
                            Console.WriteLine("Имя параметра: {0}", parameter.Name);
                            Console.WriteLine("Позиция в методе: {0}", parameter.Position);
                            Console.WriteLine("Тип параметра: {0}", parameter.ParameterType);
                        }

                        */
            //           MethodInfo mt=null;//= etAsm..ge
            return mmbers;
        }
        //получить имена классов payload
        public List<string> GetNameMembersClass()
        {
            return (from t in tRef.GetMembers(BindingFlags.Public) // определяем каждый объект из teams как t
                    where !((System.Type)t).IsAbstract //фильтрация по критерию
                                                       //  orderby t.Name descending // упорядочиваем по возрастанию
                    select t.Name).ToList();
        }

        //получить fields member
        public IList GetFieldMember(MemberInfo mi)
        {
            FieldInfo[] fields = ((System.Type)mi).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            string ctrlType = "";
            // enum extType;

            List<FieldProperty> MyList = new List<FieldProperty>();
            foreach (FieldInfo field in fields)
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
                        mProp.fType = "TextBox";
                        /*
                        //var extType = type.GetMember(field.FieldType.Name).;
                        var nstTypes = type.GetNestedTypes();
                        foreach (var nstType in nstTypes)
                        {
                            if (nstType.Name == field.FieldType.Name)
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
                        */
                        break;

                }

                MyList.Add(mProp);
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