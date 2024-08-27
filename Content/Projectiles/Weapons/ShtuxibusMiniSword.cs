using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Content.Projectiles;

namespace ssm.Content.Projectiles.Weapons
{
	public class ShtuxibusMiniSword : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			 	Main.projFrames[Type] = 10;
            	ProjectileID.Sets.MinionShot[Projectile.type] = true;
            	ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			 // ProjectileID.Sets.Homing[Projectile.type] = true;
				ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
				ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
				ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 100;
			Projectile.height = 100;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 230;
			Projectile.alpha = 100;
			Projectile.scale = 1.2f;
			Projectile.light = 0.1f;
			Projectile.ignoreWater = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
			//Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToMutantBomb = true;
			//Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToGuttedHeart = true;
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
			//  Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().noInteractionWithNPCImmunityFrames = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		
		}
private int HomeOnTarget()
		{
			int num = -1;
			for (int i = 0; i < 200; i++)
			{
				NPC val = Main.npc[i];
				if (val.CanBeChasedBy((object)Projectile, false))
				{
					_ = ((Entity)val).wet;
					float num2 = ((Entity)Projectile).Distance(((Entity)val).Center);
					if (num2 <= 10000f && (num == -1 || ((Entity)Projectile).Distance(((Entity)Main.npc[num]).Center) > num2))
					{
						num = i;
					}
				}
			}
			return num;
		}
		  public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = SpriteEffects.None;

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }

		public override void AI()
		{
			Projectile.rotation = (float)Math.PI / 4f + (float)Math.Atan2(((Entity)Projectile).velocity.Y, ((Entity)Projectile).velocity.X);
			int num = (int)Projectile.ai[1];
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > (float)num)
			{
				Projectile.ai[0] = num;
				int num2 = HomeOnTarget();
				if (num2 != -1)
				{
					NPC val = Main.npc[num2];
					Vector2 val2 = ((Entity)Projectile).DirectionTo(((Entity)val).Center) * 120f;
					((Entity)Projectile).velocity = Vector2.Lerp(((Entity)Projectile).velocity, val2, 0.04f);
				}
			}
			if (Main.rand.Next(7) == 0)
			{
			//	Projectile.Center;
				int num3 = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 267, ((Entity)Projectile).velocity.X * 0.1f, ((Entity)Projectile).velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
				Main.dust[num3].noGravity = true;
				Dust obj = Main.dust[num3];
				obj.velocity *= 1.7f;
				Main.dust[num3].noLight = true;
				int num4 = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 267, ((Entity)Projectile).velocity.X * 0.1f, ((Entity)Projectile).velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
				Dust obj2 = Main.dust[num4];
				obj2.velocity *= 0.7f;
				Main.dust[num4].noGravity = true;
				Main.dust[num4].noLight = true;
			}
			Projectile projectile = Projectile;
			((Entity)projectile).velocity = ((Entity)projectile).velocity * 1.07f;
			Projectile projectile2 = Projectile;
			if (++projectile2.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile projectile3 = Projectile;
				if (++projectile3.frame >= 10)
				{
					Projectile.frame = 0;
				}
			}
			Projectile.netUpdate = true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 0;
			if (Projectile.owner == Main.myPlayer)
			{
				int num = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)target).position + new Vector2((float)Main.rand.Next(((Entity)target).width), (float)Main.rand.Next(((Entity)target).height)), Vector2.Zero, ModContent.ProjectileType<ProtoProjectile>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
				if (num < 1000)
				{
					  Projectile.DamageType = DamageClass.Melee;
				}
			}
			target.life = 0;
			target.HitEffect(0, 10.0);
			target.checkDead();
			((Entity)target).active = false;
			target.NPCLoot();
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item90, ((Entity)Projectile).position);
			Vector2 val = new Vector2(500f, 500f);
			Vector2 center = ((Entity)Projectile).Center;
			center.X -= val.X / 2f;
			center.Y -= val.Y / 2f;
			for (int i = 0; i < 45; i++)
			{
				int num = Dust.NewDust(center, (int)val.X, (int)val.Y, 31, 0f, 0f, 100, default(Color), 1.5f);
				Dust obj = Main.dust[num];
				obj.velocity *= 1.4f;
			}
			for (int j = 0; j < 60; j++)
			{
				int num2 = Dust.NewDust(center, (int)val.X, (int)val.Y, 6, 0f, 0f, 100, default(Color), 3.5f);
				Main.dust[num2].noGravity = true;
				Dust obj2 = Main.dust[num2];
				obj2.velocity *= 7f;
				num2 = Dust.NewDust(center, (int)val.X, (int)val.Y, 6, 0f, 0f, 100, default(Color), 1.5f);
				Dust obj3 = Main.dust[num2];
				obj3.velocity *= 3f;
		}
		}
	}
}
