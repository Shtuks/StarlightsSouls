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
}
