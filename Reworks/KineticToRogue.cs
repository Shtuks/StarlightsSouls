using CalamityMod;
using SacredTools.Content.DamageClasses;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class KineticToRogue : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.CountsAsClass<KineticDamageClass>())
            {
                item.DamageType = (DamageClass)(object)ModContent.GetInstance<RogueDamageClass>();
            }
        }
    }
}