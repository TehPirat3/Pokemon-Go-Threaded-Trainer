using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PokemonGo.RocketAPI;
using POGOProtos.Inventory.Item;

namespace PokemonGoBot
{
    public partial class Bag : Form
    {
        public Client client { get; set; }
        public Bag()
        {
            InitializeComponent();
        }

        private async void Bag_Load(object sender, EventArgs e)
        {
            var inv = await client.Inventory.GetInventory();
            var items = inv.InventoryDelta.InventoryItems;

            foreach (var item in items)
            {
                var a = Regex.Replace($"{(ItemId)item.InventoryItemData.Item.ItemId}", "([a-z])([A-Z])", "$1 $2");
                ListViewItem lvi = new ListViewItem(a.Replace("Item", ""));
                listView1.Items.Add(lvi);
                lvi.SubItems.Add(item.InventoryItemData.Item.Count.ToString());
            }
        }
    }
}
