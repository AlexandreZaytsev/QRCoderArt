﻿// ***********************************************************************
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
    /// Class InvokeError.
    /// </summary>
    public class InvokeError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeError"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public InvokeError(string name)
        {
            ConstructorName = name;
        }
        /// <summary>
        /// Gets or sets the name of the constructor.
        /// </summary>
        /// <value>The name of the constructor.</value>
        public string ConstructorName { get; set; }
        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public List<string> Errors { get; set; } = new List<string>();
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="val">The value.</param>
        public void AddMessage(string val)
        {
            Errors.Add(val);
        }
    }
    /// <summary>
    /// Class GUITreeNode.
    /// GUI parameter tree node for creating a form element (GUI Control)
    /// </summary>
    public class GUITreeNode
    {
        /// <summary>
        /// The f level
        /// the nesting level of the parameter in the tree
        /// </summary>
        public int fLevel;
        /// <summary>
        /// The f parent name
        /// name of the parameter parent
        /// </summary>
        public string fParentName;
        /// <summary>
        /// The f name
        /// parameter name
        /// </summary>
        public string fName;
        /// <summary>
        /// The f type
        /// name of the parameter data type (string: 'String', 'Integer', etc.)
        /// </summary>
        public string fType;
        /// <summary>
        /// The f form
        /// name of the form element type (string: 'TextBox'; 'Combobox', etc.)
        /// </summary>
        public string fForm;
        /// <summary>
        /// The f list
        /// extended data (Dictionary) - used for for datasource ComboBox
        /// </summary>
        public Dictionary<string, object> fList;
        /// <summary>
        /// The f null
        /// indicates whether the parameter value is zero-used for CheckB
        /// </summary>
        public Boolean fNull;
        /// <summary>
        /// The f definition
        /// the default value of the parameter
        /// </summary>
        public object fDef;
    }

    /// <summary>
    /// Class GUITree.
    /// A class for working with Reflection crqoder.dll
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class GUITree : IDisposable
    {
        /// <summary>
        /// The t reference
        /// node (entry point) in Reflection
        /// </summary>
        private readonly Type tRef;

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITree"/> class.
        /// </summary>
        /// <param name="name">The name name of the Reflection node from which the structure is read.</param>
        public GUITree(string name)
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

        /// <summary>
        /// Gets the member by name.
        /// </summary>
        /// <param name="baseName">Reflection node name.</param>
        /// <returns>First node found Type.</returns>
        public Type GetMemberByName(string baseName)
        {
            return (Type)tRef.GetMember(baseName).First();
        }

        /// <summary>
        /// Gets the name of the members class.
        /// return all the names of the public, current classes of the node. To display all the payload interfaces qrcoder.dll
        /// </summary>
        /// <param name="cName">Reflection node name.</param>
        /// <returns>list of payload List&lt;System.String&gt;.</returns>
        public List<string> GetMembersClassName(string cName)
        {
            return (from t in tRef.GetMembers(BindingFlags.Public)
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select t.Name).ToList();
        }

        /// <summary>
        /// Gets the constructors.
        /// return the node constructors
        /// </summary>
        /// <param name="param">The parameter (Reflection node).</param>
        /// <returns>dictionary with constructors (name value) Dictionary&lt;System.String, System.Object&gt;.</returns>
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
        /// Gets the parameters constuctor.
        /// add GUI constructor parameters to the general list of tree parameters
        /// (if the constructor is not declared (there are no parameters), we read the properties)
        /// </summary>
        /// <param name="ctor">The constructor.</param>
        /// <param name="Params">The parameters (current GIU tree).</param>
        /// <param name="nestingLevel">The nesting level (current nesting level of the parameter).</param>
        /// <param name="parentName">Name of the parent (current name of the parameter parent).</param>
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
          START
        ************************************************************************************************************/
        /// <summary>
        /// Gets the GUI tree.
        /// return the entire GUI tree from the Reflection payload node to create the entire payload panel
        /// </summary>
        /// <param name="obj">The object (payload Reflection node).</param>
        /// <returns>payload GUI tree IList.</returns>
        public IList GetGUITree(Object obj)
        {
            List<GUITreeNode> GUITree = new List<GUITreeNode>();                                //list of parameters
            int nodeNestingLevel = 0;                                                           //nesting level of the parameter

            GetGUITreeNode(((Type)obj).Name, (Type)obj, null, GUITree, nodeNestingLevel, "");   //get parameter list
            return GUITree;
        }

        /// <summary>
        /// Gets the GUI tree nodes.
        /// return a fragment of the GUI tree from a specific constructor to recreate a fragment of the payload panel
        /// </summary>
        /// <param name="obj">The object (node of the Reflection constructor).</param>
        /// <param name="nodeParentName">Name of the node parent.</param>
        /// <param name="nodeNestingLevel">Current nesting level of the node in the GUI tree.</param>
        /// <returns>IList.</returns>
        public IList GetGUITreeNodes(ConstructorInfo obj, string nodeParentName, int nodeNestingLevel)
        {
            List<GUITreeNode> GUITreeNodes = new List<GUITreeNode>();                   //list of parameters
            GetParamsConstuctor(obj, GUITreeNodes, nodeNestingLevel, nodeParentName);   //!!! attention - recursion
            return GUITreeNodes;
        }

        /// <summary>
        /// Gets the GUI tree node.
        /// return the GUI tree node with the parameters
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="nodeType">Type of the node (converted to Type).</param>
        /// <param name="nodeDefValue">The node definition value.</param>
        /// <param name="guiTree">The GUI tree.</param>
        /// <param name="nodeNestingLevel">CCurrent nesting level of the node in the GUI tree.</param>
        /// <param name="nodeParentName">Name of the node parent.</param>
        /// <returns>System.Int32.</returns>
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

    /// <summary>Class GUIInvoke.</summary>
    public class GUIInvoke
    {
        /// <summary>Gets the invoke member.</summary>
        /// <param name="initСtor">The initialize сtor.</param>
        /// <param name="ctor">The ctor.</param>
        /// <param name="errorList">The error list.</param>
        /// <returns>System.String.</returns>
        public string GetInvokeMember(object initСtor, ConstructorInfo ctor, List<InvokeError> errorList)
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
                    InvokeError err = new InvokeError("Method. " + ctor.ReflectedType.Name + ".ToString()");
                    do
                    {
                        if (e.HResult != -2146232828)       //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                            err.AddMessage(e.Message);
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
                    InvokeError err = new InvokeError("Method. " + ctor.ReflectedType.Name + ".ToString()");
                    do
                    {
                        if (e.HResult != -2146232828)  //"Адресат вызова создал исключение."}	System.Exception {System.Reflection.TargetInvocationException}
                            err.AddMessage(e.Message);
                        e = e.InnerException;
                    }
                    while (e != null);

                    errorList.Add(err);
                }
            }
            return payloadStr;
        }
        /// <summary>
        /// Gets the invoke ctor.
        /// </summary>
        /// <param name="ctor">The ctor.</param>
        /// <param name="cntrlFromForm">The CNTRL from form.</param>
        /// <param name="errorList">The error list.</param>
        /// <returns>System.Object.</returns>
        public object GetInvokeCtor(ConstructorInfo ctor, Dictionary<string, object> cntrlFromForm, List<InvokeError> errorList)
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
                            err.AddMessage(e.Message);
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
                        err.AddMessage("Not all class constructors are initialized");
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
                                    err.AddMessage(e.Message);
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

        /// <summary>Gets the HTML formatted error description.</summary>
        /// <param name="payloadName">Name of the payload.</param>
        /// <param name="errorList">The error list.</param>
        /// <returns>HTML format string System.String.</returns>
        public string GetHTMLFormattedErrorDescription(string payloadName, List<InvokeError> errorList)
        {
            string strMsg = "<style>" +
                             "  * { " +
                             "    font-size: 10pt; " +
                             "  }" +
                             "  .descr {" +
                             //"    font-size: 9pt; " +
                             "    font-weight:bold " +
                             "  }" +
                             "  table {" +
                             //    "border: 1px solid black; "+
                             "    width:100%;" +
                             "    border - collapse: collapse; " +
                             "  }" +
                             "  th {" +
                             "    text-align: left; " +
                             //    "font-size: 11pt; " +
                             "    font-weight:normal; " +
                             "    background-color: rgb(240, 240, 240);" +
                             "  }" +
                             "</style>" +
                             "<body>" +// bgcolor='#FFEFD5'>" +
                             "<span class='descr'>&#128270;&nbsp;" + "Create " + payloadName + " payload string ERROR(s)" + "</span>" +
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

