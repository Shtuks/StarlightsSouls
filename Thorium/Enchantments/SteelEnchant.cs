using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.HealerItems;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SteelEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

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
            //steel effect
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance += 0.1f;
            }

            ModContent.Find<ModItem>(this.thorium.Name, "SpikedBracer").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(this.thorium.Name, "ThoriumShield").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<SteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumShield>());
            recipe.AddIngredient(ModContent.ItemType<SpikedBracer>());
            recipe.AddIngredient(ModContent.ItemType<SteelBlade>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
