using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantNuke : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/MutantBoss/MutantNuke";

    public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 30;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.scale = 4f;
		base.Projectile.aiStyle = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
		base.CooldownSlot = 1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(SoundID.Item20, (Vector2?)base.Projectile.position);
		}
		if (!FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) || Main.npc[ShtunNpcs.mutantEX].dontTakeDamage)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.velocity.Y += base.Projectile.ai[0];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if ((base.Projectile.localAI[0] += 1f) >= 24f)
		{
			base.Projectile.localAI[0] = 0f;
			for (int index1 = 0; index1 < 36; index1++)
			{
				Vector2 vector2 = (Vector2.UnitX * -8f + -Vector2.UnitY.RotatedBy((double)index1 * 3.14159274101257 / 36.0 * 2.0) * new Vector2(2f, 4f)).RotatedBy((double)base.Projectile.rotation - 1.57079637050629);
				int index2 = Dust.NewDust(base.Projectile.Center, 0, 0, 135);
				Main.dust[index2].scale = 2f;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].position = base.Projectile.Center + vector2 * 6f;
				Main.dust[index2].velocity = base.Projectile.velocity * 0f;
			}
		}
		Vector2 vector21 = Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 8f * 2f;
		int index21 = Dust.NewDust(base.Projectile.Center, 0, 0, 6);
		Main.dust[index21].position = base.Projectile.Center + vector21;
		Main.dust[index21].scale = 1.5f;
		Main.dust[index21].noGravity = true;
	}

	public override void Kill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item84, (Vector2?)base.Projectile.Center);
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
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0f, base.Projectile.owner, 0f, 0f);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 5)
		{
			Color color27 = Color.Cyan;
			color27.A = 0;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
