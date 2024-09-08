using ssm.Content.Items.Accessories;
using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace ssm.Content.SoulToggles
{
  public class ChtuxlagorHeartHeader : SoulHeader
  {
    public override float Priority => 745745f;
    public override int Item => ModContent.ItemType<ChtuxlagorHeart>();
  }
}
