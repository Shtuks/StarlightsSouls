using Terraria;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Perforator;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadPerforators : SwarmSummonBase
    {
        public OverloadPerforators() : base(ModContent.NPCType<PerforatorHive>(), 50, ModContent.ItemType<BloodyWormFood>())
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