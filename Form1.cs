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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                //найдем абстрактный класс (payload)
                /*
                            string baseName = (from t in tRef.GetMembers(BindingFlags.Public)
                                               where ((System.Type)t).IsAbstract
                                               select t.Name).First();
                */
                string baseName = "Payload";
                this.cbPayload.DataSource = qqRef.GetMembersClassName(baseName);        //поличить список имен классов members QRCoder.PayloadGenerator
            }

            this.viewMode.DataSource = Enum.GetValues(typeof(ImageLayout));
            this.viewMode.SelectedIndex = 4;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            textBoxQRCode.Text = "enter your text or select payload + constructor + fill in the parameters";
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                     REFLECTION
         ------------------------------------------------------------------------------------------------------------------------------------------------*/
        //выбор контента payload 
        private void cbPayload_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {

                MemberInfo mi = qqRef.GetMemberByName(cbPayload.Text);                          //получить member по имени
                cbConstructor.DataSource = null;
                cbConstructor.Items.Clear();
                cbConstructor.DataSource = new BindingSource(qqRef.GetConstructor(mi), null);   //получить конструкторы member
                cbConstructor.DisplayMember = "Key";                                            //Имя    
                cbConstructor.ValueMember = "Value";                                            //значение                                                
                cbConstructor.SelectedItem = 0;
                tbConstructor.Text = "Payload (" + cbConstructor.Items.Count.ToString() + ")";
            }
        }

        //изменение конструктора
        private void cbConstructor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbConstructor.SelectedItem != null) 
            { 
                removeControlPlayloadPanel();           //очистить панель
                IList propToCntrl = null;
                using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
                {
                    propToCntrl = qqRef.GetParamsConstuctor(((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Value);
                }
                createControlPlayloadPanel(propToCntrl);   //создать панель по параметрам конструктора
            }
        }

        //очистка панели playload
        private void removeControlPlayloadPanel()
        {
            if (panelPayload.HasChildren)
            {
                for (int i = this.panelPayload.Controls.Count - 1; i >= 0; i--) //foreach нельзя коллекция уменьшается и нумерация сбивается
                {
                    Control cn = this.panelPayload.Controls[0];
                    //     cn -= new System.EventHandler(cn, playload_Changed);
                    this.panelPayload.Controls.Remove(cn);
                    cn.Dispose();
                }
            }
        }

        //создание панели playload
        private void createControlPlayloadPanel(IList controlsList)
        {
                int labelTop = 2;
                int labelLeft = 0;
                int controlLeft = 140;
                int offSet = 21;

                foreach (FieldProperty prop in controlsList)
                {
                    if (panelPayload.HasChildren)
                    {
                        labelTop = labelTop + offSet;// panelPayload.Controls[panelPayload.Controls.Count-1].Location.Y;
                    }

                    TextBox lb = new TextBox(); //Label();
        //            lb.ReadOnly = true;
                    lb.Enabled = false;
                    lb.BorderStyle = BorderStyle.FixedSingle;//.None;
                    lb.TextAlign = HorizontalAlignment.Right;
                    lb.Location = new Point(labelLeft, labelTop);
                    lb.Size = new Size(135, 20);
                    lb.Text = prop.fName;
                    panelPayload.Controls.Add(lb);

                    switch (prop.fForm)
                    {
                        case "TextBox":
                            TextBox tb = new TextBox();
                            tb.Location = new Point(controlLeft, labelTop);
                            tb.Size = new Size(140, 20);
                            tb.Name = "" + prop.fName;
                            tb.TextChanged += new EventHandler(GeyPayLoadStringFromForm);
                            toolTip1.SetToolTip(tb, prop.fType);    
                            switch (prop.fType)
                            {
                            case "Double":
                                tb.BackColor = Color.LightBlue;
                                tb.KeyPress += new KeyPressEventHandler(filterOnlyReal);
                                break;
                            }
                            panelPayload.Controls.Add(tb);
                            break;
                        case "CheckBox":
                            CheckBox chb = new CheckBox();
                            chb.Size = new Size(140, 20);
                            chb.Location = new Point(controlLeft, labelTop);
                            chb.Name = "" + prop.fName;
                            chb.CheckedChanged += new EventHandler(GeyPayLoadStringFromForm);
                            panelPayload.Controls.Add(chb);
                            break;
                        case "DateTime":
                            DateTimePicker dtp = new DateTimePicker();
                            dtp.Size = new Size(140, 20);
                            dtp.Location = new Point(controlLeft, labelTop);
                            dtp.Name = "" + prop.fName;
                            dtp.Format = DateTimePickerFormat.Short;
                            dtp.ValueChanged += new EventHandler(GeyPayLoadStringFromForm);
                            panelPayload.Controls.Add(dtp);
                            break;
                        case "ComboBox":
                            ComboBox cmb = new ComboBox();
                            cmb.Size = new Size(140, 20);
                            cmb.Location = new Point(controlLeft, labelTop);
                            cmb.Name = "" + prop.fName;
                            cmb.DataSource = new BindingSource(prop.fList, null);   //получить конструкторы member
                            cmb.DisplayMember = "Key";                                            //Имя    
                            cmb.ValueMember = "Value";                                            //значение  
                            cmb.SelectedItem = 0;
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
        //проверка на цифры с системным разделителем
        private void filterOnlyReal(object sender, KeyPressEventArgs e)
        {
            string sep= ((float)1 / 2).ToString().Substring(1, 1);
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == Convert.ToChar(sep)) && (((TextBox)sender).Text.IndexOf(sep) == -1) && (((TextBox)sender).Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
        //общее событие генерации строки Payload
        private void GeyPayLoadStringFromForm(object sender, EventArgs e)
        {
            ArrayList ParamFromControl = new ArrayList();
            if (panelPayload.HasChildren)
            {
                foreach (Control cntrl in panelPayload.Controls) 
                {
                    if (cntrl.Created && cntrl.Enabled) 
                    {
                        switch (cntrl.GetType().Name)
                            {
                            case "TextBox":
                                ParamFromControl.Add(((TextBox)cntrl).Text);
                                break;
                            case "CheckBox":
                                ParamFromControl.Add(((CheckBox)cntrl).Checked);
                                break;
                            case "ComboBox":
                                ParamFromControl.Add(((KeyValuePair<string, object>)((ComboBox)cntrl).SelectedItem).Value);
                                break;
                            case "DateTimePicker":
                                ParamFromControl.Add(((DateTimePicker)cntrl).Value);
                                break;
                        }
                    }
                }
            }
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                textBoxQRCode.Text = qqRef.GetPayloadString(((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Value, ParamFromControl);
            }
        }
        //общее событие при изменении настроек
        private void setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();     //генерация QR
        }
        ////генерация QR
        private void RenderQrCode()
        {
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
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (FileStream fs = (System.IO.FileStream) saveFileDialog1.OpenFile())
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

        //расположение картинки в окне просмотра
        private void viewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }


    }
}
