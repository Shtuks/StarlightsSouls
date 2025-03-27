using System;
using System.IO;
using FargowiltasSouls;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo;

public class MutantSpearThrown : MutantSpearAttack
{
	private NPC npc;

	protected float scaletimer;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 0;
		base.CooldownSlot = 1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (projHitbox.Intersects(targetHitbox))
		{
			return true;
		}
		float dummy = 0f;
		Vector2 offset = 200f / 2f * base.Projectile.scale * (base.Projectile.rotation - MathHelper.ToRadians(135f)).ToRotationVector2();
		Vector2 end = base.Projectile.Center - offset;
		Vector2 tip = base.Projectile.Center + offset;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), end, tip, 8f * base.Projectile.scale, ref dummy))
		{
			return true;
		}
		return false;
	}

	public override void OnSpawn(IEntitySource source)
	{
		if (source is EntitySource_Parent { Entity: NPC sourceNPC })
		{
			this.npc = sourceNPC;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write7BitEncodedInt((this.npc != null) ? this.npc.whoAmI : (-1));
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		this.npc = FargoSoulsUtil.NPCExists(reader.Read7BitEncodedInt());
	}

	public override void AI()
	{
		if ((base.Projectile.localAI[0] -= 1f) < 0f)
		{
			base.Projectile.localAI[0] = 3f;
			for (int i = -1; i <= 1; i += 2)
			{
				if (Main.netMode != 1)
				{
					int p = Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.Center, 8f * Vector2.Normalize(base.Projectile.velocity).RotatedBy((float)Math.PI / 2f * (float)i), ModContent.ProjectileType<MutantSphereSmall>(), base.Projectile.damage, 0f, base.Projectile.owner, -1f, 0f);
					if (p != 1000)
					{
						Main.projectile[p].timeLeft = 15;
					}
				}
			}
		}
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			SoundEngine.PlaySound(SoundID.Item1, (Vector2?)base.Projectile.Center);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
		this.scaletimer += 1f;
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), target.Center + Main.rand.NextVector2Circular(100f, 100f), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0f, base.Projectile.owner, 0f, 0f);
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
		if (this.npc == null)
		{
			return;
		}
		int totalHealPerHit = this.npc.lifeMax / 100 * 5;
		for (int i = 0; i < 20; i++)
		{
			Vector2 vel = Main.rand.NextFloat(2f, 9f) * -Vector2.UnitY.RotatedByRandom(6.2831854820251465);
			float ai0 = this.npc.whoAmI;
			float ai1 = vel.Length() / (float)Main.rand.Next(30, 90);
			int healPerOrb = (int)((float)(totalHealPerHit / 20) * Main.rand.NextFloat(0.95f, 1.05f));
			if (target.whoAmI == Main.myPlayer && target.ownedProjectileCounts[ModContent.ProjectileType<MutantHeal>()] < 10)
			{
				Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), target.Center, vel, ModContent.ProjectileType<MutantHeal>(), healPerOrb, 0f, Main.myPlayer, ai0, ai1);
				SoundEngine.PlaySound(SoundID.Item27, (Vector2?)target.Center);
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White * base.Projectile.Opacity;
	}

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        int rect1 = glow.Height / Main.projFrames[base.Projectile.type];
        int rect2 = rect1 * base.Projectile.frame;
        Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
        Vector2 gloworigin2 = glowrectangle.Size() / 2f;
        Color glowcolor = Color.Lerp(new Color(51, 255, 191, 0), Color.Transparent, 0.82f);
        Color glowcolor2 = Color.Lerp(new Color(194, 255, 242, 0), Color.Transparent, 0.6f);
        glowcolor = Color.Lerp(glowcolor, glowcolor2, 0.5f + (float)Math.Sin(this.scaletimer / 7f) / 2f);
        Vector2 drawCenter = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 28f;
        for (int i = 0; i < 3; i++)
        {
            Vector2 drawCenter2 = drawCenter + (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 20f).RotatedBy((float)Math.PI / 5f - (float)i * (float)Math.PI / 5f);
            drawCenter2 -= base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 20f;
            float scale = base.Projectile.scale;
            scale += (float)Math.Sin(this.scaletimer / 7f) / 7f;
            Main.EntitySpriteDraw(glow, drawCenter2 - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, glowcolor, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.25f, SpriteEffects.None);
        }
        for (int i = ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - 1; i > 0; i--)
        {
            Color color27 = glowcolor;
            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            float scale = base.Projectile.scale * (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            scale += (float)Math.Sin(this.scaletimer / 7f) / 7f;
            Vector2 value4 = base.Projectile.oldPos[i] - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14f;
            Main.EntitySpriteDraw(glow, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, color27, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.25f, SpriteEffects.None);
        }
        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
        int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
        int y3 = num156 * base.Projectile.frame;
        Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;
        Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
    }
}
