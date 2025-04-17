using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpiderCave.Armor;
using Spooky.Content.Items.SpiderCave.OldHunter;
using Spooky.Content.Items.Quest;
using Spooky.Content.Items.SpiderCave;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Spooky.Enchantments.SpiritHorsemenEnchant;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpiOpsEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            SpookyPlayer modPlayer = player.GetModPlayer<SpookyPlayer>();
            modPlayer.SpiderSet = true;

            ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "HunterScarf").UpdateAccessory(player, hideVisual);

            if (player.AddEffect<SewingTreadEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "SewingThread").UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<SpiderHead>();
            recipe.AddIngredient<SpiderBody>();
            recipe.AddIngredient<SpiderLegs>();
            recipe.AddIngredient<HunterScarf>();
            recipe.AddIngredient<SewingThread>();
            recipe.AddIngredient<SpiderEggCannon>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class SewingTreadEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiOpsEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
