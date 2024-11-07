using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class ShtuxianDomination : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.ClearBuff(ModContent.BuffType<ChtuxlagorInferno>());
        }
    }
}