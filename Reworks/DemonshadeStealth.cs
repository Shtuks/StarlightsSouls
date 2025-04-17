using CalamityMod;
using CalamityMod.Buffs.Summon;
using Terraria;
using Terraria.ModLoader;
using ssm.Core;

namespace ssm.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeStealth : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (!player.HasBuff(ModContent.BuffType<DemonshadeSetDevilBuff>()))
                return;
            player.Calamity().rogueStealthMax += 0.1f;
            player.Calamity().wearingRogueArmor = true;
            player.Calamity().stealthStrikeHalfCost = true;
        }
    }
}
