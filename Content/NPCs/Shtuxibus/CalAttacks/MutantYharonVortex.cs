using CalamityMod;
using CalamityMod.Projectiles.Boss;
using ssm.Core;
using ssm.Content.NPCs.Shtuxibus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus.CalAttacks
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class MutantYharonVortex : YharonBulletHellVortex
    {
        public const int ThrowTime = 120;
        public override void AI()
        {
            NPC shtuxibus = Main.npc[(int)(Projectile.ai[1])];
            if (shtuxibus.active && shtuxibus.type == ModContent.NPCType<Shtuxibus>())
            {
                if (TimeCountdown > 0f)
                {

                    if (Projectile.scale < 1f)
                    {
                        Projectile.scale = MathHelper.Clamp(Projectile.scale + 0.05f, 0f, 1f);
                    }

                    Projectile.velocity = (shtuxibus.Center - Projectile.Center);
                }
                else
                {
                    if (TimeCountdown == 0f)
                    {
                        if (shtuxibus.target.WithinBounds(Main.maxPlayers))
                        {
                            Player player = Main.player[shtuxibus.target];
                            if (player.active && !player.dead)
                            {
                                Projectile.velocity = Projectile.DirectionTo(player.Center) * 10f;
                            }
                        }
                    }
                    if (TimeCountdown <= -(ThrowTime - 20))
                    {
                        Projectile.scale = MathHelper.Clamp(Projectile.scale - 0.05f, 0f, 1f);
                    }
                    if (TimeCountdown < -ThrowTime)
                    {
                        Projectile.Kill();
                    }
                }
                TimeCountdown -= 1f;
            }
            else
            {
                Projectile.Kill();
            }
        }
    }
}
