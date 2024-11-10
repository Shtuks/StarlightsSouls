using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Painting;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Coral;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OceanEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "CoralHelmet").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.thorium.Name, "SeaBreezePendant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "BubbleMagnet").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CoralHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CoralChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<CoralGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SeaBreezePendant>());
            recipe.AddIngredient(ModContent.ItemType<BubbleMagnet>());
            recipe.AddIngredient(ItemID.Swordfish);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
