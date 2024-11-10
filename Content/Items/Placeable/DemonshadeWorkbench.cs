using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using ssm.Content.Tiles;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ssm.Core;
using CalamityMod.Items.Placeables.FurnitureEutrophic;
using CalamityMod.Items.Placeables.FurnitureSilva;

namespace ssm.Content.Items.Placeable
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeWorkbench : ModItem
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
            recipe.AddIngredient(ItemType<SilvaWorkBench>());
            recipe.AddIngredient(ItemType<VoidCondenser>());
            recipe.AddIngredient(ItemType<BotanicPlanter>());
            recipe.AddIngredient(ItemType<ProfanedCrucible>());
            recipe.AddIngredient(ItemType<PlagueInfuser>());
            recipe.AddIngredient(ItemType<AncientAltar>());
            recipe.AddIngredient(ItemType<AshenAltar>());
            recipe.AddIngredient(ItemType<MonolithAmalgam>());
            recipe.AddIngredient(ItemType<StaticRefiner>());
            recipe.AddIngredient(ItemType<EutrophicWorkBench>());
            recipe.AddIngredient(ItemType<ShadowspecBar>(), 20);
            recipe.Register();
        }
    }
}
