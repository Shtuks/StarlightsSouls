using Terraria;
using Terraria.ModLoader;

namespace ssm.gunrightsmod.Buffs
{
    public class SaltedWounds : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
        }
    }

    public class SaltedWoundsPlayer : ModPlayer
    {
        public int saltedWoundsStacks;

        public override void ResetEffects()
        {
            saltedWoundsStacks = 0;
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.HasBuff(ModContent.BuffType<SaltedWounds>()))
            {
                int buffIndex = player.FindBuffIndex(ModContent.BuffType<SaltedWounds>());
                int stack = player.buffTime[buffIndex] / 60;
                saltedWoundsStacks = stack >= 3 ? 3 : stack + 1;

                player.allDamage += 0.03f * saltedWoundsStacks;
                player.lifeRegen += 5 * saltedWoundsStacks;
            }
        }
    }

    public class SaltedWoundsGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void OnHitPlayer(NPC npc, Player player, ref int damage, ref bool crit)
        {
            base.OnHitPlayer(npc, player, ref damage, ref crit);

            if (player.HasBuff(ModContent.BuffType<SaltedWounds>()) < 3)
            {
                player.AddBuff(ModContent.BuffType<SaltedWounds>(), 600);
            }
        }
    }
}
