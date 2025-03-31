using FargowiltasSouls;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Redemption.Items.Placeable.Furniture.Lab;
using Redemption.Items.Placeable.Furniture.PetrifiedWood;
using Redemption.NPCs.Bosses.Cleaver;
using Redemption.NPCs.Bosses.Gigapora;
using Redemption.NPCs.Bosses.Obliterator;
using Redemption.NPCs.Bosses.PatientZero;
using ssm.Core;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionEternityDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule emodeRule = new(new EModeDropCondition());
            if (npc.type == ModContent.NPCType<PZ>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<PetrifiedCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<OmegaCleaver>() || npc.type == ModContent.NPCType<Gigapora>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<LabCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<OO>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<LabCrate2>(), 5));
            }
        }
    }
}
