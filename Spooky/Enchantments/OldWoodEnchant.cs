using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Spooky.Core;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpookyBiome;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OldWoodEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
            Item.defense = 1;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<CreepyCandleEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "CreepyCandle").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CreepyCandleEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.Spooky.Name, "CandyBag").UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<OldWoodHead>();
            recipe.AddIngredient<OldWoodBody>();
            recipe.AddIngredient<OldWoodLegs>();
            recipe.AddIngredient<OldWoodStaff>();
            recipe.AddIngredient<CandyBag>();
            recipe.AddIngredient<CreepyCandle>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class CreepyCandleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }

        public class CandyBagEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
