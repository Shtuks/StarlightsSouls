using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantEyeWavy : MutantEye
    {
        public override int TrailAdditive => 150;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.timeLeft = 180;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
            CooldownSlot = 0;
        }

        private float Amplitude => Projectile.ai[0];
        private float Period => Projectile.ai[1];
        private float Counter => Projectile.localAI[1] * 4;

        public float oldRot;
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }
        public override void AI()
        {
            NPC mutant = ShtunUtils.NPCExists(ShtunNpcs.Shtuxibus);
            if (mutant != null && (mutant.ai[0] == -5f || mutant.ai[0] == -7f))
            {
                float targetRotation = mutant.ai[3];
                float speed = Projectile.velocity.Length();
                float rotation = targetRotation + (float)Math.PI / 4 * (float)Math.Sin(2 * (float)Math.PI * Counter / Period) * Amplitude;
                Projectile.velocity = speed * rotation.ToRotationVector2();
                if (oldRot != 0)
                {
                    Vector2 oldCenter = Projectile.Center;
                    Projectile.Center = mutant.Center + (Projectile.Center - mutant.Center).RotatedBy(targetRotation - oldRot);
                    Vector2 diff = Projectile.Center - oldCenter;
                    for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                    {
                        Projectile.oldPos[i] += diff;
                    }
                }
                oldRot = targetRotation;
            }
            else
            {
                Projectile.Kill();
                return;
            }
            Projectile.localAI[0] += 0.1f;
            base.AI();
        }

        public override void OnKill(int timeleft)
        {
        }
    }
}