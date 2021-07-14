// ***********************************************************************
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
using System.Collections;
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
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormMain : Form
    {
        //To fix errors (when controls have not yet been created) at the first
        /// <summary>
        /// The ready state
        /// </summary>
        private bool[] readyState = {false,                 //Data preparation completed
                                     false,                 //MainForm is Load
                                     false };               //Mainform is Show    

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain" /> class.
        /// </summary>
        public FormMain()
        {
            CallBack_SetParam.callbackEventHandler = new CallBack_SetParam.callbackEvent(this.CallbackReload);    //subscribe (listen) to the general notification


            InitializeComponent();
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                //find abstract class (payload)
                /*
                            string baseName = (from t in tRef.GetMembers(BindingFlags.Public)
                                               where ((System.Type)t).IsAbstract
                                               select t.Name).First();
                */
                string baseName = "Payload";
                this.cbPayload.DataSource = qqRef.GetMembersClassName(baseName);        //get list names ckasses members QRCoder.PayloadGenerator
            }

            this.viewMode.DataSource = Enum.GetValues(typeof(ImageLayout));
            this.viewMode.SelectedIndex = 4;
        }

        /// <summary>
        /// Handles the Load event of the FormMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            textBoxQRCode.Text = "enter your text or select payload + constructor + fill in the parameters";
            readyState[1] = true;           //MainForm is Load
        }
        /// <summary>
        /// Handles the Shown event of the FormMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            readyState[2] = true;           //Mainform is Show
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     REFLECTION
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
        //clear payload panel
        /// <summary>
        /// Removes the control playload panel.
        /// </summary>
        /// <param name="panel">The panel.</param>
        private void RemoveControlPlayloadPanel(Control panel)
        {
            if (panel.HasChildren)
            {
                for (int i = panel.Controls.Count - 1; i >= 0; i--) //not foreach use...
                {
                    Control cn = panel.Controls[0];
                    //     cn -= new System.EventHandler(cn, playload_Changed);
                    panel.Controls.Remove(cn);
                    cn.Dispose();
                }
            }
        }

        //create payload panel from constructor parameters
        /// <summary>
        /// Creates the control playload panel.
        /// </summary>
        /// <param name="controlsList">The controls list.</param>
        /// <param name="panel">The panel.</param>
        private void CreateControlPlayloadPanel(IList controlsList, Control panel)
        {
            int labelWidth = 135;// 122;// 135;
            int sizeWidth = 263;
            int reverseShift = 5;   // paddind compensation for nested controls
            int controlWidth;
            Panel tPanel;

            Padding padding = new Padding(0, 1, 1, 1);

            /* to paint panels in random colors
              
                        Color[] bColor = {  Color.AliceBlue, 
                                            Color.Beige, 
                                            Color.AliceBlue, 
                                            Color.BlanchedAlmond, 
                                            Color.Linen,
                                            Color.GhostWhite,
                                            Color.Snow,
                                            Color.LightGoldenrodYellow,
                                            Color.WhiteSmoke };
                        Random rnd = new Random();
                        ...
                    foreach
                        ...Color = bColor[rnd.Next(0, 8)];
            */

            Stack<Panel> panels = new Stack<Panel>();
            panels.Push((Panel)panel);

            panelPayload.Visible = false;       //render off

            foreach (FieldProperty prop in controlsList)
            {
                //                 this.Refresh();
                controlWidth = prop.fNull ? 113 : 128;
                //                controlWidth = prop.fNull ? 103 : 128;

                if (panels.Peek().Name != prop.fParentName && panels.Peek().Name != panel.Name)
                {
                    tPanel = panels.Pop();  //go back to the previous panel
                }

                Label lb = new Label
                {
                    Name = prop.fName,
                    AutoSize = false,
                    Margin = padding
                }; //Label();
                if (prop.fType != "Constructor")
                {
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    lb.Text = prop.fName;
                    lb.Size = new Size(labelWidth - prop.fLevel * reverseShift, 20);
                    panels.Peek().Controls.Add(lb);
                }
                else
                {
                    lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    lb.Text = prop.fName + " constructor's"; //(panels.Peek().Name == "panelPayload" ? "Class: " : "subClass: ") + prop.fName + " constructor's";
                    lb.Size = new Size(sizeWidth - prop.fLevel * reverseShift, 20);
                    panels.Peek().Controls.Add(lb);
                }

                switch (prop.fForm)
                {
                    case "TextBox":
                        TextBox tb = new TextBox
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.fName,
                            AccessibleName = "Get",
                            AccessibleDescription = prop.fType                          //type in tooltype
                        };
                        tb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        tb.TextChanged += new EventHandler(GetPayloadStringFromForm);
                        switch (prop.fType)
                        {
                            case "Single":
                            case "Int32":
                            case "Decimal":
                            case "Double":
                                tb.BackColor = Color.GhostWhite;//.OldLace;// LightBlue;
                                tb.KeyPress += new KeyPressEventHandler(FilterOnlyReal);
                                tb.Text = prop.fDef == null ? "" : Convert.ToString(prop.fDef);
                                break;
                            default:
                                tb.Text = prop.fDef == null ? "" : Convert.ToString(prop.fDef);
                                break;
                        }
                        panels.Peek().Controls.Add(tb);
                        if (prop.fNull)
                        {
                            CheckBox chtb = new CheckBox
                            {
                                Size = new Size(13, 20),
                                Margin = padding,
                                Name = "" + prop.fName,
                                AccessibleDescription = "Nullable"                          //type in tooltype
                            };
                            chtb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                            chtb.CheckedChanged += (sender, e) => tb.Enabled = (chtb.CheckState == CheckState.Checked); // GeyPayLoadStringFromForm(null, null);
                            panels.Peek().Controls.Add(chtb);
                            tb.Enabled = false;// chtb.Checked;
                        }
                        break;
                    case "CheckBox":
                        CheckBox chb = new CheckBox
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.fName,
                            AccessibleName = "Get",
                            AccessibleDescription = prop.fType
                        };
                        chb.CheckedChanged += new EventHandler(GetPayloadStringFromForm);
                        panels.Peek().Controls.Add(chb);
                        break;
                    case "dataGridView":
                        DataGridView dgv = new DataGridView
                        {
                            Size = new Size(103, 20),
                            Margin = padding,
                            Name = "" + prop.fName,
                            DataSource = new Dictionary<string, string> { ["plugin"] = "plugin + pluginOption", ["-"] = "-" }, //!!! refresh from callback -> CallBack_GetParam
                                                                                                                               //new Dictionary<string, string>{["plugin"] = plugin + (string.IsNullOrEmpty(pluginOption)? "": $";{pluginOption}")}
                            AccessibleName = "Get",
                            AccessibleDescription = prop.fType
                        };
                        dgv.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        dgv.DataSourceChanged += new EventHandler(GetPayloadStringFromForm);
                        panels.Peek().Controls.Add(dgv);

                        // case "Button":          //for custom parameter
                        Button bt = new Button
                        {
                            Size = new Size(23, 20),
                            Margin = padding,
                            Name = "" + prop.fName,
                            Text = "...",
                            AccessibleName = "",// "Get";
                            FlatStyle = FlatStyle.System,
                            AccessibleDescription = "setting a custom parameter"// prop.fType;
                        };
                        bt.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        bt.Click += new EventHandler(GetPropretyForParameter);
                        //                        bt.Click += (sender, e) => {dictionaryForm a = new dictionaryForm(); a.ShowDialog(); };
                        panels.Peek().Controls.Add(bt);
                        break;
                    case "DateTime":
                        DateTimePicker dtp = new DateTimePicker
                        {
                            Size = new Size(controlWidth, 20),
                            Margin = padding,
                            Name = "" + prop.fName,
                            AccessibleName = "Get",
                            AccessibleDescription = prop.fType,
                            Format = DateTimePickerFormat.Short
                        };
                        dtp.ValueChanged += new EventHandler(GetPayloadStringFromForm);
                        dtp.EnabledChanged += new EventHandler(GetPayloadStringFromForm);
                        panels.Peek().Controls.Add(dtp);
                        if (prop.fNull)
                        {
                            CheckBox chdtp = new CheckBox
                            {
                                Size = new Size(13, 20),
                                Margin = padding,
                                Name = "" + prop.fName,
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
                            Name = "" + prop.fName,
                            AccessibleName = "Get",
                            //cmb.AccessibleDescription = prop.fType;
                            DataSource = new BindingSource(prop.fList, null),   //получить конструкторы member
                            DisplayMember = "Key",                                            //Имя    
                            ValueMember = "Value",                                            //значение  
                            SelectedItem = 0,
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        if (prop.fType == "Constructor")
                        {
                            //  cmb.Name += "ctor_";
                            cmb.AccessibleDescription = "Constructor";
                            cmb.SelectedIndexChanged += new EventHandler(RebuildingPanelUpload);
                            panels.Peek().Controls.Add(cmb);
                            cmb.Size = new Size(sizeWidth - prop.fLevel * reverseShift, 20);


                            FlowLayoutPanel cPanel = new FlowLayoutPanel
                            {
                                Name = "" + prop.fName,
                                AutoSize = true,
                                Padding = new Padding(0, 2, 0, 2),
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            panels.Peek().Controls.Add(cPanel);
                            panels.Push(cPanel);

                            Control cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(prop.fName) && c is Label).First();
                            cntrl.Text += " (" + prop.fList.Count.ToString() + ")";
                        }
                        else
                        {
                            cmb.SelectedIndexChanged += new EventHandler(GetPayloadStringFromForm);
                            cmb.Size = new Size(controlWidth, 20);
                            cmb.Margin = padding;
                            panels.Peek().Controls.Add(cmb);
                        }
                        //                      cmb.EndUpdate();
                        break;
                }
            }
            panelPayload.Visible = true;    //render on
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     EVENTS
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
        //show tooltip from AccessibleDescription
        /// <summary>
        /// Tools the tip mouse hover.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ToolTipMouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((System.Windows.Forms.Control)sender,
                               ((System.Windows.Forms.Control)sender).AccessibleDescription);
        }
        //check numeric wiht system separator 
        /// <summary>
        /// Filters the only real.
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
        //edit property generic collection
        /// <summary>
        /// Gets the proprety for parameter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void GetPropretyForParameter(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            FormDictionary a = new FormDictionary();

            Control[] cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(bt.Name) && c is DataGridView);
            CallBack_GetParam.callbackEventHandler(bt.Name, bt.Parent.Name, (Dictionary<String, String>)((DataGridView)cntrl[0]).DataSource);  //send a general notification
            a.Owner = this;
            a.ShowDialog();
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------*/


        //update Payload control panel
        /// <summary>
        /// Rebuildings the panel upload.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void RebuildingPanelUpload(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            Control[] panel = combo.Name == "cbPayload" ?
                                this.FilterControls(c => c.Name != null && c.Name.Equals("panelPayload") && c is FlowLayoutPanel) :
                                this.FilterControls(c => c.Name != null && c.Name.Equals(combo.Name) && c is FlowLayoutPanel);

            if (combo.SelectedItem != null)
            {
                readyState[0] = false;                                      //full ready:= Data preparation not completed
                RemoveControlPlayloadPanel(panel.First());                  //clear payload panel
                IList propToCntrl = null;
                using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
                {
                    if (combo.Name == "cbPayload")
                    {
                        propToCntrl = qqRef.GetParamsObject(qqRef.GetMemberByName(combo.Text));
                    }
                    else
                    {
                        propToCntrl = qqRef.GetParamsCtor(((ConstructorInfo)((KeyValuePair<string, object>)combo.SelectedItem).Value), combo.Name, panel[0].GetNestleLevel("panelPayload"));
                    }
                }
                CreateControlPlayloadPanel(propToCntrl, panel.First());    //create payload panel from constructor parameters
                                                                           //                this.Refresh();
                readyState[0] = true;                                      //full ready:= Data preparation completed
                GetPayloadStringFromForm(null, null);
            }
        }


        //get param from panel control
        /// <summary>
        /// Gets the parameter from panel.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <returns>Dictionary&lt;System.String, System.Object&gt;.</returns>
        private Dictionary<string, object> GetParamFromPanel(FlowLayoutPanel panel)
        {
            object ret = null;
            Dictionary<string, object> ParamFromControl = new Dictionary<string, object>();
            if (panelPayload.HasChildren)
            {
                foreach (Control cntrl in panel.Controls)
                {
                    if (cntrl.Created && cntrl.AccessibleName == "Get")
                    {
                        ret = null;
                        if (cntrl.Enabled)
                        {
                            switch (cntrl.AccessibleDescription)
                            {
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

        //get string (invoke ToString() execute) Payload from panel control
        /// <summary>
        /// Gets the payload string from form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void GetPayloadStringFromForm(object sender, EventArgs e)
        {
            if (readyState[0] && readyState[1] && readyState[2])            //full ready:= Data preparation completed && form load && form show
            {
                Dictionary<string, object> ParamFromControl = null;// = new Dictionary<string, object>();

                /*
                                //collect all current payload constructors
                                Control[] ctrls = panelPayload.FilterControls(c => c.Name != null
                                                                        && c.AccessibleDescription != null
                                                                        && c.AccessibleDescription == "Constructor"
                                                                        && c is ComboBox);
                                //init all ctor from panel controls         
                                for (int i = 0; i < ctrls.Count(); i++)
                                 {
                                    ComboBox cb = (ComboBox)ctrls[i];
                                    ConstructorInfo ctor = (ConstructorInfo)((KeyValuePair<string, object>)cb.SelectedItem).Value;
                                    Control[] pan = panelPayload.FilterControls(c => c.Name != null
                                                                            && c.Name == cb.Name && c is FlowLayoutPanel);

                                }
                */

                //init single ctor from panel controls (delete later)
                Control[] panel = this.FilterControls(c => c.Name != null && c.Name.Equals(cbPayload.Text) && c is FlowLayoutPanel);
                ParamFromControl = GetParamFromPanel((FlowLayoutPanel)panel[0]);

                using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
                {
                    Control[] cmb = this.FilterControls(c => c.Name != null && c.Name.Equals(cbPayload.Text) && c is ComboBox);
                    ConstructorInfo ctrm = (ConstructorInfo)((System.Collections.Generic.KeyValuePair<string, object>)((ComboBox)cmb[0]).SelectedItem).Value;
                    textBoxQRCode.Text = qqRef.GetPayloadString(ctrm, ParamFromControl);
                }
            }
        }
        //change parameters panel Payload
        /// <summary>
        /// Handles the Changed event of the Setting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();     //create QR image
        }
        //create QR image
        /// <summary>
        /// Renders the qr code.
        /// </summary>
        private void RenderQrCode()
        {
            if (textBoxQRCode.Text.IndexOf("Error:") >= 0)
            {
                textBoxQRCode.BackColor = System.Drawing.Color.WhiteSmoke;
                pictureBoxQRCode.BackgroundImage = global::QRCoderArt.Properties.Resources.qr1;
            }
            else
            {
                textBoxQRCode.BackColor = SystemColors.Window;
                if (comboBoxECC.SelectedItem != null)
                {
                    string level = comboBoxECC.SelectedItem.ToString();
                    QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(textBoxQRCode.Text, eccLevel))

                        if (artPath.Text.Length == 0)
                        {
                            using (QRCode qrCode = new QRCode(qrCodeData))
                            {
                                pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic((int)pixelSize.Value, GetPrimaryColor(), GetBackgroundColor(),
                                        GetIconBitmap(), (int)iconSize.Value);
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
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     INTERFACE
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
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

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                CALLBACK return
        ------------------------------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Callbacks the reload.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="controlParentName">Name of the control parent.</param>
        /// <param name="param">The parameter.</param>
        private void CallbackReload(string controlName, string controlParentName, Dictionary<String, String> param)
        {
            if (param.Count() != 0)
            {
                Control[] cntrl = this.FilterControls(c => c.Name != null && c.Name.Equals(controlName) && c is DataGridView);
                ((DataGridView)cntrl[0]).DataSource = param;
            }
        }
    }
    /*-----------------------------------------------------------------------------------------------------------------------------------------------
                 EXTENSION
     ------------------------------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    /// Class ControlExtensions.
    /// </summary>
    static public class ControlExtensions
    {
        /// <summary>
        /// Recurses through all controls, starting at given control,
        /// and returns an array of those matching the given criteria.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="isMatch">The is match.</param>
        /// <returns>Control[].</returns>
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
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="parentName">Name of the parent.</param>
        /// <returns>System.Int32.</returns>
        public static int GetNestleLevel(this Control c, string parentName)
        {
            int l = 1;
            if (c.Parent != null && c.Name != parentName)
            {
                Control parent = c.Parent;
                while (parent.Name != parentName)
                {
                    l++;//= l + 1;
                    parent = parent.Parent;
                }
            }
            return l;
        }

    }

    /*-----------------------------------------------------------------------------------------------------------------------------------------------
             CALLBACK
     ------------------------------------------------------------------------------------------------------------------------------------------------*/
    //general notification
    /// <summary>
    /// Class CallBack_GetParam.
    /// </summary>
    public static class CallBack_GetParam
    {
        /// <summary>
        /// Delegate callbackEvent
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="controlParentName">Name of the control parent.</param>
        /// <param name="par">The par.</param>
        public delegate void callbackEvent(string controlName, string controlParentName, Dictionary<String, String> par);
        /// <summary>
        /// The callback event handler
        /// </summary>
        public static callbackEvent callbackEventHandler;
    }

}
