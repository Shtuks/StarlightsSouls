using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Thorium.Toggles
{
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumMasoSoulHeader : SoulHeader
    {
        public override float Priority => 0.99f;
        public override int Item => ModContent.ItemType<Masochist>();
        
    }
    //[ExtendsFromMod(ModCompatibility.Thorium.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    //public class ThrowerSoulHeader : SoulHeader
    //{
    //    public override float Priority => 5.11f;
    //    public override int Item => ModContent.ItemType<ThrowerSoul>();
    //}
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumColossusHeader : SoulHeader
    {
        public override float Priority => 5.12f;
        public override int Item => ModContent.ItemType<ColossusSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumBerserkerHeader : SoulHeader
    {
        public override float Priority => 5.13f;
        public override int Item => ModContent.ItemType<BerserkerSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumWizardHeader : SoulHeader
    {
        public override float Priority => 5.14f;
        public override int Item => ModContent.ItemType<ArchWizardsSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumSniperHeader : SoulHeader
    {
        public override float Priority => 5.15f;
        public override int Item => ModContent.ItemType<SnipersSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumConjuristHeader : SoulHeader
    {
        public override float Priority => 5.16f;
        public override int Item => ModContent.ItemType<ConjuristsSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class ThoriumTrawlerHeader : SoulHeader
    {
        public override float Priority => 5.17f;
        public override int Item => ModContent.ItemType<TrawlerSoul>();
    }
}
