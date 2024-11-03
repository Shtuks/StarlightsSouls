using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.HiveMind;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadHive : SwarmSummonBase
    {
        public OverloadHive() : base(ModContent.NPCType<HiveMind>(), 50, ModContent.ItemType<Teratoma>())
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