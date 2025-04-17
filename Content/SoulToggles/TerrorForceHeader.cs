using FargowiltasSouls.Core.Toggler.Content;
using ssm.Spooky.Forces;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class TerrorForceHeader : SoulHeader
    {
        public override float Priority => 7.1f;
        public override int Item => ModContent.ItemType<TerrorForce>();
    }
}
