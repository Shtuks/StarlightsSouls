using ssm.Content.Items.Accessories;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
    [JITWhenModsEnabled(new string[] { "CalamityMod" })]
    [ExtendsFromMod(new string[] { "CalamityMod" })]
    public class CalamitySoulHeader : SoulHeader
    {
        public override float Priority => 6.2f;
        public override int Item => ModContent.ItemType<CalamitySoul>();
    }
}
