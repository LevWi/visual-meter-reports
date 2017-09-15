using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleRedirection
{
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        //public bool ScrollToEnd { set; get; }

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
            //writeText = wrt;
        }

        public override void Write(char value)
        {

            base.Write(value);

            _output.BeginInvoke((MethodInvoker)(() => _output.AppendText(value.ToString())));
            //writeText(value);                            
        }
        //public  override void WriteLine(string value)
        //{

        //    base.WriteLine(value);

        //    _output.BeginInvoke((MethodInvoker)(() => _output.AppendText(value.ToString())));
        //    //writeText(value);                            
        //}

        // public Action<char> writeText;

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
