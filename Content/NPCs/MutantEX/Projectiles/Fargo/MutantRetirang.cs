using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantRetirang : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/Retirang";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.scale = 1.5f;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = -1;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		if ((base.Projectile.localAI[0] += 1f) > base.Projectile.ai[1])
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = base.Projectile.velocity.Length();
		}
		Vector2 acceleration = Vector2.Normalize(base.Projectile.velocity).RotatedBy(Math.PI / 2.0) * base.Projectile.ai[0];
		base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity) * base.Projectile.localAI[1] + acceleration;
		base.Projectile.rotation += 1f * (float)Math.Sign(base.Projectile.ai[0]);
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(69, 120);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = base.Projectile.GetAlpha(color26);
		int add = 150;
		Color glowColor = new Color(add + Main.DiscoR / 3, add + Main.DiscoG / 3, add + Main.DiscoB / 3, 0);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 2)
		{
			Color color27 = glowColor * 0.9f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
