using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Polarities.Content.Items.Armor.Flawless.MechaMayhemArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Polarities.Content.Items.Accessories.ExpertMode.Hardmode;
using Polarities.Content.Items.Weapons.Ranged.Bows.Hardmode;
using Polarities.Content.Items.Weapons.Melee.Broadswords.Hardmode;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SelfsimilarEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(154, 94, 181);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PolaritiesPlayer>().fractalSetBonusTier = 1;

            if (player.AddEffect<SelfsimilarEffect>(Item))
            {
                player.GetModPlayer<PolaritiesPlayer>().fractalMeleeShield = true;
                player.GetModPlayer<PolaritiesPlayer>().fractalMageSwords = true;
                player.GetModPlayer<PolaritiesPlayer>().fractalRangerTargets = true;
                player.GetModPlayer<PolaritiesPlayer>().fractalSummonerOrbs = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:SelfsimilarHelms");
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SentinelsHeart>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarBow>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarSlasher>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class SelfsimilarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SelfsimilarEnchant>();
        }
    }
}
