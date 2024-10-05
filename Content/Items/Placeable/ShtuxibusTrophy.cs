using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Placeable
{
    public class ShtuxibusTrophy : ModItem
    {
        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShtuxibusTrophy>());
            Item.width = 32;
            Item.height = 32;
            Item.rare = 11;
            Item.value = Item.buyPrice(745, 0, 0, 0);
        }
    }
}
