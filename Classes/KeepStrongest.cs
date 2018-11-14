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
    public class KeepStrongest : IStrong
    {
        private readonly ILog _log = new Log();
        private readonly ITransfer _tranfser = new Transfer();
        public async Task TransferAllButStrongestUnwantedPokemon(Client client, int id)
        {
            _log.Log_(id, Color.White, "Firing up the meat grinder");

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

            var unwantedPokemonTypes = new[]
            {
                PokemonId.Pidgey,
                PokemonId.Rattata,
                PokemonId.Weedle,
                PokemonId.Zubat,
                PokemonId.Caterpie,
                PokemonId.Pidgeotto,
                PokemonId.NidoranFemale,
                PokemonId.Paras,
                PokemonId.Venonat,
                PokemonId.Psyduck,
                PokemonId.Poliwag,
                PokemonId.Slowpoke,
                PokemonId.Drowzee,
                PokemonId.Gastly,
                PokemonId.Goldeen,
                PokemonId.Staryu,
                PokemonId.Magikarp,
                PokemonId.Clefairy,
                PokemonId.Eevee,
                PokemonId.Tentacool,
                PokemonId.Dratini,
                PokemonId.Ekans,
                PokemonId.Jynx,
                PokemonId.Lickitung,
                PokemonId.Spearow,
                PokemonId.NidoranFemale,
                PokemonId.NidoranMale
            };

            var inventory = await client.Inventory.GetInventory();
            var pokemons = inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.PokemonData)
                .Where(p => p != null && p?.PokemonId > 0)
                .ToArray();

            foreach (var unwantedPokemonType in unwantedPokemonTypes)
            {
                var pokemonOfDesiredType = pokemons.Where(p => p.PokemonId == unwantedPokemonType)
                    .OrderByDescending(p => p.Cp)
                    .ToList();

                var unwantedPokemon =
                    pokemonOfDesiredType.Skip(1) // keep the strongest one for potential battle-evolving
                        .ToList();

                _log.Log_(id, Color.White, "Grinding {unwantedPokemon.Count} pokemons of type {unwantedPokemonType}");
                await _tranfser.TransferAllGivenPokemons(id, client, unwantedPokemon);
            }

            _log.Log_(id, Color.White, "Finished grinding all the meat");
        }
    }
}
