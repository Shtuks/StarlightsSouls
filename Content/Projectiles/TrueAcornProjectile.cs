using ssm.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles
{
    public class TrueAcornProjectile : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Acorn; 
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1000;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
            Projectile.aiStyle = -1;
            Projectile.damage = 8000000;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override void AI()
        {
            ShtunUtils.HomeInOnNPC(Projectile, true, 100, 100, 1);
            Projectile.rotation += 0.2f;
        }
    }
}
