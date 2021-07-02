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
        public string fName;// { get; set; }            //имя параметра
        public string fType;// { get; set; }            //тип параметра
        public string fForm;// { get; set; }            //вид контрола для отображение на форме
        public Dictionary<string, object> fList;
    }
    public class QRCoderReflection : IDisposable
    {
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
            return (from ctor in ((Type)mi).GetConstructors()
                    select new { V = string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)), ctor }).ToDictionary(k => k.V, v => v.ctor);
        }

        //инициализировать конструктор и выполнить метод по умолчанию
        public string GetPayloadString(ConstructorInfo ctor, ArrayList ctorP)
        {
            object ctorObj = ctor.Invoke(ctorP.Cast<object>().ToArray());
            MethodInfo baseMethod = ctor.ReflectedType.GetMethod("ToString");
            return baseMethod.Invoke(ctorObj, null).ToString();
        }

        //получить member по имени
        public MemberInfo GetMemberByName(string baseName)
        {
            MemberInfo mmbers = tRef.GetMember(baseName).First();
            return mmbers;
        }
        //получить имена классов payload
        public List<string> GetNameMembersClass()
        {
            //найдем абстрактный класс (payload)
            string baseName = (from t in tRef.GetMembers(BindingFlags.Public) 
                              where ((System.Type)t).IsAbstract 
                             select t.Name).First();
            //вернем все классы на его основе
            return (from t in tRef.GetMembers(BindingFlags.Public) 
                   where (!((System.Type)t).IsAbstract) && 
                         ((System.Type)t).BaseType.Name == baseName 
                  select t.Name).ToList();
        }

        //получить parameters конструктора
        public IList GetParamsConstuctor(ConstructorInfo ctor)
        {
            List<FieldProperty> MyList = new List<FieldProperty>();
            foreach (ParameterInfo param in ctor.GetParameters())
            {
                FieldProperty mParam = new FieldProperty { fName = param.Name, fType = param.ParameterType.Name, fForm= "TextBox", fList = new Dictionary<string, object>() };
                switch (param.ParameterType.Name)
                {
                    case "String":
                    case "Decimal":
                    case "Nullable`1":
                        mParam.fForm = "TextBox";
                        break;
                    case "Boolean":
                        mParam.fForm = "CheckBox";
                        break;
                    default:
                        if (param.ParameterType.IsEnum) 
                        {
                            mParam.fForm = "ComboBox";
                            foreach (var val in param.ParameterType.GetEnumValues())
                                mParam.fList.Add(val.ToString(), val);
                        }
                        if (param.ParameterType.IsClass) 
                        {
                        }
                        break;
                }

                MyList.Add(mParam);
            }
            return MyList;          
        }

        //получить fields member
        public IList GetFieldMember(MemberInfo mi)
        {
            FieldInfo[] fields = ((System.Type)mi).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            string ctrlType = "";
            // enum extType;

            List<FieldProperty> MyList = new List<FieldProperty>();
            /*

                        foreach (FieldInfo field in fields)
                        {
                            FieldProperty mProp = new FieldProperty { fName = "", fType = "", fList = new List<string, object>() };
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
            //                        mProp.fType = "TextBox";

                                    //var extType = type.GetMember(field.FieldType.Name).;
                                    var nstTypes = ((Type)mi).GetNestedTypes();
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

                                    break;

                            }

                            MyList.Add(mProp);
                        }
            */
            return MyList;
        }
    }
}


/*
        https://blog.rc21net.ru/рефлексия-отражение-reflection-в-c-sharp/

        https://johnlnelson.com/tag/assembly-gettypes/
        https://johnlnelson.com/2014/06/17/system-reflection-working-with-the-type-class/

        https://forum.unity.com/threads/c-reflection-refuses-to-give-me-private-fields.587506/

        https://docs.microsoft.com/en-us/dotnet/api/system.type.getfields?view=netframework-4.7.2

        https://metanit.com/sharp/tutorial/14.2.php
        https://www.nookery.ru/understand-with-reflection/

        https://www.thebestcsharpprogrammerintheworld.com/2017/01/29/using-linq-with-reflection-in-c/

*/
