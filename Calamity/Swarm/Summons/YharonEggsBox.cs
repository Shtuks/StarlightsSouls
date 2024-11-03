using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.Yharon;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadYharon : SwarmSummonBase
    {
        public OverloadYharon() : base(ModContent.NPCType<Yharon>(), 50, ModContent.ItemType<YharonEgg>())
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