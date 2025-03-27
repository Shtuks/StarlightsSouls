using System;
using System.Collections.Generic;
using System.IO;
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

public class MutantTrueEyeL : ModProjectile
{
	private float localAI0;

	private float localAI1;

	private float localai1;

	public override string Texture => "Terraria/Images/Projectile_650";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 42;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.CooldownSlot = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
		base.Projectile.hide = true;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		Player target = Main.player[(int)base.Projectile.ai[0]];
		base.Projectile.localAI[0] += 1f;
		switch ((int)base.Projectile.ai[1])
		{
		case 0:
		{
			Vector2 newVel = target.Center - base.Projectile.Center + new Vector2(0f, -300f);
			if (newVel != Vector2.Zero)
			{
				newVel.Normalize();
				newVel *= 24f;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * 29f + newVel.X) / 30f;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 29f + newVel.Y) / 30f;
			}
			if (base.Projectile.Distance(target.Center) < 150f)
			{
				if (base.Projectile.Center.X < target.Center.X)
				{
					base.Projectile.velocity.X -= 0.25f;
				}
				else
				{
					base.Projectile.velocity.X += 0.25f;
				}
				if (base.Projectile.Center.Y < target.Center.Y)
				{
					base.Projectile.velocity.Y -= 0.25f;
				}
				else
				{
					base.Projectile.velocity.Y += 0.25f;
				}
			}
			if (base.Projectile.localAI[0] > 120f)
			{
				base.Projectile.localAI[0] = 0f;
				base.Projectile.ai[1] += 1f;
				base.Projectile.netUpdate = true;
			}
			break;
		}
		case 1:
			base.Projectile.velocity *= 0.95f;
			if (base.Projectile.velocity.Length() < 1f)
			{
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.localAI[0] = 0f;
				base.Projectile.ai[1] += 1f;
				base.Projectile.netUpdate = true;
			}
			break;
		case 2:
			if (base.Projectile.localAI[0] == 1f)
			{
				float rotationDirection = (float)Math.PI / 135f;
				if (base.Projectile.Center.X < target.Center.X)
				{
					rotationDirection *= -1f;
				}
				this.localAI0 -= rotationDirection * 60f;
				Vector2 speed = -Vector2.UnitX.RotatedBy(this.localAI0);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center - Vector2.UnitY * 6f, speed, ModContent.ProjectileType<MutantTrueEyeDeathray>(), base.Projectile.damage, 0f, base.Projectile.owner, rotationDirection, (float)base.Projectile.whoAmI);
				}
				this.localai1 = rotationDirection;
				base.Projectile.netUpdate = true;
			}
			else if (base.Projectile.localAI[0] > 90f)
			{
				base.Projectile.localAI[0] = 0f;
				base.Projectile.ai[1] += 1f;
			}
			else
			{
				this.localAI0 += this.localai1;
			}
			break;
		default:
		{
			for (int i = 0; i < 30; i++)
			{
				int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 0, default(Color), 3f);
				Main.dust[d].noGravity = true;
				Main.dust[d].noLight = true;
				Main.dust[d].velocity *= 8f;
			}
			SoundEngine.PlaySound(SoundID.Zombie102, (Vector2?)base.Projectile.Center);
			base.Projectile.Kill();
			break;
		}
		}
		if ((double)base.Projectile.rotation > 3.14159274101257)
		{
			base.Projectile.rotation = base.Projectile.rotation - 6.283185f;
		}
		base.Projectile.rotation = (((double)base.Projectile.rotation <= -0.005 || (double)base.Projectile.rotation >= 0.005) ? (base.Projectile.rotation * 0.96f) : 0f);
		if (++base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= Main.projFrames[base.Projectile.type])
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.ai[1] != 2f)
		{
			this.UpdatePupil();
		}
	}

	private void UpdatePupil()
	{
		float f1 = (float)((double)this.localAI0 % 6.28318548202515 - 3.14159274101257);
		float num13 = (float)Math.IEEERemainder(this.localAI1, 1.0);
		if ((double)num13 < 0.0)
		{
			num13 += 1f;
		}
		float num14 = (float)Math.Floor(this.localAI1);
		float max = 0.999f;
		int num15 = 0;
		float amount = 0.1f;
		float f2 = base.Projectile.AngleTo(Main.player[(int)base.Projectile.ai[0]].Center);
		num15 = 2;
		float num18 = MathHelper.Clamp(num13 + 0.05f, 0f, max);
		float num19 = num14 + (float)Math.Sign(-12f - num14);
		Vector2 rotationVector2 = f2.ToRotationVector2();
		this.localAI0 = (float)((double)Vector2.Lerp(f1.ToRotationVector2(), rotationVector2, amount).ToRotation() + (double)num15 * 6.28318548202515 + 3.14159274101257);
		this.localAI1 = num19 + num18;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override bool? CanCutTiles()
	{
		return false;
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
		Color color26 = base.Projectile.GetAlpha((base.Projectile.hide && Main.netMode == 1) ? Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16) : lightColor);
		float scale = ((float)(int)Main.mouseTextColor / 200f - 0.35f) * 0.4f + 1f;
		scale *= base.Projectile.scale;
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
		{
			Color color27 = color26 * 0.75f;
			color27.A = 0;
			color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
			Vector2 value4 = base.Projectile.oldPos[i];
			float num165 = base.Projectile.oldRot[i];
			Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		Texture2D value5 = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/TrueEyePupil", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		_ = new Vector2(this.localAI1 / 2f, 0f).RotatedBy(this.localAI0) + new Vector2(0f, -6f).RotatedBy(base.Projectile.rotation);
		_ = value5.Size() / 2f;
		return false;
	}
}
