using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Umbraphile;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class UmbraphileEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(163, 0, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<VampiricEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "VampiricTalisman").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DecietEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "CoinofDeceit").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "UmbraphileHood").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "UmbraphileRegalia").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "UmbraphileBoots").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UmbraphileHood>());
            recipe.AddIngredient(ModContent.ItemType<UmbraphileRegalia>());
            recipe.AddIngredient(ModContent.ItemType<UmbraphileBoots>());
            recipe.AddIngredient(ModContent.ItemType<VampiricTalisman>());
            recipe.AddIngredient(ModContent.ItemType<CoinofDeceit>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class VampiricEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<UmbraphileEnchant>();
        }
        public class DecietEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<UmbraphileEnchant>();
        }
    }
}
