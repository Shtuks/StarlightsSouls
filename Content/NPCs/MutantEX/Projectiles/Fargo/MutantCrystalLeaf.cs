using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantCrystalLeaf : ModProjectile
{
	public override string Texture => "FargowiltasSouls/Content/NPCs/EternityModeNPCs/CrystalLeaf";
    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.aiStyle = -1;
		base.Projectile.scale = 2.5f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		if ((base.Projectile.localAI[0] += 1f) == 0f)
		{
			for (int index1 = 0; index1 < 30; index1++)
			{
				int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 157, 0f, 0f, 0, default(Color), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 5f;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.4f, 0.2f);
		base.Projectile.scale = ((float)(int)Main.mouseTextColor / 200f - 0.35f) * 0.2f + 0.95f;
		base.Projectile.scale *= 2.5f;
		int byIdentity = FargoSoulsUtil.GetProjectileByIdentity(base.Projectile.owner, (int)base.Projectile.ai[0], ModContent.ProjectileType<MutantMark2>());
		if (byIdentity != -1)
		{
			Vector2 offset = new Vector2(100f, 0f).RotatedBy(base.Projectile.ai[1]);
			base.Projectile.Center = Main.projectile[byIdentity].Center + offset;
			base.Projectile.localAI[1] = Math.Max(0f, 150f - Main.projectile[byIdentity].ai[1]) / 150f;
			base.Projectile.ai[1] += 0.15f * base.Projectile.localAI[1];
			if (base.Projectile.localAI[1] > 1f)
			{
				base.Projectile.localAI[1] = 1f;
			}
		}
		base.Projectile.rotation = base.Projectile.ai[1] + (float)Math.PI / 2f;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(20, Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<InfestedBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), Main.rand.Next(60, 300));
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override Color? GetAlpha(Color drawColor)
	{
		float num4 = (float)(int)Main.mouseTextColor / 200f - 0.3f;
		int num5 = (int)(255f * num4) + 50;
		if (num5 > 255)
		{
			num5 = 255;
		}
		return new Color(num5, num5, num5, 200);
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
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = Color.White * base.Projectile.Opacity * base.Projectile.localAI[1];
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
