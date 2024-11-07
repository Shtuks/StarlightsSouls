using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace ssm.Assets
{
    public static class ShtunTextureRegistry
    {
        public static Asset<Texture2D> ShtuxibusStreak => ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/Trails/ShtuxibusStreak");
        public static Asset<Texture2D> starlightArena => ModContent.Request<Texture2D>("ssm/Content/NPCs/StarlightCat/ChtuxlagorArenaBlock");
    }
}
