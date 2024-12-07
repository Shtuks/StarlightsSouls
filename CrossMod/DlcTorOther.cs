using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using ThoriumMod.Items.ThrownItems;
using ssm.CrossMod.Accessories;

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

                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && !recipe.HasIngredient(ModContent.ItemType<GtTETFinal>()))
                {
                    recipe.AddIngredient<GtTETFinal>(1);
                }
            }
        }
    }
}
