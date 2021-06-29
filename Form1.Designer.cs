

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
            this.textBoxQRCode = new System.Windows.Forms.TextBox();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.comboBoxECC = new System.Windows.Forms.ComboBox();
            this.labelECC = new System.Windows.Forms.Label();
            this.labelIconsize = new System.Windows.Forms.Label();
            this.iconSize = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.colorDialogPrimaryColor = new System.Windows.Forms.ColorDialog();
            this.panelPreviewPrimaryColor = new System.Windows.Forms.Panel();
            this.labelPreviewBackgroundColor = new System.Windows.Forms.Label();
            this.panelPreviewBackgroundColor = new System.Windows.Forms.Panel();
            this.colorDialogBackgroundColor = new System.Windows.Forms.ColorDialog();
            this.artPath = new System.Windows.Forms.TextBox();
            this.selectArtBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dotSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pixelSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.selectIconBtn = new System.Windows.Forms.Button();
            this.iconPath = new System.Windows.Forms.TextBox();
            this.viewMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelPayload = new System.Windows.Forms.Panel();
            this.cbPayload = new System.Windows.Forms.ComboBox();
            this.cbConstructor = new System.Windows.Forms.ComboBox();
            this.tbConstructor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxQRCode
            // 
            this.textBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQRCode.Location = new System.Drawing.Point(11, 34);
            this.textBoxQRCode.Multiline = true;
            this.textBoxQRCode.Name = "textBoxQRCode";
            this.textBoxQRCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxQRCode.Size = new System.Drawing.Size(394, 92);
            this.textBoxQRCode.TabIndex = 1;
            this.textBoxQRCode.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxQRCode.ErrorImage = global::RicQRCoderArt.Properties.Resources.qr1;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(11, 159);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Padding = new System.Windows.Forms.Padding(3);
            this.pictureBoxQRCode.Size = new System.Drawing.Size(394, 400);
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
            this.comboBoxECC.Location = new System.Drawing.Point(78, 2);
            this.comboBoxECC.Name = "comboBoxECC";
            this.comboBoxECC.Size = new System.Drawing.Size(58, 21);
            this.comboBoxECC.TabIndex = 3;
            this.comboBoxECC.SelectedIndexChanged += new System.EventHandler(this.setting_Changed);
            // 
            // labelECC
            // 
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(17, 6);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(57, 13);
            this.labelECC.TabIndex = 4;
            this.labelECC.Text = "ECC-Level";
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(19, 56);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(55, 13);
            this.labelIconsize.TabIndex = 8;
            this.labelIconsize.Text = "Logo size:";
            // 
            // iconSize
            // 
            this.iconSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconSize.Location = new System.Drawing.Point(78, 52);
            this.iconSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iconSize.Name = "iconSize";
            this.iconSize.Size = new System.Drawing.Size(58, 20);
            this.iconSize.TabIndex = 9;
            this.iconSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.iconSize.ValueChanged += new System.EventHandler(this.setting_Changed);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(318, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(87, 25);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // panelPreviewPrimaryColor
            // 
            this.panelPreviewPrimaryColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreviewPrimaryColor.BackColor = System.Drawing.Color.Black;
            this.panelPreviewPrimaryColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewPrimaryColor.Location = new System.Drawing.Point(188, 28);
            this.panelPreviewPrimaryColor.Name = "panelPreviewPrimaryColor";
            this.panelPreviewPrimaryColor.Size = new System.Drawing.Size(24, 19);
            this.panelPreviewPrimaryColor.TabIndex = 13;
            this.panelPreviewPrimaryColor.Click += new System.EventHandler(this.panelPreviewPrimaryColor_Click);
            // 
            // labelPreviewBackgroundColor
            // 
            this.labelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewBackgroundColor.AutoSize = true;
            this.labelPreviewBackgroundColor.Location = new System.Drawing.Point(215, 31);
            this.labelPreviewBackgroundColor.Name = "labelPreviewBackgroundColor";
            this.labelPreviewBackgroundColor.Size = new System.Drawing.Size(26, 13);
            this.labelPreviewBackgroundColor.TabIndex = 14;
            this.labelPreviewBackgroundColor.Text = "light";
            // 
            // panelPreviewBackgroundColor
            // 
            this.panelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreviewBackgroundColor.BackColor = System.Drawing.Color.White;
            this.panelPreviewBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewBackgroundColor.Location = new System.Drawing.Point(247, 28);
            this.panelPreviewBackgroundColor.Name = "panelPreviewBackgroundColor";
            this.panelPreviewBackgroundColor.Size = new System.Drawing.Size(21, 19);
            this.panelPreviewBackgroundColor.TabIndex = 15;
            this.panelPreviewBackgroundColor.Click += new System.EventHandler(this.panelPreviewBackgroundColor_Click);
            // 
            // artPath
            // 
            this.artPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artPath.Location = new System.Drawing.Point(188, 77);
            this.artPath.Name = "artPath";
            this.artPath.Size = new System.Drawing.Size(80, 20);
            this.artPath.TabIndex = 17;
            this.artPath.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // selectArtBtn
            // 
            this.selectArtBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectArtBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectArtBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectArtBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectArtBtn.Location = new System.Drawing.Point(271, 77);
            this.selectArtBtn.Name = "selectArtBtn";
            this.selectArtBtn.Size = new System.Drawing.Size(21, 20);
            this.selectArtBtn.TabIndex = 18;
            this.selectArtBtn.Text = "...";
            this.selectArtBtn.UseVisualStyleBackColor = true;
            this.selectArtBtn.Click += new System.EventHandler(this.selectArtBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Art: dot size:";
            // 
            // dotSize
            // 
            this.dotSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dotSize.Location = new System.Drawing.Point(78, 77);
            this.dotSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dotSize.Name = "dotSize";
            this.dotSize.Size = new System.Drawing.Size(58, 20);
            this.dotSize.TabIndex = 20;
            this.dotSize.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.dotSize.ValueChanged += new System.EventHandler(this.setting_Changed);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Pixel: size";
            // 
            // pixelSize
            // 
            this.pixelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pixelSize.BackColor = System.Drawing.SystemColors.Window;
            this.pixelSize.Location = new System.Drawing.Point(78, 27);
            this.pixelSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelSize.Name = "pixelSize";
            this.pixelSize.Size = new System.Drawing.Size(58, 20);
            this.pixelSize.TabIndex = 16;
            this.pixelSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.pixelSize.ValueChanged += new System.EventHandler(this.setting_Changed);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "dark";
            // 
            // selectIconBtn
            // 
            this.selectIconBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectIconBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectIconBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectIconBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectIconBtn.Location = new System.Drawing.Point(271, 52);
            this.selectIconBtn.Margin = new System.Windows.Forms.Padding(0);
            this.selectIconBtn.Name = "selectIconBtn";
            this.selectIconBtn.Size = new System.Drawing.Size(21, 20);
            this.selectIconBtn.TabIndex = 11;
            this.selectIconBtn.Text = "...";
            this.selectIconBtn.UseVisualStyleBackColor = true;
            this.selectIconBtn.Click += new System.EventHandler(this.selectIconBtn_Click);
            // 
            // iconPath
            // 
            this.iconPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconPath.Location = new System.Drawing.Point(188, 52);
            this.iconPath.Name = "iconPath";
            this.iconPath.Size = new System.Drawing.Size(80, 20);
            this.iconPath.TabIndex = 10;
            this.iconPath.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // viewMode
            // 
            this.viewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.viewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewMode.FormattingEnabled = true;
            this.viewMode.Items.AddRange(new object[] {
            "Center",
            "Zoom"});
            this.viewMode.Location = new System.Drawing.Point(212, 2);
            this.viewMode.Name = "viewMode";
            this.viewMode.Size = new System.Drawing.Size(80, 21);
            this.viewMode.TabIndex = 24;
            this.viewMode.SelectedIndexChanged += new System.EventHandler(this.viewMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "view mode";
            // 
            // panelPayload
            // 
            this.panelPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPayload.AutoScroll = true;
            this.panelPayload.Location = new System.Drawing.Point(407, 159);
            this.panelPayload.Name = "panelPayload";
            this.panelPayload.Size = new System.Drawing.Size(296, 400);
            this.panelPayload.TabIndex = 2;
            // 
            // cbPayload
            // 
            this.cbPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPayload.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayload.FormattingEnabled = true;
            this.cbPayload.Location = new System.Drawing.Point(408, 132);
            this.cbPayload.Name = "cbPayload";
            this.cbPayload.Size = new System.Drawing.Size(295, 21);
            this.cbPayload.TabIndex = 1;
            this.cbPayload.SelectedIndexChanged += new System.EventHandler(this.cbPayload_SelectedIndexChanged);
            // 
            // cbConstructor
            // 
            this.cbConstructor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbConstructor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConstructor.FormattingEnabled = true;
            this.cbConstructor.Location = new System.Drawing.Point(11, 132);
            this.cbConstructor.Name = "cbConstructor";
            this.cbConstructor.Size = new System.Drawing.Size(394, 21);
            this.cbConstructor.TabIndex = 27;
            this.cbConstructor.SelectedIndexChanged += new System.EventHandler(this.playload_Changed);
            // 
            // tbConstructor
            // 
            this.tbConstructor.AutoSize = true;
            this.tbConstructor.Location = new System.Drawing.Point(12, 9);
            this.tbConstructor.Name = "tbConstructor";
            this.tbConstructor.Size = new System.Drawing.Size(279, 13);
            this.tbConstructor.TabIndex = 28;
            this.tbConstructor.Text = "Select the constructor or enter the formatted text manually";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "path";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "path";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "payload (from QRCoder reflection)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.labelECC);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBoxECC);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dotSize);
            this.panel1.Controls.Add(this.pixelSize);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panelPreviewPrimaryColor);
            this.panel1.Controls.Add(this.selectIconBtn);
            this.panel1.Controls.Add(this.viewMode);
            this.panel1.Controls.Add(this.selectArtBtn);
            this.panel1.Controls.Add(this.labelPreviewBackgroundColor);
            this.panel1.Controls.Add(this.iconPath);
            this.panel1.Controls.Add(this.panelPreviewBackgroundColor);
            this.panel1.Controls.Add(this.artPath);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.labelIconsize);
            this.panel1.Controls.Add(this.iconSize);
            this.panel1.Location = new System.Drawing.Point(408, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 119);
            this.panel1.TabIndex = 30;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelPayload);
            this.Controls.Add(this.tbConstructor);
            this.Controls.Add(this.cbPayload);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.cbConstructor);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Controls.Add(this.textBoxQRCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(724, 599);
            this.Name = "Form1";
            this.Text = "QRCoderArt";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializeComponentFromQRCOderDll()
        {

            using (QRCoderReflection qqRef = new QRCoderReflection(typeof(QRCoder.PayloadGenerator).AssemblyQualifiedName))
            {
                this.cbPayload.DataSource = qqRef.GetNameMembersClass();        //поличить список имен классов members QRCoder.PayloadGenerator
            }



        }
        #endregion
        private System.Windows.Forms.TextBox textBoxQRCode;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.ComboBox comboBoxECC;
        private System.Windows.Forms.Label labelECC;
        private System.Windows.Forms.Label labelIconsize;
        private System.Windows.Forms.NumericUpDown iconSize;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColorDialog colorDialogPrimaryColor;
        private System.Windows.Forms.Panel panelPreviewPrimaryColor;
        private System.Windows.Forms.Panel panelPreviewBackgroundColor;
        private System.Windows.Forms.ColorDialog colorDialogBackgroundColor;
        private System.Windows.Forms.TextBox artPath;
        private System.Windows.Forms.Button selectArtBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown dotSize;
        private System.Windows.Forms.Label labelPreviewBackgroundColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectIconBtn;
        private System.Windows.Forms.TextBox iconPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown pixelSize;
        private System.Windows.Forms.ComboBox viewMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPayload;
        private System.Windows.Forms.Panel panelPayload;
        private System.Windows.Forms.ComboBox cbConstructor;
        private System.Windows.Forms.Label tbConstructor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
    }
}

