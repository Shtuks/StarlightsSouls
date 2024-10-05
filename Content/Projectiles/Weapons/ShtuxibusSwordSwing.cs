using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using System;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using ssm.Content.Buffs;
using ssm.Content.Buffs.Minions;
using ssm.Content.Projectiles;
using ssm.Content.Projectiles.Deathrays;
using ssm.Content.Projectiles.Weapons;
using ssm.Content.Projectiles.Minions;

namespace ssm.Content.Projectiles.Weapons
{
    public class ShtuxibusSwordSwing : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/Weapons/ShtuxibusSpear";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.damage = 745000;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.MiniRetinaLaser;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
            Projectile.scale = 3f;
            Projectile.timeLeft = 240;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.PI / 4f + (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            int num = (int)Projectile.ai[1];
            Projectile.ai[0] += 1f;
            if (!(Projectile.ai[0] > (float)num))
            {
                return;
            }
            if ((Projectile.localAI[0] -= 1f) < 0f)
            {
                Projectile.localAI[0] = 3f;
                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 val = Utils.RotatedBy(Vector2.Normalize(((Entity)Projectile).velocity), Math.PI / 2.0, default(Vector2));
                    int numu = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)Projectile).Center, 16f * val, ModContent.ProjectileType<ShtuxibusSpearThrow>(), Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 1f, 0f);
                    if (numu != 1000)
                    {
                        Projectile.DamageType = DamageClass.Melee;
                    }
                    numu = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)Projectile).Center, 16f * -val, ModContent.ProjectileType<ShtuxibusSpearThrow>(), Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 1f, 0f);
                    if (numu != 1000)
                    {
                        Projectile.DamageType = DamageClass.Melee;
                    }
                }
                Projectile.rotation = (float)Math.PI / 4f + (float)Math.Atan2(((Entity)Projectile).velocity.Y, ((Entity)Projectile).velocity.X);
                int num22 = (int)Projectile.ai[1];
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] > (float)num22)
                {
                    Projectile.ai[0] = num22;
                    int num33 = HomeOnTarget();
                    if (num33 != -1)
                    {
                        NPC val2 = Main.npc[num33];
                        Vector2 val3 = ((Entity)Projectile).DirectionTo(((Entity)val2).Center) * 120f;
                        ((Entity)Projectile).velocity = Vector2.Lerp(((Entity)Projectile).velocity, val3, 0.04f);
                    }
                }
            }
            Projectile.ai[0] = num;
            int num2 = HomeOnTarget();
            if (num2 != -1)
            {
                NPC val = Main.npc[num2];
                Vector2 val2 = Projectile.DirectionTo(val.Center) * 120f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, val2, 0.05f);
            }
            if (Main.rand.Next(7) == 0)
            {
                //Projectile.Center;
                int num3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 267, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
                Main.dust[num3].noGravity = true;
                Dust obj = Main.dust[num3];
                obj.velocity *= 1.7f;
                Main.dust[num3].noLight = true;
                int num4 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 267, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
                Dust obj2 = Main.dust[num4];
                obj2.velocity *= 0.7f;
                Main.dust[num4].noGravity = true;
                Main.dust[num4].noLight = true;
            }
            Projectile projectile = Projectile;
            projectile.velocity = projectile.velocity * 1.07f;
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
        private int HomeOnTarget()
        {
            int num = -1;
            for (int i = 0; i < 200; i++)
            {
                NPC val = Main.npc[i];
                if (val.CanBeChasedBy(Projectile, false))
                {
                    _ = (val).wet;
                    float num2 = ShtunUtils.FindClosestHostileNPC(Projectile.Center, 1000);
                    if (num2 <= 9000f)
                    {
                        num = i;
                    }
                }
            }
            return num;
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
            // 
        }
        public override void OnKill(int timeLeft)
        {
            float num = 0f;
            for (int i = 0; i < 1000; i++)
            {
                if ((Main.projectile[i]).active && !Main.projectile[i].hostile && Main.projectile[i].owner == Projectile.owner && Main.projectile[i].minion)
                {
                    num += Main.projectile[i].minionSlots;
                }
            }
            float num2 = Main.player[Projectile.owner].maxMinions - num;
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            if (num2 > 5f)
            {
                num2 = 5f;
            }
            float num3 = num2 + 3;
            for (int j = 0; j < num3; j++)
            {
                Vector2 spawnPos = Projectile.Center;
                NPC minionAttackTargetNpc = Projectile.OwnerMinionAttackTargetNPC;
                if (minionAttackTargetNpc != null && Projectile.ai[0] != minionAttackTargetNpc.whoAmI && minionAttackTargetNpc.CanBeChasedBy())
                    Projectile.ai[0] = minionAttackTargetNpc.whoAmI;

                NPC npc = Main.npc[(int)Projectile.ai[0]];
                Vector2 vel = 600f * Vector2.Normalize(Projectile.velocity);
                Vector2 vel2 = vel.RotatedBy(MathHelper.TwoPi / num3 * j);
                Vector2 vel3 = vel.RotatedBy(MathHelper.TwoPi / num3 * j + 0.5);
                //	Vector2 val = 600f * -  new Vector2(0, 0).RotatedBy(Projectile.rotation, MathHelper.TwoPi * 2.0 / num3 * j + Projectile.localAI[1]);
                Vector2 val2 = 2f * vel2 / 90f;
                Vector2 val3 = 2f * vel2 / 90f;
                float num4 = (val2).Length() / 90f;
                float num5 = (val2).Length() / 90f;

                //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, val2 + Projectile.DirectionTo(npc.Center) * 30, ModContent.ProjectileType<ProtoProjectileAim>(), Projectile.damage, Projectile.knockBack,Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, val2 + Projectile.DirectionTo(npc.Center) * 30, ModContent.ProjectileType<GiantDeathrayULTRA>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, val2 + Projectile.DirectionTo(npc.Center) * 30, ModContent.ProjectileType<StolenArrow2>(), Projectile.damage, Projectile.knockBack,Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, val2 + Projectile.DirectionTo(npc.Center) * 30, ModContent.ProjectileType<DestictorProjectile>(), Projectile.damage, Projectile.knockBack,Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, val2 + Projectile.DirectionTo(npc.Center) * 30, ModContent.ProjectileType<ExoStolen>(), Projectile.damage, Projectile.knockBack,Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, 14f * Utils.RotatedBy(Vector2.get_UnitY(), Math.PI * 2.0 / (double)num3 * ((double)j + 0.5) + (double)((ModProjectile)this).get_projectile().localAI[1], default(Vector2)), ((ModProjectile)this).get_mod().ProjectileType("ExoStolen"), ((ModProjectile)this).get_projectile().damage, ((ModProjectile)this).get_projectile().knockBack, ((ModProjectile)this).get_projectile().owner, -1f, 45f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * Projectile.Opacity;
        }
    }
}

