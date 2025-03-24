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
using CalamityEntropy.Content.Items.Armor.Marivinium;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class MariviumEnchant : BaseEnchant
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
            if (player.AddEffect<MariviniumEffect>(Item))
            {
                player.Entropy().MariviniumSet = true;
            }
            if (player.AddEffect<HolyShieldEffect>(Item))
            {
                player.Entropy().holyMantle = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MariviniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Xytheron>());
            recipe.AddIngredient(ModContent.ItemType<WyrmToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<HolyMantle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class MariviniumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();
        }
        public class HolyShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();
        }
    }
}
