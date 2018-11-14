using PokemonGo.RocketAPI;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface IFarm
    {
        Task ExecuteFarmingPokestopsAndPokemons(Client client, int id);
    }
}
