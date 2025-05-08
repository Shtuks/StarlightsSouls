using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Core;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;
using ssm.Content.Buffs;
using BombusApisBee;
using FargowiltasSouls;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class DreadfireEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 70000;
        }

        public override Color nameColor => new(191, 62, 6);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DreadfireEffect>(Item);
        }

        public class DreadfireEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadfireEnchant>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                player.AddBuff(ModContent.BuffType<DreadflameAura>(), 600);

                player.AddBuff(ModContent.BuffType<DreadflameAuraCD>(), player.ForceEffect<DreadfireEffect>() ? 3000 : 2700);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<DreadfireMask>();
            recipe.AddIngredient<DreadfirePlate>();
            recipe.AddIngredient<DreadfireBoots>();
            recipe.AddIngredient<DreadflameEmblem>();
            recipe.AddIngredient<PumpkinFlare>();
            recipe.AddIngredient<PumpkinCarver>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
