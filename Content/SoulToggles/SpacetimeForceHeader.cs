using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ssm.Polarities.Forces;

namespace ssm.Content.SoulToggles
{
    public class SpacetimeForceHeader : SoulHeader
    {
        public override float Priority => 6.4f;
        public override int Item => ModContent.ItemType<SpacetimeForce>();
    }
}
