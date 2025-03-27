using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantSlimeBall : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/SlimeBall";
    public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hostile = true;
		base.CooldownSlot = 1;
		base.Projectile.scale = 1.5f;
		base.Projectile.alpha = 50;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 90 * (base.Projectile.extraUpdates + 1);
	}

	public override void AI()
	{
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.timeLeft % base.Projectile.MaxUpdates == 0 && ++base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= Main.projFrames[base.Projectile.type])
			{
				base.Projectile.frame = 0;
			}
		}
		if ((base.Projectile.localAI[1] += 1f) > 10f && FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && Math.Sign(base.Projectile.Center.Y - Main.npc[ShtunNpcs.mutantEX].Center.Y) == Math.Sign(base.Projectile.velocity.Y) && base.Projectile.Distance(Main.npc[ShtunNpcs.mutantEX].Center) > 1200f + base.Projectile.ai[0])
		{
			base.Projectile.timeLeft = 0;
		}
	}

	public override void Kill(int timeleft)
	{
		for (int i = 0; i < 20; i++)
		{
			int num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 59, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
			Main.dust[num469].noGravity = true;
			Main.dust[num469].velocity *= 2f;
			num469 = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 59, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100);
			Main.dust[num469].velocity *= 2f;
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(137, 240);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		int sheetClamped = (int)base.Projectile.localAI[0];
		if (sheetClamped < 1)
		{
			sheetClamped = 1;
		}
		if (sheetClamped > 3)
		{
			sheetClamped = 3;
		}
		Texture2D texture2D13 = ModContent.Request<Texture2D>($"ssm/Assets/ExtraTextures/MutantEX/MutantSlimeBall_{sheetClamped}", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		int num156 = texture2D13.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = lightColor;
		color26 = base.Projectile.GetAlpha(color26);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = color26 * 0.5f;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
