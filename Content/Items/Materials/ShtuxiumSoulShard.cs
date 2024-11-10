using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;

namespace ssm.Content.Items.Materials
{
    public class ShtuxiumSoulShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.height = 20;
            Item.value = 70000;
        }
    }
}