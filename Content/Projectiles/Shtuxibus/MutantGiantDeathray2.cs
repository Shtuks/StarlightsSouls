using ssm.Assets;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using ssm;
using FargowiltasSouls;
using ssm.Content.NPCs.Shtuxibus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus
{
	public class MutantGiantDeathray2 : MutantSpecialDeathray
    {
        public MutantGiantDeathray2() : base(600) { }

        public int dustTimer;
        public bool stall;
        public bool BeBrighter => Projectile.ai[0] > 0f;


        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            // DisplayName.SetDefault("Phantasmal Deathray");
            ProjectileID.Sets.DismountsPlayersOnHit[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.netImportant = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
            maxTime += 700;
        }

        public override bool? CanDamage()
        {
            Projectile.maxPenetrate = 1;
            return Projectile.scale >= 5f;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);

            writer.Write(Projectile.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);

            Projectile.localAI[0] = reader.ReadSingle();
        }

        public override void AI()
        {
            base.AI();

            if (!Main.dedServ && Main.LocalPlayer.active)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);

            Projectile.timeLeft = 2;

            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[1], ModContent.NPCType<Shtuxibus>());
            if (npc != null)
            {
                Projectile.Center = npc.Center + Main.rand.NextVector2Circular(5, 5) + Vector2.UnitX.RotatedBy(npc.ai[3]) * (npc.ai[0] == -7 ? 100 : 175) * Projectile.scale / 10f;
                if (npc.ai[0] == -0) //death animation, not actual attack
                {
                    maxTime = 255;
                }
                else if (npc.ai[0] == -7) //final spark
                {
                    if (npc.HasValidTarget && Main.player[npc.target].HasBuff<TimeFrozenBuff>())
                        stall = true;

                    if (npc.localAI[2] > 30) //mutant is forcing a despawn
                    {
                        //so this should disappear too
                        if (Projectile.localAI[0] < maxTime - 90)
                            Projectile.localAI[0] = maxTime - 90;
                    }
                    else if (stall)
                    {
                        Projectile.localAI[0] -= 1;
                        Projectile.netUpdate = true;

                        npc.ai[2] -= 1;
                        npc.netUpdate = true;
                    }
                    else if (Main.getGoodWorld && Projectile.localAI[0] > maxTime - 10 && npc.life > 1)
                    {
                        Projectile.localAI[0] -= 1;
                    }
                }
            }
            else
            {
                Projectile.Kill();
                return;
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Zombie104 with { Volume = 1.5f }, Main.player[Main.myPlayer].Center);
            }
            float num801 = 10f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }

            float scale = stall ? 1f : (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime);
            stall = false;
            Projectile.scale = scale * 7f * num801;
            if (WorldSavingSystem.MasochistModeReal)
                Projectile.scale *= 5f;
            if (Projectile.scale > num801)
                Projectile.scale = num801;
            float num804 = npc.ai[3] - 1.57079637f;
            float oldRot = Projectile.rotation;
            Projectile.rotation = num804;
            num804 += 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 3000f;
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);

            if (Projectile.damage > 0 && Main.LocalPlayer.active && Projectile.Colliding(Projectile.Hitbox, Main.LocalPlayer.Hitbox))
            {
                Main.LocalPlayer.immune = false;
                Main.LocalPlayer.immuneTime = 0;
                Main.LocalPlayer.hurtCooldowns[0] = 0;
                Main.LocalPlayer.hurtCooldowns[1] = 0;
                Main.LocalPlayer.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
            }
        }

        private int hits;

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            base.ModifyHitPlayer(target, ref modifiers);
            modifiers.FinalDamage *= DamageRampup();
            if (hits > 180)
                target.endurance = 0;
                target.lifeRegen -= 10000;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);
            modifiers.FinalDamage *= DamageRampup();
        }

        private float DamageRampup()
        {
            stall = true;

            hits++;
            int tempHits = hits - 90;
            if (tempHits > 0)
            {
                const float cap = 100000.0f;
                float modifier = (float)Math.Min(Math.Pow(tempHits, 2), cap);
                if (modifier < 0)
                {
                    hits--;
                    modifier = 100000.0f;
                }
                return modifier;
            }
            else
            {
                return hits / 90f;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (WorldSavingSystem.EternityMode)
            {
                target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            }
            target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);

            target.immune = false;
            target.immuneTime = 0;
            target.hurtCooldowns[0] = 0;
            target.hurtCooldowns[1] = 0;

            target.velocity = -0.4f * Vector2.UnitY;

            target.GetModPlayer<FargoSoulsPlayer>().NoUsingItems = 2;
        }

        public float WidthFunction(float trailInterpolant)
        {
            // Grow rapidly from the start to full length. Any more than this notably distorts the texture.
            float baseWidth = Projectile.scale * Projectile.width;
            return baseWidth;

            // Grow to 2x width by the end. Any more than this distorts the texture too much.
            //return MathHelper.Lerp(baseWidth, baseWidth * 2, trailInterpolant);
        }

        public static Color ColorFunction(float trailInterpolant) =>
            Color.Lerp(new(31, 187, 192, 100), new(51, 255, 191, 100), trailInterpolant);
        public override bool PreDraw(ref Color lightColor)
        {
            // This should never happen, but just in case.
            if (Projectile.velocity == Vector2.Zero)
                return false;

            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.MutantDeathray");

            // Get the laser end position.
            Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * drawDistance;

            // Create 8 points that span across the draw distance from the projectile center.

            // This allows the drawing to be pushed back, which is needed due to the shader fading in at the start to avoid
            // sharp lines.
            Vector2 initialDrawPoint = Projectile.Center - Projectile.velocity * 400f;
            Vector2[] baseDrawPoints = new Vector2[8];
            for (int i = 0; i < baseDrawPoints.Length; i++)
                baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

            // Set shader parameters. This one takes a fademap and a color.

            // The laser should fade to white in the middle.
            Color brightColor = new(194, 255, 194, 100);
            shader.TrySetParameter("mainColor", brightColor);
            FargoSoulsUtil.SetTexture1(ShtunTextureRegistry.ShtuxibusStreak.Value);
            // Draw a big glow above the start of the laser, to help mask the intial fade in due to the immense width.

            Texture2D glowTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing").Value;

            Vector2 glowDrawPosition = Projectile.Center - Projectile.velocity * (BeBrighter ? 90f : 180f);

            Main.EntitySpriteDraw(glowTexture, glowDrawPosition - Main.screenPosition, null, brightColor, Projectile.rotation, glowTexture.Size() * 0.5f, Projectile.scale * 0.4f, SpriteEffects.None, 0);
            PrimitiveRenderer.RenderTrail(baseDrawPoints, new(WidthFunction, ColorFunction, Shader: shader), 60);
            return false;
        }
    }
}