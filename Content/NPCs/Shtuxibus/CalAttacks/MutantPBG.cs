using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.PlaguebringerGoliath;
using ssm.Core;
using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus.CalAttacks
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class MutantPBG : ModProjectile
    {
        public override string Texture => "CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliath";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = Main.npcFrameCount[ModContent.NPCType<PlaguebringerGoliath>()];
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 198;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
        }
        private bool flyingFrame2 = false;
        const int TelegraphTime = 50;
        private bool charging => Projectile.ai[1] > TelegraphTime;

        public override void AI()
        {
            ref float timer = ref Projectile.ai[1];

            #region Animation
            ref float frameX = ref Projectile.localAI[0];
            int width = ((!charging) ? 266 : 322);


            Projectile.frameCounter += 1;
            if (Projectile.frameCounter > 4.0)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 3)
            {
                Projectile.frame = 0;
                frameX = (frameX == 0) ? width : 0;
                if (charging)
                {
                    flyingFrame2 = !flyingFrame2;
                }
            }

            Projectile.alpha = (int)MathHelper.Lerp(255, 0, MathHelper.Clamp(timer / TelegraphTime, 0, 1));

            Projectile.rotation = Projectile.velocity.ToRotation();
            #endregion

            #region AI
            if (timer == 0)
            {
                Particle p = new ExpandingBloomParticle(Projectile.Center, Vector2.Zero, Color.DarkGreen, Vector2.One * 40f, Vector2.Zero, TelegraphTime, true, Color.LimeGreen);
                p.Spawn();
            }
            if (timer < TelegraphTime)
            {
                Projectile.position -= Projectile.velocity;
            }
            else if (timer == TelegraphTime)
            {
                SoundEngine.PlaySound(PlaguebringerGoliath.DashSound, Projectile.Center);
                const float DashSpeed = 40;
                Projectile.velocity = Utils.SafeNormalize(Projectile.velocity, Projectile.rotation.ToRotationVector2()) * DashSpeed;
            }
            else
            {

            }
            if (timer > TelegraphTime * 3)
            {
                Projectile.Kill();
            }
            timer++;
            #endregion
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (WorldSavingSystem.EternityMode)
                target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);
            target.AddBuff(ModContent.BuffType<Plague>(), 60 * 5);
            base.OnHitPlayer(target, info);
        }

        private int curTex = 1;
        public override bool PreDraw(ref Color lightColor)
        {
            ref float frameX = ref Projectile.localAI[0];

            int frameHeight = ((!charging) ? 256 : 212);

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathGlow", (AssetRequestMode)2).Value;
            if (curTex != ((!charging) ? 1 : 2))
            {
                frameX = 0;
                Projectile.frame = 0;
            }
            if (charging)
            {
                curTex = 2;
                texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathChargeTex", (AssetRequestMode)2).Value;
                glowTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlaguebringerGoliathChargeTexGlow", (AssetRequestMode)2).Value;
            }
            else
            {
                curTex = 1;
            }
            float rotation = Projectile.rotation;
            SpriteEffects spriteEffects = Projectile.rotation.ToRotationVector2().X < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            rotation += spriteEffects == SpriteEffects.None ? MathHelper.Pi : 0;
            int frameCount = 3;
            Rectangle rectangle = new((int)frameX, Projectile.frame * frameHeight, texture.Width / 2, texture.Height / frameCount);
            Vector2 vector11 = Utils.Size(rectangle) / 2f;

            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Color drawColor = Projectile.GetAlpha(lightColor);

            Main.EntitySpriteDraw(texture, drawPos, (Rectangle?)rectangle, drawColor, rotation, vector11, Projectile.scale, spriteEffects, 0f);
            Main.EntitySpriteDraw(glowTexture, drawPos, (Rectangle?)rectangle, Color.White, rotation, vector11, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
