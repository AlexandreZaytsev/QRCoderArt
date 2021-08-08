﻿// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : zaytsev
// Created          : 07-14-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="FormMain.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
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
            //     using (GUITree qqRef = new GUITree(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            //     {
            //find abstract class (payload)
            /*
                        string baseName = (from t in tRef.GetMembers(BindingFlags.Public)
                                           where ((System.Type)t).IsAbstract
                                           select t.Name).First();
            */
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
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            QRCodeString.Text = "enter your text or select payload + constructor + fill in the parameters";
            readyState[1] = true;           //MainForm is Load
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

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     REFLECTION
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
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

        /// <summary>
        /// Setting_Changed
        /// общеия обработчик события изменения параметров панели GUITree 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();     //create QR image
        }

        /// <summary>
        /// RenderQrCode
        /// создать картинку QR кода
        /// </summary>
        private void RenderQrCode()
        {
            if (comboBoxECC.SelectedItem != null)
            {
                string level = comboBoxECC.SelectedItem.ToString();
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRCodeString.Text, eccLevel))

                    if (artPath.Text.Length == 0)
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic((int)pixelSize.Value, GetPrimaryColor(), GetBackgroundColor(), GetIconBitmap(), (int)iconSize.Value);
                            this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                            //Set the SizeMode to center the image.
                            this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
                            pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        using (ArtQRCode qrCode = new ArtQRCode(qrCodeData))
                        {
                            pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic((int)dotSize.Value, GetPrimaryColor(), GetBackgroundColor(), GetArtBitmap());// (20, GetPrimaryColor(), GetBackgroundColor(), GetIconBitmap(), (int)iconSize.Value);
                            this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                            //Set the SizeMode to center the image.
                            this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;
                            pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
            }
        }

        /*--------------------------------------------------------------------------------------------  
                     INTERFACE
        --------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Gets the icon bitmap.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap GetIconBitmap()
        {
            if (iconPath.Text.Length == 0)
            {
                return null;
            }
            try
            {
                return new Bitmap(iconPath.Text);
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
            if (artPath.Text.Length == 0)
            {
                return null;
            }
            try
            {
                return new Bitmap(artPath.Text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Handles the Click event of the SelectIconBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SelectIconBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog
            {
                Title = "Select icon",
                Multiselect = false,
                CheckFileExists = true
            };
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                iconPath.Text = openFileDlg.FileName;
                if (iconSize.Value == 0)
                {
                    iconSize.Value = 15;
                }
            }
            else
            {
                iconPath.Text = "";
            }
        }


        /// <summary>
        /// Handles the Click event of the Btn_save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotSupportedException">File extension is not supported</exception>
        private void Btn_save_Click(object sender, EventArgs e)
        {

            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPeg Image|*.jpg|Gif Image|*.gif",
                Title = "Save an Image File"
            };
            _ = saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.

                    ImageFormat imageFormat = null;
                    switch (saveFileDialog1.FilterIndex)
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

        /// <summary>
        /// Handles the Click event of the PanelPreviewPrimaryColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void PanelPreviewPrimaryColor_Click(object sender, EventArgs e)
        {
            if (colorDialogPrimaryColor.ShowDialog() == DialogResult.OK)
            {
                panelPreviewPrimaryColor.BackColor = colorDialogPrimaryColor.Color;
                RenderQrCode();
            }
        }

        /// <summary>
        /// Handles the Click event of the PanelPreviewBackgroundColor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void PanelPreviewBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialogBackgroundColor.ShowDialog() == DialogResult.OK)
            {
                panelPreviewBackgroundColor.BackColor = colorDialogBackgroundColor.Color;
                RenderQrCode();
            }
        }

        /// <summary>
        /// Gets the color of the primary.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetPrimaryColor()
        {
            return panelPreviewPrimaryColor.BackColor;
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetBackgroundColor()
        {
            return panelPreviewBackgroundColor.BackColor;
        }

        /// <summary>
        /// Handles the Click event of the SelectArtBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void SelectArtBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog
            {
                Title = "Select art",
                Multiselect = false,
                CheckFileExists = true
            };
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                artPath.Text = openFileDlg.FileName;
                // if (iconSize.Value == 0)
                // {
                //     iconSize.Value = 15;
                // }
            }
            else
            {
                artPath.Text = "";
            }
        }

        //position image from frame 
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ViewMode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }

        /// <summary>
        /// Handles the HelpButtonClicked event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            FormAbout a = new FormAbout();
            a.ShowDialog();
        }

        /*--------------------------------------------------------------------------------------------  
            CALLBACK InPut (подписка на внешние сообщения)
        --------------------------------------------------------------------------------------------*/
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
    }

    /*--------------------------------------------------------------------------------------------  
        CALLBACK OutPut (собственные сообщения)
    --------------------------------------------------------------------------------------------*/
    //general notification
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

    /*--------------------------------------------------------------------------------------------  
                 EXTENSION
    --------------------------------------------------------------------------------------------*/

    /// <summary>
    /// Class ControlExtensions.
    /// мега супер пуппер расширение по поиску CTRL - используется здесь везде!!!
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
}
