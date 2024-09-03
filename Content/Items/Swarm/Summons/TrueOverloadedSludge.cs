using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.Items.SummonItems;

namespace ssm.Content.Items.Swarm.Summons
{
    public class TrueOverloadedSludge : SwarmSummonBase
    {
        public TrueOverloadedSludge() : base(ModContent.NPCType<SlimeGodCore>(), "The underground mushrom fields trembling!", 25)
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }
    }
}