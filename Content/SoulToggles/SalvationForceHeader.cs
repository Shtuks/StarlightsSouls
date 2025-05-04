using FargowiltasSouls.Core.Toggler.Content;
using ssm.Calamity.Forces;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class SalvationForceHeader : SoulHeader
    {
        public override float Priority => 6.9f;
        public override int Item => ModContent.ItemType<SalvationForce>();
    }
}
