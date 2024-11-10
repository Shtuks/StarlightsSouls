using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.DD;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DurasteelEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //durasteel effect
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance += 0.12f;
            }

            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            player.dash = 1;

            ModContent.GetModItem(ModContent.ItemType<BallnChain>()).UpdateAccessory(player, hideVisual);
            ModContent.GetModItem(ModContent.ItemType<SpikedBracer>()).UpdateAccessory(player, hideVisual);

            //toggle
                ModContent.GetModItem(ModContent.ItemType<IncandescentSpark>()).UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DurasteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<IncandescentSpark>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelBlade>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
