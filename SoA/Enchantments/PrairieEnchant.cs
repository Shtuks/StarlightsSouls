using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Armor.Prairie;
using SacredTools.Items.Weapons;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PrairieEnchant : BaseEnchant
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 50000;
        }

        public override Color nameColor => new(129, 19, 29);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Throwing) += 0.15f;
        }

        public class PrairieEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrairieEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PrairieHelmet>();
            recipe.AddIngredient<PrairieChest>();
            recipe.AddIngredient<PrairieLegs>();
            recipe.AddIngredient<WoodJavelin>(100);
            recipe.AddIngredient<GoldJavelin>(100);
            recipe.AddIngredient<PlatinumJavelin>(100);
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
