using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using ssm.Core;
using Fargowiltas.Items.Tiles;
using ThoriumMod.Items.Placeable;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreamersForgeItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<CrucibleCosmos>());
            Item.createTile = TileType<DreamersForgeTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<SoulForge>());
            recipe.AddIngredient(ItemType<ArcaneArmorFabricator>());
            recipe.AddIngredient(ItemType<ThoriumAnvil>());
            recipe.AddIngredient(ItemType<GuidesFinalGift>());
            recipe.AddIngredient(ItemType<GrimPedestal>());
            recipe.AddIngredient(ItemType<ThoriumAnvil>());
            recipe.AddIngredient(ItemType<DeathEssence>(), 5);
            recipe.AddIngredient(ItemType<OceanEssence>(), 5);
            recipe.AddIngredient(ItemType<InfernoEssence>(), 5);
            recipe.Register();
        }
    }
}
