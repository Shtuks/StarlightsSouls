using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria.ModLoader;
using SacredTools.Content.Items.Accessories;
using Terraria.Localization;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Dragon;
using ssm.SoA.Souls;
using ssm.Core;
using SacredTools.Content.Items.Accessories.Wings;
using SacredTools.Content.Items.DEV;
using SacredTools.Content.Items.Placeable.CraftingStations;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using SacredTools.Content.Items.Materials;

namespace ssm
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoARecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Asthral Helmet", ModContent.ItemType<AsthralMage>(), ModContent.ItemType<AsthralRanged>(), ModContent.ItemType<AsthralMelee>(), ModContent.ItemType<AsthralSummon>(), ModContent.ItemType<AsthraltiteHelmetRevenant>());
            RecipeGroup.RegisterGroup("ssm:AsthralHelms", rec);
            RecipeGroup rec2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Flarium Helmet", ModContent.ItemType<FlariumCrown>(), ModContent.ItemType<FlariumMask>(), ModContent.ItemType<FlariumCowl>());
            RecipeGroup.RegisterGroup("ssm:FlariumHelms", rec2);
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<OblivionForge>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<RageSuppressor>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<MilinticaDash>(), 1).AddIngredient<BetaCoupon>(2).Register();
            Recipe.Create(ModContent.ItemType<HeartOfThePlough>(), 1).AddIngredient<BetaCoupon>(2).Register();
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    if (recipe.HasResult<UniverseSoul>() && !recipe.HasIngredient<StalkerSoul>())
                    {
                        recipe.AddIngredient<StalkerSoul>();
                    }
                }

                if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }

                if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SoASoul>())
                {
                    recipe.AddIngredient<SoASoul>();
                }

                if (recipe.HasResult<ConjuristsSoul>() && !recipe.HasIngredient<StarstreamVeil>())
                {
                    recipe.AddIngredient<StarstreamVeil>();
                    recipe.RemoveIngredient(3812);
                    recipe.RemoveIngredient(3810);
                    recipe.RemoveIngredient(3811);
                    recipe.RemoveIngredient(3809);
                }

                if (recipe.HasResult<ColossusSoul>() && !recipe.HasIngredient<RoyalGuard>())
                {
                    recipe.AddIngredient<RoyalGuard>();
                    recipe.AddIngredient<NightmareBlindfold>();
                }

                if (recipe.HasResult<SupersonicSoul>() && !recipe.HasIngredient<MilinticaDash>())
                {
                    recipe.AddIngredient<MilinticaDash>();
                    recipe.AddIngredient<HeartOfThePlough>();
                }

                if (recipe.HasResult<WorldShaperSoul>() && !recipe.HasIngredient<LunarRing>())
                {
                    recipe.AddIngredient<LunarRing>();
                    recipe.AddIngredient<RageSuppressor>();
                }

                if (recipe.HasResult<MasochistSoul>() && !recipe.HasIngredient<YataMirror>())
                {
                    recipe.AddIngredient<YataMirror>();
                    recipe.AddIngredient<PrimordialCore>();
                }

                if (recipe.HasResult<BerserkerSoul>() && !recipe.HasIngredient<FloraFist>())
                {
                    if (recipe.HasIngredient(1343))
                    {
                        recipe.RemoveIngredient(1343);
                    }
                    recipe.AddIngredient<FloraFist>();
                }

                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<GrandWings>())
                {
                    recipe.AddIngredient<GrandWings>();
                    recipe.AddIngredient<AsthraltiteWings>();
                    recipe.AddIngredient<DespairBoosters>();
                    recipe.AddIngredient<AuroraWings>();
                }

                if (recipe.createItem.ModItem is BaseForce)
                {
                    if (!recipe.HasIngredient<TraceOfChaos>())
                        recipe.AddIngredient<TraceOfChaos>(4);
                }
            }
        }
    }
}
