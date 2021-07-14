
namespace QRCoderArt
{
    partial class FormDictionary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDictionary));
            this.dataGridViewProperty = new System.Windows.Forms.DataGridView();
            this.plugin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluginOption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewProperty
            // 
            this.dataGridViewProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProperty.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plugin,
            this.pluginOption});
            this.dataGridViewProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProperty.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewProperty.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewProperty.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewProperty.Name = "dataGridViewProperty";
            this.dataGridViewProperty.RowHeadersWidth = 51;
            this.dataGridViewProperty.Size = new System.Drawing.Size(591, 266);
            this.dataGridViewProperty.TabIndex = 0;
            // 
            // plugin
            // 
            this.plugin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.plugin.FillWeight = 40F;
            this.plugin.HeaderText = "Name";
            this.plugin.MinimumWidth = 6;
            this.plugin.Name = "plugin";
            // 
            // pluginOption
            // 
            this.pluginOption.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pluginOption.FillWeight = 60F;
            this.pluginOption.HeaderText = "Value";
            this.pluginOption.MinimumWidth = 6;
            this.pluginOption.Name = "pluginOption";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 238);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(591, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send data to parameter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dictionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dictionaryForm";
            this.Text = "Properties (for the dictionary type)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProperty;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn plugin;
        private System.Windows.Forms.DataGridViewTextBoxColumn pluginOption;
    }
}