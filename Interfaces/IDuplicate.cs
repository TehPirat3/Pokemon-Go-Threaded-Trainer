using PokemonGo.RocketAPI;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface IDuplicate
    {
        Task TransferDuplicatePokemon(Client client, int id);
    }
}
