using Pokemon_Go_Threaded_Trainer.Forms;
using Pokemon_Go_Threaded_Trainer.Interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon_Go_Threaded_Trainer.Classes
{
    public class TrainerInstance
    {
        private Stopwatch sw = new Stopwatch();
        private readonly ILog _log;
        public GroupBox CreateInstance(int ID)
        {
            GroupBox gb = new GroupBox();
            gb.Size = new Size(395, 165);
            gb.Name = ID.ToString();
            //gb.Parent = flow;
            // start
            Button start = new Button();
            start.Size = new Size(42, 23);
            start.Location = new Point(10, 20);
            start.Name = $"start{ID}";
            start.Text = "Start";
            start.Parent = gb;
            start.Click += new EventHandler(delegate (Object s, EventArgs d)
            {
                int id = int.Parse(((Button)s).Parent.Name);
                var data = Main.getClientData[id];
                var startControl = ((Button)s);
                if (startControl.Text == "Start")
                {
                    ((Button)Main.getFlowLayoutPanel.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = false;
                    Main.getBots++;
                    //Task.Run(() =>
                    Task T = Task.Factory.StartNew(() => 
                    //data.thread = new Thread(() => Execute(id));
                    {
                        if (data.token.Token.IsCancellationRequested) return;
                    }, data.token.Token);
                    startControl.Text = "Stop";
                }
                else if (startControl.Text == "Stop")
                {
                    ((Button)s).Enabled = true;
                    ((Button)Main.getFlowLayoutPanel.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = true;
                    data.token.Cancel(false);
                    _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH: mm:ss")}] Stopping...");
                    //data.thread.Abort();
                    //if (data.thread.ThreadState == System.Threading.ThreadState.Aborted)
                    if (data.token.IsCancellationRequested)
                    {
                        _log.Log_(id, Color.Red, $"[{DateTime.Now.ToString("HH:mm:ss")}] Stopped!");
                        ((Button)Main.getFlowLayoutPanel.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = true;
                        ((Button)Main.getFlowLayoutPanel.Controls[$"{id}"].Controls[$"start{id}"]).Enabled = true;
                        ((Button)Main.getFlowLayoutPanel.Controls[$"{id}"].Controls[$"start{id}"]).Text = "Start";
                        Main.getBots--;
                    }
                }
            });
            gb.Controls.Add(start);
            // bag
            Button bag = new Button();
            bag.Size = new Size(47, 23);
            bag.Location = new Point(58, 20);
            bag.Name = $"bag{ID}";
            bag.Text = "Bag";
            bag.Parent = gb;
            bag.Click += new EventHandler(delegate (Object s, EventArgs d)
            {
                int id = int.Parse(((Button)s).Parent.Name);
                var data = Main.getClientData[id];
                //Bag bg = new Bag();
                //bg.client = data.client;
                //bg.Show();
                ((Button)s).Enabled = false;
                /*bg.FormClosed += new FormClosedEventHandler(delegate (Object q, FormClosedEventArgs w)
                {
                    ((Button)s).Enabled = true;
                });*/
            });
            bag.Enabled = false;
            gb.Controls.Add(bag);
            // pokemon
            Button pokemon = new Button();
            pokemon.Size = new Size(75, 23);
            pokemon.Location = new Point(111, 20);
            pokemon.Name = $"pokemon{ID}";
            pokemon.Text = "Pokemon";
            pokemon.Parent = gb;
            pokemon.Click += new EventHandler(delegate (Object s, EventArgs d)
            {
                int id = int.Parse(((Button)s).Parent.Name);
                var data = Main.getClientData[id];
                //Pokemon pk = new Pokemon();
                //pk.client = data.client;
                //pk.Show();
                ((Button)s).Enabled = false;
                /*pk.FormClosed += new FormClosedEventHandler(delegate (Object q, FormClosedEventArgs w)
                {
                    ((Button)s).Enabled = true;
                });*/
            });
            pokemon.Enabled = false;
            gb.Controls.Add(pokemon);
            // journal
            Button journal = new Button();
            journal.Size = new Size(65, 23);
            journal.Location = new Point(192, 20);
            journal.Name = $"journal{ID}";
            journal.Text = "Journal";
            journal.Parent = gb;
            journal.Enabled = false;
            gb.Controls.Add(journal);
            // remove
            Button remove = new Button();
            remove.Size = new Size(75, 23);
            remove.Location = new Point(314, 20);
            remove.Name = $"remove{ID}";
            remove.Text = "Remove";
            remove.Parent = gb;
            remove.Click += new EventHandler(delegate (Object q, EventArgs w)
            {
                DialogResult dr = MessageBox.Show("You sure you want to remove this session?", "Confirm removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
                else if (dr == DialogResult.Yes)
                {
                    var data = Main.getClientData[int.Parse(((Button)q).Parent.Name)];
                    if (Main.getBots == 0)
                    {
                        sw.Stop();
                        sw.Reset();
                        //upToken.Cancel();
                    }
                    Main.getFlowLayoutPanel.Controls.Remove(((Button)q).Parent);
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
            con.Name = $"console{ID}";
            con.Parent = gb;
            gb.Controls.Add(con);

            return gb;
        }
    }
}
