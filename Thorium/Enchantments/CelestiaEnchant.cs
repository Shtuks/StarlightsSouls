using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Tracker;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CelestiaEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //toggle
                ModContent.Find<ModItem>(this.thorium.Name, "CelestialCrown").UpdateArmorSet(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CelestialCrown>());
            recipe.AddIngredient(ModContent.ItemType<CelestialVestment>());
            recipe.AddIngredient(ModContent.ItemType<CelestialLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CelestialWand>());
            //recipe.AddIngredient(ModContent.ItemType<HealingRain>());
            recipe.AddIngredient(ModContent.ItemType<AncientTome>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
