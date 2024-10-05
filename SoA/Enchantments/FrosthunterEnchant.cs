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
using SacredTools.Content.Items.Armor.Dragon;
using SacredTools.Content.Items.Pets;
using SacredTools.Items.Mount;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons.Special;
using SacredTools.Content.Items.Armor.Decree;
using SacredTools.Content.Items.Accessories;
using CalamityMod.Items.VanillaArmorChanges;
using SacredTools.Items.Weapons.Decree;
using SacredTools.Items.Weapons;

namespace ssm.SoA.Enchantments
{
    public class FrosthunterEnchant : BaseEnchant
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 50000;
        }

        public override Color nameColor => new(73, 94, 174);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //set bonus
            modPlayer.frostburnRanged = true;
            if (player.ZoneSnow)
            {
                player.GetDamage(DamageClass.Ranged) += 0.15f;
            }

            //frigid pendant
            //ModContent.Find<ModItem>(this.soa.Name, "DecreePendant").UpdateAccessory(player, false);
        }

        public class FrosthunterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrosthunterEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FrosthunterHeaddress>();
            recipe.AddIngredient<FrosthunterWrappings>();
            recipe.AddIngredient<FrosthunterBoots>();
            recipe.AddIngredient<DecreeCharm>();
            recipe.AddIngredient<FrostGlobeStaff>();
            recipe.AddIngredient<FrostBeam>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
