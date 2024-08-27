using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
  [AutoloadEquip(EquipType.Legs)]
  public class ShtuxibusPants : ModItem
  {
    public override void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.expert = true;
      this.Item.value = Item.sellPrice(10, 0, 0, 0);
    }

    public override void AddRecipes()
    {
    }
  }
}
