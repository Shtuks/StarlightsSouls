using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class AbomSword : Deathrays.AbomSpecialDeathray
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
		private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
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
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
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
            NPC abom = ShtunUtils.NPCExists(Projectile.ai[1], ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>());
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
            float num806 = (float)Projectile.width;
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
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 spawnPos = Projectile.Center;
                        Vector2 vel = Projectile.velocity.RotatedBy(Math.PI / 2 * Math.Sign(Projectile.ai[0]));
                        const int max = 15;
                        for (int i = 1; i <= max; i++)
                        {
                            spawnPos += Projectile.velocity * 3000f / max;
                            Projectile.NewProjectile(Projectile.InheritSource(Projectile), spawnPos, vel, ModContent.Find<ModProjectile>(fargosouls.Name, "AbomSickle2").Type, Projectile.damage, 0f, Projectile.owner);
                        }
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    int d = Dust.NewDust(Projectile.position + Projectile.velocity * Main.rand.NextFloat(3000), Projectile.width, Projectile.height, 87, 0f, 0f, 0, Color.White, 6f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 4f;
                }
            }

            if (!spawnedHandle)
            {
                spawnedHandle = true;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AbomSwordHandle>(), Projectile.damage, Projectile.knockBack, Projectile.owner, (float)Math.PI / 2, Projectile.identity);
                    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<AbomSwordHandle>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -(float)Math.PI / 2, Projectile.identity);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.velocity.X = target.Center.X < Main.npc[(int)Projectile.ai[1]].Center.X ? -15f : 15f;
            target.velocity.Y = -10f;

            Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center + Main.rand.NextVector2Circular(100, 100), Vector2.Zero, ModContent.Find<ModProjectile>(fargosouls.Name, "AbomBlast").Type, 0, 0f, Projectile.owner);

          
            target.AddBuff(BuffID.WitheredArmor, 600);
            target.AddBuff(BuffID.WitheredWeapon, 600);
        }
    }
}