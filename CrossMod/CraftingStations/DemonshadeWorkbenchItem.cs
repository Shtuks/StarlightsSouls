using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ssm.Core;
using CalamityMod.Items.Placeables.FurnitureEutrophic;
using CalamityMod.Items.Placeables.FurnitureSilva;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeWorkbenchItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<ShadowspecBar>());
            Item.createTile = TileType<DemonshadeWorkbenchTile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DraedonsForge>());
            recipe.AddIngredient(ItemType<StaticRefiner>());
            recipe.AddIngredient(ItemType<ProfanedCrucible>());
            recipe.AddIngredient(ItemType<PlagueInfuser>());
            recipe.AddIngredient(ItemType<MonolithAmalgam>());
            recipe.AddIngredient(ItemType<EutrophicShelf>());
            recipe.AddIngredient(ItemType<EffulgentManipulator>());
            recipe.AddIngredient(ItemType<AncientAltar>());
            recipe.AddIngredient(ItemType<AshenAltar>());
            recipe.AddIngredient(ItemType<BotanicPlanter>());
            recipe.AddIngredient(ItemType<ShadowspecBar>(), 15);
            recipe.Register();
        }
    }
}
