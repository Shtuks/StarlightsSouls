using System;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantFishron : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_370";

    public int p = -1;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 8;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 11;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.aiStyle = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.alpha = 100;
		base.CooldownSlot = 1;
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(this.p);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		this.p = reader.ReadInt32();
	}

	public override bool? CanDamage()
	{
		return base.Projectile.localAI[0] > 85f;
	}

	public override bool PreAI()
	{
		if (base.Projectile.localAI[0] > 85f)
		{
			int num22 = 5;
			for (int index1 = 0; index1 < num22; index1++)
			{
				Vector2 vector = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)(base.Projectile.width + 50) / 2f, base.Projectile.height) * 0.75f).RotatedBy((double)(index1 - (num22 / 2 - 1)) * Math.PI / (double)num22) + base.Projectile.Center;
				Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
				Vector2 vector2_3 = vector2_2;
				int index2 = Dust.NewDust(vector + vector2_3, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].noLight = true;
				Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
				Main.dust[index2].velocity /= 4f;
				Main.dust[index2].velocity -= base.Projectile.velocity;
			}
		}
		return true;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			SoundEngine.PlaySound(SoundID.Zombie20, (Vector2?)base.Projectile.Center);
			this.p = (ShtunUtils.AnyBossAlive() ? Main.npc[FargoSoulsGlobalNPC.boss].target : Player.FindClosest(base.Projectile.Center, 0, 0));
			base.Projectile.netUpdate = true;
		}
		if ((base.Projectile.localAI[0] += 1f) > 85f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
			base.Projectile.frameCounter = 5;
			base.Projectile.frame = 6;
			return;
		}
		int ai0 = this.p;
		if (base.Projectile.localAI[0] == 85f)
		{
			base.Projectile.velocity = Main.player[ai0].Center - base.Projectile.Center;
			base.Projectile.velocity.Normalize();
			base.Projectile.velocity *= ((base.Projectile.type == ModContent.ProjectileType<MutantFishron>()) ? 24f : 20f);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
			base.Projectile.frameCounter = 5;
			base.Projectile.frame = 6;
			return;
		}
		Vector2 vel = Main.player[ai0].Center - base.Projectile.Center;
		base.Projectile.rotation = vel.ToRotation();
		if (vel.X > 0f)
		{
			vel.X -= 300f;
			base.Projectile.direction = (base.Projectile.spriteDirection = 1);
		}
		else
		{
			vel.X += 300f;
			base.Projectile.direction = (base.Projectile.spriteDirection = -1);
		}
		Vector2 distance = (Main.player[ai0].Center + new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]) - base.Projectile.Center) / 4f;
		base.Projectile.velocity = (base.Projectile.velocity * 19f + distance) / 20f;
		base.Projectile.position += Main.player[ai0].velocity / 2f;
		if (++base.Projectile.frameCounter > 5)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame > 5)
			{
				base.Projectile.frame = 0;
			}
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
		Color color26 = lightColor;
		color26 = base.Projectile.GetAlpha(color26);
		SpriteEffects spriteEffects = ((base.Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		if (base.Projectile.localAI[0] > 85f)
		{
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 2)
			{
				Color color27 = Color.Lerp(color26, Color.Pink, 0.25f);
				color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (1.5f * (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type]);
				Vector2 value4 = base.Projectile.oldPos[i];
				float num165 = base.Projectile.oldRot[i];
				if (base.Projectile.spriteDirection < 0)
				{
					num165 += (float)Math.PI;
				}
				Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, spriteEffects, 0);
			}
		}
		float drawRotation = base.Projectile.rotation;
		if (base.Projectile.spriteDirection < 0)
		{
			drawRotation += (float)Math.PI;
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), drawRotation, origin2, base.Projectile.scale, spriteEffects, 0);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		float ratio = (float)(255 - base.Projectile.alpha) / 255f;
		float white = MathHelper.Lerp(ratio, 1f, 0.25f);
		if (white > 1f)
		{
			white = 1f;
		}
		return new Color((int)((float)(int)lightColor.R * white), (int)((float)(int)lightColor.G * white), (int)((float)(int)lightColor.B * white), (int)((float)(int)lightColor.A * ratio));
	}
}
