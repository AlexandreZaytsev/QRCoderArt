using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QRCoderArt
{

    /// <summary>
    /// Class FormMain.
    /// Форма использующая динамический интерфейс на плавающих панелях для работы с payload на базе GUITree 
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormMain : Form
    {
        /// <summary>
        /// readyState
        /// структура для фиксации ошибки (при пересоздании CTRL из GUITree) при первом запуске функции payload
        /// </summary>
        private readonly bool[] readyState = {false,                 //Data preparation completed
                                              false,                 //MainForm is Load
                                              false };               //Mainform is Show    

        readonly GUITree ReflectionData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain" /> class.
        /// </summary>
        public FormMain()
        {
            CallBack_SetParam.callbackEventHandler = new CallBack_SetParam.callbackEvent(this.CallbackReload);    //subscribe (listen) to the general notification
            InitializeComponent();
        }

        public FormMain(GUITree guiTree) : this()
        {
            ReflectionData = guiTree;

            string baseName = "Payload";                                        //проще сразу указать имя
          //this.cbPayload.DataSource = ReflectionData.GetMembersClassName_(baseName);    //get list names classes members QRCoder.PayloadGenerator
            this.cbPayload.DataSource = new BindingSource(ReflectionData.GetMembersClassName(baseName), null);
            this.cbPayload.DisplayMember = "Key";                                            //Имя    
            this.cbPayload.ValueMember = "Value";                                            //значение  
       //     this.cbPayload.SelectedItem = 0;
                                                                                         //    }
            this.viewMode.DataSource = Enum.GetValues(typeof(ImageLayout));
            this.viewMode.SelectedIndex = 4;
            this.QRCodeString.Dock = DockStyle.Fill;
            this.QRCodeError.Dock = DockStyle.Fill;
            this.QRCodeString.Visible = true;
        }

        /// <summary>
        /// Handles the Load event of the FormMain control.
        /// устанавливает флаг readyState[1] для первого корректного вызова payload
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            //заполним выпадающие списки из enum
            eccLevel.DataSource = Enum.GetValues(typeof(QRCodeGenerator.ECCLevel));
//            eccLevel.SelectedIndex = eccLevel.FindStringExact(QRCodeGenerator.ECCLevel.Q.ToString());
            eccLevel.SelectedItem = QRCodeGenerator.ECCLevel.M;

            artQuietZoneRenderingStyle.DataSource = Enum.GetValues(typeof(ArtQRCode.QuietZoneStyle));
            artQuietZoneRenderingStyle.SelectedItem = ArtQRCode.QuietZoneStyle.Dotted;

            artBackgroundImageStyle.DataSource = Enum.GetValues(typeof(ArtQRCode.BackgroundImageStyle));
            artBackgroundImageStyle.SelectedItem = ArtQRCode.BackgroundImageStyle.DataAreaOnly;

            QRCodeString.Text = "enter your text or select payload + constructor + fill in the parameters";
            readyState[1] = true;           //MainForm is Load
            Art.Parent = null;
        }

        /// <summary>
        /// Handles the Shown event of the FormMain control.
        /// устанавливает флаг readyState[2] для первого корректного вызова payload
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            RebuildingGUITreePanel(this.cbPayload,null);
            readyState[2] = true;           //Main form is Show
        }

        #region //REFLECTION

        /// <summary>
        /// ClearGUITreePanel
        /// очистить все CTRL из плавающей панели payload
        /// </summary>
        /// <param name="panelGUITree">плавающая панель CTRL из GUITree</param>
        private void ClearGUITreePanel(Control panelGUITree)
        {
            if (panelGUITree.HasChildren)
            {
                for (int i = panelGUITree.Controls.Count - 1; i >= 0; i--) //not foreach use...
                {
                    Control cn = panelGUITree.Controls[0];
                    //     cn -= new System.EventHandler(cn, payload_Changed);
                    panelGUITree.Controls.Remove(cn);
                    cn.Dispose();
                }
            }
        }

        /// <summary>
        /// CreateControlPlayloadPanel
        /// создать CTRL panel из GUITree
        /// </summary>
        /// <param name="panelGUITree">плавающая панель CTRL из GUITree</param>
        private void CreateGUITreePanel(Control panelGUITree)
        {
            int labelWidth = 135;// 122;// 135;
            int sizeWidth = 263;
            int reverseShift = 5;   // paddind compensation for nested controls
            int controlWidth;
            Panel tPanel;

            Padding padding = new Padding(0, 1, 1, 1);

            Stack<Panel> panels = new Stack<Panel>();
            panels.Push((Panel)panelGUITree);

            panelPayload.Visible = false;       //render off

            foreach (var prop in ReflectionData.GetTree())
            {
                //                 this.Refresh();
                controlWidth = prop.nullValue ? 113 : 128;
                //                controlWidth = prop.fNull ? 103 : 128;

                if (panels.Peek().Name != prop.parentName && panels.Peek().Name != panelGUITree.Name)
                {
                    tPanel = panels.Pop();  //go back to the previous panel
                }

                Label lb = new Label
                {
                    Name = prop.name,
                    AutoSize = false,
                    Margin = padding
                }; //Label();
                if (prop.dataType != "Constructor")
                {
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    lb.Text = prop.name;
                    lb.Size = new Size(labelWidth - prop.nestingLevel * reverseShift, 20);
                    panels.Peek().Controls.Add(lb);
                }
                else
                {
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    lb.Text = prop.name + " constructor's"; //(panels.Peek().Name == "panelPayload" ? "Class: " : "subClass: ") + prop.fName + " constructor's";
                    lb.Size = new Size(sizeWidth - prop.nestingLevel * reverseShift, 20);
                    panels.Peek().Controls.Add(lb);
                }

                switch (prop.formType)
                {
                    case "TextBox":
                        TextBox tb = new TextBox
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.name,
                            AccessibleName = prop.id.ToString(),// "Get",
                            AccessibleDescription = prop.dataType                          //type in tooltype
                        };
                        tb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        tb.TextChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                        switch (prop.dataType)
                        {
                            case "Single":
                            case "Int32":
                            case "UInt32":
                            case "UInt64":
                            case "Decimal":
                            case "Double":
                                tb.BackColor = Color.GhostWhite;//.OldLace;// LightBlue;
                                tb.KeyPress += new KeyPressEventHandler(FilterOnlyReal);
                                tb.Text = prop.defaultValue == null ? "" : Convert.ToString(prop.defaultValue);
                                break;
                            default:
                                tb.Text = prop.defaultValue == null ? "" : Convert.ToString(prop.defaultValue);
                                break;
                        }
                        panels.Peek().Controls.Add(tb);
                        if (prop.nullValue)
                        {
                            CheckBox chtb = new CheckBox
                            {
                                Size = new Size(13, 20),
                                Margin = padding,
                                Name = "" + prop.name,
                                AccessibleDescription = "Nullable"                          //type in tooltype
                            };
                            chtb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                            chtb.CheckedChanged += (sender, e) => tb.Enabled = (chtb.CheckState == CheckState.Checked); // GeyPayLoadStringFromForm(null, null);
                            panels.Peek().Controls.Add(chtb);
                            tb.Enabled = false;// chtb.Checked;
                        }
             //           DataBindings.Add("Text", ReflectionData.pointTree.Value.formValue, "formValue");

                        break;
                    case "CheckBox":
                        CheckBox chb = new CheckBox
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.name,
                            AccessibleName = prop.id.ToString(),// "Get",
                            AccessibleDescription = prop.dataType
                        };
                        chb.CheckedChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                        panels.Peek().Controls.Add(chb);
                        break;
                    case "dataGridView":
                        DataGridView dgv = new DataGridView
                        {
                            Size = new Size(103, 20),
                            Margin = padding,
                            Name = "" + prop.name,
                            DataSource = new Dictionary<string, string> { ["plugin"] = "plugin + pluginOption", ["-"] = "-" }, //!!! refresh from callback -> CallBack_GetParam
                                                                                                                               //new Dictionary<string, string>{["plugin"] = plugin + (string.IsNullOrEmpty(pluginOption)? "": $";{pluginOption}")}
                            AccessibleName = prop.id.ToString(),// "Get",
                            AccessibleDescription = prop.dataType
                        };
                        dgv.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        dgv.DataSourceChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                        panels.Peek().Controls.Add(dgv);

                        // case "Button":          //for custom parameter
                        Button bt = new Button
                        {
                            Size = new Size(23, 20),
                            Margin = padding,
                            Name = "" + prop.name,
                            Text = "...",
                            AccessibleName = "",// "Get";
                            FlatStyle = FlatStyle.System,
                            AccessibleDescription = "setting a custom parameter"// prop.fType;
                        };
                        bt.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        bt.Click += new EventHandler(SetPropretyPairs);
                        //                        bt.Click += (sender, e) => {dictionaryForm a = new dictionaryForm(); a.ShowDialog(); };
                        panels.Peek().Controls.Add(bt);
                        break;
                    case "DateTime":
                        DateTimePicker dtp = new DateTimePicker
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.name,
                            AccessibleName = prop.id.ToString(),// "Get",
                            AccessibleDescription = prop.dataType,
                            Format = DateTimePickerFormat.Short
                        };
                        dtp.ValueChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                        dtp.EnabledChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                        panels.Peek().Controls.Add(dtp);
                        if (prop.nullValue)
                        {
                            CheckBox chdtp = new CheckBox
                            {
                                Size = new Size(13, 20),
                                Margin = padding,
                                Name = "" + prop.name,
                                AccessibleDescription = "Nullable"                          //type in tooltype
                            };
                            chdtp.MouseHover += new System.EventHandler(ToolTipMouseHover);
                            chdtp.CheckedChanged += (sender, e) => dtp.Enabled = (chdtp.CheckState == CheckState.Checked);// GeyPayLoadStringFromForm(null, null);
                            panels.Peek().Controls.Add(chdtp);
                            dtp.Enabled = false;// chdtp.Checked;
                        }
                        break;
                    case "ComboBox":
                        ComboBox cmb = new ComboBox
                        {
                            //                        cmb.BeginUpdate();
                            Name = "" + prop.name,
                            AccessibleName = prop.id.ToString(),// "Get",
                            //cmb.AccessibleDescription = prop.fType;
                            DataSource = new BindingSource(prop.dataSource, null),   //получить конструкторы member
                            DisplayMember = "Key",                                            //Имя    
                            ValueMember = "Value",                                            //значение  
                            SelectedItem = 0,
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        if (prop.dataType == "Constructor")
                        {
                            //  cmb.Name += "ctor_";
                            cmb.AccessibleDescription = "Constructor";
                            cmb.SelectedIndexChanged += new EventHandler(RebuildingGUITreePanel);
                            panels.Peek().Controls.Add(cmb);
                            cmb.Size = new Size(sizeWidth - prop.nestingLevel * reverseShift, 20);


                            FlowLayoutPanel cPanel = new FlowLayoutPanel
                            {
                                Name = "" + prop.name,
                                AutoSize = true,
                                Padding = new Padding(0, 2, 0, 2),
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            panels.Peek().Controls.Add(cPanel);
                            panels.Push(cPanel);

                            Control cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(prop.name) && c is Label).First();
                            cntrl.Text += " (" + prop.dataSource.Count.ToString() + ")";
                        }
                        else
                        {
                            cmb.SelectedIndexChanged += new EventHandler(GetPayloadStringFromGUITreePanel);
                            cmb.Size = new Size(controlWidth, 20);
                            cmb.Margin = padding;
                            panels.Peek().Controls.Add(cmb);
                            if (prop.nullValue)
                            {
                                CheckBox chcmb = new CheckBox
                                {
                                    Size = new Size(13, 20),
                                    Margin = padding,
                                    Name = "" + prop.name,
                                    AccessibleDescription = "Nullable"                          //type in tooltype
                                };
                                chcmb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                                chcmb.CheckedChanged += (sender, e) => cmb.Enabled = (chcmb.CheckState == CheckState.Checked);// GeyPayLoadStringFromForm(null, null);
                                panels.Peek().Controls.Add(chcmb);
                                cmb.Enabled = false;// chdtp.Checked;
                            }
                        }
                        //                      cmb.EndUpdate();
                        break;
                }
            }
            panelPayload.Visible = true;    //render on
        }

        #endregion

        /*--------------------------------------------------------------------------------------------  
                     EVENTS
        --------------------------------------------------------------------------------------------*/

        /// <summary>
        /// ToolTipMouseHover
        /// отобразить всплывающую подсказку из AccessibleDescription
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ToolTipMouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((System.Windows.Forms.Control)sender,
                               ((System.Windows.Forms.Control)sender).AccessibleDescription);
        }

        /// <summary>
        /// FilterOnlyReal
        /// проверка ввода на числа с плавающей точкой (контролируя системный разделитель)
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void FilterOnlyReal(object sender, KeyPressEventArgs e)
        {
            string sep = ((float)1 / 2).ToString().Substring(1, 1);  // system sparator
/*
            bool lCheck = false;
            TextBox cnt = sender as TextBox;
            string txt = (cnt.Text + e.KeyChar).Replace(" ", "");
            switch (cnt.AccessibleDescription)
            {
                case "Single":
                    lCheck = Regex.IsMatch(txt, @"^([0-9\b\" + sep + "]){1,10}$");
                    break;
                case "Int32":
                    break;
                case "UInt32":
                    lCheck = Regex.IsMatch(txt, @"^([0-9\b\" + sep + "]){1,10}$");   
                    break;
                case "UInt64":
                    lCheck = Regex.IsMatch(txt, @"^([0-9\b\" + sep + "]){1,20}$");
                    break;
                case "Decimal":
                    break;
                case "Double":
                    break;
                default:
                    lCheck = Regex.IsMatch(txt, @"^([0-9\b])");
                    break;
            }
            e.Handled = !lCheck;
*/
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == Convert.ToChar(sep)) && (((TextBox)sender).Text.IndexOf(sep) == -1) && (((TextBox)sender).Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// SetPropretyPairs
        /// передать через асинхронное сообщение (callback) пары параметров ключ-значение во внешнюю форму на редактирование
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SetPropretyPairs(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            FormDictionary a = new FormDictionary();

            Control[] cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(bt.Name) && c is DataGridView);
            CallBack_GetParam.callbackEventHandler(bt.Name, bt.Parent.Name, (Dictionary<String, String>)((DataGridView)cntrl[0]).DataSource);  //send a general notification
            a.Owner = this;
            a.ShowDialog();
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// RebuildingGUITreePanel
        /// обновить GUITree панель или ее фрагмент GUITreeNodes
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void RebuildingGUITreePanel(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            Control[] panel = combo.Name == "cbPayload" ?
                                this.FilterControls(c => c.Name != null && c.Name.Equals("panelPayload") && c is FlowLayoutPanel) :
                                this.FilterControls(c => c.Name != null && c.Name.Equals(combo.Name) && c is FlowLayoutPanel);

            if (combo.SelectedItem != null)
            {
                readyState[0] = false;                             //full ready:= Data preparation not completed
                ClearGUITreePanel(panel.First());                  //clear payload panel
                if (combo.Name == "cbPayload")
                {
                //    ReflectionData.GetGUITree(ReflectionData.GetMemberByName(combo.Text));//, combo.Name);
                    ReflectionData.GetGUITree(((KeyValuePair<string, object>)combo.SelectedItem).Value);
               //     ReflectionData.GetGUITree(ReflectionData.GetMemberByName(combo.Name));
                }
                else
                {
                    //clear child from parentName in tree
                    //                  ReflectionData.

                    ReflectionData.GetGUITreeNodes(((ConstructorInfo)((KeyValuePair<string, object>)combo.SelectedItem).Value), panel[0].GetNestleLevel("panelPayload"), combo.Name);
                }
                CreateGUITreePanel(panel.First());    //create payload panel from constructor parameters
                                                      //                this.Refresh();
                readyState[0] = true;                              //full ready:= Data preparation completed
                GetPayloadStringFromGUITreePanel(null, null);
            }
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// ReadGUITreePanel
        /// прочитать текущие значения CTRL панели GUITree и 
        /// собрать массив параметров для инициализации конструктора
        /// </summary>
        /// <param name="panelGUITree">The panel.</param>
        /// <param name="errorList">error List</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        private Dictionary<string, object> GetParamFromGUITreePanel(FlowLayoutPanel panelGUITree, List<InvokeError> errorList)
        {
            object ret;// = null;
            Dictionary<string, object> ParamFromControl = new Dictionary<string, object>();
            if (panelPayload.HasChildren)
            {
                foreach (Control cntrl in panelGUITree.Controls)
                {

//                    if (cntrl.Created && cntrl.AccessibleName == "Get" && cntrl.Parent.Name == panelGUITree.Name)
                    if (cntrl.Created && Guid.TryParse(cntrl.AccessibleName, out var newGuid) && cntrl.Parent.Name == panelGUITree.Name)
                        {
                            ret = null;
                        if (cntrl.Enabled)
                        {
                            switch (cntrl.AccessibleDescription)
                            {
                                case "Constructor":
                                    ConstructorInfo ctor = (ConstructorInfo)((KeyValuePair<string, object>)((ComboBox)cntrl).SelectedItem).Value;
                                    Control[] pan = panelPayload.FilterControls(c => c.Name != null
                                                                                && c.Name == cntrl.Name && c is FlowLayoutPanel);
                                    //                                  //!!! recursion
                                    ret = new GUIInvoke().GetInvokeCtor(ctor, GetParamFromGUITreePanel((FlowLayoutPanel)pan[0], errorList), errorList);
                                    break;
                                case "String":
                                    ret = ((TextBox)cntrl).Text;
                                    break;
                                case "Double":
                                    ret = Convert.ToDouble(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "Single":
                                    ret = Convert.ToSingle(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "Int32":
                                    ret = Convert.ToInt32(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "UInt32":
                                    ret = Convert.ToUInt32(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "UInt64":
                                    ret = Convert.ToUInt64(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "Decimal":
                                    ret = Convert.ToDecimal(((TextBox)cntrl).Text == "" ? "0" : ((TextBox)cntrl).Text);
                                    break;
                                case "Boolean":
                                    ret = ((CheckBox)cntrl).Checked;
                                    break;
                                case "DateTime":
                                    ret = ((DateTimePicker)cntrl).Value;
                                    break;
                                case "Dictionary`2":
                                    if (cntrl.Parent.Name == "ShadowSocksConfig" && cntrl.Name == "parameters")
                                    //get param pairs
                                    {
                                        ret = ((DataGridView)cntrl).DataSource;
                                    }
                                    break;
                                default:
                                    if (cntrl.GetType().Name == "ComboBox")
                                        ret = ((KeyValuePair<string, object>)((ComboBox)cntrl).SelectedItem).Value;
                                    break;
                            }
                        }
                        ParamFromControl.Add(cntrl.Name, ret);
                    }
                }
            }
            return ParamFromControl;
        }

        /// <summary>
        /// GetPayloadStringFromGUITreePanel
        /// прочитать параметры панели GUITree и вызвать базовый метод payload ((invoke ToString() execute))
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void GetPayloadStringFromGUITreePanel(object sender, EventArgs e)
        {
            List<InvokeError> errorList = new List<InvokeError>();

            if (readyState[0] && readyState[1] && readyState[2])            //full ready:= Data preparation completed && form load && form show
            {
                Dictionary<string, object> ParamFromControl = null;

                Control[] panel = this.FilterControls(c => c.Name != null && c.Name.Equals(cbPayload.Text) && c is FlowLayoutPanel);
                //собрать массив параметров для инициализации конструктора
                ParamFromControl = GetParamFromGUITreePanel((FlowLayoutPanel)panel[0], errorList);

                //инициализировать конструктор payload
                Control[] cmb = this.FilterControls(c => c.Name != null && c.Name.Equals(cbPayload.Text) && c is ComboBox);
                ConstructorInfo ctrm = (ConstructorInfo)((System.Collections.Generic.KeyValuePair<string, object>)((ComboBox)cmb[0]).SelectedItem).Value;
                object ctorObj = new GUIInvoke().GetInvokeCtor(ctrm, ParamFromControl, errorList);
                //выполнить главный метод
                string payloadStr = new GUIInvoke().GetInvokeMember(ctorObj, ctrm, errorList);
                if (ctorObj != null && errorList.Count() == 0)
                {
                    if (ctrm.GetParameters().Length == 0)           //constrictor without parameters = there is no constructor
                    {
                        QRCodeString.Text = ctrm.ReflectedType.GetMethod("ToString").Invoke(ctorObj, new object[] { }).ToString();
                    }
                    else
                        QRCodeString.Text = ctrm.ReflectedType.GetMethod("ToString").Invoke(ctorObj, null).ToString();

                    QRCodeError.Visible = false;
                    QRCodeString.Visible = true;
                }
                else
                {
                    QRCodeString.Visible = false;
                    QRCodeError.DocumentText = new GUIInvoke().GetHTMLFormattedErrorDescription(cmb[0].Name, errorList);
                    QRCodeString.Visible = false;
                    QRCodeError.Visible = true;
                    pictureBoxQRCode.BackgroundImage = global::QRCoderArt.Properties.Resources.qr_no;
                }
            }
        }

        #region //Interface

        /// <summary>
        /// выбор режима рендеринго
        /// </summary>
        private void baseMode_CheckedChanged(object sender, EventArgs e)
        {
            if (!baseMode.Checked)
            {
                Art.Parent = null;
                Logo.Parent = baseTabControl;
            }
            else
            {
                Art.Parent = baseTabControl;
                Logo.Parent = null;
            }
            Setting_Changed(null, null);
        }

        /// <summary>
        /// диалог about
        /// </summary>
        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            FormAbout a = new FormAbout();
            a.ShowDialog();
        }

        /// <summary>
        /// Gets the icon bitmap.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap GetIconBitmap()
        {
            if (logoIconPath.Text.Length == 0)
                return null;
            try
            {
                return new Bitmap(logoIconPath.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the art bitmap.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap GetArtBitmap()
        {
            if (artIconPath.Text.Length == 0)
                return null;
            try
            {
                return new Bitmap(artIconPath.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the artpattern bitmap.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap GetArtPatternBitmap()
        {
            if (artPatternPath.Text.Length == 0)
                return null;
            try
            {
                return new Bitmap(artPatternPath.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// расположение картинки QRCode в ViewMode окне
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }

        #endregion

        #region //QR Setting

        /// <summary>
        /// RenderQrCode создать картинку QR кода
        /// </summary>
        private void RenderQrCode()
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRCodeString.Text, (QRCodeGenerator.ECCLevel)eccLevel.SelectedItem))
                {
                    if (!baseMode.Checked)
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic(
                                (int)basePixelsPerModule.Value,
                                baseDarkColor.BackColor,
                                baseLightColor.BackColor,
                                GetIconBitmap(),
                                (int)logoIconSizePercent.Value,
                                (int)logoIconBorderWidth.Value,
                                baseDrawQuietZones.Checked,
                                logoIconPath.Text != "" && logoIconBorderWidth.Value > 0 ? logoBackColor.BackColor : baseLightColor.BackColor
                            );
                            pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                        }
                    }
                    else
                    {
                        using (ArtQRCode qrCode = new ArtQRCode(qrCodeData))
                        {
                            pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic(
                                (int)basePixelsPerModule.Value,
                                baseDarkColor.BackColor,
                                baseLightColor.BackColor,
                                artBackgroundColor.BackColor,
                                GetArtBitmap(),
                                (double)artPixelSizeFactor.Value,
                                baseDrawQuietZones.Checked,
                                (ArtQRCode.QuietZoneStyle)artQuietZoneRenderingStyle.SelectedItem,
                                (ArtQRCode.BackgroundImageStyle)artBackgroundImageStyle.SelectedItem,
                                GetArtPatternBitmap()
                            );
                            pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                        }
                    }
                }
            }
            //Set the SizeMode to center the image.
            //pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// Setting_Changed
        /// общий обработчик события изменения параметров панели GUITree 
        /// </summary>
        private void Setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();     //create QR image
        }

        /// <summary>
        /// проверить есть путь к картинке Logo или нет
        /// </summary>
        private void logoIconPath_TextChanged(object sender, EventArgs e)
        {
            panelLogo.Enabled = logoIconPath.Text != "";
            Setting_Changed(null, null);
        }

        /// <summary>
        /// проверить есть путь к картинке Art или нет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void artPatternPath_TextChanged(object sender, EventArgs e)
        {
            //panelArt.Enabled = artIconPath.Text != "";
            Setting_Changed(null, null);
        }

        #endregion

        #region Open Save Dialod and Setting

        /// <summary>
        /// диалог выбора файла 
        /// </summary>
        /// <param name="name">имя файла</param>
        /// <param name="filter">фильтр</param>
        /// <returns></returns>
        private string getFileName(string name, string filter)
        {
            filter = string.Empty;
            /*
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;
            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                filter = String.Format("{0}{1}{2} ({3})|{3}", filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }
            filter = String.Format("{0}{1}{2} ({3})|{3}", filter, sep, "All Files", "*.*");
            */
            filter = "png Image|*.png|jpeg Image|*.jpg|gif Image|*.gif|bmp Image|*.bmp";

            openFileDialogSetting.Title = name;
            openFileDialogSetting.Filter = filter;
//            openFileDialogSetting.DefaultExt = defaultExt;

            DialogResult dr = openFileDialogSetting.ShowDialog();
            if (dr == DialogResult.Abort)
                return "";
            if (dr == DialogResult.Cancel)
                return "";

            return openFileDialogSetting.FileName.ToString();
        }

        /// <summary>
        /// диалог сохранения файла 
        /// </summary>
        /// <returns></returns>
        private SaveFileDialog setFileName(string name, string filter, string filename)
        {
            filter = string.Empty;
            filter = "png Image|*.png|jpeg Image|*.jpg|gif Image|*.gif|bmp Image|*.bmp";
            saveFileDialogSetting.Title = name;
            saveFileDialogSetting.Filter = filter;
            saveFileDialogSetting.FileName = filename;

            DialogResult dr = saveFileDialogSetting.ShowDialog();
            if (dr == DialogResult.Abort)
                return null;
            if (dr == DialogResult.Cancel)
                return null;

            return saveFileDialogSetting;
        }

        /// <summary>
        /// кнопка выбор Logo (для сброса - выбрать и отказаться)
        /// </summary>
        private void SelectIconBtn_Click(object sender, EventArgs e)
        {
            logoIconPath.Text = getFileName("Select Logo image file", "");
        }

        /// <summary>
        /// кнопка выбор Art (для сброса - выбрать и отказаться)
        /// </summary>
        private void SelectArtBtn_Click(object sender, EventArgs e)
        {
            artIconPath.Text = getFileName("Select Art image file", "");
        }

        /// <summary>
        /// кнопка выбор ArtPattern (3 квадрата по углам) (для сброса - выбрать и отказаться)
        /// </summary>
        private void artPatternButton_Click(object sender, EventArgs e)
        {
            artPatternPath.Text = getFileName("Select ArtPattern image file", "");
        }

        /// <summary>
        /// Handles the Click event of the Btn_save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotSupportedException">File extension is not supported</exception>
        private void Btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialod = setFileName("Select save QRCode filename", "", "QRCode_" + DateTime.Now.ToString("yyyyMMddHHmm"));
            // If the file name is not an empty string open it for saving.
            if (saveDialod.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveDialod.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    ImageFormat imageFormat = null;
                    switch (saveDialod.FilterIndex)
                    {
                        case 1:
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case 2:
                            imageFormat = ImageFormat.Png;
                            break;
                        case 3:
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case 4:
                            imageFormat = ImageFormat.Gif;
                            break;
                        default:
                            throw new NotSupportedException("File extension is not supported");
                    }
                    pictureBoxQRCode.BackgroundImage.Save(fs, imageFormat);
                }
            }
        }

        #endregion

        #region Select Color Dialog

        /// <summary>
        /// диалог выбора цвета
        /// </summary>
        /// <param name="initColor"></param>
        /// <returns></returns>
        private Color getColor(Color initColor)
        {
            colorDialogSetting.FullOpen = true;
            colorDialogSetting.Color = initColor;
            if (colorDialogSetting.ShowDialog() == DialogResult.OK)
                return colorDialogSetting.Color;
            else
                return initColor;
        }

        /// <summary>
        /// цвет темной зоны
        /// </summary>
        private void baseDarkColor_Click(object sender, EventArgs e)
        {
            baseDarkColor.BackColor = getColor(baseDarkColor.BackColor);// Color.Black);
            Setting_Changed(null, null);
        }

        /// <summary>
        /// цвет светлой зоны
        /// </summary>
        private void baseLightColor_Click(object sender, EventArgs e)
        {
            baseLightColor.BackColor = getColor(baseLightColor.BackColor);
            Setting_Changed(null, null);
        }

        /// <summary>
        /// цвет за/под лого
        /// </summary>
        private void logoBackColor_Click(object sender, EventArgs e)
        {
            logoBackColor.BackColor = getColor(logoBackColor.BackColor);
            Setting_Changed(null, null);
        }

        /// <summary>
        /// цвет за/под фоновой картинкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void artBackgroundColor_Click(object sender, EventArgs e)
        {
            artBackgroundColor.BackColor = getColor(artBackgroundColor.BackColor);
            Setting_Changed(null, null);
        }

        #endregion

        #region //CALLBACK InPut (подписка на внешние сообщения)

        /// <summary>
        /// Callbacks the reload.
        /// входящее асинхронное сообщение для подписанных слушателей с передачей текущих параметров
        /// </summary>
        /// <param name="controlName">имя CTRL</param>
        /// <param name="controlParentName">имя родителя CNTRL</param>
        /// <param name="param">параметры ключ-значение.</param>
        private void CallbackReload(string controlName, string controlParentName, Dictionary<String, String> param)
        {
            if (param.Count() != 0)
            {
                Control[] cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(controlName) && c is DataGridView);
                ((DataGridView)cntrl[0]).DataSource = param;
            }
        }



        #endregion


    }

    #region //CALLBACK OutPut (собственные сообщения)

    /// <summary>
    /// CallBack_GetParam
    /// исходящее асинхронное сообщение для подписанных слушателей с передачей текущих параметров 
    /// </summary>
    public static class CallBack_GetParam
    {
        /// <summary>
        /// Delegate callbackEvent
        /// </summary>
        /// <param name="controlName">имя CTRL</param>
        /// <param name="controlParentName">имя родителя CNTRL</param>
        /// <param name="parameterPairs">параметры ключ-значение</param>
        public delegate void callbackEvent(string controlName, string controlParentName, Dictionary<String, String> parameterPairs);
        /// <summary>
        /// The callback event handler
        /// </summary>
        public static callbackEvent callbackEventHandler;
    }

    #endregion

    #region //EXTENSION
    /// <summary>
    /// Class ControlExtensions.
    /// мега супер пуппер расширение по поиску контрола (CTRL) - используется здесь везде!!!
    /// </summary>
    static public class ControlExtensions
    {
        /// <summary>
        /// Recurses through all controls, starting at given control,
        /// and returns an array of those matching the given criteria.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="isMatch">The is match.</param>
        /// <returns>массив найденных CTRL Control[].</returns>
        static public Control[] FilterControls(this Control start, Func<Control, bool> isMatch)
        {
            var matches = new List<Control>();

            Action<Control> filter = null;
            (filter = new Action<Control>(c =>
            {
                if (isMatch(c))
                    matches.Add(c);
                foreach (Control c2 in c.Controls)
                    filter(c2);
            }))(start);

            //            Control[] arrMatches = ControlExtensions.FilterControls(start, isMatch);
            //            return arrMatches.Length == 0 ? null : arrMatches[0];
            return matches.ToArray();
        }

        /// <summary>
        /// Gets the control path.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public static IEnumerable<string> GetControlPath(this Control c)
        {
            yield return c.Name;
            if (c.Parent != null)
            {
                Control parent = c.Parent;
                while (parent != null)
                {
                    yield return parent.Name;
                    parent = parent.Parent;
                }
            }
        }

        /// <summary>
        /// Gets the nestle level.
        /// получить уровень вложенности по отношению к родителю
        /// </summary>
        /// <param name="controlName">имя CTRL</param>
        /// <param name="parentName">имя CTRL родителя</param>
        /// <returns>глубина System.Int32.</returns>
        public static int GetNestleLevel(this Control controlName, string parentName)
        {
            int l = 1;
            if (controlName.Parent != null && controlName.Name != parentName)
            {
                Control parent = controlName.Parent;
                while (parent.Name != parentName)
                {
                    l++;//= l + 1;
                    parent = parent.Parent;
                }
            }
            return l;
        }

    }
    #endregion
}
