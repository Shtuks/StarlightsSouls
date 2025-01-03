using Terraria;
using Terraria.ModLoader;

namespace ssm.SHTUK.Modules
{
    public class RessurectedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = false;
            Main.debuff[this.Type] = false;
        }
    }
}
