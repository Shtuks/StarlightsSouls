using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantBombSmall : MutantBomb
{
    public override string Texture => "Terraria/Images/Projectile_645";

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = 275;
		base.Projectile.height = 275;
		base.Projectile.scale = 0.75f;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.frame > 2 && base.Projectile.frame <= 4)
		{
			base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().GrazeCD = 1;
			return false;
		}
		return true;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			SoundEngine.PlaySound(SoundID.Item14, (Vector2?)base.Projectile.Center);
		}
		if (++base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= Main.projFrames[base.Projectile.type])
			{
				base.Projectile.frame--;
				base.Projectile.Kill();
			}
		}
	}
}
