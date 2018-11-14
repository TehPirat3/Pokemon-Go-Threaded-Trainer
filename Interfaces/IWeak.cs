using PokemonGo.RocketAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoBot.Interfaces
{
    interface IWeak
    {
        Task TransferAllWeakPokemon(Client client, int cpThreshold, int id);
    }
}
