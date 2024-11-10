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
using SacredTools.Content.Items.Armor.Oblivion;
using SacredTools.Items.Weapons.Special;
using SacredTools.Content.Items.Armor.Vulcan;
using SacredTools.Items.Potions;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Placeable.Paintings;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VulcanReaperEnchant : BaseEnchant
    {
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

        public override Color nameColor => new(138, 36, 58);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            player.buffImmune[ModContent.Find<ModBuff>(this.soa.Name, "SerpentWrath").Type] = true;
            player.buffImmune[ModContent.Find<ModBuff>(this.soa.Name, "ObsidianCurse").Type] = true;
        }

        public class VulcanReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VulcanReaperEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VulcanHelm>();
            recipe.AddIngredient<VulcanChest>();
            recipe.AddIngredient<VulcanLegs>();
            recipe.AddIngredient<SmolderingSpicyCurry>();
            recipe.AddIngredient<SerpentChain>();
            recipe.AddIngredient<Warmth>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
