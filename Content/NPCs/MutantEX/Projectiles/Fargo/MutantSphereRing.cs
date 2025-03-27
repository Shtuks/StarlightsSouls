using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Fargo
{
    public class MutantSphereRing : ModProjectile
    {
        protected bool DieOutsideArena;

        private int ritualID = -1;

        private float originalSpeed;

        private bool spawned;

        public override string Texture => "Terraria/Images/Projectile_454";

        public override void SetStaticDefaults()
        {
            Main.projFrames[base.Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 40;
            base.Projectile.height = 40;
            base.Projectile.hostile = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.tileCollide = false;
            base.Projectile.timeLeft = 480;
            base.Projectile.alpha = 200;
            base.CooldownSlot = 1;
            if (base.Projectile.type == ModContent.ProjectileType<MutantSphereRing>())
            {
                this.DieOutsideArena = true;
                base.Projectile.FargoSouls().TimeFreezeImmune = true;
            }
        }

        public override bool CanHitPlayer(Player target)
        {
            if (target.hurtCooldowns[1] != 0)
            {
                return WorldSavingSystem.MasochistModeReal;
            }
            return true;
        }

        public override void AI()
        {
            if (!this.spawned)
            {
                this.spawned = true;
                this.originalSpeed = base.Projectile.velocity.Length();
            }
            base.Projectile.velocity = this.originalSpeed * Vector2.Normalize(base.Projectile.velocity).RotatedBy((double)base.Projectile.ai[1] / (Math.PI * 2.0 * (double)base.Projectile.ai[0] * (double)(base.Projectile.localAI[0] += 1f)));
            if (base.Projectile.alpha > 0)
            {
                base.Projectile.alpha -= 20;
                if (base.Projectile.alpha < 0)
                {
                    base.Projectile.alpha = 0;
                }
            }
            base.Projectile.scale = 1f - (float)base.Projectile.alpha / 255f;
            if (++base.Projectile.frameCounter >= 6)
            {
                base.Projectile.frameCounter = 0;
                if (++base.Projectile.frame > 1)
                {
                    base.Projectile.frame = 0;
                }
            }
            if (this.DieOutsideArena)
            {
                if (this.ritualID == -1)
                {
                    this.ritualID = -2;
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<MutantRitual>())
                        {
                            this.ritualID = i;
                            break;
                        }
                    }
                }
                Projectile ritual = FargoSoulsUtil.ProjectileExists(this.ritualID, ModContent.ProjectileType<MutantRitual>());
                if (ritual != null && base.Projectile.Distance(ritual.Center) > 1200f)
                {
                    base.Projectile.timeLeft = 0;
                }
            }
            this.TryTimeStop();
        }

        private void TryTimeStop()
        {
            if (Main.getGoodWorld && base.Projectile.hostile && !base.Projectile.friendly && Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()) && Main.npc[ShtunNpcs.mutantEX].ai[0] == -5f && base.Projectile.Colliding(base.Projectile.Hitbox, Main.LocalPlayer.FargoSouls().GetPrecisionHurtbox()))
            {
                if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
                {
                    SoundStyle style = new SoundStyle("FargowiltasSouls/Assets/Sounds/Accessories/ZaWarudo");
                    SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
                }
                Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 300);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (FargoSoulsUtil.BossIsAlive(ref ShtunNpcs.mutantEX, ModContent.NPCType<MutantEX>()))
            {
                target.FargoSouls().MaxLifeReduction += 100;
                target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            }
            target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360);
            this.TryTimeStop();
        }

        public override void OnKill(int timeleft)
        {
            if (Main.rand.NextBool(Main.player[base.Projectile.owner].ownedProjectileCounts[base.Projectile.type] / 10 + 1))
            {
                SoundEngine.PlaySound(in SoundID.NPCDeath6, base.Projectile.Center);
            }
            base.Projectile.position = base.Projectile.Center;
            base.Projectile.width = (base.Projectile.height = 208);
            base.Projectile.Center = base.Projectile.position;
            for (int index1 = 0; index1 < 2; index1++)
            {
                int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
            }
            for (int index1 = 0; index1 < 4; index1++)
            {
                int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 229, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[index2].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1f;
                int index3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 229, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[index3].position = new Vector2(base.Projectile.width / 2, 0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble()) * (float)Main.rand.NextDouble() + base.Projectile.Center;
                Main.dust[index3].velocity *= 1f;
                Main.dust[index3].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * base.Projectile.Opacity;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D glow = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            int rect1 = glow.Height;
            int rect2 = 0;
            Rectangle glowrectangle = new Rectangle(0, rect2, glow.Width, rect1);
            Vector2 gloworigin2 = glowrectangle.Size() / 2f;
            Color glowcolor = Color.Lerp(new Color(196, 247, 255, 0), Color.Transparent, 0.9f);
            glowcolor *= base.Projectile.Opacity;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[base.Projectile.type]; i++)
            {
                Color color27 = glowcolor;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
                float scale = base.Projectile.scale * (float)(ProjectileID.Sets.TrailCacheLength[base.Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[base.Projectile.type];
                Vector2 value4 = base.Projectile.oldPos[i] - Vector2.Normalize(base.Projectile.velocity) * i * 6f;
                Main.EntitySpriteDraw(glow, value4 + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, color27, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, scale * 1.5f, SpriteEffects.None);
            }
            glowcolor = Color.Lerp(new Color(255, 255, 255, 0), Color.Transparent, 0.85f);
            Main.EntitySpriteDraw(glow, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), glowrectangle, glowcolor, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, gloworigin2, base.Projectile.scale * 1.5f, SpriteEffects.None);
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[base.Projectile.type].Value;
            int num156 = TextureAssets.Projectile[base.Projectile.type].Value.Height / Main.projFrames[base.Projectile.type];
            int y3 = num156 * base.Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), rectangle, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin2, base.Projectile.scale, SpriteEffects.None);
        }
    }
}
