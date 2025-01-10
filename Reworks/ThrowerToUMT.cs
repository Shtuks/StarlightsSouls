using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Content.DamageClasses;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThrowingToRogue : GlobalItem
    {
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