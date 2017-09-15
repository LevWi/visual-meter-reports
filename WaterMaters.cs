using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.Data;
using System.Timers;
using System.Drawing;

namespace WindowsFormsApplication1
{
    /*
     *  <node name="Водом. узел №1" location="-2,700; 19/Л-И" consumer="Общий ВУ на вводе">
        <watermeter  snumber="14578972"  function="В1" >
            <point time="02/14/17 12:00:00" data="3542.0" />
        </watermeter>
          </node>
     * 
     * 
    */

    public struct WaterMeterOffsetPoint
    {
        public bool DataTrue;
        public float OffSetFromBase;
        public DateTime TimePoint;
        public float Offset;
        public float RealOffSet 
        {
            get
            {
                return Offset - OffSetFromBase;
            }
        }
        public string GiveDefTimePointString()
        {
            return TimePoint.ToString(DateTimeFormatInfo.InvariantInfo);
        }
        public int SetTimePointFromStrings(string strdt, string flt)
        {
            try
            {
                TimePoint = DateTime.Parse(strdt, DateTimeFormatInfo.InvariantInfo);
                Offset = float.Parse(flt);
                this.DataTrue = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public WaterMeterOffsetPoint(float offset, DateTime dt)
        {
            this.OffSetFromBase = 0;
            this.Offset = offset;
            this.TimePoint = dt;
            this.DataTrue = false;
        }
    }

    public partial class Form1 : Form
    {

        public class WaterMeter_Cells
        {
            
            private System.Timers.Timer BlinkerTimer;
            public WaterMeterOffsetPoint offsetPoint;
            //public DataGridViewTextBoxCell Status = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell NameNode = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell Watermeter_SerialNumber = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell LocationNode = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell Consumer = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell Function = new DataGridViewTextBoxCell();
            public DataGridViewTextBoxCell Data = new DataGridViewTextBoxCell();

            public void WriteNewData(float dbl, bool gooddata = true)
            {
                if (this.Data.Value.ToString() != dbl.ToString())
                {
                    this.Data.Value = dbl.ToString();
                    Data.Style.BackColor = Color.Chartreuse;
                    BlinkerTimer.Enabled = true;
                }
                if (!gooddata)
                {
                    Data.Style.BackColor = Color.Yellow;
                }
            }

            void OnBlinkerTimedEvent(Object source, ElapsedEventArgs e)
            {
                this.Data.Style.BackColor = Color.LightGray;
            }

            public WaterMeter_Cells() 
            {
                this.Data.Value = "0.0";
                //this.Status.Value = "Норм";
                offsetPoint = new WaterMeterOffsetPoint();
                BlinkerTimer = new System.Timers.Timer(1500);
                BlinkerTimer.Elapsed += this.OnBlinkerTimedEvent;
                BlinkerTimer.AutoReset = false;
            }
        }
        WaterMeter_Cells[] WaterMeters;
        SQLhandler sqlHandler;
        public string TableWatermetersNameLink = "";
        public string TableWatermetersData = "";

        public void LoadWaterMeters()
        {
            Action<DataGridViewTextBoxCell, XmlNode, string> writeCellsValue =
                (datagridCell, nodes, atr) =>
                {
                    //if (nodes.Count == 0) 
                    //    return;
                    try
                    {
                        datagridCell.Value = nodes.Attributes[atr].Value;
                    }
                    catch (ArgumentException)
                    {
                        datagridCell.Value = "#error#";
                        return;
                    }
                    catch (NullReferenceException)
                    {
                        datagridCell.Value = "н/д";
                        return;
                    }
                };
            XmlDocument xmlDocument = new XmlDocument();
            // try
            // {
            xmlDocument.Load("waterMeter_conf.xml");
            // Настройка связи с БД
            XmlNodeList Nodes = xmlDocument.SelectNodes("//DataBaseSQL");
            if (Nodes.Count > 0)
            {
              sqlHandler = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
              TableWatermetersData = Nodes[0].Attributes["tabledata"].Value;
              TableWatermetersNameLink = Nodes[0].Attributes["tableLinkName"].Value;
            }
            //
            Nodes = xmlDocument.SelectNodes("//watermeter");
            WaterMeters = new WaterMeter_Cells[Nodes.Count];
            DataGridViewRow row;
            if (Nodes.Count > 0)
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    row = new DataGridViewRow();
                    WaterMeters[i] = new WaterMeter_Cells();

                    row.Cells.AddRange(
                        //WaterMeters[i].Status,
                        WaterMeters[i].NameNode,
                        WaterMeters[i].Watermeter_SerialNumber,
                        WaterMeters[i].LocationNode,
                        WaterMeters[i].Consumer,
                        WaterMeters[i].Function,
                        WaterMeters[i].Data
                    );
                    this.dataGridView_watermeter.Rows.Add(row);
                    writeCellsValue(WaterMeters[i].NameNode, Nodes[i].ParentNode     /*для имени нужен родительский узел*/,     "name");
                    writeCellsValue(WaterMeters[i].LocationNode, Nodes[i].ParentNode /*нужен родительский узел*/,           "location");
                    writeCellsValue(WaterMeters[i].Consumer, Nodes[i].ParentNode ,                  "consumer" );
                    writeCellsValue(WaterMeters[i].Watermeter_SerialNumber, Nodes[i] ,               "snumber" );
                    writeCellsValue(WaterMeters[i].Function, Nodes[i] ,                              "function");
                    // Подчитаываем сдвиг показаний
                    if (Nodes[i].ChildNodes != null)
                    {
                        foreach (XmlNode nd in Nodes[i].ChildNodes)
                        {
                            if (nd.Name == "point")
                            {
                                try
                                {
                                    WaterMeters[i].offsetPoint.SetTimePointFromStrings(nd.Attributes["time"].Value, nd.Attributes["data"].Value);
                                }
                                catch(ArgumentException)
                                {
                                    Console.WriteLine("Ошибка чтения сдвига для счетчика №{0}", WaterMeters[i].Watermeter_SerialNumber.Value);
                                }
                            }
                        }
                    }
                   
                }
            }

            dataGridView_watermeter.AutoResizeColumns();
            /*
             * Перекресный запрос по номеру счетчика 
             * SELECT mdate.*   FROM meternamelink mlink, float_watermeter_table_test mdate
              where locate('55144578', mlink.NameMeter) > 0 and mlink.ChID = mdate.chID and flagsMask = 0 ;
             * 
             * 
             * */
            // Корректируем сдвиг показаний, сопоставля показания с прибора учета и базы данных
            Task readOffsetFromSql = Task.Factory.StartNew(delegate
            {
                for (int i = 0; i < WaterMeters.Length; i++)
                {
                    string sqlselect = "SELECT mdate.*   FROM " + TableWatermetersNameLink
                        + " mlink, " + TableWatermetersData + " mdate where locate('" + WaterMeters[i].Watermeter_SerialNumber.Value.ToString() + "', mlink.NameMeter) > 0 and mlink.ChID = mdate.chID and mdate.flagsMask = 0 and mdate.oleDT < " +
                            WaterMeters[i].offsetPoint.TimePoint.ToOADate().ToString(System.Globalization.CultureInfo.InvariantCulture) + " order by Id desc limit 1";
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = sqlHandler.ReadSqlTable(sqlselect);
                    }
                    catch(Exception e){
                        Console.WriteLine("Ошибка чтения к базе {0}", e.Message);
                        break;
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //Console.WriteLine(" Счетчик {0} - Значение доп сдвига {1} время {2}", WaterMeters[i].Watermeter_SerialNumber.Value,
                        //                                                                    dt.Rows[0]["Value"],
                        //                                                                    DateTime.FromOADate((double)dt.Rows[0]["oleDT"]));
                        WaterMeters[i].offsetPoint.OffSetFromBase = (float)dt.Rows[0]["Value"];
                    }


                }
                Console.WriteLine("Сдвиги водосчетчиков считаны");
            });

