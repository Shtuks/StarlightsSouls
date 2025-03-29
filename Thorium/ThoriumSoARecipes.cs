using ssm.Thorium.Souls;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using SacredTools.Content.Items.Materials;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    public class TorSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
            }
        }
    }
}
