using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Items.Placeable
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name, ModCompatibility.Thorium.Name)]
    public class ShtuxibusRelic : ModItem
    {
        public override void SetDefaults()
        {
            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle as well as setting a few values that are common across all placeable Items
            // The place style (here by default 0) is important if you decide to have more than one relic share the same tile type (more on that in the tiles' code)
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShtuxibusRelic>(), 0);
            Item.width = 30;
            Item.height = 40;
            Item.rare = ItemRarityID.Master;
            Item.master = true; // This makes sure that "Master" displays in the tooltip, as the rarity only changes the Item name color
            Item.value = Item.buyPrice(745, 0, 0, 0);
        }
    }
}
