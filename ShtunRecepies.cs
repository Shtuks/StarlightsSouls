using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.ModLoader;
using ssm.Content.Items.Accessories;

namespace ssm
{
  public class Recipes : ModSystem
  {
    /*public virtual void PostAddRecipes()
    {
      for (int index = 0; index < Recipe.numRecipes; ++index)
      {
        Recipe recipe = Main.recipe[index];
        Item obj;
        if (recipe.TryGetResult(ModContent.ItemType<ElementalShiv>(), ref obj))
          recipe.AddIngredient(ModContent.ItemType<TerraShiv>(), 1);
      }
    }*/

    public override void AddRecipes()
    {
      Recipe.Create(ModContent.ItemType<SCalMask>(), 1).AddIngredient<AshesofAnnihilation>(10).AddIngredient<CoreofHavoc>(8).AddIngredient<GalacticaSingularity>(5).AddIngredient<BrimflameScowl>(1).AddTile<CosmicAnvil>().Register();
      Recipe.Create(ModContent.ItemType<SCalRobes>(), 1).AddIngredient<AshesofAnnihilation>(15).AddIngredient<CoreofHavoc>(10).AddIngredient<GalacticaSingularity>(7).AddIngredient<BrimflameRobes>(1).AddTile<CosmicAnvil>().Register();
      Recipe.Create(ModContent.ItemType<SCalBoots>(), 1).AddIngredient<AshesofAnnihilation>(12).AddIngredient<CoreofHavoc>(7).AddIngredient<GalacticaSingularity>(6).AddIngredient<BrimflameBoots>(1).AddTile<CosmicAnvil>().Register();
    }
    
    public override void PostAddRecipes()
    {
      for (int i = 0; i < Recipe.numRecipes; i++)
            {
              Recipe recipe = Main.recipe[i];
              if (recipe.HasResult<TerrariaSoul>())
                {
                  recipe.AddIngredient<CelestialEnchant>();
                }
            }
    }
  }
}