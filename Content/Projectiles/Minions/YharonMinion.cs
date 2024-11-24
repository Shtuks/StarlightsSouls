using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Buffs.Minions;
using ssm.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs;

namespace ssm.Content.Projectiles.Minions
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class YharonMinion : ModProjectile
    {
        public static readonly SoundStyle RoarSound = new("CalamityMod/Sounds/Custom/Yharon/YharonRoar");
        public static readonly SoundStyle ShortRoarSound = new("CalamityMod/Sounds/Custom/Yharon/YharonRoarShort");
        public static readonly SoundStyle FireSound = new("CalamityMod/Sounds/Custom/Yharon/YharonFire");
        public static readonly SoundStyle OrbSound = new("CalamityMod/Sounds/Custom/Yharon/YharonFireOrb");
        public static readonly SoundStyle HitSound = new("CalamityMod/Sounds/NPCHit/YharonHurt");
        public static readonly SoundStyle DeathSound = new("CalamityMod/Sounds/NPCKilled/YharonDeath");

        public enum AIState
        {
            HoverNearOwner,
            ChargeAtEnemies,
            SummonFirenados
        }

        public bool UseAfterimages;

        public AIState CurrentState
        {
            get => (AIState)Projectile.ai[0];
            set => Projectile.ai[0] = (int)value;
        }

        public Player Owner => Main.player[Projectile.owner];

        public ref float AITimer => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 8;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = InfectedRemote.MinionSlotRequirement;
            Projectile.timeLeft = 90000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = InfectedRemote.DefaultIframes;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            HandleMinionBools();

            DecideFrames();

            UseAfterimages = false;
            Projectile.MaxUpdates = 1;
            Projectile.localNPCHitCooldown = InfectedRemote.DefaultIframes;

            NPC potentialTarget = Projectile.Center.MinionHoming(InfectedRemote.EnemyTargetingRange, Owner);
            switch (CurrentState)
            {
                case AIState.HoverNearOwner:
                    DoBehavior_HoverNearOwner(potentialTarget);
                    break;
                case AIState.ChargeAtEnemies:
                    DoBehavior_ChargeAtEnemies(potentialTarget);
                    break;
                case AIState.SummonFirenados:
                    DoBehavior_SummonFirenados(potentialTarget);
                    break;
            }

            AITimer++;
        }

        public void HandleMinionBools()
        {
            Owner.AddBuff(ModContent.BuffType<YharonBuff>(), 3600);
            if (Projectile.type == ModContent.ProjectileType<YharonMinion>())
            {
                if (Owner.dead)
                    Owner.Shtun().yharon = false;

                if (Owner.Shtun().yharon)
                    Projectile.timeLeft = 2;
            }
        }

        public void DecideFrames()
        {
            Projectile.frameCounter++;
            Projectile.frame = Projectile.frameCounter / 6 % Main.projFrames[Projectile.type];
        }

        public void DoBehavior_HoverNearOwner(NPC potentialTarget)
        {
            if (Projectile.WithinRange(Owner.Center, 160f))
                return;

            Projectile.velocity = (Projectile.velocity * 34f + Projectile.SafeDirectionTo(Owner.Center) * 17f) / 35f;

            if (!Projectile.WithinRange(Owner.Center, 2500f))
            {
                Projectile.Center = Owner.Center;
                Projectile.velocity *= 0.3f;
                Projectile.netUpdate = true;
            }

            if (Math.Abs(Projectile.velocity.X) > 0.2f)
                Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);

            if (potentialTarget is not null)
            {
                CurrentState = AIState.ChargeAtEnemies;
                AITimer = 0f;
                Projectile.netUpdate = true;
            }

            Projectile.rotation = Projectile.rotation.AngleTowards(0f, 0.1f);
        }

        public void DoBehavior_ChargeAtEnemies(NPC target)
        {
            int hoverTime = 18;
            int chargeTime = 16;
            int slowdownTime = 15;
            int chargeCount = 3;
            float hoverSpeed = 17f;

            if (target is null)
            {
                ReturnToIdleState();
                return;
            }

            Projectile.MaxUpdates = InfectedRemote.MaxUpdatesWhenCharging;
            Projectile.localNPCHitCooldown = InfectedRemote.ChargeIframes;

            float wrappedAttackTimer = AITimer % (hoverTime + chargeTime + slowdownTime);

            if (wrappedAttackTimer < hoverTime)
            {
                Projectile.spriteDirection = (target.Center.X > Projectile.Center.X).ToDirectionInt();
                HoverToPosition(target.Center + new Vector2(Projectile.spriteDirection * -270f, -150f), hoverSpeed);
            }

            if (wrappedAttackTimer == hoverTime)
            {
                SoundEngine.PlaySound(RoarSound, Projectile.Center);
                Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTarget(Projectile.Center, target, InfectedRemote.RegularChargeSpeed * 0.55f, 8);
                Projectile.netUpdate = true;
            }

            if (wrappedAttackTimer >= hoverTime + chargeTime)
                Projectile.velocity *= 0.825f;

            Projectile.rotation = Projectile.velocity.X * 0.014f;

            if (AITimer >= (hoverTime + chargeTime + slowdownTime) * chargeCount)
            {
                AITimer = 0f;
                Projectile.velocity *= 0.3f;
                CurrentState = AIState.SummonFirenados;
                Projectile.netUpdate = true;
            }
        }

        public void DoBehavior_SummonFirenados(NPC target)
        {
            int shootTime = 300;
            float hoverSpeed = 23f;

            if (target is null)
            {
                ReturnToIdleState();
                return;
            }

            Vector2 hoverDestination = target.Center - Vector2.UnitY * 350f;
            HoverToPosition(hoverDestination, hoverSpeed);

            if (Projectile.WithinRange(hoverDestination, 240f))
            {
                Projectile.velocity *= 0.7f;
                Projectile.Center = Vector2.Lerp(Projectile.Center, hoverDestination, 0.04f);

                if (Main.myPlayer == Projectile.owner && AITimer % 22 == 22 - 1f)
                {
                    int fireball = ModContent.ProjectileType<YharonFireballMinion>();

                    if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.Center, 0, 0))
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Vector2 Velocity = Projectile.SafeDirectionTo(target.Center) * 6f;
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Velocity, fireball, (int)(Projectile.damage * 0.65), 0f, Projectile.owner);
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }

            if (AITimer >= shootTime)
            {
                AITimer = 0f;
                Projectile.velocity *= 0.3f;
                CurrentState = AIState.ChargeAtEnemies;
                Projectile.netUpdate = true;
            }
        }

        public void HoverToPosition(Vector2 hoverDestination, float hoverSpeed)
        {
            Vector2 baseHoverVelocity = Projectile.SafeDirectionTo(hoverDestination) * hoverSpeed;

            if (!Projectile.WithinRange(hoverDestination, 150f))
            {
                float hyperspeedInterpolant = Utils.GetLerpValue(Projectile.Distance(hoverDestination), 500f, 960f, true);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Lerp(baseHoverVelocity * 1.4f, (hoverDestination - Projectile.Center) * 0.1f, hyperspeedInterpolant), 0.2f);
            }
            else
            {
                Projectile.velocity = (Projectile.velocity * 29f + baseHoverVelocity) / 30f;
                Projectile.velocity = Projectile.velocity.MoveTowards(baseHoverVelocity, hoverSpeed / 11f);
            }
        }

        public void ReturnToIdleState()
        {
            CurrentState = AIState.HoverNearOwner;
            Projectile.netUpdate = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
            Vector2 origin = frame.Size() * 0.5f;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            SpriteEffects direction = Projectile.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (UseAfterimages)
            {
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    Color afterimageDrawColor = Color.ForestGreen with { A = 25 } * Projectile.Opacity * (1f - i / (float)Projectile.oldPos.Length);
                    Vector2 afterimageDrawPosition = Projectile.oldPos[i] + Projectile.Size * 0.5f - Main.screenPosition;
                    Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, Projectile.rotation, origin, Projectile.scale, direction, 0);
                }
            }
            Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, direction, 0);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);

        public override void OnHitPlayer(Player target, Player.HurtInfo info) => target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);

        public override bool? CanDamage() => CurrentState == AIState.ChargeAtEnemies ? null : false;
    }
}
