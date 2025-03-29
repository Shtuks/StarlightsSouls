using FargowiltasSouls;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Thorium;
using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossViscount;

namespace ssm.Thorium
{
    public class ThoriumEternityDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule emodeRule = new(new EModeDropCondition());

            if(npc.type == ModContent.NPCType<TheGrandThunderBird>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<StrangeCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<QueenJellyfish>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AquaticDepthsCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<Viscount>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ScarletCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<Lich>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<WondrousCrate>(), 5));
            }
            else if (npc.type == ModContent.NPCType<ForgottenOne>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AbyssalCrate>(), 5));
            }

            else if (npc.type == ModContent.NPCType<StarScouter>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ItemID.FloatingIslandFishingCrate, 5));
            }

            //boreal and sinster crate?
        }
    }
}
