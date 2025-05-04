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
    public class DesolationForce : BaseForce
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "MolluskEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "OmegaBlueEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FathomSwarmerEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "UmbraphileEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AstralEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "MolluskEnchant");
            recipe.AddIngredient(null, "OmegaBlueEnchant");
            recipe.AddIngredient(null, "FathomSwarmerEnchant");
            recipe.AddIngredient(null, "UmbraphileEnchant");
            recipe.AddIngredient(null, "AstralEnchant");


            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
