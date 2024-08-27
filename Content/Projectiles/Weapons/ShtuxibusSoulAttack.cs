using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using ssm.Content.Buffs;
using ssm.Content.Buffs.Minions;
using ssm.Content.Projectiles.Minions;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Weapons
{
	public class ShtuxibusSoulAttack : ModProjectile
	{
		public override string Texture => "ssm/Content/Projectiles/Weapons/ShtuxibusSwordSwing";

		public override void SetStaticDefaults()
		{
        	ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 90;
			Projectile.height = 90;
			Projectile.friendly = true;
		    Projectile.DamageType = DamageClass.Summon;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 65;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 3f;
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
		//	Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToMutantBomb = true;
		//	Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().ImmuneToGuttedHeart = true;
			Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
		//	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().TimeFreezeImmune = true;
		//	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().ImmuneToMutantBomb = true;
		    Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().CanSplit = false;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().noInteractionWithNPCImmunityFrames = true;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
		//	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().ImmuneToGuttedHeart = true;
		//	Projectile.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
		}
        public override void AI()
        {
            //the important part
            int ai1 = (int)Projectile.ai[1];
            int byIdentity = ShtunUtils.GetProjectileByIdentity(Projectile.owner, ai1, ModContent.ProjectileType<ShtuxibusSoulMinion>());
            if (byIdentity != -1)
            {
                Projectile devi = Main.projectile[byIdentity];
                if (Projectile.timeLeft > 15)
                {
                    Vector2 offset = new Vector2(0, -360).RotatedBy(Math.PI / 4 * devi.spriteDirection);
                    Projectile.Center = devi.Center + offset;
                    Projectile.rotation = (float)Math.PI / 4 * devi.spriteDirection - (float)Math.PI / 4;
                }
                else //swinging down
                {
                    if (Projectile.timeLeft == 15) //confirm facing the right direction with right offset
                        Projectile.rotation = (float)Math.PI / 4 * devi.spriteDirection - (float)Math.PI / 4;

                    Projectile.rotation -= (float)Math.PI / 15 * devi.spriteDirection * 0.75f;
                    Vector2 offset = new Vector2(0, -360).RotatedBy(Projectile.rotation + (float)Math.PI / 4);
                    Projectile.Center = devi.Center + offset;
                }

                Projectile.spriteDirection = -devi.spriteDirection;

                Projectile.localAI[1] = devi.velocity.ToRotation();

                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    //if (Projectile.owner == Main.myPlayer)
                    //   Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Eat>(), 0, 0f, Main.myPlayer, -1, -14);
                    SoundEngine.PlaySound(SoundID.Item92, Projectile.Center);
                }
            }
            else if (Projectile.owner == Main.myPlayer && Projectile.timeLeft < 60)
            {
                Projectile.Kill();
                return;
            }
        }

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{

			target.immune[Projectile.owner] = 0;
			if (Projectile.owner == Main.myPlayer)
			{
				//int num = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)target).position + new Vector2((float)Main.rand.Next(((Entity)target).width), (float)Main.rand.Next(((Entity)target).height)), Vector2.Zero, ModContent.ProjectileType<ApocalypseProjectile>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
				//if (num < 1000)
				//{
				//	  Projectile.DamageType = DamageClass.Melee;
				//}
			}
			target.life = 0;
			target.HitEffect(0, 10.0);
			target.checkDead();
			((Entity)target).active = false;
			target.NPCLoot();
		}

		public override void OnKill(int timeleft)
		{
			SoundEngine.PlaySound(SoundID.Item92, ((Entity)Projectile).Center);
			if (Projectile.owner != Main.myPlayer)
			{
				return;
			}
			float num = 0f;
			for (int i = 0; i < 1000; i++)
			{
				if (((Entity)Main.projectile[i]).active && !Main.projectile[i].hostile && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].minion)
				{
					num += Main.projectile[i].minionSlots;
				}
			}
			float num2 = (float)Main.player[Projectile.owner].maxMinions - num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			if (num2 > 5f)
			{
				num2 = 5f;
			}
		int max = 8;
                for (int i = 0; i < max; i++)
			{
                Vector2 val = 600f * -Vector2.UnitY.RotatedBy(2 * Math.PI / max * i + Projectile.localAI[1]);
				Vector2 val2 = 2f * val / 90f;
				float num4 = (0f -  val2.Length()) / 90f;
				float num5 = Utils.ToRotation(val2) + (float)Math.PI / 2f;
				//Projectile.NewProjectile(Projectile.GetSource_FromThis(),((Entity)Projectile).Center, val2, ModContent.ProjectileType<Eat>(), Projectile.damage, Projectile.knockBack, Projectile.owner, num5, num4);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),((Entity)Projectile).Center, val2, ModContent.ProjectileType<ShtuxibusSwordSwing>(), Projectile.damage, Projectile.knockBack, Projectile.owner, num5, num4);
                //Projectile.NewProjectile(Projectile.GetSource_FromThis(),((Entity)Projectile).Center, 14f * Vector2.UnitY.RotatedBy(2 * Math.PI * 2.0 / max * i + 0.5 + Projectile.localAI[1]), ModContent.ProjectileType<ApocalypseProjectile>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -1f, 45f);
			}
		}
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);

            float rotationOffset = Projectile.spriteDirection > 0 ? 0 : (float)Math.PI / 2;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            Terraria.Graphics.Shaders.ArmorShaderData shader = Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderFromItemId(ItemID.PhaseDye);
            shader.Apply(Projectile, new Terraria.DataStructures.DrawData?());

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                Color color27 = new Color(255, 255, 255, 50) * 0.5f;
                if (Projectile.timeLeft > 15)
                    color27 *= 0.5f;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 value4 = Projectile.oldPos[i];
                float num165 = Projectile.oldRot[i] + rotationOffset;
                Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, effects, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects, 0);
            return false;
        }
		
    }
}
