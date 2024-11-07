using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Items.Materials
{
    public class ChtuxlagorShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
        }
    }
}