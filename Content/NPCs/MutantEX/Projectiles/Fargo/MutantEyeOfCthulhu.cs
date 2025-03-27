using System;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantEyeOfCthulhu : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_4";

    private const float degreesOffset = 22.5f;

	private const float dashSpeed = 120f;

	private const float baseDistance = 700f;

	private bool spawned;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = Main.npcFrameCount[4];
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 12;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = -1;
		base.CooldownSlot = 1;
		base.Projectile.timeLeft = 216;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		base.Projectile.alpha = 255;
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override bool? CanDamage()
	{
		return base.Projectile.ai[1] >= 120f;
	}

	public override void AI()
	{
		Player player = FargoSoulsUtil.PlayerExists(base.Projectile.ai[0]);
		if (player == null)
		{
			base.Projectile.Kill();
			return;
		}
		if (!this.spawned)
		{
			this.spawned = true;
			SoundEngine.PlaySound(SoundID.ForceRoarPitched, (Vector2?)base.Projectile.Center);
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, -1f, 4f);
			}
		}
		if ((base.Projectile.ai[1] += 1f) < 120f)
		{
			base.Projectile.alpha -= 8;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
			base.Projectile.position += player.velocity / 2f;
			float rangeModifier = base.Projectile.ai[1] * 1.5f / 120f;
			if (rangeModifier < 0.25f)
			{
				rangeModifier = 0.25f;
			}
			if (rangeModifier > 1f)
			{
				rangeModifier = 1f;
			}
			Vector2 target = player.Center + base.Projectile.DirectionFrom(player.Center).RotatedBy(MathHelper.ToRadians(20f)) * 700f * rangeModifier;
			float speedModifier = 0.6f;
			if (base.Projectile.Center.X < target.X)
			{
				base.Projectile.velocity.X += speedModifier;
				if (base.Projectile.velocity.X < 0f)
				{
					base.Projectile.velocity.X += speedModifier * 2f;
				}
			}
			else
			{
				base.Projectile.velocity.X -= speedModifier;
				if (base.Projectile.velocity.X > 0f)
				{
					base.Projectile.velocity.X -= speedModifier * 2f;
				}
			}
			if (base.Projectile.Center.Y < target.Y)
			{
				base.Projectile.velocity.Y += speedModifier;
				if (base.Projectile.velocity.Y < 0f)
				{
					base.Projectile.velocity.Y += speedModifier * 2f;
				}
			}
			else
			{
				base.Projectile.velocity.Y -= speedModifier;
				if (base.Projectile.velocity.Y > 0f)
				{
					base.Projectile.velocity.Y -= speedModifier * 2f;
				}
			}
			if (Math.Abs(base.Projectile.velocity.X) > 24f)
			{
				base.Projectile.velocity.X = 24 * Math.Sign(base.Projectile.velocity.X);
			}
			if (Math.Abs(base.Projectile.velocity.Y) > 24f)
			{
				base.Projectile.velocity.Y = 24 * Math.Sign(base.Projectile.velocity.Y);
			}
			base.Projectile.rotation = base.Projectile.DirectionTo(player.Center).ToRotation() - (float)Math.PI / 2f;
		}
		else if (base.Projectile.ai[1] == 120f)
		{
			base.Projectile.localAI[0] = player.Center.X;
			base.Projectile.localAI[1] = player.Center.Y;
			base.Projectile.Center = player.Center + base.Projectile.DirectionFrom(player.Center) * 700f;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
		}
		else if (base.Projectile.ai[1] == 121f)
		{
			if (Main.netMode != 1)
			{
				SpawnProjectile(base.Projectile.Center - base.Projectile.velocity / 2f);
				float accel = 0.025f;
				Vector2 target = new Vector2(base.Projectile.localAI[0], base.Projectile.localAI[1]);
				float angle = base.Projectile.DirectionTo(target).ToRotation();
				int p = Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MutantScythe2>(), base.Projectile.damage, 0f, Main.myPlayer, accel, angle);
				if (p != 1000)
				{
					Main.projectile[p].timeLeft = base.Projectile.timeLeft + 180 + 30;
				}
				p = Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MutantScythe2>(), base.Projectile.damage, 0f, Main.myPlayer, accel, angle);
				if (p != 1000)
				{
					Main.projectile[p].timeLeft = base.Projectile.timeLeft + 180 + 30 + 150;
				}
			}
			base.Projectile.velocity = 120f * base.Projectile.DirectionTo(new Vector2(base.Projectile.localAI[0], base.Projectile.localAI[1])).RotatedBy(MathHelper.ToRadians(22.5f));
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(SoundID.ForceRoarPitched, (Vector2?)base.Projectile.Center);
		}
		else if (base.Projectile.ai[1] < 131.66667f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
			if (Main.netMode != 1)
			{
				SpawnProjectile(base.Projectile.Center);
				SpawnProjectile(base.Projectile.Center - base.Projectile.velocity / 2f);
			}
		}
		else
		{
			if (Main.netMode != 1)
			{
				SpawnProjectile(base.Projectile.Center);
				SpawnProjectile(base.Projectile.Center - base.Projectile.velocity / 2f);
			}
			base.Projectile.ai[1] = 120f;
		}
		if (++base.Projectile.frameCounter > 6)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= Main.projFrames[base.Projectile.type])
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.frame < 3)
		{
			base.Projectile.frame = 3;
		}
		void SpawnProjectile(Vector2 position)
		{
			float accel = 0.03f;
			Vector2 target = new Vector2(base.Projectile.localAI[0], base.Projectile.localAI[1]);
			target += 180f * base.Projectile.DirectionTo(target).RotatedBy(1.5707963705062866);
			float angle = base.Projectile.DirectionTo(target).ToRotation();
			int p = Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), position, Vector2.Zero, ModContent.ProjectileType<MutantScythe1>(), base.Projectile.damage, 0f, Main.myPlayer, accel, angle);
			if (p != 1000)
			{
				Main.projectile[p].timeLeft = base.Projectile.timeLeft + 180 + 30 + 150;
				Main.projectile[p].timeLeft -= 30;
			}
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(163, 15);
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 120);
		target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 50; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 0, default(Color), 2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 5f;
		}
		Vector2 goreSpeed = ((base.Projectile.localAI[0] != 0f && base.Projectile.localAI[1] != 0f) ? (30f * base.Projectile.DirectionTo(new Vector2(base.Projectile.localAI[0], base.Projectile.localAI[1])).RotatedBy(MathHelper.ToRadians(22.5f))) : Vector2.Zero);
		for (int i = 0; i < 2; i++)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position + new Vector2(Main.rand.NextFloat(base.Projectile.width), Main.rand.NextFloat(base.Projectile.height)), goreSpeed, ModContent.Find<ModGore>(base.Mod.Name, "Gore_8").Type);
				Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position + new Vector2(Main.rand.NextFloat(base.Projectile.width), Main.rand.NextFloat(base.Projectile.height)), goreSpeed, ModContent.Find<ModGore>(base.Mod.Name, "Gore_9").Type);
				Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position + new Vector2(Main.rand.NextFloat(base.Projectile.width), Main.rand.NextFloat(base.Projectile.height)), goreSpeed, ModContent.Find<ModGore>(base.Mod.Name, "Gore_10").Type);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Color color26 = base.Projectile.GetAlpha(lightColor);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		float scale = ((float)(int)Main.mouseTextColor / 200f - 0.35f) * 0.3f + 0.9f;
		scale *= base.Projectile.scale;
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = color26 * ((base.Projectile.ai[1] >= 120f) ? 0.75f : 0.5f);
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, scale, SpriteEffects.None, 0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
