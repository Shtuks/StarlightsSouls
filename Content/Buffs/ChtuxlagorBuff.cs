using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class ChtuxlagorBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = true;
            Main.buffNoTimeDisplay[this.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
        }
    }
}