using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Auric;
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
    public class AuricTeslaEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");
        public override Color nameColor => new(255, 213, 0);
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
            if (player.AddEffect<DragonScalesEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DragonScales").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpiritOriginEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DaawnlightSpiritOrigin").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PermafrostsEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "PermafrostsConcoction").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<TransformerEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TheTransformer").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<YharimJamEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamitybardhealer.Name, "YharimsJam").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaWireHemmedVisage").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaRoyalHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaHoodedFacemask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaSpaceHelmet").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaPlumedHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "AugmentedAuricTeslaFeatheredHeadwear").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "AugmentedAuricTeslaValkyrieVisage").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "AuricTeslaHealerHead").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "AuricTeslaFrilledHelmet").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaBodyArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AuricTeslaCuisses").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaRoyalHelm>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHoodedFacemask>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaWireHemmedVisage>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaSpaceHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaPlumedHelm>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaCuisses>());
            recipe.AddIngredient(ModContent.ItemType<DaawnlightSpiritOrigin>());
            recipe.AddIngredient(ModContent.ItemType<TheTransformer>());
            recipe.AddIngredient(ModContent.ItemType<DragonScales>());
            recipe.AddIngredient(ModContent.ItemType<PermafrostsConcoction>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class DragonScalesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
        }
        public class SpiritOriginEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
        }
        public class PermafrostsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
        }
        public class TransformerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
        }
        public class YharimJamEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
        }
    }

}
