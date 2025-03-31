using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Polarities.Content.Items.Armor.Summon.PreHardmode.StormcloudArmor;
using Polarities.Content.Items.Accessories.ExpertMode.PreHardmode;
using Polarities.Content.Items.Weapons.Magic.Flawless;
using Polarities.Content.Items.Weapons.Magic.Guns.PreHardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class StormcloudEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(42, 56, 66);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions++;
            if (player.AddEffect<StormcloudEffect>(Item))
            {
                player.GetModPlayer<PolaritiesPlayer>().stormcloudArmor = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<StormcloudMask>());
            recipe.AddIngredient(ModContent.ItemType<StormcloudArmor>());
            recipe.AddIngredient(ModContent.ItemType<StormcloudGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StormScales>());
            recipe.AddIngredient(ModContent.ItemType<EyeOfTheStormfish>());
            recipe.AddIngredient(ModContent.ItemType<Flopper>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class StormcloudEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StormcloudEnchant>();
        }
    }
}
