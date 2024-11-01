using CalamityMod;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class ThrowingToRogue : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!ModLoader.HasMod("Ragnarok"))
            {
                if (item.CountsAsClass<ThrowingDamageClass>())
                {
                    item.DamageType = (DamageClass)(object)ModContent.GetInstance<RogueDamageClass>();
                } 
            }
        }
    }
}