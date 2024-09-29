using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.Items.SummonItems;

namespace ssm.Content.Items.Swarm.Summons
{
    public class AstralLump : SwarmSummonBase
    {
        public AstralLump() : base(ModContent.NPCType<AstrumAureus>(), 25)
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
    }
}