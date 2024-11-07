using CalamityMod.CalPlayer;
using FargowiltasSouls;
using FargowiltasSouls.Content.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class ChtuxlagorInfernoEX : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
            Main.persistentBuff[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Shtun().ERASE(player);
        }
    }
}
