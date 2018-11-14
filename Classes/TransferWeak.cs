using POGOProtos.Data;
using POGOProtos.Enums;
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
    public class TransferWeak : IWeak
    {
        private readonly ILog _log = new Log();
        private readonly ITransfer _transfer = new Transfer();
        public async Task TransferAllWeakPokemon(Client client, int cpThreshold, int id)
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
            //ColoredConsoleWrite(ConsoleColor.White, $"Firing up the meat grinder");

            PokemonId[] doNotTransfer = new[] //these will not be transferred even when below the CP threshold
            { // DO NOT EMPTY THIS ARRAY
                //PokemonId.Pidgey,
                //PokemonId.Rattata,
                //PokemonId.Weedle,
                //PokemonId.Zubat,
                //PokemonId.Caterpie,
                //PokemonId.Pidgeotto,
                //PokemonId.NidoranFemale,
                //PokemonId.Paras,
                //PokemonId.Venonat,
                //PokemonId.Psyduck,
                //PokemonId.Poliwag,
                //PokemonId.Slowpoke,
                //PokemonId.Drowzee,
                //PokemonId.Gastly,
                //PokemonId.Goldeen,
                //PokemonId.Staryu,
                PokemonId.Magikarp,
                PokemonId.Eevee//,
                //PokemonId.Dratini
            };

            var inventory = await client.Inventory.GetInventory();
            var pokemons = inventory.InventoryDelta.InventoryItems
                                .Select(i => i.InventoryItemData?.PokemonData)
                                .Where(p => p != null && p?.PokemonId > 0)
                                .ToArray();

            //foreach (var unwantedPokemonType in unwantedPokemonTypes)
            {
                List<PokemonData> pokemonToDiscard;
                if (doNotTransfer.Count() != 0)
                    pokemonToDiscard = pokemons.Where(p => !doNotTransfer.Contains(p.PokemonId) && p.Cp < cpThreshold).OrderByDescending(p => p.Cp).ToList();
                else
                    pokemonToDiscard = pokemons.Where(p => p.Cp < cpThreshold).OrderByDescending(p => p.Cp).ToList();


                //var unwantedPokemon = pokemonOfDesiredType.Skip(1) // keep the strongest one for potential battle-evolving
                //                                          .ToList();
                _log.Log_(id, Color.Gray, $"Grinding {pokemonToDiscard.Count} pokemon below {cpThreshold} CP.");
                await _transfer.TransferAllGivenPokemons(id, client, pokemonToDiscard);

            }

            _log.Log_(id, Color.Gray, $"Finished grinding all the meat");
        }
    }
}
