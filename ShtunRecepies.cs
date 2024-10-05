using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using Redemption.Items.Materials.PostML;
using SacredTools.Content.Items.Materials;
using ssm.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.HM;
using CalamityMod.Items.Accessories;
using Terraria.ID;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Armor.PreHM.CommonGuard;
using Terraria.Localization;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Dragon;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using ssm.SoA.Souls;

namespace ssm
{
    public class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<SCalMask>(), 1).AddIngredient<AshesofAnnihilation>(10).AddIngredient<CoreofHavoc>(8).AddIngredient<GalacticaSingularity>(5).AddIngredient<BrimflameScowl>(1).AddTile<CosmicAnvil>().Register();
            Recipe.Create(ModContent.ItemType<SCalRobes>(), 1).AddIngredient<AshesofAnnihilation>(15).AddIngredient<CoreofHavoc>(10).AddIngredient<GalacticaSingularity>(7).AddIngredient<BrimflameRobes>(1).AddTile<CosmicAnvil>().Register();
            Recipe.Create(ModContent.ItemType<SCalBoots>(), 1).AddIngredient<AshesofAnnihilation>(12).AddIngredient<CoreofHavoc>(7).AddIngredient<GalacticaSingularity>(6).AddIngredient<BrimflameBoots>(1).AddTile<CosmicAnvil>().Register();
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
                if (recipe.HasResult<TerrariaSoul>())
                {
                    recipe.AddIngredient<CelestialEnchant>();
                }
                if (recipe.HasResult<EternitySoul>() && !recipe.HasIngredient<SoASoul>())
                {
                    recipe.AddIngredient<SoASoul>();
                }

                // SoA Recipies
                if (recipe.HasResult<ShadowspecBar>())
                {
                    recipe.AddIngredient<EmberOfOmen>();
                }
                if (recipe.HasResult<ColossusSoul>())
                {
                    recipe.AddIngredient<ReflectionShield>();
                }

                // Redemption Recipies
                if (recipe.HasResult<AuricBar>())
                {
                    recipe.AddIngredient<LifeFragment>();
                }
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
                if (recipe.HasResult<AngelTreads>() && recipe.HasIngredient(ItemID.TerrasparkBoots))
                {
                    if (recipe.RemoveIngredient(ItemID.TerrasparkBoots))
                        recipe.AddIngredient<RoyalRunners>();
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