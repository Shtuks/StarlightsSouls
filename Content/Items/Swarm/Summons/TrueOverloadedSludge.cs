using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.Items.SummonItems;
using Fargowiltas.Items.Summons.SwarmSummons;

namespace ssm.Content.Items.Swarm.Summons
{
    public class TrueOverloadedSludge : SwarmSummonBase
    {
        public TrueOverloadedSludge() : base(ModContent.NPCType<SlimeGodCore>(), 25)
        {
        }

        public override bool CanUseItem(Player player)
        {
            return !ssm.SwarmActive;
        }

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<Overloader>()
            .AddIngredient<OverloadedSludge>()
            .Register();
        }
    }
}