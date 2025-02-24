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
            //if (ShtunConfig.Instance.OldCalDlcBalance)
            //{
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
                    int mutantBaseHealth = 1000000;
                    int mutantAddHealth = 1000000;
                    int multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded){multiplier++;}
                    if (ModCompatibility.Calamity.Loaded) {multiplier+=3;}
                    if (ModCompatibility.SacredTools.Loaded) {multiplier+=2;}
                    if (ModCompatibility.Redemption.Loaded) {multiplier++;}

                    if (npc.type == ModContent.NPCType<MutantBoss>())
                    {
                        npc.lifeMax = mutantBaseHealth*2 + (mutantAddHealth * multiplier);
                    }
                }
            //}
        }
    }
}
