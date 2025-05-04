using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using Clamity;
using Clamity.Content.Biomes.FrozenHell.Items.FrozenArmor;
using Clamity.Content.Items.Weapons.Melee.Swords;
using CalamityMod;
using Clamity.Content.Bosses.WoB.Drop;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class FrozenEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(ModContent.GetInstance<TrueMeleeDamageClass>()) += 0.2f;
            player.GetAttackSpeed<MeleeDamageClass>() += 0.2f;
            player.Clamity().inflicingMeleeFrostburn = true;
            player.Clamity().frozenParrying = true;
            player.buffImmune[44] = true;
            player.buffImmune[324] = true;
            player.buffImmune[47] = true;
            player.aggro += 400;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneVisor>());
            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FrozenVolcano>());
            recipe.AddIngredient(ModContent.ItemType<AMS>());
            recipe.AddIngredient(ModContent.ItemType<TheWOBbler>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
