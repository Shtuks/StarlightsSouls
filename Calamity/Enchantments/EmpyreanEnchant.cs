using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Empyrean;
using CalamityMod.Items.Armor.Reaver;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Content.SoulToggles;
using ssm.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class EmpyreanEnchant : BaseEnchant
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(20, 20, 100);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            if (player.AddEffect<DarkHeartEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "HeartofDarkness").UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DarkSheathEffect>(Item))
            {
                ModContent.Find<ModItem>(this.calamity.Name, "DarkMatterSheath").UpdateAccessory(player, hideVisual);
            }
            ModContent.Find<ModItem>(calamity.Name, "EmpyreanMask").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "EmpyreanCloak").UpdateArmorSet(player);
            ModContent.Find<ModItem>(calamity.Name, "EmpyreanCuisses").UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCuisses>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCloak>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanMask>());
            recipe.AddIngredient(ModContent.ItemType<HeartofDarkness>());
            recipe.AddIngredient(ModContent.ItemType<DarkMatterSheath>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class DarkHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EmpyreanEnchant>();
        }
        public class DarkSheathEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EmpyreanEnchant>();
        }
    }
}
