using Terraria.ModLoader;
using ssm.Content.Tiles;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ssm.Core;
using SacredTools.Content.Items.Materials;
using SacredTools.Content.Items.Placeable.CraftingStations;
using SacredTools.Items.Placeable.Asthral;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyranCraftingStationItem : ModItem
    {
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    return ShtunConfig.Instance.ExperimentalContent;
        //}
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<OblivionBar>());
            Item.createTile = TileType<SyranCraftingStationTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<NightmareFoundry>());
            recipe.AddIngredient(ItemType<TiridiumInfuser>());
            recipe.AddIngredient(ItemType<OblivionForge>());
            recipe.AddIngredient(ItemType<FlariumAnvil>());
            recipe.AddIngredient(ItemType<FlariumForge>());
            recipe.AddIngredient(ItemType<EmberOfOmen>(), 15);
            recipe.Register();
        }
    }
}
