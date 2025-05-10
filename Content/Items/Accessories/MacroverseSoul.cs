using FargowiltasSouls.Content.Items.Accessories.Souls;
using Terraria;
using Terraria.ID;
using ssm.Core;
using FargowiltasSouls.Content.Items.Materials;
using Fargowiltas.Items.Tiles;
using Terraria.ModLoader;

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
            if (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded || ModCompatibility.Spooky.Loaded)
            {
                Recipe recipe = CreateRecipe(1);

                if (ModCompatibility.Spooky.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "HorrorForce", out ModItem hor);
                    ModContent.TryFind<ModItem>(Mod.Name, "TerrrorForce", out ModItem ter);
                    recipe.AddIngredient(hor, 1);
                    recipe.AddIngredient(ter, 1);
                }
                if (ModCompatibility.Polarities.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "WildernessForce", out ModItem wil);
                    ModContent.TryFind<ModItem>(Mod.Name, "SpacetimeForce", out ModItem spa);
                    recipe.AddIngredient(wil, 1);
                    recipe.AddIngredient(spa, 1);
                }
                if (ModCompatibility.Redemption.Loaded)
                {
                    ModContent.TryFind<ModItem>(Mod.Name, "AdvancementForce", out ModItem adv);
                    recipe.AddIngredient(adv, 1);
                }

                recipe.AddIngredient<AbomEnergy>(10);

                recipe.AddTile<CrucibleCosmosSheet>();
                recipe.Register();
            }
        }
    }
}
