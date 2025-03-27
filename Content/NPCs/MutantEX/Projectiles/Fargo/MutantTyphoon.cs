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
public class MutantTyphoon : ModProjectile
{
    public override string Texture =>"Terraria/Images/Projectile_409";
    public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 100;
		base.CooldownSlot = 1;
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void AI()
	{
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy((double)base.Projectile.ai[1] / (Math.PI * 2.0 * (double)base.Projectile.ai[0] * (double)(base.Projectile.localAI[0] += 1f)));
		int cap = Main.rand.Next(3);
		for (int index1 = 0; index1 < cap; index1++)
		{
			Vector2 vector2_1 = base.Projectile.velocity;
			vector2_1.Normalize();
			vector2_1.X *= base.Projectile.width;
			vector2_1.Y *= base.Projectile.height;
			vector2_1 /= 2f;
			vector2_1 = vector2_1.RotatedBy((double)(index1 - 2) * Math.PI / 6.0);
			vector2_1 += base.Projectile.Center;
			Vector2 vector2_2 = (Main.rand.NextFloat() * (float)Math.PI - (float)Math.PI / 2f).ToRotationVector2();
			vector2_2 *= (float)Main.rand.Next(3, 8);
			int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].velocity /= 4f;
			Main.dust[index2].velocity -= base.Projectile.velocity;
		}
		base.Projectile.rotation += 0.2f * ((base.Projectile.velocity.X > 0f) ? 1f : (-1f));
		base.Projectile.frame++;
		if (base.Projectile.frame > 2)
		{
			base.Projectile.frame = 0;
		}
	}

	public virtual void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		player.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		player.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		player.AddBuff(ModContent.BuffType<DefenselessBuff>(), Main.rand.Next(600, 900));
		player.AddBuff(196, Main.rand.Next(300, 600));
	}

	public override void Kill(int timeLeft)
	{
		int num1 = 36;
		for (int index1 = 0; index1 < num1; index1++)
		{
			Vector2 vector = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, base.Projectile.height) * 0.75f).RotatedBy((double)(index1 - (num1 / 2 - 1)) * 6.28318548202515 / (double)num1) + base.Projectile.Center;
			Vector2 vector2_2 = vector - base.Projectile.Center;
			int index2 = Dust.NewDust(vector + vector2_2, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].velocity = vector2_2;
		}
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
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 2)
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

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(100, 100, 250, 200);
	}
}
