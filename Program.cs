using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Test.CommandLineParsing;

namespace WindowsFormsApplication1
{
    public class CommandLineTypes
    {
        public bool? water { set; get; }
        public bool? power { set; get; }
        public DateTime? start { set; get; }
        public DateTime? end { set; get; }
        public bool? formonth { set; get; }
        public bool? alldays { set; get; }
        public string path { set; get; }
        public bool? open { set; get; }
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                CommandLineTypes com = new CommandLineTypes();
                Reporter report = new Reporter();
                report.invokeIncrementProgressbar = delegate
                {
                    if (DateTime.Now.Second % 5 == 0)
                    {
                        Console.Write(".");
                    }
                };
                try
                {
                    CommandLineParser.ParseArguments(com, args);
                }
                catch(Exception exc)
                {
                    Console.WriteLine($"Неверный аргумент: {exc.Message}");
                    //Console.ReadLine();
                    return;
                }

                if ((!com.start.HasValue) && (!com.start.HasValue))
                {
                    Console.WriteLine("Не указан период отчета");
                    //Console.ReadLine();
                    return;
                }
                if (com.water.HasValue) 
                {
                    Console.Write("Вода.");
                    report.customSavePath = com.path;
                    report.openReportAfter = com.open ?? false;
                    report.ReadWaterBase(com.start.Value, com.end.Value);
                }
                if (com.power.HasValue)
                {
                    Console.Write("Электроэнергия.");
                    report.customSavePath = com.path;
                    report.openReportAfter = com.open ?? false;
                    report.ReadElectricBase(com.start.Value, com.end.Value, com.formonth ?? false, com.alldays ?? false);
                }
                else
                {
                    Console.WriteLine("Не указан тип отчета");
                    //Console.ReadLine();
                    return;
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
