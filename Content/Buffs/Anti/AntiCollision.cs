using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs.Anti
{
    public class AntiCollision : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = true;
            Main.buffNoTimeDisplay[this.Type] = true;
        }
    }
}