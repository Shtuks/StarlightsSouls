using FargowiltasSouls.Content.Items.Accessories.Essences;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using Redemption.Items.Accessories.PostML;
using ssm.Core;
using ssm.Redemption.Mutagens;
using ssm.Thorium.Essences;
using ssm.Thorium.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class RedemptionDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class RedemptionTorRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<BardSoul>() && !recipe.HasResult<MutagenSymphonic>())
                {
                    recipe.AddIngredient<MutagenSymphonic>();
                    recipe.RemoveIngredient(ModContent.ItemType<BardEssence>());
                }
                if (recipe.HasResult<GuardianAngelsSoul>() && !recipe.HasResult<MutagenHealing>())
                {
                    recipe.AddIngredient<MutagenHealing>();
                    recipe.RemoveIngredient(ModContent.ItemType<HealerEssence>());
                }
                if (recipe.HasResult<OlympiansSoul>() && !recipe.HasResult<MutagenThrowing>())
                {
                    recipe.AddIngredient<MutagenThrowing>();
                    recipe.RemoveIngredient(ModContent.ItemType<SlingerEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    public class RedemptionSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

            }
        }
    }
}
