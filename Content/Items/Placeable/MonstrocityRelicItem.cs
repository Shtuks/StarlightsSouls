using ssm.Content.Tiles;
using Terraria.ModLoader;

namespace ssm.Content.Items.Placeable
{
    public class MonstrocityRelicItem : FargowiltasSouls.Content.Items.Placables.Relics.BaseRelic
    {
        protected override int TileType => ModContent.TileType<MonstrocityRelicTile>();
    }
}
