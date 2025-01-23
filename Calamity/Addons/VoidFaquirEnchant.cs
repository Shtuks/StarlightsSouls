using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using CalamityEntropy.Content.Items.Armor.VoidFaquir;
using CalamityEntropy.Content.Items.Weapons;
using CalamityEntropy.Content.Items.Weapons.Chainsaw;
using CalamityEntropy.Content.Items.Accessories;
using CalamityEntropy.Util;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class VoidFaquirEnchant : BaseEnchant
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
            if (player.AddEffect<VoidFaquirEffect>(Item))
            {
                player.Entropy().VFSet = true;
            }
            player.Entropy().reincarnationBadge = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddRecipeGroup("ssm:VoidHelmet");
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirCuises>());
            recipe.AddIngredient(ModContent.ItemType<GhostdomWhisper>());
            recipe.AddIngredient(ModContent.ItemType<Pioneer>());
            recipe.AddIngredient(ModContent.ItemType<ReincarnationBadge>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class VoidFaquirEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();
        }
    }
}
