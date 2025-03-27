using System;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantGiantDeathray2 : MutantSpecialDeathray
{
	public int dustTimer;

	public bool stall;

	private int hits;

	public bool BeBrighter => base.Projectile.ai[0] > 0f;

	public PrimDrawer LaserDrawer { get; private set; }

	public MutantGiantDeathray2()
		: base(600)
	{
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		ProjectileID.Sets.DismountsPlayersOnHit[base.Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.netImportant = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		base.maxTime += 180f;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.scale >= 5f;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		base.SendExtraAI(writer);
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.ReceiveExtraAI(reader);
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		base.AI();
		if (!Main.dedServ && Main.LocalPlayer.active)
		{
			Main.LocalPlayer.GetModPlayer<ShtunPlayer>().Screenshake = 2;
		}
		base.Projectile.timeLeft = 2;
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		NPC npc = FargoSoulsUtil.NPCExists(base.Projectile.ai[1], ModContent.NPCType<MutantEX>());
		if (npc != null)
		{
			base.Projectile.Center = npc.Center + Main.rand.NextVector2Circular(5f, 5f) + Vector2.UnitX.RotatedBy(npc.ai[3]) * ((npc.ai[0] == -7f) ? 100 : 175) * base.Projectile.scale / 10f;
			if (npc.ai[0] == -7f)
			{
				base.maxTime = 255f;
			}
			else if (npc.ai[0] == -5f)
			{
				if (npc.localAI[2] > 30f)
				{
					if (base.Projectile.localAI[0] < base.maxTime - 90f)
					{
						base.Projectile.localAI[0] = base.maxTime - 90f;
					}
				}
				else if (this.stall)
				{
					this.stall = false;
					base.Projectile.localAI[0] -= 1f;
					base.Projectile.netUpdate = true;
					npc.ai[2] -= 1f;
					npc.netUpdate = true;
				}
			}
			if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
			{
				base.Projectile.velocity = -Vector2.UnitY;
			}
			if (base.Projectile.localAI[0] == 0f)
			{
				SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104");
				soundStyle.Volume = 1.5f;
				SoundEngine.PlaySound(ref soundStyle, (Vector2?)Main.player[Main.myPlayer].Center);
			}
			float num801 = 10f;
			base.Projectile.localAI[0] += 1f;
			if (base.Projectile.localAI[0] >= base.maxTime)
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / base.maxTime) * 7f * num801;
			base.Projectile.scale *= 5f;
			if (base.Projectile.scale > num801)
			{
				base.Projectile.scale = num801;
			}
			float num804 = npc.ai[3] - (float)Math.PI / 2f;
			_ = base.Projectile.rotation;
			base.Projectile.rotation = num804;
			num804 += (float)Math.PI / 2f;
			base.Projectile.velocity = num804.ToRotationVector2();
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
			if (base.Projectile.damage > 0 && Main.LocalPlayer.active && base.Projectile.Colliding(base.Projectile.Hitbox, Main.LocalPlayer.Hitbox))
			{
				Main.LocalPlayer.immune = false;
				Main.LocalPlayer.immuneTime = 0;
				Main.LocalPlayer.hurtCooldowns[0] = 0;
				Main.LocalPlayer.hurtCooldowns[1] = 0;
				Main.LocalPlayer.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public virtual void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
	{
		this.DamageRampup(ref damage);
		if (this.hits > 180)
		{
			target.endurance = 0f;
		}
	}

	private void DamageRampup(ref int damage)
	{
		int tempHits = this.hits - 90;
		if (tempHits > 0)
		{
			double modifier = Math.Min(1.0 + (double)tempHits / 6.0, 100.0);
			damage = (int)((double)damage * modifier);
		}
		else
		{
			damage = (int)((double)this.hits / 90.0 * (double)damage);
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		this.hits++;
		this.stall = true;
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
		target.immune = false;
		target.immuneTime = 0;
		target.hurtCooldowns[0] = 0;
		target.hurtCooldowns[1] = 0;
		target.velocity = -0.4f * Vector2.UnitY;
	}

    public float WidthFunction(float trailInterpolant)
    {
        float baseWidth = Projectile.scale * Projectile.width * 2;
        return baseWidth;
    }

    public static Color ColorFunction(float trailInterpolant) =>
        Color.Lerp(
            new(31, 187, 192, 100),
            new(51, 255, 191, 100),
            trailInterpolant);
    public override bool PreDraw(ref Color lightColor)
    {
        if (Projectile.velocity == Vector2.Zero)
            return false;

        ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.MutantDeathray");

        Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * drawDistance;
        Vector2 initialDrawPoint = Projectile.Center - Projectile.velocity * 400f;
        Vector2[] baseDrawPoints = new Vector2[8];
        for (int i = 0; i < baseDrawPoints.Length; i++)
            baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

        Color brightColor = new(194, 255, 242, 100);
        shader.TrySetParameter("mainColor", brightColor);

        FargoSoulsUtil.SetTexture1(FargosTextureRegistry.MutantStreak.Value);

        Texture2D glowTexture = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing").Value;

        Vector2 glowDrawPosition = Projectile.Center - Projectile.velocity * (BeBrighter ? 90f : 180f);

        Main.EntitySpriteDraw(glowTexture, glowDrawPosition - Main.screenPosition, null, brightColor, Projectile.rotation, glowTexture.Size() * 0.5f, Projectile.scale * 0.4f, SpriteEffects.None, 0);
        PrimitiveRenderer.RenderTrail(baseDrawPoints, new(WidthFunction, ColorFunction, Shader: shader), 60);
        return false;
    }
}
