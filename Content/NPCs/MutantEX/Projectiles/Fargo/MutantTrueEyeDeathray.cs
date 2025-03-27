using System;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Render.Primitives;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantTrueEyeDeathray : BaseDeathray, IPixelPrimitiveDrawer
{
	public PrimDrawer LaserDrawer { get; private set; }

    public override string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayML";

    public MutantTrueEyeDeathray()
		: base(90f)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override bool CanHitPlayer(Player target)
	{
		return target.hurtCooldowns[1] == 0;
	}

	public override void AI()
	{
		base.Projectile.hide = false;
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104");
			soundStyle.Volume = 0.5f;
			SoundEngine.PlaySound(ref soundStyle, (Vector2?)base.Projectile.Center);
		}
		float num801 = 0.4f;
		base.Projectile.localAI[0] += 1f;
		if (base.Projectile.localAI[0] >= base.maxTime)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / base.maxTime) * 10f * num801;
		if (base.Projectile.scale > num801)
		{
			base.Projectile.scale = num801;
		}
		float num804 = base.Projectile.velocity.ToRotation();
		num804 += base.Projectile.ai[0];
		base.Projectile.rotation = num804 - (float)Math.PI / 2f;
		base.Projectile.velocity = num804.ToRotationVector2();
		float num805 = 3f;
		float num806 = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float[] array3 = new float[(int)num805];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, num806 * base.Projectile.scale, 3000f, array3);
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
		DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
		base.Projectile.position -= base.Projectile.velocity;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public float WidthFunction(float trailInterpolant)
	{
		return (float)base.Projectile.width * base.Projectile.scale * 1.3f;
	}

	public Color ColorFunction(float trailInterpolant)
	{
		return Color.Cyan;
	}

	public void DrawPixelPrimitives(SpriteBatch spriteBatch)
	{
		if (this.LaserDrawer == null)
		{
			PrimDrawer primDrawer2 = (this.LaserDrawer = new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["FargowiltasSouls:GenericDeathray"]));
		}
		Vector2 laserEnd = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.drawDistance;
		Vector2 initialDrawPoint = base.Projectile.Center - base.Projectile.velocity * 70f;
		Vector2[] baseDrawPoints = new Vector2[8];
		for (int i = 0; i < baseDrawPoints.Length; i++)
		{
			baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, (float)i / ((float)baseDrawPoints.Length - 1f));
		}
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].SetShaderTexture(FargosTextureRegistry.MutantStreak);
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].UseColor(new Color(255, 52, 53));
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].Shader.Parameters["stretchAmount"].SetValue(3);
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].Shader.Parameters["scrollSpeed"].SetValue(2f);
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].Shader.Parameters["uColorFadeScaler"].SetValue(1f);
		GameShaders.Misc["FargowiltasSouls:GenericDeathray"].Shader.Parameters["useFadeIn"].SetValue(value: true);
		this.LaserDrawer.DrawPixelPrims(baseDrawPoints, -Main.screenPosition, 20);
	}
}
