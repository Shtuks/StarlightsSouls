using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using FargowiltasSouls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;

namespace ssm.Content.Buffs
{
    public class ShtuxianDomination : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = true;
            Main.buffNoSave[this.Type] = true;
            Main.buffNoTimeDisplay[this.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShtunPlayer>().shtuxianDomination = true;
        }
    }
}