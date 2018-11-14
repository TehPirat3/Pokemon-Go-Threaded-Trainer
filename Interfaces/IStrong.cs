using PokemonGo.RocketAPI;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface IStrong
    {
        Task TransferAllButStrongestUnwantedPokemon(Client client, int id);
    }
}
