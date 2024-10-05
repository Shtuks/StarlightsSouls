using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.SoA.Enchantments.FrosthunterEnchant;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Content.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;

namespace ssm.SoA.Enchantments
{
    public class DreadfireEnchant : BaseEnchant
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 70000;
        }

        public override Color nameColor => new(191, 62, 6);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.AddEffect<DreadfireEffect>(Item))
            {
                //pumpkin amulet
                modPlayer.pumpkinAmulet = true;
            }
        }

        public class DreadfireEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadfireEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<DreadfireMask>();
            recipe.AddIngredient<DreadfirePlate>();
            recipe.AddIngredient<DreadfireBoots>();
            recipe.AddIngredient<DreadflameEmblem>();
            recipe.AddIngredient<PumpkinFlare>();
            recipe.AddIngredient<PumpkinCarver>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
