using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using Microsoft.Xna.Framework.Graphics;
using SacredTools.Content.Items.Armor.Lunar.Stardust;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.Oblivion;
using SacredTools.Items.Weapons.Special;
using SacredTools.Items.Weapons.Oblivion;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VoidWardenEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.SoAEnchantments;
        }
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(79, 21, 137);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            modPlayer.voidDefense = true;
            modPlayer.voidOffense = true;
        }

        public class VoidWardenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidWardenEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VoidHelm>();
            recipe.AddIngredient<VoidChest>();
            recipe.AddIngredient<VoidLegs>();
            recipe.AddIngredient<Skill_FuryForged>();
            recipe.AddIngredient<DarkRemnant>();
            recipe.AddIngredient<EdgeOfGehenna>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
