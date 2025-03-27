using System;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Render.Primitives;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantDeathraySmall : BaseDeathray, IPixelatedPrimitiveRenderer
{
	public PrimDrawer LaserDrawer { get; private set; }
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayML";
    public MutantDeathraySmall()
		: base(30f)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		float num801 = 0.3f;
		base.Projectile.localAI[0] += 1f;
		if (base.Projectile.localAI[0] >= base.maxTime || true)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / base.maxTime) * 0.6f * num801;
		if (base.Projectile.scale > num801)
		{
			base.Projectile.scale = num801;
		}
		float num805 = 3f;
		_ = base.Projectile.width;
		_ = base.Projectile.Center;
		if (vector78.HasValue)
		{
			_ = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i] = 3000f;
		}
		float num807 = 0f;
		for (int num808 = 0; num808 < array3.Length; num808++)
		{
			num807 += array3[num808];
		}
		num807 /= num805;
		float amount = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], num807, amount);
		Vector2 vector79 = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		for (int num809 = 0; num809 < 2; num809++)
		{
			float num810 = base.Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
			Vector2 vector80 = new Vector2((float)Math.Cos(num810) * num811, (float)Math.Sin(num810) * num811);
			int num812 = Dust.NewDust(vector79, 0, 0, 244, vector80.X, vector80.Y);
			Main.dust[num812].noGravity = true;
			Main.dust[num812].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value29 = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * base.Projectile.width;
			int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 244, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[num813].velocity *= 0.5f;
			Main.dust[num813].velocity.Y = 0f - Math.Abs(Main.dust[num813].velocity.Y);
		}
		base.Projectile.position -= base.Projectile.velocity;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

    public override bool PreDraw(ref Color lightColor) => false;

    public float WidthFunction(float trailInterpolant) => Projectile.width * Projectile.scale * 1.3f;

    public static Color ColorFunction(float trailInterpolant)
    {
        Color color = Color.Cyan;
        color.A = 100;
        return color;
    }
    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
        if (Projectile.hide)
            return;

        ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.GenericDeathray");

        Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * drawDistance * 1.1f;

        Vector2 initialDrawPoint = Projectile.Center - Projectile.velocity * 150f;
        Vector2[] baseDrawPoints = new Vector2[8];
        for (int i = 0; i < baseDrawPoints.Length; i++)
            baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

        FargoSoulsUtil.SetTexture1(FargosTextureRegistry.MutantStreak.Value);

        shader.TrySetParameter("mainColor", new Color(183, 252, 253, 100));
        shader.TrySetParameter("stretchAmount", 3);
        shader.TrySetParameter("scrollSpeed", 2f);
        shader.TrySetParameter("uColorFadeScaler", 1f);
        shader.TrySetParameter("useFadeIn", true);

        PrimitiveRenderer.RenderTrail(baseDrawPoints, new(WidthFunction, ColorFunction, Pixelate: true, Shader: shader), 20);
    }
}
