using FargowiltasSouls;
using FargowiltasSouls.Content.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ssm.Content.Buffs
{
    public class ChtuxlagorInferno : ModBuff
    {
        private readonly Mod Fargos = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[this.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
            Main.persistentBuff[this.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLife -= 745;
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
            player.ichor = true;
            player.onFire = true;
            player.onFire2 = true;
            player.onFrostBurn = true;
            player.poisoned = true;
            player.venom = true;
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
            npc.life = npc.life - 745;
            npc.lifeRegen -= 745;
            npc.defense = 0;
            npc.defDefense = 0;
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    npc.buffImmune[index] = false;
            }
        }
    }
}
