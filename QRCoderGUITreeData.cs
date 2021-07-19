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
    /// <summary>Class InvokeError.</summary>
    public class InvokeError
    {
        public InvokeError(string name)
        {
            ConstructorName = name;
        }
        /// <summary>Gets or sets the name of the constructor.</summary>
        /// <value>The name of the constructor.</value>
        public string ConstructorName { get; set; }
        /// <summary>Gets or sets the errors.</summary>
        /// <value>The errors.</value>
        public List<string> Errors { get; set; } = new List<string>();
        /// <summary>Adds the MSG.</summary>
        /// <param name="val">The value.</param>
        public void AddMsg(string val)
        {
            Errors.Add(val);
        }
    }
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
    /// GUITree
    /// Класс ддя работы с Reflection crqoder.dll
    /// </summary>
    public class GUITree : IDisposable
    {
        /// <summary>
        /// узел (точка входа) в Reflection
        /// </summary>
        private readonly Type tRef;

        /// <summary>
        /// инициализация узла в Reflection по имени<see cref="GUITree" /> class.
        /// </summary>
        /// <param name="name">имя узла Reflection от которого читается структура</param>
        public GUITree(string name)
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

            return (from ctor in param.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                where !ctor.IsDefined(typeof(ObsoleteAttribute), true)
                                select new
                                {
                                    name = ((ConstructorInfo)ctor).GetParameters().Count() == 0 ? 
                                        "the constructor is not used here" : 
                                        string.Join(", ", ((ConstructorInfo)ctor).GetParameters().Select(pr => pr.Name)),
                                    ctor
                                }).ToDictionary(k => k.name, v => (Object)v.ctor);
/*            
            return (from ctor in param.GetConstructors()
              //      where !ctor.IsDefined(typeof(ObsoleteAttribute), true)
                    select new
                    {
                        name = ctor.GetParameters().Count() == 0 ? "the constructor is not used here" : string.Join(", ", ctor.GetParameters().Select(pr => pr.Name)),
                        ctor
                    }).ToDictionary(k => k.name, v => (Object)v.ctor);
*/
        }

        //get enum dictionary
         private IDictionary<string, object> GetParamEnum(Type param)
         {
//            return param.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString(), v => v);
            return (from t in param.GetFields(BindingFlags.Static | BindingFlags.Public)
                          where !t.IsDefined(typeof(ObsoleteAttribute), true)
                          select new { v = t.Name, k = t.GetValue(new object()) }).ToDictionary(k => k.v, v => v.k);
        }

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
            foreach (object Param in ctor.GetParameters().Length == 0 ? queryProp : queryParam) {}  //run function from query
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
            List<GUITreeNode> GUITree = new List<GUITreeNode>();                                //list of parameters
            int nodeNestingLevel = 0;                                                           //nesting level of the parameter

            GetGUITreeNode(((Type)obj).Name, (Type)obj, null, GUITree, nodeNestingLevel, "");   //get parameter list
            return GUITree;
        }

        /// <summary>
        /// GetGUITreeNodes
        /// вернуть фрагмент дерева GUI от конкретного конструктора
        /// для пересоздания фрагмента панели payload
        /// </summary>
        /// <param name="obj">узел конструктора Reflection</param>
        /// <param name="nodeParentName">имя узла родителя</param>
        /// <param name="nodeNestingLevel">текущий уровень вложенности узла в дереве GUI</param>
        /// <returns>фрагмент дерева GUI IList.</returns>
        public IList GetGUITreeNodes(ConstructorInfo obj, string nodeParentName, int nodeNestingLevel)
        {
            List<GUITreeNode> GUITreeNodes = new List<GUITreeNode>();                   //list of parameters
            GetParamsConstuctor(obj, GUITreeNodes, nodeNestingLevel, nodeParentName);   //!!! attention - recursion
            return GUITreeNodes;
        }

        /// <summary>
        /// GetGUITreeNode
        /// вернуть узел дерева GUI с параметрами
        /// </summary>
        /// <param name="nodeName">имя параметра</param>
        /// <param name="nodeType">параметр (приведен к типу Type)</param>
        /// <param name="nodeDefValue">значение по умолчанию</param>
        /// <param name="guiTree">текущее дерево GUI</param>
        /// <param name="nodeNestingLevel">текущий уровень вложенности в дереве GUI</param>
        /// <param name="nodeParentName">текущее имя родителя в дереве GUI</param>
        /// <returns>количество добавленных узлов в дерево GUI System.Int32.</returns>
        private int GetGUITreeNode(string nodeName, Type nodeType, object nodeDefValue, List<GUITreeNode> guiTree, int nodeNestingLevel, string nodeParentName)
        {
            GUITreeNode Node = new GUITreeNode();// { fName = paramName, fType = paramType.Name, fForm = "TextBox", fList = null, fNull = false, fDef = defValue, fLevel = nestingLevel };
            if (nodeType.IsClass && nodeType.Namespace != "System" && !nodeType.IsGenericType)
            {
                if (nodeNestingLevel > 1)
                    nodeNestingLevel--;

                Node.fParentName = nodeNestingLevel == 0 ? "" : nodeParentName;// paramName; ;
                Node.fName = nodeName; //"ctor_" +paramName;
                Node.fType = "Constructor";
                Node.fForm = "ComboBox";
                Node.fList = GetConstructors(nodeType);// (((Type)Param));
                //mParam.fNull
                //mParam.fDef
                Node.fLevel = nodeNestingLevel;

                guiTree.Add(Node);
                nodeNestingLevel++;

                //!!! recursion
                GetParamsConstuctor((ConstructorInfo)Node.fList.Values.First(), guiTree, nodeNestingLevel, Node.fName);   //!!! attention - recursion
                /*
                foreach (var ctor in mParam.fList.Values)
                {
                    GetParamsConstuctor((ConstructorInfo)ctor, Params, nestingLevel, mParam.fParentName);   //!!! attention - recursion
                }
                */
            }
            else
            {
                Node.fParentName = nodeParentName;
                Node.fName = nodeName;
                Node.fType = nodeType.Name;
                //mParam.fForm 
                //mParam.fList 
                //mParam.fNull 
                Node.fDef = nodeDefValue;
                Node.fLevel = nodeNestingLevel;

                switch (nodeType.Name)
                {
                    case "String":
                    case "Double":
                    case "Single":
                    case "Int32":
                    case "Decimal":
                        Node.fForm = "TextBox";
                        break;
                    case "DateTime":
                        Node.fForm = "DateTime";
                        break;
                    case "Nullable`1":
                        Node.fType = nodeType.GenericTypeArguments.First().Name;
                        Node.fNull = true;
                        switch (Node.fType)
                        {
                            case "String":
                            case "Double":
                            case "Single":
                            case "Int32":
                            case "Decimal":
                                Node.fForm = "TextBox";
                                break;
                            case "DateTime":
                                Node.fForm = "DateTime";
                                break;
                            default:
                                if (nodeType.GenericTypeArguments.First().IsEnum)
                                {
                                    Node.fForm = "ComboBox";
                                    Node.fList = (Dictionary<string, object>)GetParamEnum(nodeType.GenericTypeArguments.First());

                                }
                                //                             else 
                                //                             {
                                //                                    Node.fForm = "TextBox";
                                //                             }
                                break;
                        }
                        break;
                    //                    case "Dictionary`2":
                    //                        break:
                    case "Boolean":
                        Node.fForm = "CheckBox";
                        break;
                    default:
                        if (nodeType.IsEnum)
                        {
                            Node.fForm = "ComboBox";
                            Node.fList = (Dictionary<string, object>)GetParamEnum(nodeType);
                        }
                        else if (nodeType.IsGenericType)
                        {
                            //!!! Exception
                            //https://stackoverflow.com/questions/577092/c-sharp-gui-control-for-editing-a-dictionary
                            if (nodeParentName == "ShadowSocksConfig" && nodeName == "parameters")
                            {
                                //mParam.fForm = "Button"; //"Dictionary`2"
                                //mParam.fList = (new Dictionary<string, string> {["plugin"] = "plugin" + (string.IsNullOrEmpty("pluginOption") ? "" : $";" + $"{"pluginOption"}")}).Values.Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
                                Node.fForm = "dataGridView";
                            }
                        }
                        else
                        {
                        }
                        break;
                }
                guiTree.Add(Node);
            }
            return guiTree.Count;
        }
    }

    public class GUIInvoke
    {
        /// <summary>Gets the invoke member.</summary>
        /// <param name="initСtor">The initialize сtor.</param>
        /// <param name="ctor">The ctor.</param>
        /// <param name="payloadName">Name of the payload.</param>
        /// <param name="errorList">The error list.</param>
        /// <returns>System.String.</returns>
        public string GetInvokeMember(object initСtor, ConstructorInfo ctor, string payloadName, List<InvokeError> errorList)
        {
            string payloadStr = "";
            if (ctor.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
            {
                try
                {
                    payloadStr = ctor.ReflectedType.GetMethod("ToString").Invoke(initСtor, new object[] { }).ToString();
                }
                catch (Exception e)
                {
                    InvokeError err = new InvokeError("Method. " + payloadName + ".ToString()");
                    do
                    {
                        if (e.HResult != -2146232828)       //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                            err.AddMsg(e.Message);
                        e = e.InnerException;
                    }
                    while (e != null);

                    errorList.Add(err);
                }
            }
            else                                            //constrictor with parameters = there is 'constructor'
            {
                try
                {
                    payloadStr = ctor.ReflectedType.GetMethod("ToString").Invoke(initСtor, null).ToString();
                }
                catch (Exception e)
                {
                    InvokeError err = new InvokeError("Method. " + payloadName + ".ToString()");
                    do
                    {
                        if (e.HResult != -2146232828)  //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                            err.AddMsg(e.Message);
                        e = e.InnerException;
                    }
                    while (e != null);

                    errorList.Add(err);
                }
            }
            return payloadStr;
        }
        /// <summary>Gets the invoke ctor.</summary>
        /// <param name="ctor">The constructor object.</param>
        /// <param name="cntrlFromForm">The CNTRL from form.</param>
        /// <param name="payloadName">Name of the payload.</param>
        /// <param name="errorList">The error list.</param>
        /// <returns>Invoke Constructor object.</returns>
        public object GetInvokeCtor(ConstructorInfo ctor, Dictionary<string, object> cntrlFromForm, string payloadName, List<InvokeError> errorList)
        {
            object ctorObj = null;

            if (ctor.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
            {
                ctorObj = ctor.Invoke(new object[] { });
                //сопоставить параметры по имени
                foreach (KeyValuePair<string, object> entry in cntrlFromForm)
                {
                    ctorObj.GetType().GetProperty(entry.Key).SetValue(ctorObj, entry.Value);
                }

                try
                {
                    object instance = ctor.Invoke(ctorObj, new object[] { });
                }
                catch (Exception e)
                {
                    InvokeError err = new InvokeError("Constructor. " + ctor.DeclaringType.Name);
                    do
                    {
                        if (e.HResult != -2146232828)       //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                            err.AddMsg(e.Message);
                        e = e.InnerException;
                    }
                    while (e != null);

                    errorList.Add(err);
                }
            }
            else                                            //constrictor with parameters = there is 'constructor'
            {
                if (cntrlFromForm.Count != 0)
                {
                    object[] propFromForm = cntrlFromForm.Select(z => z.Value).ToArray();
                    Boolean check = false;
                    //if parameter is a constructor - check that it is not zero
                    for (int i = 0; i < propFromForm.Count(); i++)
                    {
                        Type item = ctor.GetParameters()[i].ParameterType;
                        if ((item.IsClass && item.Namespace != "System" && !item.IsGenericType) && propFromForm[i] == null)
                            check |= true;
                    }
                    if (check)
                    {
                        InvokeError err = new InvokeError("Constructor. " + ctor.DeclaringType.Name);
                        err.AddMsg("Not all class constructors are initialized");
                        errorList.Add(err);
                    }
                    else
                    {
                        try
                        {
                            ctorObj = ctor.Invoke(propFromForm);
                        }
                        catch (Exception e)
                        {
                            InvokeError err = new InvokeError("Constructor. " + ctor.DeclaringType.Name);
                            do
                            {
                                if (e.HResult != -2146232828)  //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                                    err.AddMsg(e.Message);
                                e = e.InnerException;
                            }
                            while (e != null);

                            errorList.Add(err);
                        }
                    }
                }
            }
            return ctorObj;
        }

        public string GetFormattedErrorDescription(string payloadName, List<InvokeError> errorList)
        {
            string  strMsg = "<style>" +
                             "table {" +
                             //  "border: 1px solid black; "+
                             "width:100%;" +
                             "border - collapse: collapse;}" +
                             "th {" +
                             "text-align: left; " +
                             //     "font-size: 11pt; " +
                             "font-weight:normal; " +
                             "background-color: rgb(240, 240, 240);}" +
                             "</style>" +
                             "<body>" +// bgcolor='#FFEFD5'>" +
                             "<strong>&#128270;&nbsp;" + "Create " + payloadName + " payload string error" + "</strong>" +
                             "<hr><table><tbody>";

            foreach (var err in errorList)
            {
                strMsg += "<tr><th colspan='2'>" + err.ConstructorName + "</th></tr>";
                foreach (var msg in err.Errors)
                {
                    strMsg += "<tr><td class='first'>&#10008;</td><td class='last'>" + msg + "</td></tr>";
                }
            }
            strMsg += "</tbody></table>";
            strMsg += "<hr>&#128736;&nbsp;<i><small>" + "try setting the parameters..." + "</small></i>" +
                      "</body>";
            return strMsg;
        }
    }
}

