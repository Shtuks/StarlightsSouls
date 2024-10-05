using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.Items.SummonItems;
using Fargowiltas.Items.Summons.SwarmSummons;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;

namespace ssm.Content.Items.Swarm.Summons
{
    public class OverloadedSirenPearl : SwarmSummonBase
    {
        public OverloadedSirenPearl() : base(ModContent.NPCType<Anahita>(), 25)
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
            .AddIngredient<SirensPearl>()
            .Register();
        }
    }
}