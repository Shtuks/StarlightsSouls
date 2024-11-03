using Terraria;
using Terraria.ModLoader;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.SlimeGod;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadSlimeGod : SwarmSummonBase
    {
        public OverloadSlimeGod() : base(ModContent.NPCType<SlimeGodCore>(), 50, ModContent.ItemType<OverloadedSludge>())
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