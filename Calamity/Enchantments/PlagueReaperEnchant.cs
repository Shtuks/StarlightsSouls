using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.PlagueReaper;
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
    public class PlagueReaperEnchant : BaseEnchant
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
        public override Color nameColor => new(0, 71, 12);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<AlchemicalEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "AlchemicalFlask").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FuelPackEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "PlaguedFuelPack").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "PlagueReaperMask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "PlagueReaperVest").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "PlagueReaperStriders").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperMask>());
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperVest>());
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperStriders>());
            recipe.AddIngredient(ModContent.ItemType<PlaguedFuelPack>());
            recipe.AddIngredient(ModContent.ItemType<AlchemicalFlask>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class AlchemicalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueReaperEnchant>();
        }
        public class FuelPackEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueReaperEnchant>();
        }
    }
}
