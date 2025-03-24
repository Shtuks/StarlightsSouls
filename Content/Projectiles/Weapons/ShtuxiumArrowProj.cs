using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumArrowProj : ModProjectile
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
            Projectile.penetrate = 2;
            Projectile.damage = 745;
            Projectile.timeLeft = 2000;
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
                MainColor = Color.Green;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            if (Time > 4f)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, 74, -Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(0.3f, 1f);
            }


            NPC target = Projectile.Center.ClosestNPCAt(1000);

            if (target != null)
            {
                Phase2 = true;
                HomingTime = 1;
            }

            if (HomingTime == 1)
                ShtunUtils.HomeInOnNPC(Projectile, true, 250f, 12, 60f);

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

        public override void OnKill(int timeLeft)
        {
            if(Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(this.Projectile.GetSource_FromThis(), this.Projectile.position, Vector2.Zero, ModContent.ProjectileType<ShtuxiumBlast>(), this.Projectile.damage, 0);
        }
    }
}
