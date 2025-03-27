using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantSansBeam : BaseDeathray
    {
        private const int descendTime = 50;

        public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/GolemBeam";

        public MutantSansBeam()
            : base(420f)
        {
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.hurtCooldowns[1] == 0;
        }

        public override bool? CanDamage()
        {
            return base.Projectile.localAI[0] > 50f;
        }

        public override void AI()
        {
            base.Projectile.alpha = 0;
            Vector2? vector78 = null;
            if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
            {
                base.Projectile.velocity = -Vector2.UnitY;
            }
            Projectile head = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(base.Projectile.owner, base.Projectile.ai[1]), ModContent.ProjectileType<MutantSansHead>());
            if (head != null)
            {
                base.Projectile.Center = head.Center + base.Projectile.velocity * 16f * 3f;
                if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
                {
                    base.Projectile.velocity = -Vector2.UnitY;
                }
                if (base.Projectile.localAI[0] == 0f && !Main.dedServ)
                {
                    SoundStyle style = new SoundStyle("FargowiltasSouls/Assets/Sounds/VanillaEternity/Golem/GolemBeam");
                    SoundEngine.PlaySound(in style, base.Projectile.Center);
                }
                float num801 = 1.3f;
                base.Projectile.localAI[0] += 1f;
                if (base.Projectile.localAI[0] >= base.maxTime)
                {
                    base.Projectile.Kill();
                    return;
                }
                base.Projectile.scale = num801;
                float num804 = base.Projectile.velocity.ToRotation();
                base.Projectile.rotation = num804 - (float)Math.PI / 2f;
                base.Projectile.velocity = num804.ToRotationVector2();
                float num805 = 3f;
                _ = base.Projectile.width;
                _ = base.Projectile.Center;
                if (vector78.HasValue)
                {
                    _ = vector78.Value;
                }
                float[] array3 = new float[(int)num805];
                for (int i = 0; i < array3.Length; i++)
                {
                    array3[i] = 1800f;
                }
                float num807 = 0f;
                for (int num808 = 0; num808 < array3.Length; num808++)
                {
                    num807 += array3[num808];
                }
                num807 /= num805;
                if (!(base.Projectile.localAI[0] <= 50f))
                {
                    return;
                }
                float targetLength = Math.Max(num807, 320f);
                base.Projectile.localAI[1] = MathHelper.Lerp(0f, targetLength, base.Projectile.localAI[0] / 50f);
                if (++base.Projectile.frameCounter > 3)
                {
                    base.Projectile.frameCounter = 0;
                    if (++base.Projectile.frame >= Main.projFrames[base.Projectile.type])
                    {
                        base.Projectile.frame = 0;
                    }
                }
            }
            else
            {
                base.Projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.FargoSouls().MaxLifeReduction += 100;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
            target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300);
        }

        private Rectangle Frame(Texture2D tex)
        {
            int frameHeight = tex.Height / Main.projFrames[base.Projectile.type];
            return new Rectangle(0, frameHeight * base.Projectile.frame, tex.Width, frameHeight);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (base.Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture2D19 = ModContent.Request<Texture2D>(this.Texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2D20 = ModContent.Request<Texture2D>(this.Texture + "2", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2D21 = ModContent.Request<Texture2D>(this.Texture + "3", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            float rayLength = base.Projectile.localAI[1];
            Vector2 arg_ABD8_2 = base.Projectile.Center - Main.screenPosition;
            Rectangle sourceRectangle2 = texture2D19.Bounds;
            Main.EntitySpriteDraw(texture2D19, arg_ABD8_2, sourceRectangle2, lightColor, base.Projectile.rotation, sourceRectangle2.Size() / 2f, base.Projectile.scale, SpriteEffects.None);
            rayLength -= (float)(texture2D19.Height / 2 + texture2D21.Height) * base.Projectile.scale;
            Vector2 value20 = base.Projectile.Center;
            value20 += base.Projectile.velocity * base.Projectile.scale * texture2D19.Height / 2f;
            if (rayLength > 0f)
            {
                float num224 = 0f;
                Rectangle rectangle7 = texture2D20.Bounds;
                while (num224 < rayLength)
                {
                    Main.EntitySpriteDraw(texture2D20, value20 - Main.screenPosition, rectangle7, Lighting.GetColor((int)value20.X / 16, (int)value20.Y / 16), base.Projectile.rotation, rectangle7.Size() / 2f, base.Projectile.scale, spriteEffects);
                    num224 += (float)rectangle7.Height * base.Projectile.scale;
                    value20 += base.Projectile.velocity * rectangle7.Height * base.Projectile.scale;
                }
            }
            Vector2 arg_AE2D_2 = value20 - Main.screenPosition;
            sourceRectangle2 = texture2D21.Bounds;
            Main.EntitySpriteDraw(texture2D21, arg_AE2D_2, sourceRectangle2, Lighting.GetColor((int)value20.X / 16, (int)value20.Y / 16), base.Projectile.rotation, sourceRectangle2.Size() / 2f, base.Projectile.scale, spriteEffects);
            return false;
        }
    }
}