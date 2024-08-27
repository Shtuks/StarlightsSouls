using CalamityMod.Items.LoreItems;
using ssm.Content.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Shtuxibus
{
  [LegacyName(new string[] {"KnowledgeShtuxibus"})]
  public class ShtuxibusLore : LoreItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      ((Entity) this.Item).width = 34;
      ((Entity) this.Item).height = 26;
      this.Item.rare = 11;
    }
    public override void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<ShtuxibusTrophy>(1).AddTile(101);
    }
  }
}