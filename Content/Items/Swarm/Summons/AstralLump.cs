using Microsoft.Xna.Framework;
using Terraria;
using ssm.Content.Items.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.Items.SummonItems;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using Fargowiltas.Items.Summons.SwarmSummons;

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

        public override void AddRecipes()
        {
            this.CreateRecipe(1)
            .AddIngredient<Overloader>()
            .AddIngredient<AstralLump>()
            .Register();
        }
    }
}