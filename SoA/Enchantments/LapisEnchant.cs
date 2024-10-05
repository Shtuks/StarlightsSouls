using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework.Graphics;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Decree;
using SacredTools.Items.Weapons.Decree;
using SacredTools.Content.Items.Armor.Lapis;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Special;

namespace ssm.SoA.Enchantments
{
    public class LapisEnchant : BaseEnchant
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

        public override Color nameColor => new(46, 66, 163);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //set bonus
            player.moveSpeed += 0.2f;

            ModContent.Find<ModItem>(this.soa.Name, "LapisPendant").UpdateAccessory(player, false);
        }

        public class LapisEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<LapisHelmet>();
            recipe.AddIngredient<LapisChest>();
            recipe.AddIngredient<LapisLegs>();
            recipe.AddIngredient<LapisPendant>();
            recipe.AddIngredient<LapisStaff>();
            recipe.AddIngredient<Haven>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
