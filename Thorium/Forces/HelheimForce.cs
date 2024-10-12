using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using ssm.Thorium.Enchantments;
using ssm.Core;

namespace ssm.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class HelheimForce : ModItem
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SpiritTrapperEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DreadEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DemonBloodEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "BerserkerEnchant").UpdateAccessory(player, hideVisual);
            //ModContent.Find<ModItem>(((ModType)this).Mod.Name, "HarbingerEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DreadEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DemonBloodEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BerserkerEnchant>());
            //recipe.AddIngredient(ModContent.ItemType<HarbingerEnchant>());

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }
    }
}
