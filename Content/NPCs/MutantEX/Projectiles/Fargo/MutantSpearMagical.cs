using System;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo;

public class MutantSpearMagical : MutantSpearThrown
{
	private const int attackTime = 120;

	private const int flySpeed = 25;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.timeLeft = 144;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0] == 0f)
		{
			if (base.Projectile.localAI[1] == 0f)
			{
				base.Projectile.rotation = (float)Math.PI * 2f + Main.rand.NextFloat((float)Math.PI * 2f);
				if (Main.rand.NextBool())
				{
					base.Projectile.rotation *= -1f;
				}
			}
			base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, base.Projectile.ai[1], 0.05f);
			if ((base.Projectile.localAI[1] += 1f) > 120f)
			{
				SoundEngine.PlaySound(SoundID.Item1, (Vector2?)base.Projectile.Center);
				base.Projectile.ai[0] = 1f;
				base.Projectile.velocity = 25f * base.Projectile.ai[1].ToRotationVector2();
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			if ((base.Projectile.localAI[0] -= 1f) < 0f)
			{
				base.Projectile.localAI[0] = 4f;
				if (base.Projectile.ai[1] == 0f && Main.netMode != 1)
				{
					Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MutantSphereSmall>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.ai[0], 0f);
				}
			}
		}
		base.scaletimer += 1f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		Color color = Color.White * base.Projectile.Opacity;
		color.A = (byte)(255f * Math.Min(base.Projectile.localAI[1] / 120f, 1f));
		return color;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (base.Projectile.ai[0] == 0f)
		{
			return true;
		}
		return base.PreDraw(ref lightColor);
	}
}
