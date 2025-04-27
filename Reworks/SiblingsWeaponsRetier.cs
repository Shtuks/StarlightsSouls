using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using ssm.Content.Items.Consumables;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
    public class SiblingsWeaponsRetier : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<Penetrator>() || entity.type == ModContent.ItemType<SparklingLove>() || entity.type == ModContent.ItemType<StyxGazer>())
            {
                entity.damage *= 5;
            }
        }
    }
    public class SiblingsWeaponsRetierRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if ((recipe.HasResult(ModContent.ItemType<Penetrator>()) || recipe.HasResult(ModContent.ItemType<StyxGazer>()) || recipe.HasResult(ModContent.ItemType<SparklingLove>())) && !recipe.HasIngredient(ModContent.ItemType<Sadism>()))
                {
                    recipe.AddIngredient<Sadism>(30);
                }
            }
        }
    }
}
