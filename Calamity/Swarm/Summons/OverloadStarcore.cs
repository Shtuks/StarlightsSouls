using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.NPCs.AstrumDeus;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class OverloadDeus : SwarmSummonBase
    {
        public OverloadDeus() : base(ModContent.NPCType<AstrumDeusHead>(), 50, ModContent.ItemType<AstrumCor>())
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