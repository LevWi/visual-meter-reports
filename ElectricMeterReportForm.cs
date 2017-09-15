using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Xml;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MeterReportForm : Form
    {
        public MeterReportForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            dateTimePicker_start.ValueChanged += new EventHandler(DateTimePicker_LostFocus);
            dateTimePicker_end.ValueChanged += new EventHandler(DateTimePicker_LostFocus);
            radioButton1_forMonth.CheckedChanged += new EventHandler(DateTimePicker_LostFocus);
        }

                
        
        /*
        public struct AccumulEnergyForRange
        {
            public int addr;
            public int id;
            public int[] AccumulEnergyActiveStart;
            public int[] AccumulEnergyActiveEnd;
            public int[] AccumulEnergyReactiveStart;
            public int[] AccumulEnergyReactiveEnd;
            public int[] tariff;
            public DateTime startRangeTime;
            public DateTime endRangeTime;
            public int numtariff;

            public AccumulEnergyForRange(int add_, int id_, int CountTariff)
            {
                addr = add_;
                id = id_;
                AccumulEnergyActiveStart = new int[CountTariff];
                AccumulEnergyReactiveStart = new int[CountTariff];
                AccumulEnergyActiveEnd = new int[CountTariff];
                AccumulEnergyReactiveEnd = new int[CountTariff];
                tariff = new int[CountTariff];
                startRangeTime = new DateTime();
                endRangeTime = new DateTime();
                numtariff = 0;
                
            }
        }
       
        private AccumulEnergyForRange SQL_GiveEnergyForRange(int addr, int id, DateTime dt_start, DateTime dt_end, bool forMonth = false)
        {
            Func<DateTime, string> giveStringQuery = (dt) =>
            {
                string query = (forMonth) ? "SELECT * FROM meter230 m where m.addr={0} and m.id={1} and m.oleDT={2} and month={3} and m.period=11 order by tariff"
                    : "SELECT * FROM meter230 m where m.addr={0} and m.id={1} and m.oleDT>={2} and m.oleDT<{3} and m.period=12 order by tariff" ;
                string out_ = (forMonth) ? String.Format(query,
                        addr,
                        id,
                        dt.ToOADate(),
                        dt.Month
                        )
                    :   String.Format(query,
                        addr,
                        id,
                        dt.ToOADate(),
                        dt.AddDays(1).ToOADate()
                        ) ;

                return out_;
            };

            AccumulEnergyForRange accumulEnergyStruct = new AccumulEnergyForRange(addr, id, 5)
            {
                startRangeTime = dt_start,
                endRangeTime = dt_end
            };
            
            // Ищем первую точку
            string formatstr = giveStringQuery(dt_start);
            DataTable table_start = sqlHandler.ReadSqlTable(formatstr);
            // Ищем вторую точку
            formatstr = giveStringQuery(dt_end);
            DataTable table_end = sqlHandler.ReadSqlTable(formatstr);
            // DataTable dtfin = table_start.Copy();
            // index, addr, energy_active_in, energy_reactive_in, energy_reactive_out, period, oleDT, tariff, id, month
            if ((table_start.Rows.Count > 0) && (table_end.Rows.Count == table_start.Rows.Count))
            {
                for (int i = 0; i < table_start.Rows.Count; i++)
                {
                    
                    if (Convert.ToInt32(table_end.Rows[i]["tariff"]) == Convert.ToInt32(table_start.Rows[i]["tariff"]))
                    {
                        accumulEnergyStruct.numtariff++;
                        accumulEnergyStruct.AccumulEnergyActiveStart[i] = Convert.ToInt32(table_start.Rows[i]["energy_active_in"]);
                        accumulEnergyStruct.AccumulEnergyReactiveStart[i] = Convert.ToInt32(table_start.Rows[i]["energy_reactive_in"]);
                        accumulEnergyStruct.AccumulEnergyActiveEnd[i] = Convert.ToInt32(table_end.Rows[i]["energy_active_in"]);
                        accumulEnergyStruct.AccumulEnergyReactiveEnd[i] = Convert.ToInt32(table_end.Rows[i]["energy_reactive_in"]);
                        accumulEnergyStruct.tariff[i] = Convert.ToInt32(table_start.Rows[i]["tariff"]);
                    }
                    else
                    {
                        accumulEnergyStruct.tariff[i] = -1;
                    }
                }
                
            }
            return accumulEnergyStruct;
        }
        */

      /*    <th width="5%" scope="col">№ п/п</th>
            <th  scope="col">Подразделение</th>
            <th width="5%" scope="col">Тариф</th>
            <th width="20%" scope="col">На начало кВт*ч</th>
            <th width="20%" scope="col">На конец кВт*ч</th>
            <th width="20%" scope="col">Потреблено кВт*ч</th>
            <th  scope="col">Комментарий</th>  */

       /* private string ReadBaseRange(XmlNodeList nodelist, DateTime dt_start, DateTime dt_end)
        {
            string htmlrowsTamplet = "<tr>"
                       + "<td><div align=\"center\">$index</div></td>"
                       + "<td><div align=\"center\">$namegroup</div></td>"
                       + "<td><div align=\"center\">$tariff</div></td>"
                       + "<td><div align=\"center\">$startpower</div></td>"
                       + "<td><div align=\"center\">$endpower</div></td>"
                       + "<td><div align=\"center\">$power</div></td>"
                       + "<td><div align=\"center\">$info</div></td>"
                       + "</tr>\n\r ";

            string htmlRows = "";
            string str_htmlRow = "";
            //dbl_groupSumPower = 0;
            
            foreach (XmlNode node in nodelist)
            {
                //DataTable table = sqlHandler.ReadSqlTable(String.Format("SELECT * FROM meter230 m where m.addr={0} and m.oleDT={1} and m.id={2} and m.month={3}",
                //    node.Attributes["addr"].Value,
                //    date.ToOADate(),
                //    node.Attributes["id"].Value,
                //    date.Month
                //    ));
                bool forMonth = radioButton1_forMonth.Checked;
                double current_scale = 1;
                try
                {
                 current_scale = Convert.ToDouble(node.Attributes["k_current"].Value);
                }
                catch
                {
                }

                AccumulEnergyForRange accumulenergystruct = SQL_GiveEnergyForRange(Int32.Parse(node.Attributes["addr"].Value),
                                                                                   Int32.Parse(node.Attributes["id"].Value),
                                                                                   dt_start,
                                                                                   dt_end,
                                                                                   forMonth);
                str_htmlRow = htmlrowsTamplet.Replace("$namegroup", "сч.№ " + node.Attributes["id"].Value);

                if (accumulenergystruct.numtariff > 0)
                {
                    for (int i = 0; i < accumulenergystruct.tariff.Length; i++)
                    {
                        double tarifpower = Convert.ToDouble(accumulenergystruct.AccumulEnergyActiveEnd[i] - accumulenergystruct.AccumulEnergyActiveStart[i]) / 1000 * current_scale;
                        double tarifpowerStart = Convert.ToDouble(accumulenergystruct.AccumulEnergyActiveStart[i]) / 1000 * current_scale;
                        double tarifpowerEnd = Convert.ToDouble(accumulenergystruct.AccumulEnergyActiveEnd[i]) / 1000 * current_scale;

                        if (i == 0)
                        {
                            str_htmlRow = str_htmlRow.Replace("$power", String.Format("{0:F3}", tarifpower))
                                          .Replace("$startpower", String.Format("{0:F3}", tarifpowerStart))
                                          .Replace("$endpower", String.Format("{0:F3}", tarifpowerEnd));
                        }
                        else if (tarifpower > 0)
                        {
                            str_htmlRow += htmlrowsTamplet.Replace("$power", String.Format("{0:F3}", tarifpower))
                                          .Replace("$startpower", String.Format("{0:F3}", tarifpowerStart))
                                          .Replace("$endpower", String.Format("{0:F3}", tarifpowerEnd));
                        }
                        str_htmlRow = str_htmlRow.Replace("$tariff", accumulenergystruct.tariff[i].ToString());
                        //dbl_groupSumPower += tarifpower;
                    }
                    str_htmlRow = str_htmlRow.Replace("$namegroup", "&#8659;");
                }
                else
                {
                    str_htmlRow = str_htmlRow.Replace("$power", "---");
                    str_htmlRow = str_htmlRow.Replace("$endpower", "---");
                    str_htmlRow = str_htmlRow.Replace("$startpower", "---");
                    str_htmlRow = str_htmlRow.Replace("$info", "Н/Д");
                }

                str_htmlRow = str_htmlRow.Replace("$index", "");
                str_htmlRow = str_htmlRow.Replace("$info", "");
                htmlRows += str_htmlRow;
                invokeIncrementProgressbar();
            }
            return htmlRows;
        }*/

        



        //private void ReadWaterBase()
        //{
        //    var dt_start_local = new DateTime(dateTimePicker_start.Value.Year, dateTimePicker_start.Value.Month, dateTimePicker_start.Value.Day);
        //    var dt_end_local = new DateTime(dateTimePicker_end.Value.Year, dateTimePicker_end.Value.Month, dateTimePicker_end.Value.Day);

        //    string htmlHeadrowsTamplet = "<tr class=\"Hilight\">"
        //               + "<td><div align=\"center\">--</div></td>"
        //               + "<td><div align=\"center\">$nameid</div></td>"
        //               + "<td><div align=\"center\">--</div></td>"
        //               + "<td><div align=\"center\">--</div></td>"
        //               + "<td><div align=\"center\">--</div></td>"
        //               + "<td><div align=\"center\">--</div></td>"
        //               + "<td><div align=\"center\">$info</div></td>"
        //               + "</tr>\n\r ";
        //    string htmlrowsTamplet = "<tr>"
        //               + "<td><div align=\"center\">$index</div></td>"
        //               + "<td><div align=\"center\">$nameid</div></td>"
        //               + "<td><div align=\"center\">$typewater</div></td>"
        //               + "<td><div align=\"center\">$startwater</div></td>"
        //               + "<td><div align=\"center\">$endwater</div></td>"
        //               + "<td><div align=\"center\">$difference</div></td>"
        //               + "<td><div align=\"center\">$info</div></td>"
        //               + "</tr>\n\r ";
        //    var xmlDocument = new XmlDocument();
        //    xmlDocument.Load("waterMeter_conf.xml");
        //    XmlNodeList Nodes = xmlDocument.SelectNodes("//DataBaseSQL");
        //    if (Nodes.Count > 0)
        //    {
        //        sqlHandler = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
        //        TableWatermetersData = Nodes[0].Attributes["tabledata"].Value;
        //        TableWatermetersNameLink = Nodes[0].Attributes["tableLinkName"].Value;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Отсутствуют данные к подключению к базе!");
        //        return;
        //    }
        //    // получаем группы водомерных узлов
        //    XmlNodeList watermeterunits = xmlDocument.SelectNodes("watermeters/node");
        //    int ComNumOfMeters = xmlDocument.SelectNodes("//watermeter").Count;

        //    setMaxLevelProgressbar(ComNumOfMeters);

        //    if (watermeterunits.Count > 0)
        //    {
        //        string htmlRows = "";
        //        foreach (XmlNode metersunit in watermeterunits)
        //        {
        //            htmlRows += htmlHeadrowsTamplet
        //                .Replace("$nameid", metersunit.Attributes["name"].Value)
        //                .Replace("$info", metersunit.Attributes["consumer"].Value);
        //            foreach (XmlNode watermeter in metersunit.ChildNodes)
        //            {
        //                WaterMeterConsumption localmeter = GiveWaterData(watermeter, dt_start_local, dt_end_local);
        //                htmlRows += htmlrowsTamplet
        //                    .Replace("$index", "")
        //                    .Replace("$nameid", localmeter.meterId)
        //                    .Replace("$typewater", localmeter.waterType)
        //                    .Replace("$startwater", localmeter.ConsumptionStartPoint.ToString("F3"))
        //                    .Replace("$endwater", localmeter.ConsumptionEndPoint.ToString("F3"))
        //                    .Replace("$difference", localmeter.Difference.ToString("F3"))
        //                    .Replace("$info", "");
        //                invokeIncrementProgressbar();

        //            }
        //        }
        //        string rangeStr = String.Format("c {0} по {1}", dateTimePicker_start.Value.ToString("dd.MM.yyyy"), dateTimePicker_end.Value.ToString("dd.MM.yyyy"));

        //        string tamplateStr = System.IO.File.ReadAllText(@"Reports\WaterTamplate.report");
        //        tamplateStr = tamplateStr
        //            .Replace("$comment", String.Format("Показания за период {0} <br>Общее количество счетчиков {1}", rangeStr, ComNumOfMeters.ToString()))
        //            .Replace("$rows", htmlRows);

        //        string nameFile = @"Reports\" + String.Format("Водосчетчики_за_Период_{0}_", rangeStr) +
        //                                        String.Format("-{0}_{1}_{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) + ".html";
        //        System.IO.File.WriteAllText(nameFile, tamplateStr);
        //        System.Diagnostics.Process.Start(nameFile);

        //    }

        //}


        private void ReadElectricBase()
        {
            Reporter rep = new Reporter();
            rep.invokeIncrementProgressbar = InvokeIncrementProgressbar;
            rep.setMaxLevelProgressbar = SetMaxLevelProgressbar;
            //rep.msgShow = delegate(string str) { MessageBox.Show(str); };

            rep.ReadElectricBase(dateTimePicker_start.Value, dateTimePicker_end.Value, radioButton1_forMonth.Checked, allDaysSum.Checked);
        }

        private void ReadWaterBase()
        {
            Reporter rep = new Reporter();
            rep.invokeIncrementProgressbar = InvokeIncrementProgressbar;
            rep.setMaxLevelProgressbar = SetMaxLevelProgressbar;
            //rep.msgShow = delegate (string str) { MessageBox.Show(str); };

            rep.ReadWaterBase(dateTimePicker_start.Value, dateTimePicker_end.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            if (radioButton_Electric.Checked)
                        Task.Factory.StartNew(ReadElectricBase);
            if (radioButton_Water.Checked)
                        Task.Factory.StartNew(ReadWaterBase);
        }

        public void SetMaxLevelProgressbar(int val)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate
                    {
                        toolStripProgressBar1.Maximum = val;
                    }
                ));
            }
            else
            {
                toolStripProgressBar1.Maximum = val;
            }
        }

        public void InvokeIncrementProgressbar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate
                    {
                        toolStripProgressBar1.Increment(1);
                    }
                ));

            }
            else
            {
                toolStripProgressBar1.Increment(1);
            }
        }


        private void ChangeRange()
        {
            dateTimePicker_start.ValueChanged -= new EventHandler(DateTimePicker_LostFocus);
            dateTimePicker_end.ValueChanged -= new EventHandler(DateTimePicker_LostFocus);

            if (dateTimePicker_start.Value > dateTimePicker_end.Value)
            {
                dateTimePicker_end.Value = dateTimePicker_start.Value;
            }
            if (radioButton1_forMonth.Checked)
            {
                if (dateTimePicker_end.Value.Month <= dateTimePicker_start.Value.Month)
                {
                    dateTimePicker_end.Value = (new DateTime(dateTimePicker_start.Value.Year, dateTimePicker_start.Value.Month, 1)).AddMonths(1);
                }
                if (dateTimePicker_start.Value.Day != 1)
                {
                    dateTimePicker_start.Value = new DateTime(dateTimePicker_start.Value.Year, dateTimePicker_start.Value.Month, 1);
                }
                if (dateTimePicker_end.Value.Day != 1)
                {
                    dateTimePicker_end.Value = new DateTime(dateTimePicker_end.Value.Year, dateTimePicker_end.Value.Month, 1);
                }
            }
            dateTimePicker_start.ValueChanged += new EventHandler(DateTimePicker_LostFocus);
            dateTimePicker_end.ValueChanged += new EventHandler(DateTimePicker_LostFocus);
        }

        void DateTimePicker_LostFocus(object sender, EventArgs e)
        {
            ChangeRange();
        }
    }
}
