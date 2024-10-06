using Microsoft.Xna.Framework;
using Terraria;
using ssm.Calamity.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.Items.SummonItems;
using Fargowiltas.Items.Summons.SwarmSummons;
using ssm.Core;

namespace ssm.Calamity.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OverloadSprout : SwarmSummonBase
    {
        public OverloadSprout() : base(ModContent.NPCType<Crabulon>(), 25)
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
            .AddIngredient<DecapoditaSprout>()
            .Register();
        }
    }
}