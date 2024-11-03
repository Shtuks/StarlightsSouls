using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;

namespace ssm.Reworks
{
    public class OldCalDlcNpcBalance : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (ShtunConfig.Instance.OldCalDlcBalance)
            {
                if (ModCompatibility.WrathoftheGods.Loaded)
                {
                    if (npc.type == ModCompatibility.WrathoftheGods.NoxusBoss1.Type ||
                        npc.type == ModCompatibility.WrathoftheGods.NoxusBoss2.Type ||
                        npc.type == ModCompatibility.WrathoftheGods.NamelessDeityBoss.Type)
                    {
                        npc.lifeMax = (int)(npc.lifeMax * 0.5f);
                    }
                }
                if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    npc.lifeMax = 10000000;
                    if (WorldSavingSystem.EternityMode)
                        npc.lifeMax = 50000000;
                    if (Main.expertMode)
                        npc.lifeMax = 20000000;
                    if (Main.masterMode)
                        npc.lifeMax = 30000000;
                    if (WorldSavingSystem.MasochistModeReal)
                        npc.lifeMax = 60000000;
                }
            }
        }
    }
}
