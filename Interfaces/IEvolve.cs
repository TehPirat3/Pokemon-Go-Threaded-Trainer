using POGOProtos.Data;
using PokemonGo.RocketAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface IEvolve
    {
        Task EvolveAllGivenPokemons(Client client, IEnumerable<PokemonData> pokemonToEvolve, int id);
    }
}
