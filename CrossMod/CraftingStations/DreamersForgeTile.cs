using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ssm.Core;
using ThoriumMod.Tiles;
using Terraria.DataStructures;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreamersForgeTile : ModTile
	{
        public override void SetStaticDefaults()
		{
            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.Origin = new Point16(0, 2);
            TileObjectData.newTile.DrawFlipHorizontal = false;
            TileObjectData.newTile.DrawFlipVertical = false;

            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;

			TileObjectData.addTile(Type);

            AddMapEntry(new Color(41, 157, 230), ((ModBlockType)this).CreateMapEntryName());
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
				TileType<SoulForge>(),
				TileType<SoulForgeNew>(),
				TileType<ArcaneArmorFabricator>(),
				TileType<GuidesFinalGiftTile>(),
                TileType<GrimPedestal>(),
                TileType<ThoriumAnvil>()
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

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = Main.DiscoR / 255f;
            g = Main.DiscoG / 255f;
            b = Main.DiscoB / 255f;
        }
    }
}