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
using ssm.Core;
using SacredTools.Content.Items.Accessories.Sigils;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Bismuth;
using SacredTools.Items.Weapons.Venomite;
using SacredTools.Items.Weapons.Herbs;
using SacredTools.Items.Weapons;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class BismuthEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(184, 66, 66);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            if (player.AddEffect<BismuthEffect>(Item))
            {
                //set bonus
                modPlayer.bismuthArmor = true;
            }
        }

        public class BismuthEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismuthEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BismuthChest>();
            recipe.AddIngredient<BismuthLegs>();
            recipe.AddIngredient<BismuthHelm>();
            recipe.AddIngredient<VenomiteStaff>();
            recipe.AddIngredient<DeathsGarden>();
            recipe.AddIngredient<GospelOfDismay>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
