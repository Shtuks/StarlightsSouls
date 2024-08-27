using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Content.Projectiles;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class DeviAxe : ModProjectile
    {
        public override string Texture => "ssm/Content/Projectiles/Empty";

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
            CooldownSlot = 1;
        }

        public override void AI()
        {
            //the important part
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[0], ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>());
            if (npc != null)
            {
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    Projectile.localAI[1] = Projectile.DirectionFrom(npc.Center).ToRotation();
                }

                Vector2 offset = new Vector2(Projectile.ai[1], 0).RotatedBy(npc.ai[3] + Projectile.localAI[1]);
                Projectile.Center = npc.Center + offset;
            }
            else
            {
                Projectile.Kill();
                return;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath6, Projectile.Center);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.X = target.Center.X < Main.npc[(int)Projectile.ai[0]].Center.X ? -15f : 15f;
            target.velocity.Y = -10f;
        }
    }
}