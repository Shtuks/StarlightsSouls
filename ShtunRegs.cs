using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

// Base namespace for convinience
namespace ssm
{
    public static class ShtunRegs
    {
        #region Additive Textures
        public static Asset<Texture2D> BlobBloomTexture => ModContent.Request<Texture2D>("ssm/ExtraTextures/AdditiveTextures/BlobGlow");
        public static Asset<Texture2D> BloomTexture => ModContent.Request<Texture2D>("ssm/ExtraTextures/AdditiveTextures/Bloom");
        public static Asset<Texture2D> ThatoProjectile => ModContent.Request<Texture2D>("ssm/Projectiles/Minions/ThotMonochrome");
        public static Asset<Texture2D> ThatoProjectileEX => ModContent.Request<Texture2D>("ssm/Projectiles/Minions/ThotMonochromeEX");
        public static Asset<Texture2D> DeviBorderTexture => ModContent.Request<Texture2D>("ssm/ExtraTextures/AdditiveTextures/DeviBorder");
        public static Asset<Texture2D> HardEdgeRing => ModContent.Request<Texture2D>("ssm/ExtraTextures/AdditiveTextures/HardEdgeRing");
        public static Asset<Texture2D> SoftEdgeRing => ModContent.Request<Texture2D>("ssm/ExtraTextures/AdditiveTextures/SoftEdgeRing");
        #endregion

        #region Misc Shader Textures
        public static Asset<Texture2D> DeviRingTexture => ModContent.Request<Texture2D>("ssm/ExtraTextures/MiscShaderTextures/Ring1");
        public static Asset<Texture2D> DeviRing2Texture => ModContent.Request<Texture2D>("ssm/ExtraTextures/MiscShaderTextures/Ring2");
        public static Asset<Texture2D> DeviRing3Texture => ModContent.Request<Texture2D>("ssm/ExtraTextures/MiscShaderTextures/Ring3");
        public static Asset<Texture2D> DeviRing4Texture => ModContent.Request<Texture2D>("ssm/ExtraTextures/MiscShaderTextures/Ring4");
        #endregion

        #region Trails
        public static Asset<Texture2D> DeviBackStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/DevBackStreak");
        public static Asset<Texture2D> DeviInnerStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/DevInnerStreak");
        public static Asset<Texture2D> FadedGlowStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/FadedGlowStreak");
        public static Asset<Texture2D> FadedStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/FadedStreak");
        public static Asset<Texture2D> FadedThinGlowStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/FadedThinGlowStreak");
        public static Asset<Texture2D> GenericStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/GenericStreak");
        public static Asset<Texture2D> MutantStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/MutantStreak");
        public static Asset<Texture2D> WillStreak => ModContent.Request<Texture2D>("ssm/ExtraTextures/Trails/WillStreak");
        #endregion
    }
}
