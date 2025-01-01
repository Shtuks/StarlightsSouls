using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class VanillaCalReworks : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            //Zenith
            if (item.type == 4956)
                item.damage = 260;

            //Reaver shark
            if (item.type == 2341)
                item.pick = 65;
        }
    }
}