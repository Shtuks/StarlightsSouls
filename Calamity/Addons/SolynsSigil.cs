using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using NoxusBoss.Content.Items.Accessories.VanityEffects;
using NoxusBoss.Content.Items.Accessories.Wings;
using ssm.Content.Tiles;
using ssm.CrossMod.CraftingStations;

namespace ssm.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name)]
    public class SolynsSigil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 500000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<DeificEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.WrathoftheGods.Name, "DeificTouch").UpdateAccessory(player, false);
            }

            if (player.AddEffect<SkirtEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.WrathoftheGods.Name, "PortalSkirt").UpdateAccessory(player, false);
            }
            if (player.AddEffect<DivineEffect>(Item))
            {
                ModContent.Find<ModItem>(ModCompatibility.WrathoftheGods.Name, "DivineWings").UpdateAccessory(player, false);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PortalSkirt>());
            recipe.AddIngredient(ModContent.ItemType<DeificTouch>());
            recipe.AddIngredient(ModContent.ItemType<DivineWings>());

            recipe.AddTile<DemonshadeWorkbenchTile>();
            recipe.Register();
        }

        public class SkirtEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<PortalSkirt>();
        }

        public class DeificEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<DeificTouch>();
        }
        public class DivineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineWings>();
        }
    }
}
