using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using Redemption.Items.Materials.PostML;
using ssm.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.HM;
using Terraria.ID;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Armor.PreHM.CommonGuard;
using Terraria.Localization;
using ssm.Core;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Asthral Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            //RecipeGroup.RegisterGroup("ssm:AsthralHelms", rec);
            //RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Flarium Helmet", ModContent.ItemType<FlariumCrown>(), ModContent.ItemType<FlariumMask>(), ModContent.ItemType<FlariumCowl>());
            //RecipeGroup.RegisterGroup("ssm:FlariumHelms", rec2);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
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
                if (recipe.HasResult<FlightMasterySoul>())
                {
                    recipe.AddIngredient<NebWings>();
                    recipe.AddIngredient<XenomiteJetpack>();
                }
                //if (recipe.HasResult<AeolusBoots>() && recipe.HasIngredient<ZephyrBoots>())
                //{
                //    if (recipe.RemoveIngredient(ItemType<ZephyrBoots>()))
                //        recipe.AddIngredient<Terrar>();
                //}
            }
        }
    }
}