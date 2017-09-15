using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MercuryMeter;
using System.Net.Sockets;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.IO.Ports;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class EditMeterForm : Form
    {
        ElectricMeterWinForm_forTable[] meter_elem_arr_;
        Authorization authrization_;
        XmlDocument xmldocument_;
        SQLhandler sqlhandler_;
        string TCP_Host_s;
        int TCP_port_i;


        public EditMeterForm(ElectricMeterWinForm_forTable[] meters, Authorization autoriz, XmlDocument xmldoc)
        {
            InitializeComponent();
            authrization_ = autoriz;
            meter_elem_arr_ = meters;
            xmldocument_ = xmldoc;
            authrization_.LogInGhanged += ControlsOffON;
        }

        private void EditMeterForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < meter_elem_arr_.Length; i++)
            {
                comboBox_addressMeter.Items.Add(meter_elem_arr_[i].ADDR_dgvCell.Value.ToString());
                comboBox_serialNumber.Items.Add(meter_elem_arr_[i].ID_dgvCell.Value.ToString());
                if (meter_elem_arr_[i].ADDR_dgvCell.Selected)
                {
                    comboBox_serialNumber.SelectedIndex = i;
                    comboBox_addressMeter.SelectedIndex = i;
                }
            }
            comboBox_serialNumber.SelectedIndexChanged += new EventHandler(comboBox_serialNumber_SelectedIndexChanged);
            comboBox_serialNumber.SelectedIndexChanged += new EventHandler(comboBox_serialNumber_SelectedIndexChanged2);
            comboBox_addressMeter.SelectedIndexChanged += new EventHandler(comboBox_addressMeter_SelectedIndexChanged);

            ControlsOffON();
            ShowZoneOnLabel();

            XmlNodeList xmlnode = xmldocument_.SelectNodes("//MeterServer");
            if (xmlnode.Count > 0)
            {
                try
                {
                    TCP_Host_s = xmlnode[0].Attributes["ipaddr"].Value;
                    TCP_port_i = int.Parse(xmlnode[0].Attributes["port"].Value);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else
            {
                MessageBox.Show("Ошибка чтения данных для связи с сервером", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            XmlNodeList Nodes = xmldocument_.SelectNodes("//DataBaseSQL");
            if (Nodes.Count > 0)
            {
                sqlhandler_ = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
            }
            else
            {
                MessageBox.Show("Ошибка в данных для подключения к БД", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Синхронизация работы двух combobox
        void comboBox_serialNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_addressMeter.SelectedIndexChanged -= new EventHandler(comboBox_addressMeter_SelectedIndexChanged);

            comboBox_addressMeter.SelectedIndex = comboBox_serialNumber.SelectedIndex;

            comboBox_addressMeter.SelectedIndexChanged += new EventHandler(comboBox_addressMeter_SelectedIndexChanged);
        }
        
        void comboBox_serialNumber_SelectedIndexChanged2(object sender, EventArgs e)
        {
            ShowZoneOnLabel(); // изменение комментария по счетчику проводим только в событии по серийному номеру
        }

        void comboBox_addressMeter_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_serialNumber.SelectedIndexChanged -= new EventHandler(comboBox_serialNumber_SelectedIndexChanged);

            comboBox_serialNumber.SelectedIndex = comboBox_addressMeter.SelectedIndex;

            comboBox_serialNumber.SelectedIndexChanged += new EventHandler(comboBox_serialNumber_SelectedIndexChanged);
        }
        #endregion

        private void ShowZoneOnLabel()
        {
                textBox_comment.Text = "";
                foreach (var meter in meter_elem_arr_)
                {
                    if (meter.ID_dgvCell.Value.ToString() == comboBox_serialNumber.Text)
                    {
                        if ((meter.admin_zone_dgvCell.Value == null) || (meter.zone_dgvCell.Value == null))
                        {
                            break;
                        }
                        textBox_comment.Text = meter.admin_zone_dgvCell.Value.ToString() + " \r\n" + meter.zone_dgvCell.Value.ToString();
                        break;
                    }
                }
            
            
        }

        public void ControlsOffON()
        {
           groupBox_current.Enabled = authrization_.isLoggedIn;
           groupBox_power.Enabled = authrization_.isLoggedIn;
           groupBox_voltage.Enabled = authrization_.isLoggedIn;
           button_readbase.Enabled = authrization_.isLoggedIn;
           button_writeData.Enabled = authrization_.isLoggedIn;
        }

        void WriteParametersFromMeter(Mercury230_DatabaseSignals meter)
        {
            numericUpDown_cur_hist1.Value = Convert.ToDecimal(meter.Phases[0].current.Hist);
            numericUpDown_cur_hist2.Value = Convert.ToDecimal(meter.Phases[1].current.Hist);
            numericUpDown_cur_hist3.Value = Convert.ToDecimal(meter.Phases[2].current.Hist);

            numericUpDown_cur_up1.Value = Convert.ToDecimal(meter.Phases[0].current.MaxValue);
            numericUpDown_cur_up2.Value = Convert.ToDecimal(meter.Phases[1].current.MaxValue);
            numericUpDown_cur_up3.Value = Convert.ToDecimal(meter.Phases[2].current.MaxValue);
            
            numericUpDown_voltUp1.Value = Convert.ToDecimal(meter.Phases[0].voltage.MaxValue);
            numericUpDown_voltUp2.Value = Convert.ToDecimal(meter.Phases[1].voltage.MaxValue);
            numericUpDown_voltUp3.Value = Convert.ToDecimal(meter.Phases[2].voltage.MaxValue);

            numericUpDown_voltDown1.Value = Convert.ToDecimal(meter.Phases[0].voltage.MinValue);
            numericUpDown_voltDown2.Value = Convert.ToDecimal(meter.Phases[1].voltage.MinValue);
            numericUpDown_voltDown3.Value = Convert.ToDecimal(meter.Phases[2].voltage.MinValue);

            numericUpDown_voltHist1.Value = Convert.ToDecimal(meter.Phases[0].voltage.Hist);
            numericUpDown_voltHist2.Value = Convert.ToDecimal(meter.Phases[1].voltage.Hist);
            numericUpDown_voltHist3.Value = Convert.ToDecimal(meter.Phases[2].voltage.Hist);

            numericUpDown_power_hist1.Value = Convert.ToDecimal(meter.CommonActivePower.Hist/1000);
            numericUpDown_power_up1.Value = Convert.ToDecimal(meter.CommonActivePower.MaxValue/1000);
        }

        void WriteMeterFromParameters(Mercury230_DatabaseSignals meter)
        {
           meter.Phases[0].current.Hist = Convert.ToSingle(numericUpDown_cur_hist1.Value);
           meter.Phases[1].current.Hist = Convert.ToSingle(numericUpDown_cur_hist2.Value);
           meter.Phases[2].current.Hist = Convert.ToSingle(numericUpDown_cur_hist3.Value);

           meter.Phases[0].current.MaxValue = Convert.ToSingle(numericUpDown_cur_up1.Value);
           meter.Phases[1].current.MaxValue = Convert.ToSingle(numericUpDown_cur_up2.Value);
           meter.Phases[2].current.MaxValue = Convert.ToSingle(numericUpDown_cur_up3.Value);

           meter.Phases[0].voltage.MaxValue = Convert.ToSingle(numericUpDown_voltUp1.Value);
           meter.Phases[1].voltage.MaxValue = Convert.ToSingle(numericUpDown_voltUp2.Value);
           meter.Phases[2].voltage.MaxValue = Convert.ToSingle(numericUpDown_voltUp3.Value);

           meter.Phases[0].voltage.MinValue = Convert.ToSingle(numericUpDown_voltDown1.Value);
           meter.Phases[1].voltage.MinValue = Convert.ToSingle(numericUpDown_voltDown2.Value);
           meter.Phases[2].voltage.MinValue = Convert.ToSingle(numericUpDown_voltDown3.Value);

           meter.Phases[0].voltage.Hist = Convert.ToSingle(numericUpDown_voltHist1.Value);
           meter.Phases[1].voltage.Hist = Convert.ToSingle(numericUpDown_voltHist2.Value);
           meter.Phases[2].voltage.Hist = Convert.ToSingle(numericUpDown_voltHist3.Value);

           meter.CommonActivePower.Hist     = Convert.ToSingle(numericUpDown_power_hist1.Value ) * 1000;
           meter.CommonActivePower.MaxValue = Convert.ToSingle(numericUpDown_power_up1.Value   ) * 1000;
        }

        private int Read_SQL_data_from_meter()
        {
            try
            {
                string sqlselect = String.Format("select * from dumpmeters where addr={0} and id={1}", comboBox_addressMeter.Text, comboBox_serialNumber.Text);
                DataTable dt = sqlhandler_.ReadSqlTable(sqlselect);
                if (dt.Rows.Count > 0)
                {
                    string objSerStr = dt.Rows[0]["dump"].ToString();

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Mercury230_DatabaseSignals));
                    Mercury230_DatabaseSignals meterLocal =
                                    (Mercury230_DatabaseSignals)serializer.ReadObject(new System.IO.MemoryStream(Encoding.ASCII.GetBytes(objSerStr)));
                    WriteParametersFromMeter(meterLocal);
                }
                else
                {
                    MessageBox.Show("Будут взяты данные по умолчанию\n\r", "Нет информации в БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
                return 1;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка при чтении БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            
        }

        private int WriteDumpToSQL()
        {
            try
            {
                byte addr = byte.Parse(comboBox_addressMeter.Text);
                int id = Int32.Parse(comboBox_serialNumber.Text) ;

                string objSerStr;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Mercury230_DatabaseSignals));
                byte[][] bytemas = new byte[3][];
                Mercury230_DatabaseSignals meterlocal = new Mercury230_DatabaseSignals(new SerialPort(), addr, id, bytemas);

                WriteMeterFromParameters(meterlocal);

                using (var memorystream = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(memorystream, meterlocal);
                    objSerStr = Encoding.ASCII.GetString(memorystream.ToArray());
                }
                objSerStr = MySqlHelper.EscapeString(objSerStr);

                StringBuilder strb = new StringBuilder();

                string sqlselect = String.Format("select * from dumpmeters where addr={0} and id={1}", addr, id);
                DataTable dt = sqlhandler_.ReadSqlTable(sqlselect);

                if (dt.Rows.Count == 0)
                {
                    // insert into

                    strb.AppendFormat("INSERT INTO dumpmeters (addr,id,dump) VALUES({0},{1},'{2}')", addr, id, objSerStr);

                    sqlhandler_.SendData(strb.ToString());

                    MessageBox.Show("Данные записаны в БД\n\r", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    // update

                    strb.AppendFormat("UPDATE dumpmeters SET dump='{0}' WHERE addr={1} and id={2}", objSerStr, addr, id);

                    sqlhandler_.SendData(strb.ToString());

                    MessageBox.Show("Данные изменены\n\r", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                // Пока посылку обновления не исспользуем 
                // можно добавить потом
                //if (sendRefreshSignal() > 0)
                //{
                //    MessageBox.Show("Успешно", "Посылка обновления данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                return 1;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка при работе с БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            
        }

        #region выключены из работы
        private int sendRefreshSignal()
        {
            try
            {
                TcpClient newClient = new TcpClient(TCP_Host_s, TCP_port_i);
                newClient.ReceiveTimeout = 1500;
                newClient.SendTimeout = 800;
                NetworkStream tcpStream = newClient.GetStream();

                XmlDocument xmlquery = new XmlDocument();

                byte[] sendBytes = Encoding.UTF8.GetBytes("<query><type>refresh_limits</type></query>");

                tcpStream.Write(sendBytes, 0, sendBytes.Length);

                byte[] bytes = new byte[newClient.ReceiveBufferSize];
                int bytesRead = tcpStream.Read(bytes, 0, newClient.ReceiveBufferSize);
                
                // Строка, содержащая ответ от сервера
                string answer = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                try
                {
                    xmlquery.LoadXml(answer);
                }
                catch (XmlException exc)
                {
                    MessageBox.Show("Ошибочный ответ сервера\n\r" + exc.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Threading.Thread.Sleep(50);
                    return -1;
                    //continue;
                }
                XmlNodeList nodeList = xmlquery.SelectNodes("Error");
                if (nodeList.Count == 0)
                {
                    nodeList = xmlquery.SelectNodes("answer");
                    if (nodeList.Count == 1)
                    {
                        if (nodeList[0].InnerText == "ОК")
                            return 1;
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("При посылке сигнала обновления\n\r" + ex.Message, "Ошибка связи с сервером", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -10;
                //newClient.Close();
                //Console.WriteLine("Перезапуск соединения через {0} сек", this.intervalRestartConnection / 1000);
                //RefreshTimer.Interval = this.intervalRestartConnection;
            }
            return 0;
        }

        private int Refresh_data_meters()
        {
            // Console.WriteLine("Пуск события опроса");

            //Action<MetersParameterDataGridBoxCell, MetersParameter> ParAssignment = (ParBoxCell, Par) =>
            //{
            //    ParBoxCell.Parametr.Hist = Par.Hist;
            //    ParBoxCell.Parametr.MinValue = Par.MinValue;
            //    ParBoxCell.Parametr.MaxValue = Par.MaxValue;
               
            //    //ParBoxCell.Parametr.RefreshData();
            //};
            TcpClient newClient = new TcpClient(TCP_Host_s, TCP_port_i);



            try
            {

                if (!newClient.Connected)
                {
                    Console.WriteLine("Соединение...");
                    // пошлём на установку соединения
                    if (newClient.Client != null)
                        newClient.Client.Close();
                    newClient = new TcpClient(TCP_Host_s, TCP_port_i);
                    Console.WriteLine("Успешно");
                }
                //Console.WriteLine(DateTime.Now.ToString() + "... Обновление данных");

                newClient.ReceiveTimeout = 1500;
                newClient.SendTimeout = 800;
                // порождает исключение, если
                // при соединении возникают проблемы
                NetworkStream tcpStream = newClient.GetStream();
                //string str_badformat = "<Error>badformat</Error>";
                //string str_null = "<Error>null</Error>";
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Mercury230_DatabaseSignals));
                XmlDocument xmlquery = new XmlDocument();
                //int count_normalMeter = 0;
                
                byte[] sendBytes = Encoding.UTF8.GetBytes("<query><type>readmeter</type><model>Merc230</model><add_meter>" + comboBox_addressMeter.Text
                                                               + "</add_meter><id_meter>"
                                                               + comboBox_serialNumber.Text 
                                                               + "</id_meter></query>");

                    tcpStream.Write(sendBytes, 0, sendBytes.Length);

                byte[] bytes = new byte[newClient.ReceiveBufferSize];
                int bytesRead = tcpStream.Read(bytes, 0, newClient.ReceiveBufferSize);

                    // Строка, содержащая ответ от сервера
                string answer = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                    try
                    {
                        xmlquery.LoadXml(answer);
                    }
                    catch (XmlException exc)
                    {
                        MessageBox.Show("Ошибочный ответ сервера\n\r" + exc.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Threading.Thread.Sleep(50);
                        return -1;
                        //continue;
                    }
                    XmlNodeList nodeList = xmlquery.SelectNodes("Error");
                    if (nodeList.Count == 0)
                    {
                        nodeList = xmlquery.SelectNodes("answer/meter");
                        if (nodeList.Count == 1)
                        {
                            string objSerStr = nodeList[0].InnerText;
                            Mercury230_DatabaseSignals meterLocal =
                                (Mercury230_DatabaseSignals)serializer.ReadObject(new System.IO.MemoryStream(Encoding.ASCII.GetBytes(objSerStr)));
                            //elem.DateTime_lastTime_connection = meterLocal.DateTime_lastTime_connection;
                            //elem.error_meter = meterLocal.error_meter;
                            //if (elem.error_meter == MeterDevice.error_type.none)
                            //    count_normalMeter++;

                            WriteParametersFromMeter(meterLocal);

                            //ParAssignment(elem.volt_phase1_dgvCell, meterLocal.Phases[0].voltage);
                            //ParAssignment(elem.volt_phase2_dgvCell, meterLocal.Phases[1].voltage);
                            //ParAssignment(elem.volt_phase3_dgvCell, meterLocal.Phases[2].voltage);
                            //ParAssignment(elem.cur_phase1_dgvCell, meterLocal.Phases[0].current);
                            //ParAssignment(elem.cur_phase2_dgvCell, meterLocal.Phases[1].current);
                            //ParAssignment(elem.cur_phase3_dgvCell, meterLocal.Phases[2].current);
                            //ParAssignment(elem.power_dgvCell, meterLocal.CommonPower);

                        }
                    }
            }
                //RefreshTimer.Interval = 3000;
                //Console.WriteLine("Нормально опрошены: {0}", count_normalMeter);
            catch (SocketException ex)
            {
                MessageBox.Show("Ошибка\n\r" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -10;
                //newClient.Close();
                //Console.WriteLine("Перезапуск соединения через {0} сек", this.intervalRestartConnection / 1000);
                //RefreshTimer.Interval = this.intervalRestartConnection;
            }
            return 1;
        }
#endregion

        private void button_writeData_Click(object sender, EventArgs e)
        {
            WriteDumpToSQL();
        }

        private void button_readbase_Click(object sender, EventArgs e)
        {
            Read_SQL_data_from_meter();
        }

    }
}
