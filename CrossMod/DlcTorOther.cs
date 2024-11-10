using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ThoriumMod.Items.ThrownItems;

namespace ssm.CrossMod
{
    [ExtendsFromMod(ModCompatibility.Crossmod.Name, ModCompatibility.Thorium.Name)]
    public class DlcTorOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && !recipe.HasIngredient(ModContent.ItemType<ThrowingGuideVolume3>()))
                {
                    recipe.AddIngredient<ThrowingGuideVolume3>(1);
                }
            }
        }
    }
}
