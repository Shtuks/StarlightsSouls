using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.ConvectiveArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Terraria.Localization;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.FractalArmor;

namespace ssm.Polarities
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class PolaritiesRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Convective Helmet", ModContent.ItemType<ConvectiveHelmetMelee>(), ModContent.ItemType<ConvectiveHelmetMagic>(), ModContent.ItemType<ConvectiveHelmetRanged>(), ModContent.ItemType<ConvectiveHelmetSummon>());
            RecipeGroup.RegisterGroup("ssm:ConvectiveHelms", rec);
            RecipeGroup rec1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Selfsimilar Helmet", ModContent.ItemType<SelfsimilarHelmetSummoner>(), ModContent.ItemType<SelfsimilarHelmetRanger>(), ModContent.ItemType<SelfsimilarHelmetMelee>(), ModContent.ItemType<SelfsimilarHelmetMage>());
            RecipeGroup.RegisterGroup("ssm:SelfsimilarHelms", rec1);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Fractal Helmet", ModContent.ItemType<FractalHelmetSummoner>(), ModContent.ItemType<FractalHelmetRanger>(), ModContent.ItemType<FractalHelmetMelee>(), ModContent.ItemType<FractalHelmetMage>());
            RecipeGroup.RegisterGroup("ssm:FractalHelms", rec2);
        }
    }
}