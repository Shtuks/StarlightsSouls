using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Items.SummonItems;
using Fargowiltas.Items.Summons.SwarmSummons;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;

namespace ssm.Content.Items.Swarm.Summons
{
    public class OverloadedWaifu : SwarmSummonBase
    {
        public OverloadedWaifu() : base(ModContent.NPCType<SupremeCalamitas>(), 25)
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
            .AddIngredient<EyeofExtinction>()
            .Register();
        }
    }
}