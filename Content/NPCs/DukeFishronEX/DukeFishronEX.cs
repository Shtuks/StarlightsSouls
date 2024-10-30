using System.IO;
using Terraria.ModLoader.IO;
using FargowiltasSouls.Content.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Core.NPCMatching;
using Terraria.Localization;
using ssm.Content.NPCs.Shtuxibus;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Souls;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ssm.Systems;

namespace ssm.Content.NPCs.DukeFishronEX
{
    [AutoloadBossHead]
    public class DukeFishronEX : ModNPC
    {
        public int GeneralTimer;
        public int P3Timer;
        public float endTimeVariance;
        public int attackCount;
        public int EXTornadoTimer;
        public int Counter2;
        Player player => Main.player[NPC.target];
        public Queue<float> attackHistory = new Queue<float>();

        public bool RemovedInvincibility;
        public bool TakeNoDamageOnHit;

        public bool SpectralFishronRandom; //only for spawning projs (server-side only), no mp sync needed

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
        }

        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 120;
            NPC.damage = 1000;
            NPC.defense = 300;
            NPC.value = Item.buyPrice(999999);
            NPC.lifeMax = 160000000;
            if (Main.expertMode)
            {
                this.NPC.damage = 1600;
                this.NPC.lifeMax = 220000000;
            }
            if (Main.masterMode)
            {
                this.NPC.damage = 3100;
                this.NPC.lifeMax = 300000000;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 50f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.timeLeft = NPC.activeTime * 30;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(NPC.localAI[0]);
            writer.Write(NPC.localAI[1]);
            writer.Write(NPC.localAI[2]);
            writer.Write(endTimeVariance);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadSingle();
            NPC.localAI[1] = reader.ReadSingle();
            NPC.localAI[2] = reader.ReadSingle();
            endTimeVariance = reader.ReadSingle();
        }

