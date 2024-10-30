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
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Items.Claymarine;
using SacredTools.Items.Weapons.Marstech;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Accessories;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Content.Items.Weapons.Asthraltite;
using ssm.Core;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class NebulousApprenticeEnchant : BaseEnchant
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

        public override Color nameColor => new(206, 7, 221);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            if (player.AddEffect<NebulousApprenticeEffect>(Item))
            {
                //set bonus
                modPlayer.NubaArmor = true;

                ModContent.Find<ModItem>(this.soa.Name, "NubasBlessing").UpdateAccessory(player, false);
            }
        }

        public class NebulousApprenticeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NebulousApprenticeEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<NubaHood>();
            recipe.AddIngredient<NubaChest>();
            recipe.AddIngredient<NubaRobe>();
            recipe.AddIngredient<NubasBlessing>();
            recipe.AddIngredient<LunaticBurstStaff>();
            recipe.AddIngredient<AsthralStaff>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
