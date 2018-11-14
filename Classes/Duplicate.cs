using PokemonGo.RocketAPI;
using PokemonGoBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoBot.Classes
{
    public class Duplicate : IDuplicate
    {
        private readonly ILog _log = new Log();
        public async Task TransferDuplicatePokemon(Client client, int id)
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
            //ColoredConsoleWrite(ConsoleColor.White, $"Check for duplicates");
            var inventory = await client.Inventory.GetInventory();
            var allpokemons =
                inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PokemonData)
                    .Where(p => p != null && p?.PokemonId > 0);

            var dupes = allpokemons.OrderBy(x => x.Cp).Select((x, i) => new { index = i, value = x })
                .GroupBy(x => x.value.PokemonId)
                .Where(x => x.Skip(1).Any());

            for (var i = 0; i < dupes.Count(); i++)
            {
                for (var j = 0; j < dupes.ElementAt(i).Count() - 1; j++)
                {
                    var dubpokemon = dupes.ElementAt(i).ElementAt(j).value;
                    if (dubpokemon.Favorite == 0)
                    {
                        var transfer = await client.Inventory.TransferPokemon(dubpokemon.Id);
                        string pokemonName = Convert.ToString(dubpokemon.PokemonId);
                        _log.Log_(id, Color.DarkGreen,
                            $"Transferred {pokemonName} with {dubpokemon.Cp} CP (Highest is {dupes.ElementAt(i).Last().value.Cp})");

                    }
                }
            }
        }
    }
}
