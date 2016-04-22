namespace Calderilla.Client.WinForms
{
    partial class FormAfegirPatrimoniMes
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
            System.Windows.Forms.Label dataLabel;
            System.Windows.Forms.Label tipusLabel;
            System.Windows.Forms.Label valorLabel;
            this.dataDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.patrimoniMesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tipusTextBox = new System.Windows.Forms.TextBox();
            this.valorTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            dataLabel = new System.Windows.Forms.Label();
            tipusLabel = new System.Windows.Forms.Label();
            valorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.patrimoniMesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLabel
            // 
            dataLabel.AutoSize = true;
            dataLabel.Location = new System.Drawing.Point(12, 50);
            dataLabel.Name = "dataLabel";
            dataLabel.Size = new System.Drawing.Size(33, 13);
            dataLabel.TabIndex = 1;
            dataLabel.Text = "Data:";
            // 
            // tipusLabel
            // 
            tipusLabel.AutoSize = true;
            tipusLabel.Location = new System.Drawing.Point(12, 85);
            tipusLabel.Name = "tipusLabel";
            tipusLabel.Size = new System.Drawing.Size(36, 13);
            tipusLabel.TabIndex = 3;
            tipusLabel.Text = "Tipus:";
            // 
            // valorLabel
            // 
            valorLabel.AutoSize = true;
            valorLabel.Location = new System.Drawing.Point(14, 116);
            valorLabel.Name = "valorLabel";
            valorLabel.Size = new System.Drawing.Size(34, 13);
            valorLabel.TabIndex = 5;
            valorLabel.Text = "Valor:";
            // 
            // dataDateTimePicker
            // 
            this.dataDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.patrimoniMesBindingSource, "Data", true));
            this.dataDateTimePicker.Location = new System.Drawing.Point(54, 46);
            this.dataDateTimePicker.Name = "dataDateTimePicker";
            this.dataDateTimePicker.Size = new System.Drawing.Size(197, 20);
            this.dataDateTimePicker.TabIndex = 2;
            // 
            // patrimoniMesBindingSource
            // 
            this.patrimoniMesBindingSource.DataSource = typeof(Calderilla.Model.PatrimoniMes);
            // 
            // tipusTextBox
            // 
            this.tipusTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.patrimoniMesBindingSource, "Tipus", true));
            this.tipusTextBox.Location = new System.Drawing.Point(54, 82);
            this.tipusTextBox.Name = "tipusTextBox";
            this.tipusTextBox.Size = new System.Drawing.Size(197, 20);
            this.tipusTextBox.TabIndex = 4;
            // 
            // valorTextBox
            // 
            this.valorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.patrimoniMesBindingSource, "Valor", true));
            this.valorTextBox.Location = new System.Drawing.Point(54, 113);
            this.valorTextBox.Name = "valorTextBox";
            this.valorTextBox.Size = new System.Drawing.Size(197, 20);
            this.valorTextBox.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(78, 217);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAfegirPatrimoniMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(valorLabel);
            this.Controls.Add(this.valorTextBox);
            this.Controls.Add(tipusLabel);
            this.Controls.Add(this.tipusTextBox);
            this.Controls.Add(dataLabel);
            this.Controls.Add(this.dataDateTimePicker);
            this.Name = "FormAfegirPatrimoniMes";
            this.Text = "FormAfegirPatrimoniMes";
            ((System.ComponentModel.ISupportInitialize)(this.patrimoniMesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource patrimoniMesBindingSource;
        private System.Windows.Forms.DateTimePicker dataDateTimePicker;
        private System.Windows.Forms.TextBox tipusTextBox;
        private System.Windows.Forms.TextBox valorTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}