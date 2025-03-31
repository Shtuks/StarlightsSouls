using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod;
using Polarities.Content.Items.Armor.Classless.PreHardmode.AerogelArmor;

namespace ssm.Polarities.Enchantments
{
    //[ExtendsFromMod(ModCompatibility.Polarities.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    //public class AerogelEnchant : BaseEnchant
    //{
    //    public override void SetDefaults()
    //    {
    //        Item.width = 20;
    //        Item.height = 20;
    //        Item.accessory = true;
    //        ItemID.Sets.ItemNoGravity[Item.type] = true;
    //        Item.rare = 10;
    //        Item.value = 400000;
    //    }

    //    public override Color nameColor => new(150, 168, 214);

    //    public override void UpdateAccessory(Player player, bool hideVisual)
    //    {
    //        ModContent.Find<ModItem>(this.thorium.Name, "DartPouch").UpdateAccessory(player, hideVisual);
    //    }

    //    public override void AddRecipes()
    //    {
    //        Recipe recipe = this.CreateRecipe();

    //        recipe.AddIngredient(ModContent.ItemType<AerogelHood>());
    //        recipe.AddIngredient(ModContent.ItemType<AerogelRobe>());
    //        recipe.AddIngredient(ModContent.ItemType<MasterArbalestHood>());
    //        recipe.AddIngredient(ModContent.ItemType<DartPouch>());
    //        recipe.AddIngredient(ModContent.ItemType<TheBlackBow>());
    //        recipe.AddIngredient(ModContent.ItemType<WyrmDecimator>());

    //        recipe.AddTile(TileID.LunarCraftingStation);
    //        recipe.Register();
    //    }
    //}
}
