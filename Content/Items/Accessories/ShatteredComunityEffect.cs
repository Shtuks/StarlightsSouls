using CalamityMod.Items.Accessories;
using Terraria.ModLoader;

namespace ssm.Content.Items.Accessories
{
  [JITWhenModsEnabled(new string[] {"CalamityMod"})]
  [ExtendsFromMod(new string[] {"CalamityMod"})]
  public class ShatteredCommunityEffect : CalamitySoulEffect
  {
    public override int ToggleItemType => ModContent.ItemType<ShatteredCommunity>();

    public override bool IgnoresMutantPresence => true;
  }
}