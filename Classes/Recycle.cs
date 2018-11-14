/*using AllEnum;
using Google.Protobuf;
using PokemonGoBot.API;
using PokemonGoBot.API.Enums;
using PokemonGoBot.API.Extensions;
using PokemonGoBot.API.GeneratedCode;
using PokemonGoBot.API.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGoBot.Classes
{
    public class Recycle
    {
        private readonly HttpClient _httpClient;
        private Request.Types.UnknownAuth _unknownAuth;
        public async Task<IEnumerable<Item>> GetItemsToRecycle(ISettings settings, Client client)
        {
            var myItems = await GetItems(client);

            return myItems
                .Where(x => settings.ItemRecycleFilter.Any(f => f.Key == ((ItemId)x.Item_) && x.Count > f.Value))
                .Select(x => new Item { Item_ = x.Item_, Count = x.Count - settings.ItemRecycleFilter.Single(f => f.Key == (AllEnum.ItemId)x.Item_).Value, Unseen = x.Unseen });
        }
        public async Task RecycleItems(Client client, int id)
        {
            var _settings = Main.clientData[id];
            var items = await GetItemsToRecycle(_settings, client);

            foreach (var item in items)
            {
                var transfer = await RecycleItem((AllEnum.ItemId)item.Item_, item.Count);
                //_log.Log_(id, Color.DarkCyan, $"Recycled {item.Count}x {(AllEnum.ItemId)item.Item_}");
                await Task.Delay(500);
            }
            await Task.Delay(_settings.RecycleItemsInterval * 1000);
            RecycleItems(client, id);
        }
        public async Task<IEnumerable<Item>> GetItems(Client client)
        {
            var inventory = await client.GetInventory();
            return inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.Item)
                .Where(p => p != null);
        }
    }
}*/