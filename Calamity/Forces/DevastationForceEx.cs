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
    public class DevastationForceEx : BaseForce
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HydrothermicEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "TitanHeartEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DaedalusEnchantEx").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ReaverEnchantEx").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "TitanHeartEnchantEx");
            recipe.AddIngredient(null, "HydrothermicEnchantEx");
            recipe.AddIngredient(null, "DaedalusEnchantEx");
            recipe.AddIngredient(null, "ReaverEnchantEx");

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            
            recipe.Register();
        }
    }
}
