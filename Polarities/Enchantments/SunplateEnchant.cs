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
using Polarities.Content.Items.Armor.Classless.PreHardmode.SunplateArmor;
using Polarities.Content.Items.Accessories.Combat.Offense.PreHardmode;
using Polarities.Content.Items.Tools.Multi;
using Polarities.Content.Items.Weapons.Summon.Minions.PreHardmode;
using Polarities.Content.Items.Accessories.Combat.Defense.Hardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SunplateEnchant : BaseEnchant
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

        public override Color nameColor => new(0, 72, 128);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SunplateEffect>(Item))
            {
                player.GetModPlayer<PolaritiesPlayer>().dashIndex = ModContent.GetInstance<SunplateDash>().Index;
            }

            ModContent.GetInstance<CosmicCable>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StarbindCuffs>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SunplateMask>());
            recipe.AddIngredient(ModContent.ItemType<SunplateArmor>());
            recipe.AddIngredient(ModContent.ItemType<SunplateBoots>());
            recipe.AddIngredient(ModContent.ItemType<StarbindCuffs>());
            recipe.AddIngredient(ModContent.ItemType<CosmicCable>());
            recipe.AddIngredient(ModContent.ItemType<WingedStarStaff>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class SunplateEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StormcloudEnchant>();
        }
    }
}
