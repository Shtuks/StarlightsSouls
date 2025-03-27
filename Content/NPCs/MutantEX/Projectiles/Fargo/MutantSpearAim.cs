using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Core.Systems;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo;

public class MutantSpearAim : ModProjectile
{
    public ref float MaxTime => ref base.Projectile.localAI[2];
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.timeLeft = 60;
		base.CooldownSlot = 1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (projHitbox.Intersects(targetHitbox))
		{
			return true;
		}
		float dummy = 0f;
		Vector2 offset = 200f / 2f * base.Projectile.scale * (base.Projectile.rotation - MathHelper.ToRadians(135f)).ToRotationVector2();
		Vector2 end = base.Projectile.Center - offset;
		Vector2 tip = base.Projectile.Center + offset;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), end, tip, 8f * base.Projectile.scale, ref dummy))
		{
			return true;
		}
		return false;
	}

	public override void AI()
	{
		NPC mutant = Main.npc[(int)base.Projectile.ai[0]];
		if (mutant.active && mutant.type == ModContent.NPCType<MutantEX>())
		{
			base.Projectile.Center = mutant.Center;
			if (base.Projectile.localAI[0] == 0f)
			{
				base.Projectile.rotation = mutant.DirectionTo(Main.player[mutant.target].Center).ToRotation();
			}
			if (base.Projectile.ai[1] > 1f)
			{
				if (base.Projectile.ai[1] != 4f || !((float)base.Projectile.timeLeft < Math.Abs(base.Projectile.localAI[1]) + 5f))
				{
					base.Projectile.rotation = base.Projectile.rotation.AngleLerp(mutant.DirectionTo(Main.player[mutant.target].Center + Main.player[mutant.target].velocity * 30f).ToRotation(), 0.2f);
				}
			}
			else
			{
				base.Projectile.rotation = mutant.DirectionTo(Main.player[mutant.target].Center).ToRotation();
			}
		}
		else
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			if (base.Projectile.ai[1] == -1f)
			{
				base.Projectile.timeLeft += 120;
				base.Projectile.localAI[1] = -120f;
			}
			else if (base.Projectile.ai[1] == 1f)
			{
				base.Projectile.timeLeft -= 30;
				base.Projectile.localAI[1] = 30f;
			}
			else if (base.Projectile.ai[1] == 3f)
			{
				base.Projectile.timeLeft += 30;
				base.Projectile.localAI[1] = -30f;
			}
			else if (base.Projectile.ai[1] == 4f)
			{
				base.Projectile.timeLeft += 20;
				base.Projectile.localAI[1] = -20f;
			}
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), target.Center + Main.rand.NextVector2Circular(100f, 100f), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0f, base.Projectile.owner, 0f, 0f);
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
        int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
        int y3 = num156 * base.Projectile.frame;
        Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
        Vector2 origin = rectangle.Size() / 2f;
        Color color26 = lightColor;
        color26 = base.Projectile.GetAlpha(color26);
        float rotationOffset = MathHelper.ToRadians(135f);
        int direction = Utilities.NonZeroSign(base.Projectile.rotation.ToRotationVector2().X);
        float timeFraction = (float)base.Projectile.timeLeft / this.MaxTime;
        float maxRearback = 0.5497787f;
        Vector2 maxOffset = base.Projectile.rotation.ToRotationVector2() * 30f;
        Vector2 positionOffset = Vector2.Zero;
        float windupFraction = 0.5f;
        if (!WorldSavingSystem.MasochistModeReal && base.Projectile.ai[1] <= 1f)
        {
            float rotationAnimation;
            float extensionFraction;
            if (timeFraction > windupFraction)
            {
                float rearbackFraction = (timeFraction - windupFraction) / (1f - windupFraction);
                rotationAnimation = MathHelper.SmoothStep(0f - maxRearback, (0f - maxRearback) * 0.8f, rearbackFraction);
                positionOffset = Vector2.SmoothStep(-maxOffset, -maxOffset * 0.8f, rearbackFraction);
                extensionFraction = 0f;
            }
            else
            {
                float tossFraction = timeFraction / windupFraction;
                tossFraction = MathF.Pow(tossFraction, 0.5f);
                rotationAnimation = MathHelper.Lerp(0f, 0f - maxRearback, tossFraction);
                positionOffset = Vector2.Lerp(Vector2.Zero, -maxOffset, tossFraction);
                extensionFraction = MathHelper.Lerp(1f, 0f, tossFraction);
            }
            rotationOffset += (float)direction * rotationAnimation;
            Vector2 extendMax = (base.Projectile.rotation + (float)direction * rotationAnimation).ToRotationVector2() * origin.Length() * 0.3f;
            Vector2 extensionOffset = Vector2.Lerp(-extendMax, extendMax * 0.5f, extensionFraction);
            positionOffset += extensionOffset;
        }
        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
        {
            Color color27 = color26;
            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            Vector2 value4 = base.Projectile.oldPos[i];
            float num165 = base.Projectile.oldRot[i] + rotationOffset;
            Main.EntitySpriteDraw(texture2D13, value4 + positionOffset + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color27, num165, origin, base.Projectile.scale, SpriteEffects.None);
        }
        Main.EntitySpriteDraw(texture2D13, base.Projectile.Center + positionOffset - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + rotationOffset, origin, base.Projectile.scale, SpriteEffects.None);
        if (base.Projectile.ai[1] != 5f)
        {
            Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpearAimGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            float modifier = (float)base.Projectile.timeLeft / (60f - base.Projectile.localAI[1]);
            Color glowColor = (FargoSoulsUtil.AprilFools ? new Color(255, 191, 51, 210) : new Color(51, 255, 191, 210));
            if (base.Projectile.ai[1] > 1f)
            {
                glowColor = (FargoSoulsUtil.AprilFools ? new Color(255, 0, 0, 210) : new Color(0, 0, 255, 210));
            }
            glowColor *= 1f - modifier;
            float glowScale = base.Projectile.scale * 8f * modifier;
            Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glow.Bounds, glowColor, 0f, glow.Bounds.Size() / 2f, glowScale, SpriteEffects.None);
        }
        return false;
    }
}
