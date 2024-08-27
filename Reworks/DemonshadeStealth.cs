using CalamityMod;
using CalamityMod.Buffs.Summon;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Reworks
{
  public class DemonshadeStealth : GlobalBuff
  {
    public virtual void Update(int type, Player player, ref int buffIndex)
    {
      if (!player.HasBuff(ModContent.BuffType<DemonshadeSetDevilBuff>()))
        return;
      player.Calamity().rogueStealthMax += 200f;
      player.Calamity().wearingRogueArmor = true;
      player.Calamity().stealthStrikeHalfCost = true;
    }
  }
}