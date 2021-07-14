// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : zaytsev
// Created          : 07-14-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="QRCoderReflection.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QRCoderArt
{

    /// <summary>
    /// Class FieldProperty.
    /// структура (дерево) параметров для создания элемента формы
    /// </summary>
    public class FieldProperty                          
    {
        /// <summary>
        /// уровень вложенности параметра в дереве
        /// </summary>
        public int fLevel;
        /// <summary>
        /// имя родителя параметра
        /// </summary>
        public string fParentName;                      
        /// <summary>
        /// имя параметра
        /// </summary>
        public string fName;                            
        /// <summary>
        /// наименование типа данных параметра (строка: 'String', 'Integer', и т.д.)
        /// </summary>
        public string fType;                             
        /// <summary>
        /// наименование типа элемента формы (строка: 'TextBox'; 'ConboBox', и т.д.)
        /// </summary>
        public string fForm;  
        /// <summary>
        /// расширенные данные (Словарь) - используется для для datasource ComboBox
        /// </summary>
        public Dictionary<string, object> fList;        
        /// <summary>
        /// признак наличия нулевого значения параметра - используется для CheckBox
        /// </summary>
        public Boolean fNull;                          
        /// <summary>
        /// значение параметра по умолчанию
        /// </summary>
        public object fDef;                    
    }

    //QRCoderReflection
    /// <summary>
    /// Class FieldProperty to work with reflection
    /// просто вынесено в отдельный модуль
    /// </summary>
    public class QRCoderReflection : IDisposable
    {
        /// <summary>
        /// The t reference
        /// </summary>
        private Type tRef;
        /// <summary>
        /// Initializes a new instance of the <see cref="QRCoderReflection" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public QRCoderReflection(string name)
        {
            tRef = Type.GetType(name);
        }
        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            //         throw new NotImplementedException();
        }

        //get member by name
        /// <summary>
        /// Gets the name of the member by.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <returns>Type.</returns>
        public Type GetMemberByName(string baseName)
        {
            return (Type)tRef.GetMember(baseName).First();
        }

        //get class names named (payload)
        /// <summary>
        /// Gets the name of the members class.
        /// </summary>
        /// <param name="cName">Name of the c.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetMembersClassName(string cName)
        {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        //get constructor dictionary
        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        public Dictionary<string, object> GetConstructor(Type param)
        {
            return (from ctor in param.GetConstructors()
                    select new
                    {
                        name = ctor.GetParameters().Count() == 0 ? "the constructor is not used here" : string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)),
                        ctor
                    }).ToDictionary(k => k.name, v => (Object)v.ctor);
        }

        /*
                //get enum dictionary
                private IDictionary<string, object> GetItemEnum(ParameterInfo param)
                {
                    return param.ParameterType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
                }
        */

        //get constructor parameters
        /// <summary>
        /// Gets the parameters constuctor.
        /// </summary>
        /// <param name="ctor">The ctor.</param>
        /// <param name="Params">The parameters.</param>
        /// <param name="nestingLevel">The nesting level.</param>
        /// <param name="parentName">Name of the parent.</param>
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
            foreach (object Param in ctor.GetParameters().Length == 0 ? queryProp : queryParam) { }  //run function from query
        }

        /**********************************************************************************************************
          EXECUTE MEMBER
        ************************************************************************************************************/
        //initialize the constructor and execute the default method
        /// <summary>
        /// Gets the payload string.
        /// </summary>
        /// <param name="ctor">The ctor.</param>
        /// <param name="cntrlFromForm">The CNTRL from form.</param>
        /// <returns>System.String.</returns>
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
        /// </summary>
        /// <param name="obj">(object) member (name payload) reflection</param>
        /// <returns>list parameter member for create winform panel payload</returns>
        public IList GetParamsObject(Object obj)
        {
            List<FieldProperty> Params = new List<FieldProperty>();                                 //list of parameters
            int nestingLevel = 0;                                                                   //nesting level of the parameter

            GetItemInfoForForm(obj, ((Type)obj).Name, (Type)obj, null, Params, nestingLevel, "");    //get parameter list
            return Params;
        }

        /// <summary>
        /// Gets the parameters ctor.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="parentName">Name of the parent.</param>
        /// <param name="nestingLevel">The nesting level.</param>
        /// <returns>IList.</returns>
        public IList GetParamsCtor(ConstructorInfo obj, string parentName, int nestingLevel)
        {
            List<FieldProperty> Params = new List<FieldProperty>();                                 //list of parameters
            GetParamsConstuctor(obj, Params, nestingLevel, parentName);   //!!! attention - recursion
            return Params;
        }


        /// <summary>
        /// Gets the item information for form.
        /// </summary>
        /// <param name="Param">The parameter.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramType">Type of the parameter.</param>
        /// <param name="defValue">The definition value.</param>
        /// <param name="Params">The parameters.</param>
        /// <param name="nestingLevel">The nesting level.</param>
        /// <param name="paramParent">The parameter parent.</param>
        /// <returns>System.Int32.</returns>
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
                nestingLevel++;

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

