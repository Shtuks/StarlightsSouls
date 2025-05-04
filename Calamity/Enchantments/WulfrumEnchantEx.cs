using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Wulfrum;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;


namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class WulfrumEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

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
        public override Color nameColor => new Color(206, 201, 170);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WulfrumArmorEffect>(Item))
            {
                player.GetModPlayer<WulfrumArmorPlayer>().wulfrumSet = true;
            }
            if (player.AddEffect<ChiEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TrinketofChi").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AcrobaticsPackEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "WulfrumAcrobaticsPack").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(this.fargocross.Name, "WulfrumEnchant").UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WulfrumHat>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumJacket>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumOveralls>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumTreasurePinger>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumAcrobaticsPack>());
            recipe.AddIngredient(ModContent.ItemType<TrinketofChi>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class WulfrumArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();
        }
        public class ChiEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();
        }
        public class AcrobaticsPackEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();
        }
    }
}