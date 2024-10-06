using Microsoft.Xna.Framework;
using Terraria;
using ssm.Calamity.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Items.SummonItems;
using Fargowiltas.Items.Summons.SwarmSummons;
using FargowiltasCrossmod.Content.Calamity.Items.Summons;
using ssm.Core;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
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