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
    /// Class GUITreeNode
    /// узел дерева параметров GUI для создания элемента формы (GUI Control)
    /// </summary>
    public class GUITreeNode                          
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

    /// <summary>
    /// QRCoderReflection
    /// Класс дя работы с Reflection
    /// </summary>
    public class QRCoderReflection : IDisposable
    {
        /// <summary>
        /// узел (точка входа) в Reflection
        /// </summary>
        private Type tRef;

        /// <summary>
        /// инициализация узла в Reflection по имени<see cref="QRCoderReflection" /> class.
        /// </summary>
        /// <param name="name">имя узла Reflection от которого читается структура</param>
        public QRCoderReflection(string name)
        {
            tRef = Type.GetType(name);
        }

        /// <summary>
        /// выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            //         throw new NotImplementedException();
        }

        /// <summary>
        /// GetMemberByName
        /// вернуть узел по имени 
        /// </summary>
        /// <param name="baseName">имя узла Reflection</param>
        /// <returns>первый найденный узел</returns>
        public Type GetMemberByName(string baseName)
        {
            return (Type)tRef.GetMember(baseName).First();
        }

        /// <summary>
        /// GetMembersClassName
        /// вернуть все имена публичных, актуальных классов узла. Для отображения всех интерфейсов payload qrcoder.dll
        /// </summary>
        /// <param name="cName">имя узла Reflection</param>
        /// <returns>список имен payload &lt;System.String&gt;</returns>
        public List<string> GetMembersClassName(string cName)
        {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        /// <summary>
        /// GetConstructor
        /// вернуть конструкторы узла
        /// </summary>
        /// <param name="param">узел Reflection</param>
        /// <returns>словарь с конструкторами (имя значение) &lt;System.String, System.Object&gt;.</returns>
        public Dictionary<string, object> GetConstructors(Type param)
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

        /// <summary>
        /// GetParamsConstuctor
        /// дополнить общий список параметров дерева GUI - параметрами конструктора
        /// (если конструктор не объявлен (нет параметров) - читаем свойства )
        /// </summary>
        /// <param name="ctor">конструктор</param>
        /// <param name="Params">текущее дерево GIU</param>
        /// <param name="nestingLevel">текущий уровень вложенности параметра</param>
        /// <param name="parentName">текущее имя родителя параметра</param>
        private void GetParamsConstuctor(ConstructorInfo ctor, List<GUITreeNode> Params, int nestingLevel, string parentName)
        {
            //for pure (witout k__BackingField) names here we use GetProperties()  
            //from t in ctor.ReflectedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) select t

            //constrictor without parameters = there is 'no constructor'
            IEnumerable queryProp = from t in ctor.ReflectedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                    where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                    select GetGUITreeNode(t.Name, t.PropertyType, null, Params, nestingLevel, parentName);

            //constrictor with parameters = there is 'constructor'
            IEnumerable queryParam = from t in ctor.GetParameters()
                                     where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                     select GetGUITreeNode(t.Name, t.ParameterType, t.DefaultValue, Params, nestingLevel, parentName);

            //Deferred Execution
            foreach (object Param in ctor.GetParameters().Length == 0 ? queryProp : queryParam) { }  //run function from query
        }

        /**********************************************************************************************************
          EXECUTE MEMBER
        ************************************************************************************************************/
        /// <summary>
        /// GetPayloadString
        /// Выполнить базовый метод генерации строки для QR кода для выбранного payload - ToString.
        /// </summary>
        /// <param name="ctor">конструктор</param>
        /// <param name="cntrlFromForm">параметры конструктора в виде словаря имя-значение</param>
        /// <returns>форматированная строка payload System.String.</returns>
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
        /// GetGUITree
        /// вернуть все дерево GUI от узла Reflection payload
        /// для создания всей панели payload
        /// </summary>
        /// <param name="obj">payload узел Reflection</param>
        /// <returns>payload дерево GUI &lt;System.String&gt;</returns>
        public IList GetGUITree(Object obj)
        {
            List<GUITreeNode> GUITree = new List<GUITreeNode>();                           //list of parameters
            int nestingLevel = 0;                                                                   //nesting level of the parameter

            GetGUITreeNode(((Type)obj).Name, (Type)obj, null, GUITree, nestingLevel, "");    //get parameter list
            return GUITree;
        }

        /// <summary>
        /// GetGUITreeNodes
        /// вернуть фрагмент дерева GUI от конкретного конструктора
        /// для пересоздания фрагмента панели payload
        /// </summary>
        /// <param name="obj">узел конструктора Reflection</param>
        /// <param name="parentName">текущее имя родителя в дереве GUI</param>
        /// <param name="nestingLevel">текущий уровень вложенности в дереве GUI</param>
        /// <returns>IList.</returns>
        public IList GetGUITreeNodes(ConstructorInfo obj, string parentName, int nestingLevel)
        {
            List<GUITreeNode> GUITreeNodes = new List<GUITreeNode>();           //list of parameters
            GetParamsConstuctor(obj, GUITreeNodes, nestingLevel, parentName);   //!!! attention - recursion
            return GUITreeNodes;
        }

        /// <summary>
        /// GetGUITreeNode
        /// вернуть узел дерева GUI с параметрами
        /// </summary>
        /// <param name="paramName">имя параметра</param>
        /// <param name="paramType">параметр (приведен к типу Type)</param>
        /// <param name="defValue">значение по умолчанию</param>
        /// <param name="Params">текущее дерево GUI</param>
        /// <param name="nestingLevel">текущий уровень вложенности в дереве GUI</param>
        /// <param name="paramParent">текущее имя родителя в дереве GUI</param>
        /// <returns>System.Int32.</returns>
        private int GetGUITreeNode(string paramName, Type paramType, object defValue, List<GUITreeNode> Params, int nestingLevel, string paramParent)
        {
            GUITreeNode mParam = new GUITreeNode();// { fName = paramName, fType = paramType.Name, fForm = "TextBox", fList = null, fNull = false, fDef = defValue, fLevel = nestingLevel };
            if (paramType.IsClass && paramType.Namespace != "System" && !paramType.IsGenericType)
            {
                if (nestingLevel > 1)
                    nestingLevel--;

                mParam.fParentName = nestingLevel == 0 ? "" : paramParent;// paramName; ;
                mParam.fName = paramName; //"ctor_" +paramName;
                mParam.fType = "Constructor";
                mParam.fForm = "ComboBox";
                mParam.fList = GetConstructors(paramType);// (((Type)Param));
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

