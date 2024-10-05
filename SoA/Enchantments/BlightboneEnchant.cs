﻿using Terraria;
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
using SacredTools.Content.Items.Armor.Bismuth;
using SacredTools.Items.Weapons.Herbs;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.Blightbone;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;

namespace ssm.SoA.Enchantments
{
    public class BlightboneEnchant : BaseEnchant
    {
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

        public override Color nameColor => new(124, 10, 10);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //set bonus
            modPlayer.blightEmpowerment = true;

            //dreadflame emblem
            modPlayer.dreadEmblem = true;
        }

        public class BlightboneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BlightMask>();
            recipe.AddIngredient<BlightChest>();
            recipe.AddIngredient<BlightLegs>();
            recipe.AddIngredient<DreadflameEmblem>();
            recipe.AddIngredient<FeatherHairpin>();
            recipe.AddIngredient<PumpGlove>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}