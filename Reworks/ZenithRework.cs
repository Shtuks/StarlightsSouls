using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
  public class ZenithRework : GlobalItem
  {
    public override bool InstancePerEntity => true;

    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
      if (item.type == 4956)
        item.damage = 260;
    }
  }
}