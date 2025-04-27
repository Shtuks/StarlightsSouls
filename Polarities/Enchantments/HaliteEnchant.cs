using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities.Content.Items.Armor.Classless.PreHardmode.HaliteArmor;
using Polarities.Content.Items.Accessories.Movement.PreHardmode;
using Polarities.Content.Items.Weapons.Ranged.Atlatls.PreHardmode;
using Polarities.Content.Items.Weapons.Melee.Knives.PreHardmode;
using Polarities.Content.Buffs.PreHardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class HaliteEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Polarities;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(194, 182, 172);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Desiccating>()] = true;
            player.ignoreWater = true;

            ModContent.GetInstance<SaltatoryLeg>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<HaliteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<HaliteArmor>());
            recipe.AddIngredient(ModContent.ItemType<HaliteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SaltatoryLeg>());
            recipe.AddIngredient(ModContent.ItemType<Axolatl>());
            recipe.AddIngredient(ModContent.ItemType<SaltKnife>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
