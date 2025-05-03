using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Core.Systems;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.ADD;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionHPBalance : GlobalNPC
    {
        public bool fullHP = false;
        public override bool InstancePerEntity => true;
        public override bool PreAI(NPC npc)
        {
            if (WorldSavingSystem.EternityMode)
            {
                if (npc.type == ModContent.NPCType<Nebuleus>())
                {
                    npc.lifeMax = 2800000;
                    npc.life = npc.lifeMax;
                    npc.damage = 380;
                }

                if (npc.type == ModContent.NPCType<Nebuleus2>())
                {
                    npc.lifeMax = 4300000;
                    npc.life = npc.lifeMax;
                    npc.damage = 420;
                }

                if (npc.type == ModContent.NPCType<Akka>())
                {
                    npc.lifeMax = 1900000;
                    npc.life = npc.lifeMax;
                    npc.damage = 320;
                }

                if (npc.type == ModContent.NPCType<Ukko>())
                {
                    npc.lifeMax = 2100000;
                    npc.life = npc.lifeMax;
                    npc.damage = 370;
                }
                if (!fullHP) { npc.life = npc.lifeMax; fullHP = true; }
            }
            return base.PreAI(npc);
        }
    }
}
