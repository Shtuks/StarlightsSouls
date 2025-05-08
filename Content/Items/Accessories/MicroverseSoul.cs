using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ID;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Calamity.Souls;
using ssm.Thorium.Souls;
using ssm.SoA.Souls;

namespace ssm.Content.Items.Accessories
{
    public class MacroverseSoul : BaseSoul
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
            Item.defense = 100;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            if (ModCompatibility.Calamity.Loaded)
            {
                recipe.AddIngredient<CalamitySoul>(1);
            }
            if (ModCompatibility.Thorium.Loaded)
            {
                recipe.AddIngredient<ThoriumSoul>(1);
            }
            if (ModCompatibility.SacredTools.Loaded)
            {
                recipe.AddIngredient<SoASoul>(1);
            }

            recipe.AddIngredient<MicroverseSoul>(1);

            recipe.AddIngredient<EternalEnergy>(30);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
