using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Reaver;
using ssm.Content.SoulToggles;
using ssm.Core;
using CalamityMod.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ReaverEnchantEx : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        private readonly Mod fargocross = ModLoader.GetMod("FargowiltasCrossmod");

        public override Color nameColor => new(145, 203, 102);

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<VexationEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "NecklaceofVexation").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DewEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "LivingDew").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "ReaverHeadExplore").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "ReaverHeadTank").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "ReaverHeadMobility").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "ReaverScaleMail").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "ReaverCuisses").UpdateArmorSet(player);
            ModContent.Find<ModItem>(fargocross.Name, "ReaverEnchant").UpdateAccessory(player, hideVisual);
        }
        public class VexationEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();
        }
        public class DewEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadExplore>());
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadTank>());
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadMobility>());
            recipe.AddIngredient(ModContent.ItemType<ReaverScaleMail>());
            recipe.AddIngredient(ModContent.ItemType<ReaverCuisses>());
            recipe.AddIngredient(ModContent.ItemType<NecklaceofVexation>());
            recipe.AddIngredient(ModContent.ItemType<LivingDew>());
            recipe.AddIngredient(ModContent.ItemType<ReaverEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

