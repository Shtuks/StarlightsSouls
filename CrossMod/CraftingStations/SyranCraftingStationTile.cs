using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ssm.Core;
using SacredTools.Content.Tiles.CraftingStations;
using SacredTools.Content.Tiles.Furniture.Asthral;
using Terraria.DataStructures;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyranCraftingStationTile : ModTile
	{
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    return ShtunConfig.Instance.ExperimentalContent;
        //}
        public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.DrawFlipHorizontal = false;
			TileObjectData.newTile.DrawFlipVertical = false;
            TileObjectData.newTile.Origin = new Point16(0, 2);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
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
				TileType<TiridiumInfuserTile>(),
				TileType<OblivionForgeTile>(),
				TileType<FlariumAnvilTile>(),
				TileType<FlariumForgeTile>(),
                TileType<NightmareFoundryTile>(),
                TileType<FlariumWorkBenchTile>(),
				TileType<AsthralWorkbench>()
			};
		}

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 10)
            {
                frameCounter = 0;
                frame = (frame + 1) % 6;
            }
        }
    }
}