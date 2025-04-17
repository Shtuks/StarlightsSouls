using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpiderCave.Armor;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class RootEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
            modPlayer.RootSet = true;

            //ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<RootHead>();
            recipe.AddIngredient<RootBody>();
            recipe.AddIngredient<RootLegs>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
