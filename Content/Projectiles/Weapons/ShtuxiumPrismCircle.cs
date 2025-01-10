using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Weapons
{
    internal class ShtuxiumPrismCircle : ModProjectile
    {
        public Player Owner => Main.player[Projectile.owner];
        public ref float Time => ref Projectile.ai[0];

        public float ChargeupCompletion => MathHelper.Clamp(Time / ChargeupTime, 0f, 1f);

        public const int ChargeupTime = 180;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 114;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 900000;
        }

        public override void AI()
        {
            //Projectile.Calamity().UpdatePriority = 1f;

            if (!Owner.channel || Owner.noItems || Owner.CCed)
            {
                Projectile.Kill();
                return;
            }

            if (Time >= 1f && Owner.ownedProjectileCounts[ModContent.ProjectileType<ShtuxiumPrismHoldout>()] <= 0)
            {
                Projectile.Kill();
                return;
            }

            Projectile.scale = Utils.GetLerpValue(0f, 35f, Time, true) * 1.4f;
            Projectile.Opacity = (float)Math.Pow(Projectile.scale / 1.4f, 2D);
            Projectile.rotation -= MathHelper.ToRadians(Projectile.scale * 4f);

            if (Main.myPlayer != Projectile.owner)
            {
                return;
            }

            Vector2 idealDirection = Owner.SafeDirectionTo(Main.MouseWorld, Vector2.UnitX * Owner.direction);
            Vector2 newAimDirection = Projectile.velocity.MoveTowards(idealDirection, 0.05f);

            if (newAimDirection != Projectile.velocity)
            {
                Projectile.netUpdate = true;
                Projectile.netSpam = 0;
            }

            Projectile.velocity = newAimDirection;
            Projectile.direction = (Projectile.velocity.X > 0f).ToDirectionInt();

            Vector2 circlePointDirection = Projectile.velocity.SafeNormalize(Vector2.UnitX * Owner.direction);
            Projectile.Center = Owner.Center + circlePointDirection * Projectile.scale * 56f;

            Owner.ChangeDir(Projectile.direction);

            if (Time < ChargeupTime) {
                if (Time == ChargeupTime - 1f)
                {
                    if (Main.myPlayer == Projectile.owner)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<ShtuxiumPrismDeathray>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.identity);
                }
            }

            Time++;
        }


        //public override bool PreDraw(ref Color lightColor)
        //{
        //    Texture2D outerCircleTexture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
        //    Texture2D outerCircleGlowmask = ModContent.Request<Texture2D>(Texture + "Glowmask").Value;
        //    Texture2D innerCircleTexture = ModContent.Request<Texture2D>(Texture + "Inner").Value;
        //    Texture2D innerCircleGlowmask = ModContent.Request<Texture2D>(Texture + "InnerGlowmask").Value;
        //    Vector2 drawPosition = Projectile.Center - Main.screenPosition;

        //    float directionRotation = Projectile.velocity.ToRotation();
        //    Color startingColor = Color.Red;
        //    Color endingColor = Color.Blue;

        //    void restartShader(Texture2D texture, float opacity, float circularRotation, BlendState blendMode)
        //    {
        //        Main.spriteBatch.End();
        //        Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendMode, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

        //        CalamityUtils.CalculatePerspectiveMatricies(out Matrix viewMatrix, out Matrix projectionMatrix);

        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseColor(startingColor);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSecondaryColor(endingColor);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSaturation(directionRotation);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseOpacity(opacity);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uDirection"].SetValue((float)Projectile.direction);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uCircularRotation"].SetValue(circularRotation);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uImageSize0"].SetValue(texture.Size());
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["overallImageSize"].SetValue(outerCircleTexture.Size());
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uWorldViewProjection"].SetValue(viewMatrix * projectionMatrix);
        //        GameShaders.Misc["CalamityMod:RancorMagicCircle"].Apply();
        //    }

        //    restartShader(outerCircleGlowmask, Projectile.Opacity, Projectile.rotation, BlendState.Additive);
        //    Main.EntitySpriteDraw(outerCircleGlowmask, drawPosition, null, Color.White, 0f, outerCircleGlowmask.Size() * 0.5f, Projectile.scale * 1.075f, SpriteEffects.None, 0);

        //    restartShader(outerCircleTexture, Projectile.Opacity * 0.7f, Projectile.rotation, BlendState.AlphaBlend);
        //    Main.EntitySpriteDraw(outerCircleTexture, drawPosition, null, Color.White, 0f, outerCircleTexture.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);

        //    restartShader(innerCircleGlowmask, Projectile.Opacity * 0.5f, 0f, BlendState.Additive);
        //    Main.EntitySpriteDraw(innerCircleGlowmask, drawPosition, null, Color.White, 0f, innerCircleGlowmask.Size() * 0.5f, Projectile.scale * 1.075f, SpriteEffects.None, 0);

        //    restartShader(innerCircleTexture, Projectile.Opacity * 0.7f, 0f, BlendState.AlphaBlend);
        //    Main.EntitySpriteDraw(innerCircleTexture, drawPosition, null, Color.White, 0f, innerCircleTexture.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);
        //    Main.spriteBatch.ExitShaderRegion();

        //    return false;
        //}

        public override bool ShouldUpdatePosition() => false;

    }
}