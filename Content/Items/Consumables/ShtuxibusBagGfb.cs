using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;

namespace ssm.Content.Items.Consumables
{
    public class ShtuxibusBagGfb : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = false;
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = -12;
            Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ShtuxiumSoulShard>(), 1, 100, 400));
            itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Sadism>(), 1, 30, 40));
        }
    }
}
