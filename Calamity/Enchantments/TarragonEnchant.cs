using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Tarragon;
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
    public class TarragonEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod calamitybardhealer = ModLoader.GetMod("CalamityBardHealer");

        private readonly Mod ragnarok = ModLoader.GetMod("RagnarokMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(169, 106, 52);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<DarkRingEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DarkSunRing").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BlazingEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "BlazingCore").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BraveryEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "BadgeofBravery").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "TarragonHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "TarragonParagonCrown").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "TarragonChapeau").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "TarragonShroud").UpdateArmorSet(player);
            ModContent.Find<ModItem>(ragnarok.Name, "TarragonCowl").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "TarragonLeggings").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<TarragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TarragonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DarkSunRing>());
            recipe.AddIngredient(ModContent.ItemType<BadgeofBravery>());
            recipe.AddIngredient(ModContent.ItemType<BlazingCore>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class DarkRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
        }

        public class BlazingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
        }
        public class BraveryEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
        }
    }
}
