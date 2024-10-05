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
            player.ghost = true;
            player.dead = true;
            player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got out of the scope of this pixel 2D game."), 50000000, 10);
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
        }
    }
}
