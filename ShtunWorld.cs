﻿using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ssm.Content.NPCs.StarlightCat;
using Microsoft.Xna.Framework;

namespace ssm
{
    public class ShtunWorld : ModSystem
    {
        public override void PostDrawTiles()
        {
            if (StarlightCatBoss.phase > 0)
            {
                //if (ssm.debug) { ShtunUtils.DisplayLocalizedText("Drawing arena", Color.White); }
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
                StarlightCatBoss.DrawArena(Main.spriteBatch);
                Main.spriteBatch.End();
            }
        }
    }
}
