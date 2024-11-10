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
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Lapis;
using SacredTools.Items.Weapons.Special;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Marstech;
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Items.Claymarine;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class MarstechEnchant : BaseEnchant
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 8;
            Item.value = 250000;
        }

        public override Color nameColor => new(61, 155, 189);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.AddEffect<MarstechEffect>(Item))
            {
                //set bonus
                modPlayer.marsArmor = true;
                //space junk
                modPlayer.spaceJunk = true;
            }
        }

        public class MarstechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarstechEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PhaseSlasher>();
            recipe.AddIngredient<PlasmaDischarge>();
            recipe.AddIngredient<MarstechHelm>();
            recipe.AddIngredient<MarstechPlate>();
            recipe.AddIngredient<MarstechLegs>();
            recipe.AddIngredient<SpaceJunkEnchant>();
            recipe.AddTile(125);
            recipe.Register();
        }
    }
}
