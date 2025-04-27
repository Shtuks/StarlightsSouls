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

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class MechanicalEnchant : BaseEnchant
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

        public override Color nameColor => new(156, 156, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MechanicalEffect>(Item))
            {
                player.GetModPlayer<PolaritiesPlayer>().flawlessMechArmorSet = true;
                player.GetModPlayer<PolaritiesPlayer>().flawlessMechMask = true;
                player.GetModPlayer<PolaritiesPlayer>().flawlessMechChestplate = true;
                player.GetModPlayer<PolaritiesPlayer>().flawlessMechTail = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FlawlessMechMask>());
            recipe.AddIngredient(ModContent.ItemType<FlawlessMechChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FlawlessMechTail>());
            //recipe.AddIngredient(ModContent.ItemType<RhyoliteShield>());
            //recipe.AddIngredient(ModContent.ItemType<CorrosivePolish>());
            //recipe.AddIngredient(ModContent.ItemType<GlowYo>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class MechanicalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LimestoneEnchant>();
        }
    }
}
