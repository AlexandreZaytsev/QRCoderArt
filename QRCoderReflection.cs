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
    class FieldProperty                                 //to create Control WinForm
    {
        public string fName;// { get; set; }            //parameter name
        public string fType;// { get; set; }            //parameter type
        public string fForm;// { get; set; }            //control view to display on the form
        public Dictionary<string, object> fList;        //enum
        public Boolean fNull;                           //presence of zero value
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

        //get member by name
        public MemberInfo GetMemberByName(string baseName)
        {
            return tRef.GetMember(baseName).First();
        }

        //get class names named (payload)
        public List<string> GetMembersClassName(string cName)
       {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        //get the constructor dictionary for the drop-down list
        public Dictionary<string, ConstructorInfo> GetConstructor(MemberInfo mi)
        {
            return (from ctor in ((Type)mi).GetConstructors() 
                    select new { name = ctor.GetParameters().Count() == 0 ? "the constructor is not used here" : string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)), 
                                 ctor }).ToDictionary(k=>k.name, v=>v.ctor);
        }

        //initialize the constructor and execute the default method
        public string GetPayloadString(ConstructorInfo ctor, Dictionary<string, object> cntrlFromForm)
        {
            //https://metanit.com/sharp/tutorial/14.2.php
            string ret = "";

            if (ctor.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
            {
                object ctorObj = ctor.Invoke(new object[] { });
                foreach (KeyValuePair<string, object> entry in cntrlFromForm)
                {
                    ctorObj.GetType().GetProperty(entry.Key).SetValue(ctorObj, entry.Value);
                }
                try
                {
                    ret = ctor.ReflectedType.GetMethod("ToString").Invoke(ctorObj, new object[] { }).ToString();
                }
                catch (Exception e)
                {
                    ret = "\r\nInvoke Error:\r\n" + e.Message + "\r\n\r\n" +
                        "Error creating string for QR code generation:\r\n" +
                        " - no default parameters specified(QRCoder.dll)(not documented).\r\n\r\n" +
                        "Please: -initialize the required fields";
                }
                finally
                {
                }
            }
            else
            {
                if (cntrlFromForm.Count != 0) 
                {
                    //                    object[] propFromForm = cntrlFromForm.Cast<object>().ToArray();
                    object[] propFromForm = cntrlFromForm.Select(z => z.Value).ToArray();

                    try
                    {
                        object ctorObj = ctor.Invoke(propFromForm);
                        ret = ctor.ReflectedType.GetMethod("ToString").Invoke(ctorObj, null).ToString();
                    }
                    catch (Exception e)
                    {
                        ret =   "\r\nInvoke Error:\r\n" + e.Message + "\r\n\r\n" +
                                "init Constructot\r\nTry filling in the parameters...\r\n\r\n" +
                                "I haven't figured it out yet... in progress";
                    }
                    finally
                    {
                    }
                }
            }
            return ret;
        }

        //get enum
        private IDictionary<string, object> GetItemEnum(ParameterInfo param)
        {
            return param.ParameterType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
        }

        //get field property to create control form
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
                        mParam.fList = paramType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v);
                    }
                    else if (paramType.IsClass)
                    {
                        //tRef.GetNestedTypes()[4].GetNestedTypes()[2].GetNestedTypes()[0]
//                        tRef.GetNestedTypes()[4].GetNestedTypes()
                    }
                    else 
                    {
                    }
                    break;
            }
            return mParam;
        }

        //get constructor parameters
        public IList GetParamsConstuctor(ConstructorInfo ctor)
        {
            if (ctor.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
            {
                //for pure (witout k__BackingField) names here we use GetProperties()     
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


/*just links to study the question reflection
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
