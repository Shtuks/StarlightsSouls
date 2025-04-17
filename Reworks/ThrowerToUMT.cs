using Terraria;
using Terraria.ModLoader;
using ssm.Content.DamageClasses;

namespace ssm.Reworks
{
    public class ThrowingToRogue : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.ThrowerMerge;
        }
        public override void SetDefaults(Item item)
        {
            if (!ModLoader.HasMod("Ragnarok"))
            {
                if (item.DamageType == DamageClass.Throwing)
                {
                    item.DamageType = (DamageClass)(object)ModContent.GetInstance<UnitedModdedThrower>();
                } 
            }
        }
    }
}