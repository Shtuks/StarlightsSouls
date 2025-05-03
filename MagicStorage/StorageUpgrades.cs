using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using MagicStorage.Items;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.MagicStorage
{
    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class UpgradeDeviating : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 99;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 0, 64);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DeviatingEnergy>(10);
            recipe.AddIngredient<ShadowDiamond>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }

    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class UpgradeAbominable : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 99;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 1, 28);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<ShadowDiamond>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }

    [ExtendsFromMod(ModCompatibility.MagicStorage.Name)]
    [JITWhenModsEnabled(ModCompatibility.MagicStorage.Name)]
    public class UpgradeEternal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 99;
            Item.rare = 11;
            Item.value = Item.sellPrice(0, 2, 56);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<EternalEnergy>(10);
            recipe.AddIngredient<ShadowDiamond>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
