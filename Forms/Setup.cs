using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Pokemon_Go_Threaded_Trainer.Forms
{
    public partial class Setup : Form
    {
        public bool completed = false;
        public Setup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.Controls.OfType<TextBox>().Any(t => string.IsNullOrEmpty(t.Text)))
            {
                MessageBox.Show("Not all fields are valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //var data = Main.data;
            /*Data data = new Data();
            Main.id++;
            CancellationTokenSource cts = new CancellationTokenSource();
            data.AuthType = authType.Text == "Ptc" ? AuthType.Ptc : AuthType.Google;
            //if (authType.Text == "Ptc")
            data.PtcUsername = authType.Text == "Ptc" ? user.Text : string.Empty;
            data.PtcPassword = authType.Text == "Ptc" ? pass.Text : string.Empty;
            data.GoogleUsername = authType.Text == "Google" ? user.Text : string.Empty;
            data.GooglePassword = authType.Text == "Google" ? pass.Text : string.Empty;
            data.DefaultLatitude = double.Parse(lat.Value.ToString());
            data.DefaultLongitude = double.Parse(lon.Value.ToString());
            data.LevelOutput = levelOutput.Text;
            data.LevelTimeInterval = int.Parse(levelInterval.Value.ToString());
            data.RecycleItemsInterval = int.Parse(recycleInterval.Value.ToString());
            data.TransferType = trasnferType.Text;
            data.TransferCPThreshold = int.Parse(cpThreshold.Value.ToString());
            data.EvolveAllGivenPokemons = evolve.Checked;
            data.CurrentLevel = -1;
            data.GoogleRefreshToken = string.Empty;
            data.token = cts;*/
            Main.clientData.Add(Main.id, null);
            completed = true;
            Close();
        }

        private void authType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /*switch (authType.Text)
            {
                case "Ptc":
                    pass.Enabled = true;
                    user.Enabled = true;
                    break;
                case "Google":
                    pass.Enabled = false;
                    user.Enabled = false;
                    break;
            }*/
        }

        private void Setup_Load(object sender, EventArgs e)
        {

        }

        private void levelOutput_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (levelOutput.Text)
            {
                case "levelup":
                    levelInterval.Enabled = false;
                        break;
                case "time":
                    levelInterval.Enabled = true;
                    break;
            }
        }

        private void trasnferType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (trasnferType.Text)
            {
                case "cp":
                    cpThreshold.Enabled = true;
                    break;
                default:
                    cpThreshold.Enabled = false;
                    break;
            }
        }

    }
}
