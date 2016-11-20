namespace lab10_octave_
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.niStart = new System.Windows.Forms.NumericUpDown();
            this.niStep = new System.Windows.Forms.NumericUpDown();
            this.niFinish = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.niStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.niStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.niFinish)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 132);
            this.button1.TabIndex = 0;
            this.button1.Text = "redraw";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // niStart
            // 
            this.niStart.DecimalPlaces = 2;
            this.niStart.Location = new System.Drawing.Point(611, 12);
            this.niStart.Name = "niStart";
            this.niStart.Size = new System.Drawing.Size(120, 20);
            this.niStart.TabIndex = 1;
            this.niStart.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // niStep
            // 
            this.niStep.DecimalPlaces = 2;
            this.niStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.niStep.Location = new System.Drawing.Point(611, 64);
            this.niStep.Name = "niStep";
            this.niStep.Size = new System.Drawing.Size(120, 20);
            this.niStep.TabIndex = 2;
            this.niStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // niFinish
            // 
            this.niFinish.DecimalPlaces = 2;
            this.niFinish.Location = new System.Drawing.Point(611, 38);
            this.niFinish.Name = "niFinish";
            this.niFinish.Size = new System.Drawing.Size(120, 20);
            this.niFinish.TabIndex = 3;
            this.niFinish.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(576, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(576, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Step";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(576, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Finish";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(544, 122);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.ErrorLabel.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 388);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.niFinish);
            this.Controls.Add(this.niStep);
            this.Controls.Add(this.niStart);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Надо не забыть сюда вставить инфу";
            ((System.ComponentModel.ISupportInitialize)(this.niStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.niStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.niFinish)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown niStart;
        private System.Windows.Forms.NumericUpDown niStep;
        private System.Windows.Forms.NumericUpDown niFinish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ErrorLabel;
    }
}

