

namespace QRCoderArt
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
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
            this.cbPayload = new System.Windows.Forms.ComboBox();
            this.tbConstructor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelPayload = new System.Windows.Forms.FlowLayoutPanel();
            this.outSplitContainer = new System.Windows.Forms.SplitContainer();
            this.qRCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outSplitContainer)).BeginInit();
            this.outSplitContainer.Panel1.SuspendLayout();
            this.outSplitContainer.Panel2.SuspendLayout();
            this.outSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qRCodeBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxQRCode
            // 
            this.textBoxQRCode.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxQRCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxQRCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxQRCode.Location = new System.Drawing.Point(0, 0);
            this.textBoxQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxQRCode.Multiline = true;
            this.textBoxQRCode.Name = "textBoxQRCode";
            this.textBoxQRCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxQRCode.Size = new System.Drawing.Size(534, 225);
            this.textBoxQRCode.TabIndex = 1;
            this.textBoxQRCode.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.ErrorImage = global::QRCoderArt.Properties.Resources.qr1;
            this.pictureBoxQRCode.InitialImage = global::QRCoderArt.Properties.Resources.qr1;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Padding = new System.Windows.Forms.Padding(4);
            this.pictureBoxQRCode.Size = new System.Drawing.Size(526, 400);
            this.pictureBoxQRCode.TabIndex = 2;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // comboBoxECC
            // 
            this.comboBoxECC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxECC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxECC.FormattingEnabled = true;
            this.comboBoxECC.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.comboBoxECC.Location = new System.Drawing.Point(116, 7);
            this.comboBoxECC.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxECC.Name = "comboBoxECC";
            this.comboBoxECC.Size = new System.Drawing.Size(76, 24);
            this.comboBoxECC.TabIndex = 3;
            this.comboBoxECC.SelectedIndexChanged += new System.EventHandler(this.setting_Changed);
            // 
            // labelECC
            // 
            this.labelECC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(69, 12);
            this.labelECC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(37, 17);
            this.labelECC.TabIndex = 4;
            this.labelECC.Text = "level";
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(75, 63);
            this.labelIconsize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(33, 17);
            this.labelIconsize.TabIndex = 8;
            this.labelIconsize.Text = "size";
            // 
            // iconSize
            // 
            this.iconSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconSize.Location = new System.Drawing.Point(116, 60);
            this.iconSize.Margin = new System.Windows.Forms.Padding(1);
            this.iconSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iconSize.Name = "iconSize";
            this.iconSize.Size = new System.Drawing.Size(77, 22);
            this.iconSize.TabIndex = 9;
            this.iconSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.iconSize.ValueChanged += new System.EventHandler(this.setting_Changed);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(413, 4);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(116, 31);
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
            this.panelPreviewPrimaryColor.Location = new System.Drawing.Point(247, 34);
            this.panelPreviewPrimaryColor.Margin = new System.Windows.Forms.Padding(1);
            this.panelPreviewPrimaryColor.Name = "panelPreviewPrimaryColor";
            this.panelPreviewPrimaryColor.Size = new System.Drawing.Size(31, 23);
            this.panelPreviewPrimaryColor.TabIndex = 13;
            this.panelPreviewPrimaryColor.Click += new System.EventHandler(this.panelPreviewPrimaryColor_Click);
            // 
            // labelPreviewBackgroundColor
            // 
            this.labelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewBackgroundColor.AutoSize = true;
            this.labelPreviewBackgroundColor.Location = new System.Drawing.Point(283, 38);
            this.labelPreviewBackgroundColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPreviewBackgroundColor.Name = "labelPreviewBackgroundColor";
            this.labelPreviewBackgroundColor.Size = new System.Drawing.Size(34, 17);
            this.labelPreviewBackgroundColor.TabIndex = 14;
            this.labelPreviewBackgroundColor.Text = "light";
            // 
            // panelPreviewBackgroundColor
            // 
            this.panelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreviewBackgroundColor.BackColor = System.Drawing.Color.White;
            this.panelPreviewBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewBackgroundColor.Location = new System.Drawing.Point(325, 34);
            this.panelPreviewBackgroundColor.Margin = new System.Windows.Forms.Padding(1);
            this.panelPreviewBackgroundColor.Name = "panelPreviewBackgroundColor";
            this.panelPreviewBackgroundColor.Size = new System.Drawing.Size(27, 23);
            this.panelPreviewBackgroundColor.TabIndex = 15;
            this.panelPreviewBackgroundColor.Click += new System.EventHandler(this.panelPreviewBackgroundColor_Click);
            // 
            // artPath
            // 
            this.artPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artPath.Location = new System.Drawing.Point(248, 86);
            this.artPath.Margin = new System.Windows.Forms.Padding(1);
            this.artPath.Name = "artPath";
            this.artPath.Size = new System.Drawing.Size(104, 22);
            this.artPath.TabIndex = 17;
            this.toolTip1.SetToolTip(this.artPath, "the picture below the qr code image");
            this.artPath.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // selectArtBtn
            // 
            this.selectArtBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectArtBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectArtBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectArtBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectArtBtn.Location = new System.Drawing.Point(357, 85);
            this.selectArtBtn.Margin = new System.Windows.Forms.Padding(1);
            this.selectArtBtn.Name = "selectArtBtn";
            this.selectArtBtn.Size = new System.Drawing.Size(28, 25);
            this.selectArtBtn.TabIndex = 18;
            this.selectArtBtn.Text = "...";
            this.selectArtBtn.UseVisualStyleBackColor = true;
            this.selectArtBtn.Click += new System.EventHandler(this.selectArtBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "dot size";
            // 
            // dotSize
            // 
            this.dotSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dotSize.BackColor = System.Drawing.SystemColors.Window;
            this.dotSize.Location = new System.Drawing.Point(116, 86);
            this.dotSize.Margin = new System.Windows.Forms.Padding(1);
            this.dotSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dotSize.Name = "dotSize";
            this.dotSize.Size = new System.Drawing.Size(77, 22);
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
            this.label1.Location = new System.Drawing.Point(73, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "size";
            // 
            // pixelSize
            // 
            this.pixelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pixelSize.BackColor = System.Drawing.SystemColors.Window;
            this.pixelSize.Location = new System.Drawing.Point(116, 34);
            this.pixelSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pixelSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelSize.Name = "pixelSize";
            this.pixelSize.Size = new System.Drawing.Size(77, 22);
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
            this.label3.Location = new System.Drawing.Point(201, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "dark";
            // 
            // selectIconBtn
            // 
            this.selectIconBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectIconBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectIconBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectIconBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectIconBtn.Location = new System.Drawing.Point(357, 59);
            this.selectIconBtn.Margin = new System.Windows.Forms.Padding(1);
            this.selectIconBtn.Name = "selectIconBtn";
            this.selectIconBtn.Size = new System.Drawing.Size(28, 25);
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
            this.iconPath.Location = new System.Drawing.Point(248, 60);
            this.iconPath.Margin = new System.Windows.Forms.Padding(1);
            this.iconPath.Name = "iconPath";
            this.iconPath.Size = new System.Drawing.Size(103, 22);
            this.iconPath.TabIndex = 10;
            this.toolTip1.SetToolTip(this.iconPath, "the picture above the qr code image");
            this.iconPath.TextChanged += new System.EventHandler(this.setting_Changed);
            // 
            // viewMode
            // 
            this.viewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewMode.FormattingEnabled = true;
            this.viewMode.Items.AddRange(new object[] {
            "Center",
            "Zoom"});
            this.viewMode.Location = new System.Drawing.Point(91, 7);
            this.viewMode.Margin = new System.Windows.Forms.Padding(4);
            this.viewMode.Name = "viewMode";
            this.viewMode.Size = new System.Drawing.Size(105, 24);
            this.viewMode.TabIndex = 24;
            this.viewMode.SelectedIndexChanged += new System.EventHandler(this.viewMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "view mode";
            // 
            // cbPayload
            // 
            this.cbPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPayload.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayload.FormattingEnabled = true;
            this.cbPayload.Location = new System.Drawing.Point(116, 113);
            this.cbPayload.Margin = new System.Windows.Forms.Padding(1);
            this.cbPayload.Name = "cbPayload";
            this.cbPayload.Size = new System.Drawing.Size(269, 24);
            this.cbPayload.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cbPayload, "Select PAYLOAD and Set property");
            this.cbPayload.SelectedIndexChanged += new System.EventHandler(this.RebuildingPanelUpload);
            // 
            // tbConstructor
            // 
            this.tbConstructor.Location = new System.Drawing.Point(9, 114);
            this.tbConstructor.Margin = new System.Windows.Forms.Padding(1);
            this.tbConstructor.Name = "tbConstructor";
            this.tbConstructor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbConstructor.Size = new System.Drawing.Size(73, 21);
            this.tbConstructor.TabIndex = 28;
            this.tbConstructor.Text = "Payload";
            this.tbConstructor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "path";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "path";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbConstructor);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.cbPayload);
            this.panel1.Controls.Add(this.comboBoxECC);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.labelECC);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dotSize);
            this.panel1.Controls.Add(this.pixelSize);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panelPreviewPrimaryColor);
            this.panel1.Controls.Add(this.selectIconBtn);
            this.panel1.Controls.Add(this.selectArtBtn);
            this.panel1.Controls.Add(this.labelPreviewBackgroundColor);
            this.panel1.Controls.Add(this.iconPath);
            this.panel1.Controls.Add(this.panelPreviewBackgroundColor);
            this.panel1.Controls.Add(this.artPath);
            this.panel1.Controls.Add(this.labelIconsize);
            this.panel1.Controls.Add(this.iconSize);
            this.panel1.Location = new System.Drawing.Point(547, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 145);
            this.panel1.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 89);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 17);
            this.label10.TabIndex = 31;
            this.label10.Text = "Art";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 17);
            this.label11.TabIndex = 32;
            this.label11.Text = "ECC";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 63);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 17);
            this.label9.TabIndex = 30;
            this.label9.Text = "Logo";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Pixel";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panelPayload);
            this.panel2.Location = new System.Drawing.Point(547, 158);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 530);
            this.panel2.TabIndex = 33;
            // 
            // panelPayload
            // 
            this.panelPayload.AutoScroll = true;
            this.panelPayload.Location = new System.Drawing.Point(3, 2);
            this.panelPayload.Margin = new System.Windows.Forms.Padding(0);
            this.panelPayload.Name = "panelPayload";
            this.panelPayload.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelPayload.Size = new System.Drawing.Size(387, 522);
            this.panelPayload.TabIndex = 3;
            // 
            // outSplitContainer
            // 
            this.outSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outSplitContainer.Location = new System.Drawing.Point(4, 9);
            this.outSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.outSplitContainer.Name = "outSplitContainer";
            this.outSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // outSplitContainer.Panel1
            // 
            this.outSplitContainer.Panel1.Controls.Add(this.textBoxQRCode);
            // 
            // outSplitContainer.Panel2
            // 
            this.outSplitContainer.Panel2.Controls.Add(this.pictureBoxQRCode);
            this.outSplitContainer.Size = new System.Drawing.Size(536, 678);
            this.outSplitContainer.SplitterDistance = 227;
            this.outSplitContainer.TabIndex = 34;
            // 
            // qRCodeBindingSource
            // 
            this.qRCodeBindingSource.DataSource = typeof(QRCoder.QRCode);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.viewMode);
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(4, 647);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 40);
            this.panel3.TabIndex = 3;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 690);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.outSplitContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(957, 721);
            this.Name = "mainForm";
            this.Text = "QRCoderArt";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.outSplitContainer.Panel1.ResumeLayout(false);
            this.outSplitContainer.Panel1.PerformLayout();
            this.outSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outSplitContainer)).EndInit();
            this.outSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qRCodeBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label tbConstructor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer outSplitContainer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel panelPayload;
        private System.Windows.Forms.BindingSource qRCodeBindingSource;
    }
}

