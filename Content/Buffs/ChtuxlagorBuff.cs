using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;

#nullable disable
namespace ssm.Content.Buffs
{
    public class ChtuxlagorBuff : ModBuff
    {
        public virtual void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = true;
            Main.buffNoTimeDisplay[this.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
        }

        public virtual void Update(Player player, ref int buffIndex)
        {
        }
    }
}