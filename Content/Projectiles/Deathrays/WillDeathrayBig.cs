using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ssm;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using System;
using Terraria.UI;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;


namespace ssm.Content.Projectiles.Deathrays
{
    public class WillDeathrayBig : Deathrays.BaseDeathray
    {
        public override string Texture => "ssm/Content/Projectiles/Deathrays/WillDeathray";
        public PrimDrawer LaserDrawer { get; private set; } = null;
        public WillDeathrayBig() : base(20, drawDistance: 3600, sheeting: TextureSheeting.Horizontal) { }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
        }
        public override bool? CanDamage()
        {
            return Projectile.scale == 15f;
        }
        public override void AI()
        {
            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {

            }
            float num801 = 10f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * 1.5f * num801;
            if (Projectile.scale > num801)
            {
                Projectile.scale = num801;
            }
            float num804 = Projectile.velocity.ToRotation() - 1.57079637f;
            Projectile.rotation = num804;
            num804 += 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = (float)Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 4000f;
            float num807 = 0f;
            int num3;
            for (int num808 = 0; num808 < array3.Length; num808 = num3 + 1)
            {
                num807 += array3[num808];
                num3 = num808;
            }
            num807 /= num805;
            float amount = 0.5f;
            Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num807, amount);
            Vector2 vector79 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
            for (int num809 = 0; num809 < 2; num809 = num3 + 1)
            {
                float num810 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num811 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector80 = new Vector2((float)Math.Cos((double)num810) * num811, (float)Math.Sin((double)num810) * num811);
                int num812 = Dust.NewDust(vector79, 0, 0, 228, vector80.X, vector80.Y, 0, default(Color), 1f);
                Main.dust[num812].noGravity = true;
                Main.dust[num812].scale = 1.7f;
                num3 = num809;
            }
            if (Main.rand.NextBool(5))
            {
                Vector2 value29 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
                int num813 = Dust.NewDust(vector79 + value29 - Vector2.One * 4f, 8, 8, 228, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num813];
                dust.velocity *= 0.5f;
                Main.dust[num813].velocity.Y = -Math.Abs(Main.dust[num813].velocity.Y);
            }
            Projectile.position -= Projectile.velocity;
            if (Main.LocalPlayer.active && !Main.dedServ)
            {

                if (Projectile.localAI[0] < maxTime / 2)
                {
                    const int increment = 100;
                    for (int i = 0; i < array3[0]; i += increment)
                    {
                        float offset = i + Main.rand.NextFloat(-increment, increment);
                        Vector2 spawnPos = Projectile.position + Projectile.velocity * offset;
                        if (Math.Abs(spawnPos.Y - Main.LocalPlayer.Center.Y) > Main.screenHeight * 0.75f)
                            continue;
                        int d = Dust.NewDust(spawnPos,
                            Projectile.width, Projectile.height, 228, 0f, 0f, 0, default, 6f);
                        Main.dust[d].noGravity = Main.rand.NextBool();
                        Main.dust[d].velocity += Projectile.velocity.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-6f, 6f);
                        Main.dust[d].velocity *= Main.rand.NextFloat(1f, 3f);
                    }
                }
            }

            if (++Projectile.frame >= Main.projFrames[Projectile.type])
                Projectile.frame = 0;
        }
        public float WidthFunction(float _) => Projectile.width * Projectile.scale * 3;
        public Color ColorFunction(float _) => new(253, 254, 32);
    }
}