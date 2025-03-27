using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantRitual5 : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_454";

        private const float PI = (float)Math.PI;
        private const float rotationPerTick = PI / 57f;
        private const float threshold = 350;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 2;
            Terraria.ID.ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 2400;
        }

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.scale *= 2f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            Projectile.alpha = 255;
            Projectile.FargoSouls().TimeFreezeImmune = true;
        }

        public override void AI()
        {
            NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[1], ModContent.NPCType<MutantEX>());
            if (npc != null)
            {
                Projectile.alpha -= 4;
                if (Projectile.alpha < 0)
                    Projectile.alpha = 0;
                Projectile.Center = npc.Center;
            }
            else
            {
                Projectile.ai[1] = -1;
                Projectile.velocity = Vector2.Zero;
                Projectile.alpha += 2;
                if (Projectile.alpha > 255)
                {
                    Projectile.Kill();
                    return;
                }
            }

            Projectile.timeLeft = 2;
            Projectile.scale = (1f - Projectile.alpha / 255f) * 0.5f;
            Projectile.ai[0] += rotationPerTick;
            if (Projectile.ai[0] > PI)
            {
                Projectile.ai[0] -= 2f * PI;
                Projectile.netUpdate = true;
            }

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
            int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
            int y3 = num156 * base.Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color26 = base.Projectile.GetAlpha(lightColor);
            Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            int rect1 = glow.Height;
            int rect2 = 0;
            Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
            Vector2 gloworigin2 = glowrectangle.Size() / 2f;
            Color glowcolor = Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color(196, 247, 255, 0), Color.Transparent, 0.8f);
            for (int x = 0; x < 7; x++)
            {
                Vector2 drawOffset = new Vector2(350f * base.Projectile.scale / 2f, 0f).RotatedBy(base.Projectile.ai[0]);
                drawOffset = drawOffset.RotatedBy(0.8975979f * (float)x);
                for (int i = 0; i < 4; i++)
                {
                    Color color27 = color26;
                    color27 *= (float)(4 - i) / 4f;
                    Vector2 value4 = base.Projectile.Center + drawOffset.RotatedBy((float)Math.PI / 57f * (float)(-i));
                    float num165 = base.Projectile.rotation;
                    Main.EntitySpriteDraw(texture2D13, value4 - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None);
                    Main.EntitySpriteDraw(glow, value4 - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, glowcolor * ((float)(4 - i) / 4f), base.Projectile.rotation, gloworigin2, base.Projectile.scale * 1.4f, SpriteEffects.None);
                }
                Main.EntitySpriteDraw(texture2D13, base.Projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
                Main.EntitySpriteDraw(glow, base.Projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, glowcolor, base.Projectile.rotation, gloworigin2, base.Projectile.scale * 1.3f, SpriteEffects.None);
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * base.Projectile.Opacity * 0.3f;
        }
    }
}