using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Silva;
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
    public class SilvaEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");

        public override Color nameColor => new(176, 112, 70);

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<TreeAmuletEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamitybardhealer.Name, "TreeWhispererAmulet").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ElementalEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamitybardhealer.Name, "ElementalBloom").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AbsorberEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TheAbsorber").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DynamoEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DynamoStemCells").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BlunderBoostEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "BlunderBooster").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "SilvaHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "SilvaHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "SilvaGuardianHelmet").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "SilvaHeadHealer").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "SilvaArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "SilvaLeggings").UpdateArmorSet(player);
        }
        public class TreeAmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
        }
        public class ElementalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
        }
        public class AbsorberEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
        }
        public class DynamoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
        }
        public class BlunderBoostEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SilvaHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<SilvaHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<SilvaArmor>());
            recipe.AddIngredient(ModContent.ItemType<SilvaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DynamoStemCells>());
            recipe.AddIngredient(ModContent.ItemType<BlunderBooster>());
            recipe.AddIngredient(ModContent.ItemType<TheAbsorber>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