// public override void AI()
// {

// 	if ((Projectile.localAI[0] -= 1f) < 0f)
// 	{
// 		Projectile.localAI[0] = 3f;
// 		// if (Projectile.owner == Main.myPlayer)
// 		// {
// 		// 	Vector2 val = Utils.RotatedBy(Vector2.Normalize(((Entity)Projectile).velocity), Math.PI / 2.0, default(Vector2));
// 		// 	int num = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)Projectile).Center, 16f * val, ModContent.ProjectileType<SpearLolProjectile>(), Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 1f, 0f);
// 		// 	if (num != 1000)
// 		// 	{
// 		// 	   Projectile.DamageType = DamageClass.Melee;
// 		// 	}
// 		// 	num = Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)Projectile).Center, 16f * -val, ModContent.ProjectileType<SpearLolProjectile>(), Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 1f, 0f);
// 		// 	if (num != 1000)
// 		// 	{
// 		// 			   Projectile.DamageType = DamageClass.Melee;
// 		// 	}
// 		// }
// 	}
// 	Projectile.rotation = (float)Math.PI / 4f + (float)Math.Atan2(((Entity)Projectile).velocity.Y, ((Entity)Projectile).velocity.X);
// 	int num2 = (int)Projectile.ai[1];
// 	Projectile.ai[0] += 1f;
// 	if (Projectile.ai[0] > (float)num2)
// 	{
// 		Projectile.ai[0] = num2;
// 		int num3 = HomeOnTarget();
// 		if (num3 != -1)
// 		{
// 			NPC val2 = Main.npc[num3];
// 			Vector2 val3 = ((Entity)Projectile).DirectionTo(((Entity)val2).Center) * 120f;
// 			((Entity)Projectile).velocity = Vector2.Lerp(((Entity)Projectile).velocity, val3, 0.04f);
// 		}
// 	}
// 	if (Main.rand.Next(7) == 0)
// 	{
// 		//((Entity)Projectile).Center;
// 		int num4 = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 267, ((Entity)Projectile).velocity.X * 0.1f, ((Entity)Projectile).velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
// 		Main.dust[num4].noGravity = true;
// 		Dust obj = Main.dust[num4];
// 		obj.velocity *= 1.7f;
// 		Main.dust[num4].noLight = true;
// 		int num5 = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 267, ((Entity)Projectile).velocity.X * 0.1f, ((Entity)Projectile).velocity.Y * 0.1f, 100, Main.DiscoColor, 1.9f);
// 		Dust obj2 = Main.dust[num5];
// 		obj2.velocity *= 0.7f;
// 		Main.dust[num5].noGravity = true;
// 		Main.dust[num5].noLight = true;
// 	}
// 	Projectile projectile = Projectile;
// 	((Entity)projectile).velocity = ((Entity)projectile).velocity * 1.07f;
// 	Projectile projectile2 = Projectile;
// 	if (++projectile2.frameCounter >= 6)
// 	{
// 		Projectile.frameCounter = 0;
// 		Projectile projectile3 = Projectile;
// 		if (++projectile3.frame >= 30)
// 		{
// 			Projectile.frame = 0;
// 		}
// 	}
// 	Projectile.netUpdate = true;
// 	for (int i = 0; i < 200; i++)
// 	{
// 		NPC val4 = Main.npc[i];
// 		if (val4 != null && ((Entity)val4).active && !val4.friendly && ((object)val4).GetType().GetMethod("NPCLoot") != null)
// 		{

// 				val4.NPCLoot();

// 			((Entity)val4).active = false;
// 			val4.dontTakeDamage = false;
// 			val4.dontTakeDamageFromHostiles = false;
// 			val4.immortal = false;
// 			val4.life = 0;
// 			val4.HitEffect(0, 10.0);
// 			val4.checkDead();
// 		}
// 	}
// }

// // public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
// // {
// // 	crit = true;
// // }



