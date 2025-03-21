using ssm.Calamity.Souls;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ssm.Core;
using ssm.Calamity.Addons;

namespace ssm.Content.SoulToggles
{
    public class SolynsSigilHeader : SoulHeader
    {
        public override float Priority => 6.3f;
        public override int Item => ModContent.ItemType<SolynsSigil>();
    }
}
