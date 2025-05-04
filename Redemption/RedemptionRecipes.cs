using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.HM;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using Terraria.ID;
using Terraria.Localization;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Armor.PreHM.CommonGuard;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Common Guard Helmet", ModContent.ItemType<CommonGuardHelm2>(), ModContent.ItemType<CommonGuardHelm1>());
            RecipeGroup.RegisterGroup("ssm:CommonGuardHelms", rec);
            RecipeGroup rec1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Hardlight Helmet", ModContent.ItemType<HardlightCasque>(), ModContent.ItemType<HardlightCowl>(), ModContent.ItemType<HardlightHelm>(), ModContent.ItemType<HardlightHood>(), ModContent.ItemType<HardlightVisor>());
            RecipeGroup.RegisterGroup("ssm:HardlightHelms", rec1);
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //FIXED
                //if (recipe.HasResult<MutagenMelee>())
                //{
                //    recipe.AddIngredient<LifeFragment>(5);
                //}
                //if (recipe.HasResult<MutagenRanged>())
                //{
                //    recipe.AddIngredient<LifeFragment>(5);
                //}
                //if (recipe.HasResult<MutagenMagic>())
                //{
                //    recipe.AddIngredient<LifeFragment>(5);
                //}
                //if (recipe.HasResult<MutagenSummon>())
                //{
                //    recipe.AddIngredient<LifeFragment>(5);
                //}
                //if (recipe.HasResult<MutagenRitualist>())
                //{
                //    recipe.AddIngredient<LifeFragment>(5);
                //}

                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<RoboBrain>())
                        recipe.AddIngredient<RoboBrain>();
                }

                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasResult<NebWings>())
                {
                    recipe.AddIngredient<NebWings>();
                    recipe.AddIngredient<XenomiteJetpack>();
                }

                //emblem -> essence -> mutagen -> soul
                //Where clamity post dog acc? I dont know.

                if (recipe.HasResult<MutagenMagic>() && !recipe.HasResult<ApprenticesEssence>())
                {
                    recipe.AddIngredient<ApprenticesEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenMelee>() && !recipe.HasResult<BarbariansEssence>())
                {
                    recipe.AddIngredient<BarbariansEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenSummon>() && !recipe.HasResult<OccultistsEssence>())
                {
                    recipe.AddIngredient<OccultistsEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }
                if (recipe.HasResult<MutagenRanged>() && !recipe.HasResult<SharpshootersEssence>())
                {
                    recipe.AddIngredient<SharpshootersEssence>();
                    recipe.RemoveIngredient(ItemID.DestroyerEmblem);
                }

                if (recipe.HasResult<ArchWizardsSoul>() && !recipe.HasResult<MutagenMagic>())
                {
                    recipe.AddIngredient<MutagenMagic>();
                    recipe.RemoveIngredient(ModContent.ItemType<ApprenticesEssence>());
                }
                if (recipe.HasResult<BerserkerSoul>() && !recipe.HasResult<MutagenMelee>())
                {
                    recipe.AddIngredient<MutagenMelee>();
                    recipe.RemoveIngredient(ModContent.ItemType<BarbariansEssence>());
                }
                if (recipe.HasResult<ConjuristsSoul>() && !recipe.HasResult<MutagenSummon>())
                {
                    recipe.AddIngredient<MutagenSummon>();
                    recipe.RemoveIngredient(ModContent.ItemType<OccultistsEssence>());
                }
                if (recipe.HasResult<SnipersSoul>() && !recipe.HasResult<MutagenRanged>())
                {
                    recipe.AddIngredient<SharpshootersEssence>();
                    recipe.RemoveIngredient(ModContent.ItemType<SharpshootersEssence>());
                }
            }
        }
    }
}