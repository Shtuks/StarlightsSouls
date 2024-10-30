using System;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using ssm.Core;

namespace ssm.Content.Tiles
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeWorkbenchTile : ModTile
	{
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
			TileObjectData.newTile.DrawFlipHorizontal = false;
			TileObjectData.newTile.DrawFlipVertical = false;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				18
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
				TileType<DraedonsForge>(),
				TileType<CosmicAnvil>(),
				TileType<SilvaBasin>(),
				TileType<VoidCondenser>(),
				TileType<BotanicPlanter>(),
				TileType<ProfanedCrucible>(),
				TileType<PlagueInfuser>(),
				TileType<AshenAltar>(),
				TileType<AncientAltar>(),
				TileType<MonolithAmalgam>(),
				TileType<StaticRefiner>(),
			};
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = Main.DiscoR / 255f;
			g = Main.DiscoG / 255f;
			b = Main.DiscoB / 255f;
		}
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
			Tile tile = Main.tile[i, j];
			if ((tile.TileFrameX == 0 || tile.TileFrameX == 72) && tile.TileFrameY == 0) 
            {
                Texture2D texture = ModContent.Request<Texture2D>("ssm/Content/Tiles/DemonshadeWorkbenchTileFlame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                Rectangle dRect2 = new Rectangle((int)(i * 16 + 192 - Main.screenPosition.X), (int)(j * 16 + 176 - Main.screenPosition.Y), texture.Width, texture.Height);
                spriteBatch.Draw(texture, dRect2, Color.White);
            }
            return true;
        }
    }
}