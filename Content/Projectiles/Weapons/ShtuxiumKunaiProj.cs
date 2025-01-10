using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using ssm.Content.Buffs;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumKunaiProj : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Throwing;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            ShtunUtils.HomeInOnNPC(Projectile, false, 500f, 15f, 15f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChtuxlagorInferno>(), 180);
        }

        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(this.Projectile.GetSource_FromThis(), this.Projectile.position, Vector2.Zero, ModContent.ProjectileType<ShtuxiumBlast3>(), this.Projectile.damage, 0);
        }
    }
}