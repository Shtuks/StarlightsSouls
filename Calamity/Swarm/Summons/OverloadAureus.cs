using Terraria;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using ssm.Core;
using CalamityMod.Items.SummonItems;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadAureus : SwarmSummonBase
    {
        public OverloadAureus() : base(ModContent.NPCType<AstrumAureus>(), 50, ModContent.ItemType<AstralChunk>())
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