using Terraria;
using Terraria.ModLoader;
using MagicStorage.Items;
using ssm.Core;
using ssm.Content.Items.Armor;
using ssm.MagicStorage;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class Recipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<CreativeStorageUnit>())
                {
                    recipe.AddIngredient<EternalStorageUnitItem>(10);

                    recipe.AddIngredient<TrueLumberjackBody>();
                    recipe.AddIngredient<TrueLumberjackMask>();
                    recipe.AddIngredient<TrueLumberjackPants>();

                    if (ModCompatibility.WrathoftheGods.Loaded)
                    {
                        ModContent.TryFind("NoxusBoss", "CheatPermissionSlip", out ModItem item);
                        recipe.AddIngredient(item, 1);
                    }
                }
            }
        }
    }
}