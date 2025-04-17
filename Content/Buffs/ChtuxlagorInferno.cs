using FargowiltasSouls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace ssm.Content.Buffs
{
    public class ChtuxlagorInferno : ModBuff
    {
        public const int TickNumber = 10000;
        public const int DPS = 100000;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLife -= player.statLifeMax2 / 10;
            player.endurance = 0.0f;
            player.GetDamage(DamageClass.Generic) *= 0.05f;
            player.GetCritChance(DamageClass.Generic) *= 0.05f;
            player.maxMinions = 1;
            player.maxTurrets = 1;
            player.manaCost += 0.9f;
            player.GetArmorPenetration(DamageClass.Generic) *= 0.05f;
            player.statManaMax2 = 10;
            player.moveSpeed -= 0.9f;
            player.immune = false;
            player.immuneNoBlink = false;
            player.immuneTime = 0;
            player.noFallDmg = false;
            player.wingTime = 0.0f;
            player.wingTimeMax = 0;
            player.rocketTime = 0;
            player.slow = true;
            player.moonLeech = true;
            player.FargoSouls().FlamesoftheUniverse = true;
            player.FargoSouls().Shadowflame = true;
            player.FargoSouls().Asocial = true;
            player.FargoSouls().MutantNibble = true;
            player.FargoSouls().Atrophied = true;
            player.FargoSouls().Kneecapped = true;
            player.FargoSouls().CurseoftheMoon = true;
            player.FargoSouls().Defenseless = true;
            player.FargoSouls().GodEater = true;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().MutantPresence = true;
            player.FargoSouls().Hexed = true;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().Infested = true;
            player.FargoSouls().Jammed = true;
            player.FargoSouls().DeathMarked = true;
            player.FargoSouls().MutantNibble = true;
            player.FargoSouls().OceanicMaul = true;
            player.FargoSouls().noSupersonic = true;
            player.FargoSouls().SqueakyToy = true;
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    player.buffImmune[index] = false;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.Shtun().chtuxlagorInferno < npc.buffTime[buffIndex])
            {
                npc.Shtun().chtuxlagorInferno = npc.buffTime[buffIndex];
            }

            npc.defense = 0;
            npc.defDefense = 0;

            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    npc.buffImmune[index] = false;
            }

            //npc.DelBuff(buffIndex);
            //buffIndex--;
        }
    }
}
