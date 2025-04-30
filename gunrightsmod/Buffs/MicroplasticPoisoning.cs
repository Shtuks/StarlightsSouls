using Terraria;
using Terraria.ModLoader;

namespace ssm.gunrightsmod.Buffs
{
    public class MicroplasticPoisoning : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }

    public class MicroplasticGlobalNPC : GlobalNPC
    {
        public int microplasticStacks;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            microplasticStacks = 0;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<MicroplasticPoisoning>()))
            {
                int buffIndex = npc.FindBuffIndex(ModContent.BuffType<MicroplasticPoisoning>());
                int stack = npc.buffTime[buffIndex] / 60;
                microplasticStacks = stack >= 4 ? 4 : stack + 1;

                int dps = microplasticStacks * 2;
                npc.lifeRegen -= dps * 2;
                if (damage < dps)
                    damage = dps;
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (microplasticStacks > 0)
            {
                float vulnerability = 0.04f * microplasticStacks;
                modifiers.IncomingDamageMultiplier += vulnerability;
            }
        }
    }
}
