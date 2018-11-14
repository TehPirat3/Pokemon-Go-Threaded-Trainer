///using POGOProtos.Inventory.Item;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGoBot.Classes;
using PokemonGoBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGoBot
{
    public partial class Main : Form
    {
        //static int Currentlevel = -1;
        public static int errors = 0;
        public static int bots = 0;
        public static int id = 0;
        public static Dictionary<int, IConfig> clientData = new Dictionary<int, IConfig>();
        private Stopwatch sw = new Stopwatch();
        //private CancellationTokenSource upToken = new CancellationTokenSource();
        private PerformanceCounter cpu;
        private PerformanceCounter ram;
        private readonly IExperience _experience;
        private readonly ICatch _catch;
        private readonly ILog _log;
        private readonly IDuplicate _duplicate;
        private readonly IEvolve _evolve;
        private readonly IFarm _farm;
        private readonly IStrong _strong;
        private readonly ITransfer _transfer;
        private readonly IWeak _weak;
        private readonly ILevel _level;
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
            _experience = new Experience();
            _catch = new Catch();
            _log = new Log();
            _duplicate = new Duplicate();
            _evolve = new Evolve();
            _farm = new Farm();
            _strong = new KeepStrongest();
            _transfer = new Transfer();
            _weak = new TransferWeak();
            _level = new ReturnLevels();
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

                           cpuUsage.Text = cpu.NextValue().ToString().Split(Convert.ToChar("."))[0] + "%";
                           ramUsage.Text = ram.NextValue().ToString().Split(Convert.ToChar("."))[0] + "MB";

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

                /**GroupBox gb = new GroupBox();
                gb.Size = new Size(395, 165);
                gb.Name = id.ToString();
                gb.Parent = flow;
                // start
                Button start = new Button();
                start.Size = new Size(42, 23);
                start.Location = new Point(10, 20);
                start.Name = $"start{id}";
                start.Text = "Start";
                start.Parent = gb;
                start.Click += new EventHandler(delegate (Object s, EventArgs d)
                {
                    int id = int.Parse(((Button)s).Parent.Name);
                    var data = clientData[id];
                    var startControl = ((Button)s);
                    if (startControl.Text == "Start")
                    {
                        ((Button)flow.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = false;
                        bots++;
                        Task.Run(() =>
                        //data.thread = new Thread(() => Execute(id));
                        {
                        if (data.token.Token.IsCancellationRequested) return;
                        try
                        {
                            Execute(id);
                            //data.thread.Start();
                        }
                        catch (PtcOfflineException)
                        {
                            _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH:mm:ss")}] PTC Servers are probably down OR your credentials are wrong.");
                            errors++;
                            bots--;
                        }
                        catch (Exception ex)
                        {
                            _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH:mm:ss")}] Unhandled exception: {ex}");
                            errors++;
                            bots--;
                        }
                        }, data.token.Token);
                        startControl.Text = "Stop";
                    }
                    else if (startControl.Text == "Stop")
                    {
                        ((Button)s).Enabled = true;
                        ((Button)flow.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = true;
                        data.token.Cancel(false);
                        _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH: mm:ss")}] Stopping...");
                        //data.thread.Abort();
                        //if (data.thread.ThreadState == System.Threading.ThreadState.Aborted)
                        if (data.token.IsCancellationRequested)
                        {
                            _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH:mm:ss")}] Stopped!");
                            ((Button)flow.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = true;
                            ((Button)flow.Controls[$"{id}"].Controls[$"start{id}"]).Enabled = true;
                            ((Button)flow.Controls[$"{id}"].Controls[$"start{id}"]).Text = "Start";
                            bots--;
                        }
                    }
                });
                gb.Controls.Add(start);
                // bag
                Button bag = new Button();
                bag.Size = new Size(47, 23);
                bag.Location = new Point(58, 20);
                bag.Name = $"bag{id}";
                bag.Text = "Bag";
                bag.Parent = gb;
                bag.Click += new EventHandler(delegate (Object s, EventArgs d)
                {
                    int id = int.Parse(((Button)s).Parent.Name);
                    var data = clientData[id];
                    Bag bg = new Bag();
                    bg.client = data.client;
                    bg.Show();
                    ((Button)s).Enabled = false;
                    bg.FormClosed += new FormClosedEventHandler(delegate (Object q, FormClosedEventArgs w)
                    {
                        ((Button)s).Enabled = true;
                    });
                });
                bag.Enabled = false;
                gb.Controls.Add(bag);
                // pokemon
                Button pokemon = new Button();
                pokemon.Size = new Size(75, 23);
                pokemon.Location = new Point(111, 20);
                pokemon.Name = $"pokemon{id}";
                pokemon.Text = "Pokemon";
                pokemon.Parent = gb;
                pokemon.Click += new EventHandler(delegate (Object s, EventArgs d)
                {
                    int id = int.Parse(((Button)s).Parent.Name);
                    var data = clientData[id];
                    Pokemon pk = new Pokemon();
                    pk.client = data.client;
                    pk.Show();
                    ((Button)s).Enabled = false;
                    pk.FormClosed += new FormClosedEventHandler(delegate (Object q, FormClosedEventArgs w)
                    {
                        ((Button)s).Enabled = true;
                    });
                });
                pokemon.Enabled = false;
                gb.Controls.Add(pokemon);
                // journal
                Button journal = new Button();
                journal.Size = new Size(65, 23);
                journal.Location = new Point(192, 20);
                journal.Name = $"journal{id}";
                journal.Text = "Journal";
                journal.Parent = gb;
                journal.Enabled = false;
                gb.Controls.Add(journal);
                // remove
                Button remove = new Button();
                remove.Size = new Size(75, 23);
                remove.Location = new Point(314, 20);
                remove.Name = $"remove{id}";
                remove.Text = "Remove";
                remove.Parent = gb;
                remove.Click += new EventHandler(delegate (Object q, EventArgs w)
                {
                    DialogResult dr = MessageBox.Show("You sure you want to remove this session?", "Confirm removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;
                    else if (dr == DialogResult.Yes)
                    {
                        var data = clientData[int.Parse(((Button)q).Parent.Name)];
                        if (bots == 0)
                        {
                            sw.Stop();
                            sw.Reset();
                            //upToken.Cancel();
                        }
                        flow.Controls.Remove(((Button)q).Parent);
                    }
                });
                gb.Controls.Add(remove);
                // console
                RichTextBox con = new RichTextBox();
                con.Size = new Size(383, 109);
                con.Location = new Point(6, 50);
                con.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                con.BackColor = Color.Black;
                con.ForeColor = Color.White;
                con.BorderStyle = BorderStyle.None;
                con.ReadOnly = true;
                con.Name = $"console{id}";
                con.Parent = gb;
                gb.Controls.Add(con);*/

                GroupBox gb = new TrainerInstance().CreateInstance(id);
                flow.Controls.Add(gb);
            });
        }

        private async void Execute(int id)
        {
            var ClientSettings = clientData[id];
            ClientSettings.token.Token.ThrowIfCancellationRequested();
            var client = ClientSettings.client = new Client(ClientSettings);
            try
            {
                switch (ClientSettings.AuthType)
                {
                    case AuthType.Ptc:
                        _log.Log_(id, Color.Green, "Attempting to log into Pokemon Trainers Club..");
                        //await client.Login.DoPtcLogin(ClientSettings.PtcUsername, ClientSettings.PtcPassword);
                        break;
                    case AuthType.Google:
                        _log.Log_(id, Color.Green, "Attempting to log into Google..");
                        if (ClientSettings.GoogleRefreshToken == "")
                            _log.Log_(id, Color.Green, "Now opening www.Google.com/device");// and copying the 8 digit code to your clipboard");

                        //await client.Login.DoGoogleLogin(ClientSettings.PtcUsername, ClientSettings.PtcPassword);
                        break;
                }
                //await client.SetServer();
              ///  await client.Player.UpdatePlayerLocation(ClientSettings.DefaultLatitude, ClientSettings.DefaultLongitude, ClientSettings.DefaultAltitude, 3);
               /// var profile = await client.Player?.GetPlayer();
                //var settings = await client.GetSettings();
               /// var mapObjects = await client.Map.GetMapObjects();
                //var inventory = await client.Inventory.GetInventory();

                //var pokemons = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PokemonData).Where(p => p != null && p?.PokemonId > 0);

                var pkb = (Button)flow.Controls[$"{id}"].Controls[$"pokemon{id}"];
                pkb.BeginInvoke((MethodInvoker)delegate () { pkb.Enabled = true; });
                var bagb = (Button)flow.Controls[$"{id}"].Controls[$"bag{id}"];
                bagb.BeginInvoke((MethodInvoker)delegate () { bagb.Enabled = true; });
                var jnb = (Button)flow.Controls[$"{id}"].Controls[$"journal{id}"];
                //jnb.BeginInvoke((MethodInvoker)delegate () { jnb.Enabled = true; });

                _level.ConsoleLevelTitle(client, id);

                _log.Log_(id, Color.Yellow, "----------------------------");
                _log.Log_(id, Color.Cyan, "Account: " + ClientSettings.PtcUsername);
                _log.Log_(id, Color.Cyan, "Password: " + ClientSettings.PtcPassword + "\n");
                _log.Log_(id, Color.DarkGray, "Latitude: " + ClientSettings.DefaultLatitude);
                _log.Log_(id, Color.DarkGray, "Longitude: " + ClientSettings.DefaultLongitude);
                _log.Log_(id, Color.Yellow, "----------------------------");
                _log.Log_(id, Color.DarkGray, "Your Account:\n");
              ///  _log.Log_(id, Color.DarkGray, "Name: " + profile.PlayerData.Username);
              ///  _log.Log_(id, Color.DarkGray, "Team: " + profile.PlayerData.Team);
              ///  _log.Log_(id, Color.DarkGray, "Stardust: " + profile.PlayerData.Currencies.ToArray()[1].Amount);

                _log.Log_(id, Color.Cyan, "Farming Started");
                _log.Log_(id, Color.Yellow, "----------------------------");
                switch (ClientSettings.TransferType)
                {
                    case "leaveStrongest":
                       /// await _strong.TransferAllButStrongestUnwantedPokemon(client, id);
                        break;
                    case "all":
                        //await _transfer.TransferAllGivenPokemons(id, client, pokemons);
                        break;
                    case "duplicate":
                       /// await _duplicate.TransferDuplicatePokemon(client, id);
                        break;
                    case "cp":
                       /// await _weak.TransferAllWeakPokemon(client, ClientSettings.TransferCPThreshold, id);
                        break;
                    default:
                        _log.Log_(id, Color.DarkGray, "Transfering pokemon disabled");
                        break;
                }

                //if (ClientSettings.EvolveAllGivenPokemons)
                    //await _evolve.EvolveAllGivenPokemons(client, pokemons, id);
                //if (ClientSettings.Recycler)
                  //client.RecycleItems(client, id);

                ///await Task.Delay(5000);
                _level.PrintLevel(client, id);
               /// await _farm.ExecuteFarmingPokestopsAndPokemons(client, id);
                ///await _catch.ExecuteCatchAllNearbyPokemons(client, id);
                _log.Log_(id, Color.Red, $"No nearby usefull locations found. Please wait 10 seconds.");
               /// await Task.Delay(10000);
                //CheckVersion();
                Execute(id);
            }
            catch (TaskCanceledException) { _log.Log_(id, Color.Red, "Task Canceled Exception - Restarting"); Execute(id); errors++; }
            catch (OperationCanceledException) { }
            catch (UriFormatException) { _log.Log_(id, Color.Red, "System URI Format Exception - Restarting"); Execute(id); errors++; }
            //catch (ArgumentOutOfRangeException) { _log.Log_(id, Color.Red, "ArgumentOutOfRangeException - Restarting"); Execute(id); errors++; }
            catch (ArgumentNullException) { _log.Log_(id, Color.Red, "Argument Null Refference - Restarting"); Execute(id); errors++; }
            catch (NullReferenceException) { _log.Log_(id, Color.Red, "Null Refference - Restarting"); Execute(id); errors++; }
            catch (GoogleException) { _log.Log_(id, Color.Red, "Google Exception - Bad Authentication!"); ClientSettings.token.Cancel(); errors++; return; }
            //catch (Exception ex) { _log.Log_(id, Color.Red, ex.ToString()); errors++; }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }

    public class Data/// : IConfig
    {
        public AuthType AuthType { get; set; }
        public string PtcUsername { get; set; }
        public string PtcPassword { get; set; }
        public string GoogleUsername { get; set; }
        public string GooglePassword { get; set; }
        public string GoogleRefreshToken { get; set; }
        public double DefaultLatitude { get; set; }
        public double DefaultLongitude { get; set; }
        public double DefaultAltitude { get; set; }
        public string LevelOutput { get; set; }
        public int LevelTimeInterval { get; set; }
        public string TransferType { get; set; }
        public int TransferCPThreshold { get; set; }
        public bool EvolveAllGivenPokemons { get; set; }
        /*ICollection<KeyValuePair<ItemId, int>> IConfig.ItemRecycleFilter
        {
            get
            {
                //Type and amount to keep
                return new[]
                {
                    new KeyValuePair<ItemId, int>(ItemId.ItemPokeBall, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemGreatBall, 50),
                    new KeyValuePair<ItemId, int>(ItemId.ItemUltraBall, 100),
                    new KeyValuePair<ItemId, int>(ItemId.ItemMasterBall, 200),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRazzBerry, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRevive, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemSuperPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemHyperPotion, 50)
                };
            }

            set
            {
                throw new NotImplementedException();
            }
        }*/
        public int RecycleItemsInterval { get; set; }
        public string Language { get; set; }

        public CancellationTokenSource token { get; set; }
        //public Thread thread { get; set; }
        //public int GroupBox { get; set; }
        public int CurrentLevel { get; set; }
        public string AccessToken { get; set; }
        public Client client { get; set; }

        public string Password
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Username
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DevicePlatform
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string AndroidBoardName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string AndroidBootloader
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceBrand
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceModelIdentifier
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DeviceModelBoot
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string HardwareManufacturer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string HardwareModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirmwareBrand
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirmwareTags
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirmwareType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirmwareFingerprint
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseProxy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseProxyAuthentication
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UseProxyHost
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UseProxyPort
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UseProxyUsername
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UseProxyPassword
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UsePogoDevHashServer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string UrlHashServices
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string EndPoint
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseCustomAPI
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string AuthAPIKey
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool DisplayVerboseLog
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AutoExitBotIfAccountFlagged
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double AccountLatitude
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double AccountLongitude
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AccountActive
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Country
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TimeZone
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string POSIX
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double RunStart
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double RunEnd
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public class ItemId
        {
        }
    }
}
