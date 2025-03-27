using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantSansHead : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_246B";

        public override void SetStaticDefaults()
        {
            Main.projFrames[base.Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 70;
            base.Projectile.height = 70;
            base.Projectile.penetrate = -1;
            base.Projectile.hostile = true;
            base.Projectile.tileCollide = false;
            base.Projectile.ignoreWater = true;
            base.Projectile.aiStyle = -1;
            base.CooldownSlot = 1;
            base.Projectile.timeLeft = 420;
            base.Projectile.hide = true;
            base.Projectile.FargoSouls().DeletionImmuneRank = 1;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.hurtCooldowns[1] == 0;
        }

        public override bool? CanDamage()
        {
            return base.Projectile.ai[0] < 0f;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
        }

        public override void AI()
        {
            if (base.Projectile.localAI[0] == 0f)
            {
                base.Projectile.localAI[0] = 1f;
                base.Projectile.rotation = ((base.Projectile.ai[2] == 1f) ? 0f : ((float)Math.PI));
            }
            if ((base.Projectile.ai[0] -= 1f) == 0f)
            {
                base.Projectile.velocity = Vector2.Zero;
                base.Projectile.netUpdate = true;
                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitY * base.Projectile.ai[2], ModContent.ProjectileType<MutantSansBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.identity);
                }
            }
            else if (base.Projectile.ai[0] < -170f)
            {
                base.Projectile.velocity.X *= 1.025f;
            }
            else if (base.Projectile.ai[0] < -50f)
            {
                base.Projectile.velocity.X = base.Projectile.ai[1];
                base.Projectile.velocity.Y = 0f;
            }
            base.Projectile.frame = 1;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (WorldSavingSystem.EternityMode)
            {
                target.FargoSouls().MaxLifeReduction += 100;
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            }
            target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
            int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / 6;
            int y3 = num156 * base.Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color26 = lightColor;
            color26 = base.Projectile.GetAlpha(color26);
            SpriteEffects effects = SpriteEffects.None;
            Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, effects);
            Texture2D eyes = TextureAssets.Golem[1].Value;
            Rectangle eyeRectangle = new Rectangle(0, eyes.Height / 2, eyes.Width, eyes.Height / 2);
            Vector2 eyeOrigin = eyeRectangle.Size() / 2f;
            eyeOrigin.Y -= 4f;
            Main.EntitySpriteDraw(eyes, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), eyeRectangle, Color.White * base.Projectile.Opacity, base.Projectile.rotation, eyeOrigin, base.Projectile.scale, effects);
            return false;
        }
    }
}