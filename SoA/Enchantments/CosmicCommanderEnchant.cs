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
using static ssm.SoA.Enchantments.FrosthunterEnchant;
using SacredTools.Content.Items.Armor.Bismuth;
using SacredTools.Items.Weapons.Herbs;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Weapons.Oblivion;

namespace ssm.SoA.Enchantments
{
    public class CosmicCommanderEnchant : BaseEnchant
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

        public override Color nameColor => new(21, 142, 100);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            //set bonus
            modPlayer.VoxaArmor = true;
        }

        public class CosmicCommanderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CosmicCommanderEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VortexCommanderHat>();
            recipe.AddIngredient<VortexCommanderSuit>();
            recipe.AddIngredient<VortexCommanderGreaves>();
            recipe.AddIngredient<DolphinGun>();
            recipe.AddIngredient<LightningRifle>();
            recipe.AddIngredient<PGMUltimaRatioHecateII>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
