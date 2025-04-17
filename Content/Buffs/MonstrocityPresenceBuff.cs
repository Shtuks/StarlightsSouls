using FargowiltasSouls;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class MonstrocityPresenceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.Shtun().MonstrocityPresence = true;
            player.FargoSouls().OceanicMaul = true;
            player.FargoSouls().TinEternityDamage = 0;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().noSupersonic = true;
            player.FargoSouls().MutantPresence = true;
            player.FargoSouls().GrazeRadius *= 0.5f;
            player.moonLeech = true;
        }
    }
}