        public override void AI()
        {
            ShtunNpcs.DukeEX = NPC.whoAmI;

            if (NPC.Distance(Main.LocalPlayer.Center) < 3000f)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<OceanicSealBuff>(), 2);
                Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2);
            }

            ShtunNpcs.DukeEX = NPC.whoAmI;
            NPC.position += NPC.velocity * 0.5f;
            switch ((int)NPC.ai[0])
            {
                case -1: //just spawned
                    JustSpawned();
                    break;

                case 0: //phase 1
                    p1();
                    break;

                case 1: //p1 dash
                    p1dash();
                    break;

                case 2: //p1 bubbles
                    p1bubbles();
                    break;

                case 3: //p1 drop nados
                    p1dropNados();
                    break;

                case 4: //phase 2 transition
                    p2transition();
                    break;

                case 5: //phase 2
                    p2();
                    break;

                case 6: //p2 dash
                    goto case 1;

                case 7: //p2 spin & bubbles
                    p2spinAndBubbles();
                    break;

                case 8: //p2 cthulhunado
                    p2chtulhunado();
                    break;

                case 9: //phase 3 transition
                    p3transition();
                    goto case 4;

                case 10: //phase 3
                    TakeNoDamageOnHit = false;
                    //if (Timer >= 60 + (int)(540.0 * NPC.life / NPC.lifeMax)) //yes that needs to be a double
                    Counter2++;
                    if (Counter2 >= 900)
                    {
                        Counter2 = 0;
                        if (FargoSoulsUtil.HostCheck) //spawn cthulhunado
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, NPC.target + 1);
                    }
                    break;

                case 11: //p3 dash
                    p3dash();
                    goto case 10;

                case 12: //p3 *teleports behind you*
                    teleport();
                    goto case 10;

                default:
                    break;
            }

            if (ShtunNpcs.DukeEX == NPC.whoAmI)// && NPC.ai[0] >= 10 || (NPC.ai[0] == 9 && NPC.ai[2] > 120)) //in phase 3, do this check in all stages
            {
                EXTornadoTimer--;
            }

            if (EXTornadoTimer < 0)
            {
                EXTornadoTimer = 10 * 60;

                SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);

                for (int i = -1; i <= 1; i += 2)
                {
                    int tilePosX = (int)Main.player[NPC.target].Center.X / 16;
                    int tilePosY = (int)Main.player[NPC.target].Center.Y / 16;
                    tilePosX += 75 * i;

                    if (tilePosX < 0 || tilePosX >= Main.maxTilesX || tilePosY < 0 || tilePosY >= Main.maxTilesY)
                        continue;

                    //first move up through solid tiles
                    while (Main.tile[tilePosX, tilePosY].HasUnactuatedTile && Main.tileSolid[Main.tile[tilePosX, tilePosY].TileType])
                    {
                        tilePosY--;
                        if (tilePosX < 0 || tilePosX >= Main.maxTilesX || tilePosY < 0 || tilePosY >= Main.maxTilesY)
                            break;
                    }

                    tilePosY--;

                    //then move down through air until solid tile/platform reached
                    int tilesMovedDown = 0;
                    while (!(Main.tile[tilePosX, tilePosY].HasUnactuatedTile && Main.tileSolidTop[Main.tile[tilePosX, tilePosY].TileType]))
                    {
                        tilePosY++;
                        if (tilePosX < 0 || tilePosX >= Main.maxTilesX || tilePosY < 0 || tilePosY >= Main.maxTilesY)
                            break;
                        if (++tilesMovedDown > 32)
                        {
                            tilePosY -= 28; //give up, reset
                            break;
                        }
                    }

                    tilePosY--;

                    Vector2 spawn = new(tilePosX * 16 + 8, tilePosY * 16 + 8);
                    if (FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, Vector2.UnitX * -i * 6f, ProjectileID.Cthulunado, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 10, 25);
                }
            }
        }

        #region cases
        void JustSpawned()
        {
            if (NPC.ai[2] == 2 && FargoSoulsUtil.HostCheck) //create spell circle
            {
                int ritual1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero,
                    ModContent.ProjectileType<FishronRitual>(), 0, 0f, Main.myPlayer, NPC.lifeMax, NPC.whoAmI, 0);
                if (ritual1 == Main.maxProjectiles) //failed to spawn projectile, abort spawn
                    NPC.active = false;
                SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
            }
            TakeNoDamageOnHit = true;
        }
        void p1()
        {
            if (!RemovedInvincibility)
                NPC.dontTakeDamage = false;
            TakeNoDamageOnHit = false;
            NPC.ai[2]++;
        }
        void p1dash()
        {
            GeneralTimer++;
            if (GeneralTimer > 5)
            {
                GeneralTimer = 0;
                if (FargoSoulsUtil.HostCheck)
                {
                    Vector2 spawnPos = new(NPC.position.X + Main.rand.Next(NPC.width), NPC.position.Y + Main.rand.Next(NPC.height));
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), spawnPos, NPCID.DetonatingBubble);

                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: NPC.SafeDirectionTo(Main.player[NPC.target].Center));
                }
            }
        }
        void p1bubbles()
        {
            if (NPC.ai[2] == 0f && FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, NPC.target + 1);
        }
        void p1dropNados()
        {
            if (NPC.ai[2] == 60f && FargoSoulsUtil.HostCheck)
            {
                const int max = 32;
                float rotation = 2f * (float)Math.PI / max;
                for (int i = 0; i < max; i++)
                {
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: Vector2.Normalize(Vector2.UnitY.RotatedBy(rotation * i)));
                }

                SpawnRazorbladeRing(18, 10f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f / 6), 1f);
            }
        }
        void p2transition()
        {
            RemovedInvincibility = false;
            TakeNoDamageOnHit = true;
            if (NPC.ai[2] == 1 && FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FishronRitual>(), 0, 0f, Main.myPlayer, NPC.lifeMax / 4, NPC.whoAmI);
            if (NPC.ai[2] >= 114)
            {
                GeneralTimer++;
                if (GeneralTimer > 6) //display healing effect
                {
                    GeneralTimer = 0;
                    int heal = (int)(NPC.lifeMax * Main.rand.NextFloat(0.1f, 0.12f));
                    NPC.life += heal;
                    int max = NPC.ai[0] == 9 && !WorldSavingSystem.MasochistModeReal ? NPC.lifeMax / 2 : NPC.lifeMax;
                    if (NPC.life > max)
                        NPC.life = max;
                    CombatText.NewText(NPC.Hitbox, CombatText.HealLife, heal);
                }
            }
        }
        void p2()
        {
            if (!RemovedInvincibility)
                NPC.dontTakeDamage = false;
            TakeNoDamageOnHit = false;
            NPC.ai[2]++;
        }
        void p2spinAndBubbles()
        {
            NPC.position -= NPC.velocity * 0.5f;
            GeneralTimer++;
            if (GeneralTimer > 1)
            {
                //Counter0 = 0;
                if (FargoSoulsUtil.HostCheck)
                {
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                        ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: Vector2.Normalize(NPC.velocity.RotatedBy(Math.PI / 2)));
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                        ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: Vector2.Normalize(NPC.velocity.RotatedBy(-Math.PI / 2)));
                }
            }
        }
        void p2chtulhunado()
        {
            if (FargoSoulsUtil.HostCheck && NPC.ai[2] == 60)
            {
                Vector2 spawnPos = Vector2.UnitX * NPC.direction;
                spawnPos = spawnPos.RotatedBy(NPC.rotation);
                spawnPos *= NPC.width + 20f;
                spawnPos /= 2f;
                spawnPos += NPC.Center;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, NPC.direction * 2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, NPC.direction * -2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, 0f, 2f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                SpawnRazorbladeRing(12, 12.5f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f / 6), 0.75f);
                SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f / 6), -2f);
            }
        }
        void p3transition()
        {
            if (NPC.ai[2] == 1f)
            {
                for (int i = 0; i < NPC.buffImmune.Length; i++)
                    NPC.buffImmune[i] = true;
                while (NPC.buffTime[0] != 0)
                    NPC.DelBuff(0);
                NPC.defDamage = (int)(NPC.defDamage * 1.2f);
            }
        }
        void p3dash()
        {
            if (GeneralTimer > 2)
                GeneralTimer = 2;
            if (GeneralTimer == 2)
            {
                //Counter0 = 0;
                if (FargoSoulsUtil.HostCheck)
                {
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                        ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: Vector2.Normalize(NPC.velocity.RotatedBy(Math.PI / 2)));
                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                        ModContent.NPCType<DetonatingBubbleEX>(),
                        velocity: Vector2.Normalize(NPC.velocity.RotatedBy(-Math.PI / 2)));
                }
            }
        }
        void teleport()
        {
            if (NPC.ai[2] == 15f)
            {
                if (FargoSoulsUtil.HostCheck)
                {
                    SpawnRazorbladeRing(5, 9f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f / 6), 1f, true);
                    SpawnRazorbladeRing(5, 9f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 4f / 6), -0.5f, true);
                }
            }
            else if (NPC.ai[2] == 16f)
            {
                if (FargoSoulsUtil.HostCheck)
                {
                    Vector2 spawnPos = Vector2.UnitX * NPC.direction; //GODLUL
                    spawnPos = spawnPos.RotatedBy(NPC.rotation);
                    spawnPos *= NPC.width + 20f;
                    spawnPos /= 2f;
                    spawnPos += NPC.Center;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, NPC.direction * 2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, NPC.direction * -2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                    const int max = 24;
                    float rotation = 2f * (float)Math.PI / max;
                    for (int i = 0; i < max; i++)
                    {
                        FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                            ModContent.NPCType<DetonatingBubbleEX>(),
                            velocity: Vector2.Normalize(NPC.velocity.RotatedBy(rotation * i)));
                    }
                }
            }
        }
        #endregion

        #region help funcs
        private void MovementY(float targetY, float speedModifier)
        {
            if (NPC.Center.Y < targetY)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.Y) > 24)
                NPC.velocity.Y = 24 * Math.Sign(NPC.velocity.Y);
        }
        void Movement(Vector2 target, float speed, bool fastX = true, bool obeySpeedCap = true)
        {
            float turnaroundModifier = 1f;
            float maxSpeed = 24;
            speed *= 2;
            turnaroundModifier *= 2f;
            maxSpeed *= 1.5f;

            if (Math.Abs(NPC.Center.X - target.X) > 10)
            {
                if (NPC.Center.X < target.X)
                {
                    NPC.velocity.X += speed;
                    if (NPC.velocity.X < 0)
                        NPC.velocity.X += speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
                else
                {
                    NPC.velocity.X -= speed;
                    if (NPC.velocity.X > 0)
                        NPC.velocity.X -= speed * (fastX ? 2 : 1) * turnaroundModifier;
                }
            }
            if (NPC.Center.Y < target.Y)
            {
                NPC.velocity.Y += speed;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speed * 2 * turnaroundModifier;
            }
            else
            {
                NPC.velocity.Y -= speed;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speed * 2 * turnaroundModifier;
            }

            if (obeySpeedCap)
            {
                if (Math.Abs(NPC.velocity.X) > maxSpeed)
                    NPC.velocity.X = maxSpeed * Math.Sign(NPC.velocity.X);
                if (Math.Abs(NPC.velocity.Y) > maxSpeed)
                    NPC.velocity.Y = maxSpeed * Math.Sign(NPC.velocity.Y);
            }
        }
        void SpawnRazorbladeRing(int max, float speed, int damage, float rotationModifier, bool reduceTimeleft = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = Main.player[NPC.target].Center - NPC.Center;
            vel.Normalize();
            vel *= speed;
            int type = ModContent.ProjectileType<RazorbladeTyphoon>();
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * NPC.spriteDirection, speed);
                if (reduceTimeleft && p < 1000)
                    Main.projectile[p].timeLeft /= 2;
            }
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }

        void ChooseNextAttack(params int[] args)
        {
            float buffer = NPC.ai[0] + 1;
            NPC.ai[0] = 68;
            NPC.ai[1] = 0;
            NPC.ai[2] = buffer;
            NPC.ai[3] = 0;
            NPC.localAI[0] = 0;
            NPC.localAI[1] = 0;
            NPC.localAI[2] = 0;
            NPC.netUpdate = true;
            bool useRandomizer = NPC.localAI[3] >= 3 && (Main.rand.NextFloat(0.8f) + 0.2f > (float)Math.Pow((float)NPC.life / NPC.lifeMax, 2));

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Queue<float> recentAttacks = new Queue<float>(attackHistory); //copy of attack history that i can remove elements from freely

                //if randomizer, start with a random attack, else use the previous state + 1 as starting attempt BUT DO SOMETHING ELSE IF IT'S ALREADY USED
                if (useRandomizer)
                    NPC.ai[2] = Main.rand.Next(args);

                //Main.NewText(useRandomizer ? "(Starting with random)" : "(Starting with regular next attack)");

                while (recentAttacks.Count > 0)
                {
                    bool foundAttackToUse = false;

                    for (int i = 0; i < 5; i++) //try to get next attack that isnt in this queue
                    {
                        if (!recentAttacks.Contains(NPC.ai[2]))
                        {
                            foundAttackToUse = true;
                            break;
                        }
                        NPC.ai[2] = Main.rand.Next(args);
                    }

                    if (foundAttackToUse)
                        break;

                    recentAttacks.Dequeue();
                }
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int maxMemory = 10;

                if (attackCount++ > maxMemory * 1.25) //after doing this many attacks, shorten queue so i can be more random again
                {
                    attackCount = 0;
                    maxMemory /= 4;
                }

                attackHistory.Enqueue(NPC.ai[2]);
                while (attackHistory.Count > maxMemory)
                    attackHistory.Dequeue();
            }

            endTimeVariance = Main.rand.NextFloat();
        }
        #endregion

        #region other boss' funcs
        public override void PostAI()
        {
            if (NPC.ai[0] >= 9) //phase 3
                NPC.damage = Math.Max(NPC.damage, (int)(NPC.defDamage * 1.3));

            if (NPC.ai[0] > 9)
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = true;
            }

            NPC.defense = Math.Max(NPC.defense, NPC.defDefense);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            //target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600);
            target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600);
            target.AddBuff(BuffID.Rabies, 3600);
            target.FargoSouls().MaxLifeReduction += 50;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 20 * 60);
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (TakeNoDamageOnHit)
                modifiers.Null();
        }

        public override bool CheckDead()
        {
            if (NPC.ai[0] <= 9)
            {
                NPC.life = 1;
                NPC.active = true;
                if (FargoSoulsUtil.HostCheck) //something about wack ass MP
                {
                    NPC.netUpdate = true;
                    NPC.dontTakeDamage = true;
                    RemovedInvincibility = true;
                }
                return false;
            }
            return false;
        }
        #endregion
    }
}
