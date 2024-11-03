using Terraria;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using ssm.Core;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.SupremeCalamitas;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Crossmod.Name)]
    public class OverloadWaifu : SwarmSummonBase
    {
        public OverloadWaifu() : base(ModContent.NPCType<SupremeCalamitas>(), 50, ModContent.ItemType<EyeofExtinction>())
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