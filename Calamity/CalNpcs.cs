using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using CalamityMod.NPCs.Crabulon;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using ssm.Content.Buffs;
using ssm.Content.Items;
using ssm.Content.Items.Accessories;
using ssm.Content.Items.Consumables;
using ssm.Content.Items.Materials;
using ssm.Content.Items.Singularities;
using ssm.Content.NPCs;
using ssm.Content.NPCs.Shtuxibus;
using ssm.Calamity.Swarm.Energizers;
using Fargowiltas;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Abyss;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Crags;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.Yharon;
using CalamityMod.NPCs.PlagueEnemies;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.SulphurousSea;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.TownNPCs;
using ssm.Core;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public partial class CalNpcs : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.CalSwarmItems;
        }

        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override bool InstancePerEntity => true;

        internal bool SwarmActive;
        internal bool PandoraActive;
        internal bool NoLoot = false;

        public static int boss = -1;
        public static int slimeBoss = -1;
        public static int eyeBoss = -1;
        public static int eaterBoss = -1;
        public static int brainBoss = -1;
        public static int beeBoss = -1;
        public static int skeleBoss = -1;
        public static int deerBoss = -1;
        public static int wallBoss = -1;
        public static int retiBoss = -1;
        public static int spazBoss = -1;
        public static int destroyBoss = -1;
        public static int primeBoss = -1;
        public static int queenSlimeBoss = -1;
        public static int empressBoss = -1;
        public static int betsyBoss = -1;
        public static int fishBoss = -1;
        public static int cultBoss = -1;
        public static int moonBoss = -1;
        public static int guardBoss = -1;
        public static int fishBossEX = -1;
        public static bool spawnFishronEX;
        public static int deviBoss = -1;
        public static int abomBoss = -1;
        public static int mutantBoss = -1;
        public static int Shtuxibus = -1;
        public static int championBoss = -1;

        internal static int[] Bosses = {
            NPCID.KingSlime,
            NPCID.EyeofCthulhu,
            NPCID.EaterofWorldsHead,
            NPCID.BrainofCthulhu,
            NPCID.QueenBee,
            NPCID.SkeletronHead,
            NPCID.QueenSlimeBoss,
            NPCID.TheDestroyer,
            NPCID.SkeletronPrime,
            NPCID.Retinazer,
            NPCID.Spazmatism,
            NPCID.Plantera,
            NPCID.Golem,
            NPCID.DukeFishron,
            NPCID.HallowBoss,
            NPCID.CultistBoss,
            NPCID.MoonLordCore,
            NPCID.MartianSaucerCore,
            NPCID.Pumpking,
            NPCID.IceQueen,
            NPCID.DD2Betsy,
            NPCID.DD2OgreT3,
            NPCID.IceGolem,
            NPCID.SandElemental,
            NPCID.Paladin,
            NPCID.Everscream,
            NPCID.MourningWood,
            NPCID.SantaNK1,
            NPCID.HeadlessHorseman,
            NPCID.PirateShip
        };

        public static bool Revengeance => CalamityMod.World.CalamityWorld.revenge;



        public override bool PreKill(NPC npc)
        {
            if (NoLoot)
            {
                return false;
            }
            if (SwarmActive && ssm.SwarmActive && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (npc.type == ModContent.NPCType<Crabulon>())
                {
                    Swarm(npc, ModContent.NPCType<Crabulon>(), ModContent.ItemType<CrabulonBag>(), ModContent.ItemType<CrabulonTrophy>(), ModContent.ItemType<CrabulonEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<DesertScourgeHead>())
                {
                    Swarm(npc, ModContent.NPCType<DesertScourgeHead>(), ModContent.ItemType<DesertScourgeBag>(), ModContent.ItemType<DesertScourgeTrophy>(), ModContent.ItemType<DesertEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<SupremeCalamitas>())
                {
                    Swarm(npc, ModContent.NPCType<SupremeCalamitas>(), ModContent.ItemType<CalamitasCoffer>(), ModContent.ItemType<SupremeCalamitasTrophy>(), ModContent.ItemType<WaifuEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<AstrumAureus>())
                {
                    Swarm(npc, ModContent.NPCType<AstrumAureus>(), ModContent.ItemType<AstrumAureusBag>(), ModContent.ItemType<AstrumAureusTrophy>(), ModContent.ItemType<AureusEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<CalamitasClone>())
                {
                    Swarm(npc, ModContent.NPCType<CalamitasClone>(), ModContent.ItemType<CalamitasCloneBag>(), ModContent.ItemType<CalamitasCloneTrophy>(), ModContent.ItemType<WaifuCloneEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<PerforatorHive>())
                {
                    Swarm(npc, ModContent.NPCType<PerforatorHive>(), ModContent.ItemType<PerforatorBag>(), ModContent.ItemType<PerforatorTrophy>(), ModContent.ItemType<PerforatorEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Swarm(npc, ModContent.NPCType<DevourerofGodsHead>(), ModContent.ItemType<DevourerofGodsBag>(), ModContent.ItemType<DevourerofGodsTrophy>(), ModContent.ItemType<DevouringEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Cryogen>())
                {
                    Swarm(npc, ModContent.NPCType<Cryogen>(), ModContent.ItemType<CryogenBag>(), ModContent.ItemType<CryogenTrophy>(), ModContent.ItemType<CryoEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<BrimstoneElemental>())
                {
                    Swarm(npc, ModContent.NPCType<BrimstoneElemental>(), ModContent.ItemType<BrimstoneWaifuBag>(), ModContent.ItemType<BrimstoneElementalTrophy>(), ModContent.ItemType<BrimstoneEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Bumblefuck>())
                {
                    Swarm(npc, ModContent.NPCType<Bumblefuck>(), ModContent.ItemType<DragonfollyBag>(), ModContent.ItemType<DragonfollyTrophy>(), ModContent.ItemType<FollyEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<AquaticScourgeHead>())
                {
                    Swarm(npc, ModContent.NPCType<AquaticScourgeHead>(), ModContent.ItemType<AquaticScourgeBag>(), ModContent.ItemType<AquaticScourgeTrophy>(), ModContent.ItemType<ToxicEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<PlaguebringerGoliath>())
                {
                    Swarm(npc, ModContent.NPCType<PlaguebringerGoliath>(), ModContent.ItemType<PlaguebringerGoliathBag>(), ModContent.ItemType<PlaguebringerGoliathTrophy>(), ModContent.ItemType<PlagueEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Anahita>())
                {
                    Swarm(npc, ModContent.NPCType<Anahita>(), ModContent.ItemType<LeviathanBag>(), ModContent.ItemType<LeviathanTrophy>(), ModContent.ItemType<AnahitaEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<AstrumDeusHead>())
                {
                    Swarm(npc, ModContent.NPCType<AstrumDeusHead>(), ModContent.ItemType<AstrumDeusBag>(), ModContent.ItemType<AstrumDeusTrophy>(), ModContent.ItemType<AstralEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<HiveMind>())
                {
                    Swarm(npc, ModContent.NPCType<HiveMind>(), ModContent.ItemType<HiveMindBag>(), ModContent.ItemType<HiveMindTrophy>(), ModContent.ItemType<HiveEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Yharon>())
                {
                    Swarm(npc, ModContent.NPCType<Yharon>(), ModContent.ItemType<YharonBag>(), ModContent.ItemType<YharonTrophy>(), ModContent.ItemType<AuricEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Providence>())
                {
                    Swarm(npc, ModContent.NPCType<Providence>(), ModContent.ItemType<ProvidenceBag>(), ModContent.ItemType<ProvidenceTrophy>(), ModContent.ItemType<ProfandedEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<RavagerBody>())
                {
                    Swarm(npc, ModContent.NPCType<RavagerBody>(), ModContent.ItemType<RavagerBag>(), ModContent.ItemType<RavagerTrophy>(), ModContent.ItemType<FleshyEnergizer>());
                }
                return false;
            }

            else
            {
                return true;
            }
        }
        public override void PostAI(NPC npc)
        {
            //always vulnerable in swarm
            if (SwarmActive && npc.type == ModContent.NPCType<SupremeCalamitas>())
            {
                npc.dontTakeDamage = false;
            }
            else if (SwarmActive && npc.type == ModContent.NPCType<CalamitasClone>())
            {
                npc.dontTakeDamage = false;
            }
        }
        private void SpawnBoss(NPC npc, int boss)
        {
            int spawn;

            if (SwarmActive)
            {
                spawn = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), boss);
                if (spawn != Main.maxNPCs)
                {
                    Main.npc[spawn].GetGlobalNPC<CalNpcs>().SwarmActive = true;
                    NetMessage.SendData(MessageID.SyncNPC, number: boss);
                }
            }
            else
            {
                // Pandora
                int random;

                do
                {
                    random = Main.rand.Next(Bosses);
                }

                while (NPC.CountNPCS(random) >= 4);

                spawn = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), random);
                if (spawn != Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: random);
                }
            }
        }

        private void Swarm(NPC npc, int boss, int bossbag, int trophy, int reward)
        {
            if (bossbag >= 0 && bossbag != ItemID.DefenderMedal)
            {
                npc.DropItemInstanced(npc.Center, npc.Size, bossbag);
            }

            int count = 0;
            if (SwarmActive)
            {
                count = NPC.CountNPCS(boss) - 1; // Since this one is about to be dead
            }
            else
            {
                // Pandora
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && Array.IndexOf(Bosses, Main.npc[i].type) > -1)
                    {
                        count++;
                    }
                }
            }

            int missing = ssm.SwarmSpawned - count;

            ssm.SwarmKills++;

            // Drop swarm reward every 100 kills
            if (ssm.SwarmKills % 100 == 0 && reward > 0)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.Hitbox, reward);
            }

            //drop trphy every 10 killa
            if (ssm.SwarmKills % 10 == 0 && trophy != -1)
            {
                Item.NewItem(npc.GetSource_Loot(), npc.Hitbox, trophy);
            }

            if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Killed: " + ssm.SwarmKills), new Color(206, 12, 15));
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Total: " + ssm.SwarmTotal), new Color(206, 12, 15));
            }
            else
            {
                Main.NewText("Killed: " + ssm.SwarmKills, new Color(206, 12, 15));
                Main.NewText("Total: " + ssm.SwarmTotal, new Color(206, 12, 15));
            }

            // If theres still more to spawn
            if (ssm.SwarmKills <= ssm.SwarmTotal - ssm.SwarmSpawned)
            {
                int spawned = 0;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    // Count NPCs
                    int num = 0;
                    for (int j = 0; j < Main.maxNPCs; j++)
                    {
                        if (Main.npc[j].active)
                        {
                            num++;
                        }
                    }

                    SpawnBoss(npc, boss);
                    spawned++;

                    if (spawned <= missing)
                    {
                        continue;
                    }

                    break;
                }
            }
            else if (ssm.SwarmKills >= ssm.SwarmTotal)
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The swarm has been defeated!"), new Color(206, 12, 15));
                }
                else
                {
                    Main.NewText("The swarm has been defeated!", new Color(206, 12, 15));
                }

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC kill = Main.npc[i];
                    if (kill.active && !kill.friendly && kill.type != NPCID.LunarTowerNebula && kill.type != NPCID.LunarTowerSolar && kill.type != NPCID.LunarTowerStardust && kill.type != NPCID.LunarTowerVortex)
                    {
                        Main.npc[i].GetGlobalNPC<CalNpcs>().NoLoot = true;
                        Main.npc[i].SimpleStrikeNPC(Main.npc[i].lifeMax, -Main.npc[i].direction, true, 0, null, false, 0, true);
                        //Main.npc[i].StrikeNPCNoInteraction(Main.npc[i].lifeMax, 0f, -Main.npc[i].direction, true);
                    }
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    ssm.SwarmActive = false;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData);
                }
            }

            // Make sure theres enough left to beat it
            else
            {
                // Spawn more if needed
                if (count >= ssm.SwarmSpawned || ssm.SwarmTotal <= 20)
                {
                    return;
                }

                int extraSpawn = 0;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    // Count NPCs
                    int num = 0;
                    for (int j = 0; j < Main.maxNPCs; j++)
                    {
                        if (Main.npc[j].active)
                        {
                            num++;
                        }
                    }
                    SpawnBoss(npc, boss);
                    extraSpawn++;

                    if (extraSpawn < 5)
                    {
                        continue;
                    }

                    break;
                }
            }
        }
    }
}
