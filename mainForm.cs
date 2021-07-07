using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Collections;

namespace QRCoderArt
{
    public partial class mainForm : Form
    {
        private bool completePayloadPanel = false;
        public mainForm()
        {
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

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            textBoxQRCode.Text = "enter your text or select payload + constructor + fill in the parameters";
            //      GeyPayLoadStringFromForm(null, null);
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     REFLECTION
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
        //select payload content  
        private void cbPayload_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {

                MemberInfo mi = qqRef.GetMemberByName(cbPayload.Text);                          //get member from name
                cbConstructor.DataSource = null;
                cbConstructor.Items.Clear();
                cbConstructor.DataSource = new BindingSource(qqRef.GetConstructor(mi), null);   //get constructors from member
                cbConstructor.DisplayMember = "Key";                                            //name    
                cbConstructor.ValueMember = "Value";                                            //value                                                
                cbConstructor.SelectedItem = 0;
                tbConstructor.Text = "Payload (" + cbConstructor.Items.Count.ToString() + ")";
            }
        }

        //change constructor cjmbobox select
        private void cbConstructor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConstructor.SelectedItem != null)
            {
                completePayloadPanel = false;
                removeControlPlayloadPanel();               //clear payload panel
                IList propToCntrl = null;
                using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
                {
                    propToCntrl = qqRef.GetParamsConstuctor(((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Value);
                }
                createControlPlayloadPanel(propToCntrl);    //create payload panel from constructor parameters
                completePayloadPanel = true;
                //!!!
                GeyPayLoadStringFromForm(null, null);
            }
        }

        //clear payload panel
        private void removeControlPlayloadPanel()
        {
            if (panelPayload.HasChildren)
            {
                for (int i = this.panelPayload.Controls.Count - 1; i >= 0; i--) //not foreach use...
                {
                    Control cn = this.panelPayload.Controls[0];
                    //     cn -= new System.EventHandler(cn, playload_Changed);
                    this.panelPayload.Controls.Remove(cn);
                    cn.Dispose();
                }
            }
        }

        //create payload panel from constructor parameters
        private void createControlPlayloadPanel(IList controlsList)
        {
            int labelTop = 2;
            int labelLeft = 0;
            int controlLeft = 140;
            int offSet = 21;
            int labelWidth = 135;
            int controlWidth;

            foreach (FieldProperty prop in controlsList)
            {
                if (panelPayload.HasChildren)
                {
                    labelTop += offSet;
                }
                controlWidth = prop.fNull ? 125 : 140;

                TextBox lb = new TextBox(); //Label();
                lb.Enabled = false;
                lb.BorderStyle = BorderStyle.FixedSingle;//.None;
                lb.TextAlign = HorizontalAlignment.Right;
                lb.Location = new Point(labelLeft, labelTop);
                lb.Size = new Size(labelWidth, 20);
                lb.Text = prop.fName;
                //                    lb.AccessibleDescription = prop.fType;
                panelPayload.Controls.Add(lb);
                switch (prop.fForm)
                {
                    case "TextBox":
                        TextBox tb = new TextBox();
                        tb.Location = new Point(controlLeft, labelTop);
                        tb.Size = new Size(controlWidth, 20);
                        tb.Name = "" + prop.fName;
                        tb.AccessibleName = "Get";
                        tb.AccessibleDescription = prop.fType;                          //type in tooltype
                        tb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                        tb.TextChanged += new EventHandler(GeyPayLoadStringFromForm);
                        //    tb.EnabledChanged += new EventHandler(GeyPayLoadStringFromForm);
                        switch (prop.fType)
                        {
                            case "Single":
                            case "Int32":
                            case "Decimal":
                            case "Double":
                                tb.BackColor = Color.GhostWhite;//.OldLace;// LightBlue;
                                tb.KeyPress += new KeyPressEventHandler(filterOnlyReal);
                                tb.Text = prop.fDef == null ? "" : Convert.ToString(prop.fDef);
                                break;
                            default:
                                tb.Text = prop.fDef == null ? "" : Convert.ToString(prop.fDef);
                                break;
                        }
                        panelPayload.Controls.Add(tb);
                        if (prop.fNull)
                        {
                            CheckBox chtb = new CheckBox();
                            chtb.Size = new Size(13, 20);
                            chtb.Location = new Point(controlLeft + controlWidth + 2, labelTop);
                            chtb.Name = "" + prop.fName;
                            chtb.AccessibleDescription = "Nullable";                          //type in tooltype
                            chtb.MouseHover += new System.EventHandler(ToolTipMouseHover);
                            chtb.CheckedChanged += (sender, e) => tb.Enabled = (chtb.CheckState == CheckState.Checked); // GeyPayLoadStringFromForm(null, null);
                            panelPayload.Controls.Add(chtb);
                            tb.Enabled = false;// chtb.Checked;
                        }
                        break;
                    case "CheckBox":
                        CheckBox chb = new CheckBox();
                        chb.Size = new Size(controlWidth, 20);
                        chb.Location = new Point(controlLeft, labelTop);
                        chb.Name = "" + prop.fName;
                        chb.AccessibleName = "Get";
                        chb.AccessibleDescription = prop.fType;
                        chb.CheckedChanged += new EventHandler(GeyPayLoadStringFromForm);
                        //                 chb.Checked = Convert.ToBoolean(prop.fDef);// prop.fDef == null ? false : Convert.ToBoolean(prop.fDef);
                        panelPayload.Controls.Add(chb);
                        break;
                    case "DateTime":
                        DateTimePicker dtp = new DateTimePicker();
                        dtp.Size = new Size(controlWidth, 20);
                        dtp.Location = new Point(controlLeft, labelTop);
                        dtp.Name = "" + prop.fName;
                        dtp.AccessibleName = "Get";
                        dtp.AccessibleDescription = prop.fType;
                        dtp.Format = DateTimePickerFormat.Short;
                        dtp.ValueChanged += new EventHandler(GeyPayLoadStringFromForm);
                        dtp.EnabledChanged += new EventHandler(GeyPayLoadStringFromForm);
                        //           dtp.Value = prop.fDef == null ? DateTime.Today : Convert.ToDateTime(prop.fDef);
                        panelPayload.Controls.Add(dtp);
                        if (prop.fNull)
                        {
                            CheckBox chdtp = new CheckBox();
                            chdtp.Size = new Size(13, 20);
                            chdtp.Location = new Point(controlLeft + controlWidth + 2, labelTop);
                            chdtp.Name = "" + prop.fName;
                            chdtp.AccessibleDescription = "Nullable";                          //type in tooltype
                            chdtp.MouseHover += new System.EventHandler(ToolTipMouseHover);
                            chdtp.CheckedChanged += (sender, e) => dtp.Enabled = (chdtp.CheckState == CheckState.Checked);// GeyPayLoadStringFromForm(null, null);
                            panelPayload.Controls.Add(chdtp);
                            dtp.Enabled = false;// chdtp.Checked;
                        }
                        break;
                    case "ComboBox":
                        ComboBox cmb = new ComboBox();
                        cmb.Size = new Size(controlWidth, 20);
                        cmb.Location = new Point(controlLeft, labelTop);
                        cmb.Name = "" + prop.fName;
                        cmb.AccessibleName = "Get";
                        cmb.AccessibleDescription = prop.fType;
                        cmb.DataSource = new BindingSource(prop.fList, null);   //�������� ������������ member
                        cmb.DisplayMember = "Key";                                            //���    
                        cmb.ValueMember = "Value";                                            //��������  
                        cmb.SelectedItem = 0;
                        //  cmb.SelectedItem  = prop.fDef;// == null ? DateTime.Today : Convert.ToDateTime(prop.fDef);
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.SelectedIndexChanged += new EventHandler(GeyPayLoadStringFromForm);
                        panelPayload.Controls.Add(cmb);
                        break;
                }
            }
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     EVENTS
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
        //show tooltip from AccessibleDescription
        private void ToolTipMouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((System.Windows.Forms.Control)sender,
                               ((System.Windows.Forms.Control)sender).AccessibleDescription);
        }
        //check numeric wiht system separator 
        private void filterOnlyReal(object sender, KeyPressEventArgs e)
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
        //get string Payload from panel control
        private void GeyPayLoadStringFromForm(object sender, EventArgs e)
        {
            if (completePayloadPanel)
            {
                Dictionary<string, object> ParamFromControl = new Dictionary<string, object>();
                object ret = null;
                if (panelPayload.HasChildren)
                {
                    foreach (Control cntrl in panelPayload.Controls)
                    {
                        //                        if (cntrl.Created && cntrl.AccessibleName == "Get")
                        if (cntrl.Created && completePayloadPanel && cntrl.AccessibleName == "Get")
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
                using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
                {
                    textBoxQRCode.Text = qqRef.GetPayloadString(((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Value, ParamFromControl);

                }
            }
        }
        //change parameters panel Payload
        private void setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();     //create QR image
        }
        //create QR image
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

        private void selectIconBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Title = "Select icon";
            openFileDlg.Multiselect = false;
            openFileDlg.CheckFileExists = true;
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


        private void btn_save_Click(object sender, EventArgs e)
        {

            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPeg Image|*.jpg|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
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

        private void panelPreviewPrimaryColor_Click(object sender, EventArgs e)
        {
            if (colorDialogPrimaryColor.ShowDialog() == DialogResult.OK)
            {
                panelPreviewPrimaryColor.BackColor = colorDialogPrimaryColor.Color;
                RenderQrCode();
            }
        }

        private void panelPreviewBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialogBackgroundColor.ShowDialog() == DialogResult.OK)
            {
                panelPreviewBackgroundColor.BackColor = colorDialogBackgroundColor.Color;
                RenderQrCode();
            }
        }

        private Color GetPrimaryColor()
        {
            return panelPreviewPrimaryColor.BackColor;
        }

        private Color GetBackgroundColor()
        {
            return panelPreviewBackgroundColor.BackColor;
        }

        private void selectArtBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Title = "Select art";
            openFileDlg.Multiselect = false;
            openFileDlg.CheckFileExists = true;
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
        private void viewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            aboutForm a = new aboutForm();
            a.ShowDialog();
        }
    }
}