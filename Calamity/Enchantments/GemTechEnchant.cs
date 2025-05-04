using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GemTech;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class GemTechEnchant : BaseEnchant
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

        public override Color nameColor => new(244, 25, 255);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<GemArmorEffect>(Item))
            {
                modPlayer.GemTechSet = true;
            }
            if (player.AddEffect<ShadowFlameEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TheFirstShadowflame").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DraedonEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DraedonsHeart").UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GemTechHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<GemTechBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<GemTechSchynbaulds>());
            recipe.AddIngredient(ModContent.ItemType<TheFirstShadowflame>());
            recipe.AddIngredient(ModContent.ItemType<DraedonsHeart>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class GemArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
        }
        public class ShadowFlameEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
        }
        public class DraedonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
        }
    }
}
