using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.TitanHeart;
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
    public class TitanHeartEnchantEx : BaseEnchant
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
        public override Color nameColor => new Color(102, 96, 117);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<TitanEffect>(Item))
            {
                modPlayer.titanHeartSet = true;
            }
            if (player.AddEffect<MoonCrownEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "MoonstoneCrown").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<JewelEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "InfectedJewel").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(fargocross.Name, "VictideEnchant").UpdateArmorSet(player);
        }
        public class TitanEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
        }

        public class MoonCrownEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
        }
        public class JewelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMask>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMantle>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartBoots>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MoonstoneCrown>());
            recipe.AddIngredient(ModContent.ItemType<InfectedJewel>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

    
