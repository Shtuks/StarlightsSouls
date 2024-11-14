using ssm.Content.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class DotBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Shtun().dotMount = true;
            player.mount.SetMount(ModContent.MountType<Dot>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}