using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantSlimeSpike : MutantSlimeBall
    {
        public override string Texture => "Terraria/Images/Projectile_920";

        public override void SetStaticDefaults()
        {
            Main.projFrames[base.Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Projectile.timeLeft *= 2;
        }

        public override void AI()
        {
            base.AI();
            base.Projectile.frame = (int)base.Projectile.ai[2];
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
            int num156 = texture2D13.Height / Main.projFrames[base.Projectile.type];
            int y3 = num156 * base.Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color26 = lightColor;
            color26 = base.Projectile.GetAlpha(color26);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
            {
                Color color27 = color26 * 0.5f;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
                Vector2 value4 = base.Projectile.oldPos[i];
                float num165 = base.Projectile.oldRot[i];
                Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None);
            }
            Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
            return false;
        }
    }
}

