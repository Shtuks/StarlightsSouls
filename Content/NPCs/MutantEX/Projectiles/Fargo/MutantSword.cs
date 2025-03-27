using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantSword : ModProjectile
{
	public override string Texture => "Terraria/Images/Projectile_454";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 64;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 80;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(base.Projectile.ai[0], ModContent.NPCType<MutantEX>());
		if (npc != null)
		{
			if (base.Projectile.localAI[0] == 0f)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.localAI[1] = base.Projectile.DirectionFrom(npc.Center).ToRotation();
			}
			Vector2 offset = new Vector2(base.Projectile.ai[1], 0f).RotatedBy(npc.ai[3] + base.Projectile.localAI[1]);
			base.Projectile.Center = npc.Center + offset;
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha -= 10;
				if (base.Projectile.alpha < 0)
				{
					base.Projectile.alpha = 0;
				}
			}
			base.Projectile.scale = base.Projectile.Opacity;
			if (++base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frameCounter = 0;
				if (++base.Projectile.frame > 1)
				{
					base.Projectile.frame = 0;
				}
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.velocity.X = ((target.Center.X < Main.npc[(int)base.Projectile.ai[0]].Center.X) ? (-15f) : 15f);
		target.velocity.Y = -10f;
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
	}

	public override void Kill(int timeleft)
	{
		SoundEngine.PlaySound(SoundID.NPCDeath6, (Vector2?)base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 208);
		base.Projectile.Center = base.Projectile.position;
		for (int index1 = 0; index1 < 2; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
		}
		for (int index1 = 0; index1 < 5; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 1f;
			int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[index3].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
			Main.dust[index3].velocity *= 1f;
			Main.dust[index3].noGravity = true;
		}
		for (int i = 0; i < 5; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dust].velocity *= 1.4f;
		}
		for (int i = 0; i < 5; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 7f;
			dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dust].velocity *= 3f;
		}
		for (int index1 = 0; index1 < 10; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 2f);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].velocity *= 21f * base.Projectile.scale;
			Main.dust[index2].noLight = true;
			int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100);
			Main.dust[index3].velocity *= 12f;
			Main.dust[index3].noGravity = true;
			Main.dust[index3].noLight = true;
		}
		for (int i = 0; i < 10; i++)
		{
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), Main.rand.NextFloat(2f, 3.5f));
			if (Main.rand.NextBool(3))
			{
				Main.dust[d].noGravity = true;
			}
			Main.dust[d].velocity *= Main.rand.NextFloat(9f, 12f);
			Main.dust[d].position = base.Projectile.Center;
		}
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MutantBombSmall>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D glow = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		int rect1 = glow.Height;
		int rect2 = 0;
		Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
		Vector2 gloworigin2 = glowrectangle.Size() / 2f;
		Color glowcolor = Color.Lerp(new Color(255, 255, 255, 0), Color.Transparent, 0.85f);
		for (float i = 0f; i < (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 0.5f)
		{
			Color color27 = glowcolor * 0.5f;
			color27 *= ((float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			float scale = base.Projectile.scale * ((float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[(int)i];
			Main.EntitySpriteDraw(glow, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)glowrectangle, color27, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.5f, SpriteEffects.None, 0);
		}
		glowcolor = Color.Lerp(new Color(196, 247, 255, 0), Color.Transparent, 0.8f);
		Main.EntitySpriteDraw(glow, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)glowrectangle, glowcolor, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, base.Projectile.scale * 1.5f, SpriteEffects.None, 0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
	}
}
