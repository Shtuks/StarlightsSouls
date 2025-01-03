using Terraria;
using Terraria.ModLoader;

namespace ssm.SHTUK
{
    public class CyborgBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
            Main.persistentBuff[this.Type] = true;
        }
    }
}
