using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxianArrowProj : ModProjectile
    {
        public ref float Time => ref Projectile.ai[0];
        public ref float ProjectileSpeed => ref Projectile.ai[1];
        public bool Phase2 = false;
        public float HomingTime = 0;
        public Color MainColor;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.arrow = true;
            Projectile.penetrate = int.MaxValue;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 10;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15 * Projectile.extraUpdates;
        }

        public override void AI()
        {
            if (Time == 0)
            {
                Projectile.velocity *= 0.4f;
                ProjectileSpeed = 30;
                MainColor = Main.rand.NextBool() ? Color.Cyan : Color.Magenta;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            if (Time > 4f)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 74, -Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(0.15f, 0.35f);
            }


            NPC target = Projectile.Center.ClosestNPCAt(65);

            if (target != null)
            {
                Phase2 = true;
                HomingTime = 1;
            }

            if (HomingTime == 1)
                ShtunUtils.HomeInOnNPC(Projectile, true, 20000f, 12, 200f);

            Time++;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return lightColor;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
        }
    }
}
