using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ssm.Core;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Tracker;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ShootingStarEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 250000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //dmg, regen
            //thoriumPlayer.setBalladeer = true;
            //move speed, play speed
            thoriumPlayer.accHeadset = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            //recipe.AddIngredient(ModContent.ItemType<BalladeerHat>());
            //recipe.AddIngredient(ModContent.ItemType<BalladeerShirt>());
            //recipe.AddIngredient(ModContent.ItemType<BalladeerBoots>());
            recipe.AddIngredient(ModContent.ItemType<Headset>());
            recipe.AddIngredient(ModContent.ItemType<AcousticGuitar>());
            recipe.AddIngredient(ModContent.ItemType<SunflareGuitar>());


            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
