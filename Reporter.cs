using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace WindowsFormsApplication1
{
    public class Reporter
    {
        public SQLhandler sqlHandler;

        public delegate void DInvokeIncrementProgressbar();
        public delegate void DSetMaxLevelProgressbar(int ComNumOfMeters);
        //public delegate void DMsgShow(string str);

        public DInvokeIncrementProgressbar invokeIncrementProgressbar;
        public DSetMaxLevelProgressbar setMaxLevelProgressbar;
        //public DMsgShow msgShow;

        //Опциональный путь сохранения файла
        public string customSavePath;
        public bool openReportAfter = true;

        public DataRow ElectMeter_GiveSqlDataRow(int addr, DateTime dt_start, DateTime dt_end, bool forMonth = false, bool alldaysum = false)
        {
            string formatstr = $"call sum_energy({addr}, '{dt_start:yyyy-MM-dd}', '{dt_end:yyyy-MM-dd}', {forMonth}  , {alldaysum})";
            try
            {
                DataTable table = sqlHandler.ReadSqlTable(formatstr);
                if (table.Rows.Count > 0)
                {
                    return table.Rows[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public string giveAttrStr(XmlNode node, string nameatr)
        {
            var attribute = node.Attributes[nameatr];
            if (attribute != null)
                return attribute.Value;
            return null;
        }

        public void ReadElectGrid(DateTime dateTime_start, DateTime dateTime_end, XmlNodeList nodelist, StringBuilder strBuilder, bool forMonth, bool alldaysum)
        {

            if (nodelist.Count > 0)
            {
                foreach (XmlNode node in nodelist)
                {
                    strBuilder.Append("{obj:");
                    if (node.Name == "group")
                    {

                        string name = node.Attributes["name"].Value;
                        strBuilder.AppendFormat($"new ElGroup(\"{name}\"),\n");
                    }
                    else if (node.Name == "meter")
                    {
                        // здесь нужно получить показания электроэнергии

                        string addr = giveAttrStr(node, "addr") ?? "--";
                        string id = giveAttrStr(node, "id") ?? "--";
                        string zone = giveAttrStr(node, "zone");
                        string adminGroup = giveAttrStr(node, "idgroup");

                        double k_current = Double.TryParse(giveAttrStr(node, "k_current"), out k_current) ? k_current : 1;

                        // startData, endData, consumed, replacings, countOfDay
                        string template = "(function(){" +
                           $" var elMet = new ElectricMeter({addr}, {id}, \"{zone} \", \"{adminGroup}\", {k_current});\n";

                        if (int.TryParse(addr, out int i_addr))
                        {

                            DataRow row = ElectMeter_GiveSqlDataRow(i_addr, dateTime_start, dateTime_end, forMonth, alldaysum);

                            template += row == null ?

                           "    elMet.consumedList = [null];\n" +
                           "    elMet.startData = null;\n" +
                           "    elMet.endData = null;\n"
                           :
                           $"    elMet.consumedList = [{row["consumed"]}];\n" +
                           $"    elMet.startData = {row["startData"]};\n" +
                           $"    elMet.endData = {row["endData"]};\n";
                        }
                        strBuilder.Append(template + "    return elMet;})(),\n");

                        if (invokeIncrementProgressbar != null)
                        {
                            invokeIncrementProgressbar();
                        }
                    }

                    strBuilder.Append("    children:[");

                    if (node.ChildNodes.Count > 0)
                    {

                        ReadElectGrid(dateTime_start, dateTime_end, node.ChildNodes, strBuilder, forMonth, alldaysum);
                    }

                    strBuilder.Append("],\n" +
                                      "consumed : consumed_function, residual : residual_function },");
                }
            }
        }

        public void ReadElectricBase(DateTime dateTime_start, DateTime dateTime_end, bool forMonth = false, bool alldaysum = false)
        {

            string jsTamplateAdminGroup = "adminGroupsArr.push(new AdminGroup(\"$name\", \"$idgroup\"));\n";

            //DateTime date = new DateTime((int)Year_numericUpDown1.Value, month_comboBox.SelectedIndex + 1, 1);
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("Meter_conf.xml");
            XmlNodeList Nodes = xmlDocument.SelectNodes("//DataBaseSQL");
            if (Nodes.Count > 0)
            {
                sqlHandler = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
            }
            else
            {
                Console.WriteLine("Отсутствуют данные к подключению к базе!");
                return;
            }
            var admingroups_nodes = xmlDocument.SelectNodes(@"//adgroup");
            int ComNumOfMeters = xmlDocument.SelectNodes("//meter").Count;

            if (setMaxLevelProgressbar != null)
            {
                setMaxLevelProgressbar(ComNumOfMeters);
            }

            DateTime dt_start = dateTime_start;
            DateTime dt_end = dateTime_end;


            // Хранилище выходного файла
            StringBuilder strbu = new StringBuilder();

            if (ComNumOfMeters > 0)
            {
                //string jsString = "";
                // Генерация списков с административными зонами
                foreach (XmlNode admingroup in admingroups_nodes /*xmlDocument.SelectNodes("watermeters/admin_groups/adgroup")*/)
                {
                    strbu.Append(jsTamplateAdminGroup
                        .Replace("$name", admingroup.Attributes["name"].Value)
                        .Replace("$idgroup", admingroup.Attributes["idgroup"].Value)
                        );
                }
                strbu.Append(";electricGrid =");
                ReadElectGrid(dateTime_start, dateTime_end, xmlDocument.SelectNodes("/Meters/group"), strbu, forMonth, alldaysum);

                // Удалить последнюю запятую
                strbu.Remove(strbu.Length - 1, 1);
            }


            string rangeStr = $"c {dateTime_start.ToString("dd.MM.yyyy")} по {dateTime_end.ToString("dd.MM.yyyy")}";

            string tamplateStr = System.IO.File.ReadAllText(@"tamplates\electric_tamplate.report");
            tamplateStr = tamplateStr
                .Replace("$comment", String.Format("Показания за период {0} <br>Общее количество счетчиков {1}", rangeStr, ComNumOfMeters))
                .Replace("$arhive", strbu.ToString());

            string nameFile = customSavePath ?? @"Reports\" + String.Format("Электросчетчики_за_Период_{0}_", rangeStr) +
                                            String.Format("-{0}_{1}_{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) + ".html";
            System.IO.File.WriteAllText(nameFile, tamplateStr);
            if (openReportAfter)
            {
                System.Diagnostics.Process.Start(nameFile);
            }
        }

        public struct WaterMeterConsumption //отображает водопотребление конкретного счетчика.
        {
            public double ConsumptionStartPoint;
            public double ConsumptionEndPoint;
            public double Difference;
            //public string ChID;
            public string meterId;
            public string waterType;
            public string waterConsumer;
            public WaterMeterOffsetPoint watermeterOffset;

            public WaterMeterConsumption(string id, string type, string consumer = "")
            {
                watermeterOffset = new WaterMeterOffsetPoint();
                meterId = id;
                waterType = type;
                waterConsumer = consumer;
                ConsumptionStartPoint = 0;
                ConsumptionEndPoint = 0;
                Difference = -1;
                //ChID = "";
            }
        }

        private string TableWatermetersData;
        private string TableWatermetersNameLink;


        private double CalculateWaterConsumption(string name, DateTime dt_start, DateTime dt_end)
        {
            /*
             * Находим индекс начальной точки. Она должна быть перед отрезком (т.к. импульса еще не было)
             * Зная индекс начала строим sql запрос , чтобы найти все данные по отрезку
             * Сканируем весь отрезок на предмет обнуления. Учитываем в сумме обнуления
             * 
             * @"select mdate2.* from float_watermeter_table_test mdate2,
        (select mdate.* from meternamelink mlink,float_watermeter_table_test mdate
        where locate('55144584', mlink.NameMeter) > 0
        and mlink.ChID = mdate.chID and mdate.flagsMask = 0
        and mdate.oleDT < 42743.7560452199  order by Id desc limit 1 ) mdate3
        where mdate2.chID = mdate3.chID and mdate2.Id  >= mdate3.Id and mdate2.oleDT < 42800";
             */

            double dbl_out = 0;

            string sqlselect = $"select mdate2.* from {TableWatermetersData} mdate2, " +
            $"(select mdate.* from {TableWatermetersNameLink} mlink, {TableWatermetersData} mdate " +
            $"where locate({name}, mlink.NameMeter) > 0 "+
            "and mlink.ChID = mdate.chID and mdate.flagsMask = 0 " +
            $"and mdate.oleDT <= {dt_start.ToOADate().ToString(CultureInfo.InvariantCulture)} order by Id desc limit 1 ) mdate3 " +
            $"where mdate2.chID = mdate3.chID and mdate2.Id  >= mdate3.Id and mdate2.oleDT < {dt_end.ToOADate().ToString(CultureInfo.InvariantCulture)} " +
            "and mdate2.flagsMask = 0"
                //.Replace("$tb_data", TableWatermetersData)
                //.Replace("$tb_links", TableWatermetersNameLink)
                //.Replace("$name", name)
                //.Replace("$DT_START", dt_start.ToOADate().ToString())
                //.Replace("$DT_END", dt_end.ToOADate().ToString())
            ;
            DataTable dt = new DataTable();
            try
            {
                dt = sqlHandler.ReadSqlTable(sqlselect);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка чтения к базе {0}", e.Message);

            }
            if (dt.Rows.Count > 0)
            {
                //dbl_start = Convert.ToDouble(dt.Rows[0]["Value"]);
                //using (StreamWriter sw = new StreamWriter("logWaterMeter.txt", false, System.Text.Encoding.Default))
                //{
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if ((float)dt.Rows[i]["Value"] > (float)dt.Rows[i - 1]["Value"])
                    {
                        dbl_out += Convert.ToDouble(dt.Rows[i]["Value"]) - Convert.ToDouble(dt.Rows[i - 1]["Value"]);
                    }

                    // sw.WriteLine("index {0}:: start: {2} - end : {3} == diff:{1}", i, dbl_out, dt.Rows[i]["Value"], (float)dt.Rows[i - 1]["Value"]);
                }
                //}
            }
            return dbl_out;
        }

        private WaterMeterConsumption GiveWaterData(XmlNode watermeter, DateTime dt_start, DateTime dt_end)
        {
            /*
             * Найти сдвиг для конкретной точки
             * Найти потребление с учетом сдвига в конечной точке
             * Расчитав относительное потребление за период 
             * получить начальную точку
             */
            #region Функция записи данных
            Func<XmlNode, string, string> writeValue =
                (nodes, atr) =>
                {
                    //if (nodes.Count == 0) 
                    //    return;
                    try
                    {
                        return nodes.Attributes[atr].Value;
                    }
                    catch (ArgumentException)
                    {
                        return "#error#";

                    }
                    catch (NullReferenceException)
                    {
                        return "н/д";

                    }
                };
            #endregion

            WaterMeterConsumption watermeter_struct = new WaterMeterConsumption();

            watermeter_struct.meterId = watermeter.Attributes["snumber"].Value;

            watermeter_struct.waterType = writeValue(watermeter, "function");
            watermeter_struct.waterConsumer = writeValue(watermeter.ParentNode, "consumer");

            watermeter_struct.Difference = CalculateWaterConsumption(watermeter_struct.meterId, dt_start, dt_end);
            double aftercheckpoint = 0;
            // Подчитаываем сдвиг показаний
            if (watermeter.ChildNodes != null)
            {
                foreach (XmlNode nd in watermeter.ChildNodes)
                {
                    if (nd.Name == "point")
                    {
                        try
                        {
                            watermeter_struct.watermeterOffset.SetTimePointFromStrings(nd.Attributes["time"].Value, nd.Attributes["data"].Value);
                        }
                        catch (ArgumentException)
                        {

                        }
                    }
                }
                string sqlselect = "SELECT mdate.* FROM " + TableWatermetersNameLink
                        + " mlink, " + TableWatermetersData + " mdate where locate('" + watermeter_struct.meterId.ToString() + "', mlink.NameMeter) > 0 and mlink.ChID = mdate.chID and mdate.flagsMask = 0 and mdate.oleDT < " +
                            watermeter_struct.watermeterOffset.TimePoint.ToOADate().ToString(CultureInfo.InvariantCulture) + " order by Id desc limit 1";
                DataTable dt = new DataTable();
                try
                {
                    dt = sqlHandler.ReadSqlTable(sqlselect);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка чтения к базе {0}", e.Message);

                }
                if (dt.Rows.Count > 0)
                {
                    watermeter_struct.watermeterOffset.OffSetFromBase = (float)dt.Rows[0]["Value"];
                }
                // ищем приращение объема , начиная от контрольной точки сдвига
                aftercheckpoint = CalculateWaterConsumption(watermeter_struct.meterId, watermeter_struct.watermeterOffset.TimePoint, dt_end);
                watermeter_struct.ConsumptionEndPoint = aftercheckpoint + watermeter_struct.watermeterOffset.Offset;
                watermeter_struct.ConsumptionStartPoint = watermeter_struct.ConsumptionEndPoint - watermeter_struct.Difference;

            }
            return watermeter_struct;
        }

        public void ReadWaterBase(DateTime dateTime_start, DateTime dateTime_end)
        {
            var dt_start_local = new DateTime(dateTime_start.Year, dateTime_start.Month, dateTime_start.Day);
            var dt_end_local = new DateTime(dateTime_end.Year, dateTime_end.Month, dateTime_end.Day);

            string jsTamplateAdminGroup = "adminGroupsArr.push(new AdminGroup(\"$name\", \"$idgroup\"));\n";
            string jsTamplateWaterNode = "arr.push(new WaterNode(\"$nameid\", \"$info\", \"$idgroup\"));\n";
            string jsTamplateWatermeter = "arr[arr.length - 1].waterMeters.push(new Watermeter(\"$id\", \"$type\", $startdata, $enddata));\n";
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("waterMeter_conf.xml");
            XmlNodeList Nodes = xmlDocument.SelectNodes("//DataBaseSQL");
            if (Nodes.Count > 0)
            {
                sqlHandler = new SQLhandler(Nodes[0].Attributes["Database"].Value, Nodes[0].Attributes["DataSource"].Value, Nodes[0].Attributes["UserId"].Value, Nodes[0].Attributes["Password"].Value);
                TableWatermetersData = Nodes[0].Attributes["tabledata"].Value;
                TableWatermetersNameLink = Nodes[0].Attributes["tableLinkName"].Value;
            }
            else
            {
                Console.WriteLine("Отсутствуют данные к подключению к базе!");
                return;
            }
            // получаем группы водомерных узлов
            XmlNodeList watermeterunits = xmlDocument.SelectNodes("watermeters/node");
            int ComNumOfMeters = xmlDocument.SelectNodes("//watermeter").Count;

            if (setMaxLevelProgressbar != null)
            {
                setMaxLevelProgressbar(ComNumOfMeters);
            }

            if (watermeterunits.Count > 0)
            {
                string jsString = "";

                foreach (XmlNode admingroup in xmlDocument.SelectNodes("watermeters/admin_groups/adgroup"))
                {
                    jsString += jsTamplateAdminGroup
                        .Replace("$name", admingroup.Attributes["name"].Value)
                        .Replace("$idgroup", admingroup.Attributes["idgroup"].Value);
                }

                foreach (XmlNode metersunit in watermeterunits)
                {
                    jsString += jsTamplateWaterNode
                        .Replace("$nameid", metersunit.Attributes["name"].Value)
                        .Replace("$info", metersunit.Attributes["consumer"].Value)
                        .Replace("$idgroup", metersunit.Attributes["idgroup"].Value);
                    foreach (XmlNode watermeter in metersunit.ChildNodes)
                    {
                        WaterMeterConsumption localmeter = GiveWaterData(watermeter, dt_start_local, dt_end_local);
                        jsString += jsTamplateWatermeter
                            .Replace("$id", localmeter.meterId)
                            .Replace("$type", localmeter.waterType)
                            .Replace("$startdata", localmeter.ConsumptionStartPoint.ToString(CultureInfo.InvariantCulture))
                            .Replace("$enddata", localmeter.ConsumptionEndPoint.ToString(CultureInfo.InvariantCulture));

                        if (invokeIncrementProgressbar != null)
                        {
                            invokeIncrementProgressbar();
                        }

                    }
                }
                string rangeStr = String.Format("c {0} по {1}", dateTime_start.ToString("dd.MM.yyyy"), dateTime_end.ToString("dd.MM.yyyy"));

                string tamplateStr = System.IO.File.ReadAllText(@"tamplates\WaterTamplate.report");
                tamplateStr = tamplateStr
                    .Replace("$comment", String.Format("Показания за период {0} <br>Общее количество счетчиков {1}", rangeStr, ComNumOfMeters.ToString()))
                    .Replace("$jscode", jsString);
                
                string nameFile = customSavePath ?? @"Reports\" + String.Format("Водосчетчики_за_Период_{0}_", rangeStr) +
                                                String.Format("-{0}_{1}_{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) + ".html";
                
                System.IO.File.WriteAllText(nameFile, tamplateStr);
                if (openReportAfter)
                {
                    System.Diagnostics.Process.Start(nameFile);
                }
            }

        }
    }
    
}
