using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;

namespace ssm.Thorium.Toggles
{
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class BlastShieldEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<BlastShield>();

        public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
    }
}
