using System;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantEyeWavy : MutantEye
{
	public float oldRot;

    public override string Texture => "Terraria/Images/Projectile_452";

    public override int TrailAdditive => 150;

	private float Amplitude => base.Projectile.ai[0];

	private float Period => base.Projectile.ai[1];

	private float Counter => base.Projectile.localAI[1] * 4f;

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.timeLeft = 180;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.CooldownSlot = 0;
	}

	public override void AI()
	{
		NPC mutant = FargoSoulsUtil.NPCExists(ShtunNpcs.mutantEX);
		if (mutant != null && (mutant.ai[0] == -5f || mutant.ai[0] == -7f))
		{
			float targetRotation = mutant.ai[3];
			float speed = base.Projectile.velocity.Length();
			float rotation = targetRotation + (float)Math.PI / 4f * (float)Math.Sin((float)Math.PI * 2f * this.Counter / this.Period) * this.Amplitude;
			base.Projectile.velocity = speed * rotation.ToRotationVector2();
			if (this.oldRot != 0f)
			{
				Vector2 oldCenter = base.Projectile.Center;
				base.Projectile.Center = mutant.Center + (base.Projectile.Center - mutant.Center).RotatedBy(targetRotation - this.oldRot);
				_ = base.Projectile.Center - oldCenter;
			}
			this.oldRot = targetRotation;
			base.Projectile.localAI[0] += 0.1f;
			base.AI();
		}
		else
		{
			base.Projectile.Kill();
		}
	}
}
