using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantGlowything : ModProjectile
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/MutantBoss/MutantGlowything";
    private Vector2 spawnPoint;

	private float scalefactor;

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.aiStyle = -1;
		base.Projectile.scale = 0.5f;
		base.Projectile.alpha = 0;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		base.Projectile.rotation = base.Projectile.ai[0];
		if (this.spawnPoint == Vector2.Zero)
		{
			this.spawnPoint = base.Projectile.Center;
		}
		base.Projectile.Center = this.spawnPoint + Vector2.UnitX.RotatedBy(base.Projectile.ai[0]) * 96f * base.Projectile.scale;
		if (base.Projectile.scale < 4f)
		{
			base.Projectile.scale += 0.2f;
		}
		else
		{
			base.Projectile.scale = 4f;
			base.Projectile.alpha += 10;
		}
		if (base.Projectile.alpha > 255)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D glow = TextureAssets.Projectile[base.Projectile.type].Value;
		int rect1 = glow.Height;
		int rect2 = 0;
		Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
		Vector2 gloworigin2 = glowrectangle.Size() / 2f;
		Color glowcolor = new Color(255, 0, 0, 0);
		float scale = base.Projectile.scale;
		Main.EntitySpriteDraw(glow, base.Projectile.Center + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)glowrectangle, base.Projectile.GetAlpha(glowcolor), base.Projectile.rotation, gloworigin2, scale * 2f, SpriteEffects.None, 0);
		return false;
	}
}
