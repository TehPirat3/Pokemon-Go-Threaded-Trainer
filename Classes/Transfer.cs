using POGOProtos.Data;
using PokemonGo.RocketAPI;
using PokemonGoBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POGOProtos.Networking.Responses.ReleasePokemonResponse.Types;

namespace PokemonGoBot.Classes
{
    public class Transfer : ITransfer
    {
        private readonly ILog _log = new Log();
        //Main main = new Main();
        public float Perfect(PokemonData poke)
        {
            return ((float)(poke.IndividualAttack + poke.IndividualDefense + poke.IndividualStamina) / (3.0f * 15.0f)) * 100.0f;
        }
        public async Task TransferAllGivenPokemons(int id, Client client, IEnumerable<PokemonData> unwantedPokemons, float keepPerfectPokemonLimit = 80.0f)
        {
            var ClientSettings = Main.clientData[id];
            ClientSettings.token.Token.ThrowIfCancellationRequested();
            foreach (var pokemon in unwantedPokemons)
            {
                if (Perfect(pokemon) >= keepPerfectPokemonLimit) continue;
                _log.Log_(id, Color.White, $"Pokemon {pokemon.PokemonId} with {pokemon.Cp} CP has IV percent less than {keepPerfectPokemonLimit}%");

                if (pokemon.Favorite == 0)
                {
                    var transferPokemonResponse = await client.Inventory.TransferPokemon(pokemon.Id);

                    /*
                    ReleasePokemonOutProto.Status {
                        UNSET = 0;
                        SUCCESS = 1;
                        POKEMON_DEPLOYED = 2;
                        FAILED = 3;
                        ERROR_POKEMON_IS_EGG = 4;
                    }*/
                    string pokemonName = Convert.ToString(pokemon.PokemonId);
                    if (transferPokemonResponse.Result == Result.Success)
                    {
                        _log.Log_(id, Color.Magenta, $"Transferred {pokemonName} with {pokemon.Cp} CP");
                    }
                    else
                    {
                        var status = transferPokemonResponse.Result;

                        _log.Log_(id, Color.Red, $"Somehow failed to transfer {pokemonName} with {pokemon.Cp} CP. " +
                                                 $"ReleasePokemonOutProto.Status was {status}");
                    }

                    await Task.Delay(3000);
                }
            }
        }
    }
}
