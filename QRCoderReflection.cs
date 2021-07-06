using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using QRCoder;
using System.Collections;
using System.Windows.Forms;

namespace QRCoderArt
{
    class FieldProperty                                 //для создания Control WinFoer
    {
        public string fName;// { get; set; }            //имя параметра
        public string fType;// { get; set; }            //тип параметра
        public string fForm;// { get; set; }            //вид контрола для отображение на форме
        public Dictionary<string, object> fList;        //enum
        public Boolean fNull;                           //нулевое значение
        public object fDef;                             //default value    
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

        //получить member по имени
        public MemberInfo GetMemberByName(string baseName)
        {
            return tRef.GetMember(baseName).First();
        }

        //получить имена классов по имени payload
        public List<string> GetMembersClassName(string cName)
       {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        //получить словарь конструкторов для выпадающего списка
        public Dictionary<string, ConstructorInfo> GetConstructor(MemberInfo mi)
        {
            return (from ctor in ((Type)mi).GetConstructors()
                    select new { V = ctor.GetParameters().Count()==0? "the constructor is not used here" : string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)), ctor }).ToDictionary(k => k.V, v => v.ctor);
        }

        //инициализировать конструктор и выполнить метод по умолчанию
        public string GetPayloadString(ConstructorInfo ctor, ArrayList cntrlFromForm)
        {
            //https://metanit.com/sharp/tutorial/14.2.php
            string ret = "";
            if (cntrlFromForm.Count != 0) 
            {
                object[] propFromForm = cntrlFromForm.Cast<object>().ToArray();

                try
                {
                    object ctorObj = ctor.Invoke(propFromForm);
                    MethodInfo baseMethod = ctor.ReflectedType.GetMethod("ToString");
                    ret = baseMethod.Invoke(ctorObj, null).ToString();
                }
                catch (Exception e)
                {
                    ret =   "Error:\r\n" + e.Message + "\r\n\r\n" +
                            "init Constructot\r\nTry filling in the parameters...\r\n\r\n" +
                            "I haven't figured it out yet... in progress";
                }
                finally
                {
                }
            }
            return ret;
        }

        //получить enum
        private IDictionary<string, object> GetItemEnum(ParameterInfo param)
        {
            return param.ParameterType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
        }
        //получить свойство поля для формы
        private FieldProperty GetItemInfoForForm(string paramName, Type paramType, object defValue, object src) 
        {
            FieldProperty mParam = new FieldProperty { fName = paramName, fType = paramType.Name, fForm = "TextBox", fList = null , fNull=false, fDef= defValue};
            switch (paramType.Name)
            {
                case "String":
                case "Double":
                case "Single":
                case "Int32":
                case "Decimal":
                    mParam.fForm = "TextBox";
                    break;
                case "DateTime":
                    mParam.fForm = "DateTime";
                    break;
                case "Nullable`1":
                    mParam.fType = paramType.GenericTypeArguments.First().Name;
                    mParam.fNull = true;
                    switch (mParam.fType)
                    {
                        case "DateTime":
                            mParam.fForm = "DateTime";
                            break;
                        default:
                            mParam.fForm = "TextBox";
                            break;
                    }
                    break;
                case "Boolean":
                    mParam.fForm = "CheckBox";
                    break;
                default:
                    if (paramType.IsEnum)
                    {
                        mParam.fForm = "ComboBox";
//                        mParam.fList = (Dictionary<string, object>)GetItemEnum(param);
                        mParam.fList = paramType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v);
                    }
                    else if (paramType.IsClass)
                    {
                    }
                    else 
                    {
                    }
                    break;
            }
            return mParam;
        }
           
        //получить parameters конструктора
        public IList GetParamsConstuctor(ConstructorInfo ctor)
        {
            if (ctor.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
            {
                // return (from t in ctor.ReflectedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) select GetItemInfoForForm(t.Name, t.FieldType, null, t)).ToList();
                return (from t in ctor.ReflectedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                       where !t.IsDefined(typeof(ObsoleteAttribute), true)
                      select GetItemInfoForForm(t.Name, t.PropertyType, null, t)).ToList(); 
            }
            else                                            //constrictor with parameters = there is a constructor
            { 
                return (from t in ctor.GetParameters()
                        where !t.IsDefined(typeof(ObsoleteAttribute), true)
                        select GetItemInfoForForm(t.Name, t.ParameterType, t.DefaultValue, t)).ToList();
            }
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

        https://question-it.com/questions/848396/preobrazovanie-fieldinfo-v-propertyinfo-ili-naoborot
        https://fooobar.com/questions/273903/how-to-get-both-fields-and-properties-in-single-call-via-reflection

 */
