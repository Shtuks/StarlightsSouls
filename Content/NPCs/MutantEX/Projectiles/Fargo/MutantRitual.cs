using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles
{
    public class MutantRitual : BaseArena
    {
        public override string Texture => "Terraria/Images/Projectile_454";

        private const float realRotation = MathHelper.Pi / 140f;
        private bool MutantDead;

        public MutantRitual() : base(realRotation, 1200f, ModContent.NPCType<MutantEX>(), visualCount: 48) { }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 2;
        }

        public override bool? CanDamage()
        {
            if (MutantDead)
                return false;
            return base.CanDamage();
        }

        protected override void Movement(NPC npc)
        {
            float targetRotation;
            if (npc.ai[0] == 19) 
            {
                Projectile.velocity = Vector2.Zero;

                targetRotation = -realRotation / 2;
            }
            else if (npc.ai[0] == 49)
            {
                if (npc.HasValidTarget && npc.ai[1] < 30)
                {
                    Projectile.velocity = (Main.player[npc.target].Center - Projectile.Center) / 10f;

                    targetRotation = realRotation;
                }
                else
                {
                    Projectile.velocity = Vector2.Zero;

                    targetRotation = -realRotation / 2;
                }
            }
            else
            {
                Projectile.velocity = npc.Center - Projectile.Center;
                if (npc.ai[0] == 36)
                    Projectile.velocity /= 20f; 
                else if (npc.ai[0] == 22 || npc.ai[0] == 23 || npc.ai[0] == 25)
                    Projectile.velocity /= 40f; 
                else
                    Projectile.velocity /= 60f;

                targetRotation = realRotation;
            }

            const float increment = realRotation / 40;
            if (rotationPerTick < targetRotation)
            {
                rotationPerTick += increment;
                if (rotationPerTick > targetRotation)
                    rotationPerTick = targetRotation;
            }
            else if (rotationPerTick > targetRotation)
            {
                rotationPerTick -= increment;
                if (rotationPerTick < targetRotation)
                    rotationPerTick = targetRotation;
            }

            MutantDead = npc.ai[0] <= -6;
        }

        public override void AI()
        {
            base.AI();
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 1)
                    Projectile.frame = 0;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            base.OnHitPlayer(target, info);

            target.FargoSouls().MaxLifeReduction += 100;
            target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400);
            target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180);

            if (Main.npc[ShtunNpcs.mutantEX].ai[0] == -5)
            {
                if (!target.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
                    SoundEngine.PlaySound(new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudo"), target.Center);
                target.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 300);
            }

            target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * Projectile.Opacity * (targetPlayer == Main.myPlayer && !MutantDead ? 1f : 0.15f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color outerColor = (FargoSoulsUtil.AprilFools ? Color.Red : Color.CadetBlue);
            outerColor.A = 0;
            Color darkColor = outerColor;
            Color mediumColor = Color.Lerp(outerColor, Color.White, 0.75f);
            Color lightColor2 = Color.Lerp(outerColor, Color.White, 0.5f);
            Vector2 auraPos = base.Projectile.Center;
            float leeway = (float)(base.Projectile.width / 2) * base.Projectile.scale;
            leeway *= 0.75f;
            float radius = base.threshold - leeway;
            Player target = Main.LocalPlayer;
            Asset<Texture2D> blackTile = TextureAssets.MagicPixel;
            Asset<Texture2D> diagonalNoise = FargosTextureRegistry.WavyNoise;
            float maxOpacity = base.Projectile.Opacity;
            float scale = base.Projectile.scale * 0.5f;
            ManagedShader borderShader = ShaderManager.GetShader("FargowiltasSouls.MutantP2Aura");
            borderShader.TrySetParameter("colorMult", (object)7.35f);
            borderShader.TrySetParameter("time", (object)Main.GlobalTimeWrappedHourly);
            borderShader.TrySetParameter("radius", (object)(radius * scale));
            borderShader.TrySetParameter("anchorPoint", (object)auraPos);
            borderShader.TrySetParameter("screenPosition", (object)Main.screenPosition);
            borderShader.TrySetParameter("screenSize", (object)Main.ScreenSize.ToVector2());
            borderShader.TrySetParameter("playerPosition", (object)target.Center);
            borderShader.TrySetParameter("maxOpacity", (object)maxOpacity);
            borderShader.TrySetParameter("darkColor", (object)darkColor.ToVector4());
            borderShader.TrySetParameter("midColor", (object)mediumColor.ToVector4());
            borderShader.TrySetParameter("lightColor", (object)lightColor2.ToVector4());
            Main.spriteBatch.GraphicsDevice.Textures[1] = diagonalNoise.Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, borderShader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
            Rectangle rekt = new Rectangle(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
            Main.spriteBatch.Draw(blackTile.Value, rekt, null, default(Color), 0f, blackTile.Value.Size() * 0.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
}