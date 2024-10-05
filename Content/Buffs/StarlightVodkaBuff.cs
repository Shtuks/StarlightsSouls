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
            Mod mod = Terraria.ModLoader.ModLoader.GetMod("CalamityMod");
            player.statDefense = player.statDefense += 100;
            player.GetDamage(DamageClass.Generic) += 100 / 100f;
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
            player.dash += 10;
            player.GetCritChance(DamageClass.Generic) *= 10f;
            object[] objArray = new object[3]
            {
        (object) "AddMaxStealth",
        (object) ((Entity) player).whoAmI,
        (object) 100f
            };
            mod.Call(objArray);
        }
    }
}