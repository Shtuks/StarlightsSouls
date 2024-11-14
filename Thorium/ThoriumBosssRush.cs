using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Terraria.ID;
using CalamityMod.NPCs.Crabulon;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossMini;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossViscount;
using CalamityMod.NPCs.SlimeGod;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossBoreanStrider;
using CalamityMod.NPCs.CalClone;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossThePrimordials;
using ThoriumMod.NPCs.BossFallenBeholder;
using CalamityMod.NPCs.Providence;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class ThoriumBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.HasMod("RagnarokMod") && !ModLoader.HasMod("ThoriumRework"))
            {
                for (int i = Bosses.Count - 1; i >= 0; i--)
                {
                    if (Bosses[i].EntityID == NPCID.KingSlime)
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<TheGrandThunderBird>()));
                    }
                    if (Bosses[i].EntityID == ModContent.NPCType<Crabulon>())
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<PatchWerk>(), TimeChangeContext.Night));
                    }
                    if (Bosses[i].EntityID == NPCID.BrainofCthulhu)
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<QueenJellyfish>()));
                    }
                    if (Bosses[i].EntityID == NPCID.QueenBee)
                    {
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Viscount>());
                    }
                    if (Bosses[i].EntityID == ModContent.NPCType<SlimeGodCore>())
                    {
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<BioCore>());
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<CryoCore>());
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<PyroCore>());
                        Bosses.Insert(i, new Boss(ModContent.NPCType<GraniteEnergyStorm>()));
                        Bosses.Insert(i, new Boss(ModContent.NPCType<BuriedChampion>()));
                        Bosses.Insert(i, new Boss(ModContent.NPCType<StarScouter>()));
                    }
                    if (Bosses[i].EntityID == NPCID.QueenSlimeBoss)
                    {
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<BoreanStriderPopped>());
                        Bosses.Insert(i, new Boss(ModContent.NPCType<BoreanStrider>()));
                    }
                    if (Bosses[i].EntityID == ModContent.NPCType<CalamitasClone>())
                    {
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<LichHeadless>());
                        Bosses.Insert(i, new Boss(ModContent.NPCType<Lich>(), TimeChangeContext.Night));
                    }
                    if (Bosses[i].EntityID == NPCID.QueenSlimeBoss)
                    {
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<ForgottenOneCracked>());
                        Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<ForgottenOneReleased>());
                        Bosses.Insert(i, new Boss(ModContent.NPCType<ForgottenOne>()));
                    }
                    if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<DreamEater>()));
                    }
                }

                BossIDsAfterDeath.Add(ModContent.NPCType<BoreanStrider>(), [ModContent.NPCType<BoreanStriderPopped>()]);
                BossIDsAfterDeath.Add(ModContent.NPCType<FallenBeholder>(), [ModContent.NPCType<FallenBeholder2>()]);
                BossIDsAfterDeath.Add(ModContent.NPCType<ForgottenOne>(), [ModContent.NPCType<ForgottenOneCracked>()]);
                BossIDsAfterDeath.Add(ModContent.NPCType<ForgottenOneCracked>(), [ModContent.NPCType<ForgottenOneReleased>()]);
                BossIDsAfterDeath.Add(ModContent.NPCType<Lich>(), [ModContent.NPCType<LichHeadless>()]);
            }
        }
    }
}