using ssm.SoA.Forces;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class SoranForceHeader : SoulHeader
    {
        public override float Priority => 6.4f;
        public override int Item => ModContent.ItemType<SoranForce>();
    }
}
