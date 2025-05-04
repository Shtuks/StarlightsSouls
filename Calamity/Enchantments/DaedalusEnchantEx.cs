using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Daedalus;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
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
    public class DaedalusEnchantEx : BaseEnchant
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
        public override Color nameColor => new(132, 212, 246);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<OrnateEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "OrnateShield").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FrostFlareEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "FrostFlare").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(ragnarok.Name, "DaedalusHeadBard").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "DaedalusCowl").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamitybardhealer.Name, "DaedalusHat").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusHeadMelee").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusHeadMagic").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusHeadRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusHeadRogue").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusHeadSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusBreastplate").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "DaedalusLeggings").UpdateArmorSet(player);
            ModContent.Find<ModItem>(fargocross.Name, "DaedalusEnchant").UpdateAccessory(player, hideVisual);
        }
        public class FrostFlareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
        }

        public class OrnateEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusLeggings>());
            recipe.AddIngredient(ModContent.ItemType<OrnateShield>());
            recipe.AddIngredient(ModContent.ItemType<FrostFlare>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
