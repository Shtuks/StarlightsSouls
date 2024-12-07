using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using ThoriumMod;
using ssm.Core;
using Terraria;
using CalamityMod.Items.Accessories;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ThoriumMod.Items.ThrownItems;
using CalamityMod.Items.Materials;

namespace ssm.CrossMod.Accessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class GtTETFinal : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<Nanotech>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ThrowingGuideVolume3>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<Nanotech>();
            recipe.AddIngredient<ThrowingGuideVolume3>();
            recipe.AddIngredient<SuspiciousScrap>();
            //recipe.AddIngredient<SuspiciousScrap>();

            recipe.AddTile<DraedonsForge>();

            recipe.Register();
        }
    }
}