            ReloadWaterMetersTimer = new System.Timers.Timer(3000);
            ReloadWaterMetersTimer.Elapsed += this.OnReloadWaterMetersFromMySql;
            ReloadWaterMetersTimer.AutoReset = false;
            //XmlNodeList devices = xmlDocument.SelectNodes("/watermeters/DataBaseSQL");
            //if (devices.Count > 0)
        }

        private System.Timers.Timer ReloadWaterMetersTimer;

        public void OnReloadWaterMetersFromMySql(Object source, ElapsedEventArgs e)
        {
            
            ReloadWaterMetersFromMySql();
        }

        public void ReloadWaterMetersFromMySql()
        {
            ReloadWaterMetersTimer.Enabled = false;
            Console.WriteLine("Обновление водяных счетчиков");
            // Обновляем данные водосчетчиков
            for (int i = 0; i < WaterMeters.Length; i++)
            {
                string sqlselect = "SELECT mdate.*   FROM " + TableWatermetersNameLink
                    + " mlink, " + TableWatermetersData + " mdate where locate('" + WaterMeters[i].Watermeter_SerialNumber.Value + "', mlink.NameMeter) > 0 and mlink.ChID = mdate.chID order by Id desc limit 1";
                DataTable dt = sqlHandler.ReadSqlTable(sqlselect);
                if (dt.Rows.Count > 0)
                {
                    bool goodpar = (int)dt.Rows[0]["flagsMask"] == 0;
                    WaterMeters[i].WriteNewData((float)dt.Rows[0]["Value"] + WaterMeters[i].offsetPoint.RealOffSet, goodpar);
                    WaterMeters[i].Watermeter_SerialNumber.ToolTipText = "НОРМА";
                    WaterMeters[i].Watermeter_SerialNumber.Style.BackColor = Color.LightGray;
                }
                else
                {
                    WaterMeters[i].Watermeter_SerialNumber.ToolTipText = "НЕТ ДАННЫХ";
                    WaterMeters[i].Watermeter_SerialNumber.Style.BackColor = Color.Yellow;
                }
            }

            ReloadWaterMetersTimer.Enabled = true;
        }
    }
}
