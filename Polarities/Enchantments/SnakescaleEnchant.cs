using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities;
using Polarities.Content.Items.Armor.Classless.Hardmode.SnakescaleArmor;
using Polarities.Content.Items.Weapons.Melee.Flails.Hardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SnakescaleEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(44, 64, 138);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ignoreWater = true;
            player.GetModPlayer<PolaritiesPlayer>().snakescaleSetBonus = true;
            player.GetModPlayer<PolaritiesPlayer>().critDamageBoostMultiplier *= 1.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SnakescaleMask>());
            recipe.AddIngredient(ModContent.ItemType<SnakescaleArmor>());
            recipe.AddIngredient(ModContent.ItemType<SnakescaleGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Snakebite>());
            //recipe.AddIngredient(ModContent.ItemType<SelfsimilarBow>());
            //recipe.AddIngredient(ModContent.ItemType<SelfsimilarSlasher>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
