using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class AbomSword : AbomSpecialDeathray
    {
        public AbomSword() : base(300) { }

        public int counter;
        public bool spawnedHandle;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.FargoSouls().DeletionImmuneRank = 2;
            Projectile.extraUpdates = 1;
            Projectile.netImportant = true;
        }
        public override void AI()
        {
            base.AI();

            Vector2? vector78 = null;
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            NPC abom = FargoSoulsUtil.NPCExists(Projectile.ai[1], ModContent.NPCType<MutantEX>());
            if (abom == null)
            {
                Projectile.Kill();
                return;
            }
            else
            {
                Projectile.Center = abom.Center;
            }
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Zombie104, Projectile.Center);
            }
            float num801 = 1f;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] >= maxTime)
            {
                Projectile.Kill();
                return;
            }
            Projectile.scale = (float)Math.Sin(Projectile.localAI[0] * 3.14159274f / maxTime) * num801 * 6f;
            if (Projectile.scale > num801)
            {
                Projectile.scale = num801;
            }
            float num804 = Projectile.velocity.ToRotation();
            if ((abom.velocity != Vector2.Zero || abom.ai[0] == 19) && abom.ai[0] != 20)
                num804 += Projectile.ai[0] / Projectile.MaxUpdates;
            Projectile.rotation = num804 - 1.57079637f;
            Projectile.velocity = num804.ToRotationVector2();
            float num805 = 3f;
            float num806 = Projectile.width;
            Vector2 samplingPoint = Projectile.Center;
            if (vector78.HasValue)
            {
                samplingPoint = vector78.Value;
            }
            float[] array3 = new float[(int)num805];
            for (int i = 0; i < array3.Length; i++)
                array3[i] = 3000f;
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
            if (Projectile.localAI[0] % 2 == 0)
            {
                if (abom.velocity != Vector2.Zero && --counter < 0)
                {
                    counter = 5;
                    if (FargoSoulsUtil.HostCheck)
                    {
                        Vector2 spawnPos = Projectile.Center;
                        Vector2 vel = Projectile.velocity.RotatedBy(Math.PI / 2 * Math.Sign(Projectile.ai[0]));
                        const int max = 15;
                        for (int i = 1; i <= max; i++)
                        {
                            spawnPos += Projectile.velocity * 3000f / max;
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), spawnPos, vel, ModContent.ProjectileType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomSickle2>(), Projectile.damage, 0f, Projectile.owner);
                        }
                    }
                }

                for (int i = 0; i < 15; i++)
                {
                    int d = Dust.NewDust(Projectile.position + Projectile.velocity * Main.rand.NextFloat(2000), Projectile.width, Projectile.height, DustID.GemTopaz, 0f, 0f, 0, default, 1.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 4f;
                }
            }

            if (!spawnedHandle)
            {
                spawnedHandle = true;
                if (FargoSoulsUtil.HostCheck)
                {
                    Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AbomSwordHandle>(), Projectile.damage, Projectile.knockBack, Projectile.owner, (float)Math.PI / 2, Projectile.identity);
                    Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AbomSwordHandle>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -(float)Math.PI / 2, Projectile.identity);
                }
            }
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.X = target.Center.X < Main.npc[(int)Projectile.ai[1]].Center.X ? -15f : 15f;
            target.velocity.Y = -10f;

            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), target.Center + Main.rand.NextVector2Circular(100, 100), Vector2.Zero, ModContent.ProjectileType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBlast>(), 0, 0f, Projectile.owner);

            target.AddBuff(ModContent.BuffType<FargowiltasSouls.Content.Buffs.Boss.AbomFangBuff>(), 300);
            target.AddBuff(BuffID.Burning, 180);
            target.AddBuff(BuffID.WitheredArmor, 600);
            target.AddBuff(BuffID.WitheredWeapon, 600);
        }

        public float WidthFunction(float _) => Projectile.width * Projectile.scale * 2;

        public static Color ColorFunction(float _) => new(253, 254, 32, 100);

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
                return false;

            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.WillBigDeathray");

            Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * drawDistance;

            Vector2 initialDrawPoint = Projectile.Center;
            Vector2[] baseDrawPoints = new Vector2[8];
            for (int i = 0; i < baseDrawPoints.Length; i++)
                baseDrawPoints[i] = Vector2.Lerp(initialDrawPoint, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

            Color brightColor = Color.Black;
            shader.TrySetParameter("mainColor", brightColor);
            Texture2D fademap = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/Trails/WillStreak").Value;
            FargoSoulsUtil.SetTexture1(fademap);
            for (int j = 0; j < 2; j++)
            {
                PrimitiveSettings primSettings = new(WidthFunction, ColorFunction, Shader: shader);
                PrimitiveRenderer.RenderTrail(baseDrawPoints, primSettings, 30);

                for (int i = 0; i < baseDrawPoints.Length / 2; i++)
                {
                    Vector2 temp = baseDrawPoints[i];
                    int swap = baseDrawPoints.Length - 1 - i;
                    baseDrawPoints[i] = baseDrawPoints[swap];
                    baseDrawPoints[swap] = temp;
                }
                PrimitiveRenderer.RenderTrail(baseDrawPoints, primSettings, 30);
            }
            return false;
        }
    }
}