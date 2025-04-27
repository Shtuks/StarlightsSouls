using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyBiome.Armor;
using ssm.Spooky.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;

namespace ssm.Spooky
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    class SpookyRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Spooky;
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Gore Helmet", ModContent.ItemType<GoreHoodEye>(), ModContent.ItemType<GoreHoodMouth>());
            RecipeGroup.RegisterGroup("ssm:AnyGoreHelmet", group);
            RecipeGroup group1 = new RecipeGroup(() => Lang.misc[37] + " Gilded Hat", ModContent.ItemType<WizardGangsterHead>(), ModContent.ItemType<WizardGangsterHead2>());
            RecipeGroup.RegisterGroup("ssm:AnyGildedHat", group1);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<TerrariaSoul>() && !recipe.HasIngredient<HorrorForce>())
                {
                    recipe.AddIngredient<HorrorForce>(1);
                    recipe.AddIngredient<TerrorForce>(1);
                }
            }
        }
    }
}