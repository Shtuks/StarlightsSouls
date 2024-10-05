using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
    [JITWhenModsEnabled(new string[] { "CalamityMod" })]
    [ExtendsFromMod(new string[] { "CalamityMod" })]
    public abstract class CalamitySoulEffect : AccessoryEffect
    {
        public override Header ToggleHeader
        {
            get => (Header)Header.GetHeader<CalamitySoulHeader>();
        }
    }
}
