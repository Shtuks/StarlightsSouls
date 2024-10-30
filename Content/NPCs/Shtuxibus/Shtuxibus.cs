//nice code(no)
using System.Linq;
using FargowiltasCrossmod.Content.Calamity.Bosses.SlimeGod;
using FargowiltasCrossmod.Content.Common.Projectiles;
using FargowiltasCrossmod.Core;
using FargowiltasCrossmod.Core.Calamity.Systems;
using FargowiltasCrossmod.Core.Common;
using FargowiltasSouls;
using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Core.Globals;
using ssm.Content.Projectiles.Deathrays;
using ssm.Content.Buffs;
using ssm;
using ssm.Content.Tiles;
using ssm.Systems;
using ssm.Content.NPCs;
using ssm.Content.Items.Accessories;
using ssm.Content.Projectiles.Shtuxibus;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using CalamityMod.CalPlayer;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using ssm.Content.NPCs.Shtuxibus.CalAttacks;
using Terraria.ID;
using FargowiltasSouls.Content.Projectiles;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria.Audio;
using ssm.Content.Items.Consumables;
using ModCompatibility = ssm.Core.ModCompatibility;
using Fargowiltas;
using ssm.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Content.Projectiles.Shtuxibus.Cal;

namespace ssm.Content.NPCs.Shtuxibus
{
    [AutoloadBossHead]
    public class Shtuxibus : ModNPC
    {
        Player player => Main.player[NPC.target];
        public bool playerInvulTriggered;
        public bool Aura;
        const int MUTANT_SWORD_SPACING2 = 80;
        const int MUTANT_SWORD_MAX2 = 12;
        const int MUTANT_SWORD_SPACING = 80;
        const int MUTANT_SWORD_MAX = 12;
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        bool spawned;
        private int damageTotal = 0;
        internal int dpsCap = WorldSaveSystem.downedShtuxibus ? 5000000 : 3500000;
        public int ritualProj, spriteProj, ringProj;
        private bool droppedSummon = false;
        public bool SparkAtaackUsing;
        public bool MutantRitualActive = false;
        public bool Phraze1;
        private int Bolts = 24;
        public float[] NpcaiFC = new float[4];
        public float[] NpclocalaiFC = new float[4];
        public bool INPHASE3;
        Vector2 targetPos;
        public bool INPHASE2;
        public int Timer = 0;
        public int Counter = 0;
        float epicMe;
        public Queue<float> attackHistory = new Queue<float>();
        public int attackCount;
        public static readonly Color textColor = Color.Green;
        public static readonly Color textColor2 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        public float endTimeVariance;
        public static int imtrydomove;
        public bool ShouldDrawAura;

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NoMultiplayerSmoothingByType[NPC.type] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "ssm/Assets/Textures/Bestiary/Shtuxibus_Preview",
                PortraitScale = 0.6f,
                PortraitPositionYOverride = 0f,
            };
        }

        public override void SetDefaults()
        {
            NPC.BossBar = ModContent.GetInstance<ShtuxibusBar>();
            NPC.width = 120;
            NPC.height = 120;
            NPC.damage = 5000;
            NPC.value = Item.buyPrice(999999);
            NPC.lifeMax = 450000000;
            if (Main.expertMode)
            {
                this.NPC.damage = 7000;
                this.NPC.lifeMax = 570000000;
            }
            if (Main.masterMode)
            {
                this.NPC.damage = 10000;
                this.NPC.lifeMax = 745000000;
            }

            if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                if ((bool)kal.Call(new object[3]{
                    (object) "GetDifficultyActive",
                    (object) "revengeance",
                    (object) true
                    }) && Main.expertMode)
                {
                    this.NPC.lifeMax = 745000000;
                    this.NPC.damage = 7000;
                }
                if ((bool)kal.Call(new object[3]{
                    (object) "GetDifficultyActive",
                    (object) "death",
                    (object) true
                    }) && Main.expertMode)
                {
                    this.NPC.lifeMax = 1450000000;
                    this.NPC.damage = 10000;
                }
                if ((bool)kal.Call(new object[3]{
                    (object) "GetDifficultyActive",
                    (object) "revengeance",
                    (object) true
                    }) && Main.masterMode)
                {
                    this.NPC.lifeMax = 1570000000;
                    this.NPC.damage = 10000;
                }
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
        public override bool CanHitPlayer(Player target, ref int CooldownSlot)
        {
            CooldownSlot = 1;
            return NPC.Distance(ShtunUtils.ClosestPointInHitbox(target, NPC.Center)) < Player.defaultHeight && NPC.ai[0] > -1;
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
            FixHealth();
            DeleteSusItems();
            ssm.amiactive = true;
            this.damageTotal -= this.dpsCap;
            if (this.damageTotal < 0)
            {
                this.damageTotal = 0;
            }
            ShtunNpcs.Shtuxibus = NPC.whoAmI;
            NPC.dontTakeDamage = NPC.ai[0] < 0; //invul in p3
            ShouldDrawAura = false;
            ManageAurasAndPreSpawn();
            ManageNeededProjectiles();
            NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;
            bool drainLifeInP3 = true;
            Vector2 targetPos;
            switch ((int)NPC.ai[0])
            {
                #region phase 1
                case 0: SpearTossDirectP1AndChecks(); break;
                case 1: OkuuSpheresP1(); break;
                case 2: PrepareTrueEyeDiveP1(); break;
                case 3: TrueEyeDive(); break;
                case 4: PrepareSpearDashDirectP1(); break;
                case 5: SpearDashDirectP1(); break;
                case 6: WhileDashingP1(); break;
                case 7: ApproachForNextAttackP1(); break;
                case 8: VoidRaysP1(); break;
                case 9: VoidRaysP1(); break;
                #endregion
                #region phase 2
                case 10: Phase2Transition(); break;
                case 11: ApproachForNextAttackP2(); break;
                case 12: VoidRaysP2(); break;
                case 13: PrepareSpearDashPredictiveP2(); break;
                case 14: SpearDashPredictiveP2(); break;
                case 15: WhileDashingP2(); break;
                case 16: goto case 11; //approach for bullet hell
                case 17: BoundaryBulletHellP2(); break;
                case 18: YharonBH(); break;
                case 19: PillarDunk(); break;
                case 20: //ZA WARUDO
                    targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500;
                    if (NPC.ai[1] < 130 || (NPC.Distance(player.Center) > 200 && NPC.Distance(player.Center) < 600))
                    {
                        NPC.velocity *= 0.97f;
                    }
                    else if (NPC.Distance(targetPos) > 50)
                    {
                        MovementERI(targetPos, 0.6f, 32f);
                        NPC.position += player.velocity / 4f;
                    }

                    if (NPC.ai[1] >= 10) //for timestop visual
                    {

                    }

                    if (NPC.ai[1] == 10)
                    {
                        NPC.localAI[0] = Main.rand.NextFloat(2 * (float)Math.PI);

                    }
                    else if (NPC.ai[1] < 210)
                    {
                        int duration = 60 + Math.Max(2, 210 - (int)NPC.ai[1]);

                        if (Main.LocalPlayer.active && !Main.LocalPlayer.dead)
                        {
                            Main.LocalPlayer.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, duration);
                            Main.LocalPlayer.AddBuff(BuffID.ChaosState, 300); //no cheesing this attack
                        }

                        for (int i = 0; i < Main.maxProjectiles; i++)
                        {
                            if (Main.projectile[i].active && !Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune)
                                Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFrozen = duration;
                        }


                        if (NPC.ai[1] < 130 && ++NPC.ai[2] > 12)
                        {
                            NPC.ai[2] = 0;

                            bool altAttack = NPC.localAI[2] != 0;

                            int baseDistance = 300; //altAttack ? 500 : 400;
                            float offset = altAttack ? 250f : 150f;
                            float speed = altAttack ? 4f : 2.5f;
                            int damage = ShtunUtils.ScaledProjectileDamage(NPC.damage); //altAttack ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 2 / 7 ): ShtunUtils.ScaledProjectileDamage(NPC.damage);

                            if (NPC.ai[1] < 130 - 45 || !altAttack)
                            {
                                if (altAttack && NPC.ai[3] % 2 == 0) //emode p2, asgore rings
                                {
                                    float radius = baseDistance + NPC.ai[3] * offset;
                                    int circumference = (int)(2f * (float)Math.PI * radius);

                                    //always flip it to opposite the previous side
                                    NPC.localAI[0] = MathHelper.WrapAngle(NPC.localAI[0] + (float)Math.PI + Main.rand.NextFloat((float)Math.PI / 2));
                                    const float safeRange = 60f;

                                    const int arcLength = 120;
                                    for (int i = 0; i < circumference; i += arcLength)
                                    {
                                        float angle = i / radius;
                                        if (angle > 2 * Math.PI - MathHelper.WrapAngle(MathHelper.ToRadians(safeRange)))
                                            continue;

                                        float spawnOffset = radius;// + Main.rand.NextFloat(-16, 16);
                                        Vector2 spawnPos = player.Center + spawnOffset * Vector2.UnitX.RotatedBy(angle + NPC.localAI[0]);
                                        Vector2 vel = speed * player.DirectionFrom(spawnPos);
                                        float ai0 = player.Distance(spawnPos) / speed + 30;
                                        if (Main.netMode != NetmodeID.MultiplayerClient)
                                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "CosmosInvaderTime").Type, damage, 0f, Main.myPlayer, ai0, vel.ToRotation());

                                    }
                                }
                                else //scatter
                                {
                                    int max = altAttack ? 12 : 8 + (int)NPC.ai[3] * (NPC.localAI[2] == 0 ? 2 : 4);
                                    float rotationOffset = Main.rand.NextFloat((float)Math.PI * 2);
                                    for (int i = 0; i < max; i++)
                                    {
                                        float ai0 = baseDistance;
                                        float distance = ai0 + NPC.ai[3] * offset;
                                        Vector2 spawnPos = player.Center + distance * Vector2.UnitX.RotatedBy(2 * Math.PI / max * i + rotationOffset);
                                        Vector2 vel = speed * player.DirectionFrom(spawnPos);// distance * player.DirectionFrom(spawnPos) / ai0;
                                        ai0 = distance / speed + 30;
                                        if (Main.netMode != NetmodeID.MultiplayerClient)
                                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "CosmosInvaderTime").Type, damage, 0f, Main.myPlayer, ai0, vel.ToRotation());
                                    }
                                }
                            }


                            NPC.ai[3]++;
                        }
                    }

                    if (++NPC.ai[1] > 480)
                    {
                        NPC.TargetClosest();
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                case 21: PrepareSpearDashDirectP2(); break;
                case 22: SpearDashDirectP2(); break;
                case 23: //while dashing
                    if (NPC.ai[1] % 3 == 0)
                        NPC.ai[1]++;
                    goto case 15;
                case 24: SpawnDestroyersForPredictiveThrow(); break;
                case 25: SpearTossPredictiveP2(); break;
                case 26: PrepareMechRayFan(); break;
                case 27: MechRayFan(); break;
                case 28: Providence(); break;
                case 29: PrepareFishron1(); break;
                case 30: SpawnFishrons(); break;
                case 31: PrepareTrueEyeDiveP2(); break;
                case 32: goto case 3; //spawn eyes
                case 33: PrepareNuke(); break;
                case 34: Nuke(); break;
                case 35: NPC.ai[0]++; break; //PrepareSlimeRain()
                case 36: NPC.ai[0]++; break; //SlimeRain()
                case 37: PrepareFishron2(); break;
                case 38: goto case 29; //spawn fishrons
                case 39: PrepareOkuuSpheresP2(); break;
                case 40: OkuuSpheresP2(); break;
                case 41: SpearTossDirectP2(); break;
                case 42: PrepareTwinRangsAndCrystals(); break;
                case 43: TwinRangsAndCrystals(); break;
                case 44: EmpressSwordWave(); break;
                case 45: ShtuxibusJavelinsP2(); break;
                case 46: GiantDeathrayFall(); break;
                case 47: //beginning of scythe rows and deathray rain
                    if (NPC.ai[1] == 0 && !AliveCheck(player))
                        break;

                    NPC.velocity = Vector2.Zero;
                    NPC.localAI[2] = 0;

                    if (NPC.ai[1] < 90)
                        FancyFireballs((int)NPC.ai[1]);

                    if (++NPC.ai[1] == 1)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.ai[3] = NPC.DirectionTo(player.Center).ToRotation();
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center * 16, NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center * 16, -NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center * 36, NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center * 36, -NPC.ai[3].ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathraySmall").Type, 0, 0f, Main.myPlayer);
                        }
                    }
                    else if (NPC.ai[1] == 91)
                    {
                        const int max = 50;
                        const float gap = 2000 / max;
                        for (int j = -1; j <= 1; j += 2)
                        {
                            Vector2 dustVel = NPC.ai[3].ToRotationVector2() * j * 3f;

                            for (int i = 0; i < 20; i++)
                            {
                                int dust = Dust.NewDust(NPC.Center, 0, 0, 31, dustVel.X, dustVel.Y, 0, default(Color), 3f);
                                Main.dust[dust].velocity *= 1.4f;
                            }

                            for (int i = 1; i <= max + 2; i++)
                            {
                                float speed = i * j * gap / 20;
                                float ai1 = i % 2 == 0 ? -1 : 1;

                                Vector2 vel = speed * NPC.ai[3].ToRotationVector2();

                                for (int k = 0; k < 3; k++)
                                {
                                    int d = Dust.NewDust(NPC.Center, 0, 0, 70, vel.X, vel.Y, Scale: 3f);
                                    Main.dust[d].velocity *= 1.5f;
                                    Main.dust[d].noGravity = true;
                                }

                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "AbomScytheSpin").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, NPC.whoAmI, ai1);
                            }
                        }
                    }
                    else if (NPC.ai[1] > 91 + 420)
                    {
                        NPC.netUpdate = true;

                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                    }
                    break;
                case 48: //prepare deathray rain
                    if (NPC.ai[1] < 90 && !AliveCheck(player))
                        break;

                    if (NPC.ai[2] == 0 && NPC.ai[3] == 0) //target one side of arena
                    {
                        NPC.ai[2] = NPC.Center.X + (player.Center.X < NPC.Center.X ? -1400 : 1400);
                    }

                    if (NPC.localAI[2] == 0) //direction to dash in next
                    {
                        NPC.localAI[2] = NPC.ai[2] > NPC.Center.X ? -1 : 1;
                    }

                    if (NPC.ai[1] > 90)
                    {
                        FancyFireballs((int)NPC.ai[1] - 90);
                    }
                    else
                    {
                        NPC.ai[3] = player.Center.Y - 700;
                    }

                    targetPos = new Vector2(NPC.ai[2], NPC.ai[3]);
                    Movement(targetPos, 2.4f);

                    if (++NPC.ai[1] > 150)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = NPC.localAI[2];
                        NPC.ai[3] = 0;
                        NPC.localAI[2] = 0;
                    }
                    break;
                case 49: //dash and make deathrays

                    NPC.velocity.X = NPC.ai[2] * 30f;
                    MovementY(player.Center.Y - 1000, Math.Abs(player.Center.Y - NPC.Center.Y) < 1000 ? 3f : 1f);
                    NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);

                    if (++NPC.ai[3] > 8)
                    {
                        NPC.ai[3] = 0;

                        SoundEngine.PlaySound(SoundID.Item12, NPC.Center);

                        float timeLeft = 4400 / Math.Abs(NPC.velocity.X) * 2 - NPC.ai[1] + 120;
                        if (NPC.ai[1] <= 15)
                        {
                            timeLeft = 0;
                        }
                        else
                        {
                            if (NPC.localAI[2] != 0)
                                timeLeft = 0;
                            if (++NPC.localAI[2] > 1)
                                NPC.localAI[2] = 0;
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(70) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                        }
                    }
                    if (++NPC.ai[1] > 4400 / Math.Abs(NPC.velocity.X))
                    {
                        NPC.netUpdate = true;
                        NPC.velocity.X = NPC.ai[2] * 60f;
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        //NPC.ai[2] = 0; //will be reused shortly
                        NPC.ai[3] = 0;
                    }
                    break;
                case 50: //prepare for next deathrain
                    if (NPC.ai[1] < 150 && !AliveCheck(player))
                        break;

                    NPC.velocity.Y = 0f;

                    NPC.velocity *= 0.947f;
                    NPC.ai[3] += NPC.velocity.Length();

                    if (NPC.ai[1] > 150)
                        FancyFireballs((int)NPC.ai[1] - 150);

                    if (++NPC.ai[1] > 210)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;

                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[3] = 0;
                    }
                    break;
                case 51: //second deathray dash
                    NPC.velocity.X = NPC.ai[2] * -30f;

                    MovementY(player.Center.Y - 1000, Math.Abs(player.Center.Y - NPC.Center.Y) < 1000 ? 3f : 1f);
                    NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
                    if (++NPC.ai[3] > 5)
                    {
                        NPC.ai[3] = 0;

                        SoundEngine.PlaySound(SoundID.Item12, NPC.Center);

                        float timeLeft = 4400 / Math.Abs(NPC.velocity.X) * 2 - NPC.ai[1] + 120;
                        if (NPC.ai[1] <= 30)
                        {
                            timeLeft = 0;
                        }
                        else
                        {
                            if (NPC.localAI[2] != 0)
                                timeLeft = 0;
                            if (++NPC.localAI[2] > 1)
                                NPC.localAI[2] = 0;
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(70) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(70) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                        }

                    }
                    if (++NPC.ai[1] > 4400 / Math.Abs(NPC.velocity.X))
                    {

                        ChooseNextAttack(13, 19, 20, 21, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    }
                    break;
                case 52: EOCStarSickles(); break;
                case 53: AbomSwordMassacare(); break;
                case 54: MassacareDash(); break;
                case 55: MassacareWait(); break;
                case 56: SparklingSword(); break;
                case 57: BigShtuxibusRay(); break;
                case 58: BallTorture(); break;
                case 59: UpperCutDick(); break;
                case 60: Polterghast(); break;
                //case : NPC.ai[0]++; break; //MutantSword()
                //case : NPC.ai[0]++; break; //PrepareMutantSword()
                case 61: SlimeGodSlam(); break; //PaladinHammster()
                case 62: //flocko swarm (p2 shoots ice waves horizontally after)

                    NPC.velocity *= 0.99f;
                    if (NPC.ai[2] == 0)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSlimeRain>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);
                        NPC.ai[2] = 1;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            for (int i = -3; i <= 3; i++) //make flockos
                            {
                                if (i == 0) //dont shoot one straight up
                                    continue;
                                Vector2 speed = new Vector2(Main.rand.NextFloat(60f), Main.rand.NextFloat(-40f, 40f));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<AbomFlocko>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 360 / 3 * i);
                            }

                            if (NPC.localAI[3] > 1) //prepare ice waves
                            {
                                Vector2 speed = new Vector2(Main.rand.NextFloat(50f), Main.rand.NextFloat(-40f, 40f));
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<AbomFlocko2>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, -1);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -speed, ModContent.ProjectileType<AbomFlocko2>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, 1);
                            }

                            float offset = 520;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2CircularEdge(20, 20), ModContent.ProjectileType<AbomFlocko3>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, offset);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Main.rand.NextVector2CircularEdge(20, 20), ModContent.ProjectileType<AbomFlocko3>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, -offset);

                            for (int i = -1; i <= 1; i += 2)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 3000 * i * Vector2.UnitX, Vector2.UnitY * j, ModContent.ProjectileType<GlowLine>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 5, 220 * i);
                                    if (p != Main.maxProjectiles)
                                    {
                                        Main.projectile[p].localAI[1] = NPC.whoAmI;
                                        if (Main.netMode == NetmodeID.Server)
                                            NetMessage.SendData(MessageID.SyncProjectile, number: p);
                                    }
                                }
                            }
                        }

                        SoundEngine.PlaySound(SoundID.Item27, NPC.Center);
                        for (int index1 = 0; index1 < 30; ++index1)
                        {
                            int index2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 76, 0.0f, 0.0f, 0, new Color(), 1f);
                            Main.dust[index2].noGravity = true;
                            Main.dust[index2].noLight = true;
                            Main.dust[index2].velocity *= 5f;
                        }
                    }
                    if (++NPC.ai[1] > 420)
                    {
                        //
                        ChooseNextAttack(13, 19, 20, 21, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    }
                    break;
                case 63: //saucer laser spam with rockets (p2 does two spams)

                    NPC.velocity *= 0.99f;
                    if (NPC.ai[1] == 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, NPC.whoAmI, -4);
                    }
                    if (++NPC.ai[1] > 420)
                    {
                        NPC.netUpdate = true;
                        //
                        ChooseNextAttack(13, 19, 20, 21, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    }
                    else if (NPC.ai[1] > 60) //spam lasers, lerp aim
                    {


                        float targetRot = NPC.DirectionTo(player.Center).ToRotation();
                        while (targetRot < -(float)Math.PI)
                            targetRot += 2f * (float)Math.PI;
                        while (targetRot > (float)Math.PI)
                            targetRot -= 2f * (float)Math.PI;
                        NPC.ai[3] = NPC.ai[3].AngleLerp(targetRot, 0.05f);


                        if (++NPC.ai[2] > 1) //spam lasers
                        {
                            NPC.ai[2] = 0;
                            SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                if (NPC.localAI[3] > 1) //p2 shoots to either side of you
                                {
                                    float angleOffset = MathHelper.Lerp(180, 20, NPC.ai[3]);

                                    for (int i = -1; i <= 1; i += 2)
                                    {
                                        Vector2 speed = 16f * NPC.DirectionTo(player.Center).RotatedBy((Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 3.0);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed.RotatedBy(MathHelper.ToRadians(angleOffset * i)), ModContent.ProjectileType<AbomLaser>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);

                                    }
                                }
                                else //p1 shoots directly
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        Vector2 speed = 16f * NPC.ai[3].ToRotationVector2().RotatedBy((Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0);
                                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<AbomLaser>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }

                        if (++NPC.localAI[2] > 60) //shoot rockets
                        {
                            NPC.localAI[2] = 0;

                            int max = 20;
                            for (int i = 0; i < max; i++)
                            {
                                Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy(MathHelper.TwoPi / max * i);
                                vel *= NPC.localAI[3] > 1 ? 5 : 8;
                                vel *= Main.rand.NextFloat(0.9f, 1.1f);
                                vel = vel.RotatedByRandom(MathHelper.TwoPi / max / 3);

                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<AbomRocket>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, Main.rand.Next(25, 36));
                            }
                        }
                    }

                    break;
                case 64: Calamitas(); break; //FunnyBettlee()
                case 65: LifeChampFireballz(); break;
                //gap in the numbers here so the ai loops right
                //when adding a new attack, remember to make ChooseNextAttack() point to the right case!
                case 68: P2NextAttackPause(); break;
                #endregion
                #region phase 3
                case -1: drainLifeInP3 = Phase3Transition(); break;
                case -2: VoidRaysP3(); break;
                case -3: OkuuSpheresP3(); break;
                case -4: AbomSwordMassacareP3(); break;
                case -5: MassacareDashP3(); break;
                case -6: MassacareWaitP3(); break;
                /*case -7: //prepare deathray rain //Dashes
                    if (NPC.ai[1] < 90 && !AliveCheck(player))
                        break;

                    if (NPC.ai[2] == 0 && NPC.ai[3] == 0) //target one side of arena
                    {
                        NPC.ai[2] = NPC.Center.X + (player.Center.X < NPC.Center.X ? -1400 : 1400);
                    }

                    if (NPC.localAI[2] == 0) //direction to dash in next
                    {
                        NPC.localAI[2] = NPC.ai[2] > NPC.Center.X ? -1 : 1;
                    }

                    if (NPC.ai[1] > 90)
                    {
                        FancyFireballs((int)NPC.ai[1] - 90);
                    }
                    else
                    {
                        NPC.ai[3] = player.Center.Y - 700;
                    }

                    targetPos = new Vector2(NPC.ai[2], NPC.ai[3]);
                    Movement(targetPos, 2.4f);

                    if (++NPC.ai[1] > 210)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                        NPC.netUpdate = true;
                        NPC.ai[0]--;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = NPC.localAI[2];
                        NPC.ai[3] = 0;
                        NPC.localAI[2] = 0;
                    }
                    break;
                case -8: //dash and make deathrays
                    
                    NPC.velocity.X = NPC.ai[2] * 30f;
                    MovementY(player.Center.Y - 1000, Math.Abs(player.Center.Y - NPC.Center.Y) < 1000 ? 3f : 2f);
                    NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);

                    if (++NPC.ai[3] > 8)
                    {
                        NPC.ai[3] = 0;

                        SoundEngine.PlaySound(SoundID.Item12, NPC.Center);

                        float timeLeft = 4400 / Math.Abs(NPC.velocity.X) * 2 - NPC.ai[1] + 120;
                        if (NPC.ai[1] <= 15)
                        {
                            timeLeft = 0;
                        }
                        else
                        {
                            if (NPC.localAI[2] != 0)
                                timeLeft = 0;
                            if (++NPC.localAI[2] > 1)
                                NPC.localAI[2] = 0;
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)),  ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(70) * (Main.rand.NextDouble() - 0.5)),ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)),ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(100) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(120) * (Main.rand.NextDouble() - 0.5)),ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(120) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                        
                        }
                    }
                    if (++NPC.ai[1] > 4400 / Math.Abs(NPC.velocity.X))
                    {
                        NPC.netUpdate = true;
                        NPC.velocity.X = NPC.ai[2] * 40f;
                        NPC.ai[0]--;
                        NPC.ai[1] = 0;
                        //NPC.ai[2] = 0; //will be reused shortly
                        NPC.ai[3] = 0;
                    }
                    break;*/
                case -7: FinalSpark(); break;
                case -8: DyingDramaticPause(); break;
                case -9: DyingAnimationAndHandling(); break;

                #endregion

                default: NPC.ai[0] = 11; goto case 11; //return to first phase 2 attack
            }
            if (NPC.ai[0] < 0 || NPC.ai[0] > 10 || (NPC.ai[0] == 10 && NPC.ai[1] > 150))
            {
                Main.dayTime = false;
                Main.time = 16200;
                Main.raining = false;
                Main.rainTime = 0;
                Main.maxRaining = 0;
                Main.bloodMoon = false;
            }
            if (NPC.ai[0] < 0 && NPC.life > 1 && drainLifeInP3) //in desperation
            {
                int time = 3200;
                NPC.life -= NPC.lifeMax / time;
                if (NPC.life < 1)
                    NPC.life = 1;
            }
            if (player.immune || player.hurtCooldowns[0] != 0 || player.hurtCooldowns[1] != 0)
                playerInvulTriggered = true;
        }

        #region help functions

        void DeleteSusItems()
        {
            for (int j = 0; j < Main.player[Main.myPlayer].inventory.Length; j++)
            {

                if (ModLoader.TryGetMod("PowerfulSword", out Mod PowerfulSword))
                {
                    ShtunUtils.DisplayLocalizedText("fuck yourself", textColor);
                    player.AddBuff(ModContent.BuffType<ChtuxlagorInfernoEX>(), 2);
                }

                if (Main.player[Main.myPlayer].inventory[j].type == ItemID.RodOfHarmony)
                {
                    int susindex = Main.LocalPlayer.FindItem(ItemID.RodOfHarmony);
                    Main.LocalPlayer.inventory[susindex].TurnToAir();
                }
            }
        }
        void FixHealth()
        {
            if (this.NPC.lifeMax < 40000000)
            {
                this.NPC.lifeMax = 40000000;
            }
        }
        void TryLifeSteal(Vector2 pos, int playerWhoAmI)
        {
            int totalHealPerHit = NPC.lifeMax / 100 * 10;

            const int max = 20;
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = Main.rand.NextFloat(2f, 9f) * -Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi);
                float ai0 = NPC.whoAmI;
                float ai1 = vel.Length() / Main.rand.Next(30, 90); //window in which they begin homing in

                int healPerOrb = (int)(totalHealPerHit / max * Main.rand.NextFloat(0.95f, 1.05f));

                if (playerWhoAmI == Main.myPlayer && Main.player[playerWhoAmI].ownedProjectileCounts[ModContent.ProjectileType<MutantHeal>()] < 10)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), pos, vel, ModContent.ProjectileType<MutantHeal>(), healPerOrb, 0f, Main.myPlayer, ai0, ai1);

                    SoundEngine.PlaySound(SoundID.Item27, pos);
                }
            }
        }
        void ManageAurasAndPreSpawn()
        {
            DeleteSusItems();

            if (!spawned)
            {
                spawned = true;
                int lifeMax = this.NPC.lifeMax;
                Mod mod1 = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");
                if ((bool)mod1.Call(new object[3]{
                (object) "GetDifficultyActive",
                (object) "death",
                (object) true
                }) && Main.masterMode)
                    this.NPC.lifeMax = 1745000000;
                this.NPC.damage = 15000;
                if (Main.zenithWorld)
                {
                    this.NPC.damage = 74500;
                    this.NPC.GivenName = Language.GetTextValue("Mods.ssm.NPCs.ShtuxibusGFB.DisplayName");
                    this.NPC.lifeMax = int.MaxValue;
                }
                this.NPC.life = this.NPC.lifeMax;
            }

            //if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
            Main.LocalPlayer.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "MutantPresenceBuff").Type, 2);
            Main.LocalPlayer.AddBuff(ModContent.BuffType<ShtuxibusCurse>(), 2);
            Main.LocalPlayer.AddBuff(ModContent.BuffType<OceanicSealBuff>(), 2);

            if (NPC.localAI[3] == 0)
            {
                NPC.TargetClosest();
                if (NPC.timeLeft < 30)
                    NPC.timeLeft = 30;
                if (NPC.Distance(Main.player[NPC.target].Center) < 1500)
                {
                    NPC.localAI[3] = 1;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                }
            }
            else if (NPC.localAI[3] == 1)
            {
                ShouldDrawAura = true;
                EModeGlobalNPC.Aura(NPC, 2000f, true, -1, default, ModContent.BuffType<GodEaterBuff>(), ModContent.BuffType<MutantFangBuff>());
            }
            else
            {
                if (Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f)
                {
                    if (Main.expertMode)
                    {
                        // Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresence>(), 2);
                    }

                    if (NPC.ai[0] < 0 && NPC.ai[0] > -6)
                    {
                    }
                }
            }
            DeleteSusItems();
        }
        void ManageNeededProjectiles()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) //checks for needed projs
            {
                if (NPC.ai[0] != -17 && (NPC.ai[0] < 0 || NPC.ai[0] > 10) && ShtunUtils.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null)
                    ritualProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), ShtunUtils.ScaledProjectileDamage(0), 0f, Main.myPlayer, 0f, NPC.whoAmI);

                if (ShtunUtils.ProjectileExists(ringProj, ModContent.ProjectileType<MutantRitual5>()) == null)
                    ringProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual5>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);

                if (ShtunUtils.ProjectileExists(spriteProj, ModContent.ProjectileType<Projectiles.Shtuxibus.Shtuxibus>()) == null)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        int number = 0;
                        for (int index = 999; index >= 0; --index)
                        {
                            if (!Main.projectile[index].active)
                            {
                                number = index;
                                break;
                            }
                        }
                        if (number >= 0)
                        {
                            Projectile projectile = Main.projectile[number];
                            projectile.SetDefaults(ModContent.ProjectileType<Projectiles.Shtuxibus.Shtuxibus>());
                            projectile.Center = NPC.Center;
                            projectile.owner = Main.myPlayer;
                            projectile.velocity.X = 0;
                            projectile.velocity.Y = 0;
                            projectile.damage = 0;
                            projectile.knockBack = 0f;
                            projectile.identity = number;
                            projectile.gfxOffY = 0f;
                            projectile.stepSpeed = 1f;
                            projectile.ai[1] = NPC.whoAmI;

                            spriteProj = number;
                        }
                    }
                    else //server
                    {
                        spriteProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Shtuxibus.Shtuxibus>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                }
            }
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
        void P1NextAttackOrMasoOptions(float sourceAI)
        {
            if (Main.rand.NextBool(3))
            {
                int[] options = new int[] { 0, 1, 2, 4, 7, 9, 9 };
                NPC.ai[0] = Main.rand.Next(options);
                if (NPC.ai[0] == sourceAI) //dont repeat attacks consecutively
                    NPC.ai[0] = sourceAI == 9 ? 0 : 9;

                bool badCombo = false;
                //dont go into boundary/sword from spheres, true eye dive, void rays
                if (NPC.ai[0] == 9 && (sourceAI == 1 || sourceAI == 2 || sourceAI == 7))
                    badCombo = true;
                //dont go into destroyer-toss or void rays from true eye dive
                if ((NPC.ai[0] == 0 || NPC.ai[0] == 7) && sourceAI == 2)
                    badCombo = true;

                if (badCombo)
                    NPC.ai[0] = 4; //default to dashes
                else if (NPC.ai[0] == 9 && Main.rand.NextBool())
                    NPC.localAI[2] = 1f; //force sword attack instead of boundary
                else
                    NPC.localAI[2] = 0f;
            }
            else
            {
                if (NPC.ai[0] == 9 && NPC.localAI[2] == 0)
                {
                    NPC.localAI[2] = 1;
                }
                else
                {
                    NPC.ai[0]++;
                    NPC.localAI[2] = 0f;
                }
            }

            if (NPC.ai[0] >= 10) //dont accidentally go into p2
                NPC.ai[0] = 0;

            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            NPC.ai[3] = 0;
            NPC.localAI[0] = 0;
            NPC.localAI[1] = 0;
            NPC.netUpdate = true;
        }
        void SpawnSphereRing(int max, float speed, int damage, float rotationModifier, float offset = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            float rotation = 2f * (float)Math.PI / max;
            int type = ModContent.ProjectileType<MutantSphereRing>();
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = speed * Vector2.UnitY.RotatedBy(rotation * i + offset);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * NPC.spriteDirection, speed);
            }
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }
        bool AliveCheck(Player p, bool forceDespawn = false)
        {
            DeleteSusItems();
            if (forceDespawn || ((!p.active || p.dead) && NPC.localAI[3] > 0))
            {
                NPC.TargetClosest();
                p = Main.player[NPC.target];
                if (forceDespawn || !p.active || p.dead)
                {
                    if (NPC.timeLeft > 30)
                        NPC.timeLeft = 30;
                    NPC.velocity.Y -= 1f;
                    if (NPC.timeLeft == 1)
                    {
                        if (NPC.position.Y < 0)
                            NPC.position.Y = 0;
                        SkyManager.Instance.Deactivate("ssm:Shtuxibus");
                    }
                    return false;
                }
            }

            if (NPC.timeLeft < 3600)
                NPC.timeLeft = 3600;

            if (player.Center.Y / 16f > Main.worldSurface)
            {
                NPC.velocity.X *= 0.95f;
                NPC.velocity.Y -= 1f;
                if (NPC.velocity.Y < -32f)
                    NPC.velocity.Y = -32f;
                return false;
            }

            return true;
        }
        bool Phase2Check()
        {
            DeleteSusItems();
            if (!INPHASE2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    INPHASE2 = true;
                    NPC.ai[0] = 10;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.netUpdate = true;
                    ShtunUtils.ClearHostileProjectiles(1, NPC.whoAmI);
                }
                return true;
            }
            return false;
        }
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
        void DramaticTransition(bool fightIsOver, bool normalAnimation = true)
        {
            NPC.velocity = Vector2.Zero;
            SoundEngine.PlaySound(SoundID.Item27 with { Volume = 1.5f }, NPC.Center);

            if (normalAnimation)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantBomb").Type, 0, 0f, Main.myPlayer);
            }

            const int max = 40;
            float totalAmountToHeal = fightIsOver
                ? Main.player[NPC.target].statLifeMax2 / 4f
                : NPC.lifeMax - NPC.life + NPC.lifeMax * 0.1f;
            for (int i = 0; i < max; i++)
            {
                int heal = (int)(Main.rand.NextFloat(0.9f, 1.1f) * totalAmountToHeal / max);
                Vector2 vel = normalAnimation
                    ? Main.rand.NextFloat(2f, 18f) * -Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) //looks messier normally
                    : 0.1f * -Vector2.UnitY.RotatedBy(MathHelper.TwoPi / max * i); //looks controlled during mutant p1 skip
                float ai0 = fightIsOver ? -Main.player[NPC.target].whoAmI - 1 : NPC.whoAmI; //player -1 necessary for edge case of player 0
                float ai1 = vel.Length() / Main.rand.Next(fightIsOver ? 90 : 150, 180); //window in which they begin homing in
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantHeal").Type, heal, 0f, Main.myPlayer, ai0, ai1);
            }
        }
        void EModeSpecialEffects()
        {
            if (Main.GameModeInfo.IsJourneyMode && CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled)
                CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().SetPowerInfo(false);

            if (!SkyManager.Instance["ssm:Shtuxibus"].IsActive() && !INPHASE3)
            {
                SkyManager.Instance.Activate("ssm:Shtuxibus");
            }

            if (ModLoader.TryGetMod("ssm", out Mod musicMod))
            {
                if (!Main.zenithWorld)
                    Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/ORDER");
                else
                    Music = ShtunUtils.Stalin ? MusicLoader.GetMusicSlot(musicMod, "Assets/Music/StainedBrutalCommunism") : Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Halcyon");
            }
        }
        void TryMasoP3Theme()
        {
            if (ModLoader.TryGetMod("ssm", out Mod musicMod))
            {
                if (!Main.zenithWorld)
                    Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/ORDER");
                else
                    Music = ShtunUtils.Stalin ? MusicLoader.GetMusicSlot(musicMod, "Assets/Music/StainedBrutalCommunism") : Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Halcyon");
            }
        }
        void FancyFireballs(int repeats)
        {
            float modifier = 0;
            for (int i = 0; i < repeats; i++)
                modifier = MathHelper.Lerp(modifier, 1f, 0.08f);

            float distance = 1600 * (1f - modifier);
            float rotation = MathHelper.TwoPi * modifier;
            const int max = 6;
            for (int i = 0; i < max; i++)
            {
                int d = Dust.NewDust(NPC.Center + distance * Vector2.UnitX.RotatedBy(rotation + MathHelper.TwoPi / max * i), 0, 0, DustID.Vortex, NPC.velocity.X * 0.3f, NPC.velocity.Y * 0.3f, newColor: Color.Red);
                Main.dust[d].noGravity = true;
                Main.dust[d].scale = 6f - 4f * modifier;
            }
        }

        #endregion

        #region calphaze2
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void CalamityFishron()
        {
            const int fishronDelay = 3;
            int maxFishronSets = 4;
            if (NPC.ai[1] == fishronDelay * maxFishronSets + 35)
            {
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff"), Main.LocalPlayer.Center);
                if (ShtunUtils.HostCheck)
                {
                    for (int j = -1; j <= 1; j += 2) //to both sides of player
                    {
                        Vector2 offset = NPC.ai[2] == 0 ? Vector2.UnitX * -450f * j : Vector2.UnitY * 475f * j;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantOldDuke>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                    }
                }
            }
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void Calamitas()
        {
            //
            const int Startup = 20;
            const int Distance = 450;
            int brimstoneMonster = -1;
            if (ModContent.TryFind(ModCompatibility.Calamity.Name, "BrimstoneMonster", out ModProjectile monster))
            {
                brimstoneMonster = monster.Type;
            }

            if (Timer < Startup)
            {
                Vector2 targetPos = player.Center + Vector2.UnitX * Math.Sign(NPC.Center.X - player.Center.X) * Distance;
                Movement(targetPos, 1.2f);
            }
            if (Timer == Startup)
            {
                if (ShtunUtils.HostCheck)
                {
                    Shtuxibus Shtuxibus = (NPC.ModNPC as Shtuxibus);
                    Vector2 pos = ShtunUtils.ProjectileExists(Shtuxibus.ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? NPC.Center : Main.projectile[Shtuxibus.ritualProj].Center;
                    Vector2 rot = Utils.SafeNormalize(player.velocity, Vector2.UnitY);
                    const int moons = 9;
                    for (int i = 0; i < moons; i++)
                    {
                        Vector2 offset = rot.RotatedBy(i * MathHelper.TwoPi / moons);
                        Vector2 targetPos = pos + (offset * 1450f);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), targetPos, targetPos.DirectionTo(player.Center), brimstoneMonster, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, 0f, 2f, 0f);
                    }

                }
            }
            if (Timer > Startup)
            {
                Vector2 dir = Utils.SafeNormalize(NPC.Center - player.Center, -Vector2.UnitY);
                Vector2 targetPos = player.Center + dir * Distance;
                Movement(targetPos, 1.2f);

                const int CalamitasTime = 80;
                const int TelegraphTime = 40;
                const int Attacks = 3;
                if ((Timer - Startup) % CalamitasTime == CalamitasTime - 1)
                {
                    if (ShtunUtils.HostCheck)
                    {
                        int calamiti = 10;

                        int[] rotations = Enumerable.Range(1, calamiti).ToArray();
                        rotations = rotations.OrderBy(a => Main.rand.Next()).ToArray(); //randomize list
                        Vector2 random = Main.rand.NextVector2Unit();

                        for (int i = 0; i < calamiti; i++)
                        {
                            int spawnDistance = Main.rand.Next(1200, 1400);
                            int aimDistance = Main.rand.Next(80, 400);

                            float spawnRot = MathHelper.TwoPi * ((float)i / calamiti);
                            Vector2 spawnPos = player.Center + random.RotatedBy(spawnRot) * spawnDistance;
                            float aimRot = (float)rotations[i] / calamiti;
                            Vector2 predict = player.velocity * TelegraphTime / 2;
                            Vector2 aimPos = player.Center + predict + aimRot.ToRotationVector2() * aimDistance;
                            Vector2 aim = spawnPos.DirectionTo(aimPos);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPos, aim, ModContent.ProjectileType<DLCBloomLine>(), 0, 0, Main.myPlayer, 1, NPC.whoAmI, TelegraphTime + 10);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPos, aim, ModContent.ProjectileType<MutantSCal>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, TelegraphTime);
                        }
                    }
                }
                if (Timer > Startup + (CalamitasTime * (Attacks + 1) - 10))
                {

                    foreach (Projectile projectile in Main.projectile.Where(p => p != null && p.active && p.type == brimstoneMonster))
                    {
                        SoundEngine.PlaySound(SoundID.Item14, projectile.Center);
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 pos = projectile.Center + Main.rand.NextVector2Circular(projectile.width / 2, projectile.height / 2);
                            Dust.NewDust(pos, 1, 1, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
                        }
                        projectile.Kill();
                    }

                    ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 33, 35, 37, 41, 44, 45);
                    return;
                }
            }
            Timer++;
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void SlimeGodSlam()
        {
            //
            if (!AliveCheck(player))
                return;
            ref float side = ref NPC.ai[2];
            const int Windup = 40;
            const int ParticleTime = 30;
            if (Counter == 0 && Timer == 0)
            {
                SoundEngine.PlaySound(SlimeGodCoreEternity.ExitSound, NPC.Center);
                side = Math.Sign(NPC.Center.X - player.Center.X);
                NPC.netUpdate = true;

                Particle p = new ExpandingBloomParticle(NPC.Center, Vector2.Zero, Color.Magenta, Vector2.One * 40f, Vector2.Zero, ParticleTime, true, Color.Transparent);
                p.Spawn();
            }
            if (Counter == 0 && Timer == ParticleTime)
            {
                Particle p = new ExpandingBloomParticle(NPC.Center, Vector2.Zero, Color.Crimson, Vector2.One * 40f, Vector2.Zero, ParticleTime, true, Color.Transparent);
                p.Spawn();
            }
            float distance = 500f;
            Vector2 desiredPos = player.Center + Vector2.UnitX * side * distance - Vector2.UnitY * 100;
            Movement(desiredPos, 1.2f);
            if (Timer == 30 + Windup)
            {
                SoundEngine.PlaySound(SlimeGodCoreEternity.BigShotSound, NPC.Center);
                if (ShtunUtils.HostCheck)
                {
                    float random = (Main.rand.NextFloat() - 0.5f) / 3;
                    for (int i = -8; i < 2; i++)
                    {
                        float iX = i + 0.5f;
                        float xModifier = 6f;
                        float speedX = (iX - random) * xModifier * side;
                        float speedY = -20;

                        int crimson = i % 2 == 0 ? 1 : -1; //every other slime is crimulean, every other is ebonian
                        crimson = (int)(crimson * side);

                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speedX * Vector2.UnitX + speedY * Vector2.UnitY, ModContent.ProjectileType<MutantSlimeGod>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 1f), 1f, Main.myPlayer, crimson);
                    }

                    float speed = 8;
                    Vector2 aureusVel = Vector2.Normalize(Vector2.UnitX * -Math.Sign(player.Center.X - NPC.Center.X) + Vector2.UnitY) * speed;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, aureusVel, ModContent.ProjectileType<MutantAureusSpawn>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 1f), 1f, Main.myPlayer, player.whoAmI);
                }
                side = -side; //switch side
                NPC.netUpdate = true;
            }
            if (++Timer >= MutantSlimeGod.SlamTime + Windup)
            {
                Timer = Windup;
                if (++Counter >= 3)
                {
                    ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    return;
                }
                NPC.netUpdate = true;
            }
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void CalamityMechRayFan()
        {
            //
            float timer = NPC.ai[3];
            int startTime = 90 * 2;
            if (timer % 90 == 0 && timer > startTime)
            {
                int distance = 550;
                Vector2 pos = player.Center + distance * Vector2.UnitX.RotatedBy(MathHelper.Pi * (((Main.rand.NextBool() ? 1f : -1f) / 8f) + Main.rand.Next(2)));
                //SoundEngine.PlaySound(PlaguebringerGoliath.AttackSwitchSound, pos);
                if (ShtunUtils.HostCheck)
                {
                    Vector2 vel = pos.DirectionTo(player.Center);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), pos, vel, ModContent.ProjectileType<MutantPBG>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                }
            }
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void Providence()
        {
            if (!AliveCheck(player))
                return;

            const int PrepareTime = 65;
            const int DashTime = 60;
            const int LaserPrepareTime = 30;
            const int LaserTime = 95;

            if (Timer < PrepareTime)
            {
                Shtuxibus Shtuxibus = (NPC.ModNPC as Shtuxibus);
                Projectile arena = ShtunUtils.ProjectileExists(Shtuxibus.ritualProj, ModContent.ProjectileType<MutantRitual>());
                if (arena != null)
                {
                    arena.position -= arena.velocity;
                    arena.position += arena.DirectionTo(player.Center) * 4;
                    arena.netUpdate = true;
                }
            }
            if (Timer == 0)
            {
                SoundEngine.PlaySound(SoundID.ForceRoar, NPC.Center);
                Particle p = new ExpandingBloomParticle(NPC.Center, Vector2.Zero, Color.Goldenrod, Vector2.One * 40f, Vector2.Zero, PrepareTime, true, Color.White);
                p.Spawn();
            }
            if (Timer < PrepareTime * 2f / 3)
            {
                int dirX = Math.Sign(NPC.Center.X - player.Center.X);
                int dirY = Math.Sign(NPC.Center.Y - player.Center.Y);

                if (dirX == 0)
                    dirX = 1;
                if (dirY == 0)
                    dirY = 1;

                int distanceX = 800;
                int distanceY = 400;
                Vector2 targetPos = player.Center + Vector2.UnitX * dirX * distanceX + Vector2.UnitY * dirY * distanceY;
                Movement(targetPos, 1.5f, fastX: true);
            }
            else if (Timer < PrepareTime)
            {
                NPC.velocity *= 0.9f;
            }
            else if (Timer == PrepareTime)
            {
                const int dashSpeed = 28;
                NPC.velocity.Y = 0;
                NPC.velocity.X = Math.Sign(player.Center.X - NPC.Center.X) * dashSpeed;

                //SoundEngine.PlaySound(ProfanedGuardianCommander.DashSound, NPC.Center);
                NPC.netUpdate = true;


            }
            else if (Timer - PrepareTime < DashTime)
            {
                //register values for providence ray
                int dirY = Math.Sign(player.Center.Y - NPC.Center.Y);

                if (dirY == 0)
                    dirY = 1;

                int distanceY = 900;
                NPC.ai[3] = player.Center.Y + dirY * distanceY;
                NPC.netUpdate = true;

                if (Timer % 3 == 0)
                {
                    if (ShtunUtils.HostCheck)
                    {
                        float spearSpeed = 18f;
                        Vector2 spearVel = Vector2.UnitY * Math.Sign(player.Center.Y - NPC.Center.Y) * spearSpeed;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, spearVel, ModContent.ProjectileType<HolySpear>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, 1f, 0f, 0f);
                    }
                }
            }
            else if (Timer - PrepareTime - DashTime < LaserPrepareTime) //move to deathray position
            {
                Vector2 pos = NPC.ai[2] * Vector2.UnitX + NPC.ai[3] * Vector2.UnitY;
                NPC.velocity.X *= 0.97f;
                NPC.velocity.Y = (pos.Y - NPC.Center.Y) * 0.025f;
                //Movement(player.Center + pos, 1.2f);
            }
            else if (Timer - PrepareTime - DashTime == LaserPrepareTime)
            {
                //deathray
                //SoundEngine.PlaySound(CalamityMod.NPCs.Providence.Providence.HolyRaySound, NPC.Center);
                if (ShtunUtils.HostCheck)
                {
                    float rotation = 435f;
                    Vector2 velocity2 = player.Center - NPC.Center;
                    velocity2.Normalize();
                    float beamDirection = -1f;
                    if (velocity2.X < 0f)
                    {
                        beamDirection = 1f;
                    }
                    beamDirection *= Math.Sign(NPC.ai[3]);
                    velocity2 = Utils.RotatedBy(velocity2, (0.0 - (double)beamDirection) * 6.2831854820251465 / 6.0, default(Vector2));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, velocity2.X, velocity2.Y, ModContent.ProjectileType<MutantHolyRay>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage, 3f / 2), 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation, NPC.whoAmI, 0f);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0f - velocity2.X, 0f - velocity2.Y, ModContent.ProjectileType<MutantHolyRay>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage, 3f / 2), 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation, NPC.whoAmI, 0f);
                }
                NPC.netUpdate = true;
            }
            else
            {
                NPC.velocity *= 0.96f;
                if (Timer - PrepareTime - DashTime - LaserPrepareTime == LaserTime)
                {
                    ChooseNextAttack(11, 13, 20, 21, 26, 33, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    return;
                }
            }
            Timer++;
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void YharonBH()
        {
            void DoFlareDustBulletHell(int attackType, int timer, int projectileDamage, int totalProjectiles, float projectileVelocity, float radialOffset, bool phase2)
            {
                SoundEngine.PlaySound(SoundID.Item20, NPC.Center, (SoundUpdateCallback)null);
                if (!ShtunUtils.HostCheck)
                {
                    return;
                }
                float aiVariableUsed = Timer;
                switch (attackType)
                {
                    case 0:
                        {
                            float offsetAngle = 360 / totalProjectiles;
                            int totalSpaces = totalProjectiles / 5;

                            totalSpaces = 0;

                            int spaceStart = Main.rand.Next(totalProjectiles - totalSpaces);
                            float ai0 = ((aiVariableUsed % (float)(timer * 2) == 0f) ? 1f : 0f);
                            int spacesMade = 0;
                            for (int i = 0; i < totalProjectiles; i++)
                            {
                                if (i >= spaceStart && spacesMade < totalSpaces)
                                {
                                    spacesMade++;
                                }
                                else
                                {
                                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, ModContent.ProjectileType<FlareDust>(), projectileDamage, 0f, Main.myPlayer, ai0, (float)i * offsetAngle, 0f);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            double radians = (float)Math.PI * 2f / (float)totalProjectiles;
                            Vector2 spinningPoint = Vector2.Normalize(new Vector2(0f - NPC.localAI[2], 0f - projectileVelocity));
                            for (int j = 0; j < totalProjectiles; j++)
                            {
                                Vector2 vector2 = Utils.RotatedBy(spinningPoint, radians * (double)j, default(Vector2)) * projectileVelocity;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vector2, ModContent.ProjectileType<FlareDust>(), projectileDamage, 0f, Main.myPlayer, 2f, 0f, 0f);
                            }
                            float newRadialOffset = (((float)((int)aiVariableUsed / (timer / 4)) % 2f == 0f) ? radialOffset : (0f - radialOffset));
                            NPC.localAI[2] += newRadialOffset;
                            break;
                        }
                }
            }
            const int WindupTime = 40;
            const int bhTime = 180;
            const int EndTime = MutantYharonVortex.ThrowTime;
            if (Timer < WindupTime)
            {
                NPC.velocity *= 0.9f;
            }
            if (Timer >= WindupTime - 20)
            {
                if (NPC.velocity.Length() < 1)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * (NPC.velocity.Length() + 0.05f);
                }
            }
            if (Timer == WindupTime)
            {
                NPC.netUpdate = true;
                //SoundEngine.PlaySound(Yharon.RoarSound, NPC.Center);
                int type = ModContent.ProjectileType<MutantYharonVortex>();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, type, ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, bhTime, NPC.whoAmI, 0f);
                }

            }
            if (Timer > WindupTime && Timer <= WindupTime + bhTime)
            {
                int flareDustSpawnDivisor = 30;
                int totalProjectiles = 36;
                if (Timer % flareDustSpawnDivisor == 0)
                {
                    DoFlareDustBulletHell(0, flareDustSpawnDivisor, ShtunUtils.ScaledProjectileDamage(NPC.defDamage), totalProjectiles, 0f, 0f, phase2: true);
                }

            }
            if (++Timer > WindupTime + bhTime + EndTime)
            {
                ChooseNextAttack(11, 13, 20, 21, 26, 33, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                return;
            }
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void SpawnDoG()
        {
            //
            if (!AliveCheck(player))
                return;
            Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500;
            if (Math.Abs(targetPos.X - player.Center.X) < 150) //avoid crossing up player
            {
                targetPos.X = player.Center.X + 150 * Math.Sign(targetPos.X - player.Center.X);
                Movement(targetPos, 0.3f);
            }
            if (NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, 0.9f);
            }
            if (NPC.localAI[1] == 0) //max number of attacks
            {
                NPC.localAI[1] = 9;

            }

            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 30;
                NPC.ai[1] += 15; //faster

                if (Counter > 0)
                {
                    //NPC.TargetClosest();
                    NPC.ai[0] = 25; //spear throw direct
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                }
                else
                {
                    Counter++;
                    //SoundEngine.PlaySound(DevourerofGodsHead.SpawnSound, NPC.Center);
                    if (ShtunUtils.HostCheck) //spawn worm
                    {
                        Vector2 vel = NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120)) * 10f;
                        float ai1 = 0.8f + 0.4f * NPC.ai[2] / 5f;
                        ai1 += 0.4f;
                        int current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDoGHead>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, NPC.target, ai1);
                        //timeleft: remaining duration of this case + duration of next case + extra delay after + successive death
                        Main.projectile[current].timeLeft = 30 * (1 - (int)NPC.ai[2]) + 60 * (int)NPC.localAI[1] + 30 + (int)NPC.ai[2] * 6;

                        int max = 60;

                        for (int i = 0; i < max; i++)
                            current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDoGBody>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, Main.projectile[current].identity);
                        int previous = current;
                        current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantDoGTail>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, Main.projectile[current].identity);
                        Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                        Main.projectile[previous].netUpdate = true;
                    }
                }
            }
        }
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        void Polterghast()
        {
            //
            if (!AliveCheck(player))
                return;


            NPC.velocity *= 0.85f;

            const int PolterWaves = 4;
            const int PolterTime = 70;

            if (Timer == 0)
            {
                SoundEngine.PlaySound(CalamityMod.NPCs.Polterghast.Polterghast.P2Sound with { Volume = 3f }, NPC.Center);
                NPC.ai[3] = Main.rand.NextFloat(MathHelper.TwoPi);
                NPC.netUpdate = true;
                Counter = 1;
            }

            if (Timer % PolterTime == (PolterTime / 2))
            {
                SoundEngine.PlaySound(CalamityMod.NPCs.Polterghast.Polterghast.PhantomSound with { Volume = 3f }, NPC.Center);
                if (ShtunUtils.HostCheck)
                {
                    const int Polters = 7;
                    for (int i = 0; i < Polters; i++)
                    {
                        Vector2 spawnDir = NPC.ai[3].ToRotationVector2().RotatedBy(MathHelper.TwoPi * (float)i / Polters);
                        Vector2 spawnPos = player.Center + (spawnDir * MutantPolter.StartDistance);
                        Vector2 targetPos = player.Center;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<MutantPolter>(), ShtunUtils.ScaledProjectileDamage(NPC.defDamage), 0f, Main.myPlayer, targetPos.X, targetPos.Y, Counter);
                    }
                }
                Counter = -Counter;
                NPC.ai[3] = Main.rand.NextFloat(MathHelper.TwoPi);
                NPC.netUpdate = true;
            }

            if (Timer > (PolterWaves * PolterTime) + (PolterTime / 3))
            {
                ChooseNextAttack(11, 13, 20, 21, 26, 33, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                return;
            }
            Timer++;
        }
        #endregion

        #region phaze1

        void SpearTossDirectP1AndChecks()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            NPC.localAI[2] = 0;
            Vector2 targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, NPC.localAI[3] > 0 ? 0.5f : 2f, true, NPC.localAI[3] > 0);
            }

            if (NPC.ai[3] == 0)
            {
                NPC.ai[3] = Main.rand.Next(2, 8);
                NPC.netUpdate = true;
            }

            if (NPC.localAI[3] > 0) //dont begin proper ai timer until in range to begin fight
                NPC.ai[1]++;

            if (NPC.ai[1] < 145) //track player up until just before attack
            {
                NPC.localAI[0] = NPC.DirectionTo(player.Center + player.velocity * 30f).ToRotation();
            }

            if (NPC.ai[1] > 150) //120)
            {
                NPC.netUpdate = true;
                //NPC.TargetClosest();
                NPC.ai[1] = 60;
                if (++NPC.ai[2] > NPC.ai[3])
                {
                    P1NextAttackOrMasoOptions(NPC.ai[0]);
                    NPC.velocity = NPC.DirectionTo(player.Center) * 2f;
                }
                else if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = NPC.localAI[0].ToRotationVector2() * 25f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);

                }
                NPC.localAI[0] = 0;
            }
            else if (NPC.ai[1] == 61 && NPC.ai[2] < NPC.ai[3] && Main.netMode != NetmodeID.MultiplayerClient)
            {

                // NPC.ai[0] = 10; //skip to phase 2
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.netUpdate = true;


                //   ShtunUtils.PrintLocalization($"Mods.{Mod.Name}.Message.MutantSkipP1", Color.LimeGreen);


                NPC.ai[2] = 1; //flag for different p2 transition animation


                if (NPC.ai[2] == 0) //first time only
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient) //spawn worm
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Vector2 vel = NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120)) * 10f;
                            float ai1 = 0.8f + 0.4f * j / 5f;
                            int current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerHead").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, ai1);
                            //timeleft: remaining duration of this case + extra delay after + successive death
                            Main.projectile[current].timeLeft = 90 * ((int)NPC.ai[3] + 1) + 30 + j * 6;
                            int max = Main.rand.Next(8, 19);
                            for (int i = 0; i < max; i++)
                                current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerBody").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                            int previous = current;
                            current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerTail").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                            Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                            Main.projectile[previous].netUpdate = true;
                        }
                    }
                }
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathrayAim").Type, 0, 0f, Main.myPlayer, 85f, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 3);

                //Projectile.NewProjectile(npc.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);
            }
        }
        void ApproachForNextAttackP1()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            Vector2 targetPos = player.Center + player.DirectionTo(NPC.Center) * 250;
            if (NPC.Distance(targetPos) > 50 && ++NPC.ai[2] < 180)
            {
                Movement(targetPos, 0.5f);
            }
            else
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = player.DirectionTo(NPC.Center).ToRotation();
                NPC.ai[3] = (float)Math.PI / 10f;
                if (player.Center.X < NPC.Center.X)
                    NPC.ai[3] *= -1;
            }
        }
        void VoidRaysP1()
        {

            if (Phase2Check())
                return;

            NPC.velocity = Vector2.Zero;
            if (--NPC.ai[1] < 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(2, 0).RotatedBy(NPC.ai[2]), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantMark1").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                NPC.ai[1] = 3; //delay between projs
                NPC.ai[2] += NPC.ai[3];
                if (NPC.localAI[0]++ == 20 || NPC.localAI[0] == 40)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] -= NPC.ai[3] / 3;
                }
                else if (NPC.localAI[0] >= 60)
                {
                    P1NextAttackOrMasoOptions(7);

                }
            }
        }
        void OkuuSpheresP1()
        {
            if (Phase2Check())
                return;

            NPC.velocity = Vector2.Zero;

            if (--NPC.ai[1] < 0)
            {
                NPC.netUpdate = true;
                float modifier = 3;
                NPC.ai[1] = 90 / modifier;
                if (++NPC.ai[2] > 4 * modifier)
                {
                    P1NextAttackOrMasoOptions(NPC.ai[0]);
                }
                else
                {
                    int max = 9;
                    float speed = 12;
                    int sign = (NPC.ai[2] % 2 == 0 ? 1 : -1);
                    SpawnSphereRing(max, speed, (int)(0.8 * ShtunUtils.ScaledProjectileDamage(NPC.damage)), 1f * sign);
                    SpawnSphereRing(max, speed, (int)(0.8 * ShtunUtils.ScaledProjectileDamage(NPC.damage)), -0.5f * sign);
                }
            }

        }
        void PrepareTrueEyeDiveP1()
        {
            if (!AliveCheck(player))
                return;
            if (Phase2Check())
                return;
            Vector2 targetPos = player.Center;
            targetPos.X += 700 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y -= 400;
            Movement(targetPos, 0.6f);
            if (NPC.Distance(targetPos) < 50 || ++NPC.ai[1] > 180) //dive here
            {
                NPC.velocity.X = 35f * (NPC.position.X < player.position.X ? 1 : -1);
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y *= -1;
                NPC.velocity.Y *= 0.3f;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            }
        }
        void TrueEyeDive()
        {
            if (NPC.ai[3] == 0)
                NPC.ai[3] = Math.Sign(NPC.Center.X - player.Center.X);

            if (NPC.ai[2] > 3)
            {
                Vector2 targetPos = player.Center;
                targetPos.X += NPC.Center.X < player.Center.X ? -500 : 500;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.3f);
            }
            else
            {
                NPC.velocity *= 0.99f;
            }

            if (--NPC.ai[1] < 0)
            {
                NPC.ai[1] = 15;
                int maxEyeThreshold = 6;
                int endlag = 3;
                if (++NPC.ai[2] > maxEyeThreshold + endlag)
                {
                    if (NPC.ai[0] == 3)
                        P1NextAttackOrMasoOptions(2);
                    else
                        ChooseNextAttack(13, 19, 21, 24, 33, 33, 33, 39, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
                else if (NPC.ai[2] <= maxEyeThreshold)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int type;
                        float ratio = NPC.ai[2] / maxEyeThreshold * 3;
                        if (ratio <= 1f)
                            type = ModContent.Find<ModProjectile>(fargosouls.Name, "MutantTrueEyeL").Type;
                        else if (ratio <= 2f)
                            type = ModContent.Find<ModProjectile>(fargosouls.Name, "MutantTrueEyeS").Type;
                        else
                            type = ModContent.Find<ModProjectile>(fargosouls.Name, "MutantTrueEyeR").Type;
                        int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer, NPC.target);
                        if (p != Main.maxProjectiles) //inform them which side attack began on
                        {
                            Main.projectile[p].localAI[1] = NPC.ai[3]; //this is ok, they sync this
                            Main.projectile[p].netUpdate = true;
                        }
                    }
                    SoundEngine.PlaySound(SoundID.Item92, NPC.Center);
                    for (int i = 0; i < 30; i++)
                    {
                        int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 135, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                }
            }
        }
        void PrepareSpearDashDirectP1()
        {
            if (Phase2Check())
                return;
            if (NPC.ai[3] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[3] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerBody").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 240); // 250);
            }

            if (++NPC.ai[1] > 240)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[0]++;
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
            }

            Vector2 targetPos = player.Center;
            if (NPC.Top.Y < player.Bottom.Y)
                targetPos.X += 600f * Math.Sign(NPC.Center.X - player.Center.X);
            targetPos.Y += 400f;
            Movement(targetPos, 0.7f, false);
        }
        void SpearDashDirectP1()
        {
            if (Phase2Check())
                return;
            NPC.velocity *= 0.9f;

            if (NPC.ai[3] == 0)
                NPC.ai[3] = Main.rand.Next(3, 15);

            if (++NPC.ai[1] > NPC.ai[3])
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                if (++NPC.ai[2] > 5)
                {
                    P1NextAttackOrMasoOptions(4); //go to next attack after dashes
                }
                else
                {
                    float speed = 45f;
                    NPC.velocity = speed * NPC.DirectionTo(player.Center + player.velocity);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearDash").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);


                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);

                    }
                }
            }
        }
        void WhileDashingP1()
        {
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
            if (++NPC.ai[1] > 30)
            {
                if (!AliveCheck(player))
                    return;
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
            }
        }
        void BoundaryBulletHellAndSwordP1()
        {
            switch ((int)NPC.localAI[2])
            {
                case 0: //boundary lite
                    if (NPC.ai[3] == 0)
                    {
                        if (AliveCheck(player))
                        {
                            NPC.ai[3] = 1;
                            NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (Phase2Check())
                        return;

                    NPC.velocity = Vector2.Zero;
                    if (++NPC.ai[1] > 2) //boundary
                    {
                        SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                        NPC.ai[1] = 0;
                        //ai3 - 300 so that when attack ends, the projs will behave like at start of attack normally (straight streams)
                        NPC.ai[2] += (float)Math.PI / 8 / 480 * (NPC.ai[3] - 300) * NPC.localAI[0];


                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int max = 10;
                            for (int i = 0; i < max; i++)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -7f).RotatedBy(NPC.ai[2] + MathHelper.TwoPi / max * i),
                                 ModContent.ProjectileType<MutantEye>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                            }
                        }
                    }

                    if (++NPC.ai[3] > (360))
                    {
                        P1NextAttackOrMasoOptions(NPC.ai[0]);
                    }
                    break;

                case 1:
                    PrepareMutantSword();
                    break;

                case 2:
                    MutantSword();
                    break;

                default:
                    break;
            }
        }
        void PrepareMutantSword()
        {
            int signus = 11;
            if (NPC.ai[0] == 60 && Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f && Main.expertMode)
                if (NPC.ai[2] == 0) //move onscreen so player can see
                {
                    if (!AliveCheck(player))
                        return;

                    Vector2 targetPos = player.Center;
                    targetPos.X += 420 * Math.Sign(NPC.Center.X - player.Center.X);
                    targetPos.Y -= 210 * signus;
                    Movement(targetPos, 1.2f);

                    if ((++NPC.localAI[0] > 30) && NPC.Distance(targetPos) < 64)
                    {
                        NPC.velocity = Vector2.Zero;
                        NPC.netUpdate = true;

                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                        NPC.localAI[1] = Math.Sign(player.Center.X - NPC.Center.X);
                        float startAngle = MathHelper.PiOver4 * -NPC.localAI[1];
                        NPC.ai[2] = startAngle * -4f / 20 * signus; //travel the full arc over number of ticks
                        if (signus < 0)
                            startAngle += MathHelper.PiOver2 * -NPC.localAI[1];

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 offset = Vector2.UnitY.RotatedBy(startAngle) * -MUTANT_SWORD_SPACING;

                            void MakeSword(Vector2 pos, float spacing, float rotation = 0)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + pos, Vector2.Zero, ModContent.ProjectileType<MutantSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, NPC.whoAmI, spacing);
                            }

                            for (int i = 0; i < MUTANT_SWORD_MAX; i++)
                            {
                                MakeSword(offset * i, MUTANT_SWORD_SPACING * i);
                            }

                            for (int i = -1; i <= 1; i += 2)
                            {
                                MakeSword(offset.RotatedBy(MathHelper.ToRadians(26.5f * i)), 60 * 3);
                                MakeSword(offset.RotatedBy(MathHelper.ToRadians(40 * i)), 60 * 4f);
                            }
                        }
                    }
                }
                else
                {
                    NPC.velocity = Vector2.Zero;

                    FancyFireballs((int)(NPC.ai[1] / 90f * 60f));

                    if (++NPC.ai[1] > 90)
                    {
                        if (NPC.ai[0] != 9)
                            NPC.ai[0]++;

                        NPC.localAI[2]++; //progresses state in p1, counts swings in p2

                        Vector2 targetPos = player.Center;
                        targetPos.X -= 300 * NPC.ai[2];
                        NPC.velocity = (targetPos - NPC.Center) / 20;
                        NPC.ai[1] = 0;
                        NPC.netUpdate = true;
                    }

                    NPC.direction = NPC.spriteDirection = Math.Sign(NPC.localAI[1]);
                }
        }
        void MutantSword()
        {
            float lookSign = Math.Sign(NPC.localAI[1]);
            float arcSign = Math.Sign(NPC.ai[2]);
            const int max = 8; //spread
            Vector2 offset = lookSign * Vector2.UnitX.RotatedBy(MathHelper.PiOver4 * arcSign);

            const float length = MUTANT_SWORD_SPACING * MUTANT_SWORD_MAX / 2f;
            Vector2 spawnPos = NPC.Center + length * offset;
            Vector2 baseDirection = player.DirectionFrom(spawnPos);
            if (NPC.ai[0] == 61 && Main.LocalPlayer.active && NPC.Distance(Main.LocalPlayer.Center) < 3000f && Main.expertMode)
                //      Main.LocalPlayer.AddBuff(ModContent.BuffType<Purged>(), 2);

                NPC.ai[3] += NPC.ai[2];
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.localAI[1]);

            if (NPC.ai[1] == 20)
            {
                if (!Main.dedServ && Main.LocalPlayer.active)
                    if ((NPC.ai[0] != 9))
                    {
                        if (!Main.dedServ)
                            for (int i = 0; i < Bolts; i++)
                            {
                                int x = i;
                                if (x >= Bolts / 2)
                                {
                                    x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                                }
                                if (AliveCheck(player))
                                {
                                    const int distance = 180;
                                    //  int offset = 0;
                                    Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                                    SpawnLightning(NPC, pos);
                                }
                            }

                        for (int i = 0; i < max; i++)
                        {
                            Vector2 angle = baseDirection.RotatedBy(MathHelper.TwoPi / max * i);
                            float ai1 = i <= 2 || i == max - 2 ? 48 : 24;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MoonLordMoonBlast").Type,
                                    ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, MathHelper.WrapAngle(angle.ToRotation()), ai1);
                            }
                        }
                    }
            }

            if (++NPC.ai[1] > 25)
            {
                if (NPC.ai[0] == 9)
                {
                    P1NextAttackOrMasoOptions(NPC.ai[0]);
                }
                else if (NPC.localAI[2] < 5 * endTimeVariance)
                {
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.localAI[1] = 0;
                    NPC.netUpdate = true;
                }
                else
                {
                    ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
            }
        }

        #endregion
        #region phaze2

        void Phase2Transition()
        {
            /*NPC.velocity *= 0.9f;
            NPC.dontTakeDamage = true;

            if (NPC.buffType[0] != 0)
                NPC.DelBuff(0);

            if (NPC.ai[2] == 0)
            {

            }
            else
            {
                NPC.velocity = Vector2.Zero;
            }

            if (NPC.ai[1] < 240)
            {
                //make you stop attacking
                if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && NPC.Distance(Main.LocalPlayer.Center) < 3000)
                {
                    Main.LocalPlayer.controlUseItem = false;
                    Main.LocalPlayer.controlUseTile = false;
                    //      Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().NoUsingItems = true;
                }
            }

            if (NPC.ai[1] == 0)
            {
                ShtunUtils.ClearAllProjectiles(2, NPC.whoAmI);


                DramaticTransition(false, NPC.ai[2] == 0);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    ritualProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), ShtunUtils.ScaledProjectileDamage(0), 0f, Main.myPlayer, 0f, NPC.whoAmI);


                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual2>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual3>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual4>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);



                }
            }
            else if (NPC.ai[1] == 150)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRingHollow").Type, 0, 0f, Main.myPlayer, 5);
                }



                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);


                for (int i = 0; i < 50; i++)
                {
                    int d = Dust.NewDust(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 9f;
                }
            }
            else if (NPC.ai[1] > 150)
            {
                NPC.localAI[3] = 3;
            }

            if (!Phraze1 && NPC.ai[1] > 100)
            {
                Phraze1 = true;
                ShtunUtils.DisplayLocalizedText("Burn if the flames of shtuxian abyss!", textColor);
            }
            if (++NPC.ai[1] > 200)
            {*/
                EModeSpecialEffects();
                NPC.life = NPC.lifeMax;
                NPC.ai[0] = Main.rand.Next(new int[] { 11, 13, 16, 19, 20, 21, 24, 26, 29, 35, 37, 39, 42 }); //force a random choice
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.netUpdate = true;
                attackHistory.Enqueue(NPC.ai[0]);
            //}
        }
        void VoidRaysP2()
        {

            NPC.velocity = Vector2.Zero;
            if (--NPC.ai[1] < 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(2, 0).RotatedBy(NPC.ai[2]), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantMark1").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                NPC.ai[1] = 3;
                NPC.ai[2] += NPC.ai[3];

                if (NPC.localAI[0]++ == 20 || NPC.localAI[0] == 40)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] -= NPC.ai[3] / 3;

                    if ((NPC.localAI[0] == 21 && endTimeVariance > 0.75f) //sometimes skip to end
                    || (NPC.localAI[0] == 41 && endTimeVariance < 0.25f))
                        NPC.localAI[0] = 60;
                }
                else if (NPC.localAI[0] >= 60)
                {

                    ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
            }
        }
        void GiantDeathrayFall()
        {
            int fallintu = 0;
            NPC.velocity = Vector2.Zero;
            for (int i = 0; i < Bolts; i++)
            {
                int x = i;
                if (x >= Bolts / 2)
                {
                    x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                }
                if (AliveCheck(player))
                {
                    const int distance = 180;
                    //      int offset = 0;
                    Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                    SpawnLightning(NPC, pos);
                }
            }
            for (int i = 0; i < Bolts; i++)
            {
                int x = i;
                if (x >= Bolts / 3)
                {
                    x = (Bolts / 3) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                }
                if (AliveCheck(player))
                {
                    const int distance = 180;
                    //      int offset = 0;
                    Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                    SpawnLightning(NPC, pos);
                }
            }
            for (int i = 0; i < Bolts; i++)
            {
                int x = i;
                if (x >= Bolts / 2)
                {
                    x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                }
                if (AliveCheck(player))
                {
                    const int distance = 50;
                    //      int offset = 0;
                    Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                    SpawnLightning(NPC, pos);
                }
            }
            for (int i = 0; i < 50; i++)
            {
                ++fallintu;
                if (++fallintu == 50)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, Math.Max(600f, player.Center.Y - 2000f)), Vector2.UnitY, ModContent.ProjectileType<WillDeathraySmall20>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3), 0f, Main.myPlayer, player.Center.X + Main.rand.NextFloat(1700f, -1700f), NPC.whoAmI);

                    NPC.ai[1] = 3;
                    NPC.ai[2] += NPC.ai[3];
                    fallintu = 0;
                }
            }
            ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
        }
        void BoundaryBulletHellP2()
        {

            NPC.velocity = Vector2.Zero;
            if (NPC.localAI[0] == 0)
            {
                NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(player.Center.X, Math.Max(600f, player.Center.Y - 2000f)), Vector2.UnitY, ModContent.ProjectileType<WillDeathraySmall20>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3), 0f, Main.myPlayer, player.Center.X + Main.rand.NextFloat(700f, -700f), NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, NPC.whoAmI, -2);
            }
            if (NPC.ai[3] > 60 && ++NPC.ai[1] > 2)
            {
                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                NPC.ai[1] = 0;
                NPC.ai[2] += (float)Math.PI / 8 / 480 * NPC.ai[3] * NPC.localAI[0];
                if (NPC.ai[2] > (float)Math.PI)
                    NPC.ai[2] -= (float)Math.PI * 2;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int max = 9;

                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -6f).RotatedBy(NPC.ai[2] + Math.PI * 2 / max * i),
                       ModContent.ProjectileType<MutantEye>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                    }
                }
            }

            int endTime = 360 + 60 + (int)(240 * 2 * (endTimeVariance - 0.33f));
            if (++NPC.ai[3] > endTime)
            {

                ChooseNextAttack(11, 13, 19, 20, 21, 24, 31, 33, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void ApproachForNextAttackP2()
        {
            if (!AliveCheck(player)) //проверяет живой ли игрок
                return;
            Vector2 targetPos = player.Center + player.DirectionTo(NPC.Center) * 300; //устанавливает нужную позицию
            if (NPC.Distance(targetPos) > 50 && ++NPC.ai[2] < 180) //проверяет дистанцию
            {
                Movement(targetPos, 0.8f); //двигается
            }
            else
            {
                NPC.netUpdate = true;
                NPC.ai[0]++; //добавляет к счетчику атаки
                NPC.ai[1] = 0; //ставит задержку на 0
                NPC.ai[2] = player.DirectionTo(NPC.Center).ToRotation(); //устанавливает позицию для атаки
                NPC.ai[3] = (float)Math.PI / 10f;
                NPC.localAI[0] = 0; //таймер на 0
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center); // проигрывает звук
                if (player.Center.X < NPC.Center.X)
                    NPC.ai[3] *= -1;
            }
        }
        void PrepareSpearDashPredictiveP2()
        {
            if (NPC.ai[3] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[3] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearSpin").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 180); // + 60);
            }

            if (++NPC.ai[1] > 180)
            {
                if (!AliveCheck(player))
                    return;
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[3] = 0;
            }

            Vector2 targetPos = player.Center;
            targetPos.Y += 400f * Math.Sign(NPC.Center.Y - player.Center.Y); //can be above or below
            Movement(targetPos, 0.7f, false);
            if (NPC.Distance(player.Center) < 200)
                Movement(NPC.Center + NPC.DirectionFrom(player.Center), 1.4f);
        }
        void SpearDashPredictiveP2()
        {
            //
            if (NPC.localAI[1] == 0) //max number of attacks
            {
                NPC.localAI[1] = Main.rand.Next(7);

            }

            if (NPC.ai[1] == 0) //telegraph
            {
                if (!AliveCheck(player))
                    return;

                if (NPC.ai[2] == NPC.localAI[1] - 1)
                {
                    if (NPC.Distance(player.Center) > 450) //get closer for last dash
                    {
                        Movement(player.Center, 0.6f);
                        return;
                    }

                    NPC.velocity *= 0.75f; //try not to bump into player
                }

                if (NPC.ai[2] < NPC.localAI[1])
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathrayAim").Type, 0, 0f, Main.myPlayer, 55, NPC.whoAmI);

                    if (NPC.ai[2] == NPC.localAI[1] - 1)
                    {
                        SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 4);
                    }
                }
            }

            NPC.velocity *= 0.9f;

            if (NPC.ai[1] < 55) //track player up until just before dash
            {
                NPC.localAI[0] = NPC.DirectionTo(player.Center + player.velocity * 30f).ToRotation();
            }

            int endTime = 60;
            if (NPC.ai[2] == NPC.localAI[1] - 1)
                endTime = 80;
            if ((NPC.ai[2] == 0 || NPC.ai[2] >= NPC.localAI[1]))
                endTime = 0;
            if (++NPC.ai[1] > endTime)
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[3] = 0;
                if (++NPC.ai[2] > NPC.localAI[1])
                {
                    ChooseNextAttack(16, 19, 20, 26, 29, 31, 33, 39, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
                else
                {
                    NPC.velocity = NPC.localAI[0].ToRotationVector2() * 45f;
                    float spearAi = 0f;
                    if (NPC.ai[2] == NPC.localAI[1])
                        spearAi = -2f;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearDash").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, spearAi);
                    }
                }
                NPC.localAI[0] = 0;
            }
        }
        void WhileDashingP2()
        {
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
            if (++NPC.ai[1] > 30)
            {
                if (!AliveCheck(player))
                    return;
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;

                //quickly bounce back towards player
                if (NPC.ai[0] == 14 && NPC.ai[2] == NPC.localAI[1] - 1 && NPC.Distance(player.Center) > 450)
                    NPC.velocity = NPC.DirectionTo(player.Center) * 16f;
            }
        }
        void PillarDunk()
        {
            if (!AliveCheck(player))
                return;

            int pillarAttackDelay = 60;

            if (Main.zenithWorld && NPC.ai[1] > 180)
                player.confused = true;

            if (NPC.ai[2] == 0 && NPC.ai[3] == 0) //target one corner of arena
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (ShtunUtils.HostCheck)
                {
                    void Clone(float ai1, float ai2, float ai3) => ShtunUtils.NewNPCEasy(NPC.GetSource_FromAI(), NPC.Center, ModContent.NPCType<ShtuxibusIllusion>(), NPC.whoAmI, NPC.whoAmI, ai1, ai2, ai3);
                    Clone(-1, 1, pillarAttackDelay * 4);
                    Clone(1, -1, pillarAttackDelay * 2);
                    Clone(1, 1, pillarAttackDelay * 3);
                    Clone(1, 1, pillarAttackDelay * 6);
                    if (Main.zenithWorld)
                    {
                        Clone(-1, 1, pillarAttackDelay * 7);
                        Clone(1, -1, pillarAttackDelay * 8);
                    }
                }


                NPC.netUpdate = true;
                NPC.ai[2] = NPC.Center.X;
                NPC.ai[3] = NPC.Center.Y;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>() && Main.projectile[i].ai[1] == NPC.whoAmI)
                    {
                        NPC.ai[2] = Main.projectile[i].Center.X;
                        NPC.ai[3] = Main.projectile[i].Center.Y;
                        break;
                    }
                }

                Vector2 offset = 1000f * Vector2.UnitX.RotatedBy(MathHelper.ToRadians(45));
                if (Main.rand.NextBool()) //always go to a side player isn't in but pick a way to do it randomly
                {
                    if (player.Center.X > NPC.ai[2])
                        offset.X *= -1;
                    if (Main.rand.NextBool())
                        offset.Y *= -1;
                }
                else
                {
                    if (Main.rand.NextBool())
                        offset.X *= -1;
                    if (player.Center.Y > NPC.ai[3])
                        offset.Y *= -1;
                }

                NPC.localAI[1] = NPC.ai[2]; //for illusions
                NPC.localAI[2] = NPC.ai[3];

                NPC.ai[2] = offset.Length();
                NPC.ai[3] = offset.ToRotation();
            }

            Vector2 targetPos = player.Center;
            targetPos.X += NPC.Center.X < player.Center.X ? -700 : 700;
            targetPos.Y += NPC.ai[1] < 240 ? 400 : 150;
            if (NPC.Distance(targetPos) > 50)
                Movement(targetPos, 1f);

            int endTime = 240 + pillarAttackDelay * 4 + 60;
            endTime += pillarAttackDelay * 2;

            NPC.localAI[0] = endTime - NPC.ai[1]; //for pillars to know remaining duration
            NPC.localAI[0] += 60f + 60f * (1f - NPC.ai[1] / endTime); //staggered despawn

            if (++NPC.ai[1] > endTime)
            {
                ChooseNextAttack(11, 13, 19, 20, 21, 24, 31, 33, 41, 44, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
            else if (NPC.ai[1] == pillarAttackDelay)
            {
                if (ShtunUtils.HostCheck)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5,
                        ModContent.Find<ModProjectile>(fargosouls.Name, "MutantPillar").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0, Main.myPlayer, 3, NPC.whoAmI);
                }
            }
            else if (NPC.ai[1] == pillarAttackDelay * 5)
            {
                if (ShtunUtils.HostCheck)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -5,
                        ModContent.Find<ModProjectile>(fargosouls.Name, "MutantPillar").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0, Main.myPlayer, 1, NPC.whoAmI);
                }
            }
        }
        void EOCStarSickles()
        {
            //
            if (!AliveCheck(player))
                return;

            if (NPC.ai[1] == 0)
            {
                float ai1 = 0;



                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantEyeOfCthulhu").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, ai1);
                    if (p != Main.maxProjectiles)
                        Main.projectile[p].timeLeft -= 30;
                }
            }
            if (NPC.ai[1] < 120)
            {
                NPC.ai[2] = player.Center.X;
                NPC.ai[3] = player.Center.Y;
            }
            int endTime = 60 + 180 + 150;
            Vector2 targetPos = new Vector2(NPC.ai[2], NPC.ai[3]);
            targetPos += NPC.DirectionFrom(targetPos).RotatedBy(MathHelper.ToRadians(-5)) * 450f;
            if (NPC.Distance(targetPos) > 50)
                Movement(targetPos, 0.25f);

            if (++NPC.ai[1] > endTime)
            {
                ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 33, 35, 37, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void PrepareSpearDashDirectP2()
        {
            if (NPC.ai[3] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[3] = 1;
                //NPC.velocity = NPC.DirectionFrom(player.Center) * NPC.velocity.Length();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearSpin").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 180);// + (FargoSoulsWorld.MasochistMode ? 10 : 20));
            }

            if (++NPC.ai[1] > 180)
            {
                if (!AliveCheck(player))
                    return;
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[3] = 0;
                //NPC.TargetClosest();
            }

            Vector2 targetPos = player.Center;
            targetPos.Y += 450f * Math.Sign(NPC.Center.Y - player.Center.Y); //can be above or below
            Movement(targetPos, 0.7f, false);
            if (NPC.Distance(player.Center) < 200)
                Movement(NPC.Center + NPC.DirectionFrom(player.Center), 1.4f);
        }
        void SpearDashDirectP2()
        {
            NPC.velocity *= 0.9f;

            if (NPC.localAI[1] == 0) //max number of attacks
            {

                NPC.localAI[1] = 7;

            }

            if (++NPC.ai[1] > 5)
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                if (++NPC.ai[2] > NPC.localAI[1])
                {

                    ChooseNextAttack(11, 13, 16, 19, 20, 31, 33, 35, 39, 42, 44, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);

                }
                else
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * 60f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(NPC.velocity), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpearDash").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);
                    }
                }
            }
        }
        void AbomSwordMassacare()
        {
            if (!AliveCheck(player))
                return;
            //       NPC.velocity *= 0.9f;


            if (NPC.ai[1] < 60)
                FancyFireballs((int)NPC.ai[1]);

            if (NPC.ai[1] == 0 && NPC.ai[2] != 2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                float ai1 = NPC.ai[2] == 1 ? -1 : 1;
                ai1 *= MathHelper.ToRadians(270) / 120 * -1 * 60; //spawning offset of sword below
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3, ai1);
                if (p != Main.maxProjectiles)
                {
                    Main.projectile[p].localAI[1] = NPC.whoAmI;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncProjectile, number: p);
                }
            }

            else if (NPC.ai[1] == 60 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.netUpdate = true;
                NPC.velocity = Vector2.Zero;

                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                float ai0 = NPC.ai[2] == 1 ? -1 : 1;
                ai0 *= MathHelper.ToRadians(270) / 120;
                Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy(-ai0 * 60);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<AbomSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);

                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -vel, ModContent.ProjectileType<AbomSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);
            }
            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;

                NPC.velocity.X = 0f;//(player.Center.X - NPC.Center.X) / 90 / 4;
                NPC.velocity.Y = 1.5f;
            }
        }
        void MassacareDash()
        {
            NPC.velocity.Y *= 0.97f;
            NPC.position += NPC.velocity;
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2] - NPC.Center.X);
            if (++NPC.ai[1] > 90)
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
            }
        }
        void MassacareWait()
        {
            if (!AliveCheck(player))

                NPC.localAI[2] = 0;
            targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
                //          Movement(targetPos, 0.7f);
                if (++NPC.ai[1] > 60)
                {
                    ChooseNextAttack(13, 19, 20, 21, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
        }
        void DeathrayDash()
        {
            if (!AliveCheck(player))
                return;
            NPC.velocity.X = NPC.ai[2] * 18f;
            MovementY(player.Center.Y - 250, Math.Abs(player.Center.Y - NPC.Center.Y) < 200 ? 2f : 0.7f);
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);

            if (++NPC.ai[3] > 5)
            {
                NPC.ai[3] = 0;

                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);

                float timeLeft = 2400 / Math.Abs(NPC.velocity.X) * 2 - NPC.ai[1] + 120;
                if (NPC.ai[1] <= 15)
                {
                    timeLeft = 0;
                }
                else
                {
                    if (NPC.localAI[2] != 0)
                        timeLeft = 0;
                    if (++NPC.localAI[2] > 2)
                        NPC.localAI[2] = 0;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                }
            }
            if (++NPC.ai[1] > 2400 / Math.Abs(NPC.velocity.X))
            {
                NPC.netUpdate = true;
                NPC.velocity.X = NPC.ai[2] * 18f;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                //NPC.ai[2] = 0; //will be reused shortly
                NPC.ai[3] = 0;
            }

        }
        void DeathrayDash2()
        {
            if (!AliveCheck(player))
                return;
            NPC.velocity.X = NPC.ai[2] * -18f;
            MovementY(player.Center.Y - 250, Math.Abs(player.Center.Y - NPC.Center.Y) < 200 ? 2f : 0.7f);
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
            if (++NPC.ai[3] > 5)
            {
                NPC.ai[3] = 0;

                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);

                float timeLeft = 2400 / Math.Abs(NPC.velocity.X) * 2 - NPC.ai[1] + 120;
                if (NPC.ai[1] <= 15)
                {
                    timeLeft = 0;
                }
                else
                {
                    if (NPC.localAI[2] != 0)
                        timeLeft = 0;
                    if (++NPC.localAI[2] > 2)
                        NPC.localAI[2] = 0;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(MathHelper.ToRadians(20) * (Main.rand.NextDouble() - 0.5)), ModContent.Find<ModProjectile>(fargosouls.Name, "AbomDeathrayMark").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, timeLeft);
                }
            }
            if (++NPC.ai[1] > 2400 / Math.Abs(NPC.velocity.X))
            {
                ChooseNextAttack(11, 19, 20, 29, 31, 33, 35, 37, 39, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void SpawnDestroyersForPredictiveThrow()
        {
            if (!AliveCheck(player))
                return;


            Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500;
            if (Math.Abs(targetPos.X - player.Center.X) < 150) //avoid crossing up player
            {
                targetPos.X = player.Center.X + 150 * Math.Sign(targetPos.X - player.Center.X);
                Movement(targetPos, 0.3f);
            }
            if (NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, 0.9f);
            }


            if (NPC.localAI[1] == 0) //max number of attacks
            {

                NPC.localAI[1] = 3;

            }

            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 30;
                int cap = 3;

                cap += 2;
                NPC.ai[1] += 15; //faster


                if (++NPC.ai[2] > cap)
                {
                    //NPC.TargetClosest();
                    NPC.ai[0]++;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient) //spawn worm
                    {
                        Vector2 vel = NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.ToRadians(120)) * 10f;
                        float ai1 = 0.8f + 0.4f * NPC.ai[2] / 5f;

                        ai1 += 0.4f;
                        int current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerHead").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, ai1);
                        //timeleft: remaining duration of this case + duration of next case + extra delay after + successive death
                        Main.projectile[current].timeLeft = 30 * (cap - (int)NPC.ai[2]) + 60 * (int)NPC.localAI[1] + 30 + (int)NPC.ai[2] * 6;
                        int max = Main.rand.Next(8, 19);
                        for (int i = 0; i < max; i++)
                            current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerBody").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                        int previous = current;
                        current = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDestroyerTail").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, Main.projectile[current].identity);
                        Main.projectile[previous].localAI[1] = Main.projectile[current].identity;
                        Main.projectile[previous].netUpdate = true;
                    }
                }
            }
        }
        void SpearTossPredictiveP2()
        {
            if (!AliveCheck(player))
                return;

            Vector2 targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 25)
                Movement(targetPos, 0.8f);

            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 0;
                bool shouldAttack = true;
                if (++NPC.ai[2] > NPC.localAI[1])
                {
                    shouldAttack = false;

                    ChooseNextAttack(11, 19, 20, 29, 31, 33, 35, 37, 39, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);

                }

                if ((shouldAttack) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = NPC.DirectionTo(player.Center + player.velocity * 30f) * 30f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, 1f);
                }
            }
            else if (NPC.ai[1] == 1 && (NPC.ai[2] < NPC.localAI[1]) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.DirectionTo(player.Center + player.velocity * 30f), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathrayAim").Type, 0, 0f, Main.myPlayer, 60f, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 2);
            }
        }
        void PrepareMechRayFan()
        {

            if (NPC.ai[1] == 0)
            {
                if (!AliveCheck(player))
                    return;


                NPC.ai[1] = 31; //skip the pause, skip the telegraph
            }

            if (NPC.ai[1] == 30)
            {
                SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center); //eoc roar
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, NPC.whoAmI, NPCID.Retinazer);
            }

            Vector2 targetPos;
            if (NPC.ai[1] < 30)
            {
                targetPos = player.Center + NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(15)) * 500f;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.3f);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(NPC.Center, 0, 0, DustID.Torch, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 12f;
                }

                targetPos = player.Center;
                targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                Movement(targetPos, 1.2f, false);
            }

            if (++NPC.ai[1] > 150 || (NPC.Distance(targetPos) < 64))
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                //NPC.TargetClosest();
            }
        }
        void MechRayFan()
        {
            CalamityMechRayFan();
            NPC.velocity = Vector2.Zero;

            if (NPC.ai[2] == 0)
            {
                NPC.ai[2] = Main.rand.NextBool() ? -1 : 1; //randomly aim either up or down
            }

            if (NPC.ai[3] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int max = 7;
                for (int i = 0; i <= max; i++)
                {
                    Vector2 dir = Vector2.UnitX.RotatedBy(NPC.ai[2] * i * MathHelper.Pi / max) * 6; //rotate initial velocity of telegraphs by 180 degrees depending on velocity of lasers
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + dir, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantGlowything").Type, 0, 0f, Main.myPlayer, dir.ToRotation(), NPC.whoAmI);
                }
            }

            int endTime = 60 + 180 + 150;

            if (NPC.ai[3] > (45) && NPC.ai[3] < 60 + 180 && ++NPC.ai[1] > 10)
            {
                NPC.ai[1] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float rotation = MathHelper.ToRadians(245) * NPC.ai[2] / 80f;
                    int timeBeforeAttackEnds = endTime - (int)NPC.ai[3];

                    void SpawnRay(Vector2 pos, float angleInDegrees, float turnRotation)
                    {
                        int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), pos, MathHelper.ToRadians(angleInDegrees).ToRotationVector2(),
                        ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray3").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0, Main.myPlayer, turnRotation, NPC.whoAmI);
                        if (p != Main.maxProjectiles && Main.projectile[p].timeLeft > timeBeforeAttackEnds)
                            Main.projectile[p].timeLeft = timeBeforeAttackEnds;
                    };

                    SpawnRay(NPC.Center, 8 * NPC.ai[2], rotation);
                    SpawnRay(NPC.Center, -8 * NPC.ai[2] + 180, -rotation);


                    Vector2 spawnPos = NPC.Center + NPC.ai[2] * -1800 * Vector2.UnitY;
                    SpawnRay(spawnPos, 8 * NPC.ai[2] + 180, rotation);
                    SpawnRay(spawnPos, -8 * NPC.ai[2], -rotation);

                }
            }

            void SpawnPrime(float varianceInDegrees, float rotationInDegrees)
            {
                SoundEngine.PlaySound(SoundID.Item21, NPC.Center);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float spawnOffset = (Main.rand.NextBool() ? -1 : 1) * Main.rand.NextFloat(1400, 1800);
                    float maxVariance = MathHelper.ToRadians(varianceInDegrees);
                    Vector2 aimPoint = NPC.Center - Vector2.UnitY * NPC.ai[2] * 600;
                    Vector2 spawnPos = aimPoint + spawnOffset * Vector2.UnitY.RotatedByRandom(maxVariance).RotatedBy(MathHelper.ToRadians(rotationInDegrees));
                    Vector2 vel = 32f * Vector2.Normalize(aimPoint - spawnPos);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantGuardian").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer);
                }
            }

            if (NPC.ai[3] < 180 && ++NPC.localAI[0] > 1)
            {
                NPC.localAI[0] = 0;
                SpawnPrime(15, 0);
            }

            if (++NPC.ai[3] > endTime)
            {
                ChooseNextAttack(11, 13, 16, 19, 21, 24, 29, 31, 33, 35, 37, 39, 41, 42, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);

                NPC.netUpdate = true;
            }
        }
        void PrepareFishron1()
        {
            if (!AliveCheck(player))
                return;
            Vector2 targetPos = new Vector2(player.Center.X, player.Center.Y + 600 * Math.Sign(NPC.Center.Y - player.Center.Y));
            Movement(targetPos, 1.4f, false);

            if (NPC.ai[1] == 0) //always dash towards same side i started on
                NPC.ai[2] = Math.Sign(NPC.Center.X - player.Center.X);

            if (++NPC.ai[1] > 60 || NPC.Distance(targetPos) < 64) //dive here
            {
                NPC.velocity.X = 30f * NPC.ai[2];
                NPC.velocity.Y = 0f;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
            }
        }
        void SpawnFishrons()
        {
            CalamityFishron();
            NPC.velocity *= 0.97f;
            if (NPC.ai[1] == 0)
            {
                NPC.ai[2] = Main.rand.NextBool() ? 1 : 0;
            }
            const int fishronDelay = 3;
            int maxFishronSets = 3;
            if (NPC.ai[1] % fishronDelay == 0 && NPC.ai[1] <= fishronDelay * maxFishronSets)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int j = -1; j <= 1; j += 2) //to both sides of player
                    {
                        int max = (int)NPC.ai[1] / fishronDelay;
                        for (int i = -max; i <= max; i++) //fan of fishron
                        {
                            if (Math.Abs(i) != max) //only spawn the outmost ones
                                continue;
                            float spread = MathHelper.Pi / 3 / (maxFishronSets + 1);
                            Vector2 offset = NPC.ai[2] == 0 ? Vector2.UnitY.RotatedBy(spread * i) * -450f * j : Vector2.UnitX.RotatedBy(spread * i) * 475f * j;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantFishron>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, offset.X, offset.Y);
                        }
                    }
                }
                for (int i = 0; i < 30; i++)
                {
                    int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 135, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 12f;
                }
            }

            if (++NPC.ai[1] > (60))
            {
                ChooseNextAttack(13, 19, 20, 21, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void PrepareTrueEyeDiveP2()
        {
            if (!AliveCheck(player))
                return;
            ssm.amiactive = true;
            Vector2 targetPos = player.Center;
            targetPos.X += 400 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y += 400;
            Movement(targetPos, 1.2f);

            //dive here
            if (++NPC.ai[1] > 60)
            {
                NPC.velocity.X = 30f * (NPC.position.X < player.position.X ? 1 : -1);
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y *= -1;
                NPC.velocity.Y *= 0.3f;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
            }
        }
        void PrepareNuke()
        {
            if (!AliveCheck(player))
                return;
            Vector2 targetPos = player.Center;
            targetPos.X += 400 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y -= 400;
            Movement(targetPos, 1.2f, false);
            if (++NPC.ai[1] > 60)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float gravity = 0.2f;
                    float time = 120f;
                    Vector2 distance = player.Center - NPC.Center;
                    distance.X = distance.X / time;
                    distance.Y = distance.Y / time - 0.5f * gravity * time;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, distance, ModContent.ProjectileType<MutantNuke>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), Main.myPlayer, (int)gravity);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantFishronRitual").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, NPC.whoAmI);
                }
                NPC.ai[0]++;
                NPC.ai[1] = 0;

                if (Math.Sign(player.Center.X - NPC.Center.X) == Math.Sign(NPC.velocity.X))
                    NPC.velocity.X *= -1f;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y *= -1f;
                NPC.velocity.Normalize();
                NPC.velocity *= 3f;

                NPC.netUpdate = true;
                //NPC.TargetClosest();
            }
        }
        void Nuke()
        {
            if (!AliveCheck(player))
                return;
            ssm.amiactive = true;
            Vector2 target = NPC.Bottom.Y < player.Top.Y
                ? player.Center + 300f * Vector2.UnitX * Math.Sign(NPC.Center.X - player.Center.X)
                : NPC.Center + 30 * NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(60) * Math.Sign(player.Center.X - NPC.Center.X));
            Movement(target, 0.1f);
            if (NPC.velocity.Length() > 2f)
                NPC.velocity = Vector2.Normalize(NPC.velocity) * 2f;


            if (NPC.ai[1] > (120))
            {
                if (!Main.dedServ && Main.LocalPlayer.active)
                    Main.LocalPlayer.GetModPlayer<ShtunPlayer>().Screenshake = 2;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 safeZone = NPC.Center;
                    safeZone.Y -= 100;
                    const float safeRange = 150 + 200;
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(2000, 2000);
                        if (Vector2.Distance(safeZone, spawnPos) < safeRange)
                        {
                            Vector2 directionOut = spawnPos - safeZone;
                            directionOut.Normalize();
                            spawnPos = safeZone + directionOut * Main.rand.NextFloat(safeRange, 2000);
                        }
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<MutantBomb>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer);
                    }
                }
            }

            if (++NPC.ai[1] > 360 + 360 * endTimeVariance)
            {
                ssm.amiactive = false;
                ChooseNextAttack(11, 13, 16, 19, 24, 26, 31, 35, 37, 39, 41, 42, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }

            if (NPC.ai[1] > 45)
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new Vector2();
                    offset.Y -= 100;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 150);
                    offset.Y += (float)(Math.Cos(angle) * 150);
                    Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset - new Vector2(4, 4), 0, 0, 229, 0, 0, 100, Color.White, 1.5f)];
                    dust.velocity = NPC.velocity;
                    if (Main.rand.NextBool(3))
                        dust.velocity += Vector2.Normalize(offset) * 5f;
                    dust.noGravity = true;
                }
            }
        }
        void TimeStopShtuxibus()
        {
            targetPos = player.Center + NPC.DirectionFrom(player.Center) * 500;
            if (NPC.ai[1] < 130 || (NPC.Distance(player.Center) > 200 && NPC.Distance(player.Center) < 600))
            {
                NPC.velocity *= 0.97f;
            }
            else if (NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, 2f);
                NPC.position += player.velocity / 4f;
            }

            if (NPC.ai[1] >= 10) //for timestop visual
            {

            }

            if (NPC.ai[1] == 10)
            {
                NPC.localAI[0] = Main.rand.NextFloat(2 * (float)Math.PI);
            }
            else if (NPC.ai[1] < 210)
            {
                int duration = 60 + Math.Max(2, 210 - (int)NPC.ai[1]);

                if (Main.LocalPlayer.active && !Main.LocalPlayer.dead)
                {

                    Main.LocalPlayer.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, duration);
                    //Main.LocalPlayer.AddBuff(BuffID.ChaosState, 300); //no cheesing this attack
                }

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active)
                        Main.npc[i].AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, duration, true);
                }

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && !Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune)
                        Main.projectile[i].GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFrozen = duration;
                }


                if (NPC.ai[1] < 130 && ++NPC.ai[2] > 12)
                {
                    NPC.ai[2] = 0;

                    bool altAttack = NPC.localAI[2] != 0;

                    int baseDistance = 300; //altAttack ? 500 : 400;
                    float offset = 250f;
                    float speed = 8f;
                    int damage = ShtunUtils.ScaledProjectileDamage(NPC.damage); //altAttack ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 2 / 7 ): ShtunUtils.ScaledProjectileDamage(NPC.damage);

                    if (NPC.ai[1] < 130 - 45 || !altAttack)
                    {
                        if (altAttack && NPC.ai[3] % 2 == 0) //emode p2, asgore rings
                        {
                            float radius = baseDistance + NPC.ai[3] * offset;
                            int circumference = (int)(2f * (float)Math.PI * radius);

                            //always flip it to opposite the previous side
                            NPC.localAI[0] = MathHelper.WrapAngle(NPC.localAI[0] + (float)Math.PI + Main.rand.NextFloat((float)Math.PI / 2));
                            const float safeRange = 60f;

                            const int arcLength = 120;
                            for (int i = 0; i < circumference; i += arcLength)
                            {
                                float angle = i / radius;
                                if (angle > 2 * Math.PI - MathHelper.WrapAngle(MathHelper.ToRadians(safeRange)))
                                    continue;

                                float spawnOffset = radius;// + Main.rand.NextFloat(-16, 16);
                                Vector2 spawnPos = player.Center + spawnOffset * Vector2.UnitX.RotatedBy(angle + NPC.localAI[0]);
                                Vector2 vel = speed * player.DirectionFrom(spawnPos);
                                float ai0 = player.Distance(spawnPos) / speed + 30;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "CosmosInvaderTime").Type, damage, 0f, Main.myPlayer, ai0, vel.ToRotation());
                            }
                        }
                        else //scatter
                        {
                            int max = 12 + (int)NPC.ai[3] * (NPC.localAI[2] == 0 ? 2 : 4);
                            float rotationOffset = Main.rand.NextFloat((float)Math.PI * 2);
                            for (int i = 0; i < max; i++)
                            {
                                float ai0 = baseDistance;
                                float distance = ai0 + NPC.ai[3] * offset;
                                Vector2 spawnPos = player.Center + distance * Vector2.UnitX.RotatedBy(2 * Math.PI / max * i + rotationOffset);
                                Vector2 vel = speed * player.DirectionFrom(spawnPos);// distance * player.DirectionFrom(spawnPos) / ai0;
                                ai0 = distance / speed + 30;
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "CosmosInvaderTime").Type, damage, 0f, Main.myPlayer, ai0, vel.ToRotation());
                            }
                        }
                    }

                    NPC.ai[3]++;
                }
            }



            if (++NPC.ai[1] > 480)
            {
                NPC.TargetClosest();
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.netUpdate = true;
            }




        }
        void PrepareSlimeRain()
        {
            if (!AliveCheck(player))
                return;
            ssm.amiactive = true;
            Vector2 targetPos = player.Center;
            targetPos.X += 900 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y += 700;
            Movement(targetPos, 2f);

            if (++NPC.ai[2] > 30 || (NPC.Distance(targetPos) < 64))
            {
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
                //NPC.TargetClosest();
            }
        }
        void FunnyBettlee()
        {

            NPC.velocity *= 0.9f;

            if (NPC.ai[3] == 0)
                NPC.ai[3] = NPC.Center.X < player.Center.X ? -1 : 1;

            if (++NPC.ai[2] > (NPC.localAI[2] == 1 ? 40 : 60))
            {
                NPC.ai[2] = 0;
                SoundEngine.PlaySound(SoundID.Item92, NPC.Center);

                if (NPC.localAI[0] > 0)
                    NPC.localAI[0] = -1;
                else
                    NPC.localAI[0] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 projTarget = NPC.Center;
                    projTarget.X += 1800 * NPC.ai[3];
                    projTarget.Y += 1800 * -NPC.localAI[0];
                    int max = 50;
                    int increment = NPC.localAI[2] == 1 ? 180 : 250;
                    projTarget.Y += Main.rand.NextFloat(increment);
                    for (int i = 0; i < max; i++)
                    {
                        projTarget.Y += increment * NPC.localAI[0];
                        Vector2 speed = (projTarget - NPC.Center) / 40;
                        float ai0 = (NPC.localAI[2] == 1 ? 8 : 6) * -NPC.ai[3]; //x speed of beetles
                        float ai1 = 10 * -NPC.localAI[0]; //y speed of beetles
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<ChampionBeetle>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, ai1);
                    }
                }
            }

            if (++NPC.ai[1] > 440)
            {



                ChooseNextAttack(11, 16, 19, 20, 26, 31, 33, 37, 39, 41, 42, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                NPC.netUpdate = true;
            }
        }
        void LifeChampFireballz()
        {
            NPC.velocity *= 0.98f;

            if (++NPC.ai[2] > (NPC.localAI[2] == 1 ? 45 : 60))
            {
                if (++NPC.ai[3] > 1) //spray fireballs that home down
                {

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        //spawn anywhere above self
                        Vector2 target = new Vector2(Main.rand.NextFloat(1000), 0).RotatedBy(Main.rand.NextDouble() * -Math.PI);
                        Vector2 speed = 2 * target / 60;
                        float acceleration = -speed.Length() / 60;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<LifeFireball>(),
                            ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 60f, acceleration);

                    }
                }

                if (NPC.ai[2] > 260)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] = 0;
                }
            }

            if (++NPC.ai[1] > 480)
            {
                ChooseNextAttack(11, 16, 19, 20, 26, 31, 33, 37, 39, 41, 42, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                NPC.netUpdate = true;
            }
        }
        void SlimeRain()
        {
            if (NPC.ai[3] == 0)
            {
                NPC.ai[3] = 1;
                //Main.NewText(NPC.position.Y);
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSlimeRain").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI);
            }

            if (NPC.ai[1] == 0) //telegraphs for where slime will fall
            {
                bool first = NPC.localAI[0] == 0;
                NPC.localAI[0] = Main.rand.Next(5, 9) * 120;
                if (first) //always start on the same side as the player
                {
                    if (player.Center.X < NPC.Center.X && NPC.localAI[0] > 1200)
                        NPC.localAI[0] -= 1200;
                    else if (player.Center.X > NPC.Center.X && NPC.localAI[0] < 1200)
                        NPC.localAI[0] += 1200;
                }
                else //after that, always be on opposite side from player
                {
                    if (player.Center.X < NPC.Center.X && NPC.localAI[0] < 1200)
                        NPC.localAI[0] += 1200;
                    else if (player.Center.X > NPC.Center.X && NPC.localAI[0] > 1200)
                        NPC.localAI[0] -= 1200;
                }
                NPC.localAI[0] += 60;

                Vector2 basePos = NPC.Center;
                basePos.X -= 1800;
                for (int i = -360; i <= 2760; i += 120) //spawn telegraphs
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        if (i + 60 == (int)NPC.localAI[0])
                            continue;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), basePos.X + i + 60, basePos.Y, 0f, 0f, ModContent.ProjectileType<MutantReticle>(), 0, 0f, Main.myPlayer);
                    }
                }


                NPC.ai[1] += 20; //less startup
                NPC.ai[2] += 20; //stay synced

            }

            if (NPC.ai[1] > 120 && NPC.ai[1] % 5 == 0) //rain down slime balls
            {
                SoundEngine.PlaySound(SoundID.Item34, player.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int num3 = 540;

                    num3 += 240 + (int)(120f * endTimeVariance) - 50;

                    if (NPC.ai[2] > 510f)
                    {
                        if (NPC.ai[2] % 3f == 1f && NPC.ai[2] < (float)(num3 - 80))
                        {
                            Vector2 val = player.Center + new Vector2(((float)Main.rand.Next(2) - 0.5f) * 1800f, 0f);
                            Vector2 val2 = Utils.SafeNormalize(player.Center - val, Vector2.Zero) * (15f + ((float)Main.rand.Next(2) - 0.5f) * 4f);
                            Projectile obj2 = Projectile.NewProjectileDirect(NPC.GetSource_FromAI((string)null), val + new Vector2(0f, ((float)Main.rand.Next(2) - 0.5f) * 12f), val2, ModContent.Find<ModProjectile>(fargosouls.Name, "BigSting22").Type, 50, 0f, 0, 256f, 0f);
                            obj2.hostile = true;
                            obj2.friendly = false;
                        }
                        if (NPC.ai[1] > 170f)
                        {
                            NPC.ai[1] -= 30f;
                        }
                        if (NPC.localAI[1] == 0f)
                        {
                            float num4 = NPC.Center.X - 1800f + NPC.localAI[0];
                            NPC.localAI[1] = Math.Sign(NPC.Center.X - num4);
                        }
                        NPC.localAI[0] += 4.1666665f * NPC.localAI[1];
                    }
                    void Slime(Vector2 pos, float off, Vector2 vel)
                    {
                        //dont flip in maso wave 3
                        int flip = NPC.ai[2] < 180 * 2 && Main.rand.NextBool() ? -1 : 1;
                        Vector2 spawnPos = pos + off * Vector2.UnitY * flip;
                        float ai0 = ShtunUtils.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? 0f : NPC.Distance(Main.projectile[ritualProj].Center);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel * flip, ModContent.ProjectileType<MutantSlimeBall>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0);
                    }

                    Vector2 basePos = NPC.Center;
                    basePos.X -= 1800;
                    float yOffset = -1800;

                    const float safeRange = 110;
                    for (int i = -360; i <= 4760; i += 75)
                    {
                        float xOffset = i + Main.rand.Next(75);
                        if (Math.Abs(xOffset - NPC.localAI[0]) < safeRange) //dont fall over safespot
                            continue;

                        Vector2 spawnPos = basePos;
                        spawnPos.X += xOffset;
                        Vector2 velocity = Vector2.UnitY * Main.rand.NextFloat(15f, 20f);

                        Slime(spawnPos, yOffset, velocity);
                    }

                    //spawn right on safespot borders
                    Slime(basePos + Vector2.UnitX * (NPC.localAI[0] + safeRange), yOffset, Vector2.UnitY * 20f);
                    Slime(basePos + Vector2.UnitX * (NPC.localAI[0] - safeRange), yOffset, Vector2.UnitY * 20f);
                }
            }
            if (++NPC.ai[1] > 180)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[1] = 0;
            }

            const int masoMovingRainAttackTime = 180 * 3 - 60;
            if (NPC.ai[1] == 120 && NPC.ai[2] < masoMovingRainAttackTime && Main.rand.NextBool(3))
                NPC.ai[2] = masoMovingRainAttackTime;

            NPC.velocity = Vector2.Zero;

            const int timeToMove = 240;

            if (NPC.ai[2] == masoMovingRainAttackTime)
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

            if (NPC.ai[2] > masoMovingRainAttackTime + 30)
            {
                if (NPC.ai[1] > 170) //let the balls keep falling
                    NPC.ai[1] -= 30;

                if (NPC.localAI[1] == 0) //direction to move safespot towards
                {
                    float safespotX = NPC.Center.X - 2000f + NPC.localAI[0];
                    NPC.localAI[1] = Math.Sign(NPC.Center.X - safespotX);
                }

                //move the safespot
                NPC.localAI[0] += 1800f / timeToMove * NPC.localAI[1];
            }


            int endTime = 180 * 3;

            endTime += timeToMove + (int)(120 * endTimeVariance) - 30;
            if (++NPC.ai[2] > endTime)
            {
                ChooseNextAttack(11, 16, 19, 20, 26, 31, 33, 37, 39, 41, 42, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                ssm.amiactive = false;
            }
        }
        void PrepareFishron2()
        {
            if (!AliveCheck(player))
                return;

            Vector2 targetPos = player.Center;
            targetPos.X += 400 * (NPC.Center.X < targetPos.X ? -1 : 1);
            targetPos.Y -= 400;
            Movement(targetPos, 0.9f);

            if (++NPC.ai[1] > 60 || (NPC.Distance(targetPos) < 32)) //dive here
            {
                NPC.velocity.X = 35f * (NPC.position.X < player.position.X ? 1 : -1);
                NPC.velocity.Y = 10f;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
                //NPC.TargetClosest();
            }
        }
        void PrepareOkuuSpheresP2()
        {
            if (!AliveCheck(player))
                return;

            Vector2 targetPos = player.Center + player.DirectionTo(NPC.Center) * 450;
            if (++NPC.ai[1] < 180 && NPC.Distance(targetPos) > 50)
            {
                Movement(targetPos, 0.8f);
            }
            else
            {

                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
            }
        }
        void OkuuSpheresP2()
        {
            NPC.velocity = Vector2.Zero;

            int endTime = 420 + (int)(360 * (endTimeVariance - 0.33f));

            if (++NPC.ai[1] > 10 && NPC.ai[3] > 60 && NPC.ai[3] < endTime - 60)
            {
                NPC.ai[1] = 0;
                float rotation = MathHelper.ToRadians(60) * (NPC.ai[3] - 45) / 240 * NPC.ai[2];
                int max = 12;
                float speed = 15f;
                SpawnSphereRing(max, speed, ShtunUtils.ScaledProjectileDamage(NPC.damage), -1f, rotation);
                SpawnSphereRing(max, speed, ShtunUtils.ScaledProjectileDamage(NPC.damage), 1f, rotation);
            }

            if (NPC.ai[2] == 0)
            {
                NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                NPC.ai[3] = Main.rand.NextFloat((float)Math.PI * 2);
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, NPC.whoAmI, -2);
            }

            if (++NPC.ai[3] > endTime)
            {

                ChooseNextAttack(13, 19, 20, 13, 44, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }

            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
        }
        void ShtuxibusJavelinsP2()
        {//
            if (!AliveCheck(player))
                return;
            Vector2 vel = player.Center - NPC.Center;
            NPC.rotation = vel.ToRotation();

            const float moveSpeed = 0.25f;

            if (vel.X > 0) //im on left side of target
            {
                vel.X -= 450;
                NPC.direction = NPC.spriteDirection = 1;
            }
            else //im on right side of target
            {
                vel.X += 450;
                NPC.direction = NPC.spriteDirection = -1;
            }
            vel.Y -= 200f;
            vel.Normalize();
            vel *= 16f;
            if (NPC.velocity.X < vel.X)
            {
                NPC.velocity.X += moveSpeed;
                if (NPC.velocity.X < 0 && vel.X > 0)
                    NPC.velocity.X += moveSpeed;
            }
            else if (NPC.velocity.X > vel.X)
            {
                NPC.velocity.X -= moveSpeed;
                if (NPC.velocity.X > 0 && vel.X < 0)
                    NPC.velocity.X -= moveSpeed;
            }
            if (NPC.velocity.Y < vel.Y)
            {
                NPC.velocity.Y += moveSpeed;
                if (NPC.velocity.Y < 0 && vel.Y > 0)
                    NPC.velocity.Y += moveSpeed;
            }
            else if (NPC.velocity.Y > vel.Y)
            {
                NPC.velocity.Y -= moveSpeed;
                if (NPC.velocity.Y > 0 && vel.Y < 0)
                    NPC.velocity.Y -= moveSpeed;
            }

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = NPC.localAI[2] == 1 ? 30 : 40;

                if (NPC.ai[1] < 110 || NPC.localAI[3] == 1)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            const int time = 120;
                            float speed = Main.rand.NextFloat(240, 720) / time * 2f;
                            Vector2 velocity = speed * NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.PiOver2);
                            float ai1 = speed / time;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<WillJavelin>(), NPC.damage / 4, 0f, Main.myPlayer, 0f, ai1);
                        }
                    }
                }
            }

            if (++NPC.ai[1] > 150)
            {
                ChooseNextAttack(13, 19, 20, 13, 44, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        private void MovementERI(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += fastY ? speedModifier * 2 : speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= fastY ? speedModifier * 2 : speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > cap)
                NPC.velocity.X = cap * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > cap)
                NPC.velocity.Y = cap * Math.Sign(NPC.velocity.Y);
        }
        void ShtuxibusFireballsP2()
        {
            //
            if (!AliveCheck(player))
                return;
            Vector2 vel = player.Center - NPC.Center;
            NPC.rotation = vel.ToRotation();
            ShtunUtils.DisplayLocalizedText("START FIREBALLS", textColor2);
            const float moveSpeed = 0.25f;

            if (vel.X > 0) //im on left side of target
            {
                vel.X -= 450;
                NPC.direction = NPC.spriteDirection = 1;
            }
            else //im on right side of target
            {
                vel.X += 450;
                NPC.direction = NPC.spriteDirection = -1;
            }
            vel.Y -= 200f;
            vel.Normalize();
            vel *= 16f;
            if (NPC.velocity.X < vel.X)
            {
                NPC.velocity.X += moveSpeed;
                if (NPC.velocity.X < 0 && vel.X > 0)
                    NPC.velocity.X += moveSpeed;
            }
            else if (NPC.velocity.X > vel.X)
            {
                NPC.velocity.X -= moveSpeed;
                if (NPC.velocity.X > 0 && vel.X < 0)
                    NPC.velocity.X -= moveSpeed;
            }
            if (NPC.velocity.Y < vel.Y)
            {
                NPC.velocity.Y += moveSpeed;
                if (NPC.velocity.Y < 0 && vel.Y > 0)
                    NPC.velocity.Y += moveSpeed;
            }
            else if (NPC.velocity.Y > vel.Y)
            {
                NPC.velocity.Y -= moveSpeed;
                if (NPC.velocity.Y > 0 && vel.Y < 0)
                    NPC.velocity.Y -= moveSpeed;
            }

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = NPC.localAI[2] == 1 ? 30 : 40;

                if (NPC.ai[1] < 240 || NPC.localAI[3] == 1)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            const int time = 120;
                            float speed = Main.rand.NextFloat(240, 720) / time * 2f;
                            Vector2 velocity = speed * NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.PiOver2);
                            float ai1 = speed / time;
                        }
                    }
                }
            }

            if (++NPC.ai[1] > 250)
            {
                ChooseNextAttack(13, 19, 20, 13, 44, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void ShtuxibusFireballsP3()
        {
            //
            if (!AliveCheck(player))
                return;
            Vector2 vel = player.Center - NPC.Center;
            NPC.rotation = vel.ToRotation();

            const float moveSpeed = 0.25f;

            if (vel.X > 0) //im on left side of target
            {
                vel.X -= 450;
                NPC.direction = NPC.spriteDirection = 1;
            }
            else //im on right side of target
            {
                vel.X += 450;
                NPC.direction = NPC.spriteDirection = -1;
            }
            vel.Y -= 200f;
            vel.Normalize();
            vel *= 16f;
            if (NPC.velocity.X < vel.X)
            {
                NPC.velocity.X += moveSpeed;
                if (NPC.velocity.X < 0 && vel.X > 0)
                    NPC.velocity.X += moveSpeed;
            }
            else if (NPC.velocity.X > vel.X)
            {
                NPC.velocity.X -= moveSpeed;
                if (NPC.velocity.X > 0 && vel.X < 0)
                    NPC.velocity.X -= moveSpeed;
            }
            if (NPC.velocity.Y < vel.Y)
            {
                NPC.velocity.Y += moveSpeed;
                if (NPC.velocity.Y < 0 && vel.Y > 0)
                    NPC.velocity.Y += moveSpeed;
            }
            else if (NPC.velocity.Y > vel.Y)
            {
                NPC.velocity.Y -= moveSpeed;
                if (NPC.velocity.Y > 0 && vel.Y < 0)
                    NPC.velocity.Y -= moveSpeed;
            }

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = NPC.localAI[2] == 1 ? 30 : 40;

                if (NPC.ai[1] < 240 || NPC.localAI[3] == 1)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            TryLaserAttackP3();
                        }
                    }
                }
            }

            if (++NPC.ai[1] > 250)
            {
                ChooseNextAttack(13, 19, 20, 13, 44, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        private void TeleportDust()
        {
            for (int index1 = 0; index1 < 25; ++index1)
            {
                int index2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 272, 0f, 0f, 100, new Color(), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 7f;
                Main.dust[index2].noLight = true;
                int index3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 272, 0f, 0f, 100, new Color(), 1f);
                Main.dust[index3].velocity *= 4f;
                Main.dust[index3].noGravity = true;
                Main.dust[index3].noLight = true;
            }
        }
        void StrongAttackTeleport(Vector2 teleportTarget = default)
        {
            const float range = 450f;
            if (teleportTarget == default ? NPC.Distance(player.Center) < range : NPC.Distance(teleportTarget) < 80)
                return;

            TeleportDust();
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (teleportTarget != default)
                    NPC.Center = teleportTarget;
                else if (player.velocity == Vector2.Zero)
                    NPC.Center = player.Center + range * Vector2.UnitX.RotatedByRandom(2 * Math.PI);
                else
                    NPC.Center = player.Center + range * Vector2.Normalize(player.velocity);
                NPC.velocity /= 2f;
                NPC.netUpdate = true;
            }
            TeleportDust();
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }
        void PaladinHammster()
        {
            //
            if (!AliveCheck(player))
                return;

            if (NPC.localAI[1] == 0) //pick random number of teleports to do
            {
                NPC.localAI[1] = NPC.localAI[3] > 1 ? Main.rand.Next(3, 10) : Main.rand.Next(3, 6);
                NPC.netUpdate = true;
            }

            NPC.velocity = Vector2.Zero;
            if (++NPC.ai[1] > (NPC.localAI[3] > 1 ? 10 : 20) && NPC.ai[2] < NPC.localAI[1])
            {
                //NPC.localAI[1] = 0;
                NPC.ai[1] = 0;
                NPC.ai[2]++;

                TeleportDust();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    bool wasOnLeft = NPC.Center.X < player.Center.X;
                    NPC.Center = player.Center + 200 * Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0, 2 * (float)Math.PI));
                    if (wasOnLeft ? NPC.Center.X < player.Center.X : NPC.Center.X > player.Center.X)
                    {
                        float x = player.Center.X - NPC.Center.X;
                        NPC.position.X += x * 2;
                    }
                    NPC.netUpdate = true;
                }
                TeleportDust();
                SoundEngine.PlaySound(SoundID.Item84, NPC.Center);

                if (NPC.ai[2] == NPC.localAI[1])
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) //hold out hammers for visual display
                    {
                        //const int Bolts = 24;
                        //  SoundEngine.PlaySound(NukeBeep, player.Center);
                        for (int i = 0; i < Bolts; i++)
                        {
                            int x = i;
                            if (x >= Bolts / 2)
                            {
                                x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                            }
                            if (AliveCheck(player))
                            {
                                const int distance = 180;
                                //      int offset = 0;
                                Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                                SpawnLightning(NPC, pos);
                            }
                        }
                        for (int i = -1; i <= 1; i += 2)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                float ai1 = MathHelper.ToRadians(90 + 15) - MathHelper.ToRadians(30) * j;
                                ai1 *= i;
                                ai1 = ai1 / 60 * 2;
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<DeviHammerHeld>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, ai1);
                            }
                        }
                    }
                }
            }

            if (NPC.ai[1] == 60) //finished all the prior teleports, now attack
            {
                NPC.netUpdate = true;

                SoundEngine.PlaySound(SoundID.Item92, NPC.Center);

                if (Main.netMode != NetmodeID.MultiplayerClient) //hammers
                {
                    void SpawnHammers(float rad, int direction, float angleOffset)
                    {
                        const int time = 45;
                        float speed = 2 * (float)Math.PI * rad / time;
                        float acc = speed * speed / rad * NPC.direction;

                        for (int i = 0; i < 4; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(Math.PI / 2 * i + angleOffset) * speed, ModContent.ProjectileType<DeviHammer>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, acc, time);
                    };

                    SpawnHammers(100, -NPC.direction, MathHelper.PiOver4);
                    SpawnHammers(150, NPC.direction, 0);

                    SpawnHammers(200, -NPC.direction, MathHelper.PiOver4);

                    SpawnHammers(300, NPC.direction, 0);
                    SpawnHammers(400, NPC.direction, 0);
                    SpawnHammers(500, NPC.direction, 0);
                }
            }
            else if (NPC.ai[1] > 90)
            {
                NPC.netUpdate = true;
                if (NPC.localAI[3] > 1 && ++NPC.localAI[0] < 3)
                {
                    NPC.ai[2] = 0; //reset tp counter and attack again
                    NPC.localAI[1] = 0;
                }
                else
                {
                    ChooseNextAttack(11, 16, 19, 20, 44, 31, 33, 35, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
            }
        }
        private void SpawnLightning(NPC parent, Vector2 position)
        {
            Vector2 posOrig = position;
            posOrig.Y = Main.player[parent.target].Center.Y + (150 * 7);
            for (int i = 0; i < 14; i++)
            {
                Vector2 pos = posOrig - (Vector2.UnitY * 150 * i);
                Projectile.NewProjectile(parent.GetSource_FromThis(), pos, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "LightningTelegraph").Type, ShtunUtils.ScaledProjectileDamage(parent.damage), 2f, Main.myPlayer, i);
            }

        }
        void BigShtuxibusRay()
        {

            if ((NPC.ai[1] < 420 && !AliveCheck(player)))


                if (NPC.localAI[0] == 0)
                {
                    StrongAttackTeleport();

                    NPC.localAI[0] = 1;
                    NPC.velocity = Vector2.Zero;
                }

            if (NPC.ai[3] < 4 && NPC.Distance(Main.LocalPlayer.Center) < 3000 && Collision.CanHitLine(NPC.Center, 0, 0, Main.LocalPlayer.Center, 0, 0)
                && Math.Sign(Main.LocalPlayer.direction) == Math.Sign(NPC.Center.X - Main.LocalPlayer.Center.X)
                && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
            {
                Vector2 target = Main.LocalPlayer.Center - Vector2.UnitY * 12;
                Vector2 source = NPC.Center - Vector2.UnitY * 6;
                Vector2 distance = target - source;

                int length = (int)distance.Length() / 10;
                Vector2 offset = Vector2.Normalize(distance) * 10f;
                for (int i = 0; i <= length; i++) //dust indicator
                {
                    int d = Dust.NewDust(source + offset * i, 0, 0, DustID.GoldFlame, 0f, 0f, 0, new Color());
                    Main.dust[d].noLight = true;
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 1f;
                }
            }

            if (NPC.ai[3] < 7)
            {

                NPC.ai[1] += 0.6f;
                NPC.ai[2] += 0.6f;

            }

            if (++NPC.ai[2] > 60)
            {
                NPC.ai[2] = 0;
                //only make rings in p2 and before firing ray
                if (NPC.localAI[3] > 1 && NPC.ai[3] < 7 && !Main.player[NPC.target].stoned)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        const int max = 18;
                        int damage = NPC.localAI[3] > 1 ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3) : ShtunUtils.ScaledProjectileDamage(NPC.damage);
                        for (int i = 0; i < max; i++)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 6f * NPC.DirectionTo(player.Center).RotatedBy(2 * Math.PI / max * i),
                                 ModContent.ProjectileType<DeviHeart>(), damage, 0f, Main.myPlayer);
                        }
                    }
                }

                if (++NPC.ai[3] < 4) //medusa warning
                {
                    NPC.netUpdate = true;
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center); //eoc roar

                    //  ShtunUtils.DustRing(NPC.Center, 120, 228, 20f, default, 2f);

                    if (NPC.ai[3] == 1 && Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, -1), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviMedusa").Type, 0, 0, Main.myPlayer);
                }
                else if (NPC.ai[3] == 4) //petrify
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath17, NPC.Center);

                    if (NPC.Distance(Main.LocalPlayer.Center) < 3000 && Collision.CanHitLine(NPC.Center, 0, 0, Main.LocalPlayer.Center, 0, 0)
                        && Math.Sign(Main.LocalPlayer.direction) == Math.Sign(NPC.Center.X - Main.LocalPlayer.Center.X)
                        && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    {
                        for (int i = 0; i < 40; i++) //petrify dust
                        {
                            int d = Dust.NewDust(Main.LocalPlayer.Center, 0, 0, DustID.Stone, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].velocity *= 3f;
                        }

                        Main.LocalPlayer.AddBuff(BuffID.Stoned, 300);
                        if (Main.LocalPlayer.HasBuff(BuffID.Stoned))
                            Main.LocalPlayer.AddBuff(BuffID.Featherfall, 300);

                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.LocalPlayer.Center, new Vector2(0, -1), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviMedusa").Type, 0, 0, Main.myPlayer);
                    }
                }
                else if (NPC.ai[3] < 7) //ray warning
                {
                    NPC.netUpdate = true;

                    //ShtunUtils.DustRing(NPC.Center, 160, 86, 40f, default, 2.5f);

                    NPC.localAI[1] = NPC.DirectionTo(player.Center).ToRotation(); //store for aiming ray

                    if (NPC.ai[3] == 6 && Main.netMode != NetmodeID.MultiplayerClient) //final warning
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.localAI[1]), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviDeathraySmall").Type,
                            0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                }
                else if (NPC.ai[3] == 7) //fire deathray
                {
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                    NPC.velocity = -3f * Vector2.UnitX.RotatedBy(NPC.localAI[1]);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.localAI[1]), ModContent.ProjectileType<DeviBigDeathray>(),
                           1000000000, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }

                    const int ring = 160;
                    for (int i = 0; i < ring; ++i)
                    {
                        Vector2 vector2 = (-Vector2.UnitY.RotatedBy(i * 3.14159274101257 * 2 / ring) * new Vector2(8f, 16f)).RotatedBy(NPC.velocity.ToRotation());
                        int index2 = Dust.NewDust(NPC.Center, 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 1f);
                        Main.dust[index2].scale = 5f;
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].position = NPC.Center;
                        Main.dust[index2].velocity = vector2 * 3f;
                    }
                }
            }

            if (NPC.ai[3] < 7) //charge up dust
            {
                float num1 = 0.99f;
                if (NPC.ai[3] >= 1f)
                    num1 = 0.79f;
                if (NPC.ai[3] >= 2f)
                    num1 = 0.58f;
                if (NPC.ai[3] >= 3f)
                    num1 = 0.43f;
                if (NPC.ai[3] >= 4f)
                    num1 = 0.33f;
                for (int i = 0; i < 9; ++i)
                {
                    if (Main.rand.NextFloat() >= num1)
                    {
                        float f = Main.rand.NextFloat() * 6.283185f;
                        float num2 = Main.rand.NextFloat();
                        Dust dust = Dust.NewDustPerfect(NPC.Center + f.ToRotationVector2() * (110 + 600 * num2), 86, (f - 3.141593f).ToRotationVector2() * (14 + 8 * num2), 0, default, 1f);
                        dust.scale = 0.9f;
                        dust.fadeIn = 1.15f + num2 * 0.3f;
                        //dust.color = new Color(1f, 1f, 1f, num1) * (1f - num1);
                        dust.noGravity = true;
                        //dust.noLight = true;
                    }
                }
            }

            if (NPC.localAI[1] != 0)
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.localAI[1].ToRotationVector2().X);

            if (++NPC.ai[1] > 600)//(NPC.localAI[3] > 1 ? 540 : 600))
            {

                ChooseNextAttack(11, 16, 19, 20, 44, 31, 33, 35, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void SparklingSword()
        {

            if (NPC.localAI[0] == 0)
            {
                StrongAttackTeleport(player.Center + new Vector2(300 * Math.Sign(NPC.Center.X - player.Center.X), -100));

                NPC.localAI[0] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient) //spawn ritual for strong attacks
                {
                    //   Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, NPC.whoAmI);
                }
            }

            if (++NPC.ai[1] < 150)
            {
                NPC.velocity = Vector2.Zero;

                if (NPC.ai[2] == 0) //spawn weapon, teleport
                {
                    double angle = NPC.position.X < player.position.X ? -Math.PI / 4 : Math.PI / 4;
                    NPC.ai[2] = (float)angle * -4f / 30;

                    //spawn axe
                    const int loveOffset = 90;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + -Vector2.UnitY.RotatedBy(angle) * loveOffset, Vector2.Zero, ModContent.ProjectileType<DeviSparklingLove>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, loveOffset);
                    }

                    //spawn hitboxes
                    const int spacing = 80;
                    Vector2 offset = -Vector2.UnitY.RotatedBy(angle) * spacing;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        void SpawnAxeHitbox(Vector2 spawnPos)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<DeviAxe>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, NPC.Distance(spawnPos));
                        }

                        for (int i = 0; i < 8; i++)
                            SpawnAxeHitbox(NPC.Center + offset * i);
                        for (int i = 1; i < 3; i++)
                        {
                            SpawnAxeHitbox(NPC.Center + offset * 5 + offset.RotatedBy(-angle * 2) * i);
                            SpawnAxeHitbox(NPC.Center + offset * 6 + offset.RotatedBy(-angle * 2) * i);
                        }
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 target = new Vector2(80f, 80f).RotatedBy(MathHelper.Pi / 2 * i);

                            Vector2 speed = 2 * target / 90;
                            float acceleration = -speed.Length() / 90;

                            int damage = NPC.localAI[3] > 1 ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3) : ShtunUtils.ScaledProjectileDamage(NPC.damage);

                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.ProjectileType<DeviEnergyHeart>(),
                                damage, 0f, Main.myPlayer, 0, acceleration);
                        }
                    }
                }

                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else if (NPC.ai[1] == 150) //start swinging
            {
                targetPos = player.Center;
                targetPos.X -= 360 * Math.Sign(NPC.ai[2]);
                //targetPos.Y -= 200;
                NPC.velocity = (targetPos - NPC.Center) / 30;
                NPC.netUpdate = true;

                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);

                if (Math.Sign(targetPos.X - NPC.Center.X) != Math.Sign(NPC.ai[2]))
                    NPC.velocity.X *= 0.5f; //worse movement if you're behind her
            }
            else if (NPC.ai[1] < 180)
            {
                NPC.ai[3] += NPC.ai[2];
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else
            {
                targetPos = player.Center + player.DirectionTo(NPC.Center) * 400;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.2f);

                if (NPC.ai[1] > 300)
                {

                    ChooseNextAttack(11, 16, 19, 20, 44, 31, 33, 35, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
            }
        }
        void MutantSparklingSword()
        {

            if (NPC.localAI[0] == 0)
            {
                StrongAttackTeleport(player.Center + new Vector2(300 * Math.Sign(NPC.Center.X - player.Center.X), -100));

                NPC.localAI[0] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient) //spawn ritual for strong attacks
                {
                    //   Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, NPC.whoAmI);
                }
            }

            //     Vector2 offset2 = -Vector2.UnitY.RotatedBy(angle) * spacing;
            //   const float lengthu = MUTANT_SWORD_SPACING * MUTANT_SWORD_MAX / 2f;
            Vector2 spawnPos2 = NPC.Center * (80 * 12) / 2 * 90;
            Vector2 baseDirection = player.DirectionFrom(spawnPos2);
            if (++NPC.ai[1] < 150)
            {
                NPC.velocity = Vector2.Zero;

                if (NPC.ai[2] == 0) //spawn weapon, teleport
                {
                    double angle = NPC.position.X < player.position.X ? -Math.PI / 4 : Math.PI / 4;
                    NPC.ai[2] = (float)angle * -4f / 30;

                    //spawn axe
                    const int loveOffset = 90;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + -Vector2.UnitY.RotatedBy(angle) * loveOffset, Vector2.Zero, ModContent.ProjectileType<MutantSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, loveOffset);
                    }

                    //spawn hitboxes
                    const int spacing = 80;
                    Vector2 offset = -Vector2.UnitY.RotatedBy(angle) * spacing;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        void SpawnAxeHitbox(Vector2 spawnPos)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MoonLordMoonBlast").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, NPC.Distance(spawnPos));
                        }

                        for (int i = 0; i < 12; i++)
                            SpawnAxeHitbox(NPC.Center + offset * i);
                        for (int i = 1; i < 3; i++)
                        {
                            SpawnAxeHitbox(NPC.Center + offset * 5 + offset.RotatedBy(-angle * 2) * i);
                            SpawnAxeHitbox(NPC.Center + offset * 6 + offset.RotatedBy(-angle * 2) * i);
                        }
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Vector2 target = new Vector2(80f, 80f).RotatedBy(MathHelper.Pi / 2 * i);

                            Vector2 speed = 2 * target / 90;
                            float acceleration = -speed.Length() / 90;

                            int damage = NPC.localAI[3] > 1 ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3) : ShtunUtils.ScaledProjectileDamage(NPC.damage);

                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "MoonLordMoonBlast").Type,
                                damage, 0f, Main.myPlayer, 0, acceleration);
                        }

                    }
                }
                for (int z = 0; z < 8; z++)
                {

                    //  Vector2 angle = baseDirection.RotatedBy(MathHelper.TwoPi / max * i);
                    float ai1 = z <= 2 || z == 8 - 2 ? 48 : 24;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Circular(NPC.width / 2, NPC.height / 2), Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MoonLordMoonBlast").Type,
                            ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3f), 0f, Main.myPlayer, MathHelper.WrapAngle(NPC.Center.ToRotation()), ai1);
                    }
                }
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else if (NPC.ai[1] == 150) //start swinging
            {
                targetPos = player.Center;
                targetPos.X -= 360 * Math.Sign(NPC.ai[2]);
                //targetPos.Y -= 200;
                NPC.velocity = (targetPos - NPC.Center) / 30;
                NPC.netUpdate = true;

                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);

                if (Math.Sign(targetPos.X - NPC.Center.X) != Math.Sign(NPC.ai[2]))
                    NPC.velocity.X *= 0.5f; //worse movement if you're behind her
            }
            else if (NPC.ai[1] < 180)
            {
                NPC.ai[3] += NPC.ai[2];
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else
            {
                targetPos = player.Center + player.DirectionTo(NPC.Center) * 400;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.2f);

                if (NPC.ai[1] > 300)
                {

                    ChooseNextAttack(11, 16, 19, 20, 44, 31, 33, 35, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                }
            }
        }
        void SpearTossDirectP2()
        {
            if (!AliveCheck(player))
                return;

            if (NPC.ai[1] == 0)
            {
                NPC.localAI[0] = MathHelper.WrapAngle((NPC.Center - player.Center).ToRotation()); //remember initial angle offset

                //random max number of attacks

                NPC.localAI[1] = 3;



                NPC.localAI[1] += 3;
                NPC.localAI[2] = Main.rand.NextBool() ? -1 : 1; //pick a random rotation direction
                NPC.netUpdate = true;
            }

            //slowly rotate in full circle around player
            Vector2 targetPos = player.Center + 500f * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 300 * NPC.ai[3] * NPC.localAI[2] + NPC.localAI[0]);
            if (NPC.Distance(targetPos) > 25)
                Movement(targetPos, 0.6f);

            ++NPC.ai[3]; //for keeping track of how much time has actually passed (ai1 jumps around)
                         //ShtuxibusJavelinsP2();
            void Attack()
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 vel = NPC.DirectionTo(player.Center) * 30f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.Normalize(vel), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantDeathray2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.8f), 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<MutantSpearThrown>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target);
                }
            };

            if (++NPC.ai[1] > 180)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 150;

                bool shouldAttack = true;
                if (++NPC.ai[2] > NPC.localAI[1])
                {
                    ChooseNextAttack(11, 16, 19, 20, 44, 31, 33, 35, 42, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    shouldAttack = false;
                }

                if (shouldAttack)
                {
                    Attack();
                }
            }
            else if (NPC.ai[1] == 165)
            {
                Attack();
            }
            else if (NPC.ai[1] == 151)
            {
                if (NPC.ai[2] > 0 && (NPC.ai[2] < NPC.localAI[1]) && Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, 1);
            }
            else if (NPC.ai[1] == 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, -1);
            }
        }
        void PrepareTwinRangsAndCrystals()
        {

            if (!AliveCheck(player))
                return;
            Vector2 targetPos = player.Center;
            targetPos.X += 1000 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
                Movement(targetPos, 0.8f);
            if (++NPC.ai[1] > 45)
            {
                NPC.netUpdate = true;
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                //NPC.TargetClosest();
            }
        }
        void UpperCutDick()
        {

            targetPos = player.Center + NPC.DirectionFrom(player.Center) * 400f;
            if (NPC.Distance(targetPos) > 50)
                MovementERI(targetPos, 0.3f, 24f);

            if (NPC.ai[2] == 0)
            {
                NPC.ai[2] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (NPC.localAI[3] == 1 && i % 2 == 0) //dont do half of them in p1
                            continue;
                        for (int j = 0; j < (NPC.localAI[3] == 3 ? 3 : 3); j++) //do twice as many in p3
                        {
                            Vector2 spawnPos = player.Center + Main.rand.NextFloat(500, 700) * Vector2.UnitX.RotatedBy(Main.rand.NextDouble() * 2 * Math.PI);
                            Vector2 vel = NPC.velocity.RotatedBy(Main.rand.NextDouble() * Math.PI * 2);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "ShadowClone").Type,
                                ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, 60 + 30 * i);
                        }
                    }
                }
            }

            if (++NPC.ai[1] > 360)
            {
                ChooseNextAttack(11, 13, 16, 21, 24, 26, 29, 31, 33, 35, 39, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void ShadowShtuxibus()
        {

            targetPos = player.Center + NPC.DirectionFrom(player.Center) * 400f;
            if (NPC.Distance(targetPos) > 50)
                MovementERI(targetPos, 0.3f, 24f);

            if (NPC.ai[2] == 0)
            {
                NPC.ai[2] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (NPC.localAI[3] == 1 && i % 2 == 0) //dont do half of them in p1
                            continue;
                        for (int j = 0; j < (NPC.localAI[3] == 3 ? 3 : 3); j++) //do twice as many in p3
                        {
                            Vector2 spawnPos = player.Center + Main.rand.NextFloat(500, 700) * Vector2.UnitX.RotatedBy(Main.rand.NextDouble() * 2 * Math.PI);
                            Vector2 vel = NPC.velocity.RotatedBy(Main.rand.NextDouble() * Math.PI * 2);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "ShadowClone").Type,
                                ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, 60 + 30 * i);
                        }
                    }
                }
            }

            if (++NPC.ai[1] > 360)
            {

                ChooseNextAttack(11, 13, 16, 21, 24, 26, 29, 31, 33, 35, 39, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                // NPC.ai[0]++;

            }
        }
        void BallTorture()
        {
            //
            if (NPC.ai[1] == 1)
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

            if (NPC.ai[2] == 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.whoAmI, -20);
            }

            if (++NPC.ai[2] <= 200)
            {
                targetPos = player.Center;
                targetPos.X += 600 * (NPC.Center.X < targetPos.X ? -1 : 1);
                NPC.position += player.velocity / 3f; //really good tracking movement here
                MovementERI(targetPos, 1.2f, 32f);

                if (--NPC.localAI[0] < 0)
                {
                    NPC.localAI[0] = 90;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        const int max = 11;
                        const int travelTime = 20;
                        for (int j = -1; j <= 1; j += 2)
                        {
                            for (int i = -max; i <= max; i++)
                            {
                                Vector2 target = player.Center;
                                target.X += 180f * i;
                                target.Y += (400f + 300f / max * Math.Abs(i)) * j;
                                //y pos is above and below player, adapt to always outspeed player, with additional V shapes
                                Vector2 speed = (target - NPC.Center) / travelTime;
                                int individualTiming = 60 + Math.Abs(i * 2);
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed / 2, ModContent.ProjectileType<CosmosSphere>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, travelTime, individualTiming);
                            }
                        }
                    }
                }

                NPC.rotation = NPC.DirectionTo(player.Center).ToRotation();
                if (NPC.direction < 0)
                    NPC.rotation += (float)Math.PI;

                NPC.ai[3] = NPC.Center.X < player.Center.X ? 1 : -1; //store direction im facing

                if (NPC.ai[2] == 200) //straight ray punch
                {
                    NPC.velocity = 42f * NPC.DirectionTo(player.Center);
                    NPC.netUpdate = true;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int modifier = Math.Sign(NPC.Center.Y - player.Center.Y);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + 3000 * NPC.DirectionFrom(player.Center) * modifier, NPC.DirectionTo(player.Center) * modifier,
                            ModContent.ProjectileType<CosmosDeathray2>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                    }
                }
            }
            else
            {
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[3]); //dont turn around if crossed up
            }

            if (++NPC.ai[1] > 400 || (NPC.ai[2] > 200 &&
                (NPC.ai[3] > 0 ? NPC.Center.X > player.Center.X + 800 : NPC.Center.X < player.Center.X - 800)))
            {
                NPC.velocity.X = 0f;

                NPC.TargetClosest();
                NPC.ai[0]++;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.netUpdate = true;
            }
        }
        void TwinRangsAndCrystals()
        {
            //
            NPC.velocity = Vector2.Zero;

            if (NPC.ai[3] == 0)
            {
                NPC.localAI[0] = NPC.DirectionFrom(player.Center).ToRotation();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Vector2.UnitX.RotatedBy(Math.PI / 2 * i) * 525, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRingHollow").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 1f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Vector2.UnitX.RotatedBy(Math.PI / 2 * i + Math.PI / 4) * 350, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRingHollow").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 2f);
                    }
                }
            }

            int ringDelay = 12;
            int ringMax = 5;
            if (NPC.ai[3] % ringDelay == 0 && NPC.ai[3] < ringDelay * ringMax)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float rotationOffset = MathHelper.TwoPi / ringMax * NPC.ai[3] / ringDelay + NPC.localAI[0];
                    int baseDelay = 60;
                    float flyDelay = 120 + NPC.ai[3] / ringDelay * (40);
                    int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 300f / baseDelay * Vector2.UnitX.RotatedBy(rotationOffset), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantMark2").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, baseDelay, baseDelay + flyDelay);
                    if (p != Main.maxProjectiles)
                    {
                        const int max = 8;
                        const float distance = 125f;
                        float rotation = MathHelper.TwoPi / max;
                        for (int i = 0; i < max; i++)
                        {
                            float myRot = rotation * i + rotationOffset;
                            Vector2 spawnPos = NPC.Center + new Vector2(distance, 0f).RotatedBy(myRot);
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantCrystalLeaf").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, Main.projectile[p].identity, myRot);
                        }
                    }
                }
            }

            if (NPC.ai[3] > 45 && --NPC.ai[1] < 0)
            {
                NPC.netUpdate = true;
                NPC.ai[1] = 20;
                NPC.ai[2] = NPC.ai[2] > 0 ? -1 : 1;

                SoundEngine.PlaySound(SoundID.Item92, NPC.Center);

                if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[3] < 330)
                {
                    const float retiRad = 1025;
                    const float spazRad = 750;
                    float retiSpeed = 2 * (float)Math.PI * retiRad / 300;
                    float spazSpeed = 2 * (float)Math.PI * spazRad / 180;
                    float retiAcc = retiSpeed * retiSpeed / retiRad * NPC.ai[2];
                    float spazAcc = spazSpeed * spazSpeed / spazRad * -NPC.ai[2];
                    float rotationOffset = MathHelper.PiOver4;
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(Math.PI / 2 * i + rotationOffset) * retiSpeed, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantRetirang").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, retiAcc, 300);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(Math.PI / 2 * i + Math.PI / 4 + rotationOffset) * spazSpeed, ModContent.Find<ModProjectile>(fargosouls.Name, "MutantSpazmarang").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, spazAcc, 180);
                    }
                }
            }
            if (++NPC.ai[3] > 450)
            {

                ChooseNextAttack(11, 13, 16, 21, 24, 26, 29, 31, 33, 35, 39, 41, 44, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void EmpressSwordWaveButShtuxibusP3()
        {
            int attackThreshold = 48;
            int timesToAttack = 3 + (int)(endTimeVariance * 5);
            int startup = 90;

            if (NpcaiFC[1] == 0)
            {

                NpcaiFC[3] = Main.rand.NextFloat(MathHelper.TwoPi);
            }

            void Sword(Vector2 pos, float ai0, float ai1, Vector2 vel)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {

                    Projectile.NewProjectile(NPC.GetSource_FromThis(), pos - vel * 60f, vel,
                        ProjectileID.FairyQueenLance, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, ai1);
                }
            }

            if (NpcaiFC[1] >= startup && NpcaiFC[1] < startup + attackThreshold * timesToAttack && --NpcaiFC[2] < 0) //walls of swords
            {
                NpcaiFC[2] = attackThreshold;

                SoundEngine.PlaySound(SoundID.Item163, player.Center);

                if (Math.Abs(MathHelper.WrapAngle(NPC.DirectionFrom(player.Center).ToRotation() - NpcaiFC[3])) > MathHelper.PiOver2)
                    NpcaiFC[3] += MathHelper.Pi; //swords always spawn closer to player

                const int maxHorizSpread = 1600 * 2;
                const int arenaRadius = 2000;
                int max = 16;
                float gap = maxHorizSpread / max;

                float attackAngle = NpcaiFC[3];// + Main.rand.NextFloat(MathHelper.ToDegrees(10)) * (Main.rand.NextBool() ? -1 : 1);
                Vector2 spawnOffset = -attackAngle.ToRotationVector2();

                //start by focusing on player
                Vector2 focusPoint = player.Center;

                //move focus point along grid closer so attack stays centered
                Vector2 home = NPC.Center;// ShtunUtils.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? NPC.Center : Main.projectile[ritualProj].Center;
                for (float i = 0; i < arenaRadius; i += gap)
                {
                    Vector2 newFocusPoint = focusPoint + gap * attackAngle.ToRotationVector2();
                    if ((home - newFocusPoint).Length() > (home - focusPoint).Length())
                        break;
                    focusPoint = newFocusPoint;
                }

                //doing it this way to guarantee it always remains aligned to grid
                float spawnDistance = 0;
                while (spawnDistance < arenaRadius)
                    spawnDistance += gap;

                float mirrorLength = 2f * (float)Math.Sqrt(2f * spawnDistance * spawnDistance);
                int swordCounter = 0;
                for (int i = -max; i <= max; i++)
                {

                    Vector2 spawnPos = focusPoint + spawnOffset * spawnDistance + spawnOffset.RotatedBy(MathHelper.PiOver2) * gap * i;
                    float Ai1 = swordCounter++ / (max * 2f + 1);

                    Vector2 randomOffset = Main.rand.NextVector2Unit();

                    if (randomOffset.Length() < 0.5f)
                        randomOffset = 0.5f * randomOffset.SafeNormalize(Vector2.UnitX);
                    randomOffset *= 2f;


                    Sword(spawnPos, attackAngle + MathHelper.PiOver4, Ai1, randomOffset);
                    Sword(spawnPos, attackAngle - MathHelper.PiOver4, Ai1, randomOffset);
                    Sword(spawnPos + mirrorLength * (attackAngle - MathHelper.PiOver4).ToRotationVector2(), attackAngle - MathHelper.PiOver4 + MathHelper.Pi, Ai1, randomOffset);

                }

                NpcaiFC[3] += MathHelper.PiOver4 * (Main.rand.NextBool() ? -1 : 1) //rotate 90 degrees
                    + Main.rand.NextFloat(MathHelper.PiOver4 / 2) * (Main.rand.NextBool() ? -1 : 1); //variation

                NPC.netUpdate = true;
            }



            //massive sword barrage
            int swordSwarmTime = startup + attackThreshold * timesToAttack + 40;


            if (++NpcaiFC[1] > swordSwarmTime + (60))
            {
                NpcaiFC[3] = 0;
                NpcaiFC[2] = 0;
                NpcaiFC[1] = 0;
                NpclocalaiFC[0] = 0;
                NpclocalaiFC[1] = 0;
            }
        }
        void EmpressSwordWave()
        {
            //
            if (!AliveCheck(player))
                return;
            //Vector2 targetPos = player.Center + 360 * NPC.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(10)); Movement(targetPos, 0.25f);
            NPC.velocity = Vector2.Zero;

            int attackThreshold = 48;
            int timesToAttack = 3 + (int)(endTimeVariance * 5);
            int startup = 90;

            if (NPC.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                NPC.ai[3] = Main.rand.NextFloat(MathHelper.TwoPi);
            }

            void Sword(Vector2 pos, float ai0, float ai1, Vector2 vel)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), pos - vel * 60f, vel,
                        ProjectileID.FairyQueenLance, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, ai0, ai1);
                }
            }

            if (NPC.ai[1] >= startup && NPC.ai[1] < startup + attackThreshold * timesToAttack && --NPC.ai[2] < 0) //walls of swords
            {
                NPC.ai[2] = attackThreshold;

                SoundEngine.PlaySound(SoundID.Item163, player.Center);

                if (Math.Abs(MathHelper.WrapAngle(NPC.DirectionFrom(player.Center).ToRotation() - NPC.ai[3])) > MathHelper.PiOver2)
                    NPC.ai[3] += MathHelper.Pi; //swords always spawn closer to player

                const int maxHorizSpread = 1600 * 2;
                const int arenaRadius = 2000;
                int max = 16;
                float gap = maxHorizSpread / max;

                float attackAngle = NPC.ai[3];// + Main.rand.NextFloat(MathHelper.ToDegrees(10)) * (Main.rand.NextBool() ? -1 : 1);
                Vector2 spawnOffset = -attackAngle.ToRotationVector2();

                //start by focusing on player
                Vector2 focusPoint = player.Center;

                //move focus point along grid closer so attack stays centered
                Vector2 home = NPC.Center;// ShtunUtils.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null ? NPC.Center : Main.projectile[ritualProj].Center;
                for (float i = 0; i < arenaRadius; i += gap)
                {
                    Vector2 newFocusPoint = focusPoint + gap * attackAngle.ToRotationVector2();
                    if ((home - newFocusPoint).Length() > (home - focusPoint).Length())
                        break;
                    focusPoint = newFocusPoint;
                }

                //doing it this way to guarantee it always remains aligned to grid
                float spawnDistance = 0;
                while (spawnDistance < arenaRadius)
                    spawnDistance += gap;

                float mirrorLength = 2f * (float)Math.Sqrt(2f * spawnDistance * spawnDistance);
                int swordCounter = 0;
                for (int i = -max; i <= max; i++)
                {
                    Vector2 spawnPos = focusPoint + spawnOffset * spawnDistance + spawnOffset.RotatedBy(MathHelper.PiOver2) * gap * i;
                    float Ai1 = swordCounter++ / (max * 2f + 1);

                    Vector2 randomOffset = Main.rand.NextVector2Unit();

                    if (randomOffset.Length() < 0.5f)
                        randomOffset = 0.5f * randomOffset.SafeNormalize(Vector2.UnitX);
                    randomOffset *= 2f;


                    Sword(spawnPos, attackAngle + MathHelper.PiOver4, Ai1, randomOffset);
                    Sword(spawnPos, attackAngle - MathHelper.PiOver4, Ai1, randomOffset);
                    Sword(spawnPos + mirrorLength * (attackAngle + MathHelper.PiOver4).ToRotationVector2(), attackAngle + MathHelper.PiOver4 + MathHelper.Pi, Ai1, randomOffset);
                    Sword(spawnPos + mirrorLength * (attackAngle - MathHelper.PiOver4).ToRotationVector2(), attackAngle - MathHelper.PiOver4 + MathHelper.Pi, Ai1, randomOffset);

                }

                NPC.ai[3] += MathHelper.PiOver4 * (Main.rand.NextBool() ? -1 : 1) //rotate 90 degrees
                    + Main.rand.NextFloat(MathHelper.PiOver4 / 2) * (Main.rand.NextBool() ? -1 : 1); //variation

                NPC.netUpdate = true;
            }

            void MegaSwordSwarm(Vector2 target)
            {
                SoundEngine.PlaySound(SoundID.Item164, player.Center);

                float safeAngle = NPC.ai[3];
                float safeRange = MathHelper.ToRadians(10);
                int max = 60;
                for (int i = 0; i < max; i++)
                {
                    float rotationOffset = Main.rand.NextFloat(safeRange, MathHelper.Pi - safeRange);
                    Vector2 offset = Main.rand.NextFloat(600f, 2400f) * (safeAngle + rotationOffset).ToRotationVector2();
                    if (Main.rand.NextBool())
                        offset *= -1;

                    //if (FargoSoulsWorld.MasochistModeReal) //block one side so only one real exit exists
                    //    target += Main.rand.NextFloat(600) * safeAngle.ToRotationVector2();

                    Vector2 spawnPos = target + offset;
                    Vector2 vel = (target - spawnPos) / 60f;
                    Sword(spawnPos, vel.ToRotation(), (float)i / max, -vel * 0.75f);
                }
            }

            //massive sword barrage
            int swordSwarmTime = startup + attackThreshold * timesToAttack + 40;
            if (NPC.ai[1] == swordSwarmTime)
            {
                MegaSwordSwarm(player.Center);
                NPC.localAI[0] = player.Center.X;
                NPC.localAI[1] = player.Center.Y;
            }

            if (NPC.ai[1] == swordSwarmTime + 30)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    //    ShtuxibusJavelinsP2();
                    MegaSwordSwarm(new Vector2(NPC.localAI[0], NPC.localAI[1]) + 600 * i * NPC.ai[3].ToRotationVector2());
                }
            }

            if (++NPC.ai[1] > swordSwarmTime + (60))
            {
                ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 35, 37, 39, 41, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
            }
        }
        void AbomSwords()
        {
            NPC nPC22 = NPC;
            nPC22.velocity = nPC22.velocity * 0.9f;
            if (NPC.ai[1] < 60f)
            {
                FancyFireballs((int)NPC.ai[1]);
            }
            if (NPC.ai[1] == 0f && NPC.ai[2] != 2f && Main.netMode != 1)
            {
                float num78 = ((NPC.ai[2] != 1f) ? 1 : (-1));
                num78 *= MathHelper.ToRadians(270f) / 120f * -1f * 60f;
                int num79 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3f, num78);
                if (num79 != 1000)
                {
                    Main.projectile[num79].localAI[1] = NPC.whoAmI;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(27, -1, -1, (NetworkText)null, num79, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                int num80 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3f, num78 + 3.1416f);
                if (num80 != 1000)
                {
                    Main.projectile[num80].localAI[1] = NPC.whoAmI;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(27, -1, -1, (NetworkText)null, num80, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                Vector2 val7;
                NPC.netUpdate = true;
                NPC.velocity = Vector2.Zero;
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                float num81 = ((NPC.ai[2] != 1f) ? 1 : (-1));
                num81 *= MathHelper.ToRadians(270f) / 120f;
                Vector2 val68 = NPC.DirectionTo(player.Center);
                double num82 = (0f - num81) * 60f;
                val7 = default(Vector2);
                Vector2 val69 = Utils.RotatedBy(val68, num82, val7);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, val69, ModContent.Find<ModProjectile>(fargosouls.Name, "AbomSword").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, num81, (float)NPC.whoAmI);

                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -val69, ModContent.Find<ModProjectile>(fargosouls.Name, "AbomSword").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage, 1.5f), 0f, Main.myPlayer, num81, (float)NPC.whoAmI);
                if (++NPC.ai[1] > 90)
                {
                    //	ClearNewAI();
                    ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 35, 37, 39, 41, 45, 46, 47, 52, 55, 56, 58, 59, 60, 61, 62, 63, 64, 65);
                    NPC.ai[1] = 0f;

                }
            }
        }
        void P2NextAttackPause() //choose next attack but actually, this also gives breathing space for mp to sync up
        {
            if (!AliveCheck(player))
                return;

            EModeSpecialEffects(); //manage these here, for case where players log out/rejoin in mp

            Vector2 targetPos = player.Center + NPC.DirectionFrom(player.Center) * 400;
            Movement(targetPos, 0.3f);
            if (NPC.Distance(targetPos) > 200) //faster if offscreen
                Movement(targetPos, 0.3f);

            if (++NPC.ai[1] > 60 || (NPC.Distance(targetPos) < 200 && NPC.ai[1] > (NPC.localAI[3] >= 3 ? 15 : 30)))
            {

                NPC.velocity *= 0.25f;

                //NPC.TargetClosest();
                NPC.ai[0] = NPC.ai[2];
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.netUpdate = true;
            }
        }

        #endregion
        #region phaze3

        bool Phase3Transition()
        {
            DeleteSusItems();
            bool retval = true;
            INPHASE3 = true;
            NPC.localAI[3] = 3;
            EModeSpecialEffects();
            TryMasoP3Theme();

            //NPC.damage = 0;
            if (NPC.buffType[0] != 0)
                NPC.DelBuff(0);

            if (NPC.ai[1] == 0) //entering final phase, give healing
            {
                TryMasoP3Theme();

                NPC.life = NPC.lifeMax;

                DramaticTransition(true);
            }
            if (NPC.ai[1] < 60 && !Main.dedServ && Main.LocalPlayer.active)
                Main.LocalPlayer.GetModPlayer<ShtunPlayer>().Screenshake = 2;

            if (NPC.ai[1] == 360)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

            }

            if (++NPC.ai[1] > 480)
            {
                retval = false; //dont drain life during this time, ensure it stays synced

                if (!AliveCheck(player))
                    return retval;
                Vector2 targetPos = player.Center;
                targetPos.Y -= 300;
                Movement(targetPos, 1f, true, false);
                if (NPC.Distance(targetPos) < 50 || NPC.ai[1] > 720)
                {
                    NPC.netUpdate = true;
                    NPC.velocity = Vector2.Zero;
                    NPC.localAI[0] = 0;
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = NPC.DirectionFrom(player.Center).ToRotation();
                    NPC.ai[3] = (float)Math.PI / 20f;
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                    if (player.Center.X < NPC.Center.X)
                        NPC.ai[3] *= -1;
                }
            }
            else
            {
                NPC.velocity *= 0.9f;

                //make you stop attacking
                if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && NPC.Distance(Main.LocalPlayer.Center) < 3000)
                {
                    Main.LocalPlayer.controlUseItem = false;
                    Main.LocalPlayer.controlUseTile = false;
                    //            Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().NoUsingItems = true;
                }

                if (--NPC.localAI[0] < 0)
                {
                    NPC.localAI[0] = Main.rand.Next(15);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                        //int type =   ModContent.ProjectileType<MutantEye>();
                        //Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer);
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }

            return retval;
        }
        void TryLaserAttackP3()
        {
            int what = Main.rand.Next(3);
            if (what == 1)
            {
                EmpressSwordWaveButShtuxibusP3();
            }
            if (what == 2)
            {
                EmpressSwordWaveButShtuxibusP3();
            }
            if (what == 3)
            {
            }
        }
        void BigShtuxibusRayP3()
        {

            if (NPC.ai[1] < 420 && !AliveCheck(player))


                if (NPC.localAI[0] == 0)
                {
                    StrongAttackTeleport();
                    NPC.localAI[0] = 1;
                    NPC.velocity = Vector2.Zero;
                }

            if (NPC.ai[3] < 4 && NPC.Distance(Main.LocalPlayer.Center) < 3000 && Collision.CanHitLine(NPC.Center, 0, 0, Main.LocalPlayer.Center, 0, 0)
                && Math.Sign(Main.LocalPlayer.direction) == Math.Sign(NPC.Center.X - Main.LocalPlayer.Center.X)
                && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
            {
                Vector2 target = Main.LocalPlayer.Center - Vector2.UnitY * 12;
                Vector2 source = NPC.Center - Vector2.UnitY * 6;
                Vector2 distance = target - source;

                int length = (int)distance.Length() / 10;
                Vector2 offset = Vector2.Normalize(distance) * 10f;
                for (int i = 0; i <= length; i++) //dust indicator
                {
                    int d = Dust.NewDust(source + offset * i, 0, 0, DustID.GoldFlame, 0f, 0f, 0, new Color());
                    Main.dust[d].noLight = true;
                    Main.dust[d].noGravity = true;
                    Main.dust[d].scale = 1f;
                }
            }

            if (NPC.ai[3] < 7)
            {

                NPC.ai[1] += 0.6f;
                NPC.ai[2] += 0.6f;

            }

            if (++NPC.ai[2] > 60)
            {
                NPC.ai[2] = 0;
                //only make rings in p2 and before firing ray
                if (NPC.localAI[3] > 1 && NPC.ai[3] < 7 && !Main.player[NPC.target].stoned)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Bolts; i++)
                        {
                            int x = i;
                            if (x >= Bolts / 2)
                            {
                                x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                            }
                            if (AliveCheck(player))
                            {
                                const int distance = 180;
                                // int offset = 0;
                                Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                                SpawnLightning(NPC, pos);
                            }
                        }
                        const int max = 20;
                        int damage = NPC.localAI[3] > 1 ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3) : ShtunUtils.ScaledProjectileDamage(NPC.damage);
                        for (int i = 0; i < max; i++)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 6f * NPC.DirectionTo(player.Center).RotatedBy(2 * Math.PI / max * i),
                                 ModContent.ProjectileType<DeviHeart>(), damage, 0f, Main.myPlayer);
                        }
                    }
                }

                if (++NPC.ai[3] < 4) //medusa warning
                {
                    NPC.netUpdate = true;
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center); //eoc roar

                    if (NPC.ai[3] == 1 && Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0, -1), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviMedusa").Type, 0, 0, Main.myPlayer);
                }
                else if (NPC.ai[3] == 4) //petrify
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath17, NPC.Center);

                    if (NPC.Distance(Main.LocalPlayer.Center) < 3000 && Collision.CanHitLine(NPC.Center, 0, 0, Main.LocalPlayer.Center, 0, 0)
                        && Math.Sign(Main.LocalPlayer.direction) == Math.Sign(NPC.Center.X - Main.LocalPlayer.Center.X)
                        && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
                    {
                        for (int i = 0; i < 40; i++) //petrify dust
                        {
                            int d = Dust.NewDust(Main.LocalPlayer.Center, 0, 0, DustID.Stone, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].velocity *= 3f;
                        }

                        Main.LocalPlayer.AddBuff(BuffID.Stoned, 300);
                        if (Main.LocalPlayer.HasBuff(BuffID.Stoned))
                            Main.LocalPlayer.AddBuff(BuffID.Featherfall, 300);

                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Main.LocalPlayer.Center, new Vector2(0, -1), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviMedusa").Type, 0, 0, Main.myPlayer);
                    }
                }
                else if (NPC.ai[3] < 7) //ray warning
                {
                    NPC.netUpdate = true;


                    NPC.localAI[1] = NPC.DirectionTo(player.Center).ToRotation(); //store for aiming ray

                    if (NPC.ai[3] == 6 && Main.netMode != NetmodeID.MultiplayerClient) //final warning
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.localAI[1]), ModContent.Find<ModProjectile>(fargosouls.Name, "DeviDeathraySmall").Type,
                            0, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }
                }
                else if (NPC.ai[3] == 7) //fire deathray
                {
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                    NPC.velocity = -3f * Vector2.UnitX.RotatedBy(NPC.localAI[1]);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.localAI[1]), ModContent.ProjectileType<DeviBigDeathray>(),
                            10000, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.localAI[1] * (Main.rand.NextDouble() - 0.5)), ModContent.ProjectileType<DeviBigDeathray>(),
                            10000, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitX.RotatedBy(NPC.localAI[1] * (Main.rand.NextDouble() - 0.5)), ModContent.ProjectileType<DeviBigDeathray>(),
                            10000, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                    }

                    const int ring = 160;
                    for (int i = 0; i < ring; ++i)
                    {
                        Vector2 vector2 = (-Vector2.UnitY.RotatedBy(i * 3.14159274101257 * 2 / ring) * new Vector2(8f, 16f)).RotatedBy(NPC.velocity.ToRotation());
                        int index2 = Dust.NewDust(NPC.Center, 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 1f);
                        Main.dust[index2].scale = 5f;
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].position = NPC.Center;
                        Main.dust[index2].velocity = vector2 * 3f;
                    }
                }
            }

            if (NPC.ai[3] < 7) //charge up dust
            {
                float num1 = 0.99f;
                if (NPC.ai[3] >= 1f)
                    num1 = 0.79f;
                if (NPC.ai[3] >= 2f)
                    num1 = 0.58f;
                if (NPC.ai[3] >= 3f)
                    num1 = 0.43f;
                if (NPC.ai[3] >= 4f)
                    num1 = 0.33f;
                for (int i = 0; i < 9; ++i)
                {
                    if (Main.rand.NextFloat() >= num1)
                    {
                        float f = Main.rand.NextFloat() * 6.283185f;
                        float num2 = Main.rand.NextFloat();
                        Dust dust = Dust.NewDustPerfect(NPC.Center + f.ToRotationVector2() * (110 + 600 * num2), 86, (f - 3.141593f).ToRotationVector2() * (14 + 8 * num2), 0, default, 1f);
                        dust.scale = 0.9f;
                        dust.fadeIn = 1.15f + num2 * 0.3f;
                        //dust.color = new Color(1f, 1f, 1f, num1) * (1f - num1);
                        dust.noGravity = true;
                        //dust.noLight = true;
                    }
                }
            }

            if (NPC.localAI[1] != 0)
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.localAI[1].ToRotationVector2().X);

            if (++NPC.ai[1] > 600)//(NPC.localAI[3] > 1 ? 540 : 600))
            {
                NPC.netUpdate = true;

                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
            }
        }
        void SparklingSwordP3()
        {

            if (NPC.localAI[0] == 0)
            {
                //  ShtunUtils.DisplayLocalizedText("BUT YOU NOT SO STRONG AS ME", textColor2);
                StrongAttackTeleport(player.Center + new Vector2(300 * Math.Sign(NPC.Center.X - player.Center.X), -100));

                NPC.localAI[0] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient) //spawn ritual for strong attacks
                {
                    //   Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 0f, NPC.whoAmI);
                }
            }

            if (++NPC.ai[1] < 150)
            {
                NPC.velocity = Vector2.Zero;

                if (NPC.ai[2] == 0) //spawn weapon, teleport
                {
                    double angle = NPC.position.X < player.position.X ? -Math.PI / 4 : Math.PI / 4;
                    NPC.ai[2] = (float)angle * -4f / 30;

                    //spawn axe
                    const int loveOffset = 90;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < Bolts; i++)
                        {
                            int x = i;
                            if (x >= Bolts / 2)
                            {
                                x = (Bolts / 2) - 1 - x; //split i into 1 to bolts/2 and -1 to -bolts/2
                            }
                            if (AliveCheck(player))
                            {
                                const int distance = 180;
                                // int offset = 0;
                                Vector2 pos = NPC.Center + Vector2.UnitX * (distance * x + 0);
                                SpawnLightning(NPC, pos);
                            }
                        }
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + -Vector2.UnitY.RotatedBy(angle) * loveOffset, Vector2.Zero, ModContent.ProjectileType<DeviSparklingLove>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, loveOffset);
                    }

                    //spawn hitboxes
                    const int spacing = 80;
                    Vector2 offset = -Vector2.UnitY.RotatedBy(angle) * spacing;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        void SpawnAxeHitbox(Vector2 spawnPos)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<DeviAxe>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 2), 0f, Main.myPlayer, NPC.whoAmI, NPC.Distance(spawnPos));
                        }

                        for (int i = 0; i < 8; i++)
                            SpawnAxeHitbox(NPC.Center + offset * i);
                        for (int i = 1; i < 3; i++)
                        {
                            SpawnAxeHitbox(NPC.Center + offset * 5 + offset.RotatedBy(-angle * 2) * i);
                            SpawnAxeHitbox(NPC.Center + offset * 6 + offset.RotatedBy(-angle * 2) * i);
                        }
                    }

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vector2 val7;
                            Vector2 target = new Vector2(80f, 80f).RotatedBy(MathHelper.Pi / 2 * i);
                            Vector2 val15 = new Vector2(80f, 80f);
                            double num19 = (float)Math.PI / 2f * (float)i;
                            val7 = default(Vector2);
                            Vector2 val16 = Utils.RotatedBy(val15, num19, val7);
                            Vector2 val17 = 2f * val16 / 90f;
                            //float num20 = val17.Length / 90f;
                            Vector2 speed = 2 * target / 90;
                            float acceleration = -speed.Length() / 90;

                            int damage = NPC.localAI[3] > 1 ? ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f / 3) : ShtunUtils.ScaledProjectileDamage(NPC.damage);

                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, val17, ModContent.ProjectileType<DeviEnergyHeart>(),
                                damage, 0f, Main.myPlayer, 0, acceleration);
                        }
                    }
                }

                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else if (NPC.ai[1] == 150) //start swinging
            {
                targetPos = player.Center;
                targetPos.X -= 360 * Math.Sign(NPC.ai[2]);
                //targetPos.Y -= 200;
                NPC.velocity = (targetPos - NPC.Center) / 30;
                NPC.netUpdate = true;

                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);

                if (Math.Sign(targetPos.X - NPC.Center.X) != Math.Sign(NPC.ai[2]))
                    NPC.velocity.X *= 0.5f; //worse movement if you're behind her
            }
            else if (NPC.ai[1] < 180)
            {
                NPC.ai[3] += NPC.ai[2];
                NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2]);
            }
            else
            {
                targetPos = player.Center + player.DirectionTo(NPC.Center) * 400;
                if (NPC.Distance(targetPos) > 50)
                    Movement(targetPos, 0.2f);

                if (NPC.ai[1] > 300)
                {

                    NPC.netUpdate = true;
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.localAI[0] = 0;
                }
            }
        }
        void VoidRaysP3()
        {
            TryMasoP3Theme();
            EModeSpecialEffects();

            if (--NPC.ai[1] < 0)
            {

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float speed = NPC.localAI[0] <= 40 ? 4f : 2f;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, speed * Vector2.UnitX.RotatedBy(NPC.ai[2]), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantMark1").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                }
                NPC.ai[1] = 1;
                NPC.ai[2] += NPC.ai[3];

                if (NPC.localAI[0] < 30)
                {
                    EModeSpecialEffects();
                    TryMasoP3Theme();
                }

                if (NPC.localAI[0]++ == 40 || NPC.localAI[0] == 80 || NPC.localAI[0] == 120)
                {
                    NPC.netUpdate = true;
                    NPC.ai[2] -= NPC.ai[3] / (3);
                }
                else if (NPC.localAI[0] >= (160))
                {

                    NPC.netUpdate = true;
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.localAI[0] = 0;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }

            NPC.velocity = Vector2.Zero;

        }
        void OkuuSpheresP3()
        {

            if (NPC.ai[2] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                NPC.ai[3] = Main.rand.NextFloat((float)Math.PI * 2);
            }

            int endTime = 360 + 120;

            endTime += 360;

            if (++NPC.ai[1] > 10 && NPC.ai[3] > 60 && NPC.ai[3] < endTime - 120)
            {
                NPC.ai[1] = 0;
                float rotation = MathHelper.ToRadians(45) * (NPC.ai[3] - 60) / 240 * NPC.ai[2];
                int max = 11;
                float speed = 11f;
                SpawnSphereRing(max, speed, ShtunUtils.ScaledProjectileDamage(NPC.damage), -0.75f, rotation);
                SpawnSphereRing(max, speed, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0.75f, rotation);
            }

            if (NPC.ai[3] < 30)
            {
                EModeSpecialEffects();
                TryMasoP3Theme();
            }

            if (++NPC.ai[3] > endTime)
            {
                NPC.netUpdate = true;

                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                //NPC.TargetClosest();
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }

            NPC.velocity = Vector2.Zero;
        }
        void ShtuxibusFireballsP3TRUE()
        {
            if (!AliveCheck(player))
                return;
            Vector2 vel = player.Center - NPC.Center;
            NPC.rotation = vel.ToRotation();

            const float moveSpeed = 0.25f;

            if (vel.X > 0) //im on left side of target
            {
                vel.X -= 450;
                NPC.direction = NPC.spriteDirection = 1;
            }
            else //im on right side of target
            {
                vel.X += 450;
                NPC.direction = NPC.spriteDirection = -1;
            }
            vel.Y -= 200f;
            vel.Normalize();
            vel *= 16f;
            if (NPC.velocity.X < vel.X)
            {
                NPC.velocity.X += moveSpeed;
                if (NPC.velocity.X < 0 && vel.X > 0)
                    NPC.velocity.X += moveSpeed;
            }
            else if (NPC.velocity.X > vel.X)
            {
                NPC.velocity.X -= moveSpeed;
                if (NPC.velocity.X > 0 && vel.X < 0)
                    NPC.velocity.X -= moveSpeed;
            }
            if (NPC.velocity.Y < vel.Y)
            {
                NPC.velocity.Y += moveSpeed;
                if (NPC.velocity.Y < 0 && vel.Y > 0)
                    NPC.velocity.Y += moveSpeed;
            }
            else if (NPC.velocity.Y > vel.Y)
            {
                NPC.velocity.Y -= moveSpeed;
                if (NPC.velocity.Y > 0 && vel.Y < 0)
                    NPC.velocity.Y -= moveSpeed;
            }

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = NPC.localAI[2] == 1 ? 30 : 40;

                if (NPC.ai[1] < 240 || NPC.localAI[3] == 1)
                {
                    SoundEngine.PlaySound(SoundID.ForceRoarPitched, NPC.Center);

                    if (Main.netMode != NetmodeID.MultiplayerClient && !SparkAtaackUsing)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            const int time = 120;
                            float speed = Main.rand.NextFloat(240, 720) / time * 2f;
                            Vector2 velocity = speed * NPC.DirectionFrom(player.Center).RotatedByRandom(MathHelper.PiOver2);
                            float ai1 = speed / time;
                        }
                    }
                }
            }

            if (++imtrydomove > 1)
            {

                imtrydomove = 0;
                SparkAtaackUsing = false;
            }
        }
        void AbomSwordMassacareP3()
        {
            TryMasoP3Theme();
            if (!AliveCheck(player))
                return;


            if (NPC.ai[1] < 60)
                FancyFireballs((int)NPC.ai[1]);

            if (NPC.ai[1] == 0 && NPC.ai[2] != 2 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                float ai1 = NPC.ai[2] == 1 ? -1 : 1;
                ai1 *= MathHelper.ToRadians(270) / 120 * -1 * 60; //spawning offset of sword below
                int p = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, 3, ai1);
                if (p != Main.maxProjectiles)
                {
                    Main.projectile[p].localAI[1] = NPC.whoAmI;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncProjectile, number: p);
                }
            }

            else if (NPC.ai[1] == 60 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.netUpdate = true;
                NPC.velocity = Vector2.Zero;

                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                float ai0 = NPC.ai[2] == 1 ? -1 : 1;
                ai0 *= MathHelper.ToRadians(270) / 120;
                Vector2 vel = NPC.DirectionTo(player.Center).RotatedBy(-ai0 * 60);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ModContent.ProjectileType<AbomSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);

                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -vel, ModContent.ProjectileType<AbomSword>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 4f * 3 / 8), 0f, Main.myPlayer, ai0, NPC.whoAmI);
            }
            if (++NPC.ai[1] > 60)
            {
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;

                NPC.velocity.X = 0f;//(player.Center.X - NPC.Center.X) / 90 / 4;
                NPC.velocity.Y = 1.5f; //* Math.Sign(NPC.ai[3] - NPC.Center.Y);
            }
        }
        void MassacareDashP3()
        {
            TryMasoP3Theme();
            //    NPC.velocity.Y *= 0.97f;
            NPC.position += NPC.velocity;
            NPC.direction = NPC.spriteDirection = Math.Sign(NPC.ai[2] - NPC.Center.X);
            if (++NPC.ai[1] > 90)
            {
                NPC.netUpdate = true;
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
            }
        }
        void MassacareWaitP3()
        {
            TryMasoP3Theme();
            if (!AliveCheck(player))

                NPC.localAI[2] = 0;
            targetPos = player.Center;
            targetPos.X += 500 * (NPC.Center.X < targetPos.X ? -1 : 1);
            if (NPC.Distance(targetPos) > 50)
                if (++NPC.ai[1] > 60)
                {
                    NPC.netUpdate = true;
                    NPC.ai[0]--;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    NPC.localAI[0] = 0;
                }
        }
        void BoundaryBulletHellP3()
        {

            if (NPC.localAI[0] == 0)
            {
                if (!AliveCheck(player))
                    return;
                NPC.localAI[0] = Math.Sign(NPC.Center.X - player.Center.X);
            }

            if (++NPC.ai[1] > 3)
            {
                SoundEngine.PlaySound(SoundID.Item12, NPC.Center);
                NPC.ai[1] = 0;
                NPC.ai[2] += (float)Math.PI / 5 / 420 * NPC.ai[3] * NPC.localAI[0] * (2f);
                if (NPC.ai[2] > (float)Math.PI)
                    NPC.ai[2] -= (float)Math.PI * 2;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int max = 10;
                    for (int i = 0; i < max; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(0f, -6f).RotatedBy(NPC.ai[2] + MathHelper.TwoPi / max * i),
                          ModContent.ProjectileType<MutantEye>(), ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer);
                    }
                }
            }

            if (NPC.ai[3] < 30)
            {
                EModeSpecialEffects();
                TryMasoP3Theme();
            }

            int endTime = 360;

            endTime += 360;
            if (++NPC.ai[3] > endTime)
            {
                //NPC.TargetClosest();

                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.netUpdate = true;
            }

            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }

            NPC.velocity = Vector2.Zero;
        }
        void ShadowShtuxibusP3()
        {
            targetPos = player.Center + NPC.DirectionFrom(player.Center) * 400f;
            if (NPC.Distance(targetPos) > 50)
                MovementERI(targetPos, 0.3f, 24f);

            if (Main.netMode != NetmodeID.MultiplayerClient && !SparkAtaackUsing)
            {
                SparkAtaackUsing = true;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < (NPC.localAI[3] == 3 ? 1 : 1); j++) //do twice as many in p3
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextFloat(500, 700) * Vector2.UnitX.RotatedBy(Main.rand.NextDouble() * 2 * Math.PI);
                        Vector2 vel = NPC.velocity.RotatedBy(Main.rand.NextDouble() * Math.PI * 2);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "ShadowClone").Type,
                            ShtunUtils.ScaledProjectileDamage(NPC.damage), 0f, Main.myPlayer, NPC.target, 60 + 30 * i);
                    }
                }
            }


            if (++imtrydomove > 2)
            {
                imtrydomove = 0;
                SparkAtaackUsing = false;
            }
        }
        void FinalSpark()
        {

            void SpinLaser(bool useMasoSpeed)
            {
                float newRotation = NPC.DirectionTo(Main.player[NPC.target].Center).ToRotation();
                float difference = MathHelper.WrapAngle(newRotation - NPC.ai[3]);
                float rotationDirection = 2f * (float)Math.PI * 1f / 6f / 60f;
                rotationDirection *= useMasoSpeed ? 0.525f : 1f;
                NPC.ai[3] += Math.Min(rotationDirection, Math.Abs(difference)) * Math.Sign(difference);
                if (useMasoSpeed)
                    NPC.ai[3] = NPC.ai[3].AngleLerp(newRotation, 0.010f);
            }


            //if targets are all dead, will despawn much more aggressively to reduce respawn cheese
            if (NPC.localAI[2] > 30)
            {
                NPC.localAI[2] += 1; //after 30 ticks of no target, despawn can't be stopped
                if (NPC.localAI[2] > 120)
                    AliveCheck(player, true);
                return;
            }

            if (--NPC.localAI[0] < 0) //just visual explosions
            {
                NPC.localAI[0] = Main.rand.Next(30);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {

                    Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    int type = ModContent.ProjectileType<PhantasmalBlast>();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer);
                }
            }

            bool harderRings = NPC.ai[2] >= 420 - 90;
            int ringTime = harderRings ? 100 : 120;
            if (++NPC.ai[1] > ringTime)
            {
                NPC.ai[1] = 0;
                EModeSpecialEffects();
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int max = 11;
                    int damage = ShtunUtils.ScaledProjectileDamage(NPC.damage);
                    const int maxu = 18;
                    for (int i = 0; i < maxu; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 6f * NPC.DirectionTo(player.Center).RotatedBy(2 * Math.PI / maxu * i),
                            ModContent.ProjectileType<DeviHeart>(), damage, 0f, Main.myPlayer);
                    }
                    SpawnSphereRing(max, 6f, damage, 0.5f);
                    SpawnSphereRing(max, 6f, damage, -.5f);
                }
            }

            if (NPC.ai[2] == 0)
            {
                NPC.localAI[1] = 1;
            }
            else if (NPC.ai[2] == 420 - 90) //dramatic telegraph
            {
                if (NPC.localAI[1] == 0) //maso do ordinary spark
                {
                    NPC.localAI[1] = 1;
                    NPC.ai[2] -= 600 + 180;

                    NPC.ai[3] -= MathHelper.ToRadians(20);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {

                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]),
                        ModContent.ProjectileType<MutantGiantDeathray2>(), ShtunUtils.ScaledProjectileDamage(NPC.damage, 0.5f), 0f, Main.myPlayer, 0, NPC.whoAmI);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 24f * Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0f, Main.myPlayer,
                        Main.rand.NextFloat(0.5f, 1.25f) * (Main.rand.NextBool() ? -1 : 1), Main.rand.Next(10, 60));
                    }

                    NPC.netUpdate = true;
                }
                else
                {
                    SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        const int max = 8;
                        for (int i = 0; i < max; i++)
                        {
                            float offset = i - 0.5f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, (NPC.ai[3] + MathHelper.TwoPi / max * offset).ToRotationVector2(), ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, 0, 0f, Main.myPlayer, 13f, NPC.whoAmI);
                        }
                    }
                }
            }
            if (NPC.ai[2] < 420)
            {
                //disable it while doing maso's first ray
                if (NPC.localAI[1] == 0 || NPC.ai[2] > 420 - 90)
                    NPC.ai[3] = NPC.DirectionFrom(player.Center).ToRotation(); //hold it here for glow line effect
            }
            else
            {
                if (!Main.dedServ)
                {
                    if (ShaderManager.TryGetFilter("FargowiltasSouls.FinalSpark", out ManagedScreenFilter filter))
                    {
                        filter.Activate();
                        if (ShtunConfig.Instance.ForcedFilters && Main.WaveQuality == 0)
                            Main.WaveQuality = 1;
                    }
                }
                if (NPC.ai[1] % 3 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 24f * Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.Find<ModProjectile>(fargosouls.Name, "MutantEyeWavy").Type, 0, 0f, Main.myPlayer,
                      Main.rand.NextFloat(0.5f, 1.25f) * (Main.rand.NextBool() ? -1 : 1), Main.rand.Next(10, 60));
                }
            }

            int endTime = 1520;
            if (Main.zenithWorld) { endTime += 4000; }
            if (++NPC.ai[2] > endTime)
            {
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                ShtunUtils.ClearAllProjectiles(2, NPC.whoAmI);
            }
            else if (NPC.ai[2] == 420)
            {
                NPC.netUpdate = true;
                //bias it in one direction
                NPC.ai[3] += MathHelper.ToRadians(20) * (1);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]),
                    ModContent.ProjectileType<MutantGiantDeathray2>(), ShtunUtils.ScaledProjectileDamage(1000000000, 2f), 0f, Main.myPlayer, 0, NPC.whoAmI);
                }
            }
            else if (NPC.ai[2] > 420)
            {
                NPC.netUpdate = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitX.RotatedBy(NPC.ai[3]),
                    ModContent.ProjectileType<AbomLaser>(), ShtunUtils.ScaledProjectileDamage(2, 2f), 0f, Main.myPlayer, 0, NPC.whoAmI);
                }
            }

            else if (NPC.ai[2] < 300 && NPC.localAI[1] != 0)
            {
                float num1 = 0.99f;
                if (NPC.ai[2] >= 60)
                    num1 = 0.79f;
                if (NPC.ai[2] >= 120)
                    num1 = 0.58f;
                if (NPC.ai[2] >= 180)
                    num1 = 0.43f;
                if (NPC.ai[2] >= 240)
                    num1 = 0.33f;
                for (int i = 0; i < 9; ++i)
                {
                    if (Main.rand.NextFloat() >= num1)
                    {
                        float f = Main.rand.NextFloat() * 6.283185f;
                        float num2 = Main.rand.NextFloat();
                        Dust dust = Dust.NewDustPerfect(NPC.Center + f.ToRotationVector2() * (110 + 600 * num2), 229, (f - 3.141593f).ToRotationVector2() * (14 + 8 * num2), 0, default, 1f);
                        dust.scale = 0.9f;
                        dust.fadeIn = 1.15f + num2 * 0.3f;
                        dust.noGravity = true;
                    }
                }
            }

            SpinLaser(NPC.ai[2] >= 420);
            if (AliveCheck(player))
                NPC.localAI[2] = 0;
            else
                NPC.localAI[2]++;

            NPC.velocity = Vector2.Zero; //prevents mutant from moving despite calling AliveCheck()

        }
        void DyingDramaticPause()
        {
            TryMasoP3Theme();
            if (!AliveCheck(player))
                return;
            NPC.ai[3] -= (float)Math.PI / 6f / 60f;
            NPC.velocity = Vector2.Zero;
            if (++NPC.ai[1] > 2500)
            {
                NPC.netUpdate = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[3] = (float)-Math.PI / 2;
                NPC.netUpdate = true;
                if (Main.netMode != NetmodeID.MultiplayerClient) //shoot harmless mega ray
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.UnitY * -1, ModContent.ProjectileType<MutantGiantDeathray2>(), 0, 0f, Main.myPlayer, 1, NPC.whoAmI);
            }

            NPC.ai[1] += 2400;

            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = Main.rand.Next(15);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 spawnPos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    int type = ModContent.ProjectileType<PhantasmalBlastTS>();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
        }
        void DyingAnimationAndHandling()
        {
            NPC.velocity = Vector2.Zero;
            for (int i = 0; i < 5; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 12f;
            }
            if (--NPC.localAI[0] < 0)
            {
                NPC.localAI[0] = Main.rand.Next(5);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 spawnPos = NPC.Center + Main.rand.NextVector2Circular(240, 240);

                    int type = ModContent.ProjectileType<PhantasmalBlast>();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPos, Vector2.Zero, type, 0, 0f, Main.myPlayer);
                }
            }

            if (++NPC.ai[1] % 3 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 24f * Vector2.UnitX.RotatedBy(NPC.ai[3]), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0f, Main.myPlayer,
                    Main.rand.NextFloat(0.75f, 1.5f) * (Main.rand.NextBool() ? -1 : 1), Main.rand.Next(10, 90));
            }
            if (++NPC.alpha > 255)
            {
                NPC.life = 0;
                NPC.dontTakeDamage = false;
                NPC.life = 0;
                NPC.checkDead();
            }
        }

        #endregion

        public override bool PreKill()
        {
            if (ModLoader.TryGetMod("almazikmod", out Mod alm) || (ModLoader.TryGetMod("PowerfulSword", out Mod sex)))
            {
                return false;
            }
            return true;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.zenithWorld)
            {
                target.statLife = 0;
                TryLifeSteal(target.Center, target.whoAmI);
                target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 5400);
            }
            else
            {
                target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<GodEaterBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 5400);
            }
        }
        public override bool CheckDead()
        {
            if (NPC.ai[0] == -9)
                return true;
            NPC.life = 1;
            NPC.active = true;
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] > -1)
            {
                NPC.ai[0] = NPC.ai[0] >= 10 ? -1 : 10;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                NPC.localAI[1] = 0;
                NPC.localAI[2] = 0;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                ShtunUtils.ClearAllProjectiles(2, NPC.whoAmI, NPC.ai[0] < 0);
            }
            return false;
        }
        public override void OnKill()
        {
            ssm.amiactive = false;
            if (!WorldSaveSystem.downedShtuxibus)
            {
                ModContent.GetInstance<ShtuxiumOreSystem>().BlessWorldWithShtuxiumOre();
            }
            NPC.SetEventFlagCleared(ref WorldSaveSystem.downedShtuxibus, -1);
            if (!NPC.AnyNPCs(ModContent.NPCType<ShtuxianHarbringer>()))
            {
                int index = NPC.NewNPC(((Entity)this.NPC).GetSource_FromAI((string)null), (int)((Entity)this.NPC).Center.X, (int)((Entity)this.NPC).Center.Y, ModContent.NPCType<ShtuxianHarbringer>(), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int)byte.MaxValue);
            }
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (Main.zenithWorld)
            {
                ref StatModifier local = ref modifiers.FinalDamage;
                local *= 0.05f;
            }
            else
            {
                ref StatModifier local = ref modifiers.FinalDamage;
                local *= 0.5f;
            }

            if (modifiers.FinalDamage.Base > this.NPC.lifeMax / 10)
            {
                ShtunUtils.DisplayLocalizedText("USELESS! USELESS! USELESS! USELESS!", textColor);
                modifiers.FinalDamage.Base = 1;
            }

            if (damageTotal < dpsCap * 60)
            {
                //return;
                modifiers.FinalDamage.Base = 0.0f;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npcLoot);
            //npcLoot.AddConditionalPerPlayer(() => WorldSavingSystem.EternityMode, ModContent.ItemType<ChtuxlagorHeart>());
            //npcLoot.AddConditionalPerPlayer(() => Main.expertMode, ModContent.ItemType<ShtuxibusBag>());
            //npcLoot.AddConditionalPerPlayer(() => Main.zenithWorld, ModContent.ItemType<StarlightVodka>());

            if (!Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ShtuxiumSoulShard>(), 1, 20, 30));
                npcLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<EternalEnergy>(), 1, 40, 70));
                npcLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AbomEnergy>(), 1, 70, 100));
                npcLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DeviatingEnergy>(), 1, 100, 130));
            }

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.ShtuxibusTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.ShtuxibusRelic>()));

            if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                //npcLoot.AddConditionalPerPlayer(() => !WorldSaveSystem.downedShtuxibus, ModContent.ItemType<ShtuxibusLore>());
            }
        }
        public override void BossLoot(ref string name, ref int potionType) { potionType = ModContent.ItemType<Items.Consumables.UltimateHealingPotion>(); }
        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter > 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                    NPC.frame.Y = 0;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Npc[NPC.type].Value;
            Vector2 position = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;
            SpriteEffects effects = NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.EntitySpriteDraw(texture2D13, position, new Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, effects, 0);
            return false;
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            projectile.Kill();
            hit.Crit = false;
            hit.InstantKill = false;
        }
        public static void VisualEffectsSky()
        {
            if (!SkyManager.Instance["ssm:Shtuxibus"].IsActive())
                SkyManager.Instance.Activate("ssm:Shtuxibus");
        }
        public override void OnHitByItem(Player player, Item Item, NPC.HitInfo hit, int damageDone)
        {
            hit.Crit = false;
            hit.InstantKill = false;
        }
        private void OnHit(float damage)
        {
            damageTotal += (int)damage * 60;
        }
    }
}