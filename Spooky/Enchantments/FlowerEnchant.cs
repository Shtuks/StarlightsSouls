using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.Catacomb.Armor;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.Catacomb;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FlowerEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Spooky;
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
            modPlayer.FlowerArmorSet = true;

            if (player.AddEffect<FlowerEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "DaffodilHairpin").UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<FlowerHead>();
            recipe.AddIngredient<FlowerBody>();
            recipe.AddIngredient<FlowerLegs>();
            recipe.AddIngredient<DaffodilHairpin>();
            recipe.AddIngredient<DaffodilBow>();
            recipe.AddIngredient<DaffodilStaff>();

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class FlowerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
