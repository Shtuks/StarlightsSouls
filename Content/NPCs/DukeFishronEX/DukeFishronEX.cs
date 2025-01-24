using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.DukeFishronEX
{
    [AutoloadBossHead]
    public class DukeFishronEX : ModNPC
    {
        public int GeneralTimer;
        public int P3Timer;
        public int EXTornadoTimer;

        public bool RemovedInvincibility;
        public bool TakeNoDamageOnHit;

        public bool SpectralFishronRandom;


        public override void SendExtraAI(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(GeneralTimer);
            binaryWriter.Write(P3Timer);
            binaryWriter.Write(EXTornadoTimer);
            binaryWriter.Write(RemovedInvincibility);
            binaryWriter.Write(TakeNoDamageOnHit);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            GeneralTimer = reader.Read();
            P3Timer = reader.Read();
            EXTornadoTimer = reader.Read();
            RemovedInvincibility = reader.ReadBoolean();
            TakeNoDamageOnHit = reader.ReadBoolean();
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 202;
            NPC.height = 165;
            NPC.damage = 1000;
            NPC.value = Item.buyPrice(999999);
            NPC.lifeMax = 8000000;
            if (Main.expertMode)
            {
                NPC.damage = 2000;
                NPC.lifeMax = 9000000;
            }
            if (Main.masterMode)
            {
                NPC.damage = 3000;
                NPC.lifeMax = 10000000;
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

        public override void AI()
        {
            ShtunNpcs.DukeEX = NPC.whoAmI;

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

            void EnrageDust()
            {
                int num22 = 7;
                for (int index1 = 0; index1 < num22; ++index1)
                {
                    int d;
                    if (NPC.velocity.Length() > 10)
                    {
                        Vector2 vector2_1 = (Vector2.Normalize(NPC.velocity) * new Vector2((NPC.width + 50) / 2f, NPC.height) * 0.75f).RotatedBy((index1 - (num22 / 2 - 1)) * Math.PI / num22, new Vector2()) + NPC.Center;
                        Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - (float)Math.PI / 2).ToRotationVector2() * Main.rand.Next(3, 8);
                        d = Dust.NewDust(vector2_1 + vector2_2, 0, 0, DustID.GemSapphire, vector2_2.X * 2f, vector2_2.Y * 2f, 0, default, 1.7f);
                    }
                    else
                    {
                        d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemSapphire, NPC.velocity.X * 2f, NPC.velocity.Y * 2f, 0, default, 1.7f);
                    }
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity /= 4f;
                    Main.dust[d].velocity -= NPC.velocity;
                }
            }


            #region duke ex ai

            NPC.FargoSouls().MutantNibble = false;
            NPC.FargoSouls().LifePrevious = int.MaxValue; //cant stop the healing

            while (NPC.buffType[0] != 0)
                NPC.DelBuff(0);

            if (NPC.Distance(Main.LocalPlayer.Center) < 3000f)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<OceanicSealBuff>(), 2);
                Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2);
            }

            NPC.position += NPC.velocity * 0.5f;

            switch ((int)NPC.ai[0])
            {
                case -1:
                    if (NPC.ai[2] == 2 && FargoSoulsUtil.HostCheck)
                    {
                        int ritual1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero,
                            ModContent.ProjectileType<FishronRitual>(), 0, 0f, Main.myPlayer, NPC.lifeMax, NPC.whoAmI, 0);
                        if (ritual1 == Main.maxProjectiles)
                            NPC.active = false;
                        SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
                    }
                    TakeNoDamageOnHit = true;
                    break;

                case 0: //phase 1
                    if (!RemovedInvincibility)
                        NPC.dontTakeDamage = false;
                    TakeNoDamageOnHit = false;
                    NPC.ai[2]++;
                    break;

                case 1: //p1 dash
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
                    break;

                case 2:
                    if (NPC.ai[2] == 0f && FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, NPC.target + 1);
                    break;

                case 3: 
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
                    break;

                case 4:
                    RemovedInvincibility = false;
                    TakeNoDamageOnHit = true;
                    if (NPC.ai[2] == 1 && FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FishronRitual>(), 0, 0f, Main.myPlayer, NPC.lifeMax / 4, NPC.whoAmI);
                    if (NPC.ai[2] >= 114)
                    {
                        GeneralTimer++;
                        if (GeneralTimer > 6) 
                        {
                            GeneralTimer = 0;
                            int heal = (int)(NPC.lifeMax * Main.rand.NextFloat(0.1f, 0.12f));
                            NPC.life += heal;
                            int max = NPC.ai[0] == 9 ? NPC.lifeMax / 2 : NPC.lifeMax;
                            if (NPC.life > max)
                                NPC.life = max;
                            CombatText.NewText(NPC.Hitbox, CombatText.HealLife, heal);
                        }
                    }
                    break;

                case 5: //phase 2
                    if (!RemovedInvincibility)
                        NPC.dontTakeDamage = false;
                    TakeNoDamageOnHit = false;
                    NPC.ai[2]++;
                    break;

                case 6:
                    goto case 1;

                case 7: 
                    NPC.position -= NPC.velocity * 0.5f;
                    GeneralTimer++;
                    if (GeneralTimer > 1)
                    {
                        if (FargoSoulsUtil.HostCheck)
                        {
                            FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                                velocity: Vector2.Normalize(NPC.velocity.RotatedBy(Math.PI / 2)));
                            FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                                velocity: Vector2.Normalize(NPC.velocity.RotatedBy(-Math.PI / 2)));
                        }
                    }
                    break;

                case 8:
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
                    break;

                case 9:
                    if (NPC.ai[2] == 1f)
                    {
                        for (int i = 0; i < NPC.buffImmune.Length; i++)
                            NPC.buffImmune[i] = true;
                        while (NPC.buffTime[0] != 0)
                            NPC.DelBuff(0);
                        NPC.defDamage = (int)(NPC.defDamage * 1.2f);
                    }
                    goto case 4;

                case 10:
                    TakeNoDamageOnHit = false;
                    break;

                case 11: 
                    if (GeneralTimer > 2)
                        GeneralTimer = 2;
                    if (GeneralTimer == 2)
                    {
                        if (FargoSoulsUtil.HostCheck)
                        {
                            FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                                velocity: Vector2.Normalize(NPC.velocity.RotatedBy(Math.PI / 2)));
                            FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center, ModContent.NPCType<DetonatingBubbleEX>(),
                                velocity: Vector2.Normalize(NPC.velocity.RotatedBy(-Math.PI / 2)));
                        }
                    }
                    goto case 10;

                case 12:
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
                            Vector2 spawnPos = Vector2.UnitX * NPC.direction;
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
                    goto case 10;

                default:
                    break;
            }

            #endregion


            NPC.position += NPC.velocity * 0.25f;
            const int spectralFishronDelay = 3;
            switch ((int)NPC.ai[0])
            {
                case -1:
                    break;

                case 0: 
                    if (!RemovedInvincibility)
                        NPC.dontTakeDamage = false;
                    if (!Main.player[NPC.target].ZoneBeach)
                        NPC.ai[2]++;
                    break;

                case 1: 
                    if (++GeneralTimer > 5)
                    {
                        GeneralTimer = 0;

                        if (FargoSoulsUtil.HostCheck)
                        {
                            Vector2 spawnPos = new(NPC.position.X + Main.rand.Next(NPC.width), NPC.position.Y + Main.rand.Next(NPC.height));
                            FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), spawnPos, NPCID.DetonatingBubble);
                        }
                    }
                    break;

                case 2:
                    if (NPC.ai[2] == 0f && FargoSoulsUtil.HostCheck)
                    {
                        bool random = Main.rand.NextBool(); 
                        for (int j = -1; j <= 1; j++) 
                        {
                            if (j == 0)
                                continue;

                            Vector2 offset = random ? Vector2.UnitY * -450f * j : Vector2.UnitX * 600f * j;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                        }
                    }
                    break;

                case 3:
                    if (NPC.ai[2] == 60f && FargoSoulsUtil.HostCheck)
                    {
                        SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 1f);
                    }
                    break;

                case 4:
                    break;

                case 5: 
                    if (!RemovedInvincibility)
                        NPC.dontTakeDamage = false;
                    if (!Main.player[NPC.target].ZoneBeach)
                        NPC.ai[2]++;
                    break;

                case 6:
                    goto case 1;

                case 7:
                    NPC.position -= NPC.velocity * 0.25f;

                    if (++GeneralTimer > 1)
                    {
                        GeneralTimer = 0;
                        if (FargoSoulsUtil.HostCheck)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2),
                                ModContent.ProjectileType<RazorbladeTyphoon2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, .03f);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 0.014035f * Vector2.Normalize(NPC.velocity).RotatedBy(-Math.PI / 2),
                                ModContent.ProjectileType<RazorbladeTyphoon2>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, .08f);
                        }
                    }
                    break;

                case 8:
                    {
                        const int delayForTornadoSpawn = 60;

                        if (NPC.ai[2] == 0f)
                        {
                            SpectralFishronRandom = Main.rand.NextBool();
                        }
                        if (NPC.ai[2] >= delayForTornadoSpawn && NPC.ai[2] % spectralFishronDelay == 0 && NPC.ai[2] <= spectralFishronDelay * 2 + delayForTornadoSpawn)
                        {
                            for (int j = -1; j <= 1; j += 2)
                            {
                                int max = (int)(NPC.ai[2] - delayForTornadoSpawn) / spectralFishronDelay;
                                for (int i = -max; i <= max; i++) 
                                {
                                    if (Math.Abs(i) != max)
                                        continue;
                                    Vector2 offset = SpectralFishronRandom ? Vector2.UnitY.RotatedBy(MathHelper.PiOver2 / 3 / 3 * i) * -500f * j : Vector2.UnitX.RotatedBy(MathHelper.PiOver2 / 3 / 3 * i) * 500f * j;
                                    if (FargoSoulsUtil.HostCheck)
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                                }
                            }
                        }

                        if (NPC.ai[2] == delayForTornadoSpawn && FargoSoulsUtil.HostCheck)
                        {
                            Vector2 spawnPos = Vector2.UnitX * NPC.direction;
                            spawnPos = spawnPos.RotatedBy(NPC.rotation);
                            spawnPos *= NPC.width + 20f;
                            spawnPos /= 2f;
                            spawnPos += NPC.Center;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos.X, spawnPos.Y, 0f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                            SpawnRazorbladeRing(12, 12.5f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0.75f);
                            SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 2f * NPC.direction);
                        }
                    }
                    break;

                case 9:
                    break;
                    

                case 10:
                    if (!Main.player[NPC.target].ZoneBeach || NPC.ai[3] > 5 && NPC.ai[3] < 8)
                    {
                        NPC.position += NPC.velocity;
                        NPC.ai[2]++;
                        EnrageDust();
                    }

                    if (NPC.ai[3] == 1)
                    {
                        if (P3Timer == 0)
                        {
                            SpectralFishronRandom = Main.rand.NextBool();
                        }

                        if (++P3Timer < 150)
                        {
                            void Checks(int delay)
                            {
                                int max = 6;
                                int P3TimerOffset = P3Timer - 30;
                                if (P3TimerOffset >= delay && P3TimerOffset < spectralFishronDelay * max + delay && P3TimerOffset % spectralFishronDelay == 0 && FargoSoulsUtil.HostCheck)
                                {
                                    Vector2 offset = 450 * -Vector2.UnitY.RotatedBy(MathHelper.TwoPi / max * (P3TimerOffset / spectralFishronDelay + Main.rand.NextFloat(0.5f)));
                                    if (FargoSoulsUtil.HostCheck)
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                                }
                            }

                            NPC.ai[2] = 0; 
                            NPC.position.Y -= NPC.velocity.Y * 0.5f;

                            Checks(0);
                        }
                    }
                    else if (NPC.ai[3] == 5)
                    {
                        if (NPC.ai[2] == 0)
                            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                        NPC.ai[2] -= 0.5f;
                        NPC.velocity *= 0.5f;
                        EnrageDust();
                    }

                    break;

                case 11: 
                    if (!Main.player[NPC.target].ZoneBeach || NPC.ai[3] >= 5)
                    {
                        if (NPC.ai[2] == 0 && !Main.dedServ)
                            SoundEngine.PlaySound(new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster70"), NPC.Center);

                        if (Main.player[NPC.target].ZoneBeach)
                        {
                            NPC.position += NPC.velocity * 0.5f;
                        }
                        else 
                        {
                            NPC.position += NPC.velocity;
                            NPC.ai[2]++;

                            int playerTileX = (int)Main.player[NPC.target].Center.X / 16;
                            bool customBeach = playerTileX < 500 || playerTileX > Main.maxTilesX - 500;
                            if (!customBeach)
                                EXTornadoTimer -= 2; 
                        }
                        EnrageDust();
                    }

                    P3Timer = 0;
                    if (--GeneralTimer < 0)
                    {
                        GeneralTimer = 2;
                        if (FargoSoulsUtil.HostCheck)
                        {
                            if (NPC.ai[3] == 2 || NPC.ai[3] == 3)
                            {
                                for (int i = -1; i <= 1; i += 2)
                                {
                                    for (int j = 1; j <= 2; j++)
                                    {
                                        FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                                            ModContent.NPCType<DetonatingBubbleNPC>(),
                                            velocity: Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2 * i) * j * 0.5f);
                                    }
                                }
                            }

                            if (!Main.player[NPC.target].ZoneBeach)
                            {
                                float range = MathHelper.ToRadians(Main.rand.NextFloat(1f, 15f));
                                for (int i = -1; i <= 1; i++)
                                {
                                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 8f * NPC.SafeDirectionTo(Main.player[NPC.target].Center).RotatedBy(range * i),
                                        ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                                    if (p != Main.maxProjectiles)
                                        Main.projectile[p].timeLeft = 90;
                                }

                                for (int i = -1; i <= 1; i += 2)
                                {
                                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 8f * Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2 * i),
                                        ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                                    if (p != Main.maxProjectiles)
                                        Main.projectile[p].timeLeft = 90;
                                }
                            }
                            else if (Main.expertMode)
                            {
                                for (int i = -1; i <= 1; i += 2)
                                {
                                    FargoSoulsUtil.NewNPCEasy(NPC.GetSource_FromThis(), NPC.Center,
                                        ModContent.NPCType<DetonatingBubbleNPC>(),
                                        velocity: 1.5f * Vector2.Normalize(NPC.velocity).RotatedBy(Math.PI / 2 * i));
                                }
                            }
                        }
                    }
                    break;

                case 12:
                    if (!Main.player[NPC.target].ZoneBeach || NPC.ai[3] > 5 && NPC.ai[3] < 8)
                    {
                        if (!Main.player[NPC.target].ZoneBeach)
                            NPC.position += NPC.velocity;
                        NPC.ai[2]++;
                        EnrageDust();
                    }

                    GeneralTimer = 0;
                    if (NPC.ai[2] == 15f)
                    {
                        SpawnRazorbladeRing(6, 8f, FargoSoulsUtil.ScaledProjectileDamage(NPC.damage), -0.75f);
                    }
                    else if (NPC.ai[2] == 16f)
                    {
                        const int max = 5;
                        for (int j = -max; j <= max; j++)
                        {
                            Vector2 vel = NPC.DirectionFrom(Main.player[NPC.target].Center).RotatedBy(MathHelper.PiOver2 / max * j);
                            if (FargoSoulsUtil.HostCheck)
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                        }
                    }
                    break;

                default:
                    break;
            }

            EXTornadoTimer--;

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
                    while (Main.tile[tilePosX, tilePosY].HasUnactuatedTile && Main.tileSolid[Main.tile[tilePosX, tilePosY].TileType])
                    {
                        tilePosY--;
                        if (tilePosX < 0 || tilePosX >= Main.maxTilesX || tilePosY < 0 || tilePosY >= Main.maxTilesY)
                            break;
                    }

                    tilePosY--;

                    int tilesMovedDown = 0;
                    while (!(Main.tile[tilePosX, tilePosY].HasUnactuatedTile && Main.tileSolidTop[Main.tile[tilePosX, tilePosY].TileType]))
                    {
                        tilePosY++;
                        if (tilePosX < 0 || tilePosX >= Main.maxTilesX || tilePosY < 0 || tilePosY >= Main.maxTilesY)
                            break;
                        if (++tilesMovedDown > 32)
                        {
                            tilePosY -= 28; 
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

        protected static void NetSync(NPC npc, bool onlySendFromServer = true)
        {
            if (onlySendFromServer && Main.netMode != NetmodeID.Server)
                return;

            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
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
                if (FargoSoulsUtil.HostCheck) 
                {
                    NPC.netUpdate = true;
                    NPC.dontTakeDamage = true;
                    RemovedInvincibility = true;
                    NetSync(NPC);
                }

                for (int index1 = 0; index1 < 100; ++index1)
                {
                    int index2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 2f);
                    Main.dust[index2].position.X += Main.rand.Next(-20, 21);
                    Main.dust[index2].position.Y += Main.rand.Next(-20, 21);
                    Dust dust = Main.dust[index2];
                    dust.velocity *= 0.5f;
                    Main.dust[index2].scale *= 1f + Main.rand.Next(50) * 0.01f;
                    if (Main.rand.NextBool())
                    {
                        Main.dust[index2].scale *= 1f + Main.rand.Next(50) * 0.01f;
                        Main.dust[index2].noGravity = true;
                    }
                }
                for (int i = 0; i < 5; i++) 
                {
                    int index3 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                    Main.gore[index3].scale = 2f;
                    Main.gore[index3].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index3].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index3].velocity *= 0.5f;

                    int index4 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                    Main.gore[index4].scale = 2f;
                    Main.gore[index4].velocity.X = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index4].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index4].velocity *= 0.5f;

                    int index5 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                    Main.gore[index5].scale = 2f;
                    Main.gore[index5].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index5].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index5].velocity *= 0.5f;

                    int index6 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                    Main.gore[index6].scale = 2f;
                    Main.gore[index6].velocity.X = 1.5f - Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index6].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index6].velocity *= 0.5f;

                    int index7 = Gore.NewGore(NPC.GetSource_FromThis(), NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                    Main.gore[index7].scale = 2f;
                    Main.gore[index7].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index7].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
                    Main.gore[index7].velocity *= 0.5f;
                }

                return false;
            }
            else
            {
                if (ShtunNpcs.DukeEX == NPC.whoAmI)
                {
                    WorldSavingSystem.DownedFishronEX = true;
                }
            }

            return base.CheckDead();
        }
    }
}
