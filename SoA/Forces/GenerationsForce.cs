using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using Fargowiltas.Items.Tiles;
using ssm.SoA.Enchantments;
using ssm.Core;

namespace ssm.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class GenerationsForce : BaseForce
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CairoCrusaderEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "EerieEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "BismuthEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DreadfireEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MarstechEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<CairoCrusaderEnchant>();
            recipe.AddIngredient<EerieEnchant>();
            recipe.AddIngredient<BismuthEnchant>();
            recipe.AddIngredient<DreadfireEnchant>();
            recipe.AddIngredient<MarstechEnchant>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
