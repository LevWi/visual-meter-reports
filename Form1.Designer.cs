namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView_meters = new System.Windows.Forms.DataGridView();
            this.Console_textBox = new System.Windows.Forms.TextBox();
            this.treeView_meters = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_ElectMeter = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabPage_WaterMeter = new System.Windows.Forms.TabPage();
            this.dataGridView_watermeter = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Service = new System.Windows.Forms.ToolStripMenuItem();
            this.WaterMeterStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartReadElectMeterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ElectroReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogInToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MeterImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip_meters = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChangeLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_meters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_ElectMeter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabPage_WaterMeter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_watermeter)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip_meters.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_meters
            // 
            this.dataGridView_meters.AllowUserToAddRows = false;
            this.dataGridView_meters.AllowUserToDeleteRows = false;
            this.dataGridView_meters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_meters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_meters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_meters.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_meters.Name = "dataGridView_meters";
            this.dataGridView_meters.Size = new System.Drawing.Size(783, 472);
            this.dataGridView_meters.TabIndex = 7;
            // 
            // Console_textBox
            // 
            this.Console_textBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Console_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Console_textBox.Location = new System.Drawing.Point(0, 0);
            this.Console_textBox.Multiline = true;
            this.Console_textBox.Name = "Console_textBox";
            this.Console_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Console_textBox.Size = new System.Drawing.Size(935, 125);
            this.Console_textBox.TabIndex = 12;
            // 
            // treeView_meters
            // 
            this.treeView_meters.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.treeView_meters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_meters.Location = new System.Drawing.Point(0, 0);
            this.treeView_meters.Name = "treeView_meters";
            this.treeView_meters.Size = new System.Drawing.Size(134, 472);
            this.treeView_meters.TabIndex = 10;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Console_textBox);
            this.splitContainer2.Size = new System.Drawing.Size(935, 633);
            this.splitContainer2.SplitterDistance = 504;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_ElectMeter);
            this.tabControl1.Controls.Add(this.tabPage_WaterMeter);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 504);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage_ElectMeter
            // 
            this.tabPage_ElectMeter.Controls.Add(this.splitContainer3);
            this.tabPage_ElectMeter.Location = new System.Drawing.Point(4, 22);
            this.tabPage_ElectMeter.Name = "tabPage_ElectMeter";
            this.tabPage_ElectMeter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_ElectMeter.Size = new System.Drawing.Size(927, 478);
            this.tabPage_ElectMeter.TabIndex = 0;
            this.tabPage_ElectMeter.Text = "Электроэнергия";
            this.tabPage_ElectMeter.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeView_meters);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dataGridView_meters);
            this.splitContainer3.Size = new System.Drawing.Size(921, 472);
            this.splitContainer3.SplitterDistance = 134;
            this.splitContainer3.TabIndex = 0;
            // 
            // tabPage_WaterMeter
            // 
            this.tabPage_WaterMeter.Controls.Add(this.dataGridView_watermeter);
            this.tabPage_WaterMeter.Location = new System.Drawing.Point(4, 22);
            this.tabPage_WaterMeter.Name = "tabPage_WaterMeter";
            this.tabPage_WaterMeter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_WaterMeter.Size = new System.Drawing.Size(927, 478);
            this.tabPage_WaterMeter.TabIndex = 1;
            this.tabPage_WaterMeter.Text = "Водоучет";
            this.tabPage_WaterMeter.UseVisualStyleBackColor = true;
            // 
            // dataGridView_watermeter
            // 
            this.dataGridView_watermeter.AllowUserToAddRows = false;
            this.dataGridView_watermeter.AllowUserToDeleteRows = false;
            this.dataGridView_watermeter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_watermeter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_watermeter.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_watermeter.Name = "dataGridView_watermeter";
            this.dataGridView_watermeter.Size = new System.Drawing.Size(921, 472);
            this.dataGridView_watermeter.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Service,
            this.ReportsToolStripMenuItem,
            this.UserToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Service
            // 
            this.toolStripMenuItem_Service.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WaterMeterStartToolStripMenuItem,
            this.StartReadElectMeterToolStripMenuItem});
            this.toolStripMenuItem_Service.Name = "toolStripMenuItem_Service";
            this.toolStripMenuItem_Service.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItem_Service.Text = "Сервис";
            // 
            // WaterMeterStartToolStripMenuItem
            // 
            this.WaterMeterStartToolStripMenuItem.Name = "WaterMeterStartToolStripMenuItem";
            this.WaterMeterStartToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.WaterMeterStartToolStripMenuItem.Text = "Пуск опроса водосчетчиков";
            this.WaterMeterStartToolStripMenuItem.Click += new System.EventHandler(this.OnClickStartWaterMeterRead);
            // 
            // StartReadElectMeterToolStripMenuItem
            // 
            this.StartReadElectMeterToolStripMenuItem.Name = "StartReadElectMeterToolStripMenuItem";
            this.StartReadElectMeterToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.StartReadElectMeterToolStripMenuItem.Text = "Пуск опроса электросчетчиков";
            this.StartReadElectMeterToolStripMenuItem.Click += new System.EventHandler(this.StartReadElectMeterToolStripMenuItem_Click);
            // 
            // ReportsToolStripMenuItem
            // 
            this.ReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElectroReportToolStripMenuItem});
            this.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem";
            this.ReportsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ReportsToolStripMenuItem.Text = "Отчеты";
            // 
            // ElectroReportToolStripMenuItem
            // 
            this.ElectroReportToolStripMenuItem.Name = "ElectroReportToolStripMenuItem";
            this.ElectroReportToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.ElectroReportToolStripMenuItem.Text = "Учет ресурсов";
            this.ElectroReportToolStripMenuItem.Click += new System.EventHandler(this.ElectroReportToolStripMenuItem_Click);
            // 
            // UserToolStripMenuItem
            // 
            this.UserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LogInToolStripMenuItem1,
            this.выйтиToolStripMenuItem});
            this.UserToolStripMenuItem.Name = "UserToolStripMenuItem";
            this.UserToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.UserToolStripMenuItem.Text = "Пользователь";
            // 
            // LogInToolStripMenuItem1
            // 
            this.LogInToolStripMenuItem1.Name = "LogInToolStripMenuItem1";
            this.LogInToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.LogInToolStripMenuItem1.Text = "Войти";
            this.LogInToolStripMenuItem1.Click += new System.EventHandler(this.LogInToolStripMenuItem1_Click);
            // 
            // выйтиToolStripMenuItem
            // 
            this.выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            this.выйтиToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.выйтиToolStripMenuItem.Text = "Выйти ";
            this.выйтиToolStripMenuItem.Click += new System.EventHandler(this.выйтиToolStripMenuItem_Click);
            // 
            // MeterImageList
            // 
            this.MeterImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MeterImageList.ImageStream")));
            this.MeterImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.MeterImageList.Images.SetKeyName(0, "chart_organisation.png");
            this.MeterImageList.Images.SetKeyName(1, "application_lightning.png");
            // 
            // contextMenuStrip_meters
            // 
            this.contextMenuStrip_meters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeLimitToolStripMenuItem});
            this.contextMenuStrip_meters.Name = "contextMenuStrip_meters";
            this.contextMenuStrip_meters.Size = new System.Drawing.Size(158, 26);
            // 
            // ChangeLimitToolStripMenuItem
            // 
            this.ChangeLimitToolStripMenuItem.Name = "ChangeLimitToolStripMenuItem";
            this.ChangeLimitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.ChangeLimitToolStripMenuItem.Text = "Задать лимиты";
            this.ChangeLimitToolStripMenuItem.Click += new System.EventHandler(this.ChangeLimitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 657);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Учет энергоресурсов";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_meters)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_ElectMeter.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabPage_WaterMeter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_watermeter)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip_meters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn admin_zone;
        private System.Windows.Forms.DataGridViewTextBoxColumn zone;
        private System.Windows.Forms.DataGridViewTextBoxColumn power;
        private System.Windows.Forms.DataGridViewTextBoxColumn cur_phase1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cur_phase2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cur_phase3;
        private System.Windows.Forms.DataGridViewTextBoxColumn volt_phase1;
        private System.Windows.Forms.DataGridViewTextBoxColumn volt_phase2;
        private System.Windows.Forms.DataGridViewTextBoxColumn volt_phase3;

        private System.Windows.Forms.DataGridView dataGridView_meters;
        private System.Windows.Forms.TextBox Console_textBox;
        private System.Windows.Forms.TreeView treeView_meters;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Service;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_ElectMeter;
        private System.Windows.Forms.TabPage tabPage_WaterMeter;


        //private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_WaterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_NameNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_Watermeter_SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_LocationNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_Consumer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_Function;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_water_Data;
        private System.Windows.Forms.DataGridView dataGridView_watermeter;
        private System.Windows.Forms.ToolStripMenuItem WaterMeterStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartReadElectMeterToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ImageList MeterImageList;
        private System.Windows.Forms.ToolStripMenuItem ReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ElectroReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogInToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_meters;
        private System.Windows.Forms.ToolStripMenuItem ChangeLimitToolStripMenuItem;
    }
}

