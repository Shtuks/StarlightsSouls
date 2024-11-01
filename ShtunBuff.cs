using Terraria;
using Terraria.ModLoader;

namespace ssm
{
    public class ShtunBuff : GlobalBuff
    {
        private readonly Mod FargoSoul = ModLoader.GetMod("FargowiltasSouls");

        //buggy
        /*public override void Update(int type, NPC npc, ref int buffIndex)
        {
            if (type == ModContent.Find<ModBuff>("ssm", "ShtuxibusCurse").Type)
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
            }
        }*/
    }
}