using System;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Render.Primitives;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantHeal : ModProjectile, IPixelPrimitiveDrawer
{
    public override string Texture => "FargowiltasSouls/Content/Bosses/MutantBoss/MutantHeal";
    public PrimDrawer TrailDrawer { get; private set; }

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 20;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.aiStyle = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 1800;
		base.Projectile.scale = 0.8f;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
	}

	public override bool? CanDamage()
	{
		return false;
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

	public override void AI()
	{
		int ai0;
		bool feedPlayer;
		bool num;
		if (base.Projectile.localAI[0] == 0f)
		{
			if (base.Projectile.localAI[1] == 0f)
			{
				base.Projectile.localAI[1] = Main.rand.NextFloat(MathHelper.ToRadians(1f)) * (float)(Main.rand.NextBool() ? 1 : (-1));
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity).RotatedBy(base.Projectile.localAI[1]) * (base.Projectile.velocity.Length() - base.Projectile.ai[1]);
			if (base.Projectile.velocity.Length() < 0.01f)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			if (base.Projectile.localAI[0] != 1f)
			{
				base.Projectile.extraUpdates = 1;
				ai0 = (int)Math.Abs(base.Projectile.ai[0]);
				feedPlayer = base.Projectile.ai[0] < 0f;
				if (feedPlayer)
				{
					ai0--;
					base.Projectile.Kill();
					return;
				}
				if (ai0 >= 0)
				{
					if (!feedPlayer)
					{
						if (ai0 < 200)
						{
							num = !Main.npc[ai0].active;
							goto IL_02ad;
						}
					}
					else if (ai0 < 255 && Main.player[ai0].active && !Main.player[ai0].ghost)
					{
						num = Main.player[ai0].dead;
						goto IL_02ad;
					}
				}
				goto IL_02af;
			}
			for (int i = 0; i < 2; i++)
			{
				base.Projectile.position += base.Projectile.velocity;
				Vector2 change = Vector2.Normalize(base.Projectile.velocity) * 5f;
				base.Projectile.velocity = (base.Projectile.velocity * 29f + change).RotatedBy(base.Projectile.localAI[1] * 3f) / 30f;
			}
			if (base.Projectile.velocity.Length() > 4.5f)
			{
				base.Projectile.localAI[0] = 2f;
				base.Projectile.netUpdate = true;
				base.Projectile.timeLeft = 360;
			}
		}
		goto IL_04e6;
		IL_02ad:
		if (num)
		{
			goto IL_02af;
		}
		Entity target = (feedPlayer ? ((Entity)Main.player[ai0]) : ((Entity)Main.npc[ai0]));
		if (base.Projectile.Distance(target.Center) < 5f)
		{
			if (feedPlayer)
			{
				if (Main.player[ai0].whoAmI == Main.myPlayer)
				{
					Main.player[ai0].ClearBuff(ModContent.BuffType<MutantFangBuff>());
					Main.player[ai0].statLife += base.Projectile.damage;
					Main.player[ai0].HealEffect(base.Projectile.damage);
					if (Main.player[ai0].statLife > Main.player[ai0].statLifeMax2)
					{
						Main.player[ai0].statLife = Main.player[ai0].statLifeMax2;
					}
					base.Projectile.Kill();
				}
			}
			else if (Main.netMode != 1)
			{
				Main.npc[ai0].life += base.Projectile.damage;
				Main.npc[ai0].HealEffect(base.Projectile.damage);
				if (Main.npc[ai0].life > Main.npc[ai0].lifeMax)
				{
					Main.npc[ai0].life = Main.npc[ai0].lifeMax;
				}
				Main.npc[ai0].netUpdate = true;
				base.Projectile.Kill();
			}
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 change = base.Projectile.DirectionTo(target.Center) * 5f;
				base.Projectile.velocity = (base.Projectile.velocity * 29f + change) / 30f;
			}
			base.Projectile.position += (target.position - target.oldPosition) / 2f;
		}
		for (int i = 0; i < 3; i++)
		{
			base.Projectile.position += base.Projectile.velocity;
		}
		goto IL_04e6;
		IL_02af:
		base.Projectile.Kill();
		return;
		IL_04e6:
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	public override void Kill(int timeLeft)
	{
		for (int i = 0; i < 5; i++)
		{
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 259, 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[d].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(100, 255, 244, 210) * base.Projectile.Opacity * 0.8f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.Orange, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, SpriteEffects.None, 0);
		return false;
	}

	public float WidthFunction(float completionRatio)
	{
		return MathHelper.SmoothStep(base.Projectile.scale * (float)base.Projectile.width * 1.9f, 3.5f, completionRatio);
	}

	public Color ColorFunction(float completionRatio)
	{
		return Color.Lerp(Color.Orange, Color.Transparent, completionRatio) * 0.7f;
	}

	public void DrawPixelPrimitives(SpriteBatch spriteBatch)
	{
		if (this.TrailDrawer == null)
		{
			PrimDrawer primDrawer2 = (this.TrailDrawer = new PrimDrawer(WidthFunction, ColorFunction, GameShaders.Misc["FargowiltasSouls:BlobTrail"]));
		}
		GameShaders.Misc["FargowiltasSouls:BlobTrail"].SetShaderTexture(FargosTextureRegistry.FadedStreak);
		this.TrailDrawer.DrawPixelPrims(base.Projectile.oldPos, base.Projectile.Size * 0.5f - Main.screenPosition, 25);
	}
}
