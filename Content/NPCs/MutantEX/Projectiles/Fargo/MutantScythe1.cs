using System;
using System.Collections.Generic;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantScythe1 : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/MutantBoss/MutantScythe1";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.alpha = 0;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = -1;
		base.CooldownSlot = 1;
		base.Projectile.hide = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt(base.Projectile.timeLeft);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.timeLeft = reader.Read7BitEncodedInt();
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		targetHitbox.Y = targetHitbox.Center.Y;
		targetHitbox.Height = Math.Min(targetHitbox.Width, targetHitbox.Height);
		targetHitbox.Y -= targetHitbox.Height / 2;
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void AI()
	{
		if (base.Projectile.rotation == 0f)
		{
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		}
		float modifier = (180f - (float)base.Projectile.timeLeft + 90f) / 180f;
		if (modifier < 0f)
		{
			modifier = 0f;
		}
		if (modifier > 1f)
		{
			modifier = 1f;
		}
		base.Projectile.rotation += 0.1f + 0.7f * modifier;
		if (base.Projectile.timeLeft < 180)
		{
			if (base.Projectile.velocity == Vector2.Zero)
			{
				base.Projectile.velocity = base.Projectile.ai[1].ToRotationVector2();
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity *= 1f + base.Projectile.ai[0];
		}
	}

	public override void PostAI()
	{
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
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = base.Projectile.GetAlpha(color26);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = color26;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(30, 600);
	}
}
