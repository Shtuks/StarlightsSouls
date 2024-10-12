using ssm.Calamity.Souls;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.SoulToggles
{
    public class CalamitySoulHeader : SoulHeader
    {
        public override float Priority => 6.2f;
        public override int Item => ModContent.ItemType<CalamitySoul>();
    }
}
