namespace RicQRCoderArt
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.textBoxQRCode = new System.Windows.Forms.TextBox();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.comboBoxECC = new System.Windows.Forms.ComboBox();
            this.labelECC = new System.Windows.Forms.Label();
            this.labelIcon = new System.Windows.Forms.Label();
            this.iconPath = new System.Windows.Forms.TextBox();
            this.selectIconBtn = new System.Windows.Forms.Button();
            this.labelIconsize = new System.Windows.Forms.Label();
            this.iconSize = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.colorDialogPrimaryColor = new System.Windows.Forms.ColorDialog();
            this.labelPreviewPrimaryColor = new System.Windows.Forms.Label();
            this.panelPreviewPrimaryColor = new System.Windows.Forms.Panel();
            this.labelPreviewBackgroundColor = new System.Windows.Forms.Label();
            this.panelPreviewBackgroundColor = new System.Windows.Forms.Panel();
            this.colorDialogBackgroundColor = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.artPath = new System.Windows.Forms.TextBox();
            this.selectArtBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dotSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenerate.Location = new System.Drawing.Point(12, 560);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(114, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // textBoxQRCode
            // 
            this.textBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQRCode.Location = new System.Drawing.Point(12, 36);
            this.textBoxQRCode.Multiline = true;
            this.textBoxQRCode.Name = "textBoxQRCode";
            this.textBoxQRCode.Size = new System.Drawing.Size(510, 49);
            this.textBoxQRCode.TabIndex = 1;
            this.textBoxQRCode.Text = "BEGIN:VCARD\n" +
                                      "VERSION:3.0\n"+
                                      "N:FirstName;LastName;NikName\n" +
                                      "TITLE:Руководитель отдела\n" +
                                      "ORG:АО \'РПК\'\n" +
                                      "EMAIL;TYPE=INTERNET:info@cad.ru\n" +
                                      "TEL;TYPE=voice,work:7(495)7440004,175\n" +
                                      "TEL;TYPE=voice,cell:\n" + 
                                      "END:VCARD";
            this.textBoxQRCode.TextChanged += new System.EventHandler(this.textBoxQRCode_TextChanged);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(12, 144);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(510, 410);
            this.pictureBoxQRCode.TabIndex = 2;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // comboBoxECC
            // 
            this.comboBoxECC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxECC.FormattingEnabled = true;
            this.comboBoxECC.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.comboBoxECC.Location = new System.Drawing.Point(72, 12);
            this.comboBoxECC.Name = "comboBoxECC";
            this.comboBoxECC.Size = new System.Drawing.Size(37, 21);
            this.comboBoxECC.TabIndex = 3;
            this.comboBoxECC.SelectedIndexChanged += new System.EventHandler(this.comboBoxECC_SelectedIndexChanged);
            // 
            // labelECC
            // 
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(9, 16);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(57, 13);
            this.labelECC.TabIndex = 4;
            this.labelECC.Text = "ECC-Level";
            // 
            // labelIcon
            // 
            this.labelIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelIcon.AutoSize = true;
            this.labelIcon.Location = new System.Drawing.Point(115, 95);
            this.labelIcon.Name = "labelIcon";
            this.labelIcon.Size = new System.Drawing.Size(31, 13);
            this.labelIcon.TabIndex = 5;
            this.labelIcon.Text = "Icon:";
            // 
            // iconPath
            // 
            this.iconPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconPath.Location = new System.Drawing.Point(152, 91);
            this.iconPath.Name = "iconPath";
            this.iconPath.Size = new System.Drawing.Size(299, 20);
            this.iconPath.TabIndex = 6;
            // 
            // selectIconBtn
            // 
            this.selectIconBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectIconBtn.Location = new System.Drawing.Point(457, 91);
            this.selectIconBtn.Name = "selectIconBtn";
            this.selectIconBtn.Size = new System.Drawing.Size(61, 20);
            this.selectIconBtn.TabIndex = 7;
            this.selectIconBtn.Text = "Select";
            this.selectIconBtn.UseVisualStyleBackColor = true;
            this.selectIconBtn.Click += new System.EventHandler(this.selectIconBtn_Click);
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(14, 95);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(52, 13);
            this.labelIconsize.TabIndex = 8;
            this.labelIconsize.Text = "Icon size:";
            // 
            // iconSize
            // 
            this.iconSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconSize.Location = new System.Drawing.Point(72, 91);
            this.iconSize.Name = "iconSize";
            this.iconSize.Size = new System.Drawing.Size(37, 20);
            this.iconSize.TabIndex = 9;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(425, 558);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(97, 25);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // labelPreviewPrimaryColor
            // 
            this.labelPreviewPrimaryColor.AutoSize = true;
            this.labelPreviewPrimaryColor.Location = new System.Drawing.Point(300, 16);
            this.labelPreviewPrimaryColor.Name = "labelPreviewPrimaryColor";
            this.labelPreviewPrimaryColor.Size = new System.Drawing.Size(74, 13);
            this.labelPreviewPrimaryColor.TabIndex = 12;
            this.labelPreviewPrimaryColor.Text = "Color: Primary ";
            // 
            // panelPreviewPrimaryColor
            // 
            this.panelPreviewPrimaryColor.BackColor = System.Drawing.Color.Black;
            this.panelPreviewPrimaryColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewPrimaryColor.Location = new System.Drawing.Point(380, 13);
            this.panelPreviewPrimaryColor.Name = "panelPreviewPrimaryColor";
            this.panelPreviewPrimaryColor.Size = new System.Drawing.Size(32, 19);
            this.panelPreviewPrimaryColor.TabIndex = 13;
            this.panelPreviewPrimaryColor.Click += new System.EventHandler(this.panelPreviewPrimaryColor_Click);
            // 
            // labelPreviewBackgroundColor
            // 
            this.labelPreviewBackgroundColor.AutoSize = true;
            this.labelPreviewBackgroundColor.Location = new System.Drawing.Point(418, 16);
            this.labelPreviewBackgroundColor.Name = "labelPreviewBackgroundColor";
            this.labelPreviewBackgroundColor.Size = new System.Drawing.Size(65, 13);
            this.labelPreviewBackgroundColor.TabIndex = 14;
            this.labelPreviewBackgroundColor.Text = "Background";
            // 
            // panelPreviewBackgroundColor
            // 
            this.panelPreviewBackgroundColor.BackColor = System.Drawing.Color.White;
            this.panelPreviewBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewBackgroundColor.Location = new System.Drawing.Point(489, 13);
            this.panelPreviewBackgroundColor.Name = "panelPreviewBackgroundColor";
            this.panelPreviewBackgroundColor.Size = new System.Drawing.Size(29, 19);
            this.panelPreviewBackgroundColor.TabIndex = 15;
            this.panelPreviewBackgroundColor.Click += new System.EventHandler(this.panelPreviewBackgroundColor_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Art:";
            // 
            // artPath
            // 
            this.artPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.artPath.Location = new System.Drawing.Point(152, 114);
            this.artPath.Name = "artPath";
            this.artPath.Size = new System.Drawing.Size(299, 20);
            this.artPath.TabIndex = 17;
            // 
            // selectArtBtn
            // 
            this.selectArtBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectArtBtn.Location = new System.Drawing.Point(457, 116);
            this.selectArtBtn.Name = "selectArtBtn";
            this.selectArtBtn.Size = new System.Drawing.Size(61, 20);
            this.selectArtBtn.TabIndex = 18;
            this.selectArtBtn.Text = "Select";
            this.selectArtBtn.UseVisualStyleBackColor = true;
            this.selectArtBtn.Click += new System.EventHandler(this.selectArtBtn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Dot size:";
            // 
            // dotSize
            // 
            this.dotSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dotSize.Location = new System.Drawing.Point(72, 115);
            this.dotSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dotSize.Name = "dotSize";
            this.dotSize.Size = new System.Drawing.Size(37, 20);
            this.dotSize.TabIndex = 20;
            this.dotSize.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 595);
            this.Controls.Add(this.dotSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectArtBtn);
            this.Controls.Add(this.artPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelPreviewBackgroundColor);
            this.Controls.Add(this.labelPreviewBackgroundColor);
            this.Controls.Add(this.panelPreviewPrimaryColor);
            this.Controls.Add(this.labelPreviewPrimaryColor);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.iconSize);
            this.Controls.Add(this.labelIconsize);
            this.Controls.Add(this.selectIconBtn);
            this.Controls.Add(this.iconPath);
            this.Controls.Add(this.labelIcon);
            this.Controls.Add(this.labelECC);
            this.Controls.Add(this.comboBoxECC);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Controls.Add(this.textBoxQRCode);
            this.Controls.Add(this.buttonGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(559, 602);
            this.Name = "Form1";
            this.Text = "QRCoder";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxQRCode;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.ComboBox comboBoxECC;
        private System.Windows.Forms.Label labelECC;
        private System.Windows.Forms.Label labelIcon;
        private System.Windows.Forms.TextBox iconPath;
        private System.Windows.Forms.Button selectIconBtn;
        private System.Windows.Forms.Label labelIconsize;
        private System.Windows.Forms.NumericUpDown iconSize;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColorDialog colorDialogPrimaryColor;
        private System.Windows.Forms.Label labelPreviewPrimaryColor;
        private System.Windows.Forms.Panel panelPreviewPrimaryColor;
        private System.Windows.Forms.Label labelPreviewBackgroundColor;
        private System.Windows.Forms.Panel panelPreviewBackgroundColor;
        private System.Windows.Forms.ColorDialog colorDialogBackgroundColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox artPath;
        private System.Windows.Forms.Button selectArtBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown dotSize;
    }
}

