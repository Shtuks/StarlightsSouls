using ssm.Calamity.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader; 

namespace ssm.Content.SoulToggles
{
    public class DevastationExHeader : SoulHeader
    {
        public override float Priority => 6.9f;
        public override int Item => ModContent.ItemType<DevastationForceEx>();
    }
}
