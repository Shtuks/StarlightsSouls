using System;
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
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo;

public class MutantSpearSpin : ModProjectile
{
	private bool predictive;

	private int direction = 1;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 152;
		base.Projectile.height = 152;
		base.Projectile.aiStyle = -1;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.CooldownSlot = 1;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		base.Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = ((!Main.rand.NextBool()) ? 1 : (-1));
			base.Projectile.timeLeft = (int)base.Projectile.ai[1];
		}
		NPC mutant = Main.npc[(int)base.Projectile.ai[0]];
		if (mutant.active && mutant.type == ModContent.NPCType<MutantEX>())
		{
			base.Projectile.Center = mutant.Center;
			this.direction = mutant.direction;
			if (mutant.ai[0] == 4f || mutant.ai[0] == 13f || mutant.ai[0] == 21f)
			{
				base.Projectile.rotation += 0.4586267f * base.Projectile.localAI[1];
				if ((base.Projectile.localAI[0] += 1f) > 8f)
				{
					base.Projectile.localAI[0] = 0f;
					if (Main.netMode != 1 && base.Projectile.Distance(Main.player[mutant.target].Center) > 360f)
					{
						Vector2 speed = Vector2.UnitY.RotatedByRandom(Math.PI / 2.0) * Main.rand.NextFloat(6f, 9f);
						if (mutant.Center.Y < Main.player[mutant.target].Center.Y)
						{
							speed *= -1f;
						}
						float ai1 = base.Projectile.timeLeft + Main.rand.Next(base.Projectile.timeLeft / 2);
						Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), base.Projectile.position + Main.rand.NextVector2Square(0f, base.Projectile.width), speed, ModContent.ProjectileType<MutantEyeHoming>(), base.Projectile.damage, 0f, base.Projectile.owner, (float)mutant.target, ai1);
					}
				}
				if (base.Projectile.timeLeft % 20 == 0)
				{
					SoundEngine.PlaySound(SoundID.Item1, (Vector2?)base.Projectile.Center);
				}
				if (mutant.ai[0] == 13f)
				{
					this.predictive = true;
				}
				base.Projectile.alpha = 0;
			}
			else
			{
				base.Projectile.alpha = 255;
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public virtual void OnHitPlayer(Player target, int damage, bool crit)
	{
		Projectile.NewProjectile(Terraria.Entity.InheritSource(base.Projectile), target.Center + Main.rand.NextVector2Circular(100f, 100f), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), 0, 0f, base.Projectile.owner, 0f, 0f);
		target.GetModPlayer<FargoSoulsPlayer>().MaxLifeReduction += 100;
		target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
		target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
		target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
	}

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White * base.Projectile.Opacity;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
        int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
        int y3 = num156 * base.Projectile.frame;
        Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;
        Color color26 = lightColor;
        color26 = base.Projectile.GetAlpha(color26);
        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
        {
            Color color27 = color26 * 0.5f;
            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
            Vector2 value4 = base.Projectile.oldPos[i];
            float num165 = base.Projectile.oldRot[i];
            Main.EntitySpriteDraw(texture2D13, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, color27, num165, origin2, base.Projectile.scale, SpriteEffects.None);
        }
        Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
        if (base.Projectile.ai[1] > 0f)
        {
            Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpearAimGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            float modifier = (float)base.Projectile.timeLeft / base.Projectile.ai[1];
            Color glowColor = new Color(51, 255, 191, 210);
            if (this.predictive)
            {
                glowColor = new Color(0, 0, 255, 210);
            }
            glowColor *= 1f - modifier;
            float glowScale = base.Projectile.scale * 8f * modifier;
            Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glow.Bounds, glowColor, 0f, glow.Bounds.Size() / 2f, glowScale, SpriteEffects.None);
        }
        return false;
    }
}
