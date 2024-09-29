using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.Items.SummonItems;

namespace ssm.Content.Items.Swarm.Summons
{
    public class PlagueTerminal : SwarmSummonBase
    {
        public PlagueTerminal() : base(ModContent.NPCType<PlaguebringerGoliath>(), 25)
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
    }
}