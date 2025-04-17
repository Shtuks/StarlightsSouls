using SacredTools.Content.DamageClasses;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.DamageClasses;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class KineticToUMT : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ThrowerMerge;
        }
        public override void SetDefaults(Item item)
        {
            if (item.DamageType == ModContent.GetInstance<KineticDamageClass>())
            {
                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
            }
        }
    }
}