using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantShadowHand : MutantFishron
    {
        public override string Texture => "Terraria/Images/Projectile_965";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[base.Projectile.type] = Main.projFrames[965];
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            base.Projectile.width = (base.Projectile.height = 50);
            base.Projectile.alpha = 0;
            base.Projectile.scale = 1.5f;
        }

        public override bool PreAI()
        {
            return true;
        }

        public override void AI()
        {
            if (base.Projectile.localAI[1] == 0f)
            {
                base.Projectile.localAI[1] = 1f;
                SoundEngine.PlaySound(in SoundID.DD2_GhastlyGlaiveImpactGhost, base.Projectile.Center);
                base.p = (Utilities.AnyBosses() ? Main.npc[FargoSoulsGlobalNPC.boss].target : Player.FindClosest(base.Projectile.Center, 0, 0));
                base.Projectile.netUpdate = true;
                base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
            }
            if ((base.Projectile.localAI[0] += 1f) > 85f)
            {
                base.Projectile.rotation = base.Projectile.velocity.ToRotation();
                base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
                return;
            }
            int ai0 = base.p;
            if (base.Projectile.localAI[0] == 85f)
            {
                base.Projectile.velocity = Main.player[ai0].Center - base.Projectile.Center;
                base.Projectile.velocity.Normalize();
                base.Projectile.velocity *= ((base.Projectile.type == ModContent.ProjectileType<MutantFishron>()) ? 24f : 20f);
                base.Projectile.rotation = base.Projectile.velocity.ToRotation();
                base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
                return;
            }
            Vector2 vel = Main.player[ai0].Center - base.Projectile.Center;
            base.Projectile.rotation += base.Projectile.velocity.Length() / 20f;
            base.Projectile.rotation = base.Projectile.rotation.AngleLerp((Main.player[ai0].Center - base.Projectile.Center).ToRotation(), base.Projectile.localAI[0] / 85f * 0.08f);
            if (vel.X > 0f)
            {
                vel.X -= 300f;
                base.Projectile.direction = (base.Projectile.spriteDirection = 1);
            }
            else
            {
                vel.X += 300f;
                base.Projectile.direction = (base.Projectile.spriteDirection = -1);
            }
            Vector2 distance = (Main.player[ai0].Center + new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]) - base.Projectile.Center) / 4f;
            base.Projectile.velocity = (base.Projectile.velocity * 19f + distance) / 20f;
            base.Projectile.position += Main.player[ai0].velocity / 2f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (WorldSavingSystem.EternityMode)
            {
                target.FargoSouls().MaxLifeReduction += 100;
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            }
            target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 900);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
            int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
            int y3 = num156 * base.Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color26 = lightColor;
            color26 = Color.Black * base.Projectile.Opacity;
            SpriteEffects effects = ((base.Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            float drawRotation = 0f;
            if (base.Projectile.spriteDirection < 0)
            {
                drawRotation += (float)Math.PI;
            }
            float opacityMod = ((base.Projectile.localAI[0] > 85f) ? 0.6f : 0.3f);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
            {
                Color color27 = Color.LightBlue * base.Projectile.Opacity * opacityMod;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
                Vector2 value4 = base.Projectile.oldPos[i];
                float num165 = base.Projectile.oldRot[i] + drawRotation;
                Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color27, num165, origin2, base.Projectile.scale * 1.2f, effects);
            }
            Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color26, base.Projectile.rotation + drawRotation, origin2, base.Projectile.scale, effects);
            return false;
        }
    }

}
