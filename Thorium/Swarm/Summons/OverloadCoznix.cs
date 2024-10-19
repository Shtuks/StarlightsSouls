using Microsoft.Xna.Framework;
using Terraria;
using ssm.Thorium.Swarm.Summons;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ssm.Content.Items.Materials;
using ssm.Content.Tiles;
using ssm.Core;
using ThoriumMod.NPCs.BossFallenBeholder;
using Fargowiltas.Items.Summons.SwarmSummons;
using ThoriumMod.Items.BossFallenBeholder;

namespace ssm.Thorium.Swarm.Summons
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OverloadCoznix : SwarmSummonBase
    {
        public OverloadCoznix() : base(ModContent.NPCType<FallenBeholder>(), 25)
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
            .AddIngredient<VoidLens>()
            .Register();
        }
    }
}