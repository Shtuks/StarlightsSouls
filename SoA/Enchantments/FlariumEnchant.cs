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
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Weapons.Dreadfire;
using SacredTools.Content.Items.Armor.Dragon;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons.Special;
using SacredTools.Items.Mount;
using SacredTools.Content.Items.Pets;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumEnchant : BaseEnchant
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

        public override Color nameColor => new(204, 78, 40);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //set bonus
            modPlayer.DragonSetEffect = true;
        }

        public class FlariumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlariumEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddRecipeGroup("ssm:FlariumHelms");
            recipe.AddIngredient<FlariumChest>();
            recipe.AddIngredient<FlariumLeggings>();
            recipe.AddIngredient<FlariumRocketLauncher>();
            recipe.AddIngredient<SolusKatana>();
            recipe.AddIngredient<SerpentSceptre>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
