using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Terraria.ID;
using SacredTools.Content.NPCs.Boss.Decree;
using SacredTools.NPCs.Boss.Jensen;
using CalamityMod.NPCs.Crabulon;
using SacredTools.NPCs.Boss.Pumpkin;
using SacredTools.NPCs.Boss.Araneas;
using SacredTools.NPCs.Boss.Raynare;
using SacredTools.NPCs.Boss.Primordia;
using SacredTools.NPCs.Boss.Abaddon;
using CalamityMod.NPCs.CeaselessVoid;
using SacredTools.NPCs.Boss.Araghur;
using CalamityMod.NPCs.DevourerofGods;
using SacredTools.NPCs.Boss.Lunarians;
using CalamityMod.NPCs.Yharon;
using CalamityMod.NPCs.Providence;
using SacredTools.NPCs.Boss.Erazor;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    public class SoABossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<Crabulon>())
                {
                Bosses.Insert(i, new Boss(ModContent.NPCType<DecreeLegacy>()));
                }
                if (Bosses[i].EntityID == NPCID.Skeleton)
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<JensenLegacy>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == NPCID.QueenBee)
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Ralnek2>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<RalnekPhase3>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<DreadCore>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Ralnek>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == NPCID.QueenSlimeBoss)
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Araneas>());
                }
                if (Bosses[i].EntityID == NPCID.SkeletronPrime)
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Yofaress>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<RoyalHarpy>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Raynare>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == NPCID.Golem)
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<Primordia2>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Primordia>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Providence>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Abaddon>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<CeaselessVoid>())
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurMinion>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurBody>());
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<AraghurTail>());
                    Bosses.Insert(i, new Boss(ModContent.NPCType<AraghurHead>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Novaniel>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Yharon>())
                {
                    Bosses.Add(new Boss(ModContent.NPCType<ErazorBoss>()));
                }

            }

            BossIDsAfterDeath.Add(ModContent.NPCType<Ralnek>(), [ModContent.NPCType<RalnekPhase3>()]);
            BossIDsAfterDeath.Add(ModContent.NPCType<Primordia>(), [ModContent.NPCType<Primordia2>()]);

            ////Adding bosses to boss rush
            Mod cal = ModCompatibility.Calamity.Mod;
        }
    }
}