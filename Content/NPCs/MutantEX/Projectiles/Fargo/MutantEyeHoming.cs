using System;
using System.Linq;
using FargowiltasSouls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantEyeHoming : MutantEye
{
    public override string Texture => "Terraria/Images/Projectile_452";
    public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.timeLeft = 900;
	}

    public override void AI()
    {
        const int endHomingTime = -600;

        float maxSpeed = 15f;

        bool stopAttacking = false;

        NPC npc = FargoSoulsUtil.NPCExists(ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>());
        int[] spearSpinAIs = new int[] { 4, 5, 6, 13, 14, 15, 21, 22, 23 };
        if ((npc == null || !spearSpinAIs.Contains((int)npc.ai[0]))
            && !(WorldSavingSystem.MasochistModeReal && npc.ai[0] > 10)
            && !Main.getGoodWorld)
        {
            Projectile.ai[1] = endHomingTime;
            stopAttacking = true;
        }

        Projectile.ai[1]--;

        Player p = FargoSoulsUtil.PlayerExists(npc == null ? Projectile.ai[0] : npc.target);
        if (stopAttacking || Projectile.ai[1] > 0 && p != null && Projectile.Distance(p.Center) < 240)
        {
            if (p != null)
            {
                double angle = Projectile.DirectionFrom(p.Center).ToRotation() - Projectile.velocity.ToRotation();
                if (angle > Math.PI)
                    angle -= 2.0 * Math.PI;
                if (angle < -Math.PI)
                    angle += 2.0 * Math.PI;

                Projectile.velocity = Projectile.velocity.RotatedBy(angle * 0.05);
            }

            if (Projectile.timeLeft > 180)
                Projectile.timeLeft = 180;
        }
        else if (Projectile.ai[1] < 0 && Projectile.ai[1] > endHomingTime)
        {
            if (p != null)
            {
                float homingMaxSpeed = maxSpeed;
                if (npc != null && (npc.ai[0] == 21 || npc.ai[0] == 22 || npc.ai[0] == 23))
                    homingMaxSpeed *= 2f;
                if (Projectile.velocity.Length() < homingMaxSpeed)
                    Projectile.velocity *= 1.02f;

                Vector2 target = p.Center;
                float deactivateHomingRange = 360;
                if (Projectile.Distance(target) > deactivateHomingRange)
                {
                    Vector2 distance = target - Projectile.Center;

                    double angle = distance.ToRotation() - Projectile.velocity.ToRotation();
                    if (angle > Math.PI)
                        angle -= 2.0 * Math.PI;
                    if (angle < -Math.PI)
                        angle += 2.0 * Math.PI;

                    Projectile.velocity = Projectile.velocity.RotatedBy(angle * 0.1);
                }
                else
                {
                    Projectile.ai[1] = endHomingTime;
                }
            }
        }

        if (Projectile.ai[1] < endHomingTime && !Main.getGoodWorld)
        {
            if (Projectile.velocity.Length() > maxSpeed)
                Projectile.velocity *= 0.96f;
        }

        base.AI();
    }
}
