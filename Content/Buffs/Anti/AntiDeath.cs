using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs.Anti
{
  public class AntiDeath : ModBuff
  {
    public override void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }
  }
}