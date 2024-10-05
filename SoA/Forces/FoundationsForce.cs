using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Enchantments;

namespace ssm.SoA.Forces
{
    public class FoundationsForce : BaseForce
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PrairieEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "LapisEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FrosthunterEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "BlightboneEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PrairieEnchant>();
            recipe.AddIngredient<LapisEnchant>();
            recipe.AddIngredient<FrosthunterEnchant>();
            recipe.AddIngredient<BlightboneEnchant>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}