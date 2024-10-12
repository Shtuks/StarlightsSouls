using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ZenithRework : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void ModifyWeaponDamage(Item Item, Player player, ref StatModifier damage)
        {
            if (Item.type == 4956)
                Item.damage = 260;
        }
    }
}