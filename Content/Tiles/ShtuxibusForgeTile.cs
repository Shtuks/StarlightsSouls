using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Fargowiltas.Items.Tiles;
using ssm.Content.Tiles;
using System;
using Terraria.Localization;

namespace ssm.Content.Tiles
{
    public class ShtuxibusForgeTile : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ExtraContent;
        }

        public override void SetStaticDefaults()
        {
            Main.tileLighted[(int)((ModBlockType)this).Type] = true;
            Main.tileFrameImportant[(int)((ModBlockType)this).Type] = true;
            Main.tileNoAttach[(int)((ModBlockType)this).Type] = true;
            Main.tileLavaDeath[(int)((ModBlockType)this).Type] = false;
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
            TileObjectData.addTile((int)((ModBlockType)this).Type);
            this.AddMapEntry(new Color(41, 157, 230), ((ModBlockType)this).CreateMapEntryName());
            this.AnimationFrameHeight = 54;
            TileID.Sets.DisableSmartCursor[(int)((ModBlockType)this).Type] = true;
            ((ModBlockType)this).DustType = 84;
            TileID.Sets.CountsAsHoneySource[Type] = true;
            TileID.Sets.CountsAsLavaSource[Type] = true;
            TileID.Sets.CountsAsWaterSource[Type] = true;
            if (ModLoader.TryGetMod("CalamityMod", out Mod kal))
            {
                AdjTiles = new int[]
                {
                ModContent.TileType<DemonshadeWorkbenchTile>(),
                };
            }
            AdjTiles = new int[] {
                TileID.WorkBenches,
                TileID.HeavyWorkBench,
                TileID.Furnaces,
                TileID.Anvils,
                TileID.Bottles,
                TileID.Sawmill,
                TileID.Loom,
                TileID.Tables,
                TileID.Chairs,
                TileID.CookingPots,
                TileID.Sinks,
                TileID.Kegs,
                TileID.Hellforge,
                TileID.AlchemyTable,
                TileID.TinkerersWorkbench,
                TileID.ImbuingStation,
                TileID.DyeVat,
                TileID.LivingLoom,
                TileID.GlassKiln,
                TileID.IceMachine,
                TileID.HoneyDispenser,
                TileID.SkyMill,
                TileID.Solidifier,
                TileID.BoneWelder,
                TileID.MythrilAnvil,
                TileID.AdamantiteForge,
                TileID.DemonAltar,
                TileID.Bookcases,
                TileID.CrystalBall,
                TileID.Autohammer,
                TileID.LunarCraftingStation,
                TileID.LesionStation,
                TileID.FleshCloningVat,
                TileID.LihzahrdFurnace,
                TileID.SteampunkBoiler,
                TileID.Blendomatic,
                TileID.MeatGrinder,
                TileID.Tombstones,
                //ModContent.TileType<GoldenDippingVatSheet>(),
                //ModContent.TileType<DraedonsForge>(),
                ModContent.TileType<CrucibleCosmosSheet>(),
                //ModContent.TileType<StaticRefiner>(),
                //ModContent.TileType<CosmicAnvil>(),
                //ModContent.TileType<ThoriumAnvil>(),
                //ModContent.TileType<XeniumRefineryTile>(),
                //ModContent.TileType<XeniumSmelterTile>(),
                //ModContent.TileType<GirusCorruptorTile>(),
                //ModContent.TileType<ArcaneArmorFabricator>(),
                //ModContent.TileType<SoulForge>(),
                ModContent.TileType<MutantsForgeTile>()};
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
            r = (float)Main.DiscoR / (float)byte.MaxValue;
            g = (float)Main.DiscoG / (float)byte.MaxValue;
            b = (float)Main.DiscoB / (float)byte.MaxValue;
        }
    }
}
