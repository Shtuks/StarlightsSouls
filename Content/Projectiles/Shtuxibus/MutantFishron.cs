
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantFishron : ModProjectile
    {
        private bool spawned;
        public override string Texture => "ssm/Content/Projectiles/Shtuxibus/OldDuke";
        int p = -1;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Projectile.alpha = 100;
            CooldownSlot = 1;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.hurtCooldowns[1] == 0;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(p);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            p = reader.ReadInt32();
        }
        public override bool? CanDamage()
        {
            return Projectile.localAI[0] > 85;
        }
        public override bool PreAI()
        {
            if (Projectile.localAI[0] > 85)
            {
                int num22 = 5;
                for (int index1 = 0; index1 < num22; ++index1)
                {
                    Vector2 vector2_1 = (Vector2.Normalize(Projectile.velocity) * new Vector2((Projectile.width + 50) / 2f, Projectile.height) * 0.75f).RotatedBy((index1 - (num22 / 2 - 1)) * Math.PI / num22, new Vector2()) + Projectile.Center;
                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Vector2 vector2_3 = vector2_2;
                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
                    Main.dust[index2].velocity /= 4f;
                    Main.dust[index2].velocity -= Projectile.velocity;
                }
            }
            return true;
        }

        public override void AI()
        {
            if (!spawned)
            {
                spawned = true;
                Projectile.frame = Main.rand.Next(11);
            }
            if (Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = 1;
                SoundEngine.PlaySound(SoundID.Zombie20, Projectile.Center);
                p = ShtunUtils.AnyBossAlive() ? Main.npc[ShtunNpcs.boss].target : Player.FindClosest(Projectile.Center, 0, 0);
                Projectile.netUpdate = true;
            }

            if (++Projectile.localAI[0] > 85)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.frameCounter = 5;
                Projectile.frame = 6;
            }
            else //preparing to dash
            {
                int ai0 = p;
                //const float moveSpeed = 1f;
                if (Projectile.localAI[0] == 85) //just about to dash
                {
                    Projectile.velocity = Main.player[ai0].Center - Projectile.Center;
                    Projectile.velocity.Normalize();
                    Projectile.velocity *= Projectile.type == ModContent.ProjectileType<MutantFishron>() ? 24f : 20f;
                    Projectile.rotation = Projectile.velocity.ToRotation();
                    Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
                    Projectile.frameCounter = 5;
                    Projectile.frame = 6;
                }
                else //regular movement
                {
                    Vector2 vel = Main.player[ai0].Center - Projectile.Center;
                    Projectile.rotation = vel.ToRotation();
                    if (vel.X > 0) //projectile is on left side of target
                    {
                        vel.X -= 300;
                        Projectile.direction = Projectile.spriteDirection = 1;
                    }
                    else //projectile is on right side of target
                    {
                        vel.X += 300;
                        Projectile.direction = Projectile.spriteDirection = -1;
                    }
                    Vector2 targetPos = Main.player[ai0].Center + new Vector2(Projectile.ai[0], Projectile.ai[1]);
                    Vector2 distance = (targetPos - Projectile.Center) / 4f;
                    Projectile.velocity = (Projectile.velocity * 19f + distance) / 20f;
                    Projectile.position += Main.player[ai0].velocity / 2f;
                    if (++Projectile.frameCounter > 5)
                    {
                        Projectile.frameCounter = 0;
                        if (++Projectile.frame > 5)
                            Projectile.frame = 0;
                    }
                }
            }
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

            SpriteEffects spriteEffects = Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            if (Projectile.localAI[0] > 85)
            {
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 2)
                {
                    Color color27 = Color.Lerp(color26, Color.Pink, 0.25f);
                    color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (1.5f * ProjectileID.Sets.TrailCacheLength[Projectile.type]);
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    if (Projectile.spriteDirection < 0)
                        num165 += (float)Math.PI;
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, spriteEffects, 0);
                }
            }

            float drawRotation = Projectile.rotation;
            if (Projectile.spriteDirection < 0)
                drawRotation += (float)Math.PI;
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), drawRotation, origin2, Projectile.scale, spriteEffects, 0);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            float ratio = (255 - Projectile.alpha) / 255f;
            float white = MathHelper.Lerp(ratio, 1f, 0.25f);
            if (white > 1f)
                white = 1f;
            return new Color((int)(lightColor.R * white), (int)(lightColor.G * white), (int)(lightColor.B * white), (int)(lightColor.A * ratio));
        }
    }
}