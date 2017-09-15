namespace WindowsFormsApplication1
{
    partial class Authorization_form
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
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.textBox_login = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_startTimer = new System.Windows.Forms.CheckBox();
            this.numericUpDown_intervalTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_intervalTime)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(62, 37);
            this.textBox_password.MaxLength = 50;
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(132, 20);
            this.textBox_password.TabIndex = 20;
            this.textBox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_password.UseSystemPasswordChar = true;
            // 
            // textBox_login
            // 
            this.textBox_login.Location = new System.Drawing.Point(62, 12);
            this.textBox_login.Name = "textBox_login";
            this.textBox_login.Size = new System.Drawing.Size(132, 20);
            this.textBox_login.TabIndex = 19;
            this.textBox_login.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Логин";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Пароль";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(62, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_startTimer
            // 
            this.checkBox_startTimer.AutoSize = true;
            this.checkBox_startTimer.Location = new System.Drawing.Point(14, 100);
            this.checkBox_startTimer.Name = "checkBox_startTimer";
            this.checkBox_startTimer.Size = new System.Drawing.Size(90, 17);
            this.checkBox_startTimer.TabIndex = 24;
            this.checkBox_startTimer.Text = "Выход через";
            this.checkBox_startTimer.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_intervalTime
            // 
            this.numericUpDown_intervalTime.Location = new System.Drawing.Point(121, 99);
            this.numericUpDown_intervalTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_intervalTime.Name = "numericUpDown_intervalTime";
            this.numericUpDown_intervalTime.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_intervalTime.TabIndex = 25;
            this.numericUpDown_intervalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_intervalTime.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "с";
            // 
            // Authorization_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 129);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_intervalTime);
            this.Controls.Add(this.checkBox_startTimer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_login);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Name = "Authorization_form";
            this.ShowIcon = false;
            this.Text = "Авторизация";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Authorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_intervalTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.TextBox textBox_login;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_startTimer;
        private System.Windows.Forms.NumericUpDown numericUpDown_intervalTime;
        private System.Windows.Forms.Label label1;

    }
}