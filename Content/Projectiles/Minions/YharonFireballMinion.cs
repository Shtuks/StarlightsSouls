using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using ssm.Core;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Minions
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class YharonFireballMinion : ModProjectile
    {
        public static readonly SoundStyle FlareSound = new("CalamityMod/Sounds/Custom/Yharon/YharonInfernado");

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            int infernadoDamage = 1000;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<InfernadoMarkFriendly>(), infernadoDamage, Projectile.knockBack, Projectile.owner);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Dragonfire>(), 120);
        }
    }
}
