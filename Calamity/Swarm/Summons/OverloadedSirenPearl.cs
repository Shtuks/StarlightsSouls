using Terraria;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.Leviathan;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class OverloadAnahita : SwarmSummonBase
    {
        public OverloadAnahita() : base(ModContent.NPCType<Anahita>(), 50, ModContent.ItemType<SirensPearl>())
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