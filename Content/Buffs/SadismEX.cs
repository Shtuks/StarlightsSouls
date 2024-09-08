using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
  public class SadismEX : ModBuff
  {
    private readonly Mod calamity = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");

    public override void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.buffNoSave[this.Type] = false;
      Main.persistentBuff[this.Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
      for (int index = 0; index < BuffLoader.BuffCount; ++index)
      {
        if (Main.debuff[index])
        {
          player.buffImmune[index] = true;
          player.buffImmune[ModContent.Find<ModBuff>(this.calamity.Name, "RageMode").Type] = false;
          player.buffImmune[ModContent.Find<ModBuff>(this.calamity.Name, "AdrenalineMode").Type] = false;
          //player.buffImmune[this.mod.BuffType("ChtuxlagorInferno")] = false;
        }
      }
    }
  }
}
