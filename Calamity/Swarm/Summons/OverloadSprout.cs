using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.Crabulon;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadCrabulon : SwarmSummonBase
    {
        public OverloadCrabulon() : base(ModContent.NPCType<Crabulon>(), 50, ModContent.ItemType<DecapoditaSprout>())
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