using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyHell;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LivingFleshEnchant : BaseEnchant
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
            if (player.AddEffect<EyeArmorEffect>(Item))
            {
                SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
                modPlayer.EyeArmorSet = true;
            }

            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "MonsterBloodVial").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<EyeHead>();
            recipe.AddIngredient<EyeBody>();
            recipe.AddIngredient<EyeLegs>();
            recipe.AddIngredient<MonsterBloodVial>();
            recipe.AddIngredient<LivingFleshAxe>();
            recipe.AddIngredient<LivingFleshWhip>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class EyeArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingFleshEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
