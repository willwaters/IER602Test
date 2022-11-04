using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using System.Net.Sockets;
using System.Text;

namespace IER602Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient tcpClient;

        private void button1_Click(object sender, EventArgs e)
        {

            tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}CB{(Char)3}"));
        }

        private async void handled()
        {
            byte[] buffer = new byte[1024];
            await tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

            if(chkAutoboard.Checked)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(500);
                    tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}CC{(Char)3}"));
                });
            } else if (chkNoboard.Checked)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(500);
                    tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}CB{(Char)3}"));
                });
            }

            handled();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(textBox1.Text, 9000);
            MessageBox.Show("Connected.");
            handled();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}MG#P#A\"VIDEUR ALBATROSS\"{(Char)3}"));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}CE{(Char)3}"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tcpClient.GetStream().Write(Encoding.ASCII.GetBytes($"{(Char)2}CC{(Char)3}"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}