using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.HM;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Forces;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cyber Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            //RecipeGroup.RegisterGroup("ssm:CyberHelms", rec);
            //RecipeGroup rec1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardlight Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            //RecipeGroup.RegisterGroup("ssm:HardlightHelms", rec1);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                //because mutagen is op
                if (recipe.HasResult<MutagenMelee>())
                {
                    recipe.AddIngredient<LifeFragment>(5);
                }
                if (recipe.HasResult<MutagenRanged>())
                {
                    recipe.AddIngredient<LifeFragment>(5);
                }
                if (recipe.HasResult<MutagenMagic>())
                {
                    recipe.AddIngredient<LifeFragment>(5);
                }
                if (recipe.HasResult<MutagenSummon>())
                {
                    recipe.AddIngredient<LifeFragment>(5);
                }
                if (recipe.HasResult<MutagenRitualist>())
                {
                    recipe.AddIngredient<LifeFragment>(5);
                }

                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<RoboBrain>())
                        recipe.AddIngredient<RoboBrain>();
                }

                if (recipe.HasResult<FlightMasterySoul>())
                {
                    recipe.AddIngredient<NebWings>();
                    recipe.AddIngredient<XenomiteJetpack>();
                }
            }
        }
    }
}