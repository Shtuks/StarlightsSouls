using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.FathomSwarmer;
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
    public class FathomSwarmerEnchant : BaseEnchant
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
        public override Color nameColor => new(70, 63, 69);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<AmuletEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "LumenousAmulet").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpineEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "CorrosiveSpine").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "FathomSwarmerVisage").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "FathomSwarmerBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "FathomSwarmerBoots").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerVisage>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBoots>());
            recipe.AddIngredient(ModContent.ItemType<LumenousAmulet>());
            recipe.AddIngredient(ModContent.ItemType<CorrosiveSpine>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SpineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FathomSwarmerEnchant>();
        }
        public class AmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FathomSwarmerEnchant>();
        }
    }
}
