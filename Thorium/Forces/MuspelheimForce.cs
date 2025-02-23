using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using ssm.Thorium.Enchantments;
using ssm.Core;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Forces;

namespace ssm.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MuspelheimForce : BaseForce
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
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "CyberPunkEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "DemonBloodEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SandstoneEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "NobleEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PyromancerEnchant").UpdateAccessory(player, hideVisual);
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();

        //    recipe.AddIngredient(null, "CyberPunkEnchant");
        //    recipe.AddIngredient(null, "DemonBloodEnchant");
        //    recipe.AddIngredient(null, "SandstoneEnchant");
        //    recipe.AddIngredient(null, "NobleEnchant");
        //    recipe.AddIngredient(null, "PyromancesEnchant");

        //    recipe.AddTile(TileID.LunarCraftingStation);

        //    recipe.Register();
        //}
    }
}
