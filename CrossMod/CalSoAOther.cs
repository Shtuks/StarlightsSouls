using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories;
using ssm.Core;
using SacredTools.Content.Items.Accessories;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // valor to defender
                if (recipe.HasResult(ModContent.ItemType<ElementalGauntlet>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<FloraFist>(1);
                }
            }
        }
    }
}
