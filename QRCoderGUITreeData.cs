using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace QRCoderArt
{
    //----------------------------------------------------------------------------------------------------
    /// <summary>Class DataNode.</summary>
    //----------------------------------------------------------------------------------------------------
    public class ValueNode
    {
        public ValueNode() { id = Guid.NewGuid(); }
        public readonly Guid id;                           // node field identifier
        public string name;                                // node field name
        public int nestingLevel;                            // the nesting level of the node in the tree
        public string parentName;                          // name of the parent node 
        public string dataType;                            // name of the node field data type(string: 'String', 'Integer', etc.)
        public string formType;                            // name of the form element type (string: 'TextBox'; 'Combobox', etc.)
        public Dictionary<string, object> dataSource;      // extended data (Dictionary) - used for for datasource ComboBox
        public Boolean nullValue;                          // indicates whether the parameter value is zero-used for CheckBox
        public object defaultValue;                        //the default value of the parameter
        public object formValue;                           //the current value of the parameter from form
        public Type metaDataSource;                      //the current data source form control
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>Class DataNodeTree.</summary>
    /// <typeparam name="T"></typeparam>
    //----------------------------------------------------------------------------------------------------
    public class Node<T>
    {
        //https://codengineering.ru/q/tree-data-structure-in-c-sharp-23594
        private readonly T _value;
        private readonly List<Node<T>> _children = new List<Node<T>>();
        public Node<T> Parent { get; private set; }             // parent node
        public Node(T value) { _value = value; }                                    // first constructor node    
        public Node<T> this[int i] { get { return _children[i]; } }                             // get node from List by index
        public T Value { get { return _value; } }                       // get value data (T) node
        public ReadOnlyCollection<Node<T>> Children             // get collection childrens node  
        {
            get { return _children.AsReadOnly(); }
        }
        public Node<T> AddChild(T value)                        // add single node (List)     
        {
            var node = new Node<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }
        public void RemoveChildren(Node<T> node, Node<T> root_node) // remove node (List)
        {
            if (node != null)
            {
                if (node._children.Count() > 0)
                    RemoveChildren(node._children.First(), root_node);  // get last node with children
                else
                {
                    if (node != root_node)                              // if not end?
                    {
                        node.Parent._children.Remove(node);             // delete node (from psren children)
                        RemoveChildren(node.Parent, root_node);
                    }
                }
            }
        }
        public Node<ValueNode> Find(Node<ValueNode> node, string nameDescriptor, string nameType)
        {
            if (nameDescriptor == node._value.name && nameType == node._value.dataType)
                return node;

            Node<ValueNode> personFound = null;
            for (int i = 0; i < node._children.Count; i++)
            {
                personFound = Find(node._children[i], nameDescriptor, nameType);
                if (personFound != null)
                    break;
            }
            return personFound;
        }
        public IEnumerable<T> Flatten()     // return the tree as a flat list with parent
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// Class InvokeError.
    /// </summary>
    //----------------------------------------------------------------------------------------------------
    public class InvokeError
    {
        public InvokeError(string name) { ConstructorName = name; }
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
        public void AddMessage(string val) { Errors.Add(val); }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// Class GUITree.
    /// A class for working with Reflection crqoder.dll
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    //----------------------------------------------------------------------------------------------------
    public class GUITree : IDisposable
    {
        //https://www.nookery.ru/c-search-in-depthdfs-using-a-list/
        /// <summary>
        /// The t reference
        /// node (entry point) in Reflection
        /// </summary>
        private readonly Type basePayloads;             // Payload from QRCoder.Dll
        private readonly Node<ValueNode> rootTree;      // root node in the tree data
        public Node<ValueNode> pointTree;               // current point node in the tree data

        /// <summary>
        /// Initializes a new instance of the <see cref="GUITree"/> class.
        /// </summary>
        public GUITree()
        {
            basePayloads = typeof(QRCoder.PayloadGenerator);    //node reflection
//            extPayloads = typeof(QRCoderArt.PayloadExt);        //node reflection

            rootTree = new Node<ValueNode>(new ValueNode());
            rootTree.Value.name = "Payload";
            pointTree = rootTree;
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            //         throw new NotImplementedException();
        }

        /// <summary>Gets the tree from name.</summary>
        /// <returns>DataNodeTree&lt;DataNode&gt;.</returns>
        public IEnumerable<ValueNode> GetTree()
        {
            return pointTree.Flatten().Select(n => n).Skip(1);  // without parent (first element)
        }

        /// <summary>
        /// Gets the name of the members class from two source.
        /// basePayloads return all the names of the public, current classes of the node. To display all the payload interfaces qrcoder.dll
        /// extPayloads return all the names of the public, current classes of the node. To display all the payload interfaces PayloadExt.cs
        /// </summary>
        /// <param name="cName">Reflection node name.</param>
        /// <returns>list of payload Dictionary&lt;System.String, System.Object&gt;.</returns>
        public Dictionary<string, object> GetMembersClassName(string cName)
        {
            return (from t in basePayloads.GetMembers(BindingFlags.Public) //.Concat(extPayloads.GetMembers(BindingFlags.Public))
                    where (!((System.Type)t).IsAbstract) &&
                          ((System.Type)t).BaseType.Name == cName
                    select new
                    {
                        name = t.Name,
                        t
                    }).ToDictionary(k => k.name, v => (Object)v.t);
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
        /// <param name="nestingLevel">The nesting level (current nesting level of the parameter).</param>
        /// <param name="parentName">Name of the parent (current name of the parameter parent).</param>
        private void GetParamsConstuctor(ConstructorInfo ctor, int nestingLevel, string parentName)
        {
            //constrictor without parameters = there is 'no constructor'
            IEnumerable queryProp = from t in ctor.ReflectedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                    where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                    select GetGUITreeNode(t.Name, t.PropertyType, null, nestingLevel, parentName);

            //constrictor with parameters = there is 'constructor'
            IEnumerable queryParam = from t in ctor.GetParameters()
                                     where !t.IsDefined(typeof(ObsoleteAttribute), true)
                                     select GetGUITreeNode(t.Name, t.ParameterType, t.DefaultValue, nestingLevel, parentName);

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
            pointTree = rootTree;
            pointTree.RemoveChildren(pointTree, pointTree);            // clear all payloads and children data
            GetGUITreeNode(((Type)obj).Name, (Type)obj, null, 0, "");      //get parameter list
            return null;// GUITree;
        }

        /// <summary>
        /// Gets the GUI tree nodes.
        /// return a fragment of the GUI tree from a specific constructor to recreate a fragment of the payload panel
        /// </summary>
        /// <param name="obj">The object (node of the Reflection constructor).</param>
        /// <param name="nodeParentName">Name of the node parent.</param>
        /// <param name="nodeNestingLevel">Current nesting level of the node in the GUI tree.</param>
        /// <returns>IList.</returns>
        public IList GetGUITreeNodes(ConstructorInfo obj, int nodeNestingLevel, string nodeParentName)
        {
            pointTree = rootTree.Find(rootTree, nodeParentName, "Constructor");
            pointTree.RemoveChildren(pointTree, pointTree);                 // remove children from point
            GetParamsConstuctor(obj, nodeNestingLevel, nodeParentName);   //!!! attention - recursion
            return null;// GUITreeNodes;
        }


#pragma warning disable CS1572 // Комментарий XML имеет тег param для "guiTree", но параметр с таким именем отсутствует.
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
        private int GetGUITreeNode(string nodeName, Type nodeType, object nodeDefValue, int nodeNestingLevel, string nodeParentName)
#pragma warning restore CS1572 // Комментарий XML имеет тег param для "guiTree", но параметр с таким именем отсутствует.
        {
            ValueNode dataNode = new ValueNode();//  new GUITreeNode()
                                               // DataNodeTree<DataNode> currentNode = rootNode;

            if (nodeType.IsClass && nodeType.Namespace != "System" && !nodeType.IsGenericType)
            {
                dataNode.metaDataSource = nodeType;
                dataNode.parentName = nodeNestingLevel == 0 ? "" : nodeParentName;// paramName; ;
                dataNode.name = nodeName; //"ctor_" +paramName;
                dataNode.dataType = "Constructor";
                dataNode.formType = "ComboBox";
                dataNode.dataSource = GetConstructors(nodeType);// (((Type)Param));
                //mParam.fNull
                //mParam.fDef
                dataNode.nestingLevel = nodeNestingLevel;

                pointTree = pointTree.AddChild(dataNode);
                nodeNestingLevel++;

                //!!! recursion
                GetParamsConstuctor((ConstructorInfo)dataNode.dataSource.Values.First(), nodeNestingLevel, dataNode.name);   //!!! attention - recursion
                /*
                foreach (var ctor in mParam.fList.Values)
                {
                    GetParamsConstuctor((ConstructorInfo)ctor, Params, nestingLevel, mParam.fParentName);   //!!! attention - recursion
                }
                */
                pointTree = pointTree.Parent; //down
            }
            else
            {
                dataNode.metaDataSource = nodeType;
                dataNode.parentName = nodeParentName;
                dataNode.name = nodeName;
                dataNode.dataType = nodeType.Name;
                //mParam.fForm 
                //mParam.fList 
                //mParam.fNull 
                dataNode.defaultValue = nodeDefValue;
                dataNode.nestingLevel = nodeNestingLevel;

                switch (nodeType.Name)
                {
                    case "String":
                    case "Double":
                    case "Single":
                    case "Int32":
                    case "UInt32":
                    case "UInt64":
                    case "Decimal":
                        dataNode.formType = "TextBox";
                        break;
                    case "DateTime":
                        dataNode.formType = "DateTime";
                        break;
                    case "Nullable`1":
                        dataNode.dataType = nodeType.GenericTypeArguments.First().Name;
                        dataNode.nullValue = true;
                        switch (dataNode.dataType)
                        {
                            case "String":
                            case "Double":
                            case "Single":
                            case "Int32":
                            case "UInt32":
                            case "UInt64":
                            case "Decimal":
                                dataNode.formType = "TextBox";
                                break;
                            case "DateTime":
                                dataNode.formType = "DateTime";
                                break;
                            default:
                                if (nodeType.GenericTypeArguments.First().IsEnum)
                                {
                                    dataNode.formType = "ComboBox";
                                    dataNode.dataSource = (Dictionary<string, object>)GetParamEnum(nodeType.GenericTypeArguments.First());

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
                        dataNode.formType = "CheckBox";
                        break;
                    default:
                        if (nodeType.IsEnum)
                        {
                            dataNode.formType = "ComboBox";
                            dataNode.dataSource = (Dictionary<string, object>)GetParamEnum(nodeType);
                        }
                        else if (nodeType.IsGenericType)
                        {
                            //!!! Exception
                            //https://stackoverflow.com/questions/577092/c-sharp-gui-control-for-editing-a-dictionary
                            //   if (nodeParentName == "ShadowSocksConfig" && nodeName == "parameters")
                            //   {
                            //mParam.fForm = "Button"; //"Dictionary`2"
                            //mParam.fList = (new Dictionary<string, string> {["plugin"] = "plugin" + (string.IsNullOrEmpty("pluginOption") ? "" : $";" + $"{"pluginOption"}")}).Values.Cast<object>().ToDictionary(k => k.ToString(), v => v); ;
                            dataNode.formType = "dataGridView";
                            //   }
                        }
                        else
                        {
                        }
                        break;
                }
                pointTree.AddChild(dataNode);
            }
            return 0;// guiTree.Count;
        }
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>Class GUIInvoke.</summary>
    //----------------------------------------------------------------------------------------------------
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

