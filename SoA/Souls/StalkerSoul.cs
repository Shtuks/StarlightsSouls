using Fargowiltas.Items.Tiles;
using ssm.Content.DamageClasses;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using ssm.Core;

namespace ssm.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StalkerSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            player.GetDamage<UnitedModdedThrower>() += 0.22f;
            player.GetCritChance<UnitedModdedThrower>() += 10f;
            player.GetAttackSpeed<UnitedModdedThrower>() += 0.15f;
            player.Shtun().throwerVelocity += 0.20f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            //recipe.AddIngredient<Essence>();


            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
