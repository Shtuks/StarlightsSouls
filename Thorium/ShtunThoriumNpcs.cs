using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
using Fargowiltas;
using Thorium;
using Terraria.ModLoader;
using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using ssm.Core;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossFallenBeholder;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.BossBuriedChampion;
//using ssm.Thorium.Swarm.Energizers;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.Items.BossStarScouter;
using ssm.Thorium.Swarm.Energizers;
using ThoriumMod.NPCs.BossViscount;
using ThoriumMod.Items.BossViscount;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.Items.BossForgottenOne;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public partial class ShtunThoriumNpcs : GlobalNPC
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
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

        public override bool PreKill(NPC npc)
        {
            if (NoLoot)
            {
                return false;
            }
            if (SwarmActive && ssm.SwarmActive && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (npc.type == ModContent.NPCType<BuriedChampion>())
                {
                    Swarm(npc, ModContent.NPCType<BuriedChampion>(), ModContent.ItemType<BuriedChampionTreasureBag>(), ModContent.ItemType<BuriedChampionTrophy>(), ModContent.ItemType<BuriedEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<FallenBeholder>())
                {
                    Swarm(npc, ModContent.NPCType<FallenBeholder>(), ModContent.ItemType<FallenBeholderTreasureBag>(), ModContent.ItemType<FallenBeholderTrophy>(), ModContent.ItemType<FallenEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<Lich>())
                {
                    Swarm(npc, ModContent.NPCType<Lich>(), ModContent.ItemType<LichTreasureBag>(), ModContent.ItemType<LichTrophy>(), ModContent.ItemType<LichEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<QueenJellyfish>())
                {
                    Swarm(npc, ModContent.NPCType<QueenJellyfish>(), ModContent.ItemType<QueenJellyfishTreasureBag>(), ModContent.ItemType<QueenJellyfishTrophy>(), ModContent.ItemType<JellyfishEnergizer>());
                }
                else if (npc.type == ModContent.NPCType<GraniteEnergyStorm>())
                {
                    Swarm(npc, ModContent.NPCType<GraniteEnergyStorm>(), ModContent.ItemType<GraniteEnergyStormTreasureBag>(), ModContent.ItemType<GraniteEnergyStormTrophy>(), ModContent.ItemType<GraniteEnergizer>());
                }
                //else if (npc.type == ModContent.NPCType<StarScouter>())
                //{
                //    Swarm(npc, ModContent.NPCType<StarScouter>(), ModContent.ItemType<StarScouterTreasureBag>(), ModContent.ItemType<StarScouterTrophy>(), ModContent.ItemType<PerforatorEnergizer>());
                //}
                else if (npc.type == ModContent.NPCType<BoreanStrider>())
                {
                    Swarm(npc, ModContent.NPCType<BoreanStrider>(), ModContent.ItemType<BoreanStriderTreasureBag>(), ModContent.ItemType<BoreanStriderTrophy>(), ModContent.ItemType<BoreanEnergizer>());
                }
				else if (npc.type == ModContent.NPCType<Viscount>())
				{
					Swarm(npc, ModContent.NPCType<Viscount>(), ModContent.ItemType<ViscountTreasureBag>(), ModContent.ItemType<ViscountTrophy>(), ModContent.ItemType<ViscountEnergizer>());
				}
				else if (npc.type == ModContent.NPCType<TheGrandThunderBird>())
                {
                    Swarm(npc, ModContent.NPCType<TheGrandThunderBird>(), ModContent.ItemType<TheGrandThunderBirdTreasureBag>(), ModContent.ItemType<TheGrandThunderBirdTrophy>(), ModContent.ItemType<ThunderEnergizer>());
                }
				else if (npc.type == ModContent.NPCType<ForgottenOne>())
				{
					Swarm(npc, ModContent.NPCType<ForgottenOne>(), ModContent.ItemType<ForgottenOneTreasureBag>(), ModContent.ItemType<ForgottenOneTrophy>(), ModContent.ItemType<ForgottenEnergizer>());
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
            if (SwarmActive && npc.type == ModContent.NPCType<FallenBeholder>())
            {
                npc.dontTakeDamage = false;
            }
            else if (SwarmActive && npc.type == ModContent.NPCType<BuriedChampion>())
            {
                npc.dontTakeDamage = false;
            }
            else if (SwarmActive && npc.type == ModContent.NPCType<GraniteEnergyStorm>())
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
                    Main.npc[spawn].GetGlobalNPC<ShtunThoriumNpcs>().SwarmActive = true;
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
                        Main.npc[i].GetGlobalNPC<ShtunThoriumNpcs>().NoLoot = true;
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