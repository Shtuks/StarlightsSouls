using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Fargowiltas;
using System.Linq;
using Luminance.Core.Hooking;
using MonoMod.Cil;
using System.Reflection;
using Mono.Cecil.Cil;

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
            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.LavaDeath = false;
            TileID.Sets.CountsAsHoneySource[Type] = true;
            TileID.Sets.CountsAsLavaSource[Type] = true;
            TileID.Sets.CountsAsWaterSource[Type] = true;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.newTile.Origin = new Point16(2, 1);
            TileObjectData.addTile((int)((ModBlockType)this).Type);
            AddMapEntry(new Color(41, 157, 230), ((ModBlockType)this).CreateMapEntryName());
            AnimationFrameHeight = 126;
            TileID.Sets.DisableSmartCursor[(int)((ModBlockType)this).Type] = true;
            ((ModBlockType)this).DustType = 84;

            AdjTiles = Enumerable.Range(0, TileLoader.TileCount).ToArray();
            On_Recipe.PlayerMeetsEnvironmentConditions += EnableAllEnvironmentConditions;
            On_Recipe.PlayerMeetsTileRequirements += EnableAllTileConditions;
            new ManagedILEdit("Remove Recipe Restrictions", Mod, e => IL_Recipe.FindRecipes += e.SubscriptionWrapper, e => IL_Recipe.FindRecipes -= e.SubscriptionWrapper, RemoveRecipeConditions).Apply();
        }

        private void RemoveRecipeConditions(ILContext context, ManagedILEdit edit)
        {
            ILCursor cursor = new ILCursor(context);
            MethodInfo? recipeAvailabilityMethod = typeof(RecipeLoader).GetMethod("RecipeAvailable");

            if (recipeAvailabilityMethod is null || !cursor.TryGotoNext(MoveType.After, i => i.MatchCallOrCallvirt(recipeAvailabilityMethod)))
            {
                edit.LogFailure("Could not locate the RecipeAvailable load.");
                return;
            }
            cursor.EmitDelegate(() => Main.LocalPlayer.adjTile[Type]);
            cursor.Emit(OpCodes.Or);
        }
        private bool EnableAllEnvironmentConditions(On_Recipe.orig_PlayerMeetsEnvironmentConditions orig, Player player, Recipe tempRec)
        {
            if (player.adjTile[Type])
                return true;

            return orig(player, tempRec);
        }

        private bool EnableAllTileConditions(On_Recipe.orig_PlayerMeetsTileRequirements orig, Player player, Recipe tempRec)
        {
            if (player.adjTile[Type])
                return true;

            return orig(player, tempRec);
        }
        public override bool RightClick(int i, int j)
        {
            ModContent.GetInstance<ssm>().ShowBossSummonUI();
            return true;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            ++frameCounter;
            if (frameCounter < 8)
                return;
            frame = (frame + 1) % 8;
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
