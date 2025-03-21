using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using ssm.Content.Items.Accessories;

namespace ssm
{
    public class Recipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<TerrariaSoul>())
                {
                 //   recipe.AddIngredient<CelestialEnchant>();
                }
            }
        }
    }
}