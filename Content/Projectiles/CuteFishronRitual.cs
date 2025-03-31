using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles
{
    public class CuteFishronRitual : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FishronRitual";
        public override void SetDefaults()
        {
            Projectile.width = 320;
            Projectile.height = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Main.player[Projectile.owner].active && !Main.player[Projectile.owner].dead
                && Main.player[Projectile.owner].mount.Active && Main.player[Projectile.owner].mount.Type == MountID.CuteFishron
                && Main.player[Projectile.owner].GetModPlayer<ShtunPlayer>().CyclonicFin)
            {
                Projectile.alpha -= 7;
                Projectile.timeLeft = 300;
                Projectile.Center = Main.player[Projectile.owner].MountedCenter;
            }
            else
            {
                Projectile.alpha += 7;
                Projectile.Center = Main.player[Projectile.owner].Center;
            }

            if (Projectile.alpha < 0)
                Projectile.alpha = 0;
            if (Projectile.alpha > 255)
            {
                Projectile.alpha = 255;
                Projectile.Kill();
                return;
            }
            Projectile.scale = 1f - Projectile.alpha / 255f;
            Projectile.rotation += (float)Math.PI / 70f;

            if (Projectile.alpha == 0)
            {
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    float num = Main.rand.Next(2, 4);
                    float scale = Projectile.scale * 0.6f;
                    if (index1 == 1)
                    {
                        scale *= 0.42f;
                        num *= -0.75f;
                    }
                    Vector2 vector21 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    vector21.Normalize();
                    int index21 = Dust.NewDust(Projectile.Center, 0, 0, 135, 0f, 0f, 100, new Color(), 2f);
                    Main.dust[index21].noGravity = true;
                    Main.dust[index21].noLight = true;
                    Main.dust[index21].position += vector21 * 204f * scale;
                    Main.dust[index21].velocity = vector21 * -num;
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.dust[index21].velocity *= 2f;
                        Main.dust[index21].scale += 0.5f;
                    }
                }
            }
            
            Lighting.AddLight(Projectile.Center, 0.4f, 0.9f, 1.1f);
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}