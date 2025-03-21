using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Fargowiltas;

namespace ssm.CrossMod.CraftingStations
{
    public class MutantsForgeTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[(int)((ModBlockType)this).Type] = true;
            Main.tileFrameImportant[(int)((ModBlockType)this).Type] = true;
            Main.tileNoAttach[(int)((ModBlockType)this).Type] = true;
            Main.tileLavaDeath[(int)((ModBlockType)this).Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.LavaDeath = false;
            TileID.Sets.CountsAsHoneySource[Type] = true;
            TileID.Sets.CountsAsLavaSource[Type] = true;
            TileID.Sets.CountsAsWaterSource[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new int[]
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

            #region oldSystem
            //int[] tempTiles = new int[]
            //{
            //    ModContent.TileType<DemonshadeWorkbenchTile>(),
            //    ModContent.TileType<CrucibleCosmosSheet>(),
            //    TileID.WorkBenches,
            //    TileID.Anvils,
            //    TileID.Furnaces,
            //    TileID.Hellforge,
            //    TileID.Bookcases,
            //    TileID.Sinks,
            //    TileID.Solidifier,
            //    TileID.Blendomatic,
            //    TileID.MeatGrinder,
            //    TileID.Loom,
            //    TileID.LivingLoom,
            //    TileID.FleshCloningVat,
            //    TileID.GlassKiln,
            //    TileID.BoneWelder,
            //    TileID.SteampunkBoiler,
            //    TileID.Bottles,
            //    TileID.LihzahrdFurnace,
            //    TileID.ImbuingStation,
            //    TileID.DyeVat,
            //    TileID.Kegs,
            //    TileID.HeavyWorkBench,
            //    TileID.Tables,
            //    TileID.Chairs,
            //    TileID.CookingPots,
            //    TileID.DemonAltar,
            //    TileID.Sawmill,
            //    TileID.CrystalBall,
            //    TileID.AdamantiteForge,
            //    TileID.MythrilAnvil,
            //    TileID.TinkerersWorkbench,
            //    TileID.Autohammer,
            //    TileID.IceMachine,
            //    TileID.SkyMill,
            //    TileID.HoneyDispenser,
            //    TileID.AlchemyTable,
            //    TileID.LunarCraftingStation
            //};

            //if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity) && Calamity.TryFind<ModTile>("DraedonsForge", out ModTile currTile))
            //{
            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile.Type;
            //}

            //if (ModLoader.TryGetMod(ModCompatibility.SacredTools.Name, out Mod soa) && soa.TryFind<ModTile>("TiridiumInfuserTile", out ModTile currTile6) && soa.TryFind<ModTile>("FlariumInfuserTile", out ModTile currTile7) && soa.TryFind<ModTile>("NeilBrewerTile", out ModTile currTile8))
            //{
            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile6.Type;

            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile7.Type;

            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile8.Type;
            //}

            //if (ModLoader.TryGetMod("ThoriumMod", out Mod tor) && tor.TryFind<ModTile>("SoulForge", out ModTile currTile3) && tor.TryFind<ModTile>("ArcaneArmorFabricator", out ModTile currTile4) && tor.TryFind<ModTile>("ThoriumAnvil", out ModTile currTile5))
            //{
            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile3.Type;

            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile4.Type;

            //    Array.Resize(ref tempTiles, tempTiles.Length + 1);
            //    tempTiles[tempTiles.Length - 1] = currTile5.Type;
            //}

            //AdjTiles = tempTiles;
            #endregion

            if (ssm.AllStationIDs != null && ssm.AllStationIDs.Length > 0)
            {
                AdjTiles = ssm.AllStationIDs;
            }

            AnimationFrameHeight = 74;
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

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (Main.LocalPlayer.Distance(new Vector2(i * 16 + 8, j * 16 + 8)) < 16 * 5)
            {
                Main.LocalPlayer.GetModPlayer<FargoPlayer>().ElementalAssemblerNearby = 6;
            }
        }
    }
}
