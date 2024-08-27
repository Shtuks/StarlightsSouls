using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Items.Armor
{
  [AutoloadEquip(EquipType.Legs)]
  public class TrueMonstrosityPants : ModItem
  {
    private readonly Mod calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

    public override void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.expert = true;
      this.Item.value = Item.sellPrice(10, 0, 0, 0);
      this.Item.defense = 60;
    }

    public override void UpdateEquip(Player player)
    {
      player.GetDamage(DamageClass.Generic) += 0.4f;
      player.GetCritChance(DamageClass.Generic) += 2f;
    }
    
    public override void AddRecipes()
    {
    }
  }
}
