using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using ssm.Content.Items.Accessories;
using CalamityMod.Items.Accessories;
using Terraria.ID;
using Terraria.Localization;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ssm.Core;
using ssm.Calamity.Souls;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Souls;
using ssm.Thorium.Souls;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Armor.Bloodflare;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<SCalMask>(), 1).AddIngredient<AshesofAnnihilation>(10).AddIngredient<CoreofHavoc>(8).AddIngredient<GalacticaSingularity>(5).AddIngredient<BrimflameScowl>(1).AddTile<CosmicAnvil>().Register();
            Recipe.Create(ModContent.ItemType<SCalRobes>(), 1).AddIngredient<AshesofAnnihilation>(15).AddIngredient<CoreofHavoc>(10).AddIngredient<GalacticaSingularity>(7).AddIngredient<BrimflameRobes>(1).AddTile<CosmicAnvil>().Register();
            Recipe.Create(ModContent.ItemType<SCalBoots>(), 1).AddIngredient<AshesofAnnihilation>(12).AddIngredient<CoreofHavoc>(7).AddIngredient<GalacticaSingularity>(6).AddIngredient<BrimflameBoots>(1).AddTile<CosmicAnvil>().Register();
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Auric Helmet", ModContent.ItemType<AuricTeslaHoodedFacemask>(), ModContent.ItemType<AuricTeslaSpaceHelmet>(), ModContent.ItemType<AuricTeslaPlumedHelm>(), ModContent.ItemType<AuricTeslaRoyalHelm>(), ModContent.ItemType<AuricTeslaWireHemmedVisage>());
            RecipeGroup.RegisterGroup("ssm:Auric", rec);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Godslayer Helmet", ModContent.ItemType<GodSlayerHeadMelee>(), ModContent.ItemType<GodSlayerHeadRanged>(), ModContent.ItemType<GodSlayerHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Godslayer", rec2);
            RecipeGroup rec3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silva Helmet", ModContent.ItemType<SilvaHeadMagic>(), ModContent.ItemType<SilvaHeadSummon>());
            RecipeGroup.RegisterGroup("ssm:Silva", rec3);
            RecipeGroup rec4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tarragon Helmet", ModContent.ItemType<TarragonHeadMagic>(), ModContent.ItemType<TarragonHeadMelee>(), ModContent.ItemType<TarragonHeadRanged>(), ModContent.ItemType<TarragonHeadSummon>(), ModContent.ItemType<TarragonHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Tarragon", rec4);
            RecipeGroup rec5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Bloodflare Helmet", ModContent.ItemType<BloodflareHeadMagic>(), ModContent.ItemType<BloodflareHeadMelee>(), ModContent.ItemType<BloodflareHeadRanged>(), ModContent.ItemType<BloodflareHeadSummon>(), ModContent.ItemType<BloodflareHeadRogue>());
            RecipeGroup.RegisterGroup("ssm:Bloodflare", rec5);
        }


        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                #region other
                if (ShtunConfig.Instance.OldCalDlcBalance)
                {
                    if (recipe.HasResult(ModContent.ItemType<ShadowspecBar>()) && recipe.HasIngredient<EternalEnergy>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<EternalEnergy>());
                    }
                    if (!recipe.HasIngredient<ShadowspecBar>() && recipe.HasIngredient<AshesofAnnihilation>())
                    {
                        if (recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>())
                        {
                            if (recipe.RemoveIngredient(ModContent.ItemType<AshesofAnnihilation>()) && recipe.RemoveIngredient(ModContent.ItemType<ExoPrism>()))
                                recipe.AddIngredient<ShadowspecBar>(5);
                        }
                    }
                }
                #endregion

                #region souls
                if (!ShtunConfig.Instance.OldCalDlcBalance)
                {
                    if (recipe.HasResult(ModContent.ItemType<SoASoul>()) && !recipe.HasIngredient<ExoPrism>())
                    {
                        recipe.AddIngredient<ExoPrism>(5);
                        recipe.AddIngredient<AshesofAnnihilation>(5);
                    }
                    //if (recipe.HasResult(ModContent.ItemType<RedemptionSoul>()) && !recipe.HasIngredient<ExoPrism>())
                    //{
                    //    recipe.AddIngredient<ExoPrism>(5);
                    //    recipe.AddIngredient<AshesofAnnihilation>(5);
                    //}
                    if (recipe.HasResult(ModContent.ItemType<ThoriumSoul>()) && !recipe.HasIngredient<ExoPrism>())
                    {
                        recipe.AddIngredient<ExoPrism>(5);
                        recipe.AddIngredient<AshesofAnnihilation>(5);
                    }
                    if (recipe.HasResult(ModContent.ItemType<CalamitySoul>()) && !recipe.HasIngredient<ExoPrism>())
                    {
                        recipe.AddIngredient<ExoPrism>(5);
                        recipe.AddIngredient<AshesofAnnihilation>(5);
                    }
                }
                else
                {
                    if (recipe.HasResult(ModContent.ItemType<SoASoul>()) && !recipe.HasIngredient<ShadowspecBar>())
                    {
                        recipe.AddIngredient<ShadowspecBar>(5);
                    }
                    //if (recipe.HasResult(ModContent.ItemType<RedemptionSoul>()) && !recipe.HasIngredient<ExoPrism>())
                    //{
                    //    recipe.AddIngredient<ShadowspecBar>(5);
                    //}
                    if (recipe.HasResult(ModContent.ItemType<ThoriumSoul>()) && !recipe.HasIngredient<ShadowspecBar>())
                    {
                        recipe.AddIngredient<ShadowspecBar>(5);
                    }
                    if (recipe.HasResult(ModContent.ItemType<CalamitySoul>()) && !recipe.HasIngredient<ShadowspecBar>())
                    {
                        recipe.AddIngredient<ShadowspecBar>(5);
                    }
                }
                #endregion
            }
        }
    }
}


