using System;
using System.IO;
using System.Linq;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantDestroyerHead : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_134";

    public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.netImportant = true;
		base.CooldownSlot = 1;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num214 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y6 = num214 * base.Projectile.frame;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, num214), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), base.Projectile.scale, (base.Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		return false;
	}

	public override void AI()
	{
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1));
		float desiredFlySpeedInPixelsPerFrame = 10f * base.Projectile.ai[1];
		float amountOfFramesToLerpBy = 25f / base.Projectile.ai[1];
		if ((base.Projectile.localAI[1] += 1f) > 60f)
		{
			int foundTarget = (int)base.Projectile.ai[0];
			Player p = Main.player[foundTarget];
			if (base.Projectile.Distance(p.Center) > 700f)
			{
				desiredFlySpeedInPixelsPerFrame *= 2f;
				amountOfFramesToLerpBy /= 2f;
			}
			Vector2 desiredVelocity = base.Projectile.DirectionTo(p.Center) * desiredFlySpeedInPixelsPerFrame;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
		}
		foreach (Projectile p in Main.projectile.Where((Projectile p) => p.active && p.type == base.Projectile.type && p.whoAmI != base.Projectile.whoAmI && p.Distance(base.Projectile.Center) < (float)base.Projectile.width))
		{
			base.Projectile.velocity.X += 0.05f * (float)((!(base.Projectile.position.X < p.position.X)) ? 1 : (-1));
			base.Projectile.velocity.Y += 0.05f * (float)((!(base.Projectile.position.Y < p.position.Y)) ? 1 : (-1));
			p.velocity.X += 0.05f * (float)((!(p.position.X < base.Projectile.position.X)) ? 1 : (-1));
			p.velocity.Y += 0.05f * (float)((!(p.position.Y < base.Projectile.position.Y)) ? 1 : (-1));
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.AddBuff(ModContent.BuffType<LightningRodBuff>(), Main.rand.Next(300, 1200));
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 20; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 2f;
			dust = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 60, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100);
			Main.dust[dust].velocity *= 2f;
		}
		SoundEngine.PlaySound(SoundID.NPCDeath14, (Vector2?)base.Projectile.Center);
	}
}
