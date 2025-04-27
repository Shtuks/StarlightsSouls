using Terraria.ModLoader;
using ssm.Core;
using Terraria;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Systems;
using ssm.Content.NPCs.MutantEX;

namespace ssm.Reworks
{
    public class OldCalDlcNpcBalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(NPC npc)
        {
            //if (ShtunConfig.Instance.OldCalDlcBalance)
            //{
            //if (ModCompatibility.WrathoftheGods.Loaded)
            //{
            //    if (npc.type == ModCompatibility.WrathoftheGods.NoxusBoss1.Type ||
            //        npc.type == ModCompatibility.WrathoftheGods.NoxusBoss2.Type ||
            //        npc.type == ModCompatibility.WrathoftheGods.NamelessDeityBoss.Type)
            //    {
            //        npc.lifeMax = (int)(npc.lifeMax * 0.5f);
            //    }
            //}

            if (npc.type == ModContent.NPCType<MutantBoss>())
                {
                    int mutantBaseHealth = 1000000;
                    int mutantAddHealth = 1000000;
                    int monstrBaseHealth = 10000000;
                    int monstrAddHealth = 10000000;
                    int multiplier = 0;

                    if (ModCompatibility.Thorium.Loaded){multiplier+=2;}
                    if (ModCompatibility.Calamity.Loaded) {multiplier+=6;} //calamity is fucking unbalanced
                    if (ModCompatibility.SacredTools.Loaded) {multiplier+=4;}
                    if (ModCompatibility.Homeward.Loaded) {multiplier+=3;}
                    if (ModCompatibility.Redemption.Loaded) {multiplier++;}
                    if (ModCompatibility.Polarities.Loaded && ModCompatibility.Spooky.Loaded) { multiplier++;}

                    if (npc.type == ModContent.NPCType<MutantBoss>())
                    {
                        npc.damage = 300 + (100 * multiplier);
                        npc.defense = 300 + (100 * multiplier/2);
                        npc.lifeMax = mutantBaseHealth * 2 + (mutantAddHealth * (!WorldSavingSystem.MasochistModeReal ? multiplier : multiplier*2));
                    }

                    if (npc.type == ModContent.NPCType<MutantEX>())
                    {
                        npc.defense = 100 + (100 * multiplier / 4);
                        npc.lifeMax = monstrBaseHealth * 2 + (monstrAddHealth * (!WorldSavingSystem.MasochistModeReal ? multiplier / 2 : multiplier));
                    }
            }
            //}
        }
    }
}
