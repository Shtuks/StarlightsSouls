using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Polarities;
using Polarities.Content.Items.Armor.Classless.Hardmode.LimestoneArmor;
using Polarities.Content.Items.Accessories.Combat.Defense.Hardmode;
using Polarities.Content.Items.Accessories.Combat.Offense.PreHardmode;
using Polarities.Content.Items.Weapons.Melee.Yoyos.PreHardmode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;

namespace ssm.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class LimestoneEnchant : BaseEnchant
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

        public override Color nameColor => new(14, 43, 21);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<LimestoneEffect>(Item))
            {
                player.statDefense += 40;
                player.GetModPlayer<PolaritiesPlayer>().limestoneSetBonus = true;
            }

            ModContent.GetInstance<RhyoliteShield>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CorrosivePolish>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LimestoneHelmet>());
            recipe.AddIngredient(ModContent.ItemType<LimestoneChestplate>());
            recipe.AddIngredient(ModContent.ItemType<LimestoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<RhyoliteShield>());
            recipe.AddIngredient(ModContent.ItemType<CorrosivePolish>());
            recipe.AddIngredient(ModContent.ItemType<GlowYo>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class LimestoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LimestoneEnchant>();
        }
    }
}
