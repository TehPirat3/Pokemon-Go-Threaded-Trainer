using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Responses;
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
    public class Catch : ICatch
    {
        private readonly ILog log = new Log();
        private readonly ITransfer _transfer = new Transfer();
        private readonly IStrong _strong = new KeepStrongest();
        private readonly IDuplicate _duplicate = new Duplicate();
        private readonly IWeak _weak = new TransferWeak();
        public async Task ExecuteCatchAllNearbyPokemons(Client client, int id)
        {
            var data = Main.clientData[id];
            var mapObjects = await client.Map.GetMapObjects();

            var pokemons = mapObjects.MapCells.SelectMany(i => i.CatchablePokemons);

            foreach (var pokemon in pokemons)
            {
                var update = await client.Player.UpdatePlayerLocation(pokemon.Latitude, pokemon.Longitude, 10);
                var encounterPokemonResponse = await client.Encounter.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnPointId);
                var pokemonCP = encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp;
                CatchPokemonResponse caughtPokemonResponse;
                do
                {
                    caughtPokemonResponse = await client.Encounter.CatchPokemon(pokemon.EncounterId, pokemon.SpawnPointId, ItemId.ItemPokeBall);
                    ; //note: reverted from settings because this should not be part of settings but part of logic
                } while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed || caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchEscape);
                string pokemonName;
                pokemonName = Convert.ToString(pokemon.PokemonId);
                if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                {
                    log.Log_(id, Color.Green, $"We caught a {pokemonName} with {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} CP");
                }
                else
                log.Log_(id, Color.Red, $"{pokemonName} with {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} CP got away..");

                switch (data.TransferType)
                {
                    case "leaveStrongest":
                        await _strong.TransferAllButStrongestUnwantedPokemon(client, id);
                        break;
                    case "all":
                        var inventory2 = await client.Inventory.GetInventory();
                        var pokemons2 = inventory2.InventoryDelta.InventoryItems
                            .Select(i => i.InventoryItemData?.PokemonData)
                            .Where(p => p != null && p?.PokemonId > 0)
                            .ToArray();
                        await _transfer.TransferAllGivenPokemons(id, client, pokemons2);
                        break;
                    case "duplicate":
                        await _duplicate.TransferDuplicatePokemon(client, id);
                        break;
                    case "cp":
                        await _weak.TransferAllWeakPokemon(client, data.TransferCPThreshold, id);
                        break;
                    default:
                        await _weak.TransferAllWeakPokemon(client, data.TransferCPThreshold, id);
                        break;
                }

                await Task.Delay(3000);
            }
        }
    }
}
