namespace WindowsFormsApplication1
{
    partial class MeterReportForm
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
            this.ReadBase_button = new System.Windows.Forms.Button();
            this.dateTimePicker_start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1_forMonth = new System.Windows.Forms.RadioButton();
            this.radioButton_ForDay = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_Water = new System.Windows.Forms.RadioButton();
            this.radioButton_Electric = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.allDaysSum = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadBase_button
            // 
            this.ReadBase_button.BackColor = System.Drawing.SystemColors.Window;
            this.ReadBase_button.Location = new System.Drawing.Point(150, 183);
            this.ReadBase_button.Name = "ReadBase_button";
            this.ReadBase_button.Size = new System.Drawing.Size(79, 42);
            this.ReadBase_button.TabIndex = 3;
            this.ReadBase_button.Text = "Получить отчет";
            this.ReadBase_button.UseVisualStyleBackColor = false;
            this.ReadBase_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker_start
            // 
            this.dateTimePicker_start.CustomFormat = "dd.MM.yy";
            this.dateTimePicker_start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_start.Location = new System.Drawing.Point(48, 180);
            this.dateTimePicker_start.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker_start.Name = "dateTimePicker_start";
            this.dateTimePicker_start.Size = new System.Drawing.Size(86, 20);
            this.dateTimePicker_start.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "От";
            // 
            // dateTimePicker_end
            // 
            this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_end.Location = new System.Drawing.Point(48, 206);
            this.dateTimePicker_end.Name = "dateTimePicker_end";
            this.dateTimePicker_end.Size = new System.Drawing.Size(86, 20);
            this.dateTimePicker_end.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "До";
            // 
            // radioButton1_forMonth
            // 
            this.radioButton1_forMonth.AutoSize = true;
            this.radioButton1_forMonth.Location = new System.Drawing.Point(11, 42);
            this.radioButton1_forMonth.Name = "radioButton1_forMonth";
            this.radioButton1_forMonth.Size = new System.Drawing.Size(88, 17);
            this.radioButton1_forMonth.TabIndex = 10;
            this.radioButton1_forMonth.Text = "По месяцам";
            this.radioButton1_forMonth.UseVisualStyleBackColor = true;
            // 
            // radioButton_ForDay
            // 
            this.radioButton_ForDay.AutoSize = true;
            this.radioButton_ForDay.Checked = true;
            this.radioButton_ForDay.Location = new System.Drawing.Point(11, 19);
            this.radioButton_ForDay.Name = "radioButton_ForDay";
            this.radioButton_ForDay.Size = new System.Drawing.Size(68, 17);
            this.radioButton_ForDay.TabIndex = 11;
            this.radioButton_ForDay.TabStop = true;
            this.radioButton_ForDay.Text = "По дням";
            this.radioButton_ForDay.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_ForDay);
            this.groupBox1.Controls.Add(this.radioButton1_forMonth);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 72);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Записи";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_Water);
            this.groupBox2.Controls.Add(this.radioButton_Electric);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 74);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ресурс";
            // 
            // radioButton_Water
            // 
            this.radioButton_Water.AutoSize = true;
            this.radioButton_Water.Location = new System.Drawing.Point(16, 42);
            this.radioButton_Water.Name = "radioButton_Water";
            this.radioButton_Water.Size = new System.Drawing.Size(50, 17);
            this.radioButton_Water.TabIndex = 1;
            this.radioButton_Water.TabStop = true;
            this.radioButton_Water.Text = "Вода";
            this.radioButton_Water.UseVisualStyleBackColor = true;
            // 
            // radioButton_Electric
            // 
            this.radioButton_Electric.AutoSize = true;
            this.radioButton_Electric.Location = new System.Drawing.Point(16, 19);
            this.radioButton_Electric.Name = "radioButton_Electric";
            this.radioButton_Electric.Size = new System.Drawing.Size(101, 17);
            this.radioButton_Electric.TabIndex = 0;
            this.radioButton_Electric.TabStop = true;
            this.radioButton_Electric.Text = "Электричество";
            this.radioButton_Electric.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 248);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(263, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // allDaysSum
            // 
            this.allDaysSum.AutoSize = true;
            this.allDaysSum.Location = new System.Drawing.Point(161, 25);
            this.allDaysSum.Name = "allDaysSum";
            this.allDaysSum.Size = new System.Drawing.Size(90, 30);
            this.allDaysSum.TabIndex = 15;
            this.allDaysSum.Text = "По суммам \r\nкаждого дня";
            this.allDaysSum.UseVisualStyleBackColor = true;
            // 
            // MeterReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 270);
            this.Controls.Add(this.allDaysSum);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker_end);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker_start);
            this.Controls.Add(this.ReadBase_button);
            this.Name = "MeterReportForm";
            this.ShowIcon = false;
            this.Text = "Генерация отчета";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadBase_button;
        private System.Windows.Forms.DateTimePicker dateTimePicker_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_end;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1_forMonth;
        private System.Windows.Forms.RadioButton radioButton_ForDay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_Water;
        private System.Windows.Forms.RadioButton radioButton_Electric;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.CheckBox allDaysSum;
    }
}