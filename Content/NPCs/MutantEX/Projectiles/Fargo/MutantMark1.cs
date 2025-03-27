using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;
public class MutantMark1 : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_454";

    public override void SetStaticDefaults()
    {
        Main.projFrames[base.Projectile.type] = 2;
        ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
        ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
        base.Projectile.width = 46;
        base.Projectile.height = 46;
        base.Projectile.ignoreWater = true;
        base.Projectile.tileCollide = false;
        base.Projectile.hostile = true;
        base.Projectile.timeLeft = 90;
        base.Projectile.aiStyle = -1;
        base.Projectile.scale = 0.5f;
        base.Projectile.alpha = 0;
        base.Projectile.penetrate = -1;
        base.CooldownSlot = 1;
        base.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public override bool CanHitPlayer(Player target)
    {
        return target.hurtCooldowns[1] == 0;
    }

    public override void AI()
    {
        if (base.Projectile.localAI[0] == 0f)
        {
            base.Projectile.localAI[0] = 1f;
            if (FargoSoulsUtil.HostCheck)
            {
                Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center + base.Projectile.velocity * base.Projectile.timeLeft, Vector2.Normalize(base.Projectile.velocity), ModContent.ProjectileType<MutantDeathraySmall>(), base.Projectile.damage, 0f, base.Projectile.owner);
            }
        }
        if (++base.Projectile.frameCounter >= 6)
        {
            base.Projectile.frameCounter = 0;
            if (++base.Projectile.frame > 1)
            {
                base.Projectile.frame = 0;
            }
        }
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        if (WorldSavingSystem.EternityMode)
        {
            target.FargoSouls().MaxLifeReduction += 100;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
            target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
        }
        target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
    }

    public override void OnKill(int timeleft)
    {
        SoundEngine.PlaySound(in SoundID.NPCDeath6, base.Projectile.Center);
        base.Projectile.position = base.Projectile.Center;
        base.Projectile.width = (base.Projectile.height = 208);
        base.Projectile.Center = base.Projectile.position;
        if (FargoSoulsUtil.HostCheck)
        {
            Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Normalize(base.Projectile.velocity), ModContent.ProjectileType<MutantDeathray1>(), base.Projectile.damage, 0f, base.Projectile.owner);
        }
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White * base.Projectile.Opacity;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        int rect1 = glow.Height;
        int rect2 = 0;
        Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
        Vector2 gloworigin2 = glowrectangle.Size() / 2f;

        //maso telegraph
        Color glowcolor = Color.Lerp(new Color(255, 255, 255, 0), Color.Transparent, 0.85f);
        //Asset<Texture2D> line = TextureAssets.Extra[178];
        //float opacity = 1f;
        //Main.EntitySpriteDraw(line.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), null, Color.Cyan * opacity, base.Projectile.velocity.ToRotation(), new Vector2(0f, (float)line.Height() * 0.5f), new Vector2(0.3f, base.Projectile.scale * 7f), SpriteEffects.None);

        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
        {
            Color color27 = glowcolor;
            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            float scale = base.Projectile.scale * (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            Vector2 value4 = base.Projectile.oldPos[i];
            Main.EntitySpriteDraw(glow, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, color27, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.5f, SpriteEffects.None);
        }

        Main.EntitySpriteDraw(glow, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, new Color(255, 255, 255, 200), base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, base.Projectile.scale * 1.5f, SpriteEffects.None);
        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
        int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
        int y3 = num156 * base.Projectile.frame;
        Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;
        Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
    }
}
