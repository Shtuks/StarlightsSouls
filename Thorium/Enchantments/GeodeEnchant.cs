using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Geode;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ssm.Core;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.BasicAccessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GeodeEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.TorEnchantments;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.thorium.Name, "GeodeHelmet").UpdateArmorSet(player);

            //ModContent.Find<ModItem>(this.thorium.Name, "CrystalineCharm").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "CrystalSpearTip").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GeodeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GeodeChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GeodeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CrystalGeode>(), 100);
            recipe.AddIngredient(ModContent.ItemType<CrystalSpearTip>());
            recipe.AddIngredient(ModContent.ItemType<GeodePickaxe>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
