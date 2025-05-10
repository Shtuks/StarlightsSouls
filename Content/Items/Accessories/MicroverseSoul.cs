using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ID;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using ssm.Calamity.Souls;
using ssm.Thorium.Souls;
using ssm.SoA.Souls;
using Terraria.ModLoader;

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
            if (ModCompatibility.Calamity.Loaded || ModCompatibility.Thorium.Loaded || ModCompatibility.SacredTools.Loaded)
            {
                Recipe recipe = CreateRecipe(1);

                if (ModCompatibility.Calamity.Loaded && ModCompatibility.Crossmod.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "CalamitySoul", out ModItem cal);
                    recipe.AddIngredient(cal, 1);
                }
                if (ModCompatibility.Thorium.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "ThoriumSoul", out ModItem tor);
                    recipe.AddIngredient(tor, 1);
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "SoASoul", out ModItem soa);
                    recipe.AddIngredient(soa, 1);
                }

                recipe.AddIngredient<MicroverseSoul>(1);
                recipe.AddIngredient<EternalEnergy>(30);

                recipe.AddTile<CrucibleCosmosSheet>();
                recipe.Register();
            }
        }
    }
}
