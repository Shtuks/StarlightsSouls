using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using Fargowiltas.Items.Tiles;

namespace ssm.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ExplorationForceEx : BaseForce
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "WulfrumEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AerospecEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DesertProwlerEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MarniteEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "VictideEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SulphurousEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "StatigelEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SnowRuffianEnchantEx").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "WulfrumEnchantEx");
            recipe.AddIngredient(null, "StatigelEnchantEx");
            recipe.AddIngredient(null, "VictideEnchantEx");
            recipe.AddIngredient(null, "SnowRuffianEnchantEx");
            recipe.AddIngredient(null, "SulphurousEnchantEx");
            recipe.AddIngredient(null, "AerospecEnchantEx");
            recipe.AddIngredient(null, "DesertProwlerEnchantEx");
            recipe.AddIngredient(null, "MarniteEnchantEx");

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}