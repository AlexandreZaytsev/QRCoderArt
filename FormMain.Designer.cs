// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : zaytsev
// Created          : 07-13-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="FormMain.Designer.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace QRCoderArt
{
    /// <summary>
    /// Class FormMain.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
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
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.eccLevel = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.colorDialogSetting = new System.Windows.Forms.ColorDialog();
            this.artPatternPath = new System.Windows.Forms.TextBox();
            this.artIconButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.artPixelSizeFactor = new System.Windows.Forms.NumericUpDown();
            this.viewMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPayload = new System.Windows.Forms.ComboBox();
            this.tbConstructor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.baseTabControl = new System.Windows.Forms.TabControl();
            this.Base = new System.Windows.Forms.TabPage();
            this.baseMode = new System.Windows.Forms.CheckBox();
            this.baseDrawQuietZones = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.baseLightColor = new System.Windows.Forms.Panel();
            this.labelPreviewBackgroundColor = new System.Windows.Forms.Label();
            this.basePixelsPerModule = new System.Windows.Forms.NumericUpDown();
            this.baseDarkColor = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.TabPage();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.labelIconsize = new System.Windows.Forms.Label();
            this.logoIconSizePercent = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.logoIconBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.logoBackColor = new System.Windows.Forms.Panel();
            this.logoIconPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.logoIconButton = new System.Windows.Forms.Button();
            this.Art = new System.Windows.Forms.TabPage();
            this.artIconPath = new System.Windows.Forms.TextBox();
            this.panelArt = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.artPatternButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.artBackgroundImageStyle = new System.Windows.Forms.ComboBox();
            this.artBackgroundColor = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.artQuietZoneRenderingStyle = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.QRCodeString = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelPayload = new System.Windows.Forms.FlowLayoutPanel();
            this.outSplitContainer = new System.Windows.Forms.SplitContainer();
            this.QRCodeError = new System.Windows.Forms.WebBrowser();
            this.qRCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.openFileDialogSetting = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogSetting = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artPixelSizeFactor)).BeginInit();
            this.panel1.SuspendLayout();
            this.baseTabControl.SuspendLayout();
            this.Base.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basePixelsPerModule)).BeginInit();
            this.Logo.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoIconSizePercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoIconBorderWidth)).BeginInit();
            this.Art.SuspendLayout();
            this.panelArt.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outSplitContainer)).BeginInit();
            this.outSplitContainer.Panel1.SuspendLayout();
            this.outSplitContainer.Panel2.SuspendLayout();
            this.outSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qRCodeBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.ErrorImage = global::QRCoderArt.Properties.Resources.qr_no;
            this.pictureBoxQRCode.InitialImage = global::QRCoderArt.Properties.Resources.qr_no;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Padding = new System.Windows.Forms.Padding(4);
            this.pictureBoxQRCode.Size = new System.Drawing.Size(523, 413);
            this.pictureBoxQRCode.TabIndex = 2;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // eccLevel
            // 
            this.eccLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eccLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eccLevel.FormattingEnabled = true;
            this.eccLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.eccLevel.Location = new System.Drawing.Point(84, 9);
            this.eccLevel.Margin = new System.Windows.Forms.Padding(1);
            this.eccLevel.Name = "eccLevel";
            this.eccLevel.Size = new System.Drawing.Size(61, 24);
            this.eccLevel.TabIndex = 3;
            this.eccLevel.SelectedIndexChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(411, 4);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(116, 31);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // artPatternPath
            // 
            this.artPatternPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artPatternPath.Location = new System.Drawing.Point(103, 84);
            this.artPatternPath.Margin = new System.Windows.Forms.Padding(1);
            this.artPatternPath.Name = "artPatternPath";
            this.artPatternPath.ReadOnly = true;
            this.artPatternPath.Size = new System.Drawing.Size(267, 22);
            this.artPatternPath.TabIndex = 17;
            this.toolTip1.SetToolTip(this.artPatternPath, "the picture below the qr code image");
            this.artPatternPath.TextChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // artIconButton
            // 
            this.artIconButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.artIconButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.artIconButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.artIconButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.artIconButton.Location = new System.Drawing.Point(76, 6);
            this.artIconButton.Margin = new System.Windows.Forms.Padding(1);
            this.artIconButton.Name = "artIconButton";
            this.artIconButton.Size = new System.Drawing.Size(28, 25);
            this.artIconButton.TabIndex = 18;
            this.artIconButton.Text = "...";
            this.artIconButton.UseVisualStyleBackColor = true;
            this.artIconButton.Click += new System.EventHandler(this.SelectArtBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "pixelSizeFactor";
            // 
            // artPixelSizeFactor
            // 
            this.artPixelSizeFactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.artPixelSizeFactor.BackColor = System.Drawing.SystemColors.Window;
            this.artPixelSizeFactor.DecimalPlaces = 1;
            this.artPixelSizeFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.artPixelSizeFactor.Location = new System.Drawing.Point(315, 0);
            this.artPixelSizeFactor.Margin = new System.Windows.Forms.Padding(1);
            this.artPixelSizeFactor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.artPixelSizeFactor.Name = "artPixelSizeFactor";
            this.artPixelSizeFactor.Size = new System.Drawing.Size(53, 22);
            this.artPixelSizeFactor.TabIndex = 20;
            this.toolTip1.SetToolTip(this.artPixelSizeFactor, "Value between 0.0 to 1.0 that defines how big the module dots are. \r\nThe bigger t" +
        "he value, the less round the dots will be.");
            this.artPixelSizeFactor.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            this.artPixelSizeFactor.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // viewMode
            // 
            this.viewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewMode.FormattingEnabled = true;
            this.viewMode.Items.AddRange(new object[] {
            "Center",
            "Zoom"});
            this.viewMode.Location = new System.Drawing.Point(95, 7);
            this.viewMode.Margin = new System.Windows.Forms.Padding(4);
            this.viewMode.Name = "viewMode";
            this.viewMode.Size = new System.Drawing.Size(105, 24);
            this.viewMode.TabIndex = 24;
            this.viewMode.SelectedIndexChanged += new System.EventHandler(this.ViewMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 11);
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
            this.cbPayload.Location = new System.Drawing.Point(83, 186);
            this.cbPayload.Margin = new System.Windows.Forms.Padding(1);
            this.cbPayload.Name = "cbPayload";
            this.cbPayload.Size = new System.Drawing.Size(303, 24);
            this.cbPayload.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cbPayload, "Select PAYLOAD and Set property");
            this.cbPayload.SelectedIndexChanged += new System.EventHandler(this.RebuildingGUITreePanel);
            // 
            // tbConstructor
            // 
            this.tbConstructor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbConstructor.Location = new System.Drawing.Point(9, 183);
            this.tbConstructor.Margin = new System.Windows.Forms.Padding(1);
            this.tbConstructor.Name = "tbConstructor";
            this.tbConstructor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbConstructor.Size = new System.Drawing.Size(73, 31);
            this.tbConstructor.TabIndex = 28;
            this.tbConstructor.Text = "Payload";
            this.tbConstructor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "image";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.baseTabControl);
            this.panel1.Controls.Add(this.tbConstructor);
            this.panel1.Controls.Add(this.cbPayload);
            this.panel1.Location = new System.Drawing.Point(544, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 219);
            this.panel1.TabIndex = 30;
            // 
            // baseTabControl
            // 
            this.baseTabControl.Controls.Add(this.Base);
            this.baseTabControl.Controls.Add(this.Logo);
            this.baseTabControl.Controls.Add(this.Art);
            this.baseTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.baseTabControl.Location = new System.Drawing.Point(0, 0);
            this.baseTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.baseTabControl.Name = "baseTabControl";
            this.baseTabControl.SelectedIndex = 0;
            this.baseTabControl.Size = new System.Drawing.Size(392, 181);
            this.baseTabControl.TabIndex = 33;
            // 
            // Base
            // 
            this.Base.BackColor = System.Drawing.Color.Transparent;
            this.Base.Controls.Add(this.baseMode);
            this.Base.Controls.Add(this.baseDrawQuietZones);
            this.Base.Controls.Add(this.label8);
            this.Base.Controls.Add(this.baseLightColor);
            this.Base.Controls.Add(this.labelPreviewBackgroundColor);
            this.Base.Controls.Add(this.basePixelsPerModule);
            this.Base.Controls.Add(this.baseDarkColor);
            this.Base.Controls.Add(this.label3);
            this.Base.Controls.Add(this.label11);
            this.Base.Controls.Add(this.eccLevel);
            this.Base.Location = new System.Drawing.Point(4, 25);
            this.Base.Margin = new System.Windows.Forms.Padding(4);
            this.Base.Name = "Base";
            this.Base.Size = new System.Drawing.Size(384, 152);
            this.Base.TabIndex = 2;
            this.Base.Text = "QRCode standard";
            // 
            // baseMode
            // 
            this.baseMode.AutoSize = true;
            this.baseMode.Location = new System.Drawing.Point(181, 11);
            this.baseMode.Name = "baseMode";
            this.baseMode.Size = new System.Drawing.Size(48, 21);
            this.baseMode.TabIndex = 42;
            this.baseMode.Text = "Art";
            this.baseMode.UseVisualStyleBackColor = true;
            this.baseMode.CheckedChanged += new System.EventHandler(this.baseMode_CheckedChanged);
            // 
            // baseDrawQuietZones
            // 
            this.baseDrawQuietZones.AutoSize = true;
            this.baseDrawQuietZones.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.baseDrawQuietZones.Checked = true;
            this.baseDrawQuietZones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.baseDrawQuietZones.Location = new System.Drawing.Point(236, 11);
            this.baseDrawQuietZones.Margin = new System.Windows.Forms.Padding(4);
            this.baseDrawQuietZones.Name = "baseDrawQuietZones";
            this.baseDrawQuietZones.Size = new System.Drawing.Size(134, 21);
            this.baseDrawQuietZones.TabIndex = 41;
            this.baseDrawQuietZones.Text = "drawQuietZones";
            this.baseDrawQuietZones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.baseDrawQuietZones, "If true a white border is drawn \r\naround the whole QR Code");
            this.baseDrawQuietZones.UseVisualStyleBackColor = true;
            this.baseDrawQuietZones.CheckedChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 42);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 17);
            this.label8.TabIndex = 40;
            this.label8.Text = "pixelsPerModule";
            // 
            // baseLightColor
            // 
            this.baseLightColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.baseLightColor.BackColor = System.Drawing.Color.White;
            this.baseLightColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baseLightColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.baseLightColor.Location = new System.Drawing.Point(181, 41);
            this.baseLightColor.Margin = new System.Windows.Forms.Padding(1);
            this.baseLightColor.Name = "baseLightColor";
            this.baseLightColor.Size = new System.Drawing.Size(21, 19);
            this.baseLightColor.TabIndex = 38;
            this.toolTip1.SetToolTip(this.baseLightColor, "The color of the light/white modules");
            this.baseLightColor.Click += new System.EventHandler(this.baseLightColor_Click);
            // 
            // labelPreviewBackgroundColor
            // 
            this.labelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewBackgroundColor.AutoSize = true;
            this.labelPreviewBackgroundColor.Location = new System.Drawing.Point(111, 42);
            this.labelPreviewBackgroundColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPreviewBackgroundColor.Name = "labelPreviewBackgroundColor";
            this.labelPreviewBackgroundColor.Size = new System.Drawing.Size(67, 17);
            this.labelPreviewBackgroundColor.TabIndex = 37;
            this.labelPreviewBackgroundColor.Text = "lightColor";
            this.toolTip1.SetToolTip(this.labelPreviewBackgroundColor, "The color of the light/white modules");
            // 
            // basePixelsPerModule
            // 
            this.basePixelsPerModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.basePixelsPerModule.BackColor = System.Drawing.SystemColors.Window;
            this.basePixelsPerModule.Cursor = System.Windows.Forms.Cursors.Default;
            this.basePixelsPerModule.Location = new System.Drawing.Point(325, 39);
            this.basePixelsPerModule.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.basePixelsPerModule.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.basePixelsPerModule.Name = "basePixelsPerModule";
            this.basePixelsPerModule.Size = new System.Drawing.Size(49, 22);
            this.basePixelsPerModule.TabIndex = 39;
            this.toolTip1.SetToolTip(this.basePixelsPerModule, "The pixel size each b/w module is drawn");
            this.basePixelsPerModule.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.basePixelsPerModule.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // baseDarkColor
            // 
            this.baseDarkColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.baseDarkColor.BackColor = System.Drawing.Color.Black;
            this.baseDarkColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baseDarkColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.baseDarkColor.Location = new System.Drawing.Point(84, 41);
            this.baseDarkColor.Margin = new System.Windows.Forms.Padding(1);
            this.baseDarkColor.Name = "baseDarkColor";
            this.baseDarkColor.Size = new System.Drawing.Size(21, 19);
            this.baseDarkColor.TabIndex = 35;
            this.toolTip1.SetToolTip(this.baseDarkColor, "The color of the dark/black modules");
            this.baseDarkColor.Click += new System.EventHandler(this.baseDarkColor_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 36;
            this.label3.Text = "darkColor";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 17);
            this.label11.TabIndex = 32;
            this.label11.Text = "eccLevel";
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
            this.Logo.Controls.Add(this.panelLogo);
            this.Logo.Controls.Add(this.logoIconPath);
            this.Logo.Controls.Add(this.label5);
            this.Logo.Controls.Add(this.logoIconButton);
            this.Logo.Location = new System.Drawing.Point(4, 25);
            this.Logo.Margin = new System.Windows.Forms.Padding(4);
            this.Logo.Name = "Logo";
            this.Logo.Padding = new System.Windows.Forms.Padding(4);
            this.Logo.Size = new System.Drawing.Size(384, 152);
            this.Logo.TabIndex = 0;
            this.Logo.Text = "with Logo";
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.labelIconsize);
            this.panelLogo.Controls.Add(this.logoIconSizePercent);
            this.panelLogo.Controls.Add(this.label10);
            this.panelLogo.Controls.Add(this.logoIconBorderWidth);
            this.panelLogo.Controls.Add(this.label12);
            this.panelLogo.Controls.Add(this.logoBackColor);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLogo.Enabled = false;
            this.panelLogo.Location = new System.Drawing.Point(4, 38);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(4);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(376, 110);
            this.panelLogo.TabIndex = 39;
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(9, 32);
            this.labelIconsize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(110, 17);
            this.labelIconsize.TabIndex = 39;
            this.labelIconsize.Text = "iconSizePercent";
            // 
            // logoIconSizePercent
            // 
            this.logoIconSizePercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logoIconSizePercent.Location = new System.Drawing.Point(132, 30);
            this.logoIconSizePercent.Margin = new System.Windows.Forms.Padding(1);
            this.logoIconSizePercent.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.logoIconSizePercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.logoIconSizePercent.Name = "logoIconSizePercent";
            this.logoIconSizePercent.Size = new System.Drawing.Size(53, 22);
            this.logoIconSizePercent.TabIndex = 40;
            this.logoIconSizePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.logoIconSizePercent, "Value from 1-99. \r\nSets how much % of the QR Code \r\nwill be covered by the icon");
            this.logoIconSizePercent.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.logoIconSizePercent.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(198, 32);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 17);
            this.label10.TabIndex = 42;
            this.label10.Text = "iconBorderWidth";
            // 
            // logoIconBorderWidth
            // 
            this.logoIconBorderWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logoIconBorderWidth.BackColor = System.Drawing.SystemColors.Window;
            this.logoIconBorderWidth.Location = new System.Drawing.Point(312, 30);
            this.logoIconBorderWidth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logoIconBorderWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.logoIconBorderWidth.Name = "logoIconBorderWidth";
            this.logoIconBorderWidth.Size = new System.Drawing.Size(51, 22);
            this.logoIconBorderWidth.TabIndex = 41;
            this.logoIconBorderWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.logoIconBorderWidth, "Width of the border which is drawn around the icon. \r\nMinimum: 1");
            this.logoIconBorderWidth.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.logoIconBorderWidth.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 17);
            this.label12.TabIndex = 38;
            this.label12.Text = "backgroundColor";
            // 
            // logoBackColor
            // 
            this.logoBackColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoBackColor.BackColor = System.Drawing.Color.White;
            this.logoBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoBackColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoBackColor.Location = new System.Drawing.Point(135, 4);
            this.logoBackColor.Margin = new System.Windows.Forms.Padding(1);
            this.logoBackColor.Name = "logoBackColor";
            this.logoBackColor.Size = new System.Drawing.Size(21, 19);
            this.logoBackColor.TabIndex = 37;
            this.toolTip1.SetToolTip(this.logoBackColor, resources.GetString("logoBackColor.ToolTip"));
            this.logoBackColor.Click += new System.EventHandler(this.logoBackColor_Click);
            // 
            // logoIconPath
            // 
            this.logoIconPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoIconPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoIconPath.Location = new System.Drawing.Point(108, 7);
            this.logoIconPath.Margin = new System.Windows.Forms.Padding(1);
            this.logoIconPath.Name = "logoIconPath";
            this.logoIconPath.ReadOnly = true;
            this.logoIconPath.Size = new System.Drawing.Size(267, 22);
            this.logoIconPath.TabIndex = 10;
            this.toolTip1.SetToolTip(this.logoIconPath, "If null, then ignored. \r\nIf set, the Bitmap is drawn in the middle of the QR Code" +
        "");
            this.logoIconPath.TextChanged += new System.EventHandler(this.logoIconPath_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "image";
            // 
            // logoIconButton
            // 
            this.logoIconButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoIconButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoIconButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.logoIconButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logoIconButton.Location = new System.Drawing.Point(77, 6);
            this.logoIconButton.Margin = new System.Windows.Forms.Padding(1);
            this.logoIconButton.Name = "logoIconButton";
            this.logoIconButton.Size = new System.Drawing.Size(28, 25);
            this.logoIconButton.TabIndex = 11;
            this.logoIconButton.Text = "...";
            this.logoIconButton.UseVisualStyleBackColor = true;
            this.logoIconButton.Click += new System.EventHandler(this.SelectIconBtn_Click);
            // 
            // Art
            // 
            this.Art.BackColor = System.Drawing.Color.Transparent;
            this.Art.Controls.Add(this.artIconPath);
            this.Art.Controls.Add(this.panelArt);
            this.Art.Controls.Add(this.label6);
            this.Art.Controls.Add(this.artIconButton);
            this.Art.Location = new System.Drawing.Point(4, 25);
            this.Art.Margin = new System.Windows.Forms.Padding(4);
            this.Art.Name = "Art";
            this.Art.Padding = new System.Windows.Forms.Padding(4);
            this.Art.Size = new System.Drawing.Size(384, 152);
            this.Art.TabIndex = 1;
            this.Art.Text = "with Art";
            // 
            // artIconPath
            // 
            this.artIconPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artIconPath.Location = new System.Drawing.Point(107, 7);
            this.artIconPath.Margin = new System.Windows.Forms.Padding(1);
            this.artIconPath.Name = "artIconPath";
            this.artIconPath.ReadOnly = true;
            this.artIconPath.Size = new System.Drawing.Size(264, 22);
            this.artIconPath.TabIndex = 40;
            this.toolTip1.SetToolTip(this.artIconPath, "the picture below the qr code image");
            this.artIconPath.TextChanged += new System.EventHandler(this.artPatternPath_TextChanged);
            // 
            // panelArt
            // 
            this.panelArt.Controls.Add(this.label9);
            this.panelArt.Controls.Add(this.label16);
            this.panelArt.Controls.Add(this.artPixelSizeFactor);
            this.panelArt.Controls.Add(this.artPatternButton);
            this.panelArt.Controls.Add(this.artPatternPath);
            this.panelArt.Controls.Add(this.label15);
            this.panelArt.Controls.Add(this.label2);
            this.panelArt.Controls.Add(this.artBackgroundImageStyle);
            this.panelArt.Controls.Add(this.artBackgroundColor);
            this.panelArt.Controls.Add(this.label14);
            this.panelArt.Controls.Add(this.artQuietZoneRenderingStyle);
            this.panelArt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelArt.Location = new System.Drawing.Point(4, 38);
            this.panelArt.Margin = new System.Windows.Forms.Padding(0);
            this.panelArt.Name = "panelArt";
            this.panelArt.Size = new System.Drawing.Size(376, 110);
            this.panelArt.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 5);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "backgroundColor";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 87);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 17);
            this.label16.TabIndex = 42;
            this.label16.Text = "pattern";
            // 
            // artPatternButton
            // 
            this.artPatternButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.artPatternButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.artPatternButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.artPatternButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.artPatternButton.Location = new System.Drawing.Point(72, 83);
            this.artPatternButton.Margin = new System.Windows.Forms.Padding(1);
            this.artPatternButton.Name = "artPatternButton";
            this.artPatternButton.Size = new System.Drawing.Size(28, 25);
            this.artPatternButton.TabIndex = 41;
            this.artPatternButton.Text = "...";
            this.artPatternButton.UseVisualStyleBackColor = true;
            this.artPatternButton.Click += new System.EventHandler(this.artPatternButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 60);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(152, 17);
            this.label15.TabIndex = 39;
            this.label15.Text = "backgroundImageStyle";
            // 
            // artBackgroundImageStyle
            // 
            this.artBackgroundImageStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.artBackgroundImageStyle.FormattingEnabled = true;
            this.artBackgroundImageStyle.Location = new System.Drawing.Point(183, 55);
            this.artBackgroundImageStyle.Margin = new System.Windows.Forms.Padding(4);
            this.artBackgroundImageStyle.Name = "artBackgroundImageStyle";
            this.artBackgroundImageStyle.Size = new System.Drawing.Size(184, 24);
            this.artBackgroundImageStyle.TabIndex = 38;
            // 
            // artBackgroundColor
            // 
            this.artBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.artBackgroundColor.BackColor = System.Drawing.Color.White;
            this.artBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.artBackgroundColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.artBackgroundColor.Location = new System.Drawing.Point(135, 2);
            this.artBackgroundColor.Margin = new System.Windows.Forms.Padding(1);
            this.artBackgroundColor.Name = "artBackgroundColor";
            this.artBackgroundColor.Size = new System.Drawing.Size(21, 19);
            this.artBackgroundColor.TabIndex = 27;
            this.artBackgroundColor.Click += new System.EventHandler(this.artBackgroundColor_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 32);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(169, 17);
            this.label14.TabIndex = 37;
            this.label14.Text = "quietZoneRenderingStyle";
            // 
            // artQuietZoneRenderingStyle
            // 
            this.artQuietZoneRenderingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.artQuietZoneRenderingStyle.FormattingEnabled = true;
            this.artQuietZoneRenderingStyle.Location = new System.Drawing.Point(183, 27);
            this.artQuietZoneRenderingStyle.Margin = new System.Windows.Forms.Padding(4);
            this.artQuietZoneRenderingStyle.Name = "artQuietZoneRenderingStyle";
            this.artQuietZoneRenderingStyle.Size = new System.Drawing.Size(184, 24);
            this.artQuietZoneRenderingStyle.TabIndex = 36;
            this.toolTip1.SetToolTip(this.artQuietZoneRenderingStyle, "Defines how the quiet zones shall be rendered.\r\n(Dotted or flat/filled.)");
            // 
            // QRCodeString
            // 
            this.QRCodeString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QRCodeString.DetectUrls = false;
            this.QRCodeString.Location = new System.Drawing.Point(0, 0);
            this.QRCodeString.Margin = new System.Windows.Forms.Padding(4);
            this.QRCodeString.Name = "QRCodeString";
            this.QRCodeString.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.QRCodeString.Size = new System.Drawing.Size(309, 79);
            this.QRCodeString.TabIndex = 2;
            this.QRCodeString.Text = "";
            this.toolTip1.SetToolTip(this.QRCodeString, "*is here used RichTextBox control (TexBox and RichTextBox display the newline cha" +
        "racter \'\\n\' differently)\r\n*hopefully this does not affect the generation of QR c" +
        "ode picture");
            this.QRCodeString.TextChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panelPayload);
            this.panel2.Location = new System.Drawing.Point(544, 231);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 456);
            this.panel2.TabIndex = 33;
            // 
            // panelPayload
            // 
            this.panelPayload.AutoScroll = true;
            this.panelPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPayload.Location = new System.Drawing.Point(0, 0);
            this.panelPayload.Margin = new System.Windows.Forms.Padding(0);
            this.panelPayload.Name = "panelPayload";
            this.panelPayload.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelPayload.Size = new System.Drawing.Size(392, 454);
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
            this.outSplitContainer.Panel1.Controls.Add(this.QRCodeString);
            this.outSplitContainer.Panel1.Controls.Add(this.QRCodeError);
            // 
            // outSplitContainer.Panel2
            // 
            this.outSplitContainer.Panel2.Controls.Add(this.pictureBoxQRCode);
            this.outSplitContainer.Size = new System.Drawing.Size(533, 678);
            this.outSplitContainer.SplitterDistance = 223;
            this.outSplitContainer.TabIndex = 34;
            // 
            // QRCodeError
            // 
            this.QRCodeError.Location = new System.Drawing.Point(199, 114);
            this.QRCodeError.Margin = new System.Windows.Forms.Padding(4);
            this.QRCodeError.MinimumSize = new System.Drawing.Size(27, 25);
            this.QRCodeError.Name = "QRCodeError";
            this.QRCodeError.Size = new System.Drawing.Size(311, 92);
            this.QRCodeError.TabIndex = 3;
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
            this.panel3.Size = new System.Drawing.Size(533, 40);
            this.panel3.TabIndex = 3;
            // 
            // openFileDialogSetting
            // 
            this.openFileDialogSetting.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 690);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.outSplitContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(957, 715);
            this.Name = "FormMain";
            this.Text = "QRCoderArt (QRCode-Renderer)";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artPixelSizeFactor)).EndInit();
            this.panel1.ResumeLayout(false);
            this.baseTabControl.ResumeLayout(false);
            this.Base.ResumeLayout(false);
            this.Base.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.basePixelsPerModule)).EndInit();
            this.Logo.ResumeLayout(false);
            this.Logo.PerformLayout();
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoIconSizePercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoIconBorderWidth)).EndInit();
            this.Art.ResumeLayout(false);
            this.Art.PerformLayout();
            this.panelArt.ResumeLayout(false);
            this.panelArt.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.outSplitContainer.Panel1.ResumeLayout(false);
            this.outSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outSplitContainer)).EndInit();
            this.outSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qRCodeBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        /// <summary>
        /// The picture box qr code
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        /// <summary>
        /// The combo box ecc
        /// </summary>
        private System.Windows.Forms.ComboBox eccLevel;
        /// <summary>
        /// The button save
        /// </summary>
        private System.Windows.Forms.Button buttonSave;
        /// <summary>
        /// The color dialog primary color
        /// </summary>
        private System.Windows.Forms.ColorDialog colorDialogSetting;
        /// <summary>
        /// The art path
        /// </summary>
        private System.Windows.Forms.TextBox artPatternPath;
        /// <summary>
        /// The select art BTN
        /// </summary>
        private System.Windows.Forms.Button artIconButton;
        /// <summary>
        /// The label2
        /// </summary>
        private System.Windows.Forms.Label label2;
        /// <summary>
        /// The dot size
        /// </summary>
        private System.Windows.Forms.NumericUpDown artPixelSizeFactor;
        /// <summary>
        /// The view mode
        /// </summary>
        private System.Windows.Forms.ComboBox viewMode;
        /// <summary>
        /// The label4
        /// </summary>
        private System.Windows.Forms.Label label4;
        /// <summary>
        /// The cb payload
        /// </summary>
        private System.Windows.Forms.ComboBox cbPayload;
        /// <summary>
        /// The tb constructor
        /// </summary>
        private System.Windows.Forms.Label tbConstructor;
        /// <summary>
        /// The label6
        /// </summary>
        private System.Windows.Forms.Label label6;
        /// <summary>
        /// The panel1
        /// </summary>
        private System.Windows.Forms.Panel panel1;
        /// <summary>
        /// The label11
        /// </summary>
        private System.Windows.Forms.Label label11;
        /// <summary>
        /// The tool tip1
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;
        /// <summary>
        /// The panel2
        /// </summary>
        private System.Windows.Forms.Panel panel2;
        /// <summary>
        /// The out split container
        /// </summary>
        private System.Windows.Forms.SplitContainer outSplitContainer;
        /// <summary>
        /// The panel3
        /// </summary>
        private System.Windows.Forms.Panel panel3;
        /// <summary>
        /// The panel payload
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel panelPayload;
        /// <summary>
        /// The q r code binding source
        /// </summary>
        private System.Windows.Forms.BindingSource qRCodeBindingSource;
        private System.Windows.Forms.RichTextBox QRCodeString;
        private System.Windows.Forms.WebBrowser QRCodeError;
        private System.Windows.Forms.TabControl baseTabControl;
        private System.Windows.Forms.TabPage Logo;
        private System.Windows.Forms.TabPage Art;
        private System.Windows.Forms.Panel artBackgroundColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox artQuietZoneRenderingStyle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox artBackgroundImageStyle;
        private System.Windows.Forms.TextBox artIconPath;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button artPatternButton;
        private System.Windows.Forms.TabPage Base;
        private System.Windows.Forms.CheckBox baseDrawQuietZones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel baseLightColor;
        private System.Windows.Forms.Label labelPreviewBackgroundColor;
        private System.Windows.Forms.NumericUpDown basePixelsPerModule;
        private System.Windows.Forms.Panel baseDarkColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel logoBackColor;
        private System.Windows.Forms.TextBox logoIconPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button logoIconButton;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label labelIconsize;
        private System.Windows.Forms.NumericUpDown logoIconSizePercent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown logoIconBorderWidth;
        private System.Windows.Forms.Panel panelArt;
        private System.Windows.Forms.OpenFileDialog openFileDialogSetting;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSetting;
        private System.Windows.Forms.CheckBox baseMode;
    }
}

