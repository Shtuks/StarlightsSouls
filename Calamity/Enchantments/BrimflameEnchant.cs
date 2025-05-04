using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Brimflame;
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
    public class BrimflameEnchant : BaseEnchant
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
        public override Color nameColor => new(191, 68, 59);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<FlameShellEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "FlameLickedShell").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AbaddonEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "Abaddon").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SlagSpitterEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "SlagsplitterPauldron").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VoidCalamityEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "VoidofCalamity").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SigilEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "SigilofCalamitas").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "BrimflameScowl").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BrimflameRobes").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "BrimflameBoots").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrimflameScowl>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<VoidofCalamity>());
            recipe.AddIngredient(ModContent.ItemType<Abaddon>());
            recipe.AddIngredient(ModContent.ItemType<SlagsplitterPauldron>());
            recipe.AddIngredient(ModContent.ItemType<SigilofCalamitas>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class FlameShellEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
        }
        public class AbaddonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
        }
        public class SlagSpitterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
        }
        public class VoidCalamityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
        }
        public class SigilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
        }
    }
}
