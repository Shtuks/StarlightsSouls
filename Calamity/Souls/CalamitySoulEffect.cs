using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria.ModLoader;

namespace ssm.Calamity.Souls
{
    [JITWhenModsEnabled(new string[] { "CalamityMod" })]
    [ExtendsFromMod(new string[] { "CalamityMod" })]
    public abstract class CalamitySoulEffect : AccessoryEffect
    {
        public override Header ToggleHeader
        {
            get => Header.GetHeader<CalamitySoulHeader>();
        }
    }
}
