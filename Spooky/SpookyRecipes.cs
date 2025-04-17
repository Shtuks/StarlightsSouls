using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyBiome.Armor;

namespace ssm.Spooky
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    class SpookyRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Gore Helmet", ModContent.ItemType<GoreHoodEye>(), ModContent.ItemType<GoreHoodMouth>());
            RecipeGroup.RegisterGroup("ssm:AnyGoreHelmet", group);
            RecipeGroup group1 = new RecipeGroup(() => Lang.misc[37] + " Gilded Hat", ModContent.ItemType<WizardGangsterHead>(), ModContent.ItemType<WizardGangsterHead2>());
            RecipeGroup.RegisterGroup("ssm:AnyGildedHat", group1);
        }
    }
}