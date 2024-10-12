using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using ssm.Content.NPCs;
using ssm;
using ssm.Render.Primitives;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria.Graphics.Shaders;
using ssm.Content.Projectiles.Deathrays;

namespace ssm.Content.Projectiles.Shtuxibus
{
    public class DeviEnergyHeart : ModProjectile
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            CooldownSlot = 1;
            Projectile.alpha = 150;
            Projectile.timeLeft = 90;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 1;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = 1;
                SoundEngine.PlaySound(SoundID.Item44, Projectile.Center);
            }
            if (Projectile.alpha >= 60)
                Projectile.alpha -= 10;
            Projectile.rotation = Projectile.ai[0];
            Projectile.scale += 0.01f;
            float speed = Projectile.velocity.Length();
            speed += Projectile.ai[1];
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * speed;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 velocity = Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(4f, 6f);
            }
            if (ShtunUtils.BossIsAlive(ref ShtunNpcs.Shtuxibus, ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>()))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.UnitX.RotatedBy(Projectile.rotation + (float)Math.PI / 2 * i),
                        ModContent.Find<ModProjectile>(fargosouls.Name, "DeviDeathray").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }

        // public override void OnHitPlayer(Player target, Player.HurtInfo info)
        // {
        //     target.AddBuff(ModContent.BuffType<Buffs.Masomode.Lovestruck>(), 240);
        // }
    }
}