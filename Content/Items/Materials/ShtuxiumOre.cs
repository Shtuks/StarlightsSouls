using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Materials
{
    public class ShtuxiumOre : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ShtuxiumOreTile>());
            Item.width = 12;
            Item.height = 12;
            Item.value = 10000;
        }
    }
}