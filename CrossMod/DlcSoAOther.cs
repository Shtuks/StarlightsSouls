using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using SacredTools.Content.Items.Accessories.Revenant;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Crossmod.Name, ModCompatibility.SacredTools.Name)]
    public class DlcSoAOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            //souls/mutagen overhaul

            //for (int i = 0; i < Recipe.numRecipes; i++)
            //{
            //    Recipe recipe = Main.recipe[i];

            //    if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && !recipe.HasIngredient(ModContent.ItemType<BindsOfVeracity>()))
            //    {
            //        recipe.AddIngredient<BindsOfVeracity>(1);
            //        recipe.AddIngredient<LanceSheathTalisman>(1);
            //    }
            //}
        }
    }
}
