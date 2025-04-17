using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework.Graphics;
using FargowiltasSouls.Content.Projectiles.JungleMimic;
using System.Linq;
using Luminance.Common.Utilities;

namespace ssm.Content.Projectiles
{
    public class SquirrelRing : ModProjectile
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Squirrel;

        private const float PI = (float)Math.PI;
        private const float rotationPerTick = PI / 57f;
        private const float threshold = 350;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.scale *= 3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.alpha = 255;
            Projectile.scale = 4f;
        }

        public override void AI()
        {
            if (Main.player[Projectile.owner].active && !Main.player[Projectile.owner].dead && Main.player[Projectile.owner].Shtun().lumberjackSet)
            {
                Projectile.alpha = 0;
            }
            else
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = Main.player[Projectile.owner].Center;

            Projectile.timeLeft = 2;
            Projectile.scale = (1f - Projectile.alpha / 255f) * 0.5f;
            Projectile.ai[0] += rotationPerTick;
            if (Projectile.ai[0] > PI)
            {
                Projectile.ai[0] -= 2f * PI;
                Projectile.netUpdate = true;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }

            Projectile.ai[1]++;

            if (Projectile.ai[1] >= 120f) 
            {
                NPC target = Main.npc.Where(n => n.active && !n.friendly && n.CanBeChasedBy()).OrderBy(n => Vector2.Distance(n.Center, Projectile.Center)).FirstOrDefault();
                if (target != null)
                { 
                    Vector2 direction = Vector2.Normalize(target.Center - Main.player[Projectile.owner].Center);
                    float speedAcorn = 20f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, direction * speedAcorn,
                        ModContent.ProjectileType<TrueAcornProjectile>(), 8000000, 2f, Projectile.owner);
                }
                Projectile.ai[1] = 0f;
            }
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = Projectile.GetAlpha(lightColor);

            for (int x = 0; x < 10; x++)
            {
                Vector2 drawOffset = new Vector2(threshold * Projectile.scale / 2f, 0f).RotatedBy(Projectile.ai[0]);
                drawOffset = drawOffset.RotatedBy(2f * Math.PI / 7f * x);
                Main.EntitySpriteDraw(texture2D13, Projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * Projectile.Opacity * .3f;
        }
    }
}
