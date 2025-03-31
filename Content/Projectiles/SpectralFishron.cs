using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles
{
    public class SpectralFishron : ModProjectile
    {
        public override string Texture => "Terraria/Images/NPC_370";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            Projectile.alpha = 100;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = Projectile.ai[1] + 1;
                switch ((int)Projectile.ai[1])
                {
                    case 1: Projectile.DamageType = DamageClass.Melee; break;
                    case 2: Projectile.DamageType = DamageClass.Ranged; break;
                    case 3: Projectile.DamageType = DamageClass.Magic; break;
                    case 4: Projectile.DamageType = DamageClass.Summon; break;
                    case 5: Projectile.DamageType = DamageClass.Throwing; break;
                    default: break;
                }
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }

            if (Projectile.localAI[0]++ > 30f)
            {
                Projectile.localAI[0] = 0f;
                Projectile.ai[1]++;
            }

            if (Projectile.ai[1] % 2 == 1) //dash
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.frameCounter = 5;
                Projectile.frame = 6;

                int num22 = 7;
                for (int index1 = 0; index1 < num22; ++index1)
                {
                    Vector2 vector2_1 = (Vector2.Normalize(Projectile.velocity) * new Vector2((Projectile.width + 50) / 2f, Projectile.height) * 0.75f).RotatedBy((index1 - (num22 / 2 - 1)) * Math.PI / num22, new Vector2()) + Projectile.Center;
                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Vector2 vector2_3 = vector2_2;
                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].velocity /= 4f;
                    Main.dust[index2].velocity -= Projectile.velocity;
                }
            }
            else //preparing to dash
            {
                int ai0 = (int)Projectile.ai[0];
                const float moveSpeed = 1f;
                if (Projectile.localAI[0] == 30f) //just about to dash
                {
                    if (Projectile.ai[0] >= 0 && Main.npc[ai0].CanBeChasedBy()) //has target
                    {
                        Projectile.velocity = Main.npc[ai0].Center + Main.npc[ai0].velocity * 15f - Projectile.Center;
                        Projectile.velocity.Normalize();
                        Projectile.velocity *= 27f;
                        Projectile.rotation = Projectile.velocity.ToRotation();
                        Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
                        Projectile.frameCounter = 5;
                        Projectile.frame = 6;
                    }
                    else //no target
                    {
                        Projectile.localAI[0] = -1f;
                        TargetEnemies();
                        if (++Projectile.frameCounter > 5)
                        {
                            Projectile.frameCounter = 0;
                            if (++Projectile.frame > 5)
                                Projectile.frame = 0;
                        }
                    }
                }
                else //regular movement
                {
                    if (Projectile.ai[0] >= 0 && Main.npc[ai0].CanBeChasedBy()) //has target
                    {
                        Vector2 vel = Main.npc[ai0].Center - Projectile.Center;
                        Projectile.rotation = vel.ToRotation();
                        if (vel.X > 0) //Projectile is on left side of target
                        {
                            vel.X -= 300;
                            Projectile.direction = Projectile.spriteDirection = 1;
                        }
                        else //Projectile is on right side of target
                        {
                            vel.X += 300;
                            Projectile.direction = Projectile.spriteDirection = -1;
                        }
                        vel.Y -= 200f;
                        vel.Normalize();
                        vel *= 12f;
                        if (Projectile.velocity.X < vel.X)
                        {
                            Projectile.velocity.X += moveSpeed;
                            if (Projectile.velocity.X < 0 && vel.X > 0)
                                Projectile.velocity.X += moveSpeed;
                        }
                        else if (Projectile.velocity.X > vel.X)
                        {
                            Projectile.velocity.X -= moveSpeed;
                            if (Projectile.velocity.X > 0 && vel.X < 0)
                                Projectile.velocity.X -= moveSpeed;
                        }
                        if (Projectile.velocity.Y < vel.Y)
                        {
                            Projectile.velocity.Y += moveSpeed;
                            if (Projectile.velocity.Y < 0 && vel.Y > 0)
                                Projectile.velocity.Y += moveSpeed;
                        }
                        else if (Projectile.velocity.Y > vel.Y)
                        {
                            Projectile.velocity.Y -= moveSpeed;
                            if (Projectile.velocity.Y > 0 && vel.Y < 0)
                                Projectile.velocity.Y -= moveSpeed;
                        }
                    }
                    else //no target
                    {
                        if (Projectile.velocity.X < -1f)
                            Projectile.velocity.X += moveSpeed;
                        else if (Projectile.velocity.X > 1f)
                            Projectile.velocity.X -= moveSpeed;
                        if (Projectile.velocity.Y > -8f)
                            Projectile.velocity.Y -= moveSpeed;
                        else if (Projectile.velocity.Y < -10f)
                            Projectile.velocity.Y += moveSpeed;
                        TargetEnemies();
                    }
                    if (++Projectile.frameCounter > 5)
                    {
                        Projectile.frameCounter = 0;
                        if (++Projectile.frame > 5)
                            Projectile.frame = 0;
                    }
                }
            }
            Projectile.position += Projectile.velocity / 4f;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense > 0)
                modifiers.FinalDamage += target.defense / 2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 900);
            target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900);
            target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900);
        }

        public override void Kill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Item84, Projectile.Center);
            if (Projectile.owner == Main.myPlayer)
            {
                SpawnRazorbladeRing(12, 12.5f, 0.75f);
                SpawnRazorbladeRing(12, 10f, -2f);
            }
        }

        private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            vel *= speed;
            int type = ModContent.ProjectileType<RazorbladeTyphoonFriendly>();
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, type, Projectile.damage / 3,
                    Projectile.knockBack / 4f, Projectile.owner, rotationModifier * Projectile.spriteDirection, Projectile.localAI[1] - 1);
            }
        }

        private void TargetEnemies()
        {
            float maxDistance = 1000f;
            int possibleTarget = -1;
            bool isBoss = false;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(Projectile))// && Collision.CanHitLine(Projectile.Center, 0, 0, npc.Center, 0, 0))
                {
                    float npcDistance = Projectile.Distance(npc.Center);
                    if (npcDistance < maxDistance && (npc.boss || !isBoss))
                    {
                        if (npc.boss)
                            isBoss = true;
                        maxDistance = npcDistance;
                        possibleTarget = i;
                    }
                }
            }
            Projectile.ai[0] = possibleTarget;
            Projectile.netUpdate = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);

            Main.spriteBatch.UseBlendState(BlendState.Additive);

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                Color color27 = Color.White * Projectile.Opacity * Projectile.localAI[1];
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 value4 = Projectile.oldPos[i];
                float num165 = Projectile.oldRot[i];
                Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
            }

            Main.spriteBatch.ResetToDefault();

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            float ratio = (255 - Projectile.alpha) / 255f;
            float blue = MathHelper.Lerp(ratio, 1f, 0.25f);
            if (blue > 1f)
                blue = 1f;
            return new Color((int)(lightColor.R * ratio), (int)(lightColor.G * ratio), (int)(lightColor.B * blue), (int)(lightColor.A * ratio));
        }
    }
}