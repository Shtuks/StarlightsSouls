using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ssm.gunrightsmod.Projectiles
{
    public class CeramicShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ceramic Shard"); 
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10; 
            Projectile.aiStyle = 1; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; 
            Projectile.timeLeft = 600;
            Projectile.light = 0.5f; 
            Projectile.ignoreWater = true; 
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ClayBlock, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.BrokenArmor, 300);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ClayBlock, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }
    }
}
