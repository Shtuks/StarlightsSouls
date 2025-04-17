using Terraria.ModLoader;
using Terraria;
using ssm.Core;
using FargowiltasSouls;

namespace ssm.Content.Buffs
{
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class NihilityPresenceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.FargoSouls().MutantPresence = true;
            player.FargoSouls().noDodge = true;
            player.FargoSouls().noSupersonic = true;
            player.FargoSouls().GrazeRadius *= 0.5f;
            player.moonLeech = true;
        }
    }
}
