using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Boss;
using ssm.Content.Buffs;
using ssm.Content.NPCs.MutantEX;
using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class ShtunBuff : GlobalBuff
    {
        private readonly Mod FargoSoul = ModLoader.GetMod("FargowiltasSouls");

        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (type == ModContent.BuffType<MutantPresenceBuff>())
            {
                if (NPC.AnyNPCs(ModContent.NPCType<MutantBoss>()) || NPC.AnyNPCs(ModContent.NPCType<MutantEX>()))
                {
                    if (!player.HasBuff(ModContent.BuffType<DotBuff>()))
                    {
                        player.mount.Dismount(player);
                    }
                }
            }
        }
    }
}