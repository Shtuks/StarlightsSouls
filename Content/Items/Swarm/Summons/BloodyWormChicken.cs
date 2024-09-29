using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.Perforator;
using CalamityMod.Items.SummonItems;

namespace ssm.Content.Items.Swarm.Summons
{
    public class BloodyWormChicken : SwarmSummonBase
    {
        public BloodyWormChicken() : base(ModContent.NPCType<PerforatorHive>(), 25)
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
    }
}