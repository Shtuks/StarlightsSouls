using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Assets.ExtraTextures;
using ReLogic.Content;
using Terraria.GameContent;
using Luminance.Core.Graphics;
using System;


namespace ssm.Content.Projectiles.Shtuxibus
{
    public class MutantRitual : BaseArena
    {
        private readonly Mod fargosouls = ModLoader.GetMod("FargowiltasSouls");
        public override string Texture => "ssm/Content/Projectiles/Shtuxibus/ShtuxibusRitualProj";
        public int npcType => ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>();

        private bool MutantDead;
        public MutantRitual() : base((float)Math.PI / 140f, 1600f, ModContent.NPCType<NPCs.Shtuxibus.Shtuxibus>(),135, 2, 60) { }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.projFrames[Projectile.type] = 1;
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

                targetRotation = -(float)Math.PI / 140f / 2;
            }
            else
            {
                Projectile.velocity = npc.Center - Projectile.Center;
                if (npc.ai[0] == 36)
                    Projectile.velocity /= 20f; //much faster for slime rain
                else if (npc.ai[0] == 22 || npc.ai[0] == 23 || npc.ai[0] == 25)
                    Projectile.velocity /= 40f; //move faster for direct dash, predictive throw
                else
                    Projectile.velocity /= 60f;

                targetRotation = (float)Math.PI / 140f;
            }

            const float increment = (float)Math.PI / 140f / 40;
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
            NPC npc = ShtunUtils.NPCExists(Projectile.ai[1], npcType);
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
            if (!target.HasBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type))
                target.AddBuff(ModContent.Find<ModBuff>(fargosouls.Name, "TimeFrozenBuff").Type, 300);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (ssm.amiactive)
            {
                return Color.White * Projectile.Opacity * (targetPlayer == Main.myPlayer && !MutantDead ? 1f : 0.15f);
            }
            else
            {
                return Color.White * Projectile.Opacity * 0f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Color.Green;
            color.A = 0;
            Color color2 = color;
            Color color3 = Color.Lerp(color, Color.White, 0.75f);
            Color color4 = Color.Lerp(color, Color.White, 0.5f);
            Vector2 center = base.Projectile.Center;
            float num = (float)(base.Projectile.width / 2) * base.Projectile.scale;
            num *= 0.75f;
            float num2 = base.threshold - num;
            Player localPlayer = Main.LocalPlayer;
            Asset<Texture2D> magicPixel = TextureAssets.MagicPixel;
            Asset<Texture2D> wavyNoise = FargosTextureRegistry.WavyNoise;
            float opacity = base.Projectile.Opacity;
            float num3 = base.Projectile.scale * 0.5f;
            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.MutantP2Aura");
            shader.TrySetParameter("colorMult", (object)7.35f);
            shader.TrySetParameter("time", (object)Main.GlobalTimeWrappedHourly);
            shader.TrySetParameter("radius", (object)(num2 * num3));
            shader.TrySetParameter("anchorPoint", (object)center);
            shader.TrySetParameter("screenPosition", (object)Main.screenPosition);
            shader.TrySetParameter("screenSize", (object)Main.ScreenSize.ToVector2());
            shader.TrySetParameter("playerPosition", (object)localPlayer.Center);
            shader.TrySetParameter("maxOpacity", (object)opacity);
            shader.TrySetParameter("darkColor", (object)color2.ToVector4());
            shader.TrySetParameter("midColor", (object)color3.ToVector4());
            shader.TrySetParameter("lightColor", (object)color4.ToVector4());
            Main.spriteBatch.GraphicsDevice.Textures[1] = wavyNoise.Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, shader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
            Rectangle destinationRectangle = new Rectangle(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
            Main.spriteBatch.Draw(magicPixel.Value, destinationRectangle, null, default(Color), 0f, magicPixel.Value.Size() * 0.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
}