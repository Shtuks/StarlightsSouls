using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.SnowRuffian;
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
	public class SnowRuffianEnchantEx : BaseEnchant
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

		public override Color nameColor => new Color(160, 185, 213);

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<SnowRuffianEffect>(Item))
            {
                modPlayer.snowRuffianSet = true;
            }
            if (player.AddEffect<ScuttlerEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "ScuttlersJewel").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CamperEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "TheCamper").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(fargocross.Name, "SnowRuffianEnchant").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianMask>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianChestplate>());
            recipe.AddIngredient(ModContent.ItemType<ScuttlersJewel>());
            recipe.AddIngredient(ModContent.ItemType<AmidiasPendant>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SnowRuffianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
        }
        public class ScuttlerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
        }
        public class CamperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
        }
    }
}