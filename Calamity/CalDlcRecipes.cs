using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using CalamityMod.Items.Materials;
using ssm.Core;
using ssm.Calamity.Souls;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class CalDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                #region other
                //if (ShtunConfig.Instance.OldCalDlcBalance)
                //{
                    if (recipe.HasResult<BrandoftheBrimstoneWitch>() && !recipe.HasIngredient<ShadowspecBar>() && recipe.HasIngredient<AbomEnergy>())
                    {
                        if (recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>()))
                            recipe.AddIngredient<ShadowspecBar>(5);
                    }
                    if (recipe.HasResult(ModContent.ItemType<ShadowspecBar>()) && recipe.HasIngredient<EternalEnergy>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<EternalEnergy>());
                    }
                //}
                #endregion

                #region souls
                if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<CalamitySoul>() && recipe.HasIngredient<BrandoftheBrimstoneWitch>())
                {
                    if (recipe.RemoveIngredient(ModContent.ItemType<BrandoftheBrimstoneWitch>()))
                        recipe.AddIngredient<CalamitySoul>();
                }
                #endregion
            }
        }
    }
}


