using PokemonGo.RocketAPI;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface ILevel
    {
        Task PrintLevel(Client client, int id);
        Task ConsoleLevelTitle(Client client, int id);
    }
}
