using PokemonGo.RocketAPI;
using PokemonGoBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGoBot.Classes
{
    class ReturnLevels : ILevel
    {
        private readonly IExperience _experience = new Experience();
        private readonly ILog _log = new Log();
        public async Task PrintLevel(Client client, int id)
        {
            var ClientSettings = Main.clientData[id];
            /*if (ClientSettings.thread.ThreadState == System.Threading.ThreadState.Aborted)
            {
                _log.Log_(id, Color.Red, "Stopped!");
                ((Button)flow.Controls[$"{id}"].Controls[$"remove{id}"]).Enabled = true;
                ((Button)flow.Controls[$"{id}"].Controls[$"start{id}"]).Enabled = true;
                ((Button)flow.Controls[$"{id}"].Controls[$"start{id}"]).Text = "Start";
                return;
            }*/
            ClientSettings.token.Token.ThrowIfCancellationRequested();
            var inventory = await client.Inventory.GetInventory();
            var stats = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PlayerStats).ToArray();
            foreach (var v in stats)
                if (v != null)
                {
                    //int XpDiff = GetXpDiff(client, v.Level);
                    int XpDiff = _experience.GetExperienceForLevel(v.Level);
                    if (ClientSettings.LevelOutput == "time")
                        _log.Log_(id, Color.Yellow, $"Current Level: " + v.Level + " (" + (v.Experience - XpDiff) + "/" + (v.NextLevelXp - XpDiff) + ")");
                    else if (ClientSettings.LevelOutput == "levelup")
                        if (ClientSettings.CurrentLevel != v.Level)
                        {
                            ClientSettings.CurrentLevel = v.Level;
                            _log.Log_(id, Color.Magenta, $"Current Level: " + v.Level + ". XP needed for next Level: " + (v.NextLevelXp - v.Experience));
                        }
                }
            if (ClientSettings.LevelOutput == "levelup")
                await Task.Delay(1000);
            else
                await Task.Delay(ClientSettings.LevelTimeInterval * 1000);
            await PrintLevel(client, id);
        }

        public async Task ConsoleLevelTitle(Client client, int id)
        {
            var ClientSettings = Main.clientData[id];
            ClientSettings.token.Token.ThrowIfCancellationRequested();
            var console = Main.panel.Controls.Find(id.ToString(), true)[0];
            var inventory = await client.Inventory.GetInventory();
            //var stats = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PlayerStats).ToArray();
            var player = await client.Player.GetPlayer();
            var a = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData);
            var stats = a.Select(i => i.PlayerStats);
            foreach (var v in stats)
                if (v != null)
                {
                    int XpDiff = _experience.GetExperienceForLevel(v.Level); //GetXpDiff(client, v.Level);
                    //if (InvokeRequired)
                    //{
                        console.Invoke((MethodInvoker)delegate ()
                        {
                        console.Text = string.Format(player.PlayerData.Username + " | Level {0:0} - ({1:0} / {2:0}) | Stardust {3:0}", v.Level, (v.Experience - v.PrevLevelXp - XpDiff), (v.NextLevelXp - v.PrevLevelXp - XpDiff), player.PlayerData.CurrencyBalance);
                        });
                    //}
                    //else console.Text = string.Format(Username + " | Level {0:0} - ({1:0} / {2:0}) | Stardust {3:0}", v.Level, (v.Experience - v.PrevLevelXp - XpDiff), (v.NextLevelXp - v.PrevLevelXp - XpDiff), profile.Profile.Currency.ToArray()[1].Amount);
                }

            //await Task.Delay(1000);
            //await ConsoleLevelTitle(Username, client, id);
        }
    }
}
