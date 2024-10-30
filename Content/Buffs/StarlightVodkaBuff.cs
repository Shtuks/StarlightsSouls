using Terraria;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class StarlightVodkaBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = false;
            Main.pvpBuff[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense = player.statDefense += 70;
            player.GetDamage(DamageClass.Generic) += 70 / 100f;
            player.statLifeMax2 += 10;
            player.moveSpeed += 0.1f;
            player.attackCD -= 10;
            player.lifeRegen += 10;
            player.endurance += 0.3f;
            player.manaCost += 1f;
            player.slotsMinions += 5;
            player.lavaTime += 2;
            player.lifeSteal += 0.1f;
            player.jumpSpeedBoost += 0.1f;
            player.wingTime += 0.1f;
            player.maxFallSpeed += 0.1f;
            player.ammoBox = true;
            player.dash += 30;
            player.GetCritChance(DamageClass.Generic) *= 10f;
        }
    }
}