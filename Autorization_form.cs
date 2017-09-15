using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Authorization_form : Form
    {
        public Authorization authoriz;
        public Authorization_form(Authorization obj)
        {
            authoriz = obj;
            InitializeComponent();
        }

        SQLhandler sqlHandler;
        string Database;
        string DataSource;

        private void Authorization_Load(object sender, EventArgs e)
        {
            textBox_login.Text = authoriz.login;
            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load("Meter_conf.xml");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
                this.Close();
            }
            XmlNodeList Nodes = xmlDocument.SelectNodes("//DataBaseSQL");
            if (Nodes.Count > 0)
            {
               // sqlHandler = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
                Database = Nodes[0].Attributes["Database"].Value;
                DataSource = Nodes[0].Attributes["DataSource"].Value;
            }
            else
            {
                MessageBox.Show("Отсутствуют данные к подключению к базе!");
                this.Close();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_login.BackColor = SystemColors.Window;
            textBox_password.BackColor = SystemColors.Window;
            if (textBox_login.Text == "")
            {
                MessageBox.Show("Введите логин!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_login.BackColor = Color.Yellow;
                return;
            }
            sqlHandler = new SQLhandler(Database, DataSource, textBox_login.Text, textBox_password.Text);
            try
            {
                sqlHandler.ConnectOpen();
                sqlHandler.ConnectClose();
            }
            catch
            {
                MessageBox.Show("Проверьте подключение, логин и пароль", "Проблема с авторизацией", MessageBoxButtons.OK, MessageBoxIcon.Error );
                textBox_login.BackColor = Color.Red;
                textBox_password.BackColor = Color.Red;
                return;
            }
            MessageBox.Show("Успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            
            authoriz.Login(textBox_login.Text, textBox_password.Text, checkBox_startTimer.Checked, Convert.ToDouble(numericUpDown_intervalTime.Value));
            
            this.Close();
        }

    }
}
