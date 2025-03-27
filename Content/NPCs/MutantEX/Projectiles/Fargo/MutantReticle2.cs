using System;
using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantReticle2 : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/TargetingReticle";
    public override void SetDefaults()
	{
		base.Projectile.width = 110;
		base.Projectile.height = 110;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 60;
		base.Projectile.extraUpdates = 1;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		if (FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && !Main.npc[ShtunNpcs.mutantEX].dontTakeDamage)
		{
			int modifier = 60 - base.Projectile.timeLeft;
			base.Projectile.scale = 4f - 0.05f * (float)modifier;
			base.Projectile.rotation = (float)Math.PI / 15f * (float)modifier;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.timeLeft < 10)
		{
			base.Projectile.alpha += 25;
			return;
		}
		base.Projectile.alpha -= 4;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 128) * (1f - (float)base.Projectile.alpha / 255f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}
}
