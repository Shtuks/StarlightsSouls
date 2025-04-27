using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.FractalArmor;
using Polarities.Content.Items.Accessories.ExpertMode.PreHardmode;
using Polarities.Content.Items.Accessories.Information.PreHardmode;
using Polarities.Content.Items.Weapons.Melee.Warhammers.PreHardmode.Other;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class FractalEnchant : BaseEnchant
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
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 119, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<FractalEffect>(Item))
            {
                PolaritiesPlayer modPlayer = player.GetModPlayer<PolaritiesPlayer>();

                //player.GetModPlayer<PolaritiesPlayer>().fractalSetBonusTier = 1;

                modPlayer.fractalMeleeShield = true;
                modPlayer.fractalMageSwords = true;
                modPlayer.fractalRangerTargets = true;
                modPlayer.fractalSummonerOrbs = true;
            }

            if (player.AddEffect<AnchorEffect>(Item))
            {
                ModContent.GetInstance<DimensionalAnchor>().UpdateAccessory(player, hideVisual);
            }

            ModContent.GetInstance<CloakofPockets>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:FractalHelms");
            recipe.AddIngredient(ModContent.ItemType<FractalBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<FractalGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CloakofPockets>());
            recipe.AddIngredient(ModContent.ItemType<DimensionalAnchor>());
            recipe.AddIngredient(ModContent.ItemType<RiftOnAStick>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class FractalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FractalEnchant>();
        }
        public class AnchorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FractalEnchant>();
        }
    }
}
