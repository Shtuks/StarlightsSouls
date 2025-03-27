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
public class MutantPillar : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/CelestialPillar";

    private int target = -1;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 120;
		base.Projectile.height = 120;
		base.Projectile.aiStyle = -1;
		base.Projectile.alpha = 255;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(this.target);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		this.target = reader.ReadInt32();
	}

	public override bool? CanDamage()
	{
		return base.Projectile.alpha == 0;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			int type = (int)base.Projectile.ai[0] switch
			{
				0 => 242, 
				1 => 127, 
				2 => 229, 
				_ => 135, 
			};
			for (int index = 0; index < 50; index++)
			{
				Dust dust = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, type)];
				dust.velocity *= 10f;
				dust.fadeIn = 1f;
				dust.scale = 1f + Main.rand.NextFloat() + (float)Main.rand.Next(4) * 0.3f;
				if (Main.rand.Next(3) != 0)
				{
					dust.noGravity = true;
					dust.velocity *= 3f;
					dust.scale *= 2f;
				}
			}
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.velocity.Y += 1f / 24f;
			base.Projectile.rotation += base.Projectile.velocity.Length() / 20f * 2f;
			base.Projectile.localAI[1] += base.Projectile.velocity.Y;
			base.Projectile.alpha -= 2;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				if (this.target != -1)
				{
					SoundEngine.PlaySound(SoundID.Item89, (Vector2?)base.Projectile.Center);
					base.Projectile.velocity = Main.player[this.target].Center - base.Projectile.Center;
					float distance = base.Projectile.velocity.Length();
					base.Projectile.velocity.Normalize();
					base.Projectile.velocity *= 32f;
					base.Projectile.timeLeft = (int)(distance / 32f);
					base.Projectile.netUpdate = true;
					return;
				}
				base.Projectile.Kill();
			}
			else
			{
				NPC npc = Main.npc[(int)base.Projectile.ai[1]];
				this.target = npc.target;
				base.Projectile.Center = npc.Center;
				base.Projectile.position.Y += base.Projectile.localAI[1];
			}
			if (this.target >= 0 && Main.player[this.target].active && !Main.player[this.target].dead)
			{
				if (base.Projectile.alpha < 100)
				{
					base.Projectile.rotation = base.Projectile.rotation.AngleLerp((Main.player[this.target].Center - base.Projectile.Center).ToRotation(), (float)(255 - base.Projectile.alpha) / 255f * 0.08f);
				}
			}
			else
			{
				int possibleTarget = Player.FindClosest(base.Projectile.Center, 0, 0);
				if (possibleTarget != -1)
				{
					this.target = possibleTarget;
					base.Projectile.netUpdate = true;
				}
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.frame = (int)base.Projectile.ai[0];
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (target.mount.Active)
		{
			target.mount.Dismount(target);
		}
		target.velocity.X = ((base.Projectile.velocity.X < 0f) ? (-15f) : 15f);
		target.velocity.Y = -10f;
		target.AddBuff(ModContent.BuffType<StunnedBuff>(), 60);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
		target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 240);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);

		switch ((int)base.Projectile.ai[0])
		{
		case 0:
			target.AddBuff(ModContent.BuffType<ReverseManaFlowBuff>(), 360);
			break;
		case 1:
			target.AddBuff(ModContent.BuffType<AtrophiedBuff>(), 360);
			break;
		case 2:
			target.AddBuff(ModContent.BuffType<JammedBuff>(), 360);
			break;
		default:
			target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 360);
			break;
		}
		base.Projectile.timeLeft = 0;
	}

	public override void Kill(int timeLeft)
	{
		if (Main.LocalPlayer.active && !Main.dedServ)
		{
			Main.LocalPlayer.GetModPlayer<ShtunPlayer>().Screenshake = 30;
		}
		SoundEngine.PlaySound(SoundID.Item92, (Vector2?)base.Projectile.Center);
		int type = (int)base.Projectile.ai[0] switch
		{
			0 => 242, 
			1 => 127, 
			2 => 229, 
			_ => 135, 
		};
		for (int index = 0; index < 80; index++)
		{
			Dust dust = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, type)];
			dust.velocity *= 10f;
			dust.fadeIn = 1f;
			dust.scale = 1f + Main.rand.NextFloat() + (float)Main.rand.Next(4) * 0.3f;
			if (!Main.rand.NextBool(3))
			{
				dust.noGravity = true;
				dust.velocity *= 3f;
				dust.scale *= 2f;
			}
		}
		if (Main.netMode == 1)
		{
			return;
		}
		int fragmentDuration = 240;
		if (FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && Main.npc[ShtunNpcs.mutantEX].ai[0] == 19f)
		{
			fragmentDuration = (int)Main.npc[ShtunNpcs.mutantEX].localAI[0];
		}
		float speed = 5.5f;
		for (int j = 0; j < 4; j++)
		{
			Vector2 vel = new Vector2(0f, speed * ((float)j + 0.5f)).RotatedBy(base.Projectile.rotation);
			for (int i = 0; i < 24; i++)
			{
				int p = Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, vel.RotatedBy((float)Math.PI / 12f * (float)i), ModContent.ProjectileType<MutantFragment>(), base.Projectile.damage / 2, 0f, Main.myPlayer, base.Projectile.ai[0], 0f);
				if (p != 1000)
				{
					Main.projectile[p].timeLeft = fragmentDuration;
				}
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 255 - base.Projectile.alpha);
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
		for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 3)
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
}
