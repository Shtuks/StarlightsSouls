using ssm.Content.NPCs.Shtuxibus;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;

namespace ssm.Content.Items.Consumables
{
    public class ShtuxibusBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = false; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = -12;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot ItemLoot)
        {
            if (Main.zenithWorld)
            {
                ItemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Sadism>(), 1, 1, 29));
            }
            ItemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ShtuxiumSoulShard>(), 1, 30, 40));
            ItemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<EternalEnergy>(), 1, 70, 100));
            ItemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AbomEnergy>(), 1, 100, 130));
            ItemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DeviatingEnergy>(), 1, 130, 160));
        }
    }
}
