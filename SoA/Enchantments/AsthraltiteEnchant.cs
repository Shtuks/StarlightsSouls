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
using ssm.Content.Buffs.Minions;
using ssm.Core;
using Microsoft.Xna.Framework.Graphics;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories.Sigils;

namespace ssm.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class AsthraltiteEnchant : BaseEnchant
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

        public override Color nameColor => new(94, 48, 117);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();
            modPlayer.AstralSet = true;

            //ModContent.Find<ModItem>(this.soa.Name, "AsthraliteHelmetRevenant").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.soa.Name, "AsthralSummon").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.soa.Name, "AsthralRanged").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.soa.Name, "AsthralMage").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.soa.Name, "AsthralMelee").UpdateArmorSet(player);

            ModContent.Find<ModItem>(this.soa.Name, "AsthralRing").UpdateAccessory(player, false);

            ModContent.Find<ModItem>(this.soa.Name, "MementoMori").UpdateAccessory(player, false);

            ModContent.Find<ModItem>(this.soa.Name, "CasterArcanum").UpdateAccessory(player, false);
        }

        public class AsthraliteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<AsthralChest>();
            recipe.AddIngredient<AsthralLegs>();
            recipe.AddIngredient<AsthralRing>();
            recipe.AddIngredient<CasterArcanum>();
            recipe.AddIngredient<MementoMori>();
            recipe.AddRecipeGroup("ssm:AsthralHelms");
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
