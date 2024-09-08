using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using FargowiltasSouls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using CalamityMod.CalPlayer;

namespace ssm.Content.Buffs
{
  public class ShtuxibusCurse : ModBuff
  {
    private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
    public override void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
      if (!Main.dedServ)
        {
          if (ShaderManager.TryGetFilter("FargowiltasSouls.FinalSpark", out ManagedScreenFilter filter))
            {
              filter.Activate();
              if (ShtunConfig.Instance.ForcedFilters && Main.WaveQuality == 0)
              Main.WaveQuality = 1;
            }
        }
      if(Main.zenithWorld){
        player.endurance = 0.0f;
        player.immuneTime = 0;
        player.immune = false;
        player.immuneNoBlink = false;
      }
      player.creativeGodMode = false;
      player.endurance /= 10f;
      player.statDefense = player.statDefense *= 0.5f;
      if(!Main.zenithWorld){player.chaosState = true;}
      player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "BerserkerInstallBuff").Type] = true;
      player.buffImmune[ModContent.Find<ModBuff>(this.FargoSoul.Name, "SouloftheMasochistBuff").Type] = true;
      player.carpet = false;
      player.mount.Dismount(player);
      player.FargoSouls().noDodge = true;
      player.FargoSouls().noSupersonic = true;
      player.FargoSouls().MutantPresence = true;
      player.FargoSouls().GrazeRadius *= 0.5f;
      player.moonLeech = true;
    }
  }
}