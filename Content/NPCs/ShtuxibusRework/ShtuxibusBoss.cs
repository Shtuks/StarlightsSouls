using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria;
using ssm.Content.Buffs;
using ssm.Systems;
using ssm.Content.Projectiles.Shtuxibus;
using Terraria.ID;
using System;
using Terraria.Audio;
using ssm.Content.Items.Consumables;
using ssm.Content.NPCs.Shtuxibus;

namespace ssm.Content.NPCs.ShtuxibusRework
{
    [AutoloadBossHead]
    public class ShtuxibusBoss : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        Player player => Main.player[NPC.target];

        private bool drainLifeInP3 = true;
        private bool playerInvulTriggered;
        private bool spawned;

        private bool isAura;
        private int arenaSize;
        private int ritualProj, spriteProj, ringProj;
        private bool ShtuxibusRitualActive = false;
        private bool ShouldDrawAura;

        private int maxDPS = 1000000;
        private int lastHitTime = 0;
        private int damageCounter = 0;


        private List<int> lastAttacks = new List<int>();
        private int currentAttack;
        private int attackTimer;
        private int phase = 1;
        private int generalTimer;
        

        public override void AI()
        {
            ssm.amiactive = true;
            ShtunNpcs.Shtuxibus = NPC.whoAmI;
            NPC.dontTakeDamage = currentAttack < 0;
            ShouldDrawAura = false;
            ManageAurasAndPreSpawn();
            ManageNeededProjectiles();

            NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? 1 : -1;

            if (currentAttack < 0 || currentAttack > 10)
            {
                Main.dayTime = false;
                Main.time = 16200;
                Main.raining = false;
                Main.rainTime = 0;
                Main.maxRaining = 0;
                Main.bloodMoon = false;
            }

            if (currentAttack < 0 && NPC.life > 1 && drainLifeInP3)
            {
                int time = 3200;
                NPC.life -= NPC.lifeMax / time;
                if (NPC.life < 1)
                    NPC.life = 1;
            }

            if (player.immune || player.hurtCooldowns[0] != 0 || player.hurtCooldowns[1] != 0)
                playerInvulTriggered = true;
        }

        #region helper methods
        void TryLifeSteal(Vector2 pos, int playerWhoAmI)
        {
            int totalHealPerHit = NPC.lifeMax / 100 * 10;

            const int max = 20;
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = Main.rand.NextFloat(2f, 9f) * -Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi);
                float ai0 = NPC.whoAmI;
                float ai1 = vel.Length() / Main.rand.Next(30, 90);

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
            if (!spawned)
            {
                spawned = true;
                NPC.life = NPC.lifeMax;
            }

