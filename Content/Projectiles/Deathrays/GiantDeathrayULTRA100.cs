using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;

namespace ssm.Content.Projectiles.Deathrays
{
    public class GiantDeathrayULTRA100 : Deathrays.MutantSpecialDeathray
    {
        public GiantDeathrayULTRA100() : base(180) { }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 50000;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 0;
            Projectile.netImportant = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
            //	Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToMutantBomb = true;
            //	Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToGuttedHeart = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
            //	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().TimeFreezeImmune = true;
            //	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().ImmuneToMutantBomb = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().noInteractionWithNPCImmunityFrames = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
            CooldownSlot = -1;
        }

        public override void AI()
        {
            base.AI();
            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            Projectile.Center = Main.player[Projectile.owner].Center;

            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }

            float num801 = 20f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * 3f * num801;
            if (Projectile.scale > num801)
                Projectile.scale = num801;
            float num804 = Projectile.velocity.ToRotation();
            float oldRot = Projectile.rotation;
            Projectile.rotation = num804 - 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = (float)Projectile.width * 10f;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 7000f;
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }

    }
}