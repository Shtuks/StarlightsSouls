using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Terraria.Localization;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;

namespace ssm.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AsgardForce : BaseForce
    {

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "WhiteKnightEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SacredEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FallenPaladinEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CelestiaEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "RhapsodistEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "WhiteKnightEnchant");
            recipe.AddIngredient(null, "SacredEnchant");
            recipe.AddIngredient(null, "FallenPaladinEnchant");
            recipe.AddIngredient(null, "CelestiaEnchant");
            recipe.AddIngredient(null, "RhapsodistEnchant");

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
