using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using ssm.Content.NPCs.MutantEX;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ssm.Content.Items.Consumables
{
    public class MonstrocityBag : BossBag
    {
        protected override bool IsPreHMBag => false;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<MutantEX>()));
            itemLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ModContent.ItemType<Sadism>(), 1, 20, 30));
        }
    }
}
