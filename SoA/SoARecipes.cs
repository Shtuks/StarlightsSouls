using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using SacredTools.Content.Items.Materials;
using ssm.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories;
using Terraria.ID;
using Terraria.Localization;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Dragon;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using ssm.SoA.Souls;
using ssm.Core;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoARecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SoAEnchantments;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Asthral Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            RecipeGroup.RegisterGroup("ssm:AsthralHelms", rec);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Flarium Helmet", ModContent.ItemType<FlariumCrown>(), ModContent.ItemType<FlariumMask>(), ModContent.ItemType<FlariumCowl>());
            RecipeGroup.RegisterGroup("ssm:FlariumHelms", rec2);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SoASoul>())
                {
                    recipe.AddIngredient<SoASoul>();
                }

                // SoA Recipies
                //if (recipe.HasResult<ShadowspecBar>())
                //{
                //    recipe.AddIngredient<EmberOfOmen>();
                //}
                //if (recipe.HasResult<ColossusSoul>())
                //{
                //    recipe.AddIngredient<ReflectionShield>();
                //}
            }
        }
    }
}
