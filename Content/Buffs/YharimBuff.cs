using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class YharimBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = false;
            Main.pvpBuff[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Mod mod = ModLoader.GetMod("CalamityMod");
            player.statDefense = player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += 10 / 100f;
            player.statLifeMax2 += 5;
            player.moveSpeed += 0.01f;
            player.attackCD -= 2;
            player.lifeRegen += 2;
            player.endurance += 0.05f;
            player.manaCost -= 0.05f;
            ++player.slotsMinions;
            player.breath += 50;
            player.lavaTime += 2;
            player.lifeSteal += 0.005f;
            player.jumpSpeedBoost += 0.1f;
            player.wingTime += 0.01f;
            player.maxFallSpeed += 0.1f;
            player.ammoBox = true;
            player.spaceGun = true;
            ++player.dash;
            player.GetCritChance(DamageClass.Generic) *= 1.01f;
            object[] objArray = new object[3]
            {
                (object) "AddMaxStealth",
                (object) ((Entity) player).whoAmI,
                (object) 0.2f
            };
            mod.Call(objArray);
        }
    }
}