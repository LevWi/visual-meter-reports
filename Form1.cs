using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net.Sockets;
using System.Globalization;
using System.Timers;
using System.IO;
using System.Threading.Tasks;
using MercuryMeter;
using System.Runtime.Serialization.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SuspendLayout();
            #region Настройка Таблицы для счетчиков

            this.Status      =  new System.Windows.Forms.DataGridViewTextBoxColumn() ; 
            this.ADDR        =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.ID          =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.admin_zone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.power       =  new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cur_phase1  =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.cur_phase2  =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.cur_phase3  =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.volt_phase1 =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.volt_phase2 =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            this.volt_phase3 =  new System.Windows.Forms.DataGridViewTextBoxColumn() ;
            // 
            // dataGridView_meters
            // 
            //this.dataGridView_meters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_meters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.ADDR,
            this.ID,
            this.admin_zone,
            this.zone,
            this.power,
            this.cur_phase1,
            this.cur_phase2,
            this.cur_phase3,
            this.volt_phase1,
            this.volt_phase2,
            this.volt_phase3,
            });
            //this.dataGridView_meters.Location = new System.Drawing.Point(25, 424);
            //this.dataGridView_meters.Name = "dataGridView_meters";
            //this.dataGridView_meters.Size = new System.Drawing.Size(837, 217);
            //this.dataGridView_meters.TabIndex = 5;
            // 
            // Status
            // 
            this.Status.HeaderText = "Статус";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // ADDR
            // 
            this.ADDR.HeaderText = "Адрес";
            this.ADDR.Name = "ADDR";
            this.ADDR.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "Серийный номер";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Административная зона
            // 
            this.admin_zone.HeaderText = "Административная зона";
            this.admin_zone.Name = "admin_zone";
            this.admin_zone.ReadOnly = true;
            // 
            // zone  - Зона охвата электросчетчиков
            // 
            this.zone.HeaderText = "Зона охвата";
            this.zone.Name = "zone";
            this.zone.ReadOnly = true;
            this.zone.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // 
            // power
            // 
            this.power.HeaderText = "Мощность активная (Вт)";
            this.power.Name = "ID";
            this.power.ReadOnly = true;
            // 
            // cur_phase1
            // 
            this.cur_phase1.HeaderText = "Ток 1я фаза";
            this.cur_phase1.Name = "cur_phase1";
            this.cur_phase1.ReadOnly = true;
            // 
            // cur_phase2
            // 
            this.cur_phase2.HeaderText = "Ток 2я фаза";
            this.cur_phase2.Name = "cur_phase2";
            this.cur_phase2.ReadOnly = true;
            // 
            // cur_phase2
            // 
            this.cur_phase3.HeaderText = "Ток 3я фаза";
            this.cur_phase3.Name = "cur_phase3";
            this.cur_phase3.ReadOnly = true;
            // 
            // volt_phase1
            // 
            this.volt_phase1.HeaderText = "Напряжение 1я фаза";
            this.volt_phase1.Name = "volt_phase1";
            this.volt_phase1.ReadOnly = true;
            // 
            // volt_phase2
            // 
            this.volt_phase2.HeaderText = "Напряжение 2я фаза";
            this.volt_phase2.Name = "volt_phase2";
            this.volt_phase2.ReadOnly = true;
            // 
            // volt_phase2
            // 
            this.volt_phase3.HeaderText = "Напряжение 3я фаза";
            this.volt_phase3.Name = "volt_phase3";
            this.volt_phase3.ReadOnly = true;

            #endregion

            #region Настройка таблицы для водосчетчиков
            //this.dg_water_WaterStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_NameNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_Watermeter_SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_LocationNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_Consumer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_Function = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_water_Data = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.dataGridView_watermeter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_watermeter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
             //this.dg_water_WaterStatus                ,
             this.dg_water_NameNode                   ,
             this.dg_water_Watermeter_SerialNumber    ,
             this.dg_water_LocationNode               ,
             this.dg_water_Consumer                   ,
             this.dg_water_Function                   ,
             this.dg_water_Data                       
            });

            //this.dg_water_WaterStatus.HeaderText = "Статус";
            //this.dg_water_WaterStatus.Name = "status";
            //this.dg_water_WaterStatus.ReadOnly = true;

            this.dg_water_NameNode.HeaderText = "Водомерный узел";
            this.dg_water_NameNode.Name = "namenode";
            this.dg_water_NameNode.ReadOnly = true;

            this.dg_water_Watermeter_SerialNumber.HeaderText = "Серийный номер";
            this.dg_water_Watermeter_SerialNumber.Name = "serial";
            this.dg_water_Watermeter_SerialNumber.ReadOnly = true;
            
            this.dg_water_LocationNode.HeaderText = "Расположение";
            this.dg_water_LocationNode.Name = "locationNode";
            this.dg_water_LocationNode.ReadOnly = true;

            this.dg_water_Consumer.HeaderText = "Потребитель";
            this.dg_water_Consumer.Name = "consumer";
            this.dg_water_Consumer.ReadOnly = true;

            this.dg_water_Function.HeaderText = "Назначение";
            this.dg_water_Function.Name = "function";
            this.dg_water_Function.ReadOnly = true;

            this.dg_water_Data.HeaderText = "Показания";
            this.dg_water_Data.Name = "data";
            this.dg_water_Data.ReadOnly = true;

            #endregion
            this.ResumeLayout(false);
        }
        TextWriter _writer = null;
        public TCP_reader TCP_reader_;
        public Authorization authorization;

        public ElectricMeterWinForm_forTable[] meter_elem_arr;
        XmlDocument xmlDocument;

     
        private void LoadTreeView()
        {
            treeView_meters.ImageList = MeterImageList;
            treeView_meters.Nodes.Clear();
            XmlNodeList treenodes = xmlDocument.SelectNodes(@"Meters/group");
            foreach (TreeNode node in LoadTreeView(treenodes).Nodes)
                        treeView_meters.Nodes.Add(node);
            treeView_meters.NodeMouseDoubleClick += OnReloadMeterTable;


        }

        public void OnReloadMeterTable(object sender, TreeNodeMouseClickEventArgs e)
        {

            List<TreeNode> nd = new List<TreeNode>();
            if (e.Node.Name == "meter")
                nd.Add(e.Node);
            if (e.Node.Nodes.Count > 0)
            {
                nd.AddRange(giveListOfnode(e.Node.Nodes));
            }
            var meters = from node in nd
                        from meter in meter_elem_arr
                        where node.Text == meter.meter_id.ToString()
                        select meter;

            if (meters.Count() > 0)
            {
                //перестраиваем таблицу
                DataGridViewRow row;
                this.dataGridView_meters.SuspendLayout();
                this.dataGridView_meters.Rows.Clear();
                foreach (var meter in meters)
                {
                    row = new DataGridViewRow();
                    meter.Status_dgvCell.OwningRow.Cells.Clear();
                    row.Cells.AddRange(
                        meter.Status_dgvCell,
                        meter.ADDR_dgvCell,
                        meter.ID_dgvCell,
                        meter.admin_zone_dgvCell,
                        meter.zone_dgvCell,
                        meter.power_dgvCell,
                        meter.cur_phase1_dgvCell,
                        meter.cur_phase2_dgvCell,
                        meter.cur_phase3_dgvCell,
                        meter.volt_phase1_dgvCell,
                        meter.volt_phase2_dgvCell,
                        meter.volt_phase3_dgvCell
                    );
                    this.dataGridView_meters.Rows.Add(row);
                    row.ContextMenuStrip = contextMenuStrip_meters;
                }
                this.dataGridView_meters.Refresh();
                this.dataGridView_meters.ResumeLayout();

                //this.dataGridView_meters.AutoResizeColumns();
            
            }
        }

        public List<TreeNode> giveListOfnode(TreeNodeCollection nodes)
        {
            List<TreeNode> nd = new List<TreeNode>();

            foreach (TreeNode elem in nodes)
            {
                if (elem.Nodes.Count > 0)
                {
                    if (elem.Name == "meter")
                        nd.Add(elem);

                    nd.AddRange(giveListOfnode(elem.Nodes));
                }
                else if (elem.Name == "meter")
                {
                    nd.Add(elem);
                }
            }
            return nd;
        }

        private TreeNode LoadTreeView(XmlNodeList Xmlnodes)
        { 
            int n = Xmlnodes.Count;
            if (n == 0)
            {
                return null;
            }
            TreeNode newTreeNode = new TreeNode();
            int newTreeNodeIndex = 0;
            for (int i = 0; i < n; i++)
            {
                
                if (Xmlnodes[i].Name == "group"){
                    newTreeNode.Nodes.Add(LoadTreeView(Xmlnodes[i].ChildNodes));
                    newTreeNode.Nodes[newTreeNodeIndex].Text = Xmlnodes[i].Attributes["name"].Value;
                    newTreeNode.Nodes[newTreeNodeIndex].ImageIndex = 0;
                    newTreeNodeIndex++;
                }
                else if (Xmlnodes[i].Name == "meter")
                {
                    string id = Xmlnodes[i].Attributes["id"].Value;
                    string addr = Xmlnodes[i].Attributes["addr"].Value;
                    for (int j = 0; j < meter_elem_arr.Count(); j++)
                    {
                        if ((meter_elem_arr[j].meter_id == uint.Parse(id)) && (meter_elem_arr[j].meter_address == uint.Parse(addr)))
                        {
                            newTreeNode.Nodes.Add(meter_elem_arr[j].treeNode);
                            newTreeNodeIndex++;
                            if (Xmlnodes[i].ChildNodes.Count > 0)
                            {
                                foreach(TreeNode node in LoadTreeView(Xmlnodes[i].ChildNodes).Nodes)
                                            newTreeNode.LastNode.Nodes.Add(node);
                            }
                            break;
                        }
                    }
                }
                
            }
            return newTreeNode;
        }
 

        private void LoadElectricMeter()
        {

            DataGridViewRow row;
            int i = xmlDocument.SelectNodes("//meter[@type = 'Merc234']").Count;
            meter_elem_arr = new ElectricMeterWinForm_forTable[i];
            i = -1;
            foreach (XmlNode device in xmlDocument.SelectNodes("//meter[@type = 'Merc234']"))
            {

                        i++;
                        //meterTable.Rows.Add("Меркурий 234", device.Attributes["addr"].Value, "");
                        byte addr = Convert.ToByte(device.Attributes["addr"].Value);
                        uint serialnumber = Convert.ToUInt32(device.Attributes["id"].Value);
                        meter_elem_arr[i] = new ElectricMeterWinForm_forTable(addr, serialnumber);
                        meter_elem_arr[i].zone_dgvCell.Value = " ";
                        try
                        {
                            meter_elem_arr[i].zone_dgvCell.Value = device.Attributes["zone"].Value;
                        }
                        catch { }
                        try
                        {
                            string adminzoneCode = device.Attributes["idgroup"].Value;
                            XmlNodeList nodes = xmlDocument.SelectNodes("//adgroup[@idgroup ='"+ adminzoneCode +"']");
                            if (nodes.Count > 0)
                            {
                                meter_elem_arr[i].admin_zone_dgvCell.Value = nodes[0].Attributes["name"].Value;
                            }
                            else
                            {
                                meter_elem_arr[i].admin_zone_dgvCell.Value = " ";
                            }
                        }
                        catch { }

                        row = new DataGridViewRow();
                        row.Cells.AddRange(
                            meter_elem_arr[i].Status_dgvCell,
                            meter_elem_arr[i].ADDR_dgvCell,
                            meter_elem_arr[i].ID_dgvCell,
                            meter_elem_arr[i].admin_zone_dgvCell,
                            meter_elem_arr[i].zone_dgvCell,
                            meter_elem_arr[i].power_dgvCell,
                            meter_elem_arr[i].cur_phase1_dgvCell,
                            meter_elem_arr[i].cur_phase2_dgvCell,
                            meter_elem_arr[i].cur_phase3_dgvCell,
                            meter_elem_arr[i].volt_phase1_dgvCell,
                            meter_elem_arr[i].volt_phase2_dgvCell,
                            meter_elem_arr[i].volt_phase3_dgvCell
                        );
                        this.dataGridView_meters.Rows.Add(row);
                        row.ContextMenuStrip = contextMenuStrip_meters;
            }
            
            XmlNodeList xmlnode = xmlDocument.SelectNodes("//MeterServer");
            if (xmlnode.Count > 0)
            {
                try
                {
                    TCP_reader_ = new TCP_reader(xmlnode[0].Attributes["ipaddr"].Value, int.Parse(xmlnode[0].Attributes["port"].Value), meter_elem_arr);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    StartReadElectMeterToolStripMenuItem.Enabled = false;
                    return;
                }
                Console.WriteLine("Общее количество счетчиков: {0}", meter_elem_arr.Length);
            }
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            authorization = new Authorization();
            _writer = new ConsoleRedirection.TextBoxStreamWriter(Console_textBox);
            Console.SetOut(_writer);
            xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load("Meter_conf.xml");
            }
            catch(Exception exc)
            {
                MessageBox.Show(  exc.Message , "Ошибка загрузки кофигурации");
                Application.Exit();
                return;
            }
            LoadElectricMeter();
            LoadWaterMeters();
            LoadTreeView();
            dataGridView_meters.MouseDown += new MouseEventHandler(dataGridView_meters_MouseDown);
            contextMenuStrip_meters.Click += new EventHandler(contextMenuStrip_meters_Click);
        }

        void contextMenuStrip_meters_Click(object sender, EventArgs e)
        {
            var Form_EditMeter = new EditMeterForm(meter_elem_arr, authorization, xmlDocument);
            Form_EditMeter.Show();
        }

        void dataGridView_meters_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridView_meters.HitTest(e.X, e.Y);
                dataGridView_meters.ClearSelection();
                dataGridView_meters.Rows[hti.RowIndex].Selected = true;
            }
        }
        
        
        private void OnClickStartWaterMeterRead(object sender, EventArgs e)
        {
            ReloadWaterMetersTimer.Enabled = !ReloadWaterMetersTimer.Enabled;
        }

        public System.Timers.Timer colorTreeViewRefreshTimer;

        private void StartReadElectMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Console.WriteLine("3.2.1...0");
            TCP_reader_.Start();
            colorTreeViewRefreshTimer = new System.Timers.Timer(1000);
            //colorTreeViewRefreshTimer.AutoReset = true;
            colorTreeViewRefreshTimer.Elapsed += OnTimerEventcolorTreeViewRefreshTimer;
            colorTreeViewRefreshTimer.Enabled = true;
           // Console.WriteLine("...0");
        }



        public Color RefreshTreeViewColors(TreeNodeCollection nodes)
        {
            AlarmColors alarmcolors = new AlarmColors();

            Color defColor = Color.Transparent;
            if (nodes.Count > 0){
                foreach(TreeNode elem in nodes){
                    if (elem.Nodes.Count > 0)
                    {
                        elem.BackColor = RefreshTreeViewColors(elem.Nodes);
                    }
                    //int maxcode = nodes.Cast<TreeNode>().Max(x => alarmcolors.giveAlarmCode(x.BackColor));

                    
                }
                var query = (from el in nodes.Cast<TreeNode>()
                            where alarmcolors.giveAlarmCode(el.BackColor) >= 0 
                            select el);
                if (query.Count() > 0)
                {
                    int max = query.Max(x => alarmcolors.giveAlarmCode(x.BackColor));
                    return alarmcolors.giveColorfromCode(max);

                }
            }
            return defColor;
            
        }

        public void OnTimerEventcolorTreeViewRefreshTimer(Object source, ElapsedEventArgs e){

            // обрабатываем цвет узла родителя исходя из цветов потомков
            RefreshTreeViewColors(treeView_meters.Nodes);
            colorTreeViewRefreshTimer.Enabled = true;
        }

        
        private void ElectroReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeterReportForm elctReportForm = new MeterReportForm();

            elctReportForm.ShowDialog();
        }

        
        private void LogInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Authorization_form autoriz_form = new Authorization_form(authorization);
            autoriz_form.ShowDialog();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorization.LogOut();
        }

        private void ChangeLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    public class AlarmColors
    {
        public class AlarmColorCode
        {
            public Color Color;
            public int code = -1;

            public AlarmColorCode(Color cl, int cd)
            {
                Color = cl;
                code = cd;
            }
        }
        public AlarmColorCode GoodData = new AlarmColorCode(Color.White, 0);
        public AlarmColorCode BadData = new AlarmColorCode(Color.Yellow, 1);
        public AlarmColorCode Overload = new AlarmColorCode(Color.Orange, 100);

        public int giveAlarmCode(Color cl)
        {
            List<AlarmColorCode> query = (new List<AlarmColorCode>() {
                                                          GoodData,
                                                          BadData,
                                                          Overload
                }).Where(elem => elem.Color == cl).ToList();
            //IEnumerable<AlarmColorCode> query =  AlarmColorCodeList.Where(list => list.color == cl);
            if (query.Count() > 0)
            {
                return query[0].code;
            }
            return -1;
        }
        public Color giveColorfromCode(int cd)
        {
            List<AlarmColorCode> query = (new List<AlarmColorCode>() {
                                                          GoodData,
                                                          BadData,
                                                          Overload
                }).Where(elem => elem.code == cd).ToList();
            if (query.Count() > 0)
            {
                return query[0].Color;
            }
            return Color.Transparent;
        }
    }

    public class meterArgs : EventArgs
    {
        public uint meter_address {  set; get; }
        public uint meter_id {  set; get; }
    }

    public class MetersParameterDataGridBoxCell : DataGridViewTextBoxCell
    {
        private string format = "0.0";

        // Формат для вывода вида "0.0" , 0 и тд
        public string FormatOutput
        {
            set { format = value;}
            get { return format; }
        }

        public MetersParameter Parametr;
        public MetersParameterDataGridBoxCell() 
        {

            Parametr = new MetersParameter();
            Parametr.UseScaleForInput = true;
            Parametr.ParametrUpdated += InputFormater;
        }
        // задает 
        public void InputFormater()
        {
            try
            {
                this.Value = Parametr.Value.ToString(FormatOutput);
                this.ToolTipText = String.Format(Parametr.ComAlarm ? "Недопустимое значение!\nВерхний граница {0}\nНижняя граница {1} " : "Верхний граница {0}\nНижняя граница {1} ", 
                                                 Parametr.MaxValue, Parametr.MinValue);
                if (Parametr.MaxValueAlarm)
                {
                    this.Style.BackColor = Color.Orange;
                }
                else if (Parametr.MinValueAlarm)
                {
                    this.Style.BackColor = Color.Yellow;
                }
                else
                {
                    this.Style.BackColor = Color.LightGray;
                }
            }
            catch (FormatException)
            {
                this.Value = "##ErrorFormat##";
                this.Style.BackColor = Color.Yellow;
            }
        }

    }
    public class ElectricMeterWinForm_forTable 
    {

        
        //создание ячеек 
        public DataGridViewTextBoxCell Status_dgvCell = new DataGridViewTextBoxCell();
        public DataGridViewTextBoxCell ADDR_dgvCell = new DataGridViewTextBoxCell();
        public DataGridViewTextBoxCell ID_dgvCell = new DataGridViewTextBoxCell();
        public DataGridViewTextBoxCell admin_zone_dgvCell = new DataGridViewTextBoxCell();
        public DataGridViewTextBoxCell zone_dgvCell = new DataGridViewTextBoxCell();
        public MetersParameterDataGridBoxCell power_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell cur_phase1_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell cur_phase2_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell cur_phase3_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell volt_phase1_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell volt_phase2_dgvCell = new MetersParameterDataGridBoxCell();
        public MetersParameterDataGridBoxCell volt_phase3_dgvCell = new MetersParameterDataGridBoxCell();
        public TreeNode treeNode = new TreeNode();

        public DateTime DateTime_lastTime_connection { set; get; }
        private MercuryMeter.Mercury230.error_type local_error;
        public MercuryMeter.Mercury230.error_type error_meter {
            get
            {
                return local_error;
            }
            set
            {
                local_error = value;
                AlarmColors alarmcolors = new AlarmColors();
                switch (local_error)
                {
                    case MeterDevice.error_type.none :
                        Status_dgvCell.Value = "В РАБОТЕ";
                        Status_dgvCell.ToolTipText = String.Format("Ответы электросчетчика корректны\nПоледний опрос: {0}", DateTime_lastTime_connection);
                        Status_dgvCell.Style.BackColor = alarmcolors.GoodData.Color;
                        break;
                    case MeterDevice.error_type.AnswError:
                        Status_dgvCell.Value = "ОШИБКИ В ОТВЕТЕ";
                        Status_dgvCell.ToolTipText = String.Format("Счетчик ответил с ошибками\nПоледний опрос: {0}", DateTime_lastTime_connection);
                        Status_dgvCell.Style.BackColor = alarmcolors.BadData.Color;
                        break;
                    case MeterDevice.error_type.NoConnect:
                        Status_dgvCell.Value = "НЕТ ОТВЕТА";
                        Status_dgvCell.ToolTipText = String.Format("В послений опрос счетчик не ответил\nПоледний опрос: {0}", DateTime_lastTime_connection);
                        Status_dgvCell.Style.BackColor = alarmcolors.BadData.Color;
                        break;
                    case MeterDevice.error_type.WrongId:
                        Status_dgvCell.Value = "НЕВЕРНЫЙ СЕРИЙНЫЙ НОМЕР";
                        Status_dgvCell.ToolTipText = String.Format("В послений опрос счетчик дал неверный серийный номер\nПоледний опрос: {0}\n\nПроверьте конфигурацию системы\nВозможно, в системе присутствует конфликтующий счетчик", DateTime_lastTime_connection);
                        Status_dgvCell.Style.BackColor = alarmcolors.BadData.Color;
                        break;
                    case MeterDevice.error_type.NoAnsw:
                        Status_dgvCell.Value = "НЕПОЛНЫЙ ОТВЕТ";
                        Status_dgvCell.ToolTipText = String.Format("Счетчик ответил не полностью\nПоледний опрос: {0}\n\nВозможно, проблемы со связью", DateTime_lastTime_connection);
                        Status_dgvCell.Style.BackColor = alarmcolors.BadData.Color;
                        break;
                    default:
                        Status_dgvCell.Value = "???";
                        Status_dgvCell.ToolTipText = "Уточняется...";
                        break; 

                }
                List<MetersParameterDataGridBoxCell> parlist = new List<MetersParameterDataGridBoxCell>() {              
                                 cur_phase1_dgvCell ,
                                 cur_phase2_dgvCell   ,
                                 cur_phase3_dgvCell   ,
                                 volt_phase1_dgvCell  ,
                                 volt_phase2_dgvCell  ,
                                 volt_phase3_dgvCell
                };
                foreach( MetersParameterDataGridBoxCell elem in parlist){
                    if ((elem.Parametr.ComAlarm) && (local_error == MeterDevice.error_type.none))
                    {
                        treeNode.BackColor = alarmcolors.Overload.Color; ;
                        return;
                    }
                }
                treeNode.BackColor = this.Status_dgvCell.Style.BackColor;                  
                //отработка цветов родительских узлов

            }
        }

        public string status
        {
            set
            {
                this.Status_dgvCell.Value = value;
            }
        }

        //# region Свойства фазных токов
        //public float cur_phase1
        //{
        //    set
        //    {
        //        this.cur_phase1_dgvCell.Value = value.ToString() + " A";
        //    }
        //}
        //public float cur_phase2
        //{
        //    set
        //    {
        //        this.cur_phase2_dgvCell.Value = value.ToString() + " A";
        //    }
        //}
        //public float cur_phase3
        //{
        //    set
        //    {
        //        this.cur_phase3_dgvCell.Value = value.ToString() + " A";
        //    }
        //}
        //# endregion

        //# region Свойства фазных напряжений
        //public float voltage_phase1
        //{
        //    set
        //    {
        //        this.volt_phase1_dgvCell.Value = value.ToString() + " В";
        //    }
        //}
        //public float voltage_phase2
        //{
        //    set
        //    {
        //        this.volt_phase2_dgvCell.Value = value.ToString() + " В";
        //    }
        //}
        //public float voltage_phase3
        //{
        //    set
        //    {
        //        this.volt_phase3_dgvCell.Value = value.ToString() + " В";
        //    }
        //}
        //# endregion


        public event EventHandler<meterArgs> Call; //измени название
        //void ClassEvent(object sender, EventArgs arg)
        //{
        //    // meterArgs ar
        //    meterArgs MeterArgs_ = new meterArgs();
        //    MeterArgs_.meter_address = this.meter_address;
        //    MeterArgs_.meter_id = this.meter_id;
        //    Call(sender, MeterArgs_);
        //}

        public byte meter_address { private set; get; }
        public uint meter_id { private set; get; }
        //public int indexInTable { private set; get; }

        public ElectricMeterWinForm_forTable(byte addr, uint id )
        {
            meter_address = addr;
            meter_id = id;
            ADDR_dgvCell.Value = addr.ToString();
            ID_dgvCell.Value = id.ToString();
            treeNode.Text = id.ToString();
            treeNode.ImageIndex = 1;
            treeNode.Name = "meter";
        }
    }

    public class TCP_reader
    {
        public TCP_reader(string host, int port, ElectricMeterWinForm_forTable[] elem)
        {
            Host = host;
            Port = port;
            newClient = new TcpClient();
            RefreshTimer = new System.Timers.Timer(3000);
            RefreshTimer.Elapsed += this.OnTimedEvent;
            RefreshTimer.AutoReset = false;
            meters = elem;
            intervalRestartConnection = 7000;
            Console.WriteLine("Создание объекта ТСР закончено");
        }
        //public float String_To_Float(string str)
        //{
        //    float locVal;
        //    if (Single.TryParse(str, out locVal))
        //    {
        //        return locVal;
        //    }
        //    else
        //    {
        //        return -99;
        //    }
        //}
        public void Start()
        {
            RefreshTimer.Enabled = true;
        }
        public void Stop()
        {
            RefreshTimer.Enabled = false;
        }

        public string Host { set; get; }
        public int Port { set; get; }
        public string err { set; get; }
        private ElectricMeterWinForm_forTable[] meters;

        public double intervalRestartConnection {set ; get;}
        public double intervalrefreshData
        {
            set {
                RefreshTimer.Interval = value;
                }
            get
            {
                return RefreshTimer.Interval;
            }
        }

        #region 
        //public DateTime LastCon { set; get; }
        //public int timeout { set; get; }
        //public bool lostDev { set; get; }
        //string cur1_s { set; get; }
        //string cur2_s { set; get; }
        //string cur3_s { set; get; }
        //string volt1_s { set; get; }
        //string volt2_s { set; get; }
        //string volt3_s { set; get; }
        //public bool CommonAlarm { set; get; }
        //public float setCurrent { set; get; }
        //public float PowerLimit { set; get; }
        //public bool PowerAlarm { set; get; }
        //public float CommonPower { set; get; }
        //public bool Cur1_Alarm
        //{
        //    get
        //    {
        //        if (cur1 > setCurrent)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool Cur2_Alarm
        //{
        //    get
        //    {
        //        if (cur2 > setCurrent)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool Cur3_Alarm
        //{
        //    get
        //    {
        //        if (cur3 > setCurrent)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool Volt1_Alarm
        //{
        //    get
        //    {
        //        if ((volt1 < 198) || (volt1 > 242))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool Volt2_Alarm
        //{
        //    get
        //    {
        //        if ((volt2 < 198) || (volt2 > 242))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public bool Volt3_Alarm
        //{
        //    get
        //    {
        //        if ((volt3 < 198) || (volt3 > 242))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public float cur1
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(cur1_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public float cur2
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(cur2_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public float cur3
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(cur3_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public float volt1
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(volt1_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public float volt2
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(volt2_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //public float volt3
        //{
        //    get
        //    {
        //        float locVal;
        //        if (Single.TryParse(volt3_s, out locVal))
        //        {
        //            return locVal;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}
        #endregion

        TcpClient newClient;

        private System.Timers.Timer RefreshTimer;
        XmlDocument xmlDocument = new XmlDocument();

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Refresh_data_meters();
        }

        public void Refresh_data_meters()
        {
           // Console.WriteLine("Пуск события опроса");

            Action<MetersParameterDataGridBoxCell, MetersParameter> ParAssignment = (ParBoxCell, Par) =>
            {
                ParBoxCell.Parametr.Hist = Par.Hist;
                ParBoxCell.Parametr.ScalingFactor = Par.ScalingFactor;
                ParBoxCell.Parametr.MinValue = Par.MinValue;
                ParBoxCell.Parametr.MaxValue = Par.MaxValue;
                ParBoxCell.Parametr.Value = Par.Value;
                //ParBoxCell.Parametr.RefreshData();
            };
            RefreshTimer.Enabled = false;
            try
            {
                
                if (!newClient.Connected)
                {
                    Console.WriteLine("Соединение...");
                    // пошлём на установку соединения
                    if (newClient.Client != null)
                        newClient.Client.Close();
                    newClient = new TcpClient(Host, Port);
                    Console.WriteLine("Успешно");
                }
                Console.WriteLine(DateTime.Now.ToString() + "... Обновление данных");
                //newClient.ReceiveTimeout
                // порождает исключение, если
                // при соединении возникают проблемы
                NetworkStream tcpStream = newClient.GetStream();
                tcpStream.ReadTimeout = 200;
                //string str_badformat = "<Error>badformat</Error>";
                //string str_null = "<Error>null</Error>";
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Mercury230_DatabaseSignals));
                XmlDocument xmlquery = new XmlDocument();
                int count_normalMeter = 0;
                foreach (ElectricMeterWinForm_forTable elem in meters)
                {
                    byte[] sendBytes = Encoding.UTF8.GetBytes("<query><type>readmeter</type><model>Merc230</model><add_meter>" + elem.meter_address
                                                               + "</add_meter><id_meter>"
                                                               + elem.meter_id
                                                               + "</id_meter></query>");
                    if (tcpStream.DataAvailable)
                    {
                        var buffer = new byte[4096];
                        tcpStream.Read(buffer, 0, buffer.Length);
                    }
                    tcpStream.Write(sendBytes, 0, sendBytes.Length);

                    byte[] bytes = new byte[newClient.ReceiveBufferSize];
                    int bytesRead = tcpStream.Read(bytes, 0, newClient.ReceiveBufferSize);

                    // Строка, содержащая ответ от сервера
                    string answer = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                    try
                    {
                        xmlquery.LoadXml(answer);
                    }
                    catch (XmlException)
                    {
                        Console.WriteLine("Ошибочный ответ сервера");
                        System.Threading.Thread.Sleep(50);
                        continue;
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
                            elem.DateTime_lastTime_connection = meterLocal.DateTime_lastTime_connection;
                            elem.error_meter = meterLocal.error_meter;
                            if (elem.error_meter == MeterDevice.error_type.none)
                                                         count_normalMeter++;
                            if (meterLocal.serial_number.ToString() == elem.meter_id.ToString())
                            {
                                ParAssignment(elem.volt_phase1_dgvCell, meterLocal.Phases[0].voltage);
                                ParAssignment(elem.volt_phase2_dgvCell, meterLocal.Phases[1].voltage);
                                ParAssignment(elem.volt_phase3_dgvCell, meterLocal.Phases[2].voltage);
                                ParAssignment(elem.cur_phase1_dgvCell, meterLocal.Phases[0].current);
                                ParAssignment(elem.cur_phase2_dgvCell, meterLocal.Phases[1].current);
                                ParAssignment(elem.cur_phase3_dgvCell, meterLocal.Phases[2].current);
                                ParAssignment(elem.power_dgvCell, meterLocal.CommonActivePower);
                            }

                        }
                    }
                    //if (give_arg("add", answer) == elem.meter_address.ToString())
                    //{
                    //    string par;
                    //    // ток
                    //    par = give_arg("cur", answer);
                    //    elem.cur_phase1 = String_To_Float(give_elems("1", par));
                    //    elem.cur_phase2 = String_To_Float(give_elems("2", par));
                    //    elem.cur_phase3 = String_To_Float(give_elems("3", par));
                    //    par = give_arg("volt", answer);
                    //    elem.voltage_phase1 = String_To_Float(give_elems("1", par));
                    //    elem.voltage_phase2 = String_To_Float(give_elems("2", par));
                    //    elem.voltage_phase3 = String_To_Float(give_elems("3", par));
                    //CommonPower = (cur1 * volt1 + cur2 * volt2 + cur3 * volt3) / 1000;

                    // Время последнего ответа от счетчика...
                    //par = give_arg("lastCon", answer).Replace('$', ':');
                    //LastCon = DateTime.ParseExact(par, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    //TimeSpan ts = DateTime.Now - LastCon;
                    //lostDev = (ts > new TimeSpan(0, 0, timeout));

                    //PowerAlarm = CommonPower >= PowerLimit;
                    //CommonAlarm = lostDev || Cur1_Alarm || Cur2_Alarm || Cur3_Alarm || Volt1_Alarm || Volt2_Alarm || Volt3_Alarm || PowerAlarm;

                    
                }
                RefreshTimer.Interval = 3000;
                Console.WriteLine("Нормально опрошены: {0}", count_normalMeter);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Ошибка");
                Console.WriteLine(ex.Message);
                //newClient.Close();
                Console.WriteLine("Перезапуск соединения через {0} сек", this.intervalRestartConnection / 1000);
                RefreshTimer.Interval = this.intervalRestartConnection;
            }
            finally
            {
                RefreshTimer.Enabled = true;
            }
                
        }

        string give_arg(string par, string args)
        {
            string ret = "";

            try
            {
                string[] words = args.Split('*');
                foreach (string elem in words)
                {
                    string[] memb = elem.Split('=');
                    if (memb[0] == par)
                        return memb[1];
                }

                ret = "error";
            }
            catch
            {
                return "error";
            }
            return ret;
        }

        string give_elems(string par, string args)
        {
            string ret = "";

            try
            {
                string[] words = args.Split('-');
                foreach (string elem in words)
                {
                    string[] memb = elem.Split(':');
                    if (memb[0] == par)
                        return memb[1];
                }

                ret = "error";
            }
            catch
            {
                return "error";
            }
            return ret;
        }


        //public void Refresh_data_meters()
        //{
        //    error = 0;
        //    try
        //    {
        //        string answer = ReadTcpData();

        //        if (give_arg("add", answer) == Add.ToString())
        //        {
        //            string par;
        //            // ток
        //            par = give_arg("cur", answer);
        //            cur1_s = give_elems("1", par);
        //            cur2_s = give_elems("2", par);
        //            cur3_s = give_elems("3", par);
        //            par = give_arg("volt", answer);
        //            volt1_s = give_elems("1", par);
        //            volt2_s = give_elems("2", par);
        //            volt3_s = give_elems("3", par);
        //            // Время последнего ответа от счетчика...
        //            par = give_arg("lastCon", answer).Replace('$', ':');
        //            //LastCon = DateTime.ParseExact(par, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //            //TimeSpan ts = DateTime.Now - LastCon;
        //            //lostDev = (ts > new TimeSpan(0, 0, timeout));
        //            CommonPower = (cur1 * volt1 + cur2 * volt2 + cur3 * volt3) / 1000;
        //            //PowerAlarm = CommonPower >= PowerLimit;
        //            //CommonAlarm = lostDev || Cur1_Alarm || Cur2_Alarm || Cur3_Alarm || Volt1_Alarm || Volt2_Alarm || Volt3_Alarm || PowerAlarm;
        //            return;
        //        }
        //        //lostDev = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        //lostDev = true;
        //    }
        //}

    }
}
