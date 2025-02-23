using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using CalamityMod.Items.Materials;
using ssm.Core;
using ssm.Calamity.Souls;
using ssm.Thorium.Souls;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Crossmod.Name)]
    public class TorDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
            }
        }
    }
}


