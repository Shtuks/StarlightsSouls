using CalamityMod.CalPlayer;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Microsoft.Xna.Framework;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Prismatic;
using ssm.Content.SoulToggles;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class PrismaticEnchant : BaseEnchant
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
        public override Color nameColor =>new(0, 104, 94);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<PrismaticArmorEffect>(Item))
            {
                modPlayer.prismaticSet = true;
            }
            if (player.AddEffect<HighRuleEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ShieldoftheHighRuler").UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PrismaticHelmet>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticRegalia>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ShieldoftheHighRuler>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class PrismaticArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrismaticEnchant>();
        }
        public class HighRuleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrismaticEnchant>();
        }
    }
}
