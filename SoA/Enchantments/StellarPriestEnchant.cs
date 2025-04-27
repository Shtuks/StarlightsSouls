using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles.Minions;
using ThoriumMod.Empowerments;
using SacredTools.Projectiles.Lunar;
using ssm.Content.Buffs.Minions;
using ssm.Core;
using SacredTools.Content.Items.Armor.Quasar;
using SacredTools.Items.Weapons;
using SacredTools.Content.Items.Armor.Lunar.Stardust;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons.Oblivion;


namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StellarPriestEnchant : BaseEnchant
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
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(108, 116, 204);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.DustiteArmor = true;
            if (player.AddEffect<StellarPriestEffect>(Item))
            {
                player.AddBuff(ModContent.BuffType<StellarGuardianBuff>(), 2);
            }
        }

        public class StellarPriestEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StellarPriestEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<StellarPriestHead>();
            recipe.AddIngredient<StellarPriestChest>();
            recipe.AddIngredient<StellarPriestLegs>();
            recipe.AddIngredient<GalaxyScepter>();
            recipe.AddIngredient<LunarCrystalStaff>();
            recipe.AddIngredient<OblivionRod>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
