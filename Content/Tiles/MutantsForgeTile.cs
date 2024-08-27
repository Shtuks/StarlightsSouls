using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Fargowiltas.Items.Tiles;

namespace ssm.Content.Tiles
{
  public class MutantsForgeTile : ModTile
  {
    public override void SetStaticDefaults()
    {
      Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
      Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
      Main.tileNoAttach[(int) ((ModBlockType) this).Type] = true;
      Main.tileLavaDeath[(int) ((ModBlockType) this).Type] = false;
      TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
      TileObjectData.newTile.LavaDeath = false;
      TileObjectData.newTile.Height = 3;
      TileObjectData.newTile.CoordinateHeights = new int[3]
      {
        16,
        16,
        18
      };
      TileObjectData.newTile.Origin = new Point16(2, 1);
      TileObjectData.addTile((int) ((ModBlockType) this).Type);
      this.AddMapEntry(new Color(41, 157, 230), ((ModBlockType) this).CreateMapEntryName());
      this.AnimationFrameHeight = 54;
      TileID.Sets.DisableSmartCursor[(int) ((ModBlockType) this).Type] = true;
      ((ModBlockType) this).DustType = 84;
      this.AdjTiles = new int[3]
      {
        ModContent.TileType<DraedonsForge>(),
        ModContent.TileType<CrucibleCosmosSheet>(),
        ModContent.TileType<StaticRefiner>(),
      };
    }

    public override void AnimateTile(ref int frame, ref int frameCounter)
    {
      ++frameCounter;
      if (frameCounter < 11)
        return;
      frame = (frame + 1) % 4;
      frameCounter = 0;
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
      r = (float) Main.DiscoR / (float) byte.MaxValue;
      g = (float) Main.DiscoG / (float) byte.MaxValue;
      b = (float) Main.DiscoB / (float) byte.MaxValue;
    }
  }
}
