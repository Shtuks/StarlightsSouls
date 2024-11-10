using Terraria.ModLoader;
using ssm.Core;
using static CalamityMod.Events.BossRushEvent;
using Terraria.ID;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.DevourerofGods;
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

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class ThoriumBossRush : ModSystem
    {
        public override void PostSetupContent()
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
                    Bosses.Insert(i, new Boss(ModContent.NPCType<GraniteEnergyStorm>()));
                    Bosses.Insert(i, new Boss(ModContent.NPCType<BuriedChampion>()));
                    Bosses.Insert(i, new Boss(ModContent.NPCType<StarScouter>()));
                }
                if (Bosses[i].EntityID == NPCID.QueenSlimeBoss)
                {
                    Bosses[i].HostileNPCsToNotDelete.Add(ModContent.NPCType<BoreanStriderBase>());
                }
                if (Bosses[i].EntityID == ModContent.NPCType<CalamitasClone>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Lich>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == NPCID.QueenSlimeBoss)
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<ForgottenOne>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<DreamEater>()));
                }

            }

            ////Adding bosses to boss rush
            Mod cal = ModCompatibility.Calamity.Mod;
        }
    }
}