            Main.LocalPlayer.AddBuff(ModContent.BuffType<ShtuxibusCurse>(), 2);
        }

        void ManageNeededProjectiles()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (currentAttack != -17 && (currentAttack < 0 || currentAttack > 10) && ShtunUtils.ProjectileExists(ritualProj, ModContent.ProjectileType<MutantRitual>()) == null)
                    ritualProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), ShtunUtils.ScaledProjectileDamage(0), 0f, Main.myPlayer, 0f, NPC.whoAmI);

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

        public void SelectAttack(params int[] attacks)
        {
            NPC.netUpdate = true;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                var availableAttacks = attacks.Except(lastAttacks).ToList();

                if (availableAttacks.Count == 0)
                {
                    lastAttacks.Clear();
                    availableAttacks = attacks.ToList();
                }

                Random random = new Random();
                int selectedAttack = availableAttacks[random.Next(availableAttacks.Count)];

                lastAttacks.Add(selectedAttack);

                if (lastAttacks.Count > 3)
                {
                    lastAttacks.RemoveAt(0);
                }

                currentAttack = selectedAttack;
            }
        }

        void SpawnSphereRing(int type, int max, float speed, int damage, float rotationModifier, float offset = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;
            float rotation = 2f * (float)Math.PI / max;
            for (int i = 0; i < max; i++)
            {
                Vector2 vel = speed * Vector2.UnitY.RotatedBy(rotation * i + offset);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * NPC.spriteDirection, speed);
            }
            SoundEngine.PlaySound(SoundID.Item84, NPC.Center);
        }
        bool AliveCheck(Player p, bool forceDespawn = false)
        {
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
            if (phase != 2)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    phase = 2;
                    currentAttack = 10;
                    attackTimer = 0;
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
        }

        void ExpertEffects()
        {
            if (Main.GameModeInfo.IsJourneyMode && CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().Enabled)
                CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().SetPowerInfo(false);

            if (!SkyManager.Instance["ssm:ShtuxibusReworkExpert"].IsActive())
            {
                SkyManager.Instance.Activate("ssm:ShtuxibusReworkExpert");
            }

            if (ModLoader.TryGetMod("ssm", out Mod musicMod))
            {
                if (!Main.zenithWorld)
                    Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/ORDER");
                else if(ShtunConfig.Instance.Stalin)
                    Music = ShtunUtils.Stalin ? MusicLoader.GetMusicSlot(musicMod, "Assets/Music/StainedBrutalCommunism") : Music = MusicLoader.GetMusicSlot(musicMod, "Assets/Music/Halcyon");
            }
        }
        #endregion

        #region overrides methods
        public override void ModifyIncomingHit(ref Terraria.NPC.HitModifiers modifiers)
        {
            int currentTime = (int)Main.GameUpdateCount;
            int timeSinceLastHit = currentTime - lastHitTime;

            if (timeSinceLastHit > 60)
            {
                damageCounter = 0;
                lastHitTime = currentTime;
            }

            int potentialDamage = (int)(damageCounter + modifiers.FinalDamage.Base);

            if (potentialDamage > maxDPS)
            {
                int allowedDamage = maxDPS - damageCounter;
                if (allowedDamage < 0) allowedDamage = 0;

                modifiers.FinalDamage.Base = allowedDamage;

                damageCounter = maxDPS;
            }
            else
            {
                damageCounter += (int)modifiers.FinalDamage.Base;
            }
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
                CustomTexturePath = "ssm/Content/NPCs/ShtuxibusRework/ShtuxibusBestiary",
                PortraitScale = 0.6f,
                PortraitPositionYOverride = 0f,
            };
        }
        public override void SetDefaults()
        {
            //NPC.BossBar = ModContent.GetInstance<ShtuxibusBar>();

            NPC.width = 120;
            NPC.height = 120;
            NPC.damage = 3000;
            NPC.value = Item.buyPrice(999999);
            NPC.lifeMax = 13000000;

            if (Main.expertMode)
            {
                NPC.damage = 5000;
                NPC.lifeMax = 17000000;
            }
            if (Main.masterMode)
            {
                NPC.damage = 7000;
                NPC.lifeMax = 20000000;
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

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 2300);

            if (Main.zenithWorld)
            {
                target.statLife = 0;
                TryLifeSteal(target.Center, target.whoAmI);
            }
        }
        public override bool CheckDead()
        {
            if (currentAttack == -9)
                return true;
            NPC.life = 1;
            NPC.active = true;
            if (Main.netMode != NetmodeID.MultiplayerClient && currentAttack > -1)
            {
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                ShtunUtils.ClearAllProjectiles(2, NPC.whoAmI, currentAttack < 0);
            }
            return false;
        }
        public override void OnKill()
        {
            ssm.amiactive = false;

            if (!playerInvulTriggered)
            {
                Item.NewItem(base.NPC.GetSource_Loot(), base.NPC.Hitbox, ModContent.ItemType<Sadism>(), 30);
            }
            if (!WorldSaveSystem.downedShtuxibus)
            {
                ModContent.GetInstance<Tiles.ShtuxiumOreSystem>().BlessWorldWithShtuxiumOre();
            }
            NPC.SetEventFlagCleared(ref WorldSaveSystem.downedShtuxibus, -1);
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
            hit.InstantKill = false;
        }

        public static void VisualEffectsSky()
        {
            if (!SkyManager.Instance["ssm:ShtuxibusRework"].IsActive())
                SkyManager.Instance.Activate("ssm:ShtuxibusRework");
        }

        public override void OnHitByItem(Player player, Item Item, NPC.HitInfo hit, int damageDone)
        {
            hit.InstantKill = false;
        }
        #endregion
    }
}