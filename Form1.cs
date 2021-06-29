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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxECC.SelectedIndex = 0; //Pre-select ECC level "L"
            textBoxQRCode.Text = "BEGIN:VCARD\r\n"+
                                 "VERSION:3.0\r\n" +
                                 "N:FirstName;LastName;NikName\r\n" +
                                 "TITLE:������������ ������\r\n" +
                                 "ORG:�� \'���\'\r\n" +
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
//            RenderQrCode();
        }

        private void viewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxQRCode.BackgroundImageLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), viewMode.Text);// Enum.GetName(typeof(ImageLayout), "2"); //Enum.Parse(typeof(ImageLayout), sender.ToString());
        }

        //��������� payload
        private void cbPayload_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeControlPlayloadPanel();   //�������� ������
            createControlPlayloadPanel();   //������� ������
        }

        //������� ������ playload
        private void removeControlPlayloadPanel()
        {
            if (panelPayload.HasChildren)
            {
                for (int i = this.panelPayload.Controls.Count - 1; i >= 0; i--) //foreach ������ ��������� ����������� � ��������� ���������
                {
                    Control cn = this.panelPayload.Controls[0];
               //     cn -= new System.EventHandler(cn, playload_Changed);
                    this.panelPayload.Controls.Remove(cn);
                    cn.Dispose();
                }
            }
        }

        //�������� ������ playload
        private void createControlPlayloadPanel()
        {
            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                int labelTop = 1;
                int labelLeft = 2;
                int controlLeft = 80;
                int offSet = 21;

                MemberInfo mi = qqRef.GetMemberByName(cbPayload.Text);  //�������� member �� �����
                IList propToCntrl = qqRef.GetFieldMember(mi);           //�������� fields member 

                foreach (FieldProperty prop in propToCntrl)
                {
                    if (panelPayload.HasChildren)
                    {
                        labelTop = labelTop + offSet;// panelPayload.Controls[panelPayload.Controls.Count-1].Location.Y;
                    }

                    Label lb = new Label();
                    lb.Size = new Size(70, 19);
                    lb.Location = new Point(labelLeft, labelTop);
                    lb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    //                    lb..Anchor = AnchorStyles..Top;// AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    //                    lb.Dock = DockStyle.Left | DockStyle.Top | DockStyle.Bottom;
                    lb.Text = prop.fName;
                    panelPayload.Controls.Add(lb);

                    switch (prop.fType)
                    {
                        case "TextBox":
                            TextBox tb = new TextBox();
                            lb.Size = new Size(70, 19);
                            tb.Location = new Point(controlLeft, labelTop);
                            lb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            tb.Name = "tb_" + prop.fName;
                            tb.TextChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(tb);
                            break;
                        case "CheckBox":
                            CheckBox chb = new CheckBox();
                            lb.Size = new Size(70, 19);
                            chb.Location = new Point(controlLeft, labelTop);
                            lb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            chb.Name = "chb_" + prop.fName;
                            chb.CheckedChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(chb);
                            break;
                        case "ComboBox":
                            ComboBox cmb = new ComboBox();
                            cmb.Size = new Size(100, 19);
                            // cmb.MinimumSize= new Size(70, 19);
                            cmb.Location = new Point(controlLeft, labelTop);
                            cmb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                            cmb.Name = "cmb_" + prop.fName;
                            cmb.DataSource = prop.fList;// Enum.GetValues(typeof(ImageLayout));
                            cmb.SelectedIndexChanged += new EventHandler(playload_Changed);
                            panelPayload.Controls.Add(cmb);
                            break;


                            /*
                                        //�������� ������ �� ������, �� ������� �� ������
                                        Button b = (Button)sender;
                                        //������� ����� ������
                                        Button temp = new Button();
                                        temp.Text = "������";
                                        temp.Width = b.Width;
                                        //��������� �� ������ (�� 10px) ������, �� ������� �� ������
                                        temp.Location = new Point(b.Location.X + b.Width + 10, b.Location.Y);
                                        //��������� ������� ������� �� ����� ������ 
                                        //(�� �� ��� � ��� ������� �� ��������)
                                        temp.Click += new EventHandler(button1_Click);
                                        //��������� ������� �� �����
                                        this.Controls.Add(temp);
                            */
                    }
                }

                cbConstructor.DataSource = new BindingSource(qqRef.GetConstructor(mi), null);
                cbConstructor.DisplayMember = "Key";
                cbConstructor.ValueMember = "Value";
                tbConstructor.Text = "Select the constructor (" + cbConstructor.Items.Count.ToString() + ") or enter the formatted text manually";
            }
        }

        private void buttonConstructor_Click(object sender, EventArgs e)
        {

        }
    }
}
