using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantSlimeBall : ModProjectile
    {
       public override string Texture => "Terraria/Images/Projectile_9";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            CooldownSlot = 1;
            Projectile.scale = 1.5f;
            Projectile.alpha = 50;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 90 * (Projectile.extraUpdates + 1);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - (float)Math.PI / 2;
            if (Projectile.localAI[0] == 0) //choose a texture to use
            {
                Projectile.localAI[0] += Main.rand.Next(1, 4);
            }

            if (Projectile.timeLeft % Projectile.MaxUpdates == 0)
            {
                if (++Projectile.frameCounter >= 6)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= Main.projFrames[Projectile.type])
                        Projectile.frame = 0;
                }
            }

            if (++Projectile.localAI[1] > 10 && ShtunUtils.BossIsAlive(ref ShtunNpcs.Shtuxibus, ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>()))
            {
                float yOffset = Projectile.Center.Y - Main.npc[ShtunNpcs.Shtuxibus].Center.Y;
                if (Math.Sign(yOffset) == Math.Sign(Projectile.velocity.Y) && Projectile.Distance(Main.npc[ShtunNpcs.Shtuxibus].Center) > 1200 + Projectile.ai[0])
                    Projectile.timeLeft = 0;
            }
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < 20; i++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 59, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 59, -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }

      

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Purple * Projectile.Opacity;
        }

    }

}