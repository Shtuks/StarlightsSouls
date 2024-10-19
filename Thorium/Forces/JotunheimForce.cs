using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using ssm.Thorium.Enchantments;
using FargowiltasSouls;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Forces;

namespace ssm.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JotunheimForce : BaseForce
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DepthDiverEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "TideHunterEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "NagaSkinEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CryomancerEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "WhisperingEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DepthDiverEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterEnchant>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CryomancerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<WhisperingEnchant>());

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }
    }
}
