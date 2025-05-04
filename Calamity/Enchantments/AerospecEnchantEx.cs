using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Armor.Aerospec;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AerospecEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod Ragnarok = ModLoader.GetMod("RagnarokMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod fargocross = ModLoader.GetMod("FargowiltasCrossmod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(153, 200, 193);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<GladiatorsLocketEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "GladiatorsLocket").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<UnstableGraniteEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "UnstableGraniteCore").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FeatherCrownEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "FeatherCrown").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "AerospecHelm").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecHood").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecHat").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecHelmet").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecHeadgear").UpdateArmorSet(player);
            ModContent.Find<ModItem>(Ragnarok.Name, "AerospecBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(Ragnarok.Name, "AerospecHealer").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "AerospecBiretta").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "AerospecHeadphones").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "AerospecLeggings").UpdateArmorSet(player);
            ModContent.Find<ModItem>(fargocross.Name, "AerospecEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AerospecHelm>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHood>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHat>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<AerospecBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<AerospecLeggings>());
            recipe.AddIngredient(ModContent.ItemType<FeatherCrown>());
            recipe.AddIngredient(ModContent.ItemType<GladiatorsLocket>());
            recipe.AddIngredient(ModContent.ItemType<UnstableGraniteCore>());
            recipe.AddIngredient(ModContent.ItemType<AerospecEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class GladiatorsLocketEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
        public class UnstableGraniteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
        public class FeatherCrownEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
    }
}