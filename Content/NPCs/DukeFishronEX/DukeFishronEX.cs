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
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
        
        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 156;
            NPC.damage = 1000;
            NPC.value = Item.buyPrice(999999);
            NPC.lifeMax = 100000000;
            
            if (Main.expertMode)
            {
                NPC.damage = 2000;
                NPC.lifeMax = 200000000;
            }
            
            if (Main.masterMode)
            {
                NPC.damage = 3000;
                NPC.lifeMax = 300000000;
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

            NPC.ai[0] = -1;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            GeneralTimer = 0;
            P3Timer = 0;
        }

        public override void AI()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText($"Phase: {NPC.ai[0]}, AI[2]: {NPC.ai[2]}, GeneralTimer: {GeneralTimer}, EXTornadoTimer: {EXTornadoTimer}");
            }

            Player target = Main.player[NPC.target];
            if (!target.active || target.dead)
            {
                NPC.TargetClosest();
                target = Main.player[NPC.target];
            }

            ShtunNpcs.DukeEX = NPC.whoAmI;
            EXTornadoTimer--;

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
                    NPC.ai[2]++;
                    if (NPC.ai[2] > 60)
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[2] = 0;
                        NPC.netUpdate = true; 
                    }
                    break;

                case 0: 
                    if (!RemovedInvincibility)
                        NPC.dontTakeDamage = false;
                    TakeNoDamageOnHit = false;
                    NPC.ai[2]++;
                    NPC.ai[0]++;
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
                                velocity: NPC.SafeDirectionTo(target.Center));
                        }
                    }
                    if (GeneralTimer > 120)
                    {
                        NPC.ai[0]++;
                        NPC.netUpdate = true;
                    }
                    break;

                case 2:
                    if (NPC.ai[2] == 0f && FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, NPC.target + 1);
                    if (GeneralTimer > 60)
                    {
                        NPC.ai[0]++;
                        NPC.netUpdate = true;
                    }
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
                    if (GeneralTimer > 120)
                    {
                        NPC.ai[0]++;
                        NPC.netUpdate = true;
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
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawn, Vector2.UnitX * -i * 6f, ProjectileID.Cthulunado, FargoSoulsUtil.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, 10, 25);
                }
            }
        }

        public override void PostAI()
        {
            if (NPC.ai[0] >= 9)
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
            target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600);
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
                //if (FargoSoulsUtil.HostCheck) 
                //{
                    NPC.netUpdate = true;
                    NPC.dontTakeDamage = true;
                    RemovedInvincibility = true;
                //    NetSync(NPC);
                //}

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

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                    NPC.frame.Y = 0;
            }
        }

    }
}
