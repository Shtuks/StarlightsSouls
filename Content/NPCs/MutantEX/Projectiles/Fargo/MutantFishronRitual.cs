using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantFishronRitual : ModProjectile
{
	private const int safeRange = 150;

    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FishronRitual";

    public override void SetDefaults()
	{
		base.Projectile.width = 320;
		base.Projectile.height = 320;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.CooldownSlot = -1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCheck = (Projectile projectile) => this.CanDamage() == true && Math.Abs((Main.LocalPlayer.Center - base.Projectile.Center).Length() - 150f) < 42f + Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().GrazeRadius;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override bool? CanDamage()
	{
		return (float)base.Projectile.alpha == 0f && Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantBomb>()] > 0;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if ((projHitbox.Center.ToVector2() - targetHitbox.Center.ToVector2()).Length() < 150f)
		{
			return false;
		}
		int clampedX = projHitbox.Center.X - targetHitbox.Center.X;
		int clampedY = projHitbox.Center.Y - targetHitbox.Center.Y;
		if (Math.Abs(clampedX) > targetHitbox.Width / 2)
		{
			clampedX = targetHitbox.Width / 2 * Math.Sign(clampedX);
		}
		if (Math.Abs(clampedY) > targetHitbox.Height / 2)
		{
			clampedY = targetHitbox.Height / 2 * Math.Sign(clampedY);
		}
		int num = projHitbox.Center.X - targetHitbox.Center.X - clampedX;
		int dY = projHitbox.Center.Y - targetHitbox.Center.Y - clampedY;
		return Math.Sqrt(num * num + dY * dY) <= 1200.0;
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(base.Projectile.ai[0], ModContent.NPCType<MutantEX>());
		if (npc != null && npc.ai[0] == 34f)
		{
			base.Projectile.alpha -= 7;
			base.Projectile.timeLeft = 300;
			base.Projectile.Center = npc.Center;
			base.Projectile.position.Y -= 100f;
		}
		else
		{
			base.Projectile.alpha += 17;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha > 255)
		{
			base.Projectile.alpha = 255;
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = 1f - (float)base.Projectile.alpha / 255f;
		base.Projectile.rotation += (float)Math.PI / 70f;
		Lighting.AddLight(base.Projectile.Center, 0.4f, 0.9f, 1.1f);
		if (base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD > 10)
		{
			base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 10;
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

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
