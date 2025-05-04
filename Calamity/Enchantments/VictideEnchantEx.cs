using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Victide;
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
    public class VictideEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");

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

        public override Color nameColor => new(255, 233, 197);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<FungalEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "FungalClump").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ClumpEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "FungalSymbiote").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SeaShieldEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ShieldoftheOcean").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "VictideHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "VictideAmmoniteHat").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "VictideHeadBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "VictideHeadHealer").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "VictideGreaves").UpdateArmorSet(player);
            ModContent.Find<ModItem>(fargocross.Name, "VictideEnchant").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VictideHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<VictideEnchant>());
            recipe.AddIngredient(ModContent.ItemType<VictideBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<VictideGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FungalClump>());
            recipe.AddIngredient(ModContent.ItemType<FungalSymbiote>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class FungalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
        }
        public class ClumpEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
        }
        public class SeaShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
        }

    }
}