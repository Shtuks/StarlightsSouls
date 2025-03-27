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

        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            //buggy
            /*if (type == ModContent.Find<ModBuff>("ssm", "ShtuxibusCurse").Type)
            {
                npc.life -= 10000;
            }
            if (type == ModContent.Find<ModBuff>(this.FargoSoul.Name, "MutantFangBuff").Type)
            {
                npc.life -= 5000;
            }
            if (type == ModContent.Find<ModBuff>(this.FargoSoul.Name, "AbomFangBuff").Type)
            {
                npc.life -= 1000;
            }
            if (type == ModContent.Find<ModBuff>(this.FargoSoul.Name, "DeviPresenceBuff").Type)
            {
                npc.life -= 500;
            }*/
        }
    }
}