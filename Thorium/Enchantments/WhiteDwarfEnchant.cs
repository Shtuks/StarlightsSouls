using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Geode;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.Misc;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WhiteDwarfEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 300000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "WhiteDwarfMask").UpdateArmorSet(player);
            //player.GetModPlayer<ShtunThoriumPlayer>().WhiteDwarfEnchant = true;
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfMask>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGuard>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGreaves>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfKunai>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfPickaxe>());
            recipe.AddIngredient(ModContent.ItemType<AngelsEnd>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
