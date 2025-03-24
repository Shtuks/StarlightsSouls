using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ssm.Core;
using Redemption.Tiles.Furniture.SlayerShip;
using Redemption.Tiles.Furniture.Misc;
using Redemption.Tiles.Furniture.Lab;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionCraftingStationTile : ModTile
	{
        public override void SetStaticDefaults()
		{
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
			TileObjectData.newTile.DrawFlipHorizontal = false;
			TileObjectData.newTile.DrawFlipVertical = false;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 11;
            TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16,
				16,
				16
			};
			TileObjectData.addTile(Type);
            this.AddMapEntry(new Color(41, 157, 230), ((ModBlockType)this).CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[(int)((ModBlockType)this).Type] = true;
            AdjTiles = new int[]
			{
				TileID.WorkBenches,
				TileID.Furnaces,
				TileID.Hellforge,
				TileID.AdamantiteForge,
				TileID.Anvils,
				TileID.MythrilAnvil,
				TileID.DemonAltar,
				TileID.LunarCraftingStation,
				TileID.TinkerersWorkbench,
				TileType<SlayerFabricatorTile>(),
				TileType<EnergyStationTile>(),
				TileType<GathicCryoFurnaceTile>(),
				TileType<GirusCorruptorTile>(),
				TileType<XeniumRefineryTile>(),
				TileType<XeniumSmelterTile>()
			};
		}
    }
}