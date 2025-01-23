using FargowiltasSouls.Content.Items.Accessories.Forces;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Core;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name, ModCompatibility.Goozma.Name, ModCompatibility.Clamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name, ModCompatibility.Goozma.Name, ModCompatibility.Clamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class AddonsForce : BaseForce
    {
        public override string Texture => "ssm/Content/Projectiles/Deathrays/AbomDeathray";
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DivineEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "EntropicEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "IntergelacticEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "ShogunEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "VoidFaquirEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FrozenEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "DivineEnchant");
            recipe.AddIngredient(null, "EntropicEnchant");
            recipe.AddIngredient(null, "IntergelacticEnchant");
            recipe.AddIngredient(null, "ShogunEnchant");
            recipe.AddIngredient(null, "VoidFaquirEnchant");
            recipe.AddIngredient(null, "FrozenEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }
    }
}
