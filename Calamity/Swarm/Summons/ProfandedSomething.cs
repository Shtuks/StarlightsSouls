using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.ProfanedGuardians;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadGuardians : SwarmSummonBase
    {
        public OverloadGuardians() : base(ModContent.NPCType<ProfanedGuardianCommander>(), 50, ModContent.ItemType<ProfanedShard>())
        {
        }

        public override void SetStaticDefaults()
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
    }
}