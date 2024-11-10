using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Items.Placeable
{
    public class ShtuxibusRelic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShtuxibusRelic>(), 0);
            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(745, 0, 0, 0);
        }
    }
}
