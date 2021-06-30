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

namespace RicQRCoderArt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComponentFromQRCOderDll();



            this.viewMode.DataSource = Enum.GetValues(typeof(ImageLayout));
            this.viewMode.SelectedIndex = 4;
            toolTip1.SetToolTip(this.cbPayload, "Select PAYLOAD and Set property");
            toolTip1.SetToolTip(this.cbConstructor, "Select Constructor PAYLOAD");
            toolTip1.SetToolTip(this.iconPath, "the picture above the qr code image");
            toolTip1.SetToolTip(this.artPath, "the picture below the qr code image");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            textBoxQRCode.Text = "BEGIN:VCARD\r\n"+
                                 "VERSION:3.0\r\n" +
                                 "N:FirstName;LastName;NikName\r\n" +
                                 "TITLE:Руководитель отдела\r\n" +
                                 "ORG:АО \'РПК\'\r\n" +
                                 "EMAIL;TYPE=INTERNET:info@cad.ru\r\n" +
                                 "TEL;TYPE=voice,work:7(495)7440004,175\r\n" +
                                 "TEL;TYPE=voice,cell:\r\n" +
                                 "END:VCARD";
            RenderQrCode();
             }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            RenderQrCode();
        }

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

        private void setting_Changed(object sender, EventArgs e)
        {
            RenderQrCode();
        }

        private void playload_Changed(object sender, EventArgs e)
        {
            string key = ((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Key;
            ConstructorInfo value = ((KeyValuePair<string, ConstructorInfo>)cbConstructor.SelectedItem).Value;

            object magicClassObject2 = value.Invoke(new object[] { "MyWiFi-SSID", "MyWiFi-Pass", PayloadGenerator.WiFi.Authentication.WPA, false });
            MethodInfo magicMethod2 = value.ReflectedType.GetMethod("ToString");
            textBoxQRCode.Text = magicMethod2.Invoke(magicClassObject2, null).ToString();
        }
   //     ((System.Reflection.RuntimeConstructorInfo) value).ReflectedType.GetMethod("ToString")
        private void viewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }

        //изменение payload
        private void cbPayload_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeControlPlayloadPanel();   //очистить панель
            createControlPlayloadPanel();   //создать панель
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
        private void createControlPlayloadPanel()
        {
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                int labelTop = 1;
                int labelLeft = 5;
                int controlLeft = 150;
                int offSet = 21;

                MemberInfo mi = qqRef.GetMemberByName(cbPayload.Text);  //получить member по имени
                IList propToCntrl = qqRef.GetFieldMember(mi);           //получить fields member 

                foreach (FieldProperty prop in propToCntrl)
                {
                    if (panelPayload.HasChildren)
                    {
                        labelTop = labelTop + offSet;// panelPayload.Controls[panelPayload.Controls.Count-1].Location.Y;
                    }

                    TextBox lb = new TextBox(); //Label();
                    lb.ReadOnly = true;
                    lb.BorderStyle = BorderStyle.FixedSingle;//.None;
                    lb.TextAlign = HorizontalAlignment.Right;
                    lb.Location = new Point(labelLeft, labelTop);
                    lb.Size = new Size(140, 20);
          //          lb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    //                    lb..Anchor = AnchorStyles..Top;// AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    //                    lb.Dock = DockStyle.Left | DockStyle.Top | DockStyle.Bottom;
                    lb.Text = prop.fName;
                    panelPayload.Controls.Add(lb);

                    switch (prop.fType)
                    {
                        case "TextBox":
                            TextBox tb = new TextBox();
                            tb.Location = new Point(controlLeft, labelTop);
                            tb.Size = new Size(140, 20);
            //                lb.Anchor =  AnchorStyles.Right;//AnchorStyles.Left |
                            tb.Name = "tb_" + prop.fName;
                            tb.TextChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(tb);
                            break;
                        case "CheckBox":
                            CheckBox chb = new CheckBox();
                            chb.Size = new Size(140, 20);
                            chb.Location = new Point(controlLeft, labelTop);
                       //     lb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            chb.Name = "chb_" + prop.fName;
                            chb.CheckedChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(chb);
                            break;
                        case "ComboBox":
                            ComboBox cmb = new ComboBox();
                            cmb.Size = new Size(140, 20);
                            // cmb.MinimumSize= new Size(70, 19);
                            cmb.Location = new Point(controlLeft, labelTop);
                        //    cmb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            cmb.Name = "cmb_" + prop.fName;
                            cmb.DataSource = prop.fList;// Enum.GetValues(typeof(ImageLayout));
                            cmb.SelectedIndexChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(cmb);
                            break;


                            /*
                                        //получаем ссылку на кнопку, на которую мы нажали
                                        Button b = (Button)sender;
                                        //Создаем новую кнопку
                                        Button temp = new Button();
                                        temp.Text = "Пример";
                                        temp.Width = b.Width;
                                        //Размещаем ее правее (на 10px) кнопки, на которую мы нажали
                                        temp.Location = new Point(b.Location.X + b.Width + 10, b.Location.Y);
                                        //Добавляем событие нажатия на новую кнопку 
                                        //(то же что и при нажатии на исходную)
                                        temp.Click += new EventHandler(button1_Click);
                                        //Добавляем элемент на форму
                                        this.Controls.Add(temp);
                            */
                    }
                }

                cbConstructor.DataSource = new BindingSource(qqRef.GetConstructor(mi), null);
                cbConstructor.DisplayMember = "Key";
                cbConstructor.ValueMember = "Value";
                tbConstructor.Text = "Select the Payload constructor (" + cbConstructor.Items.Count.ToString() + ")";
            }
        }

        private void buttonConstructor_Click(object sender, EventArgs e)
        {

        }
    }
}
