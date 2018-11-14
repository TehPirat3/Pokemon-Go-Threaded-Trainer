using POGOProtos.Inventory.Item;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using System.Collections.Generic;
using System.Threading;

namespace PokemonGoBot.Interfaces
{
    public interface IConfig : ISettings
    {
        AuthType AuthType { get; }
        double DefaultLatitude { get; }
        double DefaultLongitude { get; }
        string LevelOutput { get; }
        int LevelTimeInterval { get; }
        string GoogleRefreshToken { get; set; }
        string PtcPassword { get; }
        string PtcUsername { get; }
        bool EvolveAllGivenPokemons { get; }
        string TransferType { get; }
        int TransferCPThreshold { get; }
        ICollection<KeyValuePair<ItemId, int>> ItemRecycleFilter { get; set; }
        int RecycleItemsInterval { get; }
        string Language { get; }
        CancellationTokenSource token { get; }
        //Thread thread { get; set; }
        //int GroupBox { get; }
        int CurrentLevel { get; set; }
        string AccessToken { get; set; }
        Client client { get; set; }
    }
}
