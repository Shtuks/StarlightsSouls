using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class DeviSparklingLove : ModProjectile
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public int scaleCounter;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.alpha = 250;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[0], ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>());
            if (npc != null)
            {
                if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    Projectile.localAI[1] = Projectile.DirectionFrom(npc.Center).ToRotation();
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, -1, -17);
                }

                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 4;
                    if (Projectile.alpha < 0)
                        Projectile.alpha = 0;
                }

                if (++Projectile.localAI[0] > 31)
                {
                    Projectile.localAI[0] = 1;
                    if (++scaleCounter < 3)
                    {
                        Projectile.position = Projectile.Center;

                        Projectile.width *= 2;
                        Projectile.height *= 2;
                        Projectile.scale *= 2;

                        Projectile.Center = Projectile.position;

                        MakeDust();

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, -1, -16 + scaleCounter);

                        SoundEngine.PlaySound(SoundID.Item92, Projectile.Center);
                    }
                }

                Vector2 offset = new Vector2(Projectile.ai[1], 0).RotatedBy(npc.ai[3] + Projectile.localAI[1]);
                Projectile.Center = npc.Center + offset * Projectile.scale;
            }
            else
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.timeLeft == 8)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath6, Projectile.Center);
                SoundEngine.PlaySound(SoundID.Item92, Projectile.Center);

                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowRing").Type, 0, 0f, Main.myPlayer, -1, -14);

                if (!Main.dedServ && Main.LocalPlayer.active)
                    //     Main.LocalPlayer.GetModPlayer<FargoSoulsPlayer>().Screenshake = 30;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        float baseRotation = Main.rand.NextFloat(MathHelper.TwoPi);
                        float num = (Utils.NextFloat(Main.rand, (float)Math.PI * 2f));
                        int max = 12;
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (i + 0.5f) + baseRotation);
                            Vector2 speed = 2 * target / 90;
                            float acceleration = -speed.Length() / 90;
                            float rotation = speed.ToRotation();
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<DeviEnergyHeart>(),
                                (int)(Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + MathHelper.PiOver2, acceleration);

                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation + MathHelper.PiOver2);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation - MathHelper.PiOver2);
                        }
                        for (int z = 0; z < max; z++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (z + 0.5f) + baseRotation);
                            Vector2 val13 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 2.0943334102630615, default(Vector2));
                            Vector2 speed = 2 * target / 90;

                            float acceleration = -speed.Length() / 90;
                            float rotation = speed.ToRotation();
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, val13, ModContent.ProjectileType<DeviEnergyHeart>(),
                                (int)(Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + MathHelper.PiOver2, acceleration);

                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation + MathHelper.PiOver2);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation - MathHelper.PiOver2);
                        }
                        for (int t = 0; t < max; t++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (t + 0.5f) + baseRotation);
                            Vector2 speed = 2 * target / 90;
                            float acceleration = -speed.Length() / 90;
                            float rotation = speed.ToRotation();
                            Vector2 val16 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 4.188666820526123, default(Vector2));
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, val16, ModContent.ProjectileType<DeviEnergyHeart>(),
                                (int)(Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + MathHelper.PiOver2, acceleration);

                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation + MathHelper.PiOver2);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, speed, ModContent.Find<ModProjectile>(fargosouls.Name, "GlowLine").Type, Projectile.damage, 0f, Main.myPlayer, 2, rotation - MathHelper.PiOver2);
                        }
                        for (int l = 0; l < max; l++)
                        {
                            Vector2 val11 = 300f * Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.PI * 2f / (float)max * ((float)l + 0.5f) + num), default(Vector2));
                            Vector2 val12 = 2f * val11 / 90f;
                            float num9 = (0f - val12.Length()) / 90f;
                            float num10 = Utils.ToRotation(val12);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, val12, ModContent.ProjectileType<DeviEnergyHeart>(), Projectile.damage, 0f, Main.myPlayer, num10 + (float)Math.PI / 2f, num9);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num10);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, val12, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num10 + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, val12, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num10 - (float)Math.PI / 2f);
                        }
                        for (int m = 0; m < max; m++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (m + 0.5f) + baseRotation);
                            Vector2 val13 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 2.0943334102630615, default(Vector2));
                            Vector2 val14 = 300f * Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.PI * 2f / (float)max * ((float)m + 0.5f) + num), default(Vector2));
                            Vector2 val15 = 2f * val14 / 90f;
                            float num11 = (0f - val15.Length()) / 90f;
                            float num12 = Utils.ToRotation(val15);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val13, val15, ModContent.ProjectileType<DeviEnergyHeart>(), Projectile.damage, 0f, Main.myPlayer, num12 + (float)Math.PI / 2f, num11);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val13, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num12);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val13, val15, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num12 + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val13, val15, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num12 - (float)Math.PI / 2f);
                        }
                        for (int n = 0; n < max; n++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (n + 0.5f) + baseRotation);
                            Vector2 val16 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 4.188666820526123, default(Vector2));
                            Vector2 val17 = 300f * Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.PI * 2f / (float)max * ((float)n + 0.5f) + num), default(Vector2));
                            Vector2 val18 = 2f * val17 / 90f;
                            float num13 = (0f - val18.Length()) / 90f;
                            float num14 = Utils.ToRotation(val18);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<DeviEnergyHeart>(), Projectile.damage, 0f, Main.myPlayer, num14 + (float)Math.PI / 2f, num13);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 - (float)Math.PI / 2f);
                        }
                        for (int itu = 0; itu < max; itu++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (itu + 0.5f) + baseRotation);
                            Vector2 val16 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 10.188666820526123, default(Vector2));
                            Vector2 val17 = 300f * Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.PI * 2f / (float)max * ((float)itu + 0.5f) + num), default(Vector2));
                            Vector2 val18 = 2f * val17 / 90f;
                            float num13 = (0f - val18.Length()) / 90f;
                            float num14 = Utils.ToRotation(val18);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<DeviEnergyHeart>(), Projectile.damage, 0f, Main.myPlayer, num14 + (float)Math.PI / 2f, num13);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 - (float)Math.PI / 2f);
                        }
                        for (int fife = 0; fife < max; fife++)
                        {
                            Vector2 target = 300 * Vector2.UnitX.RotatedBy(MathHelper.TwoPi / max * (fife + 0.5f) + baseRotation);
                            Vector2 val16 = Projectile.Center + Utils.RotatedBy(target - Projectile.Center, 15.188666820526123, default(Vector2));
                            Vector2 val17 = 300f * Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.PI * 2f / (float)max * ((float)fife + 0.5f) + num), default(Vector2));
                            Vector2 val18 = 2f * val17 / 90f;
                            float num13 = (0f - val18.Length()) / 90f;
                            float num14 = Utils.ToRotation(val18);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<DeviEnergyHeart>(), Projectile.damage, 0f, Main.myPlayer, num14 + (float)Math.PI / 2f, num13);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), val16, val18, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, num14 - (float)Math.PI / 2f);
                        }

                    }
            }

            Projectile.direction = Projectile.spriteDirection = npc.direction;
            Projectile.rotation = npc.ai[3] + Projectile.localAI[1] + (float)Math.PI / 2 + (float)Math.PI / 4;
            if (Projectile.spriteDirection >= 0)
                Projectile.rotation -= (float)Math.PI / 2;
        }

        public override void OnKill(int timeLeft)
        {
            MakeDust();
        }

        private void MakeDust()
        {
            Vector2 start = Projectile.width * Vector2.UnitX.RotatedBy(Projectile.rotation - (float)Math.PI / 4);
            if (Math.Abs(start.X) > Projectile.width / 2) //bound it so its always inside projectile's hitbox
                start.X = Projectile.width / 2 * Math.Sign(start.X);
            if (Math.Abs(start.Y) > Projectile.height / 2)
                start.Y = Projectile.height / 2 * Math.Sign(start.Y);
            int length = (int)start.Length();
            start = Vector2.Normalize(start);
            float scaleModifier = scaleCounter / 3f + 0.5f;
            for (int j = -length; j <= length; j += 80)
            {
                Vector2 dustPoint = Projectile.Center + start * j;
                dustPoint.X -= 23;
                dustPoint.Y -= 23;
                for (int index1 = 0; index1 < 15; ++index1)
                {
                    int index2 = Dust.NewDust(dustPoint, Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), scaleModifier * 2.5f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 16f * scaleModifier;
                    int index3 = Dust.NewDust(dustPoint, Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), scaleModifier);
                    Main.dust[index3].velocity *= 8f * scaleModifier;
                    Main.dust[index3].noGravity = true;
                }

                for (int i = 0; i < 5; i++)
                {
                    int d = Dust.NewDust(dustPoint, Main.DiscoR, Main.DiscoG, Main.DiscoB, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), Main.rand.NextFloat(1f, 2f) * scaleModifier);
                    Main.dust[d].velocity *= Main.rand.NextFloat(1f, 4f) * scaleModifier;
                }
            }
        }

        /*public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.velocity.X = target.Center.X < Main.npc[(int)Projectile.ai[0]].Center.X ? -15f : 15f;
            target.velocity.Y = -10f;
            target.AddBuff(ModContent.BuffType<Lovestruck>(), 240);
        }*/

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

            NPC npc = ShtunUtils.NPCExists(Projectile.ai[0], ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>());
            if (npc != null && npc.velocity != Vector2.Zero)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
                Terraria.Graphics.Shaders.ArmorShaderData shader = Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderFromItemId(ItemID.PhaseDye);
                shader.Apply(Projectile, new Terraria.DataStructures.DrawData?());


                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                {
                    Color color27 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 50);
                    color27 *= 0.5f;
                    color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, effects, 0);
                }

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            }

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }
    }
}