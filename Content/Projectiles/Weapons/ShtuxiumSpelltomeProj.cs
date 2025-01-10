using Terraria.ModLoader;
using Terraria;
using ssm.Content.Buffs;
using Microsoft.Xna.Framework;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxiumSpelltomeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 5;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            Projectile.localAI[1]++;

            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 1;
            }
            if (Projectile.frame > 4)
            {
                Projectile.frame = 0;
            }

            if (Projectile.localAI[1] > 20)
            {
                //home in after 1 second

                ShtunUtils.HomeInOnNPC(Projectile, true, 5000f, 15f, 10f);
            }
            //else if(Projectile.localAI[1] > 20 &&)
            //{
            //    Projectile.velocity = Projectile.velocity * 0;
            //}
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
