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
    public class FieldProperty                          //info to create Control WinForm
    {
        public int fLevel;                              //nesting level of the parameter
        public string fParentName;                      //parentName (parameter block name) 
        public string fName;                            //name parameter 
        public string fType;                            //parameter data type name 
        public string fForm;                            //name of the control type to display on the winform
        public Dictionary<string, object> fList;        //winform control data source (for combobox)
        public Boolean fNull;                           //presence of zero value (for checkbox)
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
        public Type GetMemberByName(string baseName)
        {
            return (Type)tRef.GetMember(baseName).First();
        }

        //get class names named (payload)
        public List<string> GetMembersClassName(string cName)
        {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        //get constructor dictionary
        public Dictionary<string, object> GetConstructor(Type param)
        {
            return (from ctor in param.GetConstructors()
                    select new
                    {
                        name = ctor.GetParameters().Count() == 0 ? "the constructor is not used here" : string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)),
                        ctor
                    }).ToDictionary(k => k.name, v => (Object)v.ctor);
        }

        //get enum dictionary
        private IDictionary<string, object> GetItemEnum(ParameterInfo param)
        {
            return param.ParameterType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
        }

        //get constructor parameters
        private void GetParamsConstuctor(ConstructorInfo ctor, List<FieldProperty> Params, int nestingLevel, string parentName)
        {
            //for pure (witout k__BackingField) names here we use GetProperties()  
            //from t in ctor.ReflectedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) select t

            //constrictor without parameters = there is 'no constructor'
            IEnumerable queryProp = from t in ctor.ReflectedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                    where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                   select GetItemInfoForForm(t, t.Name, t.PropertyType, null, Params, nestingLevel, parentName);

            //constrictor with parameters = there is 'constructor'
            IEnumerable queryParam = from t in ctor.GetParameters()
                                     where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                    select GetItemInfoForForm(t, t.Name, t.ParameterType, t.DefaultValue, Params, nestingLevel, parentName);

            //Deferred Execution
            foreach (object Param in ctor.GetParameters().Length == 0 ? queryProp : queryParam){ }  //run function from query
        }

        /**********************************************************************************************************
          EXECUTE MEMBER
        ************************************************************************************************************/
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
                        ret = "\r\nInvoke Error:\r\n" + e.Message + "\r\n\r\n" +
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

        /**********************************************************************************************************
          START
        ************************************************************************************************************/
        /// <summary>
        /// get list field propertys to create All control form member
        /// input - member (name payload) reflection,
        /// returns an list parameter member for create winform panel payload.
        /// </summary>
        public IList GetParamsObject(Object obj)
        {
            List<FieldProperty> Params = new List<FieldProperty>();                                 //list of parameters
            int nestingLevel = 0;                                                                   //nesting level of the parameter

            GetItemInfoForForm(obj, ((Type)obj).Name, (Type)obj, null, Params, nestingLevel,"");    //get parameter list
            return Params;
        }
        /// <summary>
        /// get list field propertys to create one control form constructor
        /// input - constructor
        /// returns an list parameter member for create winform panel payload.
        /// </summary>
        public IList GetParamsCtor(ConstructorInfo obj, string parentName, int nestingLevel)
        {
            List<FieldProperty> Params = new List<FieldProperty>();                                 //list of parameters
            GetParamsConstuctor(obj, Params, nestingLevel, parentName);   //!!! attention - recursion
            return Params;
        }

        /// <summary>
        /// get field property to create control form
        /// input - member (name payload) reflection,
        /// returns an list parameter member for create winform panel payload.
        /// </summary>
        private int GetItemInfoForForm(object Param, string paramName, Type paramType, object defValue, List<FieldProperty> Params, int nestingLevel, string paramParent)
        {
            FieldProperty mParam = new FieldProperty();// { fName = paramName, fType = paramType.Name, fForm = "TextBox", fList = null, fNull = false, fDef = defValue, fLevel = nestingLevel };
            if (paramType.IsClass && paramType.Namespace != "System" && !paramType.IsGenericType)
            {
                if (nestingLevel > 1)
                    nestingLevel--;

                mParam.fParentName = nestingLevel == 0 ? "" : paramParent;// paramName; ;
                mParam.fName = paramName; //"ctor_" +paramName;
                mParam.fType = "Constructor";
                mParam.fForm = "ComboBox";
                mParam.fList = GetConstructor(paramType);// (((Type)Param));
                                                         //                mParam.fNull
                                                         //                mParam.fDef

 //               nestingLevel++;
                mParam.fLevel = nestingLevel;

                Params.Add(mParam);
                nestingLevel ++;

                GetParamsConstuctor((ConstructorInfo)mParam.fList.Values.First(), Params, nestingLevel, mParam.fName);   //!!! attention - recursion
                /*
                foreach (var ctor in mParam.fList.Values)
                {
                    GetParamsConstuctor((ConstructorInfo)ctor, Params, nestingLevel, mParam.fParentName);   //!!! attention - recursion
                }
                */
            }
            else
            {
                mParam.fParentName = paramParent;
                mParam.fName = paramName;
                mParam.fType = paramType.Name;
//                mParam.fForm 
//                mParam.fList 
//                mParam.fNull 
                mParam.fDef = defValue;
                mParam.fLevel = nestingLevel;

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
//                    case "Dictionary`2":
//                        break:
                    case "Boolean":
                        mParam.fForm = "CheckBox";
                        break;
                    default:
                        if (paramType.IsEnum)
                        {
                            mParam.fForm = "ComboBox";
                            mParam.fList = paramType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v);
                        }
                        else if (paramType.IsGenericType)
                        {
                            //!!! Exception
                            //https://stackoverflow.com/questions/577092/c-sharp-gui-control-for-editing-a-dictionary
                            if (paramParent == "ShadowSocksConfig" && paramName == "parameters")
                            {
                                //mParam.fForm = "Button"; //"Dictionary`2"
                                //mParam.fList = (new Dictionary<string, string> {["plugin"] = "plugin" + (string.IsNullOrEmpty("pluginOption") ? "" : $";" + $"{"pluginOption"}")}).Values.Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
                                mParam.fForm = "dataGridView";
                            }
                        }
                        else 
                        {
                        }
                        break;
                }
                Params.Add(mParam);
            }
            return Params.Count;
        }







    }
}

