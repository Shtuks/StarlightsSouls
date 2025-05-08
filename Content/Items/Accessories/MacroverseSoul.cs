using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ID;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Spooky.Forces;
using ssm.Redemption.Forces;
using ssm.Polarities.Forces;
using Fargowiltas.Items.Tiles;

namespace ssm.Content.Items.Accessories
{
    public class MicroverseSoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = 5000000;
            Item.rare = 11;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            if (ModCompatibility.Spooky.Loaded)
            {
                recipe.AddIngredient<TerrorForce>(1);
                recipe.AddIngredient<HorrorForce>(1);
            }
            if (ModCompatibility.Polarities.Loaded)
            {
                recipe.AddIngredient<WildernessForce>(1);
                recipe.AddIngredient<SpacetimeForce>(1);
            }
            if (ModCompatibility.Redemption.Loaded)
            {
                recipe.AddIngredient<AdvancementForce>(1);
            }

            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
