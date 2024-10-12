using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Content.Buffs;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class DeviHammerHeld : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_300";

        public override void SetStaticDefaults()
        {
        }

        const int maxTime = 60;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.scale = 1.5f;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = maxTime;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;

            Projectile.hide = true;
        }

        public override void AI()
        {
            Projectile.hide = false; //to avoid edge case tick 1 wackiness

            //the important part
            NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>());
            if (npc != null)
            {
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    Projectile.localAI[1] = Projectile.ai[1] / maxTime;
                }

                Projectile.velocity = Projectile.velocity.RotatedBy(Projectile.ai[1]);
                Projectile.ai[1] -= Projectile.localAI[1];
                Projectile.Center = npc.Center + new Vector2(20, 20).RotatedBy(Projectile.velocity.ToRotation() - MathHelper.PiOver4) * Projectile.scale;
            }
            else
            {
                Projectile.Kill();
                return;
            }

            Projectile.direction = Projectile.spriteDirection = Math.Sign(Projectile.ai[1]);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(Projectile.direction < 0 ? 135 : 45);
        }

        public override void OnKill(int timeLeft)
        {
            for (int index1 = 0; index1 < 10; ++index1)
            {
                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 246, 0f, 0f, 100, new Color(), 1.5f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 3.5f;
                Main.dust[index2].noLight = true;
                int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 246, 0f, 0f, 100, new Color(), 1f);
                Main.dust[index3].velocity *= 2f;
                Main.dust[index3].noGravity = true;
                Main.dust[index3].noLight = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            SpriteEffects effects = Projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }
    }
}