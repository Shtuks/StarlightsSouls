using FargowiltasSouls.Core.Toggler.Content;
using ssm.Calamity.Addons;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    public class AddonsForceHeader : SoulHeader
    {
        public override float Priority => 6.1f;
        public override int Item => ModContent.ItemType<AddonsForce>();
    }
}
