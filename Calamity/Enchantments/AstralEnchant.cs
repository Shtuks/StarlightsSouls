using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Astral;
using CalamityMod.Items.Fishing.AstralCatches;
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
    internal class AstralEnchant : BaseEnchant
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
        public override Color nameColor => new(0, 255, 195);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<UrsaEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "UrsaSergeant").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GravistarEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "GravistarSabaton").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "AstralHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AstralBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AstralLeggings").UpdateArmorSet(player);
        }
        public class UrsaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstralEnchant>();
        }
        public class GravistarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstralEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AstralHelm>());
            recipe.AddIngredient(ModContent.ItemType<AstralBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<AstralLeggings>());
            recipe.AddIngredient(ModContent.ItemType<UrsaSergeant>());
            recipe.AddIngredient(ModContent.ItemType<GravistarSabaton>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
