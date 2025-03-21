using ssm.CrossMod.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Tiles
{
    public class AlchemistGlobalTiles : GlobalTile
    {
        public override int[] AdjTiles(int type)
        {
            if (type == ModContent.TileType<MutantsForgeTile>())
            {
                Main.LocalPlayer.adjHoney = true;
                Main.LocalPlayer.adjLava = true;
                Main.LocalPlayer.adjWater = true;
                Main.LocalPlayer.alchemyTable = true;
            }
            return base.AdjTiles(type);
        }
    }
}