using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using CalamityHunt.Content.Items.Armor.Shogun;
using CalamityHunt.Content.Items.Accessories;
using CalamityHunt.Content.Items.Misc;
using CalamityHunt.Common.Players;
using CalamityHunt.Content.Items.Weapons.Melee;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Goozma.Name)]
    public class ShogunEnchant : BaseEnchant
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
            if (player.AddEffect<ShogunEffect>(Item))
            {
                player.jumpSpeedBoost += 2f;
                player.GetModPlayer<ShogunArmorPlayer>().active = true;
            }

            if (player.AddEffect<TendrilsEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Goozma.Name, "TendrilCursorAttachment").UpdateAccessory(player, false);
            }
            if (player.AddEffect<SplendorEffect>(Item))
            {
                player.GetModPlayer<SplendorJamPlayer>().active = true;
                //ModContent.Find<ModItem>(ModCompatibility.Goozma.Name, "SplendorJam").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShogunHelm>());
            recipe.AddIngredient(ModContent.ItemType<ShogunChestplate>());
            recipe.AddIngredient(ModContent.ItemType<ShogunPants>());
            recipe.AddIngredient(ModContent.ItemType<SplendorJam>());
            recipe.AddIngredient(ModContent.ItemType<TendrilCursorAttachment>());
            recipe.AddIngredient(ModContent.ItemType<ScytheOfTheOldGod>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class ShogunEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
        }

        public class TendrilsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
        }
        public class SplendorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
        }
    }
}
