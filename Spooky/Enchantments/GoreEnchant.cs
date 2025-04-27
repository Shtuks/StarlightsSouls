using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyHell.EggEvent;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.SpookyHell;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Spooky.Enchantments.LivingFleshEnchant;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GoreEnchant : BaseEnchant
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
            if (player.AddEffect<OrroBoroEffect>(Item))
            {
                SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
                modPlayer.GoreArmorEye = true;
                modPlayer.GoreArmorMouth = true;
            }

            if (player.AddEffect<OrgansEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "TotalOrganPackage").UpdateAccessory(player, hideVisual);
            }
        }
         
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("ssm:AnyGoreHelmet");
            recipe.AddIngredient<GoreBody>();
            recipe.AddIngredient<GoreLegs>();
            recipe.AddIngredient<TotalOrganPackage>();
            recipe.AddIngredient<OrroboroEmbryo>();
            recipe.AddIngredient<EyeFlail>();

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class OrgansEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GoreEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }

        public class OrroBoroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GoreEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
