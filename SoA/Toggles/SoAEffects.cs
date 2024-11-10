using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using SacredTools.Content.Items.Accessories;

namespace ssm.SoA.Toggles
{
    //Universe
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class FloraFistEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<FloraFist>();

        public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
    }

    //Colossus
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class RoyalGuardEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<RoyalGuard>();
        public override bool MutantsPresenceAffects => false;

        public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
    }

    //Supersonic
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class MilinticaDashEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<MilinticaDash>();
        public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class HeartOfThePloughEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<HeartOfThePlough>();
        public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
    }

    //Maso
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class YataMirrorEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<YataMirror>();
        public override Header ToggleHeader => Header.GetHeader<SoAMasoSoulHeader>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class PrimordialCoreEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<PrimordialCore>();

        public override Header ToggleHeader => Header.GetHeader<SoAMasoSoulHeader>();
    }
}
