using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Shtuxibus.Cal
{
    public class HolySpear : ModProjectile
    {
        Vector2 velocity = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
            writer.Write(Projectile.localAI[1]);
            writer.WriteVector2(velocity);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
            Projectile.localAI[1] = reader.ReadSingle();
            velocity = reader.ReadVector2();
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.45f * Projectile.Opacity, 0.35f * Projectile.Opacity, 0f);

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;

                if (Projectile.ai[0] == 1f)
                    velocity = Projectile.velocity;
            }

            bool commanderSpear = Projectile.ai[0] == -1f;
            bool enragedCommanderSpear = Projectile.ai[0] == -2f;
            float timeGateValue = 540f;
            if (Projectile.ai[0] <= 0f)
            {
                Projectile.ai[1] += 1f;

                float slowGateValue = 90f;
                float fastGateValue = 30f;
                float minVelocity = 6f;
                float maxVelocity = minVelocity * 4f;
                float extremeVelocity = maxVelocity * 2f;
                float deceleration = 0.95f;
                float acceleration = 1.2f;

                if (Projectile.localAI[1] >= timeGateValue)
                {
                    if (Projectile.velocity.Length() < extremeVelocity)
                        Projectile.velocity *= acceleration;
                }
                else
                {
                    if (Projectile.ai[1] <= slowGateValue)
                    {
                        if (Projectile.velocity.Length() > minVelocity)
                            Projectile.velocity *= deceleration;
                    }
                    else if (Projectile.ai[1] < slowGateValue + fastGateValue)
                    {
                        if (Projectile.velocity.Length() < maxVelocity)
                            Projectile.velocity *= acceleration;
                    }
                    else
                        Projectile.ai[1] = 0f;
                }
            }
            else
            {
                float frequency = 0.1f;
                float amplitude = 2f;

                Projectile.ai[1] += frequency;

                float wavyVelocity = (float)Math.Sin(Projectile.ai[1]);

                Projectile.velocity = velocity + new Vector2(wavyVelocity, wavyVelocity).RotatedBy(MathHelper.ToRadians(velocity.ToRotation())) * amplitude;
            }

            if (Projectile.localAI[1] < timeGateValue)
            {
                Projectile.localAI[1] += 1f;

                if (Projectile.timeLeft < 160)
                    Projectile.timeLeft = 160;
            }

            Projectile.Opacity = MathHelper.Lerp(240f, 220f, Projectile.timeLeft);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D drawTexture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            bool aimedSpear = Projectile.ai[0] > 0f;

            int red = 255;
            int green = 255;
            int blue = 0;

            Color baseColor = new Color(red, green, blue, 0);

            Color baseColor2 = baseColor;
            Vector2 projDirection = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Vector2 halfTextureSize = drawTexture.Size() / 2f;
            Color halfOfHalfBaseColor = baseColor2 * 0.5f;
            float timeLeftColorScale = Utils.GetLerpValue(15f, 30f, Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(240f, 200f, Projectile.timeLeft, clamped: true) * (1f + 0.2f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 30f / 0.5f * ((float)Math.PI * 2f) * 3f)) * 0.8f;
            Vector2 timeLeftDrawEffect = new Vector2(1f, 1.5f) * timeLeftColorScale;
            Vector2 timeLeftDrawEffect2 = new Vector2(0.5f, 1f) * timeLeftColorScale;
            baseColor2 *= timeLeftColorScale;
            halfOfHalfBaseColor *= timeLeftColorScale;

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = Projectile.oldPos[i] + projDirection;
                Color baseColorAlpha = Projectile.GetAlpha(baseColor2) * ((Projectile.oldPos.Length - i) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(drawTexture, drawPos, null, baseColorAlpha, Projectile.rotation, halfTextureSize, timeLeftDrawEffect, SpriteEffects.None, 0);
                Main.spriteBatch.Draw(drawTexture, drawPos, null, baseColorAlpha, Projectile.rotation, halfTextureSize, timeLeftDrawEffect2, SpriteEffects.None, 0);

                baseColorAlpha = Projectile.GetAlpha(halfOfHalfBaseColor) * ((Projectile.oldPos.Length - i) / Projectile.oldPos.Length);
                Main.spriteBatch.Draw(drawTexture, drawPos, null, baseColorAlpha, Projectile.rotation, halfTextureSize, timeLeftDrawEffect * 0.6f, SpriteEffects.None, 0);
                Main.spriteBatch.Draw(drawTexture, drawPos, null, baseColorAlpha, Projectile.rotation, halfTextureSize, timeLeftDrawEffect2 * 0.6f, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(drawTexture, projDirection, null, baseColor2, Projectile.rotation, halfTextureSize, timeLeftDrawEffect, spriteEffects, 0);
            Main.EntitySpriteDraw(drawTexture, projDirection, null, baseColor2, Projectile.rotation, halfTextureSize, timeLeftDrawEffect2, spriteEffects, 0);
            Main.EntitySpriteDraw(drawTexture, projDirection, null, halfOfHalfBaseColor, Projectile.rotation, halfTextureSize, timeLeftDrawEffect * 0.6f, spriteEffects, 0);
            Main.EntitySpriteDraw(drawTexture, projDirection, null, halfOfHalfBaseColor, Projectile.rotation, halfTextureSize, timeLeftDrawEffect2 * 0.6f, spriteEffects, 0);

            return false;
        }
    }
}
