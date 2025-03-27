using FargowiltasSouls.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public abstract class MutantSpearAttack : ModProjectile
    {
        protected NPC npc;

        public override bool? CanDamage()
        {
            base.Projectile.maxPenetrate = 1;
            return null;
        }

        protected void TryLifeSteal(Vector2 pos, int playerWhoAmI)
        {
            if (this.npc == null)
            {
                return;
            }
            int totalHealPerHit = this.npc.lifeMax / 100 * 5;
            for (int i = 0; i < 20; i++)
            {
                Vector2 vel = Main.rand.NextFloat(2f, 9f) * -Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                float ai0 = this.npc.whoAmI;
                float ai1 = vel.Length() / (float)Main.rand.Next(30, 90);
                int healPerOrb = (int)((float)(totalHealPerHit / 20) * Main.rand.NextFloat(0.95f, 1.05f));
                if (playerWhoAmI == Main.myPlayer && Main.player[playerWhoAmI].ownedProjectileCounts[ModContent.ProjectileType<MutantHeal>()] < 10)
                {
                    Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), pos, vel, ModContent.ProjectileType<MutantHeal>(), healPerOrb, 0f, Main.myPlayer, ai0, ai1);
                    SoundEngine.PlaySound(in SoundID.Item27, pos);
                }
            }
        }
    }

}
