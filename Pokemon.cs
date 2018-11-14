using POGOProtos.Data;
using PokemonGo.RocketAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGoBot
{
    public partial class Pokemon : Form
    {
        public Client client { get; set; }
        public Pokemon()
        {
            InitializeComponent();
        }

        private async void Pokemon_Load(object sender, EventArgs e)
        {
            /*var inventory = await client.GetInventory();
            var pokemons = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.Pokemon)
                .Where(p => p != null && p?.PokemonId > 0).OrderByDescending(key => key.Cp);
            foreach (var pokemon in pokemons)
            {
                ListViewItem lvi = new ListViewItem(pokemon.PokemonId.ToString());
                listView1.Items.Add(lvi);
                lvi.SubItems.Add(pokemon.Cp.ToString());
            }*/

            //try
            //{
            var inventory = await client.Inventory.GetInventory();
            var pokemons =
                inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.PokemonData)
                    .Where(p => p != null && p?.PokemonId > 0)
                    .OrderByDescending(key => key.Cp);
            var families = inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.PokemonFamily)
                .Where(p => p != null && (int)p?.FamilyId > 0)
                .OrderByDescending(p => (int)p.FamilyId);

            var imageList = new ImageList { ImageSize = new Size(50, 50) };
            listView1.ShowItemToolTips = true;

            foreach (var pokemon in pokemons)
            {

                var pokemonImage = GetPokemonImage((int)pokemon.PokemonId);
                imageList.Images.Add(pokemon.PokemonId.ToString(), pokemonImage);

                listView1.LargeImageList = imageList;
                var listViewItem = new ListViewItem();
                listViewItem.SubItems.Add("Cp: " + pokemon.Cp);


                var currentCandy = families
                    .Where(i => (int)i.FamilyId <= (int)pokemon.PokemonId)
                    .Select(f => f.Candy)
                    .First();
                var currIv = Math.Round(Perfect(pokemon));
                //listViewItem.SubItems.Add();
                listViewItem.ImageKey = pokemon.PokemonId.ToString();
                listViewItem.Text = string.Format("{0}\nCp:{1}", pokemon.PokemonId, pokemon.Cp);
                listViewItem.ToolTipText = "Candy: " + currentCandy + "\n" + "IV: " + currIv + "%";
                this.listView1.Items.Add(listViewItem);
            }
        }

        private static Bitmap GetPokemonImage(int pokemonId)
        {
            var Sprites = AppDomain.CurrentDomain.BaseDirectory + "Sprites\\";
            string location = Sprites + pokemonId + ".png";
            if (!Directory.Exists(Sprites))
                Directory.CreateDirectory(Sprites);
            if (!File.Exists(location))
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("http://pokeapi.co/media/sprites/pokemon/" + pokemonId + ".png", @location);
            }
            PictureBox picbox = new PictureBox();
            picbox.Image = Image.FromFile(location);
            Bitmap bitmapRemote = (Bitmap)picbox.Image;
            return bitmapRemote;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems.Count == 0) return;
            //label1.Text = listView1.SelectedItems[0]?.SubItems[1].Text;
            //label2.Text = listView1.SelectedItems[0]?.SubItems[2].Text;
        }
        public static float Perfect(PokemonData poke)
        {
            return ((float)(poke.IndividualAttack + poke.IndividualDefense + poke.IndividualStamina) / (3.0f * 15.0f)) * 100.0f;
        }
    }
}
