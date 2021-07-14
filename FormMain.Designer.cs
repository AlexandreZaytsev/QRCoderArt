﻿// ***********************************************************************
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
            this.textBoxQRCode.Multiline = true;
            this.textBoxQRCode.Name = "textBoxQRCode";
            this.textBoxQRCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxQRCode.Size = new System.Drawing.Size(400, 182);
            this.textBoxQRCode.TabIndex = 1;
            this.textBoxQRCode.TextChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.ErrorImage = global::QRCoderArt.Properties.Resources.qr1;
            this.pictureBoxQRCode.InitialImage = global::QRCoderArt.Properties.Resources.qr1;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pictureBoxQRCode.Size = new System.Drawing.Size(394, 325);
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
            this.comboBoxECC.Location = new System.Drawing.Point(87, 6);
            this.comboBoxECC.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxECC.Name = "comboBoxECC";
            this.comboBoxECC.Size = new System.Drawing.Size(58, 21);
            this.comboBoxECC.TabIndex = 3;
            this.comboBoxECC.SelectedIndexChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // labelECC
            // 
            this.labelECC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelECC.AutoSize = true;
            this.labelECC.Location = new System.Drawing.Point(52, 10);
            this.labelECC.Name = "labelECC";
            this.labelECC.Size = new System.Drawing.Size(29, 13);
            this.labelECC.TabIndex = 4;
            this.labelECC.Text = "level";
            // 
            // labelIconsize
            // 
            this.labelIconsize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIconsize.AutoSize = true;
            this.labelIconsize.Location = new System.Drawing.Point(56, 51);
            this.labelIconsize.Name = "labelIconsize";
            this.labelIconsize.Size = new System.Drawing.Size(25, 13);
            this.labelIconsize.TabIndex = 8;
            this.labelIconsize.Text = "size";
            // 
            // iconSize
            // 
            this.iconSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconSize.Location = new System.Drawing.Point(87, 49);
            this.iconSize.Margin = new System.Windows.Forms.Padding(1);
            this.iconSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iconSize.Name = "iconSize";
            this.iconSize.Size = new System.Drawing.Size(58, 20);
            this.iconSize.TabIndex = 9;
            this.iconSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.iconSize.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(310, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(87, 25);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save QR code";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // panelPreviewPrimaryColor
            // 
            this.panelPreviewPrimaryColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreviewPrimaryColor.BackColor = System.Drawing.Color.Black;
            this.panelPreviewPrimaryColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreviewPrimaryColor.Location = new System.Drawing.Point(185, 28);
            this.panelPreviewPrimaryColor.Margin = new System.Windows.Forms.Padding(1);
            this.panelPreviewPrimaryColor.Name = "panelPreviewPrimaryColor";
            this.panelPreviewPrimaryColor.Size = new System.Drawing.Size(24, 19);
            this.panelPreviewPrimaryColor.TabIndex = 13;
            this.panelPreviewPrimaryColor.Click += new System.EventHandler(this.PanelPreviewPrimaryColor_Click);
            // 
            // labelPreviewBackgroundColor
            // 
            this.labelPreviewBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPreviewBackgroundColor.AutoSize = true;
            this.labelPreviewBackgroundColor.Location = new System.Drawing.Point(212, 31);
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
            this.panelPreviewBackgroundColor.Location = new System.Drawing.Point(244, 28);
            this.panelPreviewBackgroundColor.Margin = new System.Windows.Forms.Padding(1);
            this.panelPreviewBackgroundColor.Name = "panelPreviewBackgroundColor";
            this.panelPreviewBackgroundColor.Size = new System.Drawing.Size(21, 19);
            this.panelPreviewBackgroundColor.TabIndex = 15;
            this.panelPreviewBackgroundColor.Click += new System.EventHandler(this.PanelPreviewBackgroundColor_Click);
            // 
            // artPath
            // 
            this.artPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.artPath.Location = new System.Drawing.Point(186, 70);
            this.artPath.Margin = new System.Windows.Forms.Padding(1);
            this.artPath.Name = "artPath";
            this.artPath.Size = new System.Drawing.Size(79, 20);
            this.artPath.TabIndex = 17;
            this.toolTip1.SetToolTip(this.artPath, "the picture below the qr code image");
            this.artPath.TextChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // selectArtBtn
            // 
            this.selectArtBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectArtBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.selectArtBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectArtBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectArtBtn.Location = new System.Drawing.Point(268, 69);
            this.selectArtBtn.Margin = new System.Windows.Forms.Padding(1);
            this.selectArtBtn.Name = "selectArtBtn";
            this.selectArtBtn.Size = new System.Drawing.Size(21, 20);
            this.selectArtBtn.TabIndex = 18;
            this.selectArtBtn.Text = "...";
            this.selectArtBtn.UseVisualStyleBackColor = true;
            this.selectArtBtn.Click += new System.EventHandler(this.SelectArtBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "dot size";
            // 
            // dotSize
            // 
            this.dotSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dotSize.BackColor = System.Drawing.SystemColors.Window;
            this.dotSize.Location = new System.Drawing.Point(87, 70);
            this.dotSize.Margin = new System.Windows.Forms.Padding(1);
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
            this.dotSize.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "size";
            // 
            // pixelSize
            // 
            this.pixelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pixelSize.BackColor = System.Drawing.SystemColors.Window;
            this.pixelSize.Location = new System.Drawing.Point(87, 28);
            this.pixelSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.pixelSize.ValueChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 31);
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
            this.selectIconBtn.Location = new System.Drawing.Point(268, 48);
            this.selectIconBtn.Margin = new System.Windows.Forms.Padding(1);
            this.selectIconBtn.Name = "selectIconBtn";
            this.selectIconBtn.Size = new System.Drawing.Size(21, 20);
            this.selectIconBtn.TabIndex = 11;
            this.selectIconBtn.Text = "...";
            this.selectIconBtn.UseVisualStyleBackColor = true;
            this.selectIconBtn.Click += new System.EventHandler(this.SelectIconBtn_Click);
            // 
            // iconPath
            // 
            this.iconPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iconPath.Location = new System.Drawing.Point(186, 49);
            this.iconPath.Margin = new System.Windows.Forms.Padding(1);
            this.iconPath.Name = "iconPath";
            this.iconPath.Size = new System.Drawing.Size(78, 20);
            this.iconPath.TabIndex = 10;
            this.toolTip1.SetToolTip(this.iconPath, "the picture above the qr code image");
            this.iconPath.TextChanged += new System.EventHandler(this.Setting_Changed);
            // 
            // viewMode
            // 
            this.viewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewMode.FormattingEnabled = true;
            this.viewMode.Items.AddRange(new object[] {
            "Center",
            "Zoom"});
            this.viewMode.Location = new System.Drawing.Point(68, 6);
            this.viewMode.Name = "viewMode";
            this.viewMode.Size = new System.Drawing.Size(80, 21);
            this.viewMode.TabIndex = 24;
            this.viewMode.SelectedIndexChanged += new System.EventHandler(this.ViewMode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "view mode";
            // 
            // cbPayload
            // 
            this.cbPayload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPayload.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayload.FormattingEnabled = true;
            this.cbPayload.Location = new System.Drawing.Point(87, 92);
            this.cbPayload.Margin = new System.Windows.Forms.Padding(1);
            this.cbPayload.Name = "cbPayload";
            this.cbPayload.Size = new System.Drawing.Size(203, 21);
            this.cbPayload.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cbPayload, "Select PAYLOAD and Set property");
            this.cbPayload.SelectedIndexChanged += new System.EventHandler(this.RebuildingPanelUpload);
            // 
            // tbConstructor
            // 
            this.tbConstructor.Location = new System.Drawing.Point(7, 93);
            this.tbConstructor.Margin = new System.Windows.Forms.Padding(1);
            this.tbConstructor.Name = "tbConstructor";
            this.tbConstructor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbConstructor.Size = new System.Drawing.Size(55, 17);
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
            this.label5.Location = new System.Drawing.Point(152, 51);
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
            this.label6.Location = new System.Drawing.Point(152, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
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
            this.panel1.Location = new System.Drawing.Point(410, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 118);
            this.panel1.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Art";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "ECC";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Logo";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Pixel";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panelPayload);
            this.panel2.Location = new System.Drawing.Point(410, 128);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(296, 431);
            this.panel2.TabIndex = 33;
            // 
            // panelPayload
            // 
            this.panelPayload.AutoScroll = true;
            this.panelPayload.Location = new System.Drawing.Point(2, 2);
            this.panelPayload.Margin = new System.Windows.Forms.Padding(0);
            this.panelPayload.Name = "panelPayload";
            this.panelPayload.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelPayload.Size = new System.Drawing.Size(290, 424);
            this.panelPayload.TabIndex = 3;
            // 
            // outSplitContainer
            // 
            this.outSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outSplitContainer.Location = new System.Drawing.Point(3, 7);
            this.outSplitContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.outSplitContainer.Size = new System.Drawing.Size(402, 551);
            this.outSplitContainer.SplitterDistance = 184;
            this.outSplitContainer.SplitterWidth = 3;
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
            this.panel3.Location = new System.Drawing.Point(3, 526);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(402, 33);
            this.panel3.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 561);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.outSplitContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(722, 593);
            this.Name = "FormMain";
            this.Text = "QRCoderArt";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
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
        /// <summary>
        /// The text box qr code
        /// </summary>
        private System.Windows.Forms.TextBox textBoxQRCode;
        /// <summary>
        /// The picture box qr code
        /// </summary>
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        /// <summary>
        /// The combo box ecc
        /// </summary>
        private System.Windows.Forms.ComboBox comboBoxECC;
        /// <summary>
        /// The label ecc
        /// </summary>
        private System.Windows.Forms.Label labelECC;
        /// <summary>
        /// The label iconsize
        /// </summary>
        private System.Windows.Forms.Label labelIconsize;
        /// <summary>
        /// The icon size
        /// </summary>
        private System.Windows.Forms.NumericUpDown iconSize;
        /// <summary>
        /// The button save
        /// </summary>
        private System.Windows.Forms.Button buttonSave;
        /// <summary>
        /// The color dialog primary color
        /// </summary>
        private System.Windows.Forms.ColorDialog colorDialogPrimaryColor;
        /// <summary>
        /// The panel preview primary color
        /// </summary>
        private System.Windows.Forms.Panel panelPreviewPrimaryColor;
        /// <summary>
        /// The panel preview background color
        /// </summary>
        private System.Windows.Forms.Panel panelPreviewBackgroundColor;
        /// <summary>
        /// The color dialog background color
        /// </summary>
        private System.Windows.Forms.ColorDialog colorDialogBackgroundColor;
        /// <summary>
        /// The art path
        /// </summary>
        private System.Windows.Forms.TextBox artPath;
        /// <summary>
        /// The select art BTN
        /// </summary>
        private System.Windows.Forms.Button selectArtBtn;
        /// <summary>
        /// The label2
        /// </summary>
        private System.Windows.Forms.Label label2;
        /// <summary>
        /// The dot size
        /// </summary>
        private System.Windows.Forms.NumericUpDown dotSize;
        /// <summary>
        /// The label preview background color
        /// </summary>
        private System.Windows.Forms.Label labelPreviewBackgroundColor;
        /// <summary>
        /// The label3
        /// </summary>
        private System.Windows.Forms.Label label3;
        /// <summary>
        /// The select icon BTN
        /// </summary>
        private System.Windows.Forms.Button selectIconBtn;
        /// <summary>
        /// The icon path
        /// </summary>
        private System.Windows.Forms.TextBox iconPath;
        /// <summary>
        /// The label1
        /// </summary>
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// The pixel size
        /// </summary>
        private System.Windows.Forms.NumericUpDown pixelSize;
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
        /// The label5
        /// </summary>
        private System.Windows.Forms.Label label5;
        /// <summary>
        /// The panel1
        /// </summary>
        private System.Windows.Forms.Panel panel1;
        /// <summary>
        /// The label11
        /// </summary>
        private System.Windows.Forms.Label label11;
        /// <summary>
        /// The label10
        /// </summary>
        private System.Windows.Forms.Label label10;
        /// <summary>
        /// The label9
        /// </summary>
        private System.Windows.Forms.Label label9;
        /// <summary>
        /// The label8
        /// </summary>
        private System.Windows.Forms.Label label8;
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
    }
}
