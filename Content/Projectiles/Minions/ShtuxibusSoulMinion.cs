using FargowiltasSouls.Content.Projectiles;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using ssm.Content.Buffs;
using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using ssm.Content.Projectiles.Minions;
using ssm.Content.Projectiles.Weapons;

namespace ssm.Content.Projectiles.Minions
{
    public class ShtuxibusSoulMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 50;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.scale = 1f;
            _ = Main.player[Projectile.owner];
            NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
            int num = HomeOnTarget();
            if (ownerMinionAttackTargetNPC != null && Projectile.ai[0] != (float)((Entity)ownerMinionAttackTargetNPC).whoAmI && ownerMinionAttackTargetNPC.CanBeChasedBy((object)Projectile, false))
            {
                num = ((Entity)ownerMinionAttackTargetNPC).whoAmI;
            }
            if ((Projectile.ai[0] += 1f) == 50f)
            {
                Projectile.netUpdate = true;
                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 val = Utils.RotatedBy(new Vector2(0f, -275f), Math.PI / 4.0 * (double)Projectile.spriteDirection, default(Vector2));
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), ((Entity)Projectile).Center + val, Vector2.Zero, ModContent.ProjectileType<ShtuxibusSoulAttack>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, (float)((Entity)Projectile).whoAmI);
                }
            }
            else if (Projectile.ai[0] < 100f)
            {
                Vector2 val2;
                if (num != -1 && Main.npc[num].CanBeChasedBy((object)Projectile, false))
                {
                    val2 = ((Entity)Main.npc[num]).Center;
                    ((Entity)Projectile).direction = (Projectile.spriteDirection = ((((Entity)Projectile).Center.X > val2.X) ? 1 : (-1)));
                    val2.X += 500 * ((Entity)Projectile).direction;
                    val2.Y -= 200f;
                }
                else
                {
                    ((Entity)Projectile).direction = (Projectile.spriteDirection = -((Entity)Main.player[Projectile.owner]).direction);
                    val2 = ((Entity)Main.player[Projectile.owner]).Center + new Vector2((float)(100 * ((Entity)Projectile).direction), -100f);
                }
                if (((Entity)Projectile).Distance(val2) > 50f)
                {
                    Movement(val2, 1f);
                }
            }
            else if (Projectile.ai[0] == 99f || Projectile.ai[0] == 100f)
            {
                Projectile.netUpdate = true;
                if (Projectile.owner == Main.myPlayer)
                {
                    Vector2 val3 = ((num == -1 || !Main.npc[num].CanBeChasedBy((object)Projectile, false)) ? Main.MouseWorld : (((Entity)Main.npc[num]).Center + ((Entity)Main.npc[num]).velocity * 10f));
                    ((Entity)Projectile).direction = (Projectile.spriteDirection = ((((Entity)Projectile).Center.X > val3.X) ? 1 : (-1)));
                    val3.X += 275 * ((Entity)Projectile).direction;
                    if (Projectile.ai[0] == 100f)
                    {
                        ((Entity)Projectile).velocity = (val3 - ((Entity)Projectile).Center) / (float)Projectile.timeLeft;
                        Projectile projectile = Projectile;
                        ((Entity)projectile).position = ((Entity)projectile).position + ((Entity)Projectile).velocity;
                    }
                }
            }
            Projectile projectile2 = Projectile;
            if (++projectile2.frameCounter > 4)
            {
                Projectile.frameCounter = 0;
                Projectile projectile3 = Projectile;
                if (++projectile3.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
            int num2 = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 267, ((Entity)Projectile).velocity.X / 2f, ((Entity)Projectile).velocity.Y / 2f, 0, Main.DiscoColor, 1.5f);
            Main.dust[num2].noGravity = true;
        }
        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (((Entity)Projectile).Center.X < targetPos.X)
            {
                ((Entity)Projectile).velocity.X += speedModifier;
                if (((Entity)Projectile).velocity.X < 0f)
                {
                    ((Entity)Projectile).velocity.X += speedModifier * 2f;
                }
            }
            else
            {
                ((Entity)Projectile).velocity.X -= speedModifier;
                if (((Entity)Projectile).velocity.X > 0f)
                {
                    ((Entity)Projectile).velocity.X -= speedModifier * 2f;
                }
            }
            if (((Entity)Projectile).Center.Y < targetPos.Y)
            {
                ((Entity)Projectile).velocity.Y += speedModifier;
                if (((Entity)Projectile).velocity.Y < 0f)
                {
                    ((Entity)Projectile).velocity.Y += speedModifier * 2f;
                }
            }
            else
            {
                ((Entity)Projectile).velocity.Y -= speedModifier;
                if (((Entity)Projectile).velocity.Y > 0f)
                {
                    ((Entity)Projectile).velocity.Y -= speedModifier * 2f;
                }
            }
            if (Math.Abs(((Entity)Projectile).velocity.X) > 24f)
            {
                ((Entity)Projectile).velocity.X = 24 * Math.Sign(((Entity)Projectile).velocity.X);
            }
            if (Math.Abs(((Entity)Projectile).velocity.Y) > 24f)
            {
                ((Entity)Projectile).velocity.Y = 24 * Math.Sign(((Entity)Projectile).velocity.Y);
            }
        }
        private int HomeOnTarget()
        {
            NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy((object)Projectile, false))
            {
                return ((Entity)ownerMinionAttackTargetNPC).whoAmI;
            }
            int num = -1;
            for (int i = 0; i < 200; i++)
            {
                NPC val = Main.npc[i];
                if (val.CanBeChasedBy((object)Projectile, false))
                {
                    float num2 = ((Entity)Projectile).Distance(((Entity)val).Center);
                    if (num2 <= 2000f && (num == -1 || ((Entity)Projectile).Distance(((Entity)Main.npc[num]).Center) > num2))
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

            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);

            SpriteEffects effects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                Color color27 = color26 * 0.5f;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 value4 = Projectile.oldPos[i];
                float num165 = Projectile.oldRot[i];
                Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, effects, 0);
            }
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }

    }
}
