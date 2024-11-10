using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.SoA.Toggles
{
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoAMasoSoulHeader : SoulHeader
    {
        public override float Priority => 0.99f;
        public override int Item => ModContent.ItemType<Masochist>();
        
    }
    //[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    //[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    //public class KineticSoulHeader : SoulHeader
    //{
    //    public override float Priority => 5.11f;
    //    public override int Item => ModContent.ItemType<KineticSoul>();
    //}
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoAColossusHeader : SoulHeader
    {
        public override float Priority => 5.12f;
        public override int Item => ModContent.ItemType<ColossusSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoABerserkerHeader : SoulHeader
    {
        public override float Priority => 5.13f;
        public override int Item => ModContent.ItemType<BerserkerSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoAWizardHeader : SoulHeader
    {
        public override float Priority => 5.14f;
        public override int Item => ModContent.ItemType<ArchWizardsSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoASniperHeader : SoulHeader
    {
        public override float Priority => 5.15f;
        public override int Item => ModContent.ItemType<SnipersSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoAConjuristHeader : SoulHeader
    {
        public override float Priority => 5.16f;
        public override int Item => ModContent.ItemType<ConjuristsSoul>();
    }
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoATrawlerHeader : SoulHeader
    {
        public override float Priority => 5.17f;
        public override int Item => ModContent.ItemType<TrawlerSoul>();
    }
}
