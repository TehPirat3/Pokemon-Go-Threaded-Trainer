using Pokemon_Go_Threaded_Trainer.Classes;
using Pokemon_Go_Threaded_Trainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon_Go_Threaded_Trainer.Forms
{
    public partial class Main : Form
    {
        public static int errors = 0;
        public static int bots = 0;
        public static int id = 0;
        public static Dictionary<int, IConfig> clientData = new Dictionary<int, IConfig>();
        private Stopwatch sw = new Stopwatch();
        private PerformanceCounter cpu;
        private PerformanceCounter ram;
        public static FlowLayoutPanel panel;

        public static Dictionary<int, IConfig> getClientData
        {
            get { return clientData; }
        }
        public static FlowLayoutPanel getFlowLayoutPanel
        {
            get { return panel; }
            set { panel = value; }
        }
        public static int getBots
        {
            get { return bots; }
            set { bots = value; }
        }

        public Main()
        {
            InitializeComponent();
            panel = flow;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            System.Timers.Timer tick = new System.Timers.Timer();
            cpu = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
            ram = new PerformanceCounter("Process", "Working Set", Process.GetCurrentProcess().ProcessName);
            tick.Elapsed += (s, g) =>
            {
                try
                {
                    if (!sw.IsRunning) sw.Start();
                    if (InvokeRequired)
                    {
                        menuStrip1.BeginInvoke((MethodInvoker)delegate
                       {
                           uptime.Text = new DateTime(sw.Elapsed.Ticks).ToString("HH:mm:ss");
                           statusBots.Text = bots.ToString();
                           statusErrors.Text = errors.ToString();

                           cpuUsage.Text = cpu.NextValue().ToString().Split(Convert.ToChar(","))[0] + "%";
                           ramUsage.Text = ram.NextValue().ToString().Split(Convert.ToChar(","))[0] + "MB";

                           cpu.NextValue();
                           ram.NextValue();
                       });
                    } else {
                        uptime.Text = new DateTime(sw.Elapsed.Ticks).ToString("HH:mm:ss");
                        statusBots.Text = bots.ToString();
                        statusErrors.Text = errors.ToString();

                        cpuUsage.Text = cpu.NextValue().ToString().Split(Convert.ToChar("."))[0] + "%";
                        ramUsage.Text = ram.NextValue().ToString().Split(Convert.ToChar("."))[0] + "MB";

                        cpu.NextValue();
                        ram.NextValue();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            };
            tick.Interval = 1000;
            Task.Factory.StartNew(() => tick.Start());
        }

        private void Tick_Tick(object sender, EventArgs e)
        {
            sw.Start();
            uptime.Text = new DateTime(sw.Elapsed.Ticks).ToString("HH:mm:ss");
            ramUsage.Text = 
            statusBots.Text = bots.ToString();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup setup = new Setup();
            setup.Show();
            setup.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a)
            {
                if (setup.completed == false) return;
                GroupBox gb = new TrainerInstance().CreateInstance(id);
                flow.Controls.Add(gb);
            });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
