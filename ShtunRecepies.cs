using Terraria;
using Terraria.ModLoader;
using MagicStorage.Items;
using ssm.Core;
using ssm.Content.Items.Armor;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class Recipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<CreativeStorageUnit>())
                {
                    recipe.AddIngredient<TrueLumberjackBody>();
                    recipe.AddIngredient<TrueLumberjackMask>();
                    recipe.AddIngredient<TrueLumberjackPants>();
                }
            }
        }
    }
}