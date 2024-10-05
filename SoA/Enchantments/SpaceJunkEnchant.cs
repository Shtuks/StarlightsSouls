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
using SacredTools.Content.Items.Armor.Quasar;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.SpaceJunk;
using SacredTools.Content.Items.Weapons.Event;

namespace ssm.SoA.Enchantments
{
    public class SpaceJunkEnchant : BaseEnchant
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(120, 135, 154);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.AddEffect<SpaceJunkEffect>(Item))
            {
                //set bonus
                modPlayer.spaceJunk = true;
            }
        }

        public class SpaceJunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpaceJunkEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<SpaceJunkHelm>();
            recipe.AddIngredient<SpaceJunkBody>();
            recipe.AddIngredient<SpaceJunkLegs>();
            recipe.AddIngredient<OrbFlayer>();
            recipe.AddIngredient<HornetNeedle>();
            recipe.AddIngredient<GoldDoorHandle>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
