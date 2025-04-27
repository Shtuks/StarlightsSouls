using System;
using System.Collections.Generic;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ssm.Content.NPCs.MutantEX.Projectiles.Fargo;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles;

public class MutantBoss : ModProjectile
{
	public bool auraTrail;

	private const int auraFrames = 19;

	public bool sansEye;

	public float SHADOWMUTANTREAL;
    public override string Texture => "ssm/Content/NPCs/MutantEX/MutantEX";

    public int npcType => ModContent.NPCType<MutantEX>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 5;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 70;
		base.Projectile.height = 62;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		if (base.Projectile.hide)
		{
			behindProjectiles.Add(index);
		}
	}

	public override void AI()
	{
		NPC npc = FargoSoulsUtil.NPCExists(base.Projectile.ai[1], this.npcType);
		if (npc != null)
		{
			base.Projectile.Center = npc.Center;
			base.Projectile.alpha = npc.alpha;
			base.Projectile.direction = (base.Projectile.spriteDirection = npc.direction);
			base.Projectile.timeLeft = 30;
			this.auraTrail = npc.localAI[3] >= 3f;
			base.Projectile.hide = Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearAim>()] > 0 || Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearDash>()] > 0 || Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearSpin>()] > 0 || Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSlimeRain>()] > 0;
			this.sansEye = (npc.ai[0] == 10f && npc.ai[1] > 150f) || (npc.ai[0] == -5f && npc.ai[2] > 330f && npc.ai[2] < 420f);
			if (npc.ai[0] == 10f)
			{
				this.SHADOWMUTANTREAL += 0.03f;
				if (this.SHADOWMUTANTREAL > 0.75f)
				{
					this.SHADOWMUTANTREAL = 0.75f;
				}
			}
			base.Projectile.localAI[1] = (this.sansEye ? MathHelper.Lerp(base.Projectile.localAI[1], 1f, 0.05f) : 0f);
			base.Projectile.ai[0] = (this.sansEye ? (base.Projectile.ai[0] + 1f) : 0f);
			if (npc.ai[0] >= 11f || npc.ai[0] < 0f)
			{
				this.sansEye = true;
				base.Projectile.ai[0] = -1f;
			}
			if (!Main.dedServ)
			{
				base.Projectile.frame = (int)((float)npc.frame.Y / (float)(TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type]));
			}
			if (npc.frameCounter == 0.0 && (base.Projectile.localAI[0] += 1f) >= 19f)
			{
				base.Projectile.localAI[0] = 0f;
			}
			this.SHADOWMUTANTREAL -= 0.01f;
			if (this.SHADOWMUTANTREAL < 0f)
			{
				this.SHADOWMUTANTREAL = 0f;
			}
		}
		else
		{
			this.sansEye = false;
			if (Main.netMode != 1)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void Kill(int timeLeft)
	{
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
		Texture2D texture2D14 = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/MutantSoul", AssetRequestMode.ImmediateLoad).Value;
        int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
		int y3 = num156 * base.Projectile.frame;
		Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
		Vector2 origin2 = rectangle.Size() / 2f;
		Texture2D aura = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/MutantAura", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
		int auraFrameHeight = aura.Height / 19;
		int auraY = auraFrameHeight * (int)base.Projectile.localAI[0];
		Rectangle auraRectangle = new Rectangle(0, auraY, aura.Width, auraFrameHeight);
		Color color26 = base.Projectile.GetAlpha((base.Projectile.hide && Main.netMode == 1) ? Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16) : lightColor);
		SpriteEffects effects = ((base.Projectile.spriteDirection >= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		float scale = ((float)(int)Main.mouseTextColor / 200f - 0.35f) * 0.4f + 0.9f;
		scale *= base.Projectile.scale;
		if (this.auraTrail || this.SHADOWMUTANTREAL > 0f)
		{
			Main.EntitySpriteDraw(texture2D14, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, Color.White * base.Projectile.Opacity, base.Projectile.rotation, origin2, scale, effects, 0);
		}
		if (this.auraTrail)
		{
			Color color25 = Color.White * base.Projectile.Opacity;
			color25.A = 200;
			for (float i = 0f; i < (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i += 0.25f)
			{
				Color color27 = color25 * 0.5f;
				color27 *= ((float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
				int max0 = (int)i - 1;
				if (max0 < 0)
				{
					max0 = 0;
				}
				float num165 = base.Projectile.oldRot[max0];
				Vector2 center = Vector2.Lerp(base.Projectile.oldPos[(int)i], base.Projectile.oldPos[max0], 1f - i % 1f);
				center += base.Projectile.Size / 2f;
				Main.EntitySpriteDraw(texture2D14, center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, effects, 0);
			}
			Main.EntitySpriteDraw(aura, -16f * Vector2.UnitY + base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)auraRectangle, color25, base.Projectile.rotation, auraRectangle.Size() / 2f, scale, effects, 0);
		}
		else
		{
			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
			{
				Color color27 = color26;
				color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
				Vector2 value4 = base.Projectile.oldPos[i];
				float num165 = base.Projectile.oldRot[i];
				Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, base.Projectile.scale, effects, 0);
			}
		}
		color26 = Color.Lerp(color26, Color.Black, this.SHADOWMUTANTREAL);
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color26, base.Projectile.rotation, origin2, base.Projectile.scale, effects, 0f);
		if (this.sansEye)
		{
			Color color = new Color(100, 255, 230);
			bool num166 = base.Projectile.ai[0] == -1f;
			float effectiveTime = base.Projectile.ai[0];
			float rotation = (float)Math.PI * 2f * base.Projectile.localAI[1];
			float modifier = Math.Min(1f, (float)Math.Sin(Math.PI * (double)effectiveTime / 120.0) * 2f);
			float opacity = (num166 ? 1f : Math.Min(1f, modifier * 2f));
			float sansScale = (num166 ? (base.Projectile.scale * Main.cursorScale * 0.8f * Main.rand.NextFloat(0.75f, 1.25f)) : (base.Projectile.scale * modifier * Main.cursorScale * 1.25f));
			Texture2D star = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/LifeStar", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			Rectangle rect = new Rectangle(0, 0, star.Width, star.Height);
			Vector2 origin = new Vector2((float)(star.Width / 2) + sansScale, (float)(star.Height / 2) + sansScale);
			Vector2 drawPos = base.Projectile.Center;
			drawPos.X += 8 * base.Projectile.spriteDirection;
			drawPos.Y -= 11f;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
			Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rect, color * opacity, rotation, origin, sansScale, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rect, Color.White * opacity * 0.75f, rotation, origin, sansScale, SpriteEffects.None, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
		}
		return false;
	}
}
