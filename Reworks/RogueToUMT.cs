using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.DamageClasses;
using CalamityMod;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class RogueToUMT : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ThrowerMerge;
        }
        public override void SetDefaults(Item item)
        {
            if (item.DamageType == ModContent.GetInstance<RogueDamageClass>())
            {
                item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
            } 
        }
    }